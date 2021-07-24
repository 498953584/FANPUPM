using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Fund.MonthPlan;
using cn.justwin.stockBLL.Fund.MonthPlan;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_MonthPlan_MonthPlanEdit : NBasePage, IRequiresSessionState
{
	private MonthDetailLogic bll = new MonthDetailLogic();
	private Guid _TEM_MonthPlanID;
	private string _plantype = string.Empty;
	private string _tem_UID;

	public Guid TEM_MonthPlanID
	{
		get
		{
			if (base.Request.QueryString["mpid"] != null)
			{
				this._TEM_MonthPlanID = new Guid(base.Request.QueryString["mpid"].ToString());
			}
			return this._TEM_MonthPlanID;
		}
		set
		{
			this._TEM_MonthPlanID = value;
		}
	}
	public string Plantype
	{
		get
		{
			return this._plantype;
		}
		set
		{
			this._plantype = value;
		}
	}
	public string Tem_UID
	{
		get
		{
			return this._tem_UID;
		}
		set
		{
			this._tem_UID = value;
		}
	}
	protected override void OnInit(EventArgs e)
	{
		this.FileLink1.Type = 1;
		this.FileLink1.MID = 1755;
		this.FileLink1.FID = Guid.NewGuid().ToString();
		this.FileUpload1.RecordCode = Guid.NewGuid().ToString();
		if (base.Request.QueryString["plantype"] != null)
		{
			this.Plantype = base.Request.QueryString["plantype"].ToString();
		}
		if (base.Request.QueryString["UID"] != null && base.Request.QueryString["UID"].ToString() != "")
		{
			this.Tem_UID = base.Request.QueryString["UID"].ToString();
			this.Tem_UID = this.Tem_UID.Substring(1, this.Tem_UID.Length - 2);
			this.FileUpload1.RecordCode = this.Tem_UID;
		}
		else
		{
			if (this.Plantype == "payout")
			{
				this.txtPlansubject.Text = "合同支出";
			}
			else
			{
				if (this.Plantype == "income")
				{
					this.txtPlansubject.Text = "合同收入";
				}
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && base.Request.QueryString["UID"] != null && base.Request.QueryString["UID"].ToString() != "")
		{
			Guid gid = new Guid(this.Tem_UID);
			this.bindInitData(gid);
		}
	}
	private void bindInitData(Guid gid)
	{
		if (gid.ToString() != "")
		{
			MonthDetailModel monthDetailModel = new MonthDetailModel();
			monthDetailModel = this.bll.GetModel(gid);
			if (monthDetailModel != null)
			{
				this.txtPlansubject.Text = monthDetailModel.Plansubject;
				this.TextBox2.Text = monthDetailModel.ReMark;
				if (monthDetailModel.OldBalance.HasValue && monthDetailModel.OldBalance.ToString() != "")
				{
					this.txtOldBalance.Text = decimal.Parse(monthDetailModel.OldBalance.ToString()).ToString();
				}
				else
				{
					this.txtOldBalance.Text = "0.00";
				}
				if (monthDetailModel.PlanMoney.HasValue && monthDetailModel.PlanMoney.ToString() != "")
				{
					this.txtPlanMoney.Text = decimal.Parse(monthDetailModel.PlanMoney.ToString()).ToString();
				}
				else
				{
					this.txtPlanMoney.Text = "0.00";
				}
				this.hdfcontrcn.Value = monthDetailModel.ContractID;
				if (this.Plantype != "" && this.Plantype == "payout")
				{
					PayoutContract payoutContract = new PayoutContract();
					this.txtContr.Value = payoutContract.GetModel(monthDetailModel.ContractID).ContractName;
					return;
				}
				if (this.Plantype != "" && this.Plantype == "income")
				{
					DataTable table = Common2.GetTable("Con_Incomet_Contract", "ContractID='" + monthDetailModel.ContractID + "'");
					if (table.Rows.Count > 0 && table.Rows[0]["ContractName"] != null)
					{
						this.txtContr.Value = table.Rows[0]["ContractName"].ToString();
					}
				}
			}
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		MonthDetailModel monthDetailModel = new MonthDetailModel();
		monthDetailModel.ContractID = this.hdfcontrcn.Value;
		monthDetailModel.MonthPlanID = this.TEM_MonthPlanID;
		if (this.txtPlanMoney.Text.ToString() == "")
		{
			this.txtPlanMoney.Text = "0";
		}
		string text = this.txtOldBalance.Text.ToString();
		monthDetailModel.OldBalance = new decimal?(decimal.Parse((text == "") ? "0" : text));
		monthDetailModel.PlanMoney = new decimal?(decimal.Parse(this.txtPlanMoney.Text.ToString()));
		monthDetailModel.Plansubject = this.txtPlansubject.Text.ToString();
		monthDetailModel.ReMark = this.TextBox2.Text.ToString();
		monthDetailModel.OrderID = new int?(0);
		monthDetailModel.ThisBalance = new decimal?(0m);
		if (this.Tem_UID != null && this.Tem_UID != "")
		{
			monthDetailModel.UID = new Guid(this.Tem_UID);
			if (this.bll.Update(monthDetailModel, null))
			{
				stringBuilder.Append("parent.desktop.flowclass.location = parent.desktop.flowclass.location;");
				stringBuilder.Append("alert('保存成功');");
				stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "sc", "<script>" + stringBuilder.ToString() + "</script>");
				return;
			}
			base.ClientScript.RegisterClientScriptBlock(base.GetType(), "sc", "<script>alert('保存失败')</script>");
			return;
		}
		else
		{
			monthDetailModel.UID = new Guid(this.FileUpload1.RecordCode.ToString());
			if (this.bll.Add(monthDetailModel))
			{
				stringBuilder.Append("parent.desktop.flowclass.location = parent.desktop.flowclass.location;");
				stringBuilder.Append("alert('保存成功');");
				stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "sc", "<script>" + stringBuilder.ToString() + "</script>");
				return;
			}
			base.ClientScript.RegisterClientScriptBlock(base.GetType(), "sc", "<script>alert('保存失败')</script>");
			return;
		}
	}
}


