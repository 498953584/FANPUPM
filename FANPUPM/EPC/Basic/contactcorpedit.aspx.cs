using ASP;
using cn.justwin.DAL;
using cn.justwin.XPMBasicContactCorp;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ContactCorpEdit : PageBase, IRequiresSessionState
	{
		private publicDbOpClass sqlcc = new publicDbOpClass();
		private StringBuilder sqlstr = new StringBuilder();
		private BasicContactCorp Cont = new BasicContactCorp();

		public int CorpID
		{
			get
			{
				object obj = this.ViewState["CORPID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["CORPID"] = value;
			}
		}
		public int TypeID
		{
			get
			{
				object obj = this.ViewState["TYPEID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TYPEID"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!base.IsPostBack)
			{
				string sqlString = "select codeid,CodeName from dbo.XPM_Basic_CodeList where typeId=4 and IsValid=1 and IsVisible=1";
				DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
				this.ddlCorpType.DataSource = dataSource;
				this.ddlCorpType.DataValueField = "codeid";
				this.ddlCorpType.DataTextField = "CodeName";
				this.ddlCorpType.DataBind();
				this.ddlCorpType.Items.Insert(0, "请选择类别");
				if (base.Request["t"] != null || base.Request["ci"] != null || base.Request["cti"] != null)
				{
					base.DealType = (OpType)Enum.Parse(typeof(OpType), base.Request["t"]);
					this.CorpID = Convert.ToInt32(base.Request["ci"]);
					this.TypeID = Convert.ToInt32(base.Request["cti"]);
				}
				this.Custom_PageInit();
				if (base.DealType == OpType.Upd)
				{
					this.CorpInfoBind();
				}
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void Custom_PageInit()
		{
            com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTCorpKind, Convert.ToInt32(BasicType.CorpKind), true);
            com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTFareSort, Convert.ToInt32(BasicType.FareSort), true);
		}
		private void CorpInfoBind()
		{
			BasicContactCorpModel basicContactCorpModel = new BasicContactCorpModel();
			basicContactCorpModel = this.Cont.GetModel(this.CorpID);
			this.TxtCorpName.Text = basicContactCorpModel.CorpName.ToString();
			this.TxtCorporation.Text = basicContactCorpModel.Corporation.ToString();
            com.jwsoft.pm.entpm.PageHelper.SetDropDownTreeSelected(this.DDTCorpKind, basicContactCorpModel.CorpKind.ToString());
            com.jwsoft.pm.entpm.PageHelper.SetDropDownTreeSelected(this.DDTFareSort, basicContactCorpModel.WorkType.ToString());
			this.TxtAptitude.Text = basicContactCorpModel.Aptitude.ToString();
			this.TxtSpeciality.Text = basicContactCorpModel.Speciality.ToString();
			this.TxtLinkMan.Text = basicContactCorpModel.LinkMan.ToString();
			this.TxtTelephone.Text = basicContactCorpModel.Telephone.ToString();
			this.TxtHandPhone.Text = basicContactCorpModel.HandPhone.ToString();
			this.TxtFax.Text = basicContactCorpModel.Fax.ToString();
			this.TxtShopCard.Text = basicContactCorpModel.ShopCard.ToString();
			this.TxtTaxCard.Text = basicContactCorpModel.TaxCard.ToString();
			this.TxtBankAccounts.Text = basicContactCorpModel.BankAccounts.ToString();
			this.TxtAccountBank.Text = basicContactCorpModel.AccountBank.ToString();
			this.TxtAddress.Text = basicContactCorpModel.Address.ToString();
			this.TxtCapital.Text = basicContactCorpModel.Capital.ToString();
			this.TxtUnderlayAbility.Text = basicContactCorpModel.UnderlayAbility.ToString();
			this.TxtPostCode.Text = basicContactCorpModel.PostCode.ToString();
			this.TxtWebSite.Text = basicContactCorpModel.WebSite.ToString();
			this.TxtClient.Text = basicContactCorpModel.Client.ToString();
			this.TxtCorpBrief.Text = basicContactCorpModel.CorpBrief.ToString();
			this.ddlCorpType.SelectedValue = basicContactCorpModel.CorpTypeID.ToString();
			if (basicContactCorpModel.IsHot.ToString().Trim() == "Y")
			{
				this.IsHot.Checked = true;
			}
			else
			{
				this.IsHot.Checked = false;
			}
			DataTable newAddCorpInfor = this.GetNewAddCorpInfor(this.CorpID.ToString());
			if (newAddCorpInfor != null && newAddCorpInfor.Rows.Count > 0)
			{
				this.txtBrand.Text = newAddCorpInfor.Rows[0][0].ToString();
				this.txtEmail.Text = newAddCorpInfor.Rows[0][1].ToString();
				this.txtContry.Text = newAddCorpInfor.Rows[0][2].ToString();
			}
		}
		private BasicContactCorpModel CorpInfo()
		{
			BasicContactCorpModel basicContactCorpModel = new BasicContactCorpModel();
			if (this.CorpID != 0)
			{
				BasicContactCorpModel basicContactCorpModel2 = new BasicContactCorpModel();
				basicContactCorpModel2 = this.Cont.GetModel(this.CorpID);
				basicContactCorpModel.CorpID = Convert.ToInt32(this.CorpID);
				basicContactCorpModel.FlowGuid = basicContactCorpModel2.FlowGuid;
				basicContactCorpModel.IsDefault = basicContactCorpModel2.IsDefault;
				basicContactCorpModel.IsFixed = basicContactCorpModel2.IsFixed;
				basicContactCorpModel.IsValid = basicContactCorpModel2.IsValid;
				basicContactCorpModel.IsVisible = basicContactCorpModel2.IsVisible;
			}
			else
			{
				basicContactCorpModel.FlowGuid = Guid.NewGuid();
				basicContactCorpModel.IsDefault = true;
				basicContactCorpModel.IsFixed = true;
				basicContactCorpModel.IsValid = true;
				basicContactCorpModel.IsVisible = true;
			}
			basicContactCorpModel.Owner = "000000";
			if (this.ddlCorpType.SelectedIndex != 0)
			{
				basicContactCorpModel.CorpTypeID = Convert.ToInt32(this.ddlCorpType.SelectedValue);
			}
			basicContactCorpModel.CorpName = this.TxtCorpName.Text.Trim();
			basicContactCorpModel.Corporation = this.TxtCorporation.Text.Trim();
			if (!string.IsNullOrEmpty(this.DDTCorpKind.SelectedValue))
			{
				basicContactCorpModel.CorpKind = new int?(Convert.ToInt32(this.DDTCorpKind.SelectedValue));
			}
			basicContactCorpModel.Aptitude = this.TxtAptitude.Text.Trim();
			basicContactCorpModel.Speciality = this.TxtSpeciality.Text.Trim();
			basicContactCorpModel.LinkMan = this.TxtLinkMan.Text.Trim();
			basicContactCorpModel.Telephone = this.TxtTelephone.Text.Trim();
			basicContactCorpModel.HandPhone = this.TxtHandPhone.Text.Trim();
			basicContactCorpModel.Fax = this.TxtFax.Text.Trim();
			basicContactCorpModel.ShopCard = this.TxtShopCard.Text.Trim();
			basicContactCorpModel.TaxCard = this.TxtTaxCard.Text.Trim();
			basicContactCorpModel.BankAccounts = this.TxtBankAccounts.Text.Trim();
			basicContactCorpModel.AccountBank = this.TxtAccountBank.Text.Trim();
			basicContactCorpModel.Address = this.TxtAddress.Text.Trim();
			basicContactCorpModel.Capital = this.TxtCapital.Text.Trim();
			basicContactCorpModel.UnderlayAbility = this.TxtUnderlayAbility.Text.Trim();
			basicContactCorpModel.PostCode = this.TxtPostCode.Text.Trim();
			basicContactCorpModel.WebSite = this.TxtWebSite.Text.Trim();
			basicContactCorpModel.Client = this.TxtClient.Text.Trim();
			if (!string.IsNullOrEmpty(this.DDTFareSort.SelectedValue))
			{
				basicContactCorpModel.WorkType = new int?(Convert.ToInt32(this.DDTFareSort.SelectedValue));
			}
			basicContactCorpModel.CorpBrief = this.TxtCorpBrief.Text.Trim();
			basicContactCorpModel.UserCode = base.UserCode;
			basicContactCorpModel.VersionTime = new DateTime?(DateTime.Now);
			if (this.IsHot.Checked)
			{
				basicContactCorpModel.IsHot = "Y";
			}
			else
			{
				basicContactCorpModel.IsHot = "N";
			}
			return basicContactCorpModel;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			BasicContactCorpModel basicContactCorpModel = this.CorpInfo();
			if (this.ddlCorpType.SelectedIndex == 0)
			{
				this.JS.Text = "alert('请选择单位类型');";
				return;
			}
			if (this.TxtCorpName.Text.Contains("'"))
			{
				this.JS.Text = "alert('单位名称不能输入单引号');";
				return;
			}
			if (base.DealType != OpType.Add)
			{
				if (base.DealType == OpType.Upd)
				{
					this.sqlstr.Append("update XPM_Basic_ContactCorp set ");
					this.sqlstr.Append("CorpTypeID=" + basicContactCorpModel.CorpTypeID + ",");
					this.sqlstr.Append("CorpName='" + basicContactCorpModel.CorpName + "',");
					this.sqlstr.Append("CorpKind=" + basicContactCorpModel.CorpKind + ",");
					this.sqlstr.Append("WorkType=" + basicContactCorpModel.WorkType + ",");
					this.sqlstr.Append("Speciality='" + basicContactCorpModel.Speciality + "',");
					this.sqlstr.Append("Aptitude='" + basicContactCorpModel.Aptitude + "',");
					this.sqlstr.Append("Capital='" + basicContactCorpModel.Capital + "',");
					this.sqlstr.Append("UnderlayAbility='" + basicContactCorpModel.UnderlayAbility + "',");
					this.sqlstr.Append("Address='" + basicContactCorpModel.Address + "',");
					this.sqlstr.Append("CorpBrief='" + basicContactCorpModel.CorpBrief + "',");
					this.sqlstr.Append("Corporation='" + basicContactCorpModel.Corporation + "',");
					this.sqlstr.Append("LinkMan='" + basicContactCorpModel.LinkMan + "',");
					this.sqlstr.Append("Telephone='" + basicContactCorpModel.Telephone + "',");
					this.sqlstr.Append("IFProvider='" + basicContactCorpModel.IFProvider + "',");
					this.sqlstr.Append("HandPhone='" + basicContactCorpModel.HandPhone + "',");
					this.sqlstr.Append("Fax='" + basicContactCorpModel.Fax + "',");
					this.sqlstr.Append("ShopCard='" + basicContactCorpModel.ShopCard + "',");
					this.sqlstr.Append("TaxCard='" + basicContactCorpModel.TaxCard + "',");
					this.sqlstr.Append("AccountBank='" + basicContactCorpModel.AccountBank + "',");
					this.sqlstr.Append("BankAccounts='" + basicContactCorpModel.BankAccounts + "',");
					this.sqlstr.Append("Zone='" + basicContactCorpModel.Zone + "',");
					this.sqlstr.Append("PostCode='" + basicContactCorpModel.PostCode + "',");
					this.sqlstr.Append("WebSite='" + basicContactCorpModel.WebSite + "',");
					this.sqlstr.Append("PeopleNumber='" + basicContactCorpModel.PeopleNumber + "',");
					this.sqlstr.Append("Client='" + basicContactCorpModel.Client + "',");
					this.sqlstr.Append("IsVisible='" + basicContactCorpModel.IsVisible + "',");
					this.sqlstr.Append("IsDefault='" + basicContactCorpModel.IsDefault + "',");
					this.sqlstr.Append("IsFixed='" + basicContactCorpModel.IsFixed + "',");
					this.sqlstr.Append("IsValid='" + basicContactCorpModel.IsValid + "',");
					this.sqlstr.Append("Owner='" + basicContactCorpModel.Owner + "',");
					this.sqlstr.Append("FlowGuid='" + basicContactCorpModel.FlowGuid + "',");
					this.sqlstr.Append("AuditState=" + basicContactCorpModel.AuditState + ",");
					this.sqlstr.Append("UserCode='" + basicContactCorpModel.UserCode + "',");
					this.sqlstr.Append("Brand='" + basicContactCorpModel.Brand + "',");
					this.sqlstr.Append("Email='" + basicContactCorpModel.Email + "',");
					this.sqlstr.Append("Contry='" + basicContactCorpModel.Contry + "',");
					this.sqlstr.Append("IsHot='" + basicContactCorpModel.IsHot + "'");
					this.sqlstr.Append(" where CorpID=" + basicContactCorpModel.CorpID);
					if (SqlHelper.ExecuteNonQuery(CommandType.Text, this.sqlstr.ToString(), null) > 0)
					{
						this.UpdateNewAddCorpInfor(basicContactCorpModel.CorpName);
						this.JS.Text = "top.ui.tabSuccess({ parentName: '_contractcorplist' });";
						return;
					}
					this.JS.Text = "top.ui.alert('保存失败');";
				}
				return;
			}
			if (this.Cont.ContactCorpcount(basicContactCorpModel) > 0)
			{
				this.JS.Text = "alert('你要保存的单位已存在或状态未激活,请更名后保存或通知管理员激活');";
				return;
			}
			this.sqlstr.Append("insert into XPM_Basic_ContactCorp(");
			this.sqlstr.Append("CorpTypeID,CorpName,CorpKind,WorkType,Speciality,");
			this.sqlstr.Append(" Aptitude,Capital,UnderlayAbility,Address,CorpBrief,Corporation,LinkMan,Telephone,");
			this.sqlstr.Append("IFProvider,HandPhone,Fax,ShopCard,TaxCard,AccountBank,BankAccounts,Zone,PostCode,");
			this.sqlstr.Append(" WebSite,PeopleNumber,Client,IsVisible,IsDefault,IsFixed,IsValid,Owner,VersionTime,FlowGuid,AuditState,UserCode,Brand,Email,Contry,IsHot)");
			this.sqlstr.Append(" values (");
			this.sqlstr.Append(string.Concat(new object[]
			{
				basicContactCorpModel.CorpTypeID,
				",'",
				basicContactCorpModel.CorpName,
				"','",
				basicContactCorpModel.CorpKind,
				"','",
				basicContactCorpModel.WorkType,
				"','",
				basicContactCorpModel.Speciality,
				"',"
			}));
			this.sqlstr.Append(string.Concat(new string[]
			{
				" '",
				basicContactCorpModel.Aptitude,
				"','",
				basicContactCorpModel.Capital,
				"','",
				basicContactCorpModel.UnderlayAbility,
				"','",
				basicContactCorpModel.Address,
				"','",
				basicContactCorpModel.CorpBrief,
				"','",
				basicContactCorpModel.Corporation,
				"','",
				basicContactCorpModel.LinkMan,
				"','",
				basicContactCorpModel.Telephone,
				"',"
			}));
			this.sqlstr.Append(string.Concat(new object[]
			{
				"'",
				basicContactCorpModel.IFProvider,
				"','",
				basicContactCorpModel.HandPhone,
				"','",
				basicContactCorpModel.Fax,
				"','",
				basicContactCorpModel.ShopCard,
				"','",
				basicContactCorpModel.TaxCard,
				"','",
				basicContactCorpModel.AccountBank,
				"','",
				basicContactCorpModel.BankAccounts,
				"','",
				basicContactCorpModel.Zone,
				"','",
				basicContactCorpModel.PostCode,
				"',"
			}));
			this.sqlstr.Append(string.Concat(new object[]
			{
				"'",
				basicContactCorpModel.WebSite,
				"','",
				basicContactCorpModel.PeopleNumber,
				"','",
				basicContactCorpModel.Client,
				"','",
				basicContactCorpModel.IsVisible,
				"','",
				basicContactCorpModel.IsDefault,
				"','",
				basicContactCorpModel.IsFixed,
				"','",
				basicContactCorpModel.IsValid,
				"','",
				basicContactCorpModel.Owner,
				"','",
				basicContactCorpModel.VersionTime,
				"',"
			}));
			this.sqlstr.Append(string.Concat(new object[]
			{
				"'",
				basicContactCorpModel.FlowGuid,
				"',",
				basicContactCorpModel.AuditState,
				",'",
				basicContactCorpModel.UserCode,
				"','",
				basicContactCorpModel.Brand,
				"','",
				basicContactCorpModel.Email,
				"','",
				basicContactCorpModel.Contry,
				"','",
				basicContactCorpModel.IsHot,
				"')"
			}));
			int num = SqlHelper.ExecuteNonQuery(CommandType.Text, this.sqlstr.ToString(), null);
			if (num > 0)
			{
				this.UpdateNewAddCorpInfor(basicContactCorpModel.CorpName);
				this.JS.Text = "top.ui.tabSuccess({ parentName: '_contractcorplist' });";
				return;
			}
			this.JS.Text = "top.ui.alert('保存失败');";
		}
		protected void UpdateNewAddCorpInfor(string corpName)
		{
			if (!string.IsNullOrEmpty(corpName))
			{
				string sqlString = string.Concat(new string[]
				{
					"update XPM_Basic_ContactCorp set Brand='",
					this.txtBrand.Text.Trim(),
					"',Email='",
					this.txtEmail.Text.Trim(),
					"',Contry='",
					this.txtContry.Text.Trim(),
					"' where CorpName='",
					corpName,
					"' "
				});
				publicDbOpClass.ExecSqlString(sqlString);
			}
		}
		protected DataTable GetNewAddCorpInfor(string corpId)
		{
			DataTable result = new DataTable();
			if (!string.IsNullOrEmpty(corpId))
			{
				string sqlString = " select Brand,Email,Contry from XPM_Basic_ContactCorp where CorpId='" + corpId + "' ";
				result = publicDbOpClass.DataTableQuary(sqlString);
			}
			return result;
		}
	}


