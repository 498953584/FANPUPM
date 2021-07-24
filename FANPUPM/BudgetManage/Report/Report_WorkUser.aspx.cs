using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_Report_WorkUser : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.gvBudget.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.BindDrop();
			this.BindTree();
		}
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable reportData = this.GetReportData();
		FileExport fileExport = new FileExport(new ExcelExporter
		{
			PercentColumns = new int[]
			{
				8
			}
		}, reportData, "分项工程消耗对比报表.xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	private void BindGv()
	{
		this.ViewState["Task"] = EReport.GetTaskReport(this.tvBudget.SelectedValue, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
		DataTable dataTable = this.ViewState["Task"] as DataTable;
		if (dataTable != null)
		{
			this.AspNetPager1.RecordCount = dataTable.Rows.Count;
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.gvBudget.DataSource = EReport.GetPageDataTable(dataTable, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
			this.gvBudget.DataKeyNames = new string[]
			{
				"TaskId"
			};
			this.gvBudget.DataBind();
			if (dataTable.Rows.Count != 0)
			{
				string[] value = new string[]
				{
					dataTable.Compute("SUM(TotalBudget)", string.Empty).ToString(),
					dataTable.Compute("SUM(TotalAcc)", string.Empty).ToString(),
					dataTable.Compute("SUM(JieChao)", string.Empty).ToString()
				};
				int[] index = new int[]
				{
					6,
					7,
					8
				};
				GridViewUtility.AddTotalRow(this.gvBudget, value, index);
				this.ViewState["Total"] = value;
				return;
			}
		}
		else
		{
			this.gvBudget.DataSource = dataTable;
			this.gvBudget.DataBind();
		}
	}
	public void BindDrop()
	{
		this.ddlYear.DataSource = this.prjInfo.GetYears(base.UserCode);
		this.ddlYear.DataTextField = "Text";
		this.ddlYear.DataValueField = "Value";
		this.ddlYear.DataBind();
		this.ddlYear.SelectedIndex = this.ddlYear.Items.Count - 1;
		if (base.Request["year"] != null)
		{
			this.ddlYear.SelectedValue = base.Request["year"];
		}
	}
	protected void BindTree()
	{
		this.tvBudget.Nodes.Clear();
		TreeNode treeNode = new TreeNode("项目", "0");
		treeNode.SelectAction = TreeNodeSelectAction.None;
		this.tvBudget.Nodes.AddAt(0, treeNode);
		System.Collections.Generic.List<PT_PrjInfo> project = this.prjInfo.GetProject(System.Convert.ToInt32(this.ddlYear.SelectedValue), base.UserCode);
		for (int i = 0; i < project.Count; i++)
		{
			TreeNode treeNode2 = new TreeNode(project[i].PrjName, project[i].PrjGuid.Value.ToString());
			if (i == 0)
			{
				treeNode2.Select();
			}
			if (base.Request["prjId"] != null && base.Request["prjId"] == treeNode2.Value)
			{
				treeNode2.Select();
			}
			treeNode.ChildNodes.Add(treeNode2);
		}
		this.tvBudget.ExpandAll();
	}
	private DataTable GetReportData()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("分项工程编号");
		dataTable.Columns.Add("分项工程名称");
		dataTable.Columns.Add("材料名称");
		dataTable.Columns.Add("规格");
		dataTable.Columns.Add("单位");
		dataTable.Columns.Add("预算量");
		dataTable.Columns.Add("实际量");
		dataTable.Columns.Add("节超");
		if (this.ViewState["Task"] != null)
		{
			int num = 0;
			foreach (DataRow dataRow in (this.ViewState["Task"] as DataTable).Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				num++;
				dataRow2["序号"] = num.ToString();
				dataRow2["分项工程编号"] = dataRow["TaskCode"];
				dataRow2["分项工程名称"] = dataRow["TaskName"];
				dataRow2["材料名称"] = dataRow["ResName"];
				dataRow2["规格"] = dataRow["Specification"];
				dataRow2["单位"] = dataRow["UnitName"];
				dataRow2["预算量"] = dataRow["TotalBudget"];
				dataRow2["实际量"] = dataRow["TotalAcc"];
				dataRow2["节超"] = dataRow["JieChao"];
				dataTable.Rows.Add(dataRow2);
			}
			string[] array = new string[3];
			array = (this.ViewState["Total"] as string[]);
			DataRow dataRow3 = dataTable.NewRow();
			dataRow3["序号"] = "合计";
			dataRow3["分项工程编号"] = "";
			dataRow3["分项工程名称"] = "";
			dataRow3["材料名称"] = "";
			dataRow3["规格"] = "";
			dataRow3["单位"] = "";
			dataRow3["预算量"] = array[0];
			dataRow3["实际量"] = array[1];
			dataRow3["节超"] = array[2];
			dataTable.Rows.Add(dataRow3);
		}
		return dataTable;
	}
	protected void gvBudget_DataBound(object sender, System.EventArgs e)
	{
		int index = 0;
		int num = 0;
		for (int i = 1; i < this.gvBudget.Rows.Count; i++)
		{
			if (this.gvBudget.Rows[i].Cells[1].Text == this.gvBudget.Rows[i - 1].Cells[1].Text && this.gvBudget.DataKeys[i].Value.ToString() == this.gvBudget.DataKeys[i - 1].Value.ToString())
			{
				if (this.gvBudget.Rows[index].Cells[1].RowSpan == 0)
				{
					this.gvBudget.Rows[index].Cells[1].RowSpan++;
				}
				this.gvBudget.Rows[index].Cells[1].RowSpan++;
				this.gvBudget.Rows[i].Cells[1].Visible = false;
				if (this.gvBudget.Rows[index].Cells[2].RowSpan == 0)
				{
					this.gvBudget.Rows[index].Cells[2].RowSpan++;
				}
				this.gvBudget.Rows[index].Cells[2].RowSpan++;
				this.gvBudget.Rows[i].Cells[2].Visible = false;
				if (this.gvBudget.Rows[index].Cells[0].RowSpan == 0)
				{
					this.gvBudget.Rows[index].Cells[0].RowSpan++;
				}
				this.gvBudget.Rows[index].Cells[0].RowSpan++;
				this.gvBudget.Rows[i].Cells[0].Visible = false;
			}
			else
			{
				this.gvBudget.Rows[index].Cells[0].Text = (num + 1).ToString();
				index = i;
				num++;
			}
			if (i == this.gvBudget.Rows.Count - 1)
			{
				this.gvBudget.Rows[index].Cells[0].Text = (num + 1).ToString();
			}
		}
	}
	protected void ddlYear_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindTree();
		this.BindGv();
	}
	protected void tvBudget_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.txtCode.Text = string.Empty;
		this.txtName.Text = string.Empty;
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
	}
}


