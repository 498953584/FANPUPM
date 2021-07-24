using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Template_TemplateEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (base.Request.QueryString["id"] == null)
		{
			this.action = "add";
		}
		else
		{
			this.action = "update";
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.hlfdtempTypeId.Value = base.Request.QueryString["tempTypeId"].ToString();
		if (!(this.action == "update"))
		{
			this.lblTitle.Text = "新增模板";
			this.hdGuid.Value = System.Guid.NewGuid().ToString();
			return;
		}
		this.lblTitle.Text = "编辑模板";
		string id = base.Request["id"];
		BudTemplate byId = BudTemplate.GetById(id);
		if (byId != null)
		{
			this.txtTemplateName.Text = byId.Name;
			this.hlfdtempTypeId.Value = byId.BudTempType.Id;
			return;
		}
		base.RegisterScript("alert('系统提示：\\n\\n此模板不存在!');");
		base.RegisterScript("winclose('TemplateEdit', 'TemplateList.aspx?tempTypeId=" + this.hlfdtempTypeId.Value + "', true);");
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.IList<BudTemplate> byName = BudTemplate.GetByName(this.txtTemplateName.Text.Trim());
		if (this.action == "update")
		{
			BudTemplate byId = BudTemplate.GetById(base.Request["id"]);
			if (byId.Name != this.txtTemplateName.Text.Trim() && byName.Count > 0)
			{
				base.RegisterScript("top.ui.alert('此名称已存在,请重新定义');");
				return;
			}
			try
			{
				BudTemplate.Update(this.UpdateModel(byId));
			}
			catch
			{
				base.RegisterScript("top.ui.alert('" + this.SetMsg() + "失败,此模板不存在');");
			}
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_TemplateList' });");
			return;
		}
		else
		{
			if (byName.Count > 0)
			{
				base.RegisterScript("top.ui.alert('此名称已存在,请重新定义')");
				return;
			}
			BudTemplate.Add(this.AddModel());
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_TemplateList' });");
			return;
		}
	}
	public BudTemplate AddModel()
	{
		return BudTemplate.Create(this.hdGuid.Value, System.DateTime.Now.ToString("yyyyMMddHHmmsss"), this.txtTemplateName.Text.Trim(), PageHelper.QueryUser(this, base.UserCode), new System.DateTime?(System.DateTime.Now), BudTemplateType.GetById(this.hlfdtempTypeId.Value.Trim()));
	}
	public BudTemplate UpdateModel(BudTemplate model)
	{
		model.Name = this.txtTemplateName.Text.Trim();
		model.BudTempType = BudTemplateType.GetById(this.hlfdtempTypeId.Value.Trim());
		return model;
	}
	public string SetMsg()
	{
		if (this.action == "update")
		{
			return "更新";
		}
		return "添加";
	}
}


