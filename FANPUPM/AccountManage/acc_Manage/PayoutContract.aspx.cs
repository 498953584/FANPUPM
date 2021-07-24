using ASP;
using cn.justwin.AccountManage.prjaccount;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutContract_PayoutContract : NBasePage, IRequiresSessionState
{
    private accountMange am = new accountMange();
    private PayoutContract payoutContract = new PayoutContract();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.gvwContract.PageSize = NBasePage.pagesize;
        if (!base.IsPostBack)
        {
            List<PayoutContractModel> conNoList = this.am.GetConNoList(" Con_Payout_Contract.IsArchived = 0 and flowstate=1 ", base.UserCode);
            this.DataBindContracts(conNoList);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        List<PayoutContractModel> contracts = this.SearchContracts();
        this.DataBindContracts(contracts);
    }
    private List<PayoutContractModel> SearchContracts()
    {
        DateTime startDate = string.IsNullOrEmpty(this.txtStartTime.Text.Trim()) ? new DateTime(1753, 1, 1) : Convert.ToDateTime(this.txtStartTime.Text);
        DateTime endDate = string.IsNullOrEmpty(this.txtEndTime.Text.Trim()) ? new DateTime(9999, 12, 31) : Convert.ToDateTime(this.txtEndTime.Text.Trim());
        decimal startMoney = string.IsNullOrEmpty(this.txtStartMoney.Text.Trim()) ? 0m : Convert.ToDecimal(this.txtStartMoney.Text.Trim());
        decimal endMoney = string.IsNullOrEmpty(this.txtEndMoney.Text.Trim()) ? new decimal(999999999999999L) : Convert.ToDecimal(this.txtEndMoney.Text.Trim());
        return this.payoutContract.SelConState(startDate, endDate, startMoney, endMoney, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.ContractType1.TypeID, this.hdnProjectCode.Value, base.UserCode);
    }
    private void DataBindContracts(List<PayoutContractModel> contracts)
    {
        this.gvwContract.DataSource = contracts;
        this.gvwContract.DataBind();
    }
    protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes["guid"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes["isMainContract"] = this.gvwContract.DataKeys[e.Row.RowIndex].Values[1].ToString();
            e.Row.Attributes["prjGuid"] = this.gvwContract.DataKeys[e.Row.RowIndex].Values[2].ToString();
            e.Row.Attributes["auditContent"] = "支出合同 ：" + this.gvwContract.DataKeys[e.Row.RowIndex].Values[3].ToString();
        }
    }
    protected void gvwContract_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvwContract.PageIndex = e.NewPageIndex;
        List<PayoutContractModel> contracts = this.SearchContracts();
        this.DataBindContracts(contracts);
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string a = base.Request["limits"].ToString();
        using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            try
            {
                List<prjaccountModel> list = new List<prjaccountModel>();
                for (int i = 0; i < this.gvwContract.Rows.Count; i++)
                {
                    CheckBox checkBox = (CheckBox)this.gvwContract.Rows[i].FindControl("chk");
                    if (checkBox.Checked)
                    {
                        string str;
                        if (i < 10)
                        {
                            str = "0" + i.ToString();
                        }
                        else
                        {
                            str = i.ToString();
                        }
                        prjaccountModel prjaccountModel = new prjaccountModel();
                        prjaccountModel.accountNum = DateTime.Now.ToString("yyyyMMddHHmmss") + str;
                        prjaccountModel.acountName = "";
                        HiddenField hiddenField = (HiddenField)this.gvwContract.Rows[i].FindControl("hdfConNum");
                        prjaccountModel.contractNum = hiddenField.Value.ToString();
                        prjaccountModel.creatData = new DateTime?(DateTime.Now);
                        prjaccountModel.createMan = base.UserCode;
                        prjaccountModel.isActivity = new int?(0);
                        prjaccountModel.prjNum = new Guid("00000000-0000-0000-0000-000000000000");
                        prjaccountModel.remark = "";
                        string authorizer = "";
                        if (a == "0")
                        {
                            authorizer = this.am.GetPrjUserCode(prjaccountModel.contractNum);
                        }
                        prjaccountModel.authorizer = authorizer;
                        list.Add(prjaccountModel);
                    }
                }
                this.am.Add(sqlTransaction, list);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" parent.desktop.flowclass.location='/AccountManage/acc_Manage/accountList.aspx';").Append(Environment.NewLine);
                stringBuilder.Append("alert('系统提示：\\n\\n添加成功！');").Append(Environment.NewLine);
                stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
                base.RegisterScript(stringBuilder.ToString());
                sqlTransaction.Commit();
            }
            catch
            {
                sqlTransaction.Rollback();
                base.RegisterScript("alert('系统提示：\\n\\n添加失败！');");
            }
        }
    }
}
