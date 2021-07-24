using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EmployeeAdd : BasePage, IRequiresSessionState
	{
		private PTDUTYAction ptDutyAction = new PTDUTYAction();

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
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.BindDdl();
				if (base.Request.QueryString["cc"].ToString() != "")
				{
					this.DeptID = System.Convert.ToInt32(base.Request.QueryString["cc"]);
				}
				else
				{
					this.DeptID = 0;
				}
				this.CorpCode = this.Session["corpcode"].ToString();
				this.hdnDept.Value = this.DeptID.ToString();
				this.txtDeptName.Text = PersonnelAction.getDeptName(this.DeptID);
				this.UserCode = base.Request.QueryString["uc"].ToString();
				this.Combox_DataBind();
				this.dbEnterCorpDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
				this.ddltDuty.DataBind();
				if (this.ddltDuty.Items.Count < 1)
				{
					this.JS.Text = "top.ui.alert('请先在该部门下增加岗位!');";
					this.BtnSave.Enabled = false;
				}
			}
		}
		public void BindDdl()
		{
			DataTable ddlPositionLevel = this.ptDutyAction.GetDdlPositionLevel();
			this.ddltPositionLevel.Items.Clear();
			for (int i = 0; i < ddlPositionLevel.Rows.Count; i++)
			{
				string text = ConverRMB.ConvertU(System.Convert.ToInt32(ddlPositionLevel.Rows[i][0].ToString())) + "级";
				this.ddltPositionLevel.Items.Add(new ListItem(text, ddlPositionLevel.Rows[i][0].ToString()));
			}
			this.ddltPositionLevel.Items.Insert(0, "");
		}
		protected void BtnSave_Click(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtLogin.Text.Trim()))
			{
				this.JS.Text = "top.ui.alert('系统账户不能为空！');";
				return;
			}
			if (!string.IsNullOrEmpty(this.txtIDCard.Text) && !Regex.IsMatch(this.txtIDCard.Text.Trim(), "([a-zA-Z]|\\d{17}|\\d{7}|\\d{8}(?:\\d|[xX])$)|(^\\d{15}$)"))
			{
				base.RegisterScript("top.ui.alert('输入的身份证格式不正确！');");
				this.txtIDCard.Focus();
				return;
			}
			DataTable dataTable = userManageDb.UserIsExist(this.txtLogin.Text.Trim());
			if (dataTable.Rows.Count > 0)
			{
				this.JS.Text = "top.ui.alert('该系统帐户已存在，请重新填写！');";
				return;
			}
			string[] array = PersonnelAction.setUserCode(this.DeptID.ToString());
			int num = System.Convert.ToInt32(array[0]);
			System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
			hashtable.Add("i_xh", num.ToString());
			hashtable.Add("v_yhdm", SqlStringConstructor.GetQuotedString(array[1]));
			hashtable.Add("v_xm", SqlStringConstructor.GetQuotedString(this.txtName.Text));
			hashtable.Add("RelationCorp", SqlStringConstructor.GetQuotedString(this.CorpCode));
			hashtable.Add("i_bmdm", this.DeptID.ToString());
			hashtable.Add("I_DUTYID", this.ddltDuty.SelectedValue);
			int num2;
			if (this.txtAge.Text != "")
			{
				num2 = System.Convert.ToInt32(this.txtAge.Text);
			}
			else
			{
				num2 = 0;
			}
			hashtable.Add("Age", num2.ToString());
			hashtable.Add("Sex", SqlStringConstructor.GetQuotedString(this.ddltSex.SelectedValue));
			hashtable.Add("Nation", SqlStringConstructor.GetQuotedString(this.CombNation.Text));
			hashtable.Add("EducationalBackground", SqlStringConstructor.GetQuotedString(this.CombEducationalBackground.Text.Trim()));
			hashtable.Add("Stature", SqlStringConstructor.GetQuotedString(this.txtStature.Text));
			if (this.ddltPositionLevel.SelectedValue != "")
			{
				hashtable.Add("PositionLevel", this.ddltPositionLevel.SelectedValue);
			}
			hashtable.Add("ClassID", this.ddltClassID.SelectedValue);
			hashtable.Add("Address", SqlStringConstructor.GetQuotedString(this.txtAddress.Text));
			hashtable.Add("RegisteredPlace", SqlStringConstructor.GetQuotedString(this.txtRegisteredPlace.Text));
			hashtable.Add("PostAndRank", SqlStringConstructor.GetQuotedString(this.ddltPostAndRank.SelectedValue));
			hashtable.Add("ComputeLevel", SqlStringConstructor.GetQuotedString(this.CombComputeLevel.Text));
			hashtable.Add("EnglishLevel", SqlStringConstructor.GetQuotedString(this.CombEnglishLevel.Text));
			hashtable.Add("DriveLevel", SqlStringConstructor.GetQuotedString(this.CombDriveLevel.Text));
			hashtable.Add("Marriage", SqlStringConstructor.GetQuotedString(this.ddltMarriage.SelectedValue));
			hashtable.Add("Postcode", SqlStringConstructor.GetQuotedString(this.txtPostcode.Text));
			hashtable.Add("Tel", SqlStringConstructor.GetQuotedString(this.txtTel.Text));
			if (this.txtExpectationSalary.Text != "")
			{
				hashtable.Add("ExpectationSalary", System.Convert.ToDecimal(this.txtExpectationSalary.Text).ToString());
			}
			hashtable.Add("CommunicateAddress", SqlStringConstructor.GetQuotedString(this.txtCommunicateAddress.Text));
			hashtable.Add("State", "1");
			hashtable.Add("c_sfyx", SqlStringConstructor.GetQuotedString("y"));
			if (this.FileUpload1.HasFile)
			{
				HttpPostedFile postedFile = this.FileUpload1.PostedFile;
				com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
				string[] array2 = fileUpload.Upload(postedFile, 5);
				hashtable.Add("UserPhoto", SqlStringConstructor.GetQuotedString(array2[1]));
			}
			hashtable.Add("MobilePhoneCode", SqlStringConstructor.GetQuotedString(this.txtMobilePhoneCode.Text));
			if (!string.IsNullOrEmpty(this.dbBirthday.Text))
			{
				hashtable.Add("Birthday", SqlStringConstructor.GetQuotedString(this.dbBirthday.Text.Trim()));
			}
			hashtable.Add("IDCard", SqlStringConstructor.GetQuotedString(this.txtIDCard.Text));
			hashtable.Add("PoliticsFace", SqlStringConstructor.GetQuotedString(this.txtPoliticsFace.Text));
			if (!string.IsNullOrEmpty(this.dbJoinPartyDate.Text))
			{
				hashtable.Add("JoinPartyDate", SqlStringConstructor.GetQuotedString(this.dbJoinPartyDate.Text));
			}
			hashtable.Add("JoinCorpMode", SqlStringConstructor.GetQuotedString(this.txtJoinCorpMode.Text));
			hashtable.Add("Introducer", SqlStringConstructor.GetQuotedString(this.txtIntroducer.Text));
			hashtable.Add("Specialty", SqlStringConstructor.GetQuotedString(this.txtSpecialty.Text));
			hashtable.Add("GraduateSchool", SqlStringConstructor.GetQuotedString(this.txtGraduateSchool.Text));
			hashtable.Add("Level", SqlStringConstructor.GetQuotedString(this.txtLevel.Text));
			if (!string.IsNullOrEmpty(this.dbBeginWorkDate.Text))
			{
				hashtable.Add("BeginWorkDate", SqlStringConstructor.GetQuotedString(this.dbBeginWorkDate.Text));
			}
			hashtable.Add("PostAndCompetency", SqlStringConstructor.GetQuotedString(this.txtPostAndCompetency.Text));
			if (this.cblInsurance.Items[0].Selected)
			{
				hashtable.Add("EndowmentInsurance", "1");
			}
			else
			{
				hashtable.Add("EndowmentInsurance", "0");
			}
			if (this.cblInsurance.Items[1].Selected)
			{
				hashtable.Add("MedicareInsurance", "1");
			}
			else
			{
				hashtable.Add("MedicareInsurance", "0");
			}
			if (this.cblInsurance.Items[2].Selected)
			{
				hashtable.Add("UnemploymentInsurance", "1");
			}
			else
			{
				hashtable.Add("UnemploymentInsurance", "0");
			}
			if (this.cblInsurance.Items[3].Selected)
			{
				hashtable.Add("InjuryInsurance", "1");
			}
			else
			{
				hashtable.Add("InjuryInsurance", "0");
			}
			if (this.cblInsurance.Items[4].Selected)
			{
				hashtable.Add("HousingAccumulationFund", "1");
			}
			else
			{
				hashtable.Add("HousingAccumulationFund", "0");
			}
			if (this.cblInsurance.Items[5].Selected)
			{
				hashtable.Add("PersonSuddennessInsurance", "1");
			}
			else
			{
				hashtable.Add("PersonSuddennessInsurance", "0");
			}
			hashtable.Add("rdeNature", SqlStringConstructor.GetQuotedString(this.ddlrdeNature.SelectedValue.ToString()));
			if (!string.IsNullOrEmpty(this.txtconEndDate.Text))
			{
				hashtable.Add("conEndDate", SqlStringConstructor.GetQuotedString(this.txtconEndDate.Text));
			}
			hashtable.Add("urgentCellMan", SqlStringConstructor.GetQuotedString(this.txturgentCellMan.Text));
			hashtable.Add("ucmConcern", SqlStringConstructor.GetQuotedString(this.txtucmConcern.Text));
			hashtable.Add("ucmTel", SqlStringConstructor.GetQuotedString(this.txtucmTel.Text));
			if (!string.IsNullOrEmpty(this.txtFormalData.Text))
			{
				hashtable.Add("FormalDate", SqlStringConstructor.GetQuotedString(this.txtFormalData.Text));
			}
			hashtable.Add("userCode", SqlStringConstructor.GetQuotedString(this.txtUserCode.Text));
			if (!this.chbIsChargeMan.Checked)
			{
				hashtable.Add("IsChargeMan", SqlStringConstructor.GetQuotedString("false"));
			}
			else
			{
				hashtable.Add("IsChargeMan", SqlStringConstructor.GetQuotedString("true"));
			}
			if (this.dbEnterCorpDate.Text != "")
			{
				hashtable.Add("EnterCorpDate", SqlStringConstructor.GetQuotedString(System.Convert.ToDateTime(this.dbEnterCorpDate.Text).ToShortDateString()));
			}
			hashtable.Add("RTXID", SqlStringConstructor.GetQuotedString(this.txtLogin.Text));
			if (!PersonnelAction.AddPersonnel(hashtable))
			{
				this.JS.Text = "top.ui.alert('保存失败！');";
				return;
			}
			if (!this.chbIsChargeMan.Checked)
			{
				userManageDb.UserAdd(hashtable, this.txtLogin.Text.Trim());
				this.JS.Text = "successed();";
				return;
			}
			if (PersonnelAction.UpdatePT_yhmc(hashtable["v_yhdm"].ToString(), this.DeptID.ToString()))
			{
				userManageDb.UserAdd(hashtable, this.txtLogin.Text.Trim());
				this.JS.Text = "successed();";
				return;
			}
			this.JS.Text = "top.ui.alert('保存失败！');";
		}
		protected void Combox_DataBind()
		{
			this.CombNation.Values = PersonnelAction.GetDistinctRegister("Nation");
			this.CombComputeLevel.Values = PersonnelAction.GetDistinctRegister("ComputeLevel");
			this.CombDriveLevel.Values = PersonnelAction.GetDistinctRegister("DriveLevel");
			this.CombEducationalBackground.Values = PersonnelAction.GetDistinctRegister("EducationalBackground");
			this.CombEnglishLevel.Values = PersonnelAction.GetDistinctRegister("EnglishLevel");
		}
		protected void ddltPositionLevel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SqlPostAndRank.DataBind();
			this.ddltPostAndRank.DataBind();
		}
		protected void ImageBtn_Click(object sender, ImageClickEventArgs e)
		{
		}
		protected void butGetPy_Click(object sender, System.EventArgs e)
		{
			if (this.txtName.Text.Trim().ToString().Length != 0)
			{
				string chineseSpell = StrToPinyin.GetChineseSpell(this.txtName.Text.Trim().ToString());
				this.txtLogin.Text = chineseSpell.ToLower();
				this.checkISright();
				this.txtLogin.Focus();
			}
		}
		public void checkISright()
		{
			bool flag = person.checkNameisExit(this.txtLogin.Text.Trim());
			if (flag)
			{
				this.Image2.Visible = true;
				this.Image2.ImageUrl = "~/images/check_error.gif";
				this.Image2.ToolTip = "账号重复！请更改系统账号！";
				return;
			}
			this.Image2.Visible = true;
			this.Image2.ImageUrl = "~/images/check_right.gif";
		}
	}


