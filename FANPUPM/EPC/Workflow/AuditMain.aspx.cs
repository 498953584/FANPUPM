using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using cn.justwin.SMS;
using cn.justwin.Web;
using com.jwsoft.phoozyan;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using PmBusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Workflow_AuditInfo : NBasePage, IRequiresSessionState
{
	private AnnexAction _AnnexAction = new AnnexAction();
	private PTPrjInfoZTBService ptInfoZTbSer = new PTPrjInfoZTBService();
	private PTPrjInfoStateChangeService prjInfoChgSev = new PTPrjInfoStateChangeService();
	private PTPrjInfoService ptInfoSer = new PTPrjInfoService();
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
				this.NodeType.Value = FlowAuditAction.GetAuditorType(this.InstanceID);
				this.tr_selector.Style.Add("display", "block");
				this.IBPick.Attributes["onclick"] = "SelectPerson();";
				if (this.NodeType.Value == "2")
				{
					this.lblMessage.Visible = true;
					this.lblMessage.Text = "此节点类型为多人，必须选择多个人";
				}
				else
				{
					this.lblMessage.Visible = false;
				}
				this.hfldNextAuditDepCode.Value = FlowAuditAction.GetNextAuditDep(this.InstanceID);
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
				string currentAuditorType = FlowAuditAction.GetCurrentAuditorType(this.InstanceID);
				if (currentAuditorType != "1" && currentAuditorType != "")
				{
					this.btnFront.Disabled = true;
					this.btnAfter.Disabled = true;
				}
				else
				{
					this.btnFront.Disabled = false;
					this.btnAfter.Disabled = false;
				}
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
		this.FileUpload1.Folder = "/UploadFiles/Audit/" + this.InstanceCode.ToString();
		this.FileUpload1.RecordCode = this.InstanceID.ToString();
		if (this.BusinessCode == "126" || this.BusinessCode == "127" || this.BusinessCode == "128")
		{
			this.hfldIsAllowRebut.Value = "0";
			return;
		}
		this.hfldIsAllowRebut.Value = "1";
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
		try
		{
			if (this.isSecValidate == "1" && !FlowAuditAction.CheckAuditPwd(this.UserCode, this.txtAuditPwd.Text.Trim()))
			{
				this.JS.Text = "alert('审核密码不正确！');";
			}
			else
			{
				string auditInfo = this.txtAuditInfo.Value.Trim();
				string value = this.hdnType.Value;
				string insertAuditor = string.Empty;
				if (value == "前插")
				{
					insertAuditor = this.hdnFrontPerson.Value;
				}
				if (value == "后插")
				{
					insertAuditor = this.hdnAfterPerson.Value;
				}
				if (value == "前插" || value == "后插")
				{
					if ((value == "前插" && this.txtFrontPerson.Text == "") || (value == "后插" && this.txtAfterPerson.Text == ""))
					{
						base.RegisterScript("alert('请选择" + value + "审核人');");
					}
					else
					{
						string value2 = this.txtAuditRemark.Value;
						FlowAuditAction.InsertNode(this.InstanceID, this.UserCode, value, this.IsAllPass, insertAuditor, true, auditInfo, value2);
						this.SendSMS(this.InstanceID);
						System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
						stringBuilder.Append("top.ui.refreshDesktop(); \n");
						stringBuilder.Append("top.ui.show('" + value + "节点成功！'); \n");
						stringBuilder.Append("top.ui.closeTab();\n");
						this.JS.Text = stringBuilder.ToString();
					}
				}
				else
				{
					int num = System.Convert.ToInt32(this.RBLRoleType.SelectedValue);
					if (!FlowAuditAction.GetNextOperator(this.InstanceID) && this.txtnextperson.Text == "" && num == 1)
					{
						this.JS.Text = "alert('请选择流程下一个审核人');";
					}
					else
					{
						if (!FlowAuditAction.GetNextOperator(this.InstanceID) && this.txtnextperson.Text != "" && num == 1)
						{
							string project = FlowAuditAction.GetProject(this.BusinessCode, this.InstanceCode);
							if (!FlowAuditAction.SelectNextOperate(this.InstanceID, this.hdnNextPerson.Value.Trim(), int.Parse(this.NodeType.Value), this.UserCode, project))
							{
								this.JS.Text = "alert('请找管理员设置" + this.txtnextperson.Text + "的流程负责人');";
								return;
							}
						}
						if (num == -3 || num == -2)
						{
							string[] allFront = FlowAuditAction.GetAllFront(this.InstanceID);
							for (int i = 0; i < allFront.Length; i++)
							{
								this.AddMsg(allFront[i]);
							}
							this.DealAgent();
						}
						if (num == 1)
						{
							this.SendSMS(this.InstanceID);
						}
						this.RecieveMsgAdd();
						FlowAuditAction.ProcessFlow(this.InstanceID, this.IsAllPass, this.UserCode, num, auditInfo);
						string maxSing = FlowAuditAction.GetMaxSing(this.InstanceID);
						WFBusinessCodeService wFBusinessCodeService = new WFBusinessCodeService();
						WFBusinessCode byId = wFBusinessCodeService.GetById(this.BusinessCode);
						string path = base.Server.MapPath("~/SelfEventInfo.xml");
						string typeName = SelfEventAction.GetTypeName(path, byId.LinkTable, byId.StateField);
						if (!string.IsNullOrWhiteSpace(typeName))
						{
							ISelfEvent selfEvent = (ISelfEvent)System.Reflection.Assembly.Load("PmBusinessLogic").CreateInstance(typeName);
							if (selfEvent != null)
							{
								if (num == 1 && maxSing == "1")
								{
									selfEvent.CommitEvent(this.InstanceCode.ToString());
								}
								if (num == -2)
								{
									selfEvent.RefuseEvent(this.InstanceCode.ToString());
								}
								if (num == -3)
								{
									selfEvent.RestatedEvent(this.InstanceCode.ToString());
								}
							}
						}
						if (this.BusinessCode == "089")
						{
							PTPrjInfoZTB byId2 = this.ptInfoZTbSer.GetById(this.InstanceCode);
							if (maxSing == "1" && num == 1)
							{
								this.ptInfoZTbSer.UpdatePrjState(byId2, new int?(2));
							}
						}
						else
						{
							if (this.BusinessCode == "100")
							{
								if (maxSing == "1" && num == 1)
								{
									PrjMember.AddLimit(this.InstanceCode);
								}
							}
							else
							{
								if (this.BusinessCode == "107")
								{
									if (maxSing == "1" && num == 1)
									{
										Progress.UpdateLatest(this.InstanceCode.ToString());
									}
								}
								else
								{
									if (this.BusinessCode == "108" && maxSing == "1" && num == 1)
									{
										cn.justwin.BLL.ProgressManagement.Version.UpdateLatest(this.InstanceCode.ToString());
									}
								}
							}
						}
						if (maxSing == "1")
						{
							this.MsgOrganiger();
						}
						this.Session.Remove("HumanCode");
						this.Session.Remove("HumanName");
						this.JS.Text = "auditSuccess();";
					}
				}
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n此审核数据已经失效!')");
		}
	}
	private void NodeInfoRestore()
	{
		if (this.hdnNodeID.Value != "")
		{
			DataTable dataTable = FlowTemplateAction.QueryOneNode(System.Convert.ToInt32(this.hdnNodeID.Value));
			if (dataTable.Rows.Count <= 0)
			{
				this.trAuditMain.Style.Add("display", "none");
				return;
			}
			if (!string.IsNullOrEmpty(dataTable.Rows[0]["AuditMain"].ToString()))
			{
				this.divAuditMain.InnerHtml = this.FormatTextArea(base.Server.HtmlEncode(dataTable.Rows[0]["AuditMain"].ToString()));
				return;
			}
			this.trAuditMain.Style.Add("display", "none");
			return;
		}
		else
		{
			DataTable auditRemark = FlowTemplateAction.GetAuditRemark(this.InstanceID);
			if (auditRemark.Rows.Count <= 0)
			{
				this.trAuditMain.Style.Add("display", "none");
				return;
			}
			if (!string.IsNullOrEmpty(auditRemark.Rows[0]["AuditRemark"].ToString()))
			{
				this.divAuditMain.InnerHtml = auditRemark.Rows[0]["AuditRemark"].ToString();
				return;
			}
			this.trAuditMain.Style.Add("display", "none");
			return;
		}
	}
	private void RecieveMsgAdd()
	{
		if (this.hdnAnnouncer.Value.ToString().Trim() != "")
		{
			string[] array = this.hdnAnnouncer.Value.ToString().Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != "")
				{
					this.AddMsg(array[i].ToString());
				}
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
	private void SendSMS(int ic)
	{
		if (!this.ckbIsSendMsg.Checked || string.IsNullOrEmpty(ConfigHelper.Get("SMS")))
		{
			return;
		}
		DataTable auditUser = FlowAuditAction.GetAuditUser(ic);
		for (int i = 0; i < auditUser.Rows.Count; i++)
		{
			if (auditUser.Rows[i]["Operator"].ToString() != "")
			{
				string str = auditUser.Rows[i]["Operator"].ToString();
				userManageDb userManageDb = new userManageDb();
				try
				{
					string userName = userManageDb.GetUserName(auditUser.Rows[i]["Organiger"].ToString());
					string text = new CommunicationAction().BackUserName(this.UserCode);
					DataTable dataTable = publicDbOpClass.DataTableQuary(string.Concat(new string[]
					{
						"select businessclassname from wf_business_class where businesscode='",
						base.Request.QueryString["bc"],
						"' and businessclass='",
						base.Request.QueryString["bcl"],
						"'"
					}));
					if (dataTable.Rows.Count > 0)
					{
						string text2 = dataTable.Rows[0][0].ToString();
						string text3 = "";
						string sqlString = "select v_xm from pt_yhmc where v_yhdm='" + str + "'";
						string str2 = publicDbOpClass.DataTableQuary(sqlString).Rows[0]["V_xm"].ToString();
						string msg = string.Concat(new string[]
						{
							"公司短信：您好！",
							userName,
							"发起的",
							text2,
							"流程已由您的上一节点审核人",
							text,
							"审核通过,请您尽快查看审核!"
						});
						string text4 = publicDbOpClass.ExecuteScalar("select mobilephonecode from pt_yhmc where v_xm='" + str2 + "'").ToString();
						if (text4 != "")
						{
							SMS sMS = new SMS();
							sMS.Send("", text4, msg, "", "", "");
						}
						else
						{
							text3 = text3 + str2 + " ";
						}
						if (text3 != "")
						{
							this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "alert('姓名为" + text3 + "号码不对.信息不能发送!')", true);
						}
					}
				}
				catch
				{
				}
				if (ConfigHelper.RTXEnabled == "1")
				{
				}
			}
		}
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
		PublicInterface.SendSysMsg(new com.jwsoft.pm.entpm.model.PTDBSJ
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
	private void DealAgent()
	{
		WFInstanceService wFInstanceService = new WFInstanceService();
		WFInstance byId = wFInstanceService.GetById(this.InstanceID);
		if (byId == null || !byId.NodeID.HasValue)
		{
			return;
		}
		System.Collections.Generic.IList<WFInstance> byNodeId = wFInstanceService.GetByNodeId(byId.ID.Value, byId.NodeID.Value);
		WFTemplateNodeService wFTemplateNodeService = new WFTemplateNodeService();
		WFTemplateNode byNodeId2 = wFTemplateNodeService.GetByNodeId(byId.NodeID.Value);
		if (byNodeId2 != null && byNodeId2.AuditorType == "1" && byNodeId.Count == 2 && this.IsAgentShip(byNodeId[0].Operator, byNodeId[1].Operator, byId.NodeID.Value))
		{
			if (byId.Operator == byNodeId[0].Operator)
			{
				wFInstanceService.Delete(byNodeId[1]);
				return;
			}
			wFInstanceService.Delete(byNodeId[0]);
		}
	}
	private bool IsAgentShip(string user1, string user2, int templateNodeId)
	{
		string cmdText = string.Format("\r\n\t\t\t\t\t\t\tSELECT COUNT(*) FROM WF_Agent \r\n\t\t\t\t\t\t\tWHERE TempNodeId = @tempNodeId AND MainUser = @mainUser AND SecondUser = @secondUser  \r\n\t\t\t\t\t\t", new object[0]);
		SqlParameter[] commandParameters = new SqlParameter[]
		{
			new SqlParameter("@tempNodeId", templateNodeId),
			new SqlParameter("@mainUser", user1),
			new SqlParameter("@secondUser", user2)
		};
		int @int = DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
		SqlParameter[] commandParameters2 = new SqlParameter[]
		{
			new SqlParameter("@tempNodeId", templateNodeId),
			new SqlParameter("@mainUser", user2),
			new SqlParameter("@secondUser", user1)
		};
		int int2 = DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters2));
		return @int == 1 || int2 == 1;
	}
}


