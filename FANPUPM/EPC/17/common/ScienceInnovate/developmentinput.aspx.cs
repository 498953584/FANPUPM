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
	public partial class DevelopmentInput : BasePage, IRequiresSessionState
	{
		protected int pageSize = 5;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["prjCode"] != null)
				{
					this.hidPrjCode.Value = base.Request.Params["prjCode"].ToString();
					if (base.Request.Params["Type"] != null)
					{
						this.hidType.Value = base.Request.Params["Type"].ToString();
					}
					this.SetControlVisible();
					this.BindData();
					return;
				}
				this.SetDisAbled();
			}
		}
		private void SetDisAbled()
		{
			this.btnAudit.Visible = false;
			this.btnNotAudit.Visible = false;
			this.btnNew.Visible = false;
			this.btnEdit.Visible = false;
			this.btnDel.Visible = false;
			this.pc.Enabled = false;
		}
		private void SetControlVisible()
		{
			this.btnDel.Attributes["onclick"] = "return doDel();";
			this.btnEdit.Attributes["onclick"] = "return doEdit(\"Edit\",\"" + base.Request.Params["prjName"].ToString() + "\");";
			this.btnNew.Attributes["onclick"] = "return doEdit(\"new\",\"" + base.Request.Params["prjName"].ToString() + "\");";
			this.btnAudit.Attributes["onclick"] = "return doAudit();";
			this.btnNotAudit.Attributes["onclick"] = "return CheckData();";
			if (this.hidType.Value == "Edit")
			{
				this.btnAudit.Visible = false;
				this.btnNotAudit.Visible = false;
				this.dgList.Columns[4].Visible = false;
				this.dgList.Columns[5].Visible = false;
				this.dgList.Columns[6].Visible = false;
				this.dgList.Columns[7].Visible = false;
				return;
			}
			this.btnNew.Visible = false;
			this.btnEdit.Visible = false;
			this.btnDel.Visible = false;
			this.dgList.Columns[1].Visible = false;
		}
		private void BindData()
		{
			this.hidMainId.Value = "";
			DevelopmentInputAction developmentInputAction = new DevelopmentInputAction();
			this.dgList.DataSource = this.GetPageData(developmentInputAction.GetMainInputInfos(this.hidPrjCode.Value));
			this.dgList.DataBind();
		}
		private DevelopmentInputCollectin GetPageData(DevelopmentInputCollectin objShouSchs)
		{
			DevelopmentInputCollectin developmentInputCollectin = new DevelopmentInputCollectin();
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
					developmentInputCollectin.Add(objShouSchs[i]);
				}
			}
			else
			{
				developmentInputCollectin = objShouSchs;
			}
			return developmentInputCollectin;
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
		protected void btnNotAudit_Click(object sender, EventArgs e)
		{
			if (DevelopmentInputAction.CancelAudit(int.Parse(this.hidMainId.Value)))
			{
				this.js.Text = "alert(\"操作成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.BindData();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			if (DevelopmentInputAction.DelInput(int.Parse(this.hidMainId.Value)))
			{
				this.js.Text = "alert(\"操作成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.BindData();
		}
		private void dgList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				string value = ((HtmlInputHidden)e.Item.FindControl("auditResult")).Value;
				if (value == "True")
				{
					e.Item.Attributes["onclick"] = string.Concat(new string[]
					{
						"OnRecord(this);ClickRow(this,'",
						this.dgList.ClientID,
						"','",
						this.dgList.DataKeys[e.Item.ItemIndex].ToString(),
						"','true');"
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
		protected void pc_PageIndexChange(object sender, EventArgs e)
		{
			this.BindData();
		}
	}


