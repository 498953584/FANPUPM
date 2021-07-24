using ASP;
using cn.justwin.AccountManage.accBaise;
using cn.justwin.AccountManage.prjaccount;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using cn.justwin.stockBLL.AccountManage.accBaise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_acc_Manage_accountList : NBasePage, IRequiresSessionState
{
	private accountMange am = new accountMange();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hidSel.Value = "";
			this.Bind("");
			this.btnAdd.Attributes.Add("onclick", "if(!rowEdit()){return false;}");
			this.btnEdit.Attributes.Add("onclick", "if(!attributeEdit()){return false;}");
		}
	}
	public void Bind(string selwhere)
	{
		accBaise accBaise = new accBaise();
		basieModel model = accBaise.GetModel(1);
		this.hdfaccAllot.Value = model.accAllot.ToString();
		this.hdflimits.Value = model.authority.ToString();
		this.gvwContract.DataSource = this.am.GetAcclist(selwhere, base.UserCode);
		this.gvwContract.DataBind();
		this.contract.Visible = true;
	}
	protected void gvwContract_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwContract.PageIndex = e.NewPageIndex;
		this.Bind(this.hidSel.Value);
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			string a = e.Row.Cells[4].Text.ToString();
			if (a == "0")
			{
				e.Row.Cells[4].Text = "否";
				return;
			}
			if (a == "1")
			{
				e.Row.Cells[4].Text = "是";
			}
		}
	}
	protected void btnActive_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				List<prjaccountModel> list = new List<prjaccountModel>();
				for (int i = 0; i < this.gvwContract.Rows.Count; i++)
				{
					CheckBox checkBox = (CheckBox)this.gvwContract.Rows[i].FindControl("chkAcc");
					if (checkBox.Checked)
					{
						list.Add(new prjaccountModel
						{
							activeData = new DateTime?(DateTime.Now),
							activeMan = base.UserCode,
							isActivity = new int?(1),
							accountID = new Guid(this.gvwContract.DataKeys[i].Value.ToString())
						});
					}
				}
				this.am.upActivity(sqlTransaction, list);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("alert('系统提示：\\n\\激活成功！');location='accountList.aspx';").Append(Environment.NewLine);
				base.RegisterScript(stringBuilder.ToString());
				sqlTransaction.Commit();
			}
			catch
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n激活失败！');");
			}
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			for (int i = 0; i < this.gvwContract.Rows.Count; i++)
			{
				CheckBox checkBox = (CheckBox)this.gvwContract.Rows[i].FindControl("chkAcc");
				if (checkBox.Checked)
				{
					text = text + ",'" + this.gvwContract.DataKeys[i].Value.ToString() + "'";
				}
			}
			this.am.upIsnullify(text);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("alert('系统提示：\\n\\注销成功！');location='accountList.aspx';").Append(Environment.NewLine);
			base.RegisterScript(stringBuilder.ToString());
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n注销失败！');");
		}
	}
	protected void btnSel_Click(object sender, EventArgs e)
	{
		string text = " where 1=1";
		if (this.txtCode.Text.ToString().Trim() != "")
		{
			text = text + "  and accountNum like '%" + this.txtCode.Text + "%'";
		}
		if (this.txtName.Text.ToString().Trim() != "")
		{
			text = text + " and acountName like '%" + this.txtName.Text + "%'";
		}
		this.Bind(text);
	}
}


