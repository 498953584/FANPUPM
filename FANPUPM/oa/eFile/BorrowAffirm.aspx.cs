using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_eFile_BorrowAffirm : BasePage, IRequiresSessionState
{
	private eFileLendAction efla
	{
		get
		{
			return new eFileLendAction();
		}
	}
	private Guid rid
	{
		get
		{
			return (Guid)this.ViewState["RID"];
		}
		set
		{
			this.ViewState["RID"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			if (base.Request["rid"] != null)
			{
				this.rid = new Guid(base.Request["rid"]);
			}
			this.DBBorrowDate.Text = DateTime.Now.ToShortDateString();
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		eFileLend eFileLend = new eFileLend();
		eFileLend.ReturnDate = Convert.ToDateTime(this.DBBorrowDate.Text);
		eFileLend.LendState = "4";
		eFileLend.RecordID = this.rid;
		if (this.efla.BorrowUpdate(eFileLend) == 1)
		{
			this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
			return;
		}
		this.JS.Text = "alert('保存失败!');";
	}
}


