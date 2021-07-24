using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProductValueMonthReport : PageBase, IRequiresSessionState
	{
		protected ProductValueAction ProductValueAct
		{
			get
			{
				return new ProductValueAction();
			}
		}
		public Guid ProjectCode
		{
			get
			{
				object obj = this.ViewState["PROJECTCODE"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["PROJECTCODE"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null || base.Request["pc"].ToString() == "" || base.Request["pn"] == null)
			{
				this.RegisterStartupScript("错误提示", "<script>alert('参数错误！');</script>");
				return;
			}
			this.ProjectCode = new Guid(base.Request["pc"].ToString());
			this.HdnProjectCode.Value = this.ProjectCode.ToString();
			this.HdnProjectName.Value = base.Request["pn"].ToString();
			if (!this.Page.IsPostBack)
			{
				this.LblMsg.Text = this.HdnProjectName.Value;
				this.DataGrid_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
			this.Init_Click();
		}
		private void InitializeComponent()
		{
		}
		private void Init_Click()
		{
			this.DGrdMonthProductValue.ItemCommand += new DataGridCommandEventHandler(this.DGrdMonthProductValue_ItemCommand);
			this.DGrdMonthProductValue.ItemDataBound += new DataGridItemEventHandler(this.DGrdMonthProductValue_ItemDataBound);
		}
		private void DataGrid_Bind()
		{
			this.DGrdMonthProductValue.DataSource = this.ProductValueAct.GetProductValueMonthPlan(this.ProjectCode);
			this.DGrdMonthProductValue.DataBind();
		}
		private void DGrdMonthProductValue_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				DateTime dtStartDate = Convert.ToDateTime(dataRowView["StartDate"].ToString());
				e.Item.Cells[0].Text = dtStartDate.ToString("yyyy年MM月");
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.DGrdMonthProductValue.ClientID.ToString() + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				try
				{
					e.Item.Cells[3].Text = this.ProductValueAct.GetCurrMonthOverProductValue(this.ProjectCode, dtStartDate).ToString();
				}
				catch
				{
					e.Item.Cells[3].Text = 0m.ToString();
				}
				if (dtStartDate.Year == DateTime.Now.Year && dtStartDate.Month == DateTime.Now.Month)
				{
					LinkButton linkButton = (LinkButton)e.Item.Cells[1].FindControl("LBtnStatWeave");
					linkButton.Attributes["onclick"] = string.Concat(new object[]
					{
						"javascript:return MonthProductValueEdit('Add','",
						e.Item.Cells[0].Text,
						"',1,'",
						this.ProjectCode,
						"');"
					});
					LinkButton linkButton2 = (LinkButton)e.Item.Cells[1].FindControl("LBtnStatEdit");
					linkButton2.Attributes["onclick"] = string.Concat(new object[]
					{
						"javascript:return MonthProductValueEdit('Upd','",
						e.Item.Cells[0].Text,
						"',1,'",
						this.ProjectCode,
						"');"
					});
					LinkButton linkButton3 = (LinkButton)e.Item.Cells[1].FindControl("LBtnStatView");
					linkButton3.Attributes["onclick"] = string.Concat(new object[]
					{
						"javascript:return MonthProductValueEdit('Other','",
						e.Item.Cells[0].Text,
						"',1,'",
						this.ProjectCode,
						"');"
					});
					LinkButton linkButton4 = (LinkButton)e.Item.Cells[1].FindControl("LBtnStatDel");
					linkButton4.Attributes.Add("onclick", "javascript:return confirm('确定删除此项数据吗？');");
				}
				else
				{
					LinkButton linkButton5 = (LinkButton)e.Item.Cells[1].FindControl("LBtnStatView");
					linkButton5.Attributes["onclick"] = string.Concat(new object[]
					{
						"javascript:return MonthProductValueEdit('Other','",
						e.Item.Cells[0].Text,
						"',1,'",
						this.ProjectCode,
						"');"
					});
				}
				if (dtStartDate.Year == DateTime.Now.Year && dtStartDate.Month == DateTime.Now.Month && dataRowView["IsHaveReprot"] != null)
				{
					switch (Convert.ToInt32(dataRowView["IsHaveReprot"].ToString()))
					{
					case 0:
					{
						LinkButton linkButton6 = (LinkButton)e.Item.Cells[2].FindControl("LBtnReprotWeave");
						linkButton6.Attributes["onclick"] = string.Concat(new object[]
						{
							"javascript:return MonthProductValueEdit('Add','",
							e.Item.Cells[0].Text,
							"',2,'",
							this.ProjectCode,
							"');"
						});
						break;
					}
					case 1:
						if (dataRowView["IsHaveExamine"] != null)
						{
							int num = int.Parse(dataRowView["IsHaveExamine"].ToString().Trim());
							if (num == 0)
							{
								LinkButton linkButton7 = (LinkButton)e.Item.Cells[2].FindControl("LBtnReprotEdit");
								linkButton7.Attributes["onclick"] = string.Concat(new object[]
								{
									"javascript:return MonthProductValueEdit('Upd','",
									e.Item.Cells[0].Text,
									"',2,'",
									this.ProjectCode,
									"');"
								});
								LinkButton linkButton8 = (LinkButton)e.Item.Cells[2].FindControl("LBtnReprotDel");
								linkButton8.Attributes.Add("onclick", "javascript:return confirm('确定删除此项数据吗？');");
							}
						}
						break;
					}
				}
				LinkButton linkButton9 = (LinkButton)e.Item.Cells[2].FindControl("LBtnReprotView");
				linkButton9.Attributes["onclick"] = string.Concat(new object[]
				{
					"javascript:return MonthProductValueEdit('Other','",
					e.Item.Cells[0].Text,
					"',2,'",
					this.ProjectCode,
					"');"
				});
				if (dataRowView["IsHaveStat"] != null)
				{
					switch (Convert.ToInt32(dataRowView["IsHaveStat"].ToString()))
					{
					case 0:
						if (dtStartDate.Year == DateTime.Now.Year && dtStartDate.Month == DateTime.Now.Month)
						{
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatWeave")).Enabled = true;
						}
						else
						{
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatWeave")).Enabled = false;
						}
						break;
					case 1:
						if (dtStartDate.Year == DateTime.Now.Year && dtStartDate.Month == DateTime.Now.Month)
						{
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatEdit")).Visible = true;
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatView")).Visible = true;
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatDel")).Visible = true;
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatWeave")).Visible = false;
						}
						else
						{
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatEdit")).Visible = true;
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatView")).Visible = true;
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatDel")).Visible = true;
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatWeave")).Visible = false;
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatEdit")).Enabled = false;
							((LinkButton)e.Item.Cells[1].FindControl("LBtnStatDel")).Enabled = false;
						}
						break;
					}
				}
				if (dataRowView["IsHaveReprot"] != null)
				{
					switch (Convert.ToInt32(dataRowView["IsHaveReprot"].ToString()))
					{
					case 0:
						if (dtStartDate.Year == DateTime.Now.Year && dtStartDate.Month == DateTime.Now.Month)
						{
							((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotWeave")).Enabled = true;
						}
						else
						{
							((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotWeave")).Enabled = false;
						}
						break;
					case 1:
						if (dtStartDate.Year == DateTime.Now.Year && dtStartDate.Month == DateTime.Now.Month)
						{
							if (dataRowView["IsHaveExamine"] != null)
							{
								switch (int.Parse(dataRowView["IsHaveExamine"].ToString().Trim()))
								{
								case 0:
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotEdit")).Visible = true;
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotView")).Visible = true;
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotDel")).Visible = true;
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotWeave")).Visible = false;
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotExamine")).Visible = true;
									break;
								case 1:
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotEdit")).Visible = true;
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotView")).Visible = true;
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotDel")).Visible = true;
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotWeave")).Visible = false;
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotExamine")).Visible = true;
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotEdit")).Enabled = false;
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotDel")).Enabled = false;
									((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotExamine")).Enabled = false;
									break;
								}
							}
						}
						else
						{
							((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotEdit")).Visible = true;
							((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotView")).Visible = true;
							((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotDel")).Visible = true;
							((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotWeave")).Visible = false;
							((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotEdit")).Enabled = false;
							((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotDel")).Enabled = false;
						}
						break;
					}
					if (base.Request["t"] != null && base.Request["t"].ToString().Trim() == "1")
					{
						((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotExamine")).Visible = false;
					}
				}
				break;
			}
			}
			e.Item.Cells[1].Style["display"] = "none";
		}
		private void DGrdMonthProductValue_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			DateTime dtStartDate = Convert.ToDateTime(e.Item.Cells[0].Text.Trim().Replace("年", "-").Replace("月", "-1"));
			if (e.CommandName == "LBtnStatDel")
			{
				if (this.ProductValueAct.DeleteProductValueStat(this.ProjectCode, dtStartDate) == 1)
				{
					this.DataGrid_Bind();
				}
				else
				{
					this.RegisterStartupScript("错误", "<script>alert('本月产值统计删除失败！');</script>");
				}
			}
			if (e.CommandName == "LBtnReprotDel")
			{
				if (this.ProductValueAct.DeleteProductValueReport(this.ProjectCode, dtStartDate) == 1)
				{
					this.DataGrid_Bind();
				}
				else
				{
					this.RegisterStartupScript("错误", "<script>alert('本月产值上报删除失败！');</script>");
				}
			}
			if (e.CommandName == "LBtnReprotExamine")
			{
				if (this.ProductValueAct.ProductValueReportExamine(this.ProjectCode, dtStartDate, this.Session["yhdm"].ToString()) == 1)
				{
					this.DataGrid_Bind();
					return;
				}
				this.RegisterStartupScript("错误", "<script>alert('本月产值上报审核失败！');</script>");
			}
		}
	}


