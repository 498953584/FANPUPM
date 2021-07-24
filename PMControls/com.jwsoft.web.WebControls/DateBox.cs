namespace com.jwsoft.web.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class DateBox : TextBox
    {
        public DateBox()
        {
            base.ToolTip = "点击进行日期选择！";
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute("onclick", "getCalendar(this)");
            if (!this.IsWrite)
            {
                writer.AddAttribute("onkeypress", "return false;");
                writer.AddAttribute("onselectstart", "return false;");
            }
            if (this.CalendarStyle.Trim() != "")
            {
                writer.AddAttribute("calendarstyle", this.CalendarStyle);
            }
            else
            {
                writer.AddAttribute("calendarstyle", "border:1px solid #7197CA;background-color:#FFFFFF");
            }
            if (this.ButtonStyle.Trim() != "")
            {
                writer.AddAttribute("buttonstyle", this.ButtonStyle);
            }
            else
            {
                writer.AddAttribute("buttonstyle", "border-left:#7197CA 1px solid;border-top:#7197CA 1px solid;border-right:#7197CA 1px solid;border-bottom:#ff9900 1px solid;background-color:#EDF2F8;font-family:宋体;");
            }
            if (this.WeekStyle.Trim() != "")
            {
                writer.AddAttribute("weekstyle", this.WeekStyle);
            }
            else
            {
                writer.AddAttribute("weekstyle", "font-size:12px;color:#FFFFFF;background-color:#618BC5;");
            }
            if (this.ActiveCellStyle.Trim() != "")
            {
                writer.AddAttribute("activecellstyle", this.ActiveCellStyle);
            }
            else
            {
                writer.AddAttribute("activecellstyle", "border:1px solid #7197CA;color:#3E5468;cursor:hand;background-color:#F8F8FC;");
            }
            if (this.InActiveCellStyle.Trim() != "")
            {
                writer.AddAttribute("inactivecellstyle", this.InActiveCellStyle);
            }
            else
            {
                writer.AddAttribute("inactivecellstyle", "border:1px solid #7197CA;color:#B5C5D2;cursor:hand;background-color:#FFFFFF;");
            }
            if (this.TodayStyle.Trim() != "")
            {
                writer.AddAttribute("todaystyle", this.TodayStyle);
            }
            else
            {
                writer.AddAttribute("todaystyle", "text-decoration:underline;color:#000000;cursor:hand;");
            }
            if (this.TodayCellStyle.Trim() != "")
            {
                writer.AddAttribute("todaycellstyle", this.TodayCellStyle);
            }
            else
            {
                writer.AddAttribute("todaycellstyle", "border:1px solid #7197CA;color:#3E5468;cursor:hand;background-color:#9CBADE;");
            }
            if (this.SelectedCellStyle.Trim() != "")
            {
                writer.AddAttribute("selectedcellstyle", this.SelectedCellStyle);
            }
            else
            {
                writer.AddAttribute("selectedcellstyle", "border:1px solid #7197CA;color:#3E5468;cursor:hand;background-color:#D5ECD2;");
            }
            if (this.MouseOverCellStyle.Trim() != "")
            {
                writer.AddAttribute("mouseovercellstyle", this.MouseOverCellStyle);
            }
            else
            {
                writer.AddAttribute("mouseovercellstyle", "border:1px solid #000000;color:#3E5468;cursor:hand;background-color:#DDDDDD;");
            }
            base.AddAttributesToRender(writer);
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.RegisterDateScript();
            base.OnPreRender(e);
        }

        protected void RegisterDateScript()
        {
            if (!this.Page.IsClientScriptBlockRegistered("DateBoxIncludeScript"))
            {
                string applicationPath = "/Web_Client/WebUIDateBox.js";
                if (this.JSPath == "")
                {
                    applicationPath = this.Context.Request.ApplicationPath;
                    if (applicationPath.Substring(applicationPath.Length - 1) != "/")
                    {
                        applicationPath = applicationPath + "/";
                    }
                    applicationPath = applicationPath + "Web_Client/WebUIDateBox.js";
                }
                string script = "\r\n<SCRIPT language=\"javascript\" src=\"" + applicationPath + "\"></SCRIPT>";
                string str3 = "\r\n<SCRIPT language=\"javascript\">";
                str3 = (((((str3 + "\r\n<!--") + "\r\nif ((typeof(clientInformation)==\"undefined\") && (clientInformation.appName.indexOf(\"Explorer\")!=-1)){" + "\r\nif (typeof(dateBoxScript_Ver)==\"undefined\"){") + "\r\n alert(\"无法找到脚本库“/Web_Client/WebUIDateBox.js”。请配置该文件！\");" + "\r\n}") + "\r\nelse if(dateBoxScript_Ver != \"001\"){" + "\r\n alert(\"此页使用了WebUIDateBox.js的错误版本，此页应该使用版本 001。\");") + "\r\n}" + "\r\n}") + "\r\n//-->" + "\r\n</SCRIPT>";
                this.Page.RegisterClientScriptBlock("DateBoxIncludeScript", script);
                this.Page.RegisterStartupScript("DateBoxIncludeScript", str3);
            }
        }

        [Description("日期的活动表格样式"), Category("Appearance"), DefaultValue("")]
        public string ActiveCellStyle
        {
            get
            {
                string str = (string) this.ViewState["ActiveCellStyle"];
                return ((str == null) ? string.Empty : str);
            }
            set
            {
                this.ViewState["ActiveCellStyle"] = value;
            }
        }

        [Description("日期的按钮样式"), Category("Appearance"), DefaultValue("")]
        public virtual string ButtonStyle
        {
            get
            {
                string str = (string) this.ViewState["ButtonStyle"];
                return ((str == null) ? string.Empty : str);
            }
            set
            {
                this.ViewState["ButtonStyle"] = value;
            }
        }

        [Category("Appearance"), DefaultValue(""), Description("日期的框架样式")]
        public string CalendarStyle
        {
            get
            {
                string str = (string) this.ViewState["CalendarStyle"];
                return ((str == null) ? string.Empty : str);
            }
            set
            {
                this.ViewState["CalendarStyle"] = value;
            }
        }

        [Description("日期的非活动表格样式"), DefaultValue(""), Category("Appearance")]
        public string InActiveCellStyle
        {
            get
            {
                string str = (string) this.ViewState["InActiveCellStyle"];
                return ((str == null) ? string.Empty : str);
            }
            set
            {
                this.ViewState["InActiveCellStyle"] = value;
            }
        }

        [Description("是否可以写入日期"), Category("Behavior"), DefaultValue(false)]
        public virtual bool IsWrite
        {
            get
            {
                object obj2 = this.ViewState["IsWrite"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["IsWrite"] = value;
            }
        }

        [DefaultValue(""), Description("日期脚本的路径"), Category("Layout")]
        public string JSPath
        {
            get
            {
                object obj2 = this.ViewState["JSPath"];
                if (obj2 == null)
                {
                    return string.Empty;
                }
                return (string) obj2;
            }
            set
            {
                this.ViewState["JSPath"] = value;
            }
        }

        [DefaultValue(""), Description("日期的活动表格样式"), Category("Appearance")]
        public string MouseOverCellStyle
        {
            get
            {
                string str = (string) this.ViewState["MouseCellStyle"];
                return ((str == null) ? string.Empty : str);
            }
            set
            {
                this.ViewState["MouseCellStyle"] = value;
            }
        }

        [Category("Appearance"), DefaultValue(""), Description("日期中选中日期样式")]
        public string SelectedCellStyle
        {
            get
            {
                string str = (string) this.ViewState["SelectedCellStyle"];
                return ((str == null) ? string.Empty : str);
            }
            set
            {
                this.ViewState["SelectedCellStyle"] = value;
            }
        }

        [DefaultValue(""), Category("Appearance"), Description("日期中日期单元格样式")]
        public string TodayCellStyle
        {
            get
            {
                string str = (string) this.ViewState["TodayCellStyle"];
                return ((str == null) ? string.Empty : str);
            }
            set
            {
                this.ViewState["TodayCellStyle"] = value;
            }
        }

        [DefaultValue(""), Description("日期中今日日期样式"), Category("Appearance")]
        public string TodayStyle
        {
            get
            {
                string str = (string) this.ViewState["TodayStyle"];
                return ((str == null) ? string.Empty : str);
            }
            set
            {
                this.ViewState["TodayStyle"] = value;
            }
        }

        [Category("Appearance"), DefaultValue(""), Description("日期的Week样式")]
        public string WeekStyle
        {
            get
            {
                string str = (string) this.ViewState["WeekStyle"];
                return ((str == null) ? string.Empty : str);
            }
            set
            {
                this.ViewState["WeekStyle"] = value;
            }
        }
    }
}

