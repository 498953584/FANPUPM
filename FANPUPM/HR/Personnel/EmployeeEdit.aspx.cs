using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using Microsoft.Web.UI.WebControls;
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
	public partial class EmployeeEdit : BasePage, IRequiresSessionState
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
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["cc"].ToString() != "")
				{
					this.DeptID = Convert.ToInt32(base.Request.QueryString["cc"]);
					this.OldDeptID = Convert.ToInt32(base.Request.QueryString["cc"]);
				}
				else
				{
					this.DeptID = 0;
					this.OldDeptID = 0;
				}
				this.CorpCode = this.Session["CorpCode"].ToString();
				this.hdnDept.Value = this.DeptID.ToString();
				this.txtDeptName.Text = PersonnelAction.getDeptName(Convert.ToInt32(this.hdnDept.Value));
				this.UserCode = base.Request.QueryString["uc"].ToString();
				this.Combox_DataBind();
				this.Bind_Duty();
				this.SqlDuty.SelectCommand = this.SqlDuty.SelectCommand.Replace("b.i_bmdm = @DeptID", "b.i_bmdm = " + this.hdnDept.Value);
				this.ddltDuty.DataBind();
				this.ddltClassID.DataBind();
				this.setData(this.UserCode);
				this.gvExperience.ShowFooter = false;
				this.gvRewardsAndPunishment.ShowFooter = false;
				this.gvEducation.ShowFooter = false;
				this.gvTrain.ShowFooter = false;
				this.gvFamilyMembers.ShowFooter = false;
			}
			this.btnAddDuty.Attributes["onclick"] = "javascript:if(!OpenDutyWin('" + this.UserCode + "')) return false;";
			this.btnDelDuty.Attributes["onclick"] = "javascript:if(!confirm('确定删除该条记录吗?')) return false;";
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("v_xm", SqlStringConstructor.GetQuotedString(this.txtName.Text));
			hashtable.Add("RelationCorp", SqlStringConstructor.GetQuotedString(this.CorpCode));
			if (this.dbEnterCorpDate.Text != "")
			{
				hashtable.Add("EnterCorpDate", SqlStringConstructor.GetQuotedString(Convert.ToDateTime(this.dbEnterCorpDate.Text).ToShortDateString()));
			}
			hashtable.Add("i_bmdm", this.hdnDept.Value.ToString());
			if (!string.IsNullOrEmpty(this.txtIDCard.Text) && !Regex.IsMatch(this.txtIDCard.Text.Trim(), "([a-zA-Z]|\\d{17}|\\d{7}|\\d{8}(?:\\d|[xX])$)|(^\\d{15}$)"))
			{
				base.RegisterScript("top.ui.alert('输入的身份证格式不正确！');");
				this.txtIDCard.Focus();
				return;
			}
			if (this.ddltDuty.SelectedItem != null)
			{
				hashtable.Add("I_DUTYID", this.ddltDuty.SelectedValue);
			}
			int num;
			if (this.txtAge.Text != "")
			{
				num = Convert.ToInt32(this.txtAge.Text);
			}
			else
			{
				num = 0;
			}
			hashtable.Add("Age", num.ToString());
			hashtable.Add("Sex", SqlStringConstructor.GetQuotedString(this.ddltSex.SelectedValue));
			hashtable.Add("Nation", SqlStringConstructor.GetQuotedString(this.CombNation.Text));
			hashtable.Add("EducationalBackground", SqlStringConstructor.GetQuotedString(this.CombEducationalBackground.Text));
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
				hashtable.Add("ExpectationSalary", Convert.ToDecimal(this.txtExpectationSalary.Text).ToString());
			}
			hashtable.Add("CommunicateAddress", SqlStringConstructor.GetQuotedString(this.txtCommunicateAddress.Text));
			hashtable.Add("State", "1");
			hashtable.Add("c_sfyx", SqlStringConstructor.GetQuotedString("y"));
			if (this.FileUpload1.HasFile)
			{
				HttpPostedFile postedFile = this.FileUpload1.PostedFile;
				com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
				string[] array = fileUpload.Upload(postedFile, 5);
				hashtable.Add("UserPhoto", SqlStringConstructor.GetQuotedString(array[1]));
			}
			hashtable.Add("MobilePhoneCode", SqlStringConstructor.GetQuotedString(this.txtMobilePhoneCode.Text));
			if (!string.IsNullOrEmpty(this.dbBirthday.Text.Trim()))
			{
				hashtable.Add("Birthday", SqlStringConstructor.GetQuotedString(this.dbBirthday.Text.Trim()));
			}
			hashtable.Add("IDCard", SqlStringConstructor.GetQuotedString(this.txtIDCard.Text));
			if (!string.IsNullOrEmpty(this.dbFormalDate.Text.Trim()))
			{
				hashtable.Add("FormalDate", SqlStringConstructor.GetQuotedString(this.dbFormalDate.Text));
			}
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
			if (!string.IsNullOrEmpty(this.dbBeginWorkDate.Text.Trim()))
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
			if (!this.chbIsChargeMan.Checked)
			{
				hashtable.Add("IsChargeMan", SqlStringConstructor.GetQuotedString("False"));
			}
			else
			{
				hashtable.Add("IsChargeMan", SqlStringConstructor.GetQuotedString("True"));
			}
			hashtable.Add("urgentCellMan", SqlStringConstructor.GetQuotedString(this.txturgentCellMan.Text));
			hashtable.Add("ucmConcern", SqlStringConstructor.GetQuotedString(this.txtucmConcern.Text));
			hashtable.Add("ucmTel", SqlStringConstructor.GetQuotedString(this.txtucmTel.Text));
			hashtable.Add("userCode", SqlStringConstructor.GetQuotedString(this.txtUserCode.Text));
			string where = " where v_yhdm = '" + this.UserCode + "'";
			userManageDb userManageDb = new userManageDb();
			string oldDept = userManageDb.userQuaryDt(this.UserCode).Rows[0]["v_bmqc"].ToString();
			if (PersonnelAction.UpdPersonnel(hashtable, where))
			{
				if (this.chbIsChargeMan.Checked)
				{
					if (PersonnelAction.UpdatePT_yhmc(this.UserCode, this.hdnDept.Value))
					{
						this.JS.Text = "successed('保存');";
					}
					else
					{
						this.JS.Text = "alert('保存失败！');";
					}
				}
				else
				{
					this.JS.Text = "successed('保存');";
				}
				if (this.DeptID != this.OldDeptID)
				{
					userManageDb.InsertRTXSynchronizationDept(this.UserCode, oldDept);
					return;
				}
			}
			else
			{
				this.JS.Text = "alert('保存失败！');";
			}
		}
		protected void Combox_DataBind()
		{
			this.CombNation.Values = PersonnelAction.GetDistinctRegister("Nation");
			this.CombComputeLevel.Values = PersonnelAction.GetDistinctRegister("ComputeLevel");
			this.CombDriveLevel.Values = PersonnelAction.GetDistinctRegister("DriveLevel");
			this.CombEducationalBackground.Values = PersonnelAction.GetDistinctRegister("EducationalBackground");
			this.CombEnglishLevel.Values = PersonnelAction.GetDistinctRegister("EnglishLevel");
		}
		protected void setData(string userCode)
		{
			DataTable dataTable = PersonnelAction.QueryPersonnelById(userCode);
			if (dataTable.Rows.Count == 1)
			{
				if (dataTable.Rows[0]["SuperordinateDuty"].ToString() != "")
				{
					int arg_5A_0 = (int)dataTable.Rows[0]["SuperordinateDuty"];
				}
				this.txtUserCode.Text = dataTable.Rows[0]["userCode"].ToString();
				this.ddltDuty.DataBind();
				this.hdnDept.Value = dataTable.Rows[0]["i_bmdm"].ToString();
				this.txtDeptName.Text = PersonnelAction.getDeptName(Convert.ToInt32(this.hdnDept.Value));
				this.txtName.Text = dataTable.Rows[0]["v_xm"].ToString();
				this.txtAge.Text = dataTable.Rows[0]["age"].ToString();
				if (dataTable.Rows[0]["Sex"].ToString() != "")
				{
					this.ddltSex.SelectedValue = dataTable.Rows[0]["Sex"].ToString();
				}
				this.CombNation.Text = dataTable.Rows[0]["Nation"].ToString();
				this.CombEducationalBackground.Text = dataTable.Rows[0]["EducationalBackground"].ToString();
				this.txtStature.Text = dataTable.Rows[0]["Stature"].ToString();
				if (dataTable.Rows[0]["I_DUTYID"].ToString() != "" && this.ddltDuty.Items.Count > 0)
				{
					this.ddltDuty.SelectedValue = dataTable.Rows[0]["I_DUTYID"].ToString();
				}
				DataTable ddlPositionLevel = this.ptDutyAction.GetDdlPositionLevel();
				this.ddltPositionLevel.Items.Clear();
				for (int i = 0; i < ddlPositionLevel.Rows.Count; i++)
				{
					string text = ConverRMB.ConvertU(Convert.ToInt32(ddlPositionLevel.Rows[i][0].ToString())) + "级";
					this.ddltPositionLevel.Items.Add(new ListItem(text, ddlPositionLevel.Rows[i][0].ToString()));
				}
				this.ddltPositionLevel.Items.Insert(0, "");
				if (dataTable.Rows[0]["PositionLevel"].ToString() != "" && this.ddltPositionLevel.Items.Count > 0)
				{
					this.ddltPositionLevel.SelectedValue = dataTable.Rows[0]["PositionLevel"].ToString();
				}
				string sqlString = "SELECT [RecordID], [PostAndRank] FROM [HR_Org_PostLevel] WHERE ([PositionLevel] = '" + this.ddltPositionLevel.SelectedValue + "')";
				DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
				this.ddltPostAndRank.DataSource = dataSource;
				this.ddltPostAndRank.DataTextField = "PostAndRank";
				this.ddltPostAndRank.DataValueField = "RecordID";
				this.ddltPostAndRank.DataBind();
				if (dataTable.Rows[0]["PostAndRank"].ToString() != "" && this.ddltPostAndRank.Items.Count > 0)
				{
					this.ddltPostAndRank.SelectedValue = dataTable.Rows[0]["PostAndRank"].ToString();
				}
				this.txtAddress.Text = dataTable.Rows[0]["Address"].ToString();
				this.txtRegisteredPlace.Text = dataTable.Rows[0]["RegisteredPlace"].ToString();
				if (dataTable.Rows[0]["ClassID"].ToString() != "" && this.ddltClassID.Items.Count > 0)
				{
					this.ddltClassID.SelectedValue = dataTable.Rows[0]["ClassID"].ToString();
				}
				this.CombComputeLevel.Text = dataTable.Rows[0]["ComputeLevel"].ToString();
				this.CombEnglishLevel.Text = dataTable.Rows[0]["EnglishLevel"].ToString();
				if (dataTable.Rows[0]["Marriage"].ToString() != "" && this.ddltMarriage.Items.Count > 0)
				{
					this.ddltMarriage.SelectedValue = dataTable.Rows[0]["Marriage"].ToString();
				}
				this.txtPostcode.Text = dataTable.Rows[0]["Postcode"].ToString();
				this.txtTel.Text = dataTable.Rows[0]["Tel"].ToString();
				this.CombDriveLevel.Text = dataTable.Rows[0]["Drivelevel"].ToString();
				if (!string.IsNullOrEmpty(dataTable.Rows[0]["ExpectationSalary"].ToString()))
				{
					this.txtExpectationSalary.Text = Convert.ToInt32(dataTable.Rows[0]["ExpectationSalary"]).ToString();
				}
				this.txtCommunicateAddress.Text = dataTable.Rows[0]["CommunicateAddress"].ToString();
				if (dataTable.Rows[0]["UserPhoto"].ToString() != "")
				{
					this.Image1.ImageUrl = "~" + dataTable.Rows[0]["UserPhoto"].ToString();
					this.hdnFilePath.Value = dataTable.Rows[0]["UserPhoto"].ToString();
					this.annexName.InnerText = dataTable.Rows[0]["v_xm"].ToString() + "照片";
					this.tr_add.Visible = false;
					this.tr_edit.Visible = true;
				}
				else
				{
					this.Image1.ImageUrl = "~/images/PhotoDefault.gif";
					this.tr_add.Visible = true;
					this.tr_edit.Visible = false;
				}
				if (dataTable.Rows[0]["IsChargeMan"].ToString() == "False")
				{
					this.chbIsChargeMan.Checked = false;
				}
				else
				{
					this.chbIsChargeMan.Checked = true;
				}
				this.txtMobilePhoneCode.Text = dataTable.Rows[0]["MobilePhoneCode"].ToString();
				if (!string.IsNullOrEmpty(dataTable.Rows[0]["Birthday"].ToString()))
				{
					this.dbBirthday.Text = Convert.ToDateTime(dataTable.Rows[0]["Birthday"]).ToString("yyyy-MM-dd");
				}
				this.txtIDCard.Text = dataTable.Rows[0]["IDCard"].ToString();
				if (!string.IsNullOrEmpty(dataTable.Rows[0]["FormalDate"].ToString()))
				{
					this.dbFormalDate.Text = Convert.ToDateTime(dataTable.Rows[0]["FormalDate"]).ToString("yyyy-MM-dd");
				}
				this.txtPoliticsFace.Text = dataTable.Rows[0]["PoliticsFace"].ToString();
				if (!string.IsNullOrEmpty(dataTable.Rows[0]["JoinPartyDate"].ToString()))
				{
					this.dbJoinPartyDate.Text = Convert.ToDateTime(dataTable.Rows[0]["JoinPartyDate"]).ToString("yyyy-MM-dd");
				}
				this.txtJoinCorpMode.Text = dataTable.Rows[0]["JoinCorpMode"].ToString();
				this.txtIntroducer.Text = dataTable.Rows[0]["Introducer"].ToString();
				this.txtSpecialty.Text = dataTable.Rows[0]["Specialty"].ToString();
				this.txtGraduateSchool.Text = dataTable.Rows[0]["GraduateSchool"].ToString();
				this.txtLevel.Text = dataTable.Rows[0]["Level"].ToString();
				if (!string.IsNullOrEmpty(dataTable.Rows[0]["BeginWorkDate"].ToString()))
				{
					this.dbBeginWorkDate.Text = Convert.ToDateTime(dataTable.Rows[0]["BeginWorkDate"]).ToString("yyyy-MM-dd");
				}
				if (!string.IsNullOrEmpty(dataTable.Rows[0]["EnterCorpDate"].ToString()))
				{
					this.dbEnterCorpDate.Text = Convert.ToDateTime(dataTable.Rows[0]["EnterCorpDate"].ToString()).ToString("yyyy-MM-dd");
				}
				this.txtPostAndCompetency.Text = dataTable.Rows[0]["PostAndCompetency"].ToString();
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
					this.txtconEndDate.Text = Convert.ToDateTime(dataTable.Rows[0]["conEndDate"]).ToString("yyyy-MM-dd");
				}
				this.txturgentCellMan.Text = dataTable.Rows[0]["urgentCellMan"].ToString();
				this.txtucmConcern.Text = dataTable.Rows[0]["ucmConcern"].ToString();
				this.txtucmTel.Text = dataTable.Rows[0]["ucmTel"].ToString();
				if (dataTable.Rows[0]["rdeNature"].ToString() != "" && this.ddlrdeNature.Items.Count > 0)
				{
					this.ddlrdeNature.SelectedValue = dataTable.Rows[0]["rdeNature"].ToString();
				}
				if (!string.IsNullOrEmpty(dataTable.Rows[0]["BeginWorkDate"].ToString()))
				{
					this.dbBeginWorkDate.Text = Convert.ToDateTime(dataTable.Rows[0]["BeginWorkDate"]).ToString("yyyy-MM-dd");
				}
			}
		}
		protected void img2_Click(object sender, ImageClickEventArgs e)
		{
		}
		protected void ddltPositionLevel_SelectedIndexChanged(object sender, EventArgs e)
		{
			string sqlString = "SELECT [RecordID], [PostAndRank] FROM [HR_Org_PostLevel] WHERE ([PositionLevel] = '" + this.ddltPositionLevel.SelectedValue + "')";
			DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
			this.ddltPostAndRank.DataSource = dataSource;
			this.ddltPostAndRank.DataTextField = "PostAndRank";
			this.ddltPostAndRank.DataValueField = "RecordID";
			this.ddltPostAndRank.DataBind();
		}
		protected void ImageBtn_Click(object sender, ImageClickEventArgs e)
		{
			com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
			string value = this.hdnFilePath.Value;
			if (fileUpload.Delete(value))
			{
				this.tr_add.Visible = true;
				this.tr_edit.Visible = false;
				Hashtable hashtable = new Hashtable();
				hashtable.Add("UserPhoto", SqlStringConstructor.GetQuotedString(""));
				string where = " where v_yhdm = '" + this.UserCode + "'";
				if (PersonnelAction.UpdPersonnel(hashtable, where))
				{
					this.JS.Text = "alert('附件删除成功！');";
					return;
				}
				this.JS.Text = "alert('附件删除失败！');";
			}
		}
		protected void gvExperience_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvExperience.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				if (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal)
				{
					LinkButton linkButton = (LinkButton)e.Row.Cells[5].FindControl("btnDelete");
					linkButton.Attributes["onclick"] = "javascript:return confirm('确定要删除该条数据吗？');";
				}
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow(1,'" + str + "');";
			}
		}
		protected void btnAdd1_Click(object sender, EventArgs e)
		{
			this.gvExperience.ShowFooter = true;
		}
		protected void btnAdd2_Click(object sender, EventArgs e)
		{
			this.gvRewardsAndPunishment.ShowFooter = true;
		}
		protected void btnAdd3_Click(object sender, EventArgs e)
		{
			this.gvEducation.ShowFooter = true;
		}
		protected void btnAdd4_Click(object sender, EventArgs e)
		{
			this.gvTrain.ShowFooter = true;
		}
		protected void btnAdd5_Click(object sender, EventArgs e)
		{
			this.gvFamilyMembers.ShowFooter = true;
		}
		protected void gvExperience_RowEditing(object sender, GridViewEditEventArgs e)
		{
			this.gvExperience.EditIndex = e.NewEditIndex;
			this.gvExperience.DataBind();
		}
		protected void gvExperience_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			TextBox textBox = new TextBox();
			TextBox textBox2 = new TextBox();
			TextBox textBox3 = new TextBox();
			TextBox textBox4 = new TextBox();
			int num = PersonnelAction.isExistData("HR_Personnel_Experience", this.UserCode);
			if (num > 0)
			{
                if (gvExperience.EditIndex != -1)
                {
                    textBox = (TextBox)this.gvExperience.Rows[gvExperience.EditIndex].FindControl("dbStartDate");
                    textBox2 = (TextBox)this.gvExperience.Rows[gvExperience.EditIndex].FindControl("dbEndDate");
                    textBox3 = (TextBox)this.gvExperience.Rows[gvExperience.EditIndex].FindControl("txtEnterpriseName");
                    textBox4 = (TextBox)this.gvExperience.Rows[gvExperience.EditIndex].FindControl("txtPost");
                }
                else
                {
                    textBox = (TextBox)this.gvExperience.FooterRow.FindControl("dbStartDate");
                    textBox2 = (TextBox)this.gvExperience.FooterRow.FindControl("dbEndDate");
                    textBox3 = (TextBox)this.gvExperience.FooterRow.FindControl("txtEnterpriseName");
                    textBox4 = (TextBox)this.gvExperience.FooterRow.FindControl("txtPost");
                }
			}
			else
			{
				Table table = (Table)this.gvExperience.Controls[0];
				GridViewRow gridViewRow = (GridViewRow)table.Rows[0];
				textBox = (TextBox)gridViewRow.FindControl("dbStartDate");
				textBox2 = (TextBox)gridViewRow.FindControl("dbEndDate");
				textBox3 = (TextBox)gridViewRow.FindControl("txtEnterpriseName");
				textBox4 = (TextBox)gridViewRow.FindControl("txtPost");
			}
			if (e.CommandName == "Insert")
			{
				this.SqlExperience.InsertParameters["UserCode"].DefaultValue = this.UserCode;
				this.SqlExperience.InsertParameters["StartDate"].DefaultValue = textBox.Text;
				this.SqlExperience.InsertParameters["EndDate"].DefaultValue = textBox2.Text;
				this.SqlExperience.InsertParameters["EnterpriseName"].DefaultValue = textBox3.Text;
				this.SqlExperience.InsertParameters["Post"].DefaultValue = textBox4.Text;
				this.gvExperience.EditIndex = 0;
				int num2 = this.SqlExperience.Insert();
				if (num2 == 1)
				{
					this.JS.Text = "alert('保存成功！');";
				}
				else
				{
					this.JS.Text = "alert('保存失败！');";
				}
				this.gvExperience.ShowFooter = false;
			}
			else
			{
				if (e.CommandName == "Update")
				{
					int editIndex = this.gvExperience.EditIndex;
					int num3 = (int)this.gvExperience.DataKeys[editIndex]["RecordID"];
					this.SqlExperience.UpdateParameters["UserCode"].DefaultValue = this.UserCode;
					this.SqlExperience.UpdateParameters["StartDate"].DefaultValue = textBox.Text;
					this.SqlExperience.UpdateParameters["EndDate"].DefaultValue = textBox2.Text;
					this.SqlExperience.UpdateParameters["EnterpriseName"].DefaultValue = textBox3.Text;
					this.SqlExperience.UpdateParameters["Post"].DefaultValue = textBox4.Text;
					this.SqlExperience.UpdateParameters["original_RecordID"].DefaultValue = num3.ToString();
					int num4 = this.SqlExperience.Update();
					if (num4 == 1)
					{
						this.JS.Text = "alert('保存成功！');";
					}
					else
					{
						this.JS.Text = "alert('保存失败！');";
					}
				}
				else
				{
					if (e.CommandName == "Delete")
					{
						this.JS.Text = "alert('删除成功！');";
					}
				}
			}
			this.gvExperience.EditIndex = -1;
			this.gvExperience.DataBind();
		}
		protected void gvExperience_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			this.gvExperience.EditIndex = e.RowIndex;
		}
		protected void gvExperience_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			this.gvExperience.ShowFooter = false;
			this.gvExperience.EditIndex = e.RowIndex;
			this.gvExperience.DataBind();
		}
		protected void gvRewardsAndPunishment_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvRewardsAndPunishment.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				if (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal)
				{
					LinkButton linkButton = (LinkButton)e.Row.Cells[5].FindControl("btnDelete");
					linkButton.Attributes["onclick"] = "javascript:return confirm('确定要删除该条数据吗？');";
				}
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow(2,'" + str + "');";
			}
		}
		protected void gvRewardsAndPunishment_RowEditing(object sender, GridViewEditEventArgs e)
		{
			this.gvRewardsAndPunishment.EditIndex = e.NewEditIndex;
			this.gvRewardsAndPunishment.DataBind();
		}
        
        protected void gvRewardsAndPunishment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TextBox box = new TextBox();
            TextBox box2 = new TextBox();
            TextBox box3 = new TextBox();
            TextBox box4 = new TextBox();
            if (PersonnelAction.isExistData("HR_Personnel_RewardsAndPunishment", this.UserCode) > 0)
            {
                if (gvRewardsAndPunishment.EditIndex != -1)
                {
                    box3 = (TextBox)this.gvRewardsAndPunishment.Rows[gvRewardsAndPunishment.EditIndex].FindControl("dbAwardDate");
                    box = (TextBox)this.gvRewardsAndPunishment.Rows[gvRewardsAndPunishment.EditIndex].FindControl("txtCredit");
                    box2 = (TextBox)this.gvRewardsAndPunishment.Rows[gvRewardsAndPunishment.EditIndex].FindControl("txtReason");
                    box4 = (TextBox)this.gvRewardsAndPunishment.Rows[gvRewardsAndPunishment.EditIndex].FindControl("txtAwardOrg");
                }
                else
                {
                    box3 = (TextBox)this.gvRewardsAndPunishment.FooterRow.FindControl("dbAwardDate");
                    box = (TextBox)this.gvRewardsAndPunishment.FooterRow.FindControl("txtCredit");
                    box2 = (TextBox)this.gvRewardsAndPunishment.FooterRow.FindControl("txtReason");
                    box4 = (TextBox)this.gvRewardsAndPunishment.FooterRow.FindControl("txtAwardOrg");
                }
            }
            else
            {
                Table table = (Table)this.gvRewardsAndPunishment.Controls[0];
                GridViewRow row = (GridViewRow)table.Rows[0];
                box3 = (TextBox)row.FindControl("dbAwardDate");
                box = (TextBox)row.FindControl("txtCredit");
                box2 = (TextBox)row.FindControl("txtReason");
                box4 = (TextBox)row.FindControl("txtAwardOrg");
            }
            if (e.CommandName == "Insert")
            {
                this.SqlRewardsAndPunishment.InsertParameters["UserCode"].DefaultValue = this.UserCode;
                this.SqlRewardsAndPunishment.InsertParameters["Credit"].DefaultValue = box.Text;
                this.SqlRewardsAndPunishment.InsertParameters["Reason"].DefaultValue = box2.Text;
                this.SqlRewardsAndPunishment.InsertParameters["AwardDate"].DefaultValue = box3.Text;
                this.SqlRewardsAndPunishment.InsertParameters["AwardOrg"].DefaultValue = box4.Text;
                this.gvRewardsAndPunishment.EditIndex = 0;
                if (this.SqlRewardsAndPunishment.Insert() == 1)
                {
                    this.JS.Text = "alert('保存成功！');";
                }
                else
                {
                    this.JS.Text = "alert('保存失败！');";
                }
                this.gvRewardsAndPunishment.ShowFooter = false;
            }
            else if (e.CommandName == "Update")
            {
                int editIndex = this.gvRewardsAndPunishment.EditIndex;
                int num4 = (int)this.gvRewardsAndPunishment.DataKeys[editIndex]["RecordID"];
                this.SqlRewardsAndPunishment.UpdateParameters["UserCode"].DefaultValue = this.UserCode;
                this.SqlRewardsAndPunishment.UpdateParameters["Credit"].DefaultValue = box.Text;
                this.SqlRewardsAndPunishment.UpdateParameters["Reason"].DefaultValue = box2.Text;
                this.SqlRewardsAndPunishment.UpdateParameters["AwardDate"].DefaultValue = box3.Text;
                this.SqlRewardsAndPunishment.UpdateParameters["AwardOrg"].DefaultValue = box4.Text;
                this.SqlRewardsAndPunishment.UpdateParameters["original_RecordID"].DefaultValue = num4.ToString();
                if (this.SqlRewardsAndPunishment.Update() == 1)
                {
                    this.JS.Text = "alert('保存成功！');";
                }
                else
                {
                    this.JS.Text = "alert('保存失败！');";
                }
            }
            else if (e.CommandName == "Delete")
            {
                this.JS.Text = "alert('删除成功！');";
            }
            this.gvRewardsAndPunishment.EditIndex = -1;
            this.gvRewardsAndPunishment.DataBind();
        }


        protected void gvRewardsAndPunishment_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			this.gvRewardsAndPunishment.EditIndex = e.RowIndex;
		}
		protected void gvRewardsAndPunishment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			this.gvRewardsAndPunishment.ShowFooter = false;
			this.gvRewardsAndPunishment.EditIndex = e.RowIndex;
			this.gvRewardsAndPunishment.DataBind();
		}
		protected void gvEducation_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvEducation.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				if (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal)
				{
					LinkButton linkButton = (LinkButton)e.Row.Cells[5].FindControl("btnDelete");
					linkButton.Attributes["onclick"] = "javascript:return confirm('确定要删除该条数据吗？');";
				}
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow(3,'" + str + "');";
			}
		}
		protected void gvEducation_RowEditing(object sender, GridViewEditEventArgs e)
		{
			this.gvEducation.EditIndex = e.NewEditIndex;
			this.gvEducation.DataBind();
		}

        protected void gvEducation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TextBox box = new TextBox();
            TextBox box2 = new TextBox();
            TextBox box3 = new TextBox();
            TextBox box4 = new TextBox();
            if (PersonnelAction.isExistData("HR_Personnel_Education", this.UserCode) > 0)
            {
                if (gvEducation.EditIndex != -1)
                {
                    box = (TextBox)this.gvEducation.Rows[gvEducation.EditIndex].FindControl("dbStartDate");
                    box2 = (TextBox)this.gvEducation.Rows[gvEducation.EditIndex].FindControl("dbEndDate");
                    box3 = (TextBox)this.gvEducation.Rows[gvEducation.EditIndex].FindControl("txtSchoolName");
                    box4 = (TextBox)this.gvEducation.Rows[gvEducation.EditIndex].FindControl("txtSpecialty");
                }
                else
                {
                    box = (TextBox)this.gvEducation.FooterRow.FindControl("dbStartDate");
                    box2 = (TextBox)this.gvEducation.FooterRow.FindControl("dbEndDate");
                    box3 = (TextBox)this.gvEducation.FooterRow.FindControl("txtSchoolName");
                    box4 = (TextBox)this.gvEducation.FooterRow.FindControl("txtSpecialty");
                }
            }
            else
            {
                Table table = (Table)this.gvEducation.Controls[0];
                GridViewRow row = (GridViewRow)table.Rows[0];
                box = (TextBox)row.FindControl("dbStartDate");
                box2 = (TextBox)row.FindControl("dbEndDate");
                box3 = (TextBox)row.FindControl("txtSchoolName");
                box4 = (TextBox)row.FindControl("txtSpecialty");
            }
            if (e.CommandName == "Insert")
            {
                this.SqlEducation.InsertParameters["UserCode"].DefaultValue = this.UserCode;
                this.SqlEducation.InsertParameters["StartDate"].DefaultValue = box.Text;
                this.SqlEducation.InsertParameters["EndDate"].DefaultValue = box2.Text;
                this.SqlEducation.InsertParameters["SchoolName"].DefaultValue = box3.Text;
                this.SqlEducation.InsertParameters["Specialty"].DefaultValue = box4.Text;
                this.gvEducation.EditIndex = 0;
                if (this.SqlEducation.Insert() == 1)
                {
                    this.JS.Text = "alert('保存成功！');";
                }
                else
                {
                    this.JS.Text = "alert('保存失败！');";
                }
                this.gvEducation.ShowFooter = false;
            }
            else if (e.CommandName == "Update")
            {
                int editIndex = this.gvEducation.EditIndex;
                int num4 = (int)this.gvEducation.DataKeys[editIndex]["RecordID"];
                this.SqlEducation.UpdateParameters["UserCode"].DefaultValue = this.UserCode;
                this.SqlEducation.UpdateParameters["StartDate"].DefaultValue = box.Text;
                this.SqlEducation.UpdateParameters["EndDate"].DefaultValue = box2.Text;
                this.SqlEducation.UpdateParameters["SchoolName"].DefaultValue = box3.Text;
                this.SqlEducation.UpdateParameters["Specialty"].DefaultValue = box4.Text;
                this.SqlEducation.UpdateParameters["original_RecordID"].DefaultValue = num4.ToString();
                if (this.SqlEducation.Update() == 1)
                {
                    this.JS.Text = "alert('保存成功！');";
                }
                else
                {
                    this.JS.Text = "alert('保存失败！');";
                }
            }
            else if (e.CommandName == "Delete")
            {
                this.JS.Text = "alert('删除成功！');";
            }
            this.gvEducation.EditIndex = -1;
            this.gvEducation.DataBind();
        }




		protected void gvEducation_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			this.gvEducation.EditIndex = e.RowIndex;
		}
		protected void gvEducation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			this.gvEducation.ShowFooter = false;
			this.gvEducation.EditIndex = e.RowIndex;
			this.gvEducation.DataBind();
		}
		protected void gvTrain_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvTrain.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				if (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal)
				{
					LinkButton linkButton = (LinkButton)e.Row.Cells[7].FindControl("btnDelete");
					linkButton.Attributes["onclick"] = "javascript:return confirm('确定要删除该条数据吗？');";
				}
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow(4,'" + str + "');";
			}
		}
		protected void gvTrain_RowEditing(object sender, GridViewEditEventArgs e)
		{
			this.gvTrain.EditIndex = e.NewEditIndex;
			this.gvTrain.DataBind();
		}
		protected void gvTrain_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			TextBox textBox = new TextBox();
			TextBox textBox2 = new TextBox();
			TextBox textBox3 = new TextBox();
			TextBox textBox4 = new TextBox();
			TextBox textBox5 = new TextBox();
			TextBox textBox6 = new TextBox();
			int num = PersonnelAction.isExistData("HR_Personnel_Train", this.UserCode);
			if (num > 0)
			{
                if (gvTrain.EditIndex != -1)
                {
                    textBox = (TextBox)this.gvTrain.Rows[gvTrain.EditIndex].FindControl("txtCourses");
                    textBox2 = (TextBox)this.gvTrain.Rows[gvTrain.EditIndex].FindControl("txtHour");
                    textBox3 = (TextBox)this.gvTrain.Rows[gvTrain.EditIndex].FindControl("dbStartDate");
                    textBox4 = (TextBox)this.gvTrain.Rows[gvTrain.EditIndex].FindControl("dbEndDate");
                    textBox5 = (TextBox)this.gvTrain.Rows[gvTrain.EditIndex].FindControl("txtTrainOrgan");
                    textBox6 = (TextBox)this.gvTrain.Rows[gvTrain.EditIndex].FindControl("txtRemark");
                }
                else
                {
                    textBox = (TextBox)this.gvTrain.FooterRow.FindControl("txtCourses");
                    textBox2 = (TextBox)this.gvTrain.FooterRow.FindControl("txtHour");
                    textBox3 = (TextBox)this.gvTrain.FooterRow.FindControl("dbStartDate");
                    textBox4 = (TextBox)this.gvTrain.FooterRow.FindControl("dbEndDate");
                    textBox5 = (TextBox)this.gvTrain.FooterRow.FindControl("txtTrainOrgan");
                    textBox6 = (TextBox)this.gvTrain.FooterRow.FindControl("txtRemark");
                }
			}
			else
			{
				Table table = (Table)this.gvTrain.Controls[0];
				GridViewRow gridViewRow = (GridViewRow)table.Rows[0];
				textBox = (TextBox)gridViewRow.FindControl("txtCourses");
				textBox2 = (TextBox)gridViewRow.FindControl("txtHour");
				textBox3 = (TextBox)gridViewRow.FindControl("dbStartDate");
				textBox4 = (TextBox)gridViewRow.FindControl("dbEndDate");
				textBox5 = (TextBox)gridViewRow.FindControl("txtTrainOrgan");
				textBox6 = (TextBox)gridViewRow.FindControl("txtRemark");
			}
			if (e.CommandName == "Insert")
			{
				this.SqlTrain.InsertParameters["UserCode"].DefaultValue = this.UserCode;
				this.SqlTrain.InsertParameters["Courses"].DefaultValue = textBox.Text;
				this.SqlTrain.InsertParameters["Hour"].DefaultValue = textBox2.Text;
				this.SqlTrain.InsertParameters["StartDate"].DefaultValue = textBox3.Text;
				this.SqlTrain.InsertParameters["EndDate"].DefaultValue = textBox4.Text;
				this.SqlTrain.InsertParameters["TrainOrgan"].DefaultValue = textBox5.Text;
				this.SqlTrain.InsertParameters["Remark"].DefaultValue = textBox6.Text;
				this.gvTrain.EditIndex = 0;
				int num2 = this.SqlTrain.Insert();
				if (num2 == 1)
				{
					this.JS.Text = "alert('保存成功！');";
				}
				else
				{
					this.JS.Text = "alert('保存失败！');";
				}
				this.gvTrain.ShowFooter = false;
			}
			else
			{
				if (e.CommandName == "Update")
				{
					int editIndex = this.gvTrain.EditIndex;
					int num3 = (int)this.gvTrain.DataKeys[editIndex]["RecordID"];
					this.SqlTrain.UpdateParameters["UserCode"].DefaultValue = this.UserCode;
					this.SqlTrain.UpdateParameters["Courses"].DefaultValue = textBox.Text;
					this.SqlTrain.UpdateParameters["Hour"].DefaultValue = textBox2.Text;
					this.SqlTrain.UpdateParameters["StartDate"].DefaultValue = textBox3.Text;
					this.SqlTrain.UpdateParameters["EndDate"].DefaultValue = textBox4.Text;
					this.SqlTrain.UpdateParameters["TrainOrgan"].DefaultValue = textBox5.Text;
					this.SqlTrain.UpdateParameters["Remark"].DefaultValue = textBox6.Text;
					this.SqlTrain.UpdateParameters["original_RecordID"].DefaultValue = num3.ToString();
					int num4 = this.SqlTrain.Update();
					if (num4 == 1)
					{
						this.JS.Text = "alert('保存成功！');";
					}
					else
					{
						this.JS.Text = "alert('保存失败！');";
					}
				}
				else
				{
					if (e.CommandName == "Delete")
					{
						this.JS.Text = "alert('删除成功！');";
					}
				}
			}
			this.gvTrain.EditIndex = -1;
			this.gvTrain.DataBind();
		}
		protected void gvTrain_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			this.gvTrain.EditIndex = e.RowIndex;
		}
		protected void gvTrain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			this.gvTrain.ShowFooter = false;
			this.gvTrain.EditIndex = e.RowIndex;
			this.gvTrain.DataBind();
		}
		protected void gvFamilyMembers_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvFamilyMembers.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				if (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal)
				{
					LinkButton linkButton = (LinkButton)e.Row.Cells[5].FindControl("btnDelete");
					linkButton.Attributes["onclick"] = "javascript:return confirm('确定要删除该条数据吗？');";
				}
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow(5,'" + str + "');";
			}
		}
		protected void gvFamilyMembers_RowEditing(object sender, GridViewEditEventArgs e)
		{
			this.gvFamilyMembers.EditIndex = e.NewEditIndex;
			this.gvFamilyMembers.DataBind();
		}
		protected void gvFamilyMembers_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			TextBox textBox = new TextBox();
			TextBox textBox2 = new TextBox();
			TextBox textBox3 = new TextBox();
			TextBox textBox4 = new TextBox();
			int num = PersonnelAction.isExistData("HR_Personnel_FamilyMembers", this.UserCode);
			if (num > 0)
			{
                if (gvFamilyMembers.EditIndex != -1)
                {
                    textBox = (TextBox)this.gvFamilyMembers.Rows[gvFamilyMembers.EditIndex].FindControl("txtName");
                    textBox2 = (TextBox)this.gvFamilyMembers.Rows[gvFamilyMembers.EditIndex].FindControl("txtRelation");
                    textBox3 = (TextBox)this.gvFamilyMembers.Rows[gvFamilyMembers.EditIndex].FindControl("txtTel");
                    textBox4 = (TextBox)this.gvFamilyMembers.Rows[gvFamilyMembers.EditIndex].FindControl("txtWorkUnit");
                }
                else
                {
                    textBox = (TextBox)this.gvFamilyMembers.FooterRow.FindControl("txtName");
                    textBox2 = (TextBox)this.gvFamilyMembers.FooterRow.FindControl("txtRelation");
                    textBox3 = (TextBox)this.gvFamilyMembers.FooterRow.FindControl("txtTel");
                    textBox4 = (TextBox)this.gvFamilyMembers.FooterRow.FindControl("txtWorkUnit");
                }
			}
			else
			{
				Table table = (Table)this.gvFamilyMembers.Controls[0];
				GridViewRow gridViewRow = (GridViewRow)table.Rows[0];
				textBox = (TextBox)gridViewRow.FindControl("txtName");
				textBox2 = (TextBox)gridViewRow.FindControl("txtRelation");
				textBox3 = (TextBox)gridViewRow.FindControl("txtTel");
				textBox4 = (TextBox)gridViewRow.FindControl("txtWorkUnit");
			}
			if (e.CommandName == "Insert")
			{
				this.SqlFamilyMembers.InsertParameters["UserCode"].DefaultValue = this.UserCode;
				this.SqlFamilyMembers.InsertParameters["Name"].DefaultValue = textBox.Text;
				this.SqlFamilyMembers.InsertParameters["Relation"].DefaultValue = textBox2.Text;
				this.SqlFamilyMembers.InsertParameters["Tel"].DefaultValue = textBox3.Text;
				this.SqlFamilyMembers.InsertParameters["WorkUnit"].DefaultValue = textBox4.Text;
				this.gvFamilyMembers.EditIndex = 0;
				int num2 = this.SqlFamilyMembers.Insert();
				if (num2 == 1)
				{
					this.JS.Text = "alert('保存成功！');";
				}
				else
				{
					this.JS.Text = "alert('保存失败！');";
				}
				this.gvFamilyMembers.ShowFooter = false;
			}
			else
			{
				if (e.CommandName == "Update")
				{
					int editIndex = this.gvFamilyMembers.EditIndex;
					int num3 = (int)this.gvFamilyMembers.DataKeys[editIndex]["RecordID"];
					this.SqlFamilyMembers.UpdateParameters["UserCode"].DefaultValue = this.UserCode;
					this.SqlFamilyMembers.UpdateParameters["Name"].DefaultValue = textBox.Text;
					this.SqlFamilyMembers.UpdateParameters["Relation"].DefaultValue = textBox2.Text;
					this.SqlFamilyMembers.UpdateParameters["Tel"].DefaultValue = textBox3.Text;
					this.SqlFamilyMembers.UpdateParameters["WorkUnit"].DefaultValue = textBox4.Text;
					this.SqlFamilyMembers.UpdateParameters["original_RecordID"].DefaultValue = num3.ToString();
					int num4 = this.SqlFamilyMembers.Update();
					if (num4 == 1)
					{
						this.JS.Text = "alert('保存成功！');";
					}
					else
					{
						this.JS.Text = "alert('保存失败！');";
					}
				}
				else
				{
					if (e.CommandName == "Delete")
					{
						this.JS.Text = "alert('删除成功！');";
					}
				}
			}
			this.gvFamilyMembers.EditIndex = -1;
			this.gvFamilyMembers.DataBind();
		}
		protected void gvFamilyMembers_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			this.gvFamilyMembers.EditIndex = e.RowIndex;
		}
		protected void gvFamilyMembers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			this.gvFamilyMembers.ShowFooter = false;
			this.gvFamilyMembers.EditIndex = e.RowIndex;
			this.gvFamilyMembers.DataBind();
		}
		private void Bind_Duty()
		{
			this.GVDuty.DataSource = HRPersonnelCommonAction.GetDutyInfo(this.UserCode);
			this.GVDuty.DataBind();
		}
		protected void GVDuty_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowIndex > -1)
			{
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Row.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);DutyClickRow('",
					dataRowView["I_DUTYID"].ToString(),
					"','",
					dataRowView["I_bmdm"].ToString(),
					"');"
				});
			}
		}
		protected void btnAddDuty_Click(object sender, EventArgs e)
		{
			this.Bind_Duty();
		}
		protected void btnDelDuty_Click(object sender, EventArgs e)
		{
			int num = HRPersonnelCommonAction.DeleteDutyAndDept(this.UserCode, this.HdnDeptID.Value, this.HdnDUTYID.Value);
			if (num > 0)
			{
				this.Bind_Duty();
			}
		}
	}


