using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_receiveGoods_AddReceiveNote : NBasePage, IRequiresSessionState
{
	private MaterialWantPlan materialWantPlan = new MaterialWantPlan();
	private MeterialPlanStock meterialPlanStock = new MeterialPlanStock();
	private sendGoodsBLL sendgoods = new sendGoodsBLL();
	private receiveNoteBLL receiveNote = new receiveNoteBLL();
	private receiveGoodsBLL receiveGood = new receiveGoodsBLL();
	private Treasury treasuryBll = new Treasury();
	private sendNoteBll sendnote = new sendNoteBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		if (base.Request.QueryString["id"] != null)
		{
			SendNodteModel sendNodteModel = new SendNodteModel();
			string a = base.Request["type"].ToString();
			if (a == "add")
			{
				sendNodteModel = this.sendnote.GetSendNoteModel(base.Request.QueryString["id"]);
				this.lblTitle.Text = "新增发货通知单";
				this.hdfSnId.Value = Guid.NewGuid().ToString();
				string[] snId = new string[]
				{
					sendNodteModel.snId
				};
				this.ViewState["DataTable"] = this.sendgoods.GetResourceBypncode(snId);
				string value = "/StockManage/sendGoods/ViewSendNote.aspx?ic=" + base.Request["id"].ToString();
				this.ifView.Attributes.Add("src", value);
			}
			else
			{
				if (a == "edit")
				{
					sm_receiveNote modelByrnId = this.receiveNote.GetModelByrnId(base.Request.QueryString["id"]);
					this.lblTitle.Text = "编辑发货通知单";
					this.hdfSnId.Value = modelByrnId.rnId.ToString();
					this.txtRemark.Text = modelByrnId.remark.ToString();
					this.txtExplain.Text = modelByrnId.Explain.ToString();
					string[] rnid = new string[]
					{
						modelByrnId.rnId
					};
					this.ViewState["DataTable"] = this.receiveGood.getResourceByRnid(rnid);
					sendNodteModel = this.sendnote.GetSendNoteModel(modelByrnId.snId);
					string value2 = "/StockManage/sendGoods/ViewSendNote.aspx?ic=" + modelByrnId.snId;
					this.ifView.Attributes.Add("src", value2);
				}
			}
			this.hdfSendUser.Value = sendNodteModel.snAddUser;
			this.hdfPrjCode.Value = sendNodteModel.prjCode.ToString();
			this.hdGuid.Value = sendNodteModel.snId;
			this.hdfrnCode.Value = sendNodteModel.snCode;
			this.hdnCodeList.Value = "1";
			this.BindGv();
		}
	}
	public DataTable GetTable()
	{
		return this.meterialPlanStock.GetListArrayByWpcode(this.hdwzId.Value);
	}
	public void BindGv()
	{
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		if (dataTable.Rows.Count == 0)
		{
			dataTable.Rows.Add(dataTable.NewRow());
			this.gvNeedNote.DataSource = dataTable;
			this.gvNeedNote.DataBind();
			this.gvNeedNote.HeaderRow.Cells[0].Visible = false;
			this.gvNeedNote.Rows[0].Visible = false;
			return;
		}
		this.gvNeedNote.DataSource = dataTable;
		this.gvNeedNote.DataBind();
	}
	protected void btnShowGv_Click(object sender, EventArgs e)
	{
		DataTable dataTable = this.GetTable();
		DataTable dataTable2 = this.ViewState["DataTable"] as DataTable;
		if (dataTable2 != null)
		{
			dataTable2.PrimaryKey = new DataColumn[]
			{
				dataTable2.Columns["scode"]
			};
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["scode"]
			};
			dataTable2.Merge(dataTable, true);
			dataTable = dataTable2;
		}
		this.ViewState["DataTable"] = dataTable;
		this.BindGv();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		foreach (GridViewRow gridViewRow in this.gvNeedNote.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			if (checkBox != null && checkBox.Checked)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					if (checkBox.ToolTip == dataTable.Rows[i]["scode"].ToString())
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
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (this.treasuryBll.IsRelationTreasury(this.hdfPrjCode.Value))
		{
			string str = "";
			using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
			{
				sqlConnection.Open();
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				try
				{
					sm_receiveNote sm_receiveNote = new sm_receiveNote();
					sm_receiveNote.remark = this.txtRemark.Text.ToString();
					sm_receiveNote.snId = this.hdGuid.Value;
					sm_receiveNote.rnTime = new DateTime?(DateTime.Now);
					sm_receiveNote.rnUser = base.UserCode;
					sm_receiveNote.rnId = this.hdfSnId.Value;
					sm_receiveNote.rnCode = this.hdfrnCode.Value;
					sm_receiveNote.stId = "";
					sm_receiveNote.soId = "";
					sm_receiveNote.Explain = this.txtExplain.Text.ToString();
					int num = 0;
					string a = base.Request["type"].ToString();
					if (a == "add")
					{
						num = this.receiveNote.Add(sqlTransaction, sm_receiveNote);
					}
					else
					{
						if (a == "edit")
						{
							num = this.receiveNote.Update(sqlTransaction, sm_receiveNote);
							this.receiveGood.Delete(sqlTransaction, this.hdfSnId.Value);
						}
					}
					if (num != 0)
					{
						DataTable dataTable = (DataTable)this.ViewState["DataTable"];
						if (dataTable != null)
						{
							int num2 = 0;
							foreach (DataRow dataRow in dataTable.Rows)
							{
								if (base.Request.QueryString["id"] == null && string.IsNullOrEmpty(this.hdnCodeList.Value))
								{
									str = str + "'" + dataRow["wpcode"].ToString() + "',";
								}
								TextBox textBox = this.gvNeedNote.Rows[num2].FindControl("txtNum") as TextBox;
								receiveGoods receiveGoods = new receiveGoods();
								if (textBox.Text != "")
								{
									receiveGoods.number = new decimal?(Convert.ToDecimal(textBox.Text));
								}
								else
								{
									receiveGoods.number = new decimal?(0m);
								}
								receiveGoods.scode = dataRow["scode"].ToString();
								receiveGoods.rnID = this.hdfSnId.Value;
								receiveGoods.sgId = Guid.NewGuid().ToString();
								try
								{
									receiveGoods.price = new decimal?(Convert.ToDecimal(dataRow["price"].ToString()));
								}
								catch
								{
									receiveGoods.price = new decimal?(0m);
								}
								receiveGoods.suppyCode = dataRow["suppyCode"].ToString();
								this.receiveGood.Add(sqlTransaction, receiveGoods);
								num2++;
							}
						}
					}
					this.sendnote.updateState(sqlTransaction, this.hdGuid.Value);
					sqlTransaction.Commit();
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("top.ui.alert('" + this.SetMsg() + "成功！');").Append(Environment.NewLine);
					stringBuilder.Append("top.ui.tabSuccess({parentName: '_addReceiveNote'});");
					base.RegisterScript(stringBuilder.ToString());
				}
				catch
				{
					sqlTransaction.Rollback();
					base.RegisterScript("alert('系统提示：\\n\\n对不起" + this.SetMsg() + "失败！');");
				}
				return;
			}
		}
		StringBuilder stringBuilder2 = new StringBuilder();
		stringBuilder2.Append("top.ui.alert('收货项目对应的仓库被更改，请设置此项目对应的仓库');").Append(Environment.NewLine);
		stringBuilder2.Append("top.ui.tabSuccess({parentName: '_addReceiveNote'});");
		base.RegisterScript(stringBuilder2.ToString());
	}
	public string SetMsg()
	{
		if (base.Request["type"].ToString() == "add")
		{
			return "添加";
		}
		return "更新";
	}
	protected void btnCrop_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < this.gvNeedNote.Rows.Count; i++)
		{
			CheckBox checkBox = this.gvNeedNote.Rows[i].FindControl("chkBox") as CheckBox;
			if (checkBox.Checked)
			{
				this.gvNeedNote.Rows[i].FindControl("labCrop");
			}
		}
	}
	protected void gvNeedNote_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			Button button = e.Row.FindControl("btnCorp") as Button;
			string sql = string.Concat(new string[]
			{
				"select * from Res_SuperGradeTab where superid='",
				this.gvNeedNote.DataKeys[e.Row.RowIndex].Values[1].ToString(),
				"' and billsid='",
				base.Request.QueryString["id"],
				"'"
			});
			DataTable table = DBHelper.GetTable(sql);
			if (table.Rows.Count > 0)
			{
				button.Enabled = false;
			}
			else
			{
				button.Enabled = true;
			}
			button.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);openCorp('",
				this.gvNeedNote.DataKeys[e.Row.RowIndex].Values[1].ToString(),
				"','",
				this.gvNeedNote.DataKeys[e.Row.RowIndex].Values[0].ToString(),
				"');"
			});
		}
	}
	protected void btnCorp_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
}


