using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.resourceBLL;
using System;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Resource_TypeAttributeEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string resourceTypeId = string.Empty;
	private string attributeId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["ResourceTypeId"]))
		{
			this.resourceTypeId = base.Request["ResourceTypeId"];
		}
		if (!string.IsNullOrEmpty(base.Request["AttributeId"]))
		{
			this.attributeId = base.Request["AttributeId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && this.action == "Update")
		{
			this.InitUpdate();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (this.action == "Add")
		{
			this.AddAttribute();
			return;
		}
		if (this.action == "Update")
		{
			this.UpdateAttribute();
		}
	}
	private void InitUpdate()
	{
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			Res_Attribute res_Attribute = (
				from a in pm2Entities.Res_Attribute
				where a.AttributeId == this.attributeId
				select a).FirstOrDefault<Res_Attribute>();
			if (res_Attribute != null)
			{
				this.txtAttributeName.Text = res_Attribute.AttributeName;
			}
		}
	}
	private void AddAttribute()
	{
		if (this.IsExists())
		{
			base.RegisterScript("top.ui.alert('此名称已经存在')");
			return;
		}
		try
		{
			using (pm2Entities pm2Entities = new pm2Entities())
			{
				Res_ResourceType res_ResourceType = (
					from rt in pm2Entities.Res_ResourceType
					where rt.ResourceTypeId == this.resourceTypeId
					select rt).FirstOrDefault<Res_ResourceType>();
				Res_Attribute res_Attribute = new Res_Attribute();
				res_Attribute.AttributeId = System.Guid.NewGuid().ToString();
				res_Attribute.AttributeName = this.txtAttributeName.Text.Trim();
				if (res_ResourceType != null)
				{
					res_ResourceType.Res_Attribute.Add(res_Attribute);
				}
				pm2Entities.SaveChanges();
			}
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("top.ui.closeWin(); \n");
			stringBuilder.Append("top.ui.reloadTab(); \n");
			stringBuilder.Append("top.ui.alert('添加成功'); \n");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch
		{
			base.RegisterScript("top.ui.alert('添加失败')");
		}
	}
	private void UpdateAttribute()
	{
		if (this.IsExists())
		{
			base.RegisterScript("top.ui.alert('此名称已经存在')");
			return;
		}
		try
		{
			using (pm2Entities pm2Entities = new pm2Entities())
			{
				Res_Attribute res_Attribute = (
					from a in pm2Entities.Res_Attribute
					where a.AttributeId == this.attributeId
					select a).FirstOrDefault<Res_Attribute>();
				if (res_Attribute != null)
				{
					res_Attribute.AttributeName = this.txtAttributeName.Text.Trim();
				}
				pm2Entities.SaveChanges();
			}
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("top.ui.closeWin(); \n");
			stringBuilder.Append("top.ui.reloadTab(); \n");
			stringBuilder.Append("top.ui.alert('修改成功'); \n");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch
		{
			base.RegisterScript("top.ui.alert('修改失败')");
		}
	}
	private bool IsExists()
	{
		TypeAttribute typeAttribute = new TypeAttribute(this.resourceTypeId);
		return typeAttribute.IsExists(this.txtAttributeName.Text.Trim(), this.attributeId);
	}
}


