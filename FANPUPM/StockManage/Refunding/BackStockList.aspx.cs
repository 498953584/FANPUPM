using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Refunding_BackStockList : NBasePage, IRequiresSessionState
{
	private PTPrjInfoBll ptPrjInfoBll = new PTPrjInfoBll();
	private OutReserveBll outReserveBll = new OutReserveBll();
	private OutStockBll outStockBll = new OutStockBll();

	protected override void OnInit(EventArgs e)
	{
		this.gvNeed.PageSize = NBasePage.pagesize3;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindNeedGv();
			this.BindNeedNodeGv();
			this.hdtp.Value = "0";
		}
	}
	public void BindNeedGv()
	{
		string procode = base.Request.QueryString["prcode"];
		string arg = base.Request.QueryString["tcode"];
		string strWhere = string.Format(" AND isOut='1' AND p1.tcode='{0}'", arg);
		DataTable listByParm = this.outReserveBll.GetListByParm(this.txtSwcode.Text.Trim(), this.txtStartTime.Text, this.txtEndTime.Text, "", procode, "", strWhere);
		if (listByParm.Rows.Count == 0)
		{
			listByParm.Rows.Add(listByParm.NewRow());
			this.gvNeed.DataSource = listByParm;
			this.gvNeed.DataBind();
			this.gvNeed.HeaderRow.Cells[0].Visible = false;
			this.gvNeed.Rows[0].Visible = false;
		}
		else
		{
			this.gvNeed.DataSource = listByParm;
			this.gvNeed.DataBind();
		}
		foreach (GridViewRow gridViewRow in this.gvNeed.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			if (checkBox != null && this.hdwzId.Value.Contains(checkBox.ToolTip))
			{
				checkBox.Checked = true;
			}
		}
	}
	public void BindNeedNodeGv()
	{
		DataTable listByOrcode = this.outStockBll.GetListByOrcode(StringUtility.GetArrayToInStr(this.hdwzId.Value.Split(new char[]
		{
			','
		})));
		if (listByOrcode.Rows.Count == 0)
		{
			listByOrcode.Rows.Add(listByOrcode.NewRow());
			this.gvNeedNote.DataSource = listByOrcode;
			this.gvNeedNote.DataBind();
			this.gvNeedNote.Rows[0].Visible = false;
			return;
		}
		this.gvNeedNote.DataSource = listByOrcode;
		this.gvNeedNote.DataBind();
	}
	protected void chkBox_CheckedChanged(object sender, EventArgs e)
	{
		if (this.hdtp.Value != "0")
		{
			return;
		}
		string text = "";
		if (this.hdwzId.Value != "")
		{
			text = this.hdwzId.Value;
		}
		foreach (GridViewRow gridViewRow in this.gvNeed.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			if (checkBox != null && checkBox.Checked && !text.Contains(checkBox.ToolTip))
			{
				text = text + checkBox.ToolTip + ",";
			}
			if (checkBox != null && !checkBox.Checked && text.Contains(checkBox.ToolTip))
			{
				text = text.Remove(text.IndexOf(checkBox.ToolTip, 0), checkBox.ToolTip.Length + 1);
			}
		}
		this.hdwzId.Value = text;
		this.BindNeedNodeGv();
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.BindNeedGv();
	}
	protected void gvNeed_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvNeed.PageIndex = e.NewPageIndex;
		this.BindNeedGv();
		this.BindNeedNodeGv();
	}
}


