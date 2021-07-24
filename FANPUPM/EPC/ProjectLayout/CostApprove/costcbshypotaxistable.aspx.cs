using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CostCbsHypotaxisTable : PageBase, IRequiresSessionState
	{
		private CBSAction ca = new CBSAction();
		private Guid ProjectCode
		{
			get
			{
				object obj = this.ViewState["ProjectCode"];
				if (obj != null)
				{
					return (Guid)this.ViewState["ProjectCode"];
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["ProjectCode"] = value;
			}
		}
		private string NodeCode
		{
			get
			{
				object obj = this.ViewState["NodeCode"];
				if (obj != null)
				{
					return (string)this.ViewState["NodeCode"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["NodeCode"] = value;
			}
		}
		protected string NodeName
		{
			get
			{
				object obj = this.ViewState["NodeName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["NodeName"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null || base.Request["nc"] == null)
			{
				this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
				return;
			}
			this.ProjectCode = new Guid(base.Request["pc"].ToString().Trim());
			this.NodeCode = base.Request["nc"].ToString().Trim();
			if (base.Request["nn"] != null)
			{
				this.NodeName = base.Request["nn"].ToString();
			}
			if (!this.Page.IsPostBack)
			{
				ArrayList cBSHypotaxisTable = this.ca.GetCBSHypotaxisTable(this.ProjectCode, this.NodeCode);
				this.DGrdData_Bind(cBSHypotaxisTable);
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DgCostCbs.ItemDataBound += new DataGridItemEventHandler(this.DgCostCbs_ItemDataBound);
		}
		private void DGrdData_Bind(ArrayList arr)
		{
			this.DgCostCbs.DataSource = arr;
			this.Session["TempData"] = arr;
			this.DgCostCbs.DataBind();
		}
		private ArrayList GetData()
		{
			ArrayList arrayList = new ArrayList();
			if (this.DgCostCbs.Items.Count > 0)
			{
				foreach (DataGridItem dataGridItem in this.DgCostCbs.Items)
				{
					CBSHypotaxisTableInfo cBSHypotaxisTableInfo = new CBSHypotaxisTableInfo();
					cBSHypotaxisTableInfo.ProjectCode = this.ProjectCode;
					cBSHypotaxisTableInfo.NodeCode = this.NodeCode;
					cBSHypotaxisTableInfo.NodeName = this.NodeName;
					cBSHypotaxisTableInfo.BudgetYear = ((DropDownList)dataGridItem.Cells[3].Controls[1]).SelectedValue;
					cBSHypotaxisTableInfo.BudgetMonth = ((DropDownList)dataGridItem.Cells[3].Controls[3]).SelectedValue;
					string value = (((TextBox)dataGridItem.Cells[2].Controls[1]).Text == "") ? "0" : ((TextBox)dataGridItem.Cells[2].Controls[1]).Text.Trim();
					try
					{
						cBSHypotaxisTableInfo.BudgetMoney = Convert.ToDecimal(value);
					}
					catch
					{
					}
					arrayList.Add(cBSHypotaxisTableInfo);
				}
			}
			return arrayList;
		}
		private void DgCostCbs_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				try
				{
					TextBox textBox = (TextBox)e.Item.Cells[2].FindControl("TxtBudgetMoney");
					textBox.Attributes["onblur"] = "checkDecimal(this);";
				}
				catch
				{
				}
				try
				{
					DropDownList dropDownList = (DropDownList)e.Item.Cells[3].FindControl("ddlYear");
					DropDownList dropDownList2 = (DropDownList)e.Item.Cells[3].FindControl("ddlMonth");
					CBSHypotaxisTableInfo cBSHypotaxisTableInfo = (CBSHypotaxisTableInfo)e.Item.DataItem;
					dropDownList.SelectedValue = cBSHypotaxisTableInfo.BudgetYear;
					dropDownList2.SelectedValue = cBSHypotaxisTableInfo.BudgetMonth;
				}
				catch
				{
				}
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.DgCostCbs.ClientID,
					"');ClickRow('",
					e.Item.ItemIndex.ToString(),
					"');"
				});
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			ArrayList data = this.GetData();
			if (this.ca.AddBudgetMoney(data) == 1)
			{
				this.Page.RegisterStartupScript("", "<script>alert('保存成功!');returnValue=true;window.close();</script>");
				return;
			}
			this.Page.RegisterStartupScript("", "<script>alert('保存失败!');</script>");
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			ArrayList data = this.GetData();
			data.Insert(0, new CBSHypotaxisTableInfo
			{
				ProjectCode = this.ProjectCode,
				NodeCode = this.NodeCode,
				NodeName = this.NodeName,
				BudgetMoney = 0m,
				BudgetMonth = DateTime.Now.Month.ToString(),
				BudgetYear = DateTime.Now.Year.ToString()
			});
			this.DGrdData_Bind(data);
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			ArrayList data = this.GetData();
			data.RemoveAt(int.Parse(this.HdnRowId.Value));
			this.DGrdData_Bind(data);
		}
	}


