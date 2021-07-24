using ASP;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TaskTreeTable : System.Web.UI.UserControl
	{
		private string[] _ScheduleCodeList;
		private DataTable _DtSchedule = new DataTable();

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
		public string Cols
		{
			get
			{
				object obj = this.ViewState["Cols"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Cols"] = value;
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
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		public void CreateTable()
		{
			this._ScheduleCodeList = this.HdnTaskCodeList.Value.Split(new char[]
			{
				'^'
			});
			this.Init_theSchedule(this.ProjectCode);
			this.tblTask_Create(true);
			string[] array = this.Cols.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < this.tblTask.Rows.Count; j++)
				{
					this.tblTask.Rows[j].Cells[Convert.ToInt32(array[i])].Visible = false;
				}
			}
		}
		private void Init_theSchedule(Guid versionCode)
		{
			this._DtSchedule.Columns.Add(new DataColumn("NoteID", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("TaskCode", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("TaskName", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("ParentTaskCode", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("Quantity", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("QuantityUnit", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("WorkLayer", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("IsHaveChild", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("SynthPrice", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("Cost", typeof(string)));
			WBSBidTaskCollection wBSBidTaskCollection = new WBSBidTaskCollection();
			wBSBidTaskCollection = this.bta.GetAllBidScheduleList(this.ProjectCode, 1);
			for (int i = 0; i < wBSBidTaskCollection.Count; i++)
			{
				DataRow dataRow = this._DtSchedule.NewRow();
				WBSBidTask wBSBidTask = wBSBidTaskCollection[i];
				dataRow["NoteID"] = wBSBidTask.NoteID;
				dataRow["TaskCode"] = wBSBidTask.TaskCode;
				dataRow["TaskName"] = wBSBidTask.TaskName;
				dataRow["Quantity"] = ((wBSBidTask.Quantity == 0m) ? "" : string.Format("{0:f}", wBSBidTask.Quantity));
				dataRow["QuantityUnit"] = wBSBidTask.QuantityUnit;
				dataRow["WorkLayer"] = Convert.ToInt32(wBSBidTask.WorkLayer);
				dataRow["ParentTaskCode"] = wBSBidTask.ParentTaskCode.ToString();
				dataRow["IsHaveChild"] = ((wBSBidTask.ChildNum > 0) ? "true" : "false");
				dataRow["SynthPrice"] = wBSBidTask.SynthPrice.ToString();
				dataRow["Cost"] = wBSBidTask.Cost.ToString();
				this._DtSchedule.Rows.Add(dataRow);
			}
		}
		private void tblTask_Create(bool firstExend)
		{
			DataTable dataTable = new DataTable();
			foreach (DataColumn dataColumn in this._DtSchedule.Columns)
			{
				dataTable.Columns.Add(new DataColumn(dataColumn.ColumnName, dataColumn.DataType));
			}
			dataTable.Columns.Add(new DataColumn("HeadImg", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Display", typeof(string)));
			this.CreateChild(dataTable, "", 0, "", false, firstExend);
			this.tblTask_Draw(dataTable);
		}
		private void tblTask_Draw(DataTable dt)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				DataRow dataRow = dt.Rows[i];
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Text = dataRow["HeadImg"].ToString() + dataRow["TaskName"].ToString().Trim();
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["TaskCode"].ToString();
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = this.WorkLayerName(Convert.ToInt32(dataRow["WorkLayer"]));
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.Text = ((Convert.ToInt32(dataRow["WorkLayer"]) == 3) ? dataRow["Quantity"].ToString() : "&nbsp;");
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = ((Convert.ToInt32(dataRow["WorkLayer"]) == 3) ? dataRow["QuantityUnit"].ToString() : "&nbsp;");
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				if (Convert.ToInt32(dataRow["WorkLayer"]) == 3)
				{
					tableCell.Text = dataRow["SynthPrice"].ToString();
				}
				else
				{
					tableCell.Text = "&nbsp;";
				}
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["Cost"].ToString();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				if (dataRow["ParentTaskCode"].ToString() != "")
				{
					tableRow.Attributes["id"] = dataRow["ParentTaskCode"].ToString();
				}
				tableRow.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.tblTask.ClientID,
					"');clickRow('",
					this.ProjectCode.ToString(),
					"','",
					dataRow["TaskCode"].ToString(),
					"',",
					dataRow["IsHaveChild"].ToString().ToLower(),
					",",
					dataRow["WorkLayer"].ToString(),
					");"
				});
				tableRow.Attributes["onmouseover"] = "doMouseOver(this);";
				tableRow.Attributes["onmouseout"] = "doMouseOut(this);";
				tableRow.Style.Add("display", dataRow["display"].ToString());
				tableRow.CssClass = "grid_row";
				this.tblTask.Rows.Add(tableRow);
			}
		}
		private string WorkLayerName(int WorkLayerID)
		{
			string result = "";
			switch (WorkLayerID)
			{
			case 1:
				result = "单位";
				break;
			case 2:
				result = "分部";
				break;
			case 3:
				result = "分项";
				break;
			case 4:
				result = "子项";
				break;
			}
			return result;
		}
		private void CreateChild(DataTable dt1, string TaskCode, int level, string inherHead, bool isExpend, bool firstExpend)
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
		private string getHeadImg(int currentIndex, int length, DataRow dr, bool isHaveChild, bool firstExpend)
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
							this.HdnTaskCodeList.Value = ((this.HdnTaskCodeList.Value.Trim() == "") ? dr["TaskCode"].ToString() : (this.HdnTaskCodeList.Value.Trim() + "^" + dr["TaskCode"].ToString()));
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
								this.HdnTaskCodeList.Value = ((this.HdnTaskCodeList.Value.Trim() == "") ? dr["TaskCode"].ToString() : (this.HdnTaskCodeList.Value.Trim() + "^" + dr["TaskCode"].ToString()));
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
		private string getInherImg(int currentIndex, int length, string inherHead)
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
	}


