<%@ WebHandler Language="C#" Class="GetPayOutFKCode" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Text;
using System.Web;
public class GetPayOutFKCode : IHttpHandler
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
		string contractID = context.Request["contractID"].ToString();
		ConPayoutContract byContractID = new ConPayoutContractService().GetByContractID(contractID);
		if (byContractID != null)
		{
			stringBuilder.Append(byContractID.ContractCode);
		}
		stringBuilder.Append("fk");
		DataTable dtblByContractCode = new ConPayoutPaymentService().GetDtblByContractCode(stringBuilder.ToString());
		for (int i = 1; i < 1000; i++)
		{
			DataRow[] array = dtblByContractCode.Select("PaymentCode='" + stringBuilder.ToString() + i.ToString().PadLeft(3, '0') + "'");
			if (array.Length == 0)
			{
				stringBuilder.Append(i.ToString().PadLeft(3, '0'));
				break;
			}
		}
		context.Response.Write(stringBuilder.ToString());
	}
}
