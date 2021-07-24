using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_IssuePayInfoMonth : BasePage, IRequiresSessionState
{
	protected int Tempid
	{
		get
		{
			return Convert.ToInt32(this.ViewState["TEMPID"]);
		}
		set
		{
			this.ViewState["TEMPID"] = value;
		}
	}
	protected int Year
	{
		get
		{
			return Convert.ToInt32(this.ViewState["YEAR"]);
		}
		set
		{
			this.ViewState["YEAR"] = value;
		}
	}
	protected string bmdm
	{
		get
		{
			return this.ViewState["BMDM"].ToString();
		}
		set
		{
			this.ViewState["BMDM"] = value;
		}
	}
	protected SalaryIPIAction sipi
	{
		get
		{
			return new SalaryIPIAction();
		}
	}
	protected string CorpCode
	{
		get
		{
			return this.ViewState["CORPCODE"].ToString();
		}
		set
		{
			this.ViewState["CORPCODE"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && (base.Request["temid"] != null || base.Request["year"] != null || base.Request["bm"] != null))
		{
			this.Tempid = Convert.ToInt32(base.Request["temid"]);
			this.Year = Convert.ToInt32(base.Request["year"]);
			this.bmdm = base.Request["bm"].ToString();
			this.CBLMonth.SelectedValue = DateTime.Now.Month.ToString();
			this.CorpCode = this.sipi.strCorpCode(this.bmdm);
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.IssuePayInfo())
		{
			this.JS.Text = "alert('你选择月份的工资表存在!');";
			return;
		}
		if (this.sipi.Add(this.getSalaryIPIModel(), this.bmdm) == 1)
		{
			this.JS.Text = "alert('生成成功！');returnValue=true;window.close();";
			return;
		}
		this.JS.Text = "alert('生成失败！');";
	}
	protected bool IssuePayInfo()
	{
		DataTable list = this.sipi.GetList(string.Concat(new object[]
		{
			" IssueYear = ",
			this.Year,
			"  and IssueMonth ='",
			this.CBLMonth.SelectedValue,
			"' and TempletID =",
			this.Tempid,
			" and CorpCode = '",
			this.CorpCode,
			"'"
		}));
		return list.Rows.Count > 0;
	}
	protected SalaryIPIModel getSalaryIPIModel()
	{
		return new SalaryIPIModel
		{
			RecordID = Guid.NewGuid(),
			AuditState = new int?(-1),
			CorpCode = this.CorpCode,
			ClassCode = this.sipi.strClassCode(this.Tempid),
			IsIssuePay = "0",
			IssueMonth = Convert.ToInt32(this.CBLMonth.SelectedValue),
			IssueYear = this.Year,
			TempletID = new int?(this.Tempid),
			Remark = this.txtRemark.Text,
			RecordDate = new DateTime?(DateTime.Now),
			UserCode = this.Session["yhdm"].ToString()
		};
	}
}


