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
public partial class oa_WorkManage_StorageManage_StorageCheckDetail : BasePage, IRequiresSessionState
{
	public ptOfficeResStockCheckDetailAction amAction
	{
		get
		{
			return new ptOfficeResStockCheckDetailAction();
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
		if (base.Request["dd"] == null || base.Request["id"] == null || base.Request["ia"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;</script>");
			return;
		}
		if (base.Request["id"].ToString() != "")
		{
			this.InStorageID = int.Parse(base.Request["id"].ToString());
		}
		if (base.Request["dd"].ToString() != "")
		{
			this.DepotID = int.Parse(base.Request["dd"].ToString());
		}
		if (base.Request["ia"].ToString() != "")
		{
			this.AuditState = int.Parse(base.Request["ia"].ToString());
		}
		if (!this.Page.IsPostBack)
		{
			if (this.AuditState <= 0)
			{
				this.btnConfirm.Enabled = true;
				this.btnSave.Enabled = true;
			}
			this.Bind();
		}
		this.btnConfirm.Attributes["onclick"] = "javascript:if(!confirm('确认该次盘点吗?')) return false;";
	}
	private void Bind()
	{
		DataTable list = this.amAction.GetList("a.DepotID=" + this.DepotID);
		this.GVBook.DataSource = list;
		this.GVBook.DataBind();
		if (list.Rows.Count <= 0)
		{
			this.btnSave.Enabled = false;
			return;
		}
		if (this.AuditState <= 0)
		{
			this.btnSave.Enabled = true;
			return;
		}
		this.btnSave.Enabled = false;
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
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
				if (!(a2 == "0"))
				{
					if (a2 == "1")
					{
						e.Row.Cells[3].Text = "部门公共";
					}
				}
				else
				{
					e.Row.Cells[3].Text = "个人领用";
				}
			}
			TextBox textBox = (TextBox)e.Row.Cells[7].FindControl("txtFactAmount");
			textBox.Attributes["onblur"] = "javascript:checkDecimal(this);";
			DataTable stockCheckDetailTable = this.amAction.GetStockCheckDetailTable(this.InStorageID.ToString(), dataRowView["MaterialID"].ToString());
			if (stockCheckDetailTable.Rows.Count > 0)
			{
				DataRow dataRow = stockCheckDetailTable.Rows[0];
				textBox.Text = dataRow["CheckNumber"].ToString();
				e.Row.Cells[8].Text = BooksCommonClass.GetUserName(dataRow["UserCode"].ToString());
				e.Row.Cells[9].Text = dataRow["Remark"].ToString();
			}
		}
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	private ArrayList GetData()
	{
		ArrayList arrayList = new ArrayList();
		foreach (GridViewRow gridViewRow in this.GVBook.Rows)
		{
			arrayList.Add(new ptOfficeResStockCheckDetail
			{
				AccountNumber = (gridViewRow.Cells[6].Text.Trim() == "") ? 0m : Convert.ToDecimal(gridViewRow.Cells[6].Text.Trim()),
				CheckDetailID = 0,
				CheckNumber = (((TextBox)gridViewRow.Cells[6].FindControl("txtFactAmount")).Text.Trim() == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[6].FindControl("txtFactAmount")).Text.Trim()),
				CheckRecordID = this.InStorageID,
				MaterialID = (gridViewRow.Cells[10].Text.Trim() == "") ? -1 : int.Parse(gridViewRow.Cells[10].Text.Trim()),
				Remark = gridViewRow.Cells[9].Text
			});
		}
		return arrayList;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		ArrayList data = this.GetData();
		if (this.amAction.Add(data) > 0)
		{
			this.Bind();
			this.Page.RegisterStartupScript("", "<script>alert('保存成功!');</script>");
		}
	}
	protected void btnConfirm_Click(object sender, EventArgs e)
	{
		if (this.amAction.UpdateStockCheck(this.InStorageID) > 0)
		{
			this.btnConfirm.Enabled = false;
			this.btnSave.Enabled = false;
			this.Page.RegisterStartupScript("", "<script>alert('确认成功!');</script>");
		}
	}
}


