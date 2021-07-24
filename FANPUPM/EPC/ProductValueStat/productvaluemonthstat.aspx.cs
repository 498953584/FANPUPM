using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProductValueMonthStat : PageBase, IRequiresSessionState
	{
		private DataTable _DtSchedule = new DataTable();
		private string[] _ScheduleCodeList;

		protected ProductValueAction ProductValueAct
		{
			get
			{
				return new ProductValueAction();
			}
		}
		protected ConstructWBSTaskAction bta
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
		public DateTime StartDate
		{
			get
			{
				object obj = this.ViewState["STARTDATE"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return DateTime.Now;
			}
			set
			{
				this.ViewState["STARTDATE"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null)
			{
				this.RegisterStartupScript("错误提示", "<script>alert('参数错误！');</script>");
				return;
			}
			this.ProjectCode = new Guid(base.Request["pc"].ToString());
			if (base.Request["pn"] != null)
			{
				this.LblMsg.Text = base.Request["pn"].ToString();
			}
			this._ScheduleCodeList = this.hdnScheduleCodeList.Value.Split(new char[]
			{
				'^'
			});
			this.Init_theSchedule();
			this.tblScheduleView_Create(true);
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void Init_theSchedule()
		{
			this._DtSchedule.Columns.Add(new DataColumn("ParentTaskCode", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("TaskCode", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("TaskName", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("ChildNum", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("WorkLayer", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("PlanQuantity", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("Quantity", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("ReportQuantity", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("SuperQuantity", typeof(string)));
			DataTable productValueStat = this.ProductValueAct.GetProductValueStat(this.ProjectCode);
			for (int i = 0; i < productValueStat.Rows.Count; i++)
			{
				DataRow dataRow = productValueStat.Rows[i];
				DataRow dataRow2 = this._DtSchedule.NewRow();
				dataRow2["ParentTaskCode"] = dataRow["ParentTaskCode"].ToString();
				dataRow2["TaskCode"] = dataRow["TaskCode"].ToString();
				dataRow2["TaskName"] = dataRow["TaskName"].ToString();
				dataRow2["ChildNum"] = ((int.Parse(dataRow["ChildNum"].ToString()) > 0) ? "true" : "false");
				dataRow2["WorkLayer"] = dataRow["WorkLayer"].ToString();
				dataRow2["PlanQuantity"] = dataRow["PlanQuantity"].ToString();
				dataRow2["ReportQuantity"] = dataRow["ReportQuantity"].ToString();
				dataRow2["SuperQuantity"] = dataRow["SuperQuantity"].ToString();
				this._DtSchedule.Rows.Add(dataRow2);
			}
		}
		private void tblScheduleView_Create(bool firstExend)
		{
			DataTable dataTable = new DataTable();
			foreach (DataColumn dataColumn in this._DtSchedule.Columns)
			{
				dataTable.Columns.Add(new DataColumn(dataColumn.ColumnName, dataColumn.DataType));
			}
			dataTable.Columns.Add(new DataColumn("HeadImg", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Display", typeof(string)));
			this.CreateChild(dataTable, "", 0, "", false, firstExend);
			decimal num = 0m;
			decimal num2 = 0m;
			decimal num3 = 0m;
			DataTable totalProductValueStat = this.ProductValueAct.GetTotalProductValueStat(this.ProjectCode);
			if (totalProductValueStat.Rows.Count > 0)
			{
				num = Math.Round((decimal)totalProductValueStat.Rows[0]["PlanOverQuantity"], 2);
				num2 = Math.Round((decimal)totalProductValueStat.Rows[0]["ReportQuantity"], 2);
				num3 = Math.Round((decimal)totalProductValueStat.Rows[0]["SuperQuantity"], 2);
			}
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.Text = base.Request["pn"].ToString();
			tableCell.Height = Unit.Pixel(20);
			tableCell.Wrap = false;
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableCell.Text = num.ToString() + "&nbsp;";
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableCell.Text = num2.ToString() + "&nbsp;";
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableCell.Text = num3.ToString() + "&nbsp;";
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			tableRow.Cells.Add(tableCell);
			tableRow.Attributes["onclick"] = "doClick(this,'" + this.tblScheduleView.ClientID + "',false);";
			tableRow.Attributes["onmouseover"] = "doMouseOver(this);";
			tableRow.Attributes["onmouseout"] = "doMouseOut(this);";
			tableRow.CssClass = "grid_row";
			this.tblScheduleView.Rows.Add(tableRow);
			this.tblScheduleView_Draw(dataTable);
		}
		private void tblScheduleView_Draw(DataTable dt)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				DataRow dataRow = dt.Rows[i];
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Wrap = false;
				tableCell.Text = dataRow["HeadImg"].ToString() + dataRow["TaskName"].ToString();
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Wrap = false;
				tableCell.Text = dataRow["PlanQuantity"].ToString() + "&nbsp;";
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Wrap = false;
				tableCell.Text = dataRow["ReportQuantity"].ToString() + "&nbsp;";
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Wrap = false;
				tableCell.Text = dataRow["SuperQuantity"].ToString() + "&nbsp;";
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableRow.Attributes["onclick"] = "doClick(this,'" + this.tblScheduleView.ClientID + "');";
				tableRow.Attributes["onmouseover"] = "doMouseOver(this);";
				tableRow.Attributes["onmouseout"] = "doMouseOut(this);";
				if (dataRow["ParentTaskCode"].ToString() != "")
				{
					tableRow.Attributes["id"] = dataRow["ParentTaskCode"].ToString();
				}
				tableRow.Style.Add("display", dataRow["display"].ToString());
				tableRow.CssClass = "grid_row";
				this.tblScheduleView.Rows.Add(tableRow);
			}
		}
		private void CreateChild(DataTable dt1, string parentCode, int level, string inherHead, bool isExpend, bool firstExpend)
		{
			DataRow[] array = this._DtSchedule.Select("ParentTaskCode ='" + parentCode + "'");
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
				bool flag2 = dataRow["ChildNum"].ToString().ToLower() == "true";
				dataRow2["HeadImg"] = inherHead + this.getHeadImg(i, num, dataRow, flag2, firstExpend);
				dataRow2["ParentTaskCode"] = parentCode;
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
		private string getHeadImg(int currentIndex, int length, DataRow dr, bool ChildNum, bool firstExpend)
		{
			string result = "";
			bool flag = false;
			if (currentIndex != length - 1)
			{
				if (ChildNum)
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
						result = "<img src=\"../../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
					}
					else
					{
						if (firstExpend)
						{
							result = "<img src=\"../../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
							this.hdnScheduleCodeList.Value = ((this.hdnScheduleCodeList.Value.Trim() == "") ? dr["TaskCode"].ToString() : (this.hdnScheduleCodeList.Value.Trim() + "^" + dr["TaskCode"].ToString()));
						}
						else
						{
							result = "<img src=\"../../../images/tree/tplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
						}
					}
				}
				else
				{
					result = "<img src=\"../../../images/tree/t.gif\" align=\"absmiddle\">";
				}
			}
			else
			{
				if (currentIndex == length - 1)
				{
					if (ChildNum)
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
							result = "<img src=\"../../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
						}
						else
						{
							if (firstExpend)
							{
								result = "<img src=\"../../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
								this.hdnScheduleCodeList.Value = ((this.hdnScheduleCodeList.Value.Trim() == "") ? dr["TaskCode"].ToString() : (this.hdnScheduleCodeList.Value.Trim() + "^" + dr["TaskCode"].ToString()));
							}
							else
							{
								result = "<img src=\"../../../images/tree/lplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
							}
						}
					}
					else
					{
						result = "<img src=\"../../../images/tree/l.gif\" align=\"absmiddle\">";
					}
				}
			}
			return result;
		}
		private string getInherImg(int currentIndex, int length, string inherHead)
		{
			string str;
			if (currentIndex == length - 1)
			{
				str = "<img src=\"../../../images/tree/white.gif\" align=\"absmiddle\">";
			}
			else
			{
				str = "<img src=\"../../../images/tree/i.gif\" align=\"absmiddle\">";
			}
			return inherHead + str;
		}
	}


