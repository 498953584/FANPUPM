using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProductValuePlanFrm : PageBase, IRequiresSessionState
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
				return;
			}
			this.ProjectCode = new Guid(base.Request["pc"].ToString());
			this.HdnProjectCode.Value = this.ProjectCode.ToString();
			if (!this.Page.IsPostBack)
			{
				this.LblMsg.Text = base.Request["pn"].ToString();
				this.DataGrid_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
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
				e.Item.Cells[0].Text = Convert.ToDateTime(dataRowView["StartDate"].ToString()).ToString("yyyy年MM月");
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.DGrdMonthProductValue.ClientID.ToString() + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				try
				{
					e.Item.Cells[3].Text = this.ProductValueAct.GetCurrMonthProductValueSta(this.ProjectCode, dtStartDate).ToString();
				}
				catch
				{
					e.Item.Cells[3].Text = 0m.ToString();
				}
				if (dtStartDate.Year == DateTime.Now.Year && dtStartDate.Month == DateTime.Now.Month + 1 && dataRowView["IsHavePlan"] != null)
				{
					switch (Convert.ToInt32(dataRowView["IsHavePlan"].ToString()))
					{
					case 0:
					{
						LinkButton linkButton = (LinkButton)e.Item.Cells[2].FindControl("LBtnReprotWeave");
						linkButton.Attributes["onclick"] = string.Concat(new object[]
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
						if (dataRowView["IsHavePlanExamine"] != null)
						{
							int num = int.Parse(dataRowView["IsHavePlanExamine"].ToString().Trim());
							if (num == 0)
							{
								LinkButton linkButton2 = (LinkButton)e.Item.Cells[2].FindControl("LBtnReprotEdit");
								linkButton2.Attributes["onclick"] = string.Concat(new object[]
								{
									"javascript:return MonthProductValueEdit('Upd','",
									e.Item.Cells[0].Text,
									"',2,'",
									this.ProjectCode,
									"');"
								});
								LinkButton linkButton3 = (LinkButton)e.Item.Cells[2].FindControl("LBtnReprotDel");
								linkButton3.Attributes.Add("onclick", "javascript:return confirm('确定删除此项数据吗？');");
							}
						}
						break;
					}
				}
				LinkButton linkButton4 = (LinkButton)e.Item.Cells[2].FindControl("LBtnReprotView");
				linkButton4.Attributes["onclick"] = string.Concat(new object[]
				{
					"javascript:return MonthProductValueEdit('Other','",
					e.Item.Cells[0].Text,
					"',2,'",
					this.ProjectCode,
					"');"
				});
				if (dataRowView["IsHavePlan"] != null)
				{
					switch (Convert.ToInt32(dataRowView["IsHavePlan"].ToString()))
					{
					case 0:
						if (dtStartDate.Year == DateTime.Now.Year && dtStartDate.Month == DateTime.Now.Month + 1)
						{
							((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotWeave")).Enabled = true;
						}
						else
						{
							((LinkButton)e.Item.Cells[2].FindControl("LBtnReprotWeave")).Enabled = false;
						}
						break;
					case 1:
						if (dtStartDate.Year == DateTime.Now.Year && dtStartDate.Month == DateTime.Now.Month + 1)
						{
							if (dataRowView["IsHavePlanExamine"] != null)
							{
								switch (int.Parse(dataRowView["IsHavePlanExamine"].ToString().Trim()))
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
				if (this.ProductValueAct.DeleteProductValuePlan(this.ProjectCode, dtStartDate) == 1)
				{
					this.DataGrid_Bind();
				}
				else
				{
					this.RegisterStartupScript("错误", "<script>alert('本月产值计划删除失败！');</script>");
				}
			}
			if (e.CommandName == "LBtnReprotExamine")
			{
				if (this.ProductValueAct.ProductValuePlanExamine(this.ProjectCode, dtStartDate, this.Session["yhdm"].ToString()) == 1)
				{
					this.DataGrid_Bind();
					return;
				}
				this.RegisterStartupScript("错误", "<script>alert('本月产值计划审核失败！');</script>");
			}
		}
	}


