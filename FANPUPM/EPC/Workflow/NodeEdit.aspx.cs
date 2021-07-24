using ASP;
using cn.justwin.BLL;
using cn.justwin.Web;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class NodeEdit : NBasePage, IRequiresSessionState
	{
		protected string xyPos = "";
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
		public string FrontNode
		{
			get
			{
				object obj = this.ViewState["FrontNode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["FrontNode"] = value;
			}
		}
		public string FrontNodeOld
		{
			get
			{
				object obj = this.ViewState["FrontNodeOld"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["FrontNodeOld"] = value;
			}
		}
		public int xpos
		{
			get
			{
				object obj = this.ViewState["xpos"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["xpos"] = value;
			}
		}
		public int ypos
		{
			get
			{
				object obj = this.ViewState["ypos"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["ypos"] = value;
			}
		}
		public string types
		{
			get
			{
				object obj = this.ViewState["types"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "0";
			}
			set
			{
				this.ViewState["types"] = value;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.TemplateID = System.Convert.ToInt32(base.Request["ti"]);
				this.NodeID = System.Convert.ToInt32(base.Request["ni"]);
				this.FrontNode = base.Request["front"];
				this.types = base.Request["lb"];
				this.hdnFrontNode.Value = this.FrontNode.ToString();
				this.xyPos = base.Request["xy"];
				int num = this.xyPos.IndexOf(",");
				this.xpos = System.Convert.ToInt32(this.xyPos.Substring(0, num));
				this.ypos = System.Convert.ToInt32(this.xyPos.Substring(num + 1, this.xyPos.Length - num - 1));
				this.GetAuditorType(this.types);
				if (this.NodeID != 0)
				{
					this.NodeInfoRestore();
				}
				else
				{
					this.txtDuring.Text = ConfigHelper.AuditExpire;
				}
				this.txtOperater.Attributes["ReadOnly"] = "true";
				this.FrontNodeOld = this.hdnFrontNode.Value.ToString();
				this.SetFrontNode(this.TemplateID, this.FrontNodeOld);
				this.rbSingle.Attributes["onclick"] = "clear();";
				this.rbMilti.Attributes["onclick"] = "clear();";
				this.rbGroup.Attributes["onclick"] = "clear();";
				this.rbItem.Attributes["onclick"] = "clear();";
				this.rbDept.Attributes["onclick"] = "clear();";
				this.BtnCondition.Attributes["onclick"] = "conditionSet(" + this.NodeID.ToString() + ");";
				this.imgPick.Attributes["onclick"] = "pickOperater(" + this.types.ToString() + ");";
				if (ConfigHelper.ProjectType == "ZdContract")
				{
					this.rbGroup.Visible = false;
					this.rbItem.Visible = false;
					this.rbDept.Visible = false;
				}
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void GetAuditorType(string t)
		{
			if (t != null)
			{
				if (t == "1")
				{
					this.rbSingle.Enabled = true;
					this.rbSingle.Checked = true;
					this.rbMilti.Enabled = false;
					this.rbGroup.Enabled = false;
					this.rbItem.Enabled = false;
					this.rbDept.Enabled = false;
					this.rbMilti.Checked = false;
					this.rbGroup.Checked = false;
					this.rbItem.Checked = false;
					this.rbDept.Checked = false;
					this.Page.Title = "单人流程节点维护";
					return;
				}
				if (t == "2")
				{
					this.rbMilti.Enabled = true;
					this.rbMilti.Checked = true;
					this.rbSingle.Enabled = false;
					this.rbGroup.Enabled = false;
					this.rbItem.Enabled = false;
					this.rbDept.Enabled = false;
					this.rbSingle.Checked = false;
					this.rbGroup.Checked = false;
					this.rbItem.Checked = false;
					this.rbDept.Checked = false;
					this.Page.Title = "多人流程节点维护";
					return;
				}
				if (t == "3")
				{
					this.rbGroup.Enabled = true;
					this.rbGroup.Checked = true;
					this.rbSingle.Enabled = false;
					this.rbMilti.Enabled = false;
					this.rbItem.Enabled = false;
					this.rbDept.Enabled = false;
					this.rbSingle.Checked = false;
					this.rbMilti.Checked = false;
					this.rbItem.Checked = false;
					this.rbDept.Checked = false;
					this.Page.Title = "群组流程节点维护";
					this.tr0.Visible = false;
					return;
				}
				if (t == "4")
				{
					this.rbItem.Enabled = true;
					this.rbItem.Checked = true;
					this.rbDept.Enabled = false;
					this.rbSingle.Enabled = false;
					this.rbGroup.Enabled = false;
					this.rbMilti.Enabled = false;
					this.rbSingle.Checked = false;
					this.rbGroup.Checked = false;
					this.rbMilti.Checked = false;
					this.rbDept.Checked = false;
					this.Page.Title = "项目相关流程节点维护";
					this.tr0.Visible = false;
					return;
				}
				if (!(t == "5"))
				{
					return;
				}
				this.rbDept.Enabled = true;
				this.rbDept.Checked = true;
				this.rbItem.Enabled = false;
				this.rbItem.Checked = false;
				this.rbGroup.Enabled = false;
				this.rbGroup.Checked = false;
				this.rbMilti.Enabled = false;
				this.rbMilti.Checked = false;
				this.rbSingle.Enabled = false;
				this.rbSingle.Checked = false;
				this.Page.Title = "部门相关流程节点维护";
				this.tr0.Visible = false;
			}
		}
		private void NodeInfoRestore()
		{
			DataTable dataTable = FlowTemplateAction.QueryOneNode(this.NodeID);
			if (dataTable.Rows.Count == 1)
			{
				this.hdnOperater.Value = dataTable.Rows[0]["Operater"].ToString();
				if ((string)dataTable.Rows[0]["AuditorType"] == "1")
				{
					this.GetAuditorType("1");
					this.txtOperater.Text = PageHelper.QueryUser(this, dataTable.Rows[0]["Operater"].ToString());
				}
				if ((string)dataTable.Rows[0]["AuditorType"] == "2")
				{
					string text = "";
					this.GetAuditorType(dataTable.Rows[0]["AuditorType"].ToString());
					string text2 = dataTable.Rows[0]["Operater"].ToString();
					string[] array = text2.Split(new char[]
					{
						','
					});
					if (array.Length > 0)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (!(array[i] == ""))
							{
								text = text + PageHelper.QueryUser(this, array[i]) + ",";
							}
						}
					}
					this.txtOperater.Text = text;
				}
				if ((string)dataTable.Rows[0]["AuditorType"] == "3")
				{
					this.GetAuditorType("3");
					this.txtOperater.Text = dataTable.Rows[0]["RoleName"].ToString();
				}
				if ((string)dataTable.Rows[0]["AuditorType"] == "4")
				{
					this.GetAuditorType("4");
					this.txtOperater.Text = dataTable.Rows[0]["RoleName"].ToString();
				}
				if ((string)dataTable.Rows[0]["AuditorType"] == "5")
				{
					this.GetAuditorType("5");
					this.txtOperater.Text = dataTable.Rows[0]["RoleName"].ToString();
				}
				if ((string)dataTable.Rows[0]["IsAllPass"] == "1")
				{
					this.rbIsAllPass1.Checked = true;
				}
				else
				{
					this.rbIsAllPass2.Checked = true;
				}
				if ((string)dataTable.Rows[0]["IsSecValidate"] == "1")
				{
					this.CkbIsSec.Checked = true;
				}
				else
				{
					this.CkbIsSec.Checked = false;
				}
				if ((string)dataTable.Rows[0]["IsSelReceiver"] == "1")
				{
					this.CkbIsSelReceiver.Checked = true;
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "check();", true);
				}
				else
				{
					this.CkbIsSelReceiver.Checked = false;
				}
				this.RbDueMode.SelectedValue = dataTable.Rows[0]["DueMode"].ToString();
				if (this.hdnOperater.Value.Length > 0)
				{
					this.hdnOperater.Value.Substring(0, this.hdnOperater.Value.Length - 1);
				}
				else
				{
					string arg_3C5_0 = this.hdnOperater.Value;
				}
				this.txtNodeName.Text = dataTable.Rows[0]["NodeName"].ToString();
				this.txtRemark.Text = dataTable.Rows[0]["ConditionDescription"].ToString();
				this.txtDuring.Text = dataTable.Rows[0]["During"].ToString();
				this.hdnFrontNode.Value = dataTable.Rows[0]["FrontNode"].ToString();
				this.txtAuditMain.Text = dataTable.Rows[0]["AuditMain"].ToString();
				this.hfldDep.Value = dataTable.Rows[0]["DepCode"].ToString();
				this.txtDep.Text = NodeEdit.getDeptName(dataTable.Rows[0]["DepCode"].ToString());
			}
		}
		private void BtnAdd_Click(object sender, System.EventArgs e)
		{
			string text = StringHelper.FilterSpecial(this.txtNodeName.Text.Trim());
			string arg_21_0 = this.hdnOperater.Value;
			string pStr = "";
			if (this.rbSingle.Checked)
			{
				pStr = "1";
			}
			if (this.rbMilti.Checked)
			{
				pStr = "2";
			}
			if (this.rbGroup.Checked)
			{
				pStr = "3";
			}
			if (this.rbItem.Checked)
			{
				pStr = "4";
			}
			if (this.rbDept.Checked)
			{
				pStr = "5";
			}
			string pStr2 = "1";
			string pStr3;
			if (this.rbIsAllPass1.Checked)
			{
				pStr3 = "1";
			}
			else
			{
				pStr3 = "0";
			}
			string pStr4;
			if (this.CkbIsSec.Checked)
			{
				pStr4 = "1";
				userManageDb userManageDb = new userManageDb();
				string[] array = this.hdnOperater.Value.Split(new char[44]);
				if (array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						DataTable auditPwd = userManageDb.GetAuditPwd(array[i]);
						if (auditPwd.Rows.Count > 0)
						{
							auditPwd.Rows[0]["AuditPwd"].ToString();
							if (string.IsNullOrEmpty(auditPwd.Rows[0]["AuditPwd"].ToString()))
							{
								userManageDb.updateUserAuditPwd(array[i], "easy");
							}
						}
					}
				}
			}
			else
			{
				pStr4 = "0";
			}
			string pStr5;
			if (this.CkbIsSelReceiver.Checked)
			{
				pStr5 = "1";
			}
			else
			{
				pStr5 = "0";
			}
			string text2 = this.txtRemark.Text;
			string value = this.hdnFrontNode.Value;
			System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
			hashtable.Add("TemplateID", this.TemplateID.ToString());
			hashtable.Add("NodeName", SqlStringConstructor.GetQuotedString(text));
			hashtable.Add("Operater", SqlStringConstructor.GetQuotedString(this.hdnOperater.Value));
			hashtable.Add("AuditorType", SqlStringConstructor.GetQuotedString(pStr));
			hashtable.Add("IsSendMsg", SqlStringConstructor.GetQuotedString(pStr2));
			hashtable.Add("IsAllPass", SqlStringConstructor.GetQuotedString(pStr3));
			hashtable.Add("ConditionDescription", SqlStringConstructor.GetQuotedString(text2));
			hashtable.Add("During", this.txtDuring.Text.Trim());
			hashtable.Add("FrontNode", SqlStringConstructor.GetQuotedString(value));
			hashtable.Add("IsSecValidate", SqlStringConstructor.GetQuotedString(pStr4));
			hashtable.Add("IsSelReceiver", SqlStringConstructor.GetQuotedString(pStr5));
			hashtable.Add("AuditMain", SqlStringConstructor.GetQuotedString(this.txtAuditMain.Text));
			hashtable.Add("DueMode", SqlStringConstructor.GetQuotedString(this.RbDueMode.SelectedValue));
			hashtable.Add("DepCode", SqlStringConstructor.GetQuotedString(this.hfldDep.Value));
			hashtable.Add("Id", SqlStringConstructor.GetQuotedString(System.Guid.NewGuid().ToString()));
			string pos = this.hdnPos.Value.ToString();
			if (!this.CkbIsSelReceiver.Checked)
			{
				if (text != "" && this.hdnOperater.Value.ToString().Trim() != "" && this.txtDuring.Text.ToString().Trim() != "")
				{
					if (this.NodeID != 0)
					{
						string where = " Where NodeID = " + this.NodeID.ToString();
						if (FlowTemplateAction.UpdNode(hashtable, where) && FlowChartAction.UpdateFlowChart(this.xpos, this.ypos, pos, this.FrontNodeOld, value, this.NodeID, this.TemplateID, text, text2, this.types))
						{
							base.RegisterScript("top.ui.winSuccess({ parentName: '_flowchart' });");
							return;
						}
					}
					else
					{
						object obj = FlowTemplateAction.AddNode(hashtable);
						if (obj != null)
						{
							int num = System.Convert.ToInt32(obj);
							string sqlString = string.Concat(new object[]
							{
								"SELECT * FROM WF_TemplateNode WHERE FrontNode='",
								value,
								"' AND TemplateID=",
								this.TemplateID,
								" AND NodeID !=",
								num,
								" "
							});
							DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
							if (dataTable.Rows.Count > 0)
							{
								string sqlString2 = string.Concat(new object[]
								{
									" UPDATE WF_TemplateNode SET FrontNode=",
									num,
									" WHERE FrontNode='",
									value,
									"' AND TemplateID=",
									this.TemplateID,
									" AND NodeID !=",
									num,
									" "
								});
								bool flag = publicDbOpClass.NonQuerySqlString(sqlString2);
								if (flag && FlowChartAction.CreateNewFlowChart(this.xpos, this.ypos, this.FrontNode.ToString(), num, this.TemplateID, text, this.types))
								{
									base.RegisterScript("top.ui.winSuccess({ parentName: '_flowchart' });");
									return;
								}
							}
							else
							{
								if (FlowChartAction.CreateNewFlowChart(this.xpos, this.ypos, this.FrontNode.ToString(), num, this.TemplateID, text, this.types))
								{
									base.RegisterScript("top.ui.winSuccess({ parentName: '_flowchart' });");
									return;
								}
							}
						}
					}
				}
			}
			else
			{
				if (text != "" && this.txtDuring.Text.ToString().Trim() != "")
				{
					if (this.NodeID != 0)
					{
						string where2 = " Where NodeID = " + this.NodeID.ToString();
						if (FlowTemplateAction.UpdNode(hashtable, where2) && FlowChartAction.UpdateFlowChart(this.xpos, this.ypos, pos, this.FrontNodeOld, value, this.NodeID, this.TemplateID, text, text2, this.types))
						{
							base.RegisterScript("top.ui.winSuccess({ parentName: '_flowchart' });");
							return;
						}
					}
					else
					{
						object obj2 = FlowTemplateAction.AddNode(hashtable);
						if (obj2 != null)
						{
							int num2 = System.Convert.ToInt32(obj2);
							value.IndexOf(",");
							string sqlString3 = string.Concat(new object[]
							{
								"SELECT * FROM WF_TemplateNode WHERE FrontNode='",
								value,
								"' AND TemplateID=",
								this.TemplateID,
								" AND NodeID !=",
								num2,
								" "
							});
							DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString3);
							if (dataTable2.Rows.Count > 0)
							{
								string sqlString4 = string.Concat(new object[]
								{
									" UPDATE WF_TemplateNode SET FrontNode=",
									num2,
									" WHERE FrontNode='",
									value,
									"' AND TemplateID=",
									this.TemplateID,
									" AND NodeID !=",
									num2,
									" "
								});
								bool flag2 = publicDbOpClass.NonQuerySqlString(sqlString4);
								if (flag2 && FlowChartAction.CreateNewFlowChart(this.xpos, this.ypos, this.FrontNode.ToString(), num2, this.TemplateID, text, this.types))
								{
									base.RegisterScript("top.ui.winSuccess({ parentName: '_flowchart' });");
									return;
								}
							}
							else
							{
								if (FlowChartAction.CreateNewFlowChart(this.xpos, this.ypos, this.FrontNode.ToString(), num2, this.TemplateID, text, this.types))
								{
									base.RegisterScript("top.ui.winSuccess({ parentName: '_flowchart' });");
								}
							}
						}
					}
				}
			}
		}
		private void SetFrontNode(int templateId, string frontNode)
		{
			string text = "";
			int i = frontNode.IndexOf(",");
			if (i > 0)
			{
				string value;
				object value2;
				while (i > 0)
				{
					value = frontNode.Substring(0, i);
					value2 = FlowTemplateAction.QueryNodeName(System.Convert.ToInt32(value));
					text = text + System.Convert.ToString(value2) + ",";
					frontNode = frontNode.Substring(i + 1, frontNode.Length - i - 1);
					i = frontNode.IndexOf(",");
				}
				value = frontNode;
				value2 = FlowTemplateAction.QueryNodeName(System.Convert.ToInt32(value));
				text += System.Convert.ToString(value2);
			}
			else
			{
				if (frontNode == "0")
				{
					text = "开始";
				}
				else
				{
					string value = frontNode;
					object value2 = FlowTemplateAction.QueryNodeName(System.Convert.ToInt32(value));
					text = System.Convert.ToString(value2);
				}
			}
			this.txtFrontNode.Text = text;
		}
		protected static string getDeptName(string codes)
		{
			string text = string.Empty;
			string[] array = codes.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string deptCode = array2[i];
				text = text + NodeEdit.getDept(deptCode) + ",";
			}
			return text.Remove(text.Length - 1);
		}
		protected static string getDept(string deptCode)
		{
			string sqlString = "select V_BMMC from pt_d_bm where v_bmbm = '" + deptCode.ToString() + "' and c_sfyx='y'";
			object value = publicDbOpClass.ExecuteScalar(sqlString);
			return System.Convert.ToString(value);
		}
	}


