<%@ WebHandler Language="C#" Class="GetSubProject" %>

using cn.justwin.BLL;
using cn.justwin.Domain.EasyUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
public class GetSubProject : IHttpHandler, IRequiresSessionState
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
		string userCode = context.Session["yhdm"].ToString();
		int year = Convert.ToInt32(context.Request["year"]);
		string a = context.Request["showType"];
		string text = context.Request["id"];
		string a2 = context.Request["stateType"];
		string text2 = context.Request["pcode"];
		IList<string> stateList = new List<string>();
		if (a2 == "1")
		{
			stateList = Parameters.PrjAvaildState;
		}
		if (a2 == "2")
		{
			stateList = Parameters.PrjAvaildState2;
		}
		if (a2 == "3")
		{
			stateList = Parameters.PrjAvaildState3;
		}
		if (a2 == "4")
		{
			stateList = Parameters.PrjAvaildState4;
		}
		if (a2 == "5")
		{
			stateList = Parameters.PrjAvaildState5;
		}
		new Project();
		IList<Project> source = new List<Project>();
		if (a == "0")
		{
			source = ProjectTree2.GetSublevelByParent(year, userCode, text, stateList);
		}
		else
		{
			source = ProjectTree2.GetSublevelByState(year, userCode, text, text2, text2);
		}
		string s = JsonHelper.JsonSerializer<Project[]>(source.ToArray<Project>());
		context.Response.Write(s);
	}
}
