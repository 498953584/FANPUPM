using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.epm.datum;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class MeasureEdit : NBasePage, IRequiresSessionState
	{
		private string page_type = string.Empty;
		private string page_PrjCode = string.Empty;
		private string page_Code = string.Empty;
		private SafetyMeasureInfo InfoObj = new SafetyMeasureInfo();
		private SafetyMeasureAction ActObj = new SafetyMeasureAction();

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
						if (a == "Update")
						{
							this.LblHead.Text = "修改安全目标方案";
							SafetyMeasureAction.GetSingleRow(this.Page_Code);
							this.InfoObj = this.ActObj.GetSafetyMeasureModel(this.Page_Code);
							if (this.InfoObj != null)
							{
								this.TxtScheduleCode.Text = this.InfoObj.TaskName;
								this.TxtMeasure.Text = this.InfoObj.SaftyMeasure;
								this.Txtremark.Text = this.InfoObj.Remark;
								this.hdnTempScheduleCode.Value = this.InfoObj.ScheduleCode;
								this.hdnprjcode.Value = this.InfoObj.PrjCode.ToString();
								int arg_13D_0 = this.InfoObj.Mark;
								if (this.InfoObj.Mark.ToString() != "")
								{
									int mark = this.InfoObj.Mark;
									if (mark != 2)
									{
										this.cbkmark.Checked = true;
										int arg_184_0 = this.InfoObj.FilesType;
										if (this.InfoObj.FilesType.ToString() != "")
										{
											this.hidenClass.Value = this.InfoObj.FilesType.ToString();
										}
									}
									this.hdnmark.Value = mark.ToString();
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
						this.LblHead.Text = "新增安全目标方案";
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
				this.InfoObj.i_id = int.Parse(this.page_Code);
			}
			this.InfoObj.PrjCode = new Guid(this.hdnprjcode.Value);
			this.InfoObj.ScheduleName = this.TxtScheduleCode.Text;
			this.InfoObj.ScheduleCode = this.hdnTempScheduleCode.Value;
			this.InfoObj.SaftyMeasure = this.TxtMeasure.Text;
			this.InfoObj.Remark = this.Txtremark.Text;
			new DatumLogic();
			int num = 0;
			if (this.cbkmark.Checked)
			{
				this.InfoObj.Mark = 3;
				this.InfoObj.FilesType = int.Parse(this.DDTClass.SelectedValue.ToString());
				this.filesClass.Visible = true;
			}
			else
			{
				this.InfoObj.Mark = 2;
				this.InfoObj.FilesType = 0;
				this.filesClass.Visible = false;
			}
			string a;
			if ((a = this.Page_type) != null)
			{
				if (!(a == "Add"))
				{
					if (a == "Update")
					{
						num = SafetyMeasureAction.Update(this.InfoObj);
					}
				}
				else
				{
					num = SafetyMeasureAction.Add(this.InfoObj);
				}
			}
			if (num > 0)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_measurelist' });");
				return;
			}
			base.RegisterScript("top.ui.alert('修改失败');");
		}
	}


