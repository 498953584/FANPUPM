using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using com.jwsoft.pm.entpm;
using System;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Resource_ResourceTypeEdit : NBasePage, IRequiresSessionState
{
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindCBS();
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			if (!string.IsNullOrEmpty(System.Convert.ToString(base.Request.QueryString["parentId"])))
			{
				string parentId = base.Request["parentId"];
				Res_ResourceType res_ResourceType = (
					from m in pm2Entities.Res_ResourceType
					where m.ResourceTypeId == parentId
					select m).First<Res_ResourceType>();
				this.txtResourceCode.Text = res_ResourceType.ResourceTypeCode;
				this.hfldParentId.Value = System.Convert.ToString(base.Request.QueryString["parentId"]);
			}
			else
			{
				this.hfldParentId.Value = "";
			}
		}
		if (base.Request.QueryString["id"] != null)
		{
			using (pm2Entities pm2Entities2 = new pm2Entities())
			{
				string id = base.Request["id"];
				string pid = base.Request["parentId"];
				Res_ResourceType res_ResourceType2 = (
					from m in pm2Entities2.Res_ResourceType
					where m.ResourceTypeId == id
					select m).First<Res_ResourceType>();
				this.txtResourceName.Text = res_ResourceType2.ResourceTypeName;
				if (!string.IsNullOrEmpty(pid))
				{
					string resourceTypeCode = (
						from m in pm2Entities2.Res_ResourceType
						where m.ResourceTypeId == pid
						select m).First<Res_ResourceType>().ResourceTypeCode;
					this.txtResourceCode.Text = res_ResourceType2.ResourceTypeCode.Substring(0, resourceTypeCode.Length);
					this.txtCode.Text = res_ResourceType2.ResourceTypeCode.Substring(resourceTypeCode.Length, res_ResourceType2.ResourceTypeCode.Length - resourceTypeCode.Length);
				}
				else
				{
					this.txtCode.Text = res_ResourceType2.ResourceTypeCode;
				}
				this.ddlCBS.SelectedValue = res_ResourceType2.CBSCode;
				return;
			}
		}
		this.hdGuid.Value = System.Guid.NewGuid().ToString();
	}
	protected void BindCBS()
	{
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IOrderedQueryable<Bud_CostAccounting> orderedQueryable =
				from m in pm2Entities.Bud_CostAccounting
				where m.Type == "D" && m.CBSCode.Length == 9
				orderby m.CBSCode
				select m;
			if (orderedQueryable != null)
			{
				this.ddlCBS.DataSource = orderedQueryable;
				this.ddlCBS.DataTextField = "Name";
				this.ddlCBS.DataValueField = "CBSCode";
				this.ddlCBS.DataBind();
				this.ddlCBS.Items.Insert(0, new ListItem("--请选择直接成本--", ""));
			}
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (!this.Page.IsValid)
		{
			return;
		}
		if (this.txtResourceCode.Text.Trim().Length > 20 || this.txtResourceName.Text.Trim().Length > 20)
		{
			base.RegisterScript("top.ui.alert('系统提示', '请输入少于20的字符个数，空格包含在内!!')");
			return;
		}
		int num = 0;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IQueryable<Res_ResourceType> source =
				from m in pm2Entities.Res_ResourceType
				where m.ResourceTypeCode == this.txtResourceCode.Text.Trim() + this.txtCode.Text.Trim()
				select m;
			IQueryable<Res_ResourceType> source2 =
				from m in pm2Entities.Res_ResourceType
				where m.ResourceTypeName == this.txtResourceName.Text.Trim()
				select m;
			if (base.Request["id"] != null)
			{
				string id = base.Request["id"];
				Res_ResourceType res_ResourceType = (
					from m in pm2Entities.Res_ResourceType
					where m.ResourceTypeId == id
					select m).First<Res_ResourceType>();
				if (res_ResourceType.ResourceTypeCode != this.txtResourceCode.Text.Trim() + this.txtCode.Text.Trim() && source.Count<Res_ResourceType>() > 0)
				{
					base.RegisterScript("top.ui.alert('系统提示:类型编码已存在!!请重新定义!!')");
					return;
				}
				if (res_ResourceType.ResourceTypeName != this.txtResourceName.Text.Trim() && source2.Count<Res_ResourceType>() > 0)
				{
					base.RegisterScript("top.ui.alert('系统提示:类型名称已存在!!请重新定义!!')");
					return;
				}
				this.UpdateModel(res_ResourceType);
				num = pm2Entities.SaveChanges();
			}
			else
			{
				if (source.Count<Res_ResourceType>() > 0)
				{
					base.RegisterScript("top.ui.alert('系统提示:类型名称已存在!!请重新定义!!')");
					return;
				}
				if (source2.Count<Res_ResourceType>() > 0)
				{
					base.RegisterScript("top.ui.alert('系统提示:类型名称已存在!!请重新定义!!')");
					return;
				}
				pm2Entities.AddToRes_ResourceType(this.AddModel(pm2Entities));
				num = pm2Entities.SaveChanges();
			}
		}
		if (num > 0)
		{
			string text = this.SetMsg() + "成功！";
			string text2 = "/BudgetManage/Resource/ResourceTypeList.aspx?id=" + base.Request["parentId"];
			string message = string.Concat(new string[]
			{
				"top.ui.closeWin(); \ntop.ui.show( '",
				text,
				"'); \ntop.ui.reloadTab({url: '",
				text2,
				"'}); \n"
			});
			base.RegisterScript(message);
			return;
		}
		base.RegisterScript("top.ui.alert('系统提示', '" + this.SetMsg() + "失败！');");
	}
	public Res_ResourceType AddModel(pm2Entities pm2)
	{
		Res_ResourceType res_ResourceType = new Res_ResourceType();
		res_ResourceType.ResourceTypeId = this.hdGuid.Value;
		res_ResourceType.IsValid = new bool?(true);
		res_ResourceType.ResourceTypeCode = this.txtResourceCode.Text.Trim() + this.txtCode.Text.Trim();
		res_ResourceType.ResourceTypeName = this.txtResourceName.Text.Trim();
		res_ResourceType.InputDate = new System.DateTime?(System.DateTime.Now);
		res_ResourceType.InputUser = PageHelper.QueryUser(this, base.UserCode);
		if (!string.IsNullOrEmpty(this.ddlCBS.SelectedValue.Trim()))
		{
			res_ResourceType.CBSCode = this.ddlCBS.SelectedValue.Trim();
		}
		else
		{
			res_ResourceType.CBSCode = null;
		}
		string parentId = base.Request["parentId"];
		if (!string.IsNullOrEmpty(parentId))
		{
			Res_ResourceType res_ResourceType2 = (
				from m in pm2.Res_ResourceType
				where m.ResourceTypeId == parentId
				select m).First<Res_ResourceType>();
			res_ResourceType2.Res_ResourceType1.Add(res_ResourceType);
		}
		return res_ResourceType;
	}
	public Res_ResourceType UpdateModel(Res_ResourceType model)
	{
		model.ResourceTypeCode = this.txtResourceCode.Text.Trim() + this.txtCode.Text.Trim();
		model.ResourceTypeName = this.txtResourceName.Text.Trim();
		if (!string.IsNullOrEmpty(this.ddlCBS.SelectedValue.Trim()))
		{
			model.CBSCode = this.ddlCBS.SelectedValue.Trim();
		}
		else
		{
			model.CBSCode = null;
		}
		return model;
	}
	public string SetMsg()
	{
		if (base.Request.QueryString["id"] != null)
		{
			return "更新";
		}
		return "添加";
	}
}


