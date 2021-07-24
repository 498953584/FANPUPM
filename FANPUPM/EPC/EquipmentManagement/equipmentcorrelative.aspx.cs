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
	public partial class EquipmentCorrelative : PageBase, IRequiresSessionState
	{
		public int GoIndex;
		public static string showTitle = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["go"] == null)
				{
					this.js.Text = "alert('参数错误！');";
					return;
				}
				this.GoIndex = int.Parse(base.Request["go"]);
				if (!this.Page.IsPostBack)
				{
					switch (this.GoIndex)
					{
					case 1:
						EquipmentCorrelative.showTitle = "调度维护";
						this.btnNav.Text = "机械器具调度";
						this.btnNav.Attributes["onclick"] = "goWhere('Attemper/AttemperList.aspx',1);return false;";
						break;
					case 2:
						EquipmentCorrelative.showTitle = "检定维护";
						this.btnNav.Text = "机械器具检定";
						this.btnNav.Attributes["onclick"] = "goWhere('Check/CheckInfoList.aspx',1);return false;";
						break;
					case 3:
						EquipmentCorrelative.showTitle = "维修维护";
						this.btnNav.Text = "机械器具维护";
						this.btnNav.Attributes["onclick"] = "goWhere('Maintain/EquipmentMaintainList.aspx',1);return false;";
						break;
					}
				}
				this.ViewState["deptId"] = "";
				this.ViewState["equipmentCode"] = "";
				this.ViewState["equipmentName"] = "";
				this.ViewState["equipmentType"] = "";
				this.EquipmentBind();
			}
		}
		private void EquipmentBind()
		{
			string deptId = this.ViewState["deptId"].ToString();
			string equipmentCode = this.ViewState["equipmentCode"].ToString();
			string equipmentName = this.ViewState["equipmentName"].ToString();
			string equipmentType = this.ViewState["equipmentType"].ToString();
			this.dgEquipment.DataSource = EquipmentAction.GetEquipmentCollection(deptId, equipmentCode, equipmentName, equipmentType);
			this.dgEquipment.DataBind();
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
		private void dgEquipment_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				EquipmentInfo equipmentInfo = (EquipmentInfo)e.Item.DataItem;
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgEquipment.ClientID,
					"');clickEquipmentRow('",
					equipmentInfo.EquipmentUniqueCode.ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				try
				{
					e.Item.Cells[9].Text = com.jwsoft.pm.entpm.PageHelper.QueryCorp(this, int.Parse(equipmentInfo.ContactDept));
				}
				catch
				{
					e.Item.Cells[9].Text = "自有机械器具";
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
			if (this.txtEquipType.Value.Length != 0)
			{
				this.ViewState["equipmentType"] = this.hdnEquipmentType.Value.ToString().Trim();
			}
			else
			{
				this.ViewState["equipmentType"] = "";
			}
			this.EquipmentBind();
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgEquipment.CurrentPageIndex = e.NewPageIndex;
			this.EquipmentBind();
		}
	}


