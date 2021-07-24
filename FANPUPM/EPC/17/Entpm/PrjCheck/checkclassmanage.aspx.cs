using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CheckClassManage : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack && base.Request["pk"] != null)
			{
				this.bind(base.Request["pk"].ToString());
			}
		}
		private void bind(string pk)
		{
			CheckClassInfo checkClassInfo = CheckClassAction.GetCheckClassInfo(pk);
			this.TextBox_name.Text = checkClassInfo.ItemInspectSortName;
			this.TextBox_remark.Text = checkClassInfo.Remark;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void Button_save_Click(object sender, EventArgs e)
		{
			if (this.TextBox_remark.Text.Trim().Length > 150)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.TextBox_name.Text.Trim()))
			{
				this.TextBox_name.Focus();
				this.JavaScriptControl1.Text = "alert('系统提示：\\n检查类别名称不能为空!')";
				return;
			}
			CheckClassInfo checkClassInfo = new CheckClassInfo();
			if (base.Request["pk"] != null)
			{
				checkClassInfo.SortID = int.Parse(base.Request["pk"].ToString());
			}
			checkClassInfo.ItemInspectSortName = this.TextBox_name.Text.Trim();
			checkClassInfo.Remark = this.TextBox_remark.Text.Trim();
			bool flag = this.IsExistItemInspectSortName(checkClassInfo.ItemInspectSortName);
			bool flag2;
			if (string.IsNullOrEmpty(base.Request["pk"]))
			{
				if (flag)
				{
					this.TextBox_name.Focus();
					this.JavaScriptControl1.Text = "top.ui.alert('n检查类别名已经存在，请重新输入!')";
					return;
				}
				flag2 = CheckClassAction.CheckClassInfoOp(checkClassInfo, "Insert");
			}
			else
			{
				if (flag)
				{
					CheckClassInfo checkClassInfo2 = CheckClassAction.GetCheckClassInfo(base.Request["pk"]);
					if (!checkClassInfo2.ItemInspectSortName.Equals(checkClassInfo.ItemInspectSortName))
					{
						base.RegisterScript("top.ui.alert('检查类别名已经存在，请重新输入!')");
						return;
					}
				}
				flag2 = CheckClassAction.CheckClassInfoOp(checkClassInfo, "Update");
			}
			if (flag2)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_checkclasslist' });");
				return;
			}
			base.RegisterScript("top.ui.alert('保存失败')");
		}
		protected bool IsExistItemInspectSortName(string itemInspectSortName)
		{
			string strwhere = "ItemInspectSortName='" + itemInspectSortName + "'";
			DataTable checkClassCollections = CheckClassAction.GetCheckClassCollections(strwhere);
			return checkClassCollections.Rows.Count != 0;
		}
	}


