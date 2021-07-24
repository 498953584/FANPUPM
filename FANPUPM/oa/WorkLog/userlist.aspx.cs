using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class userList : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				workLogDb workLogDb = new workLogDb();
				DataTable dataSource = new DataTable();
				DataTable dataTable = new DataTable();
				string text = "";
				int depId;
				if (base.Request.QueryString["code"] != null)
				{
					depId = Convert.ToInt32(this.Page.Request.QueryString["code"].ToString());
				}
				else
				{
					depId = 1;
				}
				dataSource = workLogDb.getDepTable(depId);
				this.DataGrid1.DataSource = dataSource;
				this.DataGrid1.DataBind();
				dataTable = workLogDb.getTable();
				if (dataTable.Rows.Count > 0)
				{
					text += " 从 ";
					if (dataTable.Rows[0]["dtm_kq"].ToString().Length > 0)
					{
						text += Convert.ToDateTime(dataTable.Rows[0]["dtm_kq"].ToString()).ToString("d");
					}
					text += " 到 ";
					if (dataTable.Rows[0]["dtm_kqe"].ToString().Length > 0)
					{
						text += Convert.ToDateTime(dataTable.Rows[0]["dtm_kqe"].ToString()).ToString("d");
					}
					else
					{
						text += DateTime.Today.ToString("d");
					}
					text += " 的考核结果：单位（篇）";
				}
				this.headLbl.Text = text;
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
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
		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				workLogDb workLogDb = new workLogDb();
				DataTable dataTable = new DataTable();
				dataTable = workLogDb.getTable();
				string text = "";
				string text2 = "";
				string text3 = ((DataRowView)e.Item.DataItem)["v_yhdm"].ToString();
				string text4 = ((DataRowView)e.Item.DataItem)["v_xm"].ToString();
				if (dataTable.Rows.Count > 0)
				{
					text = Convert.ToDateTime(dataTable.Rows[0]["dtm_kq"].ToString()).ToString("d");
					if (dataTable.Rows[0]["dtm_kqe"].ToString().Length > 0)
					{
						text2 = Convert.ToDateTime(dataTable.Rows[0]["dtm_kqe"].ToString()).ToString("d");
					}
					else
					{
						text2 = DateTime.Today.ToString("d");
					}
				}
				int logNum = workLogDb.getLogNum(text, text2, text3, 'y');
				int logNum2 = workLogDb.getLogNum(text, text2, text3, 'n');
				int num = this.requestNum(text, text2) - logNum;
				((Label)e.Item.Cells[2].Controls[1]).Text = logNum.ToString();
				((Label)e.Item.Cells[3].Controls[1]).Text = logNum2.ToString();
				((Label)e.Item.Cells[4].Controls[1]).Text = num.ToString();
				((HyperLink)e.Item.Cells[0].Controls[1]).NavigateUrl = string.Concat(new string[]
				{
					"logListManage.aspx?yhdm=",
					text3,
					"&xm=",
					text4,
					"&start=",
					text,
					"&end=",
					text2
				});
				((HyperLink)e.Item.Cells[0].Controls[1]).Text = text4;
			}
		}
	}


