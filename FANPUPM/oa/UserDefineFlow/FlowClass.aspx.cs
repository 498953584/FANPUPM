using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class UserDefineFlow_FlowClass : NBasePage, IRequiresSessionState
{

	public WFBusinessClassAction mcAction
	{
		get
		{
			return new WFBusinessClassAction();
		}
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		bool arg_0B_0 = this.Page.IsPostBack;
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该自定义流程吗?')) return false;";
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			string value = this.GVBook.DataKeys[e.Row.RowIndex].Values[0].ToString();
			string value2 = this.GVBook.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["busiClass"] = value2;
			e.Row.Cells[1].Text = System.Convert.ToString(e.Row.RowIndex + 1 + this.GVBook.PageIndex * this.GVBook.PageSize);
			string sqlString = "select * from wf_privilege where businessclass='" + dataRowView["BusinessClass"] + "'";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			string text = "";
			if (dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					if (i < dataTable.Rows.Count - 1)
					{
						text = text + PageHelper.QueryUser(this, dataTable.Rows[i]["userlist"].ToString()) + ",";
					}
					else
					{
						text += PageHelper.QueryUser(this, dataTable.Rows[i]["userlist"].ToString());
					}
				}
			}
			if (text.Length > 30)
			{
				Label label = (Label)e.Row.FindControl("lbName");
				label.Text = text.Substring(0, 30) + "...";
				label.ToolTip = text;
				return;
			}
			Label label2 = (Label)e.Row.FindControl("lbName");
			label2.Text = text;
			label2.ToolTip = text;
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		if (this.hfldId.Value.Contains(","))
		{
			this.hfldId.Value = this.hfldId.Value.Replace("\"", "'").Substring(1).TrimEnd(new char[]
			{
				']'
			});
		}
		DataTable dataTable = new DataTable();
		if (this.hfldId.Value.Contains(","))
		{
			dataTable = this.mcAction.GetList(" id in (" + this.hfldId.Value.Trim() + ")");
		}
		else
		{
			dataTable = this.mcAction.GetList(" id = '" + this.hfldId.Value.Trim() + "'");
		}
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			this.HdnBusinessClass.Value = dataTable.Rows[i]["BusinessClass"].ToString();
			this.HdnBusinessCode.Value = dataTable.Rows[i]["BusinessCode"].ToString();
			string sqlString = "delete from wf_privilege where businessclass='" + this.HdnBusinessClass.Value + "'";
			publicDbOpClass.ExecSqlString(sqlString);
			if (this.mcAction.Delete(this.HdnBusinessCode.Value, this.HdnBusinessClass.Value) > 0)
			{
				this.GVBook.DataBind();
			}
		}
	}
}


