using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class RepairLook : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				DateTime dateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
				this.dtxtStartDate.Text = dateTime.ToShortDateString();
				this.dtxtEndDate.Text = dateTime.AddMonths(1).AddDays(-1.0).ToShortDateString();
				this.ViewState["BEGINDATE"] = this.dtxtStartDate.Text;
				this.ViewState["ENDDATE"] = this.dtxtEndDate.Text;
				this.GetdgdRepairRecord_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgdRepairRecord.ItemDataBound += new DataGridItemEventHandler(this.dgdRepairRecord_ItemDataBound);
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.ViewState["BEGINDATE"] = this.dtxtStartDate.Text;
			this.ViewState["ENDDATE"] = this.dtxtEndDate.Text;
			this.GetdgdRepairRecord_Bind();
		}
		public void GetdgdRepairRecord_Bind()
		{
			string dtBegin = this.ViewState["BEGINDATE"].ToString();
			string dtEnd = this.ViewState["ENDDATE"].ToString();
			this.dgdRepairRecord.DataSource = EquimentmaintainSearchInfoAction.GetEquipmentMaintainList(dtBegin, dtEnd);
			this.dgdRepairRecord.DataBind();
		}
		private void dgdRepairRecord_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.dgdRepairRecord.ClientID + "');";
				e.Item.Attributes["ondblclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgdRepairRecord.ClientID,
					"');clickRow('",
					dataRowView["MaintainUniqueCode"].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				return;
			}
			default:
				return;
			}
		}
		protected void PaginationControl_PageIndexChange(object sender, EventArgs e)
		{
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgdRepairRecord.CurrentPageIndex = e.NewPageIndex;
			this.GetdgdRepairRecord_Bind();
		}
	}


