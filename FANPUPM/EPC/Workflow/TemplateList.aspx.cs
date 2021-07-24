using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TemplateList : NBasePage, IRequiresSessionState
	{
		public string businessCode
		{
			get
			{
				object obj = this.ViewState["BusinessCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["BusinessCode"] = value;
			}
		}
		public string BusinessClass
		{
			get
			{
				object obj = this.ViewState["BusinessClass"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "0";
			}
			set
			{
				this.ViewState["BusinessClass"] = value;
			}
		}
		public new string UserCode
		{
			get
			{
				object obj = this.ViewState["UserCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["UserCode"] = value;
			}
		}
		public string IsGeneral
		{
			get
			{
				object obj = this.ViewState["IsGeneral"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["IsGeneral"] = value;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.businessCode = System.Convert.ToString(base.Request["ti"]);
				this.BusinessClass = System.Convert.ToString(base.Request["class"]);
				this.IsGeneral = base.Request["flag"];
				this.hdnIsGeneral.Value = this.IsGeneral;
				this.UserCode = this.Session["yhdm"].ToString();
				if (this.BusinessClass == "" || this.BusinessClass == null)
				{
					this.btnAdd.Disabled = true;
				}
				else
				{
					this.btnAdd.Disabled = false;
				}
				this.btnAdd.Attributes["onclick"] = string.Concat(new string[]
				{
					"EditorAdd(1,'",
					this.businessCode,
					"','",
					this.BusinessClass,
					"')"
				});
				this.btnEdit.Attributes["onclick"] = string.Concat(new string[]
				{
					"EditorAdd(2,'",
					this.businessCode,
					"','",
					this.BusinessClass,
					"');"
				});
				this.btnLimit.Attributes["onclick"] = string.Concat(new string[]
				{
					"LimitAdd( '",
					this.businessCode,
					"','",
					this.BusinessClass,
					"' )"
				});
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除选中项吗？')){return false;}";
				this.dgFlow_Bind();
			}
			this.dgFlow_Bind();
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			this.dgFlow.ItemDataBound += new DataGridItemEventHandler(this.dgFlow_ItemDataBound);
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void dgFlow_Bind()
		{
			string[] childCorp = new string[]
			{
				this.Session["CorpCode"].ToString()
			};
			this.dgFlow.DataSource = FlowTemplateAction.QueryTemplateList(this.businessCode, this.BusinessClass, childCorp, this.IsGeneral);
			this.dgFlow.DataBind();
		}
		private void dgFlow_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				e.Item.Attributes["id"] = System.Convert.ToString(e.Item.ItemIndex + 1);
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgFlow.ClientID,
					"');doClickRow('",
					this.businessCode.ToString(),
					"','",
					this.BusinessClass.ToString(),
					"','",
					dataRowView["TemplateID"].ToString(),
					"','",
					this.IsGeneral.ToString(),
					"');"
				});
				if (dataRowView["Remark"].ToString().Length > 25)
				{
					e.Item.Cells[4].ToolTip = dataRowView["Remark"].ToString();
					e.Item.Cells[4].Text = dataRowView["Remark"].ToString().Substring(0, 25) + "...";
				}
				return;
			}
			default:
				return;
			}
		}
		private void btnDel_Click(object sender, System.EventArgs e)
		{
			int templateId = System.Convert.ToInt32(this.hdnTemplateID.Value);
			if (FlowTemplateAction.DelTemplate(templateId))
			{
				this.JS.Text = "alert('删除成功！');window.parent.frames[1].location.href = \"about:blank\"";
				this.dgFlow_Bind();
			}
		}
		protected void dgFlow_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgFlow.CurrentPageIndex = e.NewPageIndex;
			this.dgFlow_Bind();
		}
	}


