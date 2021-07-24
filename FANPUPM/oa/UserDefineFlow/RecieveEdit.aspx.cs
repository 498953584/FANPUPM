using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_UserDefineFlow_RecieveEdit : NBasePage, IRequiresSessionState
{
	public System.Guid InstanceCode
	{
		get
		{
			object obj = this.ViewState["InstanceCode"];
			if (obj != null)
			{
				return (System.Guid)obj;
			}
			return System.Guid.Empty;
		}
		set
		{
			this.ViewState["InstanceCode"] = value;
		}
	}
	protected RecieveMsgAction rma
	{
		get
		{
			return new RecieveMsgAction();
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.InstanceCode = new System.Guid(base.Request["ic"]);
			string businessCode = FlowAuditAction.GetBusinessCode(this.InstanceCode);
			if (businessCode != "")
			{
				int num = FlowAuditAction.DoWithUrl(businessCode).IndexOf("?");
				if (num > 0)
				{
					this.frmPage.Attributes["src"] = FlowAuditAction.DoWithUrl(businessCode) + "&ic=" + this.InstanceCode;
				}
				else
				{
					this.frmPage.Attributes["src"] = FlowAuditAction.DoWithUrl(businessCode) + "?ic=" + this.InstanceCode;
				}
			}
			this.GetPageBind();
		}
	}
	protected void GetPageBind()
	{
		DataTable list = this.rma.GetList(string.Concat(new object[]
		{
			"RecieveUsercode = '",
			this.Session["yhdm"].ToString(),
			"' and InstanceCode = '",
			this.InstanceCode,
			"'"
		}));
		string text = "";
		string text2 = "";
		string text3 = "";
		userManageDb userManageDb = new userManageDb();
		for (int i = 0; i < list.Rows.Count; i++)
		{
			text = text + userManageDb.GetUserName(list.Rows[i]["v_yhdm"].ToString()) + ",";
			text2 = text2 + list.Rows[i]["v_yhdm"].ToString() + ",";
			text3 = list.Rows[i]["LookUrl"].ToString();
		}
		if (text != "")
		{
			this.txtAnnouncer.Text = text.Remove(text.Length - 1);
			this.hdnAnnouncer.Value = text2.Remove(text2.Length - 1);
			this.txtContent.Text = text3;
		}
	}
	private void RecieveMsgAdd(string usercode)
	{
		RecieveMsgModel recieveMsgModel = new RecieveMsgModel();
		recieveMsgModel.v_yhdm = usercode;
		recieveMsgModel.TheOrder = new decimal?(1m);
		recieveMsgModel.LookUrl = this.txtContent.Text;
		recieveMsgModel.InstanceCode = this.InstanceCode;
		recieveMsgModel.RecieveDate = System.DateTime.Now;
		recieveMsgModel.RecieveUserCode = this.Session["yhdm"].ToString();
		this.rma.Add(recieveMsgModel);
	}
	protected void btnAdd_Click(object sender, System.EventArgs e)
	{
		if (this.rma.Delete(this.InstanceCode, this.Session["yhdm"].ToString()) == 1)
		{
			string[] array = this.hdnAnnouncer.Value.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				this.RecieveMsgAdd(array[i]);
			}
			this.JS.Text = "alert('保存成功！');returnValue=true;window.close();";
			return;
		}
		this.JS.Text = "alert('保存失败，请找管理员！');";
	}
}


