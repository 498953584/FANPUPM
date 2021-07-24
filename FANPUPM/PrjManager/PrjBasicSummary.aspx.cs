using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_PrjBasicSummary : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.ComputeTotal();
			this.BindGv();
		}
	}
	protected void BindGv()
	{
		this.AspNetPager1.RecordCount = ProjectInfo.GetPrjCountByCondition(this.txtName.Text.Trim(), this.txtCode.Text.Trim(), this.txtManager.Value.Trim(), this.txtPrjPlace.Value.Trim(), this.txtPrjDutyPerson.Value.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), base.UserCode);
		DataTable prjByCondition = ProjectInfo.GetPrjByCondition(this.txtName.Text.Trim(), this.txtCode.Text.Trim(), this.txtManager.Value.Trim(), this.txtPrjPlace.Value.Trim(), this.txtPrjDutyPerson.Value.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), base.UserCode, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvProject.DataSource = this.BindPrjType(prjByCondition, false, false);
		this.gvProject.DataBind();
		string[] value = (string[])this.ViewState["Total"];
		int[] index = (int[])this.ViewState["index"];
		GridViewUtility.AddTotalRow(this.gvProject, value, index);
	}
	protected void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable prjByCondition = ProjectInfo.GetPrjByCondition(this.txtName.Text.Trim(), this.txtCode.Text.Trim(), this.txtManager.Value.Trim(), this.txtPrjPlace.Value.Trim(), this.txtPrjDutyPerson.Value.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), base.UserCode, 0, 0);
		string[] array = new string[14];
		if (prjByCondition.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ContractPrice)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(prjByCondition.Compute("sum(BudTotal)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ConsTotal)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ConsResTotal)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ClearingPrice)", "1>0")).ToString("0.000");
			array[5] = System.Convert.ToDecimal(prjByCondition.Compute("sum(SurplusClearingPrice)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(prjByCondition.Compute("sum(CurrentCllectionPrice)", "1>0")).ToString("0.000");
			array[7] = System.Convert.ToDecimal(prjByCondition.Compute("sum(CllectionPrice)", "1>0")).ToString("0.000");
			array[8] = System.Convert.ToDecimal(prjByCondition.Compute("sum(CurrentPaymentMoney)", "1>0")).ToString("0.000");
			array[9] = System.Convert.ToDecimal(prjByCondition.Compute("sum(PaymentMoney)", "1>0")).ToString("0.000");
			array[10] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ManagementMargin)", "1>0")).ToString("0.000");
			array[11] = System.Convert.ToDecimal(prjByCondition.Compute("sum(MigrantQualityMarginRate)", "1>0")).ToString("0.000");
			array[12] = System.Convert.ToDecimal(prjByCondition.Compute("sum(PerformanceBond)", "1>0")).ToString("0.000");
			array[13] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ElseMargin)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
			array[4] = "0.000";
			array[5] = "0.000";
			array[6] = "0.000";
			array[7] = "0.000";
			array[8] = "0.000";
			array[9] = "0.000";
			array[10] = "0.000";
			array[11] = "0.000";
			array[12] = "0.000";
			array[13] = "0.000";
		}
		int[] value = new int[]
		{
			11,
			12,
			13,
			21,
			22,
			23,
			24,
			25,
			26,
			27,
			28,
			29,
			31,
			32
		};
		this.ViewState["Total"] = array;
		this.ViewState["index"] = value;
	}
	protected DataTable BindPrjType(DataTable dt, bool IsExport, bool isWord)
	{
		foreach (DataRow dataRow in dt.Rows)
		{
			if (isWord)
			{
				string text = dataRow["PrjName"].ToString();
				if (text != "")
				{
					dataRow["PrjName"] = "";
				}
				string text2 = dataRow["PrjCode"].ToString();
				if (text2 != "")
				{
					dataRow["PrjCode"] = "";
				}
			}
			if (dataRow["PrjGuid"].ToString().Trim() != "")
			{
				ProjectInfo byId = ProjectInfo.GetById(dataRow["PrjGuid"].ToString());
				if (byId != null && byId.EngineeringTypes != null)
				{
					System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
					foreach (EngineeringType current in byId.EngineeringTypes)
					{
						if (IsExport)
						{
							stringBuilder.Append(current.ToString()).Append(",");
						}
						else
						{
							stringBuilder.Append(current.ToString()).Append("<br />");
						}
					}
					if (IsExport)
					{
						if (stringBuilder.ToString().Trim() != "")
						{
							dataRow["PrjType"] = stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1);
						}
					}
					else
					{
						dataRow["PrjType"] = stringBuilder.ToString();
					}
				}
			}
		}
		return dt;
	}
	protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvProject.DataKeys[e.Row.RowIndex].Value.ToString();
			string text = this.gvProject.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["typeCode"] = text;
			if (text.Length == 5)
			{
				e.Row.Attributes["isMainContract"] = "True";
			}
			else
			{
				e.Row.Attributes["isMainContract"] = "False";
			}
		}
		if (e.Row.RowType == DataControlRowType.Header)
		{
			TableCellCollection cells = e.Row.Cells;
			cells.Clear();
			cells.Add(new TableHeaderCell());
			cells[0].RowSpan = 2;
			cells[0].Text = "序号";
			cells.Add(new TableHeaderCell());
			cells[1].RowSpan = 2;
			cells[1].Text = "工程名称";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 2;
			cells[2].Text = "工程编号";
			cells.Add(new TableHeaderCell());
			cells[3].RowSpan = 2;
			cells[3].Text = "工程类别";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 2;
			cells[4].Text = "所属子公司";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 2;
			cells[5].Text = "项目责任人";
			cells.Add(new TableHeaderCell());
			cells[6].RowSpan = 2;
			cells[6].Text = "项目现场负责人";
			cells.Add(new TableHeaderCell());
			cells[7].RowSpan = 2;
			cells[7].Text = "联系电话";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 2;
			cells[8].Text = "业主名称";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 2;
			cells[9].Text = "工程地点";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 2;
			cells[10].Text = "甲方资金来源";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 2;
			cells[11].Text = "合同造价";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 2;
			cells[12].Text = "目标成本价";
			cells.Add(new TableHeaderCell());
			cells[13].RowSpan = 2;
			cells[13].Text = "实际价";
			cells.Add(new TableHeaderCell());
			cells[14].RowSpan = 2;
			cells[14].Text = "合同签订日期";
			cells.Add(new TableHeaderCell());
			cells[15].RowSpan = 2;
			cells[15].Text = "合同开工日期";
			cells.Add(new TableHeaderCell());
			cells[16].RowSpan = 2;
			cells[16].Text = "合同竣工日期";
			cells.Add(new TableHeaderCell());
			cells[17].RowSpan = 2;
			cells[17].Text = "实际开工日期";
			cells.Add(new TableHeaderCell());
			cells[18].RowSpan = 2;
			cells[18].Text = "实际竣工验收日期";
			cells.Add(new TableHeaderCell());
			cells[19].RowSpan = 2;
			cells[19].Text = "合同养护期";
			cells.Add(new TableHeaderCell());
			cells[20].RowSpan = 2;
			cells[20].Text = "合同约定付款方式";
			cells.Add(new TableHeaderCell());
			cells[21].RowSpan = 2;
			cells[21].Text = "实际完成工程额";
			cells.Add(new TableHeaderCell());
			cells[22].RowSpan = 2;
			cells[22].Text = "按合同应收款";
			cells.Add(new TableHeaderCell());
			cells[23].RowSpan = 2;
			cells[23].Text = "按合同应收未收";
			cells.Add(new TableHeaderCell());
			cells[24].ColumnSpan = 2;
			cells[24].Text = "回款";
			cells.Add(new TableHeaderCell());
			cells[25].ColumnSpan = 2;
			cells[25].Text = "支出";
			cells.Add(new TableHeaderCell());
			cells[26].ColumnSpan = 5;
			cells[26].Text = "公司预留款</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[27].ColumnSpan = 1;
			cells[27].Text = "当期回款";
			cells.Add(new TableHeaderCell());
			cells[28].ColumnSpan = 1;
			cells[28].Text = "累计回款";
			cells.Add(new TableHeaderCell());
			cells[29].ColumnSpan = 1;
			cells[29].Text = "当期支出";
			cells.Add(new TableHeaderCell());
			cells[30].ColumnSpan = 1;
			cells[30].Text = "累计支出";
			cells.Add(new TableHeaderCell());
			cells[31].ColumnSpan = 1;
			cells[31].Text = "管理费";
			cells.Add(new TableHeaderCell());
			cells[32].ColumnSpan = 1;
			cells[32].Text = "民工质量保证金";
			cells.Add(new TableHeaderCell());
			cells[33].ColumnSpan = 1;
			cells[33].Text = "预扣税率";
			cells.Add(new TableHeaderCell());
			cells[34].ColumnSpan = 1;
			cells[34].Text = "履约保证金";
			cells.Add(new TableHeaderCell());
			cells[35].ColumnSpan = 1;
			cells[35].Text = "其它";
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.ComputeTotal();
		this.BindGv();
	}
	protected void btnExecl_Click(object sender, System.EventArgs e)
	{
		DataTable prjByCondition = ProjectInfo.GetPrjByCondition(this.txtName.Text.Trim(), this.txtCode.Text.Trim(), this.txtManager.Value.Trim(), this.txtPrjPlace.Value.Trim(), this.txtPrjDutyPerson.Value.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), base.UserCode, 0, 0);
		if (prjByCondition.Rows.Count > 0)
		{
			DataRow dataRow = prjByCondition.NewRow();
			dataRow["Num"] = "合计";
			dataRow["ContractPrice"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ContractPrice)", "1>0")).ToString("0.000");
			dataRow["BudTotal"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(BudTotal)", "1>0")).ToString("0.000");
			dataRow["ConsTotal"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ConsTotal)", "1>0")).ToString("0.000");
			dataRow["ConsResTotal"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ConsResTotal)", "1>0")).ToString("0.000");
			dataRow["ClearingPrice"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ClearingPrice)", "1>0")).ToString("0.000");
			dataRow["SurplusClearingPrice"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(SurplusClearingPrice)", "1>0")).ToString("0.000");
			dataRow["CurrentCllectionPrice"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(CurrentCllectionPrice)", "1>0")).ToString("0.000");
			dataRow["CllectionPrice"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(CllectionPrice)", "1>0")).ToString("0.000");
			dataRow["CurrentPaymentMoney"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(CurrentPaymentMoney)", "1>0")).ToString("0.000");
			dataRow["PaymentMoney"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(PaymentMoney)", "1>0")).ToString("0.000");
			dataRow["ManagementMargin"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ManagementMargin)", "1>0")).ToString("0.000");
			dataRow["MigrantQualityMarginRate"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(MigrantQualityMarginRate)", "1>0")).ToString("0.000");
			dataRow["PerformanceBond"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(PerformanceBond)", "1>0")).ToString("0.000");
			dataRow["ElseMargin"] = System.Convert.ToDecimal(prjByCondition.Compute("sum(ElseMargin)", "1>0")).ToString("0.000");
			prjByCondition.Rows.Add(dataRow);
		}
		DataTable dataTable = this.FormatTable(prjByCondition, false);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("回款", 1, 2, 24, 0));
		list.Add(ExcelHeader.Create("支出", 1, 2, 26, 0));
		list.Add(ExcelHeader.Create("公司预留款", 1, 5, 28, 0));
		new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal < 24)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(dataTable, "工程基础汇总表", "工程基础汇总表", "工程基础汇总表.xls", list, null, 0, base.Request.Browser.Browser);
	}
	protected DataTable FormatTable(DataTable dtProject, bool isWord)
	{
		dtProject = this.BindPrjType(dtProject, true, isWord);
		if (dtProject.Columns["Num"] != null)
		{
			dtProject.Columns["Num"].ColumnName = "序号";
		}
		if (dtProject.Columns["TypeCode"] != null)
		{
			dtProject.Columns.Remove(dtProject.Columns["TypeCode"]);
		}
		if (dtProject.Columns["PrjGuid"] != null)
		{
			dtProject.Columns.Remove(dtProject.Columns["PrjGuid"]);
		}
		if (dtProject.Columns["UserDefined_Date"] != null)
		{
			dtProject.Columns.Remove(dtProject.Columns["UserDefined_Date"]);
		}
		if (dtProject.Columns["PrjName"] != null)
		{
			dtProject.Columns["PrjName"].ColumnName = "工程名称";
		}
		if (dtProject.Columns["PrjCode"] != null)
		{
			dtProject.Columns["PrjCode"].ColumnName = "工程编号";
		}
		if (dtProject.Columns["PrjType"] != null)
		{
			dtProject.Columns["PrjType"].ColumnName = "工程类别";
		}
		if (dtProject.Columns["ProjPeopleDep"] != null)
		{
			dtProject.Columns["ProjPeopleDep"].ColumnName = "所属子公司";
		}
		if (dtProject.Columns["PrjDutyName"] != null)
		{
			dtProject.Columns["PrjDutyName"].ColumnName = "项目责任人";
		}
		if (dtProject.Columns["PrjMangerName"] != null)
		{
			dtProject.Columns["PrjMangerName"].ColumnName = "项目现场负责人";
		}
		if (dtProject.Columns["Telephone"] != null)
		{
			dtProject.Columns["Telephone"].ColumnName = "联系电话";
		}
		if (dtProject.Columns["CorpName"] != null)
		{
			dtProject.Columns["CorpName"].ColumnName = "业主电话";
		}
		if (dtProject.Columns["PrjPlace"] != null)
		{
			dtProject.Columns["PrjPlace"].ColumnName = "工程地点";
		}
		if (dtProject.Columns["PrjFundInfo"] != null)
		{
			dtProject.Columns["PrjFundInfo"].ColumnName = "甲方资金来源";
		}
		if (dtProject.Columns["ContractPrice"] != null)
		{
			dtProject.Columns["ContractPrice"].ColumnName = "合同造价";
		}
		if (dtProject.Columns["BudTotal"] != null)
		{
			dtProject.Columns["BudTotal"].ColumnName = "目标成本价";
		}
		if (dtProject.Columns["ConsTotal"] != null)
		{
			dtProject.Columns["ConsTotal"].ColumnName = "实际价";
		}
		if (dtProject.Columns["SignedTime"] != null)
		{
			dtProject.Columns["SignedTime"].ColumnName = "合同签订日期";
		}
		if (dtProject.Columns["StartDate"] != null)
		{
			dtProject.Columns["StartDate"].ColumnName = "合同开工日期";
		}
		if (dtProject.Columns["EndDate"] != null)
		{
			dtProject.Columns["EndDate"].ColumnName = "合同竣工日期";
		}
		if (dtProject.Columns["ActualRunDate"] != null)
		{
			dtProject.Columns["ActualRunDate"].ColumnName = "实际开工日期";
		}
		if (dtProject.Columns["CompletedDate"] != null)
		{
			dtProject.Columns["CompletedDate"].ColumnName = "实际竣工验收日期";
		}
		if (dtProject.Columns["QualityPeriod"] != null)
		{
			dtProject.Columns["QualityPeriod"].ColumnName = "合同养护期";
		}
		if (dtProject.Columns["PayMode"] != null)
		{
			dtProject.Columns["PayMode"].ColumnName = "合同约定付款方式";
		}
		if (dtProject.Columns["ConsResTotal"] != null)
		{
			dtProject.Columns["ConsResTotal"].ColumnName = "实际完成工程额";
		}
		if (dtProject.Columns["ClearingPrice"] != null)
		{
			dtProject.Columns["ClearingPrice"].ColumnName = "按合同应收款";
		}
		if (dtProject.Columns["SurplusClearingPrice"] != null)
		{
			dtProject.Columns["SurplusClearingPrice"].ColumnName = "按合同应收未收";
		}
		if (dtProject.Columns["CurrentCllectionPrice"] != null)
		{
			dtProject.Columns["CurrentCllectionPrice"].ColumnName = "当期回款";
		}
		if (dtProject.Columns["CllectionPrice"] != null)
		{
			dtProject.Columns["CllectionPrice"].ColumnName = "累计回款";
		}
		if (dtProject.Columns["CurrentPaymentMoney"] != null)
		{
			dtProject.Columns["CurrentPaymentMoney"].ColumnName = "当期支出";
		}
		if (dtProject.Columns["PaymentMoney"] != null)
		{
			dtProject.Columns["PaymentMoney"].ColumnName = "累计支出";
		}
		if (dtProject.Columns["ManagementMargin"] != null)
		{
			dtProject.Columns["ManagementMargin"].ColumnName = "管理费";
		}
		if (dtProject.Columns["MigrantQualityMarginRate"] != null)
		{
			dtProject.Columns["MigrantQualityMarginRate"].ColumnName = "民工质量保证金";
		}
		if (dtProject.Columns["WithholdingTaxRate"] != null)
		{
			dtProject.Columns["WithholdingTaxRate"].ColumnName = "预扣税";
		}
		if (dtProject.Columns["PerformanceBond"] != null)
		{
			dtProject.Columns["PerformanceBond"].ColumnName = "履约保证金";
		}
		if (dtProject.Columns["ElseMargin"] != null)
		{
			dtProject.Columns["ElseMargin"].ColumnName = "其他";
		}
		dtProject.AcceptChanges();
		return dtProject;
	}
	protected void DataTableToWord(DataTable dt, string FileName, string titlename)
	{
		string str;
		if (FileName == null || FileName == "")
		{
			str = "DFSOFT";
		}
		else
		{
			str = HttpUtility.UrlPathEncode(FileName);
		}
		DataRow[] array = dt.Select();
		int count = dt.Columns.Count;
		HttpResponse response = HttpContext.Current.Response;
		response.Clear();
		response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
		response.AppendHeader("Content-disposition", "attachment;filename=" + str + ".doc");
		response.ContentType = "application/vnd.ms-word";
		string text = "<table border='0' cellpadding='0' cellspacing='0' style='border-right:#000000 0.1pt solid;border-top:#000000 0.1pt solid;'>";
		string text2 = "</table>";
		string text3 = null;
		string text4 = null;
		string text5 = "<tr><td style='font-size:13px;' align='center'><b>" + titlename + "</b></td></tr>";
		string text6 = "<tr>";
		string text7 = "</tr>";
		for (int i = 0; i < count; i++)
		{
			text3 = text3 + "<td style='border-left:#000000 0.1pt solid; border-bottom:#000000 1.0pt solid; font-size:15px;' align='center'><b>" + dt.Columns[i].Caption.ToString() + "</b></td>";
		}
		text3 = text6.ToString() + text3.ToString() + text7.ToString();
		DataRow[] array2 = array;
		for (int j = 0; j < array2.Length; j++)
		{
			DataRow dataRow = array2[j];
			string text8 = null;
			for (int i = 0; i < count; i++)
			{
				text8 = text8 + "<td style='border-left:#000000 0.1pt solid; border-bottom:#000000 1.0pt solid; font-size:15px;' align='center'>" + dataRow[i].ToString() + "</td>";
			}
			text4 = text4 + text6.ToString() + text8.ToString() + text7.ToString();
		}
		text3 = string.Concat(new string[]
		{
			"<center><table>",
			text5.ToString(),
			"<tr>",
			text.ToString(),
			text3.ToString(),
			text4.ToString(),
			text2.ToString(),
			"</tr></table></center>"
		});
		response.Write(text3.ToString());
		response.End();
	}
}


