using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_FileManage_FileDestroyAssTabEdit : BasePage, IRequiresSessionState
{

	public OAFileDestroyAction amAction
	{
		get
		{
			return new OAFileDestroyAction();
		}
	}
	public Guid RecordCode
	{
		get
		{
			object obj = this.ViewState["RECORDCODE"];
			if (obj != null)
			{
				return (Guid)this.ViewState["RECORDCODE"];
			}
			return Guid.NewGuid();
		}
		set
		{
			this.ViewState["RECORDCODE"] = value;
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
	public string OperateType
	{
		get
		{
			object obj = this.ViewState["OPERATETYPE"];
			if (obj != null)
			{
				return (string)this.ViewState["OPERATETYPE"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["OPERATETYPE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["op"] == null || base.Request["ac"] == null || base.Request["lc"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.RecordCode = new Guid(base.Request["ac"].ToString().Trim());
		this.LibraryCode = base.Request["lc"].ToString();
		this.OperateType = base.Request["op"].ToString();
		if (this.OperateType == "add")
		{
			if (base.Request["isSave"].ToString() == "0")
			{
				this.btnSave.Enabled = false;
			}
			else
			{
				this.btnSave.Enabled = true;
			}
		}
		if (!this.Page.IsPostBack)
		{
			this.Bind();
		}
	}
	private void Bind()
	{
		ArrayList dataSource = new ArrayList();
		object strWhere = this.strWhere;
		this.strWhere = string.Concat(new object[]
		{
			strWhere,
			" DestroyRecordID= '",
			this.RecordCode,
			"' "
		});
		if (this.OperateType == "upd")
		{
			dataSource = this.amAction.GetModel(this.strWhere);
		}
		this.GVBook.DataSource = dataSource;
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			OAFileDestroy oAFileDestroy = (OAFileDestroy)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new object[]
			{
				"OnRecord(this);ClickRow('",
				oAFileDestroy.DestroyFileID,
				"','",
				Convert.ToString(e.Row.RowIndex + 1),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			HtmlInputText htmlInputText = (HtmlInputText)e.Row.Cells[1].FindControl("txtFileName");
			htmlInputText.Value = oAFileDestroy.OAFileCatalog.FileName;
			htmlInputText.Attributes["ondblclick"] = "javascript:SelectFileName(this,'" + this.LibraryCode + "');";
			HtmlInputHidden htmlInputHidden = (HtmlInputHidden)e.Row.Cells[1].FindControl("HdnRecordID");
			htmlInputHidden.Value = oAFileDestroy.DestroyFileID.ToString();
			HtmlInputText htmlInputText2 = (HtmlInputText)e.Row.Cells[2].FindControl("txtFileCode");
			htmlInputText2.Value = oAFileDestroy.OAFileCatalog.FileCode;
			HtmlInputText htmlInputText3 = (HtmlInputText)e.Row.Cells[3].FindControl("txtPrjName");
			htmlInputText3.Value = oAFileDestroy.OAFileCatalog.PrjName;
			HtmlInputText htmlInputText4 = (HtmlInputText)e.Row.Cells[4].FindControl("txtSecretLevel");
			switch (oAFileDestroy.OAFileCatalog.SecretLevel)
			{
			case 1:
				htmlInputText4.Value = "密秘";
				return;
			case 2:
				htmlInputText4.Value = "机密";
				return;
			case 3:
				htmlInputText4.Value = "绝密";
				break;
			default:
				return;
			}
		}
	}
	private ArrayList GetSession()
	{
		ArrayList arrayList = new ArrayList();
		foreach (GridViewRow gridViewRow in this.GVBook.Rows)
		{
			OAFileDestroy oAFileDestroy = new OAFileDestroy();
			HtmlInputHidden htmlInputHidden = (HtmlInputHidden)gridViewRow.Cells[1].FindControl("HdnRecordID");
			oAFileDestroy.DestroyFileID = ((htmlInputHidden.Value.Trim() == "") ? -2 : int.Parse(htmlInputHidden.Value));
			HtmlInputText htmlInputText = (HtmlInputText)gridViewRow.Cells[1].FindControl("txtFileName");
			oAFileDestroy.OAFileCatalog.FileName = htmlInputText.Value;
			HtmlInputText htmlInputText2 = (HtmlInputText)gridViewRow.Cells[2].FindControl("txtFileCode");
			oAFileDestroy.OAFileCatalog.FileCode = htmlInputText2.Value;
			HtmlInputText htmlInputText3 = (HtmlInputText)gridViewRow.Cells[2].FindControl("txtPrjName");
			oAFileDestroy.OAFileCatalog.PrjName = htmlInputText3.Value;
			HtmlInputText htmlInputText4 = (HtmlInputText)gridViewRow.Cells[2].FindControl("txtSecretLevel");
			oAFileDestroy.OAFileCatalog.SecretLevel = ((htmlInputText4.Value.Trim() == "密秘") ? 1 : ((htmlInputText4.Value.Trim() == "机密") ? 2 : ((htmlInputText4.Value.Trim() == "绝密") ? 3 : 0)));
			oAFileDestroy.DestroyRecordID = this.RecordCode;
			arrayList.Add(oAFileDestroy);
		}
		return arrayList;
	}
	private OAFileDestroy AddOneDestroyRecord()
	{
		return new OAFileDestroy
		{
			DestroyFileID = 0,
			OAFileCatalog =
			{
				FileName = "",
				FileCode = "",
				PrjName = "",
				SecretLevel = -2
			},
			DestroyRecordID = this.RecordCode
		};
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.Session["FILEDESTROY"] = this.GetSession();
		OAFileDestroy value = this.AddOneDestroyRecord();
		ArrayList arrayList = (ArrayList)this.Session["FILEDESTROY"];
		arrayList.Add(value);
		this.GVBook.DataSource = arrayList;
		this.GVBook.DataBind();
		this.Session["FILEDESTROY"] = arrayList;
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		ArrayList session = this.GetSession();
		session.RemoveAt(int.Parse(this.HdnIndexID.Value) - 1);
		this.GVBook.DataSource = session;
		this.GVBook.DataBind();
		this.Session["FILEDESTROY"] = session;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		ArrayList session = this.GetSession();
		if (this.amAction.Delete(this.RecordCode, session) > 0)
		{
			this.Page.RegisterStartupScript("", "<script>alert('保存成功!');</script>");
		}
	}
}


