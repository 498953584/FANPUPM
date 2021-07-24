using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryCount : BasePage, IRequiresSessionState
{

	protected SalaryESTAction sest
	{
		get
		{
			return new SalaryESTAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.DDLDataBind();
			this.btnSelect.Attributes["onclick"] = "SelDept();return false;";
			this.DDLTempID.DataBind();
			if (this.DDLTempID.Items.Count <= 0)
			{
				base.Response.Write("请创建员工工资模板！");
				base.Response.End();
			}
			this.DatatbESt(Convert.ToInt32(this.DDLTempID.SelectedValue));
		}
	}
	protected void DDLDataBind()
	{
		int year = DateTime.Now.Year;
		for (int i = 2006; i < year + 5; i++)
		{
			ListItem item = new ListItem(i.ToString(), i.ToString());
			this.DDLBeginYear.Items.Add(item);
			this.DDLEndYear.Items.Add(item);
		}
		this.DDLBeginYear.SelectedValue = year.ToString();
		this.DDLEndYear.SelectedValue = year.ToString();
		this.DDLBeginMonth.SelectedValue = DateTime.Now.Month.ToString();
		this.DDLEndMonth.SelectedValue = DateTime.Now.Month.ToString();
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
				goto IL_86;
			}
			if (!(a == "ESID"))
			{
				if (!(a == "UserCode"))
				{
					if (!(a == "v_bmbm"))
					{
						goto IL_86;
					}
					tableHeaderCell.Text = "部门名称";
				}
			}
			else
			{
				tableHeaderCell.Text = "序号";
			}
			IL_CE:
			if (dataTable.Rows[i]["ItemName"].ToString() != "UserCode")
			{
				tableRow.Cells.Add(tableHeaderCell);
			}
			i++;
			continue;
			IL_86:
			if (dataTable.Rows[i]["IsDisplay"].ToString() == "1")
			{
				tableHeaderCell.Text = dataTable.Rows[i]["ItemName"].ToString();
				goto IL_CE;
			}
			goto IL_CE;
		}
		tableRow.CssClass = "grid_head";
		this.tbEST.Rows.Add(tableRow);
		DateTime begindatetime = Convert.ToDateTime(this.DDLBeginYear.SelectedValue + "-" + this.DDLBeginMonth.SelectedValue + "-01");
		DateTime endbegin = Convert.ToDateTime(this.DDLEndYear.SelectedValue + "-" + this.DDLEndMonth.SelectedValue + "-01");
		string value = this.hdnDept.Value;
		DataTable dataTable2 = this.sest.SalaryESCount(tempid, begindatetime, endbegin, value);
		new userManageDb();
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
					goto IL_2CF;
				}
				if (!(a2 == "ESID"))
				{
					if (!(a2 == "UserCode"))
					{
						if (!(a2 == "v_bmbm"))
						{
							if (!(a2 == "PaymasterDate"))
							{
								goto IL_2CF;
							}
						}
						else
						{
							tableCell.Text = this.sbspnumber(dataRow["v_bmbm"].ToString()) + dataRow["v_bmmc"].ToString();
							tableCell.Attributes["align"] = "left";
						}
					}
				}
				else
				{
					tableCell.Text = num.ToString();
					tableCell.Attributes["align"] = "center";
				}
				IL_43A:
				if (dataTable.Rows[j]["ItemName"].ToString() != "UserCode")
				{
					tableRow2.Cells.Add(tableCell);
				}
				tableRow2.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				tableRow2.Attributes["onclick"] = "OnRecord(this);";
				j++;
				continue;
				IL_2CF:
				if (dataTable.Rows[j]["IsDisplay"].ToString() == "1" && dataTable.Rows[j]["IsEdit"].ToString() == "0")
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
				}
				if (dataTable.Rows[j]["IsDisplay"].ToString() == "1" && dataTable.Rows[j]["IsEdit"].ToString() == "1")
				{
					tableCell.Text = dataRow[dataTable.Rows[j]["columnName"].ToString()].ToString();
					goto IL_43A;
				}
				goto IL_43A;
			}
			tableRow2.CssClass = "grid_row";
			this.tbEST.Rows.Add(tableRow2);
		}
	}
	protected void btnCount_Click(object sender, EventArgs e)
	{
		this.DatatbESt(Convert.ToInt32(this.DDLTempID.SelectedValue));
	}
	protected string sbspnumber(string bmbm)
	{
		string text = "";
		for (int i = 0; i < bmbm.Length - 2; i++)
		{
			text += "&nbsp;";
		}
		return text;
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


