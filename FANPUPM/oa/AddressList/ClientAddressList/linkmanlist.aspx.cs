using ASP;
using cn.justwin.BLL;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class LinkmanList : BasePage, IRequiresSessionState
	{
		public string strClient;
		protected AddressListDb ald;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ald = new AddressListDb();
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["iClient"] == null)
				{
					this.lblTitle.Text = "最近增加的联系人";
					DataTable dataSource = this.ald.eLastLinkmanQuary(15);
					this.dgLinkman.DataSource = dataSource;
					this.dgLinkman.DataBind();
					return;
				}
				this.lblTitle.Text = base.Request.QueryString["strClientName"].ToString() + " 下的联系人";
				this.strClient = base.Request.QueryString["iClient"].ToString();
				this.btnAddLinkman.Style.Remove("display");
				this.btnDelLinkman.Visible = true;
				this.btnDelLinkman.Attributes["onclick"] = "javascript:if(!confirm('真的要删除选定记录吗？')){return false;}";
				this.dgLinkman_DataBind(this.strClient);
				this.hdnCorp.Value = this.strClient;
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
			DataTable dataSource = this.ald.eGetCorp();
			this.dgGroup.DataSource = dataSource;
			this.dgGroup.DataBind();
		}
		protected void btnGroupManage_Click(object sender, EventArgs e)
		{
			this.trLinkManInfo.Style.Add("display", "none");
			this.lblTitle.Text = "外部通讯录公司管理";
			this.btnAddLinkman.Visible = false;
			this.btnDelLinkman.Visible = false;
			this.btnGroupManage.Visible = false;
			this.dgLinkman.Visible = false;
			this.dgGroup_DataBind();
			this.dgGroup.Visible = true;
			this.btnAddGroup.Visible = true;
			this.trCompanyInfo.Style.Remove("display");
		}
		protected void btnAddGroupConfirm_Click(object sender, EventArgs e)
		{
			if (this.tbGSMC.Text.Trim() == "")
			{
				this.js.Text = "alert('请填写公司名称！');";
				return;
			}
			string gsmc = this.tbGSMC.Text.Trim();
			string dz = this.tbDZ.Text.Trim();
			string yb = this.tbYB.Text.Trim();
			this.tbGSMC.Text = "";
			this.tbDZ.Text = "";
			this.tbYB.Text = "";
			bool flag = this.ald.eAddCorpInfo(gsmc, dz, yb);
			if (flag)
			{
				this.js.Text = "parent.frames.CorpList.history.go(0);";
				this.trCompanyButton.Style.Add("display", "none");
				this.trCompanyButton.Style.Add("display", "none");
				this.Panel1.Visible = false;
				this.dgGroup_DataBind();
				return;
			}
			this.js.Text = "alert('增加公司信息失败！');";
		}
		protected void btnAddGroup_Click(object sender, EventArgs e)
		{
			this.trCompanyButton.Style.Remove("display");
			this.Panel1.Visible = true;
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
			string dz = ((TextBox)e.Item.Cells[1].Controls[1]).Text.Trim();
			string yb = ((TextBox)e.Item.Cells[2].Controls[1]).Text.Trim();
			string id = this.dgGroup.DataKeys[e.Item.ItemIndex].ToString();
			if (text == "")
			{
				this.js.Text = "alert('请填写公司名称！');";
				return;
			}
			bool flag = this.ald.eUpdateCorpInfo(id, text, dz, yb);
			if (flag)
			{
				this.js.Text = "parent.frames.CorpList.history.go(0);";
				this.dgGroup.EditItemIndex = -1;
				this.dgGroup_DataBind();
				return;
			}
			this.js.Text = "alert('更新公司信息失败！');";
		}
		protected void btCancel_ServerClick(object sender, EventArgs e)
		{
			this.Panel1.Visible = false;
			this.tbGSMC.Text = "";
			this.tbDZ.Text = "";
			this.tbYB.Text = "";
		}
		private void dgGroup_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				((LinkButton)e.Item.Cells[4].Controls[0]).Attributes["onclick"] = "return confirm('该操作将删除该公司下的所有记录，请确认删除！',1,0);";
				return;
			default:
				return;
			}
		}
		private void dgGroup_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			this.dgGroup.DataKeys[e.Item.ItemIndex].ToString();
			bool flag = this.ald.eDelCorpInfo(this.dgGroup.DataKeys[e.Item.ItemIndex].ToString());
			if (flag)
			{
				this.js.Text = "parent.frames.CorpList.history.go(0);";
				this.dgGroup_DataBind();
				return;
			}
			this.js.Text = "alert('删除公司信息失败！');";
		}
		private void dgLinkman_DataBind(string strCorp)
		{
			DataTable dataSource = this.ald.eGetCorpLinkman(strCorp);
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
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				HyperLink hyperLink = (HyperLink)e.Item.Cells[1].Controls[0];
				hyperLink.NavigateUrl = "#";
				hyperLink.NavigateUrl = string.Concat(new string[]
				{
					"javascript:void(window.open('Broker.aspx?Op=Browse&iDept=",
					this.strClient,
					"&id=",
					this.dgLinkman.DataKeys[e.Item.ItemIndex].ToString(),
					"','','left=100,top=100,width=450,height=500,toolbar=no,status=yes,scrollbars=yes,resiz able=no'));"
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
					this.ald.eDelLinkman(id);
				}
			}
			if (this.hdnCorp.Value != "")
			{
				this.dgLinkman_DataBind(this.hdnCorp.Value);
			}
		}
		protected void btnRefresh_Click(object sender, EventArgs e)
		{
			string value = this.hdnCorp.Value;
			DataTable dataSource;
			if (value != "")
			{
				dataSource = this.ald.eGetCorpLinkman(value);
			}
			else
			{
				dataSource = this.ald.eLastLinkmanQuary(15);
			}
			this.dgLinkman.DataSource = dataSource;
			this.dgLinkman.DataBind();
		}
		protected void btnExp_Click1(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			foreach (DataGridColumn dataGridColumn in this.dgLinkman.Columns)
			{
				string headerText = dataGridColumn.HeaderText;
				if (headerText != "" && headerText != "职位" && headerText != "全选")
				{
					list.Add(headerText);
				}
			}
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("V_XM");
			dataTable.Columns.Add("V_YZBM");
			dataTable.Columns.Add("V_BGDH");
			dataTable.Columns.Add("V_SJ");
			dataTable.Columns.Add("V_DZYX");
			for (int i = 0; i < this.dgLinkman.Items.Count; i++)
			{
				HyperLink hyperLink = (HyperLink)this.dgLinkman.Items[i].Cells[1].Controls[0];
				char[] trimChars = new char[]
				{
					'&',
					'n',
					'b',
					's',
					'p',
					';'
				};
				string text = hyperLink.Text;
				string text2 = this.dgLinkman.Items[i].Cells[2].Text.Trim(trimChars);
				string text3 = this.dgLinkman.Items[i].Cells[3].Text.Trim(trimChars);
				string text4 = this.dgLinkman.Items[i].Cells[4].Text.Trim(trimChars);
				string text5 = this.dgLinkman.Items[i].Cells[5].Text.Trim(trimChars);
				string[] values = new string[]
				{
					text,
					text2,
					text3,
					text4,
					text5
				};
				dataTable.Rows.Add(values);
			}
			string[] fieldName = new string[]
			{
				"V_XM",
				"V_YZBM",
				"V_BGDH",
				"V_SJ",
				"V_DZYX"
			};
			ExcelHelper.ExportExcel(dataTable, list.ToArray(), fieldName, new string[0], "外部通讯录.xls", base.Request.Browser.Browser);
		}
		public void ExportToExcel(string FileType, string FileName)
		{
			base.Response.Charset = "GB2312";
			base.Response.ContentEncoding = Encoding.Default;
			base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
			base.Response.ContentType = FileType;
			this.EnableViewState = false;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			if (this.dgLinkman.Visible)
			{
				this.dgLinkman.Columns[0].Visible = false;
				this.dgLinkman.Columns[6].Visible = false;
				this.dgLinkman.RenderControl(writer);
			}
			else
			{
				this.dgGroup.Columns[3].Visible = false;
				this.dgGroup.Columns[4].Visible = false;
				this.dgGroup.RenderControl(writer);
			}
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
	}


