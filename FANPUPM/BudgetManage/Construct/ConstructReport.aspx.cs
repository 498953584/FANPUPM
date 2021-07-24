using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Construct_ConstructReport : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.hfldYear.Value = this.year;
			this.BindGv();
			this.hfldPrjId.Value = this.prjId;
			this.SetBudFlowState();
		}
	}
	protected void BindGv()
	{
		int recordCount = ConstructReport.GetByPrj(this.prjId).Count<ConstructReport>();
		this.AspNetPager1.RecordCount = recordCount;
		System.Collections.Generic.IList<ConstructReport> dataSource = ConstructReport.GetByPrj(this.prjId).Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize).ToList<ConstructReport>();
		this.gvConstruct.DataSource = dataSource;
		this.gvConstruct.DataBind();
	}
	protected void gvConstruct_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string text = this.gvConstruct.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["guid"] = text;
			string value = this.gvConstruct.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["state"] = value;
			e.Row.Attributes["prjGuid"] = this.gvConstruct.DataKeys[e.Row.RowIndex].Values[2].ToString();
			e.Row.Style.Add("cursor", "default");
			Image image = new Image();
			image.ImageUrl = "../../images/tree/more.gif";
			image.ToolTip = "详细信息";
			image.ID = "imgMore";
			image.Attributes.Add("rowId", text);
			image.Attributes["onclick"] = "showInfo('" + text + "')";
			image.Style.Add("vertical-align", "middle");
			image.Style.Add("float", "right");
			image.Style.Add("cursor", "hand");
			e.Row.Cells[2].Controls.Add(image);
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
		try
		{
			foreach (string current in listFromJson)
			{
				SelfEventAction.SuperDelete(current, "Bud_ConsReport", "FlowState");
			}
			ConstructReport.Delete(listFromJson);
			base.RegisterScript("$('#btnBindResTask').hide();setWidthAndHeight(); alert('系统提示：\\n\\删除成功！')");
			this.BindGv();
		}
		catch
		{
			base.RegisterScript("setWidthAndHeight();alert('系统提示：\\n\\n删除失败！');");
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	private void SetBudFlowState()
	{
		BudTaskChangeService budTaskChangeService = new BudTaskChangeService();
		cn.justwin.Domain.Entities.BudTaskChange byPrj = budTaskChangeService.GetByPrj(this.prjId);
		int num = -1;
		if (byPrj != null && byPrj.FlowState.HasValue)
		{
			num = byPrj.FlowState.Value;
		}
		this.hfldBudFlowState.Value = num.ToString();
	}
}


