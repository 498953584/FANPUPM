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
	public partial class DatumAffirm1 : PageBase, IRequiresSessionState
	{
		protected string dc
		{
			get
			{
				return this.ViewState["DC"].ToString();
			}
			set
			{
				this.ViewState["DC"] = value;
			}
		}
		protected KnowledgeDataAction KD
		{
			get
			{
				return new KnowledgeDataAction();
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["DatumCode"] == null)
				{
					this.JS.Text = "alert('参数错误!');window.close();";
					return;
				}
				this.dc = base.Request["DatumCode"].ToString();
				KnowledgeDataModel singleValue = KnowledgeDataAction.GetSingleValue(new Guid(this.dc));
				this.CalDate.Text = singleValue.AffirmDateTime.ToShortDateString();
				this.TxtRemark.Text = singleValue.AffirmInfo.ToString();
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
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			if (this.KD.DatumAffirm(this.dc, this.CalDate.Text.ToString(), this.Session["yhdm"].ToString(), this.TxtRemark.Text.ToString()) == 1)
			{
				this.JS.Text = "alert('成功确认！');window.returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('确认失败！');";
		}
	}


