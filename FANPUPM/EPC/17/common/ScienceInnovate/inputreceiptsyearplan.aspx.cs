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
	public partial class InputReceiptsYearPlan : BasePage, IRequiresSessionState
	{
		protected string PrjCode;
		protected int pageSize = 5;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (!(base.Request.Params["Type"] != ""))
				{
					this.SetControlEnable("");
					return;
				}
				this.SetControlEnable(base.Request.Params["Type"].ToString());
				this.hidPageType.Value = base.Request.Params["Type"].ToString();
				this.BindData();
			}
		}
		private void SetControlEnable(string PageType)
		{
			this.btnDel.Attributes["onclick"] = "return doDel();";
			this.btnEdit.Attributes["onclick"] = "return doEdit(\"Edit\");";
			this.btnNew.Attributes["onclick"] = "return doEdit(\"New\");";
			if (PageType == "")
			{
				this.btnDel.Enabled = false;
				this.btnEdit.Enabled = false;
				this.btnNew.Enabled = false;
				this.js.Text = "document.getElementById(\"btnCreateTable\").disabled=true;";
				return;
			}
			if (PageType == "List")
			{
				this.btnDel.Visible = false;
				this.btnEdit.Visible = false;
				this.btnNew.Visible = false;
			}
		}
		private void BindData()
		{
			this.hidMainId.Value = "";
			inputReceiptAction inputReceiptAction = new inputReceiptAction();
			this.dgYearPlanList.DataSource = this.GetPageData(inputReceiptAction.GetYearPlanInfos(this.PrjCode));
			this.dgYearPlanList.DataBind();
		}
		private inputReceiptYearCollectin GetPageData(inputReceiptYearCollectin objShouSchs)
		{
			inputReceiptYearCollectin inputReceiptYearCollectin = new inputReceiptYearCollectin();
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
					inputReceiptYearCollectin.Add(objShouSchs[i]);
				}
			}
			else
			{
				inputReceiptYearCollectin = objShouSchs;
			}
			return inputReceiptYearCollectin;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgYearPlanList.ItemDataBound += new DataGridItemEventHandler(this.dgYearPlanList_ItemDataBound);
		}
		private void dgYearPlanList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);ClickRow(this,'",
					this.dgYearPlanList.ClientID,
					"','",
					this.dgYearPlanList.DataKeys[e.Item.ItemIndex].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
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
		protected void btnDel_Click(object sender, EventArgs e)
		{
			if (inputReceiptAction.DelYearPlan(int.Parse(this.hidMainId.Value)))
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
	}


