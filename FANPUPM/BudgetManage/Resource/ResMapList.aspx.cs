using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Resource_ResMapList : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBind();
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldParentId.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldParentId.Value);
		}
		else
		{
			list.Add(this.hfldParentId.Value);
		}
		try
		{
			ResMappingService resMappingService = new ResMappingService();
			foreach (string current in list)
			{
				resMappingService.DeleteFromParentId(current);
			}
			base.RegisterScript("alert('系统提示：\\n\\n删除成功!');");
			this.DataBind();
		}
		catch (System.Exception ex)
		{
			base.RegisterScript("alert('系统提示：\\n\\n" + ex.Message + "!');");
		}
	}
	protected void gvwResMap_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwResMap.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	private new void DataBind()
	{
		this.gvwResMap.DataSource = this.GetResMap();
		this.gvwResMap.DataBind();
	}
	private DataTable GetResMap()
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		stringBuilder.AppendFormat(" AND ResourceCode LIKE '%{0}%' ", this.GetSafeString(this.txtResCode.Text.Trim()));
		stringBuilder.AppendFormat(" AND ResourceName LIKE '%{0}%' ", this.GetSafeString(this.txtResName.Text.Trim()));
		stringBuilder.AppendFormat(" AND ModelNumber LIKE '%{0}%' ", this.GetSafeString(this.txtModelNumber.Text.Trim()));
		string text = "\r\n\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY r.InputDate DESC) AS No,\r\n\t\t\t\tr.ResourceId, r.ResourceCode, r.ResourceName, r.ModelNumber, r.Brand\r\n\t\t\tFROM Res_Resource r \r\n\t\t\tWHERE r.ResourceId IN (\r\n\t\t\t\tSELECT DISTINCT ParentId FROM Res_Mapping\r\n\t\t\t)";
		text += stringBuilder.ToString();
		text += " order by ResourceCode ";
		return SqlHelper.ExecuteQuery(CommandType.Text, text, new SqlParameter[0]);
	}
	protected void btnSertch_Click(object sender, System.EventArgs e)
	{
		this.DataBind();
	}
	private string GetSafeString(string oldStr)
	{
		oldStr = oldStr.Replace("'", "''");
		if (oldStr.Contains("%"))
		{
			oldStr = oldStr.Replace("%", "[%]");
		}
		return oldStr;
	}
}


