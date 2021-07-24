namespace cn.justwin.BLL
{
    using cn.justwin.Domain.Services;
    using cn.justwin.stockBLL;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class NBasePage : Page
    {
        private const string _registerCss = "styleCssPath";
        private const string _registerJs = "jsPath";
        private const string commonStyleFile = "StyleCss/Common.css";
        protected static readonly int pagesize = 15;
        protected static readonly int pagesize2 = 0x19;
        protected static readonly int pagesize3 = 6;
        protected static readonly int pagesize4 = 10;
        private static string strStyleFileName = "StockManage/skins/sm1.css";

        public string GetAnnx(string guid)
        {
            List<AnnexListModel> listArray = new AnnexListBll().GetListArray(" where RecordCode='" + guid + "'");
            string str = "";
            foreach (AnnexListModel model in listArray)
            {
                str = string.Concat(new object[] { "<img src='/images1/icon_att0b3dfa.gif' style=\"cursor:pointer;\" onclick=\"javascript:return openannexpage('", model.RecordCode, "',", model.ModuleID, ")\"></img>" });
            }
            return str.ToString();
        }

        public string GetAnnx(int moduleID, string guid)
        {
            List<AnnexListModel> listArray = new AnnexListBll().GetListArray(" where RecordCode='" + guid + "'and ModuleID = " + moduleID.ToString());
            string str = "";
            foreach (AnnexListModel model in listArray)
            {
                str = string.Concat(new object[] { "<img src='/images1/icon_att0b3dfa.gif' style=\"cursor:pointer;\" onclick=\"javascript:return openannexpage('", model.RecordCode, "',", model.ModuleID, ")\"></img>" });
            }
            return str.ToString();
        }

        public string GetUserName(string userCode)
        {
            if (string.IsNullOrEmpty(userCode))
            {
                return string.Empty;
            }
            Hashtable allUser = base.Cache["USERLIST"] as Hashtable;
            if (allUser == null)
            {
                allUser = new PTYhmcService().GetAllUser();
            }
            string name = string.Empty;
            object obj2 = allUser[userCode];
            if (obj2 == null)
            {
                name = new PTYhmcService().GetName(userCode);
                if (!string.IsNullOrEmpty(name))
                {
                    allUser.Add(userCode, name);
                    base.Cache["USERLIST"] = allUser;
                }
                return name;
            }
            return obj2.ToString();
        }

        private void InitBasePage()
        {
            HtmlMeta child = new HtmlMeta {
                HttpEquiv = "X-UA-Compatible",
                Content = "IE=8"
            };
            this.Page.Header.Controls.AddAt(0, child);
            if (HttpContext.Current.Session["SkinID"] != null)
            {
                strStyleFileName = "StockManage/skins/sm" + HttpContext.Current.Session["SkinID"] + ".css";
            }
            if (((HttpContext.Current.Session["OpModules"] != null) && (HttpContext.Current.Session["mkdm"] != null)) && (HttpContext.Current.Session["OpModules"].ToString().IndexOf("," + HttpContext.Current.Session["mkdm"].ToString() + ",") >= 0))
            {
                string script = "\r\n\t\t\t\t\t\t<script language=javascript>\r\n\t\t\t\t\t\tvar objs = document.getElementsByTagName('input');\r\n\t\t\t\t\t\tfor(i=0;i<objs.length;i++) {\r\n\t\t\t\t\t\t\tif(objs[i].id.substr(0,6)=='btnAdd' || objs[i].id.substr(0,7)=='btnEdit' || \r\n\t\t\t\t\t\t\t\tobjs[i].id.substr(0,6)=='btnDel' || objs[i].id.substr(0,10)=='btnStartWF' || \r\n\t\t\t\t\t\t\t\tobjs[i].id.substr(0,9)=='btnViewWF') {\r\n\t\t\t\t\t\t\t\tobjs[i].style.display = 'none';\r\n\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\t</script>\r\n\t\t\t\t\t";
                base.ClientScript.RegisterStartupScript(base.GetType(), "", script, true);
            }
            try
            {
                string str2 = "";
                string str3 = "";
                string str4 = string.Empty;
                if (base.Request.ApplicationPath.Substring(base.Request.ApplicationPath.Length - 1) != "/")
                {
                    str2 = base.Request.ApplicationPath + "/" + strStyleFileName;
                    str4 = base.Request.ApplicationPath + "/StyleCss/Common.css";
                    str3 = "<script language='javascript' type='text/javascript' src='" + base.Request.ApplicationPath + "/Web_Client/Common.js'></script>";
                }
                else
                {
                    str2 = base.Request.ApplicationPath + strStyleFileName;
                    str4 = base.Request.ApplicationPath + "StyleCss/Common.css";
                    str3 = "<script language='javascript' type='text/javascript' src='" + base.Request.ApplicationPath + "Web_Client/Common.js'></script>";
                }
                string text1 = "<link rel='stylesheet' href='" + str2 + "' type='text/css'>";
                if (!base.ClientScript.IsClientScriptBlockRegistered("styleCssPath"))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "jsPath", str3);
                    HtmlLink link = new HtmlLink {
                        Href = str2
                    };
                    link.Attributes.Add("rel", "stylesheet");
                    link.Attributes.Add("type", "text/css");
                    HtmlLink link2 = new HtmlLink {
                        Href = str4
                    };
                    link2.Attributes.Add("rel", "stylesheet");
                    link2.Attributes.Add("type", "text/css");
                    this.Page.Header.Controls.Add(link);
                    this.Page.Header.Controls.Add(link2);
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

        protected override void OnLoad(EventArgs e)
        {
            base.ClientScript.GetPostBackEventReference(this, "");
            base.OnLoad(e);
        }

        protected void RegisterAlert(string title, string msg)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("if (typeof jw == 'undefined') { \n").AppendFormat("\talert('{0}'); \n", msg).Append("} else { \n").AppendFormat("\tjw.alert('{0}', '{1}');", title, msg).Append("} \n");
            this.RegisterScript(builder.ToString());
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
                this.RegisterScript(string.Format(@"alert('系统提示：\n\n{0}失败');", str));
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format(@"alert('系统提示：\n\n{0}成功');", str)).AppendLine();
                builder.Append(Environment.NewLine);
                if (string.IsNullOrEmpty(btnId))
                {
                    builder.Append("try {").AppendLine();
                    builder.Append("    window.opener.location = window.opener.location;").AppendLine();
                    builder.Append("}").AppendLine();
                    builder.Append("catch (e) {").AppendLine();
                    builder.Append("    window.parent.location = window.parent.location;").AppendLine();
                    builder.Append("}").AppendLine();
                }
                else
                {
                    builder.Append("try {").AppendLine();
                    builder.Append("    window.opener.document.getElementById('{0}').click();").AppendLine();
                    builder.Append("}").AppendLine();
                    builder.Append("catch (e) {").AppendLine();
                    builder.Append("    window.parent.document.getElementById('{0}').click();").AppendLine();
                    builder.Append("}").AppendLine();
                }
                builder.Append("window.close()");
                this.RegisterScript(builder.ToString());
            }
        }

        protected void RegisterLoadEvent(string function)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type='text/javascript'>");
            builder.Append("addLoadEvent(").Append(function).Append(");").Append("</script>");
            base.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), builder.ToString());
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

        protected void RegisterShow(string title, string msg)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("if (typeof jw == 'undefined') { \n").AppendFormat("\talert('{0}'); \n", msg).Append("} else { \n").AppendFormat("\tjw.show('{0}', '{1}');", title, msg).Append("} \n");
            this.RegisterScript(builder.ToString());
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

