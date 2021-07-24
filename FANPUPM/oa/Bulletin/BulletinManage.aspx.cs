using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
    public partial class BulletinManage : PageBase, IRequiresSessionState
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            base.Response.Cache.SetNoStore();
            if (!base.IsPostBack)
            {
                if (base.Request.QueryString["type"].ToString() == "see")
                {
                    this.tr_Button.Style.Add("display", "none");
                    this.Bind();
                }
                else
                {
                    this.data_bind();
                }
                this.btnAudit.Attributes["onclick"] = "OpenAudit()";
                if (base.Request["audit"] != null)
                {
                    this.btnAdd.Style.Add("display", "none");
                    this.btnEdit.Style.Add("display", "none");
                    this.btnDel.Style.Add("display", "none");
                }
                else
                {
                    this.btnAudit.Style.Add("display", "none");
                }
                this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确认要删除选定公告信息吗？')){return false;}";
                this.BtnView.Attributes["onclick"] = "OpenLock();";
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["id"] = this.GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                e.Row.Attributes["guid"] = this.GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                string str = this.GridView1.DataKeys[e.Row.RowIndex]["I_BULLETINID"].ToString();
                ((DataRowView)e.Row.DataItem)["AuditState"].ToString();
            }
        }
        protected void btnDel_Click(object sender, System.EventArgs e)
        {
            System.Guid bulletinId = new System.Guid(this.hdnRecordID.Value);
            if (com.jwsoft.pm.entpm.action.BulletinManage.DelBulletinInfo(bulletinId))
            {
                this.JS.Text = "top.ui.show( '删除成功！');";
                this.data_bind();
            }
        }
        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            this.data_bind();
        }
        protected void btnRefresh_Click(object sender, System.EventArgs e)
        {
            this.data_bind();
        }
        protected void btnEdit_Click(object sender, System.EventArgs e)
        {
            this.data_bind();
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.data_bind();
        }
        private void data_bind()
        {
            string text = "";
            if (this.DropDownList1.SelectedValue != "%")
            {
                text = text + " and auditstate=" + this.DropDownList1.SelectedValue;
            }
            this.GridView1.DataSource = BulletionAction.ReturnTable(base.UserCode, text);
            this.GridView1.DataBind();
        }
        protected void btnAudit_Click(object sender, System.EventArgs e)
        {
            this.data_bind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            this.data_bind();
        }
        private void Bind()
        {
            string condition = " and auditstate=1 and dtm_expriesdate>getdate()-1 ";
            this.GridView1.DataSource = BulletionAction.ReturnTable(base.UserCode, condition);
            this.GridView1.DataBind();
        }
    }