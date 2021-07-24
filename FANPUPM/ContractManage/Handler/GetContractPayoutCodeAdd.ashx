<%@ WebHandler Language="C#" Class="GetContractPayoutCodeAdd" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Text;
using System.Web;
public class GetContractPayoutCodeAdd : IHttpHandler
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
		string contractID = context.Request["contractID"].ToString();
		StringBuilder stringBuilder = new StringBuilder();
		ConPayoutContractService conPayoutContractService = new ConPayoutContractService();
		ConPayoutContract byContractID = new ConPayoutContractService().GetByContractID(contractID);
		if (!string.IsNullOrEmpty(byContractID.ContractCode))
		{
			stringBuilder.Append(byContractID.ContractCode);
		}
		stringBuilder.Append("bc");
		DataTable dtblByPayoutContractCode = conPayoutContractService.GetDtblByPayoutContractCode(stringBuilder.ToString());
		for (int i = 1; i < 100; i++)
		{
			if (dtblByPayoutContractCode.Select("ContractCode='" + stringBuilder.ToString() + i.ToString().PadLeft(2, '0') + "'").Length == 0)
			{
				stringBuilder.Append(i.ToString().PadLeft(2, '0'));
				break;
			}
		}
		context.Response.Write(stringBuilder.ToString());
	}
}
