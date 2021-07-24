<%@ WebHandler Language="C#" Class="GetPayoutCountractCode" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Text;
using System.Web;
public class GetPayoutCountractCode : IHttpHandler
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
		StringBuilder stringBuilder = new StringBuilder();
		string text = context.Request["PrjCode"].ToString();
		string text2 = context.Request["ContractTypeID"].ToString();
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
		{
			context.Response.Write("");
			return;
		}
		ConContractType byID = new ConContractTypeService().GetByID(text2);
		PTPrjInfo byId = new PTPrjInfoService().GetById(text);
		if (byId != null)
		{
			stringBuilder.Append(byId.PrjCode);
		}
		if (!string.IsNullOrEmpty(byID.TypeShort))
		{
			stringBuilder.Append(byID.TypeShort.ToString());
		}
		DataTable dtblByPayoutContractCode = new ConPayoutContractService().GetDtblByPayoutContractCode(stringBuilder.ToString());
		for (int i = 1; i < 1000; i++)
		{
			if (dtblByPayoutContractCode.Select("ContractCode='" + stringBuilder.ToString() + i.ToString().PadLeft(3, '0') + "'").Length == 0)
			{
				stringBuilder.Append(i.ToString().PadLeft(3, '0'));
				break;
			}
		}
		context.Response.Write(stringBuilder.ToString());
	}
}
