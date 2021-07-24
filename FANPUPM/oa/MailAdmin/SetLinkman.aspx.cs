using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_MailAdmin_SetLinkman : BasePage, IRequiresSessionState
{
	private MailManage mail = new MailManage();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.btnAdd.Attributes["onclick"] = "javascript:showConsignee();";
			this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确认要删除选定联系人么？')){return false;}";
			this.ddlGroupBind();
		}
	}
	public void ddlGroupBind()
	{
		this.hidGroup.Value = "";
		string sqlString = "select groupID,groupName from EMS_MaileGroup where userID='" + this.Session["yhdm"].ToString() + "'";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		this.ddlGroup.DataSource = dataTable;
		this.ddlGroup.DataTextField = "groupName";
		this.ddlGroup.DataValueField = "groupID";
		this.ddlGroup.DataBind();
		int arg_7E_0 = dataTable.Rows.Count;
		ListItem listItem = new ListItem();
		listItem.Text = "添加组…";
		listItem.Value = "-1";
		this.ddlGroup.Items.Add(listItem);
		if (dataTable.Rows.Count > 0)
		{
			this.hidGroup.Value = this.ddlGroup.SelectedValue;
			return;
		}
		string str = "mailGroup.aspx";
		base.Response.Write("<script>window.open('" + str + "','newwindow', 'height=450, width=800,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,status=no,left=250px,top=50px');window.opener=null;</script>");
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				this.GridView1.DataKeys[e.Row.RowIndex]["UserCode"].ToString(),
				"','",
				this.GridView1.DataKeys[e.Row.RowIndex]["v_yhdm"].ToString(),
				"');"
			});
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		this.mail.DelLinkman(this.Session["yhdm"].ToString(), this.hdnLinkMan.Value);
		this.GridView1.DataBind();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		Guid groudId = default(Guid);
		if (this.hidGroup.Value != "")
		{
			if (this.hdnSelLinkMan.Value.ToString().Trim() != "")
			{
				groudId = new Guid(this.hidGroup.Value);
				this.mail.AddLinkman(this.Session["yhdm"].ToString(), this.hdnSelLinkMan.Value, groudId);
				this.GridView1.DataBind();
				return;
			}
		}
		else
		{
			this.Page.RegisterStartupScript("", "<script>alert('请选择用户要添加的组'), window .close (),window.opener.location = window.opener.location</script>");
		}
	}
	protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (this.ddlGroup.SelectedValue == "-1")
		{
			string str = "mailGroup.aspx";
			base.Response.Write("<script>window.open('" + str + "','newwindow', 'height=180, width=400,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,status=no,left=250px,top=150px');window.opener=null;</script>");
			return;
		}
		this.hidGroup.Value = this.ddlGroup.SelectedValue;
	}
}


