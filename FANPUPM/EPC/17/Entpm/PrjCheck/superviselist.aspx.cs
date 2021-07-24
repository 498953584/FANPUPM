using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SuperviseList : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["Type"] == "List")
			{
				this.Button_add.Visible = false;
				this.Button_edit.Visible = false;
				this.Button_del.Visible = false;
				this.Button_sp.Visible = false;
				this.btn_Search.Visible = true;
				this.TextBox_lxdw.Visible = true;
				this.Literal2.Visible = true;
				this.Literal1.Text = "项目监察查询";
			}
			else
			{
				if (base.Request["Type"] == "Edit")
				{
					this.Button_add.Visible = true;
					this.Button_edit.Visible = true;
					this.Button_del.Visible = true;
					this.Button_sp.Visible = false;
					this.btn_Search.Visible = false;
					this.TextBox_lxdw.Visible = false;
					this.Literal2.Visible = false;
					this.Literal1.Text = "项目监察";
				}
				else
				{
					if (base.Request["Type"] == "ShenHe")
					{
						this.Button_add.Visible = false;
						this.Button_edit.Visible = false;
						this.Button_del.Visible = false;
						this.Button_sp.Visible = true;
						this.btn_Search.Visible = true;
						this.TextBox_lxdw.Visible = true;
						this.Literal2.Visible = true;
						this.Literal1.Text = "项目监察审核";
					}
				}
			}
			if (base.Request["PrjCode"] != null)
			{
				this.Button_add.Attributes["onclick"] = "ShowInsertWindow('" + base.Request["PrjCode"].ToString() + "');";
			}
			this.Button_edit.Attributes.Add("onclick", "ShowEditWindow()");
			this.Button_sp.Attributes.Add("onclick", "ShowSpWindow()");
			if (!base.IsPostBack)
			{
				this.bind();
			}
		}
		private void bind()
		{
			if (base.Request["PrjCode"] != null)
			{
				string text = "prjcode='" + base.Request["PrjCode"].ToString() + "'";
				if (base.Request["Type"] == "List")
				{
					text += " and ApproveResult=1";
				}
				this.DataGrid1.DataSource = SuperviseAction.GetSuperviseCollections(text);
				this.DataGrid1.DataBind();
				return;
			}
			if (base.Request["Type"] == "ShenHe")
			{
				string strwhere = "ApproveResult=2";
				this.DataGrid1.DataSource = SuperviseAction.GetSuperviseCollections(strwhere);
				this.DataGrid1.DataBind();
				return;
			}
			this.DataGrid1.DataSource = "";
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
				e.Item.Attributes["id"] = e.Item.ItemIndex.ToString();
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);setvalue('",
					e.Item.Cells[0].Text,
					"',",
					e.Item.Cells[9].Text,
					")"
				});
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes.Add("ondblclick", "ShowInfo(); return false;");
				e.Item.ToolTip = "双击查看详细信息";
				string value = AduitAction.SetOkState(e.Item.Cells[9].Text).Substring(3);
				e.Item.Cells[8].Style.Add("color", value);
				e.Item.Cells[8].Text = AduitAction.SetOkState(e.Item.Cells[9].Text).Substring(0, 3);
				HyperLink hyperLink = (HyperLink)e.Item.Cells[3].Controls[0];
				hyperLink.NavigateUrl = "#";
				hyperLink.NavigateUrl = "javascript:void(window.open('SuperviseManage.aspx?pk=" + e.Item.Cells[0].Text + "&readonly=','','left=150,top=150,width=640,height=580,toolbar=no,status=yes,scrollbars=yes,resiz able=no'));";
			}
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.bind();
		}
		protected void Button_del_Click(object sender, EventArgs e)
		{
			if (this.TextBox_pk.Text.Trim() == "")
			{
				this.JavaScriptControl1.Text = "alert('请选择删除的记录！')";
				return;
			}
			bool flag = SuperviseAction.Delete(this.TextBox_pk.Text);
			if (flag)
			{
				this.JavaScriptControl1.Text = "alert('删除成功！');window.location.href=window.location.href";
				return;
			}
			this.JavaScriptControl1.Text = "alert('删除失败！')";
		}
		protected void Button_query_Click(object sender, EventArgs e)
		{
			if (base.Request["PrjCode"] == null)
			{
				this.DataGrid1.DataSource = "";
				this.DataGrid1.DataBind();
				return;
			}
			string text = "prjcode='" + base.Request["PrjCode"].ToString() + "'";
			if (this.TextBox_lxdw.Text != "")
			{
				text = text + " and StandNapeUnit like '%" + this.TextBox_lxdw.Text + "%'";
			}
			this.DataGrid1.DataSource = SuperviseAction.GetSuperviseCollections(text);
			this.DataGrid1.DataBind();
		}
		protected void Button_add_Click(object sender, EventArgs e)
		{
			this.bind();
		}
	}


