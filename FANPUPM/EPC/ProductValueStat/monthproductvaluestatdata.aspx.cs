using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class MonthProductValueStatData : PageBase, IRequiresSessionState
	{

		protected ProductValueAction ProductValueAct
		{
			get
			{
				return new ProductValueAction();
			}
		}
		public Guid ProjectCode
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
		public string TaskCode
		{
			get
			{
				object obj = this.ViewState["TASKCODE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["TASKCODE"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null || base.Request["tc"] == null)
			{
				this.RegisterStartupScript("错误提示", "<script>alert('参数错误！');</script>");
				return;
			}
			this.ProjectCode = new Guid(base.Request["pc"].ToString());
			this.TaskCode = base.Request["tc"].ToString();
			if (!this.Page.IsPostBack)
			{
				this.DataGd_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdProductValue.ItemDataBound += new DataGridItemEventHandler(this.DGrdProductValue_ItemDataBound);
		}
		private void DataGd_Bind()
		{
			this.DGrdProductValue.DataSource = this.ProductValueAct.GetProductValueMonthData(this.ProjectCode, this.TaskCode);
			this.DGrdProductValue.DataBind();
		}
		private void DGrdProductValue_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView arg_2D_0 = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.DGrdProductValue.ClientID + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				return;
			}
			default:
				return;
			}
		}
	}


