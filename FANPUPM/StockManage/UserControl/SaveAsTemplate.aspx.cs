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
public partial class StockManage_UserControl_SaveAsTemplate : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.IList<BudTemplate> byName = BudTemplate.GetByName(this.txtTypeName.Text.Trim());
		if (byName.Count > 0)
		{
			base.RegisterScript("alert('系统提示：\\n\\n此名称已存在!!请重新定义!!')");
			return;
		}
		string empty = string.Empty;
		this.AddTemplate(ref empty);
		BudTemplate.GetById(empty);
		string text = (this.Session["taskIds"] == null) ? "" : this.Session["taskIds"].ToString();
		if (!string.IsNullOrEmpty(text))
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			if (text.Contains("["))
			{
				list = JsonHelper.GetListFromJson(text);
			}
			else
			{
				list.Add(text);
			}
			string inputUser = PageHelper.QueryUser(this, base.UserCode);
			BudTemplateItem.SaveTemplate(list, empty, "", inputUser);
			base.RegisterScript("alert('系统提示：\\n\\n保存成功!!');");
		}
	}
	public void AddTemplate(ref string typeId)
	{
		string text = base.Request["typeId"];
		if (!string.IsNullOrEmpty(text))
		{
			typeId = System.Guid.NewGuid().ToString();
			BudTemplateType byId = BudTemplateType.GetById(text);
			BudTemplate budTemplate = BudTemplate.Create(typeId, System.DateTime.Now.ToString("yyyyMMddHHmmsss"), this.txtTypeName.Text.Trim(), PageHelper.QueryUser(this, base.UserCode), new System.DateTime?(System.DateTime.Now), byId);
			BudTemplate.Add(budTemplate);
		}
	}
}


