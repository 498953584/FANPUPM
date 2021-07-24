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
	public partial class CostFrameAnalyzeEdit : BasePage, IRequiresSessionState
	{

		protected string NodeCode
		{
			get
			{
				object obj = this.ViewState["NODECODE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["NODECODE"] = value;
			}
		}
		protected bool IsHaveChild
		{
			get
			{
				object obj = this.ViewState["ISHAVECHILD"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ISHAVECHILD"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["nc"] == null)
			{
				this.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
				return;
			}
			this.NodeCode = base.Request["nc"].ToString();
			if (!this.Page.IsPostBack)
			{
				if (base.Request["t"].ToString().Equals("Add"))
				{
					string text = TemplatesAction.initCode(this.NodeCode);
					this.TxtCBSCode.Text = text;
				}
				if (base.Request["t"].ToString().Equals("Upd"))
				{
					Templates model = TemplatesAction.GetModel(this.NodeCode);
					this.TxtCBSCode.Text = model.TemplatesCode;
					this.TxtCostName.Text = model.TemplatesName;
				}
			}
		}
		private Templates GetCostApproveData()
		{
			Templates templates = new Templates();
			templates.TemplatesCode = this.TxtCBSCode.Text.ToString().Trim();
			templates.TemplatesName = this.TxtCostName.Text.ToString().Trim();
			templates.ParentCode = this.TxtCBSCode.Text.ToString().Substring(0, templates.TemplatesCode.Length - 3);
			return templates;
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
			Templates costApproveData = this.GetCostApproveData();
			if (base.Request["t"].ToString().Equals("Add"))
			{
				bool flag = TemplatesAction.Add(costApproveData);
				if (flag)
				{
					JavaScriptControl expr_38 = this.JS;
					expr_38.Text += "location.href='CostFrameAnalyze.aspx?type=manage';";
				}
				else
				{
					this.JS.Text = "alert('添加失败!');location.href='CostFrameAnalyze.aspx?type=manage';";
				}
			}
			if (base.Request["t"].ToString().Equals("Upd"))
			{
				bool flag2 = TemplatesAction.Update(costApproveData);
				if (flag2)
				{
					this.JS.Text = "location.href='CostFrameAnalyze.aspx?type=manage';";
					return;
				}
				this.JS.Text = "alert('编辑失败!');location.href='CostFrameAnalyze.aspx?type=manage';";
			}
		}
	}


