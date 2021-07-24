namespace com.jwsoft.common.baseclass
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class BasePage : Page
    {
        private const string _registerCss = "styleCssPath";
        private const string _registerJs = "jsPath";
        private static string strStyleFileName = "StyleCss/PM1.css";

        public void CloseParent()
        {
            string script = "\r\n\t\t\t\tif (window.parent)\r\n\t\t\t\t\twindow.parent.close();\r\n\t\t\t\telse \r\n\t\t\t\t\twindow.opener.close();\r\n\t\t\t";
            this.RegisterScript(script);
        }

        private void InitBasePage()
        {
            if (HttpContext.Current.Session["SkinID"] != null)
            {
                strStyleFileName = "StyleCss/PM" + HttpContext.Current.Session["SkinID"] + ".css";
                try
                {
                    string script = "<script language=javascript>";
                    script = (((script + "var objs = document.getElementsByTagName('input');" + "for(i=0;i<objs.length;i++)") + "{" + "if(objs[i].id.substr(0,6)=='btnSee'|| objs[i].value=='查 看'|| objs[i].value=='查  看' )") + "{" + "objs[i].style.display = 'none';") + "}}" + "</script>";
                    this.Page.RegisterStartupScript("script", script);
                }
                catch
                {
                }
            }
            if (((HttpContext.Current.Session["OpModules"] != null) && (HttpContext.Current.Session["mkdm"] != null)) && (HttpContext.Current.Session["OpModules"].ToString().IndexOf("," + HttpContext.Current.Session["mkdm"].ToString() + ",") >= 0))
            {
                string str2 = "<script language=javascript>";
                str2 = (((str2 + "var objs = document.getElementsByTagName('input');" + "for(i=0;i<objs.length;i++)") + "{" + "if(objs[i].id.substr(0,6)=='btnAdd' || objs[i].id.substr(0,7)=='btnEdit' || objs[i].id.substr(0,6)=='btnDel' || objs[i].id.substr(0,10)=='btnStartWF' || objs[i].id.substr(0,9)=='btnViewWF')") + "{" + "objs[i].style.display = 'none';") + "}}" + "</script>";
                this.Page.RegisterStartupScript("script", str2);
            }
            try
            {
                string str3 = "";
                string str4 = "";
                if (base.Request.ApplicationPath.Substring(base.Request.ApplicationPath.Length - 1) != "/")
                {
                    str3 = base.Request.ApplicationPath + "/" + strStyleFileName;
                    str4 = "<script language='javascript' type='text/javascript' src='" + base.Request.ApplicationPath + "/Web_Client/Common.js'></script>";
                }
                else
                {
                    str3 = base.Request.ApplicationPath + strStyleFileName;
                    str4 = "<script language='javascript' type='text/javascript' src='" + base.Request.ApplicationPath + "Web_Client/Common.js'></script>";
                }
                string text1 = "<link rel='stylesheet' href='" + str3 + "' type='text/css'>";
                if (!this.Page.IsClientScriptBlockRegistered("styleCssPath"))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "jsPath", str4);
                    HtmlLink child = new HtmlLink {
                        Href = str3
                    };
                    child.Attributes.Add("rel", "stylesheet");
                    child.Attributes.Add("type", "text/css");
                    this.Page.Header.Controls.Add(child);
                }
                HttpContext.Current.Session["userOperationLastTime"] = DateTime.Now;
                if (HttpContext.Current.Session["yhdm"] == null)
                {
                    this.Page.Response.Redirect("/");
                }
            }
            catch
            {
                throw new Exception("读取配置文件中样式表信息出错，请重新核对配置文件中的相关信息");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.InitBasePage();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public void RegisterScript(string script)
        {
            string str = string.Format("\r\n\t\t\t\t<script>\r\n\t\t\t\t\t{0}\r\n\t\t\t\t</script>\r\n\t\t\t", script);
            base.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), str);
        }

        public void ReloadTab()
        {
            string script = "\r\n\t\t\t\tvar top = window.top;\r\n\t\t\t\tif (!top)\r\n\t\t\t\t\ttop = window.parent.top;\r\n\t\t\t\telse \r\n\t\t\t\t\ttop = window.opener.top;\r\n\t\t\t\ttop.realodTab();\r\n\t\t\t";
            this.RegisterScript(script);
        }

        protected OpType DealType
        {
            get
            {
                object obj2 = this.ViewState["DEALTYPE"];
                if (obj2 != null)
                {
                    return (OpType) obj2;
                }
                return OpType.Nothing;
            }
            set
            {
                this.ViewState["DEALTYPE"] = value;
            }
        }

        protected string UserCode
        {
            get
            {
                return this.Session["yhdm"].ToString();
            }
        }
    }
}

