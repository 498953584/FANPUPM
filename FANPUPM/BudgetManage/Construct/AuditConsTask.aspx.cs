using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_AuditConsTask : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string prjId = string.Empty;
	private static string type = string.Empty;
	private string conId = string.Empty;
	private string year = string.Empty;
	private System.Text.StringBuilder strJS = new System.Text.StringBuilder();

	protected override void OnInit(System.EventArgs e)
	{
		if (base.Request.QueryString["prjId"] != null)
		{
			this.prjId = base.Request.QueryString["prjId"];
		}
		if (base.Request.QueryString["conId"] != null)
		{
			this.conId = base.Request.QueryString["conId"];
		}
		if (base.Request.QueryString["year"] != null)
		{
			this.year = base.Request.QueryString["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
			this.hfldReportId.Value = this.conId;
			this.hfldPrjId.Value = this.prjId;
		}
	}
	protected void BindGv()
	{
		System.Collections.Generic.List<string> taskIdsByReportId = ConstructTask.GetTaskIdsByReportId(this.conId);
		string text = "";
		foreach (string current in taskIdsByReportId)
		{
			if (current != "")
			{
				text += "'";
				text += current;
				text += "',";
			}
		}
		if (text != "")
		{
			text = text.Substring(0, text.Length - 1);
		}
		else
		{
			text = "''";
		}
		DataTable allByTaskIds = ConstructTask.GetAllByTaskIds(text, this.conId, true, true);
		this.gvTask.DataSource = allByTaskIds;
		this.gvTask.DataBind();
	}
	protected void gvTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string text = this.gvTask.DataKeys[e.Row.RowIndex].Value.ToString();
			string text2 = this.gvTask.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"displayRes('",
				text,
				"','",
				text2,
				"')"
			});
			e.Row.Style.Add("cursor", "pointer");
			HiddenField expr_E2 = this.hfldResInfo;
			expr_E2.Value = expr_E2.Value + "{\"Code\": \"" + text + "\",\"resources\": [";
			DataTable resInfo = ConstructResource.GetResInfo(text);
			foreach (DataRow dataRow in resInfo.Rows)
			{
				HiddenField expr_129 = this.hfldResInfo;
				object value = expr_129.Value;
				expr_129.Value = string.Concat(new object[]
				{
					value,
					"{\"resCode\":\" ",
					dataRow["ResourceCode"],
					"\","
				});
				HiddenField expr_172 = this.hfldResInfo;
				object value2 = expr_172.Value;
				expr_172.Value = string.Concat(new object[]
				{
					value2,
					"\"consTaskResId\":\" ",
					dataRow["Id"],
					"\","
				});
				HiddenField expr_1BB = this.hfldResInfo;
				expr_1BB.Value = expr_1BB.Value + "\"num\":\" " + System.Convert.ToDecimal(dataRow["AccountingQuantity"]).ToString("0.000") + "\",";
				HiddenField expr_1F9 = this.hfldResInfo;
				object value3 = expr_1F9.Value;
				expr_1F9.Value = string.Concat(new object[]
				{
					value3,
					"\"cbsCode\":\" ",
					dataRow["CBSCode"],
					"\"},"
				});
			}
			this.hfldResInfo.Value = this.hfldResInfo.Value.Remove(this.hfldResInfo.Value.LastIndexOf(','));
			if (resInfo.Rows.Count > 0)
			{
				HiddenField expr_2A0 = this.hfldResInfo;
				expr_2A0.Value += "]},";
				return;
			}
			HiddenField expr_2BC = this.hfldResInfo;
			expr_2BC.Value += "},";
		}
	}
	protected void btnUpdate_Click(object sender, System.EventArgs e)
	{
	}
}


