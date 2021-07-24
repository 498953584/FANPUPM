using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_SmOutReserve_StuffList : NBasePage, IRequiresSessionState
{
	private TreasuryStockBll treasuryStockBll = new TreasuryStockBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		this.gvNeedNote.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.BindGv();
			this.hdtp.Value = "0";
		}
	}
	public void BindGv()
	{
		this.BindGv(this.treasuryStockBll.GetListArrayByTcode(base.Request.QueryString["tcode"]));
		List<string> list = new List<string>();
		if (this.hdTsid.Value != "")
		{
			list = JsonHelper.GetListFromJson(this.hdTsid.Value);
		}
		foreach (GridViewRow gridViewRow in this.gvNeedNote.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
			if (checkBox != null)
			{
				HiddenField hiddenField = gridViewRow.FindControl("hdScode") as HiddenField;
				HiddenField hiddenField2 = gridViewRow.FindControl("hdSprice") as HiddenField;
				HiddenField hiddenField3 = gridViewRow.FindControl("hdCorp") as HiddenField;
				string item = string.Concat(new string[]
				{
					hiddenField.Value,
					"|",
					hiddenField2.Value,
					"|",
					hiddenField3.Value,
					"|"
				});
				if (list.Contains(item))
				{
					checkBox.Checked = true;
				}
			}
		}
	}
	public void BindGv(DataTable dataSource)
	{
		if (dataSource.Rows.Count == 0)
		{
			dataSource.Rows.Add(dataSource.NewRow());
			this.gvNeedNote.DataSource = dataSource;
			this.gvNeedNote.DataBind();
			this.gvNeedNote.HeaderRow.Cells[0].Visible = false;
			this.gvNeedNote.Rows[0].Visible = false;
			return;
		}
		this.gvNeedNote.DataSource = dataSource;
		this.gvNeedNote.DataBind();
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("$(parent.document).find('.ui-icon-closethick')[0].click();");
		stringBuilder.Append("parent.document.getElementById('hdTsid').value=document.getElementById('hdTsid').value;");
		stringBuilder.Append("parent.document.getElementById('btnShowGv').click();");
		base.RegisterScript(stringBuilder.ToString());
	}
	public void tsid()
	{
		List<string> list = new List<string>();
		if (this.hdTsid.Value != "")
		{
			list = JsonHelper.GetListFromJson(this.hdTsid.Value);
		}
		foreach (GridViewRow gridViewRow in this.gvNeedNote.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
			if (checkBox != null)
			{
				HiddenField hiddenField = gridViewRow.FindControl("hdScode") as HiddenField;
				HiddenField hiddenField2 = gridViewRow.FindControl("hdSprice") as HiddenField;
				HiddenField hiddenField3 = gridViewRow.FindControl("hdCorp") as HiddenField;
				string item = string.Concat(new string[]
				{
					hiddenField.Value,
					"|",
					hiddenField2.Value,
					"|",
					hiddenField3.Value,
					"|"
				});
				if (checkBox.Checked)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				else
				{
					if (list.Contains(item))
					{
						list.Remove(item);
					}
				}
			}
		}
		this.hdTsid.Value = JsonHelper.Serialize(list.ToArray());
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv(this.treasuryStockBll.GetListArrayByParam(this.txtScode.Text.Trim().Split(new char[]
		{
			','
		}), this.txtResourceName.Text.Trim().Split(new char[]
		{
			','
		}), this.txtCorp.Text.Trim(), base.Request.QueryString["tcode"]));
	}
	protected void chkBox_CheckedChanged(object sender, EventArgs e)
	{
		if (this.hdtp.Value == "0")
		{
			this.tsid();
		}
	}
	protected void gvNeed_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvNeedNote.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
}


