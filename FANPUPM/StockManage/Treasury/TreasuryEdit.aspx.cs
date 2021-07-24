using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Treasury_TreasuryEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string currentTcode = string.Empty;
	private readonly Treasury treasury = new Treasury();
	private SmEnum.SmSetValue manageMode;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["Tcode"]))
		{
			this.currentTcode = base.Request["Tcode"];
		}
		this.manageMode = this.treasury.GetManageMode();
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (!this.action.Equals("Add"))
			{
				TreasuryModel model = this.treasury.GetModel(this.currentTcode);
				this.tbtname.Text = model.tname;
				if (!string.IsNullOrEmpty(model.tflag))
				{
					this.rblTflag.SelectedIndex = Convert.ToInt32(model.tflag);
					if (model.tflag == "1")
					{
						this.txtProject.Disabled = true;
					}
				}
				this.tbtaddress.Text = model.taddress;
				this.tbtexplain.Text = model.texplain;
				if (!string.IsNullOrEmpty(model.PrjCode))
				{
					this.hdnProjectCode.Value = model.PrjCode;
					this.txtProject.Value = this.treasury.GetPrjName(this.hdnProjectCode.Value);
				}
				if (model.IsContrast.HasValue)
				{
					this.chkIsContrast.Checked = model.IsContrast.Value;
				}
				else
				{
					this.chkIsContrast.Checked = false;
				}
			}
			if (this.manageMode == SmEnum.SmSetValue.ParallelMode)
			{
				this.rblTflag.Enabled = false;
				this.trTflag.Visible = false;
				this.promptzf.Visible = false;
				return;
			}
			this.chkIsContrast.Enabled = false;
			this.tr2.Visible = false;
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (!this.action.Equals("Add"))
		{
			if (this.action.Equals("Update"))
			{
				if (string.IsNullOrEmpty(this.currentTcode))
				{
					base.RegisterScript("top.ui.alert('对不起添加失败！');");
					return;
				}
				TreasuryModel model = this.treasury.GetModel(this.currentTcode);
				model.tname = this.tbtname.Text.Trim();
				model.taddress = this.tbtaddress.Text.Trim();
				model.texplain = this.tbtexplain.Text.Trim();
				model.tflag = this.rblTflag.SelectedValue;
				model.PrjCode = this.hdnProjectCode.Value.ToString();
				if (!this.ValidateTreasury(model))
				{
					return;
				}
				if (this.chkIsContrast.Checked)
				{
					this.treasury.DelContrast();
					model.IsContrast = new bool?(this.chkIsContrast.Checked);
				}
				else
				{
					model.IsContrast = null;
				}
				int num = this.treasury.Update(model);
				StringBuilder stringBuilder = new StringBuilder();
				if (num == 1)
				{
					stringBuilder.Append("top.ui.show('修改成功！');").Append(Environment.NewLine);
					stringBuilder.Append("winclose('TreasuryEdit', 'Treasury.aspx?tcode=" + model.ptcode + "', true)");
				}
				else
				{
					stringBuilder.Append("top.ui.alert('修改失败！');");
				}
				base.RegisterScript(stringBuilder.ToString());
			}
			return;
		}
		if (string.IsNullOrEmpty(this.currentTcode))
		{
			base.RegisterScript("top.ui.alert('添加失败！');");
			return;
		}
		string tid = Guid.NewGuid().ToString();
		string tcode = this.treasury.CreateNewCode(this.currentTcode);
		string text = this.tbtname.Text.Trim();
		string taddress = this.tbtaddress.Text.Trim();
		string texplain = base.Server.HtmlEncode(this.tbtexplain.Text.Trim());
		string selectedValue = this.rblTflag.SelectedValue;
		string value = this.hdnProjectCode.Value;
		TreasuryModel treasuryModel;
		if (this.chkIsContrast.Checked)
		{
			treasuryModel = new TreasuryModel(tid, tcode, text, this.currentTcode, selectedValue, taddress, texplain, value, new bool?(true));
		}
		else
		{
			treasuryModel = new TreasuryModel(tid, tcode, text, this.currentTcode, selectedValue, taddress, texplain, value, null);
		}
		if (!this.ValidateTreasury(treasuryModel))
		{
			return;
		}
		if (treasuryModel.IsContrast == true)
		{
			this.treasury.DelContrast();
		}
		DataTable sameModel = this.treasury.GetSameModel(text);
		if (sameModel.Rows.Count > 0)
		{
			base.RegisterScript("top.ui.alert('此仓库名称已存在，请重新填写！');");
			return;
		}
		int num2 = this.treasury.Add(treasuryModel);
		StringBuilder stringBuilder2 = new StringBuilder();
		if (num2 == 1)
		{
			this.AddDefautPermit(treasuryModel.tcode);
			stringBuilder2.Append("top.ui.show('添加成功！');").Append(Environment.NewLine);
			stringBuilder2.Append("winclose('TreasuryEdit', 'Treasury.aspx?tcode=" + this.currentTcode + "', true)");
		}
		else
		{
			stringBuilder2.Append("top.ui.alert('添加失败！');").Append(Environment.NewLine);
		}
		base.RegisterScript(stringBuilder2.ToString());
	}
	private bool ValidateTreasury(TreasuryModel model)
	{
		if (StockParameter.DepotType == DepotType.TotalMode.ToString())
		{
			if (model.tflag == "1" && !string.IsNullOrEmpty(this.treasury.GetTotalCode()) && this.treasury.GetTotalCode() != model.tcode)
			{
				this.treasury.CancelTotal();
			}
			if (model.tflag == "0" && (string.IsNullOrEmpty(this.treasury.GetTotalCode()) || model.tcode == this.treasury.GetTotalCode()))
			{
				base.RegisterScript("top.ui.alert('总分模式下必须存在总库！');");
				return false;
			}
		}
		if (string.IsNullOrEmpty(model.tname))
		{
			base.RegisterScript("top.ui.alert('仓库名称不能为空！');");
			return false;
		}
		if (string.IsNullOrEmpty(model.taddress))
		{
			base.RegisterScript("top.ui.alert('仓库地址不能为空！');");
			return false;
		}
		return true;
	}
	protected void rblTflag_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (this.rblTflag.SelectedIndex == 1)
		{
			this.txtProject.Disabled = true;
			this.hdnProjectCode.Value = string.Empty;
			this.txtProject.Value = string.Empty;
			if (this.action.Equals("Add"))
			{
				if (this.treasury.GetTotalCode() != "")
				{
					this.tishi.Visible = true;
					this.lblTishi.Text = "【提示：总库已经存在，总分模式下总库只能有一个，点击保存后会将当前库设为总库】";
					return;
				}
			}
			else
			{
				TreasuryModel model = this.treasury.GetModel(this.currentTcode);
				if (this.treasury.GetTotalCode() != model.tcode)
				{
					this.tishi.Visible = true;
					this.lblTishi.Text = "【提示：总库已经存在，总分模式下总库只能有一个，点击保存后会将当前库设为总库】";
					return;
				}
			}
		}
		else
		{
			this.tishi.Visible = false;
			this.txtProject.Disabled = false;
		}
	}
	protected void AddDefautPermit(string tcode)
	{
		TreasuryPermit treasuryPermit = new TreasuryPermit();
		treasuryPermit.pcode = base.UserCode;
		treasuryPermit.ptype = "Person";
		treasuryPermit.tcode = tcode;
		treasuryPermit.tpid = Guid.NewGuid().ToString();
		TreasuryPermitBll treasuryPermitBll = new TreasuryPermitBll();
		treasuryPermitBll.AddTreasuryPermit(treasuryPermit);
	}
	protected void chkIsContrast_CheckedChanged(object sender, EventArgs e)
	{
		if (this.chkIsContrast.Checked)
		{
			if (this.treasury.GetParallel() != "")
			{
				this.tishi1.Visible = true;
				this.lblTishi1.Text = "【提示：对比库已经存在，平行模式下对比库只能有一个，点击保存后会将当前库设为对比库】";
				return;
			}
		}
		else
		{
			this.tishi1.Visible = false;
		}
	}
}


