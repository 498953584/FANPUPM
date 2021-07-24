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
public partial class oa_WorkManage_BudgetSetSubTabEdit : BasePage, IRequiresSessionState
{
	public OAOfficeResJoinDrawItemSetAction amAction
	{
		get
		{
			return new OAOfficeResJoinDrawItemSetAction();
		}
	}
	public int PostLevel
	{
		get
		{
			object obj = this.ViewState["RECORDID"];
			if (obj != null)
			{
				return (int)this.ViewState["RECORDID"];
			}
			return 0;
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	public int MatterialID
	{
		get
		{
			object obj = this.ViewState["INSTORAGEID"];
			if (obj != null)
			{
				return (int)this.ViewState["INSTORAGEID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["INSTORAGEID"] = value;
		}
	}
	public string OperateType
	{
		get
		{
			object obj = this.ViewState["OPERATETYPE"];
			if (obj != null)
			{
				return (string)this.ViewState["OPERATETYPE"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["OPERATETYPE"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["pl"] == null || base.Request["dd"] == null || base.Request["op"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;window.close();</script>");
			return;
		}
		if (!this.Page.IsPostBack)
		{
			this.OperateType = base.Request["op"].ToString().Trim();
			if (base.Request["dd"].ToString() != "")
			{
				this.MatterialID = int.Parse(base.Request["dd"].ToString());
				this.HdnMatterialID.Value = this.MatterialID.ToString();
			}
			if (base.Request["pl"].ToString() != "")
			{
				this.PostLevel = int.Parse(base.Request["pl"].ToString());
			}
			if (this.OperateType == "upd")
			{
				this.imgsel.Disabled = true;
				this.EditDisplay();
			}
		}
		this.txtNumber.Attributes["onblur"] = "javascript:checkDecimal(this);";
	}
	private void EditDisplay()
	{
		DataTable list = this.amAction.GetList(string.Concat(new object[]
		{
			" a.PostLevel='",
			this.PostLevel,
			"' and a.DrawItemID=",
			this.MatterialID
		}));
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.txtResName.Text = dataRow["ResName"].ToString();
			this.txtUnit.Text = dataRow["Unit"].ToString();
			this.HdnMatterialID.Value = dataRow["DrawItemID"].ToString();
			this.txtNumber.Text = dataRow["Number"].ToString();
			this.txtRemark.Text = dataRow["Remark"].ToString();
		}
	}
	private OAOfficeResJoinDrawItemSet GetData()
	{
		return new OAOfficeResJoinDrawItemSet
		{
			DrawItemID = int.Parse(this.HdnMatterialID.Value),
			Number = (this.txtNumber.Text.Trim() == "") ? 0 : int.Parse(this.txtNumber.Text.Trim()),
			PostLevel = this.PostLevel,
			Remark = this.txtRemark.Text.Trim()
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAOfficeResJoinDrawItemSet data = this.GetData();
		if (this.OperateType == "add")
		{
			if (this.amAction.Exists(this.PostLevel, int.Parse(this.HdnMatterialID.Value)))
			{
				this.Page.RegisterStartupScript("", "<script>alert('相关数据已存在!');</script>");
			}
			else
			{
				int num = this.amAction.Add(data);
				if (num > 0)
				{
					this.Page.RegisterStartupScript("", "<script>alert('添加成功!');returnValue=true;window.close();</script>");
				}
				else
				{
					this.Page.RegisterStartupScript("", "<script>alert('没有相关数据可添加!');</script>");
				}
			}
		}
		if (this.OperateType == "upd")
		{
			int num = this.amAction.Update(data);
			if (num > 0)
			{
				this.Page.RegisterStartupScript("", "<script>alert('修改成功!');returnValue=true;window.close();</script>");
				return;
			}
			this.Page.RegisterStartupScript("", "<script>alert('没有相关数据可更新!');</script>");
		}
	}
}


