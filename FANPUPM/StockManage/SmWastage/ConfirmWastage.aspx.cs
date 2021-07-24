using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class StockManage_SmWastage_ConfirmWastage : NBasePage, IRequiresSessionState
{
	private SmWastageBll smWastageBll = new SmWastageBll();
	private SmWastageStockBll smWastageStockBll = new SmWastageStockBll();
	private TreasuryStockBll treasuryStockBll = new TreasuryStockBll();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	public void BindGv()
	{
		this.BindGv(this.smWastageBll.GetListByParm(this.txtPPcode.Text.Trim(), this.txtStartTime.Text, this.txtEndTime.Text, this.txtPeople.Value.Trim(), this.hfldTrea.Value, " and p1.flowstate=1 and p1.isout='false'", this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex));
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvWastage.DataSource = storageDataSource;
			this.gvWastage.DataBind();
			this.gvWastage.HeaderRow.Cells[0].Visible = false;
			this.gvWastage.Rows[0].Visible = false;
			return;
		}
		this.gvWastage.DataSource = storageDataSource;
		this.gvWastage.DataBind();
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void btnOk_Click(object sender, System.EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (GridViewRow gridViewRow in this.gvWastage.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						System.Collections.Generic.List<SmWastageStockModel> listArray = this.smWastageStockBll.GetListArray(" where WastageCode='" + checkBox.ToolTip + "' ");
						SmWastageModel modelByCode = this.smWastageBll.GetModelByCode(checkBox.ToolTip);
						foreach (SmWastageStockModel current in listArray)
						{
							System.Collections.Generic.List<TreasuryStockModel> listArray2 = this.treasuryStockBll.GetListArray(string.Concat(new object[]
							{
								" where scode='",
								current.ResourceCode,
								"' and sprice=",
								current.Sprice,
								" and ISNULL(corp,'')='",
								current.Corp,
								"' and tcode='",
								modelByCode.Treasurycode,
								"'  order by intime asc"
							}));
							decimal num = 0m;
							foreach (TreasuryStockModel current2 in listArray2)
							{
								num += current2.snumber;
							}
							if (current.Number > num)
							{
								base.RegisterScript("top.ui.alert('库存量不足!')");
								base.RegisterScript("location='ConfirmWastage.aspx'");
								return;
							}
							foreach (TreasuryStockModel current3 in listArray2)
							{
								if (current3.snumber >= current.Number)
								{
									current3.snumber -= current.Number;
									if (current3.snumber == 0m)
									{
										this.treasuryStockBll.Delete(current3.tsid);
									}
									else
									{
										this.treasuryStockBll.Update(current3);
									}
									Common2.AlarmMethod(current3.tcode, current3.scode);
									break;
								}
								if (current3.snumber < current.Number)
								{
									current.Number -= current3.snumber;
									this.treasuryStockBll.Delete(current3.tsid);
								}
							}
						}
						SmWastageModel modelByCode2 = this.smWastageBll.GetModelByCode(checkBox.ToolTip);
						modelByCode2.Isout = true;
						modelByCode2.IsOutTime = new System.DateTime?(System.DateTime.Now);
						int num2 = this.smWastageBll.Update(null, modelByCode2);
						if (num2 > 0)
						{
							base.RegisterScript("top.ui.show('操作成功');");
						}
						else
						{
							base.RegisterScript("top.ui.show('操作失败');");
						}
					}
				}
			}
			catch (System.Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("top.ui.alert('出库出现异常,出库失败！');");
			}
		}
		base.RegisterScript("location='ConfirmWastage.aspx'");
	}
	protected void gvWastage_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvWastage.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


