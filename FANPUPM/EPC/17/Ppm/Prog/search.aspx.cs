using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class Search1 : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.SetBind();
			}
		}
		private void SetBind()
		{
			string dptName = this.TextBox1.Text.Trim();
			this.DataGrid1.DataSource = mgReport1.GetInfoList(dptName);
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
		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Cells[0].Text = string.Concat(e.Item.ItemIndex + 1);
				string text = e.Item.Cells[2].Text;
				if (text.Length > 45)
				{
					e.Item.Cells[2].Text = text.Substring(0, 43) + "...";
					e.Item.ToolTip = text;
				}
				string text2 = this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.DataGrid1.ClientID.ToString(),
					"');ClickRow('",
					text2,
					"');"
				});
				e.Item.Attributes["id"] = e.Item.ItemIndex.ToString();
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes.Add("ondblclick", "OpView(); return false;");
				e.Item.ToolTip = "双击查看详细信息";
				Label label = (Label)e.Item.Cells[0].FindControl("Label1");
				label.Attributes["onclick"] = "OpQuery('" + text2 + "')";
				string text3 = e.Item.Cells[2].Text.ToString();
				if (text3.Length > 65)
				{
					e.Item.Cells[2].Text = text3.Substring(0, 65) + "...";
					e.Item.Cells[2].ToolTip = text3;
				}
			}
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.SetBind();
		}
		protected void Button1_Click(object sender, EventArgs e)
		{
			this.SetBind();
		}
	}


