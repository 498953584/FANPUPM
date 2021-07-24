using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Refunding_AddRefunding : NBasePage, IRequiresSessionState
{
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private Treasury treasury = new Treasury();
	private BackStockBll backStockBll = new BackStockBll();
	private RefundingBll refundingBll = new RefundingBll();
	private OutStockBll outStockBll = new OutStockBll();
	private string prjId = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		if (base.Request.QueryString["t"] != null)
		{
			this.btnDel.Enabled = false;
			this.btnSave.Enabled = false;
			this.btnSelectByd.Disabled = true;
		}
		if (base.Request.QueryString["id"] != null)
		{
			this.hdwzId.Value = "1";
			this.lblTitle.Text = "编辑退库单";
			RefundingModel model = this.refundingBll.GetModel(base.Request.QueryString["id"]);
			this.txtExplain.Text = model.explain;
			this.txtInTime.Text = model.intime.ToString();
			this.txtPeople.Value = model.person;
			this.txtPPCode.Text = model.rcode;
			this.txtProjectName.Value = this.pTPrjInfoBll.GetModelByPrjGuid(model.procode).PrjName;
			this.hdnProjectCode.Value = model.procode;
			this.hdflowstate.Value = model.flowstate.ToString();
			this.hdGuid.Value = model.rid;
			this.hfldTrea.Value = model.tcode;
			this.txtTrea.Text = this.treasury.GetModel(model.tcode).tname;
			this.ViewState["DataTable"] = this.backStockBll.GetTableByRcode(model.rcode);
			this.BindGv();
		}
		else
		{
			this.lblTitle.Text = "新增退库单";
			this.hdnProjectCode.Value = this.prjId;
			this.txtProjectName.Value = (this.txtProjectName.Value = this.pTPrjInfoBll.GetModelByPrjGuid(this.prjId).PrjName);
			this.txtPPCode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
			this.txtInTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			this.hdGuid.Value = Guid.NewGuid().ToString();
			this.txtPeople.Value = PageHelper.QueryUser(this, base.UserCode);
			this.ViewState["DataTable"] = this.backStockBll.GetTableByRcode("'',");
			this.BindGv();
		}
		this.FileLink1.MID = 1805;
		this.FileLink1.FID = this.hdGuid.Value;
		this.FileLink1.Type = 1;
	}
	public DataTable GetTable()
	{
		return this.outStockBll.GetListByOrcode(this.hdwzId.Value);
	}
	public void BindGv()
	{
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		if (dataTable.Rows.Count == 0)
		{
			this.gvNeedNote.DataSource = dataTable;
			this.gvNeedNote.DataBind();
			return;
		}
		this.gvNeedNote.DataSource = dataTable;
		this.gvNeedNote.DataBind();
		string total = Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
		GridViewUtility.AddTotalRow(this.gvNeedNote, total, 10);
	}
	protected void btnShowGv_Click(object sender, EventArgs e)
	{
		if (this.hdwzId.Value.Length > 0)
		{
			this.UpdateDataSource();
			DataTable dataTable = this.ViewState["DataTable"] as DataTable;
			DataTable table = this.GetTable();
			if (dataTable != null && table != null)
			{
				if (dataTable.Rows.Count == 1 && string.IsNullOrEmpty(dataTable.Rows[0]["scode"].ToString()))
				{
					dataTable.Rows.RemoveAt(0);
				}
				dataTable.PrimaryKey = new DataColumn[]
				{
					dataTable.Columns["scode"],
					dataTable.Columns["CorpId"],
					dataTable.Columns["sprice"],
					dataTable.Columns["orcode"]
				};
				table.PrimaryKey = new DataColumn[]
				{
					table.Columns["scode"],
					table.Columns["CorpId"],
					table.Columns["sprice"],
					table.Columns["orcode"]
				};
				dataTable.Merge(table, true);
			}
			this.BindGv();
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		this.UpdateDataSource();
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		foreach (GridViewRow gridViewRow in this.gvNeedNote.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			if (checkBox != null && checkBox.Checked)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					if (checkBox.ToolTip == dataTable.Rows[i]["orsid"].ToString())
					{
						((DataTable)this.ViewState["DataTable"]).Rows.Remove(dataTable.Rows[i]);
					}
				}
			}
		}
		if (dataTable.Rows.Count == 1 && dataTable.Rows[0]["scode"].ToString() == "")
		{
			((DataTable)this.ViewState["DataTable"]).Rows.Remove(dataTable.Rows[0]);
		}
		this.BindGv();
	}
	public decimal GetNumByScode(string scode)
	{
		string value = "";
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		foreach (GridViewRow gridViewRow in this.gvNeedNote.Rows)
		{
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				TextBox textBox = gridViewRow.FindControl("txtTNum") as TextBox;
				if (textBox.ToolTip == scode)
				{
					value = textBox.Text;
				}
			}
		}
		return Convert.ToDecimal(value);
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		this.UpdateDataSource();
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				RefundingModel refundingModel = new RefundingModel();
				refundingModel.annx = "";
				refundingModel.explain = this.txtExplain.Text;
				refundingModel.flowstate = Convert.ToInt32(this.hdflowstate.Value);
				refundingModel.intime = Convert.ToDateTime(this.txtInTime.Text);
				refundingModel.person = this.txtPeople.Value;
				refundingModel.rcode = this.txtPPCode.Text;
				refundingModel.rid = this.hdGuid.Value;
				refundingModel.procode = this.hdnProjectCode.Value;
				refundingModel.isin = false;
				refundingModel.tcode = this.hfldTrea.Value;
				int num;
				if (base.Request.QueryString["id"] != null)
				{
					num = this.refundingBll.Update(sqlTransaction, refundingModel);
				}
				else
				{
					num = this.refundingBll.Add(sqlTransaction, refundingModel);
				}
				if (num != 0)
				{
					this.backStockBll.DeleteByWhere(sqlTransaction, " where rcode='" + refundingModel.rcode + "'");
					DataTable dataTable = (DataTable)this.ViewState["DataTable"];
					if (dataTable != null)
					{
						foreach (DataRow dataRow in dataTable.Rows)
						{
							BackStockModel backStockModel = new BackStockModel();
							backStockModel.corp = dataRow["CorpId"].ToString();
							backStockModel.intype = "";
							backStockModel.linkcode = dataRow["orcode"].ToString();
							backStockModel.number = Convert.ToDecimal(dataRow["tnumber"]);
							backStockModel.rcode = this.txtPPCode.Text;
							backStockModel.rsid = Guid.NewGuid().ToString();
							backStockModel.scode = dataRow["scode"].ToString();
							backStockModel.sprice = Convert.ToDecimal(dataRow["sprice"]);
							backStockModel.taskId = dataRow["TaskId"].ToString();
							this.backStockBll.Add(sqlTransaction, backStockModel);
						}
					}
				}
				sqlTransaction.Commit();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("top.ui.show('" + this.SetMsg() + "成功！');").Append(Environment.NewLine);
				stringBuilder.Append("winclose('AddRefunding','RefundingList.aspx?prjGuid=" + this.hdnProjectCode.Value + "',true)");
				base.RegisterScript(stringBuilder.ToString());
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("top.ui.show('" + this.SetMsg() + "失败！');");
			}
		}
	}
	protected void gvNeedNote_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvNeedNote.DataKeys[e.Row.RowIndex].Value.ToString();
		}
		TextBox textBox = e.Row.FindControl("txtTNum") as TextBox;
		TextBox textBox2 = e.Row.FindControl("txtNum") as TextBox;
		if (textBox2 != null)
		{
			textBox.Attributes["onblur"] = "chkNum(this.No,this," + textBox2.ClientID + ")";
			HiddenField expr_A2 = this.hdTnumId;
			expr_A2.Value = expr_A2.Value + textBox.ClientID + ",";
		}
	}
	public string GetNumber(string num, string orcode, string sprice, string scode, string corp)
	{
		if (string.IsNullOrEmpty(num) || string.IsNullOrEmpty(sprice))
		{
			return "";
		}
		string numByParam = this.backStockBll.GetNumByParam(orcode, Convert.ToDecimal(sprice), scode, corp, this.txtPPCode.Text, "-2");
		if (string.IsNullOrEmpty(numByParam))
		{
			return num.ToString();
		}
		return Convert.ToString(Convert.ToDecimal(num) - Convert.ToDecimal(numByParam));
	}
	public string SetMsg()
	{
		if (base.Request.QueryString["id"] != null)
		{
			return "更新";
		}
		return "添加";
	}
	private void UpdateDataSource()
	{
		if (this.ViewState["DataTable"] is DataTable)
		{
			DataTable dataTable = this.ViewState["DataTable"] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				GridViewRow gridViewRow = this.gvNeedNote.Rows[i];
				Control control = gridViewRow.FindControl("txtTNum");
				if (control is TextBox)
				{
					TextBox textBox = control as TextBox;
					if (!string.IsNullOrEmpty(textBox.Text.Trim()))
					{
						dataRow["tnumber"] = Convert.ToDecimal(textBox.Text.Trim());
					}
					else
					{
						dataRow["tnumber"] = 0m;
					}
					decimal d = Convert.ToDecimal(dataRow["sprice"]);
					dataRow["Total"] = Convert.ToDecimal(dataRow["tnumber"]) * d;
				}
			}
			this.ViewState["DataTable"] = dataTable;
		}
	}
}


