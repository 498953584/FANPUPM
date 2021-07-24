using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_BudTaskEdit : NBasePage, IRequiresSessionState
{
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.BindDropTaskType();
			this.BindHtmlData();
		}
		if (base.Request["page"].ToString() == "BudTemplateList.aspx")
		{
			base.RegisterScript("if($('#trDate')!=null){$('#trDate').remove();$('#tdLblConsPeriod').remove();$('#tdTxtConsPeriod').remove();$('#tdUnit').attr('colspan','3');$('#txtUnit').width('95%');}");
			base.RegisterScript("if ($('#trCompute') != null) { $('#txtUnitPrice').removeClass(); $('#txtTotal').removeClass(); \r\n                            $('#trCompute').remove(); }$('#txtQuantity').removeAttr('onkeyup');");
		}
		if (base.Request["tempType"] != null)
		{
			this.hfldTempId.Value = base.Request["tempType"].ToString();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string value = this.txtQuantity.Text.Trim();
		string text = string.Empty;
		string text2 = string.Empty;
		if (string.IsNullOrEmpty(value))
		{
			base.RegisterScript("alert('工程量必须输入');");
			this.txtQuantity.Focus();
			return;
		}
		if (this.hfldIsWBSRelevance.Value == "0" && base.Request["page"].ToString() != "BudTemplateList.aspx" && string.IsNullOrEmpty(this.txtUnitPrice.Text.Trim()))
		{
			base.RegisterScript("alert('综合单价必须输入');");
			this.txtUnitPrice.Focus();
			return;
		}
		if (this.hfldIsWBSRelevance.Value == "0" && base.Request["page"].ToString() != "BudTemplateList.aspx")
		{
			text = System.Convert.ToDecimal(this.txtUnitPrice.Text.Trim()).ToString();
			text2 = System.Convert.ToDecimal(this.txtTotal.Value.Trim()).ToString();
		}
		string text3 = this.txtTaskCode.Text.Trim();
		bool flag = false;
		string text4 = base.Request["type"];
		string arg_14E_0 = base.Request["isBudTask"];
		if (this.ViewState["oldCode"] != null)
		{
			this.ViewState["oldCode"].ToString();
		}
		else
		{
			string arg_17E_0 = string.Empty;
		}
		if (flag)
		{
			base.RegisterScript("top.ui.alert('清单编码不能重复');");
			this.txtTaskCode.Focus();
			return;
		}
		string text5 = System.Guid.NewGuid().ToString();
		string text6 = this.txtTaskName.Text.Trim();
		string text7 = this.txtStartDate.Text;
		string text8 = this.txtEndDate.Text;
		string text9 = this.txtUnit.Text.Trim();
		string arg_1F9_0 = this.ddlTaskType.SelectedValue;
		string text10 = this.txtNote.Text.Trim();
		string text11 = this.txtConstructionPeriod.Text.Trim();
		string text12 = this.txtDescription.Text.Trim();
		string a = base.Request["page"];
		if (a == "BudgetPlaitList.aspx")
		{
			BudTaskService budTaskService = new BudTaskService();
			string text13 = string.Empty;
			text13 = base.Request["prjId"];
			string text14 = base.Request["id"];
			cn.justwin.Domain.Entities.BudTask budTask;
			if (text4.ToLower() == "edit")
			{
				budTask = budTaskService.GetById(text14);
			}
			else
			{
				budTask = new cn.justwin.Domain.Entities.BudTask();
				budTask.TaskId = text5;
				budTask.OrderNumber = cn.justwin.Domain.BudTask.GetOrderNumber(text13, text14);
				if (string.IsNullOrEmpty(text14))
				{
					budTask.ParentId = null;
				}
				else
				{
					budTask.ParentId = text14;
				}
				budTask.PrjId = text13;
				budTask.InputUser = base.UserCode;
				budTask.InputDate = System.DateTime.Now;
				budTask.IsValid = new bool?(true);
				budTask.Version = new int?(1);
				budTask.Modified = null;
			}
			budTask.TaskCode = text3;
			budTask.TaskName = text6;
			System.DateTime? startDate = null;
			if (!string.IsNullOrEmpty(text7))
			{
				startDate = new System.DateTime?(System.Convert.ToDateTime(text7));
			}
			budTask.StartDate = startDate;
			System.DateTime? endDate = null;
			if (!string.IsNullOrEmpty(text8))
			{
				endDate = new System.DateTime?(System.Convert.ToDateTime(text8));
			}
			budTask.EndDate = endDate;
			if (!string.IsNullOrEmpty(value))
			{
				budTask.Quantity = new decimal?(System.Convert.ToDecimal(value));
			}
			if (!string.IsNullOrEmpty(text))
			{
				budTask.UnitPrice = new decimal?(System.Convert.ToDecimal(text));
			}
			if (!string.IsNullOrEmpty(text2))
			{
				budTask.Total = new decimal?(System.Convert.ToDecimal(text2));
			}
			budTask.Unit = text9;
			budTask.Note = text10;
			if (!string.IsNullOrEmpty(text11))
			{
				budTask.ConstructionPeriod = new int?(System.Convert.ToInt32(text11));
			}
			budTask.TaskType = "";
			budTask.FeatureDescription = this.txtDescription.Text.Trim();
			if (text4.ToLower() == "edit")
			{
				budTaskService.Update(budTask);
			}
			else
			{
				budTaskService.Add(budTask);
			}
			string str = "resetData();";
			string str2 = "top.ui.winSuccess({ parentName: '_BudTaskEdit' });";
			base.RegisterScript(str + str2);
		}
		if (a == "BudTemplateList.aspx")
		{
			string message = string.Concat(new string[]
			{
				"closeDial('",
				text3,
				"','",
				text6,
				"','",
				text9,
				"','",
				System.Convert.ToDecimal(value).ToString(),
				"','",
				text10,
				"','",
				this.ddlTaskType.SelectedItem.Text,
				"','",
				text7,
				"','",
				text8,
				"','",
				text,
				"','",
				text2,
				"','",
				text11,
				"','",
				text5,
				"','",
				text12,
				"');"
			});
			string str3 = "/BudgetManage/Template/BudTemplateList.aspx?tempId=" + this.hfldTempId.Value;
			//"top.ui.winSuccess({ parentName: '_BudTaskEdit',url:" + str3 + " });";
			base.RegisterScript(message);
		}
	}
	public void BindDropTaskType()
	{
		System.Collections.Generic.IList<BudTaskType> all = BudTaskType.GetAll();
		this.ddlTaskType.DataSource = all;
		this.ddlTaskType.DataTextField = "Name";
		this.ddlTaskType.DataValueField = "Layer";
		this.ddlTaskType.DataBind();
		ListItem item = new ListItem("", "");
		this.ddlTaskType.Items.Add(item);
	}
	protected void BindHtmlData()
	{
		string text = base.Request["type"];
		string text2 = base.Request["id"];
		if (text.ToLower() == "edit")
		{
			string a = base.Request["page"];
			if (a == "BudTemplateList.aspx")
			{
				BudTemplateItem byId = BudTemplateItem.GetById(text2, base.Request["tempType"]);
				if (byId != null)
				{
					this.txtTaskCode.Text = byId.Code;
					this.ViewState["oldCode"] = byId.Code;
					this.txtTaskName.Text = byId.Name;
					this.txtUnit.Text = byId.Unit;
					this.txtQuantity.Text = byId.Quantity.ToString();
					this.ddlTaskType.SelectedValue = byId.Layer.ToString();
					this.txtNote.Text = byId.Note;
					this.txtDescription.Text = byId.FeatureDescription;
				}
			}
			else
			{
				if (a == "BudgetPlaitList.aspx" || a == "BudgetChange.aspx")
				{
					cn.justwin.Domain.Entities.BudTask budTask = null;
					BudTaskService budTaskService = new BudTaskService();
					string arg_145_0 = string.Empty;
					if (text.ToLower() == "edit")
					{
						budTask = budTaskService.GetById(text2);
					}
					if (budTask != null)
					{
						this.txtTaskCode.Text = budTask.TaskCode;
						this.ViewState["oldCode"] = budTask.TaskCode;
						this.txtTaskName.Text = budTask.TaskName;
						this.txtStartDate.Text = ((!budTask.StartDate.HasValue) ? string.Empty : budTask.StartDate.Value.ToString("yyyy-M-d"));
						this.txtEndDate.Text = ((!budTask.EndDate.HasValue) ? string.Empty : budTask.EndDate.Value.ToString("yyyy-M-d"));
						if (budTask.ConstructionPeriod.HasValue)
						{
							this.txtConstructionPeriod.Text = budTask.ConstructionPeriod.Value.ToString();
						}
						this.txtUnit.Text = budTask.Unit;
						this.txtQuantity.Text = budTask.Quantity.ToString();
						this.txtUnitPrice.Text = ((!budTask.UnitPrice.HasValue) ? 0m.ToString("0.00") : budTask.UnitPrice.Value.ToString("0.00"));
						this.txtTotal.Value = ((!budTask.Total.HasValue) ? 0m.ToString("0.00") : budTask.Total.Value.ToString("0.00"));
						this.ddlTaskType.SelectedValue = (budTask.OrderNumber.Length / 3).ToString();
						this.txtNote.Text = budTask.Note;
						this.txtDescription.Text = budTask.FeatureDescription;
					}
				}
			}
			if (a == "BudgetChange.aspx")
			{
				this.hfldState.Value = "edit";
				return;
			}
		}
		else
		{
			string text3 = base.Request["layer"];
			if (text3 == "")
			{
				this.ddlTaskType.SelectedValue = "1";
				return;
			}
			if (text3 == "0")
			{
				this.ddlTaskType.SelectedValue = string.Empty;
				return;
			}
			this.ddlTaskType.SelectedValue = text3;
		}
	}
}


