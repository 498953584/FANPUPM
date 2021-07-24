using ASP;
using cn.justwin.BLL;
using cn.justwin.opm.BuildDiary;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OPM_BuildDiary_AddBuildDiary_MX : NBasePage, IRequiresSessionState
{
	private BuildDiary_mxAction bdmx = new BuildDiary_mxAction();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && base.Request["type"] != null)
		{
			if (base.Request["type"] == "add")
			{
				this.InitAdd();
				return;
			}
			if (base.Request["type"] == "edit")
			{
				this.InitUpdate(base.Request["mxid"].ToString());
				return;
			}
			if (base.Request["type"] == "query")
			{
				this.InitQuery(base.Request["mxid"].ToString());
			}
		}
	}
	public void InitAdd()
	{
		this.txtTaskCode.Text = string.Concat(new object[]
		{
			base.UserCode,
			System.DateTime.Now.Year,
			System.DateTime.Now.Month,
			System.DateTime.Now.Day
		});
	}
	public void InitUpdate(string uid)
	{
		this.BindControls(uid);
	}
	public void InitQuery(string uid)
	{
		this.txtTaskCode.Enabled = false;
		this.txtTaskName.Enabled = false;
		this.txtWorkGroup.Enabled = false;
		this.txtBuildPosition.Attributes.Add("readonly", "readonly");
		this.txtWorkersCount.Enabled = false;
		this.txtJdqk.Attributes.Add("readonly", "readonly");
		this.txtRemark.Attributes.Add("readonly", "readonly");
		this.btnSave.Style.Add("display", "none");
		this.btnCancel.Attributes.Add("value", "关闭");
		this.BindControls(uid);
	}
	public void BindControls(string uid)
	{
		DataTable model = this.bdmx.GetModel(uid);
		this.txtTaskCode.Text = model.Rows[0]["TaskCode"].ToString();
		this.txtTaskName.Text = model.Rows[0]["TaskName"].ToString();
		this.txtWorkGroup.Text = model.Rows[0]["WorkGroup"].ToString();
		this.txtWorkersCount.Text = model.Rows[0]["WorkersCount"].ToString();
		this.txtJdqk.Value = model.Rows[0]["Jdqk"].ToString();
		this.txtBuildPosition.Value = model.Rows[0]["BuildPosition"].ToString();
		this.txtRemark.Value = model.Rows[0]["Remark"].ToString();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		BuildDiary_mxModel buildDiary_mxModel = new BuildDiary_mxModel();
		if (base.Request["UID"] != null)
		{
			buildDiary_mxModel.BDID = base.Request["UID"].ToString();
			buildDiary_mxModel.TaskCode = this.txtTaskCode.Text;
			buildDiary_mxModel.TaskName = this.txtTaskName.Text;
			buildDiary_mxModel.WorkGroup = this.txtWorkGroup.Text;
			buildDiary_mxModel.WorkersCount = this.txtWorkersCount.Text;
			buildDiary_mxModel.BuildPosition = this.txtBuildPosition.Value;
			buildDiary_mxModel.Jdqk = this.txtJdqk.Value;
			buildDiary_mxModel.Remark = this.txtRemark.Value;
			if (base.Request["type"] == "add")
			{
				buildDiary_mxModel.UID = System.Guid.NewGuid().ToString();
				try
				{
					if (this.bdmx.Exists(base.Request["UID"].ToString(), this.txtTaskCode.Text).Rows.Count == 0)
					{
						this.bdmx.Add(buildDiary_mxModel);
						base.RegisterScript("top.ui.tabSuccess({ parentName: '_BuildDiary_MX' });");
					}
					else
					{
						base.RegisterScript("top.ui.alert('已经存在相同的任务代码，请先修改任务代码!')");
					}
					return;
				}
				catch
				{
					base.RegisterScript("top.ui.alert('添加失败！')");
					return;
				}
			}
			if (!(base.Request["type"] == "edit"))
			{
				return;
			}
			buildDiary_mxModel.UID = base.Request["mxid"].ToString();
			try
			{
				this.bdmx.Update(buildDiary_mxModel);
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_BuildDiary_MX' });");
				return;
			}
			catch
			{
				base.RegisterScript("top.ui.alert('更新失败！')");
				return;
			}
		}
		base.RegisterScript("top.ui.alert('请先选择施工日志！')");
	}
}


