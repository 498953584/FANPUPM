using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CodeClassEdit : PageBase, IRequiresSessionState
	{
		protected string TypeID
		{
			get
			{
				return this.ViewState["TYPEID"].ToString();
			}
			set
			{
				this.ViewState["TYPEID"] = value;
			}
		}
		protected string type
		{
			get
			{
				return this.ViewState["TYPE"].ToString();
			}
			set
			{
				this.ViewState["TYPE"] = value;
			}
		}
		protected CodingAction ca
		{
			get
			{
				return new CodingAction();
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["ti"] == null || base.Request["t"] == null)
				{
					this.JS.Text = "alert('参数错误!');";
					return;
				}
				this.TypeID = base.Request["ti"].ToString();
				this.type = base.Request["t"].ToString();
				if (this.type == "2")
				{
					this.dgDataBind();
					this.TxtSignCode.Enabled = false;
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
		private void dgDataBind()
		{
			CodingType codingType = this.ca.QuerySingleCodingType(Convert.ToInt32(this.TypeID));
			this.TxtClassName.Text = codingType.TypeName.ToString();
			this.TxtRemark.Text = codingType.Remark;
			this.TxtSignCode.Text = codingType.SignCode;
		}
		private CodingType dgct()
		{
			CodingType codingType = new CodingType();
			codingType.TypeName = this.TxtClassName.Text.ToString();
			codingType.Remark = this.TxtRemark.Text.ToString();
			codingType.SignCode = this.TxtSignCode.Text.ToString();
			if (this.type == "2")
			{
				codingType.TypeID = Convert.ToInt32(this.TypeID);
			}
			return codingType;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			if (!(this.type == "1"))
			{
				if (this.type == "2")
				{
					if (this.ca.CodingTypeUp(this.dgct()) == 1)
					{
						this.JS.Text = "alert('保存成功！');window.returnValue = true;window.close();";
						return;
					}
					this.JS.Text = "alert('保存失败！');";
				}
				return;
			}
			if (this.ca.CodingTypeAdd(this.dgct()) == 1)
			{
				this.JS.Text = "alert('保存成功！');window.returnValue = true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败！');";
		}
	}


