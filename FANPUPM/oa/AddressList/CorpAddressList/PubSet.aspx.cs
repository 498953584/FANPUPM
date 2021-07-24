using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_AddressList_CorpAddressList_PubSet : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && base.Request.QueryString["iDept"] != "")
		{
			this.hidBmdm.Value = base.Request.QueryString["iDept"].ToString().Trim();
			this.bindpx(this.hidBmdm.Value.Trim());
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		string text = this.txtaddress.Text.Trim();
		string text2 = this.txtyb.Text.Trim();
		string text3 = this.txtfx.Text.Trim();
		string sqlString = string.Concat(new string[]
		{
			"UPDATE PT_d_bm set adss='",
			text,
			"',yb='",
			text2,
			"',fx='",
			text3,
			"' where i_bmdm='",
			this.hidBmdm.Value,
			"'"
		});
		int num = publicDbOpClass.ExecSqlString(sqlString);
		string text4 = "设置失败！";
		if (num == 1)
		{
			text4 = "设置成功";
		}
		base.ClientScript.RegisterStartupScript(base.GetType(), "", string.Concat(new string[]
		{
			" <script >top.ui.alert('系统设置','",
			text4,
			"');top.ui.closeWin();top.ui.callback(",
			this.hidBmdm.Value,
			");</script >"
		}));
	}
	public void bindpx(string _DeptCode)
	{
		string sqlString = "SELECT * FROM PT_d_bm  WHERE i_bmdm='" + _DeptCode + "'";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count >= 1)
		{
			this.txtaddress.Text = dataTable.Rows[0]["adss"].ToString().Trim();
			this.txtyb.Text = dataTable.Rows[0]["yb"].ToString().Trim();
			this.txtfx.Text = dataTable.Rows[0]["fx"].ToString().Trim();
		}
	}
}


