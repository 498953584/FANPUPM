using ASP;
using cn.justwin.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Z_Demo_TestDTC : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_06_0 = base.IsPostBack;
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		using (new TransactionScope())
		{
			string arg = Guid.NewGuid().ToString();
			string arg2 = Guid.NewGuid().ToString();
			string cmdText = string.Format("INSERT INTO Basic_Config VALUES('{0}', '{0}', '{0}', '{0}')", arg);
			string cmdText2 = string.Format("INSERT INTO Basic_Config VALUES('{0}', '{0}', '{0}', '{0}')", arg2);
			string cmdText3 = "SELECT COUNT(*) FROM Basic_Config;";
			try
			{
				int @int = DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText3, new SqlParameter[0]));
				SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[0]);
				SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText2, new SqlParameter[0]);
				int int2 = DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText3, new SqlParameter[0]));
				if (@int + 2 != int2)
				{
					throw new Exception("测试失败");
				}
				throw new Exception("MyError");
			}
			catch (Exception ex)
			{
				if (ex.Message == "MyError")
				{
					base.Response.Write("测试成功");
				}
				else
				{
					base.Response.Write("测试失败");
				}
			}
		}
	}
}


