using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_17_Entpm_ScienceInnovate_ScienceInnovateList : NBasePage, System.Web.SessionState.IRequiresSessionState
{
	private ProjectFilesUpAction pfua = new ProjectFilesUpAction();
	private DataTable dt = new DataTable();

	protected void Page_Load(object sender, EventArgs e)
	{
		this.gvwScienceInnovate.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request["tcode"]))
		{
			this.hfSelectValue.Value = base.Request["tcode"];
			DataTable fileClassList = this.pfua.GetFileClassList(this.hfSelectValue.Value);
			this.gvwScienceInnovate.DataSource = fileClassList;
			this.gvwScienceInnovate.DataBind();
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		string value = this.hfTcode.Value;
		string[] array;
		if (!value.Contains("^"))
		{
			array = new string[]
			{
				value
			};
		}
		else
		{
			array = value.Substring(1).Split(new char[]
			{
				'^'
			});
		}
		if (!this.ValidateDelete(array))
		{
			return;
		}
		if (!this.ExistInformation(array))
		{
			return;
		}
		string text = string.Empty;
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string str = array2[i];
			text = text + str + ",";
		}
		if (this.pfua.DeleteFilesClass(text.TrimEnd(new char[]
		{
			','
		})))
		{
			base.RegisterScript("top.ui.show('删除成功！');");
		}
		else
		{
			base.RegisterScript("top.ui.alert('删除失败！');");
		}
		DataTable fileClassList = this.pfua.GetFileClassList(this.hfSelectValue.Value);
		this.gvwScienceInnovate.DataSource = fileClassList;
		this.gvwScienceInnovate.DataBind();
	}
	protected void gvwScienceInnovate_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwScienceInnovate.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void gvwScienceInnovate_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwScienceInnovate.PageIndex = e.NewPageIndex;
		DataTable fileClassList = this.pfua.GetFileClassList(this.hfSelectValue.Value);
		this.gvwScienceInnovate.DataSource = fileClassList;
		this.gvwScienceInnovate.DataBind();
	}
	private bool ValidateDelete(string[] arrTcode)
	{
		for (int i = 0; i < arrTcode.Length; i++)
		{
			string strId = arrTcode[i];
			DataTable fileClassInformation = this.pfua.GetFileClassInformation(strId);
			DataTable filesClassCount = this.pfua.GetFilesClassCount(fileClassInformation.Rows[0]["ClassID"].ToString());
			int count = filesClassCount.Rows.Count;
			if (count > 0)
			{
				base.RegisterScript("alert('系统提示：\\n此类不能删除！');");
				return false;
			}
		}
		return true;
	}
	private bool ExistInformation(string[] arrTcode)
	{
		for (int i = 0; i < arrTcode.Length; i++)
		{
			string strId = arrTcode[i];
			DataTable fileClassInformation = this.pfua.GetFileClassInformation(strId);
			DataTable standardList = EntStandardAction.GetStandardList(fileClassInformation.Rows[0]["ClassID"].ToString());
			if (standardList.Rows.Count > 0)
			{
				base.RegisterScript("alert('系统提示：\\n此类已被使用，不能删除！');");
				return false;
			}
		}
		return true;
	}
}


