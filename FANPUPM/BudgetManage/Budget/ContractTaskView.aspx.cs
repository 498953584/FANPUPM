using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_ContractTaskQuery : NBasePage, IRequiresSessionState
{
    private PT_PrjInfo prjInfo = new PT_PrjInfo();
    private string ContractTaskAuditId = "";
    
    protected override void OnInit(System.EventArgs e)
    {
        if (!string.IsNullOrEmpty(base.Request["ic"]))
        {
            this.ContractTaskAuditId = base.Request["ic"];
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.InitPage();
        }
    }
    public void InitPage()
    {
        this.BindGv();
    }
    public void BindGv()
    {
        BudContractTaskAudit modelById = BudContractTaskAudit.GetModelById(this.ContractTaskAuditId);
        if (modelById != null)
        {
            DataTable taskInfo = BudContractTask.GetTaskInfo(modelById.PrjId, string.Empty, string.Empty, string.Empty);
            this.gvBudget.DataSource = taskInfo;
            this.gvBudget.DataBind();
            this.hfldPrjId.Value = modelById.PrjId;
        }
    }
    protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string text = this.gvBudget.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
            string text2 = this.gvBudget.DataKeys[e.Row.RowIndex]["SubCount"].ToString();
            string text3 = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
            int num = text3.Length / 3;
            e.Row.Attributes["id"] = text;
            e.Row.Attributes["orderNumber"] = text3;
            e.Row.Attributes["layer"] = num.ToString();
            e.Row.Attributes["subCount"] = text2;
            bool flag = text2 == "0";
            if (flag)
            {
                Image image = new Image();
                image.ImageUrl = "../../images/tree/more.gif";
                image.ToolTip = "详细信息";
                image.Attributes.Add("rowId", text);
                image.Attributes["onclick"] = "showInfo('" + text + "')";
                image.Style.Add("vertical-align", "middle");
                image.Style.Add("float", "right");
                image.Style.Add("cursor", "pointer");
                e.Row.Cells[1].Controls.Add(image);
            }
        }
    }
}
