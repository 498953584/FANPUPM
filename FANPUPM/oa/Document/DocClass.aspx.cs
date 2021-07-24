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
	public partial class DocClass : BasePage, IRequiresSessionState
	{

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
		public string FilterField
		{
			get
			{
				object obj = this.ViewState["FilterField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["FilterField"] = value;
			}
		}
		public string ClassTypeCode
		{
			get
			{
				object obj = this.ViewState["ClassTypeCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ClassTypeCode"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UserCode = this.Session["yhdm"].ToString();
				this.FilterField = base.Request["flt"];
				object value = DocumentAction.QueryClassTypeCode(this.FilterField);
				this.ClassTypeCode = Convert.ToString(value);
				this.btnAdd.Attributes["onclick"] = string.Concat(new string[]
				{
					"openClassEdit(1,'",
					this.ClassTypeCode,
					"','",
					this.UserCode,
					"')"
				});
				this.btnEdit.Attributes["onclick"] = string.Concat(new string[]
				{
					"openClassEdit(2,'",
					this.ClassTypeCode,
					"','",
					this.UserCode,
					"')"
				});
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.dgClass_Bind();
			}
		}
		protected void dgClass_Bind()
		{
			this.dgClass.DataSource = DocumentAction.QueryDocumentClass(this.ClassTypeCode);
			this.dgClass.DataBind();
		}
		protected void dgClass_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "OnRecord(this);doClickRow('" + dataRowView["ClassId"].ToString() + "');";
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
			this.dgClass_Bind();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.dgClass_Bind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			int classId = Convert.ToInt32(this.hdnClassID.Value);
			if (DocumentAction.DelDocClass(classId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.dgClass_Bind();
			}
		}
	}


