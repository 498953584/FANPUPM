using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CostApproveGather : PageBase, IRequiresSessionState
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
				','
			});
			if (!this.Page.IsPostBack)
			{
				this.DDLDate_Bind();
				if (base.Request["pc"] == null)
				{
					return;
				}
				this.ProjectCode = new Guid(base.Request["pc"].ToString());
				this.Init_theWork(this.ProjectCode);
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
		private void DDLDate_Bind()
		{
			for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 5; i++)
			{
				this.DDLYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
			}
			for (int j = 1; j <= 12; j++)
			{
				this.DDLMonth.Items.Add(new ListItem((j < 10) ? ("0" + j.ToString()) : j.ToString(), (j < 10) ? ("0" + j.ToString()) : j.ToString()));
			}
			try
			{
				this.DDLYear.SelectedValue = DateTime.Now.Year.ToString();
				this.DDLMonth.SelectedValue = ((DateTime.Now.Month < 10) ? ("0" + DateTime.Now.Month.ToString()) : DateTime.Now.Month.ToString());
			}
			catch
			{
			}
		}
		private void Init_theWork(Guid projectCode)
		{
			this.dtWork.Columns.Add(new DataColumn("ParentWorkCode", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("WorkCode", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("WorkName", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("WorkMoney", typeof(string)));
			this.dtWork.Columns.Add(new DataColumn("IsHaveChild", typeof(string)));
			DateTime startDate = Convert.ToDateTime(((this.DDLYear.SelectedValue == "") ? DateTime.Now.Year.ToString() : this.DDLYear.SelectedValue) + "-" + ((this.DDLMonth.SelectedValue == "") ? DateTime.Now.Month.ToString() : this.DDLMonth.SelectedValue) + "-1");
			ArrayList arrayList;
			if (this.ProjectCode.ToString() != Guid.Empty.ToString())
			{
				this.theCBS.InitialzeCBS(this.ProjectCode.ToString(), startDate);
				arrayList = this.theCBS.GetAllCBSFee(this.ProjectCode.ToString(), startDate);
			}
			else
			{
				arrayList = new ArrayList();
			}
			for (int i = 0; i < arrayList.Count; i++)
			{
				DataRow dataRow = this.dtWork.NewRow();
				CBSFeeNodeInfo cBSFeeNodeInfo = (CBSFeeNodeInfo)arrayList[i];
				dataRow["ParentWorkCode"] = cBSFeeNodeInfo.PNode;
				dataRow["WorkCode"] = cBSFeeNodeInfo.NodeCode;
				dataRow["WorkName"] = cBSFeeNodeInfo.NodeName.Replace("&nbsp;", "").Trim();
				dataRow["WorkMoney"] = cBSFeeNodeInfo.Money.ToString() + "&nbsp";
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
				tableCell.Text = dataRow["WorkMoney"].ToString();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableRow.Cells.Add(tableCell);
				if (dataRow["ParentWorkCode"].ToString() != "")
				{
					tableRow.Attributes["id"] = dataRow["ParentWorkCode"].ToString();
				}
				if (dataRow["WorkCode"].ToString() == "001001" || dataRow["WorkCode"].ToString() == "002" || dataRow["WorkCode"].ToString() == "003")
				{
					tableRow.Font.Bold = true;
				}
				tableRow.Attributes["onclick"] = string.Concat(new string[]
				{
					"clickRow(this,'",
					dataRow["WorkCode"].ToString(),
					"',",
					dataRow["IsHaveChild"].ToString().ToLower(),
					",'",
					dataRow["WorkMoney"].ToString(),
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
							this.hdnWorkCodeList.Value = ((this.hdnWorkCodeList.Value.Trim() == "") ? "" : (this.hdnWorkCodeList.Value + ",")) + dr["WorkCode"].ToString();
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
								this.hdnWorkCodeList.Value = ((this.hdnWorkCodeList.Value.Trim() == "") ? "" : (this.hdnWorkCodeList.Value + ",")) + dr["WorkCode"].ToString();
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
		protected void btnExp_Click1(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "月度汇总.xls");
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
			this.Tablex.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
		protected void BtnSearch_Click(object sender, EventArgs e)
		{
			this.Init_theWork(this.ProjectCode);
			this.tblWork_Create(true);
		}
	}


