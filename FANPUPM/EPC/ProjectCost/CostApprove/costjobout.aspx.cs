using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CostJobOut : PageBase, IRequiresSessionState
	{
		protected Guid ProjectCode
		{
			get
			{
				object obj = this.ViewState["PROJECTCODE"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["PROJECTCODE"] = value;
			}
		}
		protected DateTime StartDate
		{
			get
			{
				object obj = this.ViewState["STARTDATE"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return DateTime.Now;
			}
			set
			{
				this.ViewState["STARTDATE"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null)
			{
				this.RegisterStartupScript("错误", "<script>alert('参数错误');</script>");
				return;
			}
			this.ProjectCode = ((base.Request["pc"].ToString() == "") ? Guid.Empty : new Guid(base.Request["pc"].ToString()));
			this.StartDate = Convert.ToDateTime(base.Request["m"].ToString().Replace("年", "-").Replace("月", "-1"));
			if (!this.Page.IsPostBack)
			{
				this.Date_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgCostJobOut.ItemDataBound += new DataGridItemEventHandler(this.dgCostJobOut_ItemDataBound);
		}
		private void Date_Bind()
		{
			this.dgCostJobOut.DataSource = CostApproveAction.getMonthJobOutCost(this.ProjectCode, this.StartDate.Year.ToString(), this.StartDate.Month.ToString());
			this.dgCostJobOut.DataBind();
		}
		protected void BtnReturn_Click(object sender, EventArgs e)
		{
			this.Page.RegisterClientScriptBlock("Script", "<script language=javascript>self.location.href = 'MonthCostApprove.aspx?pc=" + this.ProjectCode.ToString() + "';</script>");
		}
		private void dgCostJobOut_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
	}


