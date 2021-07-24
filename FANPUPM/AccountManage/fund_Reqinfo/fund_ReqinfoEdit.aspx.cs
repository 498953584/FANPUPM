using AccountManage.BLL;
using AccountManage.Model;
using ASP;
using cn.justwin.AccountManage.accBaise;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using cn.justwin.stockBLL.AccountManage.accBaise;
using com.jwsoft.pm.entpm;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_fund_Reqinfo_fund_ReqinfoEdit : NBasePage, IRequiresSessionState
{
    private string action = string.Empty;
    private string contractID = string.Empty;
    private fund_ReqinfoBll fund_ReqinfoBll = new fund_ReqinfoBll();

    protected override void OnInit(EventArgs e)
    {
        if (!string.IsNullOrEmpty(base.Request["Action"]))
        {
            this.action = base.Request["Action"];
        }
        if (!string.IsNullOrEmpty(base.Request["Pcode"]))
        {
            this.contractID = base.Request["Pcode"].ToString().ToLower();
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.DataBindDrop();
            if (string.Compare(this.action, "Add", true) == 0)
            {
                this.InitAdd();
            }
            else
            {
                this.InitUpdateAndQuery();
            }
            accBaise accBaise = new accBaise();
            basieModel model = accBaise.GetModel(1);
            if (model != null)
            {
                string value = model.fundRatio.ToString();
                this.hdfrate.Value = value;
                if (model.accAllot == 0)
                {
                    this.Pro1.Visible = true;
                    this.Pro2.Visible = true;
                    this.Pro3.Visible = false;
                    this.Pro4.Visible = false;
                    return;
                }
                this.Pro1.Visible = false;
                this.Pro2.Visible = false;
                this.Pro3.Visible = true;
                this.Pro4.Visible = true;
                return;
            }
            else
            {
                string value = "0";
                this.hdfrate.Value = value;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.Compare(this.action, "Add", true) == 0)
        {
            this.AddContract();
            new DataTable();
            return;
        }
        if (string.Compare(this.action, "AddProtocol", true) == 0)
        {
            return;
        }
        this.UpdateContract();
    }
    private void InitAdd()
    {
        this.txtContractCode.Text = Guid.NewGuid().ToString().ToLower();
        this.hdfusercode.Value = base.UserCode;
        this.txtUsercode.Value = PageHelper.QueryUser(this, this.hdfusercode.Value);
        this.HiddenField2.Value = this.DropDownList1.SelectedValue;
    }
    private void InitUpdateAndQuery()
    {
        fund_ReqinfoModle model = this.fund_ReqinfoBll.GetModel(this.contractID);
        this.txtContractCode.Text = model.reqNum;
        this.txtContractMoney.Value = ((!model.amount.HasValue) ? string.Empty : model.amount.ToString());
        this.txtStartDate.Text = ((!model.reqDate.HasValue) ? string.Empty : Convert.ToDateTime(model.reqDate).ToShortDateString());
        this.txtEndDate.Text = ((!model.useDate.HasValue) ? string.Empty : Convert.ToDateTime(model.useDate).ToShortDateString());
        this.txtMainItem.Text = model.reqCause;
        this.txtNotes.Text = model.remark;
        this.hdfusercode.Value = ((model.reqPeopNum == null) ? string.Empty : model.reqPeopNum.ToString());
        this.txtUsercode.Value = PageHelper.QueryUser(this, this.hdfusercode.Value);
        this.ViewState["type"] = ((!model.IsContr.HasValue) ? "0" : model.IsContr.ToString());
        if (this.ViewState["type"].ToString() == "0")
        {
            this.HiddenField1.Value = ((model.PrjNum == null) ? string.Empty : model.PrjNum);
            this.txtContr.Value = this.GetContrName(model.PrjNum.Trim());
            this.Pro1.Visible = false;
            this.Pro2.Visible = false;
            this.Pro3.Visible = true;
            this.Pro4.Visible = true;
        }
        else
        {
            this.hdnProjectCode.Value = ((model.PrjNum == null) ? string.Empty : model.PrjNum);
            this.txtProject.Value = this.GetProName(model.PrjNum.Trim());
            this.Pro3.Visible = false;
            this.Pro4.Visible = false;
            this.Pro1.Visible = true;
            this.Pro2.Visible = true;
        }
        this.RadioButton1.Checked = model.isInterest;
        if (this.RadioButton1.Checked)
        {
            this.isshow.Visible = true;
            this.RadioButton3.Enabled = true;
            this.RadioButton4.Enabled = true;
            this.RadioButton2.Checked = false;
        }
        else
        {
            this.isshow.Visible = false;
            this.RadioButton3.Enabled = true;
            this.RadioButton4.Enabled = true;
        }
        this.RadioButton3.Checked = model.isDefault;
        if (this.RadioButton3.Checked)
        {
            this.RadioButton4.Checked = false;
        }
        else
        {
            this.RadioButton4.Checked = true;
            this.td2.Visible = true;
            this.td1.Visible = true;
        }
        this.TextBox1.Text = ((!model.InterestNum.HasValue) ? string.Empty : Convert.ToDecimal(model.InterestNum).ToString());
        if (string.Compare(this.action, "Query", true) == 0)
        {
            this.btnSave.Visible = false;
            this.btnCancel.Visible = false;
        }
    }
    private void DataBindDrop()
    {
    }
    private void AddContract()
    {
        fund_ReqinfoModle contract = new fund_ReqinfoModle();
        this.InitContract(contract);
        this.Add(contract);
    }
    private void Add(fund_ReqinfoModle contract)
    {
        fund_ReqinfoBll fund_ReqinfoBll = new fund_ReqinfoBll();
        try
        {
            if (fund_ReqinfoBll.Exists(contract))
            {
                base.RegisterScript("alert('系统提示：\\n\\n此资金申请编号已经存在')");
            }
            else
            {
                fund_ReqinfoBll.Add(contract);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("alert('系统提示：\\n\\n添加成功')").Append(Environment.NewLine);
                stringBuilder.Append("winclose('fund_ReqinfoEdit', 'fund_ReqinfoList.aspx', true)");
                base.RegisterScript(stringBuilder.ToString());
            }
        }
        catch (Exception)
        {
            base.RegisterScript("alert('系统提示：\\n\\n添加失败')");
        }
    }
    private void UpdateContract()
    {
        fund_ReqinfoModle model = this.fund_ReqinfoBll.GetModel(this.contractID);
        this.InitContract(model);
        try
        {
            this.fund_ReqinfoBll.Update(model);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("alert('系统提示：\\n\\n修改成功')").Append(Environment.NewLine);
            stringBuilder.Append("winclose('fund_ReqinfoEdit', 'fund_ReqinfoList.aspx', true)");
            base.RegisterScript(stringBuilder.ToString());
        }
        catch (Exception)
        {
            base.RegisterScript("alert('系统提示：\\n\\n修改失败')");
        }
    }
    private void InitContract(fund_ReqinfoModle contract)
    {
        contract.reqNum = this.txtContractCode.Text.Trim();
        accBaise accBaise = new accBaise();
        basieModel model = accBaise.GetModel(1);
        string a;
        if (model != null)
        {
            decimal arg_35_0 = model.borrowRate.Value;
            a = model.accAllot.ToString();
        }
        else
        {
            a = "0";
        }
        if (!string.IsNullOrEmpty(this.txtContractMoney.Value.Trim()))
        {
            contract.amount = new decimal?(Convert.ToDecimal(this.txtContractMoney.Value.Trim()));
        }
        contract.auditState = new int?(-1);
        contract.reqCause = ((this.txtMainItem.Text.Trim() == null) ? string.Empty : this.txtMainItem.Text.Trim());
        contract.remark = ((this.txtNotes.Text.Trim() == null) ? string.Empty : this.txtNotes.Text.Trim());
        this.hdfusercode.Value = base.UserCode;
        this.txtUsercode.Value = PageHelper.QueryUser(this, base.UserCode);
        contract.reqPeopNum = ((this.hdfusercode.Value.Trim() == null) ? string.Empty : this.hdfusercode.Value.Trim());
        if (a == "1")
        {
            contract.IsContr = new int?(0);
            contract.PrjNum = ((this.HiddenField1.Value.ToLower().Trim() == null) ? string.Empty : this.HiddenField1.Value.ToLower().Trim());
        }
        else
        {
            contract.IsContr = new int?(1);
            contract.PrjNum = ((this.hdnProjectCode.Value.ToLower().Trim() == null) ? string.Empty : this.hdnProjectCode.Value.ToLower().Trim());
        }
        if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
        {
            contract.reqDate = new DateTime?(Convert.ToDateTime(this.txtStartDate.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
        {
            contract.useDate = new DateTime?(Convert.ToDateTime(this.txtEndDate.Text.Trim()));
        }
        contract.reqType = new int?(int.Parse(this.DropDownList1.SelectedValue));
        this.HiddenField2.Value = this.DropDownList1.SelectedValue;
        if (this.RadioButton2.Checked)
        {
            contract.isInterest = false;
            contract.isDefault = false;
            contract.InterestNum = new decimal?(0m);
            return;
        }
        contract.isInterest = true;
        if (this.RadioButton3.Checked)
        {
            contract.isDefault = true;
            contract.InterestNum = new decimal?(Convert.ToDecimal(this.TextBox1.Text));
            return;
        }
        contract.isDefault = false;
        contract.InterestNum = new decimal?(Convert.ToDecimal(this.TextBox1.Text));
    }
    protected string GetProName(string pronum)
    {
        DataTable dataTable = SqlHelper.ExecuteQuery(CommandType.Text, "select *from dbo.PT_PrjInfo where PrjGuid='" + pronum.Trim() + "'", new SqlParameter[0]);
        if (dataTable.Rows.Count == 1)
        {
            return dataTable.Rows[0]["PrjName"].ToString();
        }
        return "错误项目代码";
    }
    protected string GetContrName(string contrnum)
    {
        PayoutContract payoutContract = new PayoutContract();
        PayoutContractModel model = payoutContract.GetModel(contrnum);
        if (model != null)
        {
            return contrnum = ((model.ContractName == null) ? "无" : model.ContractName);
        }
        return "无";
    }
    private void AddUserCodes(fund_ReqinfoModle contract)
    {
        List<string> list = new List<string>();
        list.Add(base.UserCode);
        if (base.UserCode != "00000000")
        {
            list.Add("00000000");
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.HiddenField2.Value = this.DropDownList1.SelectedValue;
    }
    protected void rabtnchange(object sender, EventArgs e)
    {
        accBaise accBaise = new accBaise();
        basieModel model = accBaise.GetModel(1);
        decimal num;
        if (model != null)
        {
            num = model.borrowRate.Value;
        }
        else
        {
            num = 0m;
        }
        if (this.RadioButton1.Checked)
        {
            this.isshow.Visible = true;
            this.RadioButton3.Enabled = true;
            this.RadioButton4.Enabled = true;
            this.TextBox1.Text = num.ToString();
            return;
        }
        this.RadioButton3.Enabled = false;
        this.RadioButton4.Enabled = false;
        this.isshow.Visible = false;
    }
    protected void RbtnIntertest(object sender, EventArgs e)
    {
        if (this.RadioButton3.Checked)
        {
            this.td1.Visible = false;
            this.td2.Visible = false;
            return;
        }
        this.td1.Visible = true;
        this.td2.Visible = true;
    }
}
