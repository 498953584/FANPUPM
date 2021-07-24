using ASP;
using cn.justwin.BLL;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TenderManage_PrjTypeEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private int itemCode;
	private string typeCode = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (base.Request.QueryString["itemCode"] == null)
		{
			this.action = "add";
		}
		else
		{
			this.action = "update";
			this.itemCode = System.Convert.ToInt32(base.Request.QueryString["itemCode"].ToString());
		}
		if (base.Request.QueryString["prjType"] != null)
		{
			this.typeCode = base.Request.QueryString["prjType"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (this.action == "add")
			{
				this.hlfdItemCode.Value = PrjType.GetNewItemCode(this.typeCode).ToString();
				return;
			}
			PrjType byItemCode = PrjType.GetByItemCode(this.typeCode, this.itemCode);
			this.hlfdItemCode.Value = byItemCode.ItemCode.ToString();
			this.txtPrjTypeName.Text = byItemCode.ItemName;
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (this.txtPrjTypeName.Text.Trim() == "")
		{
			base.RegisterScript("alert('系统提示：\\n\\n工程类型名称必须输入!');");
			return;
		}
		PrjType prjType = PrjType.Create(this.typeCode, System.Convert.ToInt32(this.hlfdItemCode.Value), this.txtPrjTypeName.Text.Trim());
		System.Collections.Generic.List<PrjType> byItemName = PrjType.GetByItemName(this.typeCode, this.txtPrjTypeName.Text.Trim());
		if (this.action == "add")
		{
			if (byItemName.Count > 0)
			{
				base.RegisterScript("alert('系统提示：\\n\\n此名称已存在!请重新定义!');");
				return;
			}
			PrjType.Add(prjType);
			base.RegisterScript("alert('系统提示：\\n\\n添加成功!');");
		}
		else
		{
			PrjType byItemCode = PrjType.GetByItemCode(this.typeCode, System.Convert.ToInt32(this.hlfdItemCode.Value));
			if (byItemCode.ItemName != this.txtPrjTypeName.Text.Trim() && byItemName.Count > 0)
			{
				base.RegisterScript("alert('系统提示：\\n\\n此名称已存在!请重新定义!');");
				return;
			}
			PrjType.Update(prjType);
			base.RegisterScript("alert('系统提示：\\n\\n修改成功!');");
		}
		base.RegisterScript("winclose('PrjTypeEdit', 'PrjTypeList.aspx?tv=" + this.typeCode + "', true);");
	}
}


