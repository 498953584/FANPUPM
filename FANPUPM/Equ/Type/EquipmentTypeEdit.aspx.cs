using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_Type_EquipmentTypeEdit : NBasePage, IRequiresSessionState
{
	private EquEquipmentTypeService equTypeSer = new EquEquipmentTypeService();
	private string id = string.Empty;
	private string parentId = string.Empty;
	private string parentCode = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["parentId"]))
		{
			this.parentId = base.Request["parentId"];
			EquEquipmentType equEquipmentType = (
				from t in this.equTypeSer
				where t.Id == this.parentId
				select t).FirstOrDefault<EquEquipmentType>();
			this.parentCode = equEquipmentType.Code;
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"].ToString();
		}
		else
		{
			this.id = System.Guid.NewGuid().ToString();
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
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string str = string.Empty;
		try
		{
			str = string.Format("{0:D3}", System.Convert.ToInt32(this.txtEquipmentNo.Text.Trim()));
		}
		catch
		{
			base.RegisterScript("top.ui.alert('设备类别编号必须是整数!')");
			return;
		}
		try
		{
			string equCode = this.parentCode + str;
			string equName = this.txtEquipmentName.Text.Trim();
			System.Collections.Generic.List<EquEquipmentType> list = (
				from t in this.equTypeSer
				where t.Code == equCode
				select t).ToList<EquEquipmentType>();
			System.Collections.Generic.List<EquEquipmentType> list2 = (
				from t in this.equTypeSer
				where t.Name == equName
				select t).ToList<EquEquipmentType>();
			if (!string.IsNullOrEmpty(base.Request["id"]))
			{
				EquEquipmentType byId = this.equTypeSer.GetById(this.id);
				if (byId.Code != equCode && list.Count > 0)
				{
					base.RegisterScript("top.ui.show('设备类别编号已存在，请重新定义!')");
				}
				else
				{
					if (byId.Name != equName && list2.Count > 0)
					{
						base.RegisterScript("top.ui.show('设备类别名称已存在，请重新定义!')");
					}
					else
					{
						EquEquipmentType model = this.GetModel(byId);
						this.equTypeSer.Update(model);
						string str2 = "/Equ/Type/EquipmentTypeList.aspx?id=" + this.parentId;
						string message = "top.ui.closeWin(); \ntop.ui.show( '更新成功'); \ntop.ui.reloadTab({url: '" + str2 + "'}); \n";
						base.RegisterScript(message);
					}
				}
			}
			else
			{
				if (list.Count > 0)
				{
					base.RegisterScript("top.ui.show('设备类别编号已存在，请重新定义!')");
				}
				else
				{
					if (list2.Count > 0)
					{
						base.RegisterScript("top.ui.show('设备类别名称已存在，请重新定义!')");
					}
					else
					{
						EquEquipmentType model2 = this.GetModel(null);
						this.equTypeSer.Add(model2);
						string str3 = "/Equ/Type/EquipmentTypeList.aspx?id=" + base.Request["parentId"];
						string message2 = "top.ui.closeWin(); \ntop.ui.show( '添加成功'); \ntop.ui.reloadTab({url: '" + str3 + "'}); \n";
						base.RegisterScript(message2);
					}
				}
			}
		}
		catch
		{
			base.RegisterScript("top.ui.show('添加失败!')");
		}
	}
	private void InitPage()
	{
		if (!string.IsNullOrEmpty(this.id))
		{
			EquEquipmentType byId = this.equTypeSer.GetById(this.id);
			if (byId != null)
			{
				this.txtEquipmentName.Text = byId.Name;
				this.txtEquipmentNo.Text = byId.No.ToString();
			}
		}
	}
	private EquEquipmentType GetModel(EquEquipmentType type)
	{
		string text = string.Empty;
		text = string.Format("{0:D3}", System.Convert.ToInt32(this.txtEquipmentNo.Text.Trim()));
		string code = this.parentCode + text;
		string name = this.txtEquipmentName.Text.Trim();
		if (type == null)
		{
			type = new EquEquipmentType();
			type.Id = this.id;
			if (string.IsNullOrEmpty(this.parentId))
			{
				type.ParentId = null;
			}
			else
			{
				type.ParentId = this.parentId;
			}
		}
		type.Code = code;
		type.No = System.Convert.ToInt32(text);
		type.Name = name;
		return type;
	}
}


