using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_IncomeExpensePlan_InExPlanView : NBasePage, IRequiresSessionState
{
	private InExPlanBLL ieBLL = new InExPlanBLL();
	private PrjInfo prj = new PrjInfo();
	private IEPInfoBLL IEPInfo = new IEPInfoBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdfID.Value = base.Request["ic"].ToString();
			this.bind();
			this.gvwBind();
		}
	}
	public void bind()
	{
		string id = base.Request["ic"].ToString();
		InExPlanModel model = this.ieBLL.GetModel(id);
		this.txtCode.Text = model.IEPNum.ToString();
		this.txtData.Text = Convert.ToDateTime(model.IEPdate).ToShortDateString();
		this.txtName.Text = model.IEPname.ToString();
		this.txtType.Text = "";
		if (model.IEPtype.ToString() == "0")
		{
			this.txtType.Text = "月度";
		}
		else
		{
			if (model.IEPtype.ToString() == "1")
			{
				this.txtType.Text = "季度";
			}
			else
			{
				if (model.IEPtype.ToString() == "2")
				{
					this.txtType.Text = "年度";
				}
			}
		}
		this.hdfCropCode.Value = model.prjNum;
		this.hdfCropName.Value = this.prj.GetModelByPrjGuid(this.hdfCropCode.Value.ToString()).PrjName;
		this.txtProjectName.Value = this.hdfCropName.Value;
	}
	public void gvwBind()
	{
		this.gvwIEPInfo.DataSource = this.IEPInfo.GetList(this.hdfID.Value);
		this.gvwIEPInfo.DataBind();
	}
	protected void gvwIEPInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["ID"] = this.gvwIEPInfo.DataKeys[e.Row.RowIndex].Values[0].ToString();
		}
	}
	protected void gvwIEPInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwIEPInfo.PageIndex = e.NewPageIndex;
		this.gvwBind();
	}
}


