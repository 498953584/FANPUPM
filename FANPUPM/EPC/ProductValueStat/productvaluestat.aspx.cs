using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProductValueStat : PageBase, IRequiresSessionState
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
		public bool IsLoad
		{
			get
			{
				object obj = this.ViewState["ISLOAD"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ISLOAD"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null || base.Request["t"] == null || base.Request["m"] == null)
			{
				this.RegisterStartupScript("错误提示", "<script>alert('参数错误！');</script>");
				return;
			}
			this.ProjectCode = new Guid(base.Request["pc"].ToString());
			this.StartDate = Convert.ToDateTime(base.Request["m"].ToString());
			base.DealType = (OpType)Enum.Parse(typeof(OpType), base.Request["t"].ToString());
			if (base.DealType == OpType.Other)
			{
				this.BtnSave.Enabled = false;
			}
			if (!this.Page.IsPostBack)
			{
				this.ProductValueAct.updateProductValueDetail(this.ProjectCode, this.StartDate);
			}
			this._ScheduleCodeList = this.hdnScheduleCodeList.Value.Split(new char[]
			{
				'^'
			});
			this.Init_theSchedule();
			this.tblScheduleView_Create(true);
			this.BtnExit.Attributes["onclick"] = "javascript:return OpenProduceValue('" + this.ProjectCode + "');";
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
			this._DtSchedule.Columns.Add(new DataColumn("Unit", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("Quantity", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("MonthOverQuantity", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("AccumulativeQuantity", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("SynthPrice", typeof(string)));
			this._DtSchedule.Columns.Add(new DataColumn("ProductValue", typeof(string)));
			ArrayList arrayList = new ArrayList();
			arrayList = this.ProductValueAct.ProductValueMonthStatWeave(this.ProjectCode, this.StartDate);
			for (int i = 0; i < arrayList.Count; i++)
			{
				DataRow dataRow = this._DtSchedule.NewRow();
				MonthProductValueDetail monthProductValueDetail = (MonthProductValueDetail)arrayList[i];
				dataRow["ParentTaskCode"] = monthProductValueDetail.TaskList.ParentTaskCode;
				dataRow["TaskCode"] = monthProductValueDetail.TaskList.TaskCode;
				dataRow["TaskName"] = monthProductValueDetail.TaskList.TaskName;
				dataRow["ChildNum"] = ((monthProductValueDetail.TaskList.ChildNum > 0) ? "true" : "false");
				dataRow["WorkLayer"] = monthProductValueDetail.TaskList.WorkLayer.ToString();
				dataRow["Unit"] = monthProductValueDetail.Unit;
				dataRow["Quantity"] = monthProductValueDetail.Quantity.ToString();
				dataRow["MonthOverQuantity"] = monthProductValueDetail.MonthOverQuantity.ToString();
				dataRow["AccumulativeQuantity"] = monthProductValueDetail.AccumulativeQuantity.ToString();
				dataRow["SynthPrice"] = monthProductValueDetail.SynthPrice.ToString();
				dataRow["ProductValue"] = monthProductValueDetail.ProductValue.ToString();
				this._DtSchedule.Rows.Add(dataRow);
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
				tableCell.Text = dataRow["HeadImg"].ToString() + dataRow["TaskName"].ToString();
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				if (dataRow["WorkLayer"].ToString() == "3")
				{
					tableCell.Text = dataRow["Unit"].ToString();
				}
				else
				{
					tableCell.Text = "";
				}
				tableCell.Wrap = false;
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				if (dataRow["WorkLayer"].ToString() == "3")
				{
					tableCell.Text = dataRow["Quantity"].ToString();
				}
				else
				{
					tableCell.Text = "";
				}
				tableCell.Wrap = false;
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				if (int.Parse(dataRow["WorkLayer"].ToString()) == 3)
				{
					TextBox textBox = new TextBox();
					textBox.ID = "txtMonthQuantity" + dataRow["TaskCode"].ToString();
					textBox.Text = dataRow["MonthOverQuantity"].ToString();
					textBox.Attributes["onkeydown"] = "event.returnValue=CheckInputIsFloat(event.keyCode,this)";
					textBox.Width = Unit.Pixel(70);
					tableCell.Controls.Add(textBox);
				}
				else
				{
					tableCell.Text = "";
				}
				tableCell.Wrap = false;
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				if (dataRow["WorkLayer"].ToString() == "3")
				{
					tableCell.Text = this.ProductValueAct.AccumulativeSum(this.ProjectCode, dataRow["TaskCode"].ToString(), "S");
				}
				else
				{
					tableCell.Text = "";
				}
				tableCell.Wrap = false;
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				if (dataRow["WorkLayer"].ToString() == "3")
				{
					tableCell.Text = dataRow["SynthPrice"].ToString();
				}
				else
				{
					tableCell.Text = "";
				}
				tableCell.Wrap = false;
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				if (dataRow["WorkLayer"].ToString() == "3")
				{
					tableCell.Text = Convert.ToDecimal(dataRow["ProductValue"].ToString()).ToString();
				}
				else
				{
					tableCell.Text = "";
				}
				tableCell.Wrap = false;
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["WorkLayer"].ToString();
				tableCell.Visible = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["TaskCode"].ToString();
				tableCell.Visible = false;
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
		private ArrayList GetTable_Data()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 1; i < this.tblScheduleView.Rows.Count; i++)
			{
				MonthProductValueDetail monthProductValueDetail = new MonthProductValueDetail();
				monthProductValueDetail.TaskList.ProjectCode = this.ProjectCode;
				monthProductValueDetail.TaskList.TaskCode = this.tblScheduleView.Rows[i].Cells[8].Text;
				monthProductValueDetail.StatDate = DateTime.Now;
				monthProductValueDetail.Unit = this.tblScheduleView.Rows[i].Cells[1].Text;
				if (this.tblScheduleView.Rows[i].Cells[7].Text == "3")
				{
					monthProductValueDetail.Quantity = Convert.ToDecimal(this.tblScheduleView.Rows[i].Cells[2].Text);
					monthProductValueDetail.MonthOverQuantity = Convert.ToDecimal(((TextBox)this.tblScheduleView.Rows[i].Cells[3].Controls[0]).Text);
					monthProductValueDetail.AccumulativeQuantity = Convert.ToDecimal(this.tblScheduleView.Rows[i].Cells[4].Text);
					monthProductValueDetail.SynthPrice = Convert.ToDecimal(this.tblScheduleView.Rows[i].Cells[5].Text);
					monthProductValueDetail.ProductValue = Convert.ToDecimal(((TextBox)this.tblScheduleView.Rows[i].Cells[3].Controls[0]).Text) * Convert.ToDecimal(this.tblScheduleView.Rows[i].Cells[5].Text);
				}
				else
				{
					monthProductValueDetail.Quantity = 0m;
					monthProductValueDetail.MonthOverQuantity = 0m;
					monthProductValueDetail.AccumulativeQuantity = 0m;
					monthProductValueDetail.SynthPrice = 0m;
					monthProductValueDetail.ProductValue = 0m;
				}
				arrayList.Add(monthProductValueDetail);
			}
			return arrayList;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			ArrayList table_Data = this.GetTable_Data();
			if (this.ProductValueAct.AddProductValueMonthStat(table_Data, this.ProjectCode, this.StartDate) == 1)
			{
				this.RegisterStartupScript("", "<script>alert('保存成功！');</script>");
				return;
			}
			this.RegisterStartupScript("", "<script>alert('保存失败！');</script>");
		}
	}


