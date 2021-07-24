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
    public partial class CheckInfoList : PageBase, IRequiresSessionState
    {
        protected JavaScriptControl js;
        protected int iPageSize = 10;
        protected string _strKey;
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.hdnequipmentcode.Value = base.Request.QueryString["ec"];
                this.btnAdd.Attributes["onclick"] = "javascript:return OpType('ADD')";
                this.btnSee.Attributes["onclick"] = "javascript:return OpType('SEE')";
                this.btnEdit.Attributes["onclick"] = "javascript:return OpType('EDIT')";
                this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
                this.BindDataGrid();
            }
        }
        private void BindDataGrid()
        {
            this.GrdEquipmentCheckup.DataSource = EquipmentCheckAction.GetEquipmentCheckList(new Guid(this.hdnequipmentcode.Value));
            this.GrdEquipmentCheckup.DataBind();
        }
        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.GrdEquipmentCheckup.ItemDataBound += new DataGridItemEventHandler(this.GrdEquipmentCheckup_ItemDataBound);
        }
        private void pageControl_PageIndexChange(object sender, EventArgs e)
        {
            this.BindDataGrid();
        }
        private void GrdEquipmentCheckup_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                this._strKey = this.GrdEquipmentCheckup.DataKeys[e.Item.ItemIndex].ToString();
                e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.GrdEquipmentCheckup.ClientID.ToString(),
					"');clickRow('",
					this._strKey,
					"');"
				});
                e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
                e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
            }
        }
        protected void btnDel_Click(object sender, EventArgs e)
        {
            EquipmentCheckInfo equipmentCheckInfo = new EquipmentCheckInfo();
            equipmentCheckInfo.NoteSequenceID = Convert.ToInt32(this.hdnChickId.Value);
            if (EquipmentCheckAction.DelEqiupmentCheckInfo(Convert.ToInt32(this.hdnChickId.Value)) != 1)
            {
                this.js.Text = "alert('删除数据出错！');";
            }
            this.BindDataGrid();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.BindDataGrid();
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            this.BindDataGrid();
        }
    }