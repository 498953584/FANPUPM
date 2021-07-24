using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CostCbsPlan : PageBase, IRequiresSessionState
	{
		private CBSAction ca = new CBSAction();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.prjcode.Value = base.Request["pc"].ToString();
				this.ca.InitialzeCBS(new Guid(this.prjcode.Value));
				this.DataGrid_Bind();
			}
		}
		private void DataGrid_Bind()
		{
			ArrayList allCbs = this.ca.GetAllCbs(this.prjcode.Value, "plan");
			this.DgCostCbs.DataSource = allCbs;
			this.DgCostCbs.DataBind();
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
		private void DgCostCbs_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				CBSFeeNodeInfo cBSFeeNodeInfo = (CBSFeeNodeInfo)e.Item.DataItem;
				LinkButton linkButton = (LinkButton)e.Item.Cells[5].Controls[1];
				if (cBSFeeNodeInfo.IsHaveChild)
				{
					linkButton.Visible = false;
				}
				else
				{
					linkButton.Attributes["onclick"] = string.Concat(new string[]
					{
						"if(!OpenWindow('",
						this.prjcode.Value.Trim(),
						"','",
						cBSFeeNodeInfo.NodeCode,
						"','",
						base.Server.UrlEncode(cBSFeeNodeInfo.NodeName.Replace("&nbsp;", "")),
						"')) return false;"
					});
				}
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.DgCostCbs.ClientID + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
			}
		}
		private void BtnSave_Click(object sender, EventArgs e)
		{
			ArrayList dataGrid_Data = this.GetDataGrid_Data();
			if (this.ca.UpdCBS(dataGrid_Data) == 1)
			{
				this.JS.Text = "alert('保存成功!');window.close();";
			}
			else
			{
				this.JS.Text = "alert('保存失败!');window.close();";
			}
			this.DataGrid_Bind();
		}
		private ArrayList GetDataGrid_Data()
		{
			ArrayList arrayList = new ArrayList();
			foreach (DataGridItem dataGridItem in this.DgCostCbs.Items)
			{
				if (dataGridItem.Cells[3].Text.Trim() == "False")
				{
					CostCbsInfo costCbsInfo = new CostCbsInfo();
					costCbsInfo.PrjCode = this.prjcode.Value.ToString();
					costCbsInfo.NodeCode = dataGridItem.Cells[0].Text;
					try
					{
						costCbsInfo.Money = ((((TextBox)dataGridItem.FindControl("txtPrice")).Text == "0") ? 0m : Convert.ToDecimal(((TextBox)dataGridItem.FindControl("txtPrice")).Text));
					}
					catch
					{
					}
					try
					{
						costCbsInfo.TargetMoney = ((((TextBox)dataGridItem.FindControl("TxtTargetMoney")).Text == "0") ? 0m : Convert.ToDecimal(((TextBox)dataGridItem.FindControl("TxtTargetMoney")).Text));
					}
					catch
					{
					}
					arrayList.Add(costCbsInfo);
				}
			}
			return arrayList;
		}
	}


