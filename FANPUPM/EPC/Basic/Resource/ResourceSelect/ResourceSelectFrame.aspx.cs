using ASP;
using cn.justwin.BLL;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Basic_Resource_ResourceSelect_ResourceSelectFrame : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		base.Response.Clear();
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["pt"] == null || base.Request.QueryString["rs"] == null)
			{
				this.js.Text = "alert('\\n系统提示：页面错误！\\n请找技术人员！ ');window.close();return false;";
				return;
			}
			if (base.Request.QueryString["rs"] == "-1")
			{
				this.resFraTree.Attributes["src"] = "";
				return;
			}
			if (base.Request.QueryString["rs"] == "0")
			{
				this.resFraTree.Attributes["src"] = "ResourceTree.aspx?pt=" + base.Request.QueryString["pt"] + "&rs=1";
				this.btnResPerson.Enabled = true;
				this.btnResMaterial.Enabled = true;
				this.btnResMachinery.Enabled = true;
				this.btnResPerson.Attributes["onclick"] = "setFrameSrc('" + base.Request.QueryString["pt"] + "','1')";
				this.btnResMaterial.Attributes["onclick"] = "setFrameSrc('" + base.Request.QueryString["pt"] + "','2')";
				this.btnResMachinery.Attributes["onclick"] = "setFrameSrc('" + base.Request.QueryString["pt"] + "','3')";
				return;
			}
			this.resFraTree.Attributes["src"] = "ResourceTree.aspx?pt=" + base.Request.QueryString["pt"] + "&rs=" + base.Request.QueryString["rs"];
		}
	}
}


