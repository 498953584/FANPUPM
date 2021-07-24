using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Resource_ResMap : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string parentId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"];
		}
		if (!string.IsNullOrEmpty(base.Request["parentId"]))
		{
			this.parentId = base.Request["parentId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && this.action == "update")
		{
			ResMappingService source = new ResMappingService();
			System.Collections.Generic.List<string> list = (
				from m in source
				where m.ParentId == this.parentId
				select m.ResourceId).ToList<string>();
			this.hfldParentId.Value = this.parentId;
			Resource resource = new Resource();
			this.txtParentName.Text = resource.GetResourceName(this.parentId);
			this.hfldChildResId.Value = JsonHelper.JsonSerializer<string[]>(list.ToArray());
			this.DataBindChildRes();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (string.IsNullOrEmpty(this.hfldParentId.Value) || string.IsNullOrEmpty(this.hfldChildResId.Value))
		{
			return;
		}
		ResMappingService resMappingService = new ResMappingService();
		resMappingService.DeleteFromParentId(this.parentId);
		string value = this.hfldParentId.Value;
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldChildResId.Value);
		foreach (string current in listFromJson)
		{
			this.AddMapping(current, value);
		}
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_ResMap' });");
	}
	protected void btnDataBindRes_Click(object sender, System.EventArgs e)
	{
		if (string.IsNullOrEmpty(this.hfldReturnResId.Value))
		{
			return;
		}
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldReturnResId.Value);
		System.Collections.Generic.List<string> list = JsonHelper.GetListFromJson(this.hfldChildResId.Value);
		list = listFromJson.Union(list).ToList<string>();
		this.hfldChildResId.Value = JsonHelper.JsonSerializer<string[]>(list.ToArray());
		this.DataBindChildRes();
	}
	protected void gvwChildRes_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwChildRes.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		if (string.IsNullOrEmpty(this.hfldCheckedResId.Value))
		{
			return;
		}
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldCheckedResId.Value);
		System.Collections.Generic.List<string> listFromJson2 = JsonHelper.GetListFromJson(this.hfldChildResId.Value);
		foreach (string current in listFromJson)
		{
			listFromJson2.Remove(current);
		}
		if (listFromJson2.Count == 0)
		{
			this.hfldChildResId.Value = string.Empty;
		}
		else
		{
			this.hfldChildResId.Value = JsonHelper.JsonSerializer<string[]>(listFromJson2.ToArray());
		}
		this.DataBindChildRes();
	}
	private void AddMapping(string resourceId, string parentId)
	{
		if (this.IsExits(resourceId, parentId))
		{
			return;
		}
		string cmdText = "INSERT INTO Res_Mapping(Id, ResourceId, ParentId) VALUES(NEWID(), @ResId, @ParentId);";
		SqlParameter[] commandParameters = new SqlParameter[]
		{
			new SqlParameter("@ResId", resourceId),
			new SqlParameter("@ParentId", parentId)
		};
		SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
	}
	private bool IsExits(string resourceId, string parentId)
	{
		bool result;
		try
		{
			string cmdText = "SELECT COUNT(*) FROM Res_Mapping WHERE ResourceId = @ResId AND ParentId = @ParentId";
			SqlParameter[] commandParameters = new SqlParameter[]
			{
				new SqlParameter("@ResId", resourceId),
				new SqlParameter("@ParentId", parentId)
			};
			object obj = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters);
			result = (DBHelper.GetInt(obj) > 0);
		}
		catch
		{
			result = false;
		}
		return result;
	}
	private void DataBindChildRes()
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldChildResId.Value);
		string cmdText = string.Format("\r\n\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY r.InputDate DESC) AS No,\r\n\t\t\t\tr. ResourceId, r. ResourceCode, r.ResourceName, r.ModelNumber, r.Brand\r\n\t\t\tFROM Res_Resource r \r\n\t\t\tWHERE r.ResourceId IN (\t{0}\t)", StringUtility.GetArrayToInStr(listFromJson.ToArray()));
		this.gvwChildRes.DataSource = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
		this.gvwChildRes.DataBind();
	}
}


