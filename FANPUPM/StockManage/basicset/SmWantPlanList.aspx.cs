using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_basicset_SmWantPlanList : NBasePage, IRequiresSessionState
{
	private MaterialWantPlan wantPlan = new MaterialWantPlan();
	private string prjId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
			this.hndProCode.Value = this.prjId;
		}
		this.gvSm_Wantplan.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.btnDel.Attributes.Add("onclick", "if(!delCheck()){return false;}");
			this.btnUpdate.Attributes.Add("onclick", "if(!checkCount()){return false;}");
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
			this.lblProjectName.Text = byId.PrjName;
			this.hdnProname.Value = byId.PrjName;
			this.setDataSource(this.prjId);
		}
	}
	protected void setDataSource(string procode)
	{
		DataTable tableWantPlan = this.wantPlan.getTableWantPlan(procode);
		GridViewUtility.DataBind(this.gvSm_Wantplan, tableWantPlan);
	}
	protected void btnDel_ServerClick(object sender, System.EventArgs e)
	{
		string text = "";
		if (this.gvSm_Wantplan.Rows.Count > 0)
		{
			foreach (GridViewRow gridViewRow in this.gvSm_Wantplan.Rows)
			{
				CheckBox checkBox = (CheckBox)gridViewRow.FindControl("chkBox");
				if (checkBox.Checked)
				{
					text = text + checkBox.ToolTip + ",";
				}
			}
		}
		text = text.Substring(0, text.Length - 1);
		int num;
		if (text.IndexOf(",") == -1)
		{
			num = this.wantPlan.Delete(text);
		}
		else
		{
			string[] array = text.Split(new char[]
			{
				','
			});
			string text2 = "";
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string str = array2[i];
				text2 = text2 + str + "','";
			}
			text2 = text2.Substring(0, text2.Length - 2);
			string text3 = "delete Sm_wantPlan_Stock where wpcode in('" + text2 + ") ";
			text3 = text3 + "delete from Sm_WantPlan where swcode in('" + text2 + ") ";
			num = this.wantPlan.ExcuteSql(text3);
		}
		if (num > 0)
		{
			base.RegisterScript("top.ui.show('删除成功')");
		}
		else
		{
			base.RegisterScript("top.ui.show('删除失败')");
		}
		this.setDataSource(this.hndProCode.Value);
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		System.DateTime? startDate = null;
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
		{
			startDate = new System.DateTime?(System.Convert.ToDateTime(this.txtStartTime.Text.Trim()));
		}
		System.DateTime? endDate = null;
		if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
		{
			endDate = new System.DateTime?(System.Convert.ToDateTime(this.txtEndTime.Text.Trim()).AddDays(1.0));
		}
		DataTable dataSource = this.wantPlan.Select(startDate, endDate, this.txtSwcode.Text.Trim(), this.txtPeople.Text, this.hndProCode.Value, "");
		this.gvSm_Wantplan.DataSource = dataSource;
		this.gvSm_Wantplan.DataBind();
		base.RegisterScript("");
	}
	protected void btnUpdate_ServerClick(object sender, System.EventArgs e)
	{
		string arg_15_0 = base.Request.QueryString["pn"];
		string text = base.Request.QueryString["prjId"];
		if (this.gvSm_Wantplan.Rows.Count > 0)
		{
			foreach (GridViewRow gridViewRow in this.gvSm_Wantplan.Rows)
			{
				CheckBox checkBox = (CheckBox)gridViewRow.FindControl("chkBox");
				if (checkBox.Checked)
				{
					string str = string.Concat(new string[]
					{
						"/StockManage/basicset/SmWantPlan.aspx?prjId=",
						this.prjId,
						"&swcode=",
						checkBox.ToolTip,
						"&pcode=",
						text,
						"&optype=update"
					});
					base.RegisterScript("parent.parent.desktop.SmWantPlan = window;toolbox_oncommand('" + str + "', '编辑需求计划单');");
				}
			}
		}
	}
	protected void gvSm_Wantplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvSm_Wantplan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvSm_Wantplan.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["prjGuid"] = this.gvSm_Wantplan.DataKeys[e.Row.RowIndex].Values[2].ToString();
			e.Row.Attributes["auditContent"] = "物资需求计划：" + this.gvSm_Wantplan.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void gvSm_Wantplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvSm_Wantplan.PageIndex = e.NewPageIndex;
		this.setDataSource(this.hndProCode.Value);
	}
}


