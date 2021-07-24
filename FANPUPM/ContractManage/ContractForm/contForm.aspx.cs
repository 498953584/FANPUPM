using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractForm_contForm : NBasePage, IRequiresSessionState
{
	private decimal IncoSumPrice;
	private decimal IncoSumEndPrice;
	private decimal IncoSumBalance;
	private decimal IncoSumBack;
	private decimal PayoutSumPrice;
	private decimal PayoutSumEndPrice;
	private decimal PayoutSumBalance;
	private decimal PayoutSumBack;
	private decimal oddsprice;
	private decimal oddsEndPrice;
	private decimal oddsBl;
	private decimal oddsBk;
	private decimal IcPrice;
	private decimal IcEndPrice;
	private decimal IcBl;
	private decimal IcBk;
	private decimal PoPrice;
	private decimal PoEndPrice;
	private decimal PoIcBl;
	private decimal PoIcBk;
	private formBLL form = new formBLL();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Incomet_Bind();
		}
	}
	public void Incomet_Bind()
	{
		DataTable dataSource = this.form.SelPrj(this.hdfSel.Value);
		this.Repeater1.DataSource = dataSource;
		this.Repeater1.DataBind();
	}
	protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemIndex == 0)
		{
			this.oddsprice = 0m;
			this.oddsEndPrice = 0m;
			this.oddsBl = 0m;
			this.oddsBk = 0m;
			this.IcPrice = 0m;
			this.IcEndPrice = 0m;
			this.IcBl = 0m;
			this.IcBk = 0m;
			this.PoPrice = 0m;
			this.PoEndPrice = 0m;
			this.PoIcBl = 0m;
			this.PoIcBk = 0m;
		}
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			Label label = e.Item.FindControl("labIndex") as Label;
			label.Text = (e.Item.ItemIndex + 1).ToString();
			Repeater repeater = e.Item.FindControl("Repeater3") as Repeater;
			if (repeater != null)
			{
				string str = ((DataRowView)e.Item.DataItem)["prjGuid"].ToString();
				repeater.DataSource = this.form.SelIncomet(" and project='" + str + "'");
				repeater.DataBind();
			}
			Label label2 = e.Item.FindControl("labPrice") as Label;
			Label label3 = e.Item.FindControl("labEndPrice") as Label;
			Label label4 = e.Item.FindControl("labBalance") as Label;
			Label label5 = e.Item.FindControl("labBack") as Label;
			label2.Text = (this.IncoSumPrice - this.PayoutSumPrice).ToString();
			label3.Text = (this.IncoSumEndPrice - this.PayoutSumEndPrice).ToString();
			label4.Text = (this.IncoSumBalance - this.PayoutSumBalance).ToString();
			label5.Text = (this.IncoSumBack - this.PayoutSumBack).ToString();
			this.oddsprice += this.IncoSumPrice - this.PayoutSumPrice;
			this.oddsEndPrice += this.IncoSumEndPrice - this.PayoutSumEndPrice;
			this.oddsBl += this.IncoSumBalance - this.PayoutSumBalance;
			this.oddsBk += this.IncoSumBack - this.PayoutSumBack;
		}
		if (e.Item.ItemType == ListItemType.Footer)
		{
			Label label6 = e.Item.FindControl("labIcPrice") as Label;
			Label label7 = e.Item.FindControl("labIcEndPrice") as Label;
			Label label8 = e.Item.FindControl("labIcBl") as Label;
			Label label9 = e.Item.FindControl("labIcBk") as Label;
			Label label10 = e.Item.FindControl("labPoPrice") as Label;
			Label label11 = e.Item.FindControl("labPoEndPrice") as Label;
			Label label12 = e.Item.FindControl("labPoIcBl") as Label;
			Label label13 = e.Item.FindControl("labPoIcBk") as Label;
			Label label14 = e.Item.FindControl("labOddsPrice") as Label;
			Label label15 = e.Item.FindControl("labOddsEndPrice") as Label;
			Label label16 = e.Item.FindControl("labOddsBl") as Label;
			Label label17 = e.Item.FindControl("labOddsBk") as Label;
			label14.Text = this.oddsprice.ToString();
			label15.Text = this.oddsEndPrice.ToString();
			label16.Text = this.oddsBl.ToString();
			label17.Text = this.oddsBk.ToString();
			label6.Text = this.IcPrice.ToString();
			label7.Text = this.IcEndPrice.ToString();
			label8.Text = this.IcBl.ToString();
			label9.Text = this.IcBk.ToString();
			label10.Text = this.PoPrice.ToString();
			label11.Text = this.PoEndPrice.ToString();
			label12.Text = this.PoIcBl.ToString();
			label13.Text = this.PoIcBk.ToString();
		}
	}
	protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			if (e.Item.ItemIndex == 0)
			{
				this.IncoSumPrice = 0m;
				this.IncoSumEndPrice = 0m;
				this.IncoSumBalance = 0m;
				this.IncoSumBack = 0m;
				this.PayoutSumPrice = 0m;
				this.PayoutSumEndPrice = 0m;
				this.PayoutSumBalance = 0m;
				this.PayoutSumBack = 0m;
			}
			Repeater repeater = e.Item.FindControl("Repeater2") as Repeater;
			if (repeater != null)
			{
				string text = ((DataRowView)e.Item.DataItem)["project"].ToString();
				this.hdfPrjNum.Value = text;
				int arg_F9_0 = this.form.SelIncomet(" and project='" + text + "'").Rows.Count;
				string text2 = ((DataRowView)e.Item.DataItem)["ContractID"].ToString();
				repeater.DataSource = this.form.SelPayoutCon(text2);
				repeater.DataBind();
				Label label = e.Item.FindControl("labIncoBalance") as Label;
				label.Text = this.form.GetIncoBalance(text2).ToString();
				Label label2 = e.Item.FindControl("labIncoBack") as Label;
				label2.Text = this.form.GetIncoBack(text2).ToString();
				Label label3 = e.Item.FindControl("labEndPrice") as Label;
				string price = ((DataRowView)e.Item.DataItem)["ContractPrice"].ToString();
				label3.Text = WebUtil.GetEnPrice(price, text2);
				if (((DataRowView)e.Item.DataItem)["ContractPrice"].ToString() != null && ((DataRowView)e.Item.DataItem)["ContractPrice"].ToString() != "")
				{
					this.IncoSumPrice += System.Convert.ToDecimal(((DataRowView)e.Item.DataItem)["ContractPrice"].ToString());
				}
				if (label.Text != "" && label.Text != null)
				{
					this.IncoSumBalance += System.Convert.ToDecimal(label.Text);
				}
				if (label2.Text != "" && label2.Text != null)
				{
					this.IncoSumBack += System.Convert.ToDecimal(label2.Text);
				}
				if (label3.Text != "" && label3.Text != null)
				{
					this.IncoSumEndPrice += System.Convert.ToDecimal(label3.Text);
				}
			}
		}
		if (e.Item.ItemType == ListItemType.Footer)
		{
			Repeater repeater2 = e.Item.FindControl("Repeater4") as Repeater;
			if (repeater2 != null)
			{
				repeater2.DataSource = this.form.SelPayoutPrj(this.hdfPrjNum.Value);
				repeater2.DataBind();
			}
			Label label4 = e.Item.FindControl("labIncoSumPrice") as Label;
			Label label5 = e.Item.FindControl("labIncoSumEndPrice") as Label;
			Label label6 = e.Item.FindControl("labIncoSumBalance") as Label;
			Label label7 = e.Item.FindControl("labIncoSumBack") as Label;
			Label label8 = e.Item.FindControl("labPayoutSumPrice") as Label;
			Label label9 = e.Item.FindControl("labPayoutSumEndPrice") as Label;
			Label label10 = e.Item.FindControl("labPayoutSumBalance") as Label;
			Label label11 = e.Item.FindControl("labPayoutSumBack") as Label;
			label4.Text = this.IncoSumPrice.ToString();
			label5.Text = this.IncoSumEndPrice.ToString();
			label6.Text = this.IncoSumBalance.ToString();
			label7.Text = this.IncoSumBack.ToString();
			label8.Text = this.PayoutSumPrice.ToString();
			label9.Text = this.PayoutSumEndPrice.ToString();
			label10.Text = this.PayoutSumBalance.ToString();
			label11.Text = this.PayoutSumBack.ToString();
			this.IcPrice += this.IncoSumPrice;
			this.IcEndPrice += this.IncoSumEndPrice;
			this.IcBl += this.IncoSumBalance;
			this.IcBk += this.IncoSumBack;
			this.PoPrice += this.PayoutSumPrice;
			this.PoEndPrice += this.PayoutSumEndPrice;
			this.PoIcBl += this.PayoutSumBalance;
			this.PoIcBk += this.PayoutSumBack;
		}
	}
	protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			string payoutCode = ((DataRowView)e.Item.DataItem)["ContractID"].ToString();
			Label label = e.Item.FindControl("labPayoutBalance") as Label;
			label.Text = this.form.GetPayoutBalance(payoutCode).ToString();
			Label label2 = e.Item.FindControl("labPayoutBack") as Label;
			label2.Text = this.form.GetPayoutBack(payoutCode).ToString();
			if (label.Text != "" && label.Text != null)
			{
				this.PayoutSumBalance += System.Convert.ToDecimal(label.Text);
			}
			if (label2.Text != "" && label2.Text != null)
			{
				this.PayoutSumBack += System.Convert.ToDecimal(label2.Text);
			}
			if (((DataRowView)e.Item.DataItem)["ContractMoney"].ToString() != null && ((DataRowView)e.Item.DataItem)["ContractMoney"].ToString() != "")
			{
				this.PayoutSumPrice += System.Convert.ToDecimal(((DataRowView)e.Item.DataItem)["ContractMoney"].ToString());
			}
			if (((DataRowView)e.Item.DataItem)["ModifiedMoney"].ToString() != null && ((DataRowView)e.Item.DataItem)["ModifiedMoney"].ToString() != "")
			{
				this.PayoutSumEndPrice += System.Convert.ToDecimal(((DataRowView)e.Item.DataItem)["ModifiedMoney"].ToString());
			}
		}
	}
	protected void Repeater4_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			string payoutCode = ((DataRowView)e.Item.DataItem)["ContractID"].ToString();
			Label label = e.Item.FindControl("labPayoutBalance") as Label;
			label.Text = this.form.GetPayoutBalance(payoutCode).ToString();
			Label label2 = e.Item.FindControl("labPayoutBack") as Label;
			label2.Text = this.form.GetPayoutBack(payoutCode).ToString();
			if (label.Text != "" && label.Text != null)
			{
				this.PayoutSumBalance += System.Convert.ToDecimal(label.Text);
			}
			if (label2.Text != "" && label2.Text != null)
			{
				this.PayoutSumBack += System.Convert.ToDecimal(label2.Text);
			}
			if (((DataRowView)e.Item.DataItem)["ContractMoney"].ToString() != null && ((DataRowView)e.Item.DataItem)["ContractMoney"].ToString() != "")
			{
				this.PayoutSumPrice += System.Convert.ToDecimal(((DataRowView)e.Item.DataItem)["ContractMoney"].ToString());
			}
			if (((DataRowView)e.Item.DataItem)["ModifiedMoney"].ToString() != null && ((DataRowView)e.Item.DataItem)["ModifiedMoney"].ToString() != "")
			{
				this.PayoutSumEndPrice += System.Convert.ToDecimal(((DataRowView)e.Item.DataItem)["ModifiedMoney"].ToString());
			}
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		if (this.txtPrjNum.Text.ToString() != "")
		{
			this.hdfSel.Value = " and prjName like '%" + this.txtPrjNum.Text + "%'";
		}
		this.Incomet_Bind();
		this.hdfSel.Value = "";
	}
}


