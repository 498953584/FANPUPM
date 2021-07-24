using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class FundApplicationList : PageBase, IRequiresSessionState
	{
		protected FundApplication fac = new FundApplication();
		protected string PrjCode = Guid.Empty.ToString();
		protected string Type = "";
		public string pt = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request.QueryString["pc"] != null && base.Request.QueryString["pc"].ToString() != "")
			{
				this.PrjCode = base.Request.QueryString["pc"].ToString();
				this.BtnAdd.Enabled = true;
			}
			if (base.Request.QueryString["pt"] != null)
			{
				this.pt = base.Request.QueryString["pt"].ToString();
			}
			this.Type = base.Request.QueryString["Type"].ToString();
			if (this.Type == "Audit")
			{
				this.BtnAdd.Enabled = false;
			}
			if (!base.IsPostBack)
			{
				this.DGrdList_DataBind();
				this.BtnAdd.Attributes["onclick"] = "javascript:return openWin('Add','" + this.PrjCode + "');";
				this.BtnMod.Attributes["onclick"] = "javascript:return openWin('Mod','" + this.PrjCode + "');";
				this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('确认要删除选定记录么？')){return false;}";
				this.BtnApply.Attributes["onclick"] = "javascript:if(!confirm('确定要提请审核当前选中数据吗？')){return false;}";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdList.ItemDataBound += new DataGridItemEventHandler(this.DGrdList_ItemDataBound);
		}
		private void DGrdList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				int num = e.Item.ItemIndex + 1;
				string text = this.DGrdList.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);clickRow('",
					text,
					"','",
					this.Type,
					"','",
					e.Item.Cells[5].Text,
					"');"
				});
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["style"] = "cursor:hand";
				e.Item.Cells[0].Text = num.ToString();
				e.Item.Cells[1].Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, e.Item.Cells[1].Text.Trim());
				if (e.Item.Cells[5].Text.Trim() == "-1")
				{
					e.Item.Cells[5].Text = "未确认";
					return;
				}
				if (e.Item.Cells[5].Text.Trim() == "0")
				{
					e.Item.Cells[5].Text = "处理中";
					return;
				}
				if (e.Item.Cells[5].Text.Trim() == "1")
				{
					e.Item.Cells[5].Text = "通过";
					return;
				}
				if (e.Item.Cells[5].Text.Trim() == "-2")
				{
					e.Item.Cells[5].Text = "驳回";
				}
			}
		}
		private void DGrdList_DataBind()
		{
			this.PageControl.PageCount = this.fac.getPrjAppRecordsCount(this.PrjCode, this.Session["yhdm"].ToString(), this.Type) / this.DGrdList.PageSize + 1;
			DataTable prjAppRecords = this.fac.getPrjAppRecords(this.PrjCode, this.Session["yhdm"].ToString(), this.Type, this.DGrdList.PageSize, this.PageControl.CurrentPageIndex);
			this.DGrdList.DataSource = prjAppRecords;
			this.DGrdList.DataBind();
		}
		protected void PageControl_PageIndexChange(object sender, EventArgs e)
		{
			this.DGrdList_DataBind();
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			this.DGrdList_DataBind();
		}
		protected void BtnMod_Click(object sender, EventArgs e)
		{
			this.DGrdList_DataBind();
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			Guid guidFlow = new Guid(this.hdnRecordID.Value);
			if (this.fac.delFundRecord(guidFlow))
			{
				this.JS.Text = "alert('删除记录成功！');";
			}
			else
			{
				this.JS.Text = "alert('删除记录失败！');";
			}
			this.DGrdList_DataBind();
		}
		protected void BtnApply_Click(object sender, EventArgs e)
		{
			Guid instanceCode = new Guid(this.hdnRecordID.Value);
			bool flag = FlowAuditAction.BeginFlow("101", instanceCode, 1, this.PrjCode, base.UserCode);
			if (flag)
			{
				this.JS.Text = "alert('启动申请审核成功！')";
			}
			else
			{
				this.JS.Text = "alert('启动申请审核失败！')";
			}
			this.DGrdList_DataBind();
		}
	}


