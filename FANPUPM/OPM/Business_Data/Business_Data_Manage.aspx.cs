using ASP;
using cn.justwin.BLL;
using cn.justwin.opm.action;
using cn.justwin.opm.Fire;
using cn.justwin.opm.OPS;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Business_Data_Manage : NBasePage, IRequiresSessionState
{
	private Business_DataAction action = new Business_DataAction();
	private FireAction fa = new FireAction();
	private MaintainDataAction mda = new MaintainDataAction();
	public string CodeType
	{
		get
		{
			object obj = this.ViewState["codeType"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["codeType"] = value;
		}
	}
	public string PC
	{
		get
		{
			object obj = this.ViewState["pc"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["pc"] = value;
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

	protected override void OnInit(System.EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldAdjunctPath.Value = ConfigHelper.Get("ImgBusiness");
			this.hdnPrjCode.Value = base.Request["PrjCode"].ToString();
			if (base.Request["opType"] != null)
			{
				if (base.Request["opType"].ToString() == "view")
				{
					this.btnAdd.Style.Add("display", "none");
					this.btnUpdate.Style.Add("display", "none");
					this.btnDelete.Style.Add("display", "none");
					this.WF1.Visible = false;
				}
				if (base.Request["opType"].ToString() == "schedule")
				{
					this.btn.Style.Add("display", "none");
					this.query.Style.Add("display", "none");
					this.Name1();
					this.Name2();
					this.trName.Style.Add("display", "");
				}
			}
			if (base.Request["codetype"] != null && base.Request["pc"] != null && base.Request["codeid"] != null)
			{
				this.CodeType = base.Request["codetype"].ToString();
				this.PC = base.Request["pc"].ToString();
				this.CodeID = base.Request["codeid"].ToString();
				this.hdnCodeId.Value = base.Request["codeid"].ToString();
			}
			else
			{
				base.RegisterScript("alert('系统提示：\\n\\n参数有误！')");
			}
			this.BindGv();
		}
		this.hdnChk.Value = this.action.ChkFlowState(this.hdnCodeId.Value, this.PC);
	}
	public void BindGv()
	{
		string text = "";
		if (this.txtDutyUser.Text.Trim() != "")
		{
			text = text + " and dutyuser like '%" + this.txtDutyUser.Text.Trim() + "%' ";
		}
		if (this.txtBCode.Text.Trim() != "")
		{
			text = text + " and bcode like '%" + this.txtBCode.Text.Trim() + "%'   ";
		}
		if (this.txtEndDate.Text.Trim() != "")
		{
			text = text + "and datediff(dd,enddate,'" + this.txtEndDate.Text.Trim() + "')>= 0";
		}
		DataTable listByBusType = this.action.GetListByBusType(this.CodeType, this.PC, this.CodeID, text, System.Convert.ToBoolean(base.Request["isshowall"]));
		this.gvBusiness_Data.DataSource = listByBusType;
		this.gvBusiness_Data.DataBind();
	}
	public void Name1()
	{
		DataTable byPrjID = this.fa.getByPrjID(new System.Guid(base.Request["pc"]));
		this.lblName1.Text = byPrjID.Rows[0][0].ToString();
	}
	public void Name2()
	{
		this.lblName2.Text = this.mda.GetDocTypeByID(212, System.Convert.ToInt32(base.Request["codeid"]));
	}
	protected void gvBusiness_Data_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBusiness_Data.DataKeys[e.Row.RowIndex]["UID"].ToString();
			e.Row.Attributes["guid"] = this.gvBusiness_Data.DataKeys[e.Row.RowIndex]["UID"].ToString();
			e.Row.Attributes["onclick"] = "clickRows('" + this.gvBusiness_Data.DataKeys[e.Row.RowIndex]["UID"].ToString() + "')";
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldIDArr.Value))
		{
			string uid = string.Empty;
			if (this.hfldIDArr.Value.Contains("["))
			{
				uid = this.hfldIDArr.Value.Substring(1, this.hfldIDArr.Value.Length - 2).Replace("\"", "'");
			}
			else
			{
				uid = "'" + this.hfldIDArr.Value + "'";
			}
			if (this.action.Delete(uid))
			{
				this.BindGv();
				base.RegisterScript("alert('系统提示：\\n\\n删除成功！')");
			}
		}
	}
	protected void btnUpLoad_Click(object sender, System.EventArgs e)
	{
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


