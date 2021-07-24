using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DistributeList : PageBase, IRequiresSessionState
	{
		public string jw = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["jw"] != null)
			{
				this.jw = base.Request["jw"].ToString().Trim();
				if (this.jw == "3")
				{
					this.btnSee.Attributes.CssStyle["display"] = "";
				}
			}
			if (!this.Page.IsPostBack)
			{
				this.ViewState["SQLWHERE"] = base.Request.QueryString["SqlWhere"];
				this.GridBind(this.ViewState["SQLWHERE"].ToString());
			}
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
		private void GridBind(string SqlWhere)
		{
			if (SqlWhere.ToString().Trim() == "")
			{
				SqlWhere = "  PrjState>=-1 and PrjState<6  ";
			}
			else
			{
				SqlWhere += " and PrjState>=-1 and PrjState<6 ";
			}
			this.DataGrid1.DataSource = publicDbOpClass.GetPageData(SqlWhere, "V_EPM_PrjLayout_PrjInfo", " StartDate DESC");
			this.DataGrid1.DataBind();
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
					base.Server.UrlEncode(e.Item.Cells[2].Text.Trim()),
					"','",
					e.Item.Cells[8].Text.ToString().Trim(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				HyperLink hyperLink = (HyperLink)e.Item.Cells[3].Controls[0];
				hyperLink.NavigateUrl = "#";
				hyperLink.NavigateUrl = "javascript:void(window.open('../ProjectInfo/InfoView.aspx?OpType=SEE&Code=" + text + "','','left=150,top=150,width=840,height=530,toolbar=no,status=yes,scrollbars=yes,resizable=no'));";
				e.Item.Attributes["ondblclick"] = "javascript:return WinLoad2('" + text + "')";
				e.Item.ToolTip = "双击查看详细信息";
				if (dataRowView["PrjState"].ToString() == "-1")
				{
					e.Item.Cells[10].ForeColor = Color.Blue;
					e.Item.Cells[10].Text = "在建";
				}
				try
				{
                    e.Item.Cells[9].Text = com.jwsoft.pm.entpm.PageHelper.QueryBasicCode(this, BasicType.ProjectArea, int.Parse(dataRowView["Area"].ToString()));
				}
				catch
				{
				}
			}
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.GridBind(this.ViewState["SQLWHERE"].ToString());
		}
	}


