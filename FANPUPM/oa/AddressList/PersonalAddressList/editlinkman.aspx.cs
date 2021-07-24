using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.sysManage.common;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EditLinkman : BasePage, IRequiresSessionState
	{
		protected AddressListDb ald;
		protected string _strGroup;

		protected void Page_Load(object sender, EventArgs e)
		{
			new com.jwsoft.sysManage.common.Calendar(this.Page);
			this.tbBirthday.Attributes["onClick"] = "opencalendar(this)";
			this.ald = new AddressListDb();
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["Op"].ToString() == "Add")
				{
					this._strGroup = base.Request.QueryString["Group"].ToString();
					this.ddlGroup_DataBind();
					this.Label1.Text = "增加联系人";
					return;
				}
				if (base.Request.QueryString["Op"].ToString() == "Mod")
				{
					string id = base.Request.QueryString["id"].ToString();
					DataTable dataTable = this.ald.pGetLinkman(id);
					this._strGroup = dataTable.Rows[0]["i_fz_id"].ToString();
					this.ddlGroup_DataBind();
					this.tbName.Text = dataTable.Rows[0]["v_xm"].ToString();
					this.rbtSex.SelectedValue = dataTable.Rows[0]["c_xb"].ToString();
					this.tbUnit.Text = dataTable.Rows[0]["v_dw"].ToString();
					this.tbHandset.Text = dataTable.Rows[0]["v_sj"].ToString();
					this.tbHomePhone.Text = dataTable.Rows[0]["v_zzdh"].ToString();
					this.tbCorpPhone.Text = dataTable.Rows[0]["v_bgdh"].ToString();
					if (dataTable.Rows[0]["dtm_sr"].ToString().Trim() != "")
					{
						this.tbBirthday.Text = Convert.ToDateTime(dataTable.Rows[0]["dtm_sr"]).ToString("d");
					}
					this.tbAddress.Text = dataTable.Rows[0]["v_jtzz"].ToString();
					this.tbPostalcode.Text = dataTable.Rows[0]["c_yzbm"].ToString();
					this.tbQQ.Text = dataTable.Rows[0]["v_wlch"].ToString();
					this.tbEmail.Text = dataTable.Rows[0]["v_dzyx"].ToString();
					this.tbContent.Text = dataTable.Rows[0]["v_bz"].ToString();
					this.Label1.Text = "修改联系人";
					return;
				}
				if (base.Request.QueryString["Op"].ToString() == "Browse")
				{
					this.butcancel.Visible = false;
					string id2 = base.Request.QueryString["id"].ToString();
					DataTable dataTable2 = this.ald.pGetLinkman(id2);
					this._strGroup = dataTable2.Rows[0]["i_fz_id"].ToString();
					this.ddlGroup_DataBind();
					this.tbName.Text = dataTable2.Rows[0]["v_xm"].ToString();
					this.rbtSex.SelectedValue = dataTable2.Rows[0]["c_xb"].ToString();
					this.tbUnit.Text = dataTable2.Rows[0]["v_dw"].ToString();
					this.tbHandset.Text = dataTable2.Rows[0]["v_sj"].ToString();
					this.tbHomePhone.Text = dataTable2.Rows[0]["v_zzdh"].ToString();
					this.tbCorpPhone.Text = dataTable2.Rows[0]["v_bgdh"].ToString();
					if (dataTable2.Rows[0]["dtm_sr"].ToString().Trim() != "")
					{
						this.tbBirthday.Text = Convert.ToDateTime(dataTable2.Rows[0]["dtm_sr"]).ToString("d");
					}
					this.tbAddress.Text = dataTable2.Rows[0]["v_jtzz"].ToString();
					this.tbPostalcode.Text = dataTable2.Rows[0]["c_yzbm"].ToString();
					this.tbQQ.Text = dataTable2.Rows[0]["v_wlch"].ToString();
					this.tbEmail.Text = dataTable2.Rows[0]["v_dzyx"].ToString();
					this.tbContent.Text = dataTable2.Rows[0]["v_bz"].ToString();
					this.Label1.Text = "查看联系人";
					this.tbName.Enabled = false;
					this.rbtSex.Enabled = false;
					this.tbUnit.Enabled = false;
					this.tbHandset.Enabled = false;
					this.tbHomePhone.Enabled = false;
					this.tbCorpPhone.Enabled = false;
					this.tbBirthday.Enabled = false;
					this.tbAddress.Enabled = false;
					this.tbPostalcode.Enabled = false;
					this.tbQQ.Enabled = false;
					this.tbEmail.Enabled = false;
					this.tbContent.Enabled = false;
					this.ddlGroup.Enabled = false;
					this.tbnAdd.Visible = false;
				}
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void ddlGroup_DataBind()
		{
			DataTable dataTable = this.ald.pGetGroup(this.Page.Session["yhdm"].ToString());
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				ListItem listItem = new ListItem();
				listItem.Text = dataTable.Rows[i]["v_fzmc"].ToString();
				listItem.Value = dataTable.Rows[i]["i_id"].ToString();
				if (dataTable.Rows[i]["i_id"].ToString() == this._strGroup)
				{
					listItem.Selected = true;
				}
				this.ddlGroup.Items.Add(listItem);
			}
		}
		protected void tbnAdd_Click(object sender, EventArgs e)
		{
			if (!(base.Request.QueryString["Op"].ToString() == "Add"))
			{
				if (base.Request.QueryString["Op"].ToString() == "Mod")
				{
					if (!this.ald.pUpdateLinkman(base.Request.QueryString["id"].ToString(), this.tbName.Text.Trim(), this.tbBirthday.Text.Trim(), this.tbAddress.Text.Trim(), this.tbPostalcode.Text.Trim(), this.tbUnit.Text.Trim(), this.tbCorpPhone.Text.Trim(), this.tbHomePhone.Text.Trim(), this.tbHandset.Text.Trim(), this.rbtSex.SelectedItem.Value, this.tbEmail.Text.Trim(), this.tbQQ.Text.Trim(), this.tbContent.Text.Trim(), this.ddlGroup.SelectedValue))
					{
						this.js.Text = "top.ui.alert('更新失败！');top.ui.closeWin()";
						return;
					}
					this.js.Text = "top.ui.closeWin();top.ui.callback('" + base.Request.QueryString["Group"].ToString() + "');";
				}
				return;
			}
			if (!this.ald.pAddLinkman(this.Page.Session["yhdm"].ToString(), this.tbName.Text.Trim(), this.tbBirthday.Text.Trim(), this.tbAddress.Text.Trim(), this.tbPostalcode.Text.Trim(), this.tbUnit.Text.Trim(), this.tbCorpPhone.Text.Trim(), this.tbHomePhone.Text.Trim(), this.tbHandset.Text.Trim(), this.rbtSex.SelectedItem.Value, this.tbEmail.Text.Trim(), this.tbQQ.Text.Trim(), this.tbContent.Text.Trim(), this.ddlGroup.SelectedValue))
			{
				this.js.Text = "top.ui.alert('增加失败！');top.ui.closeWin();";
				return;
			}
			this.js.Text = "top.ui.closeWin();top.ui.callback('" + base.Request.QueryString["Group"].ToString() + "');";
		}
	}


