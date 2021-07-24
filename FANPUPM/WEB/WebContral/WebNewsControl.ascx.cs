using ASP;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
	public partial class WebNewsControl : System.Web.UI.UserControl
	{

		public string NewsCode
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
		public int TopNum
		{
			get
			{
				return Convert.ToInt32(this.ViewState["TOPNUM"]);
			}
			set
			{
				this.ViewState["TOPNUM"] = value;
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
				this.CreateNewsList();
			}
		}
		private void CreateNewsList()
		{
			DataTable dataTable = this.na.NewsSel(this.NewsCode);
			string text = "";
			int count = dataTable.Rows.Count;
			if (count > this.TopNum)
			{
				for (int i = 1; i <= this.TopNum; i++)
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"<li><a href='/HXWEB/NewsView.aspx?NewsID=",
						dataTable.Rows[i - 1]["i_xw_id"].ToString(),
						"' target=\"_blank\">",
						this.FormatNewsListBT(dataTable.Rows[i - 1]["v_xwbt"].ToString()),
						"</a> [",
						Convert.ToDateTime(dataTable.Rows[i - 1]["dtm_fbsj"]).ToString("yyyy-MM-dd"),
						"]</li>"
					});
				}
			}
			else
			{
				for (int j = 1; j <= count; j++)
				{
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						"<li><a href='/HXWEB/NewsView.aspx?NewsID=",
						dataTable.Rows[j - 1]["i_xw_id"].ToString(),
						"' target=\"_blank\">",
						this.FormatNewsListBT(dataTable.Rows[j - 1]["v_xwbt"].ToString()),
						"</a> [",
						Convert.ToDateTime(dataTable.Rows[j - 1]["dtm_fbsj"]).ToString("yyyy-MM-dd"),
						"]</li>"
					});
				}
			}
			this.Literal1.Text = text;
		}
		private string FormatNewsListBT(string bt)
		{
			if (bt.Length > 21)
			{
				bt = bt.Substring(0, 18) + "... ";
			}
			return bt;
		}
		private int GetNewsPic(string strDate)
		{
			string sqlString = string.Concat(new string[]
			{
				"select datediff(d,'",
				strDate,
				"','",
				DateTime.Today.ToString(),
				"')"
			});
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			return Convert.ToInt32(dataTable.Rows[0][0].ToString());
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
	}


