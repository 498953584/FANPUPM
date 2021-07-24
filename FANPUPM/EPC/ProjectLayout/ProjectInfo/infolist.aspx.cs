using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class InfoList : PageBase, IRequiresSessionState
	{
		private string _State = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (this.Session["PrjState"] != null)
				{
					this.ViewState["STATE"] = this.Session["PrjState"].ToString();
					this._State = this.ViewState["STATE"].ToString();
					this.HdnType.Value = this._State;
					if (this._State == "SEE")
					{
						this.Button1.Visible = false;
						this.btnEdit.Visible = false;
						this.BtnDel.Visible = false;
					}
				}
				if (base.Request["jw"] != null && base.Request["jw"].ToString().Trim() == "3")
				{
					this.Button1.Attributes.CssStyle["display"] = "none";
					this.btnEdit.Attributes.CssStyle["display"] = "none";
					this.BtnDel.Attributes.CssStyle["display"] = "none";
				}
				if (base.Request["Type"] != null)
				{
					if (base.Request["Type"].ToString().Trim() == "-2")
					{
						this.Button1.Attributes.CssStyle["display"] = "none";
						this.BtnDel.Attributes.CssStyle["display"] = "none";
						this.btndelAdmin.Width = 0;
						this.BtnDel.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "";
					}
					if (base.Request["Type"].ToString().Trim() == "-1")
					{
						this.Button1.Attributes.CssStyle["display"] = "none";
						this.BtnDel.Attributes.CssStyle["display"] = "none";
					}
					this.HdnType.Value = base.Request["Type"].ToString();
					this.Button1.Attributes["onclick"] = "javascript:return WinLoad('ADD','" + this.HdnType.Value + "');";
				}
				else
				{
					this.BtnDel.Attributes.CssStyle["display"] = "none";
					this.Button1.Attributes["onclick"] = "javascript:return WinLoad('ADD','');";
				}
				this.ViewState["SQLWHERE"] = base.Request.QueryString["SqlWhere"].ToString();
				this.GridBind(this.ViewState["SQLWHERE"].ToString());
				this.btnEdit.Attributes["onclick"] = "javascript:return WinLoad('Edit','" + this.HdnType.Value + "');";
				this.btnSee.Attributes["onclick"] = "javascript:return WinLoad('SEE','');";
				this.btndelAdmin.Attributes["onclick"] = "javascript:return adminDel();";
				this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除该项目信息吗?'))return false;";
			}
		}
		private void GridBind(string SqlWhere)
		{
			if (base.Request["Type"] != null && base.Request["Type"].ToString().Trim() != "")
			{
				if (SqlWhere.ToString().Trim() == "")
				{
					if (base.Request.QueryString["Type"].ToString() == "-2")
					{
						SqlWhere = "  PrjState=" + base.Request["Type"].ToString().Trim() + " or PrjState=11";
					}
					else
					{
						SqlWhere = "  PrjState=" + base.Request["Type"].ToString().Trim() + " ";
					}
				}
				else
				{
					if (base.Request.QueryString["Type"].ToString() == "-2")
					{
						SqlWhere = SqlWhere + " and PrjState=" + base.Request["Type"].ToString().Trim() + " or PrjState=11";
					}
					else
					{
						SqlWhere = "  PrjState=" + base.Request["Type"].ToString().Trim() + " ";
					}
				}
			}
			else
			{
				if (SqlWhere.ToString().Trim() == "")
				{
					SqlWhere = "  PrjState>=-1  and  PrjState<6";
				}
				else
				{
					SqlWhere += " and PrjState>=-3 ";
				}
			}
			DataTable dataTable = new DataTable();
			dataTable = publicDbOpClass.GetPageData(SqlWhere, "V_PT_PrjInfo_ZTB", " StartDate DESC");
			this.ViewState["ProjectInfoDataTable"] = dataTable;
			this.DataGrid1.DataSource = dataTable.DefaultView;
			this.DataGrid1.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
		}
		protected void PaginationControl1_PageIndexChange(object sender, EventArgs e)
		{
			this.GridBind(this.ViewState["SQLWHERE"].ToString());
		}
		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				string text = this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.DataGrid1.ClientID.ToString(),
					"');clickRow('",
					text,
					"','",
					this.HdnType.Value.Trim(),
					"','",
					dataRowView["PrjState"].ToString(),
					"','",
					dataRowView["PrjName"].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				HyperLink hyperLink = (HyperLink)e.Item.Cells[3].Controls[0];
				hyperLink.ToolTip = hyperLink.Text;
				if (hyperLink.Text.Length > 9)
				{
					hyperLink.Text = hyperLink.Text.Substring(0, 9) + "...";
				}
				hyperLink.Style.Add("onMouseOver", "document.getElementById('btnEdit').Color='red'");
				hyperLink.NavigateUrl = "#";
				hyperLink.NavigateUrl = "javascript:void(window.open('InfoView.aspx?OpType=SEE&Code=" + text + "','','left=150,top=150,width=840,height=530,toolbar=no,status=yes,scrollbars=yes,resiz able=no'));";
				e.Item.Attributes["ondblclick"] = "javascript:return WinLoad1('" + text + "')";
				e.Item.ToolTip = "双击查看详细信息";
				if (dataRowView["PrjState"].ToString() == "-3")
				{
					e.Item.Cells[10].ForeColor = Color.Red;
				}
				if (dataRowView["PrjState"].ToString() == "-2")
				{
					e.Item.Cells[10].ForeColor = Color.Red;
				}
				if (dataRowView["PrjState"].ToString() == "-1")
				{
					e.Item.Cells[10].ForeColor = Color.Blue;
					e.Item.Cells[10].Text = "在建";
				}
				if (e.Item.Cells[7].Text.ToString().Trim().Length > 8)
				{
					e.Item.Cells[7].ToolTip = e.Item.Cells[7].Text.ToString();
					e.Item.Cells[7].Text = e.Item.Cells[7].Text.ToString().Substring(0, 8) + "...";
				}
				if (e.Item.Cells[4].Text.ToString().Trim().Length > 7)
				{
					e.Item.Cells[4].ToolTip = e.Item.Cells[4].Text.ToString();
					e.Item.Cells[4].Text = e.Item.Cells[4].Text.ToString().Substring(0, 7) + "...";
				}
			}
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			string value = this.hdnID.Value;
			if (PrjInfoAction.Del(value) == 1)
			{
				this.GridBind(this.ViewState["SQLWHERE"].ToString());
				return;
			}
			this.js.Text = "alert('删除失败！')";
		}
		protected void Button1_Click(object sender, EventArgs e)
		{
			this.GridBind(this.ViewState["SQLWHERE"].ToString());
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.GridBind(this.ViewState["SQLWHERE"].ToString());
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.GridBind(this.ViewState["SQLWHERE"].ToString());
		}
		protected void btnExp_Click1(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "市场信息.xls");
		}
		public void ExportToExcel(string FileType, string FileName)
		{
			base.Response.Charset = "GB2312";
			base.Response.ContentEncoding = Encoding.Default;
			base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
			base.Response.ContentType = FileType;
			this.EnableViewState = false;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			this.DataGrid1.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
		protected void btndelAdmin_Click(object sender, EventArgs e)
		{
			if (this.Session["twopass"] != null && this.Session["twopass"].ToString().Trim() == "IsAllowDel")
			{
				string value = this.hdnID.Value;
				if (PrjInfoAction.Del(value) == 1)
				{
					this.GridBind(this.ViewState["SQLWHERE"].ToString());
					string fid = this.hdnID.Value.ToString();
					string fname = this.hdndelname.Value.ToString();
					myxml.SetTwoPWDlog(this.Session["yhdm"].ToString(), this.Page.Request.UserHostAddress.ToString(), "投标管理", fid, fname);
					return;
				}
				this.js.Text = "alert('删除失败！')";
			}
		}
		protected void btnQuery_Click(object sender, EventArgs e)
		{
			DataTable dataTable = new DataTable();
			dataTable = (this.ViewState["ProjectInfoDataTable"] as DataTable);
			DataTable dataTable2 = new DataTable();
			dataTable2 = dataTable.Clone();
			string text = string.Empty;
			text += " 1=1 ";
			if (!string.IsNullOrEmpty(this.txtprjCodeQ.Text.Trim().ToString()))
			{
				text = text + " and PrjCode like '%" + this.txtprjCodeQ.Text.Trim().ToString() + "%'";
			}
			if (!string.IsNullOrEmpty(this.txtprjNameQ.Text.Trim().ToString()))
			{
				text = text + " and PrjName like '%" + this.txtprjNameQ.Text.Trim().ToString() + "%'";
			}
			DataRow[] array = dataTable.Select(text);
			if (array.Length > 0)
			{
				DataRow[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow row = array2[i];
					dataTable2.ImportRow(row);
				}
			}
			this.DataGrid1.DataSource = dataTable2.DefaultView;
			this.DataGrid1.DataBind();
		}
	}


