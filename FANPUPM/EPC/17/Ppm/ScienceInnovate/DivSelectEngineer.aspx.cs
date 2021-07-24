using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_17_Ppm_ScienceInnovate_DivSelectEngineer : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (base.Request.QueryString["Id"] != null)
		{
			this.id = base.Request.QueryString["Id"].ToString();
		}
		this.InitializeComponent();
		base.OnInit(e);
	}
	private void InitializeComponent()
	{
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		this.gvVehicle.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.bind(this.id);
		}
	}
	private void bind(string id)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("SELECT cimt.PrjCode ,cimt.ID,cimt.SerialNumber,cimt.ItemName,cimt.AccessoriesName,");
		stringBuilder.Append(" cimt.Remark,cimt.BigSort,cimt.SmallSort ");
		stringBuilder.Append(" FROM  Con_IncomeModify_Technology  cimt2 ");
		stringBuilder.Append(" LEFT JOIN Prj_TechnologyManage cimt  ON cimt2.TechnologyId=cimt.ID ");
		stringBuilder.Append(" LEFT JOIN Con_Incomet_Modify cim ON cim.ID=cimt2.ConModifyId ");
		stringBuilder.Append(" WHERE cimt2.ConModifyId='" + id + "' ");
		DataTable dataTable = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
		int count = dataTable.Rows.Count;
		if (count > 0)
		{
			this.gvVehicle.DataSource = dataTable.DefaultView;
			this.gvVehicle.DataBind();
		}
	}
	protected void gvVehicle_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"clickRow(this,'",
				dataRowView["ID"].ToString(),
				"','",
				dataRowView["SerialNumber"].ToString(),
				"');"
			});
			string value = dataRowView["ID"].ToString();
			e.Row.Attributes["ondblclick"] = string.Concat(new string[]
			{
				"dbClickRow(this,'",
				dataRowView["ID"].ToString(),
				"','",
				dataRowView["SerialNumber"].ToString(),
				"')"
			});
			e.Row.Attributes["ID"] = value;
			e.Row.Attributes["style"] = "cursor:hand";
			HyperLink hyperLink = (HyperLink)e.Row.Cells[3].Controls[0];
			int t = int.Parse(dataRowView["BigSort"].ToString());
			string text = this.switchTypeName(t);
			text = "查看" + text;
			hyperLink.Attributes["onclick"] = string.Concat(new object[]
			{
				"queryEngineer('/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?Id=",
				dataRowView["ID"].ToString(),
				"&bs=",
				dataRowView["BigSort"],
				"&TYPE=View','",
				text,
				"')"
			});
		}
	}
	protected void gvVehicle_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvVehicle.PageIndex = e.NewPageIndex;
		this.bind(this.id);
	}
	public string GetAnnexN(string Id)
	{
		string text = "";
		return text.ToString();
	}
	public string switchTypeName(int T)
	{
		string result = string.Empty;
		switch (T)
		{
		case 7:
			result = "工程确认单";
			break;
		case 8:
			result = "工程洽商单";
			break;
		}
		return result;
	}
}


