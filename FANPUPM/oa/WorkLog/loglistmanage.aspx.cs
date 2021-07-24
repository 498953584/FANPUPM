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
	public partial class logListManage : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				string yhdm = this.Page.Request.QueryString["yhdm"].ToString();
				string text = this.Page.Request.QueryString["xm"].ToString();
				string text2 = this.Page.Request.QueryString["start"].ToString();
				string text3 = this.Page.Request.QueryString["end"].ToString();
				workLogDb workLogDb = new workLogDb();
				string text4 = workLogDb.getLogNum(text2, text3, yhdm, 'y').ToString();
				string text5 = workLogDb.getLogNum(text2, text3, yhdm, 'n').ToString();
				int value = this.requestNum(text2, text3) - Convert.ToInt32(text4);
				string text6 = Convert.ToString(value);
				string text7 = string.Concat(new string[]
				{
					" 从",
					text2,
					"截止",
					text3,
					"， ",
					text,
					" 的工作日志为 ",
					text4,
					" 篇，被剔除",
					text5,
					"篇，缺少",
					text6,
					"篇"
				});
				this.headLbl.Text = text7;
				DataTable dataSource = new DataTable();
				dataSource = workLogDb.getTable(yhdm);
				this.DataGrid1.DataSource = dataSource;
				this.DataGrid1.DataBind();
			}
		}
		public int requestNum(string start, string end)
		{
			int num = 0;
			DateTime t = Convert.ToDateTime(start);
			DateTime t2 = Convert.ToDateTime(end);
			while (t2 >= t)
			{
				if (t.DayOfWeek.ToString() != "Saturday" && t.DayOfWeek.ToString() != "Sunday")
				{
					num++;
				}
				t = t.AddDays(1.0);
			}
			return num;
		}
		public string truncString(string oldStr, int length)
		{
			int num = 0;
			int num2 = 0;
			string result = "";
			for (int i = 0; i < oldStr.Length; i++)
			{
				char c = oldStr[i];
				if (c > '\u007f')
				{
					num += 2;
				}
				else
				{
					num++;
				}
				if (num > length)
				{
					result = oldStr.Substring(0, num2) + "...";
					break;
				}
				num2++;
			}
			if (num <= length)
			{
				result = oldStr;
			}
			return result;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DataGrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.DeleteCommand += new DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
			this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
		}
		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				this.Page.Request.QueryString["yhdm"].ToString();
				this.Page.Request.QueryString["xm"].ToString();
				this.Page.Request.QueryString["start"].ToString();
				this.Page.Request.QueryString["end"].ToString();
				string str = ((DataRowView)e.Item.DataItem)["i_gzrz_id"].ToString();
				if (((DataRowView)e.Item.DataItem)["c_bs"].ToString() == "y")
				{
					((Image)e.Item.Cells[0].Controls[1]).ImageUrl = "img/r.gif";
				}
				else
				{
					((Image)e.Item.Cells[0].Controls[1]).ImageUrl = "img/x.gif";
				}
				((HyperLink)e.Item.Cells[1].Controls[1]).Text = this.truncString(((DataRowView)e.Item.DataItem)["txt_rznr"].ToString().Trim(), 40);
				((HyperLink)e.Item.Cells[1].Controls[1]).NavigateUrl = "workLogPreview.aspx?id=" + str;
			}
		}
		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			string yhdm = this.Page.Request.QueryString["yhdm"].ToString();
			workLogDb workLogDb = new workLogDb();
			DataTable dataSource = new DataTable();
			dataSource = workLogDb.getTable(yhdm);
			this.DataGrid1.DataSource = dataSource;
			this.DataGrid1.DataBind();
		}
		private void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			string value = this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
			workLogDb workLogDb = new workLogDb();
			if (!workLogDb.logDel(Convert.ToInt32(value)))
			{
				this.js.Text = "alert ('未成功剔除！');";
			}
			string yhdm = this.Page.Request.QueryString["yhdm"].ToString();
			string text = this.Page.Request.QueryString["xm"].ToString();
			string text2 = this.Page.Request.QueryString["start"].ToString();
			string text3 = this.Page.Request.QueryString["end"].ToString();
			string text4 = workLogDb.getLogNum(text2, text3, yhdm, 'y').ToString();
			string text5 = workLogDb.getLogNum(text2, text3, yhdm, 'n').ToString();
			int value2 = this.requestNum(text2, text3) - Convert.ToInt32(text4);
			string text6 = Convert.ToString(value2);
			string text7 = string.Concat(new string[]
			{
				" 从",
				text2,
				"截止",
				text3,
				"， ",
				text,
				" 的工作日志为 ",
				text4,
				" 篇，被剔除",
				text5,
				"篇，缺少",
				text6,
				"篇"
			});
			this.headLbl.Text = text7;
			DataTable dataSource = new DataTable();
			dataSource = workLogDb.getTable(yhdm);
			this.DataGrid1.DataSource = dataSource;
			this.DataGrid1.DataBind();
		}
	}


