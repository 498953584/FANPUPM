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
public partial class HR_Organization_FunctionManagerEdit : BasePage, IRequiresSessionState
{
	public HROrgPostLevelAction hrAction
	{
		get
		{
			return new HROrgPostLevelAction();
		}
	}
	public int RecordID
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
	public int DataType
	{
		get
		{
			object obj = this.ViewState["DATATYPE"];
			if (obj != null)
			{
				return (int)this.ViewState["DATATYPE"];
			}
			return 1;
		}
		set
		{
			this.ViewState["DATATYPE"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["rid"] == null || base.Request["t"] == null || base.Request["op"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;window.close();</script>");
			return;
		}
		if (!this.Page.IsPostBack)
		{
			if (base.Request["t"].ToString() == "1")
			{
				this.title.Text = "职称类职级信息";
			}
			else
			{
				this.title.Text = "岗位证书类职级信息";
			}
			base.Response.Cache.SetNoStore();
			this.OperateType = base.Request["op"].ToString().Trim();
			if (base.Request["rid"].ToString() != "")
			{
				this.RecordID = int.Parse(base.Request["rid"].ToString());
			}
			this.DataType = int.Parse(base.Request["t"].ToString());
			if (this.OperateType == "upd")
			{
				this.EditDisplay();
			}
		}
		this.txtPositionLevel.Attributes["onblur"] = "javascript:CheckNum(this);";
		this.txtPostLevel.Attributes["onblur"] = "javascript:CheckNum(this);";
	}
	private void EditDisplay()
	{
		DataTable list = this.hrAction.GetList("RecordID='" + this.RecordID + "'");
		if (list.Rows.Count > 0)
		{
			this.txtPositionLevel.Text = list.Rows[0]["PositionLevel"].ToString();
			this.txtPostAndRank.Text = list.Rows[0]["PostAndRank"].ToString();
			this.txtPostLevel.Text = list.Rows[0]["PostLevel"].ToString();
			this.txtRemark.Text = list.Rows[0]["Remark"].ToString();
		}
	}
	private HROrgPostLevel GetData()
	{
		return new HROrgPostLevel
		{
			PositionLevel = (this.txtPositionLevel.Text.Trim() == "") ? 1 : int.Parse(this.txtPositionLevel.Text.Trim()),
			PostAndRank = this.txtPostAndRank.Text.Trim(),
			PostLevel = (this.txtPostLevel.Text.Trim() == "") ? 1 : int.Parse(this.txtPostLevel.Text.Trim()),
			RecordID = this.RecordID,
			Remark = this.txtRemark.Text.Trim(),
			Type = this.DataType.ToString()
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		HROrgPostLevel data = this.GetData();
		if (this.OperateType == "add")
		{
			if (!this.hrAction.Exists(data.PostLevel, data.PositionLevel, data.Type))
			{
				int num = this.hrAction.Add(data);
				if (num > 0)
				{
					base.RegisterScript("successed('添加');");
				}
				else
				{
					base.RegisterScript("top.ui.alert('没有相关数据可添加!')");
				}
			}
			else
			{
				base.RegisterScript("top.ui.alert('相关数据已存在!')");
			}
		}
		if (this.OperateType == "upd")
		{
			int num = this.hrAction.Update(data);
			if (num > 0)
			{
				base.RegisterScript("successed('修改');");
				return;
			}
			base.RegisterScript("top.ui.alert('没有相关数据可更新!')");
		}
	}
}


