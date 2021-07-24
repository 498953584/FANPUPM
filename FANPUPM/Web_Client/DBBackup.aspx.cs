using ASP;
using com.jwsoft.pm.data;
using System;
using System.IO;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class DataBaseBackup : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (this.Session["DBTest"] == null && this.Session["DBTest"] != "DBSet")
		{
			base.Response.Write("请勿非法登录！");
			base.Response.End();
		}
		this.txtSql.Attributes["onpropertychange"] = "CheckSQL(this);";
		if (!base.IsPostBack)
		{
			string sqlString = "Exec sp_helpdb";
			this.ddlBackupDBList.DataSource = publicDbOpClass.DataTableQuary(sqlString);
			this.ddlBackupDBList.DataTextField = "name";
			this.ddlBackupDBList.DataBind();
			this.ddlRestoreDBList.DataSource = publicDbOpClass.DataTableQuary(sqlString);
			this.ddlRestoreDBList.DataTextField = "name";
			this.ddlRestoreDBList.DataBind();
			this.ddlSearchDBList.DataSource = publicDbOpClass.DataTableQuary(sqlString);
			this.ddlSearchDBList.DataTextField = "name";
			this.ddlSearchDBList.DataBind();
			this.ddlSearchDBList_SelectedIndexChanged(null, null);
		}
	}
	protected void btnBackup_Click(object sender, EventArgs e)
	{
		string sqlString = string.Concat(new string[]
		{
			"backup database ",
			this.ddlBackupDBList.SelectedValue,
			" to disk='",
			this.txtBackupFilePath.Text.Trim(),
			".bak'"
		});
		try
		{
			if (File.Exists(this.txtBackupFilePath.Text.Trim()))
			{
				base.Response.Write("<script language=javascript>alert('此文件已存在，请从新输入！');location='Default.aspx'</script>");
			}
			else
			{
				publicDbOpClass.ExecuteScalar(sqlString);
				base.Response.Write("<script language=javascript>alert('备份数据成功！');</script>");
			}
		}
		catch (Exception ex)
		{
			base.Response.Write(ex.Message);
			base.Response.Write("<script language=javascript>alert('备份数据失败！')</script>");
		}
	}
	protected void btnRestore_Click(object sender, EventArgs e)
	{
		string value = this.FileUploadRestore.Value;
		string selectedValue = this.ddlRestoreDBList.SelectedValue;
		string text = "  use master  declare @spid nvarchar(20)  declare getspid cursor for select spid=cast(spid as varchar(20)) from sysprocesses  WHERE dbid=db_id('" + selectedValue + "')  open getspid  fetch next from getspid into @spid  while @@fetch_status=0  begin \texec('kill '+@spid)  fetch next from getspid into @spid  end  close getspid  deallocate getspid ";
		string sqlString = string.Concat(new string[]
		{
			text,
			"use master restore database ",
			selectedValue,
			" from disk='",
			value,
			"' with replace, MOVE 'SubwayNew_Data' TO 'D:\\Program Files\\Microsoft SQL Server\\MSSQL.2\\MSSQL\\Data\\subway_Remote.mdf', MOVE 'SubwayNew_Log' TO 'D:\\Program Files\\Microsoft SQL Server\\MSSQL.2\\MSSQL\\Data\\subway_Remote_log.ldf'"
		});
		try
		{
			publicDbOpClass.ExecuteScalar(sqlString);
			base.Response.Write("<script language=javascript>alert('还原数据成功！');</script>");
		}
		catch (Exception ex)
		{
			base.Response.Write(ex.Message);
			base.Response.Write("<script language=javascript>alert('还原数据失败！')</script>");
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		string sqlString = "use [" + this.ddlSearchDBList.SelectedValue + "] " + this.txtSql.Text.Trim();
		try
		{
			this.GVTable.DataSource = publicDbOpClass.DataTableQuary(sqlString);
			this.GVTable.DataBind();
		}
		catch (Exception ex)
		{
			base.Response.Write(ex.Message);
			base.Response.Write("<script language=javascript>alert('查询数据失败！')</script>");
		}
	}
	protected void ddlSearchDBList_SelectedIndexChanged(object sender, EventArgs e)
	{
		string sqlString = " use [" + this.ddlSearchDBList.SelectedValue + "]  select name from sys.objects where type='U'";
		this.ddlSearchTableList.DataSource = publicDbOpClass.DataTableQuary(sqlString);
		this.ddlSearchTableList.DataTextField = "name";
		this.ddlSearchTableList.DataBind();
	}
	protected void GVTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVTable.PageIndex = e.NewPageIndex;
		this.btnSearch_Click(null, null);
	}
	protected void btnUpdate_Click(object sender, EventArgs e)
	{
		string sqlString = this.txtSql.Text.Replace(char.ConvertFromUtf32(32), " ").Replace(char.ConvertFromUtf32(13) + char.ConvertFromUtf32(10), "\r\n");
		try
		{
			publicDbOpClass.ExecuteScalar(sqlString);
			base.Response.Write("<script language=javascript>alert('更新成功！');</script>");
		}
		catch (Exception ex)
		{
			base.Response.Write(ex.Message);
			base.Response.Write("<script language=javascript>alert('更新失败！')</script>");
		}
	}
}


