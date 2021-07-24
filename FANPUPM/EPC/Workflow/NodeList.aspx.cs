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
	public partial class NodeList : BasePage, IRequiresSessionState
	{
		public int TemplateID
		{
			get
			{
				object obj = this.ViewState["TemplateID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TemplateID"] = value;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.TemplateID = System.Convert.ToInt32(base.Request["tid"]);
				this.hdnTemplateID.Value = this.TemplateID.ToString();
				this.dgFlowChild_Bind();
				this.btnAdd.Attributes["onclick"] = "openRoleEdit(1)";
				this.btnEdit.Attributes["onclick"] = "openRoleEdit(2);";
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btn_Del.Click += new System.EventHandler(this.btn_Del_Click);
			this.dgFlowChild.ItemDataBound += new DataGridItemEventHandler(this.dgFlowChild_ItemDataBound);
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void dgFlowChild_Bind()
		{
			this.dgFlowChild.DataSource = FlowTemplateAction.QueryNodeList1(this.TemplateID);
			this.dgFlowChild.DataBind();
		}
		private void dgFlowChild_Save()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("NodeID"));
			dataTable.Columns.Add(new DataColumn("TheOrder"));
			for (int i = 0; i < this.dgFlowChild.Items.Count; i++)
			{
				DataGridItem dataGridItem = this.dgFlowChild.Items[i];
				int num = (int)this.dgFlowChild.DataKeys[i];
				int num2 = System.Convert.ToInt32(((TextBox)dataGridItem.Cells[3].Controls[1]).Text);
				DataRow dataRow = dataTable.NewRow();
				dataRow["NodeID"] = num.ToString();
				dataRow["TheOrder"] = num2.ToString();
				dataTable.Rows.Add(dataRow);
			}
			DataRow[] array = dataTable.Select("", "TheOrder asc");
			for (int j = 0; j < array.Length; j++)
			{
				FlowTemplateAction.UpdOrder(int.Parse(array[j]["NodeID"].ToString()), j + 1);
			}
		}
		private void dgFlowChild_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				if (dataRowView["RoleName"].ToString().Trim() == "")
				{
					((Label)e.Item.Cells[2].Controls[1]).Text = "[人员]" + PageHelper.QueryUser(this, dataRowView["Operater"].ToString());
				}
				else
				{
					((Label)e.Item.Cells[2].Controls[1]).Text = "[角色]" + dataRowView["RoleName"].ToString();
				}
				((TextBox)e.Item.FindControl("TxtFLowChildXH")).Attributes["onblur"] = "checkOrder(this);";
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgFlowChild.ClientID,
					"');doClickRow('",
					dataRowView["NodeID"].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				return;
			}
			default:
				return;
			}
		}
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			this.dgFlowChild_Save();
			this.dgFlowChild_Bind();
		}
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.dgFlowChild_Save();
			this.dgFlowChild_Bind();
		}
		private void btn_Del_Click(object sender, System.EventArgs e)
		{
			int nodeId = System.Convert.ToInt32(this.hdnNodeID.Value);
			int templateId = System.Convert.ToInt32(this.hdnTemplateID.Value);
			if (FlowTemplateAction.DelNode(nodeId, templateId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.dgFlowChild_Bind();
			}
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			this.dgFlowChild_Save();
			this.dgFlowChild_Bind();
		}
	}


