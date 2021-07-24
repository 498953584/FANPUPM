using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm;
using System;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_CostAccountingEdit : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			string strA = base.Request["action"];
			string text = base.Request["parent"];
			if (string.Compare(strA, "add", true) == 0)
			{
				this.txtCBSCode.Text = CostAccounting.GetCode(text);
				this.ddlType.SelectedValue = CostAccounting.GetById(text).Type;
			}
			else
			{
				if (string.Compare(strA, "update", true) == 0)
				{
					this.BindUpdateData(text);
				}
			}
			//this.ddlType.SelectedValue == "I";
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (this.txtNote.Text.Trim().Length > 200)
		{
			base.RegisterShow("系统提示", "说明不得超过200个字！");
			return;
		}
		CostAccounting costAcc = this.GetCostAcc();
		if (string.Compare(base.Request["action"], "add", true) == 0)
		{
			string inputUser = PageHelper.QueryUser(this, base.UserCode);
			costAcc.Add(costAcc, inputUser);
		}
		else
		{
			costAcc.Update(costAcc);
		}
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_costaccounting' });");
	}
	protected CostAccounting GetCostAcc()
	{
		string id = string.Empty;
		if (string.Compare(base.Request["action"], "add", true) == 0)
		{
			id = Guid.NewGuid().ToString();
		}
		else
		{
			id = base.Request["parent"];
		}
		string name = this.txtCBSName.Text.Trim();
		string selectedValue = this.ddlType.SelectedValue;
		string note = this.txtNote.Text.Trim();
		string text = this.txtCBSCode.Text;
		return CostAccounting.Create(id, name, text, selectedValue, note);
	}
	protected void BackWindows()
	{
		StringBuilder stringBuilder = new StringBuilder();
		string str = "添加";
		if (string.Compare(base.Request["action"], "update", true) == 0)
		{
			str = "修改";
		}
		stringBuilder.Append("top.ui.show('" + str + "成功！');");
		stringBuilder.Append("winclose('CostAccountingEdit', 'CostAccounting.aspx', true)");
		base.RegisterScript(stringBuilder.ToString());
	}
	protected void BindUpdateData(string id)
	{
		CostAccounting byId = CostAccounting.GetById(id);
		this.txtCBSCode.Text = byId.Code;
		this.txtCBSName.Text = byId.Name;
		this.txtNote.Text = byId.Note;
		this.ddlType.SelectedValue = byId.Type;
	}
}


