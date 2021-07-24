using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
	public partial class dbrInfo : NBasePage, IRequiresSessionState
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
		public int TempNodeId
		{
			get
			{
				object obj = this.ViewState["TempNodeId"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TempNodeId"] = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.AspNetPager1.PageSize = 9;
				this.TemplateID = System.Convert.ToInt32(base.Request["tid"]);
				this.UserCode = base.Request["code"];
				this.TempNodeId = System.Convert.ToInt32(base.Request["nodeid"]);
				this.btnAdd.Attributes["onclick"] = string.Concat(new object[]
				{
					"openDbrEdit(1,'",
					this.TemplateID,
					"','",
					this.UserCode,
					"','",
					this.TempNodeId,
					"')"
				});
				this.btnEdit.Attributes["onclick"] = string.Concat(new object[]
				{
					"openDbrEdit(2,'",
					this.TemplateID,
					"','",
					this.UserCode,
					"','",
					this.TempNodeId,
					"')"
				});
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.btnonUse.Attributes["onclick"] = " return confirm('确定启动当前人员为代办人？');";
				this.dgFlow_Bind();
			}
		}
		public bool GetState(string state)
		{
			bool result;
			try
			{
				result = System.Convert.ToBoolean(state);
			}
			catch
			{
				result = false;
			}
			return result;
		}
		private void dgFlow_Bind()
		{
			DataTable dataSource = FlowTemplateAction.QueryAgent(this.TemplateID, this.UserCode, 9, this.AspNetPager1.CurrentPageIndex);
			this.AspNetPager1.RecordCount = FlowTemplateAction.GetQueryAgentCount(this.TemplateID, this.UserCode);
			this.dgFlow.DataSource = dataSource;
			this.dgFlow.DataBind();
		}
		protected void dgFlow_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				e.Row.Attributes["id"] = System.Convert.ToString(e.Row.RowIndex + 1);
				e.Row.Attributes["onclick"] = string.Concat(new object[]
				{
					"OnRecord(this);doClickRow('",
					dataRowView["NodeId"].ToString(),
					"','",
					dataRowView["bt_use"],
					"');"
				});
				e.Row.Attributes["onmouseout"] = "OnMouseOverRecord(this);";
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			}
		}
		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.dgFlow_Bind();
		}
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			this.dgFlow_Bind();
		}
		protected void btnDel_Click(object sender, System.EventArgs e)
		{
			int nodeId = System.Convert.ToInt32(this.hdnNodeId.Value);
			if (FlowTemplateAction.DelAgent(nodeId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.dgFlow_Bind();
			}
		}
		protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
		{
			this.dgFlow_Bind();
		}
		protected void btonUse_Click(object sender, System.EventArgs e)
		{
			string text = string.Concat(new object[]
			{
				"update wf_agent set bt_Use=1 where TemplateId=",
				this.TemplateID,
				" and nodeid=",
				this.hdnNodeId.Value
			});
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				" update wf_agent set bt_use=0 where templateid=",
				this.TemplateID,
				" and nodeid<>",
				this.hdnNodeId.Value,
				"and TempNodeId=",
				this.TempNodeId
			});
			int num = publicDbOpClass.ExecSqlString(text);
			if (num > 0)
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('启动成功');", true);
				this.dgFlow_Bind();
			}
		}
	}


