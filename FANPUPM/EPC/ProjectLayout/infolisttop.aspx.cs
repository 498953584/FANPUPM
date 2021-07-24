using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class InfoListTop : NBasePage, IRequiresSessionState
	{
		protected string strURL = "";
		protected string FramUrl = "";
		public string jw = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.txtEndDate.Attributes["onactivate"] = "getBeginDate();";
			if (base.Request["jw"] != null && base.Request["jw"].ToString().Trim() != "")
			{
				this.jw = base.Request["jw"].ToString().Trim();
			}
			if (!this.Page.IsPostBack)
			{
				this.ddt_stateBind();
				this.ddt_Area_Bind();
				this.Drop_PrjKindClass_Bind();
				this.txtStartDate.Text = "";
				this.txtEndDate.Text = "";
				this.strURL = "&jw=" + this.jw;
				switch (Convert.ToInt32(base.Request.QueryString["Type"]))
				{
				case 1:
					this.FramUrl = "ProjectInfo/InfoList.aspx";
					this.Session["PrjState"] = base.Request.QueryString["State"];
					break;
				case 2:
					this.FramUrl = "ProjectDistribute/DistributeList.aspx";
					break;
				case 3:
					this.FramUrl = "../ProjectOver/ProjectList.aspx";
					break;
				}
				this.hdnUrl.Value = this.FramUrl;
			}
		}
		private PrjInfoModel GetValue()
		{
			PrjInfoModel prjInfoModel = new PrjInfoModel();
			if (this.txtStartDate.Text != "")
			{
				prjInfoModel.StartDate = Convert.ToDateTime(this.txtStartDate.Text.ToString());
			}
			if (this.txtEndDate.Text != "")
			{
				prjInfoModel.EndDate = Convert.ToDateTime(this.txtEndDate.Text.ToString());
			}
			prjInfoModel.PrjCode = this.txtPrjNum.Text;
			prjInfoModel.PrjName = this.txtPrjName.Text;
			prjInfoModel.Owner = this.txtprjUnit.Text;
			prjInfoModel.PrjKindClass = this.drop_PrjKindClass.SelectedValue;
			prjInfoModel.Remark = "1";
			prjInfoModel.PrjCost = this.DropDownList1.SelectedValue;
			try
			{
				prjInfoModel.PrjPlace = this.ddt_Area.SelectedValue;
			}
			catch
			{
			}
			prjInfoModel.PrjState = this.ddt_state.SelectedValue;
			return prjInfoModel;
		}
		private void ddt_Area_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.ddt_Area, 19);
		}
		private void Drop_PrjKindClass_Bind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.drop_PrjKindClass, 3);
		}
		private void ddt_stateBind()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.ddt_state, 7);
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.FramUrl = this.hdnUrl.Value;
			this.strURL = PrjInfoAction.GetSqlWhere(this.GetValue()) + "&jw=" + this.jw;
		}
	}


