using ASP;
using cn.justwin.stockBLL.Files;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CodeList : SubwayBasePage, IRequiresSessionState
	{
		private string[] _CodeList;

		protected static CodingAction CodingAct
		{
			get
			{
				return new CodingAction();
			}
		}
		protected int TypeID
		{
			get
			{
				object obj = this.ViewState["TYPEID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TYPEID"] = value;
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.hdnstate.Value = base.Request.QueryString["r"];
			this._CodeList = this.HdnCodeUse.Value.Split(new char[]
			{
				'^'
			});
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["tid"] == null || base.Request.Params["w"] == null)
				{
					this.JS.Text = "alert('参数错误');window.returnValue=false;window.close();";
					return;
				}
				DataTable dataTable = CodingAction.CodeInfoListType(base.Request.Params["tid"].ToString());
				this.TypeID = System.Convert.ToInt32(dataTable.Rows[0]["TypeID"].ToString());
				this.HdnTypeID.Value = this.TypeID.ToString();
				if (base.Request.Params["w"] == "0")
				{
					this.BtnClose.Visible = false;
				}
				this.Page_CustomInit();
				this.TblCodeTree_Create(true);
				this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('删除该数据可能造成数据不完整，\\r\\n 确定要删除当前选中数据吗？')){return false;}";
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void Page_CustomInit()
		{
			this.BtnClose.Attributes["onclick"] = "window.close();";
			this.BtnAdd.Attributes["onclick"] = "javascript:OpenBasicCodeEdit('1');";
			this.BtnUpd.Attributes["onclick"] = "javascript:OpenBasicCodeEdit('2');";
			this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
		}
		private void TblCodeTree_Create(bool firstExend)
		{
			DataTable dataFromCollection = this.GetDataFromCollection(CodeList.CodingAct.QueryCodeInfoList(this.TypeID, ValidState.Valid));
			DataTable dataTable = new DataTable();
			foreach (DataColumn dataColumn in dataFromCollection.Columns)
			{
				dataTable.Columns.Add(new DataColumn(dataColumn.ColumnName, dataColumn.DataType));
			}
			dataTable.Columns.Add(new DataColumn("HeadImg", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Display", typeof(string)));
			this.CreateChild(dataFromCollection, dataTable, 0, 0, "", false, firstExend);
			this.TblCodeTree_Draw(dataTable);
		}
		private DataTable GetDataFromCollection(CodingInfoCollection colt)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("CodeID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("TypeID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("ParentCodeID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("ParentCodeList", typeof(string)));
			dataTable.Columns.Add(new DataColumn("CodeName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("ChildNumber", typeof(int)));
			dataTable.Columns.Add(new DataColumn("IsDefault", typeof(bool)));
			dataTable.Columns.Add(new DataColumn("IsFixed", typeof(bool)));
			dataTable.Columns.Add(new DataColumn("Owner", typeof(string)));
			dataTable.Columns.Add(new DataColumn("I_xh", typeof(int)));
			int count = colt.Count;
			for (int i = 0; i < count; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["CodeID"] = colt[i].CodeID.ToString();
				dataRow["TypeID"] = colt[i].TypeID.ToString();
				dataRow["ParentCodeID"] = colt[i].ParentCodeID.ToString();
				dataRow["ParentCodeList"] = colt[i].ParentCodeList.ToString();
				dataRow["CodeName"] = colt[i].CodeName;
				dataRow["ChildNumber"] = colt[i].ChildNumber;
				dataRow["IsDefault"] = colt[i].IsDefault;
				dataRow["IsFixed"] = colt[i].IsFixed;
				dataRow["Owner"] = colt[i].Owner.ToString();
				dataRow["I_xh"] = colt[i].I_xh.ToString();
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}
		public void CreateChild(DataTable dtSource, DataTable dtDest, int parentCodeID, int level, string inherHead, bool isExpend, bool firstExpend)
		{
			DataRow[] array = dtSource.Select("ParentCodeID =" + parentCodeID.ToString());
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = false;
				DataRow dataRow = array[i];
				for (int j = 0; j < this._CodeList.Length; j++)
				{
					if (this._CodeList[j] == dataRow["CodeID"].ToString())
					{
						flag = true;
						break;
					}
				}
				DataRow dataRow2 = dtDest.NewRow();
				dataRow2.ItemArray = dataRow.ItemArray;
				bool flag2 = (int)dataRow["ChildNumber"] != 0;
				dataRow2["HeadImg"] = inherHead + this.getHeadImg(i, num, dataRow, flag2, firstExpend);
				dataRow2["ParentCodeID"] = parentCodeID;
				if (level == 0)
				{
					dataRow2["display"] = "";
					isExpend = true;
				}
				else
				{
					if (isExpend)
					{
						dataRow2["display"] = "";
					}
					else
					{
						if (firstExpend)
						{
							dataRow2["display"] = "";
						}
						else
						{
							dataRow2["display"] = "none";
						}
					}
				}
				dtDest.Rows.Add(dataRow2);
				if (flag2)
				{
					this.CreateChild(dtSource, dtDest, (int)dataRow["CodeID"], level + 1, this.getInherImg(i, num, inherHead), isExpend && flag, firstExpend);
				}
			}
		}
		private void TblCodeTree_Draw(DataTable dt)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			CodingType codingType = CodeList.CodingAct.QuerySingleCodingType(this.TypeID);
			tableCell.Text = codingType.TypeName.ToString();
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableRow.Attributes["onclick"] = "doClick(this,'" + this.TblCodeTree.ClientID + "');clickCodeRow('0','0',false,false);";
			tableRow.Attributes["onmouseover"] = "doMouseOver(this);";
			tableRow.Attributes["onmouseout"] = "doMouseOut(this);";
			tableRow.CssClass = "grid_row";
			this.TblCodeTree.Rows.Add(tableRow);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				DataRow dataRow = dt.Rows[i];
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Text = dataRow["HeadImg"].ToString() + dataRow["CodeName"].ToString().Trim();
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				if (dataRow["IsDefault"].ToString().ToLower() == "false")
				{
					tableCell.Text = "\t<img src=\"../../images/white.gif\"  border=\"0\" style=\"height:15px;width:16px;\" />";
				}
				else
				{
					tableCell.Text = "<img src=\"../../images/default.gif\" alt=\"默认项\" border=\"0\" style=\"height:15px;width:16px;\" />";
				}
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["Owner"].ToString().Trim();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableRow.Cells.Add(tableCell);
				tableCell = new TableCell();
				tableCell.Text = dataRow["I_xh"].ToString().Trim();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableRow.Cells.Add(tableCell);
				if ((int)dataRow["ParentCodeID"] != 0)
				{
					string text = dataRow["ParentCodeList"].ToString();
					tableRow.Attributes["id"] = text.Substring(0, text.LastIndexOf(",", text.Length - 2) + 1);
				}
				bool flag = !(bool)dataRow["IsFixed"];
				bool flag2 = (int)dataRow["ChildNumber"] == 0 && !(bool)dataRow["IsFixed"] && !(bool)dataRow["IsDefault"];
				tableRow.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.TblCodeTree.ClientID,
					"');clickCodeRow('",
					dataRow["CodeID"].ToString(),
					"','",
					dataRow["ParentCodeID"].ToString(),
					"',",
					flag.ToString().ToLower(),
					",",
					flag2.ToString().ToLower(),
					");"
				});
				tableRow.Attributes["onmouseover"] = "doMouseOver(this);";
				tableRow.Attributes["onmouseout"] = "doMouseOut(this);";
				tableRow.Style.Add("display", dataRow["display"].ToString());
				tableRow.CssClass = "grid_row";
				this.TblCodeTree.Rows.Add(tableRow);
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
					for (int i = 0; i < this._CodeList.Length; i++)
					{
						if (this._CodeList[i] == dr["ParentCodeList"].ToString())
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						result = "<img src=\"../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["ParentCodeList"].ToString() + "','t');\">";
					}
					else
					{
						if (firstExpend)
						{
							result = "<img src=\"../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["ParentCodeList"].ToString() + "','t');\">";
							this.HdnCodeUse.Value = ((this.HdnCodeUse.Value.Trim() == "") ? dr["ParentCodeList"].ToString() : (this.HdnCodeUse.Value.Trim() + "^" + dr["ParentCodeList"].ToString()));
						}
						else
						{
							result = "<img src=\"../../images/tree/tplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["ParentCodeList"].ToString() + "','t');\">";
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
						for (int j = 0; j < this._CodeList.Length; j++)
						{
							if (this._CodeList[j] == dr["ParentCodeList"].ToString())
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							result = "<img src=\"../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["ParentCodeList"].ToString() + "','l');\">";
						}
						else
						{
							if (firstExpend)
							{
								result = "<img src=\"../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["ParentCodeList"].ToString() + "','l');\">";
								this.HdnCodeUse.Value = ((this.HdnCodeUse.Value.Trim() == "") ? dr["ParentCodeList"].ToString() : (this.HdnCodeUse.Value.Trim() + "^" + dr["ParentCodeList"].ToString()));
							}
							else
							{
								result = "<img src=\"../../images/tree/lplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["ParentCodeList"].ToString() + "','l');\">";
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
		protected void BtnAdd_Click(object sender, System.EventArgs e)
		{
			this.HdnIsChange.Value = "1";
			this.JS.Text = "window.returnValue = true;";
			this.TblCodeTree_Create(true);
			com.jwsoft.pm.entpm.PageHelper.RefreshBasicCode(this, (BasicType)System.Enum.Parse(typeof(BasicType), this.TypeID.ToString()));
		}
		protected void BtnUpd_Click(object sender, System.EventArgs e)
		{
			this.HdnIsChange.Value = "1";
			this.JS.Text = "window.returnValue = true;";
			this.TblCodeTree_Create(true);
			com.jwsoft.pm.entpm.PageHelper.RefreshBasicCode(this, (BasicType)System.Enum.Parse(typeof(BasicType), this.TypeID.ToString()));
		}
		protected void BtnDel_Click(object sender, System.EventArgs e)
		{
			int num = System.Convert.ToInt32(this.HdnCodeID.Value);
			fileTypeLogic fileTypeLogic = new fileTypeLogic();
			if (fileTypeLogic.veriFileType(num, this.TypeID))
			{
				if (CodeList.CodingAct.DelBasicCode(this.TypeID, num) == 1)
				{
					this.HdnIsChange.Value = "1";
					this.JS.Text = "top.ui.show('删除成功！');";
				}
				else
				{
					this.JS.Text = "top.ui.alert('删除失败！');";
				}
			}
			else
			{
				this.JS.Text = "top.ui.alert('类型正在使用中，不可删除！');";
			}
			this.TblCodeTree_Create(true);
			com.jwsoft.pm.entpm.PageHelper.RefreshBasicCode(this, (BasicType)System.Enum.Parse(typeof(BasicType), this.TypeID.ToString()));
		}
	}


