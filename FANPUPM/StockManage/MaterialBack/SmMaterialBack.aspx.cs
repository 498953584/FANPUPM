using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_basicset_SmMaterialBack : NBasePage, IRequiresSessionState
{
	private MaterialBackModel model = new MaterialBackModel();
	private MaterialBack back = new MaterialBack();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.txtCode.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
			this.txtProName.Value = base.Request.QueryString["pname"];
			this.hdnProCode.Value = base.Request.QueryString["pcode"];
			this.FileLink1.MID = 1081;
			this.FileLink1.FID = this.txtCode.Value;
			this.FileLink1.Type = 1;
		}
	}
	protected void btnSave_ServerClick(object sender, EventArgs e)
	{
		string a = base.Request.QueryString["optype"];
		if (a == "add")
		{
			this.setModelValue();
			int num = this.back.add(this.model);
			if (num > 0)
			{
				base.RegisterScript("alert('系统提示：\\n\\n添加成功');window.opener.location.href=window.opener.location.href;window.close();");
				return;
			}
			base.RegisterScript("alert('系统提示：\\n\\n添加失败');window.opener.location.href=window.opener.location.href;window.close();");
			return;
		}
		else
		{
			this.model.Rcode = this.txtCode.Value;
			this.model.Procode = this.hdnProCode.Value;
			this.model.Person = this.txtPeople.Value;
			this.model.InTime = Convert.ToDateTime(this.dtxtApplyDate.Text);
			this.model.Explain = this.txtRemark.Value;
			int num2 = this.back.update(this.model);
			if (num2 > 0)
			{
				base.RegisterScript("alert('系统提示：\\n\\n修改成功');window.opener.location.href=window.opener.location.href;window.close();");
				return;
			}
			base.RegisterScript("alert('系统提示：\\n\\n修改失败');window.opener.location.href=window.opener.location.href;window.close();");
			return;
		}
	}
	protected void setModelValue()
	{
		this.model.Rcode = this.txtCode.Value;
		this.model.Procode = this.hdnProCode.Value;
		this.model.Person = this.txtPeople.Value;
		this.model.InTime = Convert.ToDateTime(this.dtxtApplyDate.Text);
		this.model.Explain = this.txtRemark.Value;
		this.model.IsIn = true;
		this.model.Annx = "";
		this.model.Tcode = "";
		this.model.FlowState = 0;
	}
	protected void setControlsValue()
	{
	}
}


