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
public partial class oa_eFile_ProjectFileManage : BasePage, IRequiresSessionState
{

	private int ClassID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["CLASSID"]);
		}
		set
		{
			this.ViewState["CLASSID"] = value;
		}
	}
	private string RecordType
	{
		get
		{
			return this.ViewState["RECORDTYPE"].ToString();
		}
		set
		{
			this.ViewState["RECORDTYPE"] = value;
		}
	}
	private string sl
	{
		get
		{
			return this.ViewState["sl"].ToString();
		}
		set
		{
			this.ViewState["sl"] = value;
		}
	}
	private eFileInfoAction eff
	{
		get
		{
			return new eFileInfoAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.sl = "";
			if (base.Request["cid"] != null || base.Request["rt"] != null || base.Request["prj"] != null)
			{
				if (base.Request["sl"] != null)
				{
					this.sl = base.Request["sl"].ToString();
				}
				this.ClassID = Convert.ToInt32(base.Request["cid"]);
				this.RecordType = base.Request["rt"].ToString();
				if (this.RecordType == "2")
				{
					SqlDataSource expr_DD = this.sqlBulletin;
					expr_DD.SelectCommand = expr_DD.SelectCommand + " and CorpCode =  '" + this.Session["CorpCode"].ToString() + "'";
					this.HdnPrjGuid.Value = "00000000-0000-0000-0000-000000000000";
					this.lblTitle.Text = "管理档案列表";
					if (this.sl == "1")
					{
						SqlDataSource expr_144 = this.sqlBulletin;
						expr_144.SelectCommand += " and SecretLevel = '3' ";
					}
					else
					{
						SqlDataSource expr_161 = this.sqlBulletin;
						expr_161.SelectCommand += " and SecretLevel <> '3' ";
					}
					this.GridView1.DataBind();
				}
				else
				{
					this.HdnPrjGuid.Value = base.Request["prj"].ToString();
					this.lblTitle.Text = "项目档案列表";
				}
				this.btnAdd.Attributes["onclick"] = string.Concat(new object[]
				{
					"openEdit('Add','",
					this.ClassID,
					"','",
					this.RecordType,
					"','",
					this.sl,
					"');"
				});
				this.btnEdit.Attributes["onclick"] = string.Concat(new object[]
				{
					"openEdit('Edit','",
					this.ClassID,
					"','",
					this.RecordType,
					"','",
					this.sl,
					"');"
				});
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前归档记录数据吗？');";
				return;
			}
			this.btnAdd.Enabled = false;
			this.BtnSearch.Enabled = false;
		}
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			string str = ((DataRowView)e.Row.DataItem)["RecordID"].ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + str + "');";
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[2].Text = userManageDb.GetUserName(e.Row.Cells[2].Text);
			string text;
			if ((text = e.Row.Cells[6].Text) != null)
			{
				if (text == "1")
				{
					e.Row.Cells[6].Text = "秘密";
					return;
				}
				if (text == "2")
				{
					e.Row.Cells[6].Text = "机密";
					return;
				}
				if (!(text == "3"))
				{
					return;
				}
				e.Row.Cells[6].Text = "绝密";
			}
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		if (this.RecordType == "2" && this.sl == "1")
		{
			SqlDataSource expr_2A = this.sqlBulletin;
			expr_2A.SelectCommand += " and SecretLevel = '3' ";
		}
		else
		{
			if (this.RecordType == "2" && this.sl == "")
			{
				SqlDataSource expr_6B = this.sqlBulletin;
				expr_6B.SelectCommand += " and SecretLevel <> '3' ";
			}
		}
		this.GridView1.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		eFileInfo model = this.eff.GetModel(Convert.ToInt32(this.HdnRecoreId.Value));
		if (this.eff.Delete(Convert.ToInt32(this.HdnRecoreId.Value)) == 1)
		{
			com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
			fileUpload.DeleteFile(model.FilePath.ToString());
			this.JS.Text = "alert('删除成功!');";
			if (this.RecordType == "2" && this.sl == "1")
			{
				SqlDataSource expr_8F = this.sqlBulletin;
				expr_8F.SelectCommand += " and SecretLevel = '3' ";
			}
			else
			{
				if (this.RecordType == "2" && this.sl == "")
				{
					SqlDataSource expr_D0 = this.sqlBulletin;
					expr_D0.SelectCommand += " and SecretLevel <> '3' ";
				}
			}
			this.GridView1.DataBind();
			return;
		}
		this.JS.Text = "alert('删除失败!');";
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		if (this.RecordType == "2" && this.sl == "1")
		{
			SqlDataSource expr_2A = this.sqlBulletin;
			expr_2A.SelectCommand += " and SecretLevel = '3' ";
		}
		else
		{
			if (this.RecordType == "2" && this.sl == "")
			{
				SqlDataSource expr_6B = this.sqlBulletin;
				expr_6B.SelectCommand += " and SecretLevel <> '3' ";
			}
		}
		this.GridView1.DataBind();
	}
	protected void BtnSearch_Click(object sender, EventArgs e)
	{
		string a;
		if ((a = this.DropDownList1.SelectedValue.ToString()) != null)
		{
			if (a == "1")
			{
				SqlDataSource expr_45 = this.sqlBulletin;
				expr_45.SelectCommand = expr_45.SelectCommand + " and FileTitle like '%" + this.TxtSearchCount.Text.Trim() + "%'";
				this.GridView1.DataBind();
				return;
			}
			if (a == "2")
			{
				SqlDataSource expr_81 = this.sqlBulletin;
				expr_81.SelectCommand = expr_81.SelectCommand + " and FileCode like '%" + this.TxtSearchCount.Text.Trim() + "%'";
				this.GridView1.DataBind();
				return;
			}
			if (!(a == "3"))
			{
				return;
			}
			SqlDataSource expr_BD = this.sqlBulletin;
			expr_BD.SelectCommand = expr_BD.SelectCommand + " and SaveLimit like '%" + this.TxtSearchCount.Text.Trim() + "%'";
			this.GridView1.DataBind();
		}
	}
}


