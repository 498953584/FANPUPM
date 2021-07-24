using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CostApprove : PageBase, IRequiresSessionState
	{

		protected CBSAction CBSAct
		{
			get
			{
				return new CBSAction();
			}
		}
		protected CostApproveAction CostApproveAct
		{
			get
			{
				return new CostApproveAction();
			}
		}
		protected Guid ProjectCode
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
		protected DateTime StartDate
		{
			get
			{
				object obj = this.ViewState["STARTDATE"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return DateTime.Now;
			}
			set
			{
				this.ViewState["STARTDATE"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null || base.Request["m"] == null)
			{
				this.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
				return;
			}
			this.ProjectCode = ((base.Request["pc"].ToString() == "") ? Guid.Empty : new Guid(base.Request["pc"].ToString()));
			this.StartDate = Convert.ToDateTime(base.Request["m"].ToString().Replace("年", "-").Replace("月", "-1"));
			this.HdnProjectCode.Value = this.ProjectCode.ToString();
			this.HdnStartDate.Value = this.StartDate.ToShortDateString();
			if (!this.Page.IsPostBack)
			{
				this.CBSAct.InitialzeCBS(this.ProjectCode.ToString(), this.StartDate);
				this.DGrdCon_Bind();
				this.DGrdResource_Bind(new DataTable());
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
			this.BtnSave.Click += new EventHandler(this.BtnSave_Click);
			this.DGrdConstruct.ItemCommand += new DataGridCommandEventHandler(this.DGrdConstruct_ItemCommand);
			this.DGrdConstruct.ItemDataBound += new DataGridItemEventHandler(this.DGrdConstruct_ItemDataBound);
			this.DGrdRelationRes.ItemDataBound += new DataGridItemEventHandler(this.DGrdRelationRes_ItemDataBound);
		}
		private void DGrdCon_Bind()
		{
			this.DGrdConstruct.DataSource = this.CostApproveAct.GetCostApproveList(this.ProjectCode, this.StartDate);
			this.DGrdConstruct.DataBind();
		}
		private void DGrdResource_Bind(DataTable DTable)
		{
			this.DGrdRelationRes.DataSource = DTable;
			this.DGrdRelationRes.DataBind();
		}
		private void DGrdConstruct_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.DGrdConstruct.ClientID + "');ClickRow(this);";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Cells[4].Attributes["onclick"] = string.Concat(new string[]
				{
					"javascript:return OpenWindow('",
					this.ProjectCode.ToString(),
					"','",
					dataRowView["TaskCode"].ToString(),
					"','",
					dataRowView["TaskBookCode"].ToString(),
					"');"
				});
			}
			e.Item.Cells[3].Style["display"] = "none";
			e.Item.Cells[5].Style["display"] = "none";
		}
		private void DGrdRelationRes_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex == -1)
			{
				CheckBox checkBox = (CheckBox)e.Item.FindControl("cbAll");
				if (checkBox != null)
				{
					checkBox.Attributes.Add("onclick", "selectAll(this)");
				}
			}
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.DGrdRelationRes.ClientID + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				switch (int.Parse((dataRowView["ResourceStyle"].ToString() == "") ? "0" : dataRowView["ResourceStyle"].ToString()))
				{
				case 1:
					e.Item.Cells[6].Text = "人工";
					break;
				case 2:
					e.Item.Cells[6].Text = "材料";
					break;
				case 3:
					e.Item.Cells[6].Text = "机械";
					break;
				default:
					e.Item.Cells[6].Text = "未知";
					break;
				}
				try
				{
					if (dataRowView["CostType"].ToString() != "0")
					{
						((TextBox)e.Item.Cells[7].FindControl("TxtCostType")).Text = this.GetstrCostType(dataRowView["CostType"].ToString());
						((HtmlInputHidden)e.Item.Cells[7].FindControl("HdnCostType")).Value = dataRowView["CostType"].ToString();
					}
					else
					{
						string value = "";
						string text = "";
						switch (int.Parse((dataRowView["ResourceStyle"].ToString() == "") ? "0" : dataRowView["ResourceStyle"].ToString()))
						{
						case 1:
							value = "001001001";
							text = "人工费";
							break;
						case 2:
							value = "001001002";
							text = "材料费";
							break;
						case 3:
							value = "001001003";
							text = "施工机械使用费";
							break;
						}
						((TextBox)e.Item.Cells[7].FindControl("TxtCostType")).Text = text;
						((HtmlInputHidden)e.Item.Cells[7].FindControl("HdnCostType")).Value = value;
					}
				}
				catch
				{
				}
				try
				{
					((CheckBox)e.Item.Cells[0].Controls[1]).Checked = (dataRowView["CostType"].ToString().Trim() != "0");
				}
				catch
				{
				}
				return;
			}
			default:
				return;
			}
		}
		private string GetstrCostType(string strCostType)
		{
			return this.CostApproveAct.GetCostType(this.ProjectCode, strCostType, this.StartDate);
		}
		private void DGrdConstruct_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if (e.CommandName == "Select")
			{
				string text = this.DGrdConstruct.Items[e.Item.ItemIndex].Cells[1].Text;
				string text2 = this.DGrdConstruct.Items[e.Item.ItemIndex].Cells[5].Text;
				DataTable taskBookResourceList = this.CostApproveAct.GetTaskBookResourceList(text, text2);
				this.DGrdResource_Bind(taskBookResourceList);
			}
		}
		private ArrayList GetDGrd_Data()
		{
			ArrayList arrayList = new ArrayList();
			foreach (DataGridItem dataGridItem in this.DGrdRelationRes.Items)
			{
				CostApproveInfo costApproveInfo = new CostApproveInfo();
				costApproveInfo.NoteID = ((dataGridItem.Cells[8].Text == "") ? 0 : int.Parse(dataGridItem.Cells[8].Text));
				costApproveInfo.CostType = ((((HtmlInputHidden)dataGridItem.Cells[7].FindControl("HdnCostType")).Value == "") ? "" : ((HtmlInputHidden)dataGridItem.Cells[7].FindControl("HdnCostType")).Value);
				costApproveInfo.Quantity = decimal.Parse(((TextBox)dataGridItem.FindControl("TxtQuantity")).Text);
				costApproveInfo.UnitPrice = float.Parse(((TextBox)dataGridItem.FindControl("TxtPrice")).Text);
				if (((CheckBox)dataGridItem.Cells[0].Controls[1]).Checked)
				{
					costApproveInfo.IsSelected = true;
				}
				arrayList.Add(costApproveInfo);
			}
			return arrayList;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			ArrayList dGrd_Data = this.GetDGrd_Data();
			if (this.CostApproveAct.UpdateCostApprove(dGrd_Data) == 1)
			{
				this.RegisterStartupScript("", "<script>alert('保存成功!');</script>");
				return;
			}
			this.RegisterStartupScript("", "<script>alert('保存失败!');</script>");
		}
		protected void BtnReturn_Click(object sender, EventArgs e)
		{
			this.Page.RegisterClientScriptBlock("Script", "<script language=javascript>self.location.href = 'MonthCostApprove.aspx?pc=" + this.ProjectCode.ToString() + "';</script>");
		}
	}


