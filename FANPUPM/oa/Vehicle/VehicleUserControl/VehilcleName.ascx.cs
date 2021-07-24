using ASP;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_VehicleUserControl_VehilcleName : System.Web.UI.UserControl
{
	public string TypeID
	{
		get
		{
			return this.hfldVehicleName.Value;
		}
		set
		{
			this.hfldVehicleName.Value = value;
		}
	}
	public string TypeName
	{
		get
		{
			return this.txtVehicleName.Text.Trim();
		}
		set
		{
			this.txtVehicleName.Text = value;
		}
	}
	public string CssClass
	{
		get
		{
			return this.txtVehicleName.CssClass;
		}
		set
		{
			this.txtVehicleName.CssClass = value;
		}
	}
	public string Width
	{
		set
		{
			if (value.Contains("px"))
			{
				this.txtVehicleName.Width = Convert.ToInt32(value.Substring(0, value.Length - 2)) - 27;
				this.spanVehicleName.Style["width"] = Convert.ToInt32(value.Substring(0, value.Length - 2)) - 2 + "px";
				return;
			}
			this.txtVehicleName.Width = 120;
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


