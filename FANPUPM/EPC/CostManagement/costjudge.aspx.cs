using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CostJudge : PageBase, IRequiresSessionState
	{

		public Guid ProjectCode
		{
			get
			{
				if (this.ViewState["PROJECTCODE"] != null)
				{
					return new Guid(this.ViewState["PROJECTCODE"].ToString());
				}
				return Guid.NewGuid();
			}
			set
			{
				this.ViewState["PROJECTCODE"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.ViewState["RecordID"] = base.Request.QueryString["RecordID"].ToString();
				this.LoadDateToPage();
			}
		}
		private void LoadDateToPage()
		{
			DataTable singleCostInputPriTable = CostInputPriAction.getSingleCostInputPriTable(new Guid(this.ViewState["RecordID"].ToString()));
			this.ProjectCode = new Guid(singleCostInputPriTable.Rows[0]["PrjCode"].ToString().Trim());
			this.txt_AuditPeople.Text = ((singleCostInputPriTable.Rows[0]["AuditPeople"].ToString() != "") ? singleCostInputPriTable.Rows[0]["AuditPeople"].ToString() : userManageDb.GetCurrentUserInfo().UserName);
			if (singleCostInputPriTable.Rows[0]["AuditDate"].ToString() == null || singleCostInputPriTable.Rows[0]["AuditDate"].ToString() == "")
			{
				this.txt_AuditDate.Text = DateTime.Now.Date.ToString("d");
			}
			else
			{
				this.txt_AuditDate.Text = DateTime.Parse(singleCostInputPriTable.Rows[0]["AuditDate"].ToString()).ToString("d");
			}
			this.txt_Remark.Text = singleCostInputPriTable.Rows[0]["Remark"].ToString();
			if (singleCostInputPriTable.Rows[0]["AuditResult"].ToString() == "False")
			{
				this.ddl_AuditResult.SelectedValue = "0";
				return;
			}
			this.ddl_AuditResult.SelectedValue = "1";
		}
		private CostInputPri CreatModelObject()
		{
			return new CostInputPri
			{
				RecordID = new Guid(this.ViewState["RecordID"].ToString()),
				AuditPeople = userManageDb.GetCurrentUserInfo().UserName,
				Remark = this.txt_Remark.Text,
				AuditDate = DateTime.Parse(this.txt_AuditDate.Text),
				AuditResult = int.Parse(this.ddl_AuditResult.SelectedValue.ToString())
			};
		}
		protected void btn_save_Click(object sender, EventArgs e)
		{
			try
			{
				CostInputPri objInfo = this.CreatModelObject();
				int num = CostInputPriAction.updateCostJudgePri(objInfo);
				if (num == 1)
				{
					this.Js.Text = "alert('审核成功!');window.close();";
				}
				else
				{
					this.Js.Text = "alert('审核失败!');";
				}
			}
			catch (Exception ex)
			{
				this.Js.Text = "alert('数据库连接失败!')";
				throw ex;
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
	}


