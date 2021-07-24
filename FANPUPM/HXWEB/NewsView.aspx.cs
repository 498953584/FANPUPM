using ASP;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HXWEB_NewsView : Page, IRequiresSessionState
{
	protected string NewsBt
	{
		get
		{
			return this.ViewState["NEWSBT"].ToString();
		}
		set
		{
			this.ViewState["NEWSBT"] = value;
		}
	}
	protected int NewsId
	{
		get
		{
			return Convert.ToInt32(this.ViewState["NEWSID"]);
		}
		set
		{
			this.ViewState["NEWSID"] = value;
		}
	}
	protected NewsAction na
	{
		get
		{
			return new NewsAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.NewsId = Convert.ToInt32(base.Request.QueryString["NewsID"].ToString());
			DataTable newsInfo = this.na.GetNewsInfo(this.NewsId);
			this.lblTitle.Text = newsInfo.Rows[0]["v_xwbt"].ToString();
			this.Lbxwnr.Text = newsInfo.Rows[0]["txt_xwnr"].ToString();
		}
	}
}


