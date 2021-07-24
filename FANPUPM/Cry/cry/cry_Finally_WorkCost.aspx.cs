using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class Finally_WorkCost : PageBase, IRequiresSessionState
	{
		private CryReport ca = new CryReport();

		private void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.ddl_pn.DataSource = this.ca.GetWorkCostpcList();
				this.ddl_pn.DataTextField = "pn";
				this.ddl_pn.DataValueField = "pc";
				this.ddl_pn.DataBind();
				this.ddl_year.DataSource = this.ca.GetCostYear();
				this.ddl_year.DataTextField = "y";
				this.ddl_year.DataValueField = "y";
				this.ddl_year.DataBind();
				if (this.ddl_month.Items.Count == 0)
				{
					for (int i = 1; i <= 12; i++)
					{
						ListItem listItem = new ListItem();
						listItem.Selected = false;
						listItem.Value = i.ToString();
						listItem.Text = i.ToString();
						this.ddl_month.Items.Add(listItem);
					}
				}
				try
				{
					this.ddl_year.SelectedValue = DateTime.Now.Year.ToString();
					this.ddl_month.SelectedValue = DateTime.Now.Month.ToString();
				}
				catch
				{
				}
				this.ddl_year_SelectedIndexChanged(sender, null);
				this.ddl_month_SelectedIndexChanged(sender, null);
				this.ddl_pn_SelectedIndexChanged(sender, null);
				new ContAction();
				this.Lb_userName.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, base.UserCode);
			}
		}
		private void databind()
		{
			this.dg_ManPower.DataSource = this.ca.GetFinally_WorkCostList(this.ddl_pn.SelectedValue.ToString(), this.ddl_year.SelectedValue.ToString(), this.ddl_month.SelectedValue.ToString());
			this.dg_ManPower.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.ddl_pn.SelectedIndexChanged += new EventHandler(this.ddl_pn_SelectedIndexChanged);
			this.ddl_year.SelectedIndexChanged += new EventHandler(this.ddl_year_SelectedIndexChanged);
			this.ddl_month.SelectedIndexChanged += new EventHandler(this.ddl_month_SelectedIndexChanged);
			this.dg_ManPower.ItemDataBound += new DataGridItemEventHandler(this.dg_ManPower_ItemDataBound);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void dg_ManPower_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
			{
				e.Item.Cells.Clear();
				string text = "进度</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='83'><P align='center'>已完主要<span style='mso-spacerun:yes'>&nbsp; </span>实物进度</P>\t</TD><td colspan='7' class='grid_head' bgcolor= #ece9d8  align='center'>&nbsp;</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='130'>到竣工尚有主要实物进度</TD><td class='grid_head' bgcolor= #ece9d8  align='center' colSpan='5'>&nbsp;</TD></tr><tr><td height='32' class='grid_head' bgcolor= #ece9d8  align='center'>造价</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='114'>预算造价</TD><td colspan='2' class='grid_head' bgcolor= #ece9d8  align='center'>元</TD><td colspan='2' class='grid_head' bgcolor= #ece9d8  align='center' width='184'>已完累计产值</TD><td colspan='3' class='grid_head' bgcolor= #ece9d8  align='center'>元</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='85'>到竣工尚可报产值</TD><td colspan='2' class='grid_head' bgcolor= #ece9d8  align='center'>&nbsp;</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='72'>预测最终工程造价</TD><td colspan='2' class='grid_head' bgcolor= #ece9d8  align='center'>元</TD></tr><tr ><td colspan='2' rowspan='2'  class='grid_head' bgcolor= #ece9d8  align='center' width='92'>成本项目</TD><td colspan='4' class='grid_head' bgcolor= #ece9d8  align='center'>到本月为止的累计成本</TD><td colspan='4' class='grid_head' bgcolor= #ece9d8  align='center'>预计到竣工还将发生的成本</TD><td colspan='4' class='grid_head' bgcolor= #ece9d8  align='center'>最终成本预测</TD></tr><tr><td class='grid_head' bgcolor= #ece9d8  align='center' width='105'>预算成本</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='105'>实际成本</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='99'>降低额</TD><td colspan='1' class='grid_head' bgcolor= #ece9d8  align='center'  width='99'>降低率</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='105'>预算成本</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='105'>实际成本</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='99'>降低额</TD><td class='grid_head' bgcolor= #ece9d8  align='center'>降低率</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='105'>预算成本</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='105'>实际成本</TD><td class='grid_head' bgcolor= #ece9d8  align='center'>降低额</TD><td class='grid_head' bgcolor= #ece9d8  align='center'>降低率</TD></tr><tr><td colspan='2'  class='grid_head' bgcolor= #ece9d8  align='center' width='92'>甲</TD><td class='grid_head' bgcolor= #ece9d8  align='center' x:num>1</TD><td class='grid_head' bgcolor= #ece9d8  align='center' x:num>2</TD><td class='grid_head' bgcolor= #ece9d8  align='center'>3=1-2</TD><td colspan='1' class='grid_head' bgcolor= #ece9d8  align='center'>4=3÷1</TD><td class='grid_head' bgcolor= #ece9d8  align='center' x:num>5</TD><td class='grid_head' bgcolor= #ece9d8  align='center' x:num>6</TD><td class='grid_head' bgcolor= #ece9d8  align='center' width='99'>7=5-6</TD><td class='grid_head' bgcolor= #ece9d8  align='center'>8=7÷5</TD><td class='grid_head' bgcolor= #ece9d8  align='center'>9=1+5</TD><td class='grid_head' bgcolor= #ece9d8  align='center'>10=2+6</TD><td class='grid_head' bgcolor= #ece9d8  align='center'>11=9-10</TD><td class='grid_head' bgcolor= #ece9d8  align='center'>12=11÷9";
				TableCell tableCell = new TableCell();
				tableCell.ColumnSpan = 1;
				tableCell.CssClass = "grid_head";
				tableCell.Text = text;
				e.Item.Cells.Add(tableCell);
				return;
			}
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
		private void ddl_pn_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.databind();
		}
		private void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.databind();
		}
		private void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.databind();
		}
	}


