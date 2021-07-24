using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_ProjectCost_PhotosCheckInList : PageBase, IRequiresSessionState
{
	public IntendanceMasterAction inMaster = new IntendanceMasterAction();

	public Guid ProjectCode
	{
		get
		{
			return (Guid)this.ViewState["PROJECTCODE"];
		}
		set
		{
			this.ViewState["PROJECTCODE"] = value;
		}
	}
	public string ProjectName
	{
		get
		{
			return (string)this.ViewState["ProjectName"];
		}
		set
		{
			this.ViewState["ProjectName"] = value;
		}
	}
	public string strUser
	{
		get
		{
			return (string)this.ViewState["strUser"];
		}
		set
		{
			this.ViewState["strUser"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["pc"] == null || base.Request["pn"] == null || base.Request["PrjGuid"] == null || base.Request["PrjCode"] == null || base.Request["pt"] == null)
			{
				this.js.Text = "alert('参数有误！');";
				return;
			}
			this.btnDel.Attributes.Add("onclick", "return confirm('确定要删除当前选中数据吗？');");
			this.ProjectCode = new Guid(base.Request.QueryString["PrjGuid"]);
			this.ProjectName = base.Request.QueryString["pn"];
			this.lblTitle.Text = this.ProjectName + "拍照监督";
			this.Drop_QuestionType_Bind();
			if (base.Request.QueryString["pt"] == "1")
			{
				this.strUser = " and a.OpYhdm='" + base.UserCode + "'";
				this.btnAdd.Visible = true;
				this.btnUpdate.Visible = true;
				this.btnView.Visible = true;
				this.btnValidate.Visible = false;
				this.btnSettle.Visible = false;
				this.btnAdd.Attributes["onclick"] = "if (!ClickBtn('add')) return false;";
				this.btnUpdate.Attributes["onclick"] = "if (!ClickBtn('upd')) return false;";
				this.btnView.Attributes["onclick"] = "if (!ClickBtn('view')) return false;";
			}
			else
			{
				if (base.Request.QueryString["pt"] == "2")
				{
					this.strUser = "";
					this.btnAdd.Visible = false;
					this.btnDel.Visible = false;
					this.btnUpdate.Visible = false;
					this.btnView.Visible = true;
					this.btnValidate.Visible = false;
					this.btnSettle.Visible = false;
					this.btnView.Attributes["onclick"] = "if (!ClickBtn('view')) return false;";
				}
				else
				{
					if (base.Request.QueryString["pt"] == "3")
					{
						this.strUser = " and a.SettleYhdm='" + base.UserCode + "'";
						this.btnSettle.Visible = true;
						this.btnAdd.Visible = false;
						this.btnDel.Visible = false;
						this.btnUpdate.Visible = false;
						this.btnView.Visible = false;
						this.btnValidate.Visible = false;
						this.btnSettle.Attributes["onclick"] = "if (!ClickBtn('settle')) return false;";
					}
				}
			}
			this.DataBindToPage(this.strUser);
			this.hdnflag.Value = base.Request.QueryString["pt"];
		}
	}
	private void Drop_QuestionType_Bind()
	{
		this.ddlType.DataSource = this.inMaster.GetTypeList();
		this.ddlType.DataBind();
	}
	protected void DataBindToPage(string strWhere)
	{
		DataTable list = this.inMaster.GetList(this.ProjectCode, strWhere);
		this.grdModules.DataSource = list;
		this.grdModules.DataBind();
	}
	protected void BtnSearch_Click(object sender, EventArgs e)
	{
		string text = "";
		if (this.chcDate.Checked)
		{
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				this.strUser,
				" and a.BookInDate>= '",
				this.txtBeginDate.Text,
				"' and a.BookInDate<= '",
				this.txtEndDate.Text,
				"'"
			});
		}
		if (this.chcTitle.Checked)
		{
			string text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				this.strUser,
				" and a.QuestionTitle like '%",
				this.txtTitle.Value,
				"%'"
			});
		}
		if (this.chcState.Checked)
		{
			text = text + this.strUser + " and a.SettleState=" + this.ddlState.SelectedValue;
		}
		if (this.chcType.Checked)
		{
			text = text + " and a.QuestionTypeId=" + this.ddlType.SelectedValue;
		}
		this.DataBindToPage(text);
	}
	protected void grdModules_ItemDataBound(object sender, DataGridItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.EditItem)
		{
			DataRowView dataRowView = (DataRowView)e.Item.DataItem;
			if (Convert.ToInt32(dataRowView["SettleState"]) == 0)
			{
				e.Item.Cells[4].Text = "未解决";
			}
			else
			{
				if (Convert.ToInt32(dataRowView["SettleState"]) == 1)
				{
					e.Item.Cells[4].Text = "解决中";
				}
				else
				{
					if (Convert.ToInt32(dataRowView["SettleState"]) == 2)
					{
						e.Item.Cells[4].Text = "已解决";
					}
					else
					{
						e.Item.Cells[4].Text = "已解决并验证";
					}
				}
			}
			e.Item.Attributes["onclick"] = string.Concat(new object[]
			{
				"OnRecord(this);clickRow(this,'",
				dataRowView["IntendanceGuid"].ToString(),
				"','",
				dataRowView["SettleState"].ToString(),
				"',",
				Convert.ToInt32(dataRowView["QuestionTag"]),
				");"
			});
			e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.DataBindToPage("");
	}
	protected void btnUpdate_Click(object sender, EventArgs e)
	{
		this.DataBindToPage("");
	}
	protected void btnSettle_Click(object sender, EventArgs e)
	{
		this.DataBindToPage("");
	}
	protected void btnValidate_Click(object sender, EventArgs e)
	{
		this.DataBindToPage("");
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		IntendanceMasterAction intendanceMasterAction = new IntendanceMasterAction();
		IntendancePhotoListAction intendancePhotoListAction = new IntendancePhotoListAction();
		DataTable photoInfoList = intendancePhotoListAction.GetPhotoInfoList(this.hdnIntendanceGuid.Value);
		string text;
		if (photoInfoList.Rows.Count > 0)
		{
			for (int i = 0; i < photoInfoList.Rows.Count; i++)
			{
				MakeThumbnail makeThumbnail = new MakeThumbnail();
				if (makeThumbnail.DelThumbnai(photoInfoList.Rows[i]["NoteId"].ToString()))
				{
				}
			}
			if (intendancePhotoListAction.ClearPhotosList(this.hdnIntendanceGuid.Value) > 0)
			{
				text = "1";
			}
			else
			{
				text = "删除失败";
			}
		}
		else
		{
			text = "1";
		}
		if (!(text == "1"))
		{
			this.js.Text = "alert('" + text + "!');";
			return;
		}
		if (intendanceMasterAction.Del(this.hdnIntendanceGuid.Value) > 0)
		{
			this.js.Text = "alert('删除成功!');";
			this.DataBindToPage("");
			return;
		}
		this.js.Text = "alert('删除失败!');";
	}
}


