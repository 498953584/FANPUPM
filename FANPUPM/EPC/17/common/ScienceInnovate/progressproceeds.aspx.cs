using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProgressProceeds : BasePage, IRequiresSessionState
	{
		private string PrjCode;
		private string Type;
		private int pageSize = 20;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["prjCode"] == null)
				{
					this.SetControlDisable();
					return;
				}
				this.ViewState["PRJCODE"] = base.Request.Params["prjCode"].ToString();
				this.hidPrjCode.Value = this.ViewState["PRJCODE"].ToString();
				this.PrjCode = this.ViewState["PRJCODE"].ToString();
				if (base.Request.Params["Type"] != null)
				{
					this.ViewState["TYPE"] = base.Request.Params["Type"].ToString();
					this.Type = this.ViewState["TYPE"].ToString();
				}
				this.SetControlMothd();
				this.BindData();
			}
			if (this.ViewState["PRJCODE"] != null && this.ViewState["TYPE"] != null)
			{
				this.PrjCode = this.ViewState["PRJCODE"].ToString();
				this.Type = this.ViewState["TYPE"].ToString();
				return;
			}
			this.SetControlDisable();
		}
		private void SetControlDisable()
		{
			this.btnAudit.Visible = false;
			this.btnCanncle.Visible = false;
			this.btnDel.Visible = false;
			this.btnEdit.Visible = false;
			this.btnNew.Visible = false;
			this.pc.Enabled = false;
		}
		private void SetControlMothd()
		{
			this.btnNew.Attributes["onclick"] = "return doEdit(\"New\");";
			this.btnEdit.Attributes["onclick"] = "return doEdit(\"Edit\");";
			this.btnView.Attributes["onclick"] = "return doEdit(\"View\");";
			this.btnDel.Attributes["onclick"] = "return CheckData();";
			this.btnCanncle.Attributes["onclick"] = "return CheckData();";
			this.btnAudit.Attributes["onclick"] = "return doAudit();";
			if (this.Type == "Edit")
			{
				this.btnAudit.Visible = false;
				this.btnCanncle.Visible = false;
				return;
			}
			this.btnEdit.Visible = false;
			this.btnDel.Visible = false;
			this.btnNew.Visible = false;
		}
		private void BindData()
		{
			this.hidMainId.Value = "";
			ProgressProceedAction progressProceedAction = new ProgressProceedAction();
			this.dgList.DataSource = this.GetPageData(progressProceedAction.GetProceInfos(this.PrjCode));
			this.dgList.DataBind();
		}
		private ProgressProceedCollection GetPageData(ProgressProceedCollection objShouSchs)
		{
			ProgressProceedCollection progressProceedCollection = new ProgressProceedCollection();
			if (objShouSchs.Count > this.pageSize * (objShouSchs.Count / this.pageSize))
			{
				this.pc.PageCount = objShouSchs.Count / this.pageSize + 1;
			}
			else
			{
				this.pc.PageCount = objShouSchs.Count / this.pageSize;
			}
			if (objShouSchs.Count > this.pageSize * (this.pc.CurrentPageIndex - 1))
			{
				int num = this.pageSize * this.pc.CurrentPageIndex;
				if (objShouSchs.Count < this.pageSize * this.pc.CurrentPageIndex)
				{
					num = objShouSchs.Count;
				}
				for (int i = this.pageSize * (this.pc.CurrentPageIndex - 1); i < num; i++)
				{
					progressProceedCollection.Add(objShouSchs[i]);
				}
			}
			else
			{
				progressProceedCollection = objShouSchs;
			}
			return progressProceedCollection;
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
		private void dgList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
				if (((HtmlInputHidden)e.Item.FindControl("auditResult")).Value == "True")
				{
					e.Item.Attributes["onclick"] = string.Concat(new string[]
					{
						"OnRecord(this);ClickRow(this,'",
						this.dgList.ClientID,
						"','",
						this.dgList.DataKeys[e.Item.ItemIndex].ToString(),
						"','No');"
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
				e.Item.Attributes["ondblclick"] = "return doEdit(\"View\");";
			}
		}
		protected void btnNew_Click(object sender, EventArgs e)
		{
			this.BindData();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.BindData();
		}
		protected void btnAudit_Click(object sender, EventArgs e)
		{
			this.BindData();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			if (ProgressProceedAction.DelProce(int.Parse(this.hidMainId.Value)))
			{
				this.js.Text = "alert(\"操作成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.BindData();
		}
		protected void btnCanncle_Click(object sender, EventArgs e)
		{
			if (ProgressProceedAction.CanncleAuditState(int.Parse(this.hidMainId.Value)))
			{
				this.js.Text = "alert(\"操作成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.BindData();
		}
	}


