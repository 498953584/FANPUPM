using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Web;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class FlowChart : NBasePage, IRequiresSessionState
	{
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
		public string businessCode
		{
			get
			{
				object obj = this.ViewState["businessCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "0";
			}
			set
			{
				this.ViewState["businessCode"] = value;
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
		public string IsGeneral
		{
			get
			{
				object obj = this.ViewState["IsGeneral"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["IsGeneral"] = value;
			}
		}

		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new System.EventHandler(this.Page_Load);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.TemplateID = System.Convert.ToInt32(base.Request["tid"]);
				this.businessCode = base.Request["bncode"];
				this.BusinessClass = base.Request["class"];
				this.IsGeneral = base.Request.QueryString["flag"];
				this.hdnTemplateID.Value = this.TemplateID.ToString();
				string str = "WorkFlowChart.aspx?tid=" + this.TemplateID.ToString();
				this.JS.Text = "this.fmNavigation.location.href='" + str + "';";
				this.ImgSingle.Attributes["onclick"] = "openRoleEdit(1,1)";
				this.ImgMulti.Attributes["onclick"] = "openRoleEdit(1,2)";
				this.ImgGroup.Attributes["onclick"] = "openRoleEdit(1,3)";
				this.ImgRole.Attributes["onclick"] = "openRoleEdit(1,4)";
				this.ImgCorp.Attributes["onclick"] = "openRoleEdit(1,5)";
				this.ImgEdit.Attributes["onclick"] = "openRoleEdit(2,0)";
				this.ImgDelete.Attributes["onclick"] = "return delnode();";
				bool flag = FlowTemplateAction.IsProjectRelated(this.TemplateID);
				this.hfldIsProjectRelated.Value = (flag ? "1" : "0");
				if (ConfigHelper.ProjectType == "ZdContract")
				{
					this.td_project.Visible = false;
					this.td_group.Visible = false;
					this.td_corp.Visible = false;
				}
			}
		}
		protected void ImgSingle_Click(object sender, ImageClickEventArgs e)
		{
			string str = string.Concat(new string[]
			{
				"TemplateList.aspx?ti=",
				this.businessCode.ToString(),
				"&class=",
				this.BusinessClass.ToString(),
				"&flag=",
				this.IsGeneral.ToString()
			});
			this.JS.Text = "parent.fraTemplate.location.href= '" + str + "';";
			this.fmNavigation.Attributes["src"] = "WorkFlowChart.aspx?tid=" + this.TemplateID.ToString();
		}
		protected void ImgMulti_Click(object sender, ImageClickEventArgs e)
		{
			string str = string.Concat(new string[]
			{
				"TemplateList.aspx?ti=",
				this.businessCode.ToString(),
				"&class=",
				this.BusinessClass.ToString(),
				"&flag=",
				this.IsGeneral.ToString()
			});
			this.JS.Text = "parent.fraTemplate.location.href= '" + str + "';";
			this.fmNavigation.Attributes["src"] = "WorkFlowChart.aspx?tid=" + this.TemplateID.ToString();
		}
		protected void ImgGroup_Click(object sender, ImageClickEventArgs e)
		{
			string str = string.Concat(new string[]
			{
				"TemplateList.aspx?ti=",
				this.businessCode.ToString(),
				"&class=",
				this.BusinessClass.ToString(),
				"&flag=",
				this.IsGeneral.ToString()
			});
			this.JS.Text = "parent.fraTemplate.location.href= '" + str + "';";
			this.fmNavigation.Attributes["src"] = "WorkFlowChart.aspx?tid=" + this.TemplateID.ToString();
		}
		protected void ImgRole_Click(object sender, ImageClickEventArgs e)
		{
			string str = string.Concat(new string[]
			{
				"TemplateList.aspx?ti=",
				this.businessCode.ToString(),
				"&class=",
				this.BusinessClass.ToString(),
				"&flag=",
				this.IsGeneral.ToString()
			});
			this.JS.Text = "parent.fraTemplate.location.href= '" + str + "';";
			this.fmNavigation.Attributes["src"] = "WorkFlowChart.aspx?tid=" + this.TemplateID.ToString();
		}
		protected void ImgEdit_Click(object sender, ImageClickEventArgs e)
		{
			this.fmNavigation.Attributes["src"] = "WorkFlowChart.aspx?tid=" + this.TemplateID.ToString();
		}
		protected void ImgDelete_Click(object sender, ImageClickEventArgs e)
		{
			string text = this.hdnNodeID.Value.Trim();
			string text2 = this.hdnPos.Value.Trim();
			if (text != "" && text != null)
			{
				int nodeID = System.Convert.ToInt32(text);
				int num = text2.IndexOf(",");
				int xpos = System.Convert.ToInt32(text2.Substring(0, num));
				int ypos = System.Convert.ToInt32(text2.Substring(num + 1, text2.Length - num - 1));
				int posPositionNode = FlowChartAction.GetPosPositionNode(nodeID, this.TemplateID);
				if (posPositionNode > 1)
				{
					this.JS.Text = "alert('此节点不能删除！');";
				}
				else
				{
					if ((posPositionNode == 1 || posPositionNode == 0) && !FlowChartAction.DelNode(xpos, ypos, nodeID, this.TemplateID))
					{
						this.JS.Text = "alert('节点删除失败！');";
					}
				}
			}
			this.hdnNodeID.Value = "";
			string.Concat(new string[]
			{
				"TemplateList.aspx?ti=",
				this.businessCode.ToString(),
				"&class=",
				this.BusinessClass.ToString(),
				"&flg=",
				this.IsGeneral
			});
			this.fmNavigation.Attributes["src"] = "WorkFlowChart.aspx?tid=" + this.TemplateID.ToString();
		}
		protected void ImgComplete_Click(object sender, ImageClickEventArgs e)
		{
			if (!FlowChartAction.IsHaveNode(this.TemplateID))
			{
				string cmdText = "\r\n\t\t\t\t\tUPDATE WF_FlowChart SET Column2 = '7', Column3 = '2', Column4 = '4;00;结束'\r\n\t\t\t\t\tWHERE TemplateID = @TemplateID";
				string cmdText2 = "UPDATE WF_Template SET IsComplete = '1' WHERE TemplateID = @TemplateID";
				SqlParameter[] commandParameters = new SqlParameter[]
				{
					new SqlParameter("@TemplateID", this.TemplateID)
				};
				SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
				SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText2, commandParameters);
				this.fmNavigation.Attributes["src"] = "WorkFlowChart.aspx?tid=" + this.TemplateID.ToString();
				return;
			}
			object value = FlowTemplateAction.QueryTemplateState(this.TemplateID);
			if (System.Convert.ToString(value) == "1")
			{
				this.JS.Text = "alert('该流程模板已创建结束！')";
				return;
			}
			string text = "";
			string text2 = "";
			int nodeID = 0;
			bool flag = FlowChartAction.IsPoolNode(this.TemplateID);
			if (flag)
			{
				if (!FlowChartAction.FlowComplete(this.TemplateID))
				{
					this.JS.Text = "alert('创建工作流程失败！');";
				}
			}
			else
			{
				DataTable dataTable = FlowTemplateAction.QueryFlowChart(this.TemplateID);
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i += 2)
					{
						int j = dataTable.Columns.Count - 2;
						while (j > 2)
						{
							if (dataTable.Rows[i].ItemArray[j] != null && dataTable.Rows[i].ItemArray[j].ToString() != "")
							{
								int num = 0;
								string str = "";
								string text3 = dataTable.Rows[i].ItemArray[j].ToString();
								int k = text3.IndexOf(";");
								if (k > 0)
								{
									while (k > 0)
									{
										int num2 = num;
										if (num2 == 1)
										{
											text2 = text2 + text3.Substring(0, k) + ",";
											str = string.Concat(new string[]
											{
												(j - 1).ToString(),
												";",
												(i + 1).ToString(),
												";",
												text3.Substring(0, k)
											});
										}
										text3 = text3.Substring(k + 1, text3.Length - k - 1);
										k = text3.IndexOf(";");
										num++;
									}
									text = text + str + ",";
									break;
								}
								break;
							}
							else
							{
								j--;
							}
						}
					}
				}
				if (!FlowChartAction.CreatePoolNode(text, text2, nodeID, this.TemplateID, "结束", "", "New", "0"))
				{
					this.JS.Text = "alert('创建工作流程失败！');";
				}
			}
			System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
			hashtable.Add("IsComplete", SqlStringConstructor.GetQuotedString("1"));
			string where = "where TemplateID = " + this.TemplateID.ToString();
			if (!FlowTemplateAction.UpdTemplate(hashtable, where))
			{
				this.JS.Text = "alert('创建工作流程失败！');";
			}
			string str2 = string.Concat(new string[]
			{
				"TemplateList.aspx?ti=",
				this.businessCode.ToString(),
				"&class=",
				this.BusinessClass.ToString(),
				"&flag=",
				this.IsGeneral
			});
			this.JS.Text = "parent.fraTemplate.location.href= '" + str2 + "';";
		}
		protected void ImgCorp_Click(object sender, ImageClickEventArgs e)
		{
			string str = string.Concat(new string[]
			{
				"TemplateList.aspx?ti=",
				this.businessCode.ToString(),
				"&class=",
				this.BusinessClass.ToString(),
				"&flag=",
				this.IsGeneral.ToString()
			});
			this.JS.Text = "parent.fraTemplate.location.href= '" + str + "';";
			this.fmNavigation.Attributes["src"] = "WorkFlowChart.aspx?tid=" + this.TemplateID.ToString();
		}
	}


