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
	public partial class AttemperList : PageBase, IRequiresSessionState
	{
		private EquipmentAttemperAction EquipmentAttemper = new EquipmentAttemperAction();
		protected string _strKey;
		protected EquipmentAttemperCollection eaList = new EquipmentAttemperCollection();

		protected void Page_Load(object sender, EventArgs e)
		{
			this.hdnequipmentcode.Value = base.Request.QueryString["ec"];
			if (!this.Page.IsPostBack)
			{
				this.btnAdd.Attributes["onclick"] = "javascript:return OpType('ADD')";
				this.btnSee.Attributes["onclick"] = "javascript:return OpType('SEE')";
				this.btnEdit.Attributes["onclick"] = "javascript:return OpType('EDIT')";
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
				this.BindDataGrid();
			}
		}
		private void BindDataGrid()
		{
			this.eaList = EquipmentAttemperAction.GetAttemperList(this.hdnequipmentcode.Value);
			this.GrdAttemper.DataSource = this.eaList;
			this.GrdAttemper.DataBind();
			this.labState.Text = "共" + this.eaList.Count.ToString() + "条记录！";
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.GrdAttemper.ItemDataBound += new DataGridItemEventHandler(this.GrdAttemper_ItemDataBound);
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.BindDataGrid();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.BindDataGrid();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			if (EquipmentAttemperAction.DelEqiupmentAttemperInfo(int.Parse(this.hdnAttemperId.Value), this.hdnequipmentcode.Value) == 1)
			{
				this.JS.Text = "alert('删除成功！')";
				this.BindDataGrid();
			}
		}
		private void GrdAttemper_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				EquipmentAttemperInfo equipmentAttemperInfo = (EquipmentAttemperInfo)e.Item.DataItem;
				this._strKey = this.GrdAttemper.DataKeys[e.Item.ItemIndex].ToString();
				try
				{
					e.Item.Cells[6].Text = EquipmentAttemperAction.QuerySubProject(equipmentAttemperInfo.ToProjectUniqueCode);
				}
				catch
				{
				}
				e.Item.Attributes["onclick"] = "OnRecord(this);clickRow('" + equipmentAttemperInfo.NoteSequenceID.ToString() + "');";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
	}


