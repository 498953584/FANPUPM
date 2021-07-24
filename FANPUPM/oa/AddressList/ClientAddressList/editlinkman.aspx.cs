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
	public partial class EditLinkman : BasePage, IRequiresSessionState
	{
		protected AddressListDb ald;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ald = new AddressListDb();
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["Op"].ToString() != "Browse")
				{
					if (base.Request.QueryString["Op"].ToString() == "Add")
					{
						this.lblTitle.Text = "增加联系人";
						return;
					}
					if (base.Request.QueryString["Op"].ToString() == "Mod")
					{
						this.lblTitle.Text = "修改联系人";
						DataTable dataTable = this.ald.eGetLinkman(base.Request.QueryString["id"].ToString());
						this.tbName.Text = dataTable.Rows[0]["v_xm"].ToString();
						if (dataTable.Rows[0]["c_xb"].ToString() != "")
						{
							this.rblSex.SelectedValue = dataTable.Rows[0]["c_xb"].ToString();
						}
						this.tbDuty.Text = dataTable.Rows[0]["v_zw"].ToString();
						this.tbAddress.Text = dataTable.Rows[0]["v_jtzz"].ToString();
						this.tbPostalcode.Text = dataTable.Rows[0]["v_yzbm"].ToString();
						this.tbCorpPhone.Text = dataTable.Rows[0]["v_bgdh"].ToString();
						this.tbFax.Text = dataTable.Rows[0]["v_cz"].ToString();
						this.tbHomePhone.Text = dataTable.Rows[0]["v_zzdh"].ToString();
						this.tbHandset.Text = dataTable.Rows[0]["v_sj"].ToString();
						this.tbEmail.Text = dataTable.Rows[0]["v_dzyx"].ToString();
						this.tbQQ.Text = dataTable.Rows[0]["v_wlch"].ToString();
						this.tbContent.Text = dataTable.Rows[0]["v_bz"].ToString();
						return;
					}
				}
				else
				{
					this.btnClose.Visible = false;
					this.btnConfirm.Visible = false;
					this.tbName.Visible = false;
					this.rblSex.Visible = false;
					this.tbDuty.Visible = false;
					this.tbAddress.Visible = false;
					this.tbPostalcode.Visible = false;
					this.tbCorpPhone.Visible = false;
					this.tbFax.Visible = false;
					this.tbHomePhone.Visible = false;
					this.tbHandset.Visible = false;
					this.tbEmail.Visible = false;
					this.tbQQ.Visible = false;
					this.tbContent.Visible = false;
					new userManageDb();
					DataTable dataTable2 = this.ald.eGetLinkman(base.Request.QueryString["id"].ToString());
					this.Table1.Rows[1].Cells[1].InnerText = dataTable2.Rows[0]["v_xm"].ToString();
					this.Table1.Rows[2].Cells[1].InnerText = ((dataTable2.Rows[0]["c_xb"].ToString() == "f") ? "女" : "男");
					this.Table1.Rows[3].Cells[1].InnerText = dataTable2.Rows[0]["v_zw"].ToString();
					this.Table1.Rows[4].Cells[1].InnerText = dataTable2.Rows[0]["v_jtzz"].ToString();
					this.Table1.Rows[5].Cells[1].InnerText = dataTable2.Rows[0]["v_yzbm"].ToString();
					this.Table1.Rows[6].Cells[1].InnerText = dataTable2.Rows[0]["v_bgdh"].ToString();
					this.Table1.Rows[7].Cells[1].InnerText = dataTable2.Rows[0]["v_cz"].ToString();
					this.Table1.Rows[8].Cells[1].InnerText = dataTable2.Rows[0]["v_zzdh"].ToString();
					this.Table1.Rows[9].Cells[1].InnerText = dataTable2.Rows[0]["v_sj"].ToString();
					this.Table1.Rows[10].Cells[1].InnerText = dataTable2.Rows[0]["v_dzyx"].ToString();
					this.Table1.Rows[11].Cells[1].InnerText = dataTable2.Rows[0]["v_wlch"].ToString();
					this.Table1.Rows[12].Cells[1].InnerText = dataTable2.Rows[0]["v_bz"].ToString();
					this.lblTitle.Text = dataTable2.Rows[0]["v_xm"].ToString() + "的详细信息";
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
		protected void btnConfirm_Click(object sender, EventArgs e)
		{
			string xm = this.tbName.Text.Trim();
			string zw = this.tbDuty.Text.Trim();
			string jtzz = this.tbAddress.Text.Trim();
			string yzbm = this.tbPostalcode.Text.Trim();
			string bgdh = this.tbCorpPhone.Text.Trim();
			string zzdh = this.tbHomePhone.Text.Trim();
			string sj = this.tbHandset.Text.Trim();
			string cz = this.tbFax.Text.Trim();
			string selectedValue = this.rblSex.SelectedValue;
			string dzyx = this.tbEmail.Text.Trim();
			string wlch = this.tbQQ.Text.Trim();
			string text = this.tbContent.Text;
			if (!(base.Request.QueryString["Op"].ToString() == "Add"))
			{
				if (base.Request.QueryString["Op"].ToString() == "Mod")
				{
					int id = Convert.ToInt32(base.Request.QueryString["id"].ToString());
					if (!this.ald.eUpdateLinkman(id, xm, zw, jtzz, yzbm, bgdh, zzdh, sj, cz, selectedValue, dzyx, wlch, text))
					{
						this.js.Text = "top.ui.alert('更新失败！');top.ui.closeWin()";
						return;
					}
					this.js.Text = "top.ui.closeWin();top.ui.callback('" + base.Request.QueryString["Client"].ToString() + "');";
				}
				return;
			}
			string gsid = base.Request.QueryString["Client"].ToString();
			if (!this.ald.eAddLinkman(gsid, xm, zw, jtzz, yzbm, bgdh, zzdh, sj, cz, selectedValue, dzyx, wlch, text))
			{
				this.js.Text = "top.ui.alert('增加失败！');top.ui.closeWin();";
				return;
			}
			this.js.Text = "top.ui.closeWin();top.ui.callback('" + base.Request.QueryString["Client"].ToString() + "');";
		}
	}


