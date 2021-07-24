using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.pm.util;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ReportQuerySet : PageBase, IRequiresSessionState
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
				this.ReportID = Convert.ToInt32(base.Request["reportid"]);
				this.Options = QueryItemAction.QueryValidItems(this.ReportID);
				QueryItemCollection queryItemCollection = new QueryItemCollection();
				for (int i = 0; i < this.Options.Rows.Count; i++)
				{
					queryItemCollection.Add(new QueryItem
					{
						ItemName = this.Options.Rows[i]["TermName"].ToString(),
						ItemSign = "",
						ItemValue = "",
						StrSql = this.Options.Rows[i]["StrSql"].ToString(),
						ToolTip = this.Options.Rows[i]["ToolTip"].ToString(),
						DataType = (int)this.Options.Rows[i]["DataType"],
						IsEmpty = (bool)this.Options.Rows[i]["IsEmpty"]
					});
				}
				this.ItemList = queryItemCollection;
				this.dgdQuery_Bind();
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
		private void dgdQuery_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				QueryItem queryItem = (QueryItem)e.Item.DataItem;
				e.Item.Cells[1].ToolTip = queryItem.ToolTip;
				switch (queryItem.DataType)
				{
				case 4:
					((TextBox)e.Item.Cells[1].Controls[1]).Attributes["ondblclick"] = "DataType(this)";
					break;
				case 5:
					((TextBox)e.Item.Cells[1].Controls[1]).Attributes["ondblclick"] = "DataType1(this)";
					break;
				case 6:
					((TextBox)e.Item.Cells[1].Controls[1]).Attributes["ondblclick"] = "DataType2(this)";
					break;
				case 7:
					((TextBox)e.Item.Cells[1].Controls[1]).Attributes["ondblclick"] = "DataType3(this)";
					break;
				}
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Cells[1].Attributes["onblur"] = string.Concat(new object[]
				{
					"doCheck(this,",
					queryItem.DataType,
					",",
					queryItem.IsEmpty.ToString().ToLower(),
					");"
				});
				return;
			}
			default:
				return;
			}
		}
		private void GatherData()
		{
			for (int i = 0; i < this.dgdQuery.Items.Count; i++)
			{
				DataGridItem dataGridItem = this.dgdQuery.Items[i];
				this.ItemList[i].ItemValue = ((TextBox)dataGridItem.Cells[1].Controls[1]).Text;
			}
		}
		protected void btnOk_Click(object sender, EventArgs e)
		{
			this.GatherData();
			string text = "";
			bool flag = true;
			for (int i = 0; i < this.ItemList.Count; i++)
			{
				QueryItem queryItem = this.ItemList[i];
				if (!queryItem.IsEmpty && queryItem.ItemValue.Trim() == "")
				{
					text = text + queryItem.ItemName + "不能为空！\\n";
					flag = false;
				}
				else
				{
					if (queryItem.ItemValue.Trim() != "")
					{
						switch (queryItem.DataType)
						{
						case 1:
							queryItem.ItemValue = queryItem.ItemValue.Replace("'", "''");
							break;
						case 2:
							if (!VerifyHelper.IsInt32(queryItem.ItemValue))
							{
								text = text + queryItem.ItemName + "类型不正确！\\n";
								flag = false;
							}
							break;
						case 3:
							if (!VerifyHelper.ValidatorMoney(queryItem.ItemValue))
							{
								text = text + queryItem.ItemName + "类型不正确！\\n";
								flag = false;
							}
							break;
						case 4:
							if (!VerifyHelper.IsDate(queryItem.ItemValue))
							{
								text = text + queryItem.ItemName + "类型不正确！\\n";
								flag = false;
							}
							break;
						}
					}
				}
			}
			if (flag)
			{
				this.Session["QUERY"] = this.ItemList;
				this.JS.Text = "window.returnValue = true;window.close();";
				return;
			}
			this.JS.Text = "alert('" + text + "');";
		}
		protected void btnCancel_Click(object sender, EventArgs e)
		{
			this.JS.Text = "window.returnValue = false;window.close();";
		}
	}


