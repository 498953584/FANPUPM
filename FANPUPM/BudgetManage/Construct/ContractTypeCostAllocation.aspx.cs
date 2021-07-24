using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using cn.justwin.stockBLL.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Construct_ContractTypeCostAllocation : NBasePage, IRequiresSessionState
{
	private ContractType contractType = new ContractType();

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
			this.DataBindContractType();
		}
	}
	protected void gvwContractType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwContractType.DataKeys[e.Row.RowIndex].Value.ToString();
			string selectedValue = this.gvwContractType.DataKeys[e.Row.RowIndex].Values[1].ToString();
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
	protected void btnSelect_Click(object sender, System.EventArgs e)
	{
		this.DataBindContractType();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (GridViewRow gridViewRow in this.gvwContractType.Rows)
				{
					ContractTypeModel contractTypeModel = new ContractTypeModel();
					string typeID = this.gvwContractType.DataKeys[gridViewRow.RowIndex].Value.ToString();
					DropDownList dropDownList = (DropDownList)gridViewRow.FindControl("ddlCBS");
					contractTypeModel.TypeID = typeID;
					if (!string.IsNullOrEmpty(dropDownList.SelectedValue.Trim()))
					{
						contractTypeModel.CBSCode = dropDownList.SelectedValue.Trim();
					}
					else
					{
						contractTypeModel.CBSCode = null;
					}
					this.contractType.UpdateCBSCode(sqlTransaction, contractTypeModel);
				}
				sqlTransaction.Commit();
				base.RegisterScript("alert('系统提示：\\n修改成功！');");
			}
			catch
			{
				base.RegisterScript("alert('系统提示：\\n修改失败！');");
			}
		}
	}
	private void DataBindContractType()
	{
		int count = this.contractType.GetCount(this.GetSafeString(this.txtTypeCode.Text.Trim()), this.GetSafeString(this.txtTypeName.Text.Trim()), base.UserCode);
		this.gvwContractType.DataSource = this.contractType.GetList(this.GetSafeString(this.txtTypeCode.Text.Trim()), this.GetSafeString(this.txtTypeName.Text.Trim()), 0, count, base.UserCode);
		this.gvwContractType.DataBind();
	}
	private string GetSafeString(string str)
	{
		str = str.Replace("'", "''");
		str = str.Replace("%", "[%]");
		return str;
	}
}


