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
	public partial class AttemperAdd : PageBase, IRequiresSessionState
	{
		private EquipmentAttemperAction EquipmentAttemper = new EquipmentAttemperAction();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["AttemperID"] != null)
				{
					this.ViewState["ATTEMPERID"] = Convert.ToInt32(base.Request["AttemperID"]);
				}
				else
				{
					this.ViewState["ATTEMPERID"] = 0;
				}
				if (base.Request["equipementcode"] != null)
				{
					this.ViewState["EQUIPMENTUNIQUECODE"] = new Guid(base.Request["equipementcode"]);
				}
				else
				{
					this.ViewState["EQUIPMENTUNIQUECODE"] = Guid.Empty;
				}
				if (base.Request["type"] != null)
				{
					this.ViewState["TYPE"] = base.Request["type"];
				}
				string a;
				if ((a = this.ViewState["TYPE"].ToString()) != null)
				{
					if (a == "ADD")
					{
						this.btnAdd.Visible = true;
						this.txtStarDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
						this.txtEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
						this.txtAttemperCode.Text = string.Concat(new string[]
						{
							DateTime.Now.Year.ToString(),
							DateTime.Now.Month.ToString().PadLeft(2, '0'),
							DateTime.Now.Day.ToString().PadLeft(2, '0'),
							DateTime.Now.Hour.ToString().PadLeft(2, '0'),
							DateTime.Now.Minute.ToString().PadLeft(2, '0'),
							DateTime.Now.Second.ToString().PadLeft(2, '0')
						});
						return;
					}
					if (a == "EDIT")
					{
						this.btnAdd.Visible = true;
						this.GetAttempterkInfo((int)this.ViewState["ATTEMPERID"]);
						return;
					}
					if (!(a == "SEE"))
					{
						return;
					}
					this.GetAttempterkInfo((int)this.ViewState["ATTEMPERID"]);
					this.btnAdd.Visible = false;
				}
			}
		}
		private void GetAttempterkInfo(int ID)
		{
			EquipmentAttemperInfo equipmentAttemperInfo = new EquipmentAttemperInfo();
			equipmentAttemperInfo = EquipmentAttemperAction.GetEquipmentAttemperInfo(ID);
			this.txtAttemperCode.Text = equipmentAttemperInfo.AttemperBillCode;
			this.hdnProjectCode.Value = equipmentAttemperInfo.ToProjectUniqueCode.ToString();
			this.txtToproject.Text = PMAction.GetNameByCode(equipmentAttemperInfo.ToProjectUniqueCode.ToString()).ToString();
			this.txtIntendingTime.Text = equipmentAttemperInfo.IntendingTime.ToString();
			this.txtPrice.Text = equipmentAttemperInfo.UnitPrice.ToString();
			this.txtStarDate.Text = equipmentAttemperInfo.BeginDate.ToString("yyyy-MM-dd");
			this.txtEndDate.Text = equipmentAttemperInfo.EndDate.ToString("yyyy-MM-dd");
			this.txtSuite.Text = equipmentAttemperInfo.Suite;
			this.txtRemark.Text = equipmentAttemperInfo.Remark;
		}
		private EquipmentAttemperInfo GatherEquipmentAttemperInfo()
		{
			return new EquipmentAttemperInfo
			{
				NoteSequenceID = (int)this.ViewState["ATTEMPERID"],
				AttemperBillCode = this.txtAttemperCode.Text.Trim(),
				EquipmentUniqueCode = (Guid)this.ViewState["EQUIPMENTUNIQUECODE"],
				ToProjectUniqueCode = new Guid(this.hdnProjectCode.Value),
				Suite = this.txtSuite.Text.Trim(),
				IntendingTime = Convert.ToInt32(this.txtIntendingTime.Text.Trim()),
				UnitPrice = Convert.ToDecimal(this.txtPrice.Text),
				BeginDate = Convert.ToDateTime(this.txtStarDate.Text.Trim()),
				EndDate = Convert.ToDateTime(this.txtEndDate.Text.Trim()),
				Remark = this.txtRemark.Text.Trim()
			};
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			string a;
			if ((a = this.ViewState["TYPE"].ToString()) != null)
			{
				if (!(a == "ADD"))
				{
					if (!(a == "EDIT"))
					{
						if (!(a == "SEE"))
						{
							return;
						}
					}
					else
					{
						if (this.EquipmentAttemper.EDITAttemper(this.GatherEquipmentAttemperInfo()) != 1)
						{
							this.js.Text = "alert('更新失败！');";
							return;
						}
						this.js.Text = "alert('更新成功！');window.returnValue=true;window.close();";
					}
				}
				else
				{
					if (this.EquipmentAttemper.AddAttemper(this.GatherEquipmentAttemperInfo()) != 1)
					{
						this.js.Text = "alert('增加失败！');";
						return;
					}
					this.js.Text = "alert('增加成功！');window.returnValue=true;window.close();";
					return;
				}
			}
		}
	}


