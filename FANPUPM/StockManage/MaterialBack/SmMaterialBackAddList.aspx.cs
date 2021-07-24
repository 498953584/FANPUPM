using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_MaterialBack_SmMaterialBackAddList : NBasePage, IRequiresSessionState
{
	private MaterialBack mtBack = new MaterialBack();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdnProname.Value = base.Request.QueryString["pn"];
			this.hndProCode.Value = base.Request.QueryString["PrjGuid"];
			this.lblProjectName.Text = base.Request.QueryString["pn"];
			this.btnDel.Attributes.Add("onclick", "if(!delCheck()){return false;}");
			this.btnUpdate.Attributes.Add("onclick", "if(!checkCount()){return false;}");
			this.btnMakeSure.Attributes.Add("onclick", "if(!checkCount()){return false;}");
			this.btnLook.Attributes.Add("onclick", "if(!checkCount()){return false;}");
			this.getViewList();
		}
	}
	protected void getViewList()
	{
		string procode = base.Request.QueryString["PrjGuid"];
		this.gvSm_MaterialBack.DataSource = this.mtBack.getSmRefoundByPro(procode);
		this.gvSm_MaterialBack.DataBind();
	}
	protected void btnSearch_ServerClick(object sender, EventArgs e)
	{
		string text = "";
		if (this.txtCode.Value != "")
		{
			text = text + "and rcode like'%" + this.txtCode.Value + "%'";
		}
		if (this.DateIn.Text != "")
		{
			text = text + "and intime ='" + this.DateIn.Text + " 0:00:00'";
		}
		this.gvSm_MaterialBack.DataSource = this.mtBack.search(text);
		this.gvSm_MaterialBack.DataBind();
	}
	protected void btnDel_ServerClick(object sender, EventArgs e)
	{
		foreach (GridViewRow gridViewRow in this.gvSm_MaterialBack.Rows)
		{
			CheckBox checkBox = (CheckBox)gridViewRow.FindControl("chkBox");
			if (checkBox.Checked)
			{
				HtmlInputHidden expr_42 = this.hdnSwid;
				expr_42.Value = expr_42.Value + checkBox.ToolTip + ",";
			}
		}
		string text = this.hdnSwid.Value;
		text = text.Substring(0, text.Length - 1);
		int num;
		if (text.IndexOf(",") == -1)
		{
			num = this.mtBack.delete(text);
		}
		else
		{
			string[] array = text.Split(new char[]
			{
				','
			});
			string text2 = "";
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string str = array2[i];
				text2 = text2 + str + "','";
			}
			text2 = text2.Substring(0, text2.Length - 2);
			string text3 = "delete Sm_Back_Stock where rcode in('" + text2 + ")";
			text3 = text3 + "delete from Sm_Refunding where rcode in('" + text2 + ")";
			num = this.mtBack.delMore(text3);
		}
		if (num > 0)
		{
			base.RegisterScript("alert('系统提示:\\n\\n删除成功！')");
		}
		else
		{
			base.RegisterScript("alert('系统提示:\\n\\n删除失败！')");
		}
		this.getViewList();
	}
	protected void btnUpdate_ServerClick(object sender, EventArgs e)
	{
		foreach (GridViewRow gridViewRow in this.gvSm_MaterialBack.Rows)
		{
			CheckBox checkBox = (CheckBox)gridViewRow.FindControl("chkBox");
			if (checkBox.Checked)
			{
				base.RegisterScript(string.Concat(new string[]
				{
					"winopen('SmMaterialBackAdd.aspx?optype=update&rcode=",
					checkBox.ToolTip,
					"&pname=",
					this.hdnProname.Value,
					"')"
				}));
			}
		}
	}
	public string getProName()
	{
		return this.hdnProname.Value;
	}
	protected void btnLook_ServerClick(object sender, EventArgs e)
	{
		foreach (GridViewRow gridViewRow in this.gvSm_MaterialBack.Rows)
		{
			CheckBox checkBox = (CheckBox)gridViewRow.FindControl("chkBox");
			if (checkBox.Checked)
			{
				base.RegisterScript(string.Concat(new string[]
				{
					"winopen('SmMaterialBackAdd.aspx?optype=look&rcode=",
					checkBox.ToolTip,
					"&pname=",
					this.hdnProname.Value,
					"')"
				}));
			}
		}
	}
}


