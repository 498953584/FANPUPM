using ASP;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Purchase_PickTreasury : System.Web.UI.UserControl
{

	public string TreasuryCode
	{
		get
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(this.hfldTreasuryCode.Value))
			{
				result = this.hfldTreasuryCode.Value;
			}
			return result;
		}
		set
		{
			this.hfldTreasuryCode.Value = value;
			Treasury treasury = new Treasury();
			TreasuryModel model = treasury.GetModel(value);
			this.txtTreasuryName.Text = model.tname;
		}
	}
	public string TreasuryName
	{
		get
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(this.txtTreasuryName.Text.Trim()))
			{
				result = this.txtTreasuryName.Text.Trim();
			}
			return result;
		}
		set
		{
			this.txtTreasuryName.Text = value;
		}
	}
	public bool IsContainsImg
	{
		set
		{
			this.img.Visible = value;
		}
	}
	public string Width
	{
		set
		{
			if (value.Contains("px"))
			{
				this.spanContractType.Style["width"] = Convert.ToInt32(value.Substring(0, value.Length - 2)) - 2 + "px";
				this.txtTreasuryName.Width = Convert.ToInt32(value.Substring(0, value.Length - 2)) - 27;
				return;
			}
			if (value.Contains("%"))
			{
				this.hfldWidth.Value = value;
				return;
			}
			this.txtTreasuryName.Width = 120;
		}
	}
	public string Module
	{
		set
		{
			this.img.Attributes.Add("Module", value);
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
	}
}


