using AccountManage.BLL;
using AccountManage.Model;
using ASP;
using cn.justwin.AccountManage.prjaccount;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using com.jwsoft.pm.entpm;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_AccountOperate_AccountOperate : NBasePage, IRequiresSessionState
{
	private fund_AccountOperateBLL bll = new fund_AccountOperateBLL();
	private fund_accountInfoBLL Addbll = new fund_accountInfoBLL();

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
		}
	}
	protected string GetAccount(string accounum)
	{
		accountMange accountMange = new accountMange();
		prjaccountModel prjaccountModel = new prjaccountModel();
		if (string.IsNullOrEmpty(accounum))
		{
			return "";
		}
		prjaccountModel = accountMange.GetModelByConId(accounum);
		if (prjaccountModel != null)
		{
			return prjaccountModel.accountNum;
		}
		return "";
	}
	public void BindGv()
	{
		string text = " 1=1 ";
		text += ((this.Monycode.Text.Trim() == "") ? "" : (" and Acredence like '%" + this.Monycode.Text.Trim() + "%'"));
		text += ((this.txtStartContractPrice.Text.Trim() == "") ? "" : (" and AccountMony > ='" + Convert.ToDecimal(this.txtStartContractPrice.Text.Trim()) + "'"));
		text += ((this.txtEndContractPrice.Text.Trim() == "") ? "" : (" and AccountMony <= '" + Convert.ToDecimal(this.txtEndContractPrice.Text.Trim()) + "'"));
		text += ((this.txtStartSignedTime.Text.Trim() == "") ? "" : (" and SumiTime >= '" + Convert.ToDateTime(this.txtStartSignedTime.Text.Trim()) + "'"));
		text += ((this.txtEndSignedTime.Text.Trim() == "") ? "" : (" and SumiTime <= '" + Convert.ToDateTime(this.txtEndSignedTime.Text.Trim()) + "'"));
		if (this.Session["yhdm"].ToString() != "00000000")
		{
			text = text + "  and  SumitMan like '%" + base.UserCode + "%' ";
		}
		this.BindGv(this.bll.GetList(text));
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
		if (string.IsNullOrEmpty(contrnum))
		{
			return "无";
		}
		PayoutContractModel model = payoutContract.GetModel(contrnum);
		if (model != null)
		{
			return contrnum = ((model.ContractName == null) ? "无" : model.ContractName);
		}
		return "无";
	}
	protected void BtnInAccount(object sender, EventArgs e)
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
					fund_accountInfoModle fund_accountInfoModle = new fund_accountInfoModle();
					fund_AccountOperateModle fund_AccountOperateModle = new fund_AccountOperateModle();
					if (checkBox != null && checkBox.Checked)
					{
						try
						{
							fund_AccountOperateModle = this.bll.GetModel(int.Parse(checkBox.ToolTip));
							fund_accountInfoModle.accountNum = fund_AccountOperateModle.AccountNum;
							fund_accountInfoModle.isValid = new int?(0);
							if (Convert.ToDecimal(((TextBox)gridViewRow.FindControl("tbxmony")).Text) > 0m)
							{
								fund_accountInfoModle.opClass = new int?(0);
							}
							else
							{
								fund_accountInfoModle.opClass = new int?(1);
							}
							int id = int.Parse(checkBox.ToolTip);
							fund_accountInfoModle.opMoney = new decimal?(Math.Abs(Convert.ToDecimal(((TextBox)gridViewRow.FindControl("tbxmony")).Text)));
							DateTime now = DateTime.Now;
							fund_accountInfoModle.opTime = new DateTime?(now);
							fund_accountInfoModle.opType = fund_AccountOperateModle.AccounType;
							fund_accountInfoModle.sysPeop = base.UserCode.ToString();
							fund_accountInfoModle.opMan = base.UserCode.ToString();
							if (this.Addbll.Add(fund_accountInfoModle) <= 0 || !this.bll.UpdateBoth(base.UserCode, now, Convert.ToDecimal(((TextBox)gridViewRow.FindControl("tbxmony")).Text), id))
							{
								list.Clear();
								message = "alert('系统提示：\\n\\入账失败！')";
								break;
							}
							list.Add(checkBox.ToolTip);
							message = "alert('系统提示：\\n\\n入账成功！');location='AccountOperate.aspx'";
						}
						catch
						{
							list.Clear();
							message = "alert('系统提示：\\n\\入账失败！')";
						}
					}
				}
				base.RegisterScript(message);
				sqlTransaction.Commit();
				this.BindGv();
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起入账失败！');");
			}
		}
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
						if (!this.bll.Delete(int.Parse(checkBox.ToolTip)))
						{
							list.Clear();
							message = "alert('系统提示：\\n\\n请先删除与收款合同相关的其他数据！')";
							break;
						}
						list.Add(checkBox.ToolTip);
						message = "alert('系统提示：\\n\\n数据删除成功！');location='AccountOperate.aspx'";
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
	protected string GetAccounType(string type)
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
				e.Row.Attributes["id"] = this.gvMony.DataKeys[e.Row.RowIndex].Values[0].ToString();
				e.Row.Attributes["guid"] = this.gvMony.DataKeys[e.Row.RowIndex].Values[0].ToString();
				e.Row.Attributes["bstate"] = ((this.gvMony.DataKeys[e.Row.RowIndex].Values[2].ToString() == "0") ? "False" : "True");
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
	protected new string GetUserName(string usercode)
	{
		return PageHelper.QueryUser(this, usercode);
	}
	protected string GetDeptName(string deptid)
	{
		if (!string.IsNullOrEmpty(deptid))
		{
			return PageHelper.QueryDept(this, int.Parse(deptid));
		}
		return "";
	}
}


