using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_UserControl_PayoutContract : NBasePage, IRequiresSessionState
{
    private ContAction contaction = new ContAction();
    private string prjId = string.Empty;
    protected override void OnInit(System.EventArgs e)
    {
        if (!string.IsNullOrEmpty(base.Request["PrjGuid"]))
        {
            this.prjId = base.Request["PrjGuid"].ToString();
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.BindView();
        }
    }
    private string GetWhere()
    {
        string str = "where 1=1 and conContract.flowstate = 1 and conContract.PrjGuid=ptPrjinfo.PrjGuid";
        if (!string.IsNullOrEmpty(this.prjId))
        {
            str = str + " AND conContract.prjGuid='" + this.prjId + "' ";
        }
        if (!string.IsNullOrEmpty(this.txtcontcode.Text))
        {
            str = str + " and conContract.contractcode like '%" + this.txtcontcode.Text.Trim() + "%'";
        }
        if (!string.IsNullOrEmpty(this.txtcontname.Text))
        {
            str = str + " and conContract.contractname like '%" + this.txtcontname.Text.Trim() + "%'";
        }
        return str + " order by InputDate desc";
    }
    private void BindView()
    {
        string sql = "SELECT conContract.*,ptPrjinfo.PrjGuid AS Guid,ptPrjinfo.PrjName FROM Con_Payout_Contract conContract,PT_PrjInfo ptPrjinfo " + this.GetWhere();
        DataTable table = Common2.GetTable(sql);
        this.GridView1.DataSource = table;
        this.GridView1.DataBind();
    }
    protected void searcheBt_Click(object sender, System.EventArgs e)
    {
        this.BindView();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            DataRowView dataRowView = (DataRowView)e.Row.DataItem;
            e.Row.Attributes["id"] = dataRowView["ContractCode"].ToString();
            e.Row.Attributes["onclick"] = string.Concat(new object[]
			{
				"doClick(this,'",
				this.GridView1.ClientID,
				"');clickRow('",
				dataRowView["ContractId"],
				"','",
				dataRowView["ContractName"],
				"','",
				dataRowView["BName"],
				"','",
				dataRowView["Guid"],
				"','",
				dataRowView["PrjName"],
				"');"
			});
            e.Row.Attributes["onmouseover"] = "doMouseOver(this);";
            e.Row.Attributes["onmouseout"] = "doMouseOut(this);";
            if (e.Row.Cells[2].Text.Length > 15)
            {
                e.Row.Cells[2].Text = dataRowView["contractname"].ToString().Substring(0, 15) + "...";
                e.Row.Cells[2].ToolTip = dataRowView["contractname"].ToString();
            }
            e.Row.Attributes["ondblclick"] = "doDblClickRow('" + dataRowView["ContractId"] + "');";
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        this.BindView();
    }
}
