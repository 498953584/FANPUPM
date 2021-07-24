using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using cn.justwin.Warn;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Allocation_AuditPage : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["ic"] == null)
			{
				base.RegisterScript("alert('系统提示：\\n\\n参数错误！');");
			}
			else
			{
				this.ShowAllocationData();
			}
			if (base.Request.QueryString["acode"] != null && base.Request.QueryString["allocationType"] != null)
			{
				Warning.UpdateValid(base.Request.QueryString["acode"].ToString(), base.Request.QueryString["allocationType"].ToString());
			}
		}
	}
	protected void ShowAllocationData()
	{
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		AllocationModel allocationModel = new AllocationModel();
		allocationModel = allocationBllAction.ReturnAllocatonModel("aid='" + base.Request.QueryString["ic"] + "' ");
		this.lblAllocationNo.Text = allocationModel.Acode;
		this.FileLink1.FID = allocationModel.Acode;
		this.txtOutDepository.Text = allocationBllAction.GetTreasuryNameByCode(allocationModel.TCodea).Rows[0][0].ToString();
		this.txtInDepository.Text = allocationBllAction.GetTreasuryNameByCode(allocationModel.TCodeb).Rows[0][0].ToString();
		this.txtInDate.Text = allocationModel.InTime;
		this.txtOutAllocationPerson.Text = allocationBllAction.GetUserNameByCode(allocationModel.OutAllocationPerson);
		this.txtInAllocationPerson.Text = allocationBllAction.GetUserNameByCode(allocationModel.InAllocationPerson);
		this.txtRemark.Text = allocationModel.Explain;
		this.HdnTCodea.Value = allocationModel.TCodea;
		this.HdnAcode.Value = allocationModel.Acode;
		DataTable allocationStockList = allocationBllAction.GetAllocationStockList(allocationModel.TCodea, "acode='" + allocationModel.Acode + "'");
		this.GVMaterialList.DataSource = allocationStockList;
		this.GVMaterialList.DataBind();
		if (allocationStockList.Rows.Count > 0)
		{
			string total = Convert.ToDecimal(allocationStockList.Compute("SUM(Total)", string.Empty)).ToString("0.000");
			GridViewUtility.AddTotalRow(this.GVMaterialList, total, 11);
		}
		this.lblBllProducer.Text = PageHelper.QueryUser(this, base.UserCode);
		this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
	}
	protected void GVMaterialList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		this.GVMaterialList.PageIndex = e.NewPageIndex;
		this.GVMaterialList.DataSource = allocationBllAction.GetAllocationStockList(this.HdnTCodea.Value, "acode='" + this.HdnAcode.Value + "'");
		this.GVMaterialList.DataBind();
	}
	protected void GVMaterialList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			ResourceLogicEdit resourceLogicEdit = new ResourceLogicEdit();
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			Label label = (Label)e.Row.Cells[1].FindControl("lblNo");
			label.Text = (this.GVMaterialList.PageIndex * this.GVMaterialList.PageSize + (e.Row.RowIndex + 1)).ToString();
			((Label)e.Row.Cells[5].FindControl("lblUnit")).Text = resourceLogicEdit.GetUnitNameList(resourceLogicEdit.GetUnitId(dataRowView["resourcecode"].ToString()));
			((Label)e.Row.Cells[8].FindControl("txtAllocationOutNum")).Text = dataRowView["number"].ToString();
			((Label)e.Row.Cells[8].FindControl("txtTotal")).Text = Convert.ToDecimal(dataRowView["Total"]).ToString("0.000");
			if (string.IsNullOrEmpty(dataRowView["snumber"].ToString()))
			{
				e.Row.Cells[10].Text = "0.000";
			}
		}
	}
}


