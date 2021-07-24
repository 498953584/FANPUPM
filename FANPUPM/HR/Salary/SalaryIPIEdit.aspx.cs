using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryIPIEdit : BasePage, IRequiresSessionState
{
	protected SalaryESTAction sest
	{
		get
		{
			return new SalaryESTAction();
		}
	}
	protected string depcode
	{
		get
		{
			return this.ViewState["DEPCODE"].ToString();
		}
		set
		{
			this.ViewState["DEPCODE"] = value;
		}
	}
	protected int Tempid
	{
		get
		{
			return Convert.ToInt32(this.ViewState["TEMPID"]);
		}
		set
		{
			this.ViewState["TEMPID"] = value;
		}
	}
	protected int Year
	{
		get
		{
			return Convert.ToInt32(this.ViewState["YEAR"]);
		}
		set
		{
			this.ViewState["YEAR"] = value;
		}
	}
	protected int Month
	{
		get
		{
			return Convert.ToInt32(this.ViewState["MONTH"]);
		}
		set
		{
			this.ViewState["MONTH"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && (base.Request["temid"] != null || base.Request["year"] != null || base.Request["bm"] != null || base.Request["m"] != null))
		{
			this.Tempid = Convert.ToInt32(base.Request["temid"]);
			this.Year = Convert.ToInt32(base.Request["year"]);
			this.depcode = base.Request["bm"].ToString();
			this.Month = Convert.ToInt32(base.Request["m"]);
			this.DatatbESt(this.Tempid);
		}
	}
	private void DatatbESt(int tempid)
	{
		TableRow tableRow = new TableRow();
		DataTable dataTable = this.sest.SalaryESDT(tempid);
		int i = 0;
		while (i < dataTable.Rows.Count)
		{
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			string a;
			if ((a = dataTable.Rows[i]["ItemName"].ToString()) == null)
			{
				goto IL_A1;
			}
			if (!(a == "ESID"))
			{
				if (!(a == "UserCode"))
				{
					if (!(a == "v_bmbm"))
					{
						if (!(a == "PaymasterDate"))
						{
							goto IL_A1;
						}
					}
					else
					{
						tableHeaderCell.Text = "岗位";
					}
				}
				else
				{
					tableHeaderCell.Text = "姓名";
				}
			}
			else
			{
				tableHeaderCell.Text = "序号";
			}
			IL_E9:
			tableRow.Cells.Add(tableHeaderCell);
			i++;
			continue;
			IL_A1:
			if (dataTable.Rows[i]["IsDisplay"].ToString() == "1")
			{
				tableHeaderCell.Text = dataTable.Rows[i]["ItemName"].ToString();
				goto IL_E9;
			}
			goto IL_E9;
		}
		tableRow.CssClass = "grid_head";
		this.tbEST.Rows.Add(tableRow);
		DataTable dataTable2 = this.sest.SalaryESInfo(tempid, this.depcode, this.Year, this.Month);
		userManageDb userManageDb = new userManageDb();
		int num = 0;
		foreach (DataRow dataRow in dataTable2.Rows)
		{
			TableRow tableRow2 = new TableRow();
			num++;
			int j = 0;
			while (j < dataTable.Rows.Count)
			{
				TableCell tableCell = new TableCell();
				string a2;
				if ((a2 = dataTable.Rows[j]["ItemName"].ToString()) == null)
				{
					goto IL_289;
				}
				if (!(a2 == "ESID"))
				{
					if (!(a2 == "UserCode"))
					{
						if (!(a2 == "v_bmbm"))
						{
							if (!(a2 == "PaymasterDate"))
							{
								goto IL_289;
							}
						}
						else
						{
							tableCell.Text = dataRow["RoleTypeName"].ToString();
							tableCell.Attributes["align"] = "center";
						}
					}
					else
					{
						tableCell.Text = userManageDb.GetUserName(dataRow["UserCode"].ToString());
						tableCell.Attributes["align"] = "center";
					}
				}
				else
				{
					tableCell.Text = num.ToString();
					tableCell.Attributes["align"] = "center";
				}
				IL_458:
				tableRow2.Cells.Add(tableCell);
				tableRow2.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				tableRow2.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRow["UserCode"] + "');";
				j++;
				continue;
				IL_289:
				if (!(dataTable.Rows[j]["IsDisplay"].ToString() == "1"))
				{
					goto IL_458;
				}
				if (dataTable.Rows[j]["IsEdit"].ToString() == "0")
				{
					string resplaceFormula = this.GetResplaceFormula(dataRow[dataTable.Rows[j]["columnName"].ToString()].ToString(), dataRow);
					string str;
					if (!this.isNumber(dataRow[dataTable.Rows[j]["columnName"].ToString()].ToString()))
					{
						str = "=" + this.calculateparenthesesexpression(resplaceFormula);
					}
					else
					{
						str = "";
					}
					tableCell.Text = resplaceFormula + str;
					goto IL_458;
				}
				TextBox textBox = new TextBox();
				textBox.ID = "btn" + dataRow["UserCode"].ToString() + dataTable.Rows[j]["columnName"].ToString();
				textBox.Text = dataRow[dataTable.Rows[j]["columnName"].ToString()].ToString();
				textBox.CssClass = "text-line";
				textBox.Attributes["style"] = "text-align:right";
				textBox.Attributes["onblur"] = "real_onblur(this)";
				tableCell.Controls.Add(textBox);
				textBox.Width = 60;
				tableCell.Attributes["align"] = "center";
				goto IL_458;
			}
			tableRow2.CssClass = "grid_row";
			this.tbEST.Rows.Add(tableRow2);
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.sest.SalaryESDelete(this.Tempid, this.hdnUserCode.Value.ToString(), this.Year, this.Month) == 1)
		{
			this.JS.Text = "alert('删除成功！');";
			this.btnDel.Enabled = false;
			this.DatatbESt(this.Tempid);
			return;
		}
		this.JS.Text = "alert('删除失败！');";
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		ArrayList arrayList = new ArrayList();
		DataTable dataTable = this.sest.SalaryESDT(this.Tempid);
		DataTable dataTable2 = this.sest.SalaryESInfo(this.Tempid, this.depcode, this.Year, this.Month);
		foreach (DataRow dataRow in dataTable2.Rows)
		{
			string text = "";
			new TableRow();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				new TableCell();
				string a;
				if (((a = dataTable.Rows[i]["ItemName"].ToString()) == null || (!(a == "ESTID") && !(a == "UserCode") && !(a == "v_bmbm"))) && dataTable.Rows[i]["IsDisplay"].ToString() == "1" && dataTable.Rows[i]["IsEdit"].ToString() == "1")
				{
					string key = "btn" + dataRow["UserCode"].ToString() + dataTable.Rows[i]["columnName"].ToString();
					decimal num = Convert.ToDecimal(base.Request[key]);
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						dataTable.Rows[i]["columnName"].ToString(),
						"=",
						num.ToString(),
						" ,"
					});
				}
			}
			text = text.Remove(text.Length - 1);
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				" where UserCode = '",
				dataRow["UserCode"].ToString(),
				"' and year(PaymasterDate)=",
				this.Year,
				" and month(PaymasterDate) = ",
				this.Month
			});
			arrayList.Add(text);
		}
		if (this.sest.SalaryESUpdate(this.Tempid, arrayList) == 1)
		{
			this.JS.Text = "alert('员工工资保存成功！');returnValue=true;window.close();";
			this.DatatbESt(this.Tempid);
			return;
		}
		this.JS.Text = "alert('员工工资保存失败');";
	}
	protected string GetResplaceFormula(string Formula, DataRow dr)
	{
		string text = Formula;
		ArrayList arrayList = new ArrayList();
		int num = 0;
		string text2 = "";
		for (int i = 0; i < Formula.Length; i++)
		{
			if (Formula.Substring(i, 1) == "[")
			{
				num = 1;
			}
			if (Formula.Substring(i, 1) == "]")
			{
				num = 0;
				text2 += Formula.Substring(i, 1);
				arrayList.Add(text2);
			}
			if (num == 1)
			{
				text2 += Formula.Substring(i, 1);
			}
		}
		for (int j = 0; j < arrayList.Count; j++)
		{
			string text3 = (string)arrayList[j];
			text3 = text3.Replace("F", "").Replace("[", "").Replace("]", "");
			string columnName;
			if (j == 0)
			{
				columnName = "F" + text3;
			}
			else
			{
				int length = arrayList[j - 1].ToString().Replace("F", "").Replace("[", "").Replace("]", "").Length;
				columnName = "F" + text3.Substring(length);
			}
			string text4;
			if (this.isNumber(dr[columnName].ToString()))
			{
				double num2 = Convert.ToDouble(dr[columnName].ToString());
				text4 = "(" + num2 + ")";
			}
			else
			{
				text4 = "(" + dr[columnName] + ")";
			}
			if (text4 != "()")
			{
				text = text.Replace("[F" + text3 + "]", text4);
			}
		}
		if (text.IndexOf('F') > 0)
		{
			text = this.GetResplaceFormula(text, dr);
		}
		return text;
	}
	private bool isNumber(string s)
	{
		int num = 0;
		char[] array = s.ToCharArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (!char.IsNumber(array[i]))
			{
				num = -1;
				break;
			}
			num++;
		}
		return num > 0;
	}
	private string calculateparenthesesexpression(string expression)
	{
		ArrayList arrayList = new ArrayList();
		string text = "";
		expression = expression.Replace(" ", "");
		string text2;
		while (expression.Length > 0)
		{
			text2 = "";
			if (char.IsNumber(expression[0]))
			{
				while (char.IsNumber(expression[0]))
				{
					text2 += expression[0].ToString();
					expression = expression.Substring(1);
					if (expression == "")
					{
						break;
					}
				}
				text = text + text2 + "|";
			}
			if (expression.Length > 0 && expression[0].ToString() == "(")
			{
				arrayList.Add("(");
				expression = expression.Substring(1);
			}
			text2 = "";
			if (expression.Length > 0 && expression[0].ToString() == ")")
			{
				while (arrayList[arrayList.Count - 1].ToString() != "(")
				{
					text2 = text2 + arrayList[arrayList.Count - 1].ToString() + "|";
					arrayList.RemoveAt(arrayList.Count - 1);
				}
				arrayList.RemoveAt(arrayList.Count - 1);
				text += text2;
				expression = expression.Substring(1);
			}
			text2 = "";
			if (expression.Length > 0 && (expression[0].ToString() == "*" || expression[0].ToString() == "/" || expression[0].ToString() == "+" || expression[0].ToString() == "-"))
			{
				string text3 = expression[0].ToString();
				if (arrayList.Count > 0)
				{
					if (arrayList[arrayList.Count - 1].ToString() == "(" || this.verifyoperatorpriority(text3, arrayList[arrayList.Count - 1].ToString()))
					{
						arrayList.Add(text3);
					}
					else
					{
						text2 = text2 + arrayList[arrayList.Count - 1].ToString() + "|";
						arrayList.RemoveAt(arrayList.Count - 1);
						arrayList.Add(text3);
						text += text2;
					}
				}
				else
				{
					arrayList.Add(text3);
				}
				expression = expression.Substring(1);
			}
		}
		text2 = "";
		while (arrayList.Count != 0)
		{
			text2 = text2 + arrayList[arrayList.Count - 1].ToString() + "|";
			arrayList.RemoveAt(arrayList.Count - 1);
		}
		text += text2.Substring(0, text2.Length - 1);
		return this.calculateparenthesesexpressionex(text);
	}
	private string calculateparenthesesexpressionex(string expression)
	{
		ArrayList arrayList = new ArrayList();
		expression = expression.Replace(" ", "");
		string[] array = expression.Split(new char[]
		{
			Convert.ToChar("|")
		});
		for (int i = 0; i < array.Length; i++)
		{
			if (char.IsNumber(array[i], 0))
			{
				arrayList.Add(array[i].ToString());
			}
			else
			{
				float operand = (float)Convert.ToDouble(arrayList[arrayList.Count - 1]);
				arrayList.RemoveAt(arrayList.Count - 1);
				float operand2 = (float)Convert.ToDouble(arrayList[arrayList.Count - 1]);
				arrayList.RemoveAt(arrayList.Count - 1);
				arrayList.Add(this.calculate(operand2, operand, array[i]).ToString());
			}
		}
		return arrayList[0].ToString();
	}
	private bool verifyoperatorpriority(string operator1, string operator2)
	{
		return (operator1 == "*" && operator2 == "+") || (operator1 == "*" && operator2 == "-") || (operator1 == "/" && operator2 == "+") || (operator1 == "/" && operator2 == "-");
	}
	private float calculate(float operand1, float operand2, string operator2)
	{
		if (operator2 != null)
		{
			if (!(operator2 == "*"))
			{
				if (!(operator2 == "/"))
				{
					if (!(operator2 == "+"))
					{
						if (operator2 == "-")
						{
							operand1 -= operand2;
						}
					}
					else
					{
						operand1 += operand2;
					}
				}
				else
				{
					operand1 /= operand2;
				}
			}
			else
			{
				operand1 *= operand2;
			}
		}
		return operand1;
	}
}


