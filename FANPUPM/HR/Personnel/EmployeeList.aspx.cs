using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EmployeeList : BasePage, IRequiresSessionState
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
				return string.Empty;
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
				this.UserCode = base.Request.QueryString["cc"];
				this.btnAdd.Attributes["onclick"] = "openEmployeeEdit(1,'" + this.UserCode + "')";
				this.btnEdit.Attributes["onclick"] = "openEmployeeEdit(2,'" + this.UserCode + "')";
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.gvEmployeelist.DataBind();
			}
		}
		protected void gvEmployeelist_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvEmployeelist.DataKeys[e.Row.RowIndex]["v_yhdm"].ToString();
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + str + "');";
			}
		}
		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.gvEmployeelist.DataBind();
		}
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			this.gvEmployeelist.DataBind();
		}
		protected void btnDel_Click(object sender, System.EventArgs e)
		{
			this.gvEmployeelist.DataBind();
		}
	}


