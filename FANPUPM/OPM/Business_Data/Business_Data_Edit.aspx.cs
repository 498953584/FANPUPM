using ASP;
using cn.justwin.BLL;
using cn.justwin.opm.action;
using cn.justwin.opm.model;
using cn.justwin.opm.Public;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OPM_Business_Data_Business_Data_Edit : NBasePage, IRequiresSessionState
{
	private Business_DataAction action = new Business_DataAction();
	public string PrjID
	{
		get
		{
			return base.Request["PrjGuid"].ToString();
		}
		set
		{
			this.PrjID = value;
		}
	}
	public string PrjCode
	{
		get
		{
			return base.Request["PrjCode"].ToString();
		}
		set
		{
			this.PrjCode = value;
		}
	}
	public string CodeID
	{
		get
		{
			object obj = this.ViewState["codeid"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["codeid"] = value;
		}
	}
	public string UID
	{
		get
		{
			object obj = base.Request["uid"];
			if (obj != null)
			{
				return (string)obj;
			}
			return System.Guid.NewGuid().ToString();
		}
		set
		{
			this.UID = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["type"] != null)
			{
				if (base.Request["type"].ToString() == "add")
				{
					this.InitAdd();
				}
				else
				{
					this.InitUpdate();
				}
			}
			this.CodeID = base.Request["codeid"].ToString();
			this.hdnUID.Value = this.UID;
			this.FileLink1.MID = 2917;
			this.FileLink1.FID = this.hdnUID.Value;
			this.FileLink1.Type = 1;
		}
	}
	private void InitAdd()
	{
		this.txtAddUser.Text = PageHelper.QueryUser(this, base.UserCode);
		this.txtAddTime.Text = System.DateTime.Now.ToShortDateString();
		this.txtBeginDate.Text = System.DateTime.Now.ToShortDateString();
		string prjCode = string.Empty;
		string prjID = string.Empty;
		if (!string.IsNullOrEmpty(base.Request["PrjCode"]))
		{
			prjCode = base.Request["PrjCode"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["pc"]))
		{
			prjID = base.Request["pc"].ToString();
		}
		this.txtBCode.Text = new NewCodeAction().GetBusinessCode("OPM_Business_Data", "BCode", prjCode, "TZ", prjID);
	}
	private void InitUpdate()
	{
		if (base.Request["uid"] != null)
		{
			string uid = base.Request["uid"].ToString();
			using (DataTable model = this.action.GetModel(uid))
			{
				if (model.Rows.Count > 0)
				{
					this.txtBName.Text = model.Rows[0]["BName"].ToString();
					this.txtBCode.Text = model.Rows[0]["BCode"].ToString();
					this.txtDutyUser.Text = model.Rows[0]["DutyUser"].ToString();
					string text = model.Rows[0]["Designer"].ToString();
					if (WebUtil.GetUserNames(text) != "")
					{
						this.txtDesigner.Value = WebUtil.GetUserNames(text);
					}
					else
					{
						this.txtDesigner.Value = text;
					}
					this.txtBeginDate.Text = System.Convert.ToDateTime(model.Rows[0]["BeginDate"]).ToShortDateString();
					this.txtEndDate.Text = ((model.Rows[0]["EndDate"] == System.DBNull.Value) ? "" : System.Convert.ToDateTime(model.Rows[0]["EndDate"]).ToShortDateString());
					this.txtCause.Value = model.Rows[0]["Cause"].ToString();
					this.txtRemark.Value = model.Rows[0]["Remark"].ToString();
					this.txtAddTime.Text = System.Convert.ToDateTime(model.Rows[0]["AddTime"]).ToShortDateString();
					this.txtAddUser.Text = PageHelper.QueryUser(this, model.Rows[0]["AddUser"].ToString());
				}
			}
		}
	}
	public Business_DataModel GetModel()
	{
		Business_DataModel business_DataModel = new Business_DataModel();
		if (this.hdnUID.Value != "")
		{
			business_DataModel.UID = this.hdnUID.Value;
		}
		else
		{
			business_DataModel.UID = System.Guid.NewGuid().ToString();
		}
		business_DataModel.BType = base.Request["codetype"].ToString();
		business_DataModel.PrjId = base.Request["pc"].ToString();
		business_DataModel.BCode = this.txtBCode.Text.Trim().Replace("'", "''");
		business_DataModel.BName = this.txtBName.Text.Trim().Replace("'", "''");
		business_DataModel.CodeId = this.CodeID;
		business_DataModel.DutyUser = this.txtDutyUser.Text.Trim().Replace("'", "''");
		if (this.hdfusercode.Value != "")
		{
			business_DataModel.Designer = this.hdfusercode.Value;
		}
		else
		{
			business_DataModel.Designer = this.txtDesigner.Value.Trim().Replace("'", "''");
		}
		business_DataModel.BeginDate = System.Convert.ToDateTime(this.txtBeginDate.Text);
		if (this.txtEndDate.Text != "")
		{
			business_DataModel.EndDate = this.txtEndDate.Text;
		}
		else
		{
			business_DataModel.EndDate = null;
		}
		business_DataModel.Cause = this.txtCause.Value;
		business_DataModel.Remark = this.txtRemark.Value.Replace("'", "''");
		business_DataModel.AddUser = base.UserCode;
		business_DataModel.AddTime = System.Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
		business_DataModel.IsValid = "1";
		business_DataModel.FlowState = -1;
		return business_DataModel;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		Business_DataModel model = new Business_DataModel();
		model = this.GetModel();
		if (base.Request["type"] != null)
		{
			if (base.Request["type"].ToString() == "add")
			{
				if (this.action.Add(model))
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_Business_Data_Manage' });");
					return;
				}
			}
			else
			{
				if (this.action.Update(model))
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_Business_Data_Manage' });");
				}
			}
		}
	}
}


