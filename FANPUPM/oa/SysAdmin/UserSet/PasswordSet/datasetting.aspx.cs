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
	public partial class DataSetting : BasePage, IRequiresSessionState
	{
		protected AddressListDb ald;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ald = new AddressListDb();
			if (!this.Page.IsPostBack)
			{
				this.lblTitle.Text = "设 置 用 户 资 料";
				DataRow dataRow = this.ald.cGetLinkmanDetail(this.Page.Session["yhdm"].ToString());
				if (dataRow != null)
				{
					if (dataRow["c_xb"].ToString() != "")
					{
						this.rblSex.SelectedValue = dataRow["c_xb"].ToString();
					}
					this.tbDuty.Text = dataRow["v_zw"].ToString();
					this.tbAddress.Text = dataRow["v_jtzz"].ToString();
					this.tbPostalcode.Text = dataRow["v_yzbm"].ToString();
					this.tbCorpPhone.Text = dataRow["v_bgdh"].ToString();
					this.tbFax.Text = dataRow["v_cz"].ToString();
					this.tbHomePhone.Text = dataRow["v_zzdh"].ToString();
					this.cbZdbs.Checked = (dataRow["c_zdbs"].ToString() == "y");
					this.tbHandset.Text = dataRow["v_sj"].ToString();
					this.cbSjbs.Checked = (dataRow["c_sjbs"].ToString() == "y");
					this.tbEmail.Text = dataRow["v_dzyx"].ToString();
					this.tbQQ.Text = dataRow["v_wlch"].ToString();
					this.tbContent.Text = dataRow["v_bz"].ToString();
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
			string zw = this.tbDuty.Text.Trim();
			string jtzz = this.tbAddress.Text.Trim();
			string yzbm = this.tbPostalcode.Text.Trim();
			string bgdh = this.tbCorpPhone.Text.Trim();
			string zzdh = this.tbHomePhone.Text.Trim();
			string zdbs = "n";
			if (this.cbZdbs.Checked)
			{
				zdbs = "y";
			}
			string sj = this.tbHandset.Text.Trim();
			string sjbs = "n";
			if (this.cbSjbs.Checked)
			{
				sjbs = "y";
			}
			string cz = this.tbFax.Text.Trim();
			string selectedValue = this.rblSex.SelectedValue;
			string dzyx = this.tbEmail.Text.Trim();
			string wlch = this.tbQQ.Text.Trim();
			string text = this.tbContent.Text;
			bool flag = this.ald.cUpdateUserData(this.Session["yhdm"].ToString(), zw, jtzz, yzbm, bgdh, zzdh, zdbs, sj, sjbs, cz, selectedValue, dzyx, wlch, text);
			if (flag)
			{
				this.js.Text = "alert('用户资料设置成功！');";
				return;
			}
			this.js.Text = "alert('用户资料设置失败！请与管理员联系。');";
		}
	}


