using ASP;
using cn.justwin.AccountManage.accBaise;
using cn.justwin.BLL;
using cn.justwin.stockBLL.AccountManage.accBaise;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_acc_Basic_basicManage : NBasePage, IRequiresSessionState
{
	private accBaise ab = new accBaise();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Bind();
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		basieModel basieModel = new basieModel();
		if (this.rdbPrj.Checked)
		{
			basieModel.accAllot = new int?(0);
		}
		else
		{
			if (this.rdbCon.Checked)
			{
				basieModel.accAllot = new int?(1);
			}
		}
		if (this.txtfundRatio.Text != "")
		{
			basieModel.fundRatio = new decimal?(Convert.ToDecimal(this.txtfundRatio.Text));
		}
		else
		{
			basieModel.fundRatio = new decimal?(0m);
		}
		if (this.rdbIsRate.Checked)
		{
			basieModel.isRate = new int?(1);
		}
		else
		{
			if (this.rdbNoRate.Checked)
			{
				basieModel.isRate = new int?(0);
				basieModel.borrowRate = new decimal?(0m);
			}
		}
		if (this.txtborrowRate.Text != "")
		{
			basieModel.borrowRate = new decimal?(Convert.ToDecimal(this.txtborrowRate.Text));
		}
		else
		{
			basieModel.borrowRate = new decimal?(0m);
		}
		if (this.rdbAuthPrj.Checked)
		{
			basieModel.authority = new int?(0);
		}
		else
		{
			if (this.rdbAuthHand.Checked)
			{
				basieModel.authority = new int?(1);
			}
		}
		if (this.rdbPrjMoney.Checked)
		{
			basieModel.startMoney = new int?(0);
		}
		else
		{
			if (this.rdbCouMoney.Checked)
			{
				basieModel.startMoney = new int?(1);
			}
		}
		basieModel.id = 1;
		int num = this.ab.upBaise(basieModel);
		if (num > 0)
		{
			base.RegisterScript("alert('系统提示：\\n\\修改成功！');");
			return;
		}
		base.RegisterScript("alert('系统提示：\\n\\修改失败！');");
	}
	public void Bind()
	{
		basieModel model = this.ab.GetModel(1);
		if (model.accAllot == 0)
		{
			this.rdbPrj.Checked = true;
		}
		else
		{
			if (model.accAllot == 1)
			{
				this.rdbCon.Checked = true;
			}
		}
		this.txtfundRatio.Text = model.fundRatio.ToString();
		if (model.startMoney == 0)
		{
			this.rdbPrjMoney.Checked = true;
		}
		else
		{
			if (model.startMoney == 1)
			{
				this.rdbCouMoney.Checked = true;
			}
		}
		if (model.isRate == 1)
		{
			this.rdbIsRate.Checked = true;
			this.txtborrowRate.Text = model.borrowRate.ToString();
		}
		else
		{
			if (model.isRate == 0)
			{
				this.rdbNoRate.Checked = true;
				base.ClientScript.RegisterStartupScript(base.ClientScript.GetType(), "myscript", "<script>rdbNoRateClick();</script>");
			}
		}
		if (model.authority == 0)
		{
			this.rdbAuthPrj.Checked = true;
			return;
		}
		if (model.authority == 1)
		{
			this.rdbAuthHand.Checked = true;
		}
	}
	protected void btnCancel_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
}


