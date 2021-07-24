using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TaskBillMod : PageBase, IRequiresSessionState
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
			this.FileLink1.MID = 29;
			this.FileLink1.FID = this.RecordCode;
			this.FileLink1.Type = 1;
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
				this.TaskBind();
				this.ViewState["UserCode"] = base.UserCode;
				TaskBookAction.DeleteTempResource(this.ViewState["UserCode"].ToString());
				this.BtnPickWBS.Attributes["onclick"] = "javascript:return OpenPickWBS('" + this.ProjectCode.ToString() + "');";
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
				this.TxtRecordPerson.Text = dataTable.Rows[0]["RecordPerson"].ToString();
				try
				{
					this.DbConstructDate.Text = Convert.ToDateTime(dataTable.Rows[0]["ConstructDate"].ToString()).ToShortDateString();
				}
				catch
				{
				}
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
		private ArrayList TaskBookInfo()
		{
			ArrayList arrayList = new ArrayList();
			if (this.DGrdOverWOrk.Items.Count > 0)
			{
				foreach (DataGridItem dataGridItem in this.DGrdOverWOrk.Items)
				{
					ConstructTaskBook constructTaskBook = new ConstructTaskBook();
					constructTaskBook.TaskBookCode = this.TaskBillCode;
					constructTaskBook.TaskCode = dataGridItem.Cells[7].Text;
					constructTaskBook.ProjectCode = this.ProjectCode;
					constructTaskBook.RecordPerson = this.TxtRecordPerson.Text.ToString();
					constructTaskBook.ConstructDate = Convert.ToDateTime(this.DbConstructDate.Text);
					constructTaskBook.ConstructUnit = 0;
					constructTaskBook.QualityAndSafety = this.TxtQualityAndSafety.Text;
					try
					{
						constructTaskBook.TodayComplete = ((((TextBox)dataGridItem.Cells[4].Controls[1]).Text == "") ? 0m : Convert.ToDecimal(((TextBox)dataGridItem.Cells[4].Controls[1]).Text));
					}
					catch
					{
					}
					constructTaskBook.WorkContent = ((TextBox)dataGridItem.Cells[5].Controls[1]).Text;
					constructTaskBook.TodayWorkRemark = ((TextBox)dataGridItem.Cells[6].Controls[1]).Text;
					constructTaskBook.NoteID = 0;
					arrayList.Add(constructTaskBook);
				}
			}
			return arrayList;
		}
		private ArrayList UpdateTaskListInfo()
		{
			ArrayList arrayList = new ArrayList();
			if (this.DGrdOverWOrk.Items.Count > 0)
			{
				foreach (DataGridItem dataGridItem in this.DGrdOverWOrk.Items)
				{
					TaskListInfo taskListInfo = new TaskListInfo();
					taskListInfo.ProjectCode = this.ProjectCode;
					taskListInfo.TaskCode = dataGridItem.Cells[7].Text;
					try
					{
						taskListInfo.CompleteCount = ((dataGridItem.Cells[8].Text == "") ? 0m : Convert.ToDecimal(dataGridItem.Cells[8].Text)) - ((((TextBox)dataGridItem.Cells[4].Controls[1]).Text == "") ? 0m : Convert.ToDecimal(((TextBox)dataGridItem.Cells[4].Controls[1]).Text));
					}
					catch
					{
					}
					arrayList.Add(taskListInfo);
				}
			}
			return arrayList;
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			ArrayList arrayList = this.TaskBookInfo();
			if (arrayList.Count == 0)
			{
				this.JS.Text = "alert('请选择施工内容！');";
				return;
			}
			if (this.tba.TaskBookUp(this.TaskBookInfo(), this.UpdateTaskListInfo()) == 1)
			{
				this.Session.Remove("TEMPBOOKRESOURCE");
				this.JS.Text = "alert('保存存功！');window.document.location.href = \"TaskBillList.aspx?pc=" + this.ProjectCode + "&t=1\"";
			}
			else
			{
				this.JS.Text = "alert('保存失败！');";
			}
			this.DoAlertMsg(arrayList);
		}
		protected void BtnExit_Click(object sender, EventArgs e)
		{
			this.TaskBind();
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
        private void DoAlertMsg(ArrayList TaskInfos)
        {
            string[] strArray = new string[] { ConfigurationSettings.AppSettings["Artificial consumption"], ConfigurationSettings.AppSettings["Material consumption"], ConfigurationSettings.AppSettings["Equipment consumption"] };
            UserInfo currentUserInfo = userManageDb.GetCurrentUserInfo();
            for (int i = 0; i < TaskInfos.Count; i++)
            {
                ConstructTaskBook book = (ConstructTaskBook)TaskInfos[i];
                for (int j = 0; j < strArray.Length; j++)
                {
                    AlertPoint point = new AlertPoint(strArray[j]);
                    if (point.Options[2].IsSelected)
                    {
                        SchedulePlanAction action = new SchedulePlanAction();
                        new ResourceItemAction();
                        AlertMessage message = new AlertMessage();
                        if (currentUserInfo != null)
                        {
                            message.ManInput = currentUserInfo.UserCode;
                        }
                        message.MenAlertTo = point.YHDMsOfPeopleAlertTo;
                        message.TimeInput = DateTime.Now;
                        message.TimeOutput = DateTime.Now;
                        message.TimeOver = DateTime.Now.AddDays(point.ValidTimeLong);
                        message.PresentimentID = point.pkID;
                        message.PrjCode = this.ProjectCode.ToString();
                        DataTable table = TaskBookAction.DoAlert(this.ProjectCode, book.TaskCode, j + 1, book.TaskBookCode);
                        if (table.Rows.Count > 0)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                decimal num3 = (decimal.Parse(row["ResBudget"].ToString()) == 0M) ? 0M : (decimal.Parse(row["ResConsum"].ToString()) / decimal.Parse(row["ResBudget"].ToString()));
                                decimal num4 = (decimal.Parse(row["Quantity"].ToString()) == 0M) ? 0M : (decimal.Parse(row["Complete"].ToString()) / decimal.Parse(row["Quantity"].ToString()));
                                if (num3 > num4)
                                {
                                    message.Message = string.Concat(new object[] { PrjInfoAction.GetProjectNameOfCode(this.ProjectCode.ToString()), "&nbsp;&nbsp;", action.GetTaskName(this.ProjectCode, book.TaskCode), "&nbsp;&nbsp;", row["ResourceName"].ToString(), "消耗比例超过工程完成比例(", decimal.Round(num3 * 100M, 2), "%/", decimal.Round(num4 * 100M, 2), "%)." });
                                    message.Send();
                                }
                            }
                        }
                    }
                }
            }
        }

		protected void BtnDelWork_Click(object sender, EventArgs e)
		{
			string text = string.Empty;
			string[] array = this.HdnTaskList.Value.Trim(new char[]
			{
				','
			}).Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text2 = array2[i];
				if (text2.Trim() != this.HdnTaskCode.Value.Trim())
				{
					text = text + "," + text2.Trim();
				}
			}
			this.tba.DeleteTaskResource(this.ProjectCode.ToString(), this.HdnTaskCode.Value.Trim(), this.TaskBillCode, "edit");
			this.HdnTaskList.Value = text;
			DataTable taskBook = this.tba.GetTaskBook1(this.ProjectCode, this.HdnTaskList.Value.Trim(new char[]
			{
				','
			}), this.TaskBillCode);
			this.DataGrid_Bind(taskBook);
		}
		protected void BtnPickWBS_Click(object sender, EventArgs e)
		{
			DataTable taskBook = this.tba.GetTaskBook1(this.ProjectCode, this.HdnTaskList.Value.Trim(new char[]
			{
				','
			}), this.TaskBillCode);
			this.DataGrid_Bind(taskBook);
		}
	}


