<%@ WebHandler Language="C#" Class="GetContractById" %>

using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using System;
using System.Text;
using System.Web;
public class GetContractById : IHttpHandler
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
		string text = context.Request["contractId"].ToString().Trim();
		IncometContractBll incometContractBll = new IncometContractBll();
		StringBuilder stringBuilder = new StringBuilder();
		if (!string.IsNullOrEmpty(text))
		{
			IncometContractModel model = incometContractBll.GetModel(text);
			if (model != null)
			{
				string str = Convert.ToDateTime(model.SignedTime).ToString("yyyy-MM-dd");
				string str2 = model.ReFundDate.HasValue ? Convert.ToDateTime(model.ReFundDate).ToString("yyyy-MM-dd") : "";
				string remark = model.Remark;
				stringBuilder.Append("[{\"signDate\":\"" + str + "\",");
				stringBuilder.Append("\"reFundDate\":\"" + str2 + "\",");
				stringBuilder.Append("\"remark\":\"" + remark + "\"}]");
			}
		}
		context.Response.Write(stringBuilder.ToString());
	}
}
