namespace com.jwsoft.pm.web
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.UI;

    public class PageBase : Page
    {
        private const string _registerCss = "styleCssPath";
        private const string _registerJs = "jsPath";
        private static string strStyleFileName = ConfigurationSettings.AppSettings["StyleCssPath"];

        private void InitBasePage()
        {
            try
            {
                string str = "";
                string script = "";
                if (base.Request.ApplicationPath.Substring(base.Request.ApplicationPath.Length - 1) != "/")
                {
                    str = base.Request.ApplicationPath + "/" + strStyleFileName;
                    script = "<script language='javascript' type='text/javascript' src='" + base.Request.ApplicationPath + "/Web_Client/Common.js'></script>";
                }
                else
                {
                    str = base.Request.ApplicationPath + strStyleFileName;
                    script = "<script language='javascript' type='text/javascript' src='" + base.Request.ApplicationPath + "Web_Client/Common.js'></script>";
                }
                string str3 = "<link rel='stylesheet' href='" + str + "' type='text/css'>";
                if (!this.Page.IsClientScriptBlockRegistered("styleCssPath"))
                {
                    this.Page.RegisterClientScriptBlock("styleCssPath", str3);
                    this.Page.RegisterClientScriptBlock("jsPath", script);
                }
                HttpContext.Current.Session["userOperationLastTime"] = DateTime.Now;
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

        protected virtual Guid PageGUID
        {
            get
            {
                return Guid.Empty;
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

