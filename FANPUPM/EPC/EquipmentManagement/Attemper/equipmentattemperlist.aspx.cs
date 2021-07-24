using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EquipmentAttemperList : PageBase, IRequiresSessionState
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
				this.BindDataGrid(this.ViewState["BEGINDATE"].ToString(), this.ViewState["ENDDATE"].ToString());
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.GrdAttemper.ItemDataBound += new DataGridItemEventHandler(this.GrdAttemper_ItemDataBound);
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.BindDataGrid(this.txtStar.Text, this.txtEnd.Text);
		}
		private void BindDataGrid(string beginDate, string endDate)
		{
			this.GrdAttemper.DataSource = EquipmentAttemperAction.GetEquipmentAttemperList(Convert.ToDateTime(beginDate), Convert.ToDateTime(endDate));
			this.GrdAttemper.DataBind();
		}
		private void GrdAttemper_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.GrdAttemper.ClientID.ToString() + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Cells[3].Text = ((e.Item.Cells[3].Text == "True") ? "调出" : "归还");
			}
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.GrdAttemper.CurrentPageIndex = e.NewPageIndex;
			this.BindDataGrid(this.ViewState["BEGINDATE"].ToString(), this.ViewState["ENDDATE"].ToString());
		}
	}


