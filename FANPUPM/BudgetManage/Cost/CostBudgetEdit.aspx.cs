using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using cn.justwin.Web;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_CostBudgetEdit : NBasePage, IRequiresSessionState
{
	private static bool isOrgDiary;
	private string id = string.Empty;
	private string costType = string.Empty;
	private BudPreReimburseApplyService IndiApplySer = new BudPreReimburseApplyService();
	private BudPreReimburseApplyDetailService InApplydetailSer = new BudPreReimburseApplyDetailService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"];
		}
		if (!string.IsNullOrEmpty(base.Request["costType"]))
		{
			this.costType = base.Request["costType"];
		}
		if (ConfigHelper.Get("CostDiaryNoReadonly") == "1")
		{
			this.txtIndireCode.ReadOnly = true;
			this.txtIndireCode.CssClass = "readonly";
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["year"] == "zzjg")
			{
				BudgetManage_Cost_CostBudgetEdit.isOrgDiary = true;
			}
			else
			{
				BudgetManage_Cost_CostBudgetEdit.isOrgDiary = false;
			}
			this.InitPage();
		}
	}
	protected void InitPage()
	{
		if (!string.IsNullOrEmpty(this.id))
		{
			this.InitEditPage();
			return;
		}
		this.InitAddPage();
	}
	protected void InitAddPage()
	{
		this.txtInputDate.Text = System.DateTime.Now.ToString("yyyy-M-dd");
		string text = PageHelper.QueryUser(this, base.UserCode);
		this.txtInputUser.Text = text;
		this.ViewState["guid"] = System.Guid.NewGuid().ToString();
		this.txtIndireCode.Text = this.GetIndiaryCode(BudgetManage_Cost_CostBudgetEdit.isOrgDiary, base.Request["prjId"].ToString());
		this.AddDiaryDetails();
	}
	protected void InitEditPage()
	{
		BudPreReimburseApply byId = this.IndiApplySer.GetById(this.id);
		this.txtName.Text = byId.Name;
		this.txtInputDate.Text = byId.InputDate.ToString("yyyy-M-dd");
		this.txtInputUser.Text = byId.RptUser;
		this.txtIndireCode.Text = byId.Code;
		this.costType = byId.CostType;
		this.ViewState["guid"] = base.Request["id"];
		this.ViewState["details"] = this.GetApplyDetails(this.id);
		this.BindDetails();
	}
	protected void BindDetails()
	{
		if (this.ViewState["details"] != null)
		{
			System.Collections.Generic.List<BudPreReimburseApplyDetail> list = this.ViewState["details"] as System.Collections.Generic.List<BudPreReimburseApplyDetail>;
			if (list.Count > 0)
			{
				base.RegisterScript("fillTotalAmount('" + (
					from p in list
					where p.ApplyId == this.id
					select p).Sum((BudPreReimburseApplyDetail m) => m.Cost).ToString("F3") + "')");
			}
			this.gvDiaryDetails.DataSource = list;
		}
		this.gvDiaryDetails.DataBind();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (this.gvDiaryDetails.EditIndex != -1)
		{
			int editIndex = this.gvDiaryDetails.EditIndex;
			GridViewRow gridViewRow = this.gvDiaryDetails.Rows[editIndex];
			if (!BudgetManage_Cost_CostBudgetEdit.isOrgDiary)
			{
				DropDownList dropDownList = gridViewRow.Cells[3].FindControl("ddlCBSCode") as DropDownList;
				if (dropDownList != null && string.IsNullOrEmpty(dropDownList.SelectedValue))
				{
					base.RegisterScript(string.Concat(new string[]
					{
						"setWidthHeight();top.ui.alert('请先添加间接成本科目！');wClose('CostBudgetApply.aspx?year=",
						base.Request["year"],
						"&prjId=",
						base.Request["prjId"],
						"');"
					}));
					return;
				}
			}
		}
		this.SaveDiary();
		this.SaveDiaryDetails();
		this.GoBack();
	}
	protected void GoBack()
	{
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_CostBudgetEdit' });");
	}
	protected void SaveDiary()
	{
		if (string.IsNullOrEmpty(this.id))
		{
			BudPreReimburseApply budPreReimburseApply = new BudPreReimburseApply();
			budPreReimburseApply.Id = this.ViewState["guid"].ToString();
			budPreReimburseApply.PrjId = base.Request["prjId"];
			budPreReimburseApply.Name = this.txtName.Text.Trim();
			budPreReimburseApply.ApplyDate = System.Convert.ToDateTime(this.txtInputDate.Text);
			budPreReimburseApply.RptUser = this.txtInputUser.Text.Trim();
			budPreReimburseApply.Code = this.txtIndireCode.Text.Trim();
			budPreReimburseApply.CostType = this.costType;
			budPreReimburseApply.FlowState = -1;
			budPreReimburseApply.InputDate = System.DateTime.Now;
			this.IndiApplySer.Add(budPreReimburseApply);
			return;
		}
		BudPreReimburseApply byId = this.IndiApplySer.GetById(this.id);
		if (byId != null)
		{
			byId.PrjId = base.Request["prjId"];
			byId.Name = this.txtName.Text.Trim();
			byId.ApplyDate = System.Convert.ToDateTime(this.txtInputDate.Text);
			byId.RptUser = this.txtInputUser.Text.Trim();
			byId.Code = this.txtIndireCode.Text.Trim();
			this.IndiApplySer.Update(byId);
		}
	}
	protected void SaveDiaryDetails()
	{
		this.SaveEditData();
		System.Collections.Generic.List<BudPreReimburseApplyDetail> list = this.ViewState["details"] as System.Collections.Generic.List<BudPreReimburseApplyDetail>;
		this.ClearDiaryDetails(this.ViewState["guid"].ToString());
		if (list != null)
		{
			foreach (BudPreReimburseApplyDetail current in list)
			{
				this.InApplydetailSer.Add(current);
			}
		}
	}
	protected void btnAdd_Click(object sender, System.EventArgs e)
	{
		this.AddDiaryDetails();
	}
	protected void AddDiaryDetails()
	{
		this.gvDiaryDetails.Style.Add("min-width", "1024px");
		System.Collections.Generic.List<BudPreReimburseApplyDetail> list = new System.Collections.Generic.List<BudPreReimburseApplyDetail>();
		if (this.ViewState["details"] != null)
		{
			list = (this.ViewState["details"] as System.Collections.Generic.List<BudPreReimburseApplyDetail>);
		}
		string text = this.ViewState["guid"].ToString();
		list.Add(new BudPreReimburseApplyDetail
		{
			Id = System.Guid.NewGuid().ToString(),
			ApplyId = text,
			Name = string.Empty,
			Cost = 0m,
			CBSCode = text,
			Note = string.Empty
		});
		this.ViewState["details"] = list;
		this.SaveEditData();
		this.gvDiaryDetails.EditIndex = list.Count - 1;
		this.hfldSingleId.Value = list.Count.ToString();
		this.BindDetails();
	}
	protected void btnEdit_Click(object sender, System.EventArgs e)
	{
		this.gvDiaryDetails.Style.Add("min-width", "1024px");
		this.SaveEditData();
		this.gvDiaryDetails.EditIndex = int.Parse(this.hfldSingleId.Value) - 1;
		this.BindDetails();
	}
	protected void SaveEditData()
	{
		if (this.gvDiaryDetails.EditIndex != -1)
		{
			int editIndex = this.gvDiaryDetails.EditIndex;
			GridViewRow gridViewRow = this.gvDiaryDetails.Rows[editIndex];
			System.Collections.Generic.List<BudPreReimburseApplyDetail> list = this.ViewState["details"] as System.Collections.Generic.List<BudPreReimburseApplyDetail>;
			BudPreReimburseApplyDetail budPreReimburseApplyDetail = list[gridViewRow.RowIndex];
			BudPreReimburseApply budPreReimburseApply = new BudPreReimburseApply();
			this.costType = budPreReimburseApply.CostType;
			TextBox textBox = gridViewRow.Cells[2].FindControl("txtgvName") as TextBox;
			if (textBox != null)
			{
				budPreReimburseApplyDetail.Name = textBox.Text.Trim();
			}
			DropDownList dropDownList = gridViewRow.Cells[3].FindControl("ddlCBSCode") as DropDownList;
			if (dropDownList != null)
			{
				budPreReimburseApplyDetail.CBSCode = dropDownList.SelectedValue;
			}
			TextBox textBox2 = gridViewRow.Cells[4].FindControl("txtgvAmount") as TextBox;
			if (textBox2 != null)
			{
				budPreReimburseApplyDetail.Cost = decimal.Parse(textBox2.Text.Trim());
			}
			TextBox textBox3 = gridViewRow.Cells[5].FindControl("txtgvNote") as TextBox;
			if (textBox3 != null)
			{
				budPreReimburseApplyDetail.Note = textBox3.Text.Trim();
			}
			this.ViewState["details"] = list;
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		this.SaveEditData();
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldCheckedIds.Value);
		System.Collections.Generic.List<BudPreReimburseApplyDetail> list = this.ViewState["details"] as System.Collections.Generic.List<BudPreReimburseApplyDetail>;
		BudPreReimburseApply budPreReimburseApply = new BudPreReimburseApply();
		this.costType = budPreReimburseApply.CostType;
		foreach (string arg_4F_0 in listFromJson)
		{
			list.RemoveAll(new System.Predicate<BudPreReimburseApplyDetail>(this.RemoveSearch));
		}
		this.ViewState["details"] = list;
		this.gvDiaryDetails.EditIndex = -1;
		this.BindDetails();
	}
	private bool RemoveSearch(BudPreReimburseApplyDetail indirectDetail)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldCheckedIds.Value);
		return this.IsExist(indirectDetail.Id, listFromJson);
	}
	protected bool IsExist(string id, System.Collections.Generic.List<string> ids)
	{
		bool result = false;
		foreach (string current in ids)
		{
			if (id == current)
			{
				result = true;
			}
		}
		return result;
	}
	protected void gvDiaryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvDiaryDetails.DataKeys[e.Row.RowIndex]["Id"].ToString();
			e.Row.Attributes["Id"] = this.gvDiaryDetails.DataKeys[e.Row.RowIndex]["Id"].ToString();
			DropDownList dropDownList = e.Row.Cells[3].FindControl("ddlCBSCode") as DropDownList;
			if (dropDownList != null)
			{
				dropDownList.DataSource = CostAccounting.GetIndirectCost();
				dropDownList.DataTextField = "Name";
				dropDownList.DataValueField = "Code";
				dropDownList.DataBind();
				dropDownList.SelectedValue = this.gvDiaryDetails.DataKeys[e.Row.RowIndex]["CBSCode"].ToString();
			}
		}
	}
	protected string GetIndiaryCode(bool isOrgDiary, string prjId)
	{
		string str = "ZZJG";
		if (!isOrgDiary && !string.IsNullOrEmpty(prjId))
		{
			string prjCode = this.GetPrjCode(prjId);
			str = (string.IsNullOrEmpty(prjCode) ? string.Empty : prjCode.Substring(prjCode.Length - 4));
		}
		string str2 = Common2.GetTime(System.DateTime.Now).Replace("-", "");
		string text = str + str2;
		int count;
		if (isOrgDiary)
		{
			count = OrganizationDiary.GetOrgainCostCount(text) + 1;
		}
		else
		{
			count = CostDiary.GetCostcount(text) + 1;
		}
		return this.Code(text, count, isOrgDiary);
	}
	protected string Code(string signCode, int count, bool isOrgDiary)
	{
		string str;
		if (count.ToString().Length == 1)
		{
			str = "0000" + count;
		}
		else
		{
			if (count.ToString().Length == 2)
			{
				str = "000" + count;
			}
			else
			{
				if (count.ToString().Length == 3)
				{
					str = "00" + count;
				}
				else
				{
					if (count.ToString().Length == 4)
					{
						str = "0" + count;
					}
					else
					{
						str = count.ToString();
					}
				}
			}
		}
		string text = signCode + str;
		int num = isOrgDiary ? OrganizationDiary.GetOrgainCostCount(text) : CostDiary.GetCostcount(text);
		if (num > 0)
		{
			text = this.Code(signCode, count + 1, isOrgDiary);
		}
		return text;
	}
	private string GetPrjCode(string prjId)
	{
		string result = string.Empty;
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId = pTPrjInfoService.GetById(prjId);
		if (byId != null)
		{
			result = byId.PrjCode;
		}
		else
		{
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTB byId2 = pTPrjInfoZTBService.GetById(prjId);
			if (byId2 != null)
			{
				result = byId2.PrjCode;
			}
		}
		return result;
	}
	private System.Collections.Generic.List<BudPreReimburseApplyDetail> GetApplyDetails(string id)
	{
		return (
			from d in this.InApplydetailSer
			where d.ApplyId == id
			orderby d.CBSCode
			select d).ToList<BudPreReimburseApplyDetail>();
	}
	public void ClearDiaryDetails(string applyId)
	{
		if (!string.IsNullOrEmpty(applyId))
		{
			System.Collections.Generic.IList<BudPreReimburseApplyDetail> list = (
				from detail in this.InApplydetailSer
				where applyId.Equals(detail.ApplyId)
				select detail).ToList<BudPreReimburseApplyDetail>();
			if (list != null && list.Count > 0)
			{
				foreach (BudPreReimburseApplyDetail current in list)
				{
					this.InApplydetailSer.Delete(current);
				}
			}
		}
	}
	public string CBSName(string CBSCode)
	{
		CostAccounting byCode = CostAccounting.GetByCode(CBSCode);
		if (byCode == null)
		{
			return string.Empty;
		}
		return byCode.Name;
	}
}


