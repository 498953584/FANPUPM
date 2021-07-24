using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class MonthCostApprove : PageBase, IRequiresSessionState
	{

		protected CostApproveAction CostApproveAct
		{
			get
			{
				return new CostApproveAction();
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
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null)
			{
				this.RegisterStartupScript("错误", "<script>alert('参数错误');</script>");
				return;
			}
			this.ProjectCode = ((base.Request["pc"].ToString() == "") ? Guid.Empty : new Guid(base.Request["pc"].ToString()));
			if (!this.Page.IsPostBack)
			{
				this.DGrdMonthPlan_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdMonthPlan.ItemCommand += new DataGridCommandEventHandler(this.DGrdMonthPlan_ItemCommand);
			this.DGrdMonthPlan.ItemDataBound += new DataGridItemEventHandler(this.DGrdMonthPlan_ItemDataBound);
		}
		private void DGrdMonthPlan_Bind()
		{
			this.DGrdMonthPlan.DataSource = this.CostApproveAct.GetMonthCostApprove(this.ProjectCode);
			this.DGrdMonthPlan.DataBind();
		}
		private void DGrdMonthPlan_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Cells[1].Text = Convert.ToDateTime(dataRowView["StartDate"].ToString()).Year.ToString() + "年" + Convert.ToDateTime(dataRowView["StartDate"].ToString()).Month.ToString() + "月";
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.DGrdMonthPlan.ClientID.ToString() + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				LinkButton linkButton = (LinkButton)e.Item.Cells[2].FindControl("LBtnApprove");
				linkButton.Attributes["onclick"] = string.Concat(new object[]
				{
					"javascript:return OpenCostApprove('Approve','",
					base.Server.UrlEncode(e.Item.Cells[1].Text),
					"','",
					this.ProjectCode,
					"')"
				});
				LinkButton linkButton2 = (LinkButton)e.Item.Cells[2].FindControl("LBtnApproveDel");
				linkButton2.Attributes.Add("onclick", "javascript:return confirm('确定清空该月核算成本吗？');");
				LinkButton linkButton3 = (LinkButton)e.Item.Cells[3].FindControl("LBtnApprotion");
				linkButton3.Attributes["onclick"] = string.Concat(new object[]
				{
					"javascript:return OpenCostApprove('Approtion','",
					base.Server.UrlEncode(e.Item.Cells[1].Text),
					"','",
					this.ProjectCode,
					"')"
				});
				LinkButton linkButton4 = (LinkButton)e.Item.Cells[4].FindControl("LBtnJobOut");
				linkButton4.Attributes["onclick"] = string.Concat(new object[]
				{
					"javascript:return OpenCostApprove('JobOut','",
					base.Server.UrlEncode(e.Item.Cells[1].Text),
					"','",
					this.ProjectCode,
					"')"
				});
				return;
			}
			default:
				return;
			}
		}
		private void DGrdMonthPlan_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			DateTime startDate = Convert.ToDateTime(e.Item.Cells[1].Text.Trim().Replace("年", "-").Replace("月", "-1"));
			startDate.AddMonths(1).AddDays(-1.0);
			if (e.CommandName == "LBtnApproveDel")
			{
				if (this.CostApproveAct.DeleteCostApprove(this.ProjectCode, startDate) == 1)
				{
					this.DGrdMonthPlan_Bind();
					this.RegisterStartupScript("", "<script>alert('成本核算删除成功！');</script>");
					return;
				}
				this.RegisterStartupScript("", "<script>alert('成本核算删除失败！');</script>");
			}
		}
	}


