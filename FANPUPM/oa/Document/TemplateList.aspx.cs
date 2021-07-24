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
	public partial class TemplateList : BasePage, IRequiresSessionState
	{
		public int ClassID
		{
			get
			{
				object obj = this.ViewState["ClassID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["ClassID"] = value;
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
				this.ClassID = Convert.ToInt32(base.Request["cid"]);
				this.UserCode = Convert.ToString(this.Session["yhdm"]);
				this.btnAdd.Attributes["onclick"] = string.Concat(new object[]
				{
					"openClassEdit(1,'",
					this.ClassID,
					"','",
					this.UserCode,
					"')"
				});
				this.btnEdit.Attributes["onclick"] = string.Concat(new object[]
				{
					"openClassEdit(2,'",
					this.ClassID,
					"','",
					this.UserCode,
					"')"
				});
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.dgTemplate_Bind();
			}
		}
		protected void dgTemplate_Bind()
		{
			this.dgTemplate.DataSource = DocumentAction.QueryDocTemplateList(this.ClassID);
			this.dgTemplate.DataBind();
		}
		protected void dgTemplate_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "OnRecord(this);doClickRow('" + dataRowView["TempletID"].ToString() + "');";
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
			this.dgTemplate_Bind();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.dgTemplate_Bind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			int templateId = Convert.ToInt32(this.hdnTemplateID.Value);
			if (DocumentAction.DelDocTemplate(templateId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.dgTemplate_Bind();
			}
		}
		protected void dgTemplate_SelectedIndexChanged(object sender, EventArgs e)
		{
			string str = "";
			string str2 = "";
			int templateId = Convert.ToInt32(this.dgTemplate.DataKeys[this.dgTemplate.SelectedIndex].ToString());
			DataTable dataTable = DocumentAction.QueryOneDocTemplate(templateId);
			if (dataTable.Rows.Count == 1)
			{
				str = dataTable.Rows[0]["OriginalName"].ToString();
				str2 = dataTable.Rows[0]["FilePath"].ToString();
			}
			base.Response.Redirect("/EPC/uploadfile/down.aspx?fileName=" + str + "&filePath=" + str2);
		}
	}


