using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ReportProcedure : PageBase, IRequiresSessionState
	{

		public QueryItemCollection ItemList
		{
			get
			{
				object obj = this.ViewState["ItemList"];
				if (obj != null)
				{
					return (QueryItemCollection)obj;
				}
				return new QueryItemCollection();
			}
			set
			{
				this.ViewState["ItemList"] = value;
			}
		}
		public string reportid
		{
			get
			{
				return (string)this.ViewState["reportid"];
			}
			set
			{
				this.ViewState["reportid"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.reportid = base.Request.QueryString["reportid"];
				DataRow reportRow = QueryItemAction.GetReportRow(this.reportid);
				this.Lb_Title.Text = reportRow["Title"].ToString();
				int n = Convert.ToInt32(reportRow["headwidth"].ToString());
				string text = reportRow["Header"].ToString().Replace("[getdate()]", DateTime.Now.ToLongDateString());
				text = text.Replace("[yhmc]", com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.Session["yhdm"].ToString()));
				string[] array = text.Split(new char[]
				{
					';'
				});
				try
				{
					this.Lb_H1.Text = array[0];
					this.Lb_H2.Text = array[1];
					this.Lb_H3.Text = array[2];
				}
				catch
				{
				}
				string text2 = reportRow["Footer"].ToString().Replace("[getdate()]", DateTime.Now.ToLongDateString());
				text2 = text2.Replace("[yhmc]", com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.Session["yhdm"].ToString()));
				string[] array2 = text2.Split(new char[]
				{
					';'
				});
				try
				{
					this.Lb_F1.Text = array2[0];
					this.Lb_F2.Text = array2[1];
					this.Lb_F3.Text = array2[2];
				}
				catch
				{
				}
				if (this.Lb_Title.Text.Trim() == "")
				{
					this.TRTitle.Visible = false;
				}
				this.dgdOrder.Width = n;
				if (this.Lb_F1.Text.Trim() == "" && this.Lb_F2.Text.Trim() == "" && this.Lb_F3.Text.Trim() == "")
				{
					this.TrFooter.Visible = false;
				}
				if (this.Lb_H1.Text.Trim() == "" && this.Lb_H2.Text.Trim() == "" && this.Lb_H3.Text.Trim() == "")
				{
					this.TrHeader.Visible = false;
				}
				this.btnSearch.Attributes["onclick"] = "if (!openQuery(" + this.reportid + ")){return false;}";
				this.dgdOrder_Bind("");
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgdOrder.ItemDataBound += new DataGridItemEventHandler(this.dgdOrder_ItemDataBound);
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			string text = " ";
			QueryItemCollection queryItemCollection = (QueryItemCollection)this.Session["QUERY"];
			for (int i = 0; i < queryItemCollection.Count; i++)
			{
				text = text + queryItemCollection[i].StrSql.Replace("[]", queryItemCollection[i].ItemValue.Trim()).Trim() + "   ";
			}
			this.dgdOrder_Bind(text);
		}
		private void dgdOrder_Bind(string strWhere)
		{
			if (strWhere != "")
			{
				DataTable reportPList = QueryItemAction.GetReportPList(this.reportid, strWhere);
				this.dgdOrder.DataSource = QueryItemAction.GetReportPList(this.reportid, strWhere);
				if (reportPList.Rows.Count <= 0)
				{
					return;
				}
				this.dgdOrder.DataBind();
			}
		}
		private void dgdOrder_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				try
				{
					string a = ((DataRowView)e.Item.DataItem)["header"].ToString();
					if (a == "1")
					{
						e.Item.CssClass = "report_grid_head";
					}
					return;
				}
				catch
				{
					return;
				}
			}
			e.Item.CssClass = "report_grid_head";
		}
		protected void btnexecl_Click(object sender, EventArgs e)
		{
			this.Page.Response.Clear();
			base.Response.ContentType = "application/vnd.ms-excel;";
			base.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			this.Table1.RenderControl(writer);
			base.Response.Write(stringWriter);
			base.Response.End();
		}
	}


