using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ConstructOrganizeView : NBasePage, System.Web.SessionState.IRequiresSessionState
	{
		private ConstructOrganizeBBl Bll = new ConstructOrganizeBBl();
		private string _Type = "";
		private string _Id = "";
		private string _PrjCode = "";
		private string _PrjName = "";
		private string _RecordId;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
				DataTable dataTable = new DataTable();
				if (base.Request.QueryString["ic"] != null)
				{
					string g = base.Request.QueryString["ic"];
					dataTable = this.Bll.getModelByGuid("Prj_V_ScienceInnovate", new Guid(g));
				}
				else
				{
					if (base.Request.QueryString["id"] != null)
					{
						string strId = base.Request.QueryString["id"];
						dataTable = ConstructOrganizeAction.GetSingleConOrgInfo(strId);
					}
				}
				this.lblUnit.Text = dataTable.Rows[0]["FillUnit"].ToString();
				this.hdnmark.Value = dataTable.Rows[0]["mark"].ToString();
				this.lblName.Text = ((dataTable.Rows[0]["TCO_Name"] != DBNull.Value) ? dataTable.Rows[0]["TCO_Name"].ToString() : "");
				this.lblpeople.Text = dataTable.Rows[0]["WeavePerson"].ToString();
				this.lblDate.Text = Convert.ToDateTime(dataTable.Rows[0]["WeaveTime"].ToString()).ToShortDateString();
				this.lblshuoming.Text = dataTable.Rows[0]["Maindescripe"].ToString();
				this.lblRemark.Text = dataTable.Rows[0]["Remark"].ToString();
				this.Literal1.Text = FileView.FilesBind(1720, dataTable.Rows[0]["FlowGuid"].ToString());
				this.DDTClass.SelectedValue = dataTable.Rows[0]["filesType"].ToString();
				if (dataTable.Rows[0]["mark"].ToString().Equals("2"))
				{
					this.TrGDType.Attributes.Add("style", "display:none;");
				}
				else
				{
					this.TrGDType.Attributes.Add("style", "display:block;");
				}
				this.lblmarkType.Text = this.DDTClass.SelectedItem.Text.ToString();
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
	}


