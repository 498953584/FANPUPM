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
						int num = this.ald.cGetDeptMaxSerial(Convert.ToInt32(base.Request.QueryString["iDept"].ToString())) + 1;
						this.tbSerial.Text = num.ToString();
						string text = base.Request.QueryString["iDept"].ToString();
						DeptManageDb deptManageDb = new DeptManageDb();
						DataTable deptmentDetail = deptManageDb.GetDeptmentDetail(Convert.ToInt32(text));
						ListItem item = new ListItem("返回上一级", deptmentDetail.Rows[0]["i_sjdm"].ToString());
						DataTable allSameLevelDept = deptManageDb.GetAllSameLevelDept(Convert.ToInt32(text));
						this.ddlDept.DataSource = allSameLevelDept;
						this.ddlDept.DataTextField = "v_bmmc";
						this.ddlDept.DataValueField = "i_bmdm";
						this.ddlDept.DataBind();
						this.ddlDept.SelectedValue = text;
						if (Convert.ToInt32(allSameLevelDept.Rows[0]["i_sjdm"].ToString()) != 0)
						{
							this.ddlDept.Items.Insert(allSameLevelDept.Rows.Count, item);
							return;
						}
					}
					else
					{
						if (base.Request.QueryString["Op"].ToString() == "Mod")
						{
							this.lblTitle.Text = "修改联系人";
							DataTable dataTable = this.ald.cGetLinkman(base.Request.QueryString["id"].ToString());
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
							this.cbZdbs.Checked = (dataTable.Rows[0]["c_zdbs"].ToString() == "y");
							this.tbHandset.Text = dataTable.Rows[0]["v_sj"].ToString();
							this.cbSjbs.Checked = (dataTable.Rows[0]["c_sjbs"].ToString() == "y");
							this.tbEmail.Text = dataTable.Rows[0]["v_dzyx"].ToString();
							this.tbQQ.Text = dataTable.Rows[0]["v_wlch"].ToString();
							this.tbContent.Text = dataTable.Rows[0]["v_bz"].ToString();
							this.tbSerial.Text = dataTable.Rows[0]["i_xh"].ToString();
							this.hdnSerial.Value = dataTable.Rows[0]["i_xh"].ToString();
							this.hdnDept.Value = dataTable.Rows[0]["i_bmdm"].ToString();
							string text2 = this.hdnDept.Value.ToString();
							DeptManageDb deptManageDb2 = new DeptManageDb();
							DataTable deptmentDetail2 = deptManageDb2.GetDeptmentDetail(Convert.ToInt32(text2));
							ListItem item2 = new ListItem("返回上一级", deptmentDetail2.Rows[0]["i_sjdm"].ToString());
							DataTable allSameLevelDept2 = deptManageDb2.GetAllSameLevelDept(Convert.ToInt32(text2));
							this.ddlDept.DataSource = allSameLevelDept2;
							this.ddlDept.DataTextField = "v_bmmc";
							this.ddlDept.DataValueField = "i_bmdm";
							this.ddlDept.DataBind();
							this.ddlDept.SelectedValue = text2;
							if (Convert.ToInt32(allSameLevelDept2.Rows[0]["i_sjdm"].ToString()) != 0)
							{
								this.ddlDept.Items.Insert(allSameLevelDept2.Rows.Count, item2);
								return;
							}
						}
					}
				}
				else
				{
					this.btnClose.Visible = false;
					this.btnConfirm.Visible = false;
					this.tbName.Visible = false;
					this.rblSex.Visible = false;
					this.ddlDept.Visible = false;
					this.tbDuty.Visible = false;
					this.tbAddress.Visible = false;
					this.tbPostalcode.Visible = false;
					this.tbCorpPhone.Visible = false;
					this.tbFax.Visible = false;
					this.tbHomePhone.Visible = false;
					this.cbZdbs.Visible = false;
					this.tbHandset.Visible = false;
					this.cbSjbs.Visible = false;
					this.tbEmail.Visible = false;
					this.tbQQ.Visible = false;
					this.tbContent.Visible = false;
					DataTable dataTable2 = this.ald.cGetLinkman(base.Request.QueryString["id"].ToString());
					this.Table1.Rows[1].Cells[1].InnerText = dataTable2.Rows[0]["v_xm"].ToString();
					this.Table1.Rows[2].Cells[1].InnerText = ((dataTable2.Rows[0]["c_xb"].ToString() == "f") ? "女" : "男");
					this.Table1.Rows[3].Cells[1].InnerText = this.ald.depName(dataTable2.Rows[0]["i_bmdm"].ToString());
					this.Table1.Rows[4].Cells[1].InnerText = dataTable2.Rows[0]["i_xh"].ToString();
					this.Table1.Rows[5].Cells[1].InnerText = dataTable2.Rows[0]["v_zw"].ToString();
					this.Table1.Rows[6].Cells[1].InnerText = dataTable2.Rows[0]["v_jtzz"].ToString();
					this.Table1.Rows[7].Cells[1].InnerText = dataTable2.Rows[0]["v_yzbm"].ToString();
					this.Table1.Rows[8].Cells[1].InnerText = dataTable2.Rows[0]["v_bgdh"].ToString();
					this.Table1.Rows[9].Cells[1].InnerText = dataTable2.Rows[0]["v_cz"].ToString();
					this.Table1.Rows[10].Cells[1].InnerText = ((dataTable2.Rows[0]["c_zdbs"].ToString() == "n") ? dataTable2.Rows[0]["v_zzdh"].ToString() : "-");
					this.Table1.Rows[11].Cells[1].InnerText = ((dataTable2.Rows[0]["c_sjbs"].ToString() == "n") ? dataTable2.Rows[0]["v_sj"].ToString() : "-");
					this.Table1.Rows[12].Cells[1].InnerText = dataTable2.Rows[0]["v_dzyx"].ToString();
					this.Table1.Rows[13].Cells[1].InnerText = dataTable2.Rows[0]["v_wlch"].ToString();
					this.Table1.Rows[14].Cells[1].InnerText = dataTable2.Rows[0]["v_bz"].ToString();
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
		private void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
		{
			string value = this.ddlDept.SelectedItem.Value;
			if (this.ddlDept.SelectedItem.Text == "返回上一级")
			{
				this.ReCreateDeptList(Convert.ToInt32(value), "parent");
				return;
			}
			this.ReCreateDeptList(Convert.ToInt32(value), "children");
		}
		private void ReCreateDeptList(int iDeptID, string flag)
		{
			DeptManageDb deptManageDb = new DeptManageDb();
			DataTable dataTable = new DataTable();
			if (flag == "parent")
			{
				dataTable = deptManageDb.GetAllParentLevelDept(iDeptID);
			}
			else
			{
				if (flag == "children")
				{
					dataTable = deptManageDb.GetSubDepartment(iDeptID);
				}
			}
			if (dataTable.Rows.Count > 0)
			{
				this.ddlDept.Items.Clear();
				ListItem item = new ListItem("返回上一级", dataTable.Rows[0]["i_sjdm"].ToString());
				this.ddlDept.DataSource = dataTable;
				this.ddlDept.DataTextField = "v_bmmc";
				this.ddlDept.DataValueField = "i_bmdm";
				this.ddlDept.DataBind();
				if (flag == "parent")
				{
					this.ddlDept.SelectedValue = Convert.ToString(iDeptID);
				}
				else
				{
					this.ddlDept.Items[0].Selected = true;
					iDeptID = Convert.ToInt32(this.ddlDept.Items[0].Value);
				}
				if (Convert.ToInt32(dataTable.Rows[0]["i_sjdm"].ToString()) != 0)
				{
					this.ddlDept.Items.Insert(dataTable.Rows.Count, item);
				}
			}
		}
		protected void btnConfirm_Click(object sender, EventArgs e)
		{
			this.js.Text = "alert(top.ui.callback);";
			string yhdm = this.Page.Session["yhdm"].ToString();
			string xm = this.tbName.Text.Trim();
			int bmdm = Convert.ToInt32(this.ddlDept.SelectedValue);
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
			string xh = this.tbSerial.Text.Trim();
			if (!(base.Request.QueryString["Op"].ToString() == "Add"))
			{
				if (base.Request.QueryString["Op"].ToString() == "Mod")
				{
					string xhFlag = "y";
					if (this.tbSerial.Text.ToString() == this.hdnSerial.Value.ToString())
					{
						xhFlag = "n";
					}
					string bmFlag = "y";
					if (this.ddlDept.SelectedItem.Value == this.hdnDept.Value.ToString())
					{
						bmFlag = "n";
					}
					int id = Convert.ToInt32(base.Request.QueryString["id"].ToString());
					if (!this.ald.cUpdateLinkman(id, xm, bmFlag, bmdm, zw, jtzz, yzbm, bgdh, zzdh, zdbs, sj, sjbs, cz, selectedValue, dzyx, wlch, text, xhFlag, this.tbSerial.Text.ToString()))
					{
						this.js.Text = "top.ui.alert('更新失败！');top.ui.closeWin();";
						return;
					}
					this.js.Text = "top.ui.closeWin(); top.ui.callback('" + base.Request.QueryString["iDept"].ToString() + "');";
				}
				return;
			}
			if (!this.ald.cAddLinkman(yhdm, xm, bmdm, zw, jtzz, yzbm, bgdh, zzdh, zdbs, sj, sjbs, cz, selectedValue, dzyx, wlch, text, xh))
			{
				this.js.Text = "top.ui.alert('增加失败！');top.ui.closeWin();";
				return;
			}
			this.js.Text = " top.ui.closeWin();top.ui.callback('" + base.Request.QueryString["iDept"].ToString() + "');";
		}
		protected void ddlDept_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			string value = this.ddlDept.SelectedItem.Value;
			if (this.ddlDept.SelectedItem.Text == "返回上一级")
			{
				this.ReCreateDeptList(Convert.ToInt32(value), "parent");
				return;
			}
			this.ReCreateDeptList(Convert.ToInt32(value), "children");
		}
		protected void hdnDept_ServerChange(object sender, EventArgs e)
		{
		}
	}


