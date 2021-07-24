using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProgressImplementItem : NBasePage, IRequiresSessionState
	{
		protected string planId;
		protected string Type;
		protected int pageSize = 9;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["planId"] == null)
				{
					this.btnEdit.Attributes.CssStyle.Add("display", "none");
					this.btnNew.Attributes.CssStyle.Add("display", "none");
					this.btnDel.Attributes.CssStyle.Add("display", "none");
					this.btnAppraise.Attributes.CssStyle.Add("display", "none");
					return;
				}
				this.ViewState["PLANID"] = base.Request.Params["planId"].ToString();
				this.planId = this.ViewState["PLANID"].ToString();
				this.hidPlanId.Value = this.planId;
				if (base.Request.Params["Type"] != null)
				{
					this.ViewState["TYPE"] = base.Request.Params["Type"].ToString();
					this.Type = this.ViewState["TYPE"].ToString();
				}
				this.setControlEnbale();
				this.BindData();
			}
			this.Type = this.ViewState["TYPE"].ToString();
			this.planId = this.ViewState["PLANID"].ToString();
		}
		private void setControlEnbale()
		{
			this.btnNew.Attributes["onclick"] = "return doEdit(\"new\");";
			this.btnAppraise.Attributes["onclick"] = "return doAppraise();";
			this.btnDel.Attributes["onclick"] = "return doDel();";
			this.btnEdit.Attributes["onclick"] = "return doEdit(\"edit\");";
			if (this.Type == "Edit")
			{
				this.btnAppraise.Attributes.CssStyle.Add("display", "none");
				return;
			}
			this.btnEdit.Attributes.CssStyle.Add("display", "none");
			this.btnNew.Attributes.CssStyle.Add("display", "none");
		}
		private void BindData()
		{
			this.hidMainId.Value = "";
			ProgressImplementAction progressImplementAction = new ProgressImplementAction();
			this.dgList.DataSource = progressImplementAction.GetImplementInfos(this.planId);
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
		private void dgList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Attributes["id"] = this.dgList.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);ClickRow(this,'",
					this.dgList.ClientID,
					"','",
					this.dgList.DataKeys[e.Item.ItemIndex].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
				if (e.Item.Cells[4].Text.Length > 10)
				{
					string text = e.Item.Cells[4].Text;
					e.Item.Cells[4].Attributes["Title"] = text;
					e.Item.Cells[4].Text = text.Substring(0, 10) + "...";
				}
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
		protected void btnAppraise_Click(object sender, EventArgs e)
		{
			this.BindData();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			if (ProgressImplementAction.DelImplement(this.hidMainId.Value))
			{
				this.js.Text = "alert(\"操作成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.BindData();
		}
		protected void dgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			this.BindData();
		}
		protected void pc_PageIndexChange(object sender, EventArgs e)
		{
			this.BindData();
		}
	}


