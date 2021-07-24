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
	public partial class LinkmanList : BasePage, IRequiresSessionState
	{
		public string strGroup;
		protected AddressListDb ald;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ald = new AddressListDb();
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["iGroup"] == null)
				{
					this.lblTitle.Text = "最近增加的联系人";
					DataTable dataSource = this.ald.pLastLinkmanQuary(15, this.Page.Session["yhdm"].ToString());
					this.dgLinkman.DataSource = dataSource;
					this.dgLinkman.DataBind();
					return;
				}
				this.lblTitle.Text = base.Request.QueryString["strGroupName"].ToString() + " 下的联系人";
				this.strGroup = base.Request.QueryString["iGroup"].ToString();
				this.btnAddLinkman.Style.Remove("display");
				this.btnDelLinkman.Visible = true;
				this.btnDelLinkman.Attributes["onclick"] = "javascript:if(!confirm('真的要删除选定记录吗？')){return false;}";
				this.dgLinkman_DataBind(this.strGroup);
				this.hdnGroup.Value = this.strGroup;
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgLinkman.ItemDataBound += new DataGridItemEventHandler(this.dgLinkman_ItemDataBound);
			this.dgGroup.ItemCreated += new DataGridItemEventHandler(this.dgGroup_ItemCreated);
			this.dgGroup.CancelCommand += new DataGridCommandEventHandler(this.dgGroup_CancelCommand);
			this.dgGroup.EditCommand += new DataGridCommandEventHandler(this.dgGroup_EditCommand);
			this.dgGroup.UpdateCommand += new DataGridCommandEventHandler(this.dgGroup_UpdateCommand);
			this.dgGroup.DeleteCommand += new DataGridCommandEventHandler(this.dgGroup_DeleteCommand);
		}
		private void dgGroup_DataBind()
		{
			DataTable dataSource = this.ald.pGetGroup(this.Page.Session["yhdm"].ToString());
			this.dgGroup.DataSource = dataSource;
			this.dgGroup.DataBind();
		}
		private void dgGroup_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				((LinkButton)e.Item.Cells[3].Controls[0]).Attributes["onclick"] = "return confirm('该操作将删除该分组下的所有记录，请确认删除！',1,0);";
				return;
			default:
				return;
			}
		}
		protected void btnGroupManage_Click(object sender, EventArgs e)
		{
			this.dgGroup_DataBind();
			this.dgGroup.Visible = true;
			this.btnAddGroup.Visible = true;
			this.trGroupButton.Style.Remove("display");
			this.trGroupInfo.Style.Remove("display");
			this.lblTitle.Text = "个人通讯录分组管理";
			this.btnAddLinkman.Visible = false;
			this.btnDelLinkman.Visible = false;
			this.btnGroupManage.Visible = false;
			this.dgLinkman.Visible = false;
			this.trLinkmanInfo.Style.Add("display", "none");
		}
		private void dgGroup_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.dgGroup.EditItemIndex = e.Item.ItemIndex;
			this.dgGroup_DataBind();
		}
		private void dgGroup_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.dgGroup.EditItemIndex = -1;
			this.dgGroup_DataBind();
		}
		private void dgGroup_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			string text = ((TextBox)e.Item.Cells[0].Controls[1]).Text.Trim();
			string bz = ((TextBox)e.Item.Cells[1].Controls[1]).Text.Trim();
			int id = Convert.ToInt32(this.dgGroup.DataKeys[e.Item.ItemIndex].ToString());
			if (text == "")
			{
				this.js.Text = "alert('请填写分组名称！');";
				return;
			}
			bool flag = this.ald.pUpdateGroup(id, text, bz);
			if (flag)
			{
				this.js.Text = "parent.frames.group.history.go(0);";
				this.dgGroup.EditItemIndex = -1;
				this.dgGroup_DataBind();
				return;
			}
			this.js.Text = "alert('更新分组失败！');";
		}
		private void dgGroup_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			int id = Convert.ToInt32(this.dgGroup.DataKeys[e.Item.ItemIndex].ToString());
			bool flag = this.ald.pDelGroup(id);
			if (flag)
			{
				this.js.Text = "parent.frames.group.history.go(0);";
				this.dgGroup_DataBind();
				return;
			}
			this.js.Text = "alert('删除分组失败！');";
		}
		protected void btnAddGroupConfirm_Click(object sender, EventArgs e)
		{
			if (this.tbFZMC.Text.Trim() == "")
			{
				this.js.Text = "alert('请填写分组名称！');";
				return;
			}
			string fzmc = this.tbFZMC.Text.Trim();
			string bz = this.tbBZ.Text.Trim();
			this.tbFZMC.Text = "";
			this.tbBZ.Text = "";
			bool flag = this.ald.pAddGroup(this.Page.Session["yhdm"].ToString(), fzmc, bz);
			if (flag)
			{
				this.js.Text = "parent.frames.group.history.go(0);";
				this.Panel1.Visible = false;
				this.trGroupButton.Style.Add("display", "none");
				this.dgGroup_DataBind();
				return;
			}
			this.js.Text = "alert('增加分组失败！');";
		}
		protected void Button1_ServerClick(object sender, EventArgs e)
		{
			this.Panel1.Visible = false;
			this.trGroupButton.Style.Add("display", "none");
		}
		protected void btnAddGroup_Click(object sender, EventArgs e)
		{
			this.Panel1.Visible = true;
			this.trGroupButton.Style.Remove("display");
			this.trGroupInfo.Style.Remove("display");
		}
		private void dgLinkman_DataBind(string strGroup)
		{
			DataTable dataSource = this.ald.pGetGroupMember(Convert.ToInt32(strGroup));
			this.dgLinkman.DataSource = dataSource;
			this.dgLinkman.DataBind();
		}
		private void dgLinkman_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex == -1)
			{
				CheckBox checkBox = (CheckBox)e.Item.FindControl("cbSelAllLinkman");
				if (checkBox != null)
				{
					checkBox.Attributes.Add("onclick", "selectAll(this)");
					return;
				}
			}
			else
			{
				e.Item.Cells[4].Text = ((DataRowView)e.Item.DataItem)["v_sj"].ToString();
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				HyperLink hyperLink = (HyperLink)e.Item.Cells[1].Controls[0];
				hyperLink.NavigateUrl = "#";
				hyperLink.NavigateUrl = string.Concat(new string[]
				{
					"javascript:void(window.open('Broker.aspx?Op=Browse&Group=",
					this.strGroup,
					"&id=",
					this.dgLinkman.DataKeys[e.Item.ItemIndex].ToString(),
					"','','left=100,top=100,width=450,height=500,toolbar=no,status=yes,scrollbars=yes,resizable=no'));"
				});
			}
		}
		protected void dgLinkman_PreRender(object sender, EventArgs e)
		{
			string text = "";
			text += "function selectAll(obj)\n";
			text += "{\n";
			text += "\t\tvar num = document.all.tags(\"input\").length;\n";
			text += "\t\tvar str_temp;\n";
			text += "\t\t//设置子模块复选框\n";
			text += "\t\tfor(var i=0; i<num; i++)\n";
			text += "\t\t{\n";
			text += "\t\t\tstr_temp = document.all.tags(\"input\")[i].id;\n";
			text += "\t\t\tif(str_temp.substr(str_temp.length-9,9) == 'cbLinkman')\n";
			text += "\t\t\t{\n";
			text += "\t\t\t\tdocument.all.tags(\"input\")[i].checked = obj.checked;\n";
			text += "\t\t\t}\n";
			text += "\t\t}\n";
			text += "\t}";
			this.js.Text = text;
		}
		protected void btnDelLinkman_Click(object sender, EventArgs e)
		{
			foreach (DataGridItem dataGridItem in this.dgLinkman.Items)
			{
				CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbLinkman");
				if (checkBox.Checked)
				{
					string id = this.dgLinkman.DataKeys[dataGridItem.ItemIndex].ToString();
					this.ald.pDelLinkman(id);
				}
			}
			if (this.hdnGroup.Value != "")
			{
				this.dgLinkman_DataBind(this.hdnGroup.Value);
			}
		}
		protected void btnRefresh_Click(object sender, EventArgs e)
		{
			DataTable dataSource = this.ald.pGetGroupMember(Convert.ToInt32(this.hdnGroup.Value));
			this.dgLinkman.DataSource = dataSource;
			this.dgLinkman.DataBind();
		}
		protected void btnMessage_Click(object sender, EventArgs e)
		{
			string text = "";
			foreach (DataGridItem dataGridItem in this.dgLinkman.Items)
			{
				CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbLinkman");
				if (checkBox.Checked && dataGridItem.Cells[4].Text.Trim() != "")
				{
					text = text + dataGridItem.Cells[4].Text.Trim() + ",";
				}
			}
			text = text.Trim(new char[]
			{
				','
			});
			string text2 = "";
			text2 += "<script language=javascript>";
			text2 += "var strUrl = parent.location.href;";
			text2 = text2 + "parent.location.href='../SendMsg.aspx?strHandset=" + text + "&srcUrl='+ strUrl;";
			text2 += "</script>";
			this.Page.RegisterStartupScript("redirect", text2);
		}
	}


