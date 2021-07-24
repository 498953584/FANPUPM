using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class WebSel : BasePage, IRequiresSessionState
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
			base.Response.Cache.SetNoStore();
			if (!base.IsPostBack)
			{
				if (base.Request["cd"] == null)
				{
					this.JS.Text = "alert('参数错误!')";
					return;
				}
				this.NewsName = "";
				this.NewsCode = base.Request["cd"].ToString();
				this.NewsId = Convert.ToInt32(base.Request["nid"]);
				this.NewsInfoDataBind();
			}
		}
		public void InitFuJian(string addTime)
		{
			this.Literal1.Text = "暂无";
			string text = base.Server.MapPath("/UploadFiles/Web/News/" + addTime);
			if (Directory.Exists(text))
			{
				this.Literal1.Text = "";
				string[] files = Directory.GetFiles(text);
				string arg_4E_0 = string.Empty;
				string text2 = string.Empty;
				string[] array = files;
				for (int i = 0; i < array.Length; i++)
				{
					string path = array[i];
					text2 = Path.GetFileName(path);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("<li><a class='link' target='_blank' href='/Common/DownLoad.aspx?path=");
					stringBuilder.Append(string.Concat(new string[]
					{
						"/UploadFiles/Web/News/",
						addTime,
						"/",
						HttpUtility.UrlEncode(text2, Encoding.UTF8),
						"'>"
					}));
					stringBuilder.Append(text2);
					stringBuilder.Append("</a></li>");
					Literal expr_F0 = this.Literal1;
					expr_F0.Text += stringBuilder.ToString();
				}
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
			System.Data.DataTable newsInfo = this.na.GetNewsInfo(this.NewsId);
			this.Lbxwnr.Text = newsInfo.Rows[0]["txt_xwnr"].ToString();
			this.Lbbt.Text = newsInfo.Rows[0]["v_xwbt"].ToString();
			this.InitFuJian(Convert.ToDateTime(newsInfo.Rows[0]["dtm_fbsj"]).ToString("yyyyMMddHHmmss"));
		}
	}


