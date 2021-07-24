using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProjectGSQuery : PageBase, IRequiresSessionState
	{
		private string[] _ScheduleCodeList;
		private DataTable _DtSchedule = new DataTable();
		protected int templetId;
		public string jw = string.Empty;

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
		private void Init_theSchedule(Guid versionCode)
		{
			this._DtSchedule.Reset();
			this._DtSchedule.Columns.Add(new DataColumn("TaskCode", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("TaskName", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("ParentTaskCode", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
			this._DtSchedule.Columns.Add(new DataColumn("SumCompleteQuantity", typeof(decimal)));
			this._DtSchedule.Columns.Add(new DataColumn("total", typeof(decimal)));
			this._DtSchedule.Columns.Add(new DataColumn("cctotal", typeof(decimal)));
			this._DtSchedule.Columns.Add(new DataColumn("IsHaveChild", typeof(string)));
			ArrayList allTemp = TemplatesAction.getAllTemp();
			for (int i = 0; i < allTemp.Count; i++)
			{
				DataRow dataRow = this._DtSchedule.NewRow();
				Templates templates = (Templates)allTemp[i];
				dataRow["TaskCode"] = templates.TemplatesCode;
				dataRow["TaskName"] = templates.TemplatesName;
				dataRow["ParentTaskCode"] = templates.ParentCode;
				dataRow["Quantity"] = BindBudgetAction.getTotalQuantity(this.hdnPrjGuid.Value, templates.TemplatesCode);
				dataRow["SumCompleteQuantity"] = BindBudgetAction.gettotalSumCompleteQuantity(this.hdnPrjGuid.Value, templates.TemplatesCode);
				dataRow["total"] = BindBudgetAction.getAlltotal(this.hdnPrjGuid.Value, templates.TemplatesCode);
				dataRow["cctotal"] = BindBudgetAction.getAllccTotal(this.hdnPrjGuid.Value, templates.TemplatesCode);
				this._DtSchedule.Rows.Add(dataRow);
			}
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
			new ConstructWBSTaskAction();
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
				tableCell.Text = dataRow["ParentTaskCode"].ToString() + "<font color=\"#ff0000\">" + dataRow["TaskCode"].ToString().Substring(dataRow["ParentTaskCode"].ToString().Length) + "</font>";
				tableCell.Width = Unit.Pixel(70);
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Text = dataRow["Quantity"].ToString();
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Text = dataRow["SumCompleteQuantity"].ToString();
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Text = dataRow["total"].ToString();
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Text = dataRow["cctotal"].ToString();
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				if (!string.IsNullOrEmpty(dataRow["SumCompleteQuantity"].ToString()) && !string.IsNullOrEmpty(dataRow["Quantity"].ToString()) && dataRow["SumCompleteQuantity"].ToString() != "0.00")
				{
					tableCell.Text = (Math.Round(Convert.ToDecimal(dataRow["SumCompleteQuantity"].ToString()) / Convert.ToDecimal(dataRow["Quantity"].ToString()), 2) * 100m).ToString() + "%";
				}
				else
				{
					tableCell.Text = "0%";
				}
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				if (dataRow["ParentTaskCode"].ToString() != "")
				{
					tableRow.Attributes["id"] = dataRow["ParentTaskCode"].ToString();
				}
				tableRow.Attributes["onclick"] = "doClick(this,'" + this.tblSchedule.ClientID + "',true);";
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
				bool flag2 = this._DtSchedule.Select("ParentTaskCode ='" + dataRow["TaskCode"].ToString() + "'").Length > 0;
				dataRow["IsHaveChild"] = flag2.ToString();
				DataRow dataRow2 = dt1.NewRow();
				dataRow2.ItemArray = dataRow.ItemArray;
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
				else
				{
					level++;
					DataTable newsreturnTable = BindBudgetAction.getNewsreturnTable(this.hdnPrjGuid.Value, dataRow["TaskCode"].ToString());
					DataRow[] array2 = newsreturnTable.Select("1=1");
					for (int k = 0; k < array2.Length; k++)
					{
						dataRow = array2[k];
						dataRow2 = dt1.NewRow();
						dataRow2.ItemArray = dataRow.ItemArray;
						dataRow2["HeadImg"] = inherHead + "<img src=\"../../../images/tree/i.gif\" align=\"absmiddle\">" + this.getHeadImg(i + 1, num + 1, dataRow, false, firstExpend);
						dataRow2["ParentTaskCode"] = TaskCode;
						dataRow2["display"] = "block";
						dt1.Rows.Add(dataRow2);
					}
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
		public string getInherImg(int currentIndex, int length, string inherHead)
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
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


