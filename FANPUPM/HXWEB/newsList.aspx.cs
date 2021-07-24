using ASP;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HXWEB_newsList : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.ViewState["PAGE"] = "1";
			this.GrindBind(1);
		}
	}
	private void GrindBind(int PageIndex)
	{
		this.txtPage.Text = PageIndex.ToString();
		DataTable dataTable = new DataTable();
		DataTable dataTable2 = new DataTable();
		string text = " select * from Web_News where c_xwlxdm = '03' and c_sfyx='y' order by i_xw_id desc";
		dataTable2 = publicDbOpClass.DataTableQuary(text);
		int num = 0;
		dataTable = publicDbOpClass.GetRecordFromPage(ref num, text, "i_xw_id", 20, PageIndex);
		this.lbPgCount.Text = num.ToString();
		this.lbReCount.Text = dataTable2.Rows.Count.ToString();
		if (this.ViewState["PAGE"].ToString() == "1")
		{
			this.btn_First.Enabled = false;
			this.btn_p.Enabled = false;
			this.btn_n.Enabled = true;
			this.btn_Last.Enabled = true;
		}
		else
		{
			if (this.ViewState["PAGE"].ToString() == this.lbPgCount.Text.Trim())
			{
				this.btn_n.Enabled = false;
				this.btn_Last.Enabled = false;
				this.btn_First.Enabled = true;
				this.btn_p.Enabled = true;
			}
			else
			{
				this.btn_First.Enabled = true;
				this.btn_p.Enabled = true;
				this.btn_n.Enabled = true;
				this.btn_Last.Enabled = true;
			}
		}
		foreach (DataRow dataRow in dataTable.Rows)
		{
			TableCell tableCell = new TableCell();
			TableCell tableCell2 = new TableCell();
			TableRow tableRow = new TableRow();
			TableRow tableRow2 = new TableRow();
			tableCell.Text = string.Concat(new string[]
			{
				"<a href='#' class='subpagelist'>·",
				dataRow["v_xwbt"].ToString(),
				"[",
				Convert.ToDateTime(dataRow["dtm_fbsj"].ToString()).Year.ToString(),
				".",
				Convert.ToDateTime(dataRow["dtm_fbsj"].ToString()).Month.ToString(),
				"]</a>"
			});
			tableCell.Attributes["Height"] = "28";
			tableCell.Attributes["align"] = "left";
			tableCell2.Attributes["align"] = "left";
			tableCell2.Attributes["bgcolor"] = "#EAEAEA";
			tableCell2.Attributes["Height"] = "1";
			tableCell2.Text = "";
			tableRow.Cells.Add(tableCell);
			tableRow2.Cells.Add(tableCell2);
			this.Table1.Rows.Add(tableRow);
			this.Table1.Rows.Add(tableRow2);
		}
	}
	protected void btn_First_Click(object sender, ImageClickEventArgs e)
	{
		this.ViewState["PAGE"] = "1";
		this.GrindBind(1);
	}
	protected void btn_Last_Click(object sender, ImageClickEventArgs e)
	{
		this.ViewState["PAGE"] = this.lbPgCount.Text.Trim();
		this.GrindBind(Convert.ToInt32(this.lbPgCount.Text));
	}
	protected void btn_p_Click(object sender, ImageClickEventArgs e)
	{
		this.ViewState["PAGE"] = Convert.ToString(Convert.ToInt32(this.ViewState["PAGE"].ToString()) - 1);
		this.GrindBind(Convert.ToInt32(Convert.ToInt32(this.ViewState["PAGE"].ToString())));
	}
	protected void btn_n_Click(object sender, ImageClickEventArgs e)
	{
		this.ViewState["PAGE"] = Convert.ToString(Convert.ToInt32(this.ViewState["PAGE"].ToString()) + 1);
		this.GrindBind(Convert.ToInt32(Convert.ToInt32(this.ViewState["PAGE"].ToString())));
	}
	protected void btn_jump_Click(object sender, EventArgs e)
	{
		if (this.txtPage.Text == "")
		{
			this.txtPage.Text = "1";
		}
		this.ViewState["PAGE"] = this.txtPage.Text;
		this.GrindBind(Convert.ToInt32(this.txtPage.Text.Trim()));
	}
}


