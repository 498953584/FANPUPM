using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_FileManage_FileLendSearch : BasePage, IRequiresSessionState
{

	public OAFileCatalogAction amAction
	{
		get
		{
			return new OAFileCatalogAction();
		}
	}
	public OAFileLendAction flAction
	{
		get
		{
			return new OAFileLendAction();
		}
	}
	public string strWhere
	{
		get
		{
			object obj = this.ViewState["STRWHERE"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["STRWHERE"] = value;
		}
	}
	public bool IsFirst
	{
		get
		{
			object obj = this.ViewState["ISFIRST"];
			return obj == null || (bool)obj;
		}
		set
		{
			this.ViewState["ISFIRST"] = value;
		}
	}
	public string LibraryCode
	{
		get
		{
			object obj = this.ViewState["LIBRARYCODE"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["LIBRARYCODE"] = value;
		}
	}
	public int FileAge
	{
		get
		{
			object obj = this.ViewState["FILEAGE"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			this.ViewState["FILEAGE"] = value;
		}
	}
	public int TimeLimit
	{
		get
		{
			object obj = this.ViewState["TIMELIMIT"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			this.ViewState["TIMELIMIT"] = value;
		}
	}
	public int ClassID
	{
		get
		{
			object obj = this.ViewState["CLASSID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			this.ViewState["CLASSID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (base.Request["isFirst"] == null)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "err", "alert('参数错误!');", true);
			return;
		}
		this.IsFirst = Convert.ToBoolean(base.Request["isFirst"].ToString());
		if (base.Request["lc"] != null)
		{
			this.LibraryCode = base.Request["lc"].ToString();
		}
		if (base.Request["y"] != null)
		{
			this.FileAge = int.Parse(base.Request["y"].ToString());
		}
		if (base.Request["l"] != null)
		{
			this.TimeLimit = int.Parse(base.Request["l"].ToString());
		}
		if (base.Request["cid"] != null)
		{
			this.ClassID = int.Parse(base.Request["cid"].ToString());
		}
		if (!this.Page.IsPostBack)
		{
			this.Bind(this.GetTerm());
			this.Lend_Bind();
		}
		this.txtotherSearch.Attributes["onblur"] = "IsInteger(this);";
		this.DDLSearch.Attributes["onchange"] = "CheckChanged(this);";
	}
	private string GetTerm()
	{
		this.strWhere = " RecordID not in (select distinct FileRecordID from OA_File_Lend where LendState=2 or LendState=3) ";
		if (this.LibraryCode != "")
		{
			this.strWhere = this.strWhere + " and LibraryCode='" + this.LibraryCode + "' ";
		}
		if (this.ClassID != -1)
		{
			object strWhere = this.strWhere;
			this.strWhere = string.Concat(new object[]
			{
				strWhere,
				" and ClassID='",
				this.ClassID,
				"' "
			});
		}
		if (this.FileAge != -1)
		{
			object strWhere2 = this.strWhere;
			this.strWhere = string.Concat(new object[]
			{
				strWhere2,
				" and FileAge='",
				this.FileAge,
				"' "
			});
		}
		if (this.TimeLimit != -1)
		{
			object strWhere3 = this.strWhere;
			this.strWhere = string.Concat(new object[]
			{
				strWhere3,
				" and TimeLimit='",
				this.TimeLimit,
				"' "
			});
		}
		return this.strWhere;
	}
	private void Bind(string strWhere)
	{
		DataTable dataSource = new DataTable();
		if (!this.IsFirst)
		{
			dataSource = this.amAction.GetList(strWhere);
		}
		this.GVBook.DataSource = dataSource;
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			HtmlInputCheckBox htmlInputCheckBox = (HtmlInputCheckBox)e.Row.Cells[0].Controls[1];
			htmlInputCheckBox.Value = dataRowView["FileCode"].ToString();
			htmlInputCheckBox.Attributes["onclick"] = "javascript:CheckFileCode(this);";
			switch (int.Parse(dataRowView["SecretLevel"].ToString()))
			{
			case 1:
				e.Row.Cells[9].Text = "密秘";
				return;
			case 2:
				e.Row.Cells[9].Text = "机密";
				return;
			case 3:
				e.Row.Cells[9].Text = "绝密";
				break;
			default:
				return;
			}
		}
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind(this.strWhere);
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.strWhere = this.GetTerm();
		if (this.DDLSearch.SelectedValue == "0")
		{
			this.strWhere = this.strWhere + " and FileCode='" + this.txtotherSearch.Text.Trim() + "' ";
		}
		if (this.DDLSearch.SelectedValue == "1")
		{
			if (this.txtotherSearch.Text.Trim() == "密秘")
			{
				this.strWhere += " and SecretLevel=1 ";
			}
			else
			{
				if (this.txtotherSearch.Text.Trim() == "机密")
				{
					this.strWhere += " and SecretLevel=2 ";
				}
				else
				{
					if (this.txtotherSearch.Text.Trim() == "绝密")
					{
						this.strWhere += " and SecretLevel=3 ";
					}
					else
					{
						this.strWhere += " and SecretLevel=0 ";
					}
				}
			}
		}
		if (this.DDLSearch.SelectedValue == "2")
		{
			this.strWhere = this.strWhere + " and FileNumber='" + this.txtotherSearch.Text.Trim() + "' ";
		}
		if (this.DDLSearch.SelectedValue == "3")
		{
			this.strWhere = this.strWhere + " and BoxNumber='" + this.txtotherSearch.Text.Trim() + "' ";
		}
		if (this.DDLSearch.SelectedValue == "4")
		{
			this.strWhere = this.strWhere + " and Principal='" + this.txtotherSearch.Text.Trim() + "' ";
		}
		if (this.DDLSearch.SelectedValue == "5")
		{
			this.strWhere = this.strWhere + " and [FileName] like '%" + this.txtotherSearch.Text.Trim() + "%' ";
		}
		this.Bind(this.strWhere);
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.SetSession();
		this.Add_FileLend();
		this.Lend_DataBind();
	}
	private void Lend_Bind()
	{
		this.SetSession();
		this.Lend_DataBind();
	}
	protected void GVLend_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		switch (e.Row.RowType)
		{
		case DataControlRowType.Header:
		case DataControlRowType.DataRow:
			e.Row.Cells[11].Style["display"] = "none";
			break;
		}
		if (e.Row.RowIndex > -1)
		{
			OAFileLend oAFileLend = (OAFileLend)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);FileLendClickRow('" + oAFileLend.RecordID.ToString() + "');";
			HtmlInputCheckBox htmlInputCheckBox = (HtmlInputCheckBox)e.Row.Cells[0].Controls[1];
			htmlInputCheckBox.Value = oAFileLend.RecordID.ToString();
			htmlInputCheckBox.Attributes["onclick"] = "CheckLendFileCode(this);";
			e.Row.Cells[1].Text = oAFileLend.FileCatalog.FileCode;
			e.Row.Cells[2].Text = oAFileLend.FileCatalog.FileName;
			e.Row.Cells[3].Text = oAFileLend.FileCatalog.Volume.ToString();
			e.Row.Cells[4].Text = oAFileLend.FileCatalog.FileNumber.ToString();
			e.Row.Cells[5].Text = oAFileLend.FileCatalog.BoxNumber.ToString();
			e.Row.Cells[6].Text = oAFileLend.FileCatalog.SavePlace;
			e.Row.Cells[7].Text = oAFileLend.FileCatalog.PrjName;
			e.Row.Cells[8].Text = oAFileLend.FileCatalog.Principal;
			switch (oAFileLend.FileCatalog.SecretLevel)
			{
			case 1:
				e.Row.Cells[9].Text = "密秘";
				break;
			case 2:
				e.Row.Cells[9].Text = "机密";
				break;
			case 3:
				e.Row.Cells[9].Text = "绝密";
				break;
			}
			TextBox textBox = (TextBox)e.Row.Cells[10].Controls[1];
			textBox.Attributes["onchange"] = "javascript:if(!isDate(this)) return false;";
			textBox.Text = oAFileLend.PlanReturnDate.ToShortDateString();
			textBox.ToolTip = "输入档案借阅日期!";
		}
	}
	protected void GVLend_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVLend.PageIndex = e.NewPageIndex;
		this.Lend_Bind();
	}
	private void SetSession()
	{
		if (this.Session["FILELEND"] == null)
		{
			this.Session["FILELEND"] = new ArrayList();
		}
		ArrayList arrayList = (ArrayList)this.Session["FILELEND"];
		foreach (GridViewRow gridViewRow in this.GVLend.Rows)
		{
			bool flag = false;
			for (int i = 0; i < arrayList.Count; i++)
			{
				if (int.Parse(gridViewRow.Cells[11].Text) == ((OAFileLend)arrayList[i]).RecordID)
				{
					((OAFileLend)arrayList[i]).PlanReturnDate = ((((TextBox)gridViewRow.Cells[10].Controls[1]).Text == "") ? DateTime.Now : Convert.ToDateTime(((TextBox)gridViewRow.Cells[10].Controls[1]).Text));
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				arrayList.Add(new OAFileLend
				{
					FileCatalog =
					{
						FileCode = gridViewRow.Cells[1].Text,
						FileName = gridViewRow.Cells[2].Text,
						Volume = gridViewRow.Cells[3].Text,
						FileNumber = gridViewRow.Cells[4].Text,
						BoxNumber = gridViewRow.Cells[5].Text,
						SavePlace = gridViewRow.Cells[6].Text,
						PrjName = gridViewRow.Cells[7].Text,
						Principal = gridViewRow.Cells[8].Text,
						SecretLevel = (gridViewRow.Cells[9].Text == "秘密") ? 1 : ((gridViewRow.Cells[9].Text == "机密") ? 2 : ((gridViewRow.Cells[9].Text == "绝密") ? 3 : 0))
					},
					PlanReturnDate = (((TextBox)gridViewRow.Cells[10].Controls[1]).Text == "") ? DateTime.Now : Convert.ToDateTime(((TextBox)gridViewRow.Cells[10].Controls[1]).Text),
					RecordID = (gridViewRow.Cells[11].Text == "") ? 0 : int.Parse(gridViewRow.Cells[11].Text)
				});
			}
		}
		this.Session["FILELEND"] = arrayList;
	}
	private void Add_FileLend()
	{
		ArrayList arrayList = (ArrayList)this.Session["FILELEND"];
		DataTable fileCatalogList = this.flAction.GetFileCatalogList(this.HdnFileCode.Value.Trim(new char[]
		{
			','
		}));
		for (int i = 0; i < fileCatalogList.Rows.Count; i++)
		{
			DataRow dataRow = fileCatalogList.Rows[i];
			bool flag = false;
			for (int j = 0; j < arrayList.Count; j++)
			{
				if (dataRow["RecordID"].ToString() == ((OAFileLend)arrayList[j]).RecordID.ToString())
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				arrayList.Add(new OAFileLend
				{
					FileCatalog =
					{
						FileCode = dataRow["FileCode"].ToString(),
						FileName = dataRow["FileName"].ToString(),
						Volume = dataRow["Volume"].ToString(),
						FileNumber = dataRow["FileNumber"].ToString(),
						BoxNumber = dataRow["BoxNumber"].ToString(),
						SavePlace = dataRow["SavePlace"].ToString(),
						PrjName = dataRow["PrjName"].ToString(),
						Principal = dataRow["Principal"].ToString(),
						SecretLevel = (dataRow["SecretLevel"].ToString() == "") ? 0 : int.Parse(dataRow["SecretLevel"].ToString())
					},
					PlanReturnDate = (dataRow["PlanReturnDate"].ToString() == "") ? DateTime.Now : Convert.ToDateTime(dataRow["PlanReturnDate"].ToString()),
					RecordID = (dataRow["RecordID"].ToString() == "") ? 0 : int.Parse(dataRow["RecordID"].ToString())
				});
			}
		}
		this.Session["FILELEND"] = arrayList;
	}
	private void Delete_FileLend()
	{
		ArrayList arrayList = (ArrayList)this.Session["FILELEND"];
		string[] array = this.HdnLendFileCode.Value.Trim(new char[]
		{
			','
		}).Split(new char[]
		{
			','
		});
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string s = array2[i];
			for (int j = 0; j < arrayList.Count; j++)
			{
				if (((OAFileLend)arrayList[j]).RecordID == int.Parse(s))
				{
					arrayList.RemoveAt(j);
					break;
				}
			}
		}
		this.Session["FILELEND"] = arrayList;
	}
	private void Lend_DataBind()
	{
		ArrayList arrayList = (ArrayList)this.Session["FILELEND"];
		this.GVLend.DataSource = arrayList;
		this.GVLend.DataBind();
		if (arrayList.Count > 0)
		{
			this.btnApplyLend.Enabled = true;
			return;
		}
		this.btnApplyLend.Enabled = false;
	}
	private ArrayList GetData()
	{
		ArrayList arrayList = new ArrayList();
		foreach (GridViewRow gridViewRow in this.GVLend.Rows)
		{
			arrayList.Add(new OAFileLend
			{
				BorrowMan = this.Session["yhdm"].ToString(),
				RecordID = 0,
				LendDate = DateTime.Now,
				LendState = "1",
				PlanReturnDate = (((TextBox)gridViewRow.Cells[10].Controls[1]).Text == "") ? DateTime.Now : Convert.ToDateTime(((TextBox)gridViewRow.Cells[10].Controls[1]).Text),
				FileRecordID = (gridViewRow.Cells[11].Text == "") ? 0 : int.Parse(gridViewRow.Cells[11].Text),
				ReturnApplyDate = DateTime.Now,
				ReturnDate = DateTime.Now
			});
		}
		return arrayList;
	}
	protected void btnApplyLend_Click(object sender, EventArgs e)
	{
		ArrayList data = this.GetData();
		if (this.flAction.Add(data) > 0)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('成功进行申请借阅!');", true);
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('申请借阅失败!');", true);
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		this.SetSession();
		this.Delete_FileLend();
		this.Lend_DataBind();
		this.btnDel.Enabled = false;
		this.HdnLendFileCode.Value = "";
	}
	protected void TxtPlanReturnDate_TextChanged(object sender, EventArgs e)
	{
		foreach (GridViewRow gridViewRow in this.GVLend.Rows)
		{
			if (gridViewRow.Cells[11].Text == this.HdnLendOneFileCode.Value)
			{
				ArrayList arrayList = (ArrayList)this.Session["FILELEND"];
				int num = 0;
				if (num < arrayList.Count)
				{
					((OAFileLend)arrayList[num]).PlanReturnDate = ((((TextBox)gridViewRow.Cells[10].Controls[1]).Text == "") ? DateTime.Now : Convert.ToDateTime(((TextBox)gridViewRow.Cells[10].Controls[1]).Text));
					this.Session["FILELEND"] = arrayList;
				}
			}
		}
	}
}


