using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_SupplierGrade_SupplierGrade : NBasePage, IRequiresSessionState
{
	public string superID = "";
	public string billsid = "";
	public string goodsid = "";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["SUPID"] != null && !base.Request.QueryString["SUPID"].ToString().Equals(""))
			{
				this.superID = base.Request.QueryString["SUPID"].ToString().Trim();
				this.hidSuperId.Value = this.superID;
			}
			if (base.Request.QueryString["BILLSID"] != null && !base.Request.QueryString["BILLSID"].ToString().Equals(""))
			{
				this.billsid = base.Request.QueryString["BILLSID"].ToString().Trim();
				this.hidBillsId.Value = this.billsid;
			}
			if (base.Request.QueryString["goodsid"] != null && !base.Request.QueryString["goodsid"].ToString().Equals(""))
			{
				this.goodsid = base.Request.QueryString["goodsid"].ToString().Trim();
				this.hidgoodsid.Value = this.goodsid;
			}
		}
	}
	protected void BtnOk_Click(object sender, EventArgs e)
	{
		if (this.hidSuperId.Value != "")
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			if (this.hidsx.Value.ToString() != "")
			{
				num = int.Parse(this.hidsx.Value);
			}
			if (this.hidnum.Value.ToString() != "")
			{
				num2 = int.Parse(this.hidnum.Value);
			}
			if (this.hidzl.Value.ToString() != "")
			{
				num3 = int.Parse(this.hidzl.Value);
			}
			if (this.hidxh.Value.ToString() != "")
			{
				num4 = int.Parse(this.hidxh.Value);
			}
			if (this.hidsj.Value.ToString() != "")
			{
				num5 = int.Parse(this.hidsj.Value);
			}
			if (this.hidtd.Value.ToString() != "")
			{
				num6 = int.Parse(this.hidtd.Value);
			}
			string text = this.Session["yhdm"].ToString().Trim();
			string sqlString = string.Concat(new object[]
			{
				"insert into Res_SuperGradeTab(superid ,billsid ,goodsid,gradePeopid ,fileisall ,numisover ,lookisgood ,tpyeisright ,timeisquk ,smallisok) VALUES ('",
				this.hidSuperId.Value,
				"','",
				this.hidBillsId.Value,
				"','",
				this.hidgoodsid.Value,
				"','",
				text,
				"','",
				num,
				"','",
				num2,
				"','",
				num3,
				"','",
				num4,
				"','",
				num5,
				"','",
				num6,
				"')"
			});
			try
			{
				publicDbOpClass.NonQuerySqlString(sqlString);
				base.RegisterScript("alert('系统提示：\\n\\n评分成功！');window.close();");
				return;
			}
			catch
			{
				base.RegisterScript("alert('系统提示：\\n\\n评分失败！');window.close();");
				return;
			}
		}
		base.RegisterScript("alert('系统提示：\\n\\n评分失败，供应商无效！');window.close();");
	}
}


