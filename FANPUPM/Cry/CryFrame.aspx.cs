using ASP;
using com.jwsoft.pm.web;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class CryFrame : PageBase, IRequiresSessionState
	{

		public int Type
		{
			get
			{
				object obj = this.ViewState["TYPE"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TYPE"] = value;
			}
		}
		private void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["t"] == null)
			{
				this.RegisterStartupScript("错误提示", "<script>alert('参数错误！');</script>");
				return;
			}
			try
			{
				this.Type = int.Parse(base.Request["t"].ToString().Trim());
			}
			catch
			{
				this.RegisterStartupScript("错误提示", "<script>alert('参数错误！');</script>");
				return;
			}
			if (!this.Page.IsPostBack)
			{
				this.UCProjectList.UserCode = base.UserCode;
				this.Data_Bind(this.Type);
				this.UCProjectList.TargetFrame = "InfoList";
			}
			if (this.UCProjectList.SubPrjUrl != "")
			{
				if (this.UCProjectList.SubPrjUrl.IndexOf("?") > 0)
				{
					this.Page.RegisterStartupScript("", string.Concat(new object[]
					{
						"<script language=\"javascript\">InfoList.location = '",
						this.UCProjectList.SubPrjUrl,
						"&PrjCode=&PrjGuid=",
						Guid.Empty,
						"&pc=",
						Guid.Empty,
						"&pn=&cc=';</script>"
					}));
					return;
				}
				this.Page.RegisterStartupScript("", string.Concat(new object[]
				{
					"<script language=\"javascript\">InfoList.location = '",
					this.UCProjectList.SubPrjUrl,
					"?PrjCode=&PrjGuid=",
					Guid.Empty,
					"&pc=",
					Guid.Empty,
					"&pn=&cc=';</script>"
				}));
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new EventHandler(this.Page_Load);
		}
		private void Data_Bind(int intType)
		{
			switch (intType)
			{
			case 1:
				this.UCProjectList.SubPrjUrl = "MaterialExpendInfoTab.aspx?t=1";
				return;
			case 2:
				this.UCProjectList.SubPrjUrl = "MaterialExpendAccount.aspx?t=2";
				return;
			case 3:
				this.UCProjectList.SubPrjUrl = "StructureExpendAccountTab.aspx";
				return;
			case 4:
				this.UCProjectList.SubPrjUrl = "MainMaterialExpendTab.aspx?t=1";
				return;
			case 5:
				this.UCProjectList.SubPrjUrl = "MainMaterialAndStructureDifference.aspx?t=2";
				return;
			case 6:
				this.UCProjectList.SubPrjUrl = "Equipment/EquipUserFeeDifferenceTab.aspx?t=2";
				return;
			case 7:
				this.UCProjectList.SubPrjUrl = "Equipment/EquipUserAccount.aspx?t=1";
				return;
			case 11:
				this.UCProjectList.SubPrjUrl = "ProjectCost/MonthDirectCostTab.aspx";
				return;
			case 12:
				this.UCProjectList.SubPrjUrl = "ProjectCost/MonthIndirectCostTab.aspx";
				return;
			case 13:
				this.UCProjectList.SubPrjUrl = "ProjectCost/ProjectInProcessCostTab.aspx";
				return;
			case 14:
				this.UCProjectList.SubPrjUrl = "ProjectCost/DirectTargetCostTab.aspx";
				return;
			}
			this.UCProjectList.SubPrjUrl = "MaterialExpendInfoTab.aspx?t=1";
		}
	}


