using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Allocation_PickMaterialsOfDepository : NBasePage, IRequiresSessionState
{
	private AllocationBllAction bllAction = new AllocationBllAction();
	private AllocationStockModel stockModel = new AllocationStockModel();
	private ResourceLogicEdit resLogicEdit = new ResourceLogicEdit();
	private ISerializable ser = new JsonSerializer();

	protected override void OnInit(EventArgs e)
	{
		this.GVMaterialList.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Clear();
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["did"] == null || base.Request.QueryString["ac"] == null)
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "alert", "<script>alert('\\n系统提示：\\n页面错误 请找技术人员！');</script>");
				return;
			}
			this.HdnOperator.Value = base.Request.QueryString["op"];
			this.Hdnacode.Value = base.Request.QueryString["ac"];
			this.Bind();
			this.hdtp.Value = "0";
		}
	}
	protected void GVMaterialList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			Label label = (Label)e.Row.Cells[1].FindControl("lblNo");
			e.Row.Cells[0].Attributes["id"] = dataRowView["ResourceCode"].ToString();
			dataRowView["unit"].ToString();
			((Label)e.Row.Cells[5].FindControl("lblUnit")).Text = this.resLogicEdit.GetUnitNameList(dataRowView["unit"].ToString());
			label.Text = (this.GVMaterialList.PageIndex * this.GVMaterialList.PageSize + (e.Row.RowIndex + 1)).ToString();
		}
	}
	protected void btnSertch_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void Bind()
	{
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		DataTable materialOfDepositoryList = allocationBllAction.GetMaterialOfDepositoryList((base.Request.QueryString["did"] == "") ? "0" : base.Request.QueryString["did"], this.getStrWhere());
		if (materialOfDepositoryList != null && materialOfDepositoryList.Rows.Count > NBasePage.pagesize)
		{
			this.HdnIsPage.Value = "1";
		}
		Common2.BindGvTable(materialOfDepositoryList, this.GVMaterialList, false);
		List<string> list = new List<string>();
		if (this.hdTsid.Value != "")
		{
			list = JsonHelper.GetListFromJson(this.hdTsid.Value);
		}
		foreach (GridViewRow gridViewRow in this.GVMaterialList.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkSelectIt") as CheckBox;
			if (checkBox != null)
			{
				HiddenField hiddenField = gridViewRow.FindControl("hdScode") as HiddenField;
				HiddenField hiddenField2 = gridViewRow.FindControl("hdSprice") as HiddenField;
				HiddenField hiddenField3 = gridViewRow.FindControl("hdCorp") as HiddenField;
				string item = string.Concat(new string[]
				{
					hiddenField.Value,
					"|",
					hiddenField2.Value,
					"|",
					hiddenField3.Value,
					"|"
				});
				if (list.Contains(item))
				{
					checkBox.Checked = true;
				}
			}
		}
	}
	protected string getStrWhere()
	{
		string text = "";
		text += " 1=1 ";
		if (this.txtResCode.Text.Trim() != "")
		{
			text += DBHelper.GetQuerySql("scode", this.txtResCode.Text.Trim());
		}
		if (this.txtResName.Text.Trim() != "")
		{
			text += DBHelper.GetQuerySql("ResourceName", this.txtResName.Text.Trim());
		}
		if (this.txtSupply.Text.Trim() != "")
		{
			text += DBHelper.GetQuerySql("CorpName", this.txtSupply.Text.Trim());
		}
		return text;
	}
	protected void GVMaterialList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVMaterialList.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	protected void chkSelectIt_CheckedChanged(object sender, EventArgs e)
	{
		if (this.hdtp.Value == "0")
		{
			this.tsid();
		}
	}
	public void tsid()
	{
		List<string> list = new List<string>();
		if (this.hdTsid.Value != "")
		{
			list = JsonHelper.GetListFromJson(this.hdTsid.Value);
		}
		foreach (GridViewRow gridViewRow in this.GVMaterialList.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkSelectIt") as CheckBox;
			if (checkBox != null)
			{
				HiddenField hiddenField = gridViewRow.FindControl("hdScode") as HiddenField;
				HiddenField hiddenField2 = gridViewRow.FindControl("hdSprice") as HiddenField;
				HiddenField hiddenField3 = gridViewRow.FindControl("hdCorp") as HiddenField;
				string item = string.Concat(new string[]
				{
					hiddenField.Value,
					"|",
					hiddenField2.Value,
					"|",
					hiddenField3.Value,
					"|"
				});
				if (checkBox.Checked)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				else
				{
					if (list.Contains(item))
					{
						list.Remove(item);
					}
				}
			}
		}
		this.hdTsid.Value = JsonHelper.Serialize(list.ToArray());
	}
}


