using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_IndividualDrawApplyAssTabEdit : BasePage, IRequiresSessionState
{

	public OAOfficeResPersonalApplicationDetailAction amAction
	{
		get
		{
			return new OAOfficeResPersonalApplicationDetailAction();
		}
	}
	public int RecordID
	{
		get
		{
			object obj = this.ViewState["RECORDID"];
			if (obj != null)
			{
				return (int)this.ViewState["RECORDID"];
			}
			return 0;
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	public int InStorageID
	{
		get
		{
			object obj = this.ViewState["INSTORAGEID"];
			if (obj != null)
			{
				return (int)this.ViewState["INSTORAGEID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["INSTORAGEID"] = value;
		}
	}
	public string UsePerson
	{
		get
		{
			object obj = this.ViewState["USEPERSON"];
			if (obj != null)
			{
				return (string)this.ViewState["USEPERSON"];
			}
			return "";
		}
		set
		{
			this.ViewState["USEPERSON"] = value;
		}
	}
	public string OperateType
	{
		get
		{
			object obj = this.ViewState["OPERATETYPE"];
			if (obj != null)
			{
				return (string)this.ViewState["OPERATETYPE"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["OPERATETYPE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["rid"] == null || base.Request["id"] == null || base.Request["op"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;window.close();</script>");
			return;
		}
		if (!this.Page.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			this.OperateType = base.Request["op"].ToString().Trim();
			if (base.Request["rid"].ToString() != "")
			{
				this.RecordID = int.Parse(base.Request["rid"].ToString());
			}
			if (base.Request["id"].ToString() != "")
			{
				this.InStorageID = int.Parse(base.Request["id"].ToString());
			}
			this.UsePerson = base.Request["uc"].ToString();
			this.DisplayPersonRation();
			if (this.OperateType == "upd")
			{
				this.imgsel.Disabled = true;
				this.EditDisplay();
			}
		}
		this.txtNumber.Attributes["onblur"] = "javascript:IsInteger(this);";
	}
	private void DisplayPersonRation()
	{
		DataTable personInfo = OAOfficeCommonClas.GetPersonInfo(this.UsePerson);
		if (personInfo.Rows.Count > 0)
		{
			DataRow dataRow = personInfo.Rows[0];
			decimal d = OAOfficeCommonClas.GetPersonRation(dataRow["PostAndRank"].ToString());
			string personInfo2 = OAOfficeCommonClas.GetPersonInfo(this.UsePerson, "EnterCorpDate");
			DateTime dateTime = (personInfo2 == "") ? DateTime.Now : Convert.ToDateTime(personInfo2);
			if (dateTime.Year < DateTime.Now.Year)
			{
				d *= DateTime.Now.Month;
			}
			else
			{
				d *= DateTime.Now.Month - dateTime.Month + 1;
			}
			this.txtPersonRation.Text = d.ToString("f2");
		}
	}
	private void EditDisplay()
	{
		DataTable list = this.amAction.GetList("a.PADRecordID=" + this.RecordID);
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.txtResName.Text = dataRow["ResName"].ToString();
			this.txtUnit.Text = dataRow["Unit"].ToString();
			this.DDLUseType.SelectedValue = dataRow["UseType"].ToString();
			this.HdnMatterialID.Value = dataRow["MaterialID"].ToString();
			this.txtNumber.Text = dataRow["ApplyNumber"].ToString();
			this.txtInStoragePrice.Text = dataRow["PlanFee"].ToString();
			this.HdnPrice.Value = dataRow["PlanFee"].ToString();
		}
	}
	private OAOfficeResPersonalApplicationDetail GetData()
	{
		return new OAOfficeResPersonalApplicationDetail
		{
			IsPass = "1",
			MaterialID = int.Parse(this.HdnMatterialID.Value),
			PADRecordID = this.RecordID,
			PARecordID = this.InStorageID,
			ApplyNumber = (this.txtNumber.Text.Trim() == "") ? 0 : int.Parse(this.txtNumber.Text.Trim())
		};
	}
	private bool IsPass()
	{
		decimal d = (this.txtPersonRation.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtPersonRation.Text.Trim());
		string personInfo = OAOfficeCommonClas.GetPersonInfo(this.UsePerson, "EnterCorpDate");
		DateTime dateTime = (personInfo == "") ? DateTime.Now : Convert.ToDateTime(personInfo);
		string text;
		if (dateTime.Year < DateTime.Now.Year)
		{
			text = string.Concat(new object[]
			{
				"datediff(yyyy,'",
				DateTime.Now,
				"',a.ApplyDate)=0 and UseMan='",
				this.UsePerson,
				"'"
			});
		}
		else
		{
			text = string.Concat(new object[]
			{
				"datediff(yyyy,'",
				DateTime.Now,
				"',a.ApplyDate)=0 and datediff(mm,'",
				dateTime,
				"',a.ApplyDate)>=0 and UseMan='",
				this.UsePerson,
				"'"
			});
		}
		if (this.OperateType == "upd")
		{
			text = text + " and b.PADRecordID<>" + this.RecordID;
		}
		decimal d2 = this.amAction.GetApplyStat(text) + ((this.HdnPrice.Value.Trim() == "") ? 0m : Convert.ToDecimal(this.HdnPrice.Value.Trim())) * ((this.txtNumber.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtNumber.Text.Trim()));
		return d >= d2;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		if (this.IsPass())
		{
			OAOfficeResPersonalApplicationDetail data = this.GetData();
			if (this.OperateType == "add")
			{
				int num = this.amAction.Add(data);
				if (num > 0)
				{
					this.Page.RegisterStartupScript("", "<script>alert('添加成功!');returnValue=true;window.close();</script>");
				}
				else
				{
					this.Page.RegisterStartupScript("", "<script>alert('没有相关数据可添加!');</script>");
				}
			}
			if (this.OperateType == "upd")
			{
				int num = this.amAction.Update(data);
				if (num > 0)
				{
					this.Page.RegisterStartupScript("", "<script>alert('修改成功!');returnValue=true;window.close();</script>");
					return;
				}
				this.Page.RegisterStartupScript("", "<script>alert('没有相关数据可更新!');</script>");
				return;
			}
		}
		else
		{
			this.Page.RegisterStartupScript("", "<script>alert('超出定额，不允许保存!');</script>");
		}
	}
}


