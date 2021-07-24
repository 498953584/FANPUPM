using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_BudConModifyView : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;
	private BudConModifyService conModifySer = new BudConModifyService();
	private BudConModifyTaskService conModifyTaskSer = new BudConModifyTaskService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.id = base.Request["ic"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldModifyId.Value = this.id;
			this.lblBllProducer.Text = WebUtil.GetUserNames(base.UserCode);
			this.lblPrintDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			this.BindConModifyInfo();
			System.Collections.Generic.List<BudConModifyTask> list = (
				from mt in this.conModifyTaskSer
				where mt.ModifyId == this.id && mt.ModifyType.Value == 0
				orderby mt.OrderNumber descending
				select mt).ToList<BudConModifyTask>();
			System.Collections.Generic.List<BudConModifyTask> list2 = (
				from mt in this.conModifyTaskSer
				where mt.ModifyId == this.id && mt.ModifyType.Value == 1
				orderby mt.OrderNumber descending
				select mt).ToList<BudConModifyTask>();
			decimal d = 0m;
			d += list.Sum((BudConModifyTask omt) => omt.Total);
			d += list2.Sum((BudConModifyTask imt) => imt.Total);
			this.txtBudAmount.Text = d.ToString("0.000");
			this.BindGv(list, list2);
			this.FileUpload1.InnerHtml = this.FilesBind(this.id);
		}
	}
	private void BindConModifyInfo()
	{
		BudConModify byId = this.conModifySer.GetById(this.id);
		if (byId != null)
		{
			this.txtModifyCode.Text = byId.ModifyCode;
			this.txtModifyContent.Text = byId.ModifyContent;
			this.txtModifyFileCode.Text = byId.ModifyFileCode;
			ProjectInfo byId2 = ProjectInfo.GetById(byId.PrjId);
			if (byId2 != null)
			{
				this.txtPrjName.Text = byId2.PrjName;
			}
			this.txtReportAmount.Text = byId.ReportAmount.ToString();
			this.txtApprovalAmount.Text = byId.ApprovalAmount.ToString();
			if (byId.ApprovalDate.HasValue)
			{
				this.txtApprovalDate.Text = byId.ApprovalDate.Value.ToString("yyyy-MM-dd");
			}
			this.txtNotes.Text = byId.Note;
		}
	}
	protected string GetTaskCode(string taskId)
	{
		string result = string.Empty;
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		BudContractTask byId = budContractTaskService.GetById(taskId);
		if (byId != null)
		{
			result = byId.TaskCode;
		}
		else
		{
			BudConModifyTask byId2 = this.conModifyTaskSer.GetById(taskId);
			if (byId2 != null)
			{
				result = byId2.ModifyTaskCode;
			}
		}
		return result;
	}
	protected string GetTypeName(string orderNum)
	{
		XpmCodeServices xpmCodeServices = new XpmCodeServices();
		System.Collections.Generic.IList<XpmCode> bySignCode = xpmCodeServices.GetBySignCode("taskType");
		string result = string.Empty;
		foreach (XpmCode current in bySignCode)
		{
			if (current.CodeID == orderNum.Length / 3)
			{
				result = current.CodeName;
				break;
			}
		}
		return result;
	}
	protected void gvOutTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvOutTask.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
		}
	}
	protected void gvInTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvInTask.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
		}
	}
	private void BindGv(System.Collections.Generic.List<BudConModifyTask> listOutConModifyTask, System.Collections.Generic.List<BudConModifyTask> listInConModifyTask)
	{
		if (listOutConModifyTask.Count > 0)
		{
			this.gvOutTask.DataSource = listOutConModifyTask;
			this.gvOutTask.DataBind();
		}
		if (listInConModifyTask.Count > 0)
		{
			this.gvInTask.DataSource = listInConModifyTask;
			this.gvInTask.DataBind();
		}
	}
	public string FilesBind(string modifyTaskId)
	{
		string text = ConfigHelper.Get("BudConModify");
		string result;
		try
		{
			string[] files = System.IO.Directory.GetFiles(base.Server.MapPath(text) + modifyTaskId);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = string.Empty;
				text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
				string str = text + "/" + modifyTaskId;
				string str2 = str + "/" + text3;
				text3 = string.Concat(new string[]
				{
					"<a  class=\"link\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
					HttpUtility.UrlEncode(str2),
					"\"  >",
					text3,
					"</a>"
				});
				stringBuilder.Append(text3);
				stringBuilder.Append(", ");
			}
			string text4 = string.Empty;
			if (stringBuilder.Length > 2)
			{
				text4 = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
			}
			result = text4;
		}
		catch
		{
			result = "";
		}
		return result;
	}
}


