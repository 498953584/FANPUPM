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
	public partial class ProjectGSQuery : PageBase, IRequiresSessionState
	{
		protected string[] m_TempCodeList;
		protected DataTable dtWork = new DataTable();
		public string prjguid = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request.QueryString["PrjGuid"] != null)
			{
				this.prjguid = base.Request.QueryString["PrjGuid"].ToString();
			}
			this.m_TempCodeList = this.hdnScheduleCodeList.Value.Split(new char[]
			{
				'^'
			});
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["type"].ToString().Equals("manage"))
				{
					this.btnBind.Style.Add("display", "none");
					this.bottom.Style.Add("display", "none");
				}
				else
				{
					this.btnAdd.Style.Add("display", "none");
					this.btnDel.Style.Add("display", "none");
					this.btnUpd.Style.Add("display", "none");
				}
				this.Init_theWork();
				this.tblWork_Create(true);
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('系统提示\\n\\n      删除此节点将会同时删除此节点下的预算！确定删除？')){return false;} ";
				this.btnAdd.Attributes["onclick"] = "if (!openCBSEdit('add')){return false;}";
				this.btnUpd.Attributes["onclick"] = "if (!openCBSEdit('upd')){return false;}";
				this.btnBind.Attributes["onclick"] = "if (!openCBSEdit('bind')){return false;}";
			}
		}
		private void Init_theWork()
		{
			this.dtWork.Columns.Add(new DataColumn("ParentCode", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("TempCode", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("TempName", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("IsHaveChild", typeof(string)));
			ArrayList allTemp = TemplatesAction.getAllTemp();
			for (int i = 0; i < allTemp.Count; i++)
			{
				DataRow dataRow = this.dtWork.NewRow();
				Templates templates = (Templates)allTemp[i];
				dataRow["ParentCode"] = templates.ParentCode;
				dataRow["TempCode"] = templates.TemplatesCode;
				dataRow["TempName"] = templates.TemplatesName;
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
				tableCell.Text = dataRow["HeadImg"].ToString() + dataRow["TempName"].ToString();
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["TempCode"].ToString();
				tableRow.Cells.Add(tableCell);
				if (dataRow["ParentCode"].ToString() != "")
				{
					tableRow.Attributes["id"] = dataRow["ParentCode"].ToString();
				}
				if (base.Request.QueryString["type"].Equals("manage"))
				{
					tableRow.Attributes["onclick"] = string.Concat(new string[]
					{
						"clickRow(this,'",
						dataRow["TempCode"].ToString(),
						"','",
						this.prjguid,
						"',",
						dataRow["IsHaveChild"].ToString().ToLower(),
						");"
					});
				}
				else
				{
					tableRow.Attributes["onclick"] = string.Concat(new string[]
					{
						"doClick(this,'tblWork');ClickMarketRow('",
						dataRow["TempCode"].ToString(),
						"','",
						this.prjguid,
						"',",
						dataRow["IsHaveChild"].ToString().ToLower(),
						");"
					});
				}
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
			DataRow[] array = this.dtWork.Select("ParentCode ='" + parentCode + "'");
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = false;
				DataRow dataRow = array[i];
				for (int j = 0; j < this.m_TempCodeList.Length; j++)
				{
					if (this.m_TempCodeList[j] == dataRow["TempCode"].ToString())
					{
						flag = true;
						break;
					}
				}
				bool flag2 = this.dtWork.Select("ParentCode ='" + dataRow["TempCode"].ToString() + "'").Length > 0;
				dataRow["IsHaveChild"] = flag2.ToString();
				DataRow dataRow2 = dt1.NewRow();
				dataRow2.ItemArray = dataRow.ItemArray;
				dataRow2["HeadImg"] = inherHead + this.getHeadImg(i, num, dataRow, flag2, firstExpend);
				dataRow2["ParentCode"] = parentCode;
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
					this.CreateChild(dt1, dataRow["TempCode"].ToString(), level + 1, this.getInherImg(i, num, inherHead), isExpend && flag, firstExpend);
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
					for (int i = 0; i < this.m_TempCodeList.Length; i++)
					{
						if (this.m_TempCodeList[i] == dr["TempCode"].ToString())
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						result = "<img src=\"../../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TempCode"].ToString() + "','t');\">";
					}
					else
					{
						if (firstExpend)
						{
							result = "<img src=\"../../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TempCode"].ToString() + "','t');\">";
							this.hdnScheduleCodeList.Value = ((this.hdnScheduleCodeList.Value.Trim() == "") ? "" : (this.hdnScheduleCodeList.Value + "^")) + dr["TempCode"].ToString();
						}
						else
						{
							result = "<img src=\"../../../images/tree/tplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TempCode"].ToString() + "','t');\">";
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
						for (int j = 0; j < this.m_TempCodeList.Length; j++)
						{
							if (this.m_TempCodeList[j] == dr["TempCode"].ToString())
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							result = "<img src=\"../../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TempCode"].ToString() + "','l');\">";
						}
						else
						{
							if (firstExpend)
							{
								result = "<img src=\"../../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TempCode"].ToString() + "','l');\">";
								this.hdnScheduleCodeList.Value = ((this.hdnScheduleCodeList.Value.Trim() == "") ? "" : (this.hdnScheduleCodeList.Value + "^")) + dr["TempCode"].ToString();
							}
							else
							{
								result = "<img src=\"../../../images/tree/lplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TempCode"].ToString() + "','l');\">";
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
		protected void btnDel_Click(object sender, EventArgs e)
		{
			if (!TemplatesAction.getPare(this.hdnWorkCode.Value.Trim()))
			{
				if (TemplatesAction.Delete(this.hdnWorkCode.Value))
				{
					if (!BindBudgetAction.Delete(this.hdnWorkCode.Value))
					{
						this.RegisterStartupScript("", "<script>alert('绑定删除失败!');</script>");
					}
				}
				else
				{
					this.RegisterStartupScript("", "<script>alert('删除失败!');</script>");
				}
			}
			else
			{
				this.RegisterStartupScript("", "<script>alert('该级别下面有分级，请先删除分级！');</script>");
			}
			this.Init_theWork();
			this.tblWork_Create(true);
		}
	}


