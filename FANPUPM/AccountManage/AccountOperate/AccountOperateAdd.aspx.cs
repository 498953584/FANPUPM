using AccountManage.BLL;
using AccountManage.Model;
using ASP;
using cn.justwin.AccountManage.accBaise;
using cn.justwin.AccountManage.prjaccount;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using cn.justwin.stockBLL.AccountManage.accBaise;
using cn.justwin.stockModel;
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
public partial class AccountManage_AccountOperate_AccountOperateAdd : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractID = string.Empty;
	private fund_AccountOperateBLL bll = new fund_AccountOperateBLL();

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
			accBaise accBaise = new accBaise();
			basieModel basieModel = new basieModel();
			basieModel = accBaise.GetModel(1);
			if (basieModel != null)
			{
				if (basieModel.accAllot == 0)
				{
					this.pr3.Visible = false;
					this.pr4.Visible = false;
					this.pr1.Visible = true;
					this.pr2.Visible = true;
				}
				else
				{
					this.pr1.Visible = false;
					this.pr2.Visible = false;
					this.pr3.Visible = true;
					this.pr4.Visible = true;
				}
			}
			this.DataBindDrop();
			if (string.Compare(this.action, "Add", true) == 0)
			{
				this.InitAdd();
				return;
			}
			this.InitUpdateAndQuery();
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
	protected void GetAccountNum()
	{
		new prjaccountModel();
		accountMange accountMange = new accountMange();
		if (!string.IsNullOrEmpty(this.hdfcontrcn.Value))
		{
			accountMange.GetTable(" contractNum='" + this.hdfcontrcn.Value + "'");
		}
	}
	private void InitAdd()
	{
		this.txtContractCode.Text = Guid.NewGuid().ToString().ToLower();
		this.hdfusercode.Value = base.UserCode;
		this.txtUsercode.Value = PageHelper.QueryUser(this, this.hdfusercode.Value);
	}
	private void InitUpdateAndQuery()
	{
		fund_AccountOperateModle model = this.bll.GetModel(int.Parse(this.contractID));
		this.DateBox1.Text = model.SumiTime.ToString();
		this.txtUsercode.Value = PageHelper.QueryUser(this, model.SumitMan);
		this.txtContractMoney.Value = model.AccountMony.ToString();
		this.DropDownList1.SelectedValue = model.AccounType.ToString();
		this.txtContractCode.Text = model.Acredence;
		this.hdfproject.Value = model.projnum;
		this.hdfcontrcn.Value = model.contracnum;
		model.AccountMan = string.Empty;
		this.hdfaccount.Value = model.AccountNum.ToString();
		this.hdfcontrcn.Value = model.contracnum.ToString();
		this.hdfproject.Value = model.projnum.ToString();
		this.txtProject.Value = this.GetProName(model.projnum.ToString());
		this.txtContr.Value = this.GetContrName(model.contracnum.ToString());
		this.txtaccount.Value = this.GetAccount(model.AccountNum.ToString());
		this.txtNotes.Text = model.AccountMark.ToString();
	}
	private void DataBindDrop()
	{
	}
	private void AddContract()
	{
		fund_AccountOperateModle contract = new fund_AccountOperateModle();
		this.InitContract(contract);
		this.Add(contract);
	}
	private void Add(fund_AccountOperateModle contract)
	{
		try
		{
			if (this.bll.Exists(contract.Acredence))
			{
				base.RegisterScript("alert('系统提示：\\n\\n此凭证编号已经存在')");
			}
			else
			{
				this.bll.Add(contract);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("alert('系统提示：\\n\\n添加成功')").Append(Environment.NewLine);
				stringBuilder.Append("winclose('fund_ReqinfoEdit', 'AccountOperate.aspx', true)");
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
		fund_AccountOperateModle model = this.bll.GetModel(int.Parse(this.contractID));
		this.InitContract(model);
		try
		{
			this.bll.Update(model);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("alert('系统提示：\\n\\n修改成功')").Append(Environment.NewLine);
			stringBuilder.Append("winclose('fund_ReqinfoEdit', 'AccountOperate.aspx', true)");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch (Exception)
		{
			base.RegisterScript("alert('系统提示：\\n\\n修改失败')");
		}
	}
	private void InitContract(fund_AccountOperateModle contract)
	{
		if (!string.IsNullOrEmpty(this.contractID))
		{
			contract.AoID = Convert.ToInt32(this.contractID);
		}
		contract.SumiTime = new DateTime?(Convert.ToDateTime(this.DateBox1.Text.Trim()));
		contract.SumitMan = base.UserCode;
		contract.AccountMony = new decimal?(Convert.ToDecimal(this.txtContractMoney.Value.Trim()));
		contract.RealMony = contract.AccountMony;
		contract.AccounType = new int?(int.Parse(this.DropDownList1.SelectedValue));
		contract.Acredence = this.txtContractCode.Text.Trim();
		contract.IsAccount = new int?(0);
		contract.projnum = this.hdfproject.Value.Trim();
		contract.contracnum = this.hdfcontrcn.Value.Trim();
		contract.AccountMan = string.Empty;
		contract.AccountNum = this.hdfaccount.Value.Trim();
		contract.AccountMark = this.txtNotes.Text.Trim();
		PtYhmc ptYhmc = new PtYhmc();
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		ptYhmc = ptYhmcBll.GetModelById(base.UserCode);
		contract.DepID = ptYhmc.i_bmdm.ToString();
	}
	protected string GetProName(string pronum)
	{
		if (string.IsNullOrEmpty(pronum))
		{
			return "错误项目编号";
		}
		DataTable dataTable = SqlHelper.ExecuteQuery(CommandType.Text, "select *from dbo.PT_PrjInfo where PrjGuid='" + pronum.Trim() + "'", new SqlParameter[0]);
		if (dataTable.Rows.Count == 1)
		{
			return dataTable.Rows[0]["PrjName"].ToString();
		}
		return "错误项目编号";
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
	protected string GetAccount(string accounum)
	{
		accountMange accountMange = new accountMange();
		prjaccountModel prjaccountModel = new prjaccountModel();
		prjaccountModel = accountMange.GetModelByConId(accounum);
		if (prjaccountModel != null)
		{
			return prjaccountModel.accountNum;
		}
		return "";
	}
	protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}
}


