using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Personnel_EmployeeView : BasePage, IRequiresSessionState
{
	public new string UserCode
	{
		get
		{
			object obj = this.ViewState["UserCode"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["UserCode"] = value;
		}
	}
	public string CorpCode
	{
		get
		{
			object obj = this.ViewState["CorpCode"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["CorpCode"] = value;
		}
	}
	public int DeptID
	{
		get
		{
			object obj = this.ViewState["DeptID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["DeptID"] = value;
		}
	}
	public int OldDeptID
	{
		get
		{
			object obj = this.ViewState["OldDeptID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["OldDeptID"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["cc"].ToString() != "")
			{
				this.DeptID = System.Convert.ToInt32(base.Request.QueryString["cc"]);
				this.OldDeptID = System.Convert.ToInt32(base.Request.QueryString["cc"]);
			}
			else
			{
				this.DeptID = 0;
				this.OldDeptID = 0;
			}
			this.CorpCode = this.Session["CorpCode"].ToString();
			this.Labbm.Text = PersonnelAction.getDeptName(System.Convert.ToInt32(this.DeptID.ToString()));
			this.UserCode = base.Request.QueryString["uc"].ToString();
			this.setData(this.UserCode);
			this.Bind_Duty();
		}
	}
	private void Bind_Duty()
	{
		this.GVDuty.DataSource = HRPersonnelCommonAction.GetDutyInfo(this.UserCode);
		this.GVDuty.DataBind();
	}
	protected void setData(string userCode)
	{
		DataTable dataTable = PersonnelAction.QueryPersonnelchInfoById(userCode);
		if (dataTable.Rows.Count == 1)
		{
			if (dataTable.Rows[0]["SuperordinateDuty"].ToString() != "")
			{
				int arg_5A_0 = (int)dataTable.Rows[0]["SuperordinateDuty"];
			}
			this.Labbm.Text = PersonnelAction.getDeptName(System.Convert.ToInt32(this.DeptID.ToString()));
			this.labUserCode.Text = dataTable.Rows[0]["userCode"].ToString();
			this.labname.Text = dataTable.Rows[0]["v_xm"].ToString();
			this.labAge.Text = dataTable.Rows[0]["age"].ToString();
			if (dataTable.Rows[0]["Sex"].ToString() != "")
			{
				if (dataTable.Rows[0]["Sex"].ToString() == "1")
				{
					this.labSex.Text = "男";
				}
				else
				{
					this.labSex.Text = "女";
				}
			}
			this.LabNation.Text = dataTable.Rows[0]["Nation"].ToString();
			this.LabEducational.Text = dataTable.Rows[0]["EducationalBackground"].ToString();
			this.Labhigt.Text = dataTable.Rows[0]["Stature"].ToString();
			this.LabDuty.Text = dataTable.Rows[0]["dutyname"].ToString();
			if (dataTable.Rows[0]["PositionLevel"].ToString() != "")
			{
				this.LabPositionLevel.Text = dataTable.Rows[0]["PositionLevel"].ToString() + "级";
			}
			if (dataTable.Rows[0]["PostAndRank"].ToString() != "")
			{
				this.LabPostAndRank.Text = OAOfficeCommonClas.GetPostAndRank(dataTable.Rows[0]["PostAndRank"].ToString());
			}
			this.LabAddress.Text = dataTable.Rows[0]["Address"].ToString();
			this.LabRegisteredPlace.Text = dataTable.Rows[0]["RegisteredPlace"].ToString();
			if (dataTable.Rows[0]["ClassID"].ToString() != "")
			{
				this.LabClassID.Text = DocumentAction.QueryOneDocumentClass(int.Parse(dataTable.Rows[0]["ClassID"].ToString())).Rows[0]["classname"].ToString();
			}
			this.LabComputeLevel.Text = dataTable.Rows[0]["ComputeLevel"].ToString();
			this.LabEnglishLevel.Text = dataTable.Rows[0]["EnglishLevel"].ToString();
			if (dataTable.Rows[0]["Marriage"].ToString() != "")
			{
				this.LabMarriage.Text = dataTable.Rows[0]["Marriage"].ToString();
			}
			this.LabPostcode.Text = dataTable.Rows[0]["Postcode"].ToString();
			this.LabTel.Text = dataTable.Rows[0]["Tel"].ToString();
			this.LabDriveLevel.Text = dataTable.Rows[0]["Drivelevel"].ToString();
			if (!string.IsNullOrEmpty(dataTable.Rows[0]["ExpectationSalary"].ToString()))
			{
				this.LabtExpectationSalary.Text = System.Convert.ToInt32(dataTable.Rows[0]["ExpectationSalary"]).ToString();
			}
			this.LabCommunicateAddress.Text = dataTable.Rows[0]["CommunicateAddress"].ToString();
			if (dataTable.Rows[0]["UserPhoto"].ToString() != "")
			{
				this.Image1.ImageUrl = "~" + dataTable.Rows[0]["UserPhoto"].ToString();
				this.annexName.InnerText = dataTable.Rows[0]["v_xm"].ToString() + "照片";
			}
			else
			{
				this.Image1.ImageUrl = "~/images/PhotoDefault.gif";
			}
			this.LabMobilePhoneCode.Text = dataTable.Rows[0]["MobilePhoneCode"].ToString();
			if (!string.IsNullOrEmpty(dataTable.Rows[0]["Birthday"].ToString()))
			{
				this.LabBirthday.Text = System.DateTime.Parse(dataTable.Rows[0]["Birthday"].ToString()).ToShortDateString();
			}
			this.LabIDCard.Text = dataTable.Rows[0]["IDCard"].ToString();
			this.LabPoliticsFace.Text = dataTable.Rows[0]["PoliticsFace"].ToString();
			if (!string.IsNullOrEmpty(dataTable.Rows[0]["JoinPartyDate"].ToString()))
			{
				this.LabJoinPartyDate.Text = System.DateTime.Parse(dataTable.Rows[0]["JoinPartyDate"].ToString()).ToShortDateString();
			}
			this.LabJoinCorpMode.Text = dataTable.Rows[0]["JoinCorpMode"].ToString();
			this.LabIntroducer.Text = dataTable.Rows[0]["Introducer"].ToString();
			this.LabSpecialty.Text = dataTable.Rows[0]["Specialty"].ToString();
			this.LabGraduateSchool.Text = dataTable.Rows[0]["GraduateSchool"].ToString();
			this.LabLevel.Text = dataTable.Rows[0]["Level"].ToString();
			if (!string.IsNullOrEmpty(dataTable.Rows[0]["BeginWorkDate"].ToString()))
			{
				this.LabBeginWorkDate.Text = System.DateTime.Parse(dataTable.Rows[0]["BeginWorkDate"].ToString()).ToShortDateString();
			}
			if (!string.IsNullOrEmpty(dataTable.Rows[0]["EnterCorpDate"].ToString()))
			{
				this.LabEnterCorpDate.Text = System.DateTime.Parse(dataTable.Rows[0]["EnterCorpDate"].ToString()).ToShortDateString();
			}
			this.LabtPostAndCompetency.Text = dataTable.Rows[0]["PostAndCompetency"].ToString();
			this.Labnowstata.Text = dataTable.Rows[0]["statename"].ToString();
			if (dataTable.Rows[0]["EndowmentInsurance"].ToString() == "1")
			{
				this.cblInsurance.Items[0].Selected = true;
			}
			if (dataTable.Rows[0]["MedicareInsurance"].ToString() == "1")
			{
				this.cblInsurance.Items[1].Selected = true;
			}
			if (dataTable.Rows[0]["UnemploymentInsurance"].ToString() == "1")
			{
				this.cblInsurance.Items[2].Selected = true;
			}
			if (dataTable.Rows[0]["InjuryInsurance"].ToString() == "1")
			{
				this.cblInsurance.Items[3].Selected = true;
			}
			if (dataTable.Rows[0]["HousingAccumulationFund"].ToString() == "1")
			{
				this.cblInsurance.Items[4].Selected = true;
			}
			if (dataTable.Rows[0]["PersonSuddennessInsurance"].ToString() == "1")
			{
				this.cblInsurance.Items[5].Selected = true;
			}
			if (!string.IsNullOrEmpty(dataTable.Rows[0]["conEndDate"].ToString()))
			{
				this.labconEndDate.Text = System.Convert.ToDateTime(dataTable.Rows[0]["conEndDate"]).ToString("yyyy-MM-dd");
			}
			if (!string.IsNullOrEmpty(dataTable.Rows[0]["FormalDate"].ToString()))
			{
				this.labFormalData.Text = System.Convert.ToDateTime(dataTable.Rows[0]["FormalDate"].ToString()).ToString("yyyy-MM-dd");
			}
			this.laburgentCellMan.Text = dataTable.Rows[0]["urgentCellMan"].ToString();
			this.labucmConcern.Text = dataTable.Rows[0]["ucmConcern"].ToString();
			this.labucmTel.Text = dataTable.Rows[0]["ucmTel"].ToString();
			if (dataTable.Rows[0]["rdeNature"].ToString() != "")
			{
				this.labrdeNature.Text = dataTable.Rows[0]["rdeNature"].ToString();
			}
			if (dataTable.Rows[0]["IsChargeMan"].ToString() == "True")
			{
				this.labChargeMan.Text = "是";
				return;
			}
			this.labChargeMan.Text = "否";
		}
	}
}


