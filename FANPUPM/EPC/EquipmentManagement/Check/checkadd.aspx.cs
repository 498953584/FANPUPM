using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CheckAdd : PageBase, IRequiresSessionState
	{
		
		protected int intCheckId;
		protected string _strEquimentId = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["checkid"] != null)
				{
					this.ViewState["CHECKID"] = Convert.ToInt32(base.Request["checkId"]);
				}
				else
				{
					this.ViewState["CHECKID"] = 0;
				}
				if (base.Request["ec"] != null)
				{
					this.ViewState["EQUIPMENTUNIQUECODE"] = new Guid(base.Request["ec"]);
				}
				else
				{
					this.ViewState["EQUIPMENTUNIQUECODE"] = Guid.Empty;
				}
				if (base.Request["type"] != null)
				{
					this.ViewState["TYPE"] = base.Request["type"];
				}
				this.txtTime.Text = DateTime.Today.ToString("yyyy-MM-dd");
				string a;
				if ((a = this.ViewState["TYPE"].ToString()) != null)
				{
					if (a == "ADD")
					{
						this.btnAdd.Visible = true;
						return;
					}
					if (a == "EDIT")
					{
						this.btnAdd.Visible = true;
						this.txtTime.Enabled = false;
						this.RestoreCheckInfo((int)this.ViewState["CHECKID"]);
						return;
					}
					if (!(a == "SEE"))
					{
						return;
					}
					this.RestoreCheckInfo((int)this.ViewState["CHECKID"]);
					this.btnAdd.Visible = false;
				}
			}
		}
		private void RestoreCheckInfo(int ID)
		{
			EquipmentCheckInfo equipmentCheckInfo = new EquipmentCheckInfo();
			equipmentCheckInfo = EquipmentCheckAction.GetSingleEquipmentCheckInfo(ID);
			this.txtCode.Text = equipmentCheckInfo.CheckBillCode;
			this.txtTime.Text = equipmentCheckInfo.CheckDate.ToString("yyyy-MM-dd");
			this.txtDept.Text = equipmentCheckInfo.CheckDept;
			this.txtPerson.Text = equipmentCheckInfo.CheckPerson;
			this.txtRemark.Text = equipmentCheckInfo.Remark;
			this.txtDescript.Text = equipmentCheckInfo.ResultDescript;
			if (equipmentCheckInfo.InOrOut == 0)
			{
				this.RbtTypeIn.Checked = true;
			}
			else
			{
				this.RbtTypeOut.Checked = true;
			}
			if (equipmentCheckInfo.Result == 1)
			{
				this.RbtResultOk.Checked = true;
			}
			else
			{
				this.RbtResultNo.Checked = true;
			}
			this.ViewState["EQUIPMENTUNIQUECODE"] = equipmentCheckInfo.EquipmentUniqueCode;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private EquipmentCheckInfo GatherEquipmentCheckInfo()
		{
			int inOrOut;
			if (this.RbtTypeIn.Checked)
			{
				inOrOut = 0;
			}
			else
			{
				inOrOut = 1;
			}
			int result;
			if (this.RbtResultOk.Checked)
			{
				result = 1;
			}
			else
			{
				result = 2;
			}
			return new EquipmentCheckInfo
			{
				NoteSequenceID = (int)this.ViewState["CHECKID"],
				CheckBillCode = this.txtCode.Text.Trim(),
				CheckDate = Convert.ToDateTime(this.txtTime.Text.Trim()),
				CheckDept = this.txtDept.Text.Trim(),
				CheckPerson = this.txtPerson.Text.Trim(),
				Remark = this.txtRemark.Text.Trim(),
				ResultDescript = this.txtDescript.Text.Trim(),
				IsLast = 1,
				Result = result,
				InOrOut = inOrOut,
				EquipmentUniqueCode = (Guid)this.ViewState["EQUIPMENTUNIQUECODE"]
			};
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			if ((int)this.ViewState["CHECKID"] == 0)
			{
				if (EquipmentCheckAction.AddEquipmentCheck(this.GatherEquipmentCheckInfo()) != 1)
				{
					this.js.Text = "alert('增加失败！');";
					return;
				}
				EquipmentCheckInfo equipmentCheckInfo = this.GatherEquipmentCheckInfo();
				EquipmentInfo singleEquipmentInfo = EquipmentAction.GetSingleEquipmentInfo(equipmentCheckInfo.EquipmentUniqueCode);
				string mes = string.Concat(new string[]
				{
					"设备检定通知：编号为",
					singleEquipmentInfo.EquipmentManCode,
					"的",
					singleEquipmentInfo.EquipmentName,
					"，本次检定日期于一个月后到期。"
				});
				DateTime time = equipmentCheckInfo.CheckDate.AddMonths(singleEquipmentInfo.CheckCycle).AddMonths(-1);
				this.getOrganiger(equipmentCheckInfo.EquipmentUniqueCode.ToString(), mes, base.UserCode.ToString(), time);
				this.js.Text = "alert('增加成功！');window.returnValue=true;window.close();";
				return;
			}
			else
			{
				if (EquipmentCheckAction.EditEquipmentCheck(this.GatherEquipmentCheckInfo()) != 1)
				{
					this.js.Text = "alert('更新失败！');";
					return;
				}
				this.js.Text = "alert('更新成功！');window.returnValue=true;window.close();";
				return;
			}
		}
		private void getOrganiger(string xgid, string Mes, string jsyhdm, DateTime Time)
		{
			PublicInterface.SendSysMsg(new PTDBSJ
			{
				V_LXBM = "023",
				I_XGID = xgid,
				DTM_DBSJ = Time,
				V_Content = Mes,
				V_DBLJ = "Epc/EquipmentManagement/Check/CheckView.aspx?Type=023&Guid=" + xgid,
				V_YHDM = jsyhdm,
				V_TPLJ = "new_Mail.gif",
				C_OpenFlag = "1"
			});
		}
	}


