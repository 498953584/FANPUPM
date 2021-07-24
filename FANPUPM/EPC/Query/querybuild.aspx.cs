using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class QueryBuild : PageBase, IRequiresSessionState
	{

		public int ReportID
		{
			get
			{
				object obj = this.ViewState["ReportID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["ReportID"] = value;
			}
		}
		public QueryItemCollection ItemList
		{
			get
			{
				object obj = this.ViewState["ItemList"];
				if (obj != null)
				{
					return (QueryItemCollection)obj;
				}
				return new QueryItemCollection();
			}
			set
			{
				this.ViewState["ItemList"] = value;
			}
		}
		public DataTable Options
		{
			get
			{
				object obj = this.ViewState["Terms"];
				if (obj != null)
				{
					return (DataTable)obj;
				}
				return new DataTable();
			}
			set
			{
				this.ViewState["Terms"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.ReportID = 1;
				this.Options = QueryItemAction.QueryValidItems(this.ReportID);
				QueryItem queryItem = new QueryItem();
				queryItem.ItemName = "";
				queryItem.ItemValue = "";
				queryItem.Join = JoinType.And;
				queryItem.Operator = "";
				this.ItemList = new QueryItemCollection
				{
					queryItem
				};
				this.dgdQuery_Bind();
				this.dgdQuery_Disable();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgdQuery.ItemDataBound += new DataGridItemEventHandler(this.dgdQuery_ItemDataBound);
		}
		private void dgdQuery_Bind()
		{
			this.dgdQuery.DataSource = this.ItemList;
			this.dgdQuery.DataBind();
		}
		private void dgdQuery_Enable()
		{
			for (int i = 0; i < this.dgdQuery.Items.Count; i++)
			{
				DataGridItem dataGridItem = this.dgdQuery.Items[i];
				((DropDownList)dataGridItem.Cells[0].Controls[1]).Enabled = true;
				((DropDownList)dataGridItem.Cells[1].Controls[1]).Enabled = true;
				((DropDownList)dataGridItem.Cells[2].Controls[1]).Enabled = true;
				((TextBox)dataGridItem.Cells[3].Controls[1]).Enabled = true;
			}
		}
		private void dgdQuery_Disable()
		{
			for (int i = 0; i < this.dgdQuery.Items.Count; i++)
			{
				DataGridItem dataGridItem = this.dgdQuery.Items[i];
				((DropDownList)dataGridItem.Cells[0].Controls[1]).Enabled = false;
				((DropDownList)dataGridItem.Cells[1].Controls[1]).Enabled = false;
				((DropDownList)dataGridItem.Cells[2].Controls[1]).Enabled = false;
				((TextBox)dataGridItem.Cells[3].Controls[1]).Enabled = false;
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.GatherData();
			QueryItem queryItem = new QueryItem();
			queryItem.ItemName = "";
			queryItem.ItemValue = "";
			queryItem.Join = JoinType.And;
			queryItem.Operator = "";
			this.ItemList.Add(queryItem);
			this.dgdQuery_Bind();
		}
		private void GatherData()
		{
			for (int i = 0; i < this.dgdQuery.Items.Count; i++)
			{
				DataGridItem dataGridItem = this.dgdQuery.Items[i];
				QueryItem queryItem = new QueryItem();
				queryItem.ItemName = ((DropDownList)dataGridItem.Cells[1].Controls[1]).SelectedValue;
				queryItem.Join = (JoinType)Enum.Parse(typeof(JoinType), ((DropDownList)dataGridItem.Cells[0].Controls[1]).SelectedValue);
				queryItem.Operator = ((DropDownList)dataGridItem.Cells[2].Controls[1]).SelectedValue;
				queryItem.ItemValue = ((TextBox)dataGridItem.Cells[3].Controls[1]).Text;
				this.ItemList[i] = queryItem;
			}
		}
		private void dgdQuery_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				QueryItem queryItem = (QueryItem)e.Item.DataItem;
				DropDownList dropDownList = (DropDownList)e.Item.Cells[0].Controls[1];
				for (int i = 0; i < dropDownList.Items.Count; i++)
				{
					if (dropDownList.Items[i].Value == Convert.ToInt32(queryItem.Join).ToString())
					{
						dropDownList.Items[i].Selected = true;
						break;
					}
				}
				DropDownList dropDownList2 = (DropDownList)e.Item.Cells[1].Controls[1];
				dropDownList2.DataSource = this.Options;
				dropDownList2.DataTextField = "TermName";
				dropDownList2.DataValueField = "TermSign";
				dropDownList2.DataBind();
				for (int j = 0; j < dropDownList2.Items.Count; j++)
				{
					if (dropDownList2.Items[j].Value == queryItem.ItemName)
					{
						dropDownList2.Items[j].Selected = true;
						break;
					}
				}
				DropDownList dropDownList3 = (DropDownList)e.Item.Cells[2].Controls[1];
				for (int k = 0; k < dropDownList3.Items.Count; k++)
				{
					if (dropDownList3.Items[k].Value == queryItem.Operator)
					{
						dropDownList3.Items[k].Selected = true;
						break;
					}
				}
				e.Item.Attributes["onclick"] = string.Concat(new object[]
				{
					"doClick(this,'",
					this.dgdQuery.ClientID,
					"');doClickRow(",
					e.Item.ItemIndex,
					");"
				});
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				return;
			}
			default:
				return;
			}
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			this.GatherData();
			int index = Convert.ToInt32(this.hdnIndex.Value);
			this.ItemList.RemoveAt(index);
			this.dgdQuery_Bind();
		}
		protected void rb2_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rb2.Checked)
			{
				this.dgdQuery_Enable();
				this.btnAdd.Enabled = true;
				this.btnAdd.CssClass = "button_add";
			}
		}
		protected void rb1_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rb1.Checked)
			{
				this.dgdQuery_Disable();
				this.btnAdd.Enabled = false;
				this.btnAdd.CssClass = "button_addu";
			}
		}
		protected void btnOk_Click(object sender, EventArgs e)
		{
			if (this.rb2.Checked)
			{
				this.GatherData();
				for (int i = 0; i < this.ItemList.Count; i++)
				{
				}
				this.Session["QUERY"] = this.ItemList;
				this.JS.Text = "window.returnValue = true;window.close();";
			}
		}
		protected void btnCancel_Click(object sender, EventArgs e)
		{
			this.JS.Text = "window.returnValue = false;window.close();";
		}
	}


