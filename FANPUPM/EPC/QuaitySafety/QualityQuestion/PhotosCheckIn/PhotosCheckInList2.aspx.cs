using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_ProjectCost_PhotosCheckInList : SubwayBasePage, IRequiresSessionState
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
	public bool flag
	{
		get
		{
			return (bool)this.ViewState["flag"];
		}
		set
		{
			this.ViewState["flag"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["pt"] == null)
			{
				this.js.Text = "alert('参数有误！');";
				return;
			}
			this.Drop_QuestionType_Bind();
			this.flag = true;
			if (base.Request.QueryString["pt"] == "1")
			{
				this.ProjectCode = new Guid(base.Request.QueryString["PrjGuid"]);
				DataTable prjChildNum = this.inMaster.GetPrjChildNum(this.ProjectCode);
				if (prjChildNum.Rows.Count > 0)
				{
					int num = Convert.ToInt32(prjChildNum.Rows[0][0].ToString());
					if (num > 0)
					{
						this.btnAdd.Enabled = false;
					}
					else
					{
						this.btnAdd.Enabled = true;
					}
				}
				this.ProjectName = base.Request.QueryString["pn"];
				this.strUser = "  and a.OpYhdm='" + base.UserCode + "'";
				this.btnAdd.Visible = true;
				this.btnUpdate.Visible = true;
				this.btnView.Visible = true;
				this.btnValidate.Visible = true;
				this.btnSettle.Visible = false;
				this.Panel1.Visible = false;
				this.chcPrj.Visible = false;
				this.btnAdd.Attributes["onclick"] = "if (!ClickBtn('add')) return false;";
				this.btnUpdate.Attributes["onclick"] = "if (!ClickBtn('upd')) return false;";
				this.btnValidate.Attributes["onclick"] = "if (!ClickBtn('v')) return false;";
				this.btnView.Attributes["onclick"] = "if (!ClickBtn('view')) return false;";
				this.btnDel.Attributes.Add("onclick", "return confirm('确定要删除当前选中数据吗？');");
			}
			if (base.Request.QueryString["pt"] == "2")
			{
				this.ProjectCode = new Guid(base.Request.QueryString["PrjGuid"]);
				DataTable prjChildNum2 = this.inMaster.GetPrjChildNum(this.ProjectCode);
				if (prjChildNum2.Rows.Count > 0)
				{
					int num2 = Convert.ToInt32(prjChildNum2.Rows[0][0].ToString());
					if (num2 > 0)
					{
						this.btnAdd.Enabled = false;
					}
					else
					{
						this.btnAdd.Enabled = true;
					}
				}
				this.ProjectName = base.Request.QueryString["pn"];
				this.strUser = " and a.OpYhdm='" + base.UserCode + "'";
				this.btnAdd.Visible = true;
				this.btnUpdate.Visible = true;
				this.btnView.Visible = true;
				this.btnValidate.Visible = true;
				this.btnSettle.Visible = false;
				this.Panel1.Visible = false;
				this.chcPrj.Visible = false;
				this.btnAdd.Attributes["onclick"] = "if (!ClickBtn('add')) return false;";
				this.btnUpdate.Attributes["onclick"] = "if (!ClickBtn('upd')) return false;";
				this.btnValidate.Attributes["onclick"] = "if (!ClickBtn('v')) return false;";
				this.btnView.Attributes["onclick"] = "if (!ClickBtn('view')) return false;";
			}
			else
			{
				if (base.Request.QueryString["pt"] == "3")
				{
					this.strUser = string.Concat(new string[]
					{
						"  and (a.SettleYhdm like '%",
						base.UserCode,
						"%' or a.SettleToPerson like '%",
						base.UserCode,
						"%')"
					});
					this.strUser = "  and (SettleState in (0,1))";
					this.btnAdd.Visible = false;
					this.btnDel.Visible = false;
					this.btnUpdate.Visible = false;
					this.btnSettle.Visible = true;
					this.btnValidate.Visible = false;
					this.btnSettle.Attributes["onclick"] = "if (!ClickBtn('settle')) return false;";
					if (this.hdnPrjID.Value != "")
					{
						this.ProjectCode = new Guid(this.hdnPrjID.Value);
					}
					else
					{
						this.ProjectCode = new Guid("00000000-0000-0000-0000-000000000000");
					}
					this.ProjectName = this.txtPrjName.Text;
				}
				else
				{
					if (base.Request.QueryString["pt"] == "4")
					{
						this.strUser = string.Empty;
						this.ProjectCode = new Guid(base.Request.QueryString["PrjGuid"]);
						this.btnAdd.Visible = false;
						this.btnDel.Visible = false;
						this.btnUpdate.Visible = false;
						this.btnView.Visible = true;
						this.Panel1.Visible = false;
						this.chcPrj.Visible = false;
						this.btnValidate.Visible = false;
						this.btnSettle.Visible = false;
						this.btnView.Attributes["onclick"] = "if (!ClickBtn('view')) return false;";
					}
				}
			}
			this.hdnflag.Value = base.Request.QueryString["pt"];
			this.btnView.Visible = true;
			this.btnView.Attributes["onclick"] = "if (!ClickBtn('view')) return false;";
			this.DataBindToPage(this.strUser);
			this.lblTitle.Text = this.ProjectName + "问题监控";
		}
	}
	private void Drop_QuestionType_Bind()
	{
		this.ddlType.DataSource = this.inMaster.GetTypeList();
		this.ddlType.DataBind();
	}
	protected void DataBindToPage(string strWhere)
	{
		IntendanceMasterAction intendanceMasterAction = new IntendanceMasterAction();
		DataTable dataSource = new DataTable();
		strWhere = this.strUser;
		if (this.hdnflag.Value == "2" || this.hdnflag.Value == "3")
		{
			if (this.hdnflag.Value == "2")
			{
				int num = 2;
				if (this.ProjectCode == new Guid("00000000-0000-0000-0000-000000000000"))
				{
					string sqlString = string.Concat(new object[]
					{
						"select a.*,b.v_xm,c.PrjName,c.PrjGuid ,d.CodeName from OPM_EPCM_IntendanceMaster as a inner join PT_yhmc as b on a.OpYhdm=b.v_yhdm inner join pt_prjinfo as c on a.PrjGuid=c.PrjGuid INNER JOIN dbo.XPM_Basic_CodeList as d ON a.QuestionTypeId = d.CodeID  where   d.TypeID = (select TypeId from XPM_Basic_CodeType WHERE SignCode='ProblemSupervise')",
						strWhere,
						" and a.settleState = ",
						num,
						"  order by a.BookInDate desc"
					});
					dataSource = publicDbOpClass.DataTableQuary(sqlString);
				}
				else
				{
					dataSource = intendanceMasterAction.GetList(this.ProjectCode, strWhere);
				}
			}
			else
			{
				dataSource = intendanceMasterAction.GetList(this.ProjectCode, strWhere);
			}
		}
		else
		{
			dataSource = intendanceMasterAction.GetList(this.ProjectCode, strWhere);
		}
		this.grdModules.DataSource = dataSource;
		this.grdModules.DataBind();
	}
	protected void BtnSearch_Click(object sender, EventArgs e)
	{
		this.flag = false;
		string text = "";
		if (this.txtBeginDate.Text.Trim() != "")
		{
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				this.strUser,
				" and a.BookInDate>= '",
				this.txtBeginDate.Text,
				"' "
			});
		}
		if (this.txtEndDate.Text.Trim() != "")
		{
			string text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				this.strUser,
				" and a.BookInDate<= '",
				this.txtEndDate.Text,
				"'"
			});
		}
		if (this.txtTitle.Value.Trim() != "")
		{
			string text4 = text;
			text = string.Concat(new string[]
			{
				text4,
				this.strUser,
				" and a.QuestionTitle like '%",
				this.txtTitle.Value,
				"%'"
			});
		}
		if (this.ddlState.SelectedValue.Trim() != "" && this.ddlState.SelectedIndex != 0)
		{
			text = text + this.strUser + " and a.SettleState=" + this.ddlState.SelectedValue;
		}
		if (this.ddlType.SelectedValue.Trim() != "")
		{
			text = text + this.strUser + " and a.QuestionTypeId=" + this.ddlType.SelectedValue;
		}
		if (this.hdnflag.Value == "3")
		{
			if (this.hdnPrjID.Value != "")
			{
				this.ProjectCode = new Guid(this.hdnPrjID.Value);
				this.ProjectName = this.txtPrjName.Text.Trim();
			}
			else
			{
				this.ProjectCode = new Guid("00000000-0000-0000-0000-000000000000");
				this.ProjectName = "项目";
			}
		}
		this.DataBindToPage(text);
		this.lblTitle.Text = this.ProjectName + "拍照监督";
	}
	protected void btnSettle_Click(object sender, EventArgs e)
	{
		this.DataBindToPage("");
	}
	protected void btnValidate_Click(object sender, EventArgs e)
	{
		this.DataBindToPage("");
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.DataBindToPage("");
	}
	protected void btnUpdate_Click(object sender, EventArgs e)
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
	protected void grdModules_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Cells[6].Text = WebUtil.GetQuestionState(Convert.ToInt32(dataRowView["SettleState"]));
			e.Row.Attributes["id"] = dataRowView["IntendanceGuid"].ToString();
			e.Row.Attributes["onclick"] = string.Concat(new object[]
			{
				"OnRecord(this);clickRow(this,'",
				dataRowView["IntendanceGuid"].ToString(),
				"','",
				dataRowView["SettleState"].ToString(),
				"',",
				Convert.ToInt32(dataRowView["QuestionTag"]),
				",'",
				dataRowView["PrjName"].ToString(),
				"','",
				dataRowView["PrjGuid"],
				"');"
			});
			AttributeCollection attributes;
			AttributeCollection expr_131 = attributes = e.Row.Attributes;
			string arg_19B_1 = "onclick";
			string text = attributes["onclick"];
			expr_131[arg_19B_1] = string.Concat(new string[]
			{
				text,
				" showjwDiv(this,'/EPC/QuaitySafety/QualityQuestion/PrjInfoPop.aspx?intendanceguid=",
				dataRowView["IntendanceGuid"].ToString(),
				"&id=",
				dataRowView["IntendanceGuid"].ToString(),
				"','prjbmp') "
			});
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
		}
	}
	protected void grdModules_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		GridView gridView = sender as GridView;
		int num = 0;
		if (-2 == e.NewPageIndex)
		{
			TextBox textBox = null;
			GridViewRow bottomPagerRow = gridView.BottomPagerRow;
			if (bottomPagerRow != null)
			{
				textBox = (bottomPagerRow.FindControl("txtNewPageIndex") as TextBox);
			}
			if (textBox != null)
			{
				num = int.Parse(textBox.Text) - 1;
			}
		}
		else
		{
			num = e.NewPageIndex;
		}
		num = ((num < 0) ? 0 : num);
		num = ((num >= gridView.PageCount) ? (gridView.PageCount - 1) : num);
		gridView.PageIndex = num;
		this.DataBindToPage("");
	}
}


