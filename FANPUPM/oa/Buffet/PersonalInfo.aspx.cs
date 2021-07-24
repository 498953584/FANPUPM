using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Buffet_PersonalInfo : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.setData(this.Session["yhdm"].ToString());
		}
	}
	protected void setData(string userCode)
	{
		DataTable dataTable = PersonnelAction.QueryPersonnelById(userCode);
		if (dataTable.Rows.Count == 1)
		{
			this.LbName.Text = dataTable.Rows[0]["v_xm"].ToString();
			this.LbDeptName.Text = PersonnelAction.getDeptName(Convert.ToInt32(dataTable.Rows[0]["i_bmdm"].ToString()));
			if (dataTable.Rows[0]["Sex"].ToString() == "1")
			{
				this.LbSex.Text = "男";
			}
			else
			{
				if (dataTable.Rows[0]["Sex"].ToString() == "2")
				{
					this.LbSex.Text = "女";
				}
			}
			this.LbNation.Text = dataTable.Rows[0]["Nation"].ToString();
			this.LbMobilePhoneCode.Text = dataTable.Rows[0]["MobilePhoneCode"].ToString();
			if (dataTable.Rows[0]["Birthday"].ToString() != "")
			{
				this.LbBirthday.Text = Convert.ToDateTime(dataTable.Rows[0]["Birthday"]).ToString("yyyy-MM-dd");
			}
			this.LbIDCard.Text = dataTable.Rows[0]["IDCard"].ToString();
			this.LbPostAndCompetency.Text = dataTable.Rows[0]["PostAndCompetency"].ToString();
			if (dataTable.Rows[0]["UserPhoto"].ToString() != "")
			{
				this.Image1.ImageUrl = "~" + dataTable.Rows[0]["UserPhoto"].ToString();
				this.Image1.AlternateText = dataTable.Rows[0]["v_xm"].ToString() + "照片";
			}
		}
	}
}


