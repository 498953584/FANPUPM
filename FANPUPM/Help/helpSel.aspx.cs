using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class WebSel : BasePage, System.Web.SessionState.IRequiresSessionState
	{

		protected string helpId
		{
			get
			{
				return this.ViewState["HELPID"].ToString();
			}
			set
			{
				this.ViewState["HELPID"] = value;
			}
		}
		private void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!base.IsPostBack)
			{
				if (base.Request["id"] == null)
				{
					this.JS.Text = "alert('参数错误!')";
					return;
				}
				if (base.Request["id"] != "")
				{
					this.helpId = base.Request["id"].ToString();
					this.Lbbt.Text = base.Request["mc"].ToString();
				}
				else
				{
					this.helpId = "00";
				}
				this.NewsInfoDataBind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new EventHandler(this.Page_Load);
		}
		private void NewsInfoDataBind()
		{
			DataTable dataTable = publicDbOpClass.DataTableQuary("select * from [PT_HELP] where C_MKDM='" + this.helpId + "'");
			if (dataTable.Rows.Count > 0)
			{
				this.Lbxwnr.Text = dataTable.Rows[0]["txt_help"].ToString().Replace("http://helpsel.aspx", "helpSel.aspx");
				this.Lbbt.Text = dataTable.Rows[0]["v_MKMC"].ToString();
			}
		}
	}


