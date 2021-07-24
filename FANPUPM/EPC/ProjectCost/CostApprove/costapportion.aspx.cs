using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CostApportion : PageBase, IRequiresSessionState
	{

		protected CBSAction CBSAct
		{
			get
			{
				return new CBSAction();
			}
		}
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
			if (base.Request["pc"] == null || base.Request["m"] == null)
			{
				this.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
				return;
			}
			this.ProjectCode = new Guid(base.Request["pc"].ToString());
			this.StartDate = Convert.ToDateTime(base.Request["m"].ToString().Replace("年", "-").Replace("月", "-1"));
			if (!this.Page.IsPostBack)
			{
				this.CBSAct.InitialzeCBS(this.ProjectCode.ToString(), this.StartDate);
				this.DataGrid_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdCostApprotion.ItemDataBound += new DataGridItemEventHandler(this.DGrdCostApprotion_ItemDataBound);
		}
		private void DataGrid_Bind()
		{
			this.DGrdCostApprotion.DataSource = this.CBSAct.GetAllCBSApprotionFee(this.ProjectCode.ToString(), this.StartDate);
			this.DGrdCostApprotion.DataBind();
		}
		private void DGrdCostApprotion_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.DGrdCostApprotion.ClientID + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				return;
			default:
				return;
			}
		}
		protected void BtnReturn_Click(object sender, EventArgs e)
		{
			this.Page.RegisterClientScriptBlock("Script", "<script language=javascript>self.location.href = 'MonthCostApprove.aspx?pc=" + this.ProjectCode.ToString() + "';</script>");
		}
	}


