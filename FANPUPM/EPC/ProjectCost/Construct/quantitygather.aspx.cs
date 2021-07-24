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
	public partial class QuantityGather : PageBase, IRequiresSessionState
	{
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
		protected string TaskBillCode
		{
			get
			{
				return this.ViewState["TASKBILLCODE"].ToString();
			}
			set
			{
				this.ViewState["TASKBILLCODE"] = value;
			}
		}
		protected TaskBookAction tba
		{
			get
			{
				return new TaskBookAction();
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["pc"] == null || base.Request["bc"] == null)
				{
					this.JS.Text = "alert('参数错误！');";
					return;
				}
				this.ProjectCode = new Guid(base.Request["pc"]);
				this.TaskBillCode = base.Request["bc"].ToString().Trim();
				DataTable taskBook = this.tba.GetTaskBook2(this.ProjectCode, this.TaskBillCode);
				this.DataGrid_Bind(taskBook);
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgTaskList.ItemDataBound += new DataGridItemEventHandler(this.dgTaskList_ItemDataBound);
		}
		private void DataGrid_Bind(DataTable dt)
		{
			this.dgTaskList.DataSource = dt;
			this.dgTaskList.DataBind();
		}
		private void dgTaskList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["style"] = "cursor:hand";
				return;
			default:
				return;
			}
		}
	}


