using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_ContractState_conState : NBasePage, IRequiresSessionState
{
	private PayoutContract payoutContract = new PayoutContract();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindDrop();
			this.DataBindContracts();
		}
	}
	private void DataBindDrop()
	{
		WebUtil.DataBindBalanceMode(this.dropBalanceMode);
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindContracts();
	}
	private void DataBindContracts()
	{
		DataTable dataSource = new DataTable();
		string text = "Con_Payout_Contract.IsArchived = 0  and flowState=1 ";
		if (this.txtContractCode.Text != "")
		{
			text = text + " and ContractCode like '%" + this.txtContractCode.Text.ToString().Trim() + "%'";
		}
		if (this.txtContractName.Text != "")
		{
			text = text + " and ContractName like '%" + this.txtContractName.Text.Trim() + "%'";
		}
		if (this.txtConType.Text.Trim() != "")
		{
			text = text + " and Con_ContractType.TypeName LIKE '%" + this.txtConType.Text.Trim() + "%'";
		}
		if (this.txtProject.Text.Trim() != "")
		{
			text = text + " and PT_PrjInfo.PrjName like '%" + this.txtProject.Text.Trim() + "%'";
		}
		if (this.ddlState.SelectedValue != "-1")
		{
			text = text + " and conState=" + this.ddlState.SelectedValue;
		}
		if (this.txtBName.Text != "")
		{
			text = text + " and CorpName like '%" + this.txtBName.Text.Trim() + "%'";
		}
		if (this.dropBalanceMode.SelectedIndex != -1 && this.dropBalanceMode.SelectedIndex != 0)
		{
			text = text + " and BalanceMode=" + this.dropBalanceMode.SelectedValue;
		}
		if (this.txtSignDate.Text != "")
		{
			text = text + " and SignDate='" + this.txtSignDate.Text.Trim() + "'";
		}
		System.Collections.Generic.List<PayoutContractModel> list = this.payoutContract.GetList(text, base.UserCode);
		if (list == null || list.Count < 1)
		{
			this.AspNetPager1.RecordCount = 1;
		}
		else
		{
			this.AspNetPager1.RecordCount = list.Count;
		}
		dataSource = this.payoutContract.GetList(text, base.UserCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvwContract.DataSource = dataSource;
		this.gvwContract.DataBind();
		decimal d = 0m;
		for (int i = 0; i < list.Count; i++)
		{
			d += System.Convert.ToDecimal(list[i].ModifiedMoney);
		}
		string[] value = new string[]
		{
			d.ToString()
		};
		int[] index = new int[]
		{
			5
		};
		GridViewUtility.AddTotalRow(this.gvwContract, value, index);
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["isMainContract"] = this.gvwContract.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["prjGuid"] = this.gvwContract.DataKeys[e.Row.RowIndex].Values[2].ToString();
			Label label = e.Row.FindControl("labState") as Label;
			string text = this.gvwContract.DataKeys[e.Row.RowIndex].Values[4].ToString();
			string key;
			switch (key = text)
			{
			case "0":
				label.Text = "执 行";
				goto IL_237;
			case "1":
				label.Text = "中 止";
				goto IL_237;
			case "2":
				label.Text = "保 内";
				goto IL_237;
			case "3":
				label.Text = "保 外";
				goto IL_237;
			case "4":
				label.Text = "解 除";
				goto IL_237;
			case "5":
				label.Text = "终 止";
				goto IL_237;
			}
			label.Text = "----";
			IL_237:
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + text + "');";
		}
	}
	protected void btnConState_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvwContract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvwContract.Rows[i].FindControl("chk") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvwContract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.payoutContract.updateConState(list, 0);
				this.DataBindContracts();
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n修改失败！');");
		}
	}
	protected void btnpansul_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvwContract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvwContract.Rows[i].FindControl("chk") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvwContract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.payoutContract.updateConState(list, 1);
				this.DataBindContracts();
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n修改失败！');");
		}
	}
	protected void btnbn_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvwContract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvwContract.Rows[i].FindControl("chk") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvwContract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.payoutContract.updateConState(list, 2);
				this.DataBindContracts();
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n修改失败！');");
		}
	}
	protected void btnbw_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvwContract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvwContract.Rows[i].FindControl("chk") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvwContract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.payoutContract.updateConState(list, 3);
				this.DataBindContracts();
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n修改失败！');");
		}
	}
	protected void btnjc_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvwContract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvwContract.Rows[i].FindControl("chk") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvwContract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.payoutContract.updateConState(list, 4);
				this.DataBindContracts();
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n修改失败！');");
		}
	}
	protected void btnzz_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvwContract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvwContract.Rows[i].FindControl("chk") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvwContract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.payoutContract.updateConState(list, 5);
				this.DataBindContracts();
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n修改失败！');");
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.DataBindContracts();
	}
}


