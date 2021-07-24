using ASP;
using cn.justwin.Ent_Ept_EquipmentsBLL;
using cn.justwin.stockBLL;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EquipAccountEdit : PageBase, IRequiresSessionState
	{
		public ResourceTypeManage manage = new ResourceTypeManage();
		private EquipmentsBLL BLL = new EquipmentsBLL();
		private CodingAction CodingAct = new CodingAction();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.hdnProjectCode.Value = "";
				this.dataBinddrpState();
				if (base.Request["ec"] == null || base.Request["type"] == null)
				{
					this.JS.Text = "alert('参数错误！');window.close()";
					return;
				}
				this.ViewState["OPTYPE"] = int.Parse(base.Request["type"]);
				if ((int)this.ViewState["OPTYPE"] == 1)
				{
					this.ViewState["EQUIPMENTUNIQUECODE"] = Guid.NewGuid();
				}
				else
				{
					this.ViewState["EQUIPMENTUNIQUECODE"] = new Guid(base.Request["ec"]);
				}
				if ((int)this.ViewState["OPTYPE"] == 0)
				{
					this.RestoreEquipmentInfo((Guid)this.ViewState["EQUIPMENTUNIQUECODE"]);
				}
			}
		}
		private void RestoreEquipmentInfo(Guid equipmentUniqueCode)
		{
			EquipmentInfo singleEquipmentInfo = EquipmentAction.GetSingleEquipmentInfo(equipmentUniqueCode);
			this.hdnEquipmentType.Value = singleEquipmentInfo.EquipmentType;
			DataTable list = this.manage.GetList("ResourceTypeCode='" + singleEquipmentInfo.EquipmentType + "'");
			if (list != null)
			{
				if (list.Rows.Count > 0)
				{
					this.txtEquipmentType.Value = list.Rows[0]["ResourceTypeName"].ToString();
				}
				else
				{
					this.txtEquipmentType.Value = "";
				}
			}
			this.txtManCode.Value = singleEquipmentInfo.EquipmentManCode.ToString();
			this.txtEquipName.Text = singleEquipmentInfo.EquipmentName.ToString();
			this.txtSpec.Text = singleEquipmentInfo.Spec.ToString();
			this.txtPrecision.Text = singleEquipmentInfo.ThePrecision.ToString();
			this.txtFactory.Text = singleEquipmentInfo.Manufacturer.ToString();
			this.txtFacCode.Text = singleEquipmentInfo.FactoryCode.ToString();
			this.DateFactory.Text = singleEquipmentInfo.FactoryDate.ToShortDateString();
			this.DatePursh.Text = singleEquipmentInfo.PurchaseDate.ToShortDateString();
			this.txtCheck.Text = singleEquipmentInfo.CheckCycle.ToString();
			this.txtSerYear.Text = singleEquipmentInfo.ServiceYear.ToString();
			this.txtOriginal.Text = singleEquipmentInfo.OriginalPrice.ToString();
			this.drpState.SelectedValue = singleEquipmentInfo.State.ToString();
			this.txtRemark.Text = singleEquipmentInfo.Remark;
			this.txtdepreciation.Text = singleEquipmentInfo.depreciation.ToString();
			this.HdnContractDept.Value = singleEquipmentInfo.ContactDept;
			this.txtProject.Value = this.BLL.projectName(singleEquipmentInfo.EquipmentUniqueCode.ToString());
			try
			{
				this.TxtContractDept.Value = com.jwsoft.pm.entpm.PageHelper.QueryCorp(this, int.Parse(singleEquipmentInfo.ContactDept));
			}
			catch
			{
			}
		}
		private EquipmentInfo GatherEquipmentInfo()
		{
			EquipmentInfo equipmentInfo = new EquipmentInfo();
			equipmentInfo.EquipmentType = this.hdnEquipmentType.Value;
			equipmentInfo.EquipmentManCode = this.txtManCode.Value.Trim();
			equipmentInfo.EquipmentName = this.txtEquipName.Text.Trim();
			equipmentInfo.Spec = this.txtSpec.Text.Trim();
			equipmentInfo.ThePrecision = this.txtPrecision.Text.Trim();
			equipmentInfo.Manufacturer = this.txtFactory.Text.Trim();
			equipmentInfo.FactoryCode = this.txtFacCode.Text.Trim();
			if (this.DateFactory.Text.Trim() == "")
			{
				equipmentInfo.FactoryDate = Convert.ToDateTime(DateTime.MinValue.ToShortTimeString());
			}
			else
			{
				equipmentInfo.FactoryDate = Convert.ToDateTime(this.DateFactory.Text);
			}
			if (this.DatePursh.Text.Trim() == "")
			{
				equipmentInfo.PurchaseDate = Convert.ToDateTime(DateTime.MinValue.ToShortTimeString());
			}
			else
			{
				equipmentInfo.PurchaseDate = Convert.ToDateTime(this.DatePursh.Text);
			}
			equipmentInfo.CheckCycle = int.Parse(this.txtCheck.Text.Trim());
			equipmentInfo.ServiceYear = int.Parse(this.txtSerYear.Text.Trim());
			equipmentInfo.OriginalPrice = Convert.ToDecimal(this.txtOriginal.Text);
			equipmentInfo.State = int.Parse(this.drpState.SelectedValue);
			equipmentInfo.Remark = this.txtRemark.Text.Trim();
			equipmentInfo.depreciation = decimal.Parse(this.txtdepreciation.Text);
			equipmentInfo.ContactDept = this.HdnContractDept.Value.Trim();
			return equipmentInfo;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			if ((int)this.ViewState["OPTYPE"] == 1)
			{
				EquipmentInfo equipmentInfo = this.GatherEquipmentInfo();
				equipmentInfo.EquipmentUniqueCode = Guid.NewGuid();
				int num = EquipmentAction.AddEquipment(equipmentInfo);
				if (num == 1)
				{
					this.BLL.Add(equipmentInfo.EquipmentUniqueCode.ToString(), this.hdnProjectCode.Value.Trim().ToString());
					this.JS.Text = " parent.desktop.flowclass.location='/EPC/EquipmentManagement/Account/equipaccountlist.aspx';alert('保存成功!');top.frameWorkArea.window.desktop.getActive().close();";
					return;
				}
				this.JS.Text = "alert('保存出错，请与管理员联系！');";
				return;
			}
			else
			{
				EquipmentInfo equipmentInfo2 = this.GatherEquipmentInfo();
				equipmentInfo2.EquipmentUniqueCode = (Guid)this.ViewState["EQUIPMENTUNIQUECODE"];
				int num2 = EquipmentAction.UpdEquipment(equipmentInfo2);
				if (num2 == 1)
				{
					if (this.BLL.Exists(equipmentInfo2.EquipmentUniqueCode.ToString()))
					{
						if (this.hdnProjectCode.Value.ToString() != "")
						{
							this.BLL.Update(equipmentInfo2.EquipmentUniqueCode.ToString(), this.hdnProjectCode.Value.Trim().ToString());
						}
					}
					else
					{
						this.BLL.Add(equipmentInfo2.EquipmentUniqueCode.ToString(), this.hdnProjectCode.Value.Trim().ToString());
					}
					this.JS.Text = " parent.desktop.flowclass.location='/EPC/EquipmentManagement/Account/equipaccountlist.aspx';alert('保存成功!');top.frameWorkArea.window.desktop.getActive().close();";
					return;
				}
				this.JS.Text = "alert('保存出错，请与管理员联系！');";
				return;
			}
		}
		private void dataBinddrpState()
		{
			DataTable dataFromCollection = this.GetDataFromCollection(this.CodingAct.QueryCodeInfoList(152, ValidState.Valid));
			DataRow[] array = dataFromCollection.Select("", "CodeID asc");
			DataTable dataTable = dataFromCollection.Clone();
			dataTable.Rows.Clear();
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow row = array2[i];
				dataTable.ImportRow(row);
			}
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				this.drpState.DataTextField = "CodeName";
				this.drpState.DataValueField = "CodeID";
				this.drpState.DataSource = dataTable;
				this.drpState.DataBind();
			}
		}
		private DataTable GetDataFromCollection(CodingInfoCollection colt)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("CodeID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("TypeID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("ParentCodeID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("ParentCodeList", typeof(string)));
			dataTable.Columns.Add(new DataColumn("CodeName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("ChildNumber", typeof(int)));
			dataTable.Columns.Add(new DataColumn("IsDefault", typeof(bool)));
			dataTable.Columns.Add(new DataColumn("IsFixed", typeof(bool)));
			int count = colt.Count;
			for (int i = 0; i < count; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["CodeID"] = colt[i].CodeID.ToString();
				dataRow["TypeID"] = colt[i].TypeID.ToString();
				dataRow["ParentCodeID"] = colt[i].ParentCodeID.ToString();
				dataRow["ParentCodeList"] = colt[i].ParentCodeList.ToString();
				dataRow["CodeName"] = colt[i].CodeName;
				dataRow["ChildNumber"] = colt[i].ChildNumber;
				dataRow["IsDefault"] = colt[i].IsDefault;
				dataRow["IsFixed"] = colt[i].IsFixed;
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}
	}


