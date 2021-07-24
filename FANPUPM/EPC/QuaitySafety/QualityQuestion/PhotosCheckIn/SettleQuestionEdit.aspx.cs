using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_ProjectCost_PhotosCheckIn_SettleQuestionEdit : SubwayBasePage, IRequiresSessionState
{
	public IntendanceMasterAction intendanceMasterAction = new IntendanceMasterAction();
	public EPM_IntendanceInfoAction IntendanceInfoAction = new EPM_IntendanceInfoAction();
	public IntendancePhotoListAction aa = new IntendancePhotoListAction();

	public Guid IntendanceGuid
	{
		get
		{
			return (Guid)this.ViewState["IntendanceGuid"];
		}
		set
		{
			this.ViewState["IntendanceGuid"] = value;
		}
	}
	public Guid InfoGuid
	{
		get
		{
			return (Guid)this.ViewState["InfoGuid"];
		}
		set
		{
			this.ViewState["InfoGuid"] = value;
		}
	}
	public int flag
	{
		get
		{
			return (int)this.ViewState["flag"];
		}
		set
		{
			this.ViewState["flag"] = value;
		}
	}
	public Guid ProjectCode
	{
		get
		{
			return (Guid)this.ViewState["PROJECTCODE"];
		}
		set
		{
			this.ViewState["PROJECTCODE"] = value;
		}
	}
	public string ProjectName
	{
		get
		{
			return (string)this.ViewState["ProjectName"];
		}
		set
		{
			this.ViewState["ProjectName"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["IntendanceGuid"] == null)
			{
				this.js.Text = "alert('参数有误！');";
				return;
			}
			this.IntendanceGuid = new Guid(base.Request["IntendanceGuid"]);
			using (DataTable @params = this.IntendanceInfoAction.GetParams(this.IntendanceGuid))
			{
				if (@params.Rows.Count > 0)
				{
					this.ProjectCode = new Guid(@params.Rows[0]["prjguid"].ToString());
					this.lblPrjName.Text = @params.Rows[0]["prjname"].ToString();
					this.ProjectName = @params.Rows[0]["prjname"].ToString();
				}
			}
			this.flag = Convert.ToInt32(base.Request.QueryString["t"]);
			if (this.flag != 0)
			{
				if (this.flag == 1)
				{
					this.btnSettled.Text = "问题已解决";
					this.btnSettling.Text = "还有些问题";
					this.lblTitle.Text = "现场拍照监督验证";
				}
				else
				{
					this.btnSettled.Visible = false;
					this.btnSettling.Visible = false;
				}
				this.lbl1.Visible = true;
			}
			else
			{
				this.lblTitle.Text = "现场拍照监督解决问题";
			}
			this.DataBindToPage();
			this.hdnUserCode.Value = base.UserCode;
		}
	}
	protected void DataBindToPage()
	{
		IntendanceMaster intendanceMaster = this.intendanceMasterAction.GetIntendanceMaster(this.IntendanceGuid);
		EPM_IntendanceInfo model = this.IntendanceInfoAction.GetModel(this.IntendanceGuid);
		this.txtQuestionTitle.Text = intendanceMaster.QuestionTitle;
		this.txtDate.Text = intendanceMaster.BookInDate.ToString("yyyy-MM-dd");
		this.lbltype.Text = this.intendanceMasterAction.GetQuestionNameById(intendanceMaster.QuestionTypeId);
		new userManageDb();
		string userNamesComma = WebUtil.GetUserNamesComma(intendanceMaster.SettleYhdm);
		this.txtPerson.Text = userNamesComma;
		this.hdfusercode.Value = intendanceMaster.SettleToPerson;
		this.txtQuestion.Text = model.QuestionExplain;
		if (this.flag == 0)
		{
			this.txtSettleExplain.Text = model.SettleExplain;
			if (!string.IsNullOrEmpty(intendanceMaster.SettleToPerson))
			{
				this.txtToPerson.Value = WebUtil.GetUserNamesComma(intendanceMaster.SettleToPerson);
			}
		}
		else
		{
			this.txtSettleExplain.Visible = false;
			this.lblSettleExplain.Visible = true;
			this.txtToCause.Visible = false;
			this.lblToCause.Visible = true;
			this.txtToPerson.Visible = false;
			this.lblToPerson.Visible = true;
			this.Img3.Visible = false;
			if (!string.IsNullOrEmpty(intendanceMaster.SettleToPerson))
			{
				this.lblToPerson.Text = WebUtil.GetUserNamesComma(intendanceMaster.SettleToPerson);
			}
			this.lblSettleExplain.Text = model.SettleExplain;
			this.lblToCause.Text = model.ToCause;
			this.lbl1.Text = userNamesComma + "解决问题的回复时间 " + model.SettleDate;
		}
		this.InfoGuid = model.NoteId;
	}
	protected EPM_IntendanceInfo GetIntendanceInfo()
	{
		EPM_IntendanceInfo ePM_IntendanceInfo = new EPM_IntendanceInfo();
		string strWhere = " IntendanceGuid='" + this.IntendanceGuid + "'";
		DataTable list = this.IntendanceInfoAction.GetList(1, strWhere, "AskQuestionsDate DESC ");
		ePM_IntendanceInfo.NoteId = new Guid(list.Rows[0]["NoteId"].ToString());
		ePM_IntendanceInfo.IntendanceGuid = this.IntendanceGuid;
		ePM_IntendanceInfo.SettleDate = new DateTime?(DateTime.Now);
		ePM_IntendanceInfo.SettleExplain = this.txtSettleExplain.Text.Trim();
		ePM_IntendanceInfo.SettleYhdm = base.UserCode;
		ePM_IntendanceInfo.SettleToPerson = this.hdfusercode.Value;
		ePM_IntendanceInfo.ToCause = this.txtToCause.Text.Trim();
		ePM_IntendanceInfo.QuestionTag = new int?(1);
		return ePM_IntendanceInfo;
	}
	protected EPM_IntendanceInfo GetIntendanceInfo1()
	{
		return new EPM_IntendanceInfo
		{
			NoteId = Guid.NewGuid(),
			IntendanceGuid = this.IntendanceGuid,
			AskQuestionsYhdm = base.UserCode,
			QuestionExplain = this.txtToCause.Text.Trim(),
			AskQuestionsDate = new DateTime?(DateTime.Now),
			QuestionTag = new int?(0)
		};
	}
	protected void btnSettling_Click(object sender, EventArgs e)
	{
		if (this.flag != 0)
		{
			base.Response.Redirect(string.Concat(new object[]
			{
				"PhotosCheckInEdit.aspx?op=v&prj=",
				this.ProjectCode,
				"&pn=",
				this.ProjectName,
				"&IntendanceGuid=",
				this.IntendanceGuid
			}));
			return;
		}
		EPM_IntendanceInfo intendanceInfo = this.GetIntendanceInfo();
		EPM_IntendanceInfo intendanceInfo2 = this.GetIntendanceInfo1();
		int num = this.IntendanceInfoAction.Update(intendanceInfo);
		int num2 = this.IntendanceInfoAction.Add(intendanceInfo2, base.UserCode);
		int num3 = this.intendanceMasterAction.Update(this.IntendanceGuid.ToString(), 1, this.hdfusercode.Value);
		if (num == 1 && num3 == 1 && num2 == 1)
		{
			this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.tabSuccess({ parentName: '_PhotosCheckInList2' });</script>");
			return;
		}
		this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.alert('保存失败');</script>");
	}
	protected void btnSettled_Click(object sender, EventArgs e)
	{
		if (this.flag == 0)
		{
			if (!(this.txtSettleExplain.Text != ""))
			{
				this.js.Text = "alert('解决情况不能为空！')";
				return;
			}
			EPM_IntendanceInfo intendanceInfo = this.GetIntendanceInfo();
			int num = this.IntendanceInfoAction.Update(intendanceInfo);
			int num2 = this.intendanceMasterAction.Update(this.IntendanceGuid.ToString(), 2);
			if (num != 1 || num2 != 1)
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.alert('保存失败');</script>");
				return;
			}
			if (this.intendanceMasterAction.DelDoList("2", this.IntendanceGuid.ToString(), base.UserCode) == 1 && this.intendanceMasterAction.AddDoList("1", this.IntendanceGuid.ToString(), this.intendanceMasterAction.GetIntendanceMaster(this.IntendanceGuid).OpYhdm) == 1)
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.tabSuccess({ parentName: '_PhotosCheckInList2' });</script>");
				return;
			}
		}
		else
		{
			int num3 = this.intendanceMasterAction.Update(this.IntendanceGuid.ToString(), 3);
			if (num3 == 1)
			{
				if (this.intendanceMasterAction.DelDoList("1", this.IntendanceGuid.ToString(), base.UserCode) == 1)
				{
					this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.tabSuccess({ parentName: '_PhotosCheckInList2' });</script>");
					return;
				}
			}
			else
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.alert('保存失败');</script>");
			}
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (this.flag != 0)
		{
			base.Response.Redirect(string.Concat(new object[]
			{
				"PhotosCheckInEdit.aspx?op=v&prj=",
				this.ProjectCode,
				"&pn=",
				this.ProjectName,
				"&IntendanceGuid=",
				this.IntendanceGuid
			}));
			return;
		}
		EPM_IntendanceInfo intendanceInfo = this.GetIntendanceInfo();
		int num = this.IntendanceInfoAction.Update(intendanceInfo);
		int num2 = this.intendanceMasterAction.Update(this.IntendanceGuid.ToString(), 0, this.hdfusercode.Value);
		if (num == 1 && num2 == 1)
		{
			this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.tabSuccess({ parentName: '_PhotosCheckInList2' });</script>");
			return;
		}
		this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.alert('保存失败');</script>");
	}
	[WebMethod]
	public static string GetSettleItem(string IntendanceGuid)
	{
		return EPM_IntendanceInfoAction.getJson(IntendanceGuid, 1);
	}
	[WebMethod]
	public static string GetPhotoItem(string infoGuid)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string sqlString = "select * from OPM_EPCM_IntendancePhotoList where InfoGuid='" + infoGuid + "'";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		foreach (DataRow dataRow in dataTable.Rows)
		{
			stringBuilder.Append("'" + dataRow["NoteID"] + "',");
		}
		string text = stringBuilder.ToString().Trim(new char[]
		{
			','
		});
		if (text.Length == 0)
		{
			return string.Empty;
		}
		return EPC_ProjectCost_PhotosCheckIn_SettleQuestionEdit.getJosn(text);
	}
	private static string getJosn(string annexCodes)
	{
		List<IntendancePhotoList> list = new List<IntendancePhotoList>();
		string sqlString = "SELECT a.*,b.* FROM XPM_Basic_Thumbnai as a inner join OPM_EPCM_IntendancePhotoList as b on a.ThumbnaiCode=b.NoteID where ThumbnaiCode in(" + annexCodes + ")";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		StringBuilder stringBuilder = new StringBuilder();
		foreach (DataRow dataRow in dataTable.Rows)
		{
			IntendancePhotoList intendancePhotoList = new IntendancePhotoList();
			intendancePhotoList.ThumbnaImgPath = ((dataRow["ThumbnaImgPath"] as string) ?? string.Empty);
			intendancePhotoList.ThumbnaName = ((dataRow["ThumbnaName"] as string) ?? string.Empty);
			intendancePhotoList.PhotoNumber = ((dataRow["PhotoNumber"] as string) ?? string.Empty);
			intendancePhotoList.PhotoExplain = ((dataRow["PhotoExplain"] as string) ?? string.Empty);
			intendancePhotoList.InfoGuid = new Guid(dataRow["infoguid"].ToString());
			intendancePhotoList.PhotoType = Convert.ToInt32(dataRow["photoType"]);
			if (dataRow["NoteId"] != DBNull.Value)
			{
				intendancePhotoList.NoteId = new Guid(dataRow["NoteId"].ToString());
			}
			list.Add(intendancePhotoList);
			stringBuilder.Remove(0, stringBuilder.Length);
		}
		JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
		return javaScriptSerializer.Serialize(list);
	}
	[WebMethod]
	public static string DelPhotosItem(string noteId)
	{
		IntendancePhotoListAction intendancePhotoListAction = new IntendancePhotoListAction();
		IntendancePhotoList singlePhotoInfo = intendancePhotoListAction.GetSinglePhotoInfo(new Guid(noteId));
		string result = "";
		if (singlePhotoInfo != null && intendancePhotoListAction.DelAnnex(singlePhotoInfo) > 0)
		{
			MakeThumbnail makeThumbnail = new MakeThumbnail();
			if (makeThumbnail.DelThumbnai(singlePhotoInfo.NoteId.ToString()))
			{
				result = "1";
			}
			else
			{
				result = "删除失败";
			}
		}
		return result;
	}
}


