using ASP;
using cn.justwin.BLL;
using cn.justwin.TableTopDAL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class UserSet : NBasePage, IRequiresSessionState
{
	private string usercode = string.Empty;
	private static Regex RegEmail = new Regex("^http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?$");

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.txtWebName.ToolTip = "网站名称不能为空";
			this.txtWebAddr.ToolTip = "网址不能为空,格式如:http://www.justwin.cn";
		}
	}
	public static bool IsEmail(string inputData)
	{
		Match match = UserSet.RegEmail.Match(inputData);
		return match.Success;
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		Desktop_Weblink desktop_Weblink = new Desktop_Weblink();
		ClientScriptManager arg_11_0 = this.Page.ClientScript;
		desktop_Weblink.userCode = base.UserCode;
		string webName = this.txtWebName.Text.ToString();
		string text = this.txtWebAddr.Text.ToString();
		desktop_Weblink.orderId = 1;
		if (this.GirdColNum.SelectedItem.Text.ToString() == null)
		{
			return;
		}
		desktop_Weblink.orderId = Convert.ToInt32(this.GirdColNum.SelectedItem.Text.ToString());
		if (!(this.txtWebName.Text.ToString() != ""))
		{
			return;
		}
		desktop_Weblink.WebName = this.txtWebName.Text.ToString();
		if (!(this.txtWebAddr.Text.ToString() != ""))
		{
			return;
		}
		desktop_Weblink.WebAddr = this.txtWebAddr.Text.ToString();
		if (this.txtRemark.Text.ToString() != "")
		{
			desktop_Weblink.Remark = this.txtRemark.Text.ToString();
		}
		else
		{
			desktop_Weblink.Remark = "";
		}
		if (!UserSet.IsEmail(text))
		{
			base.RegisterScript("top.ui.alert('网址格式不正确！');");
			return;
		}
		if (this.ExistsWebName(webName))
		{
			base.RegisterScript("top.ui.alert('网站名称已存在！');");
			return;
		}
		if (this.ExistsWebAddr(text))
		{
			base.RegisterScript("top.ui.alert('网址已存在！');");
			return;
		}
		if (!this.Add(desktop_Weblink))
		{
			base.RegisterScript("top.ui.alert('操作失败！');");
		}
		base.RegisterScript("top.ui.winSuccess();top.ui.reloadTab();");
	}
	public bool Add(Desktop_Weblink model)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("insert into frame_Desktop_Weblink(");
		stringBuilder.Append("userCode,orderId,WebName,WebAddr,Remark)");
		stringBuilder.Append(" values (");
		stringBuilder.Append("@userCode,@orderId,@WebName,@WebAddr,@Remark)");
		stringBuilder.Append(";select @@IDENTITY");
		SqlParameter[] array = new SqlParameter[]
		{
			new SqlParameter("@userCode", SqlDbType.VarChar, 50),
			new SqlParameter("@orderId", SqlDbType.Int, 4),
			new SqlParameter("@WebName", SqlDbType.NVarChar, 50),
			new SqlParameter("@WebAddr", SqlDbType.NVarChar, 100),
			new SqlParameter("@Remark", SqlDbType.NVarChar, 50)
		};
		array[0].Value = model.userCode;
		array[1].Value = model.orderId;
		array[2].Value = model.WebName;
		array[3].Value = model.WebAddr;
		array[4].Value = model.Remark;
		int num = publicDbOpClass.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), array);
		return num > 0;
	}
	public bool ExistsWebName(string webName1)
	{
		new Desktop_Weblink();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("select * from frame_Desktop_Weblink");
		stringBuilder.Append(string.Concat(new string[]
		{
			" where WebName='",
			webName1,
			"'and  UserCode='",
			base.UserCode,
			"'"
		}));
		DataTable dataTable = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
		return dataTable.Rows.Count > 0;
	}
	public bool ExistsWebAddr(string webAddr1)
	{
		new Desktop_Weblink();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("select * from frame_Desktop_Weblink");
		stringBuilder.Append(string.Concat(new string[]
		{
			" where WebAddr='",
			webAddr1,
			"'and  UserCode='",
			base.UserCode,
			"'"
		}));
		DataTable dataTable = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
		return dataTable.Rows.Count > 0;
	}
}


