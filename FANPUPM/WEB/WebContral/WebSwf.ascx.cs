using ASP;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
	public partial class WebSwf : System.Web.UI.UserControl
	{

		protected NewsAction na
		{
			get
			{
				return new NewsAction();
			}
		}
		private void Page_Load(object sender, EventArgs e)
		{
			this.swf();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new EventHandler(this.Page_Load);
		}
		private void swf()
		{
			DataTable picNews = this.na.getPicNews("3");
			string text = "";
			string text2 = "";
			string text3 = "";
			int num = 0;
			while (num < picNews.Rows.Count && num < 5)
			{
				if (picNews.Rows[num]["LinkLogo"].ToString() != "")
				{
					text = text + picNews.Rows[num]["LinkLogo"].ToString() + "|";
					text2 = text2 + picNews.Rows[num]["LinkUrl"].ToString() + "|";
					text3 = text3 + num + "|";
				}
				num++;
			}
			if (text != "")
			{
				text = text.Substring(0, text.Length - 1);
				text2 = text2.Substring(0, text2.Length - 1);
				text3 = text3.Substring(0, text3.Length - 1);
			}
			else
			{
				text = "|";
				text2 = "|";
				text3 = "|";
			}
			string text4 = "";
			text4 += "<DIV id=First_Pic><!--焦点图开始-->";
			text4 += "\n\r";
			text4 += "<SCRIPT type=text/javascript><!--";
			text4 += "\n\r";
			text4 += "\tvar focus_width=233;";
			text4 += "\n\r";
			text4 += "\tvar focus_height=150;";
			text4 += "\n\r";
			text4 += " var text_height=0;";
			text4 += "\n\r";
			text4 += " var swf_height = focus_height+text_height;";
			text4 += "\n\r";
			text4 = text4 + " var pics=\"" + text + "\";";
			text4 += "\n\r";
			text4 = text4 + " var links=\"" + text2 + "\";";
			text4 += "\n\r";
			text4 = text4 + " var texts=\"" + text3 + "\";";
			text4 += "\n\r";
			text4 += " document.write('<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" width=\"'+ focus_width +'\" height=\"'+ swf_height +'\">');";
			text4 += "\n\r";
			text4 += " document.write('<param name=\"allowScriptAccess\" value=\"sameDomain\"><param name=\"movie\" value=\"http://pic.zol.com.cn/pix.swf\"><param name=\"quality\" value=\"high\"><param name=\"bgcolor\" value=\"#F0F0F0\">');";
			text4 += "\n\r";
			text4 += " document.write('<param name=\"menu\" value=\"false\"><param name=wmode value=\"opaque\">');";
			text4 += "\n\r";
			text4 += " document.write('<param name=\"FlashVars\" value=\"pics='+pics+'&links='+links+'&texts='+texts+'&borderwidth='+focus_width+'&borderheight='+focus_height+'&textheight='+text_height+'\">');";
			text4 += "\n\r";
			text4 += " document.write('<embed src=\"../pix.swf\" wmode=\"opaque\" FlashVars=\"pics='+pics+'&links='+links+'&texts='+texts+'&borderwidth='+focus_width+'&borderheight='+focus_height+'&textheight='+text_height+'\" menu=\"false\" bgcolor=\"#F0F0F0\" quality=\"high\" width=\"'+ focus_width +'\" height=\"'+ swf_height +'\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />');";
			text4 += "\n\r";
			text4 += " document.write('</object>');";
			text4 += "\n\r";
			text4 += " //-->";
			text4 += "\n\r";
			text4 += " </SCRIPT>";
			text4 += "\n\r";
			text4 += "<!--焦点图结束--></DIV>";
			text4 += "\n\r";
			this.Literal1.Text = text4;
		}
	}


