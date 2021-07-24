using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ConditionSet : BasePage, IRequiresSessionState
	{

		public int nodeID
		{
			get
			{
				object obj = this.ViewState["nodeID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["nodeID"] = value;
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.nodeID = System.Convert.ToInt32(base.Request["nodeid"]);
				this.gvCondition_Bound();
				this.CreateConditionField();
				this.setOrderNumber();
			}
		}
		private void CreateConditionField()
		{
			DataTable dataTable = FlowTemplateAction.QueryConditionField(this.nodeID);
			this.ddltCondition.Items.Clear();
			foreach (DataRow dataRow in dataTable.Rows)
			{
				string key;
				switch (key = dataRow["type"].ToString())
				{
				case "bigint":
				case "float":
				case "int":
				case "money":
				case "numeric":
				case "smallint":
				case "smallmoney":
				case "tinyint":
				{
					ListItem item = new ListItem(dataRow["Description"].ToString(), dataRow["columnName"].ToString());
					this.ddltCondition.Items.Add(item);
					break;
				}
				case "datetime":
				{
					ListItem item = new ListItem(dataRow["Description"].ToString(), dataRow["columnName"].ToString());
					this.ddltCondition.Items.Add(item);
					break;
				}
				case "decimal":
				{
					ListItem item = new ListItem(dataRow["Description"].ToString(), dataRow["columnName"].ToString());
					this.ddltCondition.Items.Add(item);
					break;
				}
				}
			}
			ListItem listItem = new ListItem("---请选择条件字段---", "0");
			listItem.Selected = true;
			this.ddltCondition.Items.Insert(0, listItem);
		}
		private void setOrderNumber()
		{
			DataRow dataRow = FlowTemplateAction.QueryOrderNumber(this.nodeID);
			if (dataRow == null)
			{
				this.txtOrder.Text = "1";
				this.RbAnd.Enabled = false;
				this.RbOr.Enabled = false;
				this.RbNo.Enabled = true;
				this.RbNo.Checked = true;
				return;
			}
			if (dataRow["OrderNumber"].ToString() != "" && dataRow["OrderNumber"] != null)
			{
				int num = System.Convert.ToInt32(dataRow["OrderNumber"].ToString());
				this.txtOrder.Text = System.Convert.ToString(num + 1);
				this.RbAnd.Enabled = true;
				this.RbOr.Enabled = true;
				this.RbNo.Enabled = false;
				this.RbAnd.Checked = true;
				return;
			}
			this.txtOrder.Text = "1";
			this.RbAnd.Enabled = false;
			this.RbOr.Enabled = false;
			this.RbNo.Enabled = true;
			this.RbNo.Checked = true;
		}
		protected void BtnAdd_Click(object sender, System.EventArgs e)
		{
			System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
			hashtable.Add("NodeID", this.nodeID.ToString());
			hashtable.Add("OrderNumber", this.txtOrder.Text.Trim());
			string pStr;
			if (this.RbAnd.Checked)
			{
				pStr = "1";
			}
			else
			{
				if (this.RbOr.Checked)
				{
					pStr = "0";
				}
				else
				{
					pStr = "";
				}
			}
			hashtable.Add("AndOr", SqlStringConstructor.GetQuotedString(pStr));
			hashtable.Add("ConditionField", SqlStringConstructor.GetQuotedString(this.ddltCondition.SelectedValue.ToString()));
			hashtable.Add("FieldType", SqlStringConstructor.GetQuotedString(this.hdnFieldType.Value));
			hashtable.Add("ConditionType", SqlStringConstructor.GetQuotedString(this.ddltConditionType.SelectedValue.ToString()));
			hashtable.Add("ConditionValue", this.txtConditionValue.Text.Trim());
			DataRow dataRow = FlowTemplateAction.QueryOrderNumberList(this.nodeID, System.Convert.ToInt32(this.txtOrder.Text.Trim()));
			if (dataRow != null)
			{
				if (dataRow["NodeID"].ToString() != "" && dataRow["NodeID"] != null)
				{
					string where = "where NodeId = " + this.nodeID.ToString() + " and OrderNumber = " + this.txtOrder.Text.Trim();
					if (FlowTemplateAction.UpdNodeCondition(hashtable, where))
					{
						this.JS.Text = "alert('更改成功！');window.returnValue=false;window.close();";
						return;
					}
				}
				else
				{
					if (FlowTemplateAction.AddNodeCondition(hashtable))
					{
						this.JS.Text = "alert('增加成功！');window.returnValue=false;window.close();";
						return;
					}
				}
			}
			else
			{
				if (FlowTemplateAction.AddNodeCondition(hashtable))
				{
					this.JS.Text = "alert('增加成功！');window.returnValue=false;window.close();";
				}
			}
		}
		protected void BtnNew_Click(object sender, System.EventArgs e)
		{
			this.setOrderNumber();
			this.ddltCondition.SelectedIndex = 0;
			this.ddltConditionType.SelectedIndex = 0;
			this.txtConditionValue.Text = "";
			this.hdnFieldType.Value = "";
		}
		private void gvCondition_Bound()
		{
			this.gvCondition.DataSource = FlowTemplateAction.QueryNodeCondition(this.nodeID);
			this.gvCondition.DataBind();
		}
		protected void ddltCondition_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string b = this.ddltCondition.SelectedValue.ToString();
			DataTable dataTable = FlowTemplateAction.QueryConditionField(this.nodeID);
			foreach (DataRow dataRow in dataTable.Rows)
			{
				if (dataRow["columnName"].ToString() == b)
				{
					this.hdnFieldType.Value = dataRow["type"].ToString();
				}
			}
		}
		protected void gvCondition_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
		protected void gvCondition_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView arg_2E_0 = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseout"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				break;
			}
			}
			if (e.CommandName.ToString() == "Select")
			{
				this.nodeID = System.Convert.ToInt32(e.Item.Cells[0].Text.ToString());
				this.txtOrder.Text = e.Item.Cells[1].Text.ToString();
				if (e.Item.Cells[2].Text.ToString() == "1")
				{
					this.RbAnd.Checked = true;
				}
				else
				{
					if (e.Item.Cells[2].Text.ToString() == "0")
					{
						this.RbOr.Checked = true;
					}
					else
					{
						this.RbNo.Checked = true;
					}
				}
				this.ddltCondition.SelectedValue = e.Item.Cells[3].Text.ToString();
				this.hdnFieldType.Value = e.Item.Cells[4].Text.ToString();
				this.ddltConditionType.SelectedValue = e.Item.Cells[5].Text.ToString();
				this.txtConditionValue.Text = e.Item.Cells[6].Text.ToString();
			}
		}
	}


