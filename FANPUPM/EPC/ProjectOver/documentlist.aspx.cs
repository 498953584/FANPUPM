using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DocumentList : NBasePage, IRequiresSessionState
	{
		protected string ProjectCode
		{
			get
			{
				object obj = this.ViewState["ProjectCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ProjectCode"] = value;
			}
		}
		protected string SqlWhere
		{
			get
			{
				object obj = this.ViewState["SqlWhere"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["SqlWhere"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["prjId"] == null)
				{
					this.JS.Text = "alert('参数错误！');";
				}
				else
				{
					this.ProjectCode = base.Request["prjId"];
				}
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20);
				this.SqlWhere = ProjectOverAction.GetSqlWhere(this.ProjectCode, 0, "", 9999);
				this.DgdDocument_Bind();
				this.BtnDel.Attributes["onclick"] = "if (!confirm('你确定要删除选定的记录吗？')){return false;}";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DgdDocument.ItemDataBound += new DataGridItemEventHandler(this.DgdDocument_ItemDataBound);
		}
		private void DgdDocument_Bind()
		{
			this.DgdDocument.DataSource = ProjectOverAction.QueryProjectDocumentList(this.SqlWhere);
			this.DgdDocument.DataBind();
		}
		protected void DgdDocument_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DgdDocument.CurrentPageIndex = e.NewPageIndex;
			this.DgdDocument_Bind();
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.SqlWhere = ProjectOverAction.GetSqlWhere(this.ProjectCode, Convert.ToInt32(this.DDLTerm.SelectedValue), this.TxtTerm.Text.Trim(), Convert.ToInt32(this.DDTClass.SelectedValue.Trim()));
			this.DgdDocument_Bind();
		}
		private void DgdDocument_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["id"] = e.Item.ItemIndex.ToString();
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.DgdDocument.ClientID.ToString(),
					"');clickRow('",
					dataRowView["DocumentCode"].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
			}
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			if (ProjectOverAction.DelDocument(new Guid(this.HdnDocCode.Value)) == 1)
			{
				this.JS.Text = "alert('删除成功！');parent.frames[1].location = 'DocumentAdd.aspx?pc=" + this.ProjectCode + "&allowadd=1'";
				this.DgdDocument_Bind();
				return;
			}
			this.JS.Text = "alert('删除失败！');";
		}
	}


