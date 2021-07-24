using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DutyList2 : BasePage, IRequiresSessionState
	{
		protected ProviderClass pc = new ProviderClass();
		protected string[] DutyCodeS;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.DutyCodeS = this.HidDutyCodeS.Value.Split(new char[]
			{
				','
			});
			if (!this.Page.IsPostBack)
			{
				this.GridView1.DataSource = this.pc.dtDuty2(base.Request.QueryString["DUTYID"].ToString());
				this.GridView1.DataBind();
			}
		}
		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.GridView1.DataKeys[e.Row.RowIndex]["i_dutyid"].ToString();
				CheckBox checkBox = (CheckBox)e.Row.Cells[1].FindControl("CBoxObj");
				checkBox.Attributes["onclick"] = "parent.SelectCBox2(this.checked," + str + ",'GridView1');";
				checkBox.Checked = true;
			}
		}
	}


