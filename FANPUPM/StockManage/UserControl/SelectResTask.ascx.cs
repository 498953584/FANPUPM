using ASP;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class StockManage_UserControl_SelectResTask : System.Web.UI.UserControl
{

	public string Text
	{
		get
		{
			return this.btnSelectResource.Text;
		}
		set
		{
			this.btnSelectResource.Text = value;
		}
	}
	public double Width
	{
		get
		{
			return this.btnSelectResource.Width.Value;
		}
		set
		{
			this.btnSelectResource.Width = new Unit(value);
		}
	}
	public string TypeCode
	{
		get
		{
			return this.hfldTypeCode.Value;
		}
		set
		{
			this.hfldTypeCode.Value = value;
		}
	}
	public string ButtonId
	{
		get
		{
			return this.hfldButtonId.Value;
		}
		set
		{
			this.hfldButtonId.Value = value;
		}
	}
	public string ResourceId
	{
		get
		{
			return this.hfldResourceId.Value;
		}
		set
		{
			this.hfldResourceId.Value = value;
		}
	}
	public string ResTempType
	{
		get
		{
			return this.hfldType.Value;
		}
		set
		{
			this.hfldType.Value = value;
		}
	}
	public string ResourceCode
	{
		get
		{
			return this.hfldResourceCode.Value;
		}
		set
		{
			this.hfldResourceCode.Value = value;
		}
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitResourceType();
		}
	}
	private void InitResourceType()
	{
	}
}


