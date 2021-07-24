﻿using ASP;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_VehicleUserControl_OaUserControl : System.Web.UI.UserControl
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
		}
	}
	public string TypeName
	{
		get
		{
			return this.txtPurchaser.Text.Trim();
		}
		set
		{
			this.txtPurchaser.Text = value;
		}
	}
	public string CssClass
	{
		get
		{
			return this.txtPurchaser.CssClass;
		}
		set
		{
			this.txtPurchaser.CssClass = value;
		}
	}
	public string Width
	{
		set
		{
			if (value.Contains("px"))
			{
				this.txtPurchaser.Width = Convert.ToInt32(value.Substring(0, value.Length - 2)) - 27;
				this.spanContractType.Style["width"] = Convert.ToInt32(value.Substring(0, value.Length - 2)) - 2 + "px";
				return;
			}
			this.txtPurchaser.Width = 120;
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


