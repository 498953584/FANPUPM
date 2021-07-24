using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_ContractState_IncometState : NBasePage, IRequiresSessionState
{
	private DataTable dtCount = new DataTable();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private string IsApprove = ConfigHelper.Get("IsIncomeContractApprove");

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
			this.BindGv();
		}
	}
	private void DataBindDrop()
	{
		WebUtil.DataBindBalanceMode(this.dropBalanceMode);
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	public void BindGv()
	{
		string text = " and IsArchived='false'";
		if (this.ddlState.SelectedValue != "-1")
		{
			text = text + " and conState=" + this.ddlState.SelectedValue;
		}
		if (this.IsApprove == "1")
		{
			text += " and flowState=1 ";
		}
		if (this.ddlSignSate.SelectedValue != "-1")
		{
			text = text + " and sign=" + this.ddlSignSate.SelectedValue;
		}
		if (this.txtAName.Text.Trim() != "")
		{
			text = text + " and Party like '%" + this.txtAName.Text.Trim() + "%'";
		}
		if (this.dropBalanceMode.SelectedIndex != -1 && this.dropBalanceMode.SelectedIndex != 0)
		{
			text = text + " and BalanceMode=" + this.dropBalanceMode.SelectedValue;
		}
		if (this.txtSignDate.Text != "")
		{
			text = text + " and SignedTime='" + this.txtSignDate.Text.Trim() + "'";
		}
		this.dtCount = this.incometContractBll.GetTbByParam(this.txtContractCode.Text.ToString().Trim(), this.txtContractName.Text.ToString().Trim(), this.txtConType.Text.Trim(), "", "", "", "", this.txtProject.Value.ToString(), base.UserCode, text);
		this.AspNetPager1.RecordCount = this.dtCount.Rows.Count;
		this.BindGv(this.incometContractBll.GetTbByParamSort(this.txtContractCode.Text.ToString().Trim(), this.txtContractName.Text.ToString().Trim(), this.txtConType.Text.Trim(), "", "", "", "", this.txtProject.Value.ToString(), base.UserCode, text, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize));
		this.btnConState.Text = "执行";
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvConract.DataSource = storageDataSource;
			this.gvConract.DataBind();
			this.gvConract.HeaderRow.Cells[0].Visible = false;
			this.gvConract.Rows[0].Visible = false;
			return;
		}
		this.gvConract.DataSource = storageDataSource;
		this.gvConract.DataBind();
		decimal d = 0m;
		for (int i = 0; i < this.dtCount.Rows.Count; i++)
		{
			d += System.Convert.ToDecimal(WebUtil.GetEnPrice(this.dtCount.Rows[i]["ContractPrice"].ToString(), this.dtCount.Rows[i]["ContractId"].ToString()));
		}
		string[] value = new string[]
		{
			d.ToString()
		};
		int[] index = new int[]
		{
			4
		};
		GridViewUtility.AddTotalRow(this.gvConract, value, index);
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
				if (this.gvConract.DataKeys[e.Row.RowIndex].Values[1].ToString() == "False")
				{
					e.Row.Attributes["bstate"] = "False";
				}
				e.Row.Attributes["isMainContract"] = this.gvConract.DataKeys[e.Row.RowIndex].Values[1].ToString();
				Label label = e.Row.FindControl("labState") as Label;
				string text = ((DataRowView)e.Row.DataItem)["conState"].ToString();
				string key;
				switch (key = text)
				{
				case "0":
					label.Text = "执行";
					goto IL_202;
				case "1":
					label.Text = "中止";
					goto IL_202;
				case "2":
					label.Text = "保内";
					goto IL_202;
				case "3":
					label.Text = "保外";
					goto IL_202;
				case "4":
					label.Text = "解除";
					goto IL_202;
				case "5":
					label.Text = "终止";
					goto IL_202;
				}
				label.Text = "";
				IL_202:
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + text + "');";
			}
			catch
			{
			}
		}
	}
	public string GetParty(string party)
	{
		if (!string.IsNullOrEmpty(party))
		{
			return party.Split(new char[]
			{
				','
			})[1];
		}
		return "";
	}
	protected void btnConState_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvConract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvConract.Rows[i].FindControl("cbBox") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvConract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.incometContractBll.updateConState(list, 0);
				this.BindGv();
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('修改失败！');");
		}
	}
	protected void btnpansul_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvConract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvConract.Rows[i].FindControl("cbBox") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvConract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.incometContractBll.updateConState(list, 1);
				this.BindGv();
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('修改失败！');");
		}
	}
	protected void btnbn_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvConract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvConract.Rows[i].FindControl("cbBox") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvConract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.incometContractBll.updateConState(list, 2);
				this.BindGv();
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('修改失败！');");
		}
	}
	protected void btnbw_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvConract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvConract.Rows[i].FindControl("cbBox") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvConract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.incometContractBll.updateConState(list, 3);
				this.BindGv();
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('修改失败！');");
		}
	}
	protected void btnjc_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvConract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvConract.Rows[i].FindControl("cbBox") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvConract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.incometContractBll.updateConState(list, 4);
				this.BindGv();
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('修改失败！');");
		}
	}
	protected void btnzz_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 0; i < this.gvConract.Rows.Count; i++)
			{
				CheckBox checkBox = this.gvConract.Rows[i].FindControl("cbBox") as CheckBox;
				if (checkBox.Checked)
				{
					string item = this.gvConract.DataKeys[i].Values[0].ToString();
					list.Add(item);
				}
			}
			if (list.Count != 0)
			{
				this.incometContractBll.updateConState(list, 5);
				this.BindGv();
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('修改失败！');");
		}
	}
	protected void btnSaveSignInfo_Click(object sender, System.EventArgs e)
	{
		string text = this.hfldContractId.Value.Trim();
		if (!string.IsNullOrEmpty(text))
		{
			IncometContractModel model = this.incometContractBll.GetModel(text);
			string value = this.hfldSignedDate.Value;
			if (string.IsNullOrEmpty(value))
			{
				base.RegisterScript("top.ui.alert('签约日期不能为空！');");
				return;
			}
			model.SignedTime = new System.DateTime?(System.Convert.ToDateTime(value));
			string value2 = this.hfldReFundDate.Value;
			if (string.IsNullOrEmpty(value2))
			{
				model.ReFundDate = null;
			}
			else
			{
				model.ReFundDate = new System.DateTime?(System.Convert.ToDateTime(value2));
			}
			if (model.SignPepole.Trim() == "")
			{
				model.SignPepole = null;
			}
			model.Remark = this.hfldRemark.Value;
			model.Sign = 1;
			this.incometContractBll.Update(model);
		}
		this.BindGv();
		base.RegisterScript("top.ui.show('保存成功');");
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
}


