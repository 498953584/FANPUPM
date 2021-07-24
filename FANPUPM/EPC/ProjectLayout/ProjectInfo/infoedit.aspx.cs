using ASP;
using cn.justwin.stockBLL.PTPrjInfo;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class InfoEdit : PageBase, IRequiresSessionState
	{
		private PTPrjInfoZTBBll BLL_n = new PTPrjInfoZTBBll();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.txt_StartDate.Attributes["onactivate"] = "getBeginDate();";
				this.txt_EndDate.Attributes["onactivate"] = "getEndDate();";
				this.hdnSeeType.Value = base.Request.QueryString["SeeType"];
				this.Drop_QualityClass_Bind();
				this.Drop_PrjKindClass_Bind();
				this.ddt_stateBind();
				this.ddt_Area_Bind();
				this.Drop_zzGrade_Bind();
				this.Drop_ysType_Bind();
				this.Drop_cbType_Bind();
				this.Drop_Payment_Bind();
				this.Drop_zbType_Bind();
				this.Drop_jsType_Bind();
				this.Drop_PrimaryGrade_Bind();
				this.Drop_jzType_Bind();
				this.fatherPrj_Bind();
				if (base.Request.QueryString["OpType"] != "ADD")
				{
					this.ViewState["INFOCODE"] = base.Request.QueryString["Code"];
					PrjInfoModel pIM = new PrjInfoModel();
					pIM = PrjInfoAction.GetSingleInfo(this.ViewState["INFOCODE"].ToString());
					this.TextBind(pIM);
				}
				else
				{
					this.txt_StartDate.Text = DateTime.Now.ToString("yyyy-M-dd");
					this.txt_EndDate.Text = DateTime.Now.ToString("yyyy-M-dd");
					this.ViewState["INFOCODE"] = Guid.NewGuid();
				}
				if (base.Request.QueryString["OpType"] == "SEE")
				{
					this.btnSave.Visible = false;
				}
				this.HdnPrjCode.Value = this.ViewState["INFOCODE"].ToString();
			}
			this.FilesBind(18);
		}
		private void TextBind(PrjInfoModel PIM)
		{
			this.txt_PrjCode.Text = PIM.PrjCode;
			this.txt_PrjName.Text = PIM.PrjName;
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
			if (PIM.PrjManager.ToString().Length > 9)
			{
				this.Txt_PrjManager.Value = PIM.PrjManager.Substring(9);
				this.HideManagerCode.Value = PIM.PrjManager.Substring(0, 8);
			}
			this.drop_jzType.SelectedValue = PIM.BuildingType;
			this.Txt_TotalHouseNum.Text = PIM.TotalHouseNum;
			this.Txt_BuildingArea.Text = PIM.BuildingArea;
			this.Txt_UsegrounArea.Text = PIM.UsegrounArea;
			this.Txt_UndergroundArea.Text = PIM.UndergroundArea;
			this.Txt_PrjFundInfo.Text = PIM.PrjFundInfo;
			this.Txt_OtherStatement.Text = PIM.OtherStatement;
			DataTable dataTable = this.BLL_n.getDataTable(PIM.PrjGuid.ToString());
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					this.Txt_telphone.Text = dataTable.Rows[i]["telephone"].ToString();
					this.Txt_grade.Text = dataTable.Rows[i]["grade"].ToString();
					dataTable.Rows[i]["businessman"].ToString();
					string[] array = dataTable.Rows[i]["businessman"].ToString().Split(new char[]
					{
						'-'
					});
					if (array.Length > 0)
					{
						for (int j = 0; j < array.Length; j++)
						{
							if (j == 0)
							{
								if (array[0].ToString() != "")
								{
									this.ManagerCodeYW.Value = array[0].ToString();
								}
							}
							else
							{
								if (j == 1 && array[1].ToString() != "")
								{
									this.Txt_businessman.Text = array[1].ToString();
								}
							}
						}
					}
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
			if (base.Request.QueryString["Type"] == "-3")
			{
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.ddt_state, 7, true, base.Request.QueryString["Type"]);
				return;
			}
			if (base.Request.QueryString["type"] == "-2")
			{
				string sqlString = "select * from XPM_Basic_CodeList where (TypeID=7)  and (IsVisible=1)and(IsValid=1) and codeId In(-3,-2,16,11)";
				DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						DropDownTreeItem dropDownTreeItem = new DropDownTreeItem();
						dropDownTreeItem.Text = dataTable.Rows[i]["CodeName"].ToString();
						dropDownTreeItem.Value = dataTable.Rows[i]["CodeID"].ToString();
						this.ddt_state.Items.Add(dropDownTreeItem);
					}
				}
			}
		}
		private void ddt_Area_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.ddt_Area, 19, true);
		}
		public void fatherPrj_Bind()
		{
			string sqlString = "select typeCode,prjName from pt_prjinfo where len(typeCode)=5";
			DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
			this.fatherPrj.DataSource = dataSource;
			this.fatherPrj.DataValueField = "typeCode";
			this.fatherPrj.DataTextField = "prjName";
			this.fatherPrj.DataBind();
			this.fatherPrj.Items.Insert(0, "---请选择---");
		}
		private PrjInfoModel GetTextValue()
		{
			PrjInfoModel prjInfoModel = new PrjInfoModel();
			prjInfoModel.PrjCode = this.txt_PrjCode.Text;
			prjInfoModel.PrjGuid = new Guid(this.ViewState["INFOCODE"].ToString());
			prjInfoModel.PrjName = this.txt_PrjName.Text.Trim();
			prjInfoModel.StartDate = Convert.ToDateTime(this.txt_StartDate.Text);
			prjInfoModel.EndDate = Convert.ToDateTime(this.txt_EndDate.Text);
			prjInfoModel.ComputeClass = "1";
			if (this.txt_PrjCost.Text.Trim().Length > 0)
			{
				prjInfoModel.PrjCost = this.txt_PrjCost.Text;
			}
			else
			{
				prjInfoModel.PrjCost = "0";
			}
			if (this.txt_Duration.Text.Trim().Length > 0)
			{
				prjInfoModel.Duration = this.txt_Duration.Text;
			}
			else
			{
				prjInfoModel.Duration = "0";
			}
			prjInfoModel.QualityClass = this.drop_QualityClass.SelectedValue;
			prjInfoModel.PrjKindClass = this.drop_PrjKindClass.SelectedValue;
			prjInfoModel.Area = this.ddt_Area.SelectedValue;
			prjInfoModel.Remark = this.txt_Remark.Text;
			prjInfoModel.Owner = this.txt_Owner.Value;
			prjInfoModel.Counsellor = this.txt_Counsellor.Text;
			prjInfoModel.Designer = this.txt_Designer.Text;
			prjInfoModel.Inspector = this.txt_Inspector.Text;
			prjInfoModel.PrjInfo = this.txt_PrjInfo.Text;
			prjInfoModel.PrjState = this.ddt_state.SelectedValue;
			prjInfoModel.OwnerCode = this.HdnOwnerCode.Value;
			prjInfoModel.PrjPlace = this.txt_PrjPlace.Text.Trim();
			prjInfoModel.Rank = this.drop_zzGrade.SelectedValue;
			prjInfoModel.BudgetWay = this.drop_ysType.SelectedValue;
			prjInfoModel.ContractWay = this.drop_cbType.SelectedValue;
			prjInfoModel.PayCondition = this.drop_Payment.SelectedValue;
			prjInfoModel.TenderWay = this.drop_zbType.SelectedValue;
			prjInfoModel.PayWay = this.drop_jsType.SelectedValue;
			prjInfoModel.KeyPart = this.drop_PrimaryGrade.SelectedValue;
			prjInfoModel.WorkUnit = this.Txt_WorkUnit.Value;
			prjInfoModel.LinkMan = this.Txt_LinkMan.Text;
			prjInfoModel.PrjManager = this.HideManagerCode.Value + "-" + this.Txt_PrjManager.Value;
			prjInfoModel.BuildingType = this.drop_jzType.SelectedValue;
			prjInfoModel.TotalHouseNum = this.Txt_TotalHouseNum.Text;
			prjInfoModel.BuildingArea = this.Txt_BuildingArea.Text;
			prjInfoModel.UsegrounArea = this.Txt_UsegrounArea.Text;
			prjInfoModel.UndergroundArea = this.Txt_UndergroundArea.Text;
			prjInfoModel.PrjFundInfo = this.Txt_PrjFundInfo.Text;
			prjInfoModel.OtherStatement = this.Txt_OtherStatement.Text;
			return prjInfoModel;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (!this.timecompare(this.txt_StartDate.Text.ToString(), this.txt_EndDate.Text.ToString()))
			{
				this.js.Text = "alert('结束时间不能早于开始时间!')";
				return;
			}
			PrjInfoModel prjInfoModel = new PrjInfoModel();
			prjInfoModel = this.GetTextValue();
			if (base.Request.QueryString["OpType"] == "ADD")
			{
				if (!PrjInfoAction.CheckCode(prjInfoModel.PrjCode))
				{
					this.js.Text = "alert('项目编号重复,请更改后保存！');";
					return;
				}
				if (PrjInfoAction.ADD(prjInfoModel) == 1)
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (this.ManagerCodeYW.Value.ToString() != "")
					{
						stringBuilder.Append(this.ManagerCodeYW.Value.ToString()).Append("-").AppendLine();
					}
					if (this.Txt_businessman.Text.Trim().ToString() != "")
					{
						stringBuilder.Append(this.Txt_businessman.Text.Trim().ToString()).AppendLine();
					}
					this.BLL_n.Add(prjInfoModel.PrjGuid.ToString(), this.Txt_grade.Text.ToString().Trim(), stringBuilder.ToString(), this.Txt_telphone.Text.Trim().ToString());
					this.js.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.js.Text = "alert('保存失败！');window.returnValue=false;window.close();";
				return;
			}
			else
			{
				if (this.ddt_state.SelectedValue == "16")
				{
					if (string.IsNullOrEmpty(this.HideManagerCode.Value))
					{
						this.js.Text = "alert('项目经理不能为空!')";
						return;
					}
					prjInfoModel.PrjState = "4";
					try
					{
						string text = string.Empty;
						if (this.fatherPrj.SelectedValue != "---请选择---")
						{
							text = PMAction.MakeClassCode(this.fatherPrj.SelectedValue.ToString());
						}
						else
						{
							text = PMAction.MakeClassCode("");
						}
						if (text != null && text.ToString() != "")
						{
							PrjInfoAction.ADD2(text, this.Session["yhdm"].ToString(), DateTime.Now.ToString(), prjInfoModel);
						}
						else
						{
							text = "001";
							PrjInfoAction.ADD2(text, this.Session["yhdm"].ToString(), DateTime.Now.ToString(), prjInfoModel);
						}
						string mes = string.Concat(new string[]
						{
							"项目立项通知：编号为",
							prjInfoModel.PrjCode,
							"的",
							prjInfoModel.PrjName,
							"项目已经立项。"
						});
						this.getOrganiger(prjInfoModel.PrjGuid.ToString(), mes, this.HideManagerCode.Value.ToString());
					}
					catch
					{
						this.js.Text = "alert('插入失败！')";
						base.Response.End();
					}
				}
				int num = PrjInfoAction.Update(prjInfoModel);
				if (num == 1)
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					if (this.ManagerCodeYW.Value.ToString() != "")
					{
						stringBuilder2.Append(this.ManagerCodeYW.Value.ToString()).Append("-").AppendLine();
					}
					if (this.Txt_businessman.Text.Trim().ToString() != "")
					{
						stringBuilder2.Append(this.Txt_businessman.Text.Trim().ToString()).AppendLine();
					}
					this.BLL_n.update(prjInfoModel.PrjGuid.ToString(), this.Txt_grade.Text.ToString().Trim(), stringBuilder2.ToString(), this.Txt_telphone.Text.Trim().ToString());
					this.js.Text = "alert('编辑成功!');window.returnValue=true;window.close();";
					return;
				}
				this.js.Text = "alert('" + sqlErr.sqlerr(num) + "'+'编辑失败!');window.returnValue=false;window.close();";
				return;
			}
		}
		private void Button1_ServerClick(object sender, EventArgs e)
		{
		}
		private void FilesBind(int moduleID)
		{
			this.FileLink1.MID = moduleID;
			this.FileLink1.FID = this.ViewState["INFOCODE"].ToString();
			this.FileLink1.Type = 1;
		}
		private bool timecompare(string begtime, string endtime)
		{
			DateTime t = DateTime.Parse(begtime);
			DateTime t2 = DateTime.Parse(endtime);
			return DateTime.Compare(t2, t) >= 0;
		}
		private void getOrganiger(string xgid, string Mes, string jsyhdm)
		{
			PublicInterface.SendSysMsg(new PTDBSJ
			{
				V_LXBM = "024",
				I_XGID = xgid,
				DTM_DBSJ = DateTime.Now,
				V_Content = Mes,
				V_DBLJ = "Epc/ProjectLayout/ProjectInfo/InfoView.aspx?OpType=SEE&Code=" + xgid,
				V_YHDM = jsyhdm,
				V_TPLJ = "new_Mail.gif",
				C_OpenFlag = "1"
			});
		}
		protected void ddt_state_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ddt_state.SelectedValue == "16")
			{
				this.fatherPrj.Enabled = true;
			}
		}
	}


