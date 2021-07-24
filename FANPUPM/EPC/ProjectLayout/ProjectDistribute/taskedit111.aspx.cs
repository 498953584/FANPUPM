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
	public partial class taskedit111 : PageBase, IRequiresSessionState
	{
		public string jw = string.Empty;
		protected ConstructWBSTaskAction bta
		{
			get
			{
				return new ConstructWBSTaskAction();
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["jw"] != null)
			{
				this.jw = base.Request["jw"].ToString().Trim();
			}
			if (this.hdnType.Value == "2")
			{
				this.txtTaskCode.ReadOnly = true;
			}
			if (!this.Page.IsPostBack)
			{
				this.txtQuantity.Attributes["onblur"] = "checkDecimal(this);";
				this.txtUnitPrice.Attributes["onblur"] = "checkDecimal(this);";
				this.DateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.DateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
		private WBSBidTask BidTaskInfo()
		{
			WBSBidTask wBSBidTask = new WBSBidTask();
			wBSBidTask.ProjectCode = new Guid(this.hdnProjectCode.Value);
			this.hdnNoteID.Value.Trim();
			this.txtTaskCode.Text = this.txtTaskCode.Text.Trim().PadLeft(3, '0');
			if (this.hdnNoteID.Value.Trim() != "")
			{
				wBSBidTask.NoteID = (long)Convert.ToInt32(this.hdnNoteID.Value);
				wBSBidTask.TaskCode = this.txtParentCode.Text.Trim() + this.txtTaskCode.Text.Trim();
			}
			else
			{
				wBSBidTask.TaskCode = this.txtParentCode.Text.Trim() + this.txtTaskCode.Text.Trim();
			}
			wBSBidTask.TaskName = this.txtTaskName.Text;
			wBSBidTask.ParentTaskCode = this.txtParentCode.Text.Trim();
			wBSBidTask.Quantity = ((this.txtQuantity.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtQuantity.Text));
			wBSBidTask.QuantityUnit = this.txtUnit.Text;
			wBSBidTask.SynthPrice = ((this.txtUnitPrice.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtUnitPrice.Text.Trim()));
			wBSBidTask.WorkLayer = Convert.ToInt32(this.ddlTaskType.SelectedValue);
			wBSBidTask.StartDate = Convert.ToDateTime(this.DateStart.Text.Trim());
			wBSBidTask.EndDate = Convert.ToDateTime(this.DateEnd.Text.Trim());
			wBSBidTask.sumPrice = wBSBidTask.SynthPrice * wBSBidTask.Quantity;
			wBSBidTask.WbsType = Convert.ToInt32(this.hdnWbsType.Value);
			return wBSBidTask;
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			string value = this.hdnProjectCode.Value;
			string text = base.Server.UrlEncode(this.hdnProjectName.Value);
			string value2 = this.hdnPrjGuid.Value;
			WBSBidTask wBSBidTask = new WBSBidTask();
			wBSBidTask = this.BidTaskInfo();
			if (this.jw != string.Empty)
			{
				if (this.hdnType.Value == "1")
				{
					if (this.bta.IfSaveTaskCode(wBSBidTask.TaskCode.Trim(), new Guid(this.hdnProjectCode.Value), wBSBidTask.WbsType))
					{
						this.txtTaskCode.Text = "";
						this.JS.Text = "alert('任务代码不能重复！');";
						return;
					}
					if (this.bta.BidScheduleAdd(wBSBidTask) == 1)
					{
						this.JS.Text = string.Concat(new string[]
						{
							"alert('保存成功!');window.parent.InfoList.location.href ='WBSQuery",
							(wBSBidTask.WbsType == 1) ? "" : "1",
							".aspx?pc=",
							value,
							"&PrjGuid=",
							value2,
							"&pn=",
							text,
							"&jw=",
							this.jw,
							"';self.location.href='TaskEdit.aspx?jw=",
							this.jw,
							"';"
						});
						return;
					}
					this.JS.Text = "alert('保存失败!');";
					return;
				}
				else
				{
					if (this.bta.BidScheduleUp(wBSBidTask, "0") == 1)
					{
						this.JS.Text = string.Concat(new string[]
						{
							"alert('保存成功!');window.parent.InfoList.location.href ='WBSQuery",
							(wBSBidTask.WbsType == 1) ? "" : "1",
							".aspx?pc=",
							value,
							"&PrjGuid=",
							value2,
							"&pn=",
							text,
							"&jw=",
							this.jw,
							"';self.location.href='TaskEdit.aspx?jw=",
							this.jw,
							"';"
						});
						return;
					}
					this.JS.Text = "alert('修改错误！');";
					return;
				}
			}
			else
			{
				if (this.bta.BidScheduleUp(wBSBidTask, "1") == 1)
				{
					this.JS.Text = string.Concat(new string[]
					{
						"alert('变更任务成功!');window.parent.InfoList.location.href ='WBSQuery2.aspx?pc=",
						value,
						"&PrjGuid=",
						value2,
						"&pn=",
						text,
						"';self.location.href='TaskEdit.aspx';"
					});
					return;
				}
				this.JS.Text = "alert('变更任务失败！');";
				return;
			}
		}
	}


