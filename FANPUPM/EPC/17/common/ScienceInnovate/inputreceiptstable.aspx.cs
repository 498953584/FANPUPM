using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class InputReceiptsTable : BasePage, IRequiresSessionState
	{
		protected int PlanNo;
		protected string PlanYear;
		protected int iIndex;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.PlanNo = int.Parse(base.Request.Params["MainId"].ToString());
				this.BindData();
			}
		}
		private void BindData()
		{
			bool flag = false;
			inputReceiptReportCollectin reportInfo = inputReceiptAction.GetReportInfo(this.PlanNo, out this.PlanYear);
			this.lbPlanYear.Text = this.PlanYear;
			foreach (inputReceiptReportInfo inputReceiptReportInfo in reportInfo)
			{
				if (!inputReceiptReportInfo.IsProject)
				{
					this.CreateDpm(inputReceiptReportInfo, false);
				}
				else
				{
					if (!flag)
					{
						flag = true;
						this.CreateDpm(new inputReceiptReportInfo
						{
							PrjName = "项目经理部",
							InputAmount = reportInfo.GetPrjInputAmount(),
							InputCompAmount = reportInfo.GetPrjInputCompAmount(),
							ReceiptAmount = reportInfo.GetPrjReceiptAmount(),
							ReceiptCompAmount = reportInfo.GetPrjReceiptCompAmount()
						}, true);
						this.CreateAllPrjRows(reportInfo);
					}
				}
			}
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			this.iIndex++;
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.ColSpan = 3;
			htmlTableCell.InnerText = "合计：" + reportInfo.GetAllReceiptAmount().ToString();
			htmlTableRow.Style.Add("FONT-WEIGHT", "bold");
			htmlTableRow.Cells.Add(htmlTableCell);
			HtmlTableCell htmlTableCell2 = new HtmlTableCell();
			htmlTableCell2.InnerText = reportInfo.GetAllReceiptCompAmount().ToString();
			htmlTableRow.Cells.Add(htmlTableCell2);
			HtmlTableCell htmlTableCell3 = new HtmlTableCell();
			if (reportInfo.GetAllReceiptAmount() != 0m)
			{
				htmlTableCell3.InnerText = Convert.ToString(decimal.Round(reportInfo.GetAllReceiptCompAmount() / reportInfo.GetAllReceiptAmount() * 100m, 2));
			}
			else
			{
				htmlTableCell3.InnerText = "0";
			}
			htmlTableRow.Cells.Add(htmlTableCell3);
			HtmlTableCell htmlTableCell4 = new HtmlTableCell();
			htmlTableCell4.InnerText = reportInfo.GetAllInputAmount().ToString();
			htmlTableRow.Cells.Add(htmlTableCell4);
			HtmlTableCell htmlTableCell5 = new HtmlTableCell();
			htmlTableCell5.InnerText = reportInfo.GetAllInputCompAmount().ToString();
			htmlTableRow.Cells.Add(htmlTableCell5);
			HtmlTableCell htmlTableCell6 = new HtmlTableCell();
			if (reportInfo.GetAllInputAmount() != 0m)
			{
				htmlTableCell6.InnerText = Convert.ToString(decimal.Round(reportInfo.GetAllInputCompAmount() / reportInfo.GetAllInputAmount() * 100m, 2));
			}
			else
			{
				htmlTableCell6.InnerText = "0";
			}
			htmlTableRow.Cells.Add(htmlTableCell6);
			htmlTableRow.Height = "22";
			this.tbReport.Rows.Add(htmlTableRow);
		}
		private void CreateAllPrjRows(inputReceiptReportCollectin objInfos)
		{
			int num = 0;
			foreach (inputReceiptReportInfo inputReceiptReportInfo in objInfos)
			{
				if (inputReceiptReportInfo.IsProject)
				{
					num++;
					this.CreatePrjRow(num, inputReceiptReportInfo);
				}
			}
			this.hidPrjRowCount.Value = num.ToString();
		}
		private void CreatePrjRow(int ChildIndex, inputReceiptReportInfo objInfo)
		{
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.InnerText = this.iIndex.ToString() + "." + ChildIndex.ToString();
			htmlTableRow.ID = ChildIndex.ToString();
			htmlTableRow.Cells.Add(htmlTableCell);
			HtmlTableCell htmlTableCell2 = new HtmlTableCell();
			htmlTableCell2.InnerHtml = "&nbsp;&nbsp;" + objInfo.PrjName;
			htmlTableRow.Cells.Add(htmlTableCell2);
			HtmlTableCell htmlTableCell3 = new HtmlTableCell();
			htmlTableCell3.InnerText = objInfo.ReceiptAmount.ToString();
			htmlTableRow.Cells.Add(htmlTableCell3);
			HtmlTableCell htmlTableCell4 = new HtmlTableCell();
			htmlTableCell4.InnerText = objInfo.ReceiptCompAmount.ToString();
			htmlTableRow.Cells.Add(htmlTableCell4);
			HtmlTableCell htmlTableCell5 = new HtmlTableCell();
			htmlTableCell5.InnerText = objInfo.ReceiptCompRate.ToString();
			htmlTableRow.Cells.Add(htmlTableCell5);
			HtmlTableCell htmlTableCell6 = new HtmlTableCell();
			htmlTableCell6.InnerText = objInfo.InputAmount.ToString();
			htmlTableRow.Cells.Add(htmlTableCell6);
			HtmlTableCell htmlTableCell7 = new HtmlTableCell();
			htmlTableCell7.InnerText = objInfo.InputCompAmount.ToString();
			htmlTableRow.Cells.Add(htmlTableCell7);
			HtmlTableCell htmlTableCell8 = new HtmlTableCell();
			htmlTableCell8.InnerText = objInfo.InputCompRate.ToString();
			htmlTableRow.Cells.Add(htmlTableCell8);
			htmlTableRow.Height = "22";
			htmlTableRow.Style.Add("Display", "none");
			htmlTableRow.Attributes["onclick"] = "doClick(this,\"tbReport\");";
			htmlTableRow.Attributes["onmouseover"] = "doMouseOver(this);";
			htmlTableRow.Attributes["onmouseout"] = "doMouseOut(this);";
			this.tbReport.Rows.Add(htmlTableRow);
		}
		private void CreateDpm(inputReceiptReportInfo objInfo, bool isPrj)
		{
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			this.iIndex++;
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableCell.InnerText = this.iIndex.ToString();
			htmlTableCell.Style.Add("FONT-WEIGHT", "bold");
			htmlTableRow.Cells.Add(htmlTableCell);
			HtmlTableCell htmlTableCell2 = new HtmlTableCell();
			htmlTableCell2.InnerText = objInfo.PrjName;
			htmlTableRow.Cells.Add(htmlTableCell2);
			HtmlTableCell htmlTableCell3 = new HtmlTableCell();
			htmlTableCell3.InnerText = objInfo.ReceiptAmount.ToString();
			htmlTableRow.Cells.Add(htmlTableCell3);
			HtmlTableCell htmlTableCell4 = new HtmlTableCell();
			htmlTableCell4.InnerText = objInfo.ReceiptCompAmount.ToString();
			htmlTableRow.Cells.Add(htmlTableCell4);
			HtmlTableCell htmlTableCell5 = new HtmlTableCell();
			htmlTableCell5.InnerText = objInfo.ReceiptCompRate.ToString();
			htmlTableRow.Cells.Add(htmlTableCell5);
			HtmlTableCell htmlTableCell6 = new HtmlTableCell();
			htmlTableCell6.InnerText = objInfo.InputAmount.ToString();
			htmlTableRow.Cells.Add(htmlTableCell6);
			HtmlTableCell htmlTableCell7 = new HtmlTableCell();
			htmlTableCell7.InnerText = objInfo.InputCompAmount.ToString();
			htmlTableRow.Cells.Add(htmlTableCell7);
			HtmlTableCell htmlTableCell8 = new HtmlTableCell();
			htmlTableCell8.InnerText = objInfo.InputCompRate.ToString();
			htmlTableRow.Cells.Add(htmlTableCell8);
			htmlTableRow.Height = "22";
			htmlTableRow.Attributes["onclick"] = "doClick(this,\"tbReport\");";
			htmlTableRow.Attributes["onmouseover"] = "doMouseOver(this);";
			htmlTableRow.Attributes["onmouseout"] = "doMouseOut(this);";
			if (isPrj)
			{
				htmlTableRow.Attributes["onclick"] = "doClick(this,\"tbReport\");controlRow();";
			}
			this.tbReport.Rows.Add(htmlTableRow);
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


