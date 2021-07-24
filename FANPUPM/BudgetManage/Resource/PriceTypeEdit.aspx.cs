using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Resource_PriceTypeEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string priceTypeId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["PriceTypeId"]))
		{
			if (base.Request["PriceTypeId"].Contains("["))
			{
				this.priceTypeId = JsonHelper.GetListFromJson(base.Request["PriceTypeId"])[0];
			}
			else
			{
				this.priceTypeId = base.Request["PriceTypeId"];
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && (this.action == "Update" || this.action == "Query"))
		{
			this.InitUpdate();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (string.IsNullOrEmpty(this.action))
		{
			this.AddPriceType();
		}
		if (this.action == "Update")
		{
			this.UpdatePriceType();
		}
	}
	private void InitUpdate()
	{
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			Res_PriceType res_PriceType = (
				from pt in pm2Entities.Res_PriceType
				where pt.PriceTypeId == this.priceTypeId
				select pt).FirstOrDefault<Res_PriceType>();
			if (res_PriceType != null)
			{
				this.txtPriceTypeName.Text = res_PriceType.PriceTypeName;
				this.txtNote.Text = res_PriceType.Note;
			}
		}
	}
	private void AddPriceType()
	{
		try
		{
			if (!this.ValidateInsertPriceType())
			{
				base.RegisterScript("top.ui.alert('此价格类型已经存在!');");
			}
			else
			{
				using (pm2Entities pm2Entities = new pm2Entities())
				{
					Res_PriceType res_PriceType = new Res_PriceType();
					res_PriceType.PriceTypeId = System.Guid.NewGuid().ToString();
					res_PriceType.PriceTypeCode = System.DateTime.Now.ToString("yyyyMMddHHmmss");
					res_PriceType.PriceTypeName = this.txtPriceTypeName.Text.Trim();
					res_PriceType.Note = this.txtNote.Text.Trim();
					System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
					list.Add(base.UserCode);
					if (base.UserCode != "00000000")
					{
						list.Add("00000000");
					}
					res_PriceType.UserCodes = JsonHelper.Serialize(list.ToArray());
					pm2Entities.AddToRes_PriceType(res_PriceType);
					pm2Entities.SaveChanges();
				}
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append("top.ui.show( '添加成功'); \n");
				stringBuilder.Append("top.ui.closeWin(); \n");
				stringBuilder.Append("top.ui.reloadTab(); \n");
				base.RegisterScript(stringBuilder.ToString());
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('添加失败')");
		}
	}
	private void UpdatePriceType()
	{
		try
		{
			if (!this.ValidateUpdatePriceType())
			{
				base.RegisterScript("top.ui.alert('此价格类型已经存在!');");
			}
			else
			{
				using (pm2Entities pm2Entities = new pm2Entities())
				{
					Res_PriceType res_PriceType = (
						from pt in pm2Entities.Res_PriceType
						where pt.PriceTypeId == this.priceTypeId
						select pt).FirstOrDefault<Res_PriceType>();
					if (res_PriceType != null)
					{
						res_PriceType.PriceTypeName = this.txtPriceTypeName.Text.Trim();
						res_PriceType.Note = this.txtNote.Text.Trim();
						pm2Entities.SaveChanges();
					}
				}
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append("top.ui.show( '修改成功'); \n");
				stringBuilder.Append("top.ui.closeWin(); \n");
				stringBuilder.Append("top.ui.reloadTab(); \n");
				base.RegisterScript(stringBuilder.ToString());
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('修改失败')");
		}
	}
	private bool ValidateInsertPriceType()
	{
		string priceTypeName = this.txtPriceTypeName.Text.Trim();
		bool result;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			int num = (
				from pt in pm2Entities.Res_PriceType
				where pt.PriceTypeName == priceTypeName
				select pt).Count<Res_PriceType>();
			if (num > 0)
			{
				result = false;
			}
			else
			{
				result = true;
			}
		}
		return result;
	}
	private bool ValidateUpdatePriceType()
	{
		string priceTypeName = this.txtPriceTypeName.Text.Trim();
		bool result;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			int num = (
				from pt in pm2Entities.Res_PriceType
				where pt.PriceTypeId != this.priceTypeId && pt.PriceTypeName == priceTypeName
				select pt).Count<Res_PriceType>();
			if (num > 0)
			{
				result = false;
			}
			else
			{
				result = true;
			}
		}
		return result;
	}
}


