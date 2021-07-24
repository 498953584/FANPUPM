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
	public partial class EquipmentQuery : PageBase, IRequiresSessionState
	{
		private EquipmentAction ResA = new EquipmentAction();
		public ResourceTypeManage manage = new ResourceTypeManage();
		private EquipmentsBLL BLL = new EquipmentsBLL();
		private CodingAction CodingAct = new CodingAction();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.ViewState["deptId"] = "";
				this.ViewState["equipmentCode"] = "";
				this.ViewState["equipmentName"] = "";
				this.ViewState["equipmentType"] = "";
				this.ViewState["A"] = "";
				this.ViewState["B"] = "";
				this.dataBinddrpState();
				this.EquipmentBind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgEquipment.ItemDataBound += new DataGridItemEventHandler(this.dgEquipment_ItemDataBound);
		}
		private void EquipmentBind()
		{
			string deptId = this.ViewState["deptId"].ToString();
			string equipmentCode = this.ViewState["equipmentCode"].ToString();
			string equipmentName = this.ViewState["equipmentName"].ToString();
			string equipmentType = this.ViewState["equipmentType"].ToString();
			EquipmentsBLL equipmentsBLL = new EquipmentsBLL();
			string originalPriceA = this.ViewState["A"].ToString();
			string originalPriceB = this.ViewState["B"].ToString();
			int num = int.Parse(this.drpState.SelectedValue.ToString());
			string order = " state <10 order by EquipmentManualCode, PurchaseDate desc";
			if (num == 0)
			{
				order = " state <10 order by EquipmentManualCode, PurchaseDate desc";
			}
			DataTable list = equipmentsBLL.GetList(deptId, equipmentCode, equipmentName, equipmentType, originalPriceA, originalPriceB, num, order);
			this.dgEquipment.DataSource = list;
			this.dgEquipment.DataBind();
		}
		private void dgEquipment_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				object arg_2A_0 = e.Item.DataItem;
				e.Item.Cells[1].Text.ToString();
				string str = e.Item.Cells[4].Text.ToString();
				DataTable list = this.manage.GetList("ResourceTypeCode='" + str + "'");
				if (list != null)
				{
					if (list.Rows.Count > 0)
					{
						e.Item.Cells[4].Text = list.Rows[0]["ResourceTypeName"].ToString();
					}
					else
					{
						e.Item.Cells[4].Text = "";
					}
				}
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgEquipment.ClientID,
					"');clickEquipmentRow('",
					e.Item.Cells[12].Text.ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				if (e.Item.Cells[12].Text.ToString() != "")
				{
					e.Item.Cells[12].Text = this.BLL.projectName(e.Item.Cells[12].Text.ToString());
				}
				try
				{
					string b = e.Item.Cells[10].Text.ToString();
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
					if (dataTable.Rows.Count > 0)
					{
						for (int j = 0; j < dataTable.Rows.Count; j++)
						{
							if (dataTable.Rows[j]["CodeID"].ToString() == b)
							{
								e.Item.Cells[10].Text = dataTable.Rows[j]["CodeName"].ToString();
							}
						}
					}
				}
				catch
				{
					e.Item.Cells[10].Text = "正常状态";
				}
				try
				{
					int.Parse(e.Item.Cells[11].Text.ToString());
					e.Item.Cells[11].Text = com.jwsoft.pm.entpm.PageHelper.QueryCorp(this, int.Parse(e.Item.Cells[11].Text.ToString()));
				}
				catch
				{
					e.Item.Cells[11].Text = "自有机械器具";
				}
				return;
			}
			default:
				return;
			}
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.ViewState["deptId"] = "";
			if (this.txtEquipCode.Text.Length != 0)
			{
				this.ViewState["equipmentCode"] = this.txtEquipCode.Text.Trim();
			}
			else
			{
				this.ViewState["equipmentCode"] = "";
			}
			if (this.txtEquipName.Text.Length != 0)
			{
				this.ViewState["equipmentName"] = this.txtEquipName.Text.Trim();
			}
			else
			{
				this.ViewState["equipmentName"] = "";
			}
			if (this.txtEquipType.Text.Trim().Length != 0)
			{
				this.ViewState["equipmentType"] = this.hdnEquipmentType.Value.ToString().Trim();
			}
			else
			{
				this.ViewState["equipmentType"] = "";
			}
			if (this.TextBox1.Text.Trim().Length != 0)
			{
				this.ViewState["A"] = this.TextBox1.Text.ToString().Trim();
			}
			else
			{
				this.ViewState["A"] = "";
			}
			if (this.TextBox2.Text.Trim().Length != 0)
			{
				this.ViewState["B"] = this.TextBox2.Text.ToString().Trim();
			}
			else
			{
				this.ViewState["B"] = "";
			}
			this.EquipmentBind();
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgEquipment.CurrentPageIndex = e.NewPageIndex;
			this.EquipmentBind();
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
				this.drpState.Items.Insert(0, new ListItem("--请选择--", "0"));
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


