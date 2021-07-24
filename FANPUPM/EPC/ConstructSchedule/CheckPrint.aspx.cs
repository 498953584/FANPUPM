using ASP;
using cn.justwin.DAL;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_ConstructSchedule_CheckPrint : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.BtnAnnex.Visible = false;
			this.lblrecord.Enabled = false;
			string condition = "where PrjGuid='" + this.Session["PrjGuid"].ToString() + "'";
			DataTable table = DBHelper.GetTable("PT_PrjInfo", condition);
			string text = table.Rows[0]["PrjManager"].ToString();
			int num = text.IndexOf("-");
			this.lblblame.Text = text.Substring(num + 1).ToString();
			this.lblblame.Enabled = false;
			DataTable info = this.GetInfo(base.Request.QueryString["LogID"].ToString());
			DataRow dataRow = info.Rows[0];
			this.hdnRecordId.Value = base.Request.QueryString["LogID"].ToString();
			this.lblcode.Text = dataRow["code"].ToString();
			this.lblamweather.Text = dataRow["amweather"].ToString();
			this.lbloperations.Text = dataRow["operations"].ToString();
			this.lbltime.Text = ((DateTime)dataRow["thisDate"]).ToShortDateString();
			DateTime dateTime = (DateTime)dataRow["thisDate"];
			int month = dateTime.Month;
			int year = dateTime.Year;
			int day = dateTime.Day;
			switch (EPC_ConstructSchedule_CheckPrint.getWeekDay(year, month, day))
			{
			case 1:
				this.lbWeek.Text = "星期一";
				break;
			case 2:
				this.lbWeek.Text = "星期二";
				break;
			case 3:
				this.lbWeek.Text = "星期三";
				break;
			case 4:
				this.lbWeek.Text = "星期四";
				break;
			case 5:
				this.lbWeek.Text = "星期五";
				break;
			case 6:
				this.lbWeek.Text = "星期六";
				break;
			case 7:
				this.lbWeek.Text = "星期日";
				break;
			}
			this.lbldaycontent.Text = dataRow["daycontent"].ToString();
			this.lbldaywindpower.Text = dataRow["design"].ToString();
			this.lblnightwindpower.Text = dataRow["acceptance"].ToString();
			this.lblbeton.Text = dataRow["beton"].ToString();
			this.txtdatum.Text = dataRow["datum"].ToString();
			this.lblstuffenter.Text = dataRow["product"].ToString();
			this.lblremark.Text = dataRow["remark"].ToString();
			this.lblPart.Text = dataRow["part"].ToString();
			this.lblpmweather.Text = dataRow["pmweather"].ToString();
			this.lblnightPart.Text = dataRow["temperature"].ToString();
			this.lblrecord.Text = dataRow["RecordUser"].ToString();
			this.BtnPrint.Value = "打印";
			this.FilesBind(5);
		}
	}
	public DataTable GetInfo(string logid)
	{
		string sqlString = "select * from pm_Construction_Log where logID='" + logid + "'";
		return publicDbOpClass.DataTableQuary(sqlString);
	}
	protected void FilesBind(int moduleID)
	{
		this.Literal1.Text = "";
		string recordCode = base.Request.QueryString["LogID"].ToString();
		int annexType = 0;
		AnnexAction annexAction = new AnnexAction();
		DataTable fileList = annexAction.GetFileList(recordCode, annexType, moduleID);
		foreach (DataRow dataRow in fileList.Rows)
		{
			Literal expr_63 = this.Literal1;
			string text = expr_63.Text;
			expr_63.Text = string.Concat(new string[]
			{
				text,
				"<a href='#' onclick=\"javascript:download('",
				dataRow["FilePath"].ToString(),
				dataRow["AnnexName"].ToString(),
				"','",
				dataRow["OriginalName"].ToString(),
				"');\" >",
				dataRow["OriginalName"].ToString(),
				"</a> <br>"
			});
		}
	}
	public static int getWeekDay(int y, int m, int d)
	{
		if (m == 1)
		{
			m = 13;
		}
		if (m == 2)
		{
			m = 14;
		}
		return (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7 + 1;
	}
}


