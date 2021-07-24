using ASP;
using cn.justwin.BLL;
using cn.justwin.opm.Business_Data;
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
public partial class OPM_Business_Data_Business_Data_Examine : NBasePage, IRequiresSessionState
{
	private Business_DataItemAction action = new Business_DataItemAction();
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
			object obj = this.ViewState["PrjID"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["PrjID"] = value;
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
			if (base.Request["opType"] != null && base.Request["opType"].ToString() == "view")
			{
				this.btnAdd.Attributes["style"] = "display:none;";
				this.btnUpdate.Attributes["style"] = "display:none;";
				this.btnDelete.Attributes["style"] = "display:none;";
				this.WF1.Visible = false;
			}
			this.hfldAdjunctPath.Value = ConfigHelper.Get("ImgBusiness");
			this.BindGv();
			if (base.Request["uid"].ToString() == "00000000-0000-0000-0000-000000000000")
			{
				this.btnAdd.Visible = false;
				return;
			}
			this.btnAdd.Visible = true;
		}
	}
	public void BindGv()
	{
		string text = "";
		text = text + " where CodeID='" + base.Request["uid"] + "' ";
		DataTable listByBusType = this.action.GetListByBusType(text);
		this.gvBusiness_Data.DataSource = listByBusType;
		this.gvBusiness_Data.DataBind();
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


