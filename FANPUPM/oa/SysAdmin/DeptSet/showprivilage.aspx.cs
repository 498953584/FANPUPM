using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class showPrivilage : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			this.WarnBlock.InnerHtml = "";
			string text = "";
			try
			{
				text = base.Request.QueryString["bmdm"].Trim();
			}
			catch (Exception)
			{
				base.Response.End();
			}
			this.hdnDeptCode.Value = text;
			if (!base.IsPostBack)
			{
				this.GetDeptName(text);
				this.DGrdDuty_DataBind();
			}
		}
		private void GetDeptName(string strDeptCode)
		{
			DeptManageDb deptManageDb = new DeptManageDb();
			DataTable deptmentDetail = deptManageDb.GetDeptmentDetail(Convert.ToInt32(strDeptCode.Trim()));
			if (deptmentDetail.Rows.Count > 0)
			{
				this.LabDept.Text = deptmentDetail.Rows[0]["v_bmqc"].ToString() + "职务列表：";
			}
		}
		private void DGrdDuty_DataBind()
		{
			DutyManageDb dutyManageDb = new DutyManageDb();
			int iDeptID = Convert.ToInt32(this.hdnDeptCode.Value.ToString().Trim());
			DataTable deptDuty = dutyManageDb.GetDeptDuty(iDeptID);
			this.DGrdDuty.DataSource = deptDuty.DefaultView;
			this.DGrdDuty.DataBind();
		}
		private void DGrdDuty_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.DGrdDuty.EditItemIndex = -1;
			this.DGrdDuty_DataBind();
		}
		private void DGrdDuty_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			DutyManageDb dutyManageDb = new DutyManageDb();
			int iDutyID = Convert.ToInt32(this.DGrdDuty.DataKeys[e.Item.ItemIndex].ToString());
			if (dutyManageDb.DelDuty(iDutyID))
			{
				this.DGrdDuty_DataBind();
				return;
			}
			this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('" + dutyManageDb.MessageString + "');</SCRIPT>";
		}
		private void DGrdDuty_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.DGrdDuty.EditItemIndex = e.Item.ItemIndex;
			this.DGrdDuty_DataBind();
		}
		private void DGrdDuty_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				((LinkButton)e.Item.Cells[2].Controls[0]).Attributes["onclick"] = "return confirm(\"你确定要删除该项吗？\",1,0);";
				return;
			default:
				return;
			}
		}
		private void DGrdDuty_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			string text = ((TextBox)e.Item.Cells[0].Controls[1]).Text.ToString();
			if (text.Length == 0)
			{
				this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('部门名称不能为空！');</SCRIPT>";
				return;
			}
			int iDutyID = Convert.ToInt32(this.DGrdDuty.DataKeys[e.Item.ItemIndex].ToString());
			DutyManageDb dutyManageDb = new DutyManageDb();
			if (dutyManageDb.ModifyDuty(iDutyID, text))
			{
				this.DGrdDuty.EditItemIndex = -1;
				this.DGrdDuty_DataBind();
				return;
			}
			this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('" + dutyManageDb.MessageString + "');</SCRIPT>";
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdDuty.ItemCreated += new DataGridItemEventHandler(this.DGrdDuty_ItemCreated);
			this.DGrdDuty.CancelCommand += new DataGridCommandEventHandler(this.DGrdDuty_CancelCommand);
			this.DGrdDuty.EditCommand += new DataGridCommandEventHandler(this.DGrdDuty_EditCommand);
			this.DGrdDuty.UpdateCommand += new DataGridCommandEventHandler(this.DGrdDuty_UpdateCommand);
			this.DGrdDuty.DeleteCommand += new DataGridCommandEventHandler(this.DGrdDuty_DeleteCommand);
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			string text = this.TBoxDutyName.Text.ToString();
			if (text.Length == 0)
			{
				this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('部门名称不能为空！');</SCRIPT>";
				return;
			}
			int iDeptID = Convert.ToInt32(this.hdnDeptCode.Value.ToString());
			DutyManageDb dutyManageDb = new DutyManageDb();
			if (dutyManageDb.AddDuty(iDeptID, text))
			{
				this.PanNew.Visible = false;
				this.DGrdDuty_DataBind();
				return;
			}
			this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('部门名称不能为空！');</SCRIPT>";
		}
		protected void BtnCancel_Click(object sender, EventArgs e)
		{
			this.PanNew.Visible = false;
		}
		protected void BtnNew_Click(object sender, EventArgs e)
		{
			this.PanNew.Visible = true;
		}
	}


