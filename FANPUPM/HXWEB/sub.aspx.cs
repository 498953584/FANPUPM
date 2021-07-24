using ASP;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Default2 : Page, IRequiresSessionState
{
	private DataTable dtNews
	{
		get
		{
			return (DataTable)this.ViewState["dtNews"];
		}
		set
		{
			this.ViewState["dtNews"] = value;
		}
	}
	private string TypeCode
	{
		get
		{
			return this.ViewState["TypeCode"].ToString();
		}
		set
		{
			this.ViewState["TypeCode"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			DataTable dataSource = this.createDataSet2();
			this.GridView1.DataSource = dataSource;
			this.GridView1.DataBind();
			this.GridView1.HeaderRow.Visible = false;
			if (base.Request.QueryString["code"] == null)
			{
				this.TypeCode = base.Request.QueryString["type"].ToString();
			}
			else
			{
				this.TypeCode = base.Request.QueryString["code"].ToString();
			}
			DataTable dataTable = this.createDataSet3(this.TypeCode);
			if (dataTable.Rows.Count > 1)
			{
				this.dtNews = dataTable;
				this.gvNewList.DataSource = this.dtNews;
				this.gvNewList.DataBind();
			}
			else
			{
				if (dataTable.Rows.Count == 1)
				{
					this.Lbxwnr.Text = dataTable.Rows[0]["txt_xwnr"].ToString();
				}
			}
			this.lblNavigator.Text = this.getNavigator(this.TypeCode);
		}
	}
	private DataTable createDataSet2()
	{
		DataTable dataTable = new DataTable();
		string sqlString = "select * from Web_NewsCategories where c_xwlxdm like  '" + base.Request.QueryString["type"] + "%' order by c_xwlxdm asc ";
		return publicDbOpClass.DataTableQuary(sqlString);
	}
	private bool IsNode(string c_xwlxdm)
	{
		string sqlString = string.Concat(new object[]
		{
			"select * from Web_NewsCategories where len(c_xwlxdm) > ",
			c_xwlxdm.Length,
			" and c_xwlxdm like '",
			c_xwlxdm,
			"%'"
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		return dataTable.Rows.Count > 0;
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			if (dataRowView["c_xwlxdm"].ToString().Trim().Length == 2)
			{
				if (!this.IsNode(dataRowView["c_xwlxdm"].ToString()))
				{
					System.Web.UI.WebControls.Image image = (System.Web.UI.WebControls.Image)e.Row.FindControl("Img2");
					image.Visible = false;
					Label label = (Label)e.Row.FindControl("Label1");
					label.Text = "";
					Label label2 = (Label)e.Row.FindControl("Label2");
					label2.Text = dataRowView["c_xwlxmc"].ToString();
					label2.ForeColor = Color.Red;
					HyperLink hyperLink = (HyperLink)e.Row.FindControl("HyperLink1");
					hyperLink.Text = "<b>" + dataRowView["c_xwlxmc"].ToString() + " </b> ";
					hyperLink.Attributes["class"] = "main_news_text";
					hyperLink.NavigateUrl = "sub.aspx?type=" + base.Request.QueryString["type"] + "&code=" + dataRowView["c_xwlxdm"].ToString();
					hyperLink.Target = "_self";
					e.Row.Cells[1].Attributes["style"] = "border-top: activeborder 1px solid;";
					return;
				}
				System.Web.UI.WebControls.Image image2 = (System.Web.UI.WebControls.Image)e.Row.FindControl("Img2");
				image2.Visible = false;
				Label label3 = (Label)e.Row.FindControl("Label1");
				label3.Text = "";
				Label label4 = (Label)e.Row.FindControl("Label2");
				label4.Text = dataRowView["c_xwlxmc"].ToString();
				label4.ForeColor = Color.Red;
				HyperLink hyperLink2 = (HyperLink)e.Row.FindControl("HyperLink1");
				hyperLink2.Text = "<b>" + dataRowView["c_xwlxmc"].ToString() + " </b> ";
				hyperLink2.Attributes["class"] = "main_news_text";
				hyperLink2.Target = "_self";
				e.Row.Cells[1].Attributes["style"] = "border-top: activeborder 1px solid;";
				return;
			}
			else
			{
				if (dataRowView["c_xwlxdm"].ToString().Trim().Length == 4)
				{
					if (!this.IsNode(dataRowView["c_xwlxdm"].ToString()))
					{
						System.Web.UI.WebControls.Image image3 = (System.Web.UI.WebControls.Image)e.Row.FindControl("Img2");
						Label label5 = (Label)e.Row.FindControl("Label1");
						image3.ImageUrl = "img/02-Normal.gif";
						label5.Text = "&nbsp;&nbsp;";
						e.Row.Cells[2].Text = "&nbsp;&nbsp;" + dataRowView["c_xwlxmc"].ToString();
						Label label6 = (Label)e.Row.FindControl("Label2");
						label6.Text = dataRowView["c_xwlxmc"].ToString();
						HyperLink hyperLink3 = (HyperLink)e.Row.FindControl("HyperLink1");
						hyperLink3.Text = dataRowView["c_xwlxmc"].ToString();
						hyperLink3.Attributes["class"] = "main_news_text";
						hyperLink3.NavigateUrl = "sub.aspx?type=" + base.Request.QueryString["type"] + "&code=" + dataRowView["c_xwlxdm"].ToString();
						hyperLink3.Target = "_self";
						return;
					}
					System.Web.UI.WebControls.Image image4 = (System.Web.UI.WebControls.Image)e.Row.FindControl("Img2");
					Label label7 = (Label)e.Row.FindControl("Label1");
					image4.ImageUrl = "img/02-Normal.gif";
					label7.Text = "&nbsp;&nbsp;";
					e.Row.Cells[2].Text = "&nbsp;&nbsp;" + dataRowView["c_xwlxmc"].ToString();
					Label label8 = (Label)e.Row.FindControl("Label2");
					label8.Text = dataRowView["c_xwlxmc"].ToString();
					HyperLink hyperLink4 = (HyperLink)e.Row.FindControl("HyperLink1");
					hyperLink4.Text = "<b>" + dataRowView["c_xwlxmc"].ToString() + " </b> ";
					hyperLink4.Attributes["class"] = "main_news_text";
					hyperLink4.Target = "_self";
					return;
				}
				else
				{
					if (dataRowView["c_xwlxdm"].ToString().Trim().Length == 10)
					{
						if (!this.IsNode(dataRowView["c_xwlxdm"].ToString()))
						{
							System.Web.UI.WebControls.Image image5 = (System.Web.UI.WebControls.Image)e.Row.FindControl("Img2");
							Label label9 = (Label)e.Row.FindControl("Label1");
							image5.ImageUrl = "img/03-Normal.gif";
							label9.Text = "&nbsp;&nbsp;&nbsp;&nbsp;";
							e.Row.Cells[2].Text = "&nbsp;" + dataRowView["c_xwlxmc"].ToString();
							Label label10 = (Label)e.Row.FindControl("Label2");
							label10.Text = dataRowView["c_xwlxmc"].ToString();
							HyperLink hyperLink5 = (HyperLink)e.Row.FindControl("HyperLink1");
							hyperLink5.Text = dataRowView["c_xwlxmc"].ToString();
							hyperLink5.Attributes["class"] = "main_news_text";
							hyperLink5.NavigateUrl = "sub.aspx?type=" + base.Request.QueryString["type"] + "&code=" + dataRowView["c_xwlxdm"].ToString();
							hyperLink5.Target = "_self";
							return;
						}
						System.Web.UI.WebControls.Image image6 = (System.Web.UI.WebControls.Image)e.Row.FindControl("Img2");
						Label label11 = (Label)e.Row.FindControl("Label1");
						image6.ImageUrl = "img/03-Normal.gif";
						label11.Text = "&nbsp;&nbsp;&nbsp;&nbsp;";
						e.Row.Cells[2].Text = "&nbsp;" + dataRowView["c_xwlxmc"].ToString();
						Label label12 = (Label)e.Row.FindControl("Label2");
						label12.Text = dataRowView["c_xwlxmc"].ToString();
						HyperLink hyperLink6 = (HyperLink)e.Row.FindControl("HyperLink1");
						hyperLink6.Text = "<b>" + dataRowView["c_xwlxmc"].ToString() + " </b> ";
						hyperLink6.Attributes["class"] = "main_news_text";
						hyperLink6.Target = "_self";
					}
				}
			}
		}
	}
	private string getNavigator(string strTypeCode)
	{
		string text = "";
		string text2 = "select c_xwlxmc from dbo.Web_NewsCategories where c_xwlxdm = '" + base.Request.QueryString["type"].ToString() + "' ";
		for (int i = 2; i <= strTypeCode.Length / 2; i++)
		{
			text2 = text2 + " or c_xwlxdm = '" + strTypeCode.Substring(0, i * 2) + "'";
		}
		text2 += "  order by c_xwlxdm";
		DataTable dataTable = publicDbOpClass.DataTableQuary(text2);
		for (int j = 0; j < dataTable.Rows.Count; j++)
		{
			text = text + "&nbsp;>&nbsp;" + dataTable.Rows[j]["c_xwlxmc"].ToString();
		}
		return text;
	}
	private DataTable createDataSet3(string code)
	{
		DataTable dataTable = new DataTable();
		string sqlString = "select * from Web_News where c_xwlxdm = '" + code + "' and c_sfyx = 'y' order by dtm_fbsj desc ";
		return publicDbOpClass.DataTableQuary(sqlString);
	}
	protected void gvNewList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvNewList.DataSource = this.dtNews;
		this.gvNewList.PageIndex = e.NewPageIndex;
		this.gvNewList.DataBind();
	}
	protected void gvNewList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			LinkButton linkButton = (LinkButton)e.Row.Cells[0].Controls[0];
			string str = "NewsView.aspx?NewsID=" + dataRowView["i_xw_id"].ToString();
			linkButton.Attributes["onclick"] = "javascript:winOpen('" + str + "');return false;";
			linkButton.Text = "·" + linkButton.Text;
			linkButton.Attributes["class"] = "main_news_text";
			e.Row.Cells[1].Text = Convert.ToDateTime(dataRowView["dtm_fbsj"]).ToString("yyyy-MM-dd");
			e.Row.Attributes["style"] = "border-top: activeborder 1px solid;";
		}
	}
}


