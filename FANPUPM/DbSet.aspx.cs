using ASP;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
public partial class DbSet : Page, IRequiresSessionState
{
	private bool result;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.Request.IsLocal)
		{
			base.Response.Write("数据库设置功能只允许在服务器上操作！");
			base.Response.End();
		}
	}
	protected void Button3_Click(object sender, EventArgs e)
	{
		SqlConnection sqlConnection = new SqlConnection();
		sqlConnection.ConnectionString = string.Format("Data Source={0};Initial Catalog={1};User id ={2};Password={3}", new object[]
		{
			this.TextBox1.Text,
			this.TextBox2.Text,
			this.TextBox3.Text,
			this.TextBox4.Text
		});
		try
		{
			new SqlCommand(sqlConnection.ConnectionString);
		}
		catch
		{
			base.Response.Write("链接字符串出错");
		}
		try
		{
			sqlConnection.Open();
		}
		catch
		{
			base.Response.Write("数据库连接失败");
		}
		try
		{
			if (sqlConnection.State == ConnectionState.Open)
			{
				base.Response.Write("数据库连接成功！");
				this.result = true;
				sqlConnection.Close();
			}
			else
			{
				base.Response.Write("数据库连接失败！");
			}
		}
		catch
		{
			base.Response.Write("数据库连接失败！");
		}
	}
	protected void Button2_Click(object sender, EventArgs e)
	{
		this.WriteWebConfig();
	}
	public void WriteWebConfig()
	{
		if (this.TextBox1.Text.ToString().Trim() == "" || this.TextBox2.Text.ToString().Trim() == "")
		{
			return;
		}
		FileInfo fileInfo = new FileInfo(base.Server.MapPath("web.config"));
		if (!fileInfo.Exists)
		{
			base.Response.Write("文件丢失：" + base.Server.MapPath("./") + "\\Web.config");
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(fileInfo.FullName);
		bool flag = false;
		foreach (XmlNode xmlNode in xmlDocument["configuration"]["connectionStrings"])
		{
			if (xmlNode.Name == "add")
			{
				try
				{
					if (xmlNode.Attributes[1].Name == "connectionString")
					{
						xmlNode.Attributes[1].Value = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User id ={2};Password={3};Max Pool Size=4048", new object[]
						{
							this.TextBox1.Text,
							this.TextBox2.Text,
							this.TextBox3.Text,
							this.TextBox4.Text
						});
						flag = true;
					}
				}
				catch (Exception)
				{
					flag = false;
				}
			}
		}
		if (!flag)
		{
			base.Response.Write("修改连接失败!!");
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('修改连接失败');", true);
		}
		try
		{
			xmlDocument.Save(fileInfo.FullName);
			base.Response.Write("修改连接成功!");
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('修改连接成功');", true);
		}
		catch (Exception)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('保存失败');", true);
		}
	}
}


