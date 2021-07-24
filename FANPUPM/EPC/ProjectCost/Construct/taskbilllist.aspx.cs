using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TaskBillList : PageBase, IRequiresSessionState
    {
        protected Button BtnEdit;
		protected Guid ProjectCode
		{
			get
			{
				return (Guid)this.ViewState["PROJECTCODE"];
			}
			set
			{
				this.ViewState["PROJECTCODE"] = value;
			}
		}
		protected string TaskCode
		{
			get
			{
				return this.ViewState["TASKCODE"].ToString();
			}
			set
			{
				this.ViewState["TASKCODE"] = value;
			}
		}
		protected string TaskName
		{
			get
			{
				return this.ViewState["TASKNAME"].ToString();
			}
			set
			{
				this.ViewState["TASKNAME"] = value;
			}
		}
		protected TaskBookAction tba
		{
			get
			{
				return new TaskBookAction();
			}
		}
		protected int type
		{
			get
			{
				return (int)this.ViewState["TYPE"];
			}
			set
			{
				this.ViewState["TYPE"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["pc"] == null || base.Request["t"] == null)
				{
					this.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
					return;
				}
				this.type = Convert.ToInt32(base.Request["t"].ToString());
				this.ProjectCode = new Guid(base.Request["pc"]);
				this.dgdResourceBind();
				if (this.type == 1)
				{
					this.BtnAdd.Attributes["onclick"] = string.Concat(new object[]
					{
						"doTaskBook('",
						this.type,
						"','",
						this.ProjectCode,
						"');return false"
					});
					this.BtnMod.Attributes["onclick"] = "doTaskBook('3','" + this.ProjectCode + "');return false";
					this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;} ";
					return;
				}
				this.BtnEdit.Attributes["onclick"] = string.Concat(new object[]
				{
					"doTaskBook('",
					this.type,
					"','",
					this.ProjectCode,
					"');return false"
				});
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgdRelation.ItemDataBound += new DataGridItemEventHandler(this.dgdRelation_ItemDataBound);
		}
		private void dgdResourceBind()
		{
			this.dgdRelation.DataSource = this.tba.TaskBookList(this.ProjectCode);
			this.dgdRelation.DataBind();
		}
		private void dgdRelation_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView arg_2D_0 = (DataRowView)e.Item.DataItem;
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onclick"] = string.Concat(new object[]
				{
					"doClick(this,'",
					this.dgdRelation.ClientID,
					"');doClickRow('",
					e.Item.Cells[6].Text,
					"','",
					this.type,
					"')"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				Button button = (Button)e.Item.Cells[3].FindControl("btnQuantity");
				button.Attributes["onclick"] = string.Concat(new object[]
				{
					"javascript:doClickRow('",
					e.Item.Cells[6].Text,
					"','",
					this.type,
					"');OpenQuantityList('",
					this.ProjectCode,
					"');return false;"
				});
				Button button2 = (Button)e.Item.Cells[4].FindControl("btnRes");
				button2.Attributes["onclick"] = string.Concat(new object[]
				{
					"javascript:doClickRow('",
					e.Item.Cells[6].Text,
					"','",
					this.type,
					"');OpenResourceGather('",
					this.ProjectCode,
					"');return false;"
				});
				EPC_UserControl1_FileLink ePC_UserControl1_FileLink = (EPC_UserControl1_FileLink)e.Item.Cells[5].Controls[1];
				ePC_UserControl1_FileLink.MID = 29;
				ePC_UserControl1_FileLink.FID = e.Item.Cells[6].Text.ToString();
				ePC_UserControl1_FileLink.Type = 0;
				return;
			}
			default:
				return;
			}
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			if (this.tba.TaskCodeBookDel(this.HdnTaskBookCode.Value) == 1)
			{
				this.dgdResourceBind();
				this.RegisterStartupScript("", "<script>alert('删除成功!');</script>");
				return;
			}
			this.RegisterStartupScript("", "<script>alert('删除失败!');</script>");
		}
	}


