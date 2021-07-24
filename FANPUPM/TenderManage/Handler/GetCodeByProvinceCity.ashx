<%@ WebHandler Language="C#" Class="GetCodeByProvinceCity" %>

using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Tender;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
public class GetCodeByProvinceCity : IHttpHandler
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
		if (!string.IsNullOrEmpty(context.Request["province"]) && !string.IsNullOrEmpty(context.Request["city"]))
		{
			StringBuilder stringBuilder = new StringBuilder();
			string provinceName = context.Request["province"];
			string cityName = context.Request["city"].ToString();
			BasicProvince modelByProvinceName = new BasicProvinceService().GetModelByProvinceName(provinceName);
			BasicCity modelByCityName = new BasicCityService().GetModelByCityName(cityName);
			BasicArea areaByID = new BasicAreaService().GetAreaByID(modelByProvinceName.AreaId.ToString());
			string text = areaByID.Code + modelByProvinceName.Code + modelByCityName.Code;
			if (text.Length < 6)
			{
				context.Response.Write("");
				return;
			}
			if (text.ToLower() == "hngdgz")
			{
				text = "gzgzgz";
			}
			stringBuilder.Append(ConfigurationManager.AppSettings["CompanyCode"].ToString());
			stringBuilder.Append(text.ToLower());
			stringBuilder.Append(DateTime.Now.ToString("yyyyMM"));
			DataTable dtblProjectCode = TenderInfo.GetDtblProjectCode("PT_PrjInfo", stringBuilder.ToString());
			DataTable dtblProjectCode2 = TenderInfo.GetDtblProjectCode("PT_PrjInfo_ZTB", stringBuilder.ToString());
			for (int i = 1; i < 1000; i++)
			{
				string str = stringBuilder.ToString() + i.ToString().PadLeft(3, '0');
				DataRow[] array = dtblProjectCode.Select("PrjCode='" + str + "'");
				DataRow[] array2 = dtblProjectCode2.Select("PrjCode='" + str + "'");
				if (array.Length == 0 && array2.Length == 0)
				{
					stringBuilder.Append(i.ToString().PadLeft(3, '0'));
					break;
				}
			}
			context.Response.Write(stringBuilder.ToString());
		}
	}
}
