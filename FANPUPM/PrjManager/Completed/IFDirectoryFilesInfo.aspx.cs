using ASP;
using cn.justwin.BLL;
using cn.justwin.fileInfoBll;
using cn.justwin.fileInfoModel;
using cn.justwin.PrjManager;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_Completed_IFDirectoryFilesInfo : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	protected void BindGv()
	{
		string text = base.Request["id"];
		if (!string.IsNullOrEmpty(text))
		{
			System.Collections.Generic.List<string> directoryId;
			if (text.IndexOf(",") != -1)
			{
				directoryId = new System.Collections.Generic.List<string>(text.Split(new char[]
				{
					','
				}));
			}
			else
			{
				directoryId = new System.Collections.Generic.List<string>
				{
					text
				};
			}
			this.gvFilesInfo.DataSource = CompletedAnnex.GetFilesInfo(directoryId);
		}
		else
		{
			this.gvFilesInfo.DataSource = null;
		}
		this.gvFilesInfo.DataBind();
	}
	public string GetFileType(string FileName, string FileType)
	{
		FileTypeBll fileTypeBll = new FileTypeBll();
		string strWhere;
		if (FileType == "1" || FileType == "2")
		{
			strWhere = " where TypeSuffix='.folder'";
		}
		else
		{
			if (string.IsNullOrEmpty(FileName) || FileName.LastIndexOf(".") == -1)
			{
				strWhere = " where TypeSuffix='.other'";
			}
			else
			{
				FileName.LastIndexOf('.');
				string str = FileName.Substring(FileName.LastIndexOf('.'), FileName.Length - FileName.LastIndexOf('.'));
				strWhere = " where TypeSuffix='" + str + "'";
			}
		}
		System.Collections.Generic.List<FileTypeModel> listArray = fileTypeBll.GetListArray(strWhere);
		if (listArray.Count > 0)
		{
			return listArray[0].TypeName;
		}
		listArray = fileTypeBll.GetListArray(" where TypeSuffix='.other'");
		return listArray[0].TypeName;
	}
	protected void gvFilesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes.Add("id", e.Row.RowIndex.ToString());
		}
	}
}


