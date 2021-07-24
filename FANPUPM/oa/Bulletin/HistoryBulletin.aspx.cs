using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class HistoryBulletin : BasePage, IRequiresSessionState
	{

		public new string UserCode
		{
			get
			{
				object obj = this.ViewState["UserCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["UserCode"] = value;
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UserCode = this.Session["yhdm"].ToString();
				this.data_bind();
			}
		}
		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowIndex > -1 && e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string text = this.GridView1.DataKeys[e.Row.RowIndex]["I_BULLETINID"].ToString();
				string text2 = ((DataRowView)e.Row.DataItem)["AuditState"].ToString();
				e.Row.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);ClickRow('",
					text,
					"','",
					text2,
					"');"
				});
				string str = "BulletinView.aspx?rid=" + text;
				e.Row.Attributes["ondblclick"] = "javascript:window.showModalDialog('" + str + "', window, 'dialogHeight:450px;dialogWidth:600px;center:1;help:0;status:0;');";
			}
		}
		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.data_bind();
		}
		private void data_bind()
		{
			string text = " and ( DTM_EXPRIESDATE<'" + System.DateTime.Today.ToShortDateString() + "') AND AuditState = '1' ";
			string selectedValue = this.ddlClass.SelectedValue;
			string text2 = this.tbKey.Text.Trim();
			if (selectedValue != "")
			{
				if (this.ddlClass.SelectedItem.Text == "发布范围")
				{
					if (this.ddlnw.SelectedItem.Text == "内部")
					{
						text = text + " AND (" + selectedValue + " = 1) ";
					}
					else
					{
						text = text + " AND (" + selectedValue + " = 2) ";
					}
				}
				else
				{
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						" AND (",
						selectedValue,
						" like '%",
						text2.ToString(),
						"%') "
					});
				}
			}
			if (this.txtBeginDate.Text != "")
			{
				text = text + " AND ( [DTM_RELEASETIME] >= '" + this.txtBeginDate.Text + "' ";
				if (this.txtEndDate.Text != "")
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						" AND [DTM_RELEASETIME] < '",
						System.Convert.ToDateTime(this.txtEndDate.Text).AddDays(1.0),
						"') "
					});
				}
				else
				{
					text += ") ";
				}
			}
			else
			{
				if (this.txtEndDate.Text != "")
				{
					object obj2 = text;
					text = string.Concat(new object[]
					{
						obj2,
						" AND ( [DTM_RELEASETIME] <= '",
						System.Convert.ToDateTime(this.txtEndDate.Text).AddDays(1.0),
						"')"
					});
				}
			}
			this.GridView1.DataSource = BulletionAction.ReturnTable(this.UserCode, text);
			this.GridView1.DataBind();
		}
		protected void ddlClass_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.ddlClass.SelectedItem.Text == "发布范围")
			{
				this.ddlnw.Style.Remove("display");
				this.tbKey.Style.Add("display", "none");
				return;
			}
			this.ddlnw.Style.Add("display", "none");
			this.tbKey.Style.Remove("display");
		}
		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			this.GridView1.PageIndex = e.NewPageIndex;
			this.data_bind();
		}
	}


