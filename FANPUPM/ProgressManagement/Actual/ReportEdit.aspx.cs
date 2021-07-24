using ASP;
using cn.justwin.BLL;
using cn.justwin.ProgressManagement;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManagement_Actual_ReportEdit : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Initalize();
		}
	}
	protected void Initalize()
	{
		string strA = base.Request["type"];
		DataTable dataTable = null;
		if (string.Compare(strA, "edit", true) == 0)
		{
			string text = base.Request["reportId"];
			if (!string.IsNullOrWhiteSpace(text))
			{
				Report byId = Report.GetById(text);
				if (byId != null)
				{
					this.txtDate.Text = byId.InputDate.Value.ToString("yyyy-M-d");
					this.txtInputUser.Text = PageHelper.QueryUser(this, byId.InputUser);
					this.hfldInputUser.Value = byId.InputUser;
					this.txtWorkCard.InnerText = byId.Note;
					this.hfldReportId.Value = byId.Id;
				}
				dataTable = ReportDetail.GetDetails(text);
				this.ViewState["PTasks"] = dataTable;
			}
		}
		else
		{
			this.txtDate.Text = System.DateTime.Now.ToString("yyyy-M-d");
			this.txtInputUser.Text = PageHelper.QueryUser(this, base.UserCode);
			this.hfldInputUser.Value = base.UserCode;
			this.hfldReportId.Value = System.Guid.NewGuid().ToString();
		}
		this.Bind(dataTable);
		this.FileUpload1.RecordCode = this.hfldReportId.Value;
	}
	protected void Bind(DataTable dtSource)
	{
		this.gvwTask.DataSource = dtSource;
		this.gvwTask.DataBind();
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldCheckIds.Value;
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		DataTable dataTable = this.ViewState["PTasks"] as DataTable;
		if (value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(value);
		}
		else
		{
			list.Add(value);
		}
		DataRow[] array = dataTable.Select("TaskUID IN (" + StringUtility.GetArrayToInStr(list.ToArray()) + ")");
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow row = array2[i];
			dataTable.Rows.Remove(row);
		}
		this.ViewState["PTasks"] = dataTable;
		this.Bind(dataTable);
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldReportId.Value;
		System.DateTime? inputDate = null;
		if (!string.IsNullOrEmpty(this.txtDate.Text))
		{
			inputDate = new System.DateTime?(System.Convert.ToDateTime(this.txtDate.Text));
		}
		string value2 = this.hfldInputUser.Value;
		string innerText = this.txtWorkCard.InnerText;
		string proVerId = base.Request["id"];
		System.Collections.Generic.List<ReportDetail> list = new System.Collections.Generic.List<ReportDetail>();
		foreach (GridViewRow gridViewRow in this.gvwTask.Rows)
		{
			string id = this.gvwTask.DataKeys[gridViewRow.RowIndex]["Id"].ToString();
			string taskUID = this.gvwTask.DataKeys[gridViewRow.RowIndex]["TaskUID"].ToString();
			System.DateTime? start = null;
			string text = (gridViewRow.Cells[6].FindControl("txtStartDate") as TextBox).Text;
			if (!string.IsNullOrEmpty(text))
			{
				start = new System.DateTime?(System.Convert.ToDateTime(text));
			}
			System.DateTime? finish = null;
			string text2 = (gridViewRow.Cells[7].FindControl("txtFinshDate") as TextBox).Text;
			if (!string.IsNullOrEmpty(text2))
			{
				finish = new System.DateTime?(System.Convert.ToDateTime(text2));
			}
			byte completed = 0;
			string text3 = (gridViewRow.Cells[8].FindControl("txtCompleteQuantity") as TextBox).Text;
			if (!string.IsNullOrEmpty(text3))
			{
				completed = System.Convert.ToByte(text3);
			}
			string text4 = (gridViewRow.FindControl("txtNoet") as TextBox).Text;
			ReportDetail item = ReportDetail.Create(id, value, taskUID, start, finish, completed, text4);
			list.Add(item);
		}
		string strA = base.Request["type"];
		if (string.Compare(strA, "add", true) == 0)
		{
			this.AddReport(value, inputDate, value2, innerText, proVerId, list);
		}
		else
		{
			this.EditReport(value, inputDate, value2, innerText, list);
		}
		base.RegisterScript("CloseTab();");
	}
	protected void AddReport(string reportId, System.DateTime? inputDate, string inputUser, string note, string proVerId, System.Collections.Generic.List<ReportDetail> details)
	{
		Report report = Report.Create(reportId, inputDate, inputUser, note, proVerId);
		report.Add(report, details);
	}
	protected void EditReport(string reportId, System.DateTime? inputDate, string inputUser, string note, System.Collections.Generic.List<ReportDetail> details)
	{
		Report byId = Report.GetById(reportId);
		if (byId != null)
		{
			byId.InputUser = inputUser;
			byId.InputDate = inputDate;
			byId.Note = note;
			byId.Update(byId, details);
		}
	}
	protected void gvwTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwTask.DataKeys[e.Row.RowIndex]["TaskUID"].ToString();
		}
	}
	protected void btnBindResTask_Click(object sender, System.EventArgs e)
	{
		try
		{
			string value = this.hfldTaskId.Value;
			System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(value);
			string proVerId = base.Request["id"];
			DataTable tasks = ReportDetail.GetTasks(proVerId, listFromJson);
			if (this.ViewState["PTasks"] != null)
			{
				DataTable dataTable = this.ViewState["PTasks"] as DataTable;
				System.Collections.Generic.List<DataRow> list = new System.Collections.Generic.List<DataRow>();
				foreach (DataRow dataRow in tasks.Rows)
				{
					int num = dataTable.Select("TaskUID='" + dataRow["TaskUID"] + "'").Count<DataRow>();
					if (num > 0)
					{
						list.Add(dataRow);
					}
				}
				foreach (DataRow current in list)
				{
					tasks.Rows.Remove(current);
				}
				dataTable.Merge(tasks);
				this.ViewState["PTasks"] = dataTable;
			}
			else
			{
				this.ViewState["PTasks"] = tasks;
			}
			DataTable dtSource = this.ViewState["PTasks"] as DataTable;
			this.Bind(dtSource);
		}
		catch
		{
		}
	}
	protected string GetTime(object time)
	{
		if (time != null && time.ToString() != "")
		{
			return System.Convert.ToDateTime(time).ToString("yyyy-M-d");
		}
		return "";
	}
}


