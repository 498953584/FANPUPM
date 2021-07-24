using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_CostDiaryEdit : NBasePage, IRequiresSessionState
{
	private static bool isOrgDiary;
	private string id = string.Empty;
	private string costType = string.Empty;
	private BudIndirectDiaryCostService budInCostSer = new BudIndirectDiaryCostService();
	private BudIndirectDiaryDetailsService budInDetailSer = new BudIndirectDiaryDetailsService();
	private PCPettyCashService pettyCashSer = new PCPettyCashService();

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
				BudgetManage_Cost_CostDiaryEdit.isOrgDiary = true;
			}
			else
			{
				BudgetManage_Cost_CostDiaryEdit.isOrgDiary = false;
			}
			this.InitPage();
		}
		this.txtPeople.Attributes.Add("readonly", "readonly");
		this.hfldAdjunctPath.Value = ConfigHelper.Get("IndirectCost");
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
		this.txtPeople.Text = text;
		this.hdnPeople.Value = base.UserCode;
		this.ViewState["guid"] = System.Guid.NewGuid().ToString();
		this.txtIndireCode.Text = this.GetIndiaryCode(BudgetManage_Cost_CostDiaryEdit.isOrgDiary, base.Request["prjId"].ToString());
		this.AddDiaryDetails();
	}
	protected void InitEditPage()
	{
		BudIndirectDiaryCost byId = this.budInCostSer.GetById(this.id);
		this.txtName.Text = byId.Name;
		this.txtIndireCode.Text = byId.IndireCode;
		this.txtDeparment.Text = byId.Department;
		this.txtInputDate.Text = byId.InputDate.ToString("yyyy-M-dd");
		this.txtInputUser.Text = byId.InputUser;
		this.txtPeople.Text = byId.IssuedBy;
		this.hdnPeople.Value = byId.InssuedByCode;
		this.hfldPettyCashId.Value = byId.PettyCashId;
		PCPettyCash byId2 = this.pettyCashSer.GetById(byId.PettyCashId);
		if (byId2 != null)
		{
			this.txtConType.Text = byId2.Matter;
		}
		this.costType = byId.CostType;
		this.ViewState["guid"] = base.Request["id"];
		this.ViewState["details"] = this.GetDiaryDetails(this.id);
		this.BindDetails();
	}
	protected void BindDetails()
	{
		if (this.ViewState["details"] != null)
		{
			System.Collections.Generic.List<BudIndirectDiaryDetails> list = this.ViewState["details"] as System.Collections.Generic.List<BudIndirectDiaryDetails>;
			if (list.Count > 0)
			{
				base.RegisterScript("fillTotalAmount('" + (
					from p in list
					where p.InDiaryId == this.id
					select p).Sum((BudIndirectDiaryDetails m) => m.Amount).ToString("F3") + "')");
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
			if (!BudgetManage_Cost_CostDiaryEdit.isOrgDiary)
			{
				DropDownList dropDownList = gridViewRow.Cells[5].FindControl("ddlCBSCode") as DropDownList;
				if (dropDownList != null && string.IsNullOrEmpty(dropDownList.SelectedValue))
				{
					base.RegisterScript(string.Concat(new string[]
					{
						"setWidthHeight();top.ui.alert('请先添加间接成本科目！');wClose('CostDiary.aspx?year=",
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
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_CostDiaryEdit' });");
	}
	protected void SaveDiary()
	{
		this.txtName.Text.Trim();
		this.txtDeparment.Text.Trim();
		this.txtInputUser.Text.Trim();
		System.Convert.ToDateTime(this.txtInputDate.Text);
		string arg_4F_0 = this.txtPeople.Text;
		this.ViewState["guid"].ToString();
		string arg_76_0 = base.Request["prjId"];
		this.txtIndireCode.Text.Trim();
		this.hdnPeople.Value.Trim();
		string arg_9F_0 = base.UserCode;
		this.txtConType.Text.Trim();
		this.ViewState["pettyCashId"] = this.hfldPettyCashId.Value;
		if (string.IsNullOrEmpty(this.id))
		{
			BudIndirectDiaryCost budIndirectDiaryCost = new BudIndirectDiaryCost();
			budIndirectDiaryCost.InDiaryId = this.ViewState["guid"].ToString();
			budIndirectDiaryCost.InputUserCode = base.UserCode;
			budIndirectDiaryCost.InssuedByCode = this.hdnPeople.Value.Trim();
			budIndirectDiaryCost.Name = this.txtName.Text.Trim();
			budIndirectDiaryCost.Department = this.txtDeparment.Text.Trim();
			budIndirectDiaryCost.InputUser = this.txtInputUser.Text.Trim();
			budIndirectDiaryCost.IssuedBy = this.txtPeople.Text.Trim();
			budIndirectDiaryCost.InputDate = System.Convert.ToDateTime(this.txtInputDate.Text);
			budIndirectDiaryCost.ProjectId = base.Request["prjId"];
			budIndirectDiaryCost.IndireCode = this.txtIndireCode.Text.Trim();
			budIndirectDiaryCost.Type = "0";
			budIndirectDiaryCost.IsAudit = false;
			budIndirectDiaryCost.AuditDate = System.DateTime.Now;
			budIndirectDiaryCost.CostType = this.costType;
			budIndirectDiaryCost.PettyCashId = this.hfldPettyCashId.Value;
			budIndirectDiaryCost.FlowState = -1;
			budIndirectDiaryCost.InputDate2 = System.DateTime.Now;
			this.budInCostSer.Add(budIndirectDiaryCost);
			return;
		}
		BudIndirectDiaryCost byId = this.budInCostSer.GetById(this.id);
		if (byId != null)
		{
			byId.InputUserCode = base.UserCode;
			byId.InssuedByCode = this.hdnPeople.Value.Trim();
			if (this.hfldPettyCashId.Value != "")
			{
				byId.PettyCashId = this.hfldPettyCashId.Value;
			}
			byId.Name = this.txtName.Text.Trim();
			byId.Department = this.txtDeparment.Text.Trim();
			byId.InputUser = this.txtInputUser.Text.Trim();
			byId.IssuedBy = this.txtPeople.Text.Trim();
			byId.InputDate = System.Convert.ToDateTime(this.txtInputDate.Text);
			byId.ProjectId = base.Request["prjId"];
			byId.IndireCode = this.txtIndireCode.Text.Trim();
			this.budInCostSer.Update(byId);
		}
	}
	protected void SaveDiaryDetails()
	{
		this.SaveEditData();
		System.Collections.Generic.List<BudIndirectDiaryDetails> list = this.ViewState["details"] as System.Collections.Generic.List<BudIndirectDiaryDetails>;
		BudgetManage_Cost_CostDiaryEdit.ClearDiaryDetails(this.ViewState["guid"].ToString());
		if (list != null)
		{
			foreach (BudIndirectDiaryDetails current in list)
			{
				current.PettyCashId = (string.IsNullOrEmpty(this.ViewState["pettyCashId"].ToString()) ? string.Empty : this.ViewState["pettyCashId"].ToString());
				this.budInDetailSer.Add(current);
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
		System.Collections.Generic.List<BudIndirectDiaryDetails> list = new System.Collections.Generic.List<BudIndirectDiaryDetails>();
		if (this.ViewState["details"] != null)
		{
			list = (this.ViewState["details"] as System.Collections.Generic.List<BudIndirectDiaryDetails>);
		}
		string text = this.ViewState["guid"].ToString();
		list.Add(new BudIndirectDiaryDetails
		{
			InDiaryDetailsId = System.Guid.NewGuid().ToString(),
			InDiaryId = text,
			PettyCashId = text,
			Name = string.Empty,
			Amount = 0m,
			CBSCode = text,
			Note = string.Empty,
			AuditAmount = 0m,
			No = 0
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
			System.Collections.Generic.List<BudIndirectDiaryDetails> list = this.ViewState["details"] as System.Collections.Generic.List<BudIndirectDiaryDetails>;
			BudIndirectDiaryDetails budIndirectDiaryDetails = list[gridViewRow.RowIndex];
			BudIndirectDiaryCost budIndirectDiaryCost = new BudIndirectDiaryCost();
			this.costType = budIndirectDiaryCost.CostType;
			budIndirectDiaryDetails.No = editIndex + 1;
			TextBox textBox = gridViewRow.Cells[2].FindControl("txtgvCode") as TextBox;
			if (textBox != null)
			{
				budIndirectDiaryDetails.IndetailsCode = textBox.Text.Trim();
			}
			TextBox textBox2 = gridViewRow.Cells[3].FindControl("txtgvName") as TextBox;
			if (textBox2 != null)
			{
				budIndirectDiaryDetails.Name = textBox2.Text.Trim();
			}
			TextBox textBox3 = gridViewRow.Cells[4].FindControl("txtgvAmount") as TextBox;
			if (textBox3 != null)
			{
				budIndirectDiaryDetails.Amount = decimal.Parse(textBox3.Text.Trim());
			}
			DropDownList dropDownList = gridViewRow.Cells[5].FindControl("ddlCBSCode") as DropDownList;
			if (dropDownList != null)
			{
				budIndirectDiaryDetails.CBSCode = dropDownList.SelectedValue;
			}
			TextBox textBox4 = gridViewRow.Cells[6].FindControl("txtgvNote") as TextBox;
			if (textBox4 != null)
			{
				budIndirectDiaryDetails.Note = textBox4.Text.Trim();
			}
			this.ViewState["details"] = list;
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		this.SaveEditData();
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldCheckedIds.Value);
		System.Collections.Generic.List<BudIndirectDiaryDetails> list = this.ViewState["details"] as System.Collections.Generic.List<BudIndirectDiaryDetails>;
		BudIndirectDiaryCost budIndirectDiaryCost = new BudIndirectDiaryCost();
		this.costType = budIndirectDiaryCost.CostType;
		foreach (string arg_4F_0 in listFromJson)
		{
			list.RemoveAll(new System.Predicate<BudIndirectDiaryDetails>(this.RemoveSearch));
		}
		this.ViewState["details"] = list;
		this.gvDiaryDetails.EditIndex = -1;
		this.BindDetails();
	}
	private bool RemoveSearch(BudIndirectDiaryDetails indirectDetail)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldCheckedIds.Value);
		return this.IsExist(indirectDetail.InDiaryDetailsId, listFromJson);
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
			e.Row.Attributes["id"] = this.gvDiaryDetails.DataKeys[e.Row.RowIndex]["InDiaryDetailsId"].ToString();
			e.Row.Attributes["InDiaryDetailsId"] = this.gvDiaryDetails.DataKeys[e.Row.RowIndex]["InDiaryDetailsId"].ToString();
			DropDownList dropDownList = e.Row.Cells[5].FindControl("ddlCBSCode") as DropDownList;
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
	private System.Collections.Generic.List<BudIndirectDiaryDetails> GetDiaryDetails(string id)
	{
		return (
			from d in this.budInDetailSer
			where d.InDiaryId == id
			orderby d.No
			select d).ToList<BudIndirectDiaryDetails>();
	}
	private static void ClearDiaryDetails(string diaryId)
	{
		if (!string.IsNullOrEmpty(diaryId))
		{
			using (pm2Entities pm2Entities = new pm2Entities())
			{
				System.Collections.Generic.List<Bud_IndirectDiaryDetails> list = (
					from d in pm2Entities.Bud_IndirectDiaryDetails
					where d.Bud_IndirectDiaryCost.InDiaryId == diaryId
					select d).ToList<Bud_IndirectDiaryDetails>();
				foreach (Bud_IndirectDiaryDetails current in list)
				{
					pm2Entities.DeleteObject(current);
				}
				pm2Entities.SaveChanges();
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


