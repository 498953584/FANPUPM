using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class CommonWindow_CodeDictionary_CodeDictionaryEdit : BasePage, IRequiresSessionState
{

	private string Type
	{
		get
		{
			return this.ViewState["YTPE"].ToString();
		}
		set
		{
			this.ViewState["YTPE"] = value;
		}
	}
	protected PTDictionaryItemAction pia
	{
		get
		{
			return new PTDictionaryItemAction();
		}
	}
	private int rid
	{
		get
		{
			return (int)this.ViewState["RID"];
		}
		set
		{
			this.ViewState["RID"] = value;
		}
	}
	protected int ClassCode
	{
		get
		{
			return (int)this.ViewState["CLASSCODE"];
		}
		set
		{
			this.ViewState["CLASSCODE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && (base.Request["rid"] != null || base.Request["t"] != null || base.Request["cc"] != null))
		{
			this.rid = Convert.ToInt32(base.Request["rid"]);
			this.Type = base.Request["t"].ToString();
			this.ClassCode = Convert.ToInt32(base.Request["cc"]);
			if (this.Type != "Add")
			{
				this.GetPageData();
			}
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.Type == "Add")
		{
			if (this.pia.Add(this.getPTDictionaryItem()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
		else
		{
			if (this.pia.Update(this.getPTDictionaryItem()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
	}
	protected PTDictionaryItem getPTDictionaryItem()
	{
		return new PTDictionaryItem
		{
			ClassID = this.ClassCode,
			DisplayValue = this.txtDisplayValue.Text,
			IsValid = this.CBIsValid.Checked ? "1" : "0",
			KeyValue = this.rid
		};
	}
	protected void GetPageData()
	{
		PTDictionaryItem model = this.pia.GetModel(this.rid);
		this.txtDisplayValue.Text = model.DisplayValue;
		this.CBIsValid.Checked = !(model.IsValid == "0");
	}
}


