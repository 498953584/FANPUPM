using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.TableTopBLL;
using cn.justwin.TableTopDAL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TableTop_ModelList : NBasePage, IRequiresSessionState
{
	private DesktopBLL deskTop = new DesktopBLL();
	public string dtrow;
	private static Regex RegWebAddr = new Regex("^http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?");

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdfModel.Value = "0";
			this.Bind();
			if (base.Request.QueryString["orderid"] != null)
			{
				int num = this.deskTop.UpdateWeblinkByLinkID(base.Request.QueryString["orderid"].ToString(), base.UserCode);
				HttpContext.Current.Response.ContentType = "text/plain";
				HttpContext.Current.Response.Write(num);
				HttpContext.Current.Response.End();
			}
		}
	}
	public void Bind()
	{
		DataTable webLinkList = this.deskTop.GetWebLinkList(base.UserCode);
		this.gvwWebLineList.DataSource = webLinkList;
		this.gvwWebLineList.DataBind();
	}
	protected void gvwModelList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			HtmlInputText htmlInputText = e.Row.FindControl("txtOrderID") as HtmlInputText;
			htmlInputText.Value = dataRowView["OrderID"].ToString();
			HtmlInputText htmlInputText2 = e.Row.FindControl("txtWebName") as HtmlInputText;
			htmlInputText2.Value = dataRowView["WebName"].ToString();
			HtmlInputText htmlInputText3 = e.Row.FindControl("txtWebAddr") as HtmlInputText;
			htmlInputText3.Value = dataRowView["WebAddr"].ToString();
			HtmlInputText htmlInputText4 = e.Row.FindControl("txtRemark") as HtmlInputText;
			htmlInputText4.Value = dataRowView["Remark"].ToString();
			e.Row.Attributes["id"] = this.gvwWebLineList.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwWebLineList.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + this.gvwWebLineList.DataKeys[e.Row.RowIndex].Value.ToString() + "');";
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		bool flag = true;
		for (int i = 0; i < this.gvwWebLineList.Rows.Count; i++)
		{
			this.gvwWebLineList.Rows[i].FindControl("chk");
			Desktop_Weblink desktop_Weblink = new Desktop_Weblink();
			ClientScriptManager arg_36_0 = this.Page.ClientScript;
			desktop_Weblink.userCode = base.UserCode;
			desktop_Weblink.LinkID = (int)this.gvwWebLineList.DataKeys[i].Value;
			this.gvwWebLineList.Rows[i].FindControl("txtOrderID");
			desktop_Weblink.orderId = i + 1;
			HtmlInputText htmlInputText = this.gvwWebLineList.Rows[i].FindControl("txtWebName") as HtmlInputText;
			if (this.ExistsWebName(htmlInputText.Value, desktop_Weblink.LinkID))
			{
				base.RegisterScript("top.ui.alert('网站名称已存在！'); \n");
				flag = false;
				break;
			}
			desktop_Weblink.WebName = htmlInputText.Value.ToString();
			HtmlInputText htmlInputText2 = this.gvwWebLineList.Rows[i].FindControl("txtWebAddr") as HtmlInputText;
			if (this.ExistsWebAddr(htmlInputText2.Value, desktop_Weblink.LinkID))
			{
				base.RegisterScript("top.ui.alert('网址已存在！'); \n");
				flag = false;
				break;
			}
			desktop_Weblink.WebAddr = htmlInputText2.Value.ToString();
			if (!this.IsWebAddr(htmlInputText2.Value.ToString()))
			{
				base.RegisterScript("top.ui.alert('网址格式不正确！'); \n");
				flag = false;
				break;
			}
			HtmlInputText htmlInputText3 = this.gvwWebLineList.Rows[i].FindControl("txtRemark") as HtmlInputText;
			desktop_Weblink.Remark = htmlInputText3.Value.ToString();
			this.deskTop.UpdateWeblink(desktop_Weblink);
		}
		if (flag)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("top.ui.show( '保存成功！'); \n");
			stringBuilder.Append("top.ui.closeTab(); \n");
			base.RegisterScript(stringBuilder.ToString());
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			for (int i = 0; i < this.gvwWebLineList.Rows.Count; i++)
			{
				CheckBox checkBox = (CheckBox)this.gvwWebLineList.Rows[i].FindControl("chk");
				if (checkBox.Checked)
				{
					this.deskTop.DeleteWebAddr(base.UserCode, this.gvwWebLineList.DataKeys[i].Value.ToString(), sqlTransaction);
				}
			}
			sqlTransaction.Commit();
		}
		this.Bind();
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	public bool ExistsWebName(string webName1, int LinkID)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("select * from frame_Desktop_Weblink");
		stringBuilder.Append(string.Concat(new object[]
		{
			" where WebName='",
			webName1,
			"' and  UserCode='",
			base.UserCode,
			"' and linkid!='",
			LinkID,
			"'"
		}));
		DataTable dataTable = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
		return dataTable.Rows.Count > 0;
	}
	public bool ExistsWebAddr(string webAddr1, int LinkID)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("select * from frame_Desktop_Weblink");
		stringBuilder.Append(string.Concat(new object[]
		{
			" where WebAddr='",
			webAddr1,
			"' and  UserCode='",
			base.UserCode,
			"' and linkid!='",
			LinkID,
			"'"
		}));
		DataTable dataTable = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
		return dataTable.Rows.Count > 0;
	}
	public bool IsWebAddr(string uri)
	{
		Match match = TableTop_ModelList.RegWebAddr.Match(uri);
		return match.Success;
	}
}


