using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_UserControl_IncomeContract : NBasePage, IRequiresSessionState
{
    private IncometContractBll incomeContract = new IncometContractBll();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!base.IsPostBack)
        {
            System.Collections.Generic.List<IncometContractModel> listArray = this.incomeContract.GetListArray(string.Empty);
            this.gvwContract.DataSource = listArray;
            this.gvwContract.DataBind();
        }
    }
    protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
        }
    }
}
