<%@ WebHandler Language="C#" Class="GetContractCodeSupply" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Text;
using System.Web;
public class GetContractCodeSupply : IHttpHandler
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
		string contractId = context.Request["contractID"].ToString();
		StringBuilder stringBuilder = new StringBuilder();
		ConIncometContractService conIncometContractService = new ConIncometContractService();
		ConIncometContract byContractId = conIncometContractService.GetByContractId(contractId);
		if (byContractId != null)
		{
			stringBuilder.Append(byContractId.ContractCode);
		}
		stringBuilder.Append("bc");
		string text = stringBuilder.ToString();
		DataTable dtblByContractCode = conIncometContractService.GetDtblByContractCode(text);
		for (int i = 1; i < 100; i++)
		{
			if (dtblByContractCode.Select("ContractCode='" + text + i.ToString().PadLeft(2, '0') + "'").Length == 0)
			{
				stringBuilder.Append(i.ToString().PadLeft(2, '0'));
				break;
			}
		}
		context.Response.Write(stringBuilder.ToString());
	}
}
