using ASP;
using cn.justwin.stockBLL;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class PrjInfoEdit : BasePage, IRequiresSessionState
	{
		private PTPrjInfoBll BLL_n = new PTPrjInfoBll();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				string sqlString = "select NoteID,CodeName from  dbo.XPM_Basic_CodeList where TypeID=146 and IsValid='true' and IsVisible='true'";
				System.Data.DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
				this.ddlXmgroup.DataSource = dataSource;
				this.ddlXmgroup.DataValueField = "NoteID";
				this.ddlXmgroup.DataTextField = "CodeName";
				this.ddlXmgroup.DataBind();
				this.ddlXmgroup.Items.Insert(0, new ListItem("--请选择--", "-1"));
				this.txt_StartDate.Attributes["onactivate"] = "getBeginDate();";
				this.txt_EndDate.Attributes["onactivate"] = "getEndDate();";
				this.Drop_QualityClass_Bind();
				this.Drop_PrjKindClass_Bind();
				this.ddt_Area_Bind();
				this.Drop_zzGrade_Bind();
				this.Drop_ysType_Bind();
				this.Drop_cbType_Bind();
				this.Drop_Payment_Bind();
				this.Drop_zbType_Bind();
				this.Drop_jsType_Bind();
				this.Drop_PrimaryGrade_Bind();
				this.Drop_jzType_Bind();
				if (base.Request.QueryString["TypeCode"] == null)
				{
					this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert(\"参数不正确！\");window.close();</script>");
				}
				else
				{
					this.ViewState["TYPECODE"] = base.Request.QueryString["TypeCode"].ToString();
				}
				if (base.Request.QueryString["op"] == null)
				{
					this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert(\"参数不正确！\");window.close();</script>");
				}
				else
				{
					this.ViewState["OP"] = base.Request.QueryString["op"].ToString();
				}
				string a;
				if ((a = this.ViewState["OP"].ToString().ToLower()) != null)
				{
					if (a == "add")
					{
						this.txt_StartDate.Text = System.DateTime.Now.ToString("yyyy-M-dd");
						this.txt_EndDate.Text = System.DateTime.Now.ToString("yyyy-M-dd");
						base.Header.Title = "添加资源分类";
						this.tbxPreCode.Text = this.ViewState["TYPECODE"].ToString();
						this.tbxPreCode.ReadOnly = true;
						this.TxtTypeCode.ReadOnly = true;
						if (this.ViewState["TYPECODE"].ToString().Trim() != "")
						{
							string text = PMAction.MakeClassCode(this.ViewState["TYPECODE"].ToString().Trim());
							this.TxtTypeCode.Text = text.Substring(this.tbxPreCode.Text.Trim().Length, text.Length - this.tbxPreCode.Text.Trim().Length);
						}
						else
						{
							string text = PMAction.MakeClassCode("");
							this.TxtTypeCode.Text = text;
						}
						this.hdfGuid.Value = System.Guid.NewGuid().ToString();
						this.FileLink1.MID = 18;
						this.FileLink1.FID = this.hdfGuid.Value;
						this.FileLink1.Type = 1;
						return;
					}
					if (a == "upd")
					{
						PMModel pIM = new PMModel();
						pIM = PMAction.GetSingleInfo(this.ViewState["TYPECODE"].ToString());
						this.TextBind(pIM);
						base.Header.Title = "更新资源分类";
						this.tbxPreCode.Visible = false;
						this.TxtTypeCode.Columns = 20;
						this.TxtTypeCode.ReadOnly = true;
						this.SetModuleState(this.ViewState["TYPECODE"].ToString());
						this.FileLink1.MID = 18;
						this.FileLink1.FID = this.hdfGuid.Value;
						this.FileLink1.Type = 1;
						return;
					}
				}
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert(\"参数不正确！\");window.close();</script>");
			}
		}
		private void SetModuleState(string moduleCode)
		{
			System.Data.DataTable prjInfoInfoByTypeCode = PMAction.GetPrjInfoInfoByTypeCode(moduleCode);
			if (prjInfoInfoByTypeCode.Rows.Count == 1)
			{
				System.Data.DataRow dataRow = prjInfoInfoByTypeCode.Rows[0];
				this.TxtTypeCode.Text = dataRow["TypeCode"].ToString();
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(this.ManagerCode.Value))
			{
				this.Page.RegisterStartupScript("提示", "<script language=\"JavaScript\">alert(\"项目经理不能为空!\");</script>");
				return;
			}
			string userCode = base.UserCode;
			System.DateTime now = System.DateTime.Now;
			string podepom = string.Empty;
			if (base.UserCode == "00000000")
			{
				podepom = "," + base.UserCode;
			}
			else
			{
				podepom = ",00000000," + base.UserCode;
			}
			string str = this.tbxPreCode.Text.Trim();
			string text = this.TxtTypeCode.Text.Trim();
			string recordDate = now.ToString();
			PMModel pMModel = new PMModel();
			pMModel = this.GetTextValue();
			decimal childAllpri = PMAction.getChildAllpri(str + text);
			decimal perpri = PMAction.getPerpri(str + text);
			decimal childAllpriNotReg = PMAction.getChildAllpriNotReg(str + text);
			if (this.ViewState["OP"].ToString().ToLower() == "add")
			{
				if (PMAction.GetPrjInfoCount(str + text) > 0)
				{
					this.Page.RegisterStartupScript("提示", "<script language=\"JavaScript\">alert(\"分类编码已经存在，请纠正！\");</script>");
					return;
				}
				if (!PMAction.CheckCode(pMModel.PrjCode))
				{
					this.Page.RegisterStartupScript("提示", "<script language=\"JavaScript\">alert(\"编号重复！\");window.returnValue = true;window.close();</script>");
					return;
				}
				if (this.txt_PrjCost.Text.ToString().Trim() != "")
				{
				}
				if (perpri != 0m)
				{
					if (PMAction.AddPrjInfo(str + text, userCode, recordDate, pMModel, podepom))
					{
						this.BLL_n.update(pMModel.PrjGuid.ToString(), this.Txt_grade.Text.ToString().Trim(), this.ManagerCodeYW.Value.ToString() + "-" + this.Txt_businessman.Text.Trim().ToString(), this.Txt_telphone.Text.Trim().ToString());
						string mes = string.Concat(new string[]
						{
							"项目立项通知：编号为",
							pMModel.PrjCode,
							"的",
							pMModel.PrjName,
							"项目已经立项。"
						});
						this.getOrganiger(str + text, mes, this.ManagerCode.Value.ToString());
						this.Page.RegisterStartupScript("提示", "<script language=\"JavaScript\">alert(\"保存成功！\");window.returnValue = true;window.close();</script>");
						return;
					}
					this.Page.RegisterStartupScript("提示", "<script language=\"JavaScript\">alert(\"保存失败！\");window.returnValue = true;window.close();</script>");
					return;
				}
				else
				{
					if (PMAction.AddPrjInfo(str + text, userCode, recordDate, pMModel, podepom))
					{
						this.BLL_n.update(pMModel.PrjGuid.ToString(), this.Txt_grade.Text.ToString().Trim(), this.ManagerCodeYW.Value.ToString() + "-" + this.Txt_businessman.Text.Trim().ToString(), this.Txt_telphone.Text.Trim().ToString());
						string mes2 = string.Concat(new string[]
						{
							"项目立项通知：编号为",
							pMModel.PrjCode,
							"的",
							pMModel.PrjName,
							"项目已经立项。"
						});
						this.getOrganiger(str + text, mes2, this.ManagerCode.Value.ToString());
						this.Page.RegisterStartupScript("提示", "<script language=\"JavaScript\">alert(\"保存成功！\");window.returnValue = true;window.close();</script>");
						return;
					}
					this.Page.RegisterStartupScript("提示", "<script language=\"JavaScript\">alert(\"保存失败！\");window.returnValue = true;window.close();</script>");
					return;
				}
			}
			else
			{
				if (this.txt_PrjCost.Text.ToString().Trim() != "")
				{
				}
				if (!PMAction.UpCheckCode(this.ViewState["oldPrjcode"].ToString().Trim(), pMModel.PrjCode))
				{
					this.LabcodeWR.Text = "编号重复";
					return;
				}
				if (childAllpri == 0m)
				{
					if (PMAction.UpdPrjInfo(text, userCode, recordDate, pMModel))
					{
						PMPrjAction.updatePrjCode(pMModel.PrjCode, pMModel.PrjGuid.ToString());
						this.Page.RegisterStartupScript("提示", "<script language=\"JavaScript\">alert(\"保存成功！\");window.returnValue = true;window.close();</script>");
						return;
					}
					this.Page.RegisterStartupScript("提示", "<script language=\"JavaScript\">alert(\"保存失败！\");</script>");
					return;
				}
				else
				{
					if (PMAction.UpdPrjInfo(text, userCode, recordDate, pMModel))
					{
						this.Page.RegisterStartupScript("提示", "<script language=\"JavaScript\">alert(\"保存成功！\");window.returnValue = true;window.close();</script>");
						return;
					}
					this.Page.RegisterStartupScript("提示", "<script language=\"JavaScript\">alert(\"保存失败！\");</script>");
					return;
				}
			}
		}
		private void Drop_QualityClass_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.drop_QualityClass, 13, true);
		}
		private void Drop_PrjKindClass_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.drop_PrjKindClass, 3, true);
		}
		private void Drop_zzGrade_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.drop_zzGrade, 22, true);
		}
		private void Drop_ysType_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.drop_ysType, 23, true);
		}
		private void Drop_cbType_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.drop_cbType, 24, true);
		}
		private void Drop_Payment_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.drop_Payment, 25, true);
		}
		private void Drop_zbType_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.drop_zbType, 26, true);
		}
		private void Drop_jsType_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.drop_jsType, 27, true);
		}
		private void Drop_PrimaryGrade_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.drop_PrimaryGrade, 28, true);
		}
		private void Drop_jzType_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.drop_jzType, 29, true);
		}
		private void ddt_stateBind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.ddt_state, 7, true);
		}
		private void ddt_Area_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.ddt_Area, 19, true);
		}
		private PMModel GetTextValue()
		{
			PMModel pMModel = new PMModel();
			pMModel.PrjCode = this.txt_PrjCode.Text;
			pMModel.PrjGuid = new System.Guid(this.hdfGuid.Value.ToString());
			pMModel.PrjName = this.txt_PrjName.Text.Trim();
			pMModel.StartDate = System.Convert.ToDateTime(this.txt_StartDate.Text);
			pMModel.EndDate = System.Convert.ToDateTime(this.txt_EndDate.Text);
			pMModel.ComputeClass = "1";
			if (this.txt_PrjCost.Text.Trim().Length > 0)
			{
				pMModel.PrjCost = this.txt_PrjCost.Text;
			}
			else
			{
				pMModel.PrjCost = "0";
			}
			if (this.txt_Duration.Text.Trim().Length > 0)
			{
				pMModel.Duration = this.txt_Duration.Text;
			}
			else
			{
				pMModel.Duration = "0";
			}
			pMModel.QualityClass = this.drop_QualityClass.SelectedValue;
			pMModel.PrjKindClass = this.drop_PrjKindClass.SelectedValue;
			pMModel.Area = this.ddt_Area.SelectedValue;
			pMModel.Remark = this.txt_Remark.Text;
			pMModel.Owner = this.txt_Owner.Value;
			pMModel.Counsellor = this.txt_Counsellor.Text;
			pMModel.Designer = this.txt_Designer.Text;
			pMModel.Inspector = this.txt_Inspector.Text;
			pMModel.PrjInfo = this.txt_PrjInfo.Text;
			pMModel.PrjState = this.ddt_state.SelectedValue;
			pMModel.OwnerCode = this.HdnOwnerCode.Value;
			pMModel.PrjPlace = this.txt_PrjPlace.Text.Trim();
			pMModel.Rank = this.drop_zzGrade.SelectedValue;
			pMModel.BudgetWay = this.drop_ysType.SelectedValue;
			pMModel.ContractWay = this.drop_cbType.SelectedValue;
			pMModel.PayCondition = this.drop_Payment.SelectedValue;
			pMModel.TenderWay = this.drop_zbType.SelectedValue;
			pMModel.PayWay = this.drop_jsType.SelectedValue;
			pMModel.KeyPart = this.drop_PrimaryGrade.SelectedValue;
			pMModel.WorkUnit = this.Txt_WorkUnit.Value;
			pMModel.LinkMan = this.Txt_LinkMan.Text;
			pMModel.PrjManager = this.ManagerCode.Value.Trim().ToString() + "-" + this.Txt_PrjManager.Value;
			pMModel.BuildingType = this.drop_jzType.SelectedValue;
			pMModel.TotalHouseNum = this.Txt_TotalHouseNum.Text;
			pMModel.BuildingArea = this.Txt_BuildingArea.Text;
			pMModel.UsegrounArea = this.Txt_UsegrounArea.Text;
			pMModel.UndergroundArea = this.Txt_UndergroundArea.Text;
			pMModel.PrjFundInfo = this.Txt_PrjFundInfo.Text;
			pMModel.OtherStatement = this.Txt_OtherStatement.Text;
			pMModel.Xmgroup = this.ddlXmgroup.SelectedValue.ToString();
			return pMModel;
		}
		private void TextBind(PMModel PIM)
		{
			this.txt_PrjCode.Text = PIM.PrjCode;
			this.ViewState["oldPrjcode"] = this.txt_PrjCode.Text;
			this.txt_PrjName.Text = PIM.PrjName;
			this.hdfGuid.Value = PIM.PrjGuid.ToString();
			this.txt_StartDate.Text = PIM.StartDate.ToString("yyyy-MM-dd");
			this.txt_EndDate.Text = PIM.EndDate.ToString("yyyy-MM-dd");
			this.txt_PrjCost.Text = PIM.PrjCost;
			this.txt_Duration.Text = PIM.Duration.ToString();
			this.drop_QualityClass.SelectedValue = PIM.QualityClass;
			this.drop_PrjKindClass.SelectedValue = PIM.PrjKindClass;
			this.ddt_state.SelectedValue = PIM.PrjState;
			this.HdnOwnerCode.Value = PIM.OwnerCode;
			try
			{
				this.ddt_Area.SelectedValue = PIM.Area;
			}
			catch
			{
			}
			this.txt_Remark.Text = PIM.Remark;
			this.txt_Owner.Value = PIM.Owner;
			this.txt_Counsellor.Text = PIM.Counsellor;
			this.txt_Designer.Text = PIM.Designer;
			this.txt_Inspector.Text = PIM.Inspector;
			this.txt_PrjInfo.Text = PIM.PrjInfo;
			this.ddt_state.SelectedValue = PIM.PrjState;
			this.txt_PrjPlace.Text = PIM.PrjPlace;
			this.drop_zzGrade.SelectedValue = PIM.Rank;
			this.drop_ysType.SelectedValue = PIM.BudgetWay;
			this.drop_cbType.SelectedValue = PIM.ContractWay;
			this.drop_Payment.SelectedValue = PIM.PayCondition;
			this.drop_zbType.SelectedValue = PIM.TenderWay;
			this.drop_jsType.SelectedValue = PIM.PayWay;
			this.drop_PrimaryGrade.SelectedValue = PIM.KeyPart;
			this.Txt_WorkUnit.Value = PIM.WorkUnit;
			this.Txt_LinkMan.Text = PIM.LinkMan;
			if (PIM.PrjManager.ToString().Length > 8)
			{
				this.Txt_PrjManager.Value = PIM.PrjManager.Substring(9);
				this.ManagerCode.Value = PIM.PrjManager.Substring(0, 8);
			}
			this.drop_jzType.SelectedValue = PIM.BuildingType;
			this.Txt_TotalHouseNum.Text = PIM.TotalHouseNum;
			this.Txt_BuildingArea.Text = PIM.BuildingArea;
			this.Txt_UsegrounArea.Text = PIM.UsegrounArea;
			this.Txt_UndergroundArea.Text = PIM.UndergroundArea;
			this.Txt_PrjFundInfo.Text = PIM.PrjFundInfo;
			this.Txt_OtherStatement.Text = PIM.OtherStatement;
			if (PIM.Xmgroup != "")
			{
				this.ddlXmgroup.SelectedValue = PIM.Xmgroup.ToString();
			}
			System.Data.DataTable dataTable = new System.Data.DataTable();
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					this.Txt_telphone.Text = dataTable.Rows[i]["telephone"].ToString();
					this.Txt_grade.Text = dataTable.Rows[i]["grade"].ToString();
					string text = dataTable.Rows[i]["businessman"].ToString();
					this.Txt_businessman.Text = text.Substring(text.LastIndexOf("-") + 1);
				}
			}
		}
		private void getOrganiger(string xgid, string Mes, string jsyhdm)
		{
			PublicInterface.SendSysMsg(new PTDBSJ
			{
				V_LXBM = "024",
				I_XGID = xgid,
				DTM_DBSJ = System.DateTime.Now,
				V_Content = Mes,
				V_DBLJ = "EPC/Pm/PrjInfo/PrjInfoView.aspx?typecode=" + xgid,
				V_YHDM = jsyhdm,
				V_TPLJ = "new_Mail.gif",
				C_OpenFlag = "1"
			});
		}
	}


