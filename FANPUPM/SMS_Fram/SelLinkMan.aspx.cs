using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SMS_Fram_SelLinkMan : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		this.btnSaveSure.Attributes["onclick"] = "javascript:doAction()";
	}
	protected void dgLinks_ItemDataBound(object sender, DataGridItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
		{
			e.Item.Style.Add(HtmlTextWriterStyle.Cursor, "hand");
			e.Item.Attributes.Add("onclick", "if(window.oldtr!=null){window.oldtr.runtimeStyle.cssText='';}this.runtimeStyle.cssText='background-color:#feca40';window.oldtr=this");
			e.Item.Attributes["ondblclick"] = string.Concat(new string[]
			{
				"javascript:return rowDbclick('",
				e.Item.Cells[1].Text,
				"','",
				e.Item.Cells[2].Text,
				"')"
			});
		}
	}
}


