using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EquipmentCheckList : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				DateTime dateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
				this.txtStar.Text = dateTime.ToShortDateString();
				this.txtEnd.Text = dateTime.AddMonths(1).AddDays(-1.0).ToShortDateString();
				this.ViewState["BEGINDATE"] = this.txtStar.Text;
				this.ViewState["ENDDATE"] = this.txtEnd.Text;
				this.ViewState["CHECKTYPE"] = this.DropDownList1.SelectedValue;
				this.BindDataGrid(this.ViewState["BEGINDATE"].ToString(), this.ViewState["ENDDATE"].ToString(), Convert.ToInt32(this.ViewState["CHECKTYPE"]));
			}
		}
		private void BindDataGrid(string beginDate, string endDate, int checkType)
		{
			this.GrdEquipmentCheckup.DataSource = EquipmentCheckAction.GetEquipmentCheckList(Convert.ToDateTime(beginDate), Convert.ToDateTime(endDate), checkType);
			this.GrdEquipmentCheckup.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.GrdEquipmentCheckup.ItemDataBound += new DataGridItemEventHandler(this.GrdEquipmentCheckup_ItemDataBound);
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.BindDataGrid(this.txtStar.Text, this.txtEnd.Text, Convert.ToInt32(this.DropDownList1.SelectedValue));
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.GrdEquipmentCheckup.CurrentPageIndex = e.NewPageIndex;
			this.BindDataGrid(this.txtStar.Text, this.txtEnd.Text, Convert.ToInt32(this.DropDownList1.SelectedValue));
		}
		private void GrdEquipmentCheckup_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.GrdEquipmentCheckup.ClientID.ToString() + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
			}
		}
	}


