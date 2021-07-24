using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Leave_StatCountList : BasePage, IRequiresSessionState
{
	private ArrayList Depdm = new ArrayList();
	private string sWhere;

	protected StatAction sa
	{
		get
		{
			return new StatAction();
		}
	}
	protected int bmdm
	{
		get
		{
			return Convert.ToInt32(this.ViewState["BMDM"]);
		}
		set
		{
			this.ViewState["BMDM"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			DataTable dataTable = this.sa.YearList();
			if (dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					this.DDLYear.Items.Add(new ListItem(dataTable.Rows[i][0].ToString(), dataTable.Rows[i][0].ToString()));
				}
			}
			else
			{
				this.DDLYear.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
			}
			this.DDLYear.SelectedValue = DateTime.Now.Year.ToString();
			this.DDLMonth.SelectedValue = DateTime.Now.Month.ToString();
			if (base.Request["bm"] != null)
			{
				this.bmdm = Convert.ToInt32(base.Request["bm"]);
				this.Hidstate.Value = "0";
				this.GVStat.DataSource = this.InitDataTable();
				this.GVStat.DataBind();
				this.btnEdit.Attributes["onclick"] = "return openEdit('Edit');";
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
			}
		}
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		if (this.Hidstate.Value == "0")
		{
			this.GVStat.DataSource = this.InitDataTable();
			this.GVStat.DataBind();
			return;
		}
		this.GVStat.DataSource = this.SearchDataTable(this.GetWhere());
		this.GVStat.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.sa.Delete(Convert.ToInt32(this.DDLYear.SelectedValue), Convert.ToInt32(this.DDLMonth.SelectedValue), this.HdnUserCode.Value.ToString()) != 1)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('删除失败！');", true);
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('删除成功！');", true);
		if (this.Hidstate.Value == "0")
		{
			this.GVStat.DataSource = this.InitDataTable();
			this.GVStat.DataBind();
			return;
		}
		this.GVStat.DataSource = this.SearchDataTable(this.GetWhere());
		this.GVStat.DataBind();
	}
	protected void BtnSearch_Click(object sender, EventArgs e)
	{
		this.Hidstate.Value = "1";
		if (base.Request.QueryString["bm"] != null)
		{
			this.GVStat.DataSource = this.SearchDataTable(this.GetWhere());
			this.GVStat.DataBind();
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('请选择部门！');", true);
	}
	protected void GVStat_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DateTime dateTime;
			if (string.IsNullOrEmpty(this.DDLYear.SelectedValue) || string.IsNullOrEmpty(this.DDLMonth.SelectedValue))
			{
				dateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
			}
			else
			{
				dateTime = Convert.ToDateTime(this.DDLYear.SelectedValue + "-" + this.DDLMonth.SelectedValue + "-01");
			}
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new object[]
			{
				"OnRecord(this);ClickRow('",
				this.bmdm,
				"','",
				dateTime,
				"','",
				e.Row.Cells[1].Text,
				"');"
			});
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[1].Text = userManageDb.GetUserName(e.Row.Cells[1].Text);
			if (dataRowView["Holiday8"].ToString() == "")
			{
				e.Row.Cells[11].Text = "0.0";
			}
			if (dataRowView["FactDay"].ToString() == "")
			{
				e.Row.Cells[12].Text = "0.0";
			}
		}
	}
	protected void btnStat_Click(object sender, EventArgs e)
	{
		this.Hidstate.Value = "0";
		DateTime countDate = Convert.ToDateTime(this.DDLYear.SelectedValue + "-" + this.DDLMonth.SelectedValue + "-01");
		this.sa.ApplicationCount(this.bmdm, countDate);
		this.GVStat.DataSource = this.InitDataTable();
		this.GVStat.DataBind();
	}
	protected void GetNextDepar(string DepNO)
	{
		DataTable bMList = this.sa.GetBMList(this.Session["CorpCode"].ToString(), DepNO);
		this.Depdm.Add(DepNO);
		if (bMList.Rows.Count > 0)
		{
			foreach (DataRow dataRow in bMList.Rows)
			{
				this.Depdm.Add(dataRow["i_bmdm"].ToString());
				this.GetNextDepar(dataRow["i_bmdm"].ToString());
			}
		}
	}
	protected DataTable InitDataTable()
	{
		this.GetNextDepar(this.bmdm.ToString());
		this.sWhere = " DeptCode in(0";
		for (int i = 0; i < this.Depdm.Count; i++)
		{
			this.sWhere = this.sWhere + "," + this.Depdm[i].ToString();
		}
		string text = this.sWhere;
		this.sWhere = string.Concat(new string[]
		{
			text,
			" ) and iYear='",
			this.DDLYear.Text,
			"' and iMonth='",
			this.DDLMonth.Text,
			"' "
		});
		this.sWhere += "and UserCode in (select v_yhdm from pt_yhmc where c_sfyx='y' and State!=2) ";
		return this.GetList(this.sWhere);
	}
	protected DataTable GetList(string strWhere)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("select iYear,iMonth,UserCode,DeptCode,Later,LeaveEarly,Holiday1,Holiday2,Holiday3,Holiday4,Holiday5,Holiday6,Holiday7,Holiday8,FactDay ");
		stringBuilder.Append(" FROM HR_Leave_Stat ");
		if (strWhere.Trim() != "")
		{
			stringBuilder.Append(" where " + strWhere);
		}
		return publicDbOpClass.DataTableQuary(stringBuilder.ToString());
	}
	protected void btnExp_Click1(object sender, EventArgs e)
	{
		this.ExportToExcel("application/ms-excel", this.DDLYear.Text + "年" + this.DDLMonth.Text + "月考勤信息.xls");
	}
	public void ExportToExcel(string FileType, string FileName)
	{
		base.Response.Charset = "GB2312";
		base.Response.ContentEncoding = Encoding.UTF8;
		base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
		base.Response.ContentType = FileType;
		this.EnableViewState = false;
		StringWriter stringWriter = new StringWriter();
		HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
		GridView gridView = new GridView();
		gridView.AllowPaging = false;
		if (this.Hidstate.Value == "0")
		{
			gridView.DataSource = this.FormateExportTable(this.InitDataTable());
		}
		if (this.Hidstate.Value == "1")
		{
			gridView.DataSource = this.FormateExportTable(this.SearchDataTable(this.GetWhere()));
		}
		gridView.DataBind();
		gridView.RenderControl(writer);
		base.Response.Write(stringWriter.ToString());
		base.Response.End();
	}
	protected void GVStat_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVStat.PageIndex = e.NewPageIndex;
		if (this.Hidstate.Value == "0")
		{
			this.GVStat.DataSource = this.InitDataTable();
			this.GVStat.DataBind();
			return;
		}
		this.GVStat.DataSource = this.SearchDataTable(this.GetWhere());
		this.GVStat.DataBind();
	}
	protected string GetWhere()
	{
		string text = "1=1 and DeptCode = " + this.bmdm;
		if (base.Request.QueryString["bm"] != null)
		{
			if (!string.IsNullOrEmpty(this.DDLYear.SelectedValue.ToString()))
			{
				text = text + "  and iYear = " + this.DDLYear.SelectedValue;
			}
			if (!string.IsNullOrEmpty(this.DDLMonth.SelectedValue.ToString()))
			{
				text = text + "  and iMonth = " + this.DDLMonth.SelectedValue;
			}
			text += " AND UserCode IN (select v_yhdm from pt_yhmc where c_sfyx='y' and State!=2) ";
		}
		else
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('请选择部门！');", true);
		}
		return text;
	}
	protected DataTable SearchDataTable(string strwhere)
	{
		DataTable list = this.sa.GetList(strwhere);
		if (list.Rows.Count <= 0)
		{
			DateTime countDate = Convert.ToDateTime(this.DDLYear.SelectedValue + "-" + this.DDLMonth.SelectedValue + "-01");
			this.sa.ApplicationCount(this.bmdm, countDate);
		}
		return list;
	}
	protected DataTable FormateExportTable(DataTable dt2)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("姓名", typeof(string));
		dataTable.Columns.Add("迟到", typeof(string));
		dataTable.Columns.Add("早退", typeof(string));
		dataTable.Columns.Add("事假", typeof(string));
		dataTable.Columns.Add("带薪年假", typeof(string));
		dataTable.Columns.Add("带薪婚假", typeof(string));
		dataTable.Columns.Add("工伤", typeof(string));
		dataTable.Columns.Add("病假", typeof(string));
		dataTable.Columns.Add("产假", typeof(string));
		dataTable.Columns.Add("丧假", typeof(string));
		userManageDb userManageDb = new userManageDb();
		for (int i = 0; i < dt2.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["姓名"] = userManageDb.GetUserName(dt2.Rows[i]["UserCode"].ToString());
			dataRow["迟到"] = dt2.Rows[i]["Later"].ToString();
			dataRow["早退"] = dt2.Rows[i]["LeaveEarly"].ToString();
			dataRow["事假"] = dt2.Rows[i]["Holiday1"].ToString();
			dataRow["带薪年假"] = dt2.Rows[i]["Holiday3"].ToString();
			dataRow["带薪婚假"] = dt2.Rows[i]["Holiday2"].ToString();
			dataRow["工伤"] = dt2.Rows[i]["Holiday4"].ToString();
			dataRow["病假"] = dt2.Rows[i]["Holiday5"].ToString();
			dataRow["产假"] = dt2.Rows[i]["Holiday6"].ToString();
			dataRow["丧假"] = dt2.Rows[i]["Holiday7"].ToString();
			dataTable.Rows.Add(dataRow);
		}
		return dataTable;
	}
}


