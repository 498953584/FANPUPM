using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Globalization;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class WebManager : BasePage, IRequiresSessionState
	{

		protected string NewsName
		{
			get
			{
				return this.ViewState["NEWSNAME"].ToString();
			}
			set
			{
				this.ViewState["NEWSNAME"] = value;
			}
		}
		protected string NewsCode
		{
			get
			{
				return this.ViewState["NEWSCODE"].ToString();
			}
			set
			{
				this.ViewState["NEWSCODE"] = value;
			}
		}
		protected string NewsBt
		{
			get
			{
				return this.ViewState["NEWSBT"].ToString();
			}
			set
			{
				this.ViewState["NEWSBT"] = value;
			}
		}
		protected int NewsId
		{
			get
			{
				return Convert.ToInt32(this.ViewState["NEWSID"]);
			}
			set
			{
				this.ViewState["NEWSID"] = value;
			}
		}
		protected NewsAction na
		{
			get
			{
				return new NewsAction();
			}
		}
		private void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.hdnRecorde.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
				if (base.Request["na"] == null || base.Request["cd"] == null)
				{
					this.JS.Text = "alert('参数错误!')";
					return;
				}
				this.NewsName = base.Request["na"].ToString();
				this.NewsCode = base.Request["cd"].ToString();
				this.NewsId = Convert.ToInt32(base.Request["nid"]);
				this.LbNewsName.Text = this.NewsName;
				this.LbNewsName1.Text = this.NewsName;
				if (this.NewsId != 0)
				{
					this.NewsBt = base.Request["nbt"].ToString();
					this.NewsInfoDataBind();
					return;
				}
				this.FileUpload1.RecordCode = this.hdnRecorde.Value;
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
		private News NewsInfo()
		{
			News news = new News();
			news.i_xw_id = this.NewsId;
			news.c_xwlxdm = this.NewsCode;
			news.c_sfyx = "y";
			news.txt_xwnr = this.txtContent.Value.Replace("\r\n", "").Replace("'", "''");
			news.v_xwbt = this.TbxNewsBt.Text;
			DateTime dtm_fbsj;
			DateTime.TryParseExact(this.hdnRecorde.Value, "yyyyMMddHHmmss", null, DateTimeStyles.None, out dtm_fbsj);
			news.dtm_fbsj = dtm_fbsj;
			news.c_ttbj = "n";
			return news;
		}
		private void NewsInfoDataBind()
		{
			System.Data.DataTable newsInfo = this.na.GetNewsInfo(this.NewsId);
			this.TbxNewsBt.Text = newsInfo.Rows[0]["v_xwbt"].ToString();
			this.txtContent.Value = newsInfo.Rows[0]["txt_xwnr"].ToString();
			this.FileUpload1.Folder = "/UploadFiles/Web/News/" + Convert.ToDateTime(newsInfo.Rows[0]["dtm_fbsj"]).ToString("yyyyMMddHHmmss");
		}
		public void Btn_save_Click(object sender, EventArgs e)
		{
			if (this.NewsId == 0)
			{
				if (this.na.NewsAdd(this.NewsInfo()) == 1)
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_WebManagerList' });");
					return;
				}
				this.JS.Text = "top.ui.alert('保存失败!');";
				return;
			}
			else
			{
				if (this.na.NewsUpdate(this.NewsInfo()) == 1)
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_WebManagerList' });");
					return;
				}
				this.JS.Text = "top.ui.alert('保存失败!');";
				return;
			}
		}
	}


