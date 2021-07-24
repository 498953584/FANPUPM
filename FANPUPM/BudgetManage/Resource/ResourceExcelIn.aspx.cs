using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Resource_ResourceExcelIn : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindddlTemplate();
		this.BindTemGv();
	}
	public void BindddlTemplate()
	{
		System.Collections.Generic.List<ResTemplate> dataSource = ResTemplate.GetAll().ToList<ResTemplate>();
		this.ddlTemlpate.DataSource = dataSource;
		this.ddlTemlpate.DataTextField = "Name";
		this.ddlTemlpate.DataValueField = "Id";
		this.ddlTemlpate.DataBind();
	}
	public void BindTemGv()
	{
		System.Collections.Generic.List<ResTemplateItem> allByTemplateId = ResTemplateItem.GetAllByTemplateId(this.ddlTemlpate.SelectedValue);
		this.gvTemplateIn.DataSource = allByTemplateId;
		this.gvTemplateIn.DataBind();
	}
	protected void btnUp_Click(object sender, System.EventArgs e)
	{
		this.ConnectionExcel();
	}
	public void ConnectionExcel()
	{
		bool flag = this.ValidateExcel(this.fuExcel);
		if (flag)
		{
			string text = base.Server.MapPath("/UploadFiles/Resource/ExcelUp/");
			string arg_2F_0 = this.fuExcel.FileName;
			if (!System.IO.Directory.Exists(text))
			{
				System.IO.Directory.CreateDirectory(text);
			}
			string text2 = text + this.fuExcel.FileName;
			this.ViewState["ExcelName"] = text2;
			if (System.IO.File.Exists(text2))
			{
				new System.IO.FileInfo(text2)
				{
					IsReadOnly = false
				}.Delete();
			}
			this.fuExcel.SaveAs(text2);
			DataTable sheets = ExcelHelper.GetSheets(this.ViewState["ExcelName"].ToString());
			this.ddlSheets.DataSource = sheets;
			this.ddlSheets.DataValueField = "sheetIndex";
			this.ddlSheets.DataTextField = "sheetName";
			this.ddlSheets.DataBind();
			this.BindExcel();
		}
	}
	public void BindExcel()
	{
		string excelFilePath = this.ViewState["ExcelName"].ToString();
		try
		{
			DataTable dataTable = ExcelHelper.ImportDataTableFromExcel(excelFilePath, System.Convert.ToInt32(this.ddlSheets.SelectedValue), new System.Collections.Generic.List<string>());
			this.ViewState["ExcelInfo"] = dataTable;
			this.gvResource.DataSource = this.GetTableColumns(dataTable);
			this.gvResource.DataBind();
			base.RegisterScript("setTem('1');");
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n你的Excel中没有数据！');");
		}
	}
	public DataTable GetTableColumns(DataTable dt)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("ExcelColumn");
		foreach (DataColumn dataColumn in dt.Columns)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["ExcelColumn"] = dataColumn.ColumnName;
			dataTable.Rows.Add(dataRow);
		}
		return dataTable;
	}
	private bool ValidateExcel(System.Web.UI.WebControls.FileUpload fup)
	{
		string value = this.hdTem.Value;
		if (!fup.HasFile)
		{
			base.RegisterScript("setTem('" + value + "');alert('系统提示：\\n请选择文件！')");
			return false;
		}
		string extension = System.IO.Path.GetExtension(fup.FileName);
		if (string.Compare(extension, ".xls", true) != 0 && string.Compare(extension, ".xlsx", true) != 0)
		{
			base.RegisterScript("setTem('" + value + "');alert('系统提示：\\n请选择Excel文件(*.xls)！')");
			return false;
		}
		return true;
	}
	protected void gvResource_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
			DropDownList dropDownList = e.Row.FindControl("ddlDBColmn") as DropDownList;
			dropDownList.DataSource = new cn.justwin.BLL.Resource().GetResourceColumn();
			dropDownList.DataBind();
			dropDownList.Items.Insert(0, new ListItem("无", ""));
			ListItem expr_8C = dropDownList.Items[1];
			expr_8C.Text += "*";
			ListItem expr_AD = dropDownList.Items[2];
			expr_AD.Text += "*";
			string text = ((DataBoundLiteralControl)e.Row.Cells[1].Controls[0]).Text.Trim();
			if (text.Equals("资源编号") || text.Equals("资源名称"))
			{
				text += "*";
			}
			ListItem listItem = dropDownList.Items.FindByText(text);
			if (listItem != null)
			{
				listItem.Selected = true;
			}
		}
	}
	protected void gvTemplateIn_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
	}
	protected void btnInInfo_Click(object sender, System.EventArgs e)
	{
		string[] array = this.hfldExcelColumns.Value.Split(new char[]
		{
			','
		});
		DataTable dataTable = this.ViewState["ExcelInfo"] as DataTable;
		if (dataTable.Rows.Count == 0)
		{
			base.RegisterScript("setTem('" + this.hdTem.Value + "');alert('系统提示：\\nExcel中没有数据可以导入！');");
			return;
		}
		System.Collections.Generic.Dictionary<string, int> cloumnIndex = this.getCloumnIndex(array);
		this.ImportResource(dataTable, cloumnIndex);
	}
	protected void ImportResource(DataTable dtExcelInfo, System.Collections.Generic.Dictionary<string, int> dicRes)
	{
		System.Collections.Generic.List<string> priceTypeCodes = ResPriceType.GetPriceTypeCodes();
		string text = PageHelper.QueryUser(this, base.UserCode);
		int num = 0;
		for (int i = 0; i < dtExcelInfo.Rows.Count; i++)
		{
			DataRow dataRow = dtExcelInfo.Rows[i];
			string text2 = dataRow[dicRes["ResourceCode"]].ToString();
			bool flag = this.IsExistResourceCode(text2);
			if (flag)
			{
				Label expr_57 = this.lblErrorMsg;
				string text3 = expr_57.Text;
				expr_57.Text = string.Concat(new string[]
				{
					text3,
					"<div align='left' style=' margin-top:3px; padding-left:15px;  border-bottom-style:solid; border-bottom-color:#cccccc; border-bottom-width:1px;  background-color:white; line-height:28px; height:28px; text-align:left;'>在您的Excel中：",
					dtExcelInfo.Columns[dicRes["ResourceCode"]].ColumnName,
					"<span style='color:green;'>",
					text2,
					"</span>，此编码已经存在导入失败！</div>"
				});
			}
			else
			{
				string text4 = dtExcelInfo.Rows[i][dicRes["ResourceName"]].ToString();
				if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text4))
				{
					string brand = dicRes.Keys.Any((string a) => a == "Brand") ? dataRow[dicRes["Brand"]].ToString() : string.Empty;
					string text5 = dicRes.Keys.Any((string a) => a == "TaxRate") ? dataRow[dicRes["TaxRate"]].ToString() : string.Empty;
					decimal? taxRate = null;
					decimal value;
					if (!string.IsNullOrEmpty(text5) && decimal.TryParse(text5, out value))
					{
						taxRate = new decimal?(value);
					}
					string specification = dicRes.Keys.Any((string a) => a == "Specification") ? dataRow[dicRes["Specification"]].ToString() : string.Empty;
					string technicalParam = dicRes.Keys.Any((string a) => a == "TechnicalParameter") ? dataRow[dicRes["TechnicalParameter"]].ToString() : string.Empty;
					string text6 = dicRes.Keys.Any((string a) => a == "Unit") ? dataRow[dicRes["Unit"]].ToString() : string.Empty;
					if (string.IsNullOrEmpty(text6))
					{
						text6 = "xx";
					}
					string attributeName = dicRes.Keys.Any((string a) => a == "Attribute") ? dataRow[dicRes["Attribute"]].ToString() : string.Empty;
					string series = dicRes.Keys.Any((string a) => a == "Series") ? dataRow[dicRes["Series"]].ToString() : string.Empty;
					string supplierName = dicRes.Keys.Any((string a) => a == "SupplierId") ? dataRow[dicRes["SupplierId"]].ToString() : string.Empty;
					int? supplierIdByName = cn.justwin.Domain.Resource.GetSupplierIdByName(supplierName);
					string modelNumber = dicRes.Keys.Any((string a) => a == "ModelNumber") ? dataRow[dicRes["ModelNumber"]].ToString() : string.Empty;
					if (!dicRes.Keys.Any((string a) => a == "Note"))
					{
						string arg_400_0 = string.Empty;
					}
					else
					{
						dataRow[dicRes["Note"]].ToString();
					}
					System.Collections.Generic.Dictionary<ResPriceType, decimal?> dictionary = new System.Collections.Generic.Dictionary<ResPriceType, decimal?>();
					foreach (string key in dicRes.Keys)
					{
						bool flag2 = priceTypeCodes.Any((string a) => a == key);
						if (flag2)
						{
							string s = dataRow[dicRes[key]].ToString();
							decimal value2;
							decimal.TryParse(s, out value2);
							dictionary.Add(ResPriceType.GetByTypeCode(key), new decimal?(value2));
						}
					}
					ResType byId = ResType.GetById(base.Request["parentId"]);
					ResUnit unit = this.GetUnit(text6, text);
					ResTypeAttribute byAttributeName = ResTypeAttribute.GetByAttributeName(attributeName);
					cn.justwin.Domain.Resource resource = cn.justwin.Domain.Resource.Create(System.Guid.NewGuid().ToString(), byId, text2, text4, brand, taxRate, specification, modelNumber, technicalParam, unit, byAttributeName, series, dictionary, text, new System.DateTime?(System.DateTime.Now), supplierIdByName);
					cn.justwin.Domain.Resource.Add(resource);
					num++;
				}
			}
		}
		if (num == 0)
		{
			base.RegisterScript("alert('系统提示：\\n数据导入失败！');setTem('2');");
			return;
		}
		if (num == dtExcelInfo.Rows.Count)
		{
			base.RegisterScript("alert('系统提示：\\n数据导入成功！');setTem('2');closePage();");
			return;
		}
		base.RegisterScript("alert('系统提示：\\n数据导入成功！');setTem('2');");
	}
	protected ResUnit GetUnit(string unitName, string inputUser)
	{
		if (string.IsNullOrEmpty(unitName))
		{
			return null;
		}
		ResUnit resUnit = ResUnit.GetByUnitName(unitName);
		if (resUnit == null)
		{
			resUnit = ResUnit.Create(System.Guid.NewGuid().ToString(), System.DateTime.Now.ToString("yyyyMMddHHmmsss"), unitName, inputUser, new System.DateTime?(System.DateTime.Now));
			ResUnit.Add(resUnit);
		}
		return resUnit;
	}
	protected System.Collections.Generic.Dictionary<string, int> getCloumnIndex(string[] array)
	{
		System.Collections.Generic.Dictionary<string, int> dictionary = new System.Collections.Generic.Dictionary<string, int>();
		System.Collections.Generic.Dictionary<int, string> dictionary2 = new System.Collections.Generic.Dictionary<int, string>();
		int num = -1;
		for (int i = 0; i < array.Length; i++)
		{
			string value = array[i];
			num++;
			dictionary2.Add(num, value);
		}
		int num2 = -1;
		foreach (string current in dictionary2.Values)
		{
			num2++;
			if (current != "")
			{
				dictionary.Add(current, num2);
			}
		}
		return dictionary;
	}
	protected bool IsExistResourceCode(string resCode)
	{
		bool result = false;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			Res_Resource res_Resource = (
				from m in pm2Entities.Res_Resource
				where m.ResourceCode == resCode
				select m).FirstOrDefault<Res_Resource>();
			if (res_Resource != null)
			{
				result = true;
			}
		}
		return result;
	}
	protected void btnTemIn_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = this.ViewState["ExcelInfo"] as DataTable;
		foreach (GridViewRow gridViewRow in this.gvTemplateIn.Rows)
		{
			HiddenField hiddenField = gridViewRow.FindControl("hfldExcelColumn") as HiddenField;
			HiddenField hiddenField2 = gridViewRow.FindControl("hfldDbColumn") as HiddenField;
			if (hiddenField2 != null && hiddenField2 != null)
			{
				string value = hiddenField.Value;
				string value2 = hiddenField2.Value;
				if ((value2 == "ResourceCode" || value2 == "ResourceName") && dataTable.Columns.IndexOf(value) == -1)
				{
					base.RegisterScript("alert('系统提示：\\n指定导入的Excel中不包含必选列 " + value + ",此模板不能完成导入！\\n请重新选择模板或修改Excel！');");
					return;
				}
			}
		}
		System.Collections.Generic.Dictionary<string, int> dictionary = new System.Collections.Generic.Dictionary<string, int>();
		foreach (GridViewRow gridViewRow2 in this.gvTemplateIn.Rows)
		{
			HiddenField hiddenField3 = gridViewRow2.FindControl("hfldExcelColumn") as HiddenField;
			HiddenField hiddenField4 = gridViewRow2.FindControl("hfldDbColumn") as HiddenField;
			if (hiddenField4 != null && hiddenField4 != null)
			{
				string value3 = hiddenField3.Value;
				string value4 = hiddenField4.Value;
				int num = dataTable.Columns.IndexOf(value3);
				if (num != -1)
				{
					dictionary.Add(value4, num);
				}
			}
		}
		this.ImportResource(dataTable, dictionary);
	}
	protected void btnSaveTemplate_Click(object sender, System.EventArgs e)
	{
		string name = this.txtTemplateName.Text.Trim();
		string user = PageHelper.QueryUser(this, base.UserCode);
		string id = System.Guid.NewGuid().ToString();
		ResTemplate resTemplate = ResTemplate.Create(id, name, 0, true, user, new System.DateTime?(System.DateTime.Now));
		ResTemplate.Add(resTemplate);
		foreach (GridViewRow gridViewRow in this.gvResource.Rows)
		{
			HiddenField hiddenField = gridViewRow.FindControl("hflddExcelColumn") as HiddenField;
			DropDownList dropDownList = gridViewRow.FindControl("ddlDBColmn") as DropDownList;
			if (hiddenField != null && dropDownList != null && !(dropDownList.SelectedValue == ""))
			{
				string value = hiddenField.Value;
				string text = dropDownList.SelectedItem.Text;
				string selectedValue = dropDownList.SelectedValue;
				ResTemplateItem restemplateitem = ResTemplateItem.Create(System.Guid.NewGuid().ToString(), resTemplate, value, text, selectedValue);
				ResTemplateItem.Add(restemplateitem);
			}
		}
		base.RegisterScript("alert('系统提示：\\n已成功保存为模板！');setTem('0');");
		this.BindddlTemplate();
		this.BindTemGv();
	}
	protected void ddlTemlpate_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindTemGv();
	}
	public string GetColumnName(string dbcolumn)
	{
		string result = "";
		DataTable resourceColumn = new cn.justwin.BLL.Resource().GetResourceColumn();
		foreach (DataRow dataRow in resourceColumn.Rows)
		{
			if (System.Convert.ToString(dataRow["name"]) == dbcolumn)
			{
				result = System.Convert.ToString(dataRow["value"]);
			}
		}
		return result;
	}
	protected void ddlSheets_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindExcel();
	}
}


