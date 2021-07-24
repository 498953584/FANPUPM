using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ForAuditingList : BasePage, IRequiresSessionState
	{
		public OAWFApplyItemAction hrAction
		{
			get
			{
				return new OAWFApplyItemAction();
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.btnAudit.Attributes["onclick"] = "OpenWin();";
				this.DDLBusinessClass.DataBind();
				this.DDLBind();
				SqlDataSource expr_39 = this.SqlAuditingList;
				expr_39.SelectCommand += " order by starttime desc";
			}
		}
		protected void DDLBind()
		{
			this.DDLBusinessClass.Items.Insert(0, new ListItem("所有类别", "所有类别"));
		}
		protected void gvAuditingList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string text = this.gvAuditingList.DataKeys[e.Row.RowIndex]["InstanceCode"].ToString();
				string text2 = this.gvAuditingList.DataKeys[e.Row.RowIndex]["NoteID"].ToString();
				string text3 = this.gvAuditingList.DataKeys[e.Row.RowIndex]["IsAllPass"].ToString();
				string text4 = this.gvAuditingList.DataKeys[e.Row.RowIndex]["NodeID"].ToString();
				string text5 = this.gvAuditingList.DataKeys[e.Row.RowIndex]["BusinessCode"].ToString();
				string text6 = dataRowView["BusinessClass"].ToString();
				string text7 = FlowAuditAction.DoWithUrl(text5);
				if (text7 == "about:blank")
				{
					text7 = "";
				}
				e.Row.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);ClickRow('",
					text5,
					"','",
					text,
					"','",
					text2,
					"','",
					text3,
					"','",
					text4,
					"','",
					text7,
					"','",
					text6,
					"');"
				});
				if (dataRowView["BusinessCode"].ToString() == "999")
				{
					OAWFApplyItem model = this.hrAction.GetModel((System.Guid)dataRowView["InstanceCode"]);
					if (model != null)
					{
						e.Row.Cells[1].Text = model.Title;
					}
				}
			}
		}
		protected void btnAudit_Click(object sender, System.EventArgs e)
		{
			this.gvAuditingList.DataBind();
		}
		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			if (this.DDLBusinessClass.SelectedValue != "所有类别")
			{
				SqlDataSource expr_1D = this.SqlAuditingList;
				string selectCommand = expr_1D.SelectCommand;
				expr_1D.SelectCommand = string.Concat(new string[]
				{
					selectCommand,
					" and BusinessCode = '",
					this.DDLBusinessClass.SelectedValue.Substring(0, 3),
					"'and a.BusinessClass =  '",
					this.DDLBusinessClass.SelectedValue.Substring(3, 3),
					"'"
				});
			}
			if (this.txtTemplateCode.Text != "")
			{
				SqlDataSource expr_99 = this.SqlAuditingList;
				expr_99.SelectCommand = expr_99.SelectCommand + " and TemplateID in (select TemplateID from WF_Template where TemplateCode like '%" + this.txtTemplateCode.Text + "%')";
			}
			if (this.DBDate.Text != "")
			{
				SqlDataSource expr_DB = this.SqlAuditingList;
				expr_DB.SelectCommand = expr_DB.SelectCommand + " and datediff(dd,StartTime,'" + this.DBDate.Text + "')=0 ";
			}
			if (this.txtUserCode.Text != "")
			{
				SqlDataSource expr_11D = this.SqlAuditingList;
				expr_11D.SelectCommand = expr_11D.SelectCommand + " and Organiger in (SELECT v_yhdm FROM PT_yhmc where v_xm like '%" + this.txtUserCode.Text + "%')";
			}
			SqlDataSource expr_148 = this.SqlAuditingList;
			expr_148.SelectCommand += " order by starttime desc";
			this.SqlAuditingList.DataBind();
		}
	}


