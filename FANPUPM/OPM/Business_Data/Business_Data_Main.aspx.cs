using ASP;
using cn.justwin.BLL;
using cn.justwin.opm.action;
using cn.justwin.opm.Fire;
using cn.justwin.opm.OPS;
using cn.justwin.Web;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OPM_Business_Data_Business_Data_Main : NBasePage, IRequiresSessionState
{
	private Business_DataAction action = new Business_DataAction();
	private FireAction fa = new FireAction();
	private MaintainDataAction mda = new MaintainDataAction();
	private string uid;
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
			if (base.Request["opType"] != null && base.Request["opType"].ToString() == "schedule")
			{
				this.query.Style.Add("display", "none");
				this.Name1();
				this.Name2();
				this.trName.Style.Add("display", "");
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
		text += " and FlowState=1";
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
		if (listByBusType.Rows.Count > 0)
		{
			this.hfldUID.Value = listByBusType.Rows[0]["UID"].ToString();
		}
		else
		{
			this.hfldUID.Value = "00000000-0000-0000-0000-000000000000";
		}
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
			e.Row.Attributes["id"] = this.gvBusiness_Data.DataKeys[e.Row.RowIndex]["uid"].ToString();
			e.Row.Attributes["guid"] = this.gvBusiness_Data.DataKeys[e.Row.RowIndex]["uid"].ToString();
			e.Row.Attributes["onclick"] = "ClickRow('" + this.gvBusiness_Data.DataKeys[e.Row.RowIndex]["uid"].ToString() + "')";
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected string getSrc()
	{
		if (string.IsNullOrEmpty(base.Request["isShow"]))
		{
			return string.Concat(new string[]
			{
				"/OPM/Business_Data/Business_Data_Examine.aspx?uid=",
				this.hfldUID.Value,
				"&codeid=",
				this.hdnCodeId.Value,
				"&PrjCode=",
				this.hdnPrjCode.Value,
				"&PrjID=",
				base.Request["pc"].ToString(),
				"&codetype=",
				base.Request["codetype"].ToString(),
				"&isshowall=",
				base.Request["isshowall"].ToString(),
				"&opType=",
				base.Request["opType"].ToString()
			});
		}
		string text = base.Request["isShow"].ToString();
		return string.Concat(new string[]
		{
			"/OPM/Business_Data/Business_Data_Examine.aspx?isShow=",
			text,
			"&uid=",
			this.hfldUID.Value,
			"&codeid=",
			this.hdnCodeId.Value,
			"&PrjCode=",
			this.hdnPrjCode.Value,
			"&PrjID=",
			base.Request["pc"].ToString(),
			"&codetype=",
			base.Request["codetype"].ToString(),
			"&isshowall=",
			base.Request["isshowall"].ToString(),
			"&opType=",
			base.Request["opType"].ToString()
		});
	}
	protected void gvBusiness_Data_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvBusiness_Data.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
}


