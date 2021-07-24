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
public partial class EPC_ProjectCost_PhotosCheckIn_PhotosCheckInEdit : SubwayBasePage, IRequiresSessionState
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
	public string opType
	{
		get
		{
			return (string)this.ViewState["opType"];
		}
		set
		{
			this.ViewState["opType"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["op"] == null || base.Request["prj"] == null || base.Request["pn"] == null)
			{
				this.js.Text = "alert('参数有误！');";
				return;
			}
			this.ProjectCode = new Guid(base.Request.QueryString["prj"]);
			this.opType = base.Request.QueryString["op"];
			this.lblPrjName.Text = base.Request.QueryString["pn"];
			this.ProjectName = base.Request.QueryString["pn"];
			if (this.aa.DelPhotoList(base.UserCode) == 1)
			{
				if (this.opType == "add")
				{
					this.IntendanceGuid = default(Guid);
					this.InfoGuid = default(Guid);
					this.txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				}
				else
				{
					if (this.opType == "upd")
					{
						this.lblTitle.Text = "现场拍照监督修改";
						this.IntendanceGuid = new Guid(base.Request.QueryString["IntendanceGuid"]);
					}
					else
					{
						this.IntendanceGuid = new Guid(base.Request.QueryString["IntendanceGuid"]);
					}
				}
				this.DataBindToPage();
				this.Drop_QuestionType_Bind();
			}
			this.hdnUserCode.Value = base.UserCode;
		}
	}
	private void Drop_QuestionType_Bind()
	{
		this.ddlType.DataSource = this.intendanceMasterAction.GetTypeList();
		this.ddlType.DataBind();
	}
	protected void DataBindToPage()
	{
		IntendanceMaster intendanceMaster = this.intendanceMasterAction.GetIntendanceMaster(this.IntendanceGuid);
		EPM_IntendanceInfo model = this.IntendanceInfoAction.GetModel(this.IntendanceGuid);
		userManageDb userManageDb = new userManageDb();
		if (this.opType != "v" && this.opType != "add")
		{
			this.txtQuestionTitle.Text = intendanceMaster.QuestionTitle;
			this.txtDate.Text = intendanceMaster.BookInDate.ToString("yyyy-MM-dd");
			this.ddlType.SelectedValue = intendanceMaster.QuestionTypeId.ToString();
			this.txtPerson.Value = WebUtil.GetUserNamesComma(intendanceMaster.SettleYhdm);
			this.ManagerCode.Value = intendanceMaster.SettleYhdm;
			this.txtQuestion.Text = model.QuestionExplain;
			this.InfoGuid = model.NoteId;
			return;
		}
		if (this.opType == "v")
		{
			this.lbltype.Visible = true;
			this.lblQuestionTitle.Visible = true;
			this.lblDate.Visible = true;
			this.txtDate.Visible = false;
			this.ddlType.Visible = false;
			this.txtPerson.Visible = false;
			this.txtQuestionTitle.Visible = false;
			this.Img3.Visible = false;
			this.lblQuestionTitle.Text = intendanceMaster.QuestionTitle;
			this.lblDate.Text = intendanceMaster.BookInDate.ToString("yyyy-MM-dd");
			this.lbltype.Text = this.intendanceMasterAction.GetQuestionNameById(intendanceMaster.QuestionTypeId);
			this.lblPerson.Text = userManageDb.GetUserName(intendanceMaster.SettleYhdm);
			this.InfoGuid = model.NoteId;
		}
	}
	protected IntendanceMaster GetIntendanceMaster()
	{
		IntendanceMaster intendanceMaster = new IntendanceMaster();
		if (this.opType == "add")
		{
			intendanceMaster.IntendanceGuid = Guid.NewGuid();
			this.IntendanceGuid = intendanceMaster.IntendanceGuid;
			intendanceMaster.QuestionTag = 0;
		}
		else
		{
			intendanceMaster.IntendanceGuid = this.IntendanceGuid;
		}
		intendanceMaster.PrjGuid = this.ProjectCode;
		intendanceMaster.QuestionTitle = this.txtQuestionTitle.Text;
		intendanceMaster.BookInDate = Convert.ToDateTime(this.txtDate.Text);
		intendanceMaster.SettleState = 0;
		intendanceMaster.SettleYhdm = this.hdfusercode.Value;
		if (!intendanceMaster.SettleYhdm.Contains("00000000"))
		{
			IntendanceMaster expr_A2 = intendanceMaster;
			expr_A2.SettleYhdm += "00000000,";
		}
		intendanceMaster.QuestionTypeId = Convert.ToInt32(this.ddlType.SelectedValue);
		intendanceMaster.OpYhdm = base.UserCode;
		return intendanceMaster;
	}
	protected EPM_IntendanceInfo GetIntendanceInfo()
	{
		EPM_IntendanceInfo ePM_IntendanceInfo = new EPM_IntendanceInfo();
		if (this.opType == "add" || this.opType == "v")
		{
			ePM_IntendanceInfo.NoteId = Guid.NewGuid();
		}
		else
		{
			ePM_IntendanceInfo.NoteId = this.IntendanceInfoAction.GetModel(this.IntendanceGuid).NoteId;
		}
		ePM_IntendanceInfo.IntendanceGuid = this.IntendanceGuid;
		ePM_IntendanceInfo.AskQuestionsYhdm = base.UserCode;
		ePM_IntendanceInfo.AskQuestionsDate = new DateTime?(DateTime.Now);
		ePM_IntendanceInfo.QuestionExplain = this.txtQuestion.Text;
		ePM_IntendanceInfo.SettleYhdm = this.hdfusercode.Value;
		if (!ePM_IntendanceInfo.SettleYhdm.Contains("00000000"))
		{
			EPM_IntendanceInfo expr_B0 = ePM_IntendanceInfo;
			expr_B0.SettleYhdm += "00000000,";
		}
		ePM_IntendanceInfo.QuestionTag = new int?(0);
		return ePM_IntendanceInfo;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (this.opType == "add" || this.opType == "v")
		{
			int num;
			if (this.opType == "v")
			{
				num = this.intendanceMasterAction.Update(this.IntendanceGuid.ToString(), 0);
			}
			else
			{
				num = this.intendanceMasterAction.Add(this.GetIntendanceMaster(), base.UserCode);
			}
			int num2 = this.IntendanceInfoAction.Add(this.GetIntendanceInfo(), base.UserCode);
			if (num != 1 || num2 != 1)
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.alert('保存失败');</script>");
				return;
			}
			if (this.intendanceMasterAction.DelDoList("1", this.IntendanceGuid.ToString(), base.UserCode) == 1 && this.intendanceMasterAction.AddDoList("2", this.IntendanceGuid.ToString(), this.intendanceMasterAction.GetIntendanceMaster(this.IntendanceGuid).SettleYhdm) == 1)
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.tabSuccess({ parentName: '_PhotosCheckInList2' });</script>");
				return;
			}
		}
		else
		{
			if (this.opType == "upd")
			{
				int num3 = this.intendanceMasterAction.Update(this.GetIntendanceMaster());
				int num4 = this.IntendanceInfoAction.Update(this.GetIntendanceInfo());
				if (num3 == 1 && num4 == 1)
				{
					this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.tabSuccess({ parentName: '_PhotosCheckInList2' });</script>");
					return;
				}
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">top.ui.alert('保存失败');</script>");
			}
		}
	}
	[WebMethod]
	public static string GetSettleItem(string IntendanceGuid)
	{
		return EPM_IntendanceInfoAction.getJson(IntendanceGuid, 0);
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
		return EPC_ProjectCost_PhotosCheckIn_PhotosCheckInEdit.getJosn(text);
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
			intendancePhotoList.PhotoType = Convert.ToInt32(dataRow["photoType"]);
			intendancePhotoList.InfoGuid = new Guid(dataRow["InfoGuid"].ToString());
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


