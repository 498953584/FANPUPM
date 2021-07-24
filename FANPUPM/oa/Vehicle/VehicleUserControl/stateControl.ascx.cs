using ASP;
using cn.justwin.VehiclesBLL;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_VehicleUserControl_stateControl : System.Web.UI.UserControl
{
	public string TypeID
	{
		get
		{
			return this.hfldStateID.Value;
		}
		set
		{
			this.hfldStateID.Value = value;
			TypeAndStateBll typeAndStateBll = new TypeAndStateBll();
			if (typeAndStateBll.Exists(new Guid(value)))
			{
				this.txtVehicleState.Text = typeAndStateBll.GetModel(new Guid(value)).Name;
			}
		}
	}
	public string TypeName
	{
		get
		{
			return this.txtVehicleState.Text.Trim();
		}
		set
		{
			this.txtVehicleState.Text = value;
		}
	}
	public string CssClass
	{
		get
		{
			return this.txtVehicleState.CssClass;
		}
		set
		{
			this.txtVehicleState.CssClass = value;
		}
	}
	public string Width
	{
		set
		{
			if (value.Contains("px"))
			{
				this.txtVehicleState.Width = Convert.ToInt32(value.Substring(0, value.Length - 2)) - 27;
				this.spanContractType.Style["width"] = Convert.ToInt32(value.Substring(0, value.Length - 2)) - 2 + "px";
				return;
			}
			this.txtVehicleState.Width = 120;
		}
	}
	public bool IsContainsImg
	{
		set
		{
			this.img.Visible = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
	}
}


