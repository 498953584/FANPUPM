using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometContract_IncometBudgetList : NBasePage, IRequiresSessionState
{
	private string type = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["spType"]))
		{
			this.type = base.Request["spType"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindBudget();
		}
	}
	protected void gvwResource_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			string value = this.gvBudget.DataKeys[e.Row.RowIndex]["SubCount"].ToString();
			string text2 = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			string text3 = this.gvBudget.DataKeys[e.Row.RowIndex]["ModifyType"].ToString();
			int num = text2.Length / 3;
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["Guid"] = text;
			e.Row.Attributes["orderNumber"] = text2;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = value;
			e.Row.Attributes["ModifyType"] = text3;
			if (this.type == "out")
			{
				e.Row.Attributes["resCount"] = this.GetResCount(text).ToString();
			}
			if (text3 == "0")
			{
				e.Row.CssClass = "tr-waring";
			}
		}
	}
	private void DataBindBudget()
	{
		string text = base.Request["prjId"];
		string contractID = base.Request["ContractId"];
		if (!string.IsNullOrEmpty(text))
		{
			DataTable conByParent = BudContractTask.GetConByParent(text, contractID, string.Empty, string.Empty, string.Empty, string.Empty, 0);
			this.gvBudget.DataSource = conByParent;
			this.gvBudget.DataBind();
		}
	}
	private string GetResCount(string taskId)
	{
		string cmdText = string.Format("SELECT COUNT(*) FROM Bud_ContractResource WHERE TaskId = '{0}'", taskId);
		object obj = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]);
		return DBHelper.GetString(obj);
	}
}


