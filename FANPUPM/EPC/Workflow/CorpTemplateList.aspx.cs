using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CorpTemplateList : BasePage, IRequiresSessionState
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
					this.btnAdd.Enabled = false;
				}
				else
				{
					this.btnAdd.Enabled = true;
				}
				this.btnAdd.Attributes["onclick"] = string.Concat(new string[]
				{
					"openRoleEdit(1,'",
					this.businessCode,
					"','",
					this.BusinessClass,
					"')"
				});
				this.btnEdit.Attributes["onclick"] = string.Concat(new string[]
				{
					"openRoleEdit(2,'",
					this.businessCode,
					"','",
					this.BusinessClass,
					"');"
				});
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
				this.dgFlow_Bind();
			}
		}
		private string strCorpCodeName(string CorpCode)
		{
			return "";
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
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
			if (e.Item.ItemIndex > -1 && this.IsGeneral == "0")
			{
				string a = ((DataRowView)e.Item.DataItem)["s_bs"].ToString();
				if (a == "1")
				{
					e.Item.Cells[0].ForeColor = Color.Red;
					e.Item.Cells[1].ForeColor = Color.Red;
					e.Item.Cells[4].ForeColor = Color.Red;
					e.Item.Cells[5].ForeColor = Color.Red;
				}
				else
				{
					e.Item.Cells[0].ForeColor = Color.Black;
					e.Item.Cells[1].ForeColor = Color.Black;
					e.Item.Cells[4].ForeColor = Color.Black;
					e.Item.Cells[5].ForeColor = Color.Black;
				}
			}
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
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
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Cells[4].Text = this.strCorpCodeName(dataRowView["CorpCode"].ToString());
				return;
			}
			default:
				return;
			}
		}
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.dgFlow_Bind();
		}
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			this.dgFlow_Bind();
		}
		private void btnDel_Click(object sender, System.EventArgs e)
		{
			int templateId = System.Convert.ToInt32(this.hdnTemplateID.Value);
			if (FlowTemplateAction.IsDelTemplate(templateId))
			{
				if (FlowTemplateAction.DelTemplate(templateId))
				{
					this.JS.Text = "alert('删除成功！');window.parent.frames[1].location.href = \"about:blank\"";
					this.dgFlow_Bind();
					return;
				}
			}
			else
			{
				this.JS.Text = "alert('该流程正在使用，不能删除！');";
			}
		}
	}


