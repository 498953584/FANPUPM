using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.action.ContractManage;
using com.jwsoft.sysManage.common;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProjectManage_Provider_ProManage : BasePage, IRequiresSessionState
{
	protected ProviderClass pc = new ProviderClass();
	protected ContractClass CClassObj = new ContractClass();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			if (base.Request.QueryString["opType"] == null)
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert(\"参数不正确！\");window.close();</script>");
			}
			else
			{
				this.ViewState["opType"] = base.Request.QueryString["opType"].ToString();
			}
			string text = "";
			if (this.ViewState["opType"].ToString().Trim() == "ProvTypeMge")
			{
				this.HidClassId.Value = base.Request.QueryString["ClassID"].ToString();
				text = this.pc.getDutyIDs(base.Request.QueryString["ClassID"].ToString()).Trim();
			}
			if (this.ViewState["opType"].ToString().Trim() == "ContClassMge")
			{
				this.ViewState["ContClassID"] = base.Request.QueryString["ContClassID"].ToString();
				text = this.CClassObj.getPurviews(this.ViewState["ContClassID"].ToString());
			}
			if (this.ViewState["opType"].ToString() == "InsClass")
			{
				this.ViewState["InsCCode"] = base.Request.QueryString["InsCCode"].Trim();
				text = this.GetInsClassPer("slt", this.ViewState["InsCCode"].ToString(), "");
			}
			this.HidDutyCodeS.Value = ((text == "") ? "" : (text + ","));
			text = ((text == "") ? "0" : text);
			this.rFrame2.Attributes["src"] = "DutyList2.aspx?DUTYID=" + text;
			CreatDepTree creatDepTree = new CreatDepTree(this.tv.Nodes);
			creatDepTree.EnabledLink = true;
			creatDepTree.Target = "rFrame";
			creatDepTree.NavigationURL = "DutyList.aspx";
			userManageDb userManageDb = new userManageDb();
			string a = userManageDb.manageDept(this.Session["yhdm"].ToString()).Trim();
			if (a == "1")
			{
				creatDepTree.BuildTreeView(this.Page.Session["yhdm"].ToString(), 0);
			}
			else
			{
				creatDepTree.BuildTreeView(this.Page.Session["yhdm"].ToString(), 0);
			}
			if (this.tv.Nodes.Count > 0)
			{
				this.tv.Nodes[0].Selected = true;
			}
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
	protected void ButOk_Click(object sender, EventArgs e)
	{
		string text = this.HidDutyCodeS.Value.ToString().Trim();
		string classId = this.HidClassId.Value.ToString().Trim();
		if (text != "")
		{
			text = text.Substring(0, text.Length - 1);
		}
		if (this.ViewState["opType"].ToString().Trim() == "ProvTypeMge")
		{
			if (!this.pc.updateWDUUser(classId, text))
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert(\"权限设置失败！\");</script>");
			}
			else
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert(\"权限设置成功！\");returnValue=true;window.close();</script>");
			}
		}
		if (this.ViewState["opType"].ToString().Trim() == "ContClassMge")
		{
			if (!this.CClassObj.updContClassPurv(this.ViewState["ContClassID"].ToString(), text))
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert(\"权限设置失败！\");</script>");
			}
			else
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert(\"权限设置成功！\");returnValue=true;window.close();</script>");
			}
		}
		if (this.ViewState["opType"].ToString().Trim() == "InsClass")
		{
			this.RegisterStartupScript("warn", "<script language=\"JavaScript\">returnValue='" + text + "';window.close()</script>");
		}
	}
	protected string GetInsClassPer(string op, string insCCode, string DutyIDs)
	{
		string result = "";
		if (op == "upd")
		{
			string sqlString = string.Concat(new string[]
			{
				" update InstitutionClass set PermissionSet='",
				DutyIDs,
				"',PermissionClass=0 where LeveCode='",
				insCCode,
				"' "
			});
			if (publicDbOpClass.ExecuteSQL(sqlString) > 0)
			{
				result = "1";
			}
			else
			{
				result = "0";
			}
		}
		else
		{
			if (op == "slt")
			{
				string sqlString2 = " select PermissionClass from InstitutionClass where levecode='" + insCCode + "' ";
				object obj = publicDbOpClass.ExecuteScalar(sqlString2);
				string a = "";
				if (obj != null)
				{
					a = obj.ToString();
				}
				if (a == "-1" || a == "1")
				{
					result = "";
				}
				else
				{
					if (a == "0")
					{
						string sqlString3 = " select PermissionSet from InstitutionClass where levecode='" + insCCode + "' ";
						object obj2 = publicDbOpClass.ExecuteScalar(sqlString3);
						if (obj2 != null)
						{
							result = obj2.ToString();
						}
					}
				}
			}
		}
		return result;
	}
}


