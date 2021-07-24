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
public partial class oa_eFile_FileLendList : BasePage, IRequiresSessionState
{

	private eFileLendAction efla
	{
		get
		{
			return new eFileLendAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_06_0 = base.IsPostBack;
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			string text = ((DataRowView)e.Row.DataItem)["RecordID"].ToString();
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				text,
				"','",
				dataRowView["LendState"].ToString(),
				"','",
				e.Row.Cells[1].Text,
				"','",
				dataRowView["AuditState"].ToString(),
				"');"
			});
			string text2;
			if ((text2 = e.Row.Cells[1].Text) != null)
			{
				if (!(text2 == "1"))
				{
					if (!(text2 == "2"))
					{
						if (text2 == "3")
						{
							e.Row.Cells[1].Text = "绝密";
						}
					}
					else
					{
						e.Row.Cells[1].Text = "机密";
					}
				}
				else
				{
					e.Row.Cells[1].Text = "秘密";
				}
			}
			string a;
			if ((a = dataRowView["LendState"].ToString()) != null)
			{
				if (!(a == "0"))
				{
					if (!(a == "1"))
					{
						if (!(a == "2"))
						{
							if (!(a == "3"))
							{
								if (a == "4")
								{
									e.Row.Cells[5].Text = "已归还";
								}
							}
							else
							{
								e.Row.Cells[5].Text = "归还申请";
							}
						}
						else
						{
							e.Row.Cells[5].Text = "已借出";
						}
					}
					else
					{
						e.Row.Cells[5].Text = "未借出";
					}
				}
				else
				{
					e.Row.Cells[5].Text = "未申请";
				}
			}
			switch (int.Parse(dataRowView["ReturnType"].ToString()))
			{
			case 0:
				e.Row.Cells[6].Text = "";
				return;
			case 1:
				e.Row.Cells[6].Text = "主动";
				return;
			case 2:
				e.Row.Cells[6].Text = "被动";
				break;
			default:
				return;
			}
		}
	}
	protected void btnBorrow_Click(object sender, EventArgs e)
	{
		eFileLend eFileLend = new eFileLend();
		eFileLend.LendDate = DateTime.Now;
		eFileLend.LendState = "2";
		eFileLend.RecordID = new Guid(this.HdnRecoreId.Value);
		if (this.efla.BorrowUpdate(eFileLend) == 1)
		{
			this.JS.Text = "alert('确认成功!');";
			this.GVeFileLend.DataBind();
			return;
		}
		this.JS.Text = "alert('确认失败!');";
	}
	protected void btnReturn_Click(object sender, EventArgs e)
	{
		eFileLend eFileLend = new eFileLend();
		eFileLend.LendDate = DateTime.Now;
		eFileLend.LendState = "4";
		eFileLend.ReturnType = 2;
		eFileLend.RecordID = new Guid(this.HdnRecoreId.Value);
		if (this.efla.CompelReturnUpdate(eFileLend) == 1)
		{
			this.JS.Text = "alert('强制归还成功!');";
			this.GVeFileLend.DataBind();
			return;
		}
		this.JS.Text = "alert('强制归还失败!');";
	}
	protected void BtnSearch_Click(object sender, EventArgs e)
	{
		if (this.DropDownList1.SelectedValue == "1")
		{
			SqlDataSource expr_1D = this.SqleFileLend;
			expr_1D.SelectCommand = expr_1D.SelectCommand + " and FileTitle like '%" + this.TxtSearchCount.Text.Trim() + "%'";
		}
		else
		{
			if (this.DropDownList1.SelectedValue == "2")
			{
				SqlDataSource expr_66 = this.SqleFileLend;
				expr_66.SelectCommand = expr_66.SelectCommand + " and v_xm like '%" + this.TxtSearchCount.Text.Trim() + "%'";
			}
		}
		this.GVeFileLend.DataBind();
	}
}


