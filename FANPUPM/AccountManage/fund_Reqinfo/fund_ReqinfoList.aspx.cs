using AccountManage.BLL;
using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_fund_Reqinfo_fund_ReqinfoList : NBasePage, IRequiresSessionState
{
	private fund_ReqinfoBll fund_ReqinfoBll = new fund_ReqinfoBll();

	protected override void OnInit(EventArgs e)
	{
		this.gvMony.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
			this.Pro1.Visible = false;
			this.Pro2.Visible = false;
		}
	}
	public void BindGv()
	{
		string text = "";
		if (this.DroType.SelectedValue == "0")
		{
			text += " and IsContr=0  ";
			text += ((this.txtContr.Value.Trim() == "") ? "" : ("  and  PrjNum like '%" + this.HiddenField1.Value.Trim() + "%'"));
		}
		else
		{
			text += " and IsContr!=0 ";
			text += ((this.txtProject.Value.Trim() == "") ? "" : ("  and PrjNum like '%" + this.hdnProjectCode.Value.Trim() + "%'"));
		}
		text += ((this.Monycode.Text.Trim() == "") ? "" : (" and reqNum like '%" + this.Monycode.Text.Trim() + "%'"));
		text += ((this.txtStartContractPrice.Text.Trim() == "") ? "" : (" and amount > ='" + Convert.ToDecimal(this.txtStartContractPrice.Text.Trim()) + "'"));
		text += ((this.txtEndContractPrice.Text.Trim() == "") ? "" : (" and amount <= '" + Convert.ToDecimal(this.txtEndContractPrice.Text.Trim()) + "'"));
		text += ((this.txtStartSignedTime.Text.Trim() == "") ? "" : (" and reqDate >= '" + Convert.ToDateTime(this.txtStartSignedTime.Text.Trim()) + "'"));
		text += ((this.txtEndSignedTime.Text.Trim() == "") ? "" : (" and reqDate <= '" + Convert.ToDateTime(this.txtEndSignedTime.Text.Trim()) + "'"));
		if (this.Session["yhdm"].ToString() != "00000000")
		{
			text = text + " and reqPeopNum like '" + base.UserCode + "'";
		}
		this.BindGv(this.fund_ReqinfoBll.GetListWhere(text));
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvMony.DataSource = storageDataSource;
			this.gvMony.DataBind();
			this.gvMony.HeaderRow.Cells[0].Visible = false;
			this.gvMony.Rows[0].Visible = false;
			return;
		}
		this.gvMony.DataSource = storageDataSource;
		this.gvMony.DataBind();
	}
	protected string GetProName(string pronum)
	{
		if (string.IsNullOrEmpty(pronum))
		{
			return "错误项目编号";
		}
		DataTable dataTable = SqlHelper.ExecuteQuery(CommandType.Text, "select *from dbo.PT_PrjInfo where PrjGuid='" + pronum.Trim() + "'", new SqlParameter[0]);
		if (dataTable.Rows.Count == 1)
		{
			return dataTable.Rows[0]["PrjName"].ToString();
		}
		return "错误项目编号";
	}
	protected string GetTypeName(string type)
	{
		if (type != null)
		{
			if (type == "0")
			{
				return "启动资金";
			}
			if (type == "1")
			{
				return "合同款";
			}
			if (type == "2")
			{
				return "拆借";
			}
			if (type == "3")
			{
				return "其它";
			}
		}
		return "启动资金";
	}
	protected string GetContrName(string contrnum)
	{
		PayoutContract payoutContract = new PayoutContract();
		PayoutContractModel model = payoutContract.GetModel(contrnum);
		if (model != null)
		{
			return contrnum = ((model.ContractName == null) ? "无" : model.ContractName);
		}
		return "无";
	}
	protected string GetWhich(string contrnum)
	{
		if (this.DroType.SelectedValue == "0")
		{
			return this.GetContrName(contrnum);
		}
		return this.GetProName(contrnum);
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				string message = "";
				List<string> list = new List<string>();
				foreach (GridViewRow gridViewRow in this.gvMony.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						if (!this.fund_ReqinfoBll.Delete(checkBox.ToolTip))
						{
							list.Clear();
							message = "alert('系统提示：\\n\\n请先删除与收款合同相关的其他数据！')";
							break;
						}
						list.Add(checkBox.ToolTip);
						message = "alert('系统提示：\\n\\n数据删除成功！');location='fund_ReqinfoList.aspx'";
					}
				}
				base.RegisterScript(message);
				sqlTransaction.Commit();
				this.BindGv();
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起删除失败！');");
			}
		}
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvMony.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvMony.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.gvMony.DataKeys[e.Row.RowIndex].Values[0].ToString();
				e.Row.Attributes["bstate"] = this.gvMony.DataKeys[e.Row.RowIndex].Values[1].ToString();
				e.Row.Attributes["bstate"].ToString();
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
	protected void DroType_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (this.DroType.SelectedValue == "0")
		{
			this.Pro1.Visible = false;
			this.Pro2.Visible = false;
			this.Pro3.Visible = true;
			this.Pro4.Visible = true;
			this.gvMony.Columns[3].HeaderText = "合同名称";
		}
		else
		{
			this.Pro3.Visible = false;
			this.Pro4.Visible = false;
			this.Pro1.Visible = true;
			this.Pro2.Visible = true;
			this.gvMony.Columns[3].HeaderText = "项目名称";
		}
		this.BindGv();
	}
}


