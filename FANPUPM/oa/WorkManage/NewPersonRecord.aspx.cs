using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_NewPersonRecord : BasePage, IRequiresSessionState
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
		if (base.Request["cc"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;</script>");
			return;
		}
		bool arg_33_0 = this.Page.IsPostBack;
		this.btnTransact.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["v_yhdm"].ToString(),
				"','",
				dataRowView["PositionLevel"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Cells[1].Text = BooksCommonClass.GetDepartmentName(int.Parse(dataRowView["i_bmdm"].ToString()));
			e.Row.Cells[3].Text = OAOfficeCommonClas.GetChinaNum(dataRowView["PositionLevel"].ToString());
			e.Row.Cells[4].Text = OAOfficeCommonClas.GetPostAndRank(dataRowView["PostAndRank"].ToString());
		}
	}
}


