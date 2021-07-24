using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using cn.justwin.SMS;
using cn.justwin.Web;
using com.justwin.phoozyan;
using com.jwsoft.phoozyan;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using PmBusinessLogic;
using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class StartWf : NBasePage, IRequiresSessionState
	{
		private PTPrjInfoZTBService ptInfoZTbSer = new PTPrjInfoZTBService();
		private PTPrjInfoStateChangeService prjInfoChgSev = new PTPrjInfoStateChangeService();
		private PTPrjInfoService ptInfoSer = new PTPrjInfoService();
		public string BusinessCode
		{
			get
			{
				object obj = this.ViewState["BusinessCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "0";
			}
			set
			{
				this.ViewState["BusinessCode"] = value;
			}
		}
		public string BusinessClass
		{
			get
			{
				object obj = this.ViewState["BusinessClass"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "0";
			}
			set
			{
				this.ViewState["BusinessClass"] = value;
			}
		}
		public System.Guid RecordID
		{
			get
			{
				object obj = this.ViewState["RecordID"];
				if (obj != null)
				{
					return (System.Guid)obj;
				}
				return System.Guid.Empty;
			}
			set
			{
				this.ViewState["RecordID"] = value;
			}
		}
		public int NodeID
		{
			get
			{
				object obj = this.ViewState["NodeID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["NodeID"] = value;
			}
		}
		public string ProjectCode
		{
			get
			{
				object obj = this.ViewState["ProjectCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ProjectCode"] = value;
			}
		}
		public int TemplateID
		{
			get
			{
				object obj = this.ViewState["TemplateID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TemplateID"] = value;
			}
		}
		public int TemplateCount
		{
			get
			{
				object obj = this.ViewState["TemplateCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TemplateCount"] = value;
			}
		}
		public int OffsetCount
		{
			get
			{
				object obj = this.ViewState["OffsetCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["OffsetCount"] = value;
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

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.BusinessCode = base.Request["bcode"];
				this.BusinessClass = base.Request["bclass"];
				this.RecordID = new System.Guid(base.Request["fid"]);
				this.ProjectCode = base.Request["prjguid"].ToString();
				if (string.IsNullOrEmpty(this.ProjectCode))
				{
					this.ProjectCode = FlowAuditAction.GetProject(this.BusinessCode, this.RecordID);
				}
				this.txtReceiver.Attributes["ReadOnly"] = "true";
				if (this.CheckTemplate(base.Request["bcode"], base.Request["bclass"], new System.Guid(base.Request["fid"]), base.Request["prjguid"]) == "1")
				{
					this.JS.Text = "top.ui.alert('请先设置当前模块的审核流程');$(parent.document).find('.ui-icon-closethick').each(function() {this.click();});";
					this.BtnAdd.Enabled = false;
					return;
				}
				this.SetViewState(base.Request["bcode"], base.Request["bclass"], new System.Guid(base.Request["fid"]), base.Request["prjguid"], this.Session["yhdm"].ToString());
				this.UserCode = this.Session["yhdm"].ToString();
				this.CreateTemplateInfo();
				this.ddltTemplate.Attributes["onchange"] = "selectTemplate();";
				this.BtnAdd.Attributes["OnClick"] = base.GetPostBackEventReference(this.BtnAdd) + ";this.disabled=true;";
			}
		}
		protected string CheckTemplate(string bcode, string bclass, System.Guid fid, string prjguid)
		{
			string a = FlowAuditAction.BeginFlow(this.BusinessCode, this.BusinessClass, fid, prjguid, this.Session["yhdm"].ToString());
			if (a == "请先设置当前模块的审核流程")
			{
				return "1";
			}
			return "2";
		}
		protected void SetViewState(string businessCode, string businessClass, System.Guid recordID, string projectCode, string userCode)
		{
			string[] array = new string[3];
			DataTable dataTable = FlowTemplateAction.QueryTemplateByClass(businessClass, businessCode, userCode);
			int count = dataTable.Rows.Count;
			if (count == 1)
			{
				int num = System.Convert.ToInt32(dataTable.Rows[0]["TemplateID"].ToString());
				int posPositionNode = FlowChartAction.GetPosPositionNode(0, num);
				if (posPositionNode > 1)
				{
					array = FlowAuditAction.SelectOffSet(num, "0", 0, businessClass, recordID);
					if (array[2].ToString() != "0")
					{
						this.TemplateCount = count;
					}
					if (this.TemplateCount == 1)
					{
						this.OffsetCount = posPositionNode;
						this.NodeID = System.Convert.ToInt32(array[0]);
						return;
					}
				}
				else
				{
					if (posPositionNode == 1)
					{
						DataTable dataTable2 = FlowTemplateAction.QueryOffsetNodeFirst("0", num);
						this.TemplateCount = count;
						string a = dataTable2.Rows[0]["IsSelReceiver"].ToString();
						int nodeID = System.Convert.ToInt32(dataTable2.Rows[0]["NodeID"].ToString());
						if (a != "0")
						{
							this.NodeID = nodeID;
							return;
						}
					}
				}
			}
			else
			{
				if (count > 1)
				{
					this.TemplateCount = count;
				}
			}
		}
		protected void CreateTemplateInfo()
		{
			DataTable dataSource = FlowTemplateAction.QueryTempByClass(this.BusinessClass, this.BusinessCode, this.UserCode);
			this.ddltTemplate.Items.Clear();
			this.ddltTemplate.DataSource = dataSource;
			this.ddltTemplate.DataTextField = "TemplateName";
			this.ddltTemplate.DataValueField = "TemplateID";
			this.ddltTemplate.DataBind();
			ListItem listItem = new ListItem("---请选择流程模板---", "-1");
			listItem.Selected = true;
			this.ddltTemplate.Items.Insert(0, listItem);
			if (!string.IsNullOrEmpty(base.Request["templateid"]))
			{
				this.TemplateID = System.Convert.ToInt32(base.Request["templateid"]);
				this.hdnTemplateID.Value = base.Request["templateid"];
				this.ddltTemplate.Enabled = false;
				this.ddltTemplate.SelectedValue = base.Request["templateid"];
				FlowChartAction.display_FlowChart(this.tbFlowChart, this.TemplateID);
				if (FlowAuditAction.FirstNodeIsSelected(this.TemplateID))
				{
					this.txtReceiver.Enabled = true;
					this.IBPick.Attributes["onclick"] = "return pickPerson();";
					this.IBPick.Attributes["Style"] = "cursor: hand";
					DataTable dataTable = FlowTemplateAction.QueryOffsetNodeFirst("0", this.TemplateID);
					this.Type.Value = dataTable.Rows[0]["AuditorType"].ToString();
					if (this.Type.Value == "2")
					{
						this.Message.Visible = true;
						this.lblMessage.Text = "此节点类型为多人，必须选择多个人";
					}
					else
					{
						this.Message.Visible = false;
					}
					this.hfldNextAuditDepCode.Value = dataTable.Rows[0]["DepCode"].ToString();
					return;
				}
				this.Message.Visible = false;
				DataTable fistDt = FlowAuditAction.GetFistDt(this.TemplateID);
				if (fistDt.Rows.Count > 0)
				{
					if (fistDt.Rows[0][3].ToString() != "1")
					{
						string text = fistDt.Rows[0][2].ToString();
						if (fistDt.Rows[0][1].ToString() == "2" || fistDt.Rows[0][1].ToString() == "1")
						{
							this.txtReceiver.Text = FlowAuditAction.GetUsers(text);
						}
						else
						{
							if (fistDt.Rows[0][1].ToString() == "3" || fistDt.Rows[0][1].ToString() == "4" || fistDt.Rows[0][1].ToString() == "5")
							{
								DataTable role = FlowAuditAction.GetRole(text);
								this.txtReceiver.Text = role.Rows[0][0].ToString();
							}
						}
						this.txtReceiver.Enabled = false;
					}
					else
					{
						this.txtReceiver.Text = "";
					}
				}
				this.IBPick.Attributes["onclick"] = "return false";
				this.IBPick.Attributes["Style"] = "cursor: default";
				if (this.TemplateID == -1)
				{
					this.txtReceiver.Text = "";
					this.hdnReceiver.Value = "";
				}
			}
		}
		protected void ddltTemplate_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.ddltTemplate.SelectedValue != "-1")
			{
				this.TemplateID = System.Convert.ToInt32(this.ddltTemplate.SelectedItem.Value.ToString());
				DataTable dataTable = FlowTemplateAction.QueryOneTemplate(this.TemplateID);
				string text = dataTable.Rows[0]["Remark"].ToString();
				if (text.Length > 30)
				{
					this.lbltishi.Text = text.Substring(0, 30) + "...";
					this.lbltishi.ToolTip = text;
				}
				else
				{
					this.lbltishi.Text = text;
				}
				if (FlowAuditAction.FirstNodeIsSelected(this.TemplateID))
				{
					this.txtReceiver.Enabled = true;
					this.txtReceiver.Text = "";
					this.IBPick.Attributes["onclick"] = "return pickPerson();";
					this.IBPick.Attributes["Style"] = "cursor: hand";
					DataTable dataTable2 = FlowTemplateAction.QueryOffsetNodeFirst("0", this.TemplateID);
					this.Type.Value = dataTable2.Rows[0]["AuditorType"].ToString();
					if (this.Type.Value == "2")
					{
						this.Message.Visible = true;
						this.lblMessage.Text = "此节点类型为多人";
					}
					else
					{
						this.Message.Visible = false;
					}
					DataTable fistDt = FlowAuditAction.GetFistDt(this.TemplateID);
					if (fistDt.Rows.Count > 0)
					{
						this.hfldNextAuditDepCode.Value = fistDt.Rows[0]["DepCode"].ToString();
					}
				}
				else
				{
					this.Message.Visible = false;
					this.IBPick.Attributes["onclick"] = "return false";
					this.IBPick.Attributes["Style"] = "cursor: default";
					this.txtReceiver.Enabled = false;
					DataTable fistDt2 = FlowAuditAction.GetFistDt(this.TemplateID);
					if (fistDt2.Rows.Count != 0)
					{
						if (fistDt2.Rows[0][3].ToString() != "1")
						{
							string text2 = fistDt2.Rows[0][2].ToString();
							if (fistDt2.Rows[0][1].ToString() == "2" || fistDt2.Rows[0][1].ToString() == "1")
							{
								if (!FlowAuditAction.GetUsersis(text2))
								{
									this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "top.ui.alert('此审核流程中的接收人在系统中已离职或不存在，请重新选择流程模板');", true);
									this.ddltTemplate.SelectedValue = "-1";
									return;
								}
								this.txtReceiver.Text = FlowAuditAction.GetUsers(text2);
							}
							else
							{
								if (fistDt2.Rows[0][1].ToString() == "3" || fistDt2.Rows[0][1].ToString() == "4" || fistDt2.Rows[0][1].ToString() == "5")
								{
									DataTable role = FlowAuditAction.GetRole(text2);
									this.txtReceiver.Text = role.Rows[0][0].ToString();
								}
							}
						}
						else
						{
							this.txtReceiver.Text = "";
						}
					}
					if (this.TemplateID == -1)
					{
						this.txtReceiver.Text = "";
						this.hdnReceiver.Value = "";
					}
				}
				FlowChartAction.display_FlowChart(this.tbFlowChart, this.TemplateID);
				return;
			}
			this.txtReceiver.Text = "";
			this.hdnReceiver.Value = "";
			this.lblMessage.Text = "";
			this.lbltishi.Text = "";
			this.IBPick.Attributes["onclick"] = "";
		}
		protected void BtnAdd_Click(object sender, System.EventArgs e)
		{
			if (this.ddltTemplate.SelectedValue == "-1")
			{
				base.RegisterScript("top.ui.alert('请选择流程模板')");
				return;
			}
			WFInstanceMainService wFInstanceMainService = new WFInstanceMainService();
			WFTemplateNodeService wFTemplateNodeService = new WFTemplateNodeService();
			if (wFTemplateNodeService.GetNodes(this.TemplateID).Count == 0)
			{
				wFInstanceMainService.UpdateBusinessData(this.RecordID.ToString(), this.BusinessCode, 1);
				WFBusinessCodeService wFBusinessCodeService = new WFBusinessCodeService();
				WFBusinessCode byId = wFBusinessCodeService.GetById(this.BusinessCode);
				string path = base.Server.MapPath("~/SelfEventInfo.xml");
				string typeName = SelfEventAction.GetTypeName(path, byId.LinkTable, byId.StateField);
				if (!string.IsNullOrEmpty(typeName))
				{
					ISelfEvent selfEvent = (ISelfEvent)System.Reflection.Assembly.Load("PmBusinessLogic").CreateInstance(typeName);
					if (selfEvent != null)
					{
						System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(selfEvent.CommitEvent), this.RecordID.ToString());
					}
				}
				if (this.BusinessCode == "089")
				{
					PTPrjInfoZTB byId2 = this.ptInfoZTbSer.GetById(this.RecordID);
					this.ptInfoZTbSer.UpdatePrjState(byId2, new int?(2));
				}
				else
				{
					if (this.BusinessCode == "100")
					{
						PrjMember.AddLimit(this.RecordID);
					}
					else
					{
						if (this.BusinessCode == "107")
						{
							Progress.UpdateLatest(this.RecordID.ToString());
						}
						else
						{
							if (this.BusinessCode == "108")
							{
								cn.justwin.BLL.ProgressManagement.Version.UpdateLatest(this.RecordID.ToString());
							}
						}
					}
				}
				base.RegisterScript("top.ui.winSuccess({parentName: '_StartWf'}); top.ui.show('审核通过'); ");
				return;
			}
			if (wFInstanceMainService.IsReSubmit(this.RecordID.ToString(), this.BusinessCode))
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "top.ui.alert('当前数据已经被提交，禁止二次提交');", true);
				return;
			}
			if (this.ddltTemplate.SelectedValue != "-1")
			{
				FlowChartAction.display_FlowChart(this.tbFlowChart, this.TemplateID);
				if (this.txtReceiver.Text != "")
				{
					if (this.TemplateID == 0)
					{
						this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "top.ui.alert('请选择流程模板');", true);
						return;
					}
					string[] array = new string[3];
					this.TemplateID = System.Convert.ToInt32(this.hdnTemplateID.Value);
					if (this.TemplateCount > 1)
					{
						array = FlowAuditAction.GetOffsetNodeFirst(this.TemplateID, this.BusinessClass, this.RecordID);
						this.hdnNodeId.Value = array[0].ToString();
						this.hdnOrderNumber.Value = array[1].ToString();
					}
					else
					{
						if (this.TemplateCount == 1)
						{
							if (this.OffsetCount > 1)
							{
								this.hdnNodeId.Value = this.NodeID.ToString();
								this.hdnOrderNumber.Value = "1";
							}
							else
							{
								array = FlowAuditAction.GetOffsetNodeFirst(this.TemplateID, this.BusinessClass, this.RecordID);
								this.hdnNodeId.Value = array[0].ToString();
								this.hdnOrderNumber.Value = array[1].ToString();
							}
						}
					}
					int offsetorder = System.Convert.ToInt32(this.hdnOrderNumber.Value.ToString().Trim());
					this.NodeID = System.Convert.ToInt32(this.hdnNodeId.Value.ToString().Trim());
					if (this.hdnNodeId.Value.Trim() == "0")
					{
						this.JS.Text = "top.ui.alert('工作流程多分支模板未设置分支条件！');";
						return;
					}
					if (this.Type.Value != "")
					{
						FlowAuditAction.UpdateNode(this.hdnReceiver.Value, this.TemplateID);
					}
					if (FlowAuditAction.BeginFlow(this.BusinessClass, this.BusinessCode, this.RecordID, this.TemplateID, this.NodeID, offsetorder, this.ProjectCode, this.UserCode))
					{
						try
						{
							if (ConfigHelper.Get("SMS") != "")
							{
								DataTable dataTable = publicDbOpClass.DataTableQuary("select * from wf_templatenode where templateid=" + this.TemplateID + " and frontnode=0");
								DataTable dataTable2 = publicDbOpClass.DataTableQuary(string.Concat(new string[]
								{
									"select businessclassname from wf_business_class where businesscode='",
									this.BusinessCode,
									"' and businessclass='",
									this.BusinessClass,
									"'"
								}));
								if (dataTable.Rows.Count > 0)
								{
									string text = dataTable2.Rows[0][0].ToString();
									if (dataTable.Rows[0]["isselreceiver"].ToString() == "1")
									{
										SMS sMS = new SMS();
										string mbNo = new PhoozyanHelpAction().RePhoneCode(this.hdnReceiver.Value);
										string msg = string.Concat(new string[]
										{
											"公司短信:",
											this.txtReceiver.Text,
											"您好!",
											new CommunicationAction().BackUserName(this.UserCode),
											"已发起",
											text,
											"流程,请您查看审核!"
										});
										if (!(bool)sMS.Send("", mbNo, msg, "", "", "")[0])
										{
										}
									}
									else
									{
										string arg = "";
										System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
										System.Collections.ArrayList arrayList2 = new System.Collections.ArrayList();
										string text2 = dataTable.Rows[0]["Operater"].ToString();
										if (text2.Contains(","))
										{
											string[] array2 = text2.Split(new char[]
											{
												','
											});
											for (int i = 0; i < array2.Length - 1; i++)
											{
												new userManageDb().GetUserName(array2[i]);
												arrayList.Add(arrayList);
												string value = new PhoozyanHelpAction().RePhoneCode(array2[i]);
												arrayList2.Add(value);
											}
											for (int j = 0; j < arrayList.Count; j++)
											{
												if (!string.IsNullOrEmpty(arrayList2[j].ToString()))
												{
													string msg2 = string.Concat(new object[]
													{
														"公司短信:",
														arrayList[j],
														"您好!",
														new CommunicationAction().BackUserName(this.UserCode),
														"已发起",
														text,
														"流程,请您查看审核!"
													});
													SMS sMS2 = new SMS();
													if (!(bool)sMS2.Send("", arrayList2[j].ToString(), msg2, "", "", "")[0])
													{
														arg += arrayList[j];
													}
												}
												else
												{
													arg = arg + arrayList[j] + " ";
												}
											}
										}
										else
										{
											string text3 = new CommunicationAction().BackUserName(dataTable.Rows[0][0].ToString());
											DataTable dataTable3 = publicDbOpClass.DataTableQuary("select mobilephonecode from pt_yhmc where v_yhdm='" + dataTable.Rows[0]["Operater"].ToString() + "'");
											if (dataTable3.Rows.Count > 0)
											{
												string mbNo2 = dataTable3.Rows[0][0].ToString();
												if (dataTable2.Rows.Count > 0)
												{
													string msg3 = string.Concat(new string[]
													{
														"公司短信:",
														text3,
														"您好!",
														new CommunicationAction().BackUserName(this.UserCode),
														"已发起",
														text,
														"流程,请您查看审核!"
													});
													SMS sMS3 = new SMS();
													bool arg_85F_0 = (bool)sMS3.Send("", mbNo2, msg3, "", "", "")[0];
												}
											}
										}
									}
								}
							}
						}
						catch
						{
						}
						if (!string.IsNullOrEmpty(base.Request["purl"]))
						{
							base.RegisterScript("top.ui.winSuccess({parentName: '_StartWf'}); top.ui.show('工作流程已成功启动'); ");
							return;
						}
						base.RegisterScript("top.ui.winSuccess({parentName: '_StartWf'}); top.ui.show('工作流程已成功启动'); ");
						return;
					}
					else
					{
						base.ClientScript.RegisterStartupScript(base.GetType(), "myalert", "<script type='text/javascript'>alert('请找管理员设置 " + this.ddltTemplate.SelectedItem.Text.ToString() + " 流程的负责人。')</script>", false);
					}
				}
			}
		}
	}


