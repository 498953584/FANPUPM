using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.stockBLL.Domain;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Construct_ResTypeCostAllocation : NBasePage, IRequiresSessionState
{

	protected override void OnInit(System.EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			System.Collections.Generic.List<CostAccounting> byD = CostAccounting.GetByD();
			this.ViewState["CBSList"] = byD;
			this.DataBindResType();
		}
	}
	protected void gvwResType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwResType.DataKeys[e.Row.RowIndex].Value.ToString();
			string selectedValue = string.Empty;
			if (this.gvwResType.DataKeys[e.Row.RowIndex].Values[1] != null)
			{
				selectedValue = this.gvwResType.DataKeys[e.Row.RowIndex].Values[1].ToString();
			}
			System.Collections.Generic.List<CostAccounting> dataSource = (System.Collections.Generic.List<CostAccounting>)this.ViewState["CBSList"];
			DropDownList dropDownList = (DropDownList)e.Row.FindControl("ddlCBS");
			dropDownList.DataSource = dataSource;
			dropDownList.DataTextField = "Name";
			dropDownList.DataValueField = "Code";
			dropDownList.DataBind();
			dropDownList.Items.Insert(0, new ListItem("", ""));
			dropDownList.SelectedValue = selectedValue;
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<ResType> list = new System.Collections.Generic.List<ResType>();
			foreach (GridViewRow gridViewRow in this.gvwResType.Rows)
			{
				string id = this.gvwResType.DataKeys[gridViewRow.RowIndex].Value.ToString();
				ResType byId = ResType.GetById(id);
				DropDownList dropDownList = (DropDownList)gridViewRow.FindControl("ddlCBS");
				if (!string.IsNullOrEmpty(dropDownList.SelectedValue.Trim()))
				{
					byId.CBSCode = dropDownList.SelectedValue.Trim();
				}
				else
				{
					byId.CBSCode = null;
				}
				list.Add(byId);
			}
			ResType.Update(list);
			base.RegisterScript("alert('系统提示：\\n修改成功！');");
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n修改失败！');");
		}
	}
	private void DataBindResType()
	{
		this.gvwResType.DataSource = ResType.GetAll();
		this.gvwResType.DataBind();
	}
}


