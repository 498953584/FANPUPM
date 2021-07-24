using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Stocktake_StocktakeEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string storageCode = string.Empty;
	private string endDate = string.Empty;
	private string tCode = string.Empty;
	private string tName = string.Empty;
	private string isFirst = string.Empty;
	private StocktakeBll stocktakeBll = new StocktakeBll();
	private StocktakeDetailBll stocktakeDetailBll = new StocktakeDetailBll();
	private readonly string stockDataSourceName = "Stock";

	protected override void OnInit(EventArgs e)
	{
		if (base.Request["Action"] != null)
		{
			this.action = base.Request["Action"];
		}
		if (base.Request["endDate"] != null)
		{
			this.endDate = base.Request["endDate"];
		}
		if (base.Request["TCode"] != null)
		{
			this.tCode = base.Request["TCode"];
		}
		if (base.Request["TName"] != null)
		{
			this.tName = base.Request["TName"];
		}
		if (base.Request["endDate"] != null)
		{
			this.endDate = base.Request["endDate"];
		}
		if (base.Request["IsFirst"] != null)
		{
			this.isFirst = base.Request["IsFirst"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (this.ddlYear != null)
			{
				this.AddItem(this.ddlYear, "year");
			}
			if (this.ddlMonth != null)
			{
				this.AddItem(this.ddlMonth, "month");
			}
			if (this.action.Equals("Add"))
			{
				this.hfldStocktakeId.Value = Guid.NewGuid().ToString();
				this.txtCode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
				this.DateInTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				this.txtPerson.Text = PageHelper.QueryUser(this, base.UserCode);
				this.txtTName.Text = this.tName;
				this.txtEndDate.Text = this.endDate;
				if (this.isFirst == "True")
				{
					this.ddlYear.SelectedIndex = this.ddlYear.Items.Count - 1;
					this.ddlMonth.SelectedValue = DateTime.Now.Month.ToString("00");
					DateTime initializeDate = this.stocktakeBll.GetInitializeDate(this.tCode);
					if (initializeDate.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
					{
						this.txtBeginDate.Text = initializeDate.ToString("yyyy-MM-dd");
					}
					else
					{
						string text = this.stocktakeBll.GetStorageDate(this.tCode).ToString("yyyy-MM-dd");
						string text2 = this.stocktakeBll.GetAllocationDate(this.tCode).ToString("yyyy-MM-dd");
						if (text != DateTime.Now.ToString("yyyy-MM-dd") && text2 != DateTime.Now.ToString("yyyy-MM-dd") && Convert.ToDateTime(text).CompareTo(Convert.ToDateTime(text2)) < 0)
						{
							this.txtBeginDate.Text = text;
						}
						else
						{
							if (text != DateTime.Now.ToString("yyyy-MM-dd") && text2 != DateTime.Now.ToString("yyyy-MM-dd") && Convert.ToDateTime(text).CompareTo(Convert.ToDateTime(text2)) > 0)
							{
								this.txtBeginDate.Text = text2;
							}
							else
							{
								if (text != DateTime.Now.ToString("yyyy-MM-dd") && text2 == DateTime.Now.ToString("yyyy-MM-dd"))
								{
									this.txtBeginDate.Text = text;
								}
								else
								{
									if (text == DateTime.Now.ToString("yyyy-MM-dd") && text2 != DateTime.Now.ToString("yyyy-MM-dd"))
									{
										this.txtBeginDate.Text = text2;
									}
									else
									{
										this.txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
									}
								}
							}
						}
					}
				}
				else
				{
					StocktakeModel lastStocktakeModel = this.stocktakeBll.GetLastStocktakeModel(this.tCode);
					string stocktakeDate = lastStocktakeModel.StocktakeDate;
					string str = stocktakeDate.Substring(0, 4);
					string str2 = stocktakeDate.Substring(4, 2);
					string value = str + "-" + str2;
					string text3 = Convert.ToDateTime(value).AddMonths(1).ToString("yyyyMM");
					string selectedValue = text3.Substring(0, 4);
					string selectedValue2 = text3.Substring(4, 2);
					this.ddlYear.SelectedValue = selectedValue;
					this.ddlMonth.SelectedValue = selectedValue2;
					DateTime dateTime = lastStocktakeModel.BeginDate.AddDays(1.0);
					this.txtBeginDate.Text = dateTime.ToString("yyyy-MM-dd");
				}
				this.txtName.Text = this.ddlYear.SelectedValue + "年" + this.ddlMonth.SelectedValue + "月的盘点单";
				List<StocktakeDetailModel> dataSource = new List<StocktakeDetailModel>();
				dataSource = this.stocktakeDetailBll.GetByTreasuryCode(this.tCode, Convert.ToBoolean(this.isFirst), Convert.ToDateTime(this.endDate));
				this.gvwStocktake.DataSource = dataSource;
				this.gvwStocktake.DataBind();
			}
			else
			{
				StocktakeModel editModel = this.stocktakeBll.GetEditModel(this.tCode);
				this.hfldStocktakeId.Value = editModel.Id;
				this.txtCode.Text = editModel.Code;
				this.DateInTime.Text = editModel.InputDate.ToString("yyyy-MM-dd  HH:mm:ss");
				this.txtPerson.Text = editModel.InputUser;
				this.txtName.Text = editModel.Name;
				this.txtTName.Text = this.tName;
				this.txtBeginDate.Text = editModel.BeginDate.ToString("yyyy-MM-dd");
				this.txtEndDate.Text = editModel.EndDate.ToString("yyyy-MM-dd");
				string stocktakeDate2 = editModel.StocktakeDate;
				string selectedValue3 = stocktakeDate2.Substring(0, 4);
				string selectedValue4 = stocktakeDate2.Substring(4, 2);
				this.ddlYear.SelectedValue = selectedValue3;
				this.ddlMonth.SelectedValue = selectedValue4;
				this.hfldState.Value = editModel.State.ToString();
				this.txtExplain.Text = editModel.Note;
				List<StocktakeDetailModel> dataSource2 = new List<StocktakeDetailModel>();
				dataSource2 = this.stocktakeDetailBll.GetByStocktakeId(editModel.Id);
				this.gvwStocktake.DataSource = dataSource2;
				this.gvwStocktake.DataBind();
			}
			this.hfldTCode.Value = this.tCode;
			this.hfldToday.Value = DateTime.Now.ToString("yyyy/MM");
		}
	}
	protected void AddItem(DropDownList ddl, string action)
	{
		if (string.Compare(action, "month", true) == 0)
		{
			for (int i = 1; i <= 12; i++)
			{
				ddl.Items.Add(new ListItem(i.ToString(), i.ToString("00")));
			}
			return;
		}
		for (int j = DateTime.Now.Year - 4; j <= DateTime.Now.Year; j++)
		{
			ddl.Items.Add(new ListItem(j.ToString(), j.ToString()));
		}
	}
	private bool IsExists(DataTable target, DataRow row, string primarykey)
	{
		foreach (DataRow dataRow in target.Rows)
		{
			if (dataRow[primarykey].ToString() == row[primarykey].ToString())
			{
				return true;
			}
		}
		return false;
	}
	protected void gvwStocktake_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwStocktake.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (this.action.Equals("Add"))
		{
			this.Add(1);
			return;
		}
		this.Update(1);
	}
	protected void btnHangUp_Click(object sender, EventArgs e)
	{
		if (this.action.Equals("Add"))
		{
			this.Add(0);
			return;
		}
		this.Update(0);
	}
	private void Add(int state)
	{
		if (this.txtName.Text.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('盘点名称不能为空！');");
			return;
		}
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				this.AddStocktake(sqlTransaction, state);
				this.AddStocktakeDetail(sqlTransaction);
				StringBuilder stringBuilder = new StringBuilder();
				if (state == 0)
				{
					stringBuilder.Append("top.ui.show('挂起成功！');").Append(Environment.NewLine);
				}
				else
				{
					stringBuilder.Append("top.ui.show('保存并提交成功！');").Append(Environment.NewLine);
				}
				stringBuilder.Append("winclose('StocktakeEdit', 'StocktakeList.aspx?tcode=" + this.tCode + "', true)");
				base.RegisterScript(stringBuilder.ToString());
				sqlTransaction.Commit();
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("top.ui.show('操作失败！');");
			}
		}
	}
	private void AddStocktake(SqlTransaction trans, int state)
	{
		StocktakeModel stocktakeModel = new StocktakeModel();
		stocktakeModel.Id = this.hfldStocktakeId.Value;
		stocktakeModel.Code = this.txtCode.Text.Trim();
		stocktakeModel.Name = this.txtName.Text.Trim();
		stocktakeModel.TreasuryCode = this.tCode;
		stocktakeModel.StocktakeDate = this.ddlYear.SelectedValue + this.ddlMonth.SelectedValue;
		stocktakeModel.InputUser = this.txtPerson.Text.Trim();
		stocktakeModel.BeginDate = Convert.ToDateTime(this.txtBeginDate.Text.Trim());
		stocktakeModel.InputDate = Convert.ToDateTime(this.DateInTime.Text.Trim());
		stocktakeModel.EndDate = Convert.ToDateTime(this.txtEndDate.Text.Trim());
		stocktakeModel.State = state;
		stocktakeModel.FlowState = -1;
		stocktakeModel.Note = this.txtExplain.Text.Trim();
		this.stocktakeBll.Add(trans, stocktakeModel);
	}
	private void AddStocktakeDetail(SqlTransaction trans)
	{
		if (this.gvwStocktake.Rows.Count > 0)
		{
			List<StocktakeDetailModel> list = new List<StocktakeDetailModel>();
			foreach (GridViewRow gridViewRow in this.gvwStocktake.Rows)
			{
				list.Add(new StocktakeDetailModel
				{
					Id = Guid.NewGuid().ToString(),
					StocktakeId = this.hfldStocktakeId.Value.Trim(),
					ResourceCode = gridViewRow.Cells[1].Text,
					ResourceName = ((HiddenField)gridViewRow.Cells[2].FindControl("hlfdResourceName")).Value.Trim(),
					Specification = ((HiddenField)gridViewRow.Cells[3].FindControl("hlfdSpecification")).Value.Trim(),
					Unit = ((Label)gridViewRow.Cells[8].FindControl("lblUnit")).Text,
					Price = Convert.ToDecimal(gridViewRow.Cells[5].Text.Trim()),
					Supplier = ((HiddenField)gridViewRow.Cells[6].FindControl("hlfdSupplier")).Value.Trim(),
					SupplierId = ((HiddenField)gridViewRow.Cells[6].FindControl("hlfdSupplierId")).Value.Trim(),
					LastMonthNum = Convert.ToDecimal(((Label)gridViewRow.Cells[7].FindControl("lblLastMonthNum")).Text.Trim()),
					StorageNum = Convert.ToDecimal(((Label)gridViewRow.Cells[8].FindControl("lblStorageNum")).Text.Trim()),
					FirstStorageNum = Convert.ToDecimal(((Label)gridViewRow.Cells[9].FindControl("lblFirstStorageNum")).Text.Trim()),
					OutReserveNum = Convert.ToDecimal(((Label)gridViewRow.Cells[10].FindControl("lblOutReserveNum")).Text.Trim()),
					TransferringInNum = Convert.ToDecimal(((Label)gridViewRow.Cells[11].FindControl("lblTransferringInNum")).Text.Trim()),
					TransferringOutNum = Convert.ToDecimal(((Label)gridViewRow.Cells[12].FindControl("lblTransferringOutNum")).Text.Trim()),
					WastageNum = Convert.ToDecimal(((Label)gridViewRow.Cells[11].FindControl("lblWastageNum")).Text.Trim()),
					RefundingNum = Convert.ToDecimal(((Label)gridViewRow.Cells[13].FindControl("lblRefundingNum")).Text.Trim()),
					BookNum = Convert.ToDecimal(((Label)gridViewRow.Cells[14].FindControl("lblBookNum")).Text.Trim()),
					StocktakeNum = Convert.ToDecimal(((TextBox)gridViewRow.Cells[15].FindControl("txtNum")).Text.Trim()),
					InputDate = DateTime.Now,
					Note = ((TextBox)gridViewRow.Cells[17].FindControl("txtNote")).Text.Trim()
				});
			}
			this.stocktakeDetailBll.Add(trans, list);
		}
	}
	private void Update(int state)
	{
		if (this.txtName.Text.Trim() == "")
		{
			base.RegisterScript("top.ui.alert('盘点名称不能为空！');");
			return;
		}
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				this.UpdateStocktake(sqlTransaction, state);
				this.AddStocktakeDetail(sqlTransaction);
				sqlTransaction.Commit();
				StringBuilder stringBuilder = new StringBuilder();
				if (state == 0)
				{
					stringBuilder.Append("top.ui.show('挂起成功！');").Append(Environment.NewLine);
				}
				else
				{
					stringBuilder.Append("top.ui.show('保存并提交成功！');").Append(Environment.NewLine);
				}
				stringBuilder.Append("winclose('StocktakeEdit', 'StocktakeList.aspx?tcode=" + this.tCode + "', true)");
				base.RegisterScript(stringBuilder.ToString());
			}
			catch
			{
				base.RegisterScript("top.ui.alert('操作失败！');");
			}
		}
	}
	private void UpdateStocktake(SqlTransaction trans, int state)
	{
		StocktakeModel byId = this.stocktakeBll.GetById(this.hfldStocktakeId.Value);
		byId.Name = this.txtName.Text.Trim();
		byId.StocktakeDate = this.ddlYear.SelectedValue + this.ddlMonth.SelectedValue;
		byId.Note = this.txtExplain.Text.Trim();
		byId.State = state;
		byId.LockDate = byId.LockDate;
		this.stocktakeBll.Update(trans, byId);
	}
}


