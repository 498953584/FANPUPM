using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryTIEdit : BasePage, IRequiresSessionState
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
	private SalaryTIAction stia
	{
		get
		{
			return new SalaryTIAction();
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
	private int TempletID
	{
		get
		{
			return (int)this.ViewState["TEMPLETID"];
		}
		set
		{
			this.ViewState["TEMPLETID"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && (base.Request["rid"] != null || base.Request["t"] != null || base.Request["tid"] != null))
		{
			this.rid = Convert.ToInt32(base.Request["rid"]);
			this.Type = base.Request["t"].ToString();
			this.TempletID = Convert.ToInt32(base.Request["tid"]);
			if (this.Type != "Add")
			{
				this.GetPageData();
			}
			if (this.DDLItemID.Items.Count > 0)
			{
				this.DDLItemID.Attributes["onclick"] = "sel(this);";
			}
			this.BtnFormula.Attributes["onclick"] = "return  OpenFormulaEdit('" + this.TempletID + "');";
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.Type == "Add")
		{
			if (this.IsTempletID(Convert.ToInt32(this.DDLItemID.SelectedValue)))
			{
				this.JS.Text = "alert('你选择地薪金项目以使用过了，请选择其它薪金项目。');";
				return;
			}
			if (this.stia.Add(this.getSalaryTIModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
		else
		{
			if (this.stia.Update(this.getSalaryTIModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
	}
	protected bool IsTempletID(int itemid)
	{
		DataTable list = this.stia.GetList(string.Concat(new object[]
		{
			"TempletID=",
			this.TempletID,
			" AND ItemID = ",
			itemid
		}));
		return list.Rows.Count > 0;
	}
	protected SalaryTIModel getSalaryTIModel()
	{
		return new SalaryTIModel
		{
			TempletID = this.TempletID,
			ItemID = Convert.ToInt32(this.DDLItemID.SelectedValue),
			ItemName = this.txtItemName.Text,
			TheOrder = new int?(Convert.ToInt32(this.txtTheOrder.Text)),
			IsDisplay = this.CBIsDisplay.Checked ? "1" : "0",
			IsEdit = this.CBIsEdit.Checked ? "1" : "0",
			Formula = this.txtFormula.Text
		};
	}
	protected void GetPageData()
	{
		SalaryTIModel model = this.stia.GetModel(this.TempletID, this.rid);
		this.DDLItemID.SelectedValue = this.rid.ToString();
		this.txtItemName.Text = model.ItemName;
		this.txtTheOrder.Text = model.TheOrder.ToString();
		this.CBIsDisplay.Checked = (model.IsDisplay == "1");
		this.CBIsEdit.Checked = (model.IsEdit == "1");
		this.txtFormula.Text = model.Formula;
	}
	protected void BtnFormula_Click(object sender, EventArgs e)
	{
		this.CBIsEdit.Checked = false;
	}
	protected string GetResplaceFormula(string Formula)
	{
		string text = Formula;
		ArrayList arrayList = new ArrayList();
		int num = 0;
		string text2 = "";
		for (int i = 0; i < Formula.Length - 1; i++)
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
			string text4 = "(" + this.stia.GetFormula(this.TempletID, Convert.ToInt64(text3)) + ")";
			if (text4 != "()")
			{
				text = text.Replace((string)arrayList[j], text4);
			}
		}
		return text;
	}
}


