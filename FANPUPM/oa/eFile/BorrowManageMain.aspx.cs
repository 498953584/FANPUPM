using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_eFile_BorrowManageMain : BasePage, IRequiresSessionState
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
		base.Response.Cache.SetNoStore();
		string href = base.Request.ApplicationPath + "StyleCss/MenuCss.css";
		HtmlLink htmlLink = new HtmlLink();
		htmlLink.Href = href;
		htmlLink.Attributes.Add("rel", "stylesheet");
		htmlLink.Attributes.Add("type", "text/css");
		this.Page.Header.Controls.Add(htmlLink);
		if (!base.IsPostBack)
		{
			this.BtnDel.Attributes["onclick"] = "return confirm('是否要删除!')";
		}
		this.GVeFileLend.DataBind();
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			string text = ((DataRowView)e.Row.DataItem)["RecordID"].ToString();
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["guid"] = text;
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				text,
				"','",
				dataRowView["SecretLevel"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"','",
				dataRowView["LendState"].ToString(),
				"');"
			});
			DataTable list = this.efla.GetList(string.Concat(new string[]
			{
				"FileRecordID = '",
				dataRowView["FileRecordID"].ToString(),
				"' and BorrowMan='",
				this.Session["yhdm"].ToString(),
				"' and LendState>1"
			}));
			e.Row.Cells[4].Text = list.Rows.Count.ToString();
			string text2;
			if ((text2 = e.Row.Cells[5].Text) != null)
			{
				if (!(text2 == "1"))
				{
					if (!(text2 == "2"))
					{
						if (text2 == "3")
						{
							e.Row.Cells[5].Text = "绝密";
						}
					}
					else
					{
						e.Row.Cells[5].Text = "机密";
					}
				}
				else
				{
					e.Row.Cells[5].Text = "秘密";
				}
			}
			HyperLink arg_25E_0 = (HyperLink)e.Row.Cells[10].Controls[1];
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
									e.Row.Cells[7].Text = "已归还";
								}
							}
							else
							{
								e.Row.Cells[7].Text = "归还申请";
							}
						}
						else
						{
							e.Row.Cells[7].Text = "已借出";
						}
					}
					else
					{
						e.Row.Cells[7].Text = "未借出";
					}
				}
				else
				{
					e.Row.Cells[7].Text = "未申请";
				}
			}
			switch (int.Parse(dataRowView["ReturnType"].ToString()))
			{
			case 0:
				e.Row.Cells[8].Text = "";
				return;
			case 1:
				e.Row.Cells[8].Text = "主动";
				return;
			case 2:
				e.Row.Cells[8].Text = "被动";
				break;
			default:
				return;
			}
		}
	}
	protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
	{
		this.MultiView1.ActiveViewIndex = Convert.ToInt32(e.Item.Value);
		if (e.Item.Value != "0")
		{
			SqlDataSource expr_38 = this.sqlProjecteFile;
			expr_38.SelectCommand = expr_38.SelectCommand + " where RecordID in (select FileRecordID from OA_eFile_Authorization where ReaderCodes like '%" + this.Session["yhdm"].ToString() + "%' )";
		}
	}
	protected void GVeFile_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			((DataRowView)e.Row.DataItem)["RecordID"].ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			string text;
			if ((text = e.Row.Cells[2].Text) != null)
			{
				if (text == "1")
				{
					e.Row.Cells[2].Text = "秘密";
					return;
				}
				if (text == "2")
				{
					e.Row.Cells[2].Text = "机密";
					return;
				}
				if (!(text == "3"))
				{
					return;
				}
				e.Row.Cells[2].Text = "绝密";
			}
		}
	}
	protected void BtnPlanReturn_Click(object sender, EventArgs e)
	{
		eFileLend eFileLend = new eFileLend();
		eFileLend.ReturnDate = DateTime.Now;
		eFileLend.LendState = "4";
		eFileLend.ReturnType = 1;
		eFileLend.RecordID = new Guid(this.HdnRecoreId.Value);
		if (this.efla.ReturnUpdate(eFileLend) == 1)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('归还成功!');", true);
			this.GVeFileLend.DataBind();
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('归还失败!');", true);
	}
	protected void BtnApply_Click(object sender, EventArgs e)
	{
		eFileLend eFileLend = new eFileLend();
		eFileLend.LendState = "1";
		eFileLend.RecordID = new Guid(this.HdnRecoreId.Value);
		if (this.efla.LendStateUpdate(eFileLend) == 1)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('已提交借阅申请!');", true);
			this.GVeFileLend.DataBind();
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('提交借阅申请失败!');", true);
	}
	protected void BtnDel_Click(object sender, EventArgs e)
	{
		if (this.efla.Delete(new Guid(this.HdnRecoreId.Value)) == 1)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('删除成功!');", true);
			this.GVeFileLend.DataBind();
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('删除失败!');", true);
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		string text = FlowAuditAction.BeginFlow("010", "001", new Guid(this.HdnRecoreId.Value), "", base.UserCode);
		if (text == "工作流程已成功启动")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			this.GVeFileLend.DataBind();
			return;
		}
		if (text == "请先设置当前模块的审核流程")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){window.location.href=window.location.href};", true);
		this.GVeFileLend.DataBind();
	}
	protected void BtnPrject_Click(object sender, EventArgs e)
	{
		this.GVeFileLend.DataBind();
	}
	protected void BtnManager_Click(object sender, EventArgs e)
	{
		this.GVeFileLend.DataBind();
	}
}


