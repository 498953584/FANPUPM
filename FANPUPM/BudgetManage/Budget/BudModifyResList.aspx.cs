using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Serialize;
using cn.justwin.Web;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_BudModifyResList : NBasePage, IRequiresSessionState
{
	private string modifyTaskId = string.Empty;
	private string prjId = string.Empty;
	private string isAllowEditRes = string.Empty;
	private string pageURl = string.Empty;
	private string modifyId = string.Empty;
	private string spType = string.Empty;
	private string modifyPage = string.Empty;
	private System.Text.StringBuilder strJS = new System.Text.StringBuilder("setWidthHeight();");
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private BudModifyTaskResService budModifyTaskResSer = new BudModifyTaskResService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["modifyTaskId"]))
		{
			this.modifyTaskId = base.Request["modifyTaskId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["modifyId"]))
		{
			this.modifyId = base.Request["modifyId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["isAllowEditRes"]))
		{
			this.isAllowEditRes = base.Request["isAllowEditRes"];
		}
		if (!string.IsNullOrEmpty(base.Request["pageURl"]))
		{
			this.pageURl = base.Request["pageURl"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["spType"]))
		{
			this.spType = base.Request["spType"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["modifyPage"]))
		{
			this.modifyPage = base.Request["modifyPage"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.hfldIsAllowEditRes.Value = this.isAllowEditRes;
			this.BindGv();
		}
	}
	private void BindGv()
	{
		DataTable dataTable = new DataTable();
		if (this.hfldIsWBSRelevance.Value == "1")
		{
			HttpCookie httpCookie = base.Request.Cookies[this.modifyTaskId];
			System.Collections.Generic.List<BudModifyTaskRes> list = new System.Collections.Generic.List<BudModifyTaskRes>();
			if (httpCookie == null)
			{
				goto IL_353;
			}
			string value = httpCookie.Value;
			if (!string.IsNullOrEmpty(value))
			{
				list = JsonConvert.DeserializeObject<System.Collections.Generic.List<BudModifyTaskRes>>(value);
			}
			System.Collections.Generic.List<string> list2 = new System.Collections.Generic.List<string>();
			foreach (BudModifyTaskRes current in list)
			{
				list2.Add(current.ResourceId);
			}
			dataTable = BudModifyTaskResBll.showResForAdd(DBHelper.GetInParameterSql(list2.ToArray()), this.modifyTaskId);
			System.Collections.IEnumerator enumerator2 = dataTable.Rows.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					DataRow dataRow = (DataRow)enumerator2.Current;
					string b = dataRow["ResourceId"].ToString();
					foreach (BudModifyTaskRes current2 in list)
					{
						if (current2.ResourceId == b)
						{
							dataRow["price"] = current2.ResourcePrice;
							dataRow["number"] = current2.ResourceQuantity;
							dataRow["LossCoefficient"] = ((!current2.LossCoefficient.HasValue) ? new decimal?(1m) : current2.LossCoefficient);
							break;
						}
					}
				}
				goto IL_353;
			}
			finally
			{
				System.IDisposable disposable = enumerator2 as System.IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		HttpCookie httpCookie2 = base.Request.Cookies[this.modifyId];
		System.Collections.Generic.List<BudModifyTaskRes> list3 = new System.Collections.Generic.List<BudModifyTaskRes>();
		if (httpCookie2 != null)
		{
			string value2 = httpCookie2.Value;
			if (!string.IsNullOrEmpty(value2))
			{
				list3 = JsonConvert.DeserializeObject<System.Collections.Generic.List<BudModifyTaskRes>>(value2);
			}
			System.Collections.Generic.List<string> list4 = new System.Collections.Generic.List<string>();
			foreach (BudModifyTaskRes current3 in list3)
			{
				list4.Add(current3.ResourceId);
			}
			dataTable = BudModifyTaskResBll.showResForAddByModify(DBHelper.GetInParameterSql(list4.ToArray()), this.modifyId);
			foreach (DataRow dataRow2 in dataTable.Rows)
			{
				string b2 = dataRow2["ResourceId"].ToString();
				foreach (BudModifyTaskRes current4 in list3)
				{
					if (current4.ResourceId == b2)
					{
						dataRow2["price"] = current4.ResourcePrice;
						dataRow2["number"] = current4.ResourceQuantity;
						dataRow2["LossCoefficient"] = ((!current4.LossCoefficient.HasValue) ? new decimal?(1m) : current4.LossCoefficient);
						break;
					}
				}
			}
		}
		IL_353:
		this.ViewState["ResourcesTable"] = dataTable;
		this.gvResource.DataSource = dataTable;
		this.gvResource.DataBind();
	}
	protected void btnBindResource_Click(object sender, System.EventArgs e)
	{
		this.ReplaceViewState();
		if (!string.IsNullOrEmpty(this.hfldResourceId.Value))
		{
			ISerializable serializable = new cn.justwin.Serialize.JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(this.hfldResourceId.Value);
			if (array != null)
			{
				DataTable dataTable = new DataTable();
				if (this.hfldIsWBSRelevance.Value == "1")
				{
					dataTable = BudModifyTaskResBll.showResForAdd(DBHelper.GetInParameterSql(array), this.modifyTaskId);
				}
				else
				{
					dataTable = BudModifyTaskResBll.showResForAddByModify(DBHelper.GetInParameterSql(array), this.prjId);
				}
				DataTable dataTable2 = this.ViewState["ResourcesTable"] as DataTable;
				if (dataTable2 != null)
				{
					dataTable2.PrimaryKey = new DataColumn[]
					{
						dataTable2.Columns["ResourceCode"]
					};
					dataTable.PrimaryKey = new DataColumn[]
					{
						dataTable.Columns["ResourceCode"]
					};
					dataTable2.Merge(dataTable, true);
					dataTable = dataTable2;
				}
				if (!dataTable.Columns.Contains("LossCoefficient"))
				{
					DataColumn dataColumn = new DataColumn();
					dataColumn.ColumnName = "LossCoefficient";
					dataColumn.DataType = System.Type.GetType("System.Decimal");
					dataColumn.DefaultValue = 1.0;
					dataTable.Columns.Add(dataColumn);
				}
				this.ViewState["ResourcesTable"] = dataTable;
				this.gvResource.DataSource = dataTable;
				this.gvResource.DataBind();
			}
		}
		this.hfldResourceId.Value = string.Empty;
	}
	protected void gvResource_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				string value = this.gvResource.DataKeys[e.Row.RowIndex].Values[0].ToString();
				e.Row.Attributes["id"] = value;
				string value2 = this.gvResource.DataKeys[e.Row.RowIndex].Values[1].ToString();
				e.Row.Attributes["resourceId"] = value2;
				if (this.hfldIsAllowEditRes.Value == "0")
				{
					TextBox textBox = (TextBox)e.Row.FindControl("txtPrice");
					textBox.Enabled = false;
					TextBox textBox2 = (TextBox)e.Row.FindControl("txtNumber");
					textBox2.Enabled = false;
				}
			}
			catch
			{
			}
		}
	}
	protected void btnSave_ServerClick(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<BudModifyTaskRes> list = new System.Collections.Generic.List<BudModifyTaskRes>();
			for (int i = 0; i < this.gvResource.Rows.Count; i++)
			{
				string resourceId = this.gvResource.DataKeys[i].Values[0].ToString();
				decimal resourceQuantity = System.Convert.ToDecimal(((TextBox)this.gvResource.Rows[i].FindControl("txtNumber")).Text);
				decimal resourcePrice = System.Convert.ToDecimal(((TextBox)this.gvResource.Rows[i].FindControl("txtPrice")).Text);
				decimal? lossCoefficient = new decimal?(System.Convert.ToDecimal(((TextBox)this.gvResource.Rows[i].FindControl("txtLossCoefficient")).Text));
				if (this.hfldIsWBSRelevance.Value == "1")
				{
					list.Add(new BudModifyTaskRes
					{
						ModifyTaskResId = System.Guid.NewGuid().ToString(),
						ModifyTaskId = this.modifyTaskId,
						ModifyId = this.modifyId,
						ResourceId = resourceId,
						ResourcePrice = resourcePrice,
						ResourceQuantity = resourceQuantity,
						LossCoefficient = lossCoefficient
					});
				}
				else
				{
					list.Add(new BudModifyTaskRes
					{
						ModifyTaskResId = System.Guid.NewGuid().ToString(),
						ModifyTaskId = null,
						ModifyId = this.modifyId,
						ResourceId = resourceId,
						ResourcePrice = resourcePrice,
						ResourceQuantity = resourceQuantity,
						LossCoefficient = lossCoefficient
					});
				}
			}
			string value = JsonHelper.JsonSerializer<System.Collections.Generic.List<BudModifyTaskRes>>(list);
			HttpCookie httpCookie;
			if (this.hfldIsWBSRelevance.Value == "1")
			{
				httpCookie = new HttpCookie(this.modifyTaskId);
			}
			else
			{
				httpCookie = new HttpCookie(this.modifyId);
			}
			httpCookie.Value = value;
			base.Response.Cookies.Add(httpCookie);
			this.strJS.Append("$('#btnBindResource').hide(); alert('系统提示：\\n\\n保存成功!');");
			if (this.hfldIsWBSRelevance.Value == "1")
			{
				if (this.modifyPage == "0")
				{
					this.strJS.Append("var par = parent.desktop['BudModifyResList']; par.document.getElementById('btnBindRes').click();");
				}
				else
				{
					this.strJS.Append("var par = parent.desktop['BudModifyResList']; par.document.getElementById('btnBindResource').click();");
				}
			}
			if (this.modifyPage == "0")
			{
				this.strJS.Append("winclose('BudModifyResList', 'EditModify.aspx', false)");
			}
			else
			{
				this.strJS.Append("winclose('BudModifyResList', 'ModifyEdit.aspx', false)");
			}
			base.RegisterScript(this.strJS.ToString());
		}
		catch (System.Exception)
		{
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		try
		{
			this.ReplaceViewState();
			DataTable dataTable = (DataTable)this.ViewState["ResourcesTable"];
			if (this.gvResource.Rows.Count > 0)
			{
				for (int i = this.gvResource.Rows.Count - 1; i >= 0; i--)
				{
					GridViewRow gridViewRow = this.gvResource.Rows[i];
					CheckBox checkBox = (CheckBox)gridViewRow.FindControl("cbBox");
					if (checkBox.Checked)
					{
						dataTable.Rows.RemoveAt(gridViewRow.RowIndex);
					}
				}
			}
			this.ViewState["ResourcesTable"] = dataTable;
			this.gvResource.DataSource = (DataTable)this.ViewState["ResourcesTable"];
			this.gvResource.DataBind();
		}
		catch (System.Exception)
		{
			this.strJS.Append("$('#btnBindResource').hide(); alert('没有找到要删除的资源!');");
			base.RegisterScript(this.strJS.ToString());
		}
	}
	private void ReplaceViewState()
	{
		DataTable dataTable = (DataTable)this.ViewState["ResourcesTable"];
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			GridViewRow gridViewRow = this.gvResource.Rows[i];
			DataRow dataRow = dataTable.Rows[i];
			if (gridViewRow.FindControl("txtNumber") is TextBox)
			{
				TextBox textBox = gridViewRow.FindControl("txtNumber") as TextBox;
				dataRow["number"] = textBox.Text.Trim();
			}
			if (gridViewRow.FindControl("txtPrice") is TextBox)
			{
				TextBox textBox2 = gridViewRow.FindControl("txtPrice") as TextBox;
				dataRow["price"] = textBox2.Text.Trim();
			}
		}
		this.ViewState["ResourcesTable"] = dataTable;
	}
}


