using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.RequestPayment.BLL;
using cn.justwin.Fund.RequestPayment.Model;
using cn.justwin.Serialize;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_RequestPayment_RequestPaymentEdit : NBasePage, IRequiresSessionState
{
	private RequestPayMain mainBll = new RequestPayMain();
	private RequestPayDetail detailBill = new RequestPayDetail();
	private RequestPayDetailModel detailModel;
	private RequestPayMainModel mainModel;
	private Fund_Plan_MonthMainAction PlanAction = new Fund_Plan_MonthMainAction();

	protected void Page_Load(object sender, EventArgs e)
	{
		this.hdnPrjguid.Value = base.Request.QueryString["prjId"].ToString();
		this.ifSelectFromPlan.Attributes.Add("src ", "SelectPlan.aspx?prjguid=" + this.hdnPrjguid.Value + "&type=payout");
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["action"].ToString() == "add")
			{
				this.txtRPayCode.Text = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				this.hdnCode.Value = Guid.NewGuid().ToString();
				this.hdnPerson.Value = base.UserCode;
				this.txtPerson.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, base.UserCode);
				this.DateInTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.gvwPlan.DataSource = null;
				this.gvwPlan.DataBind();
			}
			else
			{
				this.hdnCode.Value = base.Request.QueryString["ID"].ToString();
				this.mainModel = this.mainBll.GetModel(new Guid(this.hdnCode.Value.ToString()));
				this.txtRPayCode.Text = this.mainModel.RPayCode.ToString();
				this.txtPerson.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.mainModel.RPayUserCode.ToString());
				this.hdnPerson.Value = this.mainModel.RPayUserCode.ToString();
				this.DateInTime.Text = this.mainModel.RPayDate.ToString();
				this.txtExplain.Text = this.mainModel.ReMark.ToString();
				this.txtRPayCode.Enabled = false;
				DataTable list = this.detailBill.GetList(" RpayMainId='" + this.mainModel.RPayMainID + "'");
				list.Columns.Add(new DataColumn("Surplus"));
				for (int i = 0; i < list.Rows.Count; i++)
				{
					list.Rows[i]["Surplus"] = (Convert.ToDecimal(list.Rows[i]["ThisBalance"]) - Convert.ToDecimal(list.Rows[i]["AllowMoney"])).ToString();
				}
				this.ViewState["PlanTable"] = list;
				this.PlanTableBind();
			}
			this.txtprjName.Text = base.Request.QueryString["prjname"].ToString();
			this.flAnnx.MID = 1023;
			this.flAnnx.FID = this.hdnCode.Value.ToString();
			this.flAnnx.Type = 1;
		}
	}
	protected void gvwPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			Label label = (Label)e.Row.Cells[5].FindControl("lblBalance");
			label.Text.ToString();
			if (Convert.ToDecimal(label.Text.ToString()) <= 0m)
			{
				e.Row.Cells[5].Style.Add("Color", "red");
			}
			Label label2 = (Label)e.Row.Cells[2].FindControl("lblPlanName");
			TextBox textBox = (TextBox)e.Row.Cells[3].FindControl("txtCont");
			if (label2.Text.Trim() == "")
			{
				textBox.Attributes.Add("ondblclick", "selectContr(this)");
			}
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		RequestPayMainModel model = this.getModel();
		this.detailBill.Delete(model.RPayMainID);
		if (!(base.Request.QueryString["action"].ToString() == "add"))
		{
			this.mainBll.Update(model);
			this.SavePlanDetails();
			stringBuilder.Append("alert('系统提示：\\n\\n保存成功！');");
			stringBuilder.Append("winclose('RequestPaymentEdit', 'RequestPayment.aspx?prjGuid=" + base.Request.QueryString["prjId"].ToString() + "', true)");
			base.RegisterScript(stringBuilder.ToString());
			return;
		}
		if (this.mainBll.Exists(model.RPayCode))
		{
			stringBuilder.Append("alert('系统提示：\\n\\n请款单号重复！');");
			base.RegisterScript(stringBuilder.ToString());
			return;
		}
		this.mainBll.Add(model);
		this.SavePlanDetails();
		stringBuilder.Append("alert('系统提示：\\n\\n保存成功！');");
		stringBuilder.Append("winclose('RequestPaymentEdit', 'RequestPayment.aspx?prjGuid=" + base.Request.QueryString["prjId"].ToString() + "', true)");
		base.RegisterScript(stringBuilder.ToString());
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		this.SaveEditData();
		DataTable dataTable = this.ViewState["PlanTable"] as DataTable;
		foreach (GridViewRow gridViewRow in this.gvwPlan.Rows)
		{
			Label label = (Label)gridViewRow.Cells[9].FindControl("lbluid");
			if (((CheckBox)gridViewRow.Cells[0].FindControl("chkSelectSingle")).Checked)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					if (dataRow["RPayUID"].ToString() == label.Text.ToString())
					{
						dataTable.Rows.Remove(dataRow);
						break;
					}
				}
			}
		}
		this.ViewState["PlanTable"] = dataTable;
		this.PlanTableBind();
	}
	protected void btnPlan_Click(object sender, EventArgs e)
	{
		string value = this.hdnUID.Value;
		ISerializable serializable = new JsonSerializer();
		string[] planUid;
		if (value.StartsWith("["))
		{
			planUid = serializable.Deserialize<string[]>(value);
		}
		else
		{
			planUid = new string[]
			{
				value
			};
		}
		DataTable tableByPlan = this.PlanAction.getTableByPlan(planUid);
		DataTable dataTable = new DataTable();
		if (this.ViewState["PlanTable"] == null)
		{
			dataTable.Columns.Add(new DataColumn("RPayMainID"));
			dataTable.Columns.Add(new DataColumn("RPayUID"));
			dataTable.Columns.Add(new DataColumn("PlanUID"));
			dataTable.Columns.Add(new DataColumn("ContractID"));
			dataTable.Columns.Add(new DataColumn("RPaysubject"));
			dataTable.Columns.Add(new DataColumn("ReMark"));
			dataTable.Columns.Add(new DataColumn("RPayUser"));
			dataTable.Columns.Add(new DataColumn("RPayMoney"));
			dataTable.Columns.Add(new DataColumn("PlanName"));
			dataTable.Columns.Add(new DataColumn("ThisBalance"));
			dataTable.Columns.Add(new DataColumn("contractOutName"));
			dataTable.Columns.Add(new DataColumn("AllowMoney"));
			dataTable.Columns.Add(new DataColumn("Surplus"));
		}
		else
		{
			dataTable = (this.ViewState["PlanTable"] as DataTable);
		}
		for (int i = 0; i < tableByPlan.Rows.Count; i++)
		{
			bool flag = false;
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				string a = tableByPlan.Rows[i]["UID"].ToString();
				string b = dataTable.Rows[j]["PlanUID"].ToString();
				if (a == b)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["RPayMainID"] = this.hdnCode.Value.ToString();
				dataRow["RPayUID"] = Guid.NewGuid().ToString();
				dataRow["PlanUID"] = tableByPlan.Rows[i]["UID"];
				dataRow["ContractID"] = tableByPlan.Rows[i]["ContractId"];
				dataRow["RPaysubject"] = tableByPlan.Rows[i]["plansubject"];
				dataRow["ReMark"] = "";
				dataRow["RPayUser"] = "管理员";
				dataRow["RPayMoney"] = "0.000";
				dataRow["PlanName"] = tableByPlan.Rows[i]["PlanName"];
				dataRow["ThisBalance"] = tableByPlan.Rows[i]["ThisBalance"];
				dataRow["contractOutName"] = tableByPlan.Rows[i]["contractOutName"];
				dataRow["AllowMoney"] = tableByPlan.Rows[i]["RPayMoney"];
				dataRow["Surplus"] = (Convert.ToDecimal(tableByPlan.Rows[i]["ThisBalance"]) - Convert.ToDecimal(tableByPlan.Rows[i]["RPayMoney"])).ToString();
				dataTable.Rows.Add(dataRow);
			}
		}
		this.ViewState["PlanTable"] = dataTable;
		this.SaveEditData();
		this.PlanTableBind();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		DataTable dataTable = new DataTable();
		if (this.ViewState["PlanTable"] == null)
		{
			dataTable.Columns.Add(new DataColumn("RPayMainID"));
			dataTable.Columns.Add(new DataColumn("RPayUID"));
			dataTable.Columns.Add(new DataColumn("PlanUID"));
			dataTable.Columns.Add(new DataColumn("ContractID"));
			dataTable.Columns.Add(new DataColumn("RPaysubject"));
			dataTable.Columns.Add(new DataColumn("ReMark"));
			dataTable.Columns.Add(new DataColumn("RPayUser"));
			dataTable.Columns.Add(new DataColumn("RPayMoney"));
			dataTable.Columns.Add(new DataColumn("PlanName"));
			dataTable.Columns.Add(new DataColumn("ThisBalance"));
			dataTable.Columns.Add(new DataColumn("contractOutName"));
			dataTable.Columns.Add(new DataColumn("AllowMoney"));
			dataTable.Columns.Add(new DataColumn("Surplus"));
		}
		else
		{
			dataTable = (this.ViewState["PlanTable"] as DataTable);
		}
		DataRow dataRow = dataTable.NewRow();
		dataRow["RPayMainID"] = this.hdnCode.Value.ToString();
		dataRow["RPayUID"] = Guid.NewGuid().ToString();
		dataRow["RPayUser"] = "管理员";
		dataRow["RPayMoney"] = "0.000";
		dataRow["ThisBalance"] = "0.000";
		dataRow["ContractID"] = "";
		dataRow["AllowMoney"] = "0.000";
		dataRow["Surplus"] = "0.000";
		dataTable.Rows.Add(dataRow);
		this.ViewState["PlanTable"] = dataTable;
		this.SaveEditData();
		this.PlanTableBind();
	}
	protected void SaveEditData()
	{
		new StringBuilder();
		for (int i = 0; i < this.gvwPlan.Rows.Count; i++)
		{
			GridViewRow gridViewRow = this.gvwPlan.Rows[i];
			DataTable dataTable = this.ViewState["PlanTable"] as DataTable;
			DataRow dataRow = dataTable.Rows[gridViewRow.RowIndex];
			TextBox textBox = gridViewRow.Cells[3].FindControl("txtCont") as TextBox;
			dataRow["contractOutName"] = textBox.Text.ToString();
			HiddenField hiddenField = gridViewRow.Cells[3].FindControl("hdnCont") as HiddenField;
			dataRow["ContractID"] = hiddenField.Value.ToString();
			TextBox textBox2 = gridViewRow.Cells[4].FindControl("txtsubject") as TextBox;
			dataRow["RPaysubject"] = textBox2.Text.ToString();
			TextBox textBox3 = gridViewRow.Cells[6].FindControl("txtRPMoney") as TextBox;
			dataRow["RPayMoney"] = textBox3.Text.ToString();
			TextBox textBox4 = gridViewRow.Cells[7].FindControl("txtPerson") as TextBox;
			dataRow["RPayUser"] = textBox4.Text.ToString();
			TextBox textBox5 = gridViewRow.Cells[8].FindControl("txtremark") as TextBox;
			dataRow["ReMark"] = textBox5.Text.ToString();
			this.ViewState["PlanTable"] = dataTable;
			hiddenField.Value = "";
		}
	}
	protected void PlanTableBind()
	{
		if (this.ViewState["PlanTable"] is DataTable)
		{
			DataTable dataTable = this.ViewState["PlanTable"] as DataTable;
			if (dataTable.Rows.Count > 0)
			{
				this.gvwPlan.DataSource = dataTable;
				this.gvwPlan.DataBind();
				return;
			}
			this.gvwPlan.DataSource = dataTable;
			this.gvwPlan.DataBind();
		}
	}
	protected RequestPayMainModel getModel()
	{
		this.mainModel = new RequestPayMainModel();
		this.mainModel.RPayMainID = new Guid(this.hdnCode.Value.ToString());
		this.mainModel.RPayCode = this.txtRPayCode.Text.ToString();
		this.mainModel.PrjGuid = new Guid(base.Request.QueryString["prjId"].ToString());
		this.mainModel.RPayDate = new DateTime?(Convert.ToDateTime(this.DateInTime.Text.ToString()));
		this.mainModel.ReMark = this.txtExplain.Text.ToString();
		this.mainModel.RPayUserCode = this.hdnPerson.Value.ToString();
		if (base.Request.QueryString["action"].ToString() == "add")
		{
			this.mainModel.FlowState = new int?(-1);
		}
		return this.mainModel;
	}
	protected void SavePlanDetails()
	{
		this.SaveEditData();
		DataTable dataTable = new DataTable();
		if (this.ViewState["PlanTable"] != null)
		{
			dataTable = (this.ViewState["PlanTable"] as DataTable);
		}
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			this.detailModel = new RequestPayDetailModel();
			if (dataTable.Rows[i]["ContractID"].ToString() != "" && dataTable.Rows[i]["ContractID"] != null)
			{
				this.detailModel.ContractID = dataTable.Rows[i]["ContractID"].ToString();
			}
			else
			{
				this.detailModel.ContractID = "";
			}
			if (dataTable.Rows[i]["PlanUID"] != null && dataTable.Rows[i]["PlanUID"].ToString() != "")
			{
				this.detailModel.PlanUID = new Guid(dataTable.Rows[i]["PlanUID"].ToString());
			}
			this.detailModel.ReMark = dataTable.Rows[i]["ReMark"].ToString();
			this.detailModel.RPayMainID = new Guid(dataTable.Rows[i]["RPayMainID"].ToString());
			this.detailModel.RPayMoney = new decimal?(Convert.ToDecimal(dataTable.Rows[i]["RPayMoney"].ToString()));
			this.detailModel.RPaysubject = dataTable.Rows[i]["RPaysubject"].ToString();
			this.detailModel.RPayUser = dataTable.Rows[i]["RPayUser"].ToString();
			this.detailModel.RPayUID = new Guid(dataTable.Rows[i]["RPayUID"].ToString());
			this.detailModel.RPayCause = "";
			this.detailModel.OrderID = 0;
			this.detailModel.isInterest = false;
			if (this.detailBill.Exists(this.detailModel.RPayUID))
			{
				this.detailBill.Update(this.detailModel);
			}
			else
			{
				this.detailBill.Add(this.detailModel);
			}
		}
	}
}


