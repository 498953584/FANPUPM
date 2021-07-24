using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.prj;
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
	public partial class TechnologyJDEdit : NBasePage, System.Web.SessionState.IRequiresSessionState
	{
		private string _Type = "";
		private string _Id = "";
		private string _PrjCode = "";
		private string _PrjName = "";
		private string _RecordId;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["Type"] != null)
				{
					this._Type = base.Request.QueryString["Type"].ToString();
				}
				if (base.Request.QueryString["pc"] != null)
				{
					this._PrjCode = base.Request.QueryString["pc"].ToString();
				}
				if (base.Request.QueryString["pn"] != null)
				{
					this._PrjName = base.Request.QueryString["pn"].ToString();
				}
				this.ViewState["TYPE"] = this._Type;
				this.ViewState["PRJCODE"] = this._PrjCode;
				this.ViewState["PRJNAME"] = this._PrjName;
				if (this._Type == "Upd")
				{
					this._Id = base.Request.QueryString["Id"].ToString();
					this.TxtId.Enabled = false;
					this.GetTechnologyOfUpd(this._Id);
					this.lb_change.Text = "编辑技术交底";
					this.FileLink1.Type = 1;
				}
				else
				{
					if (this._Type == "Add")
					{
						this.FileLink1.Type = 2;
						this.TxtId.Text = TechnologyJDAction.GetMaxId().ToString();
						this.TxtId.Enabled = false;
						this.TxtPrjName.Text = this._PrjName;
						this.DateFill.Text = DateTime.Now.ToString("yyyy-MM-dd");
						this.DateTellTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
						this.HdnFill.Value = userManageDb.GetCurrentUserInfo().UserCode;
						this.TxtFillName.Text = userManageDb.GetCurrentUserInfo().UserName;
						this.HdnTell.Value = userManageDb.GetCurrentUserInfo().UserCode;
						this.TxtTellName.Value = userManageDb.GetCurrentUserInfo().UserName;
						this.HdnByTell.Value = userManageDb.GetCurrentUserInfo().UserCode;
						this.TxtByTellName.Text = userManageDb.GetCurrentUserInfo().UserName;
						this.ViewState["RecordId"] = TechnologyJDAction.GetMaxId().ToString();
						this._RecordId = this.ViewState["RecordId"].ToString();
						this._Id = this.TxtId.Text;
						this.lb_change.Text = "新增技术交底";
						this.hdnTechGuid.Value = Guid.NewGuid().ToString();
					}
					else
					{
						if (this._Type == "View")
						{
							if (base.Request.QueryString["Id"] != null)
							{
								this._Id = base.Request.QueryString["Id"].ToString();
							}
							if (base.Request.QueryString["ic"] != null)
							{
								string guid = base.Request.QueryString["ic"].ToString();
								DataTable modelByGuid = TechnologyJDAction.GetModelByGuid(guid);
								this._Id = modelByGuid.Rows[0]["MainId"].ToString();
							}
							this.FileLink1.Type = 0;
							this.FileLink1.Visible = false;
							this.BtnSave.Visible = false;
							this.SetControl();
							this.TxtId.Enabled = false;
							this.GetTechnologyOfUpd(this._Id);
							this.lb_change.Text = "查看技术交底";
							this.BunClose.Value = "关 闭";
							if (this.cbkmark.Checked)
							{
								this.DDTClass.Enabled = false;
							}
							this.Img1.Disabled = true;
							this.TxtTellName.Disabled = true;
							this.Literal1.Text = FileView.FilesBind(1728, this.hdnTechGuid.Value.Trim());
						}
					}
				}
				this.ViewState["ID"] = this._Id;
				this.FileLink1.MID = 1728;
				this.FileLink1.FID = this.hdnTechGuid.Value.Trim();
			}
			else
			{
				this._Type = this.ViewState["TYPE"].ToString();
				this._PrjCode = this.ViewState["PRJCODE"].ToString();
				if (this._Type == "Add")
				{
					this._PrjName = this.ViewState["PRJNAME"].ToString();
					this._RecordId = this.ViewState["RecordId"].ToString();
				}
				else
				{
					this._PrjName = this.ViewState["PRJNAME"].ToString();
					this._Id = this.ViewState["ID"].ToString();
				}
			}
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void SetControl()
		{
			this.TxtId.Enabled = false;
			this.DateFill.Enabled = false;
			this.TxtFillName.Enabled = false;
			this.TxtPrjName.Enabled = false;
			this.TxtConUnit.Enabled = false;
			this.TxtByTellUnit.Enabled = false;
			this.TxtTellName.Disabled = false;
			this.TxtByTellName.Enabled = false;
			this.TxtTellLocus.Enabled = false;
			this.DateTellTime.Enabled = false;
			this.TxtContentAbstract.Enabled = false;
			this.TxtRemark.Enabled = false;
			this.cbkmark.Enabled = false;
		}
		private void GetTechnologyOfUpd(string Id)
		{
			DataTable technologyOfSingle = TechnologyJDAction.GetTechnologyOfSingle(Id);
			this.TxtId.Text = technologyOfSingle.Rows[0]["SerialNumber"].ToString();
			this.DateFill.Text = Convert.ToDateTime(technologyOfSingle.Rows[0]["FillDate"].ToString()).ToShortDateString();
			this.TxtFillName.Text = technologyOfSingle.Rows[0]["FillName"].ToString();
			this.HdnFill.Value = technologyOfSingle.Rows[0]["FillPeople"].ToString();
			this.TxtPrjName.Text = technologyOfSingle.Rows[0]["PrejectName"].ToString();
			this.TxtConUnit.Text = technologyOfSingle.Rows[0]["ConstructionUnit"].ToString();
			this.TxtByTellUnit.Text = technologyOfSingle.Rows[0]["ByTellUnit"].ToString();
			this.TxtTellName.Value = technologyOfSingle.Rows[0]["TellName"].ToString();
			this.HdnTell.Value = technologyOfSingle.Rows[0]["TellPeople"].ToString();
			this.TxtByTellName.Text = technologyOfSingle.Rows[0]["ByTellPeople"].ToString();
			this.TxtTellLocus.Text = technologyOfSingle.Rows[0]["TellLocus"].ToString();
			this.DateTellTime.Text = Convert.ToDateTime(technologyOfSingle.Rows[0]["TellDate"].ToString()).ToShortDateString();
			this.TxtContentAbstract.Text = technologyOfSingle.Rows[0]["TellContentAbstract"].ToString();
			this.TxtRemark.Text = technologyOfSingle.Rows[0]["Remark"].ToString();
			this.hdnTechGuid.Value = technologyOfSingle.Rows[0]["TechGuid"].ToString();
			this.hidenClass.Value = technologyOfSingle.Rows[0]["filesType"].ToString();
			this.hidenMark.Value = technologyOfSingle.Rows[0]["mark"].ToString();
			if (technologyOfSingle.Rows[0]["mark"].ToString() == "2")
			{
				this.cbkmark.Checked = false;
				return;
			}
			this.cbkmark.Checked = true;
		}
		private TechnologyJDInfo GetTechnologyInfo()
		{
			TechnologyJDInfo technologyJDInfo = new TechnologyJDInfo();
			if (this._Type == "Add")
			{
				technologyJDInfo.MainId = int.Parse(this._RecordId);
			}
			else
			{
				technologyJDInfo.MainId = int.Parse(this._Id);
			}
			technologyJDInfo.PrjCode = this._PrjCode;
			technologyJDInfo.SeriaNumber = this.TxtId.Text.Trim();
			technologyJDInfo.FillDate = Convert.ToDateTime(this.DateFill.Text);
			technologyJDInfo.FillPeople = this.HdnFill.Value;
			technologyJDInfo.PrejectName = this.TxtPrjName.Text.Trim();
			technologyJDInfo.ConstructionUnit = this.TxtConUnit.Text;
			technologyJDInfo.ByTellUnit = this.TxtByTellUnit.Text;
			technologyJDInfo.TellPeople = this.HdnTell.Value;
			technologyJDInfo.ByTellPeople = this.TxtByTellName.Text;
			technologyJDInfo.TellLocus = this.TxtTellLocus.Text.Trim();
			technologyJDInfo.TellDate = Convert.ToDateTime(this.DateTellTime.Text.Trim());
			technologyJDInfo.TellContentAbstract = this.TxtContentAbstract.Text;
			technologyJDInfo.Remark = this.TxtRemark.Text;
			technologyJDInfo.FlowState = -1;
			technologyJDInfo.TechGuid = this.hdnTechGuid.Value.Trim();
			return technologyJDInfo;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			TechnologyTellLogic technologyTellLogic = new TechnologyTellLogic();
			if (this.TxtId.Text.Trim() == "")
			{
				base.RegisterScript("top.ui.alert('编号不能为空！')");
				return;
			}
			int mark;
			if (this.cbkmark.Checked)
			{
				mark = 3;
			}
			else
			{
				mark = 2;
			}
			TechnologyJDInfo technologyInfo = this.GetTechnologyInfo();
			if (this._Type == "Add")
			{
				int num = TechnologyJDAction.Add(technologyInfo);
				if (num == 1)
				{
					technologyTellLogic.Update(mark, int.Parse(this.DDTClass.SelectedValue.ToString()), technologyTellLogic.GetMaxId());
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_flowclass' });");
					this.BtnSave.Enabled = false;
					return;
				}
				base.RegisterScript("top.ui.alert('保存失败！'); ");
				return;
			}
			else
			{
				int num = TechnologyJDAction.Upd(technologyInfo);
				if (num == 1)
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_flowclass' });");
					return;
				}
				base.RegisterScript("top.ui.alert('保存失败！'); ");
				return;
			}
		}
	}


