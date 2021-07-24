using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
	public partial class prjSearList : NBasePage, IRequiresSessionState
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

		protected override void OnInit(System.EventArgs e)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			base.OnInit(e);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.reportid = base.Request.QueryString["reportid"];
				this.DG1_BIND();
			}
		}
		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.AspNetPager1.CurrentPageIndex = 1;
			this.DG1_BIND();
		}
		public void DG1_BIND()
		{
			System.Data.DataTable dataSource = new System.Data.DataTable();
			this.AspNetPager1.RecordCount = PMAction.GetPrjCount(this.txtPrjCode.Text.Trim(), this.txtprjName.Text.Trim(), "", "", this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), base.UserCode);
			dataSource = PMAction.GetPrjInfo(this.txtPrjCode.Text.Trim(), this.txtprjName.Text.Trim(), "", "", this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), base.UserCode, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
			this.gvPrjInfo.DataSource = dataSource;
			this.gvPrjInfo.DataBind();
		}
		protected void gvPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				string value = this.gvPrjInfo.DataKeys[e.Row.RowIndex]["PrjGuid"].ToString();
				e.Row.Attributes["id"] = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["child"] = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Values[2].ToString();
				e.Row.Attributes["prjGuid"] = value;
				e.Row.Attributes["guid"] = value;
				string text = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Values[1].ToString();
				e.Row.Attributes["typeCode"] = text;
				if (text.Length == 5)
				{
					e.Row.Attributes["isMainContract"] = "True";
				}
				else
				{
					e.Row.Attributes["isMainContract"] = "False";
				}
				if (e.Row.Cells[1].Text.Length > 5)
				{
					e.Row.Cells[1].Text = e.Row.Cells[1].Text.Substring(0, 5) + "<font color=\"#ff0000\">" + e.Row.Cells[1].Text.Substring(5, e.Row.Cells[1].Text.Length - 5) + "</font>";
				}
				string a = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Values[3].ToString();
				if (a == "-1")
				{
					e.Row.Cells[8].ForeColor = Color.Blue;
					e.Row.Cells[8].Text = "在建";
				}
				if (a == "4")
				{
					e.Row.Cells[8].Text = "在建";
				}
				if (a == "6")
				{
					e.Row.Cells[8].Text = "停工";
				}
				if (a == "7")
				{
					e.Row.Cells[8].Text = "竣工";
				}
				if (a == "8")
				{
					e.Row.Cells[8].Text = "验收";
				}
				if (a == "9")
				{
					e.Row.Cells[8].Text = "维保";
				}
				if (a == "0")
				{
					e.Row.Cells[8].Text = "";
				}
			}
		}
		protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
		{
			this.DG1_BIND();
		}
		protected void btnexecl_Click(object sender, System.EventArgs e)
		{
			System.Data.DataTable dataTable = new System.Data.DataTable();
			new System.Text.StringBuilder();
			dataTable = PMAction.GetPrjInfo(this.txtPrjCode.Text.Trim(), this.txtprjName.Text.Trim(), "", "", this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), base.UserCode, 0, 0);
			System.Data.DataTable dataTable2 = new System.Data.DataTable();
			dataTable2.Columns.Add(new System.Data.DataColumn("序号", typeof(string)));
			dataTable2.Columns.Add(new System.Data.DataColumn("项目编号", typeof(string)));
			dataTable2.Columns.Add(new System.Data.DataColumn("项目名称", typeof(string)));
			dataTable2.Columns.Add(new System.Data.DataColumn("建设单位", typeof(string)));
			dataTable2.Columns.Add(new System.Data.DataColumn("工程造价", typeof(string)));
			dataTable2.Columns.Add(new System.Data.DataColumn("项目地点", typeof(string)));
			dataTable2.Columns.Add(new System.Data.DataColumn("开始时间", typeof(string)));
			dataTable2.Columns.Add(new System.Data.DataColumn("结束时间", typeof(string)));
			dataTable2.Columns.Add(new System.Data.DataColumn("项目状态", typeof(string)));
			foreach (System.Data.DataRow dataRow in dataTable.Rows)
			{
				System.Data.DataRow dataRow2 = dataTable2.NewRow();
				dataRow2["序号"] = dataRow["TypeCode"].ToString();
				dataRow2["项目编号"] = dataRow["PrjCode"].ToString();
				dataRow2["项目名称"] = dataRow["PrjName"].ToString();
				dataRow2["建设单位"] = dataRow["Owner"].ToString();
				dataRow2["工程造价"] = dataRow["PrjCost"].ToString();
				dataRow2["项目地点"] = dataRow["PrjPlace"].ToString();
				dataRow2["开始时间"] = dataRow["StartDate"].ToString();
				dataRow2["结束时间"] = dataRow["EndDate"].ToString();
				string value = string.Empty;
				if (dataRow["PrjState"].ToString() == "-1")
				{
					value = "在建";
				}
				if (dataRow["PrjState"].ToString() == "4")
				{
					value = "在建";
				}
				if (dataRow["PrjState"].ToString() == "6")
				{
					value = "停工";
				}
				if (dataRow["PrjState"].ToString() == "7")
				{
					value = "竣工";
				}
				if (dataRow["PrjState"].ToString() == "8")
				{
					value = "验收";
				}
				if (dataRow["PrjState"].ToString() == "9")
				{
					value = "维保";
				}
				if (dataRow["PrjState"].ToString() == "0")
				{
					value = "";
				}
				dataRow2["项目状态"] = value;
				dataTable2.Rows.Add(dataRow2);
			}
			new Common2().ExportToExecelAll(dataTable2, "项目信息.xls", base.Request.Browser.Browser);
		}
		protected void btnWord_Click(object sender, System.EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "项目信息.doc");
		}
		public void ExportToExcel(string FileType, string FileName)
		{
			base.Response.Charset = "GB2312";
			base.Response.ContentEncoding = System.Text.Encoding.UTF8;
			base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
			base.Response.ContentType = FileType;
			this.EnableViewState = false;
			System.IO.StringWriter stringWriter = new System.IO.StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			this.Table1.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
	}


