using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
    public partial class TypeList : NBasePage, IRequiresSessionState
    {
        protected string QProjectID
        {
            get
            {
                if (base.Request.QueryString["PrjGuid"] != null)
                {
                    return base.Request.QueryString["PrjGuid"];
                }
                return "";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.Session["PrjGuid"] = base.Request.QueryString["PrjGuid"].ToString();
                this.hdnPmId.Value = this.QProjectID;
                this.myDataBind();
            }
        }
        private void myDataBind()
        {
            string text = " select * from pm_Construction_Log where ProjectId='" + this.QProjectID + "' ";
            text += this.SearchContional();
            DataTable dataSource = publicDbOpClass.DataTableQuary(text);
            this.dgClass.DataSource = dataSource;
            this.dgClass.DataBind();
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected void dgClass_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                e.Item.Attributes["id"] = this.dgClass.DataKeys[e.Item.ItemIndex].ToString();
                if (e.Item.Cells[3].Text.Length > 30)
                {
                    e.Item.Cells[3].ToolTip = e.Item.Cells[3].Text;
                    e.Item.Cells[3].Text = e.Item.Cells[3].Text.Substring(0, 29) + "...";
                }
                if (e.Item.Cells[4].Text.Length > 30)
                {
                    e.Item.Cells[4].ToolTip = e.Item.Cells[4].Text;
                    e.Item.Cells[4].Text = e.Item.Cells[4].Text.Substring(0, 29) + "...";
                }
            }
        }
        public static string toShowInTextBox(string inputstring)
        {
            string text = inputstring.Replace("&nbsp;", " ");
            text = text.Replace("<br>", "\r\n");
            return HttpContext.Current.Server.HtmlDecode(text);
        }
        public static string LengthString(string inputStr, int ByteLength)
        {
            int length = inputStr.Length;
            if (length == 0)
            {
            }
            string text;
            if (length < ByteLength)
            {
                text = inputStr;
            }
            else
            {
                text = inputStr.Substring(0, ByteLength);
                text += "…";
            }
            return text;
        }
        protected void dgClass_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.dgClass.CurrentPageIndex = e.NewPageIndex;
            this.myDataBind();
        }
        protected void BtnDel_Click(object sender, EventArgs e)
        {
            string logID = Convert.ToString(this.hdnLogID.Value);
            if (publicDbOpClass.Delete(logID) == 1)
            {
                this.js.Text = "alert('删除成功！');";
                this.myDataBind();
                return;
            }
            this.js.Text = "alert('删除错误！');";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.myDataBind();
        }
        protected string SearchContional()
        {
            string text = " and 1=1 ";
            if (this.txtCode.Text.Trim() != "")
            {
                text = text + " and code like'%" + this.txtCode.Text.Trim() + "%' ";
            }
            if (this.txtOperations.Text.Trim() != "")
            {
                text = text + "and operations like '%" + this.txtOperations.Text.Trim() + "%'";
            }
            if (this.workdate.Text.Trim() != "")
            {
                if (this.enddate.Text.Trim() == "")
                {
                    text = text + "and thisDate='" + this.workdate.Text.Trim() + " 0:00:00'";
                }
                else
                {
                    string text2 = text;
                    text = string.Concat(new string[]
					{
						text2,
						"and thisDate between ' ",
						this.workdate.Text.Trim(),
						" 0:00:00'and'",
						this.enddate.Text.Trim(),
						" 0:00:00'"
					});
                }
            }
            if (this.enddate.Text.Trim() != "")
            {
                if (this.workdate.Text.Trim() == "")
                {
                    text = text + "and thisDate='" + this.enddate.Text.Trim() + " 0:00:00'";
                }
                else
                {
                    string text3 = text;
                    text = string.Concat(new string[]
					{
						text3,
						"and thisDate between ' ",
						this.workdate.Text.Trim(),
						" 0:00:00'and'",
						this.enddate.Text.Trim(),
						" 0:00:00'"
					});
                }
            }
            return text;
        }
    }
