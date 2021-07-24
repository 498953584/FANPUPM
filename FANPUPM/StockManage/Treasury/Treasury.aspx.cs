using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Treasury_Treasury : NBasePage, IRequiresSessionState
{
	private const int tflagIndex = 3;
	private const string rootCode = "0";
	private readonly Treasury treasury = new Treasury();
	private SmEnum.SmSetValue manageMode;

	protected override void OnInit(EventArgs e)
	{
		this.manageMode = this.treasury.GetManageMode();
		if (this.treasury.GetCount() == 0)
		{
			this.treasury.AddRoot();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		this.gvwTreasury.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.treasury.ParseTreasuryList(this.tvTreasury, string.Empty, false, false);
			if (this.tvTreasury.Nodes.Count > 0)
			{
				if (!string.IsNullOrEmpty(base.Request["tcode"]))
				{
					string storagetSelect = base.Request["tcode"];
					this.SetStoragetSelect(storagetSelect);
				}
				else
				{
					this.tvTreasury.Nodes[0].Selected = true;
				}
				this.hfSelectValue.Value = this.tvTreasury.SelectedValue;
				DataTable childData = this.treasury.GetChildData(this.tvTreasury.SelectedValue);
				this.gvwTreasury.DataSource = childData;
				this.gvwTreasury.DataBind();
			}
			SmEnum.SmSetValue arg_DB_0 = this.manageMode;
		}
	}
	protected void TreeView_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.hfSelectValue.Value = this.tvTreasury.SelectedValue;
		this.BindGridView();
	}
	private bool ValidateDelete(string[] arrTcode)
	{
		for (int i = 0; i < arrTcode.Length; i++)
		{
			string tcode = arrTcode[i];
			int chidldNodesLength = this.treasury.GetChidldNodesLength(tcode);
			if (chidldNodesLength > 0)
			{
				base.RegisterScript("alert('系统提示：\\n所选仓库不能删除！');");
				return false;
			}
		}
		return true;
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		string value = this.hfTcode.Value;
		string[] array;
		if (!value.Contains("^"))
		{
			array = new string[]
			{
				value
			};
		}
		else
		{
			array = value.Substring(1).Split(new char[]
			{
				'^'
			});
		}
		if (!this.ValidateDelete(array))
		{
			return;
		}
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string tcode = array2[i];
			TreasuryModel model = this.treasury.GetModel(tcode);
			if (this.manageMode != SmEnum.SmSetValue.ParallelMode && model.tflag.ToString() == "1")
			{
				base.RegisterScript("alert('系统提示：\\n总分模式下不能删除总库！');");
				return;
			}
		}
		if (this.treasury.Delete(array) == 0)
		{
			base.RegisterScript("alert('系统提示：\\n所选仓库已被使用，不能删除！');");
			return;
		}
		this.tvTreasury.Nodes.Clear();
		this.treasury.ParseTreasuryList(this.tvTreasury, string.Empty, false, false);
		this.tvTreasury.ExpandAll();
		this.SetStoragetSelect(this.hfSelectValue.Value);
		this.BindGridView(this.hfSelectValue.Value);
	}
	protected void gvwTreasury_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwTreasury.DataKeys[e.Row.RowIndex].Value.ToString();
			Label label = e.Row.FindControl("labprjName") as Label;
			if (label.Text.ToString() != "")
			{
				label.Text = this.treasury.GetPrjName(label.Text.ToString());
			}
		}
	}
	protected void gvwTreasury_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwTreasury.PageIndex = e.NewPageIndex;
		this.BindGridView();
	}
	private void BindGridView(string currentTcode)
	{
		int chidldNodesLength = this.treasury.GetChidldNodesLength(currentTcode);
		DataTable childData = this.treasury.GetChildData(currentTcode);
		if (chidldNodesLength == 0)
		{
			childData.Rows.Add(childData.NewRow());
		}
		this.gvwTreasury.DataSource = childData;
		this.gvwTreasury.DataBind();
		if (chidldNodesLength == 0)
		{
			this.gvwTreasury.Rows[0].Visible = false;
		}
	}
	private void BindGridView()
	{
		string selectedValue = this.tvTreasury.SelectedValue;
		int chidldNodesLength = this.treasury.GetChidldNodesLength(selectedValue);
		DataTable childData = this.treasury.GetChildData(selectedValue);
		if (chidldNodesLength == 0)
		{
			childData.Rows.Add(childData.NewRow());
		}
		this.gvwTreasury.DataSource = childData;
		this.gvwTreasury.DataBind();
		if (chidldNodesLength == 0)
		{
			this.gvwTreasury.Rows[0].Visible = false;
		}
	}
	protected void SetStoragetSelect(string selectedValue)
	{
		StringBuilder stringBuilder = new StringBuilder("0");
		for (int i = 0; i < selectedValue.Length / 3; i++)
		{
			stringBuilder.Append("/" + selectedValue.Substring(0, 3 * (i + 1)));
		}
		this.tvTreasury.FindNode(stringBuilder.ToString()).Select();
	}
	public SmEnum.SmSetValue GetModel()
	{
		return this.manageMode;
	}
}


