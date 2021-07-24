using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EntStandardQuery : NBasePage, System.Web.SessionState.IRequiresSessionState
	{
		private string _ClassId = "";
		private string _Type = "";
		private string _Levels = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["FilesClassID"] != null || base.Request["type"] != null)
				{
					this.ViewState["TYPE"] = base.Request.QueryString["type"].ToString();
					this._Type = this.ViewState["TYPE"].ToString();
					this.ViewState["LEVELS"] = base.Request.QueryString["Levels"].ToString();
					this._Levels = this.ViewState["LEVELS"].ToString();
					this.HdnClassId.Value = base.Request.QueryString["FilesClassID"].ToString().Trim();
					this.ViewState["ClassId"] = this.HdnClassId.Value;
					this._ClassId = this.ViewState["ClassId"].ToString();
					this.StandardBind();
					if (this._Type.Trim() == "1")
					{
						if (this._Levels.Trim() == "1")
						{
							this.DGrdStandard.Columns[6].Visible = false;
							this.BtnSave.Visible = false;
							this.BtnClose.Visible = false;
							this.BtnAdd.Visible = false;
							this.BtnUpd.Visible = false;
							this.BtnDel.Visible = false;
							this.lblTitle.Text = "企业技术标准查询";
						}
						else
						{
							this.DGrdStandard.Columns[6].Visible = false;
							this.BtnSave.Visible = false;
							this.BtnClose.Visible = false;
							this.BtnAdd.Enabled = true;
							this.BtnUpd.Enabled = false;
							this.BtnDel.Enabled = false;
							this.lblTitle.Text = "企业技术标准";
						}
					}
					else
					{
						if (this._Type.Trim() == "2")
						{
							if (this._Levels == "1")
							{
								this.DGrdStandard.Columns[6].Visible = false;
								this.BtnSave.Visible = false;
								this.BtnClose.Visible = false;
								this.BtnAdd.Visible = false;
								this.BtnUpd.Visible = false;
								this.BtnDel.Visible = false;
								this.lblTitle.Text = "企业技术标准查询";
							}
							else
							{
								this.ViewState["PRJCODE"] = base.Request.QueryString["PrjCode"].ToString().Trim();
								this.Table1.Rows[0].Visible = false;
								this.BtnAdd.Visible = false;
								this.BtnUpd.Visible = false;
								this.BtnDel.Visible = false;
								this.BtnView.Visible = false;
								this.DGrdStandard.Columns[5].Visible = false;
								this.lblTitle.Text = "企业技术标准";
							}
						}
					}
				}
				this.BtnAdd.Attributes["onclick"] = "openEdit('Add')";
				this.BtnUpd.Attributes["onclick"] = "openEdit('Upd')";
				this.BtnView.Attributes["onclick"] = "openEdit('View')";
				this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
				return;
			}
			this._ClassId = this.ViewState["ClassId"].ToString();
			this._Type = this.ViewState["TYPE"].ToString();
			this._Levels = this.ViewState["LEVELS"].ToString();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdStandard.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DGrdStandard_ItemDataBound);
		}
		private void StandardBind()
		{
			string classId = this._ClassId;
			this.DGrdStandard.DataSource = EntStandardAction.GetStandardList(classId);
			this.DGrdStandard.DataBind();
		}
		protected void DGrdStandard_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGrdStandard.CurrentPageIndex = e.NewPageIndex;
			this.StandardBind();
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			this.StandardBind();
		}
		protected void BtnUpd_Click(object sender, EventArgs e)
		{
			this.StandardBind();
		}
		private void DGrdStandard_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["id"] = e.Item.ItemIndex.ToString();
				e.Item.Attributes["ondblclick"] = "openEdit('View')";
				e.Item.ToolTip = "请双击查看详细信息";
				if (base.Request.QueryString["type"] != null && base.Request.QueryString["type"].ToString() != "" && base.Request.QueryString["type"].ToString() == "2")
				{
					e.Item.Attributes["ondblclick"] = "";
					e.Item.ToolTip = "";
				}
				System.Web.UI.WebControls.HyperLink hyperLink = (System.Web.UI.WebControls.HyperLink)e.Item.Cells[3].Controls[0];
				hyperLink.NavigateUrl = "#";
				hyperLink.Attributes["onclick"] = string.Concat(new string[]
				{
					"rowQuery('/EPC/17/Entpm/ScienceInnovate/EntStandardEdit.aspx?Type=View&ClassId=",
					base.Request.QueryString["FilesClassID"].ToString().Trim(),
					"&Id=",
					this.DGrdStandard.DataKeys[e.Item.ItemIndex].ToString(),
					"',' 查看企业技术标准 ')"
				});
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);clickRow('",
					this.DGrdStandard.DataKeys[e.Item.ItemIndex].ToString(),
					"','",
					this._Type,
					"','",
					this._Levels,
					"');"
				});
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
				e.Item.Cells[3].ToolTip = ((System.Web.UI.WebControls.HyperLink)e.Item.Cells[3].Controls[0]).Text.ToString();
				if (((System.Web.UI.WebControls.HyperLink)e.Item.Cells[3].Controls[0]).Text.ToString().Length > 10)
				{
					((System.Web.UI.WebControls.HyperLink)e.Item.Cells[3].Controls[0]).Text = ((System.Web.UI.WebControls.HyperLink)e.Item.Cells[3].Controls[0]).Text.ToString().Substring(0, 10) + "……";
				}
				e.Item.Cells[5].ToolTip = e.Item.Cells[5].Text.ToString();
				if (e.Item.Cells[5].Text.ToString().Length > 20)
				{
					e.Item.Cells[5].Text = e.Item.Cells[5].Text.ToString().Substring(0, 10) + "…";
				}
			}
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			int num = EntStandardAction.StandDel(this.HdnId.Value);
			if (num == 1)
			{
				this.Js.Text = "alert('删除成功！');";
				this.StandardBind();
				return;
			}
			this.Js.Text = "alert('删除失败！'); ";
		}
		private TechnologyStandardCollectin GetSelect()
		{
			TechnologyStandardCollectin technologyStandardCollectin = new TechnologyStandardCollectin();
			ArrayList arrayList = new ArrayList();
			int maxId = TechnologyStandardAction.GetMaxId();
			for (int i = 0; i < this.DGrdStandard.Items.Count; i++)
			{
				System.Web.UI.WebControls.DataGridItem dataGridItem = this.DGrdStandard.Items[i];
				System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)dataGridItem.Cells[6].FindControl("CHK");
				if (checkBox.Checked)
				{
					TechnologyStandardInfo technologyStandardInfo = new TechnologyStandardInfo();
					technologyStandardInfo.TechnologyCriterion = int.Parse(this.DGrdStandard.DataKeys[i].ToString());
					technologyStandardInfo.MainId = maxId + i;
					technologyStandardInfo.PrjCode = this.ViewState["PRJCODE"].ToString();
					technologyStandardInfo.TechnologyCriterionID = dataGridItem.Cells[1].Text.ToString();
					technologyStandardInfo.TechnologyClassify = dataGridItem.Cells[4].Text.ToString();
					technologyStandardInfo.TechnologyName = dataGridItem.Cells[3].Text.ToString();
					technologyStandardInfo.TechnologyPromulgateTime = dataGridItem.Cells[2].Text.ToString();
					technologyStandardInfo.Remark = dataGridItem.Cells[5].Text.ToString();
					technologyStandardCollectin.Add(technologyStandardInfo);
					arrayList.Add(technologyStandardInfo.MainId);
				}
			}
			this.ViewState["list"] = arrayList;
			return technologyStandardCollectin;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			TechnologyStandardCollectin select = this.GetSelect();
			if (select.Count < 1)
			{
				return;
			}
			TechnologyStandardAction.TechStandardSelect(select);
			ConstructOrganizeBBl constructOrganizeBBl = new ConstructOrganizeBBl();
			if (this.ViewState["list"] != null)
			{
				ArrayList arrayList = this.ViewState["list"] as ArrayList;
				for (int i = 0; i < arrayList.Count; i++)
				{
					constructOrganizeBBl.UpdGuidang("Prj_TechnologyCriterion", 2, 1, " where MainId=" + arrayList[i].ToString());
				}
			}
			this.Js.Text = "window.close();";
		}
	}


