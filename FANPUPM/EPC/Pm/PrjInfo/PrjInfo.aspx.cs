using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
    public partial class PrjInfo : NBasePage, IRequiresSessionState
    {
        protected PersistScrollPosition PersistScrollPosition1;
        protected string[] moduleCodeList;

        protected override void OnInit(System.EventArgs e)
        {
            this.AspNetPager1.PageSize = NBasePage.pagesize;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.moduleCodeList = this.hdnModuleCodeList.Value.Split(new char[]
			{
				'^'
			});
            if (!this.Page.IsPostBack)
            {
                this.btnAdd.Attributes["onclick"] = "if (!ClickBtn('add')) return false;";
                this.btnEdit.Attributes["onclick"] = "if (!ClickBtn('upd')) return false;";
                this.btnDel.Attributes["onclick"] = "if (!ClickBtn('del')) return false;";
                this.btnUser.Attributes["onclick"] = "if (!ClickBtn('User')) return false;";
                this.btndelAdmin.Attributes["onclick"] = "javascript:return adminDel();";
                this.ShowTaskList();
                this.btndelAdmin.Attributes.Add("style", "display:none");
            }
        }
        public void ShowTaskList()
        {
            System.Data.DataTable dataSource = new System.Data.DataTable();
            this.AspNetPager1.RecordCount = PMAction.GetPrjCount(this.txtPrjCode.Text.Trim(), this.txtprjName.Text.Trim(), this.txtOwner.Text.Trim(), this.txtPrjPlace.Text.Trim(), "", "", base.UserCode);
            dataSource = PMAction.GetPrjInfo(this.txtPrjCode.Text.Trim(), this.txtprjName.Text.Trim(), this.txtOwner.Text.Trim(), this.txtPrjPlace.Text.Trim(), "", "", base.UserCode, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
            this.gvPrjInfo.DataSource = dataSource;
            this.gvPrjInfo.DataBind();
        }
        public void GridBind(System.Data.DataTable dt)
        {
        }
        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            this.ShowTaskList();
        }
        protected void btnUpd_Click(object sender, System.EventArgs e)
        {
            this.ShowTaskList();
        }
        protected void btnDel_Click(object sender, System.EventArgs e)
        {
            string text = this.hdnModuleCode.Value.ToString();
            string a = PMAction.CheckPrjBegin(text);
            if (a != "0")
            {
                this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">document.getElementById(\"FraFlow\").src =\"ResTypeOther.aspx?TypeCode=" + text + "&pt=1\"; alert('已应用，不能进行删除！');</script>");
            }
            else
            {
                if (!PMAction.DelPrjInfo(text))
                {
                    this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert('删除失败！');</script>");
                }
            }
            this.ShowTaskList();
        }
        protected void btnUser_Click(object sender, System.EventArgs e)
        {
            this.ShowTaskList();
        }
        protected void btndelAdmin_Click(object sender, System.EventArgs e)
        {
            if (this.Session["twopass"] != null && this.Session["twopass"].ToString().Trim() == "IsAllowDel")
            {
                if (!PMAction.DelPrjInfoLogic(this.hdnModuleCode.Value.ToString()))
                {
                    this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert('删除失败！');</script>");
                }
                this.ShowTaskList();
            }
        }
        protected void btnQuery_Click(object sender, System.EventArgs e)
        {
            this.AspNetPager1.CurrentPageIndex = 1;
            this.ShowTaskList();
        }
        protected void gvPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string value = this.gvPrjInfo.DataKeys[e.Row.RowIndex]["PrjGuid"].ToString();
                e.Row.Attributes["id"] = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Value.ToString();
                e.Row.Attributes["child"] = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Values[2].ToString();
                e.Row.Attributes["prjGuid"] = value;
                e.Row.Attributes["guid"] = value;
                string text = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Values[1].ToString();
                e.Row.Attributes["typeCode"] = text;
                if (text.Length == 5)
                {
                    e.Row.Attributes["isMainContract"] = "True";
                }
                else
                {
                    e.Row.Attributes["isMainContract"] = "False";
                }
                if (e.Row.Cells[1].Text.Length > 5)
                {
                    e.Row.Cells[1].Text = e.Row.Cells[1].Text.Substring(0, 5) + "<font color=\"#ff0000\">" + e.Row.Cells[1].Text.Substring(5, e.Row.Cells[1].Text.Length - 5) + "</font>";
                }
                string a = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Values[3].ToString();
                if (a == "-1")
                {
                    e.Row.Cells[8].ForeColor = Color.Blue;
                    e.Row.Cells[8].Text = "在建";
                }
                if (a == "4")
                {
                    e.Row.Cells[8].Text = "在建";
                }
                if (a == "6")
                {
                    e.Row.Cells[8].Text = "停工";
                }
                if (a == "7")
                {
                    e.Row.Cells[8].Text = "竣工";
                }
                if (a == "8")
                {
                    e.Row.Cells[8].Text = "验收";
                }
                if (a == "9")
                {
                    e.Row.Cells[8].Text = "维保";
                }
                if (a == "0")
                {
                    e.Row.Cells[8].Text = "";
                }
            }
        }
        protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
        {
            this.ShowTaskList();
        }
    }
