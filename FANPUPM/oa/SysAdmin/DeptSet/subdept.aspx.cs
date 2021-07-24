using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SubDept : BasePage, IRequiresSessionState
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
			}
			if (text.Length != 0)
			{
				this.HdnDeptCode.Value = text;
				if (!base.IsPostBack)
				{
					this.hfldbtnadd.Value = "0";
					DeptManageDb deptManageDb = new DeptManageDb();
					deptManageDb.ObjPage = this.Page;
					DataTable deptmentDetail = deptManageDb.GetDeptmentDetail(Convert.ToInt32(text.Trim()));
					if (deptmentDetail.Rows.Count > 0)
					{
						this.LabDept.Text = deptmentDetail.Rows[0]["v_bmmc"].ToString();
						this.HdnLevel.Value = deptmentDetail.Rows[0]["i_jb"].ToString();
					}
					DataTable dataTable = deptManageDb.Getjb(Convert.ToInt32(text.Trim()));
					if (Convert.ToInt32(dataTable.Rows[0]["i_jb"]) != 1)
					{
						this.ddl.Visible = false;
						this.td1.Visible = false;
						this.td2.Visible = false;
						this.TBoxDeptName.Columns = 24;
						this.TBoxNumber.Columns = 6;
					}
					this.DGrdSubDept_DataBind();
					this.DDLtAllDept_DataBind();
					this.dropdownlistBind();
					if (Convert.ToInt32(base.Request.QueryString["bmdm"].Trim()) == 1)
					{
						this.BtnMove.Visible = false;
						this.BtnMerge.Visible = false;
						return;
					}
					this.BtnMove.Visible = true;
					this.BtnMerge.Visible = true;
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
			this.DGrdSubDept.ItemCreated += new DataGridItemEventHandler(this.DGrdSubDept_ItemCreated);
			this.DGrdSubDept.CancelCommand += new DataGridCommandEventHandler(this.DGrdSubDept_CancelCommand);
			this.DGrdSubDept.EditCommand += new DataGridCommandEventHandler(this.DGrdSubDept_EditCommand);
			this.DGrdSubDept.UpdateCommand += new DataGridCommandEventHandler(this.DGrdSubDept_UpdateCommand);
			this.DGrdSubDept.DeleteCommand += new DataGridCommandEventHandler(this.DGrdSubDept_DeleteCommand);
			this.DGrdSubDept.ItemDataBound += new DataGridItemEventHandler(this.DGrdSubDept_ItemDataBound);
		}
		private void DGrdSubDept_DataBind()
		{
			int iDeptCode = Convert.ToInt32(this.HdnDeptCode.Value.ToString().Trim());
			DeptManageDb deptManageDb = new DeptManageDb();
			DataTable subDepartment = deptManageDb.GetSubDepartment(iDeptCode);
			this.DGrdSubDept.DataSource = subDepartment.DefaultView;
			this.DGrdSubDept.DataBind();
		}
		private void DDLtAllDept_DataBind()
		{
			this.DDLtAllDept.Items.Clear();
			DeptManageDb deptManageDb = new DeptManageDb();
			DataTable dataTable = deptManageDb.GetlParentLevelDept(Convert.ToInt32(base.Request.QueryString["bmdm"].Trim()));
			if (dataTable.Rows.Count > 0)
			{
				DataRow[] array = dataTable.Select("i_sjdm=0");
				for (int i = 0; i < array.Length; i++)
				{
					ListItem listItem = new ListItem();
					listItem.Value = array[i]["i_Bmdm"].ToString();
					listItem.Text = array[i]["v_Bmmc"].ToString();
					this.DDLtAllDept.Items.Add(listItem);
					if (array[i]["i_xjbm"].ToString() != "0")
					{
						this.DeptTree_GenerateFirst(dataTable, array[i]["i_Bmdm"].ToString());
					}
				}
				return;
			}
			this.DDLtAllDept.Items.Add(new ListItem("没有部门", ""));
		}
		private void DeptTree_GenerateFirst(DataTable dtDept, string strDeptCode)
		{
			DataRow[] array = dtDept.Select("i_sjdm='" + strDeptCode + "'");
			for (int i = 0; i < array.Length; i++)
			{
				ListItem listItem = new ListItem();
				listItem.Value = array[i]["i_Bmdm"].ToString();
				if (i != array.Length - 1)
				{
					listItem.Text = "├" + array[i]["v_Bmmc"].ToString();
					this.DDLtAllDept.Items.Add(listItem);
					if (array[i]["i_xjbm"].ToString() != "0")
					{
						this.DeptTree_Generate(dtDept, array[i]["i_Bmdm"].ToString(), false, int.Parse(array[i]["i_jb"].ToString()), "");
					}
				}
				else
				{
					listItem.Text = "└" + array[i]["v_Bmmc"].ToString();
					this.DDLtAllDept.Items.Add(listItem);
					if (array[i]["i_xjbm"].ToString() != "0")
					{
						this.DeptTree_Generate(dtDept, array[i]["i_Bmdm"].ToString(), true, int.Parse(array[i]["i_jb"].ToString()), "");
					}
				}
			}
		}
		private void DeptTree_Generate(DataTable dtDept, string strDeptCode, bool isEnd, int iLevel, string strHead)
		{
			DataRow[] array = dtDept.Select("i_sjdm='" + strDeptCode + "'");
			if (isEnd)
			{
				for (int i = 0; i < array.Length; i++)
				{
					ListItem listItem = new ListItem();
					listItem.Value = array[i]["i_Bmdm"].ToString();
					if (i != array.Length - 1)
					{
						listItem.Text = strHead + this.RepeatString("│", iLevel) + "├" + array[i]["v_Bmmc"].ToString();
						this.DDLtAllDept.Items.Add(listItem);
						if (array[i]["i_xjbm"].ToString() != "0")
						{
							this.DeptTree_Generate(dtDept, array[i]["i_Bmdm"].ToString(), false, int.Parse(array[i]["i_jb"].ToString()), "");
						}
					}
					else
					{
						listItem.Text = strHead + this.RepeatString("│", iLevel) + "└" + array[i]["v_Bmmc"].ToString();
						this.DDLtAllDept.Items.Add(listItem);
						if (array[i]["i_xjbm"].ToString() != "0")
						{
							this.DeptTree_Generate(dtDept, array[i]["i_Bmdm"].ToString(), true, int.Parse(array[i]["i_jb"].ToString()), strHead + "");
						}
					}
				}
				return;
			}
			for (int j = 0; j < array.Length; j++)
			{
				ListItem listItem2 = new ListItem();
				listItem2.Value = array[j]["i_Bmdm"].ToString();
				if (j != array.Length - 1)
				{
					listItem2.Text = strHead + this.RepeatString("│", iLevel) + "├" + array[j]["v_Bmmc"].ToString();
				}
				else
				{
					listItem2.Text = strHead + this.RepeatString("│", iLevel) + "└" + array[j]["v_Bmmc"].ToString();
				}
				this.DDLtAllDept.Items.Add(listItem2);
				if (array[j]["i_xjbm"].ToString() != "0")
				{
					this.DeptTree_Generate(dtDept, array[j]["i_Bmdm"].ToString(), false, int.Parse(array[j]["i_jb"].ToString()), strHead);
				}
			}
		}
		private string RepeatString(string strStr, int iNumber)
		{
			string text = "";
			for (int i = 0; i < iNumber; i++)
			{
				text += strStr;
			}
			return text;
		}
		private void DGrdSubDept_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.DGrdSubDept.EditItemIndex = e.Item.ItemIndex;
			this.DGrdSubDept_DataBind();
		}
		private void DGrdSubDept_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			int iDeptCode = Convert.ToInt32(this.DGrdSubDept.DataKeys[e.Item.ItemIndex].ToString());
			DeptManageDb deptManageDb = new DeptManageDb();
			deptManageDb.ObjPage = this.Page;
			if (deptManageDb.DelDepartment(iDeptCode))
			{
				this.DGrdSubDept_DataBind();
				return;
			}
			this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('" + deptManageDb.MessageString + "');</SCRIPT>");
		}
		private void DGrdSubDept_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.DGrdSubDept.EditItemIndex = -1;
			this.DGrdSubDept_DataBind();
		}
		private void DGrdSubDept_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			int iUpDeptCode = 0;
			int iDeptCode = 0;
			string text = "";
			int num = 0;
			iUpDeptCode = Convert.ToInt32(this.HdnDeptCode.Value.ToString());
			iDeptCode = Convert.ToInt32(this.DGrdSubDept.DataKeys[e.Item.ItemIndex].ToString());
			text = ((TextBox)e.Item.Cells[0].Controls[1]).Text.ToString().Trim();
			if (text.Length == 0)
			{
				this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('部门名称不能为空!');</SCRIPT>";
				return;
			}
			string value = ((TextBox)e.Item.Cells[2].Controls[1]).Text.ToString();
			try
			{
				num = Convert.ToInt32(value);
			}
			catch (Exception)
			{
				this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('部门序号不是数字!');</SCRIPT>";
				return;
			}
			if (num < 0)
			{
				this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('部门序号必须为自然数!');</SCRIPT>";
			}
			else
			{
				if (new DeptManageDb
				{
					ObjPage = this.Page
				}.ModifyDepart(iDeptCode, text, num, iUpDeptCode))
				{
					this.DGrdSubDept.EditItemIndex = -1;
					this.DGrdSubDept_DataBind();
					return;
				}
				this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('更新失败!');</SCRIPT>";
				return;
			}
		}
		private void DGrdSubDept_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				((LinkButton)e.Item.Cells[7].Controls[0]).Attributes["onclick"] = "return confirm('该操作不可恢复，请确认是否删除？',1,0);";
				return;
			default:
				return;
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			object obj = new object();
			int iUpDeptCode = Convert.ToInt32(this.HdnDeptCode.Value.ToString());
			string text = this.TBoxDeptName.Text.ToString().Trim();
			string strBMBM = this.ddl.SelectedValue.ToString();
			if (!Information.IsNumeric(this.TBoxNumber.Text.ToString().Trim()) || this.TBoxNumber.Text.ToString().Trim().Contains("."))
			{
				this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('序号必须为数字!');</SCRIPT>";
				return;
			}
			if (Convert.ToInt32(this.TBoxNumber.Text.ToString().Trim()) < 0)
			{
				this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('序号必须为自然数!');</SCRIPT>";
				return;
			}
			int iNumber = Convert.ToInt32(this.TBoxNumber.Text.ToString().Trim());
			if (text.Length == 0)
			{
				this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('部门名称不能为空!');</SCRIPT>";
				return;
			}
			DeptManageDb deptManageDb = new DeptManageDb();
			deptManageDb.ObjPage = this.Page;
			lock (obj)
			{
				if (deptManageDb.AddDepart(text, iNumber, iUpDeptCode, strBMBM))
				{
					this.PanNewDept.Visible = false;
					this.hfldbtnadd.Value = "0";
					this.DGrdSubDept_DataBind();
				}
				else
				{
					this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('" + deptManageDb.MessageString + "');</SCRIPT>";
				}
			}
		}
		protected void BtnAddSub_Click(object sender, EventArgs e)
		{
			this.PanMove.Visible = false;
			this.PanNewDept.Visible = true;
			this.hfldbtnadd.Value = "1";
		}
		protected void BtnMove_Click(object sender, EventArgs e)
		{
			this.PanMove.Visible = true;
			this.LabTitle.Text = "移动到：";
			this.BtnMoveOK.Visible = true;
			this.BtnMergeOK.Visible = false;
			this.PanNewDept.Visible = false;
			this.hfldbtnadd.Value = "0";
		}
		protected void BtnMerge_Click(object sender, EventArgs e)
		{
			this.PanMove.Visible = true;
			this.LabTitle.Text = "合并到：";
			this.BtnMoveOK.Visible = false;
			this.BtnMergeOK.Visible = true;
			this.PanNewDept.Visible = false;
			this.hfldbtnadd.Value = "0";
		}
		protected void BtnMoveCancel_Click(object sender, EventArgs e)
		{
			this.PanMove.Visible = false;
		}
		protected void BtnMoveOK_Click(object sender, EventArgs e)
		{
			int num = Convert.ToInt32(this.HdnDeptCode.Value.ToString());
			int num2 = Convert.ToInt32(this.DDLtAllDept.SelectedItem.Value.ToString());
			DeptManageDb deptManageDb = new DeptManageDb();
			deptManageDb.ObjPage = this.Page;
			if (num2 == num)
			{
				this.Page.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('不需要该操作！');</SCRIPT>");
				return;
			}
			if (deptManageDb.MoveDeptartment(num2, num))
			{
				this.PanMove.Visible = false;
				this.DGrdSubDept_DataBind();
				return;
			}
			this.WarnBlock.InnerHtml = "<SCRIPT language=\"JavaScript\">alert('" + deptManageDb.MessageString + "');</SCRIPT>";
		}
		protected void BtnMergeOK_Click(object sender, EventArgs e)
		{
			int num = Convert.ToInt32(this.HdnDeptCode.Value.ToString());
			int num2 = Convert.ToInt32(this.DDLtAllDept.SelectedItem.Value.ToString());
			DeptManageDb deptManageDb = new DeptManageDb();
			deptManageDb.ObjPage = this.Page;
			if (num2 == num)
			{
				this.Page.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('不需要该操作！');</SCRIPT>");
				return;
			}
			if (deptManageDb.MergeDepartment(num2, num))
			{
				this.PanMove.Visible = false;
				this.DGrdSubDept_DataBind();
				return;
			}
			this.Page.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('" + deptManageDb.MessageString + "');</SCRIPT>");
		}
		protected void BtnAddCancel_Click(object sender, EventArgs e)
		{
			this.PanNewDept.Visible = false;
			this.hfldbtnadd.Value = "0";
		}
		private void DGrdSubDept_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			e.Item.Cells[4].Attributes["style"] = "display:none";
		}
		protected void dropdownlistBind()
		{
			DeptManageDb deptManageDb = new DeptManageDb();
			DataTable detDDL = deptManageDb.GetDetDDL();
			this.ddl.DataValueField = "CorpCode";
			this.ddl.DataTextField = "CorpName";
			this.ddl.DataSource = detDDL;
			this.ddl.DataBind();
		}
	}


