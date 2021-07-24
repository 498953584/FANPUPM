using ASP;
using cn.justwin.BLL;
using cn.justwin.opm.action;
using cn.justwin.opm.Business_Data;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OPM_Business_Data_Business_Data_ItemView : NBasePage, IRequiresSessionState
{
	private Business_DataItemAction action = new Business_DataItemAction();
	private Business_DataAction act = new Business_DataAction();
	private string prjId = string.Empty;
	private string bId = string.Empty;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (base.Request["pc"] != null)
		{
			this.prjId = base.Request["pc"].ToString();
		}
		if (base.Request["ic"] != null)
		{
			this.bId = base.Request["ic"].ToString();
		}
		this.InitPage(this.bId);
		this.hfldAdjunctPath.Value = ConfigHelper.Get("ImgBusiness");
	}
	public void InitPage(string uid)
	{
		DataTable model = this.action.GetModel(uid);
		DataTable codeID = this.action.GetCodeID(uid);
		string uid2 = codeID.Rows[0]["CodeID"].ToString();
		DataTable model2 = this.act.GetModel(uid2);
		this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
		if (model.Rows.Count > 0)
		{
			this.lblPCode.Text = model.Rows[0]["PCode"].ToString();
			this.lblPName.Text = model.Rows[0]["PName"].ToString();
			this.lblDesignDate.Text = System.Convert.ToDateTime(model.Rows[0]["DesignDate"]).ToShortDateString();
			this.lblBllProducer.Text = PageHelper.QueryUser(this, model.Rows[0]["AddUser"].ToString());
			this.lblEndDate.Text = ((model2.Rows[0]["EndDate"] == System.DBNull.Value) ? "" : System.Convert.ToDateTime(model2.Rows[0]["EndDate"]).ToShortDateString());
			this.lblDutyUser.Text = model2.Rows[0]["DutyUser"].ToString();
			string userCodes = model2.Rows[0]["Designer"].ToString();
			this.lblDesigner.Text = WebUtil.GetUserNames(userCodes);
			this.lblRemark.Text = model.Rows[0]["Remark"].ToString();
			this.lblFlowState.Text = Common2.GetState(model.Rows[0]["FlowState"].ToString());
		}
		this.upload.InnerHtml = base.GetAnnx(this.bId);
	}
	public string FilesBind(string recordCode)
	{
		string text = ConfigHelper.Get("ImgBusiness").ToString();
		string result;
		try
		{
			string[] files = System.IO.Directory.GetFiles(base.Server.MapPath(text) + recordCode);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = string.Empty;
				text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
				string str = text + "/" + recordCode;
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


