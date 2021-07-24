using ASP;
using cn.justwin.BLL;
using cn.justwin.contractDAL;
using cn.justwin.VehiclesBLL;
using cn.justwin.VehiclesModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_Main_VehicleReport : NBasePage, IRequiresSessionState
{
	private PayoutContract payoutContract = new PayoutContract();
	private TypeAndStateBll typeBLL = new TypeAndStateBll();
	private MainBLL BLL = new MainBLL();

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		this.gvwContract.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			List<MainModel> modelList = this.BLL.GetModelList(" IsShare <2 ORDER BY id ASC ");
			this.DataBindContracts(modelList);
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		List<MainModel> mainList = this.SearchVehicle();
		this.DataBindContracts(mainList);
	}
	private List<MainModel> SearchVehicle()
	{
		string startDate = string.IsNullOrEmpty(this.txtBeinTime.Text.Trim()) ? "" : this.txtBeinTime.Text;
		string endDate = string.IsNullOrEmpty(this.txtEndTime.Text.Trim()) ? "" : this.txtEndTime.Text.Trim();
		string startDateBX = string.IsNullOrEmpty(this.txtEndTimeBX1.Text.Trim()) ? "" : this.txtEndTimeBX1.Text;
		string endDateBX = string.IsNullOrEmpty(this.txtEndTimeBX2.Text.Trim()) ? "" : this.txtEndTimeBX2.Text.Trim();
		string startDateNJ = string.IsNullOrEmpty(this.txtEndTimeNJ1.Text.Trim()) ? "" : this.txtEndTimeNJ1.Text;
		string endDateNJ = string.IsNullOrEmpty(this.txtEndTimeNJ2.Text.Trim()) ? "" : this.txtEndTimeNJ2.Text.Trim();
		string startDateSH = string.IsNullOrEmpty(this.txtEndTimesh0.Text.Trim()) ? "" : this.txtEndTimesh0.Text;
		string endDateSH = string.IsNullOrEmpty(this.txtEndTimesh1.Text.Trim()) ? "" : this.txtEndTimesh1.Text.Trim();
		string vehicleType = string.IsNullOrEmpty(this.TypeControl1.TypeID) ? "" : this.TypeControl1.TypeID;
		string vehicleCode = string.IsNullOrEmpty(this.txtVehicleCode.Text.ToString().Trim()) ? "" : this.txtVehicleCode.Text.ToString().Trim();
		string vehicleIdentify = string.IsNullOrEmpty(this.txtVehicleIdentify.Text.Trim()) ? "" : this.txtVehicleIdentify.Text.Trim();
		string address = string.IsNullOrEmpty(this.txtAddress.Text.Trim()) ? "" : this.txtAddress.Text.Trim();
		string vehicleName = string.IsNullOrEmpty(this.txtVehicleName.Text.ToString().Trim()) ? "" : this.txtVehicleName.Text.ToString().Trim();
		string engineCode = string.IsNullOrEmpty(this.txtEngineCode.Text.ToString().Trim()) ? "" : this.txtEngineCode.Text.ToString().Trim();
		string state = string.IsNullOrEmpty(this.hfldStateID.Value.ToString().Trim()) ? "" : this.hfldStateID.Value.ToString().Trim();
		string isShare = "";
		if (this.DropDownList1.SelectedValue != "")
		{
			isShare = this.DropDownList1.SelectedValue.ToString();
		}
		string order = " ORDER BY id ASC";
		return this.BLL.SearchVehicleList(startDate, endDate, startDateBX, endDateBX, startDateNJ, endDateNJ, startDateSH, endDateSH, vehicleType, vehicleCode, vehicleIdentify, address, vehicleName, engineCode, state, isShare, order);
	}
	private void DataBindContracts(List<MainModel> mainList)
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
			e.Row.Cells[10].Text = this.typeBLL.GetModel(new Guid(e.Row.Cells[10].Text.ToString())).Name.ToString();
		}
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
		if (list.Count <= 0)
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
			return;
		}
		string text = string.Empty;
		for (int i = 0; i < list.Count; i++)
		{
			text = text + "'" + list[i].ToString() + "',";
		}
		text = text.Substring(0, text.Length - 1);
		if (this.BLL.DeleteList(text))
		{
			base.RegisterScript("window.location = window.location;");
			return;
		}
		base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
	}
	protected void gvwContract_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwContract.PageIndex = e.NewPageIndex;
		List<MainModel> mainList = this.SearchVehicle();
		this.DataBindContracts(mainList);
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


