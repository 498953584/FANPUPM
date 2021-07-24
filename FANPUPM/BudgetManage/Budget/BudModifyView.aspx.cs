using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
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
using System.Web.Configuration;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_BudModifyView : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private BudModifyService modifySer = new BudModifyService();
	private BudModifyTaskService modifyTaskSer = new BudModifyTaskService();

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
			this.hfldAdjunctPath.Value = WebConfigurationManager.AppSettings["BudModify"];
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.hfldModifyId.Value = this.id;
			this.lblBllProducer.Text = WebUtil.GetUserNames(base.UserCode);
			this.lblPrintDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			this.BindModifyInfo();
			System.Collections.Generic.List<BudModifyTask> list = (
				from mt in this.modifyTaskSer
				where mt.ModifyId == this.id && mt.ModifyType.Value == 0
				orderby mt.OrderNumber descending
				select mt).ToList<BudModifyTask>();
			System.Collections.Generic.List<BudModifyTask> list2 = (
				from mt in this.modifyTaskSer
				where mt.ModifyId == this.id && mt.ModifyType.Value == 1
				orderby mt.OrderNumber descending
				select mt).ToList<BudModifyTask>();
			decimal d = 0m;
			d += list.Sum((BudModifyTask omt) => omt.Total);
			d += list2.Sum((BudModifyTask imt) => imt.Total);
			this.txtBudAmount.Text = d.ToString("0.000");
			this.BindGv(list, list2);
			this.FileUpload1.InnerHtml = this.FilesBind(this.id);
			if (this.hfldIsWBSRelevance.Value == "0")
			{
				base.Request.Cookies.Remove(this.id);
				BudModifyTaskResService source = new BudModifyTaskResService();
				System.Collections.Generic.List<BudModifyTaskRes> t = (
					from mtss in source
					where mtss.ModifyId == this.id
					select mtss).ToList<BudModifyTaskRes>();
				string value = JsonHelper.JsonSerializer<System.Collections.Generic.List<BudModifyTaskRes>>(t);
				HttpCookie httpCookie = new HttpCookie(this.id);
				httpCookie.Value = value;
				base.Response.Cookies.Add(httpCookie);
			}
		}
	}
	private void BindModifyInfo()
	{
		BudModify byId = this.modifySer.GetById(this.id);
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
		cn.justwin.Domain.BudTask byId = cn.justwin.Domain.BudTask.GetById(taskId);
		if (byId != null)
		{
			result = byId.Code;
		}
		else
		{
			BudModifyTask byId2 = this.modifyTaskSer.GetById(taskId);
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
			string text = this.gvOutTask.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = text;
			if (this.hfldIsWBSRelevance.Value == "1" && text != null)
			{
				Image image = new Image();
				image.ImageUrl = "../../images/tree/more.gif";
				image.ToolTip = "查看资源";
				image.Attributes.Add("rowId", text);
				image.Attributes["onclick"] = "showInfo('" + text + "')";
				image.Style.Add("vertical-align", "middle");
				image.Style.Add("float", "right");
				image.Style.Add("cursor", "pointer");
				e.Row.Cells[2].Controls.Add(image);
			}
		}
	}
	protected void gvInTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvInTask.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = text;
			if (this.hfldIsWBSRelevance.Value == "1" && text != null)
			{
				Image image = new Image();
				image.ImageUrl = "../../images/tree/more.gif";
				image.ToolTip = "查看资源";
				image.Attributes.Add("rowId", text);
				image.Attributes["onclick"] = "showInfo('" + text + "')";
				image.Style.Add("vertical-align", "middle");
				image.Style.Add("float", "right");
				image.Style.Add("cursor", "pointer");
				e.Row.Cells[2].Controls.Add(image);
			}
		}
	}
	private void BindGv(System.Collections.Generic.List<BudModifyTask> listOutModifyTask, System.Collections.Generic.List<BudModifyTask> listInModifyTask)
	{
		if (listOutModifyTask.Count > 0)
		{
			this.gvOutTask.DataSource = listOutModifyTask;
			this.gvOutTask.DataBind();
		}
		if (listInModifyTask.Count > 0)
		{
			this.gvInTask.DataSource = listInModifyTask;
			this.gvInTask.DataBind();
		}
	}
	public string FilesBind(string modifyTaskId)
	{
		string text = ConfigHelper.Get("BudModify");
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


