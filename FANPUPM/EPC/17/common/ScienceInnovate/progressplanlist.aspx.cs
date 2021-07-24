using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProgressPlanList_ : NBasePage, IRequiresSessionState
	{
		protected string Type;
		protected string prjCode;
		protected int pageSize = 20;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["prjCode"] == null)
				{
					this.btnPromiss.Visible = false;
					this.btnNew.Visible = false;
					this.btnEdit.Visible = false;
					this.btnDel.Visible = false;
					this.btnAudit.Visible = false;
					this.js.Text = "DisplayBtn();";
					return;
				}
				this.ViewState["PRJCODE"] = base.Request.Params["prjCode"].ToString();
				this.prjCode = base.Request.Params["prjCode"].ToString();
				if (base.Request.Params["Type"] != null)
				{
					this.ViewState["TYPE"] = base.Request.Params["Type"].ToString();
					this.Type = base.Request.Params["Type"].ToString();
				}
				this.BindData();
				this.BindControlMothd();
			}
			this.prjCode = this.ViewState["PRJCODE"].ToString();
			this.Type = this.ViewState["TYPE"].ToString();
		}
		private void BindControlMothd()
		{
			this.hidPrjCode.Value = this.prjCode;
			this.btnPromiss.Attributes["onclick"] = "return dodoAudit();";
			this.btnNew.Attributes["onclick"] = "return doEdit('New');";
			this.btnEdit.Attributes["onclick"] = "return doEdit('Edit');";
			this.btnDel.Attributes["onclick"] = "return doDel();";
			this.btnAudit.Attributes["onclick"] = "return doPrjAudit();";
		}
		private void setControl()
		{
			if (this.Type == "Edit")
			{
				this.btnPromiss.Visible = false;
				this.btnAudit.Visible = false;
				this.js.Text = "document.getElementById(\"btnShow\").style.display=\"none\";";
			}
			else
			{
				if (this.Type == "Auditing")
				{
					this.btnNew.Visible = false;
					this.btnEdit.Visible = false;
					this.btnAudit.Visible = false;
				}
			}
			if (this.Type == "View")
			{
				this.btnNew.Visible = false;
				this.btnEdit.Visible = false;
				this.btnDel.Visible = false;
				this.btnAudit.Visible = false;
				this.btnPromiss.Visible = false;
			}
			if (this.Type == "List")
			{
				this.btnNew.Visible = false;
				this.btnEdit.Visible = false;
				this.btnDel.Visible = false;
				this.btnPromiss.Visible = false;
			}
		}
		private void BindData()
		{
			this.setControl();
			ProgressPlanCollection dataSource = new ProgressPlanCollection();
			ProgressPlanAction progressPlanAction = new ProgressPlanAction();
			if (this.Type == "Edit")
			{
				dataSource = progressPlanAction.GetPpmProgressPlanInfos(this.prjCode);
				this.dgList.Columns[1].Visible = false;
			}
			else
			{
				dataSource = progressPlanAction.GetEntProgressPlanInfos(this.prjCode, this.Type);
			}
			this.dgList.DataSource = dataSource;
			this.dgList.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgList.ItemDataBound += new DataGridItemEventHandler(this.dgList_ItemDataBound);
		}
		public void dgList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Attributes["id"] = this.dgList.DataKeys[e.Item.ItemIndex].ToString();
				int num = int.Parse(((HtmlInputHidden)e.Item.FindControl("hidAuditState")).Value);
				if (num > 3)
				{
					e.Item.Attributes["onclick"] = string.Concat(new string[]
					{
						"OnRecord(this);ClickRow(this,'",
						this.dgList.ClientID,
						"','",
						this.dgList.DataKeys[e.Item.ItemIndex].ToString(),
						"','DisAbled');"
					});
				}
				else
				{
					e.Item.Attributes["onclick"] = string.Concat(new string[]
					{
						"OnRecord(this);ClickRow(this,'",
						this.dgList.ClientID,
						"','",
						this.dgList.DataKeys[e.Item.ItemIndex].ToString(),
						"','');"
					});
				}
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
		protected void btnPromiss_Click(object sender, EventArgs e)
		{
			this.BindData();
		}
		protected void btnNew_Click(object sender, EventArgs e)
		{
			this.BindData();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.BindData();
		}
		protected void dgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			this.BindData();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			if (ProgressPlanAction.DelPlanRecord(this.hidPlanId.Value))
			{
				this.js.Text = "alert(\"操作成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.BindData();
		}
		protected void pc_PageIndexChange(object sender, EventArgs e)
		{
			this.BindData();
		}
		protected void btnAudit_Click(object sender, EventArgs e)
		{
			this.BindData();
		}
	}


