using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_SelectOneUser : NBasePage, IRequiresSessionState
{
	private string depCode = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["depCode"]))
		{
			this.depCode = base.Request["depCode"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Init();
		}
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.Init();
	}
	protected void gvwUser_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwUser.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["name"] = this.gvwUser.DataKeys[e.Row.RowIndex].Values[1].ToString();
		}
	}
	private new void Init()
	{
		string[] arr = this.depCode.Split(new char[]
		{
			','
		});
		this.depCode = StringUtility.GetArrayToInStr(arr);
		string arg = this.txtUserName.Text.Trim();
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		stringBuilder.AppendFormat("\r\n\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY y.i_bmdm, y.v_yhdm) No, \r\n\t\t\t\ty.v_yhdm, y.v_xm, y.i_bmdm, d.V_BMMC\r\n\t\t\tFROM PT_yhmc y\r\n\t\t\tLEFT JOIN PT_d_bm d ON y.i_bmdm = d.i_bmdm\r\n\t\t\tWHERE y.c_sfyx = 'y' AND state = '1'\r\n\t\t\t\tAND y.v_xm LIKE '%{0}%'\r\n\t\t", arg);
		if (!string.IsNullOrEmpty(this.depCode))
		{
			stringBuilder.AppendFormat(" AND d.v_bmbm IN({0})", this.depCode);
		}
		this.gvwUser.DataSource = SqlHelper.ExecuteQuery(CommandType.Text, stringBuilder.ToString(), new SqlParameter[0]);
		this.gvwUser.DataBind();
	}
}


