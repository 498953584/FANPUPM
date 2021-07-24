using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryItemEdit : BasePage, IRequiresSessionState
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
	private SalaryItemAction sia
	{
		get
		{
			return new SalaryItemAction();
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
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && (base.Request["rid"] != null || base.Request["t"] != null))
		{
			this.rid = Convert.ToInt32(base.Request["rid"]);
			this.Type = base.Request["t"].ToString();
			if (this.Type != "Add")
			{
				this.GetPageData();
			}
			DataTable list = this.sia.GetList(" ItemType = 0");
			if (list.Rows.Count > 0)
			{
				this.DDLItemType.SelectedValue = "1";
			}
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.Type == "Add")
		{
			if (this.sia.Add(this.getSalaryItemModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
		else
		{
			if (this.sia.Update(this.getSalaryItemModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
	}
	protected SalaryItemModel getSalaryItemModel()
	{
		return new SalaryItemModel
		{
			ItemID = this.rid,
			ItemName = this.txtItemName.Text,
			ItemType = this.DDLItemType.SelectedValue,
			IsValid = this.RBIsValid.SelectedValue
		};
	}
	protected void GetPageData()
	{
		SalaryItemModel model = this.sia.GetModel(this.rid);
		this.txtItemName.Text = model.ItemName;
		this.DDLItemType.SelectedValue = model.ItemType;
		this.RBIsValid.SelectedValue = model.IsValid;
	}
}


