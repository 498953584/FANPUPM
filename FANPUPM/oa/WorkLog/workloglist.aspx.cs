using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.sysManage.common;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class workLogList : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			new com.jwsoft.sysManage.common.Calendar(this.Page);
			this.startDate.Attributes["onclick"] = "opencalendar(this)";
			this.endDate.Attributes["onclick"] = "opencalendar(this)";
			if (!this.Page.IsPostBack)
			{
				DateTime dateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
				this.startDate.Text = dateTime.ToString("d");
				this.endDate.Text = DateTime.Today.ToString("d");
				DataTable dataSource = new DataTable();
				workLogDb workLogDb = new workLogDb();
				dataSource = workLogDb.getTable(this.Page.Session["yhdm"].ToString());
				this.DataGrid1.DataSource = dataSource;
				this.DataGrid1.DataBind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DataGrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
		}
		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				string b = DateTime.Today.ToString("d");
				if (Convert.ToDateTime(this.DataGrid1.DataKeys[e.Item.ItemIndex]).ToString("d") == b)
				{
					((HyperLink)e.Item.Cells[1].Controls[1]).Text = "修改";
					return;
				}
				((HyperLink)e.Item.Cells[1].Controls[1]).Enabled = false;
			}
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
			return result;
		}
		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			DataTable dataSource = new DataTable();
			workLogDb workLogDb = new workLogDb();
			string value = this.searchFlag.Value;
			if (value == "true")
			{
				string value2 = this.hidStart.Value;
				string value3 = this.hidEnd.Value;
				dataSource = workLogDb.getTable(this.Page.Session["yhdm"].ToString(), value2, value3);
			}
			else
			{
				dataSource = workLogDb.getTable(this.Page.Session["yhdm"].ToString());
			}
			this.DataGrid1.DataSource = dataSource;
			this.DataGrid1.DataBind();
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			string yhdm = this.Page.Session["yhdm"].ToString();
			this.searchFlag.Value = "true";
			string text = this.startDate.Text;
			string text2 = this.endDate.Text;
			this.hidStart.Value = text;
			this.hidEnd.Value = text2;
			workLogDb workLogDb = new workLogDb();
			DataTable table = workLogDb.getTable(yhdm, text, text2);
			this.DataGrid1.DataSource = table;
			this.DataGrid1.CurrentPageIndex = 0;
			this.DataGrid1.DataBind();
		}
	}


