using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.epm.datum;
using com.jwsoft.pm.entpm;
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
	public partial class CheckManage : NBasePage, IRequiresSessionState
	{
		private static string page_type = string.Empty;
		private string pk = string.Empty;
		private DatumLogic dl = new DatumLogic();
		private int id_max;
		private string rectifyMarkID = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ViewState["Mark"] = string.Empty;
			if (!base.IsPostBack)
			{
				this.lbTitle.Text = "项目检查";
				this.DateBox_jcrq.Text = DateTime.Now.ToString("d");
				if (!string.IsNullOrWhiteSpace(this.pk))
				{
					this.bind(this.pk);
				}
				DataTable allCheckClassCollections = CheckClassAction.GetAllCheckClassCollections();
				this.DropDownList_lb.DataSource = allCheckClassCollections;
				this.DropDownList_lb.DataTextField = "ItemInspectSortName";
				this.DropDownList_lb.DataValueField = "SortID";
				this.DropDownList_lb.DataBind();
				if (!string.IsNullOrWhiteSpace(this.pk))
				{
					this.FileLink1.MID = 1755;
					this.FileLink1.FID = this.pk;
					this.FileLink1.Type = 1;
					this.FileLink2.MID = 1755;
					this.FileLink2.Type = 1;
					if (string.IsNullOrEmpty(this.rectifyMarkID))
					{
						this.rectifyMarkID = Guid.NewGuid().ToString();
					}
					this.ViewState["rmid"] = this.rectifyMarkID;
					this.FileLink2.FID = this.rectifyMarkID;
				}
				else
				{
					this.id_max = this.dl.getMaxID("Prj_ItemInspect");
					this.FileLink1.Type = 1;
					this.FileLink1.MID = 1755;
					this.FileLink1.FID = this.id_max.ToString();
					this.FileLink2.Type = 1;
					this.FileLink2.MID = 1755;
					this.rectifyMarkID = Guid.NewGuid().ToString();
					this.ViewState["rmid"] = this.rectifyMarkID;
					this.FileLink2.FID = this.rectifyMarkID;
				}
				if (base.Request.Params["Type"] != null)
				{
					if (base.Request["Levels"] != null && base.Request["Levels"].ToString() == "0")
					{
						if (base.Request.Params["Type"].ToString() == PageType.Edit.ToString())
						{
							this.DateBox_sjwcsj.Enabled = false;
							this.DateBox_jhwcsj.Enabled = false;
							this.TextBox_sjjg.Enabled = false;
							this.ddlCertifiResult.Enabled = false;
							this.TextBox_xmjh.Enabled = false;
							this.Textbox_zgnr.Enabled = false;
						}
						if (base.Request.Params["Type"].ToString() == PageType.Rectify.ToString())
						{
							this.SetDisplayControl();
							this.DateBox_sjwcsj.Enabled = true;
							this.DateBox_jhwcsj.Enabled = true;
							this.TextBox_xmjh.Enabled = true;
							this.lbTitle.Text = "项目整改";
							this.FileLink1.Visible = false;
							this.Literal1.Text = FileView.FilesBind(1755, base.Request["pk"].ToString());
						}
						if (base.Request.Params["Type"].ToString() == PageType.Certify.ToString())
						{
							this.SetDisplayControl();
							this.ddlCertifiResult.Enabled = true;
							this.lbTitle.Text = "项目验证";
							this.FileLink1.Visible = false;
							this.Literal1.Text = FileView.FilesBind(1755, base.Request["pk"].ToString());
							this.FileLink2.Visible = false;
							this.Literal2.Text = FileView.FilesBind(1755, this.ViewState["rmid"].ToString());
						}
					}
					if (base.Request.Params["Type"].ToString() == "View")
					{
						this.Button_save.Visible = false;
						this.Button1.Style.Add("display", "");
						this.Button1.Value = "关 闭";
						this.SetDisplayControl();
						this.FileLink1.Type = 0;
						this.FileLink1.Visible = false;
						this.Literal1.Text = FileView.FilesBind(1755, this.pk);
						this.FileLink2.Visible = false;
						this.Literal2.Text = FileView.FilesBind(1755, this.ViewState["rmid"].ToString());
					}
				}
			}
		}
		private void SetDisplayControl()
		{
			this.Textbox_bz.CssClass = "txtsee";
			this.Textbox_bz.Enabled = false;
			this.TextBox_jcdw.Enabled = false;
			this.TextBox_jcfzr.Enabled = false;
			this.Textbox_jcjg.Enabled = false;
			this.TextBox_jcnr.Enabled = false;
			this.TextBox_jcyj.Enabled = false;
			this.TextBox_sjdw.Enabled = false;
			if (CheckManage.page_type.ToLower() != "rectify")
			{
				this.cbkmark.Enabled = false;
				this.TextBox_xmjh.Enabled = false;
				this.Textbox_zgnr.Enabled = false;
			}
			else
			{
			}
			if (CheckManage.page_type.ToLower() != "Certify".ToLower())
			{
				this.cbkmark.Enabled = false;
				this.TextBox_sjjg.Enabled = false;
			}
			this.DateBox_jcrq.Enabled = false;
			this.DateBox_jhwcsj.Enabled = false;
			this.DateBox_sjwcsj.Enabled = false;
			this.DateBox_yqwcsj.Enabled = false;
			this.DropDownList_lb.Enabled = false;
			this.ddlCertifiResult.Enabled = false;
			this.ddlCheckResult.Enabled = false;
			this.ddlCheckResults.Enabled = false;
		}
		private void bind(string pk)
		{
			CheckInfo checkInfo = CheckAction.GetCheckInfo(pk);
			this.TextBox_jcdw.Text = checkInfo.ExamineUnit;
			this.TextBox_sjdw.Text = checkInfo.AcceptCheckUnit;
			this.TextBox_jcnr.Text = checkInfo.AcceptCheckContent;
			this.TextBox_jcyj.Text = checkInfo.AcceptCheckGist;
			this.DropDownList_lb.SelectedValue = checkInfo.AcceptCheckSort.ToString();
			this.TextBox_jcfzr.Text = checkInfo.AcceptCheckAnswerForPerson;
			this.DateBox_jcrq.Text = checkInfo.AcceptCheckDate.ToString("d");
			this.Textbox_jcjg.Text = checkInfo.AcceptCheckResult;
			this.Textbox_zgnr.Text = checkInfo.CompleteApplyContent;
			this.Textbox_bz.Text = checkInfo.Remark;
			this.DateBox_yqwcsj.Text = checkInfo.requestfinishtime.ToString("d");
			this.DateBox_jhwcsj.Text = checkInfo.planfinishtime.ToString("d");
			this.DateBox_sjwcsj.Text = checkInfo.factfinishtime.ToString("d");
			this.TextBox_sjjg.Text = checkInfo.factresult;
			this.TextBox_xmjh.Text = checkInfo.prjplan;
			this.ddlCheckResult.SelectedValue = checkInfo.CheckResult.ToString();
			this.ddlCertifiResult.SelectedValue = checkInfo.CertifiResult.ToString();
			this.ddlCheckResults.SelectedValue = checkInfo.checkResults.ToString();
			this.DDTClass.SelectedValue = checkInfo.FilesType.ToString();
			this.ViewState["Mark"] = checkInfo.Mark.ToString();
			if (checkInfo.Mark.ToString() != "2")
			{
				this.cbkmark.Checked = true;
			}
			else
			{
				this.cbkmark.Checked = false;
			}
			this.rectifyMarkID = checkInfo.RectifyMarkID;
		}
		protected override void OnInit(EventArgs e)
		{
			if (base.Request.QueryString["pk"] != null)
			{
				this.pk = base.Request.QueryString["pk"].ToString();
				this.HiddenField1.Value = this.pk;
			}
			if (base.Request.QueryString["ic"] != null)
			{
				string arg = base.Request.QueryString["ic"].ToString();
				DataTable table = Common2.GetTable("Prj_ItemInspect", string.Format(" WHERE UID='{0}'", arg));
				if (table.Columns.Count > 0)
				{
					this.pk = table.Rows[0][0].ToString();
				}
			}
			if (base.Request.QueryString["Type"] != null)
			{
				CheckManage.page_type = base.Request.QueryString["Type"].ToString();
			}
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void Button_save_Click(object sender, EventArgs e)
		{
			if (this.TextBox_jcnr.Text.Length > this.TextBox_jcnr.MaxLength)
			{
				this.JavaScriptControl1.Text = "top.ui.alert('检查内容输入的内容过长！')";
				return;
			}
			if (this.TextBox_jcyj.Text.Length > this.TextBox_jcyj.MaxLength)
			{
				this.JavaScriptControl1.Text = "top.ui.alert('检查依据输入的内容过长！')";
				return;
			}
			if (this.Textbox_jcjg.Text.Length > this.Textbox_jcjg.MaxLength)
			{
				this.JavaScriptControl1.Text = "top.ui.alert('检查结果输入的内容过长！')";
				return;
			}
			if (this.Textbox_zgnr.Text.Length > this.Textbox_zgnr.MaxLength)
			{
				this.JavaScriptControl1.Text = "top.ui.alert('整改要求输入的内容过长！')";
				return;
			}
			if (this.TextBox_xmjh.Text.Length > this.TextBox_xmjh.MaxLength)
			{
				this.JavaScriptControl1.Text = "top.ui.alert('项目部整改计划输入的内容过长！')";
				return;
			}
			if (this.TextBox_sjjg.Text.Length > this.TextBox_sjjg.MaxLength)
			{
				this.JavaScriptControl1.Text = "top.ui.alert('实际整改结果输入的内容过长！')";
				return;
			}
			if (this.Textbox_bz.Text.Length > this.Textbox_bz.MaxLength)
			{
				this.JavaScriptControl1.Text = "top.ui.alert('备注输入的内容过长！')";
				return;
			}
			CheckInfo checkInfo = new CheckInfo();
			if (base.Request["pk"] != null)
			{
				checkInfo.ID = int.Parse(base.Request["pk"].ToString());
			}
			checkInfo.Flags = 2;
			checkInfo.ExamineUnit = this.TextBox_jcdw.Text;
			checkInfo.AcceptCheckAnswerForPerson = this.TextBox_jcfzr.Text;
			checkInfo.AcceptCheckContent = this.TextBox_jcnr.Text;
			if (!string.IsNullOrEmpty(this.DateBox_jcrq.Text))
			{
				checkInfo.AcceptCheckDate = DateTime.Parse(this.DateBox_jcrq.Text);
			}
			else
			{
				checkInfo.AcceptCheckDate = DateTime.Now;
			}
			checkInfo.AcceptCheckGist = this.TextBox_jcyj.Text;
			checkInfo.AcceptCheckResult = this.Textbox_jcjg.Text;
			if (this.DropDownList_lb.SelectedValue != "")
			{
				checkInfo.AcceptCheckSort = int.Parse(this.DropDownList_lb.SelectedValue);
			}
			checkInfo.AcceptCheckUnit = this.TextBox_sjdw.Text;
			checkInfo.CompleteApplyContent = this.Textbox_zgnr.Text;
			checkInfo.Remark = this.Textbox_bz.Text;
			if (this.DateBox_yqwcsj.Text != "")
			{
				checkInfo.requestfinishtime = DateTime.Parse(this.DateBox_yqwcsj.Text);
			}
			if (this.DateBox_jhwcsj.Text != "")
			{
				checkInfo.planfinishtime = DateTime.Parse(this.DateBox_jhwcsj.Text);
			}
			if (this.DateBox_sjwcsj.Text != "")
			{
				checkInfo.factfinishtime = DateTime.Parse(this.DateBox_sjwcsj.Text);
			}
			checkInfo.prjplan = this.TextBox_xmjh.Text;
			checkInfo.factresult = this.TextBox_sjjg.Text;
			checkInfo.IsRectified = false;
			checkInfo.checkResults = int.Parse(this.ddlCheckResults.SelectedValue);
			base.Request.Params["Type"].ToString();
			if (base.Request.Params["Type"].ToString() == PageType.Rectify.ToString())
			{
				checkInfo.IsRectified = true;
			}
			else
			{
				if (base.Request.Params["Type"].ToString() == PageType.Certify.ToString())
				{
					checkInfo.IsRectified = (this.ddlCertifiResult.SelectedValue == "2");
				}
			}
			checkInfo.CertifiResult = int.Parse(this.ddlCertifiResult.SelectedValue);
			checkInfo.CheckResult = int.Parse(this.ddlCheckResult.SelectedValue);
			int mark = 2;
			if (this.cbkmark.Checked)
			{
				mark = 3;
			}
			if (this.ViewState["Mark"].ToString().Equals("1"))
			{
				mark = 1;
			}
			checkInfo.Mark = mark;
			checkInfo.FilesType = int.Parse(this.DDTClass.SelectedValue.ToString());
			checkInfo.RectifyMarkID = this.FileLink2.FID;
			if (base.Request["Levels"] != null)
			{
				checkInfo.Flags = int.Parse(base.Request["Levels"].ToString());
			}
			if (base.Request["Prjcode"] != null)
			{
				checkInfo.PrjCode = base.Request["Prjcode"].ToString();
			}
			new StringBuilder();
			bool flag;
			if (base.Request["pk"] == null)
			{
				flag = CheckAction.CheckInfoOp(checkInfo, "Insert");
			}
			else
			{
				flag = CheckAction.CheckInfoOp(checkInfo, "Update");
			}
			if (flag)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_checklist' });");
				return;
			}
			base.RegisterScript("top.ui.alert('保存失败！');");
		}
	}


