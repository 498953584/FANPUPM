using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ShowInfomation : BasePage, IRequiresSessionState
	{
		private int _iReleaseDept;
		public string strSkinPath = "";
		private RecieveMsgAction rma = new RecieveMsgAction();

		public OAWFApplyItemAction hrAction
		{
			get
			{
				return new OAWFApplyItemAction();
			}
		}
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Cache.SetNoStore();
            this.strSkinPath = "skin" + this.Session["SkinID"].ToString();
            if (!this.Page.IsPostBack)
            {
                this.DealOutTimeAudit();
                if (userManageDb.GetCurrentUserInfo() != null)
                {
                    string userCode = userManageDb.GetCurrentUserInfo().UserCode;
                }
                else
                {
                    base.Response.Write("用户身份过期，请重新登录！");
                    return;
                }
                UserInfo currentUserInfo = userManageDb.GetCurrentUserInfo();
                if (currentUserInfo != null)
                {
                    this._iReleaseDept = new userManageDb().GetUserDept(currentUserInfo.UserCode);
                    this.Bulletin_Bind(this._iReleaseDept);
                }
                else
                {
                    this.Page.RegisterStartupScript("err", "<script language='javascript'>alert(\"用户身份已过期，请重新登录！\");parent.window.close();</script>");
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
		private void Bulletin_Bind(int iReleaseDept)
		{
			new BulletinManage();
			DataTable todayBulletin = this.GetTodayBulletin(iReleaseDept);
			this.GVbulletin.DataSource = todayBulletin;
			this.GVbulletin.DataBind();
		}
		public DataTable GetTodayBulletin(int iReleaseDept)
		{
			string sqlString;
			if (base.UserCode != "00000000")
			{
				string str = BulletionAction.getv_bmbm(base.UserCode);
				sqlString = "SELECT top 6 * FROM [v_bulletin_list] WHERE dtm_expriesdate>getdate()-1 and AuditState=1 and (([CorpCode] = left('" + str + "',len([CorpCode]))) or ([CorpCode]='00')) order by  DTM_RELEASETIME desc";
			}
			else
			{
				sqlString = "SELECT top 6 * FROM [v_bulletin_list] where dtm_expriesdate>getdate()-1 and auditstate=1 order by DTM_RELEASETIME desc";
			}
			return publicDbOpClass.DataTableQuary(sqlString);
		}
		protected void GVNews_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowIndex > -1)
			{
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				HyperLink hyperLink = (HyperLink)e.Row.Cells[1].Controls[0];
				if (hyperLink.Text.Length > 18)
				{
					hyperLink.Text = hyperLink.Text.Substring(0, 17) + "...&nbsp;";
				}
				hyperLink.Attributes["title"] = dataRowView["InsName"].ToString();
				hyperLink.Attributes["class"] = "firstpage";
				hyperLink.Style["cursor"] = "hand";
				string text = "/oa/System/Institution/InstitutionView.aspx?ic=" + dataRowView["insCode"].ToString();
				hyperLink.Attributes.Add("para", text);
				hyperLink.Attributes["onclick"] = "javascript:parent.desktop.flowclass = window;toolbox_oncommand('" + text + "', '公司制度');";
			}
		}
		protected void GVDBSJ_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowIndex > -1)
			{
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				HyperLink hyperLink = (HyperLink)e.Row.Cells[1].Controls[0];
				hyperLink.Attributes["class"] = "firstpage";
				hyperLink.Style["cursor"] = "hand";
				hyperLink.Text = ShowInfomation.htmlInputText(dataRowView["V_Content"].ToString()).ToString();
				try
				{
					string[] array = ShowInfomation.htmlInputText(dataRowView["V_Content"].ToString()).Split(new char[]
					{
						'-'
					});
					array[1].ToString();
				}
				catch
				{
				}
				if (ShowInfomation.htmlInputText(hyperLink.Text).Length > 17)
				{
					hyperLink.Text = ShowInfomation.htmlInputText(hyperLink.Text).Substring(0, 17) + "...&nbsp&nbsp";
				}
				else
				{
					hyperLink.Text = ShowInfomation.htmlInputText(hyperLink.Text) + "...&nbsp&nbsp";
				}
				hyperLink.ToolTip = dataRowView["V_Content"].ToString();
				string str = "/" + dataRowView["V_DBLJ"].ToString().Replace("ModuleSet", "Epc");
				hyperLink.Attributes["onclick"] = "javascript:window.open('" + str + "','_blank','left=150,top=150,width=600,height=400,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1',false);";
			}
		}
		protected void gvAuditingList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1) + "、";
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				string text = dataRowView["InstanceCode"].ToString();
				string text2 = dataRowView["NoteID"].ToString();
				string text3 = dataRowView["IsAllPass"].ToString();
				string text4 = dataRowView["NodeID"].ToString();
				string text5 = dataRowView["BusinessCode"].ToString();
				string text6 = dataRowView["BusinessClass"].ToString();
				Convert.ToInt32(dataRowView["During"]);
				decimal d = 0m;
				try
				{
					d = Convert.ToDecimal(dataRowView["cs"]);
				}
				catch
				{
				}
				LinkButton linkButton = (LinkButton)e.Row.Cells[1].Controls[0];
				if (dataRowView["BusinessCode"].ToString() == "999")
				{
					OAWFApplyItem model = this.hrAction.GetModel((Guid)dataRowView["InstanceCode"]);
					if (model != null)
					{
						if (model.Title.Length > 12)
						{
							linkButton.Text = model.Title.Substring(0, 11) + "..&nbsp;";
							linkButton.Attributes["title"] = model.Title;
						}
						else
						{
							linkButton.Text = model.Title + "&nbsp;";
							linkButton.Attributes["title"] = model.Title;
						}
					}
				}
				linkButton.Attributes["class"] = "firstpage";
				linkButton.Style["cursor"] = "hand";
				if (d > 0m)
				{
					string str = "超时";
					linkButton.Text = "[<font color=\"red\">" + str + "</font>]" + linkButton.Text;
				}
				string text7 = string.Concat(new string[]
				{
					"../EPC/Workflow/AuditFrame.aspx?ic=",
					text,
					"&id=",
					text2,
					"&pass=",
					text3,
					"&nid=",
					text4,
					"&bc=",
					text5,
					"&bcl=",
					text6
				});
				linkButton.Attributes.Add("para", text7);
				linkButton.Attributes["onclick"] = "javascript:parent.desktop.flowclass = window;toolbox_oncommand('" + text7 + "', '流程审核');";
			}
		}
		private string GetPageDuringInfo(int during, int InstanceID, Guid InstanceCode)
		{
			string result = "";
			DataTable lastAuditUser = FlowAuditAction.GetLastAuditUser(InstanceID);
			if (lastAuditUser.Rows.Count > 0)
			{
				DateTime value = Convert.ToDateTime(lastAuditUser.Rows[0]["AuditDate"]);
				TimeSpan timeSpan = DateTime.Now.Subtract(value);
				int num = timeSpan.Days * 24 + timeSpan.Hours - 4;
				if (num > 0)
				{
					result = "超时";
				}
				else
				{
					result = "";
				}
			}
			else
			{
				string startDateTime = FlowAuditAction.GetStartDateTime(InstanceCode);
				if (startDateTime != "")
				{
					DateTime value = Convert.ToDateTime(startDateTime);
					TimeSpan timeSpan2 = DateTime.Now.Subtract(value);
					int num2 = timeSpan2.Days * 24 + timeSpan2.Hours - 4;
					if (num2 > 0)
					{
						result = "超时";
					}
					else
					{
						result = "";
					}
				}
			}
			return result;
		}
		protected void DealOutTimeAudit()
		{
			DataTable dataTable = FlowAuditAction.OutTimeAudit();
			if (dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					string dueMode = FlowAuditAction.GetDueMode(Convert.ToInt32(Convert.ToInt32(dataTable.Rows[i]["NodeId"]).ToString()));
					if (dueMode == "0")
					{
						FlowAuditAction.ProcessFlow(Convert.ToInt32(dataTable.Rows[i]["NoteId"].ToString()), dataTable.Rows[i]["IsAllPass"].ToString(), dataTable.Rows[i]["Operator"].ToString(), false, "超时驳回");
					}
					else
					{
						if (dueMode == "1")
						{
							FlowAuditAction.ProcessFlow(Convert.ToInt32(dataTable.Rows[i]["NoteId"].ToString()), dataTable.Rows[i]["IsAllPass"].ToString(), dataTable.Rows[i]["Operator"].ToString(), true, "超时通过");
						}
					}
				}
			}
		}
		protected void GVbulletin_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				LinkButton linkButton = (LinkButton)e.Row.Cells[1].Controls[0];
				linkButton.Attributes["class"] = "firstpage";
				linkButton.Style["cursor"] = "hand";
				linkButton.Attributes["title"] = linkButton.Text;
				linkButton.Font.Size = new FontUnit("9pt");
				string text = linkButton.Text;
				if (linkButton.Text.Length > 17)
				{
					text = linkButton.Text.Substring(0, 16) + "...&nbsp";
				}
				else
				{
					text = linkButton.Text;
				}
				if (e.Row.RowIndex < 3)
				{
					linkButton.Text = text;
				}
				else
				{
					linkButton.Text = text;
				}
				string str = dataRowView["I_BULLETINID"].ToString();
				string str2 = dataRowView["V_TITLE"].ToString();
				string str3 = "../oa/bulletin/BulletinLock.aspx?ic=" + str + "&mc= " + str2;
				linkButton.Attributes["onclick"] = "window.open('" + str3 + "','_blank','left=100,top=50,width=700,height=500,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1',false);";
			}
		}
		public static string htmlInputText(string inputString)
		{
			if (inputString != null && inputString != string.Empty)
			{
				inputString = inputString.Trim();
				inputString = inputString.Replace("'", "&quot;");
				inputString = inputString.Replace("<", "&lt;");
				inputString = inputString.Replace(">", "&gt;");
				inputString = inputString.Replace(" ", "&nbsp;");
				inputString = inputString.Replace("\n", "<br>");
				return inputString.ToString();
			}
			return "";
		}
		public static string htmlOutputText(string inputString)
		{
			if (inputString != null && inputString != string.Empty)
			{
				inputString = inputString.Trim();
				inputString = inputString.Replace("&quot;", "'");
				inputString = inputString.Replace("&lt;", "<");
				inputString = inputString.Replace("&gt;", ">");
				inputString = inputString.Replace("&nbsp;", " ");
				inputString = inputString.Replace("<br>", "\n");
				return inputString.ToString();
			}
			return "";
		}
	}


