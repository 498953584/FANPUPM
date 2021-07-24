using ASP;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_UserControl_ContractType : System.Web.UI.UserControl
{

	public string TypeID
	{
		get
		{
			return this.hfldTypeID.Value;
		}
		set
		{
			this.hfldTypeID.Value = value;
			ContractType contractType = new ContractType();
			ContractTypeModel model = contractType.GetModel(value);
			if (model != null)
			{
				this.txtTypeName.Text = model.TypeName;
				return;
			}
			this.txtTypeName.Text = "";
		}
	}
	public string TypeName
	{
		get
		{
			return this.txtTypeName.Text.Trim();
		}
		set
		{
			this.txtTypeName.Text = value;
		}
	}
	public string CssClass
	{
		get
		{
			return this.txtTypeName.CssClass;
		}
		set
		{
			this.txtTypeName.CssClass = value;
		}
	}
	public string Width
	{
		set
		{
			if (value.Contains("px"))
			{
				this.txtTypeName.Width = System.Convert.ToInt32(value.Substring(0, value.Length - 2)) - 27;
				this.spanContractType.Style["width"] = System.Convert.ToInt32(value.Substring(0, value.Length - 2)) - 2 + "px";
				return;
			}
			if (value.Contains("%"))
			{
				this.hfldTypeID.Value = value;
				return;
			}
			this.txtTypeName.Width = 120;
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


