using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_17_Entpm_ScienceInnovate_ScienceInnovateEditor : NBasePage, System.Web.SessionState.IRequiresSessionState
{
	private string action = string.Empty;
	private string currentTcode = string.Empty;
	private ProjectFilesUpAction pfua = new ProjectFilesUpAction();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["Tcode"]))
		{
			this.currentTcode = base.Request["Tcode"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (this.action.Equals("Add"))
			{
				this.Init();
				return;
			}
			this.InitBind();
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (!(this.action == "Add"))
		{
			if (this.action == "Update")
			{
				if (string.IsNullOrEmpty(this.currentTcode))
				{
					base.RegisterScript("top.ui.show('添加失败');");
					return;
				}
				DataTable fileClassInformation = this.pfua.GetFileClassInformation(this.currentTcode);
				string value = fileClassInformation.Rows[0]["ParentClassID"].ToString();
				string className = this.txtName.Text.Trim();
				string remark = this.txtRemark.Text.Trim();
				string classOpters = base.UserCode.Trim();
				new StringBuilder();
				if (this.pfua.UpdateFilesClass(className, remark, classOpters, this.currentTcode))
				{
					if (string.IsNullOrEmpty(value))
					{
					}
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_ScienceInnovateList' });");
					this.btnSave.Enabled = false;
				}
			}
			return;
		}
		if (string.IsNullOrEmpty(this.currentTcode))
		{
			base.RegisterScript("top.ui.alert('对不起添加失败');");
			return;
		}
		string classID = Guid.NewGuid().ToString();
		string parentClassID = this.currentTcode;
		string className2 = this.txtName.Text.Trim();
		string remark2 = this.txtRemark.Text.Trim();
		int isValid = 1;
		int id_xh = Convert.ToInt32(this.txtCode.Text.Trim());
		string userCode = base.UserCode;
		new StringBuilder();
		if (this.pfua.InsertFilesClass(classID, parentClassID, className2, remark2, isValid, id_xh, userCode))
		{
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_ScienceInnovateList' });");
			this.btnSave.Enabled = false;
			return;
		}
		base.RegisterScript("top.ui.show('添加失败');");
	}
	private new void Init()
	{
		DataTable fileClassId_xh = this.pfua.GetFileClassId_xh();
		if (fileClassId_xh.Rows.Count > 0)
		{
			if (string.IsNullOrEmpty(fileClassId_xh.Rows[0][0].ToString()))
			{
				this.txtCode.Text = "0";
			}
			else
			{
				int num = Convert.ToInt32(fileClassId_xh.Rows[0][0].ToString()) + 1;
				this.txtCode.Text = num.ToString();
			}
		}
		else
		{
			this.txtCode.Text = "0";
		}
		this.txtName.Text = string.Empty;
		this.txtRemark.Text = string.Empty;
	}
	private void InitBind()
	{
		DataTable fileClassInformation = this.pfua.GetFileClassInformation(this.currentTcode);
		this.txtCode.Text = fileClassInformation.Rows[0]["Id_xh"].ToString();
		this.txtName.Text = fileClassInformation.Rows[0]["ClassName"].ToString();
		this.txtRemark.Text = fileClassInformation.Rows[0]["Remark"].ToString();
	}
}


