<%@ WebHandler Language="C#" Class="GetIncometPaySKCode" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Text;
using System.Web;
public class GetIncometPaySKCode : IHttpHandler
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
		string contractId = context.Request["contractID"].ToString();
		ConIncometContract byContractId = new ConIncometContractService().GetByContractId(contractId);
		if (byContractId != null)
		{
			stringBuilder.Append(byContractId.ContractCode);
		}
		stringBuilder.Append("sk");
		DataTable dtblByContractCode = new ConIncometPaymentService().GetDtblByContractCode(stringBuilder.ToString());
		for (int i = 1; i < 1000; i++)
		{
			DataRow[] array = dtblByContractCode.Select("CllectionCode='" + stringBuilder.ToString() + i.ToString().PadLeft(3, '0') + "'");
			if (array.Length == 0)
			{
				stringBuilder.Append(i.ToString().PadLeft(3, '0'));
				break;
			}
		}
		context.Response.Write(stringBuilder.ToString());
	}
}
