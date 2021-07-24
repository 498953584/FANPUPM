using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
public partial class PlatForm_ShowInfo_moveBmp_moveBmp : SubwayBasePage, IRequiresSessionState
{
	public DataTable PhotoDat = new DataTable();
	public int PhotoCount;
	private string prjID = "";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(base.Request.QueryString["Ptype"]))
		{
			base.Response.End();
		}
		if (string.IsNullOrEmpty(base.Request.QueryString["PHc"]))
		{
			base.Response.End();
		}
		int num = 0;
		if (!int.TryParse(base.Request.QueryString["PHc"], out num))
		{
			base.Response.End();
		}
		if (!string.IsNullOrEmpty(base.Request["pc"]))
		{
			this.prjID = base.Request["pc"].ToString();
		}
		this.PhotoDat = this.getMoveImg(num);
		this.PhotoCount = num;
	}
	public DataTable getMoveImg(int icount)
	{
		string text = "select top " + icount + "  ";
		string text2 = "";
		if (this.prjID != "")
		{
			text2 = text2 + "and  PT_PrjInfo.PrjGuid='" + this.prjID + "'";
		}
		string a;
		if ((a = base.Request["Ptype"]) != null)
		{
			if (!(a == "PrjInfo"))
			{
				if (!(a == "CheckIn"))
				{
					if (a == "GraphicProgress")
					{
						text += " c.prjguid id2,c.prjname ,c.prjcode pc, c.prjguid id1,a.Remark title,(b.ThumbnaImgPath+b.ThumbnaName)tpath\r\nfrom XPM_Basic_AnnexList as a \r\ninner join XPM_Basic_Thumbnai as b on b.ThumbnaiCode=a.annexCode  \r\ninner join  PT_PrjInfo c on c.typecode=RecordCode\r\nwhere  a.ModuleID = '1810' and a.state<>-1 order by prjname";
					}
				}
				else
				{
					text += " im.prjguid id2,pj.prjname,ii.intendanceguid as id1,im.QuestionTitle title,(ip.Photopath+ip.PhotoName) as tpath from OPM_EPCM_IntendancePhotoList ip,OPM_EPCM_IntendanceInfo ii,OPM_EPCM_IntendanceMaster im,PT_PrjInfo pj where ii.noteid=ip.infoguid and ii.intendanceguid=im.intendanceguid and im.Prjguid=pj.prjguid and pj.IsValid='1' ";
					text += "order by prjname,ip.PhotoName desc ";
				}
			}
			else
			{
				text += " PT_PrjInfo.PrjName,PT_PrjInfo.PrjGuid as id1,PT_PrjInfo.PrjGuid as id2,PT_PrjInfo_PhotoList.Remark as title,(XPM_Basic_Thumbnai.ThumbnaImgPath+XPM_Basic_Thumbnai.ThumbnaName) AS tpath ";
				text += " FROM XPM_Basic_Thumbnai,PT_PrjInfo_PhotoList,PT_PrjInfo ";
				text += " WHERE ";
				text += " XPM_Basic_Thumbnai.ThumbnaiCode=PT_PrjInfo_PhotoList.UID ";
				text = text + " AND PT_PrjInfo_PhotoList.PrjGuid in (select [PrjGuid] FROM [PT_PrjInfo] where  [Podepom] like '%" + base.UserCode + "%' and IsValid='1' ) ";
				text += " and PT_PrjInfo_PhotoList.IsValid='1' ";
				text = text + " and PT_PrjInfo.PrjGuid=PT_PrjInfo_PhotoList.PrjGuid " + text2;
				text += " order by prjname,addtime desc, id1 ";
			}
		}
		return publicDbOpClass.DataTableQuary(text);
	}
	public string getOnclickstr(int index)
	{
		string a;
		if ((a = base.Request["Ptype"]) != null)
		{
			if (a == "PrjInfo")
			{
				return "imgclick('PrjInfo','" + this.PhotoDat.Rows[index]["id2"].ToString() + "'); ";
			}
			if (a == "CheckIn")
			{
				return string.Concat(new string[]
				{
					"imgclick('CheckIn','",
					this.PhotoDat.Rows[index]["id2"].ToString(),
					"','",
					base.Server.UrlEncode(this.PhotoDat.Rows[index]["prjname"].ToString()),
					"','",
					this.PhotoDat.Rows[index]["id1"].ToString(),
					"');"
				});
			}
			if (a == "GraphicProgress")
			{
				return string.Concat(new string[]
				{
					"imgclick('GraphicProgress','",
					this.PhotoDat.Rows[index]["id2"].ToString(),
					"','",
					base.Server.UrlEncode(this.PhotoDat.Rows[index]["prjname"].ToString()),
					"','",
					this.PhotoDat.Rows[index]["id1"].ToString(),
					"','",
					this.PhotoDat.Rows[index]["pc"].ToString(),
					"');"
				});
			}
		}
		return string.Empty;
	}
}


