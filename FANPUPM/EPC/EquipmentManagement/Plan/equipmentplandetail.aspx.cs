using ASP;
using cn.justwin.DAL;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EquipmentPlanDetail : PageBase, IRequiresSessionState
	{
		private EquipmentAction equipmentAction = new EquipmentAction();
		private string PlanCode;
		private OpType _OpType;
		protected string strPrjCode = "";
		protected string strSubPrjName = "";
		protected string strSubPrjReg = "";
		private string _strGuid = "6A1A7050-1F92-4291-B932-43E1DFCE6E92";
		private MeterialPlanStock materialStock = new MeterialPlanStock();

		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!this.Page.IsPostBack)
			{
				if (this.Session["EquipmentPROJECTCODE"] != null)
				{
					this.strSubPrjReg = this.Session["EquipmentPROJECTCODE"].ToString();
				}
				if (this.Session["EquipmentPROJECTNAME"] != null)
				{
					this.strSubPrjName = this.Session["EquipmentPROJECTNAME"].ToString();
				}
				else
				{
					this.btnSave.Enabled = false;
					this.txtPrjName.Text = "请选择项目后再新增！";
					this.txtPrjName.ForeColor = Color.Red;
				}
				this.txtPrjName.Text = this.strSubPrjName;
				if (base.Request.Params["t"] == null || base.Request.Params["ci"] == null)
				{
					if (base.Request.Params["ic"] != null && base.Request.Params["ic"].ToString() != "")
					{
						this.resWfplan(base.Request.Params["ic"].ToString());
						this.trhide.Style.Add("display", "none");
						return;
					}
					this.js.Text = "alert('参数错误！');window.returnValue = false;window.close();";
					return;
				}
				else
				{
					this._OpType = (OpType)Enum.Parse(typeof(OpType), base.Request.Params["t"]);
					this.ViewState["optype"] = this._OpType.ToString();
					if (this._OpType != OpType.Add)
					{
						this.hdfCode.Value = base.Request.Params["ci"].ToString();
					}
					switch (this._OpType)
					{
					case OpType.Add:
					{
						this.txtPlanCode.Text = string.Concat(new string[]
						{
							DateTime.Now.Year.ToString(),
							DateTime.Now.Month.ToString().PadLeft(2, '0'),
							DateTime.Now.Day.ToString().PadLeft(2, '0'),
							DateTime.Now.Hour.ToString().PadLeft(2, '0'),
							DateTime.Now.Minute.ToString().PadLeft(2, '0'),
							DateTime.Now.Second.ToString().PadLeft(2, '0')
						});
						this.hdfCode.Value = this.txtPlanCode.Text;
						this.calExitDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
						this.calPlanCreatTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
						this.calEnterDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
						userManageDb userManageDb = new userManageDb();
						this.txtPlanMaker.Text = userManageDb.GetUserName(this.Session["yhdm"].ToString());
						this.BindDataGrid(new ArrayList());
						return;
					}
					case OpType.Upd:
						new EquipmentAction();
						this.RestoreEquipmentPlan(this.hdfCode.Value);
						this.txtPlanCode.Enabled = false;
						return;
					default:
						this.RestoreEquipmentPlan(this.hdfCode.Value);
						this.txtPlanCode.Enabled = false;
						this.btnDel.Visible = false;
						this.btnSave.Visible = false;
						this.calEnterDate.Enabled = false;
						this.calExitDate.Enabled = false;
						this.calPlanCreatTime.Enabled = false;
						this.txtPlanMaker.Enabled = false;
						this.txtRemark.Enabled = false;
						break;
					}
				}
			}
		}
		private void BindDataGrid(ArrayList al)
		{
			this.grdDetail.DataSource = al;
			this.grdDetail.DataBind();
			this.Session["MACHINE_MACHINE_USE_CST"] = al;
		}
		private void RestoreEquipmentPlan(string PlanCode)
		{
			EquipmentPlanInfo equipmentPlanInfo = new EquipmentPlanInfo();
			equipmentPlanInfo = this.equipmentAction.GetSingleEquipmentPlan(this.hdfCode.Value);
			this.txtPlanCode.Text = equipmentPlanInfo.PlanCode;
			this.txtPlanMaker.Text = equipmentPlanInfo.PlanMaker;
			this.txtRemark.Text = equipmentPlanInfo.Remark;
			this.calPlanCreatTime.Text = equipmentPlanInfo.PlanCreatTime.ToShortDateString();
			this.calEnterDate.Text = equipmentPlanInfo.EnterDate.ToShortDateString();
			this.calExitDate.Text = equipmentPlanInfo.ExitDate.ToShortDateString();
			this.ViewState["ResourcesTable"] = this.equipmentAction.GetResourceByPlanCode(this.hdfCode.Value);
			this.grdDetail.DataSource = this.ViewState["ResourcesTable"];
			this.grdDetail.DataBind();
		}
		private void resWfplan(string wfcode)
		{
			EquipmentPlanInfo equipmentPlanInfo = new EquipmentPlanInfo();
			equipmentPlanInfo = this.equipmentAction.GetwfPlanforGuid(wfcode);
			this.txtPlanCode.Text = equipmentPlanInfo.PlanCode;
			this.txtPlanMaker.Text = equipmentPlanInfo.PlanMaker;
			this.txtRemark.Text = equipmentPlanInfo.Remark;
			this.calPlanCreatTime.Text = equipmentPlanInfo.PlanCreatTime.ToShortDateString();
			this.calEnterDate.Text = equipmentPlanInfo.EnterDate.ToShortDateString();
			this.calExitDate.Text = equipmentPlanInfo.ExitDate.ToShortDateString();
			this.ViewState["ResourcesTable"] = this.equipmentAction.GetResourceByPlanCode(this.hdfCode.Value);
			this.grdDetail.DataSource = this.ViewState["ResourcesTable"];
			this.grdDetail.DataBind();
		}
		private EquipmentPlanInfo GatherEquipmentPlanInfo()
		{
			return new EquipmentPlanInfo
			{
				PlanCode = this.txtPlanCode.Text.Trim(),
				PlanCreatTime = Convert.ToDateTime(this.calPlanCreatTime.Text),
				EnterDate = Convert.ToDateTime(this.calEnterDate.Text),
				ExitDate = Convert.ToDateTime(this.calExitDate.Text),
				PlanMaker = this.txtPlanMaker.Text.Trim(),
				PrjCode = new Guid(this.Session["EquipmentPROJECTCODE"].ToString()),
				Remark = this.txtRemark.Text.Trim(),
				IsAuditing = -1,
				IsFullUsed = 0
			};
		}
		private ArrayList grdDetail_GetData()
		{
			ArrayList arrayList = (ArrayList)this.Session["MACHINE_MACHINE_USE_CST"];
			for (int i = 0; i < this.grdDetail.Items.Count; i++)
			{
				DataGridItem dataGridItem = this.grdDetail.Items[i];
				Label label = (Label)dataGridItem.FindControl("labCode");
				TextBox textBox = (TextBox)dataGridItem.FindControl("txtEquipmentCount");
				TextBox textBox2 = (TextBox)dataGridItem.FindControl("txtRemarkDetail");
				((EquipmentPlanDetailInfo)arrayList[i]).EquipmentCode = label.Text;
				((EquipmentPlanDetailInfo)arrayList[i]).EquipmentCount = Convert.ToDecimal(textBox.Text);
				((EquipmentPlanDetailInfo)arrayList[i]).Remark = textBox2.Text;
			}
			return arrayList;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.grdDetail.ItemDataBound += new DataGridItemEventHandler(this.grdDetail_ItemDataBound);
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			if (this.IsInfoValidator())
			{
				this.Plan_add();
			}
		}
		private void Plan_add()
		{
			DataTable dataTable = new DataTable();
			this.Session["yhdm"].ToString();
			dataTable = EquipmentAction.getEquPlanTempRes(new Guid(this._strGuid), this.Session["yhdm"].ToString(), this.txtPlanCode.Text.Trim());
			ArrayList arrayList = new ArrayList();
			EquipmentPlanDetailInfo equipmentPlanDetailInfo = new EquipmentPlanDetailInfo();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				arrayList.Add(new EquipmentPlanDetailInfo
				{
					EquipmentCode = dataTable.Rows[i]["ResourceCode"].ToString(),
					EquipmentName = dataTable.Rows[i]["ResourceName"].ToString(),
					EquipmentCount = Convert.ToDecimal(dataTable.Rows[i]["EquipmentCount"].ToString()),
					Remark = dataTable.Rows[i]["Remark"].ToString()
				});
			}
			this.BindDataGrid(arrayList);
		}
		private bool IsInfoValidator()
		{
			foreach (Control control in this.pnlPubInfo.Controls)
			{
				if (control is IValidator)
				{
					IValidator validator = (IValidator)control;
					validator.Validate();
				}
			}
			return true;
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			this.delRow();
		}
		protected void delRow()
		{
			DataTable dataTable = (DataTable)this.ViewState["ResourcesTable"];
			if (this.grdDetail.Items.Count > 0)
			{
				for (int i = this.grdDetail.Items.Count - 1; i >= 0; i--)
				{
					DataGridItem dataGridItem = this.grdDetail.Items[i];
					CheckBox checkBox = (CheckBox)dataGridItem.FindControl("chkBox");
					if (checkBox.Checked)
					{
						dataTable.Rows.RemoveAt(dataGridItem.ItemIndex);
					}
				}
				this.ViewState["ResourcesTable"] = dataTable;
				this.grdDetail.DataSource = (DataTable)this.ViewState["ResourcesTable"];
				this.grdDetail.DataBind();
			}
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (this.txtPlanCode.Text.Trim().Length == 0)
			{
				this.js.Text = "alert('计划编号不能为空！')";
				return;
			}
			if (this.txtPlanMaker.Text.Trim().Length == 0)
			{
				this.js.Text = "alert('计划制作人不能为空！')";
				return;
			}
			if (this.calPlanCreatTime.Text.Length == 0)
			{
				this.js.Text = "alert('计划时间不能为空！')";
				return;
			}
			if (this.calEnterDate.Text.Length == 0)
			{
				this.js.Text = "alert('进场时间不能为空！')";
				return;
			}
			if (this.calExitDate.Text.Length == 0)
			{
				this.js.Text = "alert('出场时间不能为空！')";
				return;
			}
			string text = " begin delete from Ent_Ept_PlanDetail where planCode='" + this.hdfCode.Value + "'";
			for (int i = 0; i < this.grdDetail.Items.Count; i++)
			{
				DataGridItem dataGridItem = this.grdDetail.Items[i];
				Label label = (Label)dataGridItem.FindControl("labCode");
				Label arg_10E_0 = (Label)dataGridItem.FindControl("labName");
				TextBox textBox = (TextBox)dataGridItem.FindControl("txtEquipmentCount");
				TextBox textBox2 = (TextBox)dataGridItem.FindControl("txtRemarkDetail");
				text += " insert into Ent_Ept_PlanDetail(PlanCode, EquipmentCode, EquipmentType, EquipmentCount,  Remark)";
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					" values('",
					this.hdfCode.Value,
					"','",
					label.Text,
					"',0,'",
					Convert.ToDecimal(textBox.Text),
					"','",
					textBox2.Text,
					"') "
				});
			}
			text += "  end ";
			publicDbOpClass.ExecSqlString(text);
			string a;
			if ((a = this.ViewState["optype"].ToString()) != null)
			{
				if (!(a == "Add"))
				{
					if (!(a == "Upd"))
					{
						return;
					}
					if (this.equipmentAction.UpdEquipmentPlan(this.GatherEquipmentPlanInfo()) != 1)
					{
						this.js.Text = "alert('更新失败！');";
						return;
					}
					this.js.Text = "alert('更新成功！');winclose('equipmentplandetail', 'equipmentplanlist.aspx', false)";
				}
				else
				{
					if (this.equipmentAction.AddEquipmentPlan(this.GatherEquipmentPlanInfo()) != 1)
					{
						this.js.Text = "alert('增加失败！');";
						return;
					}
					this.js.Text = "alert('增加成功！');winclose('equipmentplandetail', 'equipmentplanlist.aspx', false)";
					this.btnSave.Enabled = false;
					return;
				}
			}
		}
		private void grdDetail_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"clickRow('",
					this.grdDetail.ClientID,
					"',this,'",
					e.Item.ItemIndex.ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "overRow(this);";
				e.Item.Attributes["onmouseout"] = "outRow(this);";
				e.Item.Attributes["style"] = "cursor:hand";
				return;
			default:
				return;
			}
		}
		protected void btnBindResource_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.SelectResource1.ResourceId))
			{
				ISerializable serializable = new JsonSerializer();
				string[] array = serializable.Deserialize<string[]>(this.SelectResource1.ResourceId);
				if (array != null)
				{
					string resouceCodeById = this.equipmentAction.GetResouceCodeById(DBHelper.GetInParameterSql(array));
					DataTable dataTable = this.equipmentAction.GetResouceById(resouceCodeById);
					DataTable dataTable2 = this.ViewState["ResourcesTable"] as DataTable;
					if (dataTable2 != null)
					{
						dataTable2.PrimaryKey = new DataColumn[]
						{
							dataTable2.Columns["EquipmentCode"]
						};
						dataTable.PrimaryKey = new DataColumn[]
						{
							dataTable.Columns["EquipmentCode"]
						};
						dataTable2.Merge(dataTable, true);
						dataTable = dataTable2;
					}
					this.ViewState["ResourcesTable"] = dataTable;
					this.grdDetail.DataSource = dataTable;
					this.grdDetail.DataBind();
				}
			}
			this.SelectResource1.ResourceId = string.Empty;
		}
	}


