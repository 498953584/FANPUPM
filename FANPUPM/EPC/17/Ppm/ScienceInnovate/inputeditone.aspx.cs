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
	public partial class InputEditOne : BasePage, System.Web.SessionState.IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["prjCode"] != null)
				{
					this.hidPrjCode.Value = base.Request.Params["prjCode"].ToString();
					this.txtPrjName.Value = base.Request.Params["prjName"].ToString();
					this.txtFillTime.Text = DateTime.Today.ToShortDateString();
				}
				if (base.Request.Params["MainId"] != null)
				{
					this.hidMainId.Value = base.Request.Params["MainId"].ToString();
					this.BindData(int.Parse(this.hidMainId.Value));
					return;
				}
				UserInfo currentUserInfo = userManageDb.GetCurrentUserInfo();
				if (userManageDb.GetCurrentUserInfo() != null)
				{
					this.hidUnitCode.Value = currentUserInfo.UserDepartCode;
					this.txtUnitName.Value = currentUserInfo.UserDepartName;
					this.txtWeavePeople.Text = currentUserInfo.UserName;
					this.txtScienceEmpolderMoney.Text = "0";
					return;
				}
				this.js.Text = "alert(\"您登陆时间过期，请重新登录！\");window.close();";
			}
		}
		private void BindData(int MainId)
		{
			DevelopmentInputAction developmentInputAction = new DevelopmentInputAction();
			DevelopmentInputInfo mainInputInfo = developmentInputAction.GetMainInputInfo(MainId);
			this.hidPrjCode.Value = mainInputInfo.PrjCode;
			this.txtPrjName.Value = mainInputInfo.PrjName;
			this.hidUnitCode.Value = mainInputInfo.UnitCode;
			this.txtUnitName.Value = mainInputInfo.UnitName;
			this.txtWeavePeople.Text = mainInputInfo.WeavePeople;
			this.txtScienceEmpolderMoney.Text = mainInputInfo.ScienceEmpolderMoney.ToString();
			this.txtFillTime.Text = mainInputInfo.FillTime.ToShortDateString();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			int mainId = 0;
			DevelopmentInputInfo developmentInputInfo = new DevelopmentInputInfo();
			developmentInputInfo.PrjCode = this.hidPrjCode.Value;
			developmentInputInfo.UnitCode = this.hidUnitCode.Value;
			developmentInputInfo.FillTime = DateTime.Parse(this.txtFillTime.Text);
			developmentInputInfo.WeavePeople = this.txtWeavePeople.Text;
			developmentInputInfo.ScienceEmpolderMoney = decimal.Parse(this.txtScienceEmpolderMoney.Text);
			if (this.hidMainId.Value == "")
			{
				if (DevelopmentInputAction.AddNewInput(developmentInputInfo, out mainId))
				{
					this.hidMainId.Value = mainId.ToString();
					this.js.Text = "alert(\"操作成功！\");";
				}
				else
				{
					this.js.Text = "alert(\"操作失败！\");";
				}
			}
			else
			{
				mainId = int.Parse(this.hidMainId.Value);
				developmentInputInfo.MainID = int.Parse(this.hidMainId.Value);
				if (DevelopmentInputAction.UpdateInput(developmentInputInfo))
				{
					this.js.Text = "alert(\"操作成功！\");";
				}
				else
				{
					this.js.Text = "alert(\"操作失败！\");";
				}
			}
			this.BindData(mainId);
		}
	}


