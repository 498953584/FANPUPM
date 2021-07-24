using ASP;
using cn.justwin.Fund.FundConfig;
using cn.justwin.stockBLL.Fund.FundConfig;
using com.jwsoft.common.baseclass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_config : NBasePage, IRequiresSessionState
{
	private configBll BLL = new configBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Bind();
			this.hldfIsFundWarn.Value = ConfigurationManager.AppSettings["IsFundWarn"];
		}
	}
	protected void butSave_Click(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToInt32(this.txtBeginDate.Text.ToString()) < 28 && Convert.ToInt32(this.txtBeginDate.Text.ToString()) > 0)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("isBeginDate", this.GetIntFromBool(this.chkisBeginDate.Checked));
				dictionary.Add("BeginDate", this.txtBeginDate.Text.Trim());
				dictionary.Add("isEndDate", this.GetIntFromBool(this.chkisEndDate.Checked));
				if (this.rdbisEndNowMonthT.Checked)
				{
					dictionary.Add("isEndNowMonth", "1");
				}
				else
				{
					if (this.rdbisEndNowMonthN.Checked)
					{
						dictionary.Add("isEndNowMonth", "0");
					}
				}
				dictionary.Add("EndDate", this.txtEndDate.Text.Trim());
				dictionary.Add("isPayAccount", this.GetIntFromBool(this.chkisPayAccount.Checked));
				dictionary.Add("isHightPayAccountRank", this.GetIntFromBool(this.chkisHightPayAccountRank.Checked));
				dictionary.Add("HightPayAccountRank", this.txtHightPayAccountRank.Text.Trim());
				dictionary.Add("isMidPayAccountRank", this.GetIntFromBool(this.chkisMidPayAccountRank.Checked));
				dictionary.Add("BeginMidPayAccountRank", this.txtBeginMidPayAccountRank.Text.Trim());
				dictionary.Add("EndMidPayAccountRank", this.txtEndMidPayAccountRank.Text.Trim());
				dictionary.Add("isLowPayAccountRank", this.GetIntFromBool(this.chkisLowPayAccountRank.Checked));
				dictionary.Add("BeginLowPayAccountRank", this.txtBeginLowPayAccountRank.Text.Trim());
				dictionary.Add("EndLowPayAccountRank", this.txtEndLowPayAccountRank.Text.Trim());
				dictionary.Add("isIcAccount", this.GetIntFromBool(this.chkisIcAccount.Checked));
				dictionary.Add("isHightIcAccountRank", this.GetIntFromBool(this.chkisHightIcAccountRank.Checked));
				dictionary.Add("HightIcAccountRank", this.txtHightIcAccountRank.Text.Trim());
				dictionary.Add("isMidIcAccountRank", this.GetIntFromBool(this.chkisMidIcAccountRank.Checked));
				dictionary.Add("BeginMidIcAccountRank", this.txtBeginMidIcAccountRank.Text.Trim());
				dictionary.Add("EndMidIcAccountRank", this.txtEndMidIcAccountRank.Text.Trim());
				dictionary.Add("isLowIcAccountRank", this.GetIntFromBool(this.chkisLowIcAccountRank.Checked));
				dictionary.Add("BeginLowIcAccountRank", this.txtBeginLowIcAccountRank.Text.Trim());
				dictionary.Add("EndLowIcAccountRank", this.txtEndLowIcAccountRank.Text.Trim());
				dictionary.Add("isMidLoanAccountRank", this.GetIntFromBool(this.chkisMidLoanAccountRank.Checked));
				dictionary.Add("isHightLoanAccountRank", this.GetIntFromBool(this.chkisHightLoanAccountRank.Checked));
				dictionary.Add("BeginLowLoanAccountRank", this.txtBeginLowLoanAccountRank.Text.Trim());
				dictionary.Add("isLowLoanAccountRank", this.GetIntFromBool(this.chkisLowLoanAccountRank.Checked));
				dictionary.Add("BeginMidLoanAccountRank", this.txtBeginMidLoanAccountRank.Text.Trim());
				dictionary.Add("EndLowLoanAccountRank", this.txtEndLowLoanAccountRank.Text.Trim());
				dictionary.Add("isLoanAccount", this.GetIntFromBool(this.chkisLoanAccount.Checked));
				dictionary.Add("HightLoanAccountRank", this.txtHightLoanAccountRank.Text.Trim());
				dictionary.Add("EndMidLoanAccountRank", this.txtEndMidLoanAccountRank.Text.Trim());
				try
				{
					this.BLL.Update(dictionary);
					base.RegisterScript("hideFundWarn();hidePlanEndDate();alert('系统提示:\\n\\n更新成功')");
					goto IL_421;
				}
				catch (Exception ex)
				{
					StringBuilder stringBuilder = new StringBuilder("hideFundWarn();hidePlanEndDate();alert('系统提示:\\n\\n");
					stringBuilder.Append(ex.Message).Append("')");
					base.RegisterScript(stringBuilder.ToString());
					goto IL_421;
				}
			}
			base.RegisterScript("hideFundWarn();hidePlanEndDate();alert('系统提示:\\n\\n请输入1-28之间的数字')");
			IL_421:;
		}
		catch
		{
		}
	}
	private void Bind()
	{
		List<configModel> list = this.BLL.GetList("");
		foreach (configModel current in list)
		{
			if (current.ParaName == "BeginMidLoanAccountRank")
			{
				this.txtBeginMidLoanAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "BeginLowPayAccountRank")
			{
				this.txtBeginLowPayAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "BeginLowLoanAccountRank")
			{
				this.txtBeginLowLoanAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "BeginMidIcAccountRank")
			{
				this.txtBeginMidIcAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "EndLowLoanAccountRank")
			{
				this.txtEndLowLoanAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "HightPayAccountRank")
			{
				this.txtHightPayAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "EndMidPayAccountRank")
			{
				this.txtEndMidPayAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "BeginMidPayAccountRank")
			{
				this.txtBeginMidPayAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "BeginDate")
			{
				this.txtBeginDate.Text = current.ParaValue;
			}
			if (current.ParaName == "HightLoanAccountRank")
			{
				this.txtHightLoanAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "EndMidLoanAccountRank")
			{
				this.txtEndMidLoanAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "EndDate")
			{
				this.txtEndDate.Text = current.ParaValue;
			}
			if (current.ParaName == "EndMidIcAccountRank")
			{
				this.txtEndMidIcAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "HightIcAccountRank")
			{
				this.txtHightIcAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "BeginLowIcAccountRank")
			{
				this.txtBeginLowIcAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "EndLowIcAccountRank")
			{
				this.txtEndLowIcAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "EndLowPayAccountRank")
			{
				this.txtEndLowPayAccountRank.Text = current.ParaValue;
			}
			if (current.ParaName == "isHightPayAccountRank")
			{
				if (current.ParaValue == "1")
				{
					this.chkisHightPayAccountRank.Checked = true;
				}
				else
				{
					this.chkisHightPayAccountRank.Checked = false;
				}
			}
			if (current.ParaName == "isEndNowMonth")
			{
				if (current.ParaValue == "1")
				{
					this.rdbisEndNowMonthT.Checked = true;
					this.rdbisEndNowMonthN.Checked = false;
				}
				else
				{
					this.rdbisEndNowMonthT.Checked = false;
					this.rdbisEndNowMonthN.Checked = true;
				}
			}
			if (current.ParaName == "isLowPayAccountRank")
			{
				if (current.ParaValue == "1")
				{
					this.chkisLowPayAccountRank.Checked = true;
				}
				else
				{
					this.chkisLowPayAccountRank.Checked = false;
				}
			}
			if (current.ParaName == "isLoanAccount")
			{
				if (current.ParaValue == "1")
				{
					this.chkisLoanAccount.Checked = true;
				}
				else
				{
					this.chkisLoanAccount.Checked = false;
				}
			}
			if (current.ParaName == "isMidIcAccountRank")
			{
				if (current.ParaValue == "1")
				{
					this.chkisMidIcAccountRank.Checked = true;
				}
				else
				{
					this.chkisMidIcAccountRank.Checked = false;
				}
			}
			if (current.ParaName == "isHightIcAccountRank")
			{
				if (current.ParaValue == "1")
				{
					this.chkisHightIcAccountRank.Checked = true;
				}
				else
				{
					this.chkisHightIcAccountRank.Checked = false;
				}
			}
			if (current.ParaName == "isBeginDate")
			{
				if (current.ParaValue == "1")
				{
					this.chkisBeginDate.Checked = true;
				}
				else
				{
					this.chkisBeginDate.Checked = false;
				}
			}
			if (current.ParaName == "isMidPayAccountRank")
			{
				if (current.ParaValue == "1")
				{
					this.chkisMidPayAccountRank.Checked = true;
				}
				else
				{
					this.chkisMidPayAccountRank.Checked = false;
				}
			}
			if (current.ParaName == "isMidLoanAccountRank")
			{
				if (current.ParaValue == "1")
				{
					this.chkisMidLoanAccountRank.Checked = true;
				}
				else
				{
					this.chkisMidLoanAccountRank.Checked = false;
				}
			}
			if (current.ParaName == "isHightLoanAccountRank")
			{
				if (current.ParaValue == "1")
				{
					this.chkisHightLoanAccountRank.Checked = true;
				}
				else
				{
					this.chkisHightLoanAccountRank.Checked = false;
				}
			}
			if (current.ParaName == "isLowLoanAccountRank")
			{
				if (current.ParaValue == "1")
				{
					this.chkisLowLoanAccountRank.Checked = true;
				}
				else
				{
					this.chkisLowLoanAccountRank.Checked = false;
				}
			}
			if (current.ParaName == "isPayAccount")
			{
				if (current.ParaValue == "1")
				{
					this.chkisPayAccount.Checked = true;
				}
				else
				{
					this.chkisPayAccount.Checked = false;
				}
			}
			if (current.ParaName == "isEndDate")
			{
				if (current.ParaValue == "1")
				{
					this.chkisEndDate.Checked = true;
				}
				else
				{
					this.chkisEndDate.Checked = false;
				}
			}
			if (current.ParaName == "isLowIcAccountRank")
			{
				if (current.ParaValue == "1")
				{
					this.chkisLowIcAccountRank.Checked = true;
				}
				else
				{
					this.chkisLowIcAccountRank.Checked = false;
				}
			}
			if (current.ParaName == "isIcAccount")
			{
				if (current.ParaValue == "1")
				{
					this.chkisIcAccount.Checked = true;
				}
				else
				{
					this.chkisIcAccount.Checked = false;
				}
			}
		}
	}
	protected void butCancel_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	private string GetIntFromBool(bool b)
	{
		if (b)
		{
			return "1";
		}
		return "0";
	}
	protected void chkisBeginDate_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisBeginDate.Checked)
		{
			this.txtBeginDate.Enabled = true;
			return;
		}
		this.txtBeginDate.Enabled = false;
	}
	protected void chkisEndDate_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisEndDate.Checked)
		{
			this.rdbisEndNowMonthT.Enabled = true;
			this.rdbisEndNowMonthN.Enabled = true;
			this.txtEndDate.Enabled = true;
			return;
		}
		this.rdbisEndNowMonthT.Enabled = false;
		this.rdbisEndNowMonthN.Enabled = false;
		this.txtEndDate.Enabled = false;
	}
	protected void rdbisEndNowMonthT_CheckedChanged(object sender, EventArgs e)
	{
		if (this.rdbisEndNowMonthT.Checked)
		{
			this.txtEndDate.Enabled = false;
		}
	}
	protected void rdbisEndNowMonthN_CheckedChanged(object sender, EventArgs e)
	{
		if (this.rdbisEndNowMonthN.Checked)
		{
			this.txtEndDate.Enabled = true;
		}
	}
	protected void chkisPayAccount_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisPayAccount.Checked)
		{
			this.chkisHightPayAccountRank.Checked = true;
			this.chkisMidPayAccountRank.Checked = true;
			this.chkisLowPayAccountRank.Checked = true;
			this.txtHightPayAccountRank.Enabled = true;
			this.txtBeginMidPayAccountRank.Enabled = true;
			this.txtEndMidPayAccountRank.Enabled = true;
			this.txtBeginLowPayAccountRank.Enabled = true;
			this.txtEndLowPayAccountRank.Enabled = true;
			return;
		}
		this.chkisHightPayAccountRank.Checked = false;
		this.chkisMidPayAccountRank.Checked = false;
		this.chkisLowPayAccountRank.Checked = false;
		this.txtHightPayAccountRank.Enabled = false;
		this.txtBeginMidPayAccountRank.Enabled = false;
		this.txtEndMidPayAccountRank.Enabled = false;
		this.txtBeginLowPayAccountRank.Enabled = false;
		this.txtEndLowPayAccountRank.Enabled = false;
	}
	protected void chkisHightPayAccountRank_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisHightPayAccountRank.Checked)
		{
			this.txtHightPayAccountRank.Enabled = true;
			return;
		}
		this.txtHightPayAccountRank.Enabled = false;
	}
	protected void chkisMidPayAccountRank_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisMidPayAccountRank.Checked)
		{
			this.txtBeginMidPayAccountRank.Enabled = true;
			this.txtEndMidPayAccountRank.Enabled = true;
			return;
		}
		this.txtBeginMidPayAccountRank.Enabled = false;
		this.txtEndMidPayAccountRank.Enabled = false;
	}
	protected void chkisLowPayAccountRank_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisLowPayAccountRank.Checked)
		{
			this.txtBeginLowPayAccountRank.Enabled = true;
			this.txtEndLowPayAccountRank.Enabled = true;
			return;
		}
		this.txtBeginLowPayAccountRank.Enabled = false;
		this.txtEndLowPayAccountRank.Enabled = false;
	}
	protected void chkisIcAccount_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisIcAccount.Checked)
		{
			this.chkisHightIcAccountRank.Checked = true;
			this.chkisMidIcAccountRank.Checked = true;
			this.chkisLowIcAccountRank.Checked = true;
			this.txtHightIcAccountRank.Enabled = true;
			this.txtBeginMidIcAccountRank.Enabled = true;
			this.txtEndMidIcAccountRank.Enabled = true;
			this.txtBeginLowIcAccountRank.Enabled = true;
			this.txtEndLowIcAccountRank.Enabled = true;
			return;
		}
		this.chkisHightIcAccountRank.Checked = false;
		this.chkisMidIcAccountRank.Checked = false;
		this.chkisLowIcAccountRank.Checked = false;
		this.txtHightIcAccountRank.Enabled = false;
		this.txtBeginMidIcAccountRank.Enabled = false;
		this.txtEndMidIcAccountRank.Enabled = false;
		this.txtBeginLowIcAccountRank.Enabled = false;
		this.txtEndLowIcAccountRank.Enabled = false;
	}
	protected void chkisHightIcAccountRank_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisHightIcAccountRank.Checked)
		{
			this.txtHightIcAccountRank.Enabled = true;
			return;
		}
		this.txtHightIcAccountRank.Enabled = false;
	}
	protected void chkisMidIcAccountRank_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisMidIcAccountRank.Checked)
		{
			this.txtBeginMidIcAccountRank.Enabled = true;
			this.txtEndMidIcAccountRank.Enabled = true;
			return;
		}
		this.txtBeginMidIcAccountRank.Enabled = false;
		this.txtEndMidIcAccountRank.Enabled = false;
	}
	protected void chkisLowIcAccountRank_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisLowIcAccountRank.Checked)
		{
			this.txtBeginLowIcAccountRank.Enabled = true;
			this.txtEndLowIcAccountRank.Enabled = true;
			return;
		}
		this.txtBeginLowIcAccountRank.Enabled = false;
		this.txtEndLowIcAccountRank.Enabled = false;
	}
	protected void chkisLoanAccount_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisLoanAccount.Checked)
		{
			this.chkisHightLoanAccountRank.Checked = true;
			this.chkisMidLoanAccountRank.Checked = true;
			this.chkisLowLoanAccountRank.Checked = true;
			this.txtHightLoanAccountRank.Enabled = true;
			this.txtBeginMidLoanAccountRank.Enabled = true;
			this.txtEndMidLoanAccountRank.Enabled = true;
			this.txtBeginLowLoanAccountRank.Enabled = true;
			this.txtEndLowLoanAccountRank.Enabled = true;
			return;
		}
		this.chkisHightLoanAccountRank.Checked = false;
		this.chkisMidLoanAccountRank.Checked = false;
		this.chkisLowLoanAccountRank.Checked = false;
		this.txtHightLoanAccountRank.Enabled = false;
		this.txtBeginMidLoanAccountRank.Enabled = false;
		this.txtEndMidLoanAccountRank.Enabled = false;
		this.txtBeginLowLoanAccountRank.Enabled = false;
		this.txtEndLowLoanAccountRank.Enabled = false;
	}
	protected void chkisHightLoanAccountRank_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisHightLoanAccountRank.Checked)
		{
			this.txtHightLoanAccountRank.Enabled = true;
			return;
		}
		this.txtHightLoanAccountRank.Enabled = false;
	}
	protected void chkisMidLoanAccountRank_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisMidLoanAccountRank.Checked)
		{
			this.txtBeginMidLoanAccountRank.Enabled = true;
			this.txtEndMidLoanAccountRank.Enabled = true;
			return;
		}
		this.txtBeginMidLoanAccountRank.Enabled = false;
		this.txtEndMidLoanAccountRank.Enabled = false;
	}
	protected void chkisLowLoanAccountRank_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkisLowLoanAccountRank.Checked)
		{
			this.txtBeginLowLoanAccountRank.Enabled = true;
			this.txtEndLowLoanAccountRank.Enabled = true;
			return;
		}
		this.txtBeginLowLoanAccountRank.Enabled = false;
		this.txtEndLowLoanAccountRank.Enabled = false;
	}
}


