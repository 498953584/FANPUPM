using ASP;
using System;
using System.Text;
using System.Threading;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Z_Demo_testwin : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		base.ClientScript.GetPostBackEventReference(this, "");
	}
	protected void btnOk_Click(object sender, EventArgs e)
	{
		Thread.Sleep(3000);
		base.Response.Write(DateTime.Now.ToLongTimeString());
	}
	public void PreventRepeatSubmit(WebControl btn)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(string.Format("document.getElementById('{0}').setAttribute('disabled', 'disabled');\n", btn.ID));
		stringBuilder.Append(base.ClientScript.GetPostBackEventReference(btn, null));
		btn.Attributes.Add("onclick", stringBuilder.ToString());
	}
	protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
	{
		Thread.Sleep(3000);
		base.Response.Write(DateTime.Now.ToLongTimeString());
	}
	private void PrevenRepeat()
	{
		ControlCollection controls = base.Form.Controls;
		foreach (object current in controls)
		{
			Button button = current as Button;
			if (button != null)
			{
				this.PreventRepeatSubmit(button);
			}
		}
	}
}


