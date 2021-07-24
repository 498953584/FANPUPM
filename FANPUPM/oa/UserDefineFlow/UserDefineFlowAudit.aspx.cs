using ASP;
using cn.justwin.BLL;
using cn.justwin.Web;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class UserDefineFlowAudit : NBasePage, IRequiresSessionState
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
		public string NodeID
		{
			get
			{
				object obj = this.ViewState["NodeID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["NodeID"] = value;
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.InstanceCode = new System.Guid(base.Request["ic"]);
				this.InstanceID = System.Convert.ToInt32(base.Request["id"]);
				this.IsAllPass = base.Request["pass"];
				this.NodeID = base.Request["nid"];
				this.UserCode = System.Convert.ToString(this.Session["yhdm"]);
				this.lblBllProducer.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.UserCode);
				this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
				this.HdnRecordCode.Value = this.InstanceCode.ToString();
				this.setDate();
				this.Literal1.Text = this.FilesBind(this.InstanceCode.ToString(), "fujian");
				DataTable dataTable = publicDbOpClass.DataTableQuary("select * ,(select v_xm from pt_yhmc where v_yhdm = Operator) as auditor,(case when AuditResult='1' then '通过' when AuditResult='-2' then '驳回' when AuditResult='-3' then '重报' else '' end) as Result from WF_Instance where ID  IN (select ID from WF_Instance_Main where InstanceCode='" + this.InstanceCode.ToString() + "')");
				if (dataTable.Rows.Count == 0)
				{
					this.table.Visible = false;
					this.tableHeader.Visible = false;
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					HtmlTableRow htmlTableRow = new HtmlTableRow();
					htmlTableRow.Attributes["class"] = "rowa";
					htmlTableRow.Attributes["style"] = "text-align:center;";
					HtmlTableCell htmlTableCell = new HtmlTableCell();
					htmlTableCell.InnerText = (i + 1).ToString();
					htmlTableCell.RowSpan = 3;
					htmlTableRow.Cells.Add(htmlTableCell);
					HtmlTableCell htmlTableCell2 = new HtmlTableCell();
					htmlTableCell2.InnerText = dataTable.Rows[i]["NodeName"].ToString();
					htmlTableRow.Cells.Add(htmlTableCell2);
					HtmlTableCell htmlTableCell3 = new HtmlTableCell();
					htmlTableCell3.InnerText = dataTable.Rows[i]["auditor"].ToString();
					htmlTableRow.Cells.Add(htmlTableCell3);
					HtmlTableCell htmlTableCell4 = new HtmlTableCell();
					htmlTableCell4.InnerText = dataTable.Rows[i]["AuditDate"].ToString();
					htmlTableRow.Cells.Add(htmlTableCell4);
					HtmlTableCell htmlTableCell5 = new HtmlTableCell();
					htmlTableCell5.InnerText = dataTable.Rows[i]["Result"].ToString();
					htmlTableRow.Cells.Add(htmlTableCell5);
					this.table.Rows.Add(htmlTableRow);
					HtmlTableRow htmlTableRow2 = new HtmlTableRow();
					HtmlTableCell htmlTableCell6 = new HtmlTableCell();
					htmlTableCell6.InnerHtml = "<b >审核意见:</b>";
					htmlTableCell6.InnerText = "审核意见：";
					htmlTableCell6.Align = "right";
					htmlTableCell6.Attributes["style"] = " white-space:nowrap;";
					htmlTableRow2.Cells.Add(htmlTableCell6);
					HtmlTableCell htmlTableCell7 = new HtmlTableCell();
					htmlTableCell7.InnerHtml = "<div style=\"width: 95%; word-break: break-all;\">";
					HtmlTableCell expr_314 = htmlTableCell7;
					expr_314.InnerHtml = expr_314.InnerHtml + "<font size=\"4pt\">" + dataTable.Rows[i]["AuditInfo"].ToString() + "</font>";
					HtmlTableCell expr_34B = htmlTableCell7;
					expr_34B.InnerHtml += "</div>";
					htmlTableCell7.ColSpan = 4;
					htmlTableRow2.Cells.Add(htmlTableCell7);
					this.table.Rows.Add(htmlTableRow2);
					HtmlTableRow htmlTableRow3 = new HtmlTableRow();
					HtmlTableCell htmlTableCell8 = new HtmlTableCell();
					htmlTableCell8.InnerHtml = "<b >相关附件:</b>";
					htmlTableCell8.InnerText = "相关附件：";
					htmlTableCell8.Align = "right";
					htmlTableCell8.Attributes["style"] = " white-space:nowrap;";
					htmlTableRow3.Cells.Add(htmlTableCell8);
					HtmlTableCell htmlTableCell9 = new HtmlTableCell();
					htmlTableCell9.InnerHtml = this.FilesBind(this.InstanceCode.ToString(), dataTable.Rows[i]["NoteID"].ToString());
					htmlTableCell9.ColSpan = 4;
					htmlTableRow3.Cells.Add(htmlTableCell9);
					this.table.Rows.Add(htmlTableRow3);
				}
			}
		}
		public string FilesBind(string recordCode, string noteid)
		{
			string text = ConfigHelper.Get("Audit");
			string result;
			try
			{
				string[] files = System.IO.Directory.GetFiles(string.Concat(new string[]
				{
					base.Server.MapPath(text),
					"\\",
					recordCode,
					"\\",
					noteid
				}));
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				string[] array = files;
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i];
					string text3 = string.Empty;
					text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
					string str = string.Concat(new string[]
					{
						text,
						"/",
						recordCode,
						"/",
						noteid
					});
					string str2 = str + "/" + text3;
					text3 = string.Concat(new string[]
					{
						"<a  class=\"link\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
						HttpUtility.UrlEncode(str2),
						"\"  >",
						text3,
						"</a>"
					});
					stringBuilder.Append(text3);
					stringBuilder.Append(", ");
				}
				string text4 = string.Empty;
				if (stringBuilder.Length > 2)
				{
					text4 = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
				}
				result = text4;
			}
			catch
			{
				result = "";
			}
			return result;
		}
		protected void setDate()
		{
			string strWhere = " RecordID = '" + this.InstanceCode.ToString() + "'";
			OAWFApplyItemAction oAWFApplyItemAction = new OAWFApplyItemAction();
			DataTable list = oAWFApplyItemAction.GetList(strWhere);
			userManageDb userManageDb = new userManageDb();
			if (list.Rows.Count == 1)
			{
				this.txtTitle.Text = list.Rows[0]["Title"].ToString();
				this.txtContent.Text = StringUtility.ReplaceTxt(list.Rows[0]["Remark"].ToString());
				this.LbMan.Text = userManageDb.GetUserName(list.Rows[0]["UserCode"].ToString());
			}
		}
		protected void getParm()
		{
			string sqlString = " select b.*,a.InstanceCode from wf_instance_Main a ,wf_Message b where  a.id = b.instanceMainID and charindex('" + this.UserCode + "',b.MsgRecievers) > 0";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count == 1)
			{
				this.InstanceCode = new System.Guid(dataTable.Rows[0]["InstanceCode"].ToString());
				this.InstanceID = System.Convert.ToInt32(dataTable.Rows[0]["InstanceID"].ToString());
				this.IsAllPass = dataTable.Rows[0]["IsAllPass"].ToString();
				this.NodeID = dataTable.Rows[0]["NodeID"].ToString();
			}
		}
	}


