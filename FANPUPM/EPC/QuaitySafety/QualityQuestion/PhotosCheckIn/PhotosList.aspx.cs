using ASP;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_ProjectCost_PhotosCheckIn_PhotosList : Page, IRequiresSessionState
{

	public Guid IntendanceGuid
	{
		get
		{
			return (Guid)this.ViewState["IntendanceGuid"];
		}
		set
		{
			this.ViewState["IntendanceGuid"] = value;
		}
	}
	public int index
	{
		get
		{
			return (int)this.ViewState["index"];
		}
		set
		{
			this.ViewState["index"] = value;
		}
	}
	public int photoType
	{
		get
		{
			return (int)this.ViewState["photoType"];
		}
		set
		{
			this.ViewState["photoType"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["IntendanceGuid"] == null || base.Request["type"] == null)
		{
			this.js.Text = "alert('参数有误！');";
			return;
		}
		this.IntendanceGuid = new Guid(base.Request.QueryString["IntendanceGuid"]);
		this.photoType = Convert.ToInt32(base.Request.QueryString["type"]);
		if (base.Request["index"] != null)
		{
			this.index = Convert.ToInt32(base.Request.QueryString["index"]);
			return;
		}
		this.index = 0;
	}
	[WebMethod]
	public static string GetImages(Guid infoGuid, int type)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string sqlString = string.Concat(new object[]
		{
			"select * from OPM_EPCM_IntendancePhotoList where infoGuid='",
			infoGuid,
			"' and photoType=",
			type
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		foreach (DataRow dataRow in dataTable.Rows)
		{
			stringBuilder.Append("'" + dataRow["NoteID"] + "',");
		}
		string text = stringBuilder.ToString().Trim(new char[]
		{
			','
		});
		if (text.Length == 0)
		{
			return string.Empty;
		}
		return EPC_ProjectCost_PhotosCheckIn_PhotosList.getJosn(text, type);
	}
	private static string getJosn(string annexCodes, int type)
	{
		List<IntendancePhotoList> list = new List<IntendancePhotoList>();
		string sqlString = string.Concat(new object[]
		{
			"SELECT a.*,b.* FROM OPM_EPCM_IntendanceInfo as a inner join OPM_EPCM_IntendancePhotoList as b on a.NoteID=b.InfoGuid where b.noteId in(",
			annexCodes,
			") and b.photoType=",
			type
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count <= 0)
		{
			string sqlString2 = string.Concat(new object[]
			{
				"select * from OPM_EPCM_IntendancePhotoList where noteId in(",
				annexCodes,
				") and photoType=",
				type
			});
			dataTable = publicDbOpClass.DataTableQuary(sqlString2);
			dataTable.Columns.Add("QuestionExplain");
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (DataRow dataRow in dataTable.Rows)
		{
			IntendancePhotoList intendancePhotoList = new IntendancePhotoList();
			intendancePhotoList.PhotoNumber = ((dataRow["PhotoNumber"] as string) ?? string.Empty);
			intendancePhotoList.PhotoPath = ((dataRow["PhotoPath"] as string) ?? string.Empty);
			intendancePhotoList.PhotoName = ((dataRow["PhotoName"] as string) ?? string.Empty);
			intendancePhotoList.PhotoExplain = ((dataRow["PhotoExplain"] as string) ?? string.Empty);
			if (dataRow["NoteId"] != DBNull.Value)
			{
				intendancePhotoList.NoteId = new Guid(dataRow["NoteId"].ToString());
			}
			intendancePhotoList.ThumbnaName = ((dataRow["QuestionExplain"] as string) ?? string.Empty);
			list.Add(intendancePhotoList);
			stringBuilder.Remove(0, stringBuilder.Length);
		}
		if (list.Count > 0)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Serialize(list);
		}
		return "";
	}
}


