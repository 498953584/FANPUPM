using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class dbrEdit : NBasePage, IRequiresSessionState
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
		public int TempNodeID
		{
			get
			{
				object obj = this.ViewState["TempNodeID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TempNodeID"] = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.TemplateID = System.Convert.ToInt32(base.Request["tid"]);
				this.NodeID = System.Convert.ToInt32(base.Request["nid"]);
				this.UserCode = base.Request["code"];
				if (base.Request["Tempnid"] != null)
				{
					this.TempNodeID = System.Convert.ToInt32(base.Request["Tempnid"]);
				}
				this.setData(this.TemplateID, this.UserCode);
			}
		}
		protected void BtnAdd_Click(object sender, System.EventArgs e)
		{
			System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
			hashtable.Add("TemplateID", this.TemplateID.ToString());
			hashtable.Add("MainUser", SqlStringConstructor.GetQuotedString(this.UserCode.ToString()));
			hashtable.Add("SecondUser", SqlStringConstructor.GetQuotedString(this.hdnDbr.Value));
			hashtable.Add("TempNodeId", this.TempNodeID.ToString());
			if (this.NodeID == 0)
			{
				if (FlowTemplateAction.AddAgent(hashtable))
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_dbrinfo'});");
					return;
				}
				base.RegisterScript("top.ui.alert('保存失败');");
				return;
			}
			else
			{
				string where = " where NodeId = '" + this.NodeID + " '";
				if (FlowTemplateAction.UpdAgent(hashtable, where))
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_dbrinfo'});");
					return;
				}
				base.RegisterScript("top.ui.alert('保存失败');");
				return;
			}
		}
		private void setData(int templateId, string userCode)
		{
			object value = FlowTemplateAction.QueryTemplateName(templateId);
			this.txtTemplateName.Text = System.Convert.ToString(value);
			value = FlowTemplateAction.QueryAuditor(userCode);
			this.txtAuditor.Text = System.Convert.ToString(value);
			if (this.NodeID != 0)
			{
				value = FlowTemplateAction.QueryOneAgent(this.NodeID);
				this.hdnDbr.Value = System.Convert.ToString(value);
				value = FlowTemplateAction.QueryAuditor(this.hdnDbr.Value);
				this.txtDbr.Text = System.Convert.ToString(value);
			}
		}
	}


