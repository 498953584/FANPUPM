using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class List1 : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Button1.Attributes["onclick"] = "return CheckDelete()";
			if (!base.IsPostBack)
			{
				this.SetBind();
			}
		}
		private void SetBind()
		{
			this.DataGrid1.DataSource = mgReport1.GetInfoList();
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
				string text = this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["id"] = e.Item.ItemIndex.ToString();
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.DataGrid1.ClientID.ToString(),
					"');ClickRow('",
					text,
					"');"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes.Add("ondblclick", "OpView(); return false;");
				e.Item.ToolTip = "双击查看详细信息";
			}
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.SetBind();
		}
		protected void Button1_Click(object sender, EventArgs e)
		{
			string value = this.hdnClientCode.Value;
			bool bRet = mgReport1.DeleteInfo(value);
			ProjectInfoCommon.OperateSelf(bRet, this.Page, "删除成功！", "删除失败！");
		}
	}


