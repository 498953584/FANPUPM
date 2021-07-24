using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Equipment_EquipmentList : NBasePage, IRequiresSessionState
{
	private EquEquipmentService equipmentSer = new EquEquipmentService();
	private EquEquipmentTypeService equipmentTypeSer = new EquEquipmentTypeService();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindStateAndProperty();
			this.BindGV();
		}
	}
	private void BindGV()
	{
		DataTable equEquipmentInfo = this.equipmentSer.GetEquEquipmentInfo(this.hfldEquTypeId.Value.Trim(), this.ddlState.SelectedValue, this.ddlProperty.SelectedValue, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
		this.AspNetPager1.RecordCount = equEquipmentInfo.Rows.Count;
		int num = (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize;
		DataRow[] array = equEquipmentInfo.Select(string.Format("Num >= {0} AND Num <= {1}", num + 1, num + this.AspNetPager1.PageSize));
		DataTable dataTable = equEquipmentInfo.Copy();
		dataTable.Clear();
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow dataRow = array2[i];
			dataTable.Rows.Add(dataRow.ItemArray);
		}
		this.gvEquipment.DataSource = dataTable;
		this.gvEquipment.DataBind();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGV();
	}
	private void BindStateAndProperty()
	{
		BasicCodeListService basicCodeListService = new BasicCodeListService();
		this.ddlState.DataSource = basicCodeListService.GetByType("EquState");
		this.ddlState.DataTextField = "ItemName";
		this.ddlState.DataValueField = "ItemCode";
		this.ddlState.DataBind();
		this.ddlState.Items.Insert(0, new ListItem("", ""));
		this.ddlProperty.DataSource = basicCodeListService.GetByType("EquProperty");
		this.ddlProperty.DataTextField = "ItemName";
		this.ddlProperty.DataValueField = "ItemCode";
		this.ddlProperty.DataBind();
		this.ddlProperty.Items.Insert(0, new ListItem("", ""));
	}
	protected void gvEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvEquipment.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGV();
	}
	protected string GetCorpName(object corpId)
	{
		string result = string.Empty;
		if (!string.IsNullOrEmpty(corpId.ToString()))
		{
			XPMBasicContactCorpService xPMBasicContactCorpService = new XPMBasicContactCorpService();
			XPMBasicContactCorp byId = xPMBasicContactCorpService.GetById(System.Convert.ToInt32(corpId));
			if (byId != null)
			{
				result = byId.CorpName;
			}
		}
		return result;
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldIdsChecked.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldIdsChecked.Value);
		}
		else
		{
			list.Add(this.hfldIdsChecked.Value);
		}
		try
		{
			EquShipTechnicalParasService equShipTechnicalParasService = new EquShipTechnicalParasService();
			foreach (string current in list)
			{
				equShipTechnicalParasService.DeleteAllEquShipTechInfos(current);
				EquEquipment byId = this.equipmentSer.GetById(current);
				this.equipmentSer.Delete(byId);
			}
			base.RegisterScript("top.ui.show('删除成功！');");
			this.BindGV();
		}
		catch
		{
			base.RegisterScript("top.ui.show('删除失败！');");
		}
	}
}


