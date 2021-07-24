using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using PmBusinessLogic;
using System;
using System.Data;
using System.Reflection;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_Workflow_WorkflowAuditFrame : NBasePage, IRequiresSessionState
{
	private AnnexAction _AnnexAction = new AnnexAction();
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
	public int InstanceID
	{
		get
		{
			object obj = this.ViewState["InstanceID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["InstanceID"] = value;
		}
	}
	public string IsAllPass
	{
		get
		{
			object obj = this.ViewState["IsAllPass"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["IsAllPass"] = value;
		}
	}
	public new string UserCode
	{
		get
		{
			object obj = this.ViewState["UserCode"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["UserCode"] = value;
		}
	}
	public string BusinessCode
	{
		get
		{
			object obj = this.ViewState["BUSINESSCODE"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["BUSINESSCODE"] = value;
		}
	}
	protected string BusinessClass
	{
		get
		{
			return this.ViewState["BUSINESSCLASS"].ToString();
		}
		set
		{
			this.ViewState["BUSINESSCLASS"] = value;
		}
	}
	public string isSecValidate
	{
		get
		{
			object obj = this.ViewState["ISSECVALIDATE "];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["ISSECVALIDATE "] = value;
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
			this.Session.Remove("HumanCode");
			this.Session.Remove("HumanName");
			this.InstanceCode = new System.Guid(base.Request["ic"]);
			this.InstanceID = System.Convert.ToInt32(base.Request["id"]);
			this.Session["InstanceId"] = base.Request["id"];
			if (!FlowAuditAction.GetNextOperator(this.InstanceID))
			{
				this.tr_selector.Style.Add("display", "block");
			}
			this.IsAllPass = base.Request["pass"];
			this.hdnNodeID.Value = base.Request["nid"];
			this.BusinessCode = base.Request["bc"].ToString();
			this.BusinessClass = base.Request["bcl"].ToString();
			if (this.hdnNodeID.Value == "0" || this.hdnNodeID.Value == "")
			{
				this.btnFront.Disabled = true;
				this.btnAfter.Disabled = true;
			}
			else
			{
				this.btnFront.Disabled = false;
				this.btnAfter.Disabled = false;
			}
			this.UserCode = System.Convert.ToString(this.Session["yhdm"]);
			this.hdnInstanceCode.Value = this.InstanceCode.ToString();
			DataTable dataTable = FlowAuditAction.TempNodesList(this.Session["yhdm"].ToString(), this.InstanceCode);
			if (dataTable.Rows.Count > 0)
			{
				this.isSecValidate = dataTable.Rows[0]["isSecValidate"].ToString();
			}
			if (this.isSecValidate == "0" || this.isSecValidate == "")
			{
				this.txtAuditPwd.Enabled = false;
				this.RequiredFieldValidator1.Enabled = false;
				this.LbAduPass.Enabled = false;
				this.LbAduPass.Visible = false;
				this.txtAuditPwd.Visible = false;
				this.RequiredFieldValidator1.Visible = false;
			}
			else
			{
				this.trPass.Visible = true;
			}
			string.Concat(new object[]
			{
				FlowAuditAction.DoWithUrl(this.BusinessCode),
				"ic=",
				this.InstanceCode,
				"&id=",
				this.InstanceID,
				"&pass=",
				this.IsAllPass
			});
			this.frmPage.Attributes["src"] = string.Concat(new object[]
			{
				FlowAuditAction.DoWithUrl(this.BusinessCode),
				"ic=",
				this.InstanceCode,
				"&id=",
				this.InstanceID,
				"&pass=",
				this.IsAllPass
			});
			this.NodeInfoRestore();
			this.GetPageDuring();
			this.btnFlowStatus.Attributes["onclick"] = string.Concat(new string[]
			{
				"openAudit(0,'",
				this.BusinessCode,
				"','",
				this.BusinessClass,
				"');"
			});
			this.btnAuditrecord.Attributes["onclick"] = string.Concat(new string[]
			{
				"openAudit(1,'",
				this.BusinessCode,
				"','",
				this.BusinessClass,
				"');"
			});
		}
	}
	public string FormatTextArea(string str)
	{
		while (str.IndexOf("\n") != -1)
		{
			str = str.Substring(0, str.IndexOf("\n")) + "<br>" + str.Substring(str.IndexOf("\n") + 1);
		}
		while (str.IndexOf(" ") != -1)
		{
			str = str.Substring(0, str.IndexOf(" ")) + "&nbsp;" + str.Substring(str.IndexOf(" ") + 1);
		}
		return str;
	}
	protected void btnSubmit_Click(object sender, System.EventArgs e)
	{
		if (!FlowAuditAction.GetNextOperator(this.InstanceID) && this.txtnextperson.Text == "" && this.RBLRoleType.SelectedValue == "1")
		{
			this.JS.Text = "alert('请选择流程下一个审核人');";
			return;
		}
		if (!FlowAuditAction.GetNextOperator(this.InstanceID) && this.txtnextperson.Text != "" && this.RBLRoleType.SelectedValue == "1")
		{
			FlowAuditAction.InsertOperator(this.InstanceID, this.hdnNextPerson.Value.Trim());
		}
		if (this.isSecValidate == "1")
		{
			string password = this.txtAuditPwd.Text.Trim();
			if (!FlowAuditAction.CheckAuditPwd(this.UserCode, password))
			{
				this.JS.Text = "alert('审核密码不正确！');";
				return;
			}
		}
		string value = this.txtAuditInfo.Value;
		if (this.RBLRoleType.SelectedValue == "-3" || this.RBLRoleType.SelectedValue == "-2")
		{
			string[] allFront = FlowAuditAction.GetAllFront(this.InstanceID);
			for (int i = 0; i < allFront.Length; i++)
			{
				this.AddMsg(allFront[i]);
			}
		}
		if (this.RBLRoleType.SelectedValue == "-3")
		{
			DataTable instanceMain = FlowAuditAction.GetInstanceMain(this.InstanceID);
			FlowAuditAction.DealAudit(new System.Guid(instanceMain.Rows[0]["instancecode"].ToString()), instanceMain.Rows[0]["businesscode"].ToString(), instanceMain.Rows[0]["businessclass"].ToString(), -3);
			string text = " parent.desktop.flowclass.location='/SysFrame/showinfomation.aspx';";
			text += "alert('保存审核结果成功');";
			text += "top.frameWorkArea.window.desktop.getActive().close();";
			this.JS.Text = text;
			return;
		}
		int num = 0;
		string selectedValue;
		if ((selectedValue = this.RBLRoleType.SelectedValue) != null)
		{
			if (!(selectedValue == "1"))
			{
				if (!(selectedValue == "-2"))
				{
					if (selectedValue == "-3")
					{
						num = -3;
					}
				}
				else
				{
					num = -2;
				}
			}
			else
			{
				num = 1;
			}
		}
		string value2 = this.hdnType.Value;
		string value3;
		if (value2 == "前插")
		{
			value3 = this.hdnFrontPerson.Value;
			this.RequiredFieldValidator2.Enabled = true;
		}
		else
		{
			value3 = this.hdnAfterPerson.Value;
			this.RequiredFieldValidator3.Enabled = true;
		}
		if (!(value2 == "前插") && !(value2 == "后插"))
		{
			if (num == 1)
			{
				this.GetMsgDB(this.InstanceID);
			}
			if (this.Session["HumanCode"] != null)
			{
				this.RecieveMsgAdd();
			}
			FlowAuditAction.ProcessFlow(this.InstanceID, this.IsAllPass, this.UserCode, num, value);
			FlowAuditAction.GetbusinesscodeTable(this.InstanceID);
			DataTable dataTable = null;
			if (dataTable.Rows.Count > 0)
			{
				ISelfEvent selfEvent = (ISelfEvent)System.Reflection.Assembly.Load(dataTable.Rows[0]["assemblename"].ToString()).CreateInstance(dataTable.Rows[0]["assemblename"].ToString() + "." + dataTable.Rows[0]["classname"].ToString());
				string maxSing = FlowAuditAction.GetMaxSing(this.InstanceID);
				if (selfEvent != null)
				{
					if (this.RBLRoleType.SelectedValue == "1" && maxSing == "1")
					{
						selfEvent.CommitEvent(this.InstanceCode.ToString());
					}
					if (this.RBLRoleType.SelectedValue == "0")
					{
						selfEvent.RefuseEvent(this.InstanceCode.ToString());
					}
				}
			}
			this.MsgOrganiger();
			this.Session.Remove("HumanCode");
			this.Session.Remove("HumanName");
			string text2 = "parent.desktop.flowclass.location='/SysFrame/showinfomation.aspx';";
			text2 += "alert('保存审核结果成功！');";
			text2 += "top.frameWorkArea.window.desktop.getActive().close();";
			this.JS.Text = text2;
			return;
		}
		if ((value2 == "前插" && this.txtFrontPerson.Text == "") || (value2 == "后插" && this.txtAfterPerson.Text == ""))
		{
			base.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "", "alert('请选择" + value2 + "审核人');", true);
			return;
		}
		string value4 = this.txtAuditRemark.Value;
		FlowAuditAction.InsertNode(this.InstanceID, this.UserCode, value2, this.IsAllPass, value3, false, value, value4);
		string text3 = " parent.desktop.flowclass.location='/SysFrame/showinfomation.aspx';";
		text3 = text3 + "alert('" + value2 + "节点成功！');";
		text3 += "top.frameWorkArea.window.desktop.getActive().close();";
		this.JS.Text = text3;
	}
	private void NodeInfoRestore()
	{
		if (this.hdnNodeID.Value != "")
		{
			DataTable dataTable = FlowTemplateAction.QueryOneNode(System.Convert.ToInt32(this.hdnNodeID.Value));
			if (dataTable.Rows.Count > 0)
			{
				this.divAuditMain.InnerHtml = this.FormatTextArea(base.Server.HtmlEncode(dataTable.Rows[0]["AuditMain"].ToString()));
				return;
			}
		}
		else
		{
			DataTable auditRemark = FlowTemplateAction.GetAuditRemark(this.InstanceID);
			if (auditRemark.Rows.Count > 0)
			{
				this.divAuditMain.InnerHtml = auditRemark.Rows[0]["AuditRemark"].ToString();
			}
		}
	}
	private void RecieveMsgAdd()
	{
		if (this.hdnAnnouncer.Value.ToString().Trim() != "")
		{
			string text = this.Session["HumanCode"].ToString().Substring(0, this.Session["HumanCode"].ToString().Length - 1);
			string[] array = text.Split(new char[]
			{
				'!'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					':'
				});
				this.AddMsg(array2[1].ToString());
			}
		}
	}
	private void AddMsg(string usercode)
	{
		RecieveMsgModel recieveMsgModel = new RecieveMsgModel();
		recieveMsgModel.v_yhdm = usercode;
		recieveMsgModel.TheOrder = new decimal?(1m);
		string text;
		if (this.RBLRoleType.SelectedValue == "-2")
		{
			text = "流程驳回";
		}
		else
		{
			if (this.RBLRoleType.SelectedValue == "1")
			{
				text = "流程通过";
			}
			else
			{
				text = "流程重报";
			}
		}
		DataTable instanceMain = FlowAuditAction.GetInstanceMain(this.InstanceID);
		string businessClassName = FlowAuditAction.GetBusinessClassName(instanceMain.Rows[0]["businesscode"].ToString(), instanceMain.Rows[0]["businessclass"].ToString());
		RecieveMsgModel expr_B1 = recieveMsgModel;
		string lookUrl = expr_B1.LookUrl;
		expr_B1.LookUrl = string.Concat(new string[]
		{
			lookUrl,
			businessClassName,
			text,
			"提醒",
			this.txtContent.Value
		});
		recieveMsgModel.InstanceCode = this.InstanceCode;
		recieveMsgModel.RecieveDate = System.DateTime.Now;
		recieveMsgModel.RecieveUserCode = this.Session["yhdm"].ToString();
		this.rma.Add(recieveMsgModel);
	}
	private void GetMsgDB(int ic)
	{
		DataTable auditUser = FlowAuditAction.GetAuditUser(ic);
		if (auditUser.Rows.Count > 0)
		{
			for (int i = 0; i < auditUser.Rows.Count; i++)
			{
				if (auditUser.Rows[i]["Operator"].ToString() != "")
				{
					string xgid = auditUser.Rows[i]["ID"].ToString();
					string text = "您需要审核：";
					string jsyhdm = auditUser.Rows[i]["Operator"].ToString();
					userManageDb userManageDb = new userManageDb();
					text = text + userManageDb.GetUserName(auditUser.Rows[i]["Organiger"].ToString()) + "发起的" + auditUser.Rows[i]["BusinessClassName"].ToString();
					if (this.ckbIsSendInfo.Checked)
					{
						this.getPTDBSJ(xgid, text, jsyhdm);
					}
					if (this.ckbIsSendMsg.Checked)
					{
						this.GetSms(xgid, text, jsyhdm);
					}
				}
			}
		}
	}
	private void getPTDBSJ(string xgid, string Mes, string jsyhdm)
	{
		PublicInterface.SendSysMsg(new PTDBSJ
		{
			V_LXBM = "015",
			I_XGID = xgid,
			DTM_DBSJ = System.DateTime.Now,
			V_Content = Mes,
			V_DBLJ = "",
			V_YHDM = jsyhdm
		});
	}
	private void GetSms(string xgid, string Mes, string jsyhdm)
	{
		PublicInterface.SendSmsMsg(new SMSLog
		{
			SendUser = this.Session["yhdm"].ToString(),
			SendTime = System.DateTime.Now,
			ReceiveUser = jsyhdm,
			Message = Mes,
			V_LXBM = "015",
			I_XGID = xgid
		});
	}
	private void GetPageDuring()
	{
		DataTable during = FlowAuditAction.GetDuring(this.InstanceID);
		if (during.Rows.Count > 0 && during.Rows[0]["During"].ToString() != "")
		{
			this.LbDuring.Text = during.Rows[0]["During"].ToString();
			this.GetPageDuringInfo(System.Convert.ToInt32(during.Rows[0]["During"]), System.Convert.ToDateTime(during.Rows[0]["ArriveTime"]));
		}
	}
	private void GetPageDuringInfo(int during, System.DateTime ArriveTime)
	{
		System.TimeSpan timeSpan = System.DateTime.Now.Subtract(ArriveTime);
		int num = timeSpan.Days * 24 + timeSpan.Hours - during;
		if (num > 0)
		{
			this.LbDuringInfo.Text = string.Concat(new object[]
			{
				"超时 ",
				num.ToString(),
				" 小时",
				timeSpan.Minutes,
				"分"
			});
			return;
		}
		num = during - 1 - (timeSpan.Days * 24 + timeSpan.Hours);
		if (num == -1)
		{
			num = 0;
		}
		this.LbDuringInfo.Text = string.Concat(new string[]
		{
			"还剩 ",
			num.ToString(),
			" 小时",
			(60 - timeSpan.Minutes).ToString(),
			"分"
		});
	}
	protected void MsgOrganiger()
	{
		if (FlowAuditAction.IsAuditOver(this.InstanceID))
		{
			DataTable dataTable = FlowAuditAction.OrganigerCode(this.InstanceID);
			if (dataTable.Rows.Count > 0)
			{
				this.getOrganiger(this.InstanceID.ToString(), "您发起" + dataTable.Rows[0]["BusinessClassName"].ToString() + "的流程已审核完结！", dataTable.Rows[0]["Organiger"].ToString());
			}
		}
	}
	private void getOrganiger(string xgid, string Mes, string jsyhdm)
	{
		PublicInterface.SendSysMsg(new PTDBSJ
		{
			V_LXBM = "017",
			I_XGID = xgid,
			DTM_DBSJ = System.DateTime.Now,
			V_Content = Mes,
			V_DBLJ = string.Concat(new string[]
			{
				"?rid=",
				xgid,
				"&bc=",
				this.BusinessCode,
				"&bcl=",
				this.BusinessClass
			}),
			V_YHDM = jsyhdm
		});
	}
}


