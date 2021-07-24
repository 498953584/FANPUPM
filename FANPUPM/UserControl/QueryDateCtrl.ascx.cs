using ASP;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class EPC_UserControl1_QueryDateCtrl : System.Web.UI.UserControl
{
	public HiddenField txt;

	public string DateType
	{
		get
		{
			return this.ViewState["DateType"].ToString();
		}
		set
		{
			this.ViewState["DateType"] = value;
		}
	}
	public string Year
	{
		get
		{
			return this.ViewState["Year"].ToString();
		}
		set
		{
			this.ViewState["Year"] = value;
		}
	}
	public string Month
	{
		get
		{
			return this.ViewState["Month"].ToString();
		}
		set
		{
			this.ViewState["Month"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.setControlText(this.DateType);
		}
	}
	public void setControlText(string type)
	{
		this.txt = (HiddenField)this.Parent.FindControl("YMDBox");
		if (type == "Y")
		{
			this.lblName.Text = "年度:";
			this.btnPre.Text = "前一年";
			this.btnNext.Text = "后一年";
			this.btnNow.Text = "当 年";
			this.txtDate.Visible = false;
			this.txtMonth.Visible = false;
			this.ddlYear.Visible = true;
			this.BindProjectYear();
			this.txt.Value = this.ddlYear.SelectedValue;
			return;
		}
		if (type == "M")
		{
			this.lblName.Text = "月份:";
			this.btnPre.Text = "前一月";
			this.btnNext.Text = "后一月";
			this.btnNow.Text = "当 月";
			this.ddlYear.Visible = false;
			this.txtDate.Visible = false;
			this.txtMonth.Visible = true;
			try
			{
				if (this.Month != "")
				{
					this.txtMonth.Text = this.Month;
				}
			}
			catch (Exception)
			{
				this.txtMonth.Text = DateTime.Now.ToString("yyyy-MM");
			}
			this.hdnMonth.Value = this.txtMonth.Text;
			this.txt.Value = this.txtMonth.Text;
			this.txtMonth.Attributes.Add("onfocus", "showDiv(this);");
			return;
		}
		if (type == "D")
		{
			this.lblName.Text = "日期:";
			this.btnPre.Text = "前一日";
			this.btnNext.Text = "后一日";
			this.btnNow.Text = "当日";
			this.ddlYear.Visible = false;
			this.txtMonth.Visible = false;
			this.txtDate.Visible = true;
			this.txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			this.txt.Value = this.txtDate.Text;
			this.hdnDate.Value = this.txtDate.Text;
		}
	}
	private void BindProjectYear()
	{
		DataTable projectYears = PMAction.GetProjectYears();
		if (projectYears.Rows.Count > 0)
		{
			int num = (projectYears.Rows[0]["BeginYear"] == DBNull.Value) ? DateTime.Today.Year : int.Parse(projectYears.Rows[0]["BeginYear"].ToString());
			int num2 = (projectYears.Rows[0]["EndYear"] == DBNull.Value) ? DateTime.Today.Year : int.Parse(projectYears.Rows[0]["EndYear"].ToString());
			for (int i = num; i <= num2; i++)
			{
				this.ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
			}
		}
		try
		{
			try
			{
				if (this.Year != "")
				{
					this.ddlYear.SelectedValue = this.Year;
				}
			}
			catch (Exception)
			{
				this.ddlYear.SelectedValue = DateTime.Today.Year.ToString();
			}
		}
		catch
		{
		}
	}
	protected void btnPre_Click(object sender, EventArgs e)
	{
		this.setTime(-1);
	}
	protected void btnNow_Click(object sender, EventArgs e)
	{
		this.txt = (HiddenField)this.Parent.FindControl("YMDBox");
		if (this.DateType == "D")
		{
			this.txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			this.txt.Value = this.txtDate.Text;
			return;
		}
		if (this.DateType == "Y")
		{
			this.ddlYear.SelectedValue = DateTime.Today.Year.ToString();
			this.txt.Value = this.ddlYear.SelectedValue;
			return;
		}
		if (this.DateType == "M")
		{
			this.txtMonth.Text = DateTime.Now.ToString("yyyy-MM");
			this.txt.Value = this.txtMonth.Text;
		}
	}
	protected void btnNext_Click(object sender, EventArgs e)
	{
		this.setTime(1);
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
	}
	private void setTime(int isPreOrNext)
	{
		this.txt = (HiddenField)this.Parent.FindControl("YMDBox");
		if (this.DateType == "D")
		{
			this.txtDate.Text = Convert.ToDateTime(this.txtDate.Text).AddDays((double)isPreOrNext).ToString("yyyy-MM-dd");
			this.txt.Value = this.txtDate.Text;
			return;
		}
		if (this.DateType == "Y")
		{
			if (isPreOrNext == -1 && Convert.ToInt32(this.ddlYear.Items[0].Value) < Convert.ToInt32(this.ddlYear.SelectedValue))
			{
				this.ddlYear.SelectedValue = Convert.ToDateTime(this.ddlYear.Text + "-01-01").AddYears(-1).ToString("yyyy");
			}
			if (isPreOrNext == 1 && Convert.ToInt32(this.ddlYear.Items[this.ddlYear.Items.Count - 1].Value) > Convert.ToInt32(this.ddlYear.SelectedValue))
			{
				this.ddlYear.SelectedValue = Convert.ToDateTime(this.ddlYear.Text + "-01-01").AddYears(1).ToString("yyyy");
			}
			this.txt.Value = this.ddlYear.SelectedValue;
			return;
		}
		if (this.DateType == "M")
		{
			this.txtMonth.Text = Convert.ToDateTime(this.txtMonth.Text).AddMonths(isPreOrNext).ToString("yyyy-MM");
			this.txt.Value = this.txtMonth.Text;
		}
	}
	protected void txtDate_TextChanged(object sender, EventArgs e)
	{
		this.txt = (HiddenField)this.Parent.FindControl("YMDBox");
		this.txt.Value = this.txtDate.Text.ToString();
	}
	protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.txt = (HiddenField)this.Parent.FindControl("YMDBox");
		this.txt.Value = this.ddlYear.SelectedValue.ToString();
	}
	protected void txtMonth_TextChanged(object sender, EventArgs e)
	{
		this.txt = (HiddenField)this.Parent.FindControl("YMDBox");
		this.txt.Value = this.txtMonth.Text.ToString();
	}
}


