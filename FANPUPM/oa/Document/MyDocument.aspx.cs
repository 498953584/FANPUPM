using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class MyDocument : BasePage, IRequiresSessionState
	{
		public string CorpCode
		{
			get
			{
				object obj = this.ViewState["CorpCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["CorpCode"] = value;
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

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UserCode = Convert.ToString(this.Session["yhdm"]);
				DataTable dataTable = DocumentAction.QueryCorpCode(this.UserCode);
				if (dataTable.Rows.Count > 0)
				{
					this.CorpCode = dataTable.Rows[0]["CorpCode"].ToString();
				}
				else
				{
					this.CorpCode = "";
				}
				this.btnAdd.Attributes["onclick"] = string.Concat(new string[]
				{
					"openMyDocEdit(1,'",
					this.CorpCode,
					"','",
					this.UserCode,
					"')"
				});
				this.btnEdit.Attributes["onclick"] = string.Concat(new string[]
				{
					"openMyDocEdit(2,'",
					this.CorpCode,
					"','",
					this.UserCode,
					"')"
				});
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.btnStartWF.Attributes["onclick"] = " return confirm('确定提交审核吗？');";
				this.btnViewWF.Attributes["onclick"] = "return OpenViewWF();";
				this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF();";
				this.BtnView.Attributes["onclick"] = "OpenLock();";
				this.dgFileList_Bind();
			}
		}
		protected void dgFileList_Bind()
		{
			this.dgFileList.DataSource = DocumentAction.QuerySendFileList(this.CorpCode, 0, this.UserCode);
			this.dgFileList.DataBind();
		}
		protected void dgFileList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);doClickRow('",
					dataRowView["FileID"].ToString(),
					"','",
					dataRowView["AuditState"].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseout"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				return;
			}
			default:
				return;
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.dgFileList_Bind();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.dgFileList_Bind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			Guid fileId = new Guid(this.hdnFileID.Value);
			if (DocumentAction.DelSendFileInfo(fileId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.dgFileList_Bind();
			}
		}
		protected void btnStartWF_Click(object sender, EventArgs e)
		{
			Guid recordID = new Guid(this.hdnFileID.Value);
			string text = FlowAuditAction.BeginFlow("001", "001", recordID, "", this.UserCode);
			if (text == "工作流程已成功启动")
			{
				this.JS.Text = "alert('" + text + "!')";
				this.dgFileList_Bind();
				return;
			}
			if (text == "请先设置当前模块的审核流程")
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
				return;
			}
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){window.location.href=window.location.href};", true);
		}
		protected void btnRefresh_Click(object sender, EventArgs e)
		{
			this.dgFileList_Bind();
		}
	}


