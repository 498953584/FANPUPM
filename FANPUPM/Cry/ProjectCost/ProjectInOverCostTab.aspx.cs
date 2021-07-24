using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
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
	public partial class ProjectInOverCostTab : PageBase, IRequiresSessionState
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
				this.DDL_Bind();
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
			this.DGrdMaterialExpend.ItemCreated += new DataGridItemEventHandler(this.DGrdMaterialExpend_ItemCreated);
			this.DGrdMaterialExpend.ItemDataBound += new DataGridItemEventHandler(this.DGrdMaterialExpend_ItemDataBound);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void DDL_Bind()
		{
			CryReport cryReport = new CryReport();
			this.DDLInProcess.DataSource = cryReport.GetWorkCostpcListover();
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
			DataTable projectInOverAccount = this.arAction.GetProjectInOverAccount(this.PrjCode);
			this.DGrdMaterialExpend.DataSource = projectInOverAccount;
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
				expr_44.Text += "   <td colspan=3 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">人工费</font></td> ";
				TableCell expr_5A = tableCell;
				expr_5A.Text += "   <td colspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">材料费</font></td> ";
				TableCell expr_70 = tableCell;
				expr_70.Text += "   <td colspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">主材</font></td> ";
				TableCell expr_86 = tableCell;
				expr_86.Text += "   <td colspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">辅材</font></td> ";
				TableCell expr_9C = tableCell;
				expr_9C.Text += "   <td colspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">机械费</font></td> ";
				TableCell expr_B2 = tableCell;
				expr_B2.Text += "   <td colspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">其他直接费</font></td> ";
				TableCell expr_C8 = tableCell;
				expr_C8.Text += "   <td colspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">其它间接费</font></td> ";
				TableCell expr_DE = tableCell;
				expr_DE.Text += "   <td colspan=4 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">合计</font></td> ";
				TableCell expr_F4 = tableCell;
				expr_F4.Text += "  </tr> ";
				TableCell expr_10A = tableCell;
				expr_10A.Text += "  <tr> ";
				TableCell expr_120 = tableCell;
				expr_120.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">预算</font></td> ";
				TableCell expr_136 = tableCell;
				expr_136.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际</font></td> ";
				TableCell expr_14C = tableCell;
				expr_14C.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">外包人工费</font></td> ";
				TableCell expr_162 = tableCell;
				expr_162.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">预算</font></td> ";
				TableCell expr_178 = tableCell;
				expr_178.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际</font></td> ";
				TableCell expr_18E = tableCell;
				expr_18E.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">预算</font></td> ";
				TableCell expr_1A4 = tableCell;
				expr_1A4.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际</font></td> ";
				TableCell expr_1BA = tableCell;
				expr_1BA.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">预算</font></td> ";
				TableCell expr_1D0 = tableCell;
				expr_1D0.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际</font></td> ";
				TableCell expr_1E6 = tableCell;
				expr_1E6.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">预算</font></td> ";
				TableCell expr_1FC = tableCell;
				expr_1FC.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际</font></td> ";
				TableCell expr_212 = tableCell;
				expr_212.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">预算</font></td> ";
				TableCell expr_228 = tableCell;
				expr_228.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际</font></td> ";
				TableCell expr_23E = tableCell;
				expr_23E.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">预算</font></td> ";
				TableCell expr_254 = tableCell;
				expr_254.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际</font></td> ";
				TableCell expr_26A = tableCell;
				expr_26A.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold;Font-Color:red\">预算成本</font></td> ";
				TableCell expr_280 = tableCell;
				expr_280.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际成本</font></td> ";
				TableCell expr_296 = tableCell;
				expr_296.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">降低额</font></td> ";
				TableCell expr_2AC = tableCell;
				expr_2AC.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">降低率</font></td> ";
				e.Item.Cells.Clear();
				e.Item.Cells.Add(tableCell);
			}
		}
		private void BtnSearch_Click(object sender, EventArgs e)
		{
			this.DGrd_Bind();
		}
		private void DGrdMaterialExpend_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				try
				{
					e.Item.Cells[19].Text = Convert.ToDouble(dataRowView["ReducePercent"].ToString()).ToString("p");
				}
				catch
				{
				}
				try
				{
					e.Item.Cells[23].Text = Convert.ToDouble(dataRowView["SumReduceCurrYearPercent"].ToString()).ToString("p");
				}
				catch
				{
				}
			}
		}
		protected void btnexecl_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "竣工工程成本明细表.xls");
		}
		protected void btnWord_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "竣工工程成本明细表.doc");
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
		protected void DDLInProcess_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.DGrd_Bind();
		}
	}


