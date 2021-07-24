using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using System;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_IEPInfo_IEPInfoEdit : NBasePage, IRequiresSessionState
{
	private IEPInfoBLL infoBLL = new IEPInfoBLL();
	private InExPlanBLL IEPBLL = new InExPlanBLL();
	private PtYhmcBll yhmc = new PtYhmcBll();
	private IncometContractBll incometConBLL = new IncometContractBll();
	private PayoutContract payoutCon = new PayoutContract();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.rdbTypeIn.Checked = true;
			this.txtUser.Text = this.yhmc.GetModelById(base.UserCode).v_xm;
			this.txtData.Text = DateTime.Now.ToShortDateString();
			if (base.Request["type"].ToString() == "add")
			{
				this.txtInfoCode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
				this.hdfID.Value = Guid.NewGuid().ToString();
			}
			else
			{
				if (base.Request["type"].ToString() == "edit")
				{
					this.hdfID.Value = base.Request["id"].ToString();
					this.bind();
				}
			}
			string id = base.Request["IEPid"].ToString();
			InExPlanModel model = this.IEPBLL.GetModel(id);
			this.txtIEPcode.Text = model.IEPNum;
			this.txtIEPname.Text = model.IEPname;
		}
	}
	public void bind()
	{
		string value = this.hdfID.Value;
		IEPInfoModel model = this.infoBLL.GetModel(value);
		this.txtInfoCode.Text = model.infoNum;
		if (model.type == 0)
		{
			this.rdbTypeIn.Checked = true;
			this.rdbTypeOut.Checked = false;
		}
		else
		{
			if (model.type == 1)
			{
				this.rdbTypeOut.Checked = true;
				this.rdbTypeIn.Checked = false;
			}
		}
		IncometContractModel model2 = this.incometConBLL.GetModel(model.conId);
		if (model2 != null)
		{
			this.txtContract.Text = model2.ContractName;
			this.hfldContract.Value = model2.ContractID;
		}
		else
		{
			PayoutContractModel model3 = this.payoutCon.GetModel(model.conId);
			this.txtContract.Text = model3.ContractName;
			this.hfldContract.Value = model3.ContractID;
		}
		this.txtMoney.Text = model.Money.ToString();
		this.txtData.Text = model.addDate.ToString();
		this.txtUser.Text = this.yhmc.GetModelById(model.addPeople).v_xm;
		this.txtRemark.Text = model.remark;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		string str = "";
		try
		{
			IEPInfoModel iEPInfoModel = new IEPInfoModel();
			iEPInfoModel.ID = this.hdfID.Value;
			iEPInfoModel.infoNum = this.txtInfoCode.Text;
			iEPInfoModel.IEPid = base.Request["IEPid"].ToString();
			if (this.rdbTypeIn.Checked)
			{
				iEPInfoModel.type = new int?(0);
			}
			else
			{
				if (this.rdbTypeOut.Checked)
				{
					iEPInfoModel.type = new int?(1);
				}
			}
			if (this.txtMoney.Text.ToString() != "")
			{
				iEPInfoModel.Money = new decimal?(Convert.ToDecimal(this.txtMoney.Text.ToString()));
			}
			iEPInfoModel.conId = this.hfldContract.Value;
			if (this.txtData.Text.ToString() != "")
			{
				iEPInfoModel.addDate = new DateTime?(Convert.ToDateTime(this.txtData.Text.ToString()));
			}
			iEPInfoModel.addPeople = base.UserCode;
			iEPInfoModel.remark = this.txtRemark.Text;
			if (base.Request["type"].ToString() == "add")
			{
				this.infoBLL.Add(iEPInfoModel);
				str = "添加";
			}
			else
			{
				this.infoBLL.Update(iEPInfoModel);
				str = "修改";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("parent.desktop.flowclass.location='/AccountManage/IEPInfo/IEPInfoList.aspx?&id=" + base.Request["IEPid"].ToString() + "';");
			stringBuilder.Append("alert('系统提示：\\n\\n" + str + "成功！');").Append(Environment.NewLine);
			stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n对不起" + str + "失败！');");
		}
	}
}


