using ASP;
using cn.justwin.BLL;
using cn.justwin.contractDAL;
using cn.justwin.VehiclesBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_Main_Default : NBasePage, IRequiresSessionState
{
	private PayoutContract payoutContract = new PayoutContract();
	private TypeAndStateBll typeBLL = new TypeAndStateBll();
	private MainBLL BLL = new MainBLL();
	private InsuranceAndReviewBLL IAR = new InsuranceAndReviewBLL();

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		this.hdnVehiclesCode.Value = "";
		this.gvwContract.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			DataTable mainList = this.SearchVehicle();
			this.DataBindContracts(mainList);
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		DataTable mainList = this.SearchVehicle();
		this.DataBindContracts(mainList);
	}
	private DataTable SearchVehicle()
	{
		string startDate = string.Empty;
		if (this.txtBeinTime.Text.Trim().ToString() != "")
		{
			startDate = this.txtBeinTime.Text;
		}
		else
		{
			startDate = "";
		}
		string endDate = string.Empty;
		if (this.txtEndTime.Text.Trim() != "")
		{
			endDate = this.txtEndTime.Text.Trim();
		}
		else
		{
			endDate = "";
		}
		string vehicleType = string.IsNullOrEmpty(this.TypeControl1.TypeID) ? "" : this.TypeControl1.TypeID;
		string vehicleCode = string.IsNullOrEmpty(this.txtVehicleCode.Text.ToString().Trim()) ? "" : this.txtVehicleCode.Text.ToString().Trim();
		string vehicleIdentify = string.IsNullOrEmpty(this.txtVehicleIdentify.Text.Trim()) ? "" : this.txtVehicleIdentify.Text.Trim();
		string address = string.IsNullOrEmpty(this.txtAddress.Text.Trim()) ? "" : this.txtAddress.Text.Trim();
		string vehicleName = string.IsNullOrEmpty(this.txtVehicleName.Text.ToString().Trim()) ? "" : this.txtVehicleName.Text.ToString().Trim();
		string isShare = "";
		if (this.DropDownList1.SelectedValue != "")
		{
			isShare = this.DropDownList1.SelectedValue.ToString();
		}
		string order = " ORDER BY id asc";
		return this.BLL.SearchVehicleTable(startDate, endDate, vehicleType, vehicleCode, vehicleIdentify, address, vehicleName, null, null, isShare, order);
	}
	private void DataBindContracts(DataTable mainList)
	{
		this.gvwContract.DataSource = mainList;
		this.gvwContract.DataBind();
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Cells[11].Text = this.typeBLL.GetModel(new Guid(e.Row.Cells[11].Text.ToString())).Name.ToString();
		}
	}
	protected void gvwContract_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwContract.PageIndex = e.NewPageIndex;
		DataTable mainList = this.SearchVehicle();
		this.DataBindContracts(mainList);
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> list = new List<string>();
		if (this.hfldContract.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldContract.Value);
		}
		else
		{
			list.Add(this.hfldContract.Value);
		}
		for (int i = 0; i < list.Count; i++)
		{
			DataTable list2 = this.IAR.GetList("OA_Vehicle_InsuranceAndReview.VehicleCode='" + new Guid(list[i].ToString()) + "'");
			if (list2.Rows.Count > 0)
			{
				base.RegisterScript("alert('系统提示：\\n\\n删除失败, 此车辆正在使用中！');");
				return;
			}
		}
		if (list.Count <= 0)
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
			return;
		}
		string text = string.Empty;
		for (int j = 0; j < list.Count; j++)
		{
			text = text + "'" + list[j].ToString() + "',";
		}
		text = text.Substring(0, text.Length - 1);
		if (this.BLL.DeleteList(text))
		{
			base.RegisterShow("系统提示", "删除成功");
			DataTable mainList = this.SearchVehicle();
			this.DataBindContracts(mainList);
			return;
		}
		base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
	}
	public string getVehicleName(string guid)
	{
		string result = string.Empty;
		if (guid != null)
		{
			TypeAndStateBll typeAndStateBll = new TypeAndStateBll();
			if (typeAndStateBll.Exists(new Guid(guid)))
			{
				result = typeAndStateBll.GetModel(new Guid(guid)).Name;
			}
		}
		return result;
	}
}


