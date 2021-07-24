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

		protected CBSAction theCBS
		{
			get
			{
				return new CBSAction();
			}
		}
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
		protected string CostType
		{
			get
			{
				object obj = this.ViewState["COSTTYPE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["COSTTYPE"] = value;
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
			if (base.Request["nc"] == null || base.Request["t"] == null)
			{
				this.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
				return;
			}
			this.NodeCode = base.Request["nc"].ToString();
			base.DealType = (OpType)Enum.Parse(typeof(OpType), base.Request["t"].ToString());
			if (base.Request["tf"] != null && base.Request["tf"].ToString() != "")
			{
				this.IsHaveChild = (base.Request["tf"].ToString() == "true");
			}
			if (!this.Page.IsPostBack)
			{
				if (base.Request["ct"] != null && base.Request["ct"].ToString() != "")
				{
					this.DDLCostType.SelectedValue = base.Request["ct"].ToString();
				}
				if (base.DealType == OpType.Add)
				{
					if (this.NodeCode.Length != 3 && this.NodeCode.Length != 6)
					{
						this.DDLCostType.Enabled = true;
					}
					string text = this.theCBS.InitCBSCode(this.NodeCode);
					this.TxtCBSCode.Text = text;
				}
				if (base.DealType == OpType.Upd)
				{
					if (!this.IsHaveChild && this.NodeCode.Length == 3)
					{
						this.DDLCostType.Enabled = true;
					}
					CBSFeeNodeInfo cBSNodeByCode = this.theCBS.GetCBSNodeByCode(this.NodeCode);
					this.TxtCBSCode.Text = cBSNodeByCode.NodeCode;
					this.TxtCostName.Text = cBSNodeByCode.NodeName;
					this.TxtRemark.Text = cBSNodeByCode.Remark;
					try
					{
						this.DDLCostType.SelectedValue = cBSNodeByCode.CostType.ToString();
					}
					catch
					{
					}
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
		private CBSFeeNodeInfo GetCostApproveData()
		{
			return new CBSFeeNodeInfo
			{
				NodeCode = this.TxtCBSCode.Text.Trim(),
				NodeName = this.TxtCostName.Text.Trim(),
				Remark = this.TxtRemark.Text.Trim(),
				CostType = this.DDLCostType.SelectedValue,
				NodeLayer = this.TxtCBSCode.Text.Trim().Length / 3,
				IsValid = 1
			};
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			CBSFeeNodeInfo costApproveData = this.GetCostApproveData();
			if (base.DealType == OpType.Add)
			{
				int num = this.theCBS.AddCBSNodeInfo(costApproveData);
				if (num == 1)
				{
					this.JS.Text = "alert('添加成功!');";
					JavaScriptControl expr_37 = this.JS;
					expr_37.Text += "location.href='CostFrameAnalyze.aspx';";
				}
				else
				{
					this.JS.Text = "alert('添加失败!');location.href='CostFrameAnalyze.aspx';";
				}
			}
			if (base.DealType == OpType.Upd)
			{
				int num2 = this.theCBS.UpdateCBSNodeInfo(costApproveData);
				if (num2 == 1)
				{
					this.JS.Text = "alert('编辑成功!');location.href='CostFrameAnalyze.aspx';";
					return;
				}
				this.JS.Text = "alert('编辑失败!');location.href='CostFrameAnalyze.aspx';";
			}
		}
	}


