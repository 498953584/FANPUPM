using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DatumManageWin : PageBase, IRequiresSessionState
	{
		public string strRur = "";
		public string strTree = "";
		protected string SubPrjGuid = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.ddlPrjBind(this.GetValue("", ""));
				this.ddlItemBind(this.ddlPrj.SelectedValue);
				string arg_3F_0 = this.ddlItem.SelectedValue;
				if (this.ddlItem.SelectedValue != "")
				{
					this.SubPrjGuid = this.ddlItem.SelectedValue;
				}
				else
				{
					this.SubPrjGuid = Guid.Empty.ToString();
				}
				this.InitTypeList();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void InitTypeList()
		{
			if (this.ddlItem.SelectedItem != null)
			{
				this.lbPrjName.Text = this.ddlItem.SelectedItem.Text;
			}
			else
			{
				this.SubPrjGuid = Guid.Empty.ToString();
			}
			string text = base.Request.QueryString["type"];
			string a;
			if ((a = text) != null)
			{
				if (a == "2")
				{
					this.strRur = "DatumList.aspx?TypeId=&PrjCode=" + this.SubPrjGuid;
					return;
				}
				if (a == "3")
				{
					this.strRur = "DatumAffirmList.aspx?TypeId=&PrjCode=" + this.SubPrjGuid;
					return;
				}
				if (!(a == "4"))
				{
					return;
				}
				this.strRur = "DatumSeach.aspx?TypeId=&PrjCode=" + this.SubPrjGuid;
			}
		}
		private void ddlPrjBind(PrjInfoModel PIM)
		{
			string text = PrjInfoAction.GetSqlWhere(PIM);
			text = " Podepom like '%" + this.Session["yhdm"].ToString().Trim() + "%' and " + text;
			DataTable dateList = PrjInfoAction.GetDateList(text);
			if (dateList.Rows.Count > 0)
			{
				this.ddlPrj.DataSource = dateList;
				this.ddlPrj.DataTextField = "PrjName";
				this.ddlPrj.DataValueField = "PrjCode";
				this.ddlPrj.DataBind();
			}
		}
		private void ddlItemBind(string PrjCode)
		{
			this.ddlItem.Items.Clear();
			DataTable list = PMAction.GetList(PrjCode);
			if (list.Rows.Count > 0)
			{
				this.ddlItem.DataSource = list;
				this.ddlItem.DataTextField = "PrjName";
				this.ddlItem.DataValueField = "Prjguid";
				this.ddlItem.DataBind();
			}
		}
		private PrjInfoModel GetValue(string PrjCode, string PrjName)
		{
			return new PrjInfoModel
			{
				PrjCode = PrjCode,
				PrjName = PrjName,
				Remark = "0"
			};
		}
		protected void ddlPrj_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ddlItemBind(this.ddlPrj.SelectedValue);
			this.SubPrjGuid = this.ddlItem.SelectedValue;
			this.InitTypeList();
			this.lbPrjName.Text = this.ddlPrj.SelectedItem.Text;
		}
		protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.SubPrjGuid = this.ddlItem.SelectedValue;
			this.InitTypeList();
		}
		protected void btnSelect_Click(object sender, EventArgs e)
		{
			this.InitTypeList();
		}
	}


