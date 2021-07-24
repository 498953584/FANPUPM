using ASP;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_UserControl_IncomeContract : System.Web.UI.UserControl
{
	public string ContractID
	{
		get
		{
			return this.hfldContractId.Value;
		}
		set
		{
			try
			{
				this.hfldContractId.Value = value;
				IncometContractBll incometContractBll = new IncometContractBll();
				IncometContractModel model = incometContractBll.GetModel(value);
				this.txtContractName.Text = model.ContractName;
			}
			catch (System.Exception)
			{
			}
		}
	}
	public string ContractName
	{
		get
		{
			return this.txtContractName.Text;
		}
	}
	public string Width
	{
		set
		{
			if (value.Contains("px"))
			{
				this.txtContractName.Width = System.Convert.ToInt32(value.Substring(0, value.Length - 2)) - 27;
				return;
			}
			this.txtContractName.Width = 120;
		}
	}
	public bool IsContainsImg
	{
		set
		{
			this.img.Visible = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
	}
}


