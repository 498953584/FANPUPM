using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ShowMember : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			string strDeptCode = "";
			string strIsValid = "y";
			try
			{
				strDeptCode = base.Request.QueryString["bmdm"].Trim();
				strIsValid = base.Request.QueryString["yx"].Trim();
			}
			catch (Exception)
			{
			}
			this.GetDeptName(strDeptCode);
			this.DGrdMember_DataBind(strDeptCode, strIsValid);
		}
		private void GetDeptName(string strDeptCode)
		{
			DeptManageDb deptManageDb = new DeptManageDb();
			DataTable deptmentDetail = deptManageDb.GetDeptmentDetail(Convert.ToInt32(strDeptCode.Trim()));
			if (deptmentDetail.Rows.Count > 0)
			{
				this.LabDept.Text = deptmentDetail.Rows[0]["v_bmqc"].ToString() + "人员列表：";
			}
		}
		private void DGrdMember_DataBind(string strDeptCode, string strIsValid)
		{
			userManageDb userManageDb = new userManageDb();
			DataTable dataTable = userManageDb.DepUserQuaryDt(strDeptCode, strIsValid);
			this.DGrdMember.DataSource = dataTable.DefaultView;
			this.DGrdMember.DataBind();
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


