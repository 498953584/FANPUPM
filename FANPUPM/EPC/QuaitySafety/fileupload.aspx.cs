using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
    public partial class FileUpload : NBasePage, IRequiresSessionState
    {
        protected string strTitle = "";
        private string wheresql = " CA>0 and CA<4";
        private static string _typeVal = string.Empty;
        public string listTitleStr = string.Empty;
        public static string _QS = string.Empty;
        public static string TYPE = "Edit";
        private int CA_val;
        public Guid PrjCode
        {
            get
            {
                object obj = this.ViewState["PrjCode"];
                if (obj != null)
                {
                    return (Guid)obj;
                }
                return Guid.Empty;
            }
            set
            {
                this.ViewState["PrjCode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.P_Type();
            if (!base.IsPostBack)
            {
                if (base.Request.QueryString["TypeId"] == null)
                {
                    base.Response.End();
                }
                if (base.Request.QueryString["prjId"] != null)
                {
                    this.PrjCode = new Guid(base.Request.QueryString["prjId"].ToString());
                }
                this.Session["DATUMCLASS"] = base.Request.QueryString["TypeId"].ToString();
                this.Data_Bind();
            }
        }
        private void P_Type()
        {
            this.EDIT.Value = "Edit";
            if (base.Request.QueryString["Type"] != "Edit")
            {
                this.EDIT.Value = "List";
            }
            this.Session["DATUMCLASS"] = base.Request.QueryString["TypeId"].ToString();
        }
        protected void Data_Bind()
        {
            this.Session["DATUMCLASS"].ToString();
            DataTable pageData = AffairAction.GetPageData(this.PrjCode, this.Session["DATUMCLASS"].ToString(), this.DDLLookup.SelectedValue, this.TxtLookup.Text);
            DataRow[] array = pageData.Select(this.wheresql);
            DataTable dataTable = new DataTable();
            dataTable = pageData.Clone();
            DataRow[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                DataRow row = array2[i];
                dataTable.ImportRow(row);
            }
            this.GridView1.DataSource = dataTable;
            this.GridView1.DataBind();
        }
        protected void BtnModify_Click(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected override void OnInit(EventArgs e)
        {
            if (base.Request.QueryString["CA"] != null && base.Request.QueryString["CA"].ToString() != "")
            {
                this.hiden_CA.Value = base.Request.QueryString["CA"].ToString();
                this.CA_val = int.Parse(base.Request.QueryString["CA"].ToString());
            }
            if (base.Request.QueryString["Flag"] != null && base.Request.QueryString["Flag"] == "Q")
            {
                _QS = "Q";
                this.QS.Value = "Q";
                this.Session["AffairFlageCode"] = "11";
                this.hdnClass.Value = "10";
                this.strTitle = "质量台账";
            }
            else
            {
                if (base.Request.QueryString["Flag"] != null && base.Request.QueryString["Flag"] == "S")
                {
                    _QS = "S";
                    this.QS.Value = "S";
                    this.Session["AffairFlageCode"] = "10";
                    this.hdnClass.Value = "9";
                    this.wheresql = " CA>=4";
                    this.strTitle = "安全台账";
                }
            }
            if (base.Request.QueryString["Type"] != null && base.Request.QueryString["Type"] == "Edit")
            {
                _typeVal = "Edit";
            }
            else
            {
                if (base.Request.QueryString["Type"] != null && base.Request.QueryString["Type"] == "List")
                {
                    _typeVal = "List";
                }
            }
            base.OnInit(e);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dataRowView = (DataRowView)e.Row.DataItem;
                string text = this.GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                e.Row.Attributes["id"] = text;
                e.Row.Attributes["mark"] = dataRowView["mark"].ToString();
                string text2 = StripHTML(e.Row.Cells[4].Text);
                if (text2.Length > 22)
                {
                    e.Row.Cells[4].Text = text2.Substring(0, 21) + "...";
                }
                else
                {
                    e.Row.Cells[4].Text = text2;
                }
                e.Row.Cells[4].ToolTip = text2;
                Label label = (Label)e.Row.Cells[1].FindControl("Label1");
                label.Attributes["onclick"] = string.Concat(new object[]
				{
					"clickRow('",
					text,
					"','",
					_typeVal,
					"'); OpType('SEE','",
					this.PrjCode,
					"','",
					e.Row.Cells[5].Text.ToString(),
					"');"
				});
                label.Text = dataRowView["AffairTitle"].ToString();
                if (e.Row.Cells[5].Text != "")
                {
                    e.Row.Cells[5].Text = this.typeSw(e.Row.Cells[5].Text.ToString());
                }
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        public static string DelHTML(string Htmlstring)
        {
            Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            return Htmlstring;
        }
        public static string StripHTML(string strHtml)
        {
            string[] array = new string[]
			{
				"<script[^>]*?>.*?</script>",
				"<(\\/\\s*)?!?((\\w+:)?\\w+)(\\w+(\\s*=?\\s*(([\"'])(file://[\"'tbnr]|[^/7])*?/7|/w+)|.{0})|/s)*?(///s*)?>",
				"([\\r\\n])[\\s]+",
				"&(quot|#34);",
				"&(amp|#38);",
				"&(lt|#60);",
				"&(gt|#62);",
				"&(nbsp|#160);",
				"&(iexcl|#161);",
				"&(cent|#162);",
				"&(pound|#163);",
				"&(copy|#169);",
				"&#(\\d+);",
				"-->",
				"<!--.*\\n"
			};
            string[] array2 = new string[]
			{
				"",
				"",
				"",
				"\"",
				"&",
				"<",
				">",
				" ",
				"¡",
				"¢",
				"£",
				"©",
				"",
				"\r\n",
				""
			};
            string arg_135_0 = array[0];
            string text = strHtml;
            for (int i = 0; i < array.Length; i++)
            {
                Regex regex = new Regex(array[i], RegexOptions.IgnoreCase);
                text = regex.Replace(text, array2[i]);
            }
            text.Replace("<", "");
            text.Replace(">", "");
            text.Replace("\r\n", "");
            return DelHTML(text);
        }
        private string typeSw(string _val)
        {
            string result = string.Empty;
            if (_val != "" && _val != null)
            {
                if (!(_val == "1"))
                {
                    if (!(_val == "2"))
                    {
                        if (!(_val == "3"))
                        {
                            if (!(_val == "4"))
                            {
                                if (!(_val == "5"))
                                {
                                    if (_val == "6")
                                    {
                                        result = "安全目标";
                                    }
                                }
                                else
                                {
                                    result = "安全事故报告";
                                }
                            }
                            else
                            {
                                result = "安全监督";
                            }
                        }
                        else
                        {
                            result = "工程质量竣工验收";
                        }
                    }
                    else
                    {
                        result = "质量事故报告";
                    }
                }
                else
                {
                    result = "工程质量监督";
                }
            }
            return result;
        }
        public string swTitle(string v, string edit, string QS)
        {
            string result = string.Empty;
            if (v != null)
            {
                if (!(v == "1"))
                {
                    if (!(v == "2"))
                    {
                        if (!(v == "3"))
                        {
                            if (!(v == "4"))
                            {
                                if (!(v == "5"))
                                {
                                    if (v == "6")
                                    {
                                        if (QS == "S")
                                        {
                                            if (edit == "Edit")
                                            {
                                                result = "安全目标维护";
                                            }
                                            else
                                            {
                                                if (edit == "List")
                                                {
                                                    result = "安全目标查看";
                                                }
                                            }
                                            this.listTitleStr = "安全目标名称";
                                        }
                                    }
                                }
                                else
                                {
                                    if (QS == "S")
                                    {
                                        if (edit == "Edit")
                                        {
                                            result = "质量事故报告";
                                        }
                                        else
                                        {
                                            if (edit == "List")
                                            {
                                                result = "事故查看";
                                            }
                                        }
                                        this.listTitleStr = "安全事故名称";
                                    }
                                }
                            }
                            else
                            {
                                if (QS == "S")
                                {
                                    if (edit == "Edit")
                                    {
                                        result = "安全监督";
                                    }
                                    else
                                    {
                                        if (edit == "List")
                                        {
                                            result = "安全检查查看";
                                        }
                                    }
                                    this.listTitleStr = "安全检查名称";
                                }
                            }
                        }
                        else
                        {
                            if (QS == "Q")
                            {
                                if (edit == "Edit")
                                {
                                    result = "工程质量竣工验收资料管理";
                                }
                                else
                                {
                                    if (edit == "List")
                                    {
                                        result = "工程质量竣工验收查看";
                                    }
                                }
                                this.listTitleStr = "资料名称";
                            }
                        }
                    }
                    else
                    {
                        if (QS == "Q")
                        {
                            if (edit == "Edit")
                            {
                                result = "质量事故";
                            }
                            else
                            {
                                if (edit == "List")
                                {
                                    result = "事故查看";
                                }
                            }
                            this.listTitleStr = "事故名称";
                        }
                    }
                }
                else
                {
                    if (QS == "Q")
                    {
                        if (edit == "Edit")
                        {
                            result = "质量检查";
                        }
                        else
                        {
                            if (edit == "List")
                            {
                                result = "质量检查资料查询";
                            }
                        }
                        this.listTitleStr = "检查名称";
                    }
                }
            }
            return result;
        }
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        private ArrayList getSory(string va)
        {
            ArrayList result = new ArrayList();
            Regex regex = new Regex("[0-9a-z]{3,5}");
            string[] array = new string[]
			{
				"abc",
				"123456",
				"(aa22bb33)",
				"ab"
			};
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text = array2[i];
                if (regex.IsMatch(text))
                {
                    Console.WriteLine("{0}中有匹配的项", text);
                }
                else
                {
                    Console.WriteLine("{0}中没有匹配的项", text);
                }
            }
            return result;
        }
    }
