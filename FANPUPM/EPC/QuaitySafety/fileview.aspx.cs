using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
    public partial class FileViewWeb : PageBase, IRequiresSessionState
    {
        private QualityGoalAction qa = new QualityGoalAction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.Request["PrjCode"].ToString() == "")
            {
                this.JS.Text = "alert('请选择项目!');window.close();";
            }
            if (!this.Page.IsPostBack)
            {
                this.getDataBind();
            }
        }
        private void getDataBind()
        {
            string text = base.Request.QueryString["modelkey"];
            string text2 = base.Request.QueryString["PrjCode"];
            if (base.Request.QueryString["PrjCode"] != "")
            {
                string strWhere = string.Concat(new string[]
				{
					" modelkey ='",
					text,
					"' and prjcode='",
					text2,
					"'"
				});
                this.dgFileView.DataSource = this.qa.GetDatafromQuality(this.PaginationControl1, this.dgFileView.PageSize, strWhere);
                this.dgFileView.DataBind();
                return;
            }
            this.dgFileView.DataSource = "";
            this.dgFileView.DataBind();
        }
        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.dgFileView.ItemDataBound += new DataGridItemEventHandler(this.dgFileView_ItemDataBound);
        }
        private void dgFileView_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                e.Item.Attributes["onclick"] = "OnRecord(this);";
                e.Item.Attributes["style"] = "cursor:hand";
                e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
            }
        }
        protected void PaginationControl1_PageIndexChange(object sender, EventArgs e)
        {
            this.getDataBind();
        }
    }
