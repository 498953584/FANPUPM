using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Serialize;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_ConstructMain : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private string prjId = string.Empty;
	private string type = string.Empty;
	private string conId = string.Empty;
	private string year = string.Empty;
	private string contractId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (base.Request.QueryString["prjId"] != null)
		{
			this.prjId = base.Request.QueryString["prjId"];
		}
		if (base.Request.QueryString["type"] != null)
		{
			this.type = base.Request.QueryString["type"];
		}
		if (base.Request.QueryString["conId"] != null)
		{
			this.conId = base.Request.QueryString["conId"];
		}
		if (base.Request.QueryString["year"] != null)
		{
			this.year = base.Request.QueryString["year"];
		}
		if (!string.IsNullOrEmpty(base.Request["contractId"]))
		{
			this.contractId = base.Request["contractId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			if (this.type == "add")
			{
				this.txtDate.Text = System.DateTime.Now.ToString("yyyy-M-d");
				this.txtInputUser.Text = PageHelper.QueryUser(this, base.UserCode);
				this.hfldReportId.Value = System.Guid.NewGuid().ToString();
			}
			else
			{
				this.hfldReportId.Value = this.conId;
				this.BindGv();
				ConstructReport byId = ConstructReport.GetById(this.conId);
				this.txtDate.Text = byId.InputDate.ToString("yyyy-M-dd");
				this.txtInputUser.Text = byId.InputUser;
				this.txtWorkCard.Value = byId.WorkCard;
			}
			this.hfldAddOrEdit.Value = this.type;
			this.hfldPrjId.Value = this.prjId;
			this.FileUpload1.RecordCode = this.hfldReportId.Value;
		}
	}
	protected void gvTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string text = this.gvTask.DataKeys[e.Row.RowIndex].Values[1].ToString();
			string text2 = this.gvTask.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			e.Row.Attributes["id"] = text;
			if (this.hfldIsWBSRelevance.Value == "1")
			{
				Image image = new Image();
				image.ImageUrl = "../../images/tree/more.gif";
				image.ToolTip = "详细信息";
				image.Attributes.Add("rowId", text2);
				image.Attributes["onclick"] = string.Concat(new string[]
				{
					"showInfo('",
					text2,
					"','",
					text,
					"')"
				});
				image.Style.Add("float", "right");
				image.Style.Add("cursor", "hand");
				e.Row.Cells[2].Controls.Add(image);
			}
		}
	}
	protected void btnUpdate_Click(object sender, System.EventArgs e)
	{
		if (this.gvTask.Rows.Count == 0)
		{
			base.RegisterScript("alert('系统提示：\\n\\n没有选择任务的时候不能进行保存！')");
			return;
		}
		this.hfldIsPostBack.Value = "true";
		ConstructReport constructReport = ConstructReport.Create(this.hfldReportId.Value, this.hfldPrjId.Value, this.txtInputUser.Text.Trim(), System.Convert.ToDateTime(this.txtDate.Text), null, this.txtWorkCard.Value.Trim(), -1);
		ConstructReport byId = ConstructReport.GetById(this.hfldReportId.Value);
		constructReport.IsValid = true;
		if (byId != null)
		{
			constructReport.Update(constructReport);
			System.Collections.IEnumerator enumerator = this.gvTask.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
					string taskId = this.gvTask.DataKeys[gridViewRow.RowIndex].Values[0].ToString();
					string id = this.gvTask.DataKeys[gridViewRow.RowIndex].Values[1].ToString();
					string text = ((TextBox)gridViewRow.FindControl("txtCompleteQuantity")).Text;
					string text2 = ((TextBox)gridViewRow.FindControl("txtWorkContent")).Text;
					System.Collections.Generic.List<ConstructResource> resourceList = null;
					ConstructTask constructTask = ConstructTask.Create(id, this.hfldReportId.Value, taskId, System.Convert.ToDecimal(text), text2, "", resourceList);
					constructTask.Update(constructTask);
				}
				goto IL_1A8;
			}
			finally
			{
				System.IDisposable disposable = enumerator as System.IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		constructReport.State = "0";
		constructReport.Add(constructReport);
		IL_1A8:
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_ConstructTask' });");
	}
	protected void btnBindResTask_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldResourceId.Value))
		{
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(this.hfldResourceId.Value);
			if (this.type == "add" && ConstructReport.GetById(this.hfldReportId.Value) == null)
			{
				ConstructReport constructReport = ConstructReport.Create(this.hfldReportId.Value, this.hfldPrjId.Value, this.txtInputUser.Text.Trim(), System.DateTime.Now, null, this.txtWorkCard.Value.Trim(), -1);
				constructReport.State = "0";
				constructReport.IsValid = false;
				constructReport.Add(constructReport);
				BudConsReportService budConsReportService = new BudConsReportService();
				BudConsReport byId = budConsReportService.GetById(this.hfldReportId.Value);
				if (byId != null)
				{
					byId.ConstractId = this.contractId;
					budConsReportService.Update(byId);
				}
			}
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string taskId = array2[i];
				if (!ConstructTask.isExist(this.hfldReportId.Value, taskId))
				{
					System.Collections.Generic.List<ConstructResource> resourceList = null;
					ConstructTask constructTask = ConstructTask.Create(System.Guid.NewGuid().ToString(), this.hfldReportId.Value, taskId, System.Convert.ToDecimal("0.000"), "", "", resourceList);
					constructTask.Add(constructTask, this.isWBSRelevance);
				}
			}
		}
		this.SelectResource1.ResourceId = string.Empty;
		this.SelectResource1.ResTempType = string.Empty;
		this.conId = this.hfldReportId.Value;
		this.BindGv();
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldPurchaseChecked.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
		}
		else
		{
			list.Add(this.hfldPurchaseChecked.Value);
		}
		try
		{
			ConstructTask.Delete(list);
			base.RegisterScript("$('#btnBindResTask').hide(); alert('系统提示：\\n\\删除成功！')");
			this.BindGv();
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
		}
	}
	private void BindGv()
	{
		System.Collections.Generic.List<string> taskIdsByReportId = ConstructTask.GetTaskIdsByReportId(this.hfldReportId.Value);
		string text = string.Empty;
		foreach (string current in taskIdsByReportId)
		{
			if (!string.IsNullOrEmpty(current))
			{
				text += "'";
				text += current;
				text += "',";
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Substring(0, text.Length - 1);
		}
		else
		{
			text = "''";
		}
		DataTable allByTaskIds = ConstructTask.GetAllByTaskIds(text, this.hfldReportId.Value, true, false);
		this.ViewState["TasksTable"] = allByTaskIds;
		this.gvTask.DataSource = allByTaskIds;
		this.gvTask.DataBind();
		if (this.hfldIsWBSRelevance.Value == "1")
		{
			this.gvTask.Columns[10].Visible = false;
			return;
		}
		this.gvTask.Columns[9].Visible = false;
	}
}


