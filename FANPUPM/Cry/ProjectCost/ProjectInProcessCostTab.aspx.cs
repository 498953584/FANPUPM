using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
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
	public partial class ProjectInProcessCostTab : PageBase, IRequiresSessionState
	{
		public AccountReportAction arAction
		{
			get
			{
				return new AccountReportAction();
			}
		}
		public string PrjCode
		{
			get
			{
				object obj = this.ViewState["PrjCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PrjCode"] = value;
			}
		}
		public DateTime dtDateTime
		{
			get
			{
				object obj = this.ViewState["dtDateTime"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return DateTime.Now;
			}
			set
			{
				this.ViewState["dtDateTime"] = value;
			}
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.LblDate.Text = DateTime.Now.ToString("yyyy年MM月dd");
				this.DDL_Bind();
				this.Bind_Date();
				this.DGrd_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.BtnSearch.Click += new EventHandler(this.BtnSearch_Click);
			this.DGrdMaterialExpend.ItemCreated += new DataGridItemEventHandler(this.DGrdMaterialExpend_ItemCreated);
			this.DGrdMaterialExpend.ItemDataBound += new DataGridItemEventHandler(this.DGrdMaterialExpend_ItemDataBound);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void Bind_Date()
		{
			try
			{
				this.DtbStartDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
			}
			catch
			{
			}
			this.DtbEndDate.Text = DateTime.Now.ToShortDateString();
		}
		private void DDL_Bind()
		{
			CryReport cryReport = new CryReport();
			this.DDLInProcess.DataSource = cryReport.GetWorkCostpcListoverbuid();
			this.DDLInProcess.DataTextField = "pn";
			this.DDLInProcess.DataValueField = "pc";
			this.DDLInProcess.DataBind();
		}
		private void DGrd_Bind()
		{
			try
			{
				this.PrjCode = this.DDLInProcess.SelectedValue.ToString().Trim();
			}
			catch
			{
			}
			DateTime dtStartDate = Convert.ToDateTime(this.DtbStartDate.Text.Trim());
			DateTime dtEndDate = Convert.ToDateTime(this.DtbEndDate.Text.Trim());
			DataTable projectInProcessAccount = this.arAction.GetProjectInProcessAccount(this.PrjCode, dtStartDate, dtEndDate);
			this.DGrdMaterialExpend.DataSource = projectInProcessAccount;
			this.DGrdMaterialExpend.DataBind();
		}
		private void DGrdMaterialExpend_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
			{
				TableCell tableCell = new TableCell();
				tableCell.RowSpan = 2;
				tableCell.BackColor = Color.FromName("#ece9d8");
				TableCell expr_2E = tableCell;
				expr_2E.Text += " <font style=\"FONT-WEIGHT: bold\">项目名称</font></td> ";
				TableCell expr_44 = tableCell;
				expr_44.Text += "   <td colspan=14 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">本月数</font></td> ";
				TableCell expr_5A = tableCell;
				expr_5A.Text += "   <td colspan=5 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">本年度累计</font></td> ";
				TableCell expr_70 = tableCell;
				expr_70.Text += "   <td colspan=5 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">跨年度累计</font></td> ";
				TableCell expr_86 = tableCell;
				expr_86.Text += "  </tr> ";
				TableCell expr_9C = tableCell;
				expr_9C.Text += "  <tr> ";
				TableCell expr_B2 = tableCell;
				expr_B2.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">其它预算</font></td> ";
				TableCell expr_C8 = tableCell;
				expr_C8.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">人工费</font></td> ";
				TableCell expr_DE = tableCell;
				expr_DE.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">外包人工费</font></td> ";
				TableCell expr_F4 = tableCell;
				expr_F4.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">材料费用</font></td> ";
				TableCell expr_10A = tableCell;
				expr_10A.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">主材</font></td> ";
				TableCell expr_120 = tableCell;
				expr_120.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">辅材</font></td> ";
				TableCell expr_136 = tableCell;
				expr_136.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">机械费</font></td> ";
				TableCell expr_14C = tableCell;
				expr_14C.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">其他直接费</font></td> ";
				TableCell expr_162 = tableCell;
				expr_162.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">其它间接费</font></td> ";
				TableCell expr_178 = tableCell;
				expr_178.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际成本合计</font></td> ";
				TableCell expr_18E = tableCell;
				expr_18E.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">降低额</font></td> ";
				TableCell expr_1A4 = tableCell;
				expr_1A4.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">降低率</font></td> ";
				TableCell expr_1BA = tableCell;
				expr_1BA.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">预算成本</font></td> ";
				TableCell expr_1D0 = tableCell;
				expr_1D0.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际成本</font></td> ";
				TableCell expr_1E6 = tableCell;
				expr_1E6.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">降低额</font></td> ";
				TableCell expr_1FC = tableCell;
				expr_1FC.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">降低率</font></td> ";
				TableCell expr_212 = tableCell;
				expr_212.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">预算成本</font></td> ";
				TableCell expr_228 = tableCell;
				expr_228.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际成本</font></td> ";
				TableCell expr_23E = tableCell;
				expr_23E.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">降低额</font></td> ";
				TableCell expr_254 = tableCell;
				expr_254.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">降低率</font></td> ";
				e.Item.Cells.Clear();
				e.Item.Cells.Add(tableCell);
			}
		}
		private void BtnSearch_Click(object sender, EventArgs e)
		{
			this.DGrd_Bind();
		}
		protected void btnexecl_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "在建工程成本明细表.xls");
		}
		protected void btnWord_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "在建工程成本明细表.doc");
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
			this.TableOUT.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
		private void DGrdMaterialExpend_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				try
				{
					e.Item.Cells[13].Text = Convert.ToDouble(dataRowView["CurrMonthReducePercent"].ToString()).ToString("p");
				}
				catch
				{
				}
				try
				{
					e.Item.Cells[18].Text = Convert.ToDouble(dataRowView["CurrYearReducePercent"].ToString()).ToString("p");
				}
				catch
				{
				}
				try
				{
					e.Item.Cells[23].Text = Convert.ToDouble(dataRowView["SplitYearReducePercent"].ToString()).ToString("p");
				}
				catch
				{
				}
			}
		}
	}


