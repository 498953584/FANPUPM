using ASP;
using cn.justwin.AccountManage.prjaccount;
using cn.justwin.BLL;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using System;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_acc_Manage_accountEdit : NBasePage, IRequiresSessionState
{
	private accountMange am = new accountMange();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Session["HumanCode"] = "";
			this.Session["HumanName"] = "";
			this.Bind();
		}
	}
	public void Bind()
	{
		string text = base.Request["accountid"].ToString();
		if (!(text != ""))
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" parent.desktop.flowclass.location='/AccountManage/acc_Manage/accountList.aspx';").Append(Environment.NewLine);
			stringBuilder.Append("alert('系统提示：\\n\\请选择要修改的行！');").Append(Environment.NewLine);
			stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
			base.RegisterScript(stringBuilder.ToString());
			return;
		}
		prjaccountModel modelByConId = this.am.GetModelByConId(text);
		this.txtaccountNum.Text = modelByConId.accountNum.ToString();
		this.txtacountName.Text = modelByConId.acountName.ToString();
		string text2 = "";
		string[] array = modelByConId.authorizer.ToString().Split(new char[]
		{
			','
		});
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text3 = array2[i];
			if (text3 != "")
			{
				text2 = text2 + "," + text3;
			}
		}
		string userName = this.am.GetUserName(text2);
		this.Session["HumanCode"] = text2;
		this.Session["HumanName"] = userName;
		this.txtauthorizer.Text = userName;
		this.txtremark.Text = modelByConId.remark.ToString();
		if (base.Request["limits"].ToString() == "0")
		{
			this.txtauthorizer.ReadOnly = true;
			this.btnSelect.Enabled = false;
			return;
		}
		this.txtauthorizer.ReadOnly = false;
		this.btnSelect.Enabled = true;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		prjaccountModel prjaccountModel = new prjaccountModel();
		prjaccountModel.remark = this.txtremark.Text;
		prjaccountModel.accountNum = this.txtaccountNum.Text;
		prjaccountModel.acountName = this.txtacountName.Text;
		prjaccountModel.accountID = new Guid(base.Request["accountid"].ToString());
		string text = "";
		string[] array = this.Session["HumanCode"].ToString().Split(new char[]
		{
			','
		});
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text2 = array2[i];
			if (text2 != "")
			{
				text = text + "," + text2;
			}
		}
		prjaccountModel.authorizer = text;
		int num = this.am.Edit(prjaccountModel);
		if (num > 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" parent.desktop.flowclass.location='/AccountManage/acc_Manage/accountList.aspx';").Append(Environment.NewLine);
			stringBuilder.Append("alert('系统提示：\\n\\修改成功！');").Append(Environment.NewLine);
			stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
			base.RegisterScript(stringBuilder.ToString());
			return;
		}
		base.RegisterScript("alert('系统提示：\\n\\修改失败！');");
	}
	protected void btnSelect_Click(object sender, EventArgs e)
	{
		this.txtauthorizer.Text = this.Session["HumanName"].ToString();
	}
}


