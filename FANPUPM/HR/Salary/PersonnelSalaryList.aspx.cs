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
public partial class HR_Salary_PersonnelSalaryList : BasePage, IRequiresSessionState
{
	private int estid;
	protected SalaryESTAction sest
	{
		get
		{
			return new SalaryESTAction();
		}
	}
	private SalaryTIAction stia
	{
		get
		{
			return new SalaryTIAction();
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

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && base.Request["vb"] != null)
		{
			this.depcode = base.Request["vb"].ToString();
			this.DDLTempID.DataBind();
			int tempid = 0;
			if (this.DDLTempID.Items.Count > 0)
			{
				tempid = Convert.ToInt32(this.DDLTempID.SelectedValue);
			}
			else
			{
				base.Response.Write("请创建员工工资模板！");
				base.Response.End();
			}
			this.DatatbESt(tempid);
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		int tempid = Convert.ToInt32(this.DDLTempID.SelectedValue);
		this.DatatbESt(tempid);
		this.btnSave.Enabled = true;
	}
	private void DatatbESt(int tempid)
	{
		TableRow tableRow = new TableRow();
		DataTable dataTable = this.sest.SalaryESTDT(tempid);
		int i = 0;
		while (i < dataTable.Rows.Count)
		{
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			string a;
			if ((a = dataTable.Rows[i]["ItemName"].ToString()) == null)
			{
				goto IL_93;
			}
			if (!(a == "ESTID"))
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
		DataTable dataTable2 = this.sest.SalaryESTInfo(tempid, this.depcode);
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
				if (!(a2 == "ESTID"))
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
				IL_413:
				tableRow2.Cells.Add(tableCell);
				tableRow2.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				tableRow2.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRow["UserCode"] + "');";
				j++;
				continue;
				IL_25E:
				if (!(dataTable.Rows[j]["IsDisplay"].ToString() == "1"))
				{
					goto IL_413;
				}
				if (dataTable.Rows[j]["IsEdit"].ToString() == "0")
				{
					string value = dataTable.Rows[j]["columnName"].ToString().Substring(1);
					string formula = this.stia.GetFormula(Convert.ToInt32(this.DDLTempID.SelectedValue), Convert.ToInt64(value));
					string resplaceFormula = this.GetResplaceFormula(formula);
					tableCell.Text = resplaceFormula;
					tableCell.Attributes["align"] = "left";
					goto IL_413;
				}
				TextBox textBox = new TextBox();
				textBox.ID = "btn" + dataRow["UserCode"].ToString() + dataTable.Rows[j]["columnName"].ToString();
				textBox.Text = Convert.ToDecimal(dataRow[dataTable.Rows[j]["columnName"].ToString()]).ToString("f2");
				textBox.CssClass = "text-line";
				textBox.Attributes["style"] = "text-align:right";
				textBox.Attributes["onblur"] = "real_onblur(this)";
				tableCell.Controls.Add(textBox);
				textBox.Width = 50;
				tableCell.Attributes["align"] = "center";
				goto IL_413;
			}
			tableRow2.CssClass = "grid_row";
			this.tbEST.Rows.Add(tableRow2);
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		ArrayList arrayList = new ArrayList();
		int tempid = Convert.ToInt32(this.DDLTempID.SelectedValue);
		DataTable dataTable = this.sest.SalaryESTDT(tempid);
		DataTable dataTable2 = this.sest.SalaryESTInfo(tempid, this.depcode);
		foreach (DataRow dataRow in dataTable2.Rows)
		{
			string text = "";
			new TableRow();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				new TableCell();
				dataTable.Rows[i]["ItemName"].ToString();
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
			if (text != "")
			{
				text = text.Remove(text.Length - 1);
				text = text + " where UserCode = '" + dataRow["UserCode"].ToString() + "'";
				arrayList.Add(text);
			}
		}
		if (arrayList.Count < 1)
		{
			this.JS.Text = "alert('没有需要保存员工工资模板！');";
			return;
		}
		if (this.sest.SalaryESTUpdate(tempid, arrayList) == 1)
		{
			this.JS.Text = "alert('员工工资模板保存成功！');";
			this.hidsave.Value = "1";
			this.DatatbESt(tempid);
			return;
		}
		this.JS.Text = "alert('员工工资模板保存失败');";
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		int tempid = Convert.ToInt32(this.DDLTempID.SelectedValue);
		this.estid = this.sest.SalaryESTAdd(tempid, this.depcode);
		if (this.estid > 0)
		{
			this.JS.Text = "alert('员工工资模板生成成功！');";
			this.hidestId.Value = this.estid.ToString();
			this.DatatbESt(tempid);
			return;
		}
		this.JS.Text = "alert('员工工资模板生成失败！');";
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		int tempid = Convert.ToInt32(this.DDLTempID.SelectedValue);
		if (this.sest.SalaryESTDelete(tempid, this.hdnUserCode.Value.ToString()) == 1)
		{
			this.JS.Text = "alert('删除成功！');";
			this.btnDel.Enabled = false;
			this.DatatbESt(tempid);
			return;
		}
		this.JS.Text = "alert('删除失败！');";
	}
	protected string GetResplaceFormula(string Formula)
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
			string text4 = "(" + this.stia.GetItemName(Convert.ToInt32(this.DDLTempID.SelectedValue), Convert.ToInt64(text3)) + ")";
			if (text4 != "()")
			{
				text = text.Replace((string)arrayList[j], text4);
			}
		}
		if (text.IndexOf('F') > 0)
		{
			text = this.GetResplaceFormula(text);
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


