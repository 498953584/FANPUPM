using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using cn.justwin.stockModel;
using cn.justwin.Web;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_sendGoods_AddSendNote : NBasePage, IRequiresSessionState
{
	private MaterialWantPlan materialWantPlan = new MaterialWantPlan();
	private MeterialPlanStock meterialPlanStock = new MeterialPlanStock();
	private SmPurchaseplanBll smPurchaseplanBll = new SmPurchaseplanBll();
	private PtYhmcBll ptYhmcBll = new PtYhmcBll();
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private SmPurchaseplanStockBll smPurchaseplanStockBll = new SmPurchaseplanStockBll();
	private PtYhmcBll yhmc = new PtYhmcBll();
	private sendGoodsBLL sendgoods = new sendGoodsBLL();
	private sendNoteBll sendnote = new sendNoteBll();
	private accountMange am = new accountMange();
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
			this.Session["HumanCode"] = "";
			this.Session["HumanName"] = "";
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
			this.lblTitle.Text = "编辑发货通知单";
			SendNodteModel sendNoteModel = this.sendnote.GetSendNoteModel(base.Request.QueryString["id"]);
			this.txtAddUser.Value = this.yhmc.GetModelById(sendNoteModel.snAddUser).v_xm;
			this.txtremark.Text = sendNoteModel.remark;
			this.txtsnAddTime.Text = sendNoteModel.snAddTime.ToString();
			this.txtsnCode.Text = sendNoteModel.snCode;
			this.txtProjectName.Value = this.pTPrjInfoBll.GetModelByPrjGuid(sendNoteModel.prjCode.ToString()).PrjName;
			this.hdnProjectCode.Value = sendNoteModel.prjCode.ToString();
			this.hdGuid.Value = sendNoteModel.snId;
			this.hdnCodeList.Value = "1";
			string userName = this.am.GetUserName(sendNoteModel.Limits.ToString());
			this.hf.Value = sendNoteModel.Limits.ToString();
			this.TBoxConsignee.Value = userName;
			string[] snId = new string[]
			{
				sendNoteModel.snId
			};
			this.ViewState["DataTable"] = this.sendgoods.GetResourceBypncode(snId);
			this.BindGv();
			return;
		}
		this.lblTitle.Text = "新增发货通知单";
		this.hdnProjectCode.Value = this.prjId;
		this.txtProjectName.Value = this.pTPrjInfoBll.GetModelByPrjGuid(this.prjId).PrjName;
		this.txtsnCode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
		this.txtsnAddTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		this.hdGuid.Value = Guid.NewGuid().ToString();
		this.txtAddUser.Value = PageHelper.QueryUser(this, base.UserCode);
		string[] snId2 = new string[]
		{
			"''"
		};
		DataTable resourceBypncode = this.sendgoods.GetResourceBypncode(snId2);
		this.ViewState["DataTable"] = resourceBypncode;
		this.BindGv();
	}
	public DataTable GetTable()
	{
		return this.meterialPlanStock.GetListArrayByWpcodenew(this.hdwzId.Value);
	}
	public void BindGv()
	{
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		this.hdfCount.Value = dataTable.Rows.Count.ToString();
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
		this.hdwzId.Value = string.Empty;
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
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				SendNodteModel sendNodteModel = new SendNodteModel();
				sendNodteModel.sendState = new int?(0);
				sendNodteModel.remark = this.txtremark.Text;
				sendNodteModel.snId = this.hdGuid.Value;
				sendNodteModel.snAddTime = new DateTime?(Convert.ToDateTime(this.txtsnAddTime.Text));
				sendNodteModel.snAddUser = base.UserCode;
				sendNodteModel.snCode = this.txtsnCode.Text;
				if (this.hdnProjectCode.Value != "")
				{
					sendNodteModel.prjCode = new Guid(this.hdnProjectCode.Value);
				}
				else
				{
					sendNodteModel.prjCode = new Guid("00000000-0000-0000-0000-000000000000");
				}
				sendNodteModel.Limits = (
					from c in this.hf.Value.Split(new char[]
					{
						','
					})
					where c.Length == 8
					select c).ToCsv();
				int num;
				if (base.Request.QueryString["id"] != null)
				{
					num = this.sendnote.Update(sqlTransaction, sendNodteModel);
				}
				else
				{
					num = this.sendnote.Add(sqlTransaction, sendNodteModel);
				}
				int num2 = 0;
				if (num != 0)
				{
					this.sendgoods.DeleteBysnId(sqlTransaction, sendNodteModel.snId);
					DataTable dataTable = (DataTable)this.ViewState["DataTable"];
					if (dataTable != null)
					{
						int num3 = 0;
						foreach (DataRow dataRow in dataTable.Rows)
						{
							TextBox textBox = this.gvNeedNote.Rows[num3].FindControl("txtNum") as TextBox;
							SendGoodsModel sendGoodsModel = new SendGoodsModel();
							if (textBox.Text != "")
							{
								sendGoodsModel.Number = new decimal?(Convert.ToDecimal(textBox.Text));
							}
							else
							{
								sendGoodsModel.Number = new decimal?(0m);
							}
							sendGoodsModel.scode = dataRow["scode"].ToString();
							sendGoodsModel.snCode = this.hdGuid.Value;
							sendGoodsModel.sgId = Guid.NewGuid().ToString();
							TextBox textBox2 = this.gvNeedNote.Rows[num3].FindControl("txtPrice") as TextBox;
							try
							{
								sendGoodsModel.Price = new decimal?(Convert.ToDecimal(textBox2.Text.ToString()));
							}
							catch
							{
								sendGoodsModel.Price = new decimal?(0m);
							}
							Label label = this.gvNeedNote.Rows[num3].FindControl("labCrop") as Label;
							if (label.ToolTip == "")
							{
								num2 = 1;
								break;
							}
							sendGoodsModel.suppyCode = label.ToolTip;
							this.sendgoods.Add(sqlTransaction, sendGoodsModel);
							num3++;
						}
						if (num2 == 1)
						{
							base.RegisterScript("top.ui.alert('供应商不能为空');");
							return;
						}
					}
				}
				if (num2 == 0)
				{
					this.UpdateWantplanAState(sqlTransaction, this.hdlfWantplanCodes.Value.Trim());
					sqlTransaction.Commit();
					string mes;
					if (base.Request.QueryString["id"] != null)
					{
						mes = "现场收货通知：发货单号为" + sendNodteModel.snCode + "的发货单已修改。";
					}
					mes = "现场收货通知：发货单号为" + sendNodteModel.snCode + "的物资已发货。";
					string[] array = (
						from c in this.hf.Value.Split(new char[]
						{
							','
						})
						where c.Length == 8
						select c).ToArray<string>();
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string jsyhdm = array2[i];
						this.getOrganiger(sendNodteModel.snId, mes, jsyhdm);
					}
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("top.ui.show('" + this.SetMsg() + "成功！');").Append(Environment.NewLine);
					stringBuilder.Append("top.ui.tabSuccess({parentName: '_addSendNote'});");
					base.RegisterScript(stringBuilder.ToString());
				}
				else
				{
					if (num2 == 1)
					{
						new StringBuilder();
						base.RegisterScript("top.ui.show('" + this.SetMsg() + "失败！');");
					}
				}
			}
			catch
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起" + this.SetMsg() + "失败！');");
			}
		}
	}
	public void UpdateWantplanAState(SqlTransaction trans, string wpcode)
	{
		if (base.Request.QueryString["id"] == null && string.IsNullOrEmpty(this.hdnCodeList.Value))
		{
			MaterialWantPlan materialWantPlan = new MaterialWantPlan();
			materialWantPlan.UpdateAcceptstate(trans, wpcode);
		}
	}
	private void getOrganiger(string xgid, string Mes, string jsyhdm)
	{
		PublicInterface.SendSysMsg(new PTDBSJ
		{
			V_LXBM = "026",
			I_XGID = xgid,
			DTM_DBSJ = DateTime.Now,
			V_Content = Mes,
			V_DBLJ = "StockManage/sendGoods/ViewSendNote.aspx?ic=" + xgid,
			V_YHDM = jsyhdm,
			V_TPLJ = "",
			C_OpenFlag = "1"
		});
	}
	public string SetMsg()
	{
		if (base.Request.QueryString["id"] != null)
		{
			return "更新";
		}
		return "添加";
	}
	protected void btnCrop_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < this.gvNeedNote.Rows.Count; i++)
		{
			CheckBox checkBox = this.gvNeedNote.Rows[i].FindControl("chkBox") as CheckBox;
			if (checkBox.Checked)
			{
				Label label = this.gvNeedNote.Rows[i].FindControl("labCrop") as Label;
				label.Text = this.hdfCropName.Value;
				label.ToolTip = this.hdfCropCode.Value;
			}
		}
	}
}


