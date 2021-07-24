using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class FlowList : BasePage, IRequiresSessionState
	{

		public System.Guid InstanceCode
		{
			get
			{
				object obj = this.ViewState["InstanceCode"];
				if (obj != null)
				{
					return (System.Guid)obj;
				}
				return System.Guid.Empty;
			}
			set
			{
				this.ViewState["InstanceCode"] = value;
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.InstanceCode = new System.Guid(base.Request["ic"]);
				this.hdnInstanceCode.Value = this.InstanceCode.ToString();
				this.btnStartWF.Attributes["onclick"] = "openAuditdit();";
				this.dgdFlow_Bind();
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnStartWF.Click += new System.EventHandler(this.btnStartWF_Click);
			this.dgdFlow.ItemDataBound += new DataGridItemEventHandler(this.dgdFlow_ItemDataBound);
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void dgdFlow_Bind()
		{
			this.dgdFlow.DataSource = FlowAuditAction.QueryFlow(this.InstanceCode, base.UserCode);
			this.dgdFlow.DataBind();
		}
		private void btnStartWF_Click(object sender, System.EventArgs e)
		{
			this.dgdFlow_Bind();
		}
		private void dgdFlow_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				if (dataRowView["FactOperator"].ToString() == "")
				{
					e.Item.Cells[3].Text = "";
				}
				else
				{
					e.Item.Cells[3].Text = PageHelper.QueryUser(this, dataRowView["FactOperator"].ToString());
				}
				if (dataRowView["Sing"].ToString() == "1")
				{
					if ((bool)dataRowView["AuditResult"])
					{
						e.Item.Cells[5].Text = "通过";
					}
					else
					{
						e.Item.Cells[5].Text = "驳回";
					}
				}
				else
				{
					if (dataRowView["Sing"].ToString() == "0")
					{
						e.Item.Cells[5].Text = "当前";
					}
					else
					{
						if (dataRowView["sing"].ToString() == "-1")
						{
							e.Item.Cells[5].Text = "待审";
						}
					}
				}
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.dgdFlow.ClientID + "');" + ((dataRowView["sing"].ToString() == "0" && dataRowView["UserCode"].ToString() != "") ? string.Concat(new string[]
				{
					"doClickRow('",
					dataRowView["NodeID"].ToString(),
					"','",
					dataRowView["TheOrder"].ToString(),
					"');"
				}) : "doClear();");
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				return;
			}
			default:
				return;
			}
		}
	}


