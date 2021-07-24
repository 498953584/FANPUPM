using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ReturnLogin : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack && this.Session["userLoginID"] != null && base.Application["UserCodeCollection"] != null)
			{
				DataTable dataTable = (DataTable)base.Application["UserCodeCollection"];
				foreach (DataRow dataRow in dataTable.Rows)
				{
					if (dataRow["loginID"].ToString() == this.Session["userLoginID"].ToString())
					{
						this.l_name.Text = dataRow["loginID"].ToString() + "：";
						this.l_name2.Text = dataRow["loginID"].ToString();
						this.l_loginTime.Text = dataRow["loginTime"].ToString();
						this.l_ip.Text = dataRow["ip"].ToString();
						break;
					}
				}
				this.Button1.Attributes["onclick"] = "alert('该登陆信息已经撤消，请点击确定登陆到本系统！');";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void Button1_Click(object sender, EventArgs e)
		{
			this.AddUserInfo(this.Session["yhdm"].ToString(), true);
			FormsAuthentication.RedirectFromLoginPage(this.Session["yhdm"].ToString(), false);
			string str = FormsAuthentication.HashPasswordForStoringInConfigFile(this.Session["loginCipher"].ToString(), "md5");
			base.Response.Redirect("/login.aspx?n=" + this.Session["userLoginID"].ToString() + "&p=" + str);
			this.Session.Remove("loginCipher");
			this.Session.Remove("userLoginID");
		}
		private void AddUserInfo(string userCode, bool isReturn)
		{
			DataTable dataTable = this.GetUserTable();
			base.Application.Lock();
			if (isReturn)
			{
				if (base.Application["TempUserCodeCollection"] != null)
				{
					dataTable = (DataTable)base.Application["TempUserCodeCollection"];
				}
			}
			else
			{
				if (base.Application["UserCodeCollection"] != null)
				{
					dataTable = (DataTable)base.Application["UserCodeCollection"];
				}
			}
			DataRow dataRow = dataTable.NewRow();
			dataRow["userCode"] = userCode;
			dataRow["loginID"] = this.Session["userLoginID"].ToString();
			dataRow["loginTime"] = DateTime.Now;
			dataRow["endTime"] = DateTime.Now;
			dataRow["ip"] = HttpContext.Current.Request.UserHostAddress;
			dataTable.Rows.Add(dataRow);
			if (isReturn)
			{
				base.Application["TempUserCodeCollection"] = dataTable;
			}
			else
			{
				base.Application["UserCodeCollection"] = dataTable;
			}
			base.Application.UnLock();
		}
		private DataTable GetUserTable()
		{
			DataTable dataTable = new DataTable();
			DataColumn column = new DataColumn("userCode", Type.GetType("System.String"));
			dataTable.Columns.Add(column);
			column = new DataColumn("loginID", Type.GetType("System.String"));
			dataTable.Columns.Add(column);
			column = new DataColumn("loginTime", Type.GetType("System.DateTime"));
			dataTable.Columns.Add(column);
			column = new DataColumn("endTime", Type.GetType("System.DateTime"));
			dataTable.Columns.Add(column);
			column = new DataColumn("ip", Type.GetType("System.String"));
			dataTable.Columns.Add(column);
			return dataTable;
		}
	}


