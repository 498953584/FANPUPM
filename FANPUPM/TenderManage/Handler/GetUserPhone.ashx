<%@ WebHandler Language="C#" Class="GetUserPhone" %>

using cn.justwin.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
public class GetUserPhone : IHttpHandler
{
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		context.Response.ContentType = "text/plain";
		if (!string.IsNullOrEmpty(context.Request["usercode"]))
		{
			string phone = this.GetPhone(context.Request["userCode"].ToString());
			context.Response.Write(phone);
		}
	}
	public string GetPhone(string userCode)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat(" SELECT MobilePhoneCode FROM PT_yhmc WHERE v_yhdm='{0}' ", userCode);
		return SqlHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), new SqlParameter[0]).ToString();
	}
}
