using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_CostPreReimburseModifyEdit : NBasePage, IRequiresSessionState
{
	private static bool isOrgDiary;
	private string id = string.Empty;
	private string costType = string.Empty;
	private BudPreReimburseModifyService budModifySer = new BudPreReimburseModifyService();
	private BudPreReimburseModifyDetailService budModifyDetailSer = new BudPreReimburseModifyDetailService();
	private BudPreReimburseApplyService budApplySer = new BudPreReimburseApplyService();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"];
		}
		if (!string.IsNullOrEmpty(base.Request["costType"]))
		{
			this.costType = base.Request["costType"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["year"] == "zzjg")
			{
				BudgetManage_Cost_CostPreReimburseModifyEdit.isOrgDiary = true;
			}
			else
			{
				BudgetManage_Cost_CostPreReimburseModifyEdit.isOrgDiary = false;
			}
			this.InitPage();
		}
		this.txtIndireCode.Attributes["ReadyOnly"] = "true";
	}
	public void InitPage()
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
		this.txtInputDate.Text = DateTime.Now.ToString("yyyy-M-dd");
		string text = PageHelper.QueryUser(this, base.UserCode);
		this.txtInputUser.Text = text;
		this.ViewState["guid"] = Guid.NewGuid().ToString();
	}
	protected void InitEditPage()
	{
		BudPreReimburseModify byId = this.budModifySer.GetById(this.id);
		this.txtName.Text = byId.Name;
		this.txtInputDate.Text = byId.InputDate.ToString("yyyy-M-dd");
		this.txtInputUser.Text = byId.RptUser;
		this.txtIndireCode.Text = byId.Code;
		this.hfldModifyId.Value = byId.ApplyId;
		BudPreReimburseApply byId2 = this.budApplySer.GetById(byId.ApplyId);
		if (byId2 != null)
		{
			this.txtIndireCode.Text = byId.Code;
		}
		this.costType = byId.CostType;
		this.ViewState["guid"] = base.Request["id"];
		this.ViewState["details"] = this.GetModifyDetails(this.id);
		this.BindDetails();
	}
	private List<BudPreReimburseModifyDetail> GetModifyDetails(string id)
	{
		return (
			from d in this.budModifyDetailSer
			where d.ModifyId == id
			orderby d.CBSCode
			select d).ToList<BudPreReimburseModifyDetail>();
	}
	public void AddDiaryDetails()
	{
		this.gvDiaryDetails.Style.Add("min-width", "1024px");
		List<BudPreReimburseModifyDetail> list = new List<BudPreReimburseModifyDetail>();
		if (this.ViewState["details"] != null)
		{
			list = (this.ViewState["details"] as List<BudPreReimburseModifyDetail>);
		}
		string modifyId = this.ViewState["guid"].ToString();
		DataTable table = this.budModifyDetailSer.GetTable(this.hfldModifyId.Value);
		for (int i = 0; i < table.Rows.Count; i++)
		{
			list.Add(new BudPreReimburseModifyDetail
			{
				Id = Guid.NewGuid().ToString(),
				ModifyId = modifyId,
				Name = table.Rows[i]["Name"].ToString(),
				CBSCode = table.Rows[i]["CBSCode"].ToString(),
				BeginCost = Convert.ToDecimal(table.Rows[i]["Cost"]),
				AfterCost = 0m,
				ModifyReason = string.Empty,
				Note = table.Rows[i]["Note"].ToString()
			});
		}
		this.ViewState["details"] = list;
		this.gvDiaryDetails.EditIndex = list.Count - 1;
		this.hfldSingleId.Value = list.Count.ToString();
		this.BindDetails();
	}
	protected void BindDetails()
	{
		if (this.ViewState["details"] != null)
		{
			List<BudPreReimburseModifyDetail> list = this.ViewState["details"] as List<BudPreReimburseModifyDetail>;
			if (list.Count > 0)
			{
				decimal num = (
					from p in list
					where p.ModifyId == this.id
					select p).Sum((BudPreReimburseModifyDetail m) => m.BeginCost);
				decimal num2 = (
					from p in list
					where p.ModifyId == this.id
					select p).Sum((BudPreReimburseModifyDetail m) => m.AfterCost);
				base.RegisterScript(string.Concat(new string[]
				{
					"fillTotalAmount('",
					num.ToString("F3"),
					"','",
					num2.ToString("F3"),
					"')"
				}));
			}
			this.gvDiaryDetails.DataSource = list;
		}
		this.gvDiaryDetails.DataBind();
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (this.gvDiaryDetails.EditIndex != -1)
		{
			int editIndex = this.gvDiaryDetails.EditIndex;
			GridViewRow gridViewRow = this.gvDiaryDetails.Rows[editIndex];
			if (!BudgetManage_Cost_CostPreReimburseModifyEdit.isOrgDiary)
			{
				DropDownList dropDownList = gridViewRow.Cells[3].FindControl("ddlCBSCode") as DropDownList;
				if (dropDownList != null && string.IsNullOrEmpty(dropDownList.SelectedValue))
				{
					base.RegisterScript(string.Concat(new string[]
					{
						"setWidthHeight();top.ui.alert('请先添加间接成本科目！');wClose('CostPreReimburseModify.aspx?year=",
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
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_CostPreReimburseModifyEdit' });");
	}
	public void ClearModifyDetails(string applyId)
	{
		if (!string.IsNullOrEmpty(applyId))
		{
			IList<BudPreReimburseModifyDetail> list = (
				from detail in this.budModifyDetailSer
				where applyId.Equals(detail.ModifyId)
				select detail).ToList<BudPreReimburseModifyDetail>();
			if (list != null && list.Count > 0)
			{
				foreach (BudPreReimburseModifyDetail current in list)
				{
					this.budModifyDetailSer.Delete(current);
				}
			}
		}
	}
	protected void SaveDiary()
	{
		this.ViewState["ApplyId"] = this.hfldModifyId.Value;
		if (string.IsNullOrEmpty(this.id))
		{
			BudPreReimburseModify budPreReimburseModify = new BudPreReimburseModify();
			budPreReimburseModify.Id = this.ViewState["guid"].ToString();
			budPreReimburseModify.PrjId = base.Request["prjId"];
			budPreReimburseModify.Name = this.txtName.Text.Trim();
			budPreReimburseModify.ApplyDate = Convert.ToDateTime(this.txtInputDate.Text);
			budPreReimburseModify.RptUser = this.txtInputUser.Text.Trim();
			budPreReimburseModify.Code = this.txtIndireCode.Text.Trim();
			budPreReimburseModify.CostType = this.costType;
			budPreReimburseModify.FlowState = -1;
			budPreReimburseModify.InputDate = DateTime.Now;
			budPreReimburseModify.ApplyId = this.hfldModifyId.Value;
			this.budModifySer.Add(budPreReimburseModify);
			return;
		}
		BudPreReimburseModify byId = this.budModifySer.GetById(this.id);
		if (byId != null)
		{
			byId.PrjId = base.Request["prjId"];
			byId.Name = this.txtName.Text.Trim();
			byId.ApplyDate = Convert.ToDateTime(this.txtInputDate.Text);
			byId.RptUser = this.txtInputUser.Text.Trim();
			byId.Code = this.txtIndireCode.Text.Trim();
			if (byId.ApplyId != null)
			{
				byId.ApplyId = this.hfldModifyId.Value;
			}
			this.budModifySer.Update(byId);
		}
	}
	protected void SaveDiaryDetails()
	{
		this.SaveEditData();
		List<BudPreReimburseModifyDetail> list = this.ViewState["details"] as List<BudPreReimburseModifyDetail>;
		this.ClearModifyDetails(this.ViewState["guid"].ToString());
		if (list != null)
		{
			foreach (BudPreReimburseModifyDetail current in list)
			{
				this.budModifyDetailSer.Add(current);
			}
		}
	}
	protected void SaveEditData()
	{
		if (this.gvDiaryDetails.EditIndex != -1)
		{
			int editIndex = this.gvDiaryDetails.EditIndex;
			GridViewRow gridViewRow = this.gvDiaryDetails.Rows[editIndex];
			List<BudPreReimburseModifyDetail> list = this.ViewState["details"] as List<BudPreReimburseModifyDetail>;
			BudPreReimburseModifyDetail budPreReimburseModifyDetail = list[gridViewRow.RowIndex];
			BudPreReimburseModify budPreReimburseModify = new BudPreReimburseModify();
			this.costType = budPreReimburseModify.CostType;
			TextBox textBox = gridViewRow.Cells[2].FindControl("txtgvName") as TextBox;
			if (textBox != null)
			{
				budPreReimburseModifyDetail.Name = textBox.Text.Trim();
			}
			DropDownList dropDownList = gridViewRow.Cells[3].FindControl("ddlCBSCode") as DropDownList;
			if (dropDownList != null)
			{
				budPreReimburseModifyDetail.CBSCode = dropDownList.SelectedValue;
			}
			TextBox textBox2 = gridViewRow.Cells[4].FindControl("txtBeginAmount") as TextBox;
			if (textBox2 != null)
			{
				budPreReimburseModifyDetail.BeginCost = decimal.Parse(textBox2.Text.Trim());
			}
			TextBox textBox3 = gridViewRow.Cells[5].FindControl("txtAfterAmount") as TextBox;
			if (textBox3 != null)
			{
				budPreReimburseModifyDetail.AfterCost = decimal.Parse(textBox3.Text.Trim());
			}
			TextBox textBox4 = gridViewRow.Cells[6].FindControl("txtModifyReason") as TextBox;
			if (textBox4 != null)
			{
				budPreReimburseModifyDetail.ModifyReason = textBox4.Text.Trim();
			}
			TextBox textBox5 = gridViewRow.Cells[7].FindControl("txtgvNote") as TextBox;
			if (textBox5 != null)
			{
				budPreReimburseModifyDetail.Note = textBox5.Text.Trim();
			}
			this.ViewState["details"] = list;
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
	protected void gvDiaryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvDiaryDetails.DataKeys[e.Row.RowIndex]["Id"].ToString();
			e.Row.Attributes["Id"] = this.gvDiaryDetails.DataKeys[e.Row.RowIndex]["Id"].ToString();
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.AddDiaryDetails();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.gvDiaryDetails.Style.Add("min-width", "1024px");
		this.SaveEditData();
		this.gvDiaryDetails.EditIndex = int.Parse(this.hfldSingleId.Value) - 1;
		this.BindDetails();
	}
}


