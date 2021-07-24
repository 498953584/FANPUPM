using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractForm_contForm1 : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.bindGV();
		}
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			TableCellCollection cells = e.Row.Cells;
			cells.Clear();
			cells.Add(new TableHeaderCell());
			cells[0].CssClass = "header";
			cells[0].RowSpan = 2;
			cells[0].Text = "序号";
			cells.Add(new TableHeaderCell());
			cells[1].CssClass = "header";
			cells[1].RowSpan = 2;
			cells[1].Text = "项目名称";
			cells.Add(new TableHeaderCell());
			cells[2].CssClass = "header";
			cells[2].ColumnSpan = 7;
			cells[2].Text = "收入合同";
			cells.Add(new TableHeaderCell());
			cells[3].CssClass = "header";
			cells[3].ColumnSpan = 7;
			cells[3].Text = "支出合同";
			cells.Add(new TableHeaderCell());
			cells[4].CssClass = "header";
			cells[4].RowSpan = 2;
			cells[4].Text = "原合同差额";
			cells.Add(new TableHeaderCell());
			cells[5].CssClass = "header";
			cells[5].RowSpan = 2;
			cells[5].Text = "变更后合同差额";
			cells.Add(new TableHeaderCell());
			cells[6].CssClass = "header";
			cells[6].RowSpan = 2;
			cells[6].Text = "结算差额";
			cells.Add(new TableHeaderCell());
			cells[7].CssClass = "header";
			cells[7].RowSpan = 2;
			cells[7].Text = "支付差额</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 1;
			cells[8].Text = "合同编号";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 1;
			cells[9].Text = "合同名称";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 1;
			cells[10].Text = "原始金额";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 1;
			cells[11].Text = "变更后金额";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 1;
			cells[12].Text = "开累结算";
			cells.Add(new TableHeaderCell());
			cells[13].RowSpan = 1;
			cells[13].Text = "开累回款";
			cells.Add(new TableHeaderCell());
			cells[14].RowSpan = 1;
			cells[14].Text = "挂靠项目开累付款";
			cells.Add(new TableHeaderCell());
			cells[15].RowSpan = 1;
			cells[15].Text = "合同编号";
			cells.Add(new TableHeaderCell());
			cells[16].RowSpan = 1;
			cells[16].Text = "合同名称";
			cells.Add(new TableHeaderCell());
			cells[17].RowSpan = 1;
			cells[17].Text = "合同类型";
			cells.Add(new TableHeaderCell());
			cells[18].RowSpan = 1;
			cells[18].Text = "原始金额";
			cells.Add(new TableHeaderCell());
			cells[19].RowSpan = 1;
			cells[19].Text = "变更后金额";
			cells.Add(new TableHeaderCell());
			cells[20].RowSpan = 1;
			cells[20].Text = "开累结算";
			cells.Add(new TableHeaderCell());
			cells[21].RowSpan = 1;
			cells[21].Text = "开累支付";
		}
	}
	private void bindGV()
	{
		DataTable dataTable = formBLL.ContractReport(this.txtProject.Value, this.txtStartDate.Text.Trim());
		if (dataTable.Rows.Count > 1)
		{
			this.gvwContract.DataSource = dataTable;
		}
		else
		{
			this.gvwContract.DataSource = null;
		}
		this.gvwContract.DataBind();
	}
	private void colspanTable(int cellindex, int colspan, string value)
	{
		for (int i = 0; i < this.gvwContract.Rows.Count; i++)
		{
			if (this.gvwContract.Rows[i].Cells[cellindex].Text == value)
			{
				this.gvwContract.Rows[i].Cells[cellindex].ColumnSpan = colspan;
				this.gvwContract.Rows[i].Cells[cellindex].HorizontalAlign = HorizontalAlign.Center;
				for (int j = cellindex + 1; j < cellindex + colspan; j++)
				{
					this.gvwContract.Rows[i].Cells[j].Visible = false;
				}
			}
		}
	}
	private void rowspanTable(int cellindex, int colspan)
	{
		int num = 0;
		for (int i = num + 1; i < this.gvwContract.Rows.Count; i++)
		{
			if (this.gvwContract.Rows[i].Cells[cellindex].Text == this.gvwContract.Rows[i - 1].Cells[cellindex].Text)
			{
				if (colspan != 0)
				{
					for (int j = cellindex; j < cellindex + colspan; j++)
					{
						if (this.gvwContract.Rows[num].Cells[j].RowSpan == 0)
						{
							this.gvwContract.Rows[num].Cells[j].RowSpan++;
						}
						this.gvwContract.Rows[num].Cells[j].RowSpan++;
						this.gvwContract.Rows[i].Cells[j].Visible = false;
					}
				}
				else
				{
					if (this.gvwContract.Rows[num].Cells[cellindex].RowSpan == 0)
					{
						this.gvwContract.Rows[num].Cells[cellindex].RowSpan++;
					}
					this.gvwContract.Rows[num].Cells[cellindex].RowSpan++;
					this.gvwContract.Rows[i].Cells[cellindex].Visible = false;
				}
			}
			else
			{
				num = i;
			}
		}
	}
	protected void gvwContract_DataBound(object sender, System.EventArgs e)
	{
		int index = 0;
		int num = 0;
		for (int i = 1; i < this.gvwContract.Rows.Count; i++)
		{
			string a = ((Label)this.gvwContract.Rows[i].FindControl("lblPrjName")).Text.Trim();
			string b = ((Label)this.gvwContract.Rows[i - 1].FindControl("lblPrjName")).Text.Trim();
			if (a == b)
			{
				if (this.gvwContract.Rows[index].Cells[1].RowSpan == 0)
				{
					this.gvwContract.Rows[index].Cells[1].RowSpan++;
				}
				this.gvwContract.Rows[index].Cells[1].RowSpan++;
				this.gvwContract.Rows[i].Cells[1].Visible = false;
				if (this.gvwContract.Rows[index].Cells[0].RowSpan == 0)
				{
					this.gvwContract.Rows[index].Cells[0].RowSpan++;
				}
				this.gvwContract.Rows[index].Cells[0].RowSpan++;
				this.gvwContract.Rows[i].Cells[0].Visible = false;
				if (this.gvwContract.Rows[index].Cells[16].RowSpan == 0)
				{
					this.gvwContract.Rows[index].Cells[16].RowSpan++;
				}
				this.gvwContract.Rows[index].Cells[16].RowSpan++;
				this.gvwContract.Rows[i].Cells[16].Visible = false;
				if (this.gvwContract.Rows[index].Cells[17].RowSpan == 0)
				{
					this.gvwContract.Rows[index].Cells[17].RowSpan++;
				}
				this.gvwContract.Rows[index].Cells[17].RowSpan++;
				this.gvwContract.Rows[i].Cells[17].Visible = false;
				if (this.gvwContract.Rows[index].Cells[18].RowSpan == 0)
				{
					this.gvwContract.Rows[index].Cells[18].RowSpan++;
				}
				this.gvwContract.Rows[index].Cells[18].RowSpan++;
				this.gvwContract.Rows[i].Cells[18].Visible = false;
				if (this.gvwContract.Rows[index].Cells[19].RowSpan == 0)
				{
					this.gvwContract.Rows[index].Cells[19].RowSpan++;
				}
				this.gvwContract.Rows[index].Cells[19].RowSpan++;
				this.gvwContract.Rows[i].Cells[19].Visible = false;
				if (this.gvwContract.Rows[i].Cells[2].Text == "小计")
				{
					this.gvwContract.Rows[i].Cells[2].ColumnSpan = 2;
					this.gvwContract.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Center;
					this.gvwContract.Rows[i].Cells[3].Visible = false;
					this.gvwContract.Rows[i].Cells[9].ColumnSpan = 3;
					this.gvwContract.Rows[i].Cells[9].HorizontalAlign = HorizontalAlign.Center;
					this.gvwContract.Rows[i].Cells[10].Visible = false;
					this.gvwContract.Rows[i].Cells[11].Visible = false;
					this.gvwContract.Rows[index].Cells[18].Text = this.gvwContract.Rows[i].Cells[18].Text;
					this.gvwContract.Rows[index].Cells[17].Text = this.gvwContract.Rows[i].Cells[17].Text;
					this.gvwContract.Rows[index].Cells[19].Text = this.gvwContract.Rows[i].Cells[19].Text;
					this.gvwContract.Rows[index].Cells[16].Text = this.gvwContract.Rows[i].Cells[16].Text;
				}
			}
			else
			{
				this.gvwContract.Rows[index].Cells[0].Text = (num + 1).ToString();
				index = i;
				num++;
			}
			if (i == this.gvwContract.Rows.Count - 1 && this.gvwContract.Rows[i].Cells[1].Text == "合计")
			{
				this.gvwContract.Rows[i].Cells[0].ColumnSpan = 4;
				this.gvwContract.Rows[i].Cells[0].Text = "合计";
				this.gvwContract.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Center;
				this.gvwContract.Rows[i].Cells[1].Visible = false;
				this.gvwContract.Rows[i].Cells[2].Visible = false;
				this.gvwContract.Rows[i].Cells[3].Visible = false;
				this.gvwContract.Rows[i].Cells[9].ColumnSpan = 3;
				this.gvwContract.Rows[i].Cells[9].Text = "合计";
				this.gvwContract.Rows[i].Cells[9].HorizontalAlign = HorizontalAlign.Center;
				this.gvwContract.Rows[i].Cells[10].Visible = false;
				this.gvwContract.Rows[i].Cells[11].Visible = false;
			}
		}
		this.rowspanTable(2, 7);
		this.colspanTable(2, 7, "&nbsp;");
		this.rowspanTable(9, 7);
		this.colspanTable(9, 7, "&nbsp;");
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.bindGV();
	}
}


