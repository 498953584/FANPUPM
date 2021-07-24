using ASP;
using cn.justwin.DAL;
using cn.justwin.Web;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EditModule : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["mkdm"] == null)
				{
					base.ClientScript.RegisterStartupScript(base.GetType(), "warn", "<script language=\"JavaScript\">alert(\"参数不正确！\");window.close();</script>");
				}
				else
				{
					this.ViewState["MKDM"] = base.Request.QueryString["mkdm"].ToString();
				}
				if (base.Request["op"] == null)
				{
					base.ClientScript.RegisterStartupScript(base.GetType(), "warn", "<script language=\"JavaScript\">alert(\"参数不正确！\");window.close();</script>");
				}
				else
				{
					this.ViewState["OP"] = base.Request.QueryString["op"].ToString();
				}
				string a;
				if ((a = this.ViewState["OP"].ToString().ToLower()) != null)
				{
					if (a == "add")
					{
						base.Header.Title = "添加模块";
						this.tbxPreCode.Text = this.ViewState["MKDM"].ToString();
						this.tbxPreCode.ReadOnly = true;
						this.tbxModuleCode.Text = this.CreateModuleCode(this.ViewState["MKDM"].ToString());
						return;
					}
					if (a == "upd")
					{
						this.RegularExpressionValidator1.Enabled = false;
						base.Header.Title = "更新模块";
						this.tbxPreCode.Visible = false;
						this.tbxModuleCode.Columns = 20;
						this.tbxModuleCode.ReadOnly = true;
						this.SetModuleState(this.ViewState["MKDM"].ToString());
						return;
					}
				}
				base.ClientScript.RegisterStartupScript(base.GetType(), "warn", "<script language=\"JavaScript\">alert(\"参数不正确！\");window.close();</script>");
			}
		}
		private void SetModuleState(string moduleCode)
		{
			DataTable oneModule = SystemModule.GetOneModule(moduleCode);
			if (oneModule.Rows.Count == 1)
			{
				DataRow dataRow = oneModule.Rows[0];
				this.tbxModuleCode.Text = dataRow["c_mkdm"].ToString();
				this.tbxModuleName.Text = dataRow["v_mkmc"].ToString();
				this.tbxModuleOrder.Text = dataRow["i_xh"].ToString();
				this.tbxModulePath.Text = dataRow["v_cdlj"].ToString();
				this.tbxModuleIcon.Text = dataRow["v_img"].ToString();
				this.cbBasic.Checked = Convert.ToBoolean(Convert.ToInt32(dataRow["IsBasic"]));
				this.cbMaintainable.Checked = Convert.ToBoolean(Convert.ToInt32(dataRow["IsMaintainable"]));
				this.TxthelpPath.Text = dataRow["helpPath"].ToString();
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
		protected void btnSave_Click(object sender, EventArgs e)
		{
			CacheHelper.Delete("MODULEVIEW");
			string str = this.tbxPreCode.Text.Trim();
			string text = this.tbxModuleCode.Text.Trim();
			string mkmc = this.tbxModuleName.Text.Trim();
			string cdlj = this.tbxModulePath.Text.Trim();
			string img = this.tbxModuleIcon.Text.Trim();
			int xh = (this.tbxModuleOrder.Text.Trim() == "") ? 0 : int.Parse(this.tbxModuleOrder.Text.Trim());
			string helpPath = this.TxthelpPath.Text.Trim();
			string strBasic = this.cbBasic.Checked ? "1" : "0";
			string strMaintainable = this.cbMaintainable.Checked ? "1" : "0";
			if (this.ViewState["OP"].ToString().ToLower() == "add")
			{
				if (SystemModule.AddModule(str + text, mkmc, cdlj, xh, img, strBasic, strMaintainable, helpPath))
				{
					string script = "\r\n\t\t\t\t\t\ttop.ui.show('保存成功！');\r\n\t\t\t\t\t\tjw.closeWin();\r\n\t\t\t\t\t\tjw.reloadTab();\r\n\t\t\t\t\t";
					base.RegisterScript(script);
					return;
				}
				string script2 = "\r\n\t\t\t\t\t\ttop.ui.alert('保存失败！');\r\n\t\t\t\t\t\tjw.closeWin();\r\n\t\t\t\t\t";
				base.RegisterScript(script2);
				return;
			}
			else
			{
				if (SystemModule.UpdModule(text, mkmc, cdlj, xh, img, strBasic, strMaintainable, helpPath))
				{
					string script3 = "\r\n\t\t\t\t\t\ttop.ui.show('保存成功！');\r\n\t\t\t\t\t\tjw.closeWin();\t\t\r\n\t\t\t\t\t\tjw.reloadTab();\r\n\t\t\t\t\t";
					base.RegisterScript(script3);
					return;
				}
				string script4 = "\r\n\t\t\t\t\t\ttop.ui.alert('保存失败！');\r\n\t\t\t\t\t\tjw.closeWin();\r\n\t\t\t\t\t";
				base.RegisterScript(script4);
				return;
			}
		}
		private string CreateModuleCode(string preCode)
		{
			string cmdText = "SELECT COUNT(*) FROM PT_mk WHERE C_MKDM LIKE @PreCode AND LEN(C_MKDM) = @Len";
			SqlParameter[] commandParameters = new SqlParameter[]
			{
				new SqlParameter("@PreCode", preCode + "%"),
				new SqlParameter("@Len", preCode.Length + 2)
			};
			object obj = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters);
			int num = DBHelper.GetInt(obj) + 1;
			while (this.IsExists(preCode + num.ToString().PadLeft(2, '0')))
			{
				num--;
			}
			return num.ToString().PadLeft(2, '0');
		}
		private bool IsExists(string code)
		{
			string cmdText = "SELECT COUNT(*) FROM PT_mk WHERE C_MKDM = @Code";
			SqlParameter[] commandParameters = new SqlParameter[]
			{
				new SqlParameter("@Code", code)
			};
			object obj = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters);
			return DBHelper.GetInt(obj) > 0;
		}
	}


