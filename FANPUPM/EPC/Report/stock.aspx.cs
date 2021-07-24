using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class reportView : PageBase, IRequiresSessionState
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
				this.DG1.Width = n;
				if (this.Lb_F1.Text.Trim() == "" && this.Lb_F2.Text.Trim() == "" && this.Lb_F3.Text.Trim() == "")
				{
					this.TrFooter.Visible = false;
				}
				if (this.Lb_H1.Text.Trim() == "" && this.Lb_H2.Text.Trim() == "" && this.Lb_H3.Text.Trim() == "")
				{
					this.TrHeader.Visible = false;
				}
				this.btnSet.Attributes["onclick"] = "if (!openQuery(" + this.reportid + ")){return false;}";
				DataTable dataTable = QueryItemAction.CodeInfoList("21");
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					this.DropDownList1.Items.Add(dataTable.Rows[i][5].ToString());
				}
				if (dataTable.Rows.Count > 0)
				{
					this.DG1_BIND(" where (1=1)", dataTable.Rows[0][5].ToString());
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
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			string text = "   where (1=1) ";
			QueryItemCollection queryItemCollection = (QueryItemCollection)this.Session["QUERY"];
			for (int i = 0; i < queryItemCollection.Count; i++)
			{
				if (queryItemCollection[i].ItemValue.Trim() != "")
				{
					text = text + "   and " + queryItemCollection[i].StrSql.Replace("[]", queryItemCollection[i].ItemValue.Trim()).Trim();
				}
			}
			this.DG1_BIND(text, this.DropDownList1.SelectedValue);
		}
		public void DG1_BIND(string strWhere, string selectlist)
		{
			if (strWhere == "")
			{
				strWhere = "   where (1=0)";
			}
			DataTable reportLister = QueryItemAction.GetReportLister(this.reportid, strWhere, selectlist);
			this.DG1.DataSource = reportLister;
			this.DG1.DataBind();
		}
		protected void DG1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowIndex != -1)
			{
				e.Row.Cells[0].Text = Convert.ToString(Convert.ToInt32(e.Row.RowIndex) + 1);
				DataRowView arg_4B_0 = (DataRowView)e.Row.DataItem;
				e.Row.Attributes["onclick"] = "OnRecord(this);";
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
			}
		}
		protected void btnexecl_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", this.Lb_Title.Text + ".xls");
		}
		protected void btnWord_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", this.Lb_Title.Text + ".doc");
		}
		public void ExportToExcel(string FileType, string FileName)
		{
			base.Response.Charset = "GB2312";
			base.Response.ContentEncoding = Encoding.UTF8;
			base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
			base.Response.ContentType = FileType;
			this.EnableViewState = false;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			this.Table1.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
		protected void DG1_Sorting(object sender, GridViewSortEventArgs e)
		{
			string sortExpression = e.SortExpression;
			if (this.ViewState["SortOrder"].ToString() != sortExpression)
			{
				this.ViewState["SortOrder"] = e.SortExpression;
			}
		}
		protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.DG1_BIND(" where (1=1)", this.DropDownList1.SelectedValue.ToString());
		}
	}


