using ASP;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
	public partial class USchedulePlan : System.Web.UI.UserControl
	{
		protected ConstructWBSTaskAction TaskAct
		{
			get
			{
				return new ConstructWBSTaskAction();
			}
		}
		public Guid ProjectCode
		{
			get
			{
				object obj = this.ViewState["PROJECTCODE"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["PROJECTCODE"] = value;
			}
		}
		public string TaskCode
		{
			get
			{
				return (string)this.ViewState["TASKCODE"];
			}
			set
			{
				this.ViewState["TASKCODE"] = value;
			}
		}
		protected string ProjectName
		{
			get
			{
				object obj = this.ViewState["PROJECTNAME"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PROJECTNAME"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.DtxStartDate.Attributes["onactivate "] = "getEndDate(this,'UCSchedulePlan_DtxEndDate');";
				this.DtxEndDate.Attributes["onactivate "] = "getBeginDate(this,'UCSchedulePlan_DtxStartDate');";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		public void TaskList_Bind(string flag)
		{
			WBSBidTask singleTask = this.TaskAct.GetSingleTask(this.ProjectCode, this.TaskCode, 1);
			if (singleTask != null)
			{
				this.GetTaskListFromWBSBidTask(singleTask);
			}
			else
			{
				this.DtxEndDate.Text = DateTime.Now.ToShortDateString();
				this.DtxStartDate.Text = DateTime.Now.ToShortDateString();
			}
			if (flag == "View")
			{
				this.TxtQuality.Enabled = false;
				this.TxtQuantity.Enabled = false;
				this.TxtRemark.Enabled = false;
				this.TxtSafety.Enabled = false;
				this.TxtTaskCode.Enabled = false;
				this.TxtTaskName.Enabled = false;
				this.TxtUnit.Enabled = false;
				this.TxtWorkDay.Enabled = false;
				this.DtxEndDate.Enabled = false;
				this.DtxStartDate.Enabled = false;
				this.ChkPivotal.Enabled = false;
				this.DDLTaskState.Enabled = false;
				this.DDLTaskType.Enabled = false;
			}
		}
		private void GetTaskListFromWBSBidTask(WBSBidTask tk)
		{
			this.TxtQuality.Text = tk.Quality;
			this.TxtQuantity.Text = tk.Quantity.ToString();
			this.TxtRemark.Text = tk.Remark;
			this.TxtSafety.Text = tk.Safety;
			this.TxtTaskCode.Text = tk.TaskCode;
			this.TxtTaskName.Text = tk.TaskName;
			this.TxtUnit.Text = tk.QuantityUnit;
			this.TxtWorkDay.Text = tk.WorkDay.ToString();
			this.DtxEndDate.Text = tk.EndDate.ToShortDateString();
			this.DtxStartDate.Text = tk.StartDate.ToShortDateString();
			this.ChkPivotal.Checked = tk.Pivotal;
			try
			{
				this.DDLTaskState.SelectedValue = tk.TaskState.ToString();
				this.DDLTaskType.SelectedValue = tk.WorkLayer.ToString();
			}
			catch
			{
			}
		}
		private WBSBidTask GetWBSBidTaskFromPage()
		{
			WBSBidTask wBSBidTask = new WBSBidTask();
			wBSBidTask.ProjectCode = this.ProjectCode;
			wBSBidTask.Quality = this.TxtQuality.Text;
			wBSBidTask.Quantity = Convert.ToDecimal((this.TxtQuantity.Text == "") ? "0" : this.TxtQuantity.Text);
			wBSBidTask.Remark = this.TxtRemark.Text;
			wBSBidTask.Safety = this.TxtSafety.Text;
			wBSBidTask.TaskCode = this.TxtTaskCode.Text;
			wBSBidTask.TaskName = this.TxtTaskName.Text;
			wBSBidTask.QuantityUnit = this.TxtUnit.Text;
			wBSBidTask.WorkDay = Convert.ToInt32((this.TxtWorkDay.Text == "") ? "0" : this.TxtWorkDay.Text);
			wBSBidTask.EndDate = ((this.DtxEndDate.Text.Trim() == "") ? DateTime.MinValue : Convert.ToDateTime(this.DtxEndDate.Text));
			wBSBidTask.StartDate = ((this.DtxStartDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(this.DtxStartDate.Text));
			wBSBidTask.Pivotal = this.ChkPivotal.Checked;
			try
			{
				wBSBidTask.TaskState = int.Parse(this.DDLTaskState.SelectedValue);
				wBSBidTask.WorkLayer = int.Parse(this.DDLTaskType.SelectedValue);
			}
			catch
			{
			}
			return wBSBidTask;
		}
		public int UpdateTaskList()
		{
			WBSBidTask wBSBidTaskFromPage = this.GetWBSBidTaskFromPage();
			if (this.TaskAct.UpdateTaskList(wBSBidTaskFromPage) == 1)
			{
				return 1;
			}
			return 0;
		}
	}


