namespace com.jwsoft.common.baseclass
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class NBasePage : Page
    {
        private const string _registerCss = "styleCssPath";
        private const string _registerJs = "jsPath";
        protected static readonly int pagesize = 15;
        protected static readonly int pagesize2 = 0x19;
        protected static readonly int pagesize3 = 6;
        private static string strStyleFileName = "StockManage/skins/sm1.css";

        public string GetAnnx(string guid)
        {
            List<AnnexListModel> listArray = new AnnexList().GetListArray(" where RecordCode='" + guid + "'");
            string str = "";
            foreach (AnnexListModel model in listArray)
            {
                str = string.Concat(new object[] { "<img src='/images1/icon_att0b3dfa.gif' style=\"cursor:pointer;\" onclick=\"javascript:return openannexpage('", model.RecordCode, "',", model.ModuleID, ")\"></img>" });
            }
            return str.ToString();
        }

        private void InitBasePage()
        {
            if (HttpContext.Current.Session["SkinID"] != null)
            {
                strStyleFileName = "StockManage/skins/sm" + HttpContext.Current.Session["SkinID"] + ".css";
            }
            if (((HttpContext.Current.Session["OpModules"] != null) && (HttpContext.Current.Session["mkdm"] != null)) && (HttpContext.Current.Session["OpModules"].ToString().IndexOf("," + HttpContext.Current.Session["mkdm"].ToString() + ",") >= 0))
            {
                string script = "<script language=javascript>";
                script = (((script + "var objs = document.getElementsByTagName('input');" + "for(i=0;i<objs.length;i++)") + "{" + "if(objs[i].id.substr(0,6)=='btnAdd' || objs[i].id.substr(0,7)=='btnEdit' || objs[i].id.substr(0,6)=='btnDel' || objs[i].id.substr(0,10)=='btnStartWF' || objs[i].id.substr(0,9)=='btnViewWF')") + "{" + "objs[i].style.display = 'none';") + "}}" + "</script>";
                base.ClientScript.RegisterStartupScript(base.GetType(), "", script, true);
            }
            try
            {
                string str2 = "";
                string str3 = "";
                if (base.Request.ApplicationPath.Substring(base.Request.ApplicationPath.Length - 1) != "/")
                {
                    str2 = base.Request.ApplicationPath + "/" + strStyleFileName;
                    str3 = "<script language='javascript' type='text/javascript' src='" + base.Request.ApplicationPath + "/Web_Client/Common.js'></script>";
                }
                else
                {
                    str2 = base.Request.ApplicationPath + strStyleFileName;
                    str3 = "<script language='javascript' type='text/javascript' src='" + base.Request.ApplicationPath + "Web_Client/Common.js'></script>";
                }
                string text1 = "<link rel='stylesheet' href='" + str2 + "' type='text/css'>";
                if (!base.ClientScript.IsClientScriptBlockRegistered("styleCssPath"))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "jsPath", str3);
                    HtmlLink child = new HtmlLink {
                        Href = str2
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
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.InitBasePage();
        }

        protected void RegisterHintScript(string action, bool isSucceed, string btnId)
        {
            string str = string.Empty;
            if (string.Compare(action, "Add", true) == 0)
            {
                str = "添加";
            }
            else if (string.Compare(action, "Update", true) == 0)
            {
                str = "修改";
            }
            if (!isSucceed)
            {
                this.RegisterScript(string.Format(@"alert('系统提示：\n\n{0}失败')", str));
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format(@"alert('系统提示：\n\n{0}成功')", str));
                builder.Append(Environment.NewLine);
                if (string.IsNullOrEmpty(btnId))
                {
                    builder.Append("window.opener.location = window.opener.location;");
                }
                else
                {
                    builder.AppendFormat("window.opener.document.getElementById('{0}').click()", btnId).AppendLine();
                }
                builder.Append("window.close()");
                this.RegisterScript(builder.ToString());
            }
        }

        protected void RegisterScript(string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type='text/javascript'>").Append(Environment.NewLine).Append(message).Append("</script>");
            base.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), builder.ToString());
        }

        protected void RegisterScriptRefresh()
        {
            this.RegisterScript("window.location.href = window.location.href");
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

