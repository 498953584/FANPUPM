using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TempletView : NBasePage, IRequiresSessionState
	{
		private TableAccountAction taa = new TableAccountAction();
		private TableAccountModelInfo tami = new TableAccountModelInfo();
		private string _showListTitle = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["Flag"] == "Q")
				{
					this._showListTitle = "质量资料模板名称";
				}
				else
				{
					if (base.Request["Flag"] == "S")
					{
						this._showListTitle = "安全资料模板名称";
					}
				}
				this.ViewState["Flag"] = base.Request.QueryString["Flag"].ToString();
				this.DataGridBind();
				this.btnAdd.Attributes["onclick"] = "javascritp:return OpenTempletEdit('" + base.Request["Flag"].ToString() + "','Add');";
				this.btnEdit.Attributes["onclick"] = "javascritp:return OpenTempletEdit('" + base.Request["Flag"].ToString() + "','Edit');";
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确认要删除选定记录么？')){return false;}";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdList.ItemDataBound += new DataGridItemEventHandler(this.DGrdList_ItemDataBound);
		}
		private void DataGridBind()
		{
			DataTable accountModelList = this.taa.getAccountModelList(this.ViewState["Flag"].ToString());
			this.DGrdList.DataSource = accountModelList;
			this.DGrdList.DataBind();
		}
		private void DGrdList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				string str = this.DGrdList.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["id"] = e.Item.ItemIndex.ToString();
				e.Item.Attributes["onclick"] = "OnRecord(this);clickRow('" + str + "');";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["style"] = "cursor:hand";
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Cells[1].Text = string.Concat(new string[]
				{
					"<a href='",
					((DataRowView)e.Item.DataItem)["FilePath"].ToString(),
					"' title='",
					((DataRowView)e.Item.DataItem)["ModelName"].ToString(),
					"'>",
					((DataRowView)e.Item.DataItem)["ModelName"].ToString(),
					"</a>"
				});
				return;
			}
			e.Item.Cells[1].Text = this._showListTitle;
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.DataGridBind();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.DataGridBind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			if (this.taa.delAccountModelRecord(Convert.ToInt32(this.hdnRecordID.Value)))
			{
				this.JS.Text = "alert('删除记录成功!');";
			}
			else
			{
				this.JS.Text = "alert('删除记录失败!');";
			}
			this.DataGridBind();
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DGrdList.CurrentPageIndex = e.NewPageIndex;
			this.DataGridBind();
		}
	}


