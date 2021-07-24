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
	public partial class TaskBillLook : PageBase, IRequiresSessionState
	{
		public string RecordCode = "";
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
		protected PlanAction plan
		{
			get
			{
				return new PlanAction();
			}
		}
		protected TaskBookAction tba
		{
			get
			{
				return new TaskBookAction();
			}
		}
		protected ResourceItemAction ta
		{
			get
			{
				return new ResourceItemAction();
			}
		}
		protected int NoteID
		{
			get
			{
				return Convert.ToInt32(this.ViewState["NOTEID"]);
			}
			set
			{
				this.ViewState["NOTEID"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.RecordCode = base.Request["tbc"].ToString().Trim();
			if (!base.IsPostBack)
			{
				if (base.Request["pc"] == null || base.Request["tbc"] == null)
				{
					this.JS.Text = "alert('参数错误！');history.go(-1);";
					return;
				}
				this.ProjectCode = new Guid(base.Request["pc"]);
				this.HdnProjectCode.Value = this.ProjectCode.ToString();
				this.TaskBillCode = base.Request["tbc"].ToString().Trim();
				this.FileLink1.MID = 29;
				this.FileLink1.FID = this.TaskBillCode;
				this.TaskBind();
				this.ViewState["UserCode"] = base.UserCode;
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
			this.DGrdOverWOrk.ItemDataBound += new DataGridItemEventHandler(this.DGrdOverWOrk_ItemDataBound);
		}
		private void TaskBind()
		{
			DataTable dataTable = this.tba.TaskBookList(this.ProjectCode, this.TaskBillCode);
			if (dataTable.Rows.Count > 0)
			{
				this.TxtQualityAndSafety.Text = dataTable.Rows[0]["QualityAndSafety"].ToString();
				this.lblRecordPerson.Text = dataTable.Rows[0]["RecordPerson"].ToString();
				this.lblConstructDate.Text = Convert.ToDateTime(dataTable.Rows[0]["ConstructDate"].ToString()).ToShortDateString();
			}
			DataTable dataTable2 = this.tba.TaskBookResourceList(this.TaskBillCode);
			dataTable2.Columns.Add("UserCode", typeof(string));
		}
		private void DataGrid_Bind(DataTable dtx)
		{
			this.HdnTaskList.Value = string.Empty;
			this.DGrdOverWOrk.DataSource = dtx;
			this.DGrdOverWOrk.DataBind();
			if (this.DGrdOverWOrk.Items.Count > 0)
			{
				string text = string.Empty;
				foreach (DataGridItem dataGridItem in this.DGrdOverWOrk.Items)
				{
					text = text + "," + dataGridItem.Cells[7].Text.Trim();
				}
				this.HdnTaskList.Value = text;
			}
		}
		private void DGrdOverWOrk_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.DGrdOverWOrk.ClientID,
					"');clickRow('",
					dataRowView["TaskCode"].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				Button button = (Button)e.Item.Cells[9].Controls[1];
				button.Attributes["onclick"] = string.Concat(new string[]
				{
					"javascript:return dblclickTaskRow('",
					this.ProjectCode.ToString(),
					"','",
					dataRowView["TaskCode"].ToString(),
					"','",
					this.TaskBillCode,
					"');"
				});
				return;
			}
			default:
				return;
			}
		}
	}


