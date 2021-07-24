using ASP;
using cn.justwin.opm.PM.prj_plan;
using cn.justwin.PrjManager;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
public partial class OPM_PM_PrjInfo_PrjInfoPop : NBasePage, IRequiresSessionState
{
	public string Explain;
	public string GetImgstr;
	public string GetFirstImgstr;
	public string PrjLink;
	public int imgcount = 5;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (string.IsNullOrEmpty(base.Request.QueryString["id"]))
			{
				base.RegisterScript("btn_close_onclick()");
				return;
			}
			if (!string.IsNullOrEmpty(base.Request.QueryString["pc"]))
			{
				ProjectInfo byId = ProjectInfo.GetById(base.Request.QueryString["pc"]);
				DataTable thumbnail = new PrjInfoPhotoAction().GetThumbnail(base.Request.QueryString["pc"]);
				this.Explain = "工程概况:" + (string.IsNullOrEmpty(byId.PrjInfo) ? "" : byId.PrjInfo);
				this.GetImgstr = this.GetImgStr(thumbnail, "tpath", string.Empty, string.Empty);
				this.GetFirstImgstr = this.GetFirstImgStr(thumbnail, "tpath", "");
				this.PrjLink = (string.IsNullOrEmpty(base.Request.QueryString["pn"]) ? "缩略图" : ("<a href='javascript:void(0)' onclick='ParentView();'>" + base.Request.QueryString["pn"].ToString() + "</a>"));
				return;
			}
			if (!string.IsNullOrEmpty(base.Request.QueryString["intendanceguid"]))
			{
				DataTable dataTable = publicDbOpClass.DataTableQuary(" select ii.questionexplain,ip.PhotoExplain,(ip.Photopath+ip.PhotoName) as Photopath from OPM_EPCM_IntendancePhotoList ip,OPM_EPCM_IntendanceInfo ii where ii.noteid=ip.infoguid and intendanceguid='" + base.Request.QueryString["intendanceguid"] + "' ");
				this.Explain = "问题描述:" + ((dataTable.Rows.Count > 0) ? dataTable.Rows[0]["questionexplain"].ToString() : string.Empty);
				this.GetImgstr = this.GetImgStr(dataTable, "Photopath", "PhotoExplain", "");
				this.GetFirstImgstr = this.GetFirstImgStr(dataTable, "Photopath", (dataTable.Rows.Count > 0) ? dataTable.Rows[0]["PhotoExplain"].ToString() : string.Empty);
				this.PrjLink = (string.IsNullOrEmpty(base.Request.QueryString["pn"]) ? "缩略图" : ("<a href='javascript:void(0)' onclick='ParentView();'>" + base.Request.QueryString["pn"].ToString() + "</a>"));
			}
		}
	}
	public string GetImgStr(DataTable dt, string pathcolumnname, string titlecolumnname, string imgtitle)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int num = (dt.Rows.Count - this.imgcount >= 0) ? (dt.Rows.Count - this.imgcount) : 0;
		int num2 = 0;
		while (num2 < dt.Rows.Count && num2 != this.imgcount)
		{
			stringBuilder.Append(string.Concat(new object[]
			{
				"<div onmouseover=\"this.style.color='black'\" onmouseout=\"this.style.color='white'\"  onclick=\"ChangeImg('",
				dt.Rows[dt.Rows.Count - (num2 + 1 + num)][pathcolumnname].ToString(),
				"','",
				string.IsNullOrEmpty(imgtitle) ? (string.IsNullOrEmpty(titlecolumnname) ? string.Empty : dt.Rows[dt.Rows.Count - (num2 + 1 + num)][titlecolumnname].ToString()) : imgtitle,
				"',this)\">",
				dt.Rows.Count - num2 - num,
				"</div>"
			}));
			num2++;
		}
		return stringBuilder.ToString();
	}
	public string GetFirstImgStr(DataTable dt, string pathcoulumnname, string title)
	{
		return string.Concat(new string[]
		{
			" src=\"",
			(dt.Rows.Count > 0) ? dt.Rows[0][pathcoulumnname].ToString() : "Scripts/PrjInfomoveBmp/images//nopic.png",
			"\" ",
			string.IsNullOrEmpty(title) ? string.Empty : (" title=\"" + title + "\" "),
			" "
		});
	}
}


