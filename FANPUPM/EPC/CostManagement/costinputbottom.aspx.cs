using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CostInputBottom : PageBase, IRequiresSessionState
	{
		private string RecordID = Guid.Empty.ToString();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["RecordID"] != null)
			{
				this.RecordID = base.Request["RecordID"].ToString();
			}
			if (!this.Page.IsPostBack)
			{
				this.CostInputDatabind(this.RecordID);
			}
		}
		private void CostInputDatabind(string RecordID)
		{
			string strWhere = string.Format("RecordID = '{0}'", RecordID);
			this.dgCostInputSlave.DataSource = CostInputSlaveAction.GetPageData(strWhere);
			this.dgCostInputSlave.DataBind();
		}
		private void dgCostInputSlave_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.dgCostInputSlave.ClientID + "');";
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this)";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgCostInputSlave.ItemDataBound += new DataGridItemEventHandler(this.dgCostInputSlave_ItemDataBound);
		}
	}


