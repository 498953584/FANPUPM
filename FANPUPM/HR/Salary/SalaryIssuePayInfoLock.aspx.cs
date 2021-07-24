using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryIssuePayInfoLock : BasePage, IRequiresSessionState
{
	protected SalaryIPIAction sipi
	{
		get
		{
			return new SalaryIPIAction();
		}
	}
	protected SalaryESTAction sest
	{
		get
		{
			return new SalaryESTAction();
		}
	}
	public Guid InstanceCode
	{
		get
		{
			object obj = this.ViewState["InstanceCode"];
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["InstanceCode"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && base.Request["ic"] != null)
		{
			this.InstanceCode = new Guid(base.Request["ic"]);
			this.PageBind();
		}
	}
	private void PageBind()
	{
		SalaryIPIModel model = this.sipi.GetModel(this.InstanceCode);
		this.LbDate.Text = model.IssueYear.ToString() + "年" + model.IssueMonth.ToString() + "月";
		this.LbRemark.Text = model.Remark;
		this.DatatbESt(model.TempletID.Value);
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
				goto IL_93;
			}
			if (!(a == "ESID"))
			{
				if (!(a == "UserCode"))
				{
					if (!(a == "v_bmbm"))
					{
						goto IL_93;
					}
					tableHeaderCell.Text = "岗位";
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
			IL_DB:
			tableRow.Cells.Add(tableHeaderCell);
			i++;
			continue;
			IL_93:
			if (dataTable.Rows[i]["IsDisplay"].ToString() == "1")
			{
				tableHeaderCell.Text = dataTable.Rows[i]["ItemName"].ToString();
				goto IL_DB;
			}
			goto IL_DB;
		}
		tableRow.CssClass = "grid_head";
		this.tbEST.Rows.Add(tableRow);
		DataTable dataTable2 = this.sest.SalaryESInfo(tempid, this.InstanceCode);
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
					goto IL_25E;
				}
				if (!(a2 == "ESID"))
				{
					if (!(a2 == "UserCode"))
					{
						if (!(a2 == "v_bmbm"))
						{
							goto IL_25E;
						}
						tableCell.Text = dataRow["RoleTypeName"].ToString();
						tableCell.Attributes["align"] = "center";
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
				IL_3C9:
				tableRow2.Cells.Add(tableCell);
				tableRow2.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				tableRow2.Attributes["onclick"] = "OnRecord(this);";
				j++;
				continue;
				IL_25E:
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
					goto IL_3C9;
				}
				goto IL_3C9;
			}
			tableRow2.CssClass = "grid_row";
			this.tbEST.Rows.Add(tableRow2);
		}
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


