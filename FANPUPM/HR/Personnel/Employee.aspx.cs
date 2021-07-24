using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
	public partial class Employee : BasePage, IRequiresSessionState
	{
		private int pagesize = 15;

		public PTDUTYAction hrAction
		{
			get
			{
				return new PTDUTYAction();
			}
		}
		public AnnexAction _AnnexAction
		{
			get
			{
				return new AnnexAction();
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.AspNetPager1.PageSize = this.pagesize;
			base.OnInit(e);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.Tree_Bind();
				this.btnAdd.Attributes["onclick"] = "openEmployeeEdit(1);";
				this.btnEdit.Attributes["onclick"] = "openEmployeeEdit(2);";
				this.btnDel.Attributes["onclick"] = " return confirm('确定该人员要离职吗？');";
				this.btnRe.Attributes["onclick"] = " return confirm('确定该人员要回到职位吗？');";
				this.AspNetPager1.CurrentPageIndex = 1;
				if (base.Request["sfyx"].ToString() == "2")
				{
					DataTable dataTable = new DataTable();
					dataTable = this.hrAction.GetDepartment();
					this.hdnDeptID.Value = dataTable.Rows[0][0].ToString();
					this.hfldDepatId.Value = dataTable.Rows[0][0].ToString();
					for (int i = 1; i < dataTable.Rows.Count; i++)
					{
						HiddenField expr_10E = this.hfldDepatId;
						expr_10E.Value = expr_10E.Value + "','" + dataTable.Rows[i][0].ToString();
					}
					this.AspNetPager1.RecordCount = this.hrAction.GetDTtyhmcAll(base.Request["sfyx"], this.hfldDepatId.Value, this.txtUserName.Text.Trim()).Rows.Count;
					this.gvEmployeelist.DataSource = this.hrAction.GetDTtyhmc(base.Request["sfyx"], this.hfldDepatId.Value, this.txtUserName.Text.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
					this.gvEmployeelist.Columns[8].Visible = true;
				}
				else
				{
					this.hdnDeptID.Value = this.TVCorp.SelectedValue.ToString();
					this.AspNetPager1.RecordCount = this.hrAction.GetDTtyhmcAll(base.Request["sfyx"], this.TVCorp.SelectedValue.ToString(), this.txtUserName.Text.Trim()).Rows.Count;
					this.gvEmployeelist.DataSource = this.hrAction.GetDTtyhmc(base.Request["sfyx"], this.TVCorp.SelectedValue.ToString(), this.txtUserName.Text.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
					this.gvEmployeelist.Columns[8].Visible = false;
				}
				this.gvEmployeelist.DataBind();
				if (base.Request["sfyx"] != null && base.Request["sfyx"].ToString() == "2")
				{
					this.LblTitle.Text = "离职员工管理";
					this.btnAdd.Style.Add("display", "none");
					this.btnDel.Style.Add("display", "none");
					this.btnEdit.Style.Add("display", "none");
					return;
				}
				this.LblTitle.Text = "员工信息管理";
				this.btnAdd.Style.Add("dispaly", "");
				this.btnDel.Style.Add("dispaly", "");
				this.btnEdit.Style.Add("dispaly", "");
				this.btnRe.Style.Add("display", "none");
			}
		}
		protected void gvEmployeelist_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string text = this.gvEmployeelist.DataKeys[e.Row.RowIndex]["v_yhdm"].ToString();
				((DataRowView)e.Row.DataItem)["RelationCorp"].ToString();
				string text2 = ((DataRowView)e.Row.DataItem)["i_bmdm"].ToString();
				e.Row.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);ClickRow('",
					text,
					"','",
					text2,
					"');"
				});
				HyperLink hyperLink = (HyperLink)e.Row.Cells[2].Controls[0];
				hyperLink.NavigateUrl = "#";
				hyperLink.NavigateUrl = string.Concat(new string[]
				{
					"javascript:void(window.open('EmployeeView.aspx?t=3&cc=",
					text2,
					"&uc=",
					text,
					"','','left=150,top=150,width=700,height=600,toolbar=no,status=yes,scrollbars=yes,resiz able=no'));"
				});
				e.Row.Attributes["ondblclick"] = "javascript:return openEmployeeEdit(3)";
				e.Row.ToolTip = "双击查看详细信息";
			}
		}
		protected void btnSelect_Click(object sender, System.EventArgs e)
		{
			this.AspNetPager1.CurrentPageIndex = 1;
			this.BindData();
		}
		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.AspNetPager1.CurrentPageIndex = 1;
			this.BindData();
		}
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			this.AspNetPager1.CurrentPageIndex = 1;
			this.BindData();
		}
		protected void btnDel_Click(object sender, System.EventArgs e)
		{
			this.AspNetPager1.CurrentPageIndex = 1;
			string value = this.hdnUserCode.Value;
			if (PersonnelAction.DelPerson(value))
			{
				this.JS.Text = "alert('人员离职成功!');";
				this.BindData();
			}
		}
		protected void btnRe_Click(object sender, System.EventArgs e)
		{
			this.AspNetPager1.CurrentPageIndex = 1;
			string value = this.hdnUserCode.Value;
			if (PersonnelAction.RePerson(value))
			{
				this.JS.Text = "alert('人员回到职位成功!');";
				this.BindData();
			}
		}
		private void Tree_Bind()
		{
			string b = base.Request["depCode"];
			DataTable departmentTree = this.hrAction.GetDepartmentTree("0");
			if (departmentTree.Rows.Count > 0)
			{
				foreach (DataRow dataRow in departmentTree.Rows)
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = dataRow["V_BMMC"].ToString();
					treeNode.Value = dataRow["i_bmdm"].ToString();
					if (treeNode.Value == b)
					{
						treeNode.Select();
					}
					this.TVCorp.Nodes.Add(treeNode);
					this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
				}
			}
		}
		private void Bind_SubTree(TreeNode ftn, string strBMDM)
		{
			string b = base.Request["depCode"];
			DataTable departmentTree = this.hrAction.GetDepartmentTree(strBMDM);
			if (departmentTree.Rows.Count > 0)
			{
				foreach (DataRow dataRow in departmentTree.Rows)
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = dataRow["V_BMMC"].ToString();
					treeNode.Value = dataRow["i_bmdm"].ToString();
					if (treeNode.Value == b)
					{
						treeNode.Select();
					}
					ftn.ChildNodes.Add(treeNode);
					this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
				}
			}
		}
		protected void TVCorp_SelectedNodeChanged(object sender, System.EventArgs e)
		{
			this.AspNetPager1.CurrentPageIndex = 1;
			this.hdnDeptID.Value = this.TVCorp.SelectedValue.ToString();
			this.hfldDepatId.Value = this.TVCorp.SelectedValue.ToString();
			if (this.hfldDepatId.Value == "1")
			{
				DataTable dataTable = new DataTable();
				dataTable = this.hrAction.GetDepartment();
				this.hfldDepatId.Value = dataTable.Rows[0][0].ToString();
				for (int i = 1; i < dataTable.Rows.Count; i++)
				{
					HiddenField expr_97 = this.hfldDepatId;
					expr_97.Value = expr_97.Value + "','" + dataTable.Rows[i][0].ToString();
				}
			}
			this.BindData();
		}
		protected void btnExp_Click1(object sender, System.EventArgs e)
		{
			string text = "员工信息.xls";
			if (base.Request.Browser.Browser.ToUpper() != "SARARI")
			{
				text = HttpUtility.UrlDecode(text, System.Text.Encoding.UTF8);
			}
			this.ExportToExcel("application/ms-excel", text);
		}
		public void ExportToExcel(string FileType, string FileName)
		{
			base.Response.Charset = "GB2312";
			base.Response.ContentEncoding = System.Text.Encoding.UTF8;
			HttpContext.Current.Response.Charset = "UTF-8";
			HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
			HttpContext.Current.Response.ContentType = "application/ms-excel";
			base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
			base.Response.ContentType = FileType;
			this.EnableViewState = false;
			System.IO.StringWriter stringWriter = new System.IO.StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			GridView gridView = new GridView();
			DataTable dataTable = new DataTable();
			if (base.Request["sfyx"].ToString() == "2")
			{
				dataTable = this.hrAction.GetDTtyhmc(base.Request["sfyx"], this.hfldDepatId.Value, this.txtUserName.Text.Trim(), 0, 0);
			}
			else
			{
				dataTable = this.hrAction.GetDTtyhmc(base.Request["sfyx"], this.TVCorp.SelectedValue.ToString(), this.txtUserName.Text.Trim(), 0, 0);
			}
			dataTable.Columns.Remove("v_yhdm");
			dataTable.Columns.Remove("i_bmdm");
			dataTable.Columns.Remove("I_DUTYID");
			dataTable.Columns.Remove("OtherDepts");
			dataTable.Columns.Remove("OtherDutyIDs");
			dataTable.Columns.Remove("i_xh");
			dataTable.Columns.Remove("c_sfyx");
			dataTable.Columns.Remove("MobilePhoneCode");
			dataTable.Columns.Remove("RelationCorp");
			dataTable.Columns.Remove("State");
			dataTable.Columns.Remove("IsChargeMan");
			dataTable.Columns["Num"].ColumnName = "序号";
			dataTable.Columns["bmmc"].ColumnName = "部门名称";
			dataTable.Columns["v_xm"].ColumnName = "姓名";
			dataTable.Columns["DutyName"].ColumnName = "岗位";
			dataTable.Columns["EnterCorpDate"].ColumnName = "入司日期";
			dataTable.Columns["Age"].ColumnName = "年龄";
			dataTable.Columns["StateName"].ColumnName = "状态";
			dataTable.Columns["IsChargeManName"].ColumnName = "部门负责人";
			dataTable.Columns["姓名"].SetOrdinal(2);
			if (base.Request["sfyx"].ToString() == "2")
			{
				dataTable.Columns["LeaveDate"].ColumnName = "离职日期";
			}
			else
			{
				dataTable.Columns.Remove("LeaveDate");
			}
			gridView.DataSource = dataTable;
			gridView.DataBind();
			gridView.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}
		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.AspNetPager1.CurrentPageIndex = 1;
			this.BindData();
		}
		private void BindData()
		{
			if (base.Request["sfyx"].ToString() == "2")
			{
				this.AspNetPager1.RecordCount = this.hrAction.GetDTtyhmcAll(base.Request["sfyx"], this.hfldDepatId.Value, this.txtUserName.Text.Trim()).Rows.Count;
				this.gvEmployeelist.DataSource = this.hrAction.GetDTtyhmc(base.Request["sfyx"], this.hfldDepatId.Value, this.txtUserName.Text.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
			}
			else
			{
				this.AspNetPager1.RecordCount = this.hrAction.GetDTtyhmcAll(base.Request["sfyx"], this.TVCorp.SelectedValue.ToString(), this.txtUserName.Text.Trim()).Rows.Count;
				this.gvEmployeelist.DataSource = this.hrAction.GetDTtyhmc(base.Request["sfyx"], this.TVCorp.SelectedValue.ToString(), this.txtUserName.Text.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
			}
			this.gvEmployeelist.DataBind();
		}
	}


