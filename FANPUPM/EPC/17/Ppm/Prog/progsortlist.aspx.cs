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
	public partial class ProgSortList : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Button_add.Attributes.Add("onclick", "ShowInsertWindow()");
			this.Button_edit.Attributes.Add("onclick", "ShowEditWindow()");
			this.Button_del.Attributes.Add("onclick", "if( !confirm('删除该项,将删除所有该项相关的信息,是否确认删除?')){ return false;}");
			if (!base.IsPostBack)
			{
				this.bind();
			}
		}
		private void bind()
		{
			DataTable progSortCollections = ProgSortAction.GetProgSortCollections("");
			this.DataGrid1.DataSource = progSortCollections;
			this.DataGrid1.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
		}
		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["id"] = e.Item.ItemIndex.ToString();
				e.Item.Attributes["onclick"] = "OnRecord(this);setvalue('" + dataRowView["ProgSortCode"] + "')";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.bind();
		}
		protected void Button_del_Click(object sender, EventArgs e)
		{
			int itemProgCount = ItemProgAction.GetItemProgCount("ProgSortCode=" + this.TextBox_pk.Text);
			if (itemProgCount > 0)
			{
				this.JavaScriptControl1.Text = "alert('该类别对应的有奖罚记录，请删除对应的奖罚记录后再删除该类别！');";
				return;
			}
			if (this.TextBox_pk.Text.Trim() == "")
			{
				this.JavaScriptControl1.Text = "alert('请选择删除的记录！')";
				return;
			}
			bool flag = ProgSortAction.Delete(this.TextBox_pk.Text);
			if (flag)
			{
				this.JavaScriptControl1.Text = "alert('删除成功！');window.location.href=window.location.href";
				return;
			}
			this.JavaScriptControl1.Text = "alert('删除失败！')";
		}
	}


