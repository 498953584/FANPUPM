using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SuperviseManage : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["prjcode"] != null)
				{
					this.TextBox_prjname.Text = this.GetPrjName(base.Request["prjcode"]);
					string cmdText = " select [StartDate] FROM [PT_PrjInfo] WHERE [PrjGuid]='" + base.Request.QueryString["prjcode"].ToString() + "'";
					SqlDataReader sqlDataReader = publicDbOpClass.ExecuteReader(CommandType.Text, cmdText, new SqlParameter[0]);
					if (sqlDataReader.Read())
					{
						this.DateBox_lxsj.Text = DateTime.Parse(sqlDataReader.GetValue(0).ToString()).ToShortDateString().Replace("/", "-");
					}
				}
				this.DateBox_jxsj.Text = DateTime.Now.ToString("d").Replace("/", "-");
				if (base.Request.Params["Type"] != null && base.Request.Params["Type"].ToString() == "View")
				{
					this.Button_save.Visible = false;
					this.Button1.Style.Add("display", "");
					this.Button1.Value = "关 闭";
					this.TextBox_lxdw.ReadOnly = true;
					this.TextBox_lxay.ReadOnly = true;
					this.TextBox_ldps.ReadOnly = true;
					this.TextBox_jcjy.ReadOnly = true;
					this.TextBox_zgcs.ReadOnly = true;
					this.TextBox_prjname.ReadOnly = true;
					this.TextBox_lxmc.ReadOnly = true;
					this.TextBox_jcxn.ReadOnly = true;
					this.DateBox_lxsj.Enabled = false;
					this.DateBox_jxsj.Enabled = false;
				}
				if (base.Request["pk"] != null)
				{
					this.bind(base.Request["pk"].ToString());
				}
				if (base.Request["readonly"] != null)
				{
					this.Button_save.Visible = false;
					this.Button1.Visible = false;
					for (int i = 0; i < this.Controls.Count; i++)
					{
						foreach (Control control in this.Controls[i].Controls)
						{
							if (control is TextBox)
							{
								(control as TextBox).ReadOnly = true;
							}
						}
					}
				}
			}
		}
		private void bind(string pk)
		{
			SuperviseInfo superviseInfo = SuperviseAction.GetSuperviseInfo(pk);
			this.TextBox_prjname.Text = superviseInfo.PrjName;
			this.TextBox_lxdw.Text = superviseInfo.StandNapeUnit;
			this.DateBox_lxsj.Text = superviseInfo.StandNapeDate.ToString("d").Replace("/", "-");
			this.TextBox_lxmc.Text = superviseInfo.StandNapeName;
			this.TextBox_lxay.Text = superviseInfo.StandItemRecord;
			switch (superviseInfo.ApproveResult)
			{
			case 0:
				this.TextBox_ldps.Text = "未通过";
				break;
			case 1:
				this.TextBox_ldps.Text = "通过";
				break;
			case 2:
				this.TextBox_ldps.Text = "未审核";
				break;
			}
			this.TextBox_jcjy.Text = superviseInfo.SuperviseAdvice;
			this.TextBox_zgcs.Text = superviseInfo.CompleteApply;
			this.DateBox_jxsj.Text = superviseInfo.KnotItemDate.ToString("d").Replace("/", "-");
			this.TextBox_jcxn.Text = superviseInfo.SuperviseEffciency;
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
			if (this.TextBox_jcjy.Text.Length > this.TextBox_jcjy.MaxLength)
			{
				this.JavaScriptControl1.Text = "alert('监察建议输入过长！');";
				return;
			}
			if (this.TextBox_zgcs.Text.Length > this.TextBox_zgcs.MaxLength)
			{
				this.JavaScriptControl1.Text = "alert('整改措施及效果输入过长！');";
				return;
			}
			SuperviseInfo superviseInfo = new SuperviseInfo();
			if (base.Request["pk"] != null)
			{
				superviseInfo.ID = int.Parse(base.Request["pk"].ToString());
			}
			superviseInfo.ApproveResult = 0;
			superviseInfo.StandNapeUnit = this.TextBox_lxdw.Text;
			superviseInfo.StandNapeDate = DateTime.Parse(this.DateBox_lxsj.Text);
			superviseInfo.StandNapeName = this.TextBox_lxmc.Text;
			superviseInfo.StandItemRecord = this.TextBox_lxay.Text;
			superviseInfo.SuperviseAdvice = this.TextBox_jcjy.Text;
			superviseInfo.CompleteApply = this.TextBox_zgcs.Text;
			superviseInfo.KnotItemDate = DateTime.Parse(this.DateBox_jxsj.Text);
			superviseInfo.SuperviseEffciency = this.TextBox_jcxn.Text;
			if (base.Request["Prjcode"] != null)
			{
				superviseInfo.PrjCode = base.Request["Prjcode"].ToString();
			}
			bool flag;
			if (base.Request["pk"] == null)
			{
				flag = SuperviseAction.SuperviseInfoOp(superviseInfo, "Insert");
			}
			else
			{
				flag = SuperviseAction.SuperviseInfoOp(superviseInfo, "Update");
			}
			if (flag)
			{
				base.RegisterScript(" top.ui.tabSuccess({ parentName: '_superviselist' });");
				return;
			}
			base.RegisterScript("top.ui.alert('保存失败！');");
		}
		protected string GetPrjName(string prjId)
		{
			string result = string.Empty;
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId = pTPrjInfoService.GetById(prjId);
			if (byId != null)
			{
				result = byId.PrjName;
			}
			return result;
		}
	}


