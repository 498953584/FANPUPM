using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_PrjMilestoneEdit : NBasePage, IRequiresSessionState
{
	private PrjMilestoneService PrjMileSer = new PrjMilestoneService();
	private PTYhmcService ptyhSer = new PTYhmcService();
	private string type = string.Empty;
	private string Id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		this.type = base.Request["action"];
		this.Id = base.Request["id"];
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (this.type == "Add")
			{
				this.hlfUserCode.Value = base.UserCode;
				string name = this.ptyhSer.GetName(base.UserCode);
				this.txtUserName.Text = name;
				System.DateTime dateTime = System.DateTime.Today.AddDays((double)(1 - System.DateTime.Today.Day + 13));
				System.DateTime dateTime2 = System.DateTime.Today.AddDays((double)(1 - System.DateTime.Today.Day)).AddMonths(1).AddDays(-1.0);
				if (System.DateTime.Now <= dateTime)
				{
					this.hlfRptDate.Value = Common2.GetTime(dateTime);
					this.txtRptDate.Text = string.Concat(new object[]
					{
						dateTime.Year,
						"年-",
						dateTime.Month,
						"月-上半月"
					});
				}
				else
				{
					this.hlfRptDate.Value = Common2.GetTime(dateTime2);
					this.txtRptDate.Text = string.Concat(new object[]
					{
						dateTime2.Year,
						"年-",
						dateTime2.Month,
						"月-下半月"
					});
				}
				System.Collections.Generic.Dictionary<string, decimal> dictionary = this.PrjMileSer.PrjStateCount(name);
				this.txtSton1.Text = System.Convert.ToInt32(dictionary["prjZTb"]).ToString();
				this.txtSton2.Text = System.Convert.ToInt32(dictionary["sampling"]).ToString();
				this.txtSton4.Text = System.Convert.ToInt32(dictionary["prjTend"]).ToString();
				this.txtSton5.Text = System.Convert.ToInt32(dictionary["prjBind"]).ToString();
				this.txtSton6.Text = System.Convert.ToInt32(dictionary["submitOrder"]).ToString();
				this.txtSton7.Text = System.Convert.ToInt32(dictionary["delivery"]).ToString();
				this.txtSton8.Text = System.Convert.ToInt32(dictionary["sale"]).ToString();
				this.txtSton9.Text = System.Convert.ToInt32(dictionary["giveUp"]).ToString();
				this.txtStoreAmont.Text = dictionary["SumprjCost"].ToString();
				this.txtForeCast.Text = dictionary["yearPrjCost"].ToString();
				this.txtNextForeCast.Text = dictionary["nextPrjCost"].ToString();
				return;
			}
			if (this.type == "Edit")
			{
				this.Bindpage();
			}
		}
	}
	protected void Bindpage()
	{
		if (!string.IsNullOrEmpty(this.Id))
		{
			PrjMilestone byId = this.PrjMileSer.GetById(this.Id);
			if (byId != null)
			{
				this.hlfUserCode.Value = byId.UserCode;
				this.txtUserName.Text = this.ptyhSer.GetName(byId.UserCode);
				this.txtStoreAmont.Text = byId.StoreAmount.ToString();
				this.txtForeCast.Text = byId.ForeCast.ToString();
				this.txtNextForeCast.Text = byId.NextForeCast.ToString();
				System.DateTime rptDate = byId.RptDate;
				this.hlfRptDate.Value = Common2.GetTime(rptDate);
				if (rptDate.Day > System.DateTime.Today.AddDays((double)(1 - System.DateTime.Today.Day + 13)).Day)
				{
					this.txtRptDate.Text = string.Concat(new object[]
					{
						rptDate.Year,
						"年-",
						rptDate.Month,
						"月-下半月"
					});
				}
				else
				{
					this.txtRptDate.Text = string.Concat(new object[]
					{
						rptDate.Year,
						"年-",
						rptDate.Month,
						"月-上半月"
					});
				}
				this.txtSton1.Text = byId.Stone1.ToString();
				this.txtSton2.Text = byId.Stone2.ToString();
				this.txtSton3.Text = byId.Stone3.ToString();
				this.txtSton4.Text = byId.Stone4.ToString();
				this.txtSton5.Text = byId.Stone5.ToString();
				this.txtSton6.Text = byId.Stone6.ToString();
				this.txtSton7.Text = byId.Stone7.ToString();
				this.txtSton8.Text = byId.Stone8.ToString();
				this.txtSton9.Text = byId.Stone9.ToString();
			}
		}
	}
	protected PrjMilestone GetModel()
	{
		PrjMilestone prjMilestone = new PrjMilestone();
		prjMilestone.Id = (string.IsNullOrEmpty(this.Id) ? System.Guid.NewGuid().ToString() : this.Id);
		prjMilestone.RptDate = System.Convert.ToDateTime(this.hlfRptDate.Value.Trim());
		prjMilestone.UserCode = this.hlfUserCode.Value.Trim();
		decimal num = string.IsNullOrEmpty(this.txtStoreAmont.Text.Trim()) ? 0m : System.Convert.ToDecimal(this.txtStoreAmont.Text.Trim());
		prjMilestone.StoreAmount = num;
		decimal num2 = string.IsNullOrEmpty(this.txtForeCast.Text.Trim()) ? 0m : System.Convert.ToDecimal(this.txtForeCast.Text.Trim());
		prjMilestone.ForeCast = num2;
		prjMilestone.StoreSwitchRate = decimal.Divide(num2, num);
		decimal nextForeCast = string.IsNullOrEmpty(this.txtNextForeCast.Text.Trim()) ? (num - num2) : System.Convert.ToDecimal(this.txtNextForeCast.Text.Trim());
		prjMilestone.NextForeCast = nextForeCast;
		prjMilestone.Stone1 = (string.IsNullOrEmpty(this.txtSton1.Text.Trim()) ? 0 : System.Convert.ToInt32(this.txtSton1.Text.Trim()));
		prjMilestone.Stone2 = (string.IsNullOrEmpty(this.txtSton2.Text.Trim()) ? 0 : System.Convert.ToInt32(this.txtSton2.Text.Trim()));
		prjMilestone.Stone3 = (string.IsNullOrEmpty(this.txtSton3.Text.Trim()) ? 0 : System.Convert.ToInt32(this.txtSton3.Text.Trim()));
		prjMilestone.Stone4 = (string.IsNullOrEmpty(this.txtSton4.Text.Trim()) ? 0 : System.Convert.ToInt32(this.txtSton4.Text.Trim()));
		prjMilestone.Stone5 = (string.IsNullOrEmpty(this.txtSton5.Text.Trim()) ? 0 : System.Convert.ToInt32(this.txtSton5.Text.Trim()));
		prjMilestone.Stone6 = (string.IsNullOrEmpty(this.txtSton6.Text.Trim()) ? 0 : System.Convert.ToInt32(this.txtSton6.Text.Trim()));
		prjMilestone.Stone7 = (string.IsNullOrEmpty(this.txtSton7.Text.Trim()) ? 0 : System.Convert.ToInt32(this.txtSton7.Text.Trim()));
		prjMilestone.Stone8 = (string.IsNullOrEmpty(this.txtSton8.Text.Trim()) ? 0 : System.Convert.ToInt32(this.txtSton8.Text.Trim()));
		prjMilestone.Stone9 = (string.IsNullOrEmpty(this.txtSton9.Text.Trim()) ? 0 : System.Convert.ToInt32(this.txtSton9.Text.Trim()));
		return prjMilestone;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		PrjMilestone model = this.GetModel();
		if (this.IsExists(model))
		{
			string str = string.Empty;
			if (model.RptDate <= System.DateTime.Today.AddDays((double)(1 - System.DateTime.Today.Day + 13)))
			{
				str = "上半月数据已存在，请在下半月填写相关数据！";
			}
			else
			{
				str = "下半月数据已存在，请再下个月的上半月填写相关数据！";
			}
			base.RegisterScript("top.ui.alert('" + str + "');");
			return;
		}
		if (this.type == "Add")
		{
			this.PrjMileSer.Add(model);
		}
		else
		{
			if (this.type == "Edit")
			{
				this.PrjMileSer.Update(model);
			}
		}
		base.RegisterScript("top.ui.tabSuccess({parentName:'_PrjMilestoneList'});");
	}
	protected bool IsExists(PrjMilestone model)
	{
		bool result;
		if (this.type == "Add")
		{
			result = ((
				from p in this.PrjMileSer
				where p.UserCode == model.UserCode && p.RptDate == model.RptDate
				select p).Count<PrjMilestone>() > 0);
		}
		else
		{
			result = ((
				from p in this.PrjMileSer
				where p.UserCode == model.UserCode && p.RptDate == model.RptDate && p.Id != model.Id
				select p).Count<PrjMilestone>() > 0);
		}
		return result;
	}
}


