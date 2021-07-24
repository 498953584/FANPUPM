using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_BuildDiary_ShowImg : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (base.Request["imgUrl"] != null)
		{
			string src = base.Request["imgUrl"].ToString();
			string alt = base.Request["imgIndex"].ToString();
			string value = base.Request["imgPath"].ToString();
			this.img.Src = src;
			this.img.Alt = alt;
			this.img.Height = 400;
			this.img.Width = 410;
			this.hfldImgPath.Value = value;
		}
	}
}


