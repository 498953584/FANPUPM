using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class WEB_NewsTypeEdit : BasePage, IRequiresSessionState
{
	private string strTypeCode = "";
	private string strOp = "";
	private NewsAction na = new NewsAction();

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		this.strTypeCode = base.Request.QueryString["typeCode"].ToString();
		this.strOp = base.Request.QueryString["t"].ToString();
		if (!base.IsPostBack)
		{
			if (this.strOp == "add")
			{
				this.txtParentClass.Text = this.na.getNewsTypeName(this.strTypeCode);
				return;
			}
			if (this.strOp == "upd")
			{
				this.txtParentClass.Text = this.na.getNewsTypeName(this.strTypeCode.Substring(0, this.strTypeCode.Length - 2));
				this.txtClassName.Text = this.na.getNewsTypeName(this.strTypeCode);
			}
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		if (!(this.strOp == "add"))
		{
			if (this.strOp == "upd")
			{
				if (this.na.updNewsType(this.strTypeCode, this.txtClassName.Text.Trim()))
				{
					this.JS.Text = "alert('更新成功!');returnValue = true; window.close();";
					return;
				}
				this.JS.Text = "alert('更新失败!');";
			}
			return;
		}
		if (this.na.addNewsType(this.strTypeCode, this.txtClassName.Text.Trim()))
		{
			this.JS.Text = "alert('增加成功!');returnValue = true; window.close();";
			return;
		}
		this.JS.Text = "alert('增加失败!');";
	}
}


