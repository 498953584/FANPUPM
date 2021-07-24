using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ScheduleCompleteQuery : PageBase, IRequiresSessionState
	{
		private string[] _ScheduleCodeList;
		private DataTable _DtSchedule = new DataTable();
		protected int templetId;

		public string ProjectName
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
		public Guid ProjectCode
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
		protected ConstructWBSTaskAction bta
		{
			get
			{
				return new ConstructWBSTaskAction();
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
				if (base.Request["pc"] == null || base.Request["pn"] == null || base.Request["PrjGuid"] == null)
				{
					this.js.Text = "alert('参数有误！');";
					return;
				}
				this.hdnPrjGuid.Value = base.Request.QueryString["PrjGuid"];
				this.ProjectCode = new Guid(base.Request["pc"]);
				this.ProjectName = base.Request["pn"].ToString();
				this.lblProjectName.Text = this.ProjectName;
				this.hdnProjectCode.Value = this.ProjectCode.ToString();
				this.hdnProjectName.Value = base.Request["pn"].ToString();
				this.Init_theSchedule(this.ProjectCode);
				this.tblSchedule_Create(true);
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
		private void Init_theSchedule(Guid versionCode)
		{
			this._DtSchedule.Columns.Add(new DataColumn("NoteID", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("TaskCode", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("TaskName", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("ParentTaskCode", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("Quantity", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("CompleteCount", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("SumCompleteQuantity", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("LeaveQuantity", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("IsHaveChild", typeof(string)));
			DataTable scheduleCompleteQuantityList = this.bta.GetScheduleCompleteQuantityList(this.ProjectCode);
			for (int i = 0; i < scheduleCompleteQuantityList.Rows.Count; i++)
			{
				DataRow dataRow = this._DtSchedule.NewRow();
				scheduleCompleteQuantityList.Rows[i]["ChildNum"].ToString();
				dataRow["NoteID"] = scheduleCompleteQuantityList.Rows[i]["NoteID"].ToString();
				dataRow["TaskCode"] = scheduleCompleteQuantityList.Rows[i]["TaskCode"].ToString();
				dataRow["TaskName"] = scheduleCompleteQuantityList.Rows[i]["TaskName"].ToString();
				dataRow["ParentTaskCode"] = scheduleCompleteQuantityList.Rows[i]["ParentTaskCode"].ToString();
				dataRow["Quantity"] = scheduleCompleteQuantityList.Rows[i]["Quantity"].ToString();
				dataRow["CompleteCount"] = decimal.Parse(scheduleCompleteQuantityList.Rows[i]["CompleteCount"].ToString()).ToString("F2");
				dataRow["SumCompleteQuantity"] = decimal.Parse(scheduleCompleteQuantityList.Rows[i]["CompleteCount"].ToString()).ToString("F2");
				dataRow["LeaveQuantity"] = decimal.Subtract(decimal.Parse(scheduleCompleteQuantityList.Rows[i]["Quantity"].ToString()), decimal.Parse(dataRow["SumCompleteQuantity"].ToString())).ToString();
				dataRow["IsHaveChild"] = ((Convert.ToInt32(scheduleCompleteQuantityList.Rows[i]["ChildNum"]) > 0) ? "true" : "false");
				this._DtSchedule.Rows.Add(dataRow);
			}
		}
		private decimal GetParentCompleteCount(string taskCode, decimal quantity, DataTable dtSource)
		{
			decimal num = 0m;
			decimal num2 = 0m;
			foreach (DataRow dataRow in dtSource.Rows)
			{
				string a = dataRow["ParentTaskCode"].ToString();
				if (a == taskCode)
				{
					string a2 = dataRow["ChildNum"].ToString();
					if (a2 == "0")
					{
						num += decimal.Parse(dataRow["SumCompleteQuantity"].ToString());
					}
					else
					{
						num += this.GetParentCompleteCount(dataRow["TaskCode"].ToString(), decimal.Parse(dataRow["Quantity"].ToString()), dtSource);
					}
					num2 += decimal.Parse(dataRow["Quantity"].ToString());
				}
			}
			decimal d = (num2 == 0m) ? 0m : (quantity / num2 * num);
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
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.Text = this.ProjectName;
			tableCell.Height = Unit.Pixel(20);
			tableCell.Wrap = false;
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableCell.Text = "&nbsp;";
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableRow.Attributes["onclick"] = "doClick(this,'" + this.tblSchedule.ClientID + "',false);";
			tableRow.Attributes["onmouseover"] = "doMouseOver(this);";
			tableRow.Attributes["onmouseout"] = "doMouseOut(this);";
			tableRow.CssClass = "grid_row";
			this.tblSchedule.Rows.Add(tableRow);
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
				tableCell.Text = dataRow["HeadImg"].ToString() + ((dataRow["TaskName"].ToString().Trim() == "") ? ".." : dataRow["TaskName"].ToString().Trim());
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				string text = dataRow["Quantity"].ToString();
				tableCell.Text = ((Convert.ToDecimal(text) == 0m) ? string.Empty : text);
				tableCell.Width = Unit.Pixel(70);
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["SumCompleteQuantity"].ToString();
				tableCell.Width = Unit.Pixel(70);
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				try
				{
					double num = Convert.ToDouble(dataRow["Quantity"]);
					if (num > 0.0)
					{
						tableCell.Text = (Convert.ToDouble(dataRow["SumCompleteQuantity"]) / num).ToString("P2");
					}
					else
					{
						tableCell.Text = "0.00%";
					}
				}
				catch
				{
					tableCell.Text = "0.00%";
				}
				tableCell.Width = Unit.Pixel(50);
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["LeaveQuantity"].ToString();
				tableCell.Width = Unit.Pixel(70);
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
						result = "<img src=\"/images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
					}
					else
					{
						if (firstExpend)
						{
							result = "<img src=\"/images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
							this.hdnScheduleCodeList.Value = ((this.hdnScheduleCodeList.Value.Trim() == "") ? dr["TaskCode"].ToString() : (this.hdnScheduleCodeList.Value.Trim() + "^" + dr["TaskCode"].ToString()));
						}
						else
						{
							result = "<img src=\"/images/tree/tplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','t');\">";
						}
					}
				}
				else
				{
					result = "<img src=\"/images/tree/t.gif\" align=\"absmiddle\">";
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
							result = "<img src=\"/images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
						}
						else
						{
							if (firstExpend)
							{
								result = "<img src=\"/images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
								this.hdnScheduleCodeList.Value = ((this.hdnScheduleCodeList.Value.Trim() == "") ? dr["TaskCode"].ToString() : (this.hdnScheduleCodeList.Value.Trim() + "^" + dr["TaskCode"].ToString()));
							}
							else
							{
								result = "<img src=\"/images/tree/lplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TaskCode"].ToString() + "','l');\">";
							}
						}
					}
					else
					{
						result = "<img src=\"/images/tree/l.gif\" align=\"absmiddle\">";
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
				str = "<img src=\"/images/tree/white.gif\" align=\"absmiddle\">";
			}
			else
			{
				str = "<img src=\"/images/tree/i.gif\" align=\"absmiddle\">";
			}
			return inherHead + str;
		}
		protected void btnExp_Click1(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "项目完成量.xls");
		}
		public void ExportToExcel(string FileType, string FileName)
		{
			base.Response.Charset = "GB2312";
			base.Response.ContentEncoding = Encoding.UTF8;
			base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
			base.Response.ContentType = FileType;
			this.EnableViewState = false;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			this.tblSchedule.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
	}


