using ASP;
using cn.justwin.BLL;
using cn.justwin.opm.action;
using cn.justwin.opm.Business_Data;
using cn.justwin.opm.Fire;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OPM_Business_Data_Business_Data_ItemEdit : NBasePage, IRequiresSessionState
{
	private Business_DataAction action = new Business_DataAction();
	private FireAction fa = new FireAction();
	private Business_DataItemAction act = new Business_DataItemAction();
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
	public string PUID
	{
		get
		{
			object obj = base.Request["puid"];
			if (obj != null)
			{
				return (string)obj;
			}
			return System.Guid.NewGuid().ToString();
		}
		set
		{
			this.PUID = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["uid"] != null)
			{
				string uid = base.Request["uid"].ToString();
				using (DataTable model = this.action.GetModel(uid))
				{
					if (model.Rows.Count > 0)
					{
						this.txtBCode.Text = model.Rows[0]["BCode"].ToString();
						this.txtBName.Text = model.Rows[0]["BName"].ToString();
						string userCodes = model.Rows[0]["Designer"].ToString();
						this.txtDesigner.Text = WebUtil.GetUserNames(userCodes);
						this.txtEndDate.Text = ((model.Rows[0]["EndDate"] == System.DBNull.Value) ? "" : System.Convert.ToDateTime(model.Rows[0]["EndDate"]).ToShortDateString());
						this.txtAddUser.Text = PageHelper.QueryUser(this, model.Rows[0]["AddUser"].ToString());
						this.txtRemark.Value = model.Rows[0]["Remark"].ToString();
					}
				}
			}
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
			this.Name1();
			this.hdnUID.Value = this.PUID;
			this.FileLink1.MID = 2929;
			this.FileLink1.FID = this.hdnUID.Value;
			this.FileLink1.Type = 1;
		}
	}
	public void Name1()
	{
		DataTable byPrjID = this.fa.getByPrjID(new System.Guid(base.Request["PrjID"]));
		this.txtPrjName.Text = byPrjID.Rows[0][0].ToString();
	}
	private void InitAdd()
	{
		this.txtAddUser.Text = PageHelper.QueryUser(this, base.UserCode);
		this.txtAddTime.Text = System.DateTime.Now.ToShortDateString();
	}
	private void InitUpdate()
	{
		if (base.Request["puid"] != null)
		{
			string uid = base.Request["puid"].ToString();
			using (DataTable model = this.act.GetModel(uid))
			{
				if (model.Rows.Count > 0)
				{
					this.txtPName.Text = model.Rows[0]["PName"].ToString();
					this.txtPCode.Text = model.Rows[0]["PCode"].ToString();
					this.txtDesignDate.Text = System.Convert.ToDateTime(model.Rows[0]["DesignDate"]).ToShortDateString();
					this.txtNode.Value = model.Rows[0]["Remark"].ToString();
					this.txtAddTime.Text = System.Convert.ToDateTime(model.Rows[0]["AddTime"]).ToShortDateString();
					this.txtAddUser.Text = PageHelper.QueryUser(this, model.Rows[0]["AddUser"].ToString());
				}
			}
		}
	}
	public Business_DataItemModel GetModel()
	{
		Business_DataItemModel business_DataItemModel = new Business_DataItemModel();
		if (this.hdnUID.Value != "")
		{
			business_DataItemModel.UID = this.hdnUID.Value;
		}
		else
		{
			business_DataItemModel.UID = System.Guid.NewGuid().ToString();
		}
		business_DataItemModel.CodeId = base.Request["uid"].ToString();
		business_DataItemModel.PCode = this.txtPCode.Text.Trim();
		business_DataItemModel.PName = this.txtPName.Text.Trim();
		business_DataItemModel.DesignDate = System.Convert.ToDateTime(this.txtDesignDate.Text);
		business_DataItemModel.Remark = this.txtNode.Value;
		business_DataItemModel.AddUser = base.UserCode;
		business_DataItemModel.AddTime = System.Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
		business_DataItemModel.IsValid = "1";
		business_DataItemModel.FlowState = -1;
		return business_DataItemModel;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		Business_DataItemModel model = new Business_DataItemModel();
		model = this.GetModel();
		if (base.Request["type"] != null)
		{
			if (base.Request["type"].ToString() == "add")
			{
				if (this.act.Add(model))
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_Business_Data_Examine' })");
					return;
				}
			}
			else
			{
				if (this.act.Update(model))
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_Business_Data_Examine' })");
				}
			}
		}
	}
}


