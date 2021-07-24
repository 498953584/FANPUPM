using ASP;
using cn.justwin.BLL;
using cn.justwin.VehiclesBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_Reimbursement_ReimbursementManage : NBasePage, IRequiresSessionState
{
	private decimal Tolls = 0.00m;
	private decimal Repairs = 0.00m;
	private decimal FuelCosts = 0.00m;
	private decimal MaintenanceCosts = 0.00m;
	private ReimbursementBLL reiBll = new ReimbursementBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.GvDataBind();
		}
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.GvDataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		foreach (GridViewRow gridViewRow in this.GvReimbursement.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
			if (checkBox.Checked && !this.reiBll.Delete(new Guid(checkBox.ToolTip)))
			{
				base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
				break;
			}
		}
		this.GvDataBind();
	}
	private void GvDataBind()
	{
		string text = " 1=1 ";
		if (this.txtStartTime.Text != "" && this.txtEndTime.Text == "")
		{
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				" and a.Date >= '",
				Convert.ToDateTime(this.txtStartTime.Text.ToString()),
				"' "
			});
		}
		if (this.txtEndTime.Text != "" && this.txtStartTime.Text == "")
		{
			object obj2 = text;
			text = string.Concat(new object[]
			{
				obj2,
				" and a.Date <= '",
				Convert.ToDateTime(this.txtEndTime.Text.ToString()),
				"' "
			});
		}
		if (this.txtStartTime.Text != "" && this.txtEndTime.Text != "")
		{
			object obj3 = text;
			text = string.Concat(new object[]
			{
				obj3,
				" and a.Date between '",
				Convert.ToDateTime(this.txtStartTime.Text.ToString()),
				"' and '",
				Convert.ToDateTime(this.txtEndTime.Text.ToString()),
				"' "
			});
		}
		if (this.txtVehiclesCode.Text.Trim() != "")
		{
			text = text + " and b.VehicleCode like'%" + this.txtVehiclesCode.Text.Trim() + "%'";
		}
		if (this.txtPeople.Text.Trim() != "")
		{
			text = text + " and a.UserName like '%" + this.txtPeople.Text.Trim() + "%' ";
		}
		ReimbursementBLL reimbursementBLL = new ReimbursementBLL();
		DataTable allList = reimbursementBLL.getAllList(text);
		if (allList != null)
		{
			for (int i = 0; i < allList.Rows.Count; i++)
			{
				if (!string.IsNullOrEmpty(allList.Rows[i]["Tolls"].ToString()))
				{
					this.Tolls += Convert.ToDecimal(allList.Rows[i]["Tolls"].ToString());
				}
				if (!string.IsNullOrEmpty(allList.Rows[i]["Repairs"].ToString()))
				{
					this.Repairs += Convert.ToDecimal(allList.Rows[i]["Repairs"].ToString());
				}
				if (!string.IsNullOrEmpty(allList.Rows[i]["FuelCosts"].ToString()))
				{
					this.FuelCosts += Convert.ToDecimal(allList.Rows[i]["FuelCosts"].ToString());
				}
				if (!string.IsNullOrEmpty(allList.Rows[i]["MaintenanceCosts"].ToString()))
				{
					this.MaintenanceCosts += Convert.ToDecimal(allList.Rows[i]["MaintenanceCosts"].ToString());
				}
			}
		}
		this.GvReimbursement.DataSource = allList;
		this.GvReimbursement.DataBind();
	}
	protected void GvReimbursement_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.GvReimbursement.DataKeys[e.Row.RowIndex].Value.ToString();
			if (e.Row.Cells[11].Text.Length > 30)
			{
				e.Row.Cells[11].ToolTip = e.Row.Cells[11].Text;
				e.Row.Cells[11].Text = e.Row.Cells[11].Text.Substring(0, 30) + "...";
			}
			e.Row.Cells[10].Text = (Convert.ToDecimal(e.Row.Cells[9].Text) + Convert.ToDecimal(e.Row.Cells[8].Text) + Convert.ToDecimal(e.Row.Cells[7].Text) + Convert.ToDecimal(e.Row.Cells[6].Text)).ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[6].Text = this.Tolls.ToString();
			e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
			e.Row.Cells[7].Text = this.Repairs.ToString();
			e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
			e.Row.Cells[8].Text = this.FuelCosts.ToString();
			e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
			e.Row.Cells[9].Text = this.MaintenanceCosts.ToString();
			e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
			e.Row.Cells[10].Text = (this.MaintenanceCosts + this.Tolls + this.Repairs + this.FuelCosts).ToString();
			e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
		}
	}
	protected void GvReimbursement_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GvReimbursement.PageIndex = e.NewPageIndex;
		this.GvDataBind();
	}
}


