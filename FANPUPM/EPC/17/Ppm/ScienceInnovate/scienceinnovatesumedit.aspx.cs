using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ScienceInnovateSumEdit : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
				if (base.Request.Params["id"] == null)
				{
					this.ViewState["ISEDIT"] = "false";
					this.txtReporter.Text = userManageDb.GetCurrentUserInfo().UserName;
					this.txtReportTime.Text = DateTime.Today.ToString("yyyy-MM-dd");
					if (base.Request["PrjCode"] != null)
					{
						string sqlString = "select PrjName   from pt_PrjInfo where Prjguid='" + base.Request["PrjCode"].ToString() + "'";
						DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
						if (dataTable.Rows.Count > 0)
						{
							this.txtSumPrjName.Text = dataTable.Rows[0]["PrjName"].ToString();
						}
					}
					this.FileLink1.MID = 1754;
					this.hdnWfGuid.Value = Guid.NewGuid().ToString();
					this.FileLink1.FID = this.hdnWfGuid.Value.Trim();
					this.FileLink1.Type = 2;
				}
				else
				{
					this.hidID.Value = base.Request.Params["id"].ToString();
					this.BindData();
					this.ViewState["ISEDIT"] = "true";
					this.FileLink1.MID = 1754;
					this.FileLink1.Type = 1;
				}
				if (base.Request.Params["Type"] != null)
				{
					string text = base.Request.Params["Type"].ToString();
					if (text != null && text == "view")
					{
						this.FileLink1.Type = 0;
						this.FileLink1.Visible = false;
						this.Literal1.Text = FileView.FilesBind(1754, this.hdnWfGuid.Value.Trim());
						this.txtReporter.Enabled = false;
						this.txtReportTime.Enabled = false;
						this.txtSummaryName.Enabled = false;
						this.TextBox5.Enabled = false;
						this.btnSave.Attributes.Add("style", "display:none;");
						this.BunClose.Value = "关 闭";
						this.hdnType.Value = "view";
						this.cbkmark.Disabled = true;
						this.DDTClass.Enabled = false;
						this.txtReportUnit.Enabled = false;
					}
				}
				if (base.Request["ic"] != null)
				{
					this.divFooter2.Visible = false;
				}
			}
		}
		private void BindData()
		{
			string text = "select * from Prj_Summary where";
			if (base.Request["ic"] != null)
			{
				text = text + " wfguid='" + base.Request["ic"].ToString() + "'";
			}
			else
			{
				if (base.Request["id"] != null)
				{
					text = text + " id='" + base.Request.Params["id"].ToString() + "'";
				}
			}
			DataTable dataTable = publicDbOpClass.DataTableQuary(text);
			if (dataTable.Rows.Count > 0)
			{
				this.hidID.Value = dataTable.Rows[0]["ID"].ToString();
				this.txtReportUnit.Text = dataTable.Rows[0]["bm"].ToString();
				this.txtSummaryName.Text = dataTable.Rows[0]["SummaryName"].ToString();
				this.txtReportTime.Text = DateTime.Parse(dataTable.Rows[0]["ReporterDate"].ToString()).ToShortDateString();
				this.TextBox5.Text = dataTable.Rows[0]["Comment"].ToString();
				this.hdnWfGuid.Value = dataTable.Rows[0]["WfGuid"].ToString();
				this.FileLink1.FID = this.hdnWfGuid.Value.Trim();
				if (userManageDb.GetCurrentUserInfo(dataTable.Rows[0]["Reporter"].ToString()) != null)
				{
					this.txtReporter.Text = userManageDb.GetCurrentUserInfo(dataTable.Rows[0]["Reporter"].ToString()).UserName;
				}
				else
				{
					this.txtReporter.Text = "-----------";
				}
				text = "select PrjName   from pt_PrjInfo where Prjguid='" + dataTable.Rows[0]["PrjId"].ToString() + "'";
				DataTable dataTable2 = publicDbOpClass.DataTableQuary(text);
				if (dataTable2.Rows.Count > 0)
				{
					this.txtSumPrjName.Text = dataTable2.Rows[0]["PrjName"].ToString();
				}
				this.hdnmark.Value = dataTable.Rows[0]["mark"].ToString();
				this.DDTClass.SelectedValue = dataTable.Rows[0]["filesType"].ToString();
				if (dataTable.Rows[0]["mark"].ToString().Equals("2"))
				{
					this.cbkmark.Checked = false;
					this.TrGDType.Attributes.Add("style", "display:none;");
					return;
				}
				this.cbkmark.Checked = true;
				this.TrGDType.Attributes.Add("style", "display:block;");
			}
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
			bool flag = Convert.ToBoolean(this.ViewState["ISEDIT"].ToString().ToLower());
			int num = 2;
			if (this.cbkmark.Checked)
			{
				num = 3;
			}
			string sqlString;
			if (flag)
			{
				sqlString = string.Concat(new object[]
				{
					"update Prj_Summary set bm='",
					this.txtReportUnit.Text,
					"',ReporterDate='",
					this.txtReportTime.Text,
					"',Comment='",
					this.TextBox5.Text,
					"',SummaryName='",
					this.txtSummaryName.Text,
					"',mark=",
					num,
					",filesType=",
					Convert.ToInt32(this.DDTClass.SelectedValue.Trim()),
					" where id=",
					base.Request.Params["id"].ToString()
				});
			}
			else
			{
				sqlString = string.Concat(new object[]
				{
					"insert into Prj_Summary(bm,ReporterDate,SummaryName,Comment,PrjId,Reporter,mark,filesType,wfGuid) values('",
					this.txtReportUnit.Text,
					"','",
					this.txtReportTime.Text,
					"','",
					this.txtSummaryName.Text,
					"','",
					this.TextBox5.Text,
					"','",
					base.Request.Params["PrjCode"].ToString(),
					"','",
					userManageDb.GetCurrentUserInfo().UserCode,
					"',",
					num,
					",",
					Convert.ToInt32(this.DDTClass.SelectedValue.Trim()),
					",'",
					this.hdnWfGuid.Value.Trim(),
					"')"
				});
			}
			if (publicDbOpClass.NonQuerySqlString(sqlString))
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_scienceinnovatesum' });");
				return;
			}
			base.RegisterScript("top.ui.show('保存失败！');");
		}
	}


