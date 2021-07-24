using ASP;
using cn.justwin.BLL;
using cn.justwin.epm.prog;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProgManage : NBasePage, IRequiresSessionState
	{
		private string _pk = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack && !string.IsNullOrEmpty(this._pk))
			{
				this.bind(this._pk);
			}
		}
		private void bind(string pk)
		{
			ProgSortInfo progSortInfo = ProgSortAction.GetProgSortInfo(pk);
			this.TextBox_name.Text = progSortInfo.ProgSortName;
			this.TextBox_remark.Text = progSortInfo.Remark;
			this.HiddenField1.Value = progSortInfo.ProgSortName;
		}
		protected override void OnInit(EventArgs e)
		{
			if (base.Request.QueryString["pk"] != null && base.Request.QueryString["pk"].ToString() != "")
			{
				this._pk = base.Request.QueryString["pk"].ToString();
			}
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void Button_save_Click(object sender, EventArgs e)
		{
			if (this.TextBox_remark.Text.Length > this.TextBox_remark.MaxLength)
			{
				this.JavaScriptControl1.Text = "alert('备注输入的长度过长！')";
				return;
			}
			ProgSortInfo progSortInfo = new ProgSortInfo();
			if (base.Request["pk"] != null)
			{
				progSortInfo.ProgSortCode = int.Parse(base.Request["pk"].ToString());
			}
			progSortInfo.ProgSortName = this.TextBox_name.Text;
			progSortInfo.Remark = this.TextBox_remark.Text;
			bool flag = false;
			progLogic progLogic = new progLogic();
			if (this._pk == "")
			{
				if (progLogic.Exists(progSortInfo.ProgSortName))
				{
					this.JavaScriptControl1.Text = "top.ui.alert('奖罚类别名称已存在！');";
					return;
				}
				flag = ProgSortAction.ProgSortInfoOp(progSortInfo, "Insert");
			}
			else
			{
				if (this._pk != "")
				{
					if (this.HiddenField1.Value.ToString() != progSortInfo.ProgSortName)
					{
						if (progLogic.Exists(progSortInfo.ProgSortName))
						{
							this.JavaScriptControl1.Text = "top.ui.alert('奖罚类别名称已存在！');";
							return;
						}
						flag = ProgSortAction.ProgSortInfoOp(progSortInfo, "Update");
					}
					else
					{
						flag = ProgSortAction.ProgSortInfoOp(progSortInfo, "Update");
					}
				}
			}
			if (flag)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_progsortlist' });");
				return;
			}
			base.RegisterScript("top.ui.alert('保存失败！');");
		}
	}


