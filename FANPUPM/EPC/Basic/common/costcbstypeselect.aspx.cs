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
	public partial class CostcbsTypeSelect : PageBase, IRequiresSessionState
	{
		protected string[] m_WorkCodeList;
		protected DataTable dtWork = new DataTable();

		protected CBSAction theCBS
		{
			get
			{
				return new CBSAction();
			}
		}
		protected DateTime StartDate
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
			this.m_WorkCodeList = this.hdnWorkCodeList.Value.Split(new char[]
			{
				'^'
			});
			if (!this.Page.IsPostBack)
			{
				this.Init_theWork();
				this.tblWork_Create(true);
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
		private void Init_theWork()
		{
			this.dtWork.Columns.Add(new DataColumn("ParentWorkCode", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("WorkCode", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("WorkName", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("WorkType", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("WorkRemark", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("IsHaveChild", typeof(string)));
			ArrayList allCBSApproveList = this.theCBS.GetAllCBSApproveList2();
			for (int i = 0; i < allCBSApproveList.Count; i++)
			{
				DataRow dataRow = this.dtWork.NewRow();
				CBSFeeNodeInfo cBSFeeNodeInfo = (CBSFeeNodeInfo)allCBSApproveList[i];
				dataRow["ParentWorkCode"] = cBSFeeNodeInfo.PNode;
				dataRow["WorkCode"] = cBSFeeNodeInfo.NodeCode;
				dataRow["WorkName"] = cBSFeeNodeInfo.NodeName;
				dataRow["WorkType"] = ((cBSFeeNodeInfo.CostType == "1") ? "成本核算" : ((cBSFeeNodeInfo.CostType == "2") ? "其它成本" : ""));
				dataRow["WorkRemark"] = cBSFeeNodeInfo.Remark;
				this.dtWork.Rows.Add(dataRow);
			}
		}
		private void tblWork_Create(bool firstExend)
		{
			DataTable dataTable = new DataTable();
			foreach (DataColumn dataColumn in this.dtWork.Columns)
			{
				dataTable.Columns.Add(new DataColumn(dataColumn.ColumnName, dataColumn.DataType));
			}
			dataTable.Columns.Add(new DataColumn("HeadImg", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Display", typeof(string)));
			this.CreateChild(dataTable, "", 0, "", false, firstExend);
			this.tblWork_Draw(dataTable);
		}
		private void tblWork_Draw(DataTable dt)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				DataRow dataRow = dt.Rows[i];
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Text = dataRow["HeadImg"].ToString() + dataRow["WorkName"].ToString();
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["WorkCode"].ToString();
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["WorkType"].ToString();
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["WorkRemark"].ToString();
				tableRow.Cells.Add(tableCell);
				if (dataRow["ParentWorkCode"].ToString() != "")
				{
					tableRow.Attributes["id"] = dataRow["ParentWorkCode"].ToString();
				}
				tableRow.Attributes["onclick"] = string.Concat(new string[]
				{
					"clickRow(this,'",
					dataRow["WorkCode"].ToString(),
					"','",
					dataRow["IsHaveChild"].ToString(),
					"','",
					dataRow["WorkType"].ToString(),
					"','",
					dataRow["WorkName"].ToString().ToLower(),
					"');"
				});
				tableRow.Attributes["onmouseover"] = "overRow(this);";
				tableRow.Attributes["onmouseout"] = "outRow(this);";
				tableRow.Style.Add("display", dataRow["display"].ToString());
				tableRow.Height = Unit.Pixel(20);
				this.tblWork.Rows.Add(tableRow);
			}
			this.tblWork.Rows[0].Style.Add("position", "relative");
			this.tblWork.Rows[0].Style.Add("top", "expression(this.offsetParent.scrollTop-2)");
		}
		public void CreateChild(DataTable dt1, string parentCode, int level, string inherHead, bool isExpend, bool firstExpend)
		{
			DataRow[] array = this.dtWork.Select("ParentWorkCode ='" + parentCode + "'");
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = false;
				DataRow dataRow = array[i];
				for (int j = 0; j < this.m_WorkCodeList.Length; j++)
				{
					if (this.m_WorkCodeList[j] == dataRow["WorkCode"].ToString())
					{
						flag = true;
						break;
					}
				}
				bool flag2 = this.dtWork.Select("ParentWorkCode ='" + dataRow["WorkCode"].ToString() + "'").Length > 0;
				dataRow["IsHaveChild"] = flag2.ToString();
				DataRow dataRow2 = dt1.NewRow();
				dataRow2.ItemArray = dataRow.ItemArray;
				dataRow2["HeadImg"] = inherHead + this.getHeadImg(i, num, dataRow, flag2, firstExpend);
				dataRow2["ParentWorkCode"] = parentCode;
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
					this.CreateChild(dt1, dataRow["WorkCode"].ToString(), level + 1, this.getInherImg(i, num, inherHead), isExpend && flag, firstExpend);
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
					for (int i = 0; i < this.m_WorkCodeList.Length; i++)
					{
						if (this.m_WorkCodeList[i] == dr["WorkCode"].ToString())
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						result = "<img src=\"../../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["WorkCode"].ToString() + "','t');\">";
					}
					else
					{
						if (firstExpend)
						{
							result = "<img src=\"../../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["WorkCode"].ToString() + "','t');\">";
							this.hdnWorkCodeList.Value = ((this.hdnWorkCodeList.Value.Trim() == "") ? "" : (this.hdnWorkCodeList.Value + "^")) + dr["WorkCode"].ToString();
						}
						else
						{
							result = "<img src=\"../../../images/tree/tplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["WorkCode"].ToString() + "','t');\">";
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
						for (int j = 0; j < this.m_WorkCodeList.Length; j++)
						{
							if (this.m_WorkCodeList[j] == dr["WorkCode"].ToString())
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							result = "<img src=\"../../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["WorkCode"].ToString() + "','l');\">";
						}
						else
						{
							if (firstExpend)
							{
								result = "<img src=\"../../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["WorkCode"].ToString() + "','l');\">";
								this.hdnWorkCodeList.Value = ((this.hdnWorkCodeList.Value.Trim() == "") ? "" : (this.hdnWorkCodeList.Value + "^")) + dr["WorkCode"].ToString();
							}
							else
							{
								result = "<img src=\"../../../images/tree/lplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["WorkCode"].ToString() + "','l');\">";
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
	}


