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
	public partial class CostInputShow : BasePage, IRequiresSessionState
	{

		public Guid RecordID
		{
			get
			{
				object obj = this.ViewState["RecordID"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["RecordID"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["ic"] != null)
				{
					this.RecordID = new Guid(base.Request.QueryString["ic"]);
				}
				DataTable singleCostInputPriTable = CostInputPriAction.getSingleCostInputPriTable(this.RecordID);
				this.lblItemName.Text = singleCostInputPriTable.Rows[0]["CostItemName"].ToString();
				this.lblDate.Text = Convert.ToDateTime(singleCostInputPriTable.Rows[0]["HappenDate"].ToString()).ToShortDateString();
				this.lblDept.Text = singleCostInputPriTable.Rows[0]["HappenUnit"].ToString();
				this.lblUser.Text = singleCostInputPriTable.Rows[0]["FillPeople"].ToString();
				this.HdnPerson.Value = singleCostInputPriTable.Rows[0]["TouchMan"].ToString();
				this.lblPerson.Text = PageHelper.QueryUser(this, singleCostInputPriTable.Rows[0]["TouchMan"].ToString());
				DataTable costInputSlaveTable = CostInputSlaveAction.getCostInputSlaveTable(this.RecordID);
				this.ViewState["CostSlave"] = costInputSlaveTable;
				this.dgCostInputSlave.DataSource = (DataTable)this.ViewState["CostSlave"];
				this.dgCostInputSlave.DataBind();
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
		private void Bind_dgCostInputSlave()
		{
			this.dgCostInputSlave.DataSource = (DataTable)this.ViewState["CostSlave"];
			this.dgCostInputSlave.DataBind();
		}
		private void DataGridToSession()
		{
			DataTable dataTable = (DataTable)this.ViewState["CostSlave"];
			dataTable.Clear();
			foreach (DataGridItem dataGridItem in this.dgCostInputSlave.Items)
			{
				if (dataGridItem.ItemType == ListItemType.EditItem)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["ChildID"] = dataGridItem.Cells[0].Text;
					dataRow["ItemName"] = ((TextBox)dataGridItem.FindControl("txtFeeName")).Text;
					dataRow["Price"] = ((TextBox)dataGridItem.FindControl("txtPrice")).Text;
					dataRow["Remark"] = ((TextBox)dataGridItem.FindControl("txtContent")).Text;
					dataRow["RecordID"] = dataGridItem.Cells[6].Text;
					dataRow["CostCode"] = ((TextBox)dataGridItem.FindControl("txtCostItemCode")).Text;
					dataTable.Rows.Add(dataRow);
				}
				else
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["ChildID"] = dataGridItem.Cells[0].Text;
					dataRow2["ItemName"] = ((Label)dataGridItem.Cells[1].FindControl("lblFeeName")).Text;
					dataRow2["Price"] = ((Label)dataGridItem.Cells[1].FindControl("lblPrice")).Text;
					dataRow2["CBSName"] = ((Label)dataGridItem.Cells[1].FindControl("lblCostItem")).Text;
					dataRow2["Remark"] = ((Label)dataGridItem.Cells[1].FindControl("lblContent")).Text;
					dataRow2["RecordID"] = dataGridItem.Cells[6].Text;
					dataRow2["CostCode"] = ((Label)dataGridItem.Cells[1].FindControl("lblCostItemCode")).Text;
					dataTable.Rows.Add(dataRow2);
				}
			}
			this.ViewState["CostSlave"] = dataTable;
		}
		private void dgCostInputSlave_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				this.dgCostInputSlave.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
			}
		}
	}


