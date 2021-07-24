using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Organization_PostWeaveEdit : BasePage, IRequiresSessionState
{
	public PTDUTYAction hrAction
	{
		get
		{
			return new PTDUTYAction();
		}
	}
	public int RecordID
	{
		get
		{
			object obj = this.ViewState["RECORDID"];
			if (obj != null)
			{
				return (int)this.ViewState["RECORDID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	public int BMDM
	{
		get
		{
			object obj = this.ViewState["BMDM"];
			if (obj != null)
			{
				return (int)this.ViewState["BMDM"];
			}
			return -1;
		}
		set
		{
			this.ViewState["BMDM"] = value;
		}
	}
	public string DepartmentName
	{
		get
		{
			object obj = this.ViewState["DepartmentName"];
			if (obj != null)
			{
				return (string)this.ViewState["DepartmentName"];
			}
			return "";
		}
		set
		{
			this.ViewState["DepartmentName"] = value;
		}
	}
	public string OperateType
	{
		get
		{
			object obj = this.ViewState["OPERATETYPE"];
			if (obj != null)
			{
				return (string)this.ViewState["OPERATETYPE"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["OPERATETYPE"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["rid"] == null || base.Request["t"] == null || base.Request["dc"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;window.close();</script>");
			return;
		}
		if (!this.Page.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			this.OperateType = base.Request["t"].ToString().Trim();
			if (base.Request["rid"].ToString() != "")
			{
				this.RecordID = int.Parse(base.Request["rid"].ToString());
			}
			if (base.Request["dc"].ToString() != "")
			{
				this.BMDM = int.Parse(base.Request["dc"].ToString());
			}
			if (base.Request["dn"].ToString() != null)
			{
				this.DepartmentName = base.Request["dn"].ToString();
			}
			if (this.OperateType == "add")
			{
				this.AddDisplay();
			}
			if (this.OperateType == "upd")
			{
				this.EditDisplay();
			}
		}
	}
	private void AddDisplay()
	{
		this.txtDepartment.Text = this.DepartmentName;
	}
	private void EditDisplay()
	{
		DataTable list = this.hrAction.GetList(this.RecordID.ToString());
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.txtDepartment.Text = dataRow["V_BMMC"].ToString();
			this.txtDutyNumber.Text = dataRow["DutyNumber"].ToString();
			this.txtDutyName.Text = dataRow["DutyName"].ToString();
			this.txtRemark.Text = dataRow["Remark"].ToString();
		}
	}
	private PTDUTY GetData()
	{
		return new PTDUTY
		{
			c_sfyx = "1",
			i_bmdm = this.BMDM,
			I_DUTYID = this.RecordID,
			DutyNumber = (this.txtDutyNumber.Text.Trim() == "") ? 0 : int.Parse(this.txtDutyNumber.Text.Trim()),
			DutyName = this.txtDutyName.Text.Trim(),
			Remark = this.txtRemark.Text.Trim()
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(this.txtDutyName.Text.Trim()))
		{
			base.RegisterScript("alert('岗位名称不能为空!');");
			return;
		}
		if (string.IsNullOrEmpty(this.txtDutyNumber.Text.Trim()))
		{
			base.RegisterScript("alert('岗位编制不能为空!');");
			this.txtDutyNumber.Focus();
			return;
		}
		if (!Information.IsNumeric(this.txtDutyNumber.Text.ToString().Trim()) || this.txtDutyNumber.Text.ToString().Trim().Contains("."))
		{
			base.RegisterScript("alert('岗位编制必须为自然数!');");
			this.txtDutyNumber.Focus();
			return;
		}
		if (Convert.ToInt32(this.txtDutyNumber.Text.ToString().Trim()) < 0)
		{
			base.RegisterScript("alert('岗位编制必须为自然数!');");
			this.txtDutyNumber.Focus();
			return;
		}
		PTDUTY data = this.GetData();
		if (this.OperateType == "add")
		{
			DataTable pTDUTYDifferent = this.hrAction.GetPTDUTYDifferent(this.txtDutyName.Text.Trim(), this.BMDM.ToString());
			if (pTDUTYDifferent.Rows.Count > 0)
			{
				base.RegisterScript("alert('岗位名称已存在，请重新输入!');");
				return;
			}
			int num = this.hrAction.Add(data);
			if (num > 0)
			{
				base.RegisterScript("successed('添加');");
			}
			else
			{
				base.RegisterScript("alert('没有相关数据可添加!');");
			}
		}
		if (this.OperateType == "upd")
		{
			DataTable list = this.hrAction.GetList(this.RecordID.ToString());
			if (list.Rows.Count > 0 && this.txtDutyName.Text.Trim() != list.Rows[0]["DutyName"].ToString())
			{
				DataTable pTDUTYDifferent2 = this.hrAction.GetPTDUTYDifferent(this.txtDutyName.Text.Trim(), this.BMDM.ToString());
				if (pTDUTYDifferent2.Rows.Count > 0)
				{
					base.RegisterScript("alert('岗位名称已存在，请重新输入!');");
					return;
				}
			}
			int num = this.hrAction.Update(data);
			if (num > 0)
			{
				base.RegisterScript("successed('修改');");
				return;
			}
			base.RegisterScript("alert('没有相关数据可更新!');");
		}
	}
}


