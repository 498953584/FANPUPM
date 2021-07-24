using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_checkBudget : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private System.Collections.Generic.IList<BudTask> BudTaskList = new System.Collections.Generic.List<BudTask>();
	private BudTaskService budTaskSer = new BudTaskService();
	private BudModifyService modifySer = new BudModifyService();
	private BudModifyTaskService budModifyTaskSer = new BudModifyTaskService();
	private string prjId = string.Empty;
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.hfldPrjId.Value = this.prjId;
			this.BindGv();
		}
	}
	public void BindGv()
	{
		string text = this.dropTaskType.SelectedValue;
		string value = this.dropYear.SelectedValue;
		string value2 = this.dropMonth.SelectedValue;
		if (string.IsNullOrEmpty(text))
		{
			value = string.Empty;
			value2 = string.Empty;
		}
		else
		{
			if (text == "Y")
			{
				value2 = string.Empty;
			}
		}
		if (string.IsNullOrEmpty(value))
		{
			text = string.Empty;
		}
		if (text == "M" && string.IsNullOrEmpty(value2))
		{
			text = string.Empty;
			value = string.Empty;
		}
		DataTable table = this.budTaskSer.GetTable(this.prjId);
		this.gvBudget.DataSource = table;
		this.gvBudget.DataBind();
		if (table.Rows.Count <= 0)
		{
			this.btnRes.Enabled = false;
			return;
		}
		this.btnRes.Enabled = true;
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			string text2 = this.gvBudget.DataKeys[e.Row.RowIndex]["SubCount"].ToString();
			string text3 = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			int num = text3.Length / 3;
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["orderNumber"] = text3;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = text2;
			string value = this.gvBudget.DataKeys[e.Row.RowIndex]["Modified"].ToString();
			if (!string.IsNullOrEmpty(value))
			{
				e.Row.Style.Add("color", "red");
				HyperLink hyperLink = e.Row.FindControl("hlinkShowNote") as HyperLink;
				if (hyperLink != null)
				{
					hyperLink.Style.Add("color", "red");
				}
			}
			bool flag = text2 == "0";
			if (flag)
			{
				Image image = new Image();
				image.ImageUrl = "../../images/tree/more.gif";
				image.ToolTip = "详细信息";
				image.Attributes.Add("rowId", text);
				image.Attributes["onclick"] = "showInfo('" + text + "')";
				image.Style.Add("vertical-align", "middle");
				image.Style.Add("float", "right");
				image.Style.Add("cursor", "pointer");
				e.Row.Cells[1].Controls.Add(image);
			}
		}
	}
	protected void ddlVersion_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		ExcelExporter excelExporter = new ExcelExporter();
		System.Collections.Generic.List<DataTable> data = this.GetData();
		HttpContext current = HttpContext.Current;
		excelExporter.ExportAll(data, current, "目标成本预算.xls");
	}
	private System.Collections.Generic.List<DataTable> GetData()
	{
		System.Collections.Generic.List<DataTable> list = new System.Collections.Generic.List<DataTable>();
		string text = this.dropTaskType.SelectedValue;
		string value = this.dropYear.SelectedValue;
		string value2 = this.dropMonth.SelectedValue;
		if (string.IsNullOrEmpty(text))
		{
			value = string.Empty;
			value2 = string.Empty;
		}
		else
		{
			if (text == "Y")
			{
				value2 = string.Empty;
			}
		}
		if (string.IsNullOrEmpty(value))
		{
			text = string.Empty;
		}
		if (text == "M" && string.IsNullOrEmpty(value2))
		{
			text = string.Empty;
			value = string.Empty;
		}
		DataTable allTable = this.budTaskSer.GetAllTable(this.prjId);
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("名称");
		dataTable.Columns.Add("编码");
		dataTable.Columns.Add("类型");
		dataTable.Columns.Add("单位");
		dataTable.Columns.Add("工程量");
		if (this.hfldIsWBSRelevance.Value == "1")
		{
			dataTable.Columns.Add("开始时间");
			dataTable.Columns.Add("结束时间");
		}
		dataTable.Columns.Add("综合单价");
		dataTable.Columns.Add("小计");
		dataTable.Columns.Add("备注");
		DataTable dataTable2 = new DataTable();
		dataTable2.Columns.Add("资源编号");
		dataTable2.Columns.Add("资源名称");
		dataTable2.Columns.Add("单位");
		dataTable2.Columns.Add("规格");
		dataTable2.Columns.Add("品牌");
		dataTable2.Columns.Add("型号");
		dataTable2.Columns.Add("技术参数");
		dataTable2.Columns.Add("单价");
		dataTable2.Columns.Add("数量");
		dataTable2.Columns.Add("损耗系数");
		dataTable2.Columns.Add("合计金额");
		dataTable2.Columns.Add("序号");
		for (int i = 0; i < allTable.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["序号"] = i + 1;
			dataRow["名称"] = allTable.Rows[i]["TaskName"].ToString();
			dataRow["编码"] = allTable.Rows[i]["TaskCode"].ToString();
			dataRow["类型"] = allTable.Rows[i]["TypeName"].ToString();
			dataRow["单位"] = allTable.Rows[i]["Unit"].ToString();
			dataRow["工程量"] = allTable.Rows[i]["Quantity"].ToString();
			if (this.hfldIsWBSRelevance.Value == "1")
			{
				dataRow["开始时间"] = allTable.Rows[i]["StartDate"].ToString();
				dataRow["结束时间"] = allTable.Rows[i]["EndDate"].ToString();
			}
			dataRow["综合单价"] = allTable.Rows[i]["UnitPrice"].ToString();
			dataRow["小计"] = allTable.Rows[i]["Total2"].ToString();
			dataRow["备注"] = allTable.Rows[i]["Note"].ToString();
			dataTable.Rows.Add(dataRow);
			if (allTable.Rows[i]["SubCount"].ToString() == "0")
			{
				string resourcesInfoByTaskId = BudTask.GetResourcesInfoByTaskId(allTable.Rows[i]["TaskId"].ToString());
				if (resourcesInfoByTaskId != string.Empty)
				{
					string[] array = new string[0];
					if (resourcesInfoByTaskId.Contains("⊙"))
					{
						array = resourcesInfoByTaskId.Split(new char[]
						{
							'⊙'
						});
					}
					string[] array2 = array;
					for (int j = 0; j < array2.Length; j++)
					{
						string text2 = array2[j];
						if (text2 != string.Empty)
						{
							string[] array3 = text2.Split(new char[]
							{
								','
							});
							DataRow dataRow2 = dataTable2.NewRow();
							for (int k = 0; k < array3.Length; k++)
							{
								dataRow2[k] = array3[k];
							}
							dataRow2["序号"] = i + 1;
							dataTable2.Rows.Add(dataRow2);
						}
					}
				}
			}
		}
		list.Add(dataTable);
		list.Add(dataTable2);
		return list;
	}
	protected void dropTaskType_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.dropTaskType.SelectedValue))
		{
			this.BindYear();
			this.BindMonth();
		}
		else
		{
			this.dropYear.SelectedValue = string.Empty;
			this.dropMonth.SelectedValue = string.Empty;
		}
		this.BindGv();
	}
	protected void dropYear_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindMonth();
		this.BindGv();
	}
	protected void dropMonth_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	private void BindYear()
	{
		this.dropYear.Items.Clear();
		this.dropYear.Items.Add(new ListItem("选择年份", ""));
		BudTaskService budTaskService = new BudTaskService();
		System.Collections.Generic.IList<int> years = budTaskService.GetYears(this.prjId);
		if (years != null)
		{
			foreach (int current in years)
			{
				this.dropYear.Items.Add(new ListItem(current.ToString() + "年", current.ToString("0000")));
			}
		}
	}
	private void BindMonth()
	{
		this.dropMonth.Items.Clear();
		this.dropMonth.Items.Add(new ListItem("选择月份", ""));
		BudTaskService budTaskService = new BudTaskService();
		if (!string.IsNullOrEmpty(this.dropYear.SelectedValue))
		{
			System.Collections.Generic.IList<int> months = budTaskService.GetMonths(this.prjId, this.dropYear.SelectedValue);
			if (months != null)
			{
				foreach (int current in months)
				{
					this.dropMonth.Items.Add(new ListItem(current.ToString() + "月", current.ToString("00")));
				}
			}
		}
	}
}


