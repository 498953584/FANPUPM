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
public partial class BudgetManage_Resource_UnitEdit : NBasePage, IRequiresSessionState
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
		if (base.Request.QueryString["id"] != null)
		{
			using (pm2Entities pm2Entities = new pm2Entities())
			{
				string id = base.Request["id"];
				Res_Unit res_Unit = (
					from m in pm2Entities.Res_Unit
					where m.UnitId == id
					select m).First<Res_Unit>();
				this.txtName.Text = res_Unit.Name;
				return;
			}
		}
		this.hdGuid.Value = System.Guid.NewGuid().ToString();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (!this.Page.IsValid)
		{
			return;
		}
		if (this.txtName.Text.Trim().Length > 30)
		{
			base.RegisterScript("top.ui.alert('请输入少于30的字符!!')");
			return;
		}
		int num = 0;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IQueryable<Res_Unit> source =
				from m in pm2Entities.Res_Unit
				where m.Name == this.txtName.Text.Trim()
				select m;
			if (base.Request["id"] != null)
			{
				string id = base.Request["id"];
				Res_Unit res_Unit = (
					from m in pm2Entities.Res_Unit
					where m.UnitId == id
					select m).First<Res_Unit>();
				if (res_Unit.Name != this.txtName.Text.Trim() && source.Count<Res_Unit>() > 0)
				{
					base.RegisterScript("top.ui.alert('此名称已存在!!请重新定义!!')");
					return;
				}
				this.UpdateModel(res_Unit);
				num = pm2Entities.SaveChanges();
			}
			else
			{
				if (source.Count<Res_Unit>() > 0)
				{
					base.RegisterScript("top.ui.alert('此名称已存在!!请重新定义!!')");
					return;
				}
				pm2Entities.AddToRes_Unit(this.AddModel());
				num = pm2Entities.SaveChanges();
			}
		}
		if (num > 0)
		{
			base.RegisterScript("top.ui.show( '" + this.SetMsg() + "成功！'); top.ui.closeWin(); top.ui.reloadTab();");
			return;
		}
		base.RegisterScript("top.ui.alert('" + this.SetMsg() + "失败！');");
	}
	public Res_Unit AddModel()
	{
		return new Res_Unit
		{
			UnitId = this.hdGuid.Value,
			Code = System.DateTime.Now.ToString("yyyyMMddHHmmsss"),
			Name = this.txtName.Text.Trim(),
			InputUser = PageHelper.QueryUser(this, base.UserCode),
			InputDate = new System.DateTime?(System.DateTime.Now)
		};
	}
	public Res_Unit UpdateModel(Res_Unit model)
	{
		model.Name = this.txtName.Text.Trim();
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


