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
public partial class BudgetManage_Template_TemplateTypeEdit : NBasePage, IRequiresSessionState
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
		if (!(this.action == "update"))
		{
			this.lblTitle.Text = "新增模板类型";
			this.hdGuid.Value = System.Guid.NewGuid().ToString();
			return;
		}
		this.lblTitle.Text = "编辑模板类型";
		string id = base.Request["id"];
		BudTemplateType byId = BudTemplateType.GetById(id);
		if (byId != null)
		{
			this.txtTypeName.Text = byId.Name;
			return;
		}
		base.RegisterScript("alert('系统提示：\\n\\n此模板类型不存在!');winclose('TemplateTypeEdit', 'TemplateTypeList.aspx', true)");
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.IList<BudTemplateType> byName = BudTemplateType.GetByName(this.txtTypeName.Text.Trim());
		if (this.action == "update")
		{
			BudTemplateType byId = BudTemplateType.GetById(base.Request["id"]);
			if (byId.Name != this.txtTypeName.Text && byName.Count > 0)
			{
				base.RegisterScript("top.ui.alert('此名称已存在,请重新定义')");
				return;
			}
			try
			{
				BudTemplateType.Update(this.UpdateModel(byId));
				base.RegisterScript("top.ui.show('" + this.SetMsg() + "成功');");
			}
			catch
			{
				base.RegisterScript("top.ui.alert('" + this.SetMsg() + "失败!\\n此模板类型不存在!');");
			}
			base.RegisterScript("winclose('TemplateTypeEdit', 'TemplateTypeList.aspx', true);");
			return;
		}
		else
		{
			if (byName.Count > 0)
			{
				base.RegisterScript("top.ui.alert('此名称已存在,请重新定义')");
				return;
			}
			BudTemplateType.Add(this.AddModel());
			base.RegisterScript("top.ui.show('" + this.SetMsg() + "成功！');winclose('TemplateTypeEdit', 'TemplateTypeList.aspx', true)");
			return;
		}
	}
	public BudTemplateType AddModel()
	{
		return BudTemplateType.Create(this.hdGuid.Value, System.DateTime.Now.ToString("yyyyMMddHHmmsss"), this.txtTypeName.Text, PageHelper.QueryUser(this, base.UserCode), new System.DateTime?(System.DateTime.Now));
	}
	public BudTemplateType UpdateModel(BudTemplateType model)
	{
		model.Name = this.txtTypeName.Text;
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


