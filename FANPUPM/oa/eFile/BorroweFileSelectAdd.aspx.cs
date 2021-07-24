using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_eFile_BorroweFileSelectAdd : BasePage, IRequiresSessionState
{
	private DataTable eFileInfoDT;
	public string ClassTypeCode
	{
		get
		{
			return this.ViewState["CLASSTYPECODE"].ToString();
		}
		set
		{
			this.ViewState["CLASSTYPECODE"] = value;
		}
	}
	private PTMultiClassesAction pca
	{
		get
		{
			return new PTMultiClassesAction();
		}
	}
	private eFileInfoAction efia
	{
		get
		{
			return new eFileInfoAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
	}
	protected void BtnSelAdd_Click(object sender, EventArgs e)
	{
		string text = "";
		for (int i = 0; i < this.GVeFileInfo.Rows.Count; i++)
		{
			CheckBox checkBox = (CheckBox)this.GVeFileInfo.Rows[i].FindControl("CBeFileRecordID");
			if (checkBox.Checked)
			{
				text = text + this.GVeFileInfo.DataKeys[i]["RecordID"].ToString() + ",";
			}
		}
		if (text == "")
		{
			return;
		}
		text = text.Substring(0, text.Length - 1);
		DataTable list = this.efia.GetList(" RecordID in (" + text + ")");
		for (int j = 0; j < list.Rows.Count; j++)
		{
			DataRow arg_C2_0 = list.Rows[j];
		}
		this.GVSelecteFile.DataSource = list;
		this.GVSelecteFile.DataBind();
	}
}


