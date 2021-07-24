using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using System;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_IncomeExpensePlan_InExPlanEdit : NBasePage, IRequiresSessionState
{
	private InExPlanBLL ieBLL = new InExPlanBLL();
	private PrjInfo prj = new PrjInfo();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["type"].ToString() == "add")
			{
				this.txtCode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
				this.hdfID.Value = Guid.NewGuid().ToString();
				return;
			}
			if (base.Request["type"].ToString() == "edit")
			{
				this.hdfID.Value = base.Request["id"].ToString();
				this.bind();
			}
		}
	}
	public void bind()
	{
		string id = base.Request["id"].ToString();
		InExPlanModel model = this.ieBLL.GetModel(id);
		this.txtCode.Text = model.IEPNum.ToString();
		this.txtData.Text = Convert.ToDateTime(model.IEPdate).ToShortDateString();
		this.txtName.Text = model.IEPname.ToString();
		this.ddlType.SelectedValue = model.IEPtype.ToString();
		this.hdnProjectCode.Value = model.prjNum;
		this.hdnProjectCode.Value = this.prj.GetModelByPrjGuid(this.hdnProjectCode.Value.ToString()).PrjName;
		this.txtProjectName.Value = this.hdfCropName.Value;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		string str = "";
		try
		{
			InExPlanModel inExPlanModel = new InExPlanModel();
			inExPlanModel.ID = this.hdfID.Value.ToString();
			if (this.txtData.Text.ToString() != "")
			{
				inExPlanModel.IEPdate = new DateTime?(Convert.ToDateTime(this.txtData.Text.ToString()));
			}
			else
			{
				inExPlanModel.IEPdate = new DateTime?(DateTime.Now);
			}
			inExPlanModel.IEPname = this.txtName.Text;
			inExPlanModel.IEPNum = this.txtCode.Text;
			inExPlanModel.IEPtype = new int?(Convert.ToInt32(this.ddlType.SelectedValue.ToString()));
			inExPlanModel.prjNum = this.hdnProjectCode.Value;
			inExPlanModel.state = new int?(-1);
			if (base.Request["type"].ToString() == "add")
			{
				this.ieBLL.Add(inExPlanModel);
				str = "添加";
			}
			else
			{
				this.ieBLL.Update(inExPlanModel);
				str = "修改";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("parent.desktop.flowclass.location='/AccountManage/IncomeExpensePlan/InExPlanList.aspx';");
			stringBuilder.Append("alert('系统提示：\\n\\n" + str + "成功！');").Append(Environment.NewLine);
			stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n对不起" + str + "失败！');");
		}
	}
}


