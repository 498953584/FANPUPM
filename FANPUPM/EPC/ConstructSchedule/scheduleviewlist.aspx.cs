using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ScheduleViewList : PageBase, IRequiresSessionState
	{
		private string[] _ScheduleCodeList;
		private DataTable _DtSchedule = new DataTable();
		private DateTime dtStartDate = DateTime.Now;
		private DateTime dtEndDate = DateTime.Now;

		protected ConstructWBSTaskAction bta
		{
			get
			{
				return new ConstructWBSTaskAction();
			}
		}
		protected string ProjectName
		{
			get
			{
				return (string)this.ViewState["PROJECTNAME"];
			}
			set
			{
				this.ViewState["PROJECTNAME"] = value;
			}
		}
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
		protected string Type
		{
			get
			{
				object obj = this.ViewState["TYPE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "N";
			}
			set
			{
				this.ViewState["TYPE"] = value;
			}
		}
		protected Guid PageCode
		{
			get
			{
				return new Guid("73447941-D2FB-4EFD-B73B-4733E693FB6D");
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			this._ScheduleCodeList = this.hdnScheduleCodeList.Value.Split(new char[]
			{
				'^'
			});
			if (!this.Page.IsPostBack)
			{
				this.DDL_Bind();
				if (base.Request["pc"] == null || base.Request["pn"] == null)
				{
					this.RegisterStartupScript("错误提示", "<script>alert('参数错误！');</script>");
					return;
				}
				this.ProjectCode = new Guid(base.Request["pc"]);
				this.ProjectName = base.Server.UrlDecode(base.Request["pn"].ToString());
				this.hdnProjectCode.Value = this.ProjectCode.ToString();
				this.bta.ClearTempTableData(this.ProjectCode, this.PageCode, this.Session["yhdm"].ToString());
				this.Init_theSchedule(this.ProjectCode);
				this.tblSchedule_Create(true);
				this.BtnSearchPhase.Attributes["onclick"] = string.Concat(new object[]
				{
					"if (!OpenScheduleView('",
					this.ProjectCode,
					"','",
					this.ProjectName,
					"')){return false;}"
				});
			}
		}
		private void Init_theSchedule(Guid versionCode)
		{
			this._DtSchedule.Columns.Add(new DataColumn("TaskCode", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("TaskName", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("ParentTaskCode", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("Quantity", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("QuantityUnit", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("WorkLayer", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("IsHaveChild", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("IsHaveServerChild", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("StartDate", typeof(DateTime)));
			this._DtSchedule.Columns.Add(new DataColumn("EndDate", typeof(DateTime)));
			this._DtSchedule.Columns.Add(new DataColumn("ContractPrice", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("CompleteCount", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("CompleteRate", typeof(string)));
			WBSBidTaskCollection wBSBidTaskCollection = new WBSBidTaskCollection();
			if (this.Type == "N" || this.Type == "S")
			{
				wBSBidTaskCollection = this.bta.GetAllScheduleList(this.Type, this.ProjectCode, this.dtStartDate, this.dtEndDate, this.PageCode, this.Session["yhdm"].ToString(), 1);
			}
			if (this.Type == "T")
			{
				wBSBidTaskCollection = this.bta.GetAllScheduleList(this.ProjectCode, this.TxtTaskCode.Text, this.TxtTaskName.Text, int.Parse(this.DDLProjectType.SelectedValue), this.PageCode, this.Session["yhdm"].ToString(), 1);
			}
			if (this.Type == "P")
			{
				wBSBidTaskCollection = this.bta.GetAllScheduleList(this.ProjectCode, this.PageCode, this.Session["yhdm"].ToString());
			}
			for (int i = 0; i < wBSBidTaskCollection.Count; i++)
			{
				DataRow dataRow = this._DtSchedule.NewRow();
				WBSBidTask wBSBidTask = wBSBidTaskCollection[i];
				dataRow["TaskCode"] = wBSBidTask.TaskCode;
				dataRow["TaskName"] = wBSBidTask.TaskName;
				dataRow["Quantity"] = ((wBSBidTask.Quantity == 0m) ? "" : string.Format("{0:F2}", wBSBidTask.Quantity));
				dataRow["QuantityUnit"] = wBSBidTask.QuantityUnit;
				dataRow["WorkLayer"] = Convert.ToInt32(wBSBidTask.WorkLayer);
				dataRow["ParentTaskCode"] = wBSBidTask.ParentTaskCode.ToString();
				dataRow["IsHaveChild"] = ((wBSBidTask.ChildNum > 0) ? "true" : "false");
				dataRow["IsHaveServerChild"] = "false";
				dataRow["StartDate"] = wBSBidTask.StartDate;
				dataRow["EndDate"] = wBSBidTask.EndDate;
				dataRow["CompleteCount"] = wBSBidTask.CompleteCount.ToString("F2");
				decimal d = Convert.ToDecimal(dataRow["CompleteCount"].ToString());
				dataRow["CompleteRate"] = ((wBSBidTask.Quantity == 0m) ? 0m.ToString("P2") : (d / wBSBidTask.Quantity).ToString("P2"));
				this._DtSchedule.Rows.Add(dataRow);
			}
		}
		protected decimal GetParentCompleteCount(string taskCode, decimal quantity, WBSBidTaskCollection scheduleList)
		{
			decimal num = 0m;
			decimal num2 = 0m;
			foreach (WBSBidTask wBSBidTask in scheduleList)
			{
				if (wBSBidTask.ParentTaskCode == taskCode)
				{
					if (wBSBidTask.ChildNum == 0)
					{
						num += wBSBidTask.CompleteCount;
					}
					else
					{
						num += this.GetParentCompleteCount(wBSBidTask.TaskCode, wBSBidTask.Quantity, scheduleList);
					}
					num2 += wBSBidTask.Quantity;
				}
			}
			decimal d = (num == 0m) ? 0m : (quantity / num2 * num);
			return decimal.Round(d, 2);
		}
		private void tblSchedule_Create(bool firstExend)
		{
			DataTable dataTable = new DataTable();
			foreach (DataColumn dataColumn in this._DtSchedule.Columns)
			{
				dataTable.Columns.Add(new DataColumn(dataColumn.ColumnName, dataColumn.DataType));
			}
			dataTable.Columns.Add(new DataColumn("HeadImg", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Display", typeof(string)));
			this.CreateChild(dataTable, "", 0, "", false, firstExend);
			this.tblSchedule_Draw(dataTable);
		}
		private void tblSchedule_Draw(DataTable dt)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				DataRow dataRow = dt.Rows[i];
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Width = Unit.Pixel(400);
				tableCell.Text = dataRow["HeadImg"].ToString() + ((dataRow["TaskName"].ToString().Trim() == "") ? ".." : dataRow["TaskName"].ToString().Trim());
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["ParentTaskCode"].ToString() + "<font color=\"#ff0000\">" + dataRow["TaskCode"].ToString().Substring(dataRow["ParentTaskCode"].ToString().Length) + "</font>";
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = this.WorkLayerName(Convert.ToInt32(dataRow["WorkLayer"]));
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = Convert.ToDateTime(dataRow["StartDate"]).ToString("yyyy-MM-dd");
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = Convert.ToDateTime(dataRow["EndDate"]).ToString("yyyy-MM-dd");
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["QuantityUnit"].ToString();
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Text = dataRow["Quantity"].ToString();
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["ContractPrice"].ToString();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				try
				{
					tableCell.Text = decimal.Parse(dataRow["CompleteCount"].ToString()).ToString("F2");
				}
				catch
				{
					tableCell.Text = "0";
				}
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				try
				{
					tableCell.Text = dataRow["CompleteRate"].ToString();
				}
				catch
				{
					tableCell.Text = "0";
				}
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				if (dataRow["ParentTaskCode"].ToString() != "")
				{
					tableRow.Attributes["id"] = dataRow["ParentTaskCode"].ToString();
				}
				if (dataRow["IsHaveChild"].ToString().ToLower() == "false")
				{
					tableRow.Attributes["onclick"] = "doClick(this,'" + this.tblSchedule.ClientID + "',true);";
				}
				else
				{
					tableRow.Attributes["onclick"] = "doClick(this,'" + this.tblSchedule.ClientID + "',false);";
				}
				tableRow.Attributes["onmouseover"] = "doMouseOver(this);";
				tableRow.Attributes["onmouseout"] = "doMouseOut(this);";
				tableRow.Style.Add("display", dataRow["display"].ToString());
				tableRow.CssClass = "grid_row";
				this.tblSchedule.Rows.Add(tableRow);
			}
		}
		private string WorkLayerName(int WorkLayerID)
		{
			string result = "";
			switch (WorkLayerID)
			{
			case 1:
				result = "单位工程";
				break;
			case 2:
				result = "分部工程";
				break;
			case 3:
				result = "分项工程";
				break;
			}
			return result;
		}
		public void CreateChild(DataTable dt1, string TaskCode, int level, string inherHead, bool isExpend, bool firstExpend)
		{
			DataRow[] array = this._DtSchedule.Select("ParentTaskCode ='" + TaskCode + "'");
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = false;
				DataRow dataRow = array[i];
				for (int j = 0; j < this._ScheduleCodeList.Length; j++)
				{
					if (this._ScheduleCodeList[j] == dataRow["TaskCode"].ToString())
					{
						flag = true;
						break;
					}
				}
				DataRow dataRow2 = dt1.NewRow();
				dataRow2.ItemArray = dataRow.ItemArray;
				bool flag2 = dataRow["IsHaveChild"].ToString().ToLower() == "true";
				dataRow2["HeadImg"] = inherHead + this.getHeadImg(i, num, dataRow, flag2, firstExpend);
				dataRow2["ParentTaskCode"] = TaskCode;
				dataRow2["IsHaveServerChild"] = (this._DtSchedule.Select("ParentTaskCode = '" + dataRow["TaskCode"].ToString() + "' ").Length > 0).ToString().ToLower();
				if (level == 0)
				{
					dataRow2["display"] = "block";
					isExpend = true;
				}
				else
				{
					if (isExpend)
					{
						dataRow2["display"] = "block";
					}
					else
					{
						if (firstExpend)
						{
							dataRow2["display"] = "block";
						}
						else
						{
							dataRow2["display"] = "none";
						}
					}
				}
				dt1.Rows.Add(dataRow2);
				if (flag2)
				{
					this.CreateChild(dt1, dataRow["TaskCode"].ToString(), level + 1, this.getInherImg(i, num, inherHead), isExpend && flag, firstExpend);
				}
			}
		}
		public string getHeadImg(int currentIndex, int length, DataRow dr, bool isHaveChild, bool firstExpend)
		{
			string result = "";
			bool flag = false;
			if (currentIndex != length - 1)
			{
				if (isHaveChild)
				{
					for (int i = 0; i < this._ScheduleCodeList.Length; i++)
					{
						if (this._ScheduleCodeList[i] == dr["TaskCode"].ToString())
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						result = "<img src=\"../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
					}
					else
					{
						if (firstExpend)
						{
							result = "<img src=\"../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
							this.hdnScheduleCodeList.Value = ((this.hdnScheduleCodeList.Value.Trim() == "") ? dr["TaskCode"].ToString() : (this.hdnScheduleCodeList.Value.Trim() + "^" + dr["TaskCode"].ToString()));
						}
						else
						{
							result = "<img src=\"../../images/tree/tplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
						}
					}
				}
				else
				{
					result = "<img src=\"../../images/tree/t.gif\" align=\"absmiddle\">";
				}
			}
			else
			{
				if (currentIndex == length - 1)
				{
					if (isHaveChild)
					{
						for (int j = 0; j < this._ScheduleCodeList.Length; j++)
						{
							if (this._ScheduleCodeList[j] == dr["TaskCode"].ToString())
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							result = "<img src=\"../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
						}
						else
						{
							if (firstExpend)
							{
								result = "<img src=\"../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
								this.hdnScheduleCodeList.Value = ((this.hdnScheduleCodeList.Value.Trim() == "") ? dr["TaskCode"].ToString() : (this.hdnScheduleCodeList.Value.Trim() + "^" + dr["TaskCode"].ToString()));
							}
							else
							{
								result = "<img src=\"../../images/tree/lplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
							}
						}
					}
					else
					{
						result = "<img src=\"../../images/tree/l.gif\" align=\"absmiddle\">";
					}
				}
			}
			return result;
		}
		public string getInherImg(int currentIndex, int length, string inherHead)
		{
			string str;
			if (currentIndex == length - 1)
			{
				str = "<img src=\"../../images/tree/white.gif\" align=\"absmiddle\">";
			}
			else
			{
				str = "<img src=\"../../images/tree/i.gif\" align=\"absmiddle\">";
			}
			return inherHead + str;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
			this.Init_Click();
		}
		private void InitializeComponent()
		{
		}
		private void Init_Click()
		{
			this.BtnSearchPhase.Click += new EventHandler(this.BtnSearchPhase_Click);
			this.BtnSearchCTT.Click += new EventHandler(this.BtnSearchCTT_Click);
			this.RBtnDate.SelectedIndexChanged += new EventHandler(this.RBtnDate_SelectedIndexChanged);
			this.BtnSearchDate.Click += new EventHandler(this.BtnSearchDate_Click);
		}
		private void DDL_Bind()
		{
			string selectedValue;
			if ((selectedValue = this.RBtnDate.SelectedValue) != null)
			{
				if (selectedValue == "1")
				{
					this.InsertIntoYear();
					return;
				}
				if (selectedValue == "2")
				{
					this.InsertIntoQuarter();
					return;
				}
				if (selectedValue == "3")
				{
					this.InsertIntoMonth();
					return;
				}
				if (selectedValue == "4")
				{
					this.InsertIntoWeek();
					return;
				}
			}
			this.RBtnDate.SelectedValue = "1";
			this.InsertIntoYear();
		}
		private void InsertIntoYear()
		{
			this.DDLStartDate.Items.Clear();
			for (int i = DateTime.Now.Year - 9; i <= DateTime.Now.Year; i++)
			{
				this.DDLStartDate.Items.Add(new ListItem(i.ToString(), i.ToString()));
			}
			this.DDLEndDate.Items.Clear();
			for (int j = DateTime.Now.Year - 9; j <= DateTime.Now.Year; j++)
			{
				this.DDLEndDate.Items.Add(new ListItem(j.ToString(), j.ToString()));
			}
			try
			{
				this.DDLStartDate.SelectedValue = DateTime.Now.Year.ToString();
				this.DDLEndDate.SelectedValue = DateTime.Now.Year.ToString();
			}
			catch
			{
			}
		}
		private void InsertIntoQuarter()
		{
			this.DDLStartDate.Items.Clear();
			for (int i = DateTime.Now.Year - 9; i <= DateTime.Now.Year; i++)
			{
				for (int j = 1; j <= 4; j++)
				{
					this.DDLStartDate.Items.Add(new ListItem(i.ToString() + "-" + j.ToString(), i.ToString() + "-" + j.ToString()));
				}
			}
			this.DDLEndDate.Items.Clear();
			for (int k = DateTime.Now.Year - 9; k <= DateTime.Now.Year; k++)
			{
				for (int l = 1; l <= 4; l++)
				{
					this.DDLEndDate.Items.Add(new ListItem(k.ToString() + "-" + l.ToString(), k.ToString() + "-" + l.ToString()));
				}
			}
			try
			{
				int num = 0;
				switch (DateTime.Now.Month)
				{
				case 1:
				case 2:
				case 3:
					num = 1;
					break;
				case 4:
				case 5:
				case 6:
					num = 2;
					break;
				case 7:
				case 8:
				case 9:
					num = 3;
					break;
				case 10:
				case 11:
				case 12:
					num = 4;
					break;
				}
				this.DDLStartDate.SelectedValue = DateTime.Now.Year.ToString() + "-" + num.ToString();
				this.DDLEndDate.SelectedValue = DateTime.Now.Year.ToString() + "-" + num.ToString();
			}
			catch
			{
			}
		}
		private void InsertIntoMonth()
		{
			this.DDLStartDate.Items.Clear();
			for (int i = DateTime.Now.Year - 9; i <= DateTime.Now.Year; i++)
			{
				for (int j = 1; j <= 12; j++)
				{
					this.DDLStartDate.Items.Add(new ListItem(i.ToString() + "-" + ((j < 10) ? ("0" + j.ToString()) : j.ToString()), i.ToString() + "-" + ((j < 10) ? ("0" + j.ToString()) : j.ToString())));
				}
			}
			this.DDLEndDate.Items.Clear();
			for (int k = DateTime.Now.Year - 9; k <= DateTime.Now.Year; k++)
			{
				for (int l = 1; l <= 12; l++)
				{
					this.DDLEndDate.Items.Add(new ListItem(k.ToString() + "-" + ((l < 10) ? ("0" + l.ToString()) : l.ToString()), k.ToString() + "-" + ((l < 10) ? ("0" + l.ToString()) : l.ToString())));
				}
			}
			try
			{
				this.DDLStartDate.SelectedValue = DateTime.Now.Year.ToString() + "-" + ((DateTime.Now.Month < 10) ? ("0" + DateTime.Now.Month.ToString()) : DateTime.Now.Month.ToString());
				this.DDLEndDate.SelectedValue = DateTime.Now.Year.ToString() + "-" + ((DateTime.Now.Month < 10) ? ("0" + DateTime.Now.Month.ToString()) : DateTime.Now.Month.ToString());
			}
			catch
			{
			}
		}
		private void InsertIntoWeek()
		{
			int dayOfYear = Convert.ToDateTime(DateTime.Now.AddYears(1).Year.ToString() + "-1-1").AddDays(-1.0).DayOfYear;
			this.DDLStartDate.Items.Clear();
			for (int i = DateTime.Now.Year - 9; i <= DateTime.Now.Year; i++)
			{
				for (int j = 1; j <= dayOfYear / 7; j++)
				{
					this.DDLStartDate.Items.Add(new ListItem(i.ToString() + "-" + ((j < 10) ? ("0" + j.ToString()) : j.ToString()), i.ToString() + "-" + ((j < 10) ? ("0" + j.ToString()) : j.ToString())));
				}
			}
			this.DDLEndDate.Items.Clear();
			for (int k = DateTime.Now.Year - 9; k <= DateTime.Now.Year; k++)
			{
				for (int l = 1; l <= dayOfYear / 7; l++)
				{
					this.DDLEndDate.Items.Add(new ListItem(k.ToString() + "-" + ((l < 10) ? ("0" + l.ToString()) : l.ToString()), k.ToString() + "-" + ((l < 10) ? ("0" + l.ToString()) : l.ToString())));
				}
			}
			try
			{
				int num = this.DatePart(DateTime.Now);
				this.DDLStartDate.SelectedValue = DateTime.Now.Year.ToString() + "-" + ((num < 10) ? ("0" + num.ToString()) : num.ToString());
				this.DDLEndDate.SelectedValue = DateTime.Now.Year.ToString() + "-" + ((num < 10) ? ("0" + num.ToString()) : num.ToString());
			}
			catch
			{
			}
		}
		public int DatePart(DateTime dt)
		{
			int num = Convert.ToInt32(dt.DayOfWeek);
			int num2 = -1 * (num + 1);
			int dayOfYear = DateTime.Now.AddDays((double)num2).DayOfYear;
			int num3 = dayOfYear / 7;
			if (dayOfYear % 7 != 0)
			{
				num3++;
			}
			return num3 + 1;
		}
		public string WeekToStartDate(int intYear, int intWeek)
		{
			DateTime dateTime = new DateTime(intYear, 1, 1);
			DateTime dateTime2 = dateTime.AddDays((double)((intWeek - 1) * 7));
			int num = (int)(DayOfWeek.Monday - dateTime2.DayOfWeek);
			return dateTime2.AddDays((double)num).ToShortDateString();
		}
		public string WeekToEndDate(int intYear, int intWeek)
		{
			return Convert.ToDateTime(this.WeekToStartDate(intYear, intWeek)).AddDays(6.0).ToShortDateString();
		}
		private void GetDateTime(string type, out DateTime dtStartDate, out DateTime dtEndDate)
		{
			dtStartDate = DateTime.Now;
			dtEndDate = DateTime.Now;
			if (type != null)
			{
				if (type == "1")
				{
					dtStartDate = Convert.ToDateTime(this.DDLStartDate.SelectedValue + "-1-1");
					dtEndDate = Convert.ToDateTime(this.DDLEndDate.SelectedValue + "-1-1").AddYears(1).AddDays(-1.0);
					return;
				}
				if (!(type == "2"))
				{
					if (type == "3")
					{
						dtStartDate = Convert.ToDateTime(this.DDLStartDate.SelectedValue + "-1");
						dtEndDate = Convert.ToDateTime(this.DDLEndDate.SelectedValue + "-1").AddMonths(1).AddDays(-1.0);
						return;
					}
					if (!(type == "4"))
					{
						return;
					}
					int intYear = int.Parse(this.DDLStartDate.SelectedValue.Substring(0, 4));
					int intWeek = int.Parse(this.DDLStartDate.SelectedValue.Substring(5, 2));
					dtStartDate = Convert.ToDateTime(this.WeekToStartDate(intYear, intWeek));
					int intYear2 = int.Parse(this.DDLEndDate.SelectedValue.Substring(0, 4));
					int intWeek2 = int.Parse(this.DDLEndDate.SelectedValue.Substring(5, 2));
					dtEndDate = Convert.ToDateTime(this.WeekToEndDate(intYear2, intWeek2));
				}
				else
				{
					string text = this.DDLStartDate.SelectedValue.Substring(5, 1);
					string text2 = this.DDLEndDate.SelectedValue.Substring(5, 1);
					string a;
					if ((a = text) != null)
					{
						if (!(a == "1"))
						{
							if (!(a == "2"))
							{
								if (!(a == "3"))
								{
									if (a == "4")
									{
										dtStartDate = Convert.ToDateTime(this.DDLStartDate.SelectedValue.Substring(0, 4) + "-10-1");
									}
								}
								else
								{
									dtStartDate = Convert.ToDateTime(this.DDLStartDate.SelectedValue.Substring(0, 4) + "-7-1");
								}
							}
							else
							{
								dtStartDate = Convert.ToDateTime(this.DDLStartDate.SelectedValue.Substring(0, 4) + "-4-1");
							}
						}
						else
						{
							dtStartDate = Convert.ToDateTime(this.DDLStartDate.SelectedValue.Substring(0, 4) + "-1-1");
						}
					}
					string a2;
					if ((a2 = text2) != null)
					{
						if (a2 == "1")
						{
							dtEndDate = Convert.ToDateTime(this.DDLEndDate.SelectedValue.Substring(0, 4) + "-3-1").AddMonths(1).AddDays(-1.0);
							return;
						}
						if (a2 == "2")
						{
							dtEndDate = Convert.ToDateTime(this.DDLEndDate.SelectedValue.Substring(0, 4) + "-6-1").AddMonths(1).AddDays(-1.0);
							return;
						}
						if (a2 == "3")
						{
							dtEndDate = Convert.ToDateTime(this.DDLEndDate.SelectedValue.Substring(0, 4) + "-9-1").AddMonths(1).AddDays(-1.0);
							return;
						}
						if (!(a2 == "4"))
						{
							return;
						}
						dtEndDate = Convert.ToDateTime(this.DDLEndDate.SelectedValue.Substring(0, 4) + "-12-1").AddMonths(1).AddDays(-1.0);
						return;
					}
				}
			}
		}
		private void RBtnDate_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.DDL_Bind();
			this.bta.ClearTempTableData(this.ProjectCode, this.PageCode, this.Session["yhdm"].ToString());
			this.GetDateTime(this.RBtnDate.SelectedValue, out this.dtStartDate, out this.dtEndDate);
			this.Type = "S";
			this.Init_theSchedule(this.ProjectCode);
			this.tblSchedule_Create(true);
		}
		private void BtnSearchDate_Click(object sender, EventArgs e)
		{
			this.bta.ClearTempTableData(this.ProjectCode, this.PageCode, this.Session["yhdm"].ToString());
			this.GetDateTime(this.RBtnDate.SelectedValue, out this.dtStartDate, out this.dtEndDate);
			this.Type = "S";
			this.Init_theSchedule(this.ProjectCode);
			this.tblSchedule_Create(true);
		}
		private void BtnSearchCTT_Click(object sender, EventArgs e)
		{
			this._DtSchedule.Clear();
			this.bta.ClearTempTableData(this.ProjectCode, this.PageCode, this.Session["yhdm"].ToString());
			this.GetDateTime(this.RBtnDate.SelectedValue, out this.dtStartDate, out this.dtEndDate);
			this.Type = "T";
			this.Init_theSchedule(this.ProjectCode);
			this.tblSchedule_Create(true);
		}
		private void BtnSearchPhase_Click(object sender, EventArgs e)
		{
			this.Type = "P";
			this.Init_theSchedule(this.ProjectCode);
			this.tblSchedule_Create(true);
		}
	}


