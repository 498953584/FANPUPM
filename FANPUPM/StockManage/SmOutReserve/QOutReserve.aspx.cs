using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Services;
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
public partial class StockManage_SmOutReserve_QOutReserve : NBasePage, IRequiresSessionState
{
	private OutReserveBll outReserveBll = new OutReserveBll();
	private OutStockBll outStockBll = new OutStockBll();
	private TreasuryStockBll treasuryStockBll = new TreasuryStockBll();
	private string prjId = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		this.gvOutReserve.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["prjGuid"]))
		{
			this.prjId = base.Request["prjGuid"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			this.lblProject.Text = pTPrjInfoService.GetById(this.prjId).PrjName;
		}
	}
	public void BindGv()
	{
		string str = string.Empty;
		if (!string.IsNullOrEmpty(this.prjId))
		{
			str = " and procode='" + this.prjId + "'";
		}
		else
		{
			str = " and procode is null ";
		}
		this.BindGv(this.outReserveBll.GetListByParm(this.txtPPcode.Text.Trim(), this.txtStartTime.Text, this.txtEndTime.Text, this.txtPeople.Value.Trim(), "", this.hfldTrea.Value, " and p1.flowstate=1 and p1.isout='false' " + str));
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvOutReserve.DataSource = storageDataSource;
			this.gvOutReserve.DataBind();
			this.gvOutReserve.HeaderRow.Cells[0].Visible = false;
			this.gvOutReserve.Rows[0].Visible = false;
			return;
		}
		this.gvOutReserve.DataSource = storageDataSource;
		this.gvOutReserve.DataBind();
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvOutReserve.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void btnOk_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (GridViewRow gridViewRow in this.gvOutReserve.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						List<OutStockModel> listArray = this.outStockBll.GetListArray(" where orcode='" + checkBox.ToolTip + "'");
						OutReserveModel model = this.outReserveBll.GetModel(checkBox.ToolTip);
						foreach (OutStockModel current in listArray)
						{
							List<TreasuryStockModel> listArray2 = this.treasuryStockBll.GetListArray(string.Concat(new object[]
							{
								" where scode='",
								current.scode,
								"' and sprice=",
								current.sprice,
								" and corp='",
								current.corp,
								"' and tcode='",
								model.tcode,
								"'  order by intime asc"
							}));
							decimal num = 0m;
							foreach (TreasuryStockModel current2 in listArray2)
							{
								num += current2.snumber;
							}
							if (current.number > num)
							{
								base.RegisterScript("alert('库存量不足!!')");
								base.RegisterScript("location='QOutReserve.aspx?prjGuid=" + base.Request["prjGuid"].ToString() + "'");
								return;
							}
							foreach (TreasuryStockModel current3 in listArray2)
							{
								if (current3.snumber >= current.number)
								{
									current3.snumber -= current.number;
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
								if (current3.snumber < current.number)
								{
									current.number -= current3.snumber;
									this.treasuryStockBll.Delete(current3.tsid);
								}
							}
						}
						OutReserveModel model2 = this.outReserveBll.GetModel(checkBox.ToolTip);
						model2.isout = true;
						model2.IsOutTime = DateTime.Now;
						int num2 = this.outReserveBll.Update(null, model2);
						if (num2 > 0)
						{
							base.RegisterScript("alert('系统提示：\\n\\n操作成功');");
						}
						else
						{
							base.RegisterScript("alert('系统提示：\\n\\n操作失败');");
						}
					}
				}
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n出库出现异常,出库失败！');");
			}
		}
		base.RegisterScript("location='QOutReserve.aspx?prjGuid=" + base.Request["prjGuid"].ToString() + "'");
	}
	protected void gvOutReserve_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvOutReserve.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
}


