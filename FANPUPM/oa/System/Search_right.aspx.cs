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
public partial class oa_System_Search_right : BasePage, IRequiresSessionState
{

	public SystemInfoAction sia
	{
		get
		{
			return new SystemInfoAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.SystemInfoBind();
		}
	}
	protected void SystemInfoBind()
	{
		if (base.Request["cid"] != "")
		{
			string strWhere;
			if (this.txtName.Text != "" && base.Request["cid"] != "0")
			{
				strWhere = string.Concat(new string[]
				{
					"a.SystemName like'%",
					this.txtName.Text.ToString(),
					"%' and  a.ClassID='",
					base.Request["cid"].ToString(),
					"' and a.AuditState=1 order by SignDate desc"
				});
			}
			else
			{
				if (this.txtName.Text != "" && base.Request["cid"] == "0")
				{
					strWhere = "a.SystemName like'%" + this.txtName.Text.ToString() + "%' and a.AuditState=1 order by SignDate desc";
				}
				else
				{
					if (this.txtName.Text == "" && base.Request["cid"] == "0")
					{
						strWhere = "a.AuditState=1 order by SignDate desc";
					}
					else
					{
						strWhere = " a.ClassID='" + base.Request["cid"].ToString() + "'and a.AuditState=1 order by SignDate desc";
					}
				}
			}
			DataTable usreNameList = this.sia.GetUsreNameList(strWhere);
			this.DataGrid1.DataSource = usreNameList;
			this.DataGrid1.DataBind();
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.SystemInfoBind();
	}
	protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
	{
		this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
		this.SystemInfoBind();
	}
	protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
	{
		if (e.Item.ItemIndex != -1)
		{
			DataRowView dataRowView = (DataRowView)e.Item.DataItem;
			e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Item.Attributes["onclick"] = "OnRecord(this);getRecordID('" + ((DataRowView)e.Item.DataItem)["RecordID"].ToString() + "')";
			e.Item.Cells[4].Text = ((dataRowView["IsCurrent"].ToString() == "1") ? "是" : "否");
			ImageButton imageButton = (ImageButton)e.Item.Cells[5].FindControl("IBAnnex");
			imageButton.Attributes["onclick"] = "UpFile(3,'" + dataRowView["RecordID"].ToString() + "')";
			imageButton.ImageUrl = "img/ico.gif";
			e.Item.Attributes["ondblclick"] = "OnRecord(this);getRecordID('" + ((DataRowView)e.Item.DataItem)["RecordID"].ToString() + "');javascript:showEditWin('see')";
		}
	}
}


