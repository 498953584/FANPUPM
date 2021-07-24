using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_SysAdmin_DeptSet_deptset2 : NBasePage, IRequiresSessionState
{
	protected int code;
	private PTdbmService dSer = new PTdbmService();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["sid"]))
		{
			this.code = Convert.ToInt32(base.Request["sid"].ToString());
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindDep();
		}
	}
	protected void gvwDep_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwDep.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	private void DataBindDep()
	{
		this.gvwDep.DataSource =
			from d in this.dSer.GetChildren(this.code)
			where d.C_sfyx == "y"
			orderby d.I_xh
			select d;
		this.gvwDep.DataBind();
	}
	protected void btnDelete_Click1(object sender, EventArgs e)
	{
		string value = this.hfldDepId.Value;
		string cmdText = string.Format("select Count(*) from PT_yhmc where i_bmdm = '{0}'\r\n                                          Union\r\n                                          select Count(*) from PT_DUTY where i_bmdm = '{0}'", value);
		DataTable dataTable = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
		if (dataTable != null && dataTable.Rows.Count > 0)
		{
			int num = 0;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				num += DBHelper.GetInt(dataTable.Rows[i][0]);
			}
			if (num == 0)
			{
				string value2 = this.dSer.Delete(value);
				if ("1".Equals(value2))
				{
					base.RegisterScript("alert('系统提示：数据删除成功！');");
					this.DataBindDep();
					return;
				}
				base.RegisterScript("alert('系统提示：数据删除失败！');");
				return;
			}
			else
			{
				base.RegisterScript("alert('系统提示：\\n\\项目下存在数据，删除失败！');");
			}
		}
	}
}


