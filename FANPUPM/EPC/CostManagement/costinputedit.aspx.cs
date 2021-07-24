using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CostInputEdit : PageBase, IRequiresSessionState
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
		public string PrjCode
		{
			get
			{
				object obj = this.ViewState["PrjCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["PrjCode"] = value;
			}
		}
		public string opType
		{
			get
			{
				object obj = this.ViewState["opType"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["opType"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["RecordID"] == null || base.Request.QueryString["RecordID"].ToString() == "")
				{
					this.RecordID = Guid.NewGuid();
				}
				else
				{
					this.RecordID = new Guid(base.Request.QueryString["RecordID"].ToString());
				}
				this.PrjCode = base.Request.QueryString["PrjCode"].ToString();
				this.opType = base.Request.QueryString["opType"].ToString();
				if (this.opType == "add")
				{
					userManageDb userManageDb = new userManageDb();
					this.txtUser.Text = userManageDb.GetUserName(base.UserCode);
					this.dbDate.Text = DateTime.Now.ToShortDateString();
					this.HdnPerson.Value = base.UserCode;
					this.TxtPerson.Value = userManageDb.GetUserName(base.UserCode);
				}
				if (this.opType == "edit")
				{
					DataTable singleCostInputPriTable = CostInputPriAction.getSingleCostInputPriTable(this.RecordID);
					this.txtItemName.Text = singleCostInputPriTable.Rows[0]["CostItemName"].ToString();
					this.dbDate.Text = Convert.ToDateTime(singleCostInputPriTable.Rows[0]["HappenDate"].ToString()).ToShortDateString();
					this.txtDept.Text = singleCostInputPriTable.Rows[0]["HappenUnit"].ToString();
					this.txtUser.Text = singleCostInputPriTable.Rows[0]["FillPeople"].ToString();
					this.HdnPerson.Value = singleCostInputPriTable.Rows[0]["TouchMan"].ToString();
					this.TxtPerson.Value = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, singleCostInputPriTable.Rows[0]["TouchMan"].ToString());
				}
				DataTable costInputSlaveTable = CostInputSlaveAction.getCostInputSlaveTable(this.RecordID);
				this.ViewState["CostSlave"] = costInputSlaveTable;
				this.dgCostInputSlave.DataSource = (DataTable)this.ViewState["CostSlave"];
				this.dgCostInputSlave.DataBind();
				this.btnDel.Attributes["onclick"] = "javascript:doWith();";
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
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.DataGridToSession();
			DataTable dataTable = (DataTable)this.ViewState["CostSlave"];
			DataRow dataRow = dataTable.NewRow();
			dataRow["ChildID"] = 0;
			dataRow["ItemName"] = "";
			dataRow["Price"] = "0.00";
			dataRow["CBSName"] = "";
			dataRow["CostCode"] = "";
			dataRow["Remark"] = "";
			dataRow["RecordID"] = this.RecordID.ToString();
			dataTable.Rows.Add(dataRow);
			this.ViewState["CostSlave"] = dataTable;
			this.dgCostInputSlave.EditItemIndex = dataTable.Rows.Count - 1;
			this.Bind_dgCostInputSlave();
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
				e.Item.Attributes["onclick"] = string.Concat(new object[]
				{
					"doClick(this,'",
					this.dgCostInputSlave.ClientID.ToString(),
					"');clickRow('",
					e.Item.ItemIndex,
					"',this);"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
			}
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			this.dgCostInputSlave.EditItemIndex = -1;
			this.DataGridToSession();
			DataTable dataTable = (DataTable)this.ViewState["CostSlave"];
			dataTable.Rows.RemoveAt(int.Parse(this.hdnItemIndex.Value));
			this.Bind_dgCostInputSlave();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.dgCostInputSlave.EditItemIndex = int.Parse(this.hdnItemIndex.Value);
			this.DataGridToSession();
			this.Bind_dgCostInputSlave();
		}
		protected void btn_save_Click(object sender, EventArgs e)
		{
			CostInputPri costInputPri = new CostInputPri();
			costInputPri.RecordID = this.RecordID;
			costInputPri.PrjCode = this.PrjCode.ToString();
			costInputPri.CostItemName = this.txtItemName.Text.Trim();
			costInputPri.HappenDate = Convert.ToDateTime(this.dbDate.Text.Trim());
			costInputPri.HappenUnit = this.txtDept.Text.Trim();
			costInputPri.FillPeople = this.txtUser.Text.Trim();
			costInputPri.TouchMan = this.HdnPerson.Value.Trim();
			this.DataGridToSession();
			if (CostInputPriAction.insertCostInput(costInputPri, (DataTable)this.ViewState["CostSlave"], this.opType))
			{
				this.JS.Text = "alert('保存成本数据成功！');window.returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存成本数据失败！请与管理员联系！');window.returnValue=true;window.close();";
		}
	}


