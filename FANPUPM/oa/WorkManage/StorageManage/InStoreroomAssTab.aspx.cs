using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_InStoreroomAssTab : BasePage, IRequiresSessionState
{

	public ptOfficeResInStorageDetailAction amAction
	{
		get
		{
			return new ptOfficeResInStorageDetailAction();
		}
	}
	public int DepotID
	{
		get
		{
			object obj = this.ViewState["DEPOTID"];
			if (obj != null)
			{
				return (int)this.ViewState["DEPOTID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["DEPOTID"] = value;
		}
	}
	public int InStorageID
	{
		get
		{
			object obj = this.ViewState["INSTORAGEID"];
			if (obj != null)
			{
				return (int)this.ViewState["INSTORAGEID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["INSTORAGEID"] = value;
		}
	}
	public string BillCode
	{
		get
		{
			object obj = this.ViewState["BILLCODE"];
			if (obj != null)
			{
				return (string)this.ViewState["BILLCODE"];
			}
			return "";
		}
		set
		{
			this.ViewState["BILLCODE"] = value;
		}
	}
	public int AuditState
	{
		get
		{
			object obj = this.ViewState["AUDITSTATE"];
			if (obj != null)
			{
				return (int)this.ViewState["AUDITSTATE"];
			}
			return -1;
		}
		set
		{
			this.ViewState["AUDITSTATE"] = value;
		}
	}
	public string strWhere
	{
		get
		{
			object obj = this.ViewState["STRWHERE"];
			if (obj != null)
			{
				return (string)this.ViewState["STRWHERE"];
			}
			return "";
		}
		set
		{
			this.ViewState["STRWHERE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["id"] == null || base.Request["dd"] == null || base.Request["bc"] == null || base.Request["ia"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;</script>");
			return;
		}
		if (base.Request["dd"].ToString() != "")
		{
			this.DepotID = int.Parse(base.Request["dd"].ToString());
			this.HdnDepotID.Value = this.DepotID.ToString();
		}
		if (base.Request["id"].ToString() != "")
		{
			this.InStorageID = int.Parse(base.Request["id"].ToString());
			this.HdnInStorageID.Value = this.InStorageID.ToString();
		}
		if (base.Request["ia"].ToString() != "")
		{
			this.AuditState = int.Parse(base.Request["ia"].ToString());
		}
		this.BillCode = base.Request["bc"].ToString();
		this.HdnBillCode.Value = this.BillCode;
		if (!this.Page.IsPostBack)
		{
			if (this.AuditState <= 0)
			{
				this.btnAdd.Enabled = true;
			}
			this.Bind();
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该项吗?')) return false;";
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.amAction.GetList("a.InStorageID=" + this.InStorageID);
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new object[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["InStorageID"].ToString(),
				"','",
				this.AuditState,
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			string a;
			if ((a = dataRowView["UseType"].ToString()) != null)
			{
				if (!(a == "0"))
				{
					if (a == "1")
					{
						e.Row.Cells[2].Text = "以旧换新";
					}
				}
				else
				{
					e.Row.Cells[2].Text = "不回收";
				}
			}
			string a2;
			if ((a2 = dataRowView["GetType"].ToString()) != null)
			{
				if (a2 == "0")
				{
					e.Row.Cells[3].Text = "个人领用";
					return;
				}
				if (!(a2 == "1"))
				{
					return;
				}
				e.Row.Cells[3].Text = "部门公共";
			}
		}
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		this.amAction.DeleteStock(int.Parse(this.HdnRecordID.Value), this.DepotID);
		if (this.amAction.Delete(int.Parse(this.HdnRecordID.Value)) > 0)
		{
			this.HdnRecordID.Value = "-1";
			this.Bind();
		}
	}
}


