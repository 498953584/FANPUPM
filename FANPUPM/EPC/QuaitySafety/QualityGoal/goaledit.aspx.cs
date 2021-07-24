using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.epm.datum;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class GoalEdit : NBasePage, IRequiresSessionState
	{
		private string page_type = string.Empty;
		private string page_PrjCode = string.Empty;
		private string page_Code = string.Empty;
		private QualityGoalAction ActObj = new QualityGoalAction();
		private QualityGoalInfo InfoObj = new QualityGoalInfo();
		private BaseEditPage bp = new BaseEditPage();

		public string Page_type
		{
			get
			{
				return this.page_type;
			}
			set
			{
				this.page_type = value;
			}
		}
		public string Page_PrjCode
		{
			get
			{
				return this.page_PrjCode;
			}
			set
			{
				this.page_PrjCode = value;
			}
		}
		public string Page_Code
		{
			get
			{
				return this.page_Code;
			}
			set
			{
				this.page_Code = value;
			}
		}
		protected override void OnInit(EventArgs e)
		{
			if (base.Request.QueryString["PrjCode"] != null && base.Request.QueryString["PrjCode"].ToString() != "")
			{
				this.page_PrjCode = base.Request.QueryString["PrjCode"].ToString();
				this.hdnprjcode.Value = this.Page_PrjCode;
			}
			if (base.Request.QueryString["Code"] != null && base.Request.QueryString["Code"].ToString() != "")
			{
				this.Page_Code = base.Request.QueryString["Code"].ToString();
				this.hdnPn.Value = this.Page_Code;
			}
			if (base.Request.QueryString["Type"] != null && base.Request.QueryString["Type"].ToString() != "")
			{
				this.page_type = base.Request.QueryString["Type"].ToString();
			}
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
			base.OnInit(e);
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				string a;
				if ((a = this.page_type) != null)
				{
					if (!(a == "Add"))
					{
						if (!(a == "Update"))
						{
							if (a == "Query")
							{
								this.LblHead.Text = "查看工程质量目标";
								DataTable singleRow = QualityGoalAction.GetSingleRow(this.Page_Code);
								if (singleRow.Rows.Count != 0)
								{
									this.TxtScheduleCode.Value = singleRow.Rows[0]["TaskName"].ToString();
									this.TxtGoal.Text = singleRow.Rows[0]["QualityGoal"].ToString();
									this.Txtremark.Text = singleRow.Rows[0]["Remark"].ToString();
									this.hdnTempScheduleCode.Value = singleRow.Rows[0]["ScheduleCode"].ToString();
									this.hdnprjcode.Value = singleRow.Rows[0]["prjcode"].ToString();
									if (singleRow.Rows[0]["mark"] != null)
									{
										int num = int.Parse(singleRow.Rows[0]["mark"].ToString());
										if (num != 2)
										{
											this.cbkmark.Checked = true;
											if (singleRow.Rows[0]["filesType"] != null)
											{
												this.hidenClass.Value = singleRow.Rows[0]["filesType"].ToString();
											}
										}
										this.hdnmark.Value = num.ToString();
									}
								}
							}
						}
						else
						{
							this.LblHead.Text = "修改工程质量目标";
							DataTable singleRow2 = QualityGoalAction.GetSingleRow(this.Page_Code);
							if (singleRow2.Rows.Count != 0)
							{
								this.TxtScheduleCode.Value = singleRow2.Rows[0]["TaskName"].ToString();
								this.TxtGoal.Text = singleRow2.Rows[0]["QualityGoal"].ToString();
								this.Txtremark.Text = singleRow2.Rows[0]["Remark"].ToString();
								this.hdnTempScheduleCode.Value = singleRow2.Rows[0]["ScheduleCode"].ToString();
								this.hdnprjcode.Value = singleRow2.Rows[0]["prjcode"].ToString();
								if (singleRow2.Rows[0]["mark"] != null && singleRow2.Rows[0]["mark"].ToString() != "")
								{
									int num2 = int.Parse(singleRow2.Rows[0]["mark"].ToString());
									if (num2 != 2)
									{
										this.cbkmark.Checked = true;
										if (singleRow2.Rows[0]["filesType"] != null && singleRow2.Rows[0]["filesType"].ToString() != "")
										{
											this.hidenClass.Value = singleRow2.Rows[0]["filesType"].ToString();
										}
									}
								}
							}
						}
					}
					else
					{
						if (base.Request["PrjCode"] == "")
						{
							this.JS.Text = "alert('请选择项目!');window.close();";
						}
						this.LblHead.Text = "新增工程质量目标";
					}
				}
				this.TxtScheduleCode.Attributes["onclick"] = "javascript:pickSchedule(this);";
			}
		}
		protected void BtnOK_Click(object sender, EventArgs e)
		{
			if (this.page_Code != "")
			{
				this.hdnPn.Value = this.page_Code;
			}
			this.InfoObj.PrjCode = new Guid(this.hdnprjcode.Value);
			this.InfoObj.ScheduleName = this.TxtScheduleCode.Value;
			this.InfoObj.ScheduleCode = this.hdnTempScheduleCode.Value;
			this.InfoObj.QualityGoal = this.TxtGoal.Text;
			this.InfoObj.Remark = this.Txtremark.Text;
			new DatumLogic();
			int mark = 2;
			int filesType = 0;
			if (this.cbkmark.Checked)
			{
				mark = 3;
				filesType = int.Parse(this.DDTClass.SelectedValue.ToString());
				this.filesClass.Visible = true;
			}
			else
			{
				this.filesClass.Visible = false;
			}
			this.InfoObj.Mark = mark;
			this.InfoObj.FilesType = filesType;
			string a;
			if ((a = this.Page_type) != null)
			{
				if (!(a == "Add"))
				{
					if (a == "Update")
					{
						this.InfoObj.i_id = Convert.ToInt32(this.Page_Code);
						QualityGoalAction.Update(this.InfoObj);
					}
				}
				else
				{
					new object();
					QualityGoalAction.ADD(this.InfoObj);
				}
			}
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_goallist' })");
		}
	}


