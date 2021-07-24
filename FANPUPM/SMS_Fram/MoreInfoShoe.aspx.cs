using ASP;
using cn.justwin.BLL;
using cn.justwin.SMS;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SMS_Fram_MoreInfoShoe : NBasePage, IRequiresSessionState
{
	private int PageSize;
	private int PageCount;
	private int RecordCount;
	private int CurrentPage;
	private SqlConnection myCon = new SMS().MSConnection();

	protected void Page_Load(object sender, EventArgs e)
	{
		this.PageSize = 25;
		if (!base.IsPostBack)
		{
			this.ViewState["IsSearchClick"] = false;
			this.CurrentPage = 0;
			this.ViewState["pageindex"] = 0;
			this.RecordCount = this.GetRecordNumber();
			this.ViewState["recordcount"] = this.RecordCount;
			this.PageCount = (this.RecordCount + this.PageSize - 1) / this.PageSize;
			this.ViewState["pagecount"] = this.PageCount;
			this.lblPageCounts.Text = this.PageCount.ToString();
			this.CurPageIndex.Text = (this.CurrentPage + 1).ToString();
			this.bindDataList();
		}
	}
	protected void bindDataList()
	{
		string text = "select * from sendedoutbox where 1=1";
		if (base.UserCode != "00000000")
		{
			text = text + " AND substring(username,1,8)='" + base.UserCode + "'";
		}
		if ((bool)this.ViewState["IsSearchClick"])
		{
			text = this.GetQuery(text);
		}
		text += " order by sendtime desc";
		this.dlRecorde.DataSource = this.CreateSource(text);
		this.dlRecorde.DataBind();
	}
	protected int GetRecordNumber()
	{
		string text = "select count(*) from sendedoutbox where 1=1";
		if (base.UserCode != "00000000")
		{
			text = text + " AND substring(username,1,8)='" + base.UserCode + "'";
		}
		SqlCommand sqlCommand = new SqlCommand(text, this.myCon);
		int result = (int)sqlCommand.ExecuteScalar();
		this.myCon.Close();
		return result;
	}
	protected string GetQuery(string strsql)
	{
		strsql = (strsql ?? "");
		if (!string.IsNullOrEmpty(this.txtDateSend.Text))
		{
			strsql = strsql + " and convert(varchar(100),sendtime,23)='" + DateTime.Parse(this.txtDateSend.Text).ToString("yyyy'-'MM'-'dd") + "'";
		}
		if (!string.IsNullOrEmpty(this.txtPeoples.Value))
		{
			strsql = strsql + " and mbno='" + this.txtPeoples.Value.Trim() + "'";
		}
		if (!string.IsNullOrEmpty(this.txtKeyWord.Text))
		{
			strsql = strsql + " and msg like '%" + this.txtKeyWord.Text + "%'";
		}
		return strsql;
	}
	protected DataView CreateSource(string condition)
	{
		int startRecord = this.PageSize * this.CurrentPage;
		DataSet dataSet = new DataSet();
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(condition, this.myCon);
		sqlDataAdapter.Fill(dataSet, startRecord, this.PageSize, "sendedoutbox");
		if (this.myCon.State == ConnectionState.Open)
		{
			this.myCon.Close();
		}
		return dataSet.Tables["sendedoutbox"].DefaultView;
	}
	protected void FirPage_Click(object sender, EventArgs e)
	{
		this.CurrentPage = 0;
		this.CurPageIndex.Text = (this.CurrentPage + 1).ToString();
		this.bindDataList();
	}
	protected void NextPage_Click(object sender, EventArgs e)
	{
		this.CurrentPage = (int)this.ViewState["pageindex"];
		if (this.CurrentPage < (int)this.ViewState["pagecount"] - 1)
		{
			this.CurrentPage++;
		}
		this.ViewState["pageindex"] = this.CurrentPage;
		this.CurPageIndex.Text = ((int)this.ViewState["pageindex"] + 1).ToString();
		this.bindDataList();
	}
	protected void PrePage_Click(object sender, EventArgs e)
	{
		this.CurrentPage = (int)this.ViewState["pageindex"];
		if (this.CurrentPage > 0)
		{
			this.CurrentPage--;
		}
		this.ViewState["pageindex"] = this.CurrentPage;
		this.CurPageIndex.Text = ((int)this.ViewState["pageindex"] + 1).ToString();
		this.bindDataList();
	}
	protected void LastPage_Click(object sender, EventArgs e)
	{
		if ((int)this.ViewState["pageindex"] > 1)
		{
			this.CurrentPage = (int)this.ViewState["pagecount"] - 1;
			this.CurPageIndex.Text = this.ViewState["pagecount"].ToString();
			this.bindDataList();
			return;
		}
		this.CurrentPage = 1;
	}
	protected void BtnSearch_Click(object sender, EventArgs e)
	{
		string text = "select * from sendedoutbox where 1=1";
		if (base.UserCode != "00000000")
		{
			text = text + " AND substring(username,1,8)='" + base.UserCode + "'";
		}
		string text2 = this.GetQuery(text) + " order by sendtime desc";
		DataView dataSource = this.CreateSource(text2);
		DataTable tableByQuery = this.GetTableByQuery(text2);
		if (tableByQuery.Rows.Count > this.PageSize)
		{
			this.CurrentPage = 0;
			this.ViewState["pageindex"] = 0;
			this.RecordCount = tableByQuery.Rows.Count;
			this.PageCount = (this.RecordCount + this.PageSize - 1) / this.PageSize;
			if (this.RecordCount < (int)this.ViewState["recordcount"])
			{
				this.ViewState["pagecount"] = this.PageCount;
				this.ViewState["IsSearchClick"] = true;
				bool arg_114_0 = (bool)this.ViewState["IsSearchClick"];
			}
			else
			{
				this.Session["IsSearchClick"] = false;
				bool arg_142_0 = (bool)this.ViewState["IsSearchClick"];
			}
			this.lblPageCounts.Text = this.PageCount.ToString();
			this.CurPageIndex.Text = (this.CurrentPage + 1).ToString();
		}
		else
		{
			this.lblPageCounts.Text = "1";
			this.CurPageIndex.Text = "1";
		}
		this.dlRecorde.DataSource = dataSource;
		this.dlRecorde.DataBind();
	}
	protected DataTable GetTableByQuery(string strSql)
	{
		DataTable result;
		try
		{
			DataSet dataSet = new DataSet();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strSql, this.myCon);
			sqlDataAdapter.Fill(dataSet);
			result = dataSet.Tables[0];
		}
		catch (Exception arg)
		{
			base.RegisterScript("alert('" + arg + "')");
			this.myCon.Close();
			result = null;
		}
		finally
		{
			this.myCon.Close();
		}
		return result;
	}
	protected string GetNameByPhone(string phoneNumber)
	{
		string sqlString = "select v_xm from pt_yhmc where mobilephonecode='" + phoneNumber + "'";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count > 0)
		{
			return dataTable.Rows[0][0].ToString();
		}
		return phoneNumber;
	}
}


