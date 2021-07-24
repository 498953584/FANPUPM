using ASP;
using cn.justwin.BLL;
using cn.justwin.opm.BuildDiary;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OPM_BuildDiary_AddBuildDiary : NBasePage, IRequiresSessionState
{
	private BuildDiaryAction bdAction = new BuildDiaryAction();
	private string diaryId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (base.Request["type"] == "add")
		{
			this.diaryId = System.Guid.NewGuid().ToString();
		}
		else
		{
			if (base.Request["type"] == "edit")
			{
				this.diaryId = base.Request["uid"].ToString();
			}
		}
		base.OnInit(e);
	}
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
				this.InitUpdate(base.Request["uid"].ToString());
			}
		}
	}
	public void InitAdd()
	{
		this.ViewState["diaryId"] = this.diaryId;
		this.FileUpload1.RecordCode = this.ViewState["diaryId"].ToString();
		this.txtSN.Value = string.Concat(new object[]
		{
			base.UserCode,
			System.DateTime.Now.Year,
			System.DateTime.Now.Month,
			System.DateTime.Now.Day
		});
		this.txtAddUser.Value = PageHelper.QueryUser(this, base.UserCode);
		this.txtAddTime.Text = System.DateTime.Now.ToString();
		this.txtRecord.Value = WebUtil.GetUserNames(this.Session["yhdm"].ToString());
		if (base.Request["pc"] != null)
		{
			this.hdnPrjID.Value = base.Request["pc"].ToString();
			string sqlString = "select prjname from pt_prjinfo where prjguid='" + this.hdnPrjID.Value + "'";
			using (DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString))
			{
				if (dataTable.Rows.Count > 0)
				{
					this.txtPrjName.Text = dataTable.Rows[0]["PrjName"].ToString();
				}
			}
		}
	}
	public void InitUpdate(string uid)
	{
		this.FileUpload1.RecordCode = uid;
		this.BindControls(uid);
	}
	public void BindControls(string uid)
	{
		DataTable model = this.bdAction.GetModel(uid);
		this.txtSN.Value = model.Rows[0]["SN"].ToString();
		this.txtPrjName.Text = model.Rows[0]["prjname"].ToString();
		this.hdnPrjID.Value = model.Rows[0]["PrjGuid"].ToString();
		this.ddlSfgl.SelectedValue = model.Rows[0]["Sfgl"].ToString();
		this.txtJbr.Value = model.Rows[0]["Jbr"].ToString();
		this.txtRecord.Value = model.Rows[0]["Record"].ToString();
		this.txtAmWeather.Value = model.Rows[0]["AmWeather"].ToString();
		this.txtPmWeather.Value = model.Rows[0]["PmWeather"].ToString();
		if (model.Rows[0]["Fsrq"] != null)
		{
			this.txtFsrq.Text = System.Convert.ToDateTime(model.Rows[0]["Fsrq"]).ToString("yyyy-M-d");
		}
		if (model.Rows[0]["Bzrq"] != null)
		{
			this.txtBzrq.Text = System.Convert.ToDateTime(model.Rows[0]["Bzrq"]).ToString("yyyy-M-d");
		}
		this.txt2Cemperature.Value = model.Rows[0]["Cemperature2"].ToString();
		this.txt8Cemperature.Value = model.Rows[0]["Cemperature8"].ToString();
		this.txt14Cemperature.Value = model.Rows[0]["Cemperature14"].ToString();
		this.txt20Cemperature.Value = model.Rows[0]["Cemperature20"].ToString();
		this.txtPreview.Value = model.Rows[0]["Yjqk"].ToString();
		this.txtCheck.Value = model.Rows[0]["Ysqk"].ToString();
		this.txtDesignChange.Value = model.Rows[0]["Sjbg"].ToString();
		this.txtMaterials.Value = model.Rows[0]["Cljc"].ToString();
		this.txtTechnology.Value = model.Rows[0]["Jsjd"].ToString();
		this.txtMaterialSubmission.Value = model.Rows[0]["Clsj"].ToString();
		this.txtDataTransfer.Value = model.Rows[0]["Zljj"].ToString();
		this.txtExternalMeet.Value = model.Rows[0]["Wbhy"].ToString();
		this.txtSuperiorCheck.Value = model.Rows[0]["Sjjc"].ToString();
		this.txtSafeHand.Value = model.Rows[0]["Aqcl"].ToString();
		this.txtShyj.Value = model.Rows[0]["Shyj"].ToString();
		this.txtOtherSituation.Value = model.Rows[0]["Qtqk"].ToString();
		this.txtRemark.Value = model.Rows[0]["Remark"].ToString();
		this.txtAddUser.Value = PageHelper.QueryUser(this, model.Rows[0]["AddUser"].ToString());
		this.txtAddTime.Text = model.Rows[0]["AddTime"].ToString();
		if (model.Rows[0]["WaterElec"].ToString() != "0" || model.Rows[0]["Mason"].ToString() != "0" || model.Rows[0]["Painter"].ToString() != "0" || model.Rows[0]["Carpentry"].ToString() != "0")
		{
			this.hfldShowWorkNum.Value = "true";
			this.txtWaterElec.Value = ((model.Rows[0]["WaterElec"] == null) ? "" : model.Rows[0]["WaterElec"].ToString());
			this.txtMason.Value = ((model.Rows[0]["Mason"] == null) ? "" : model.Rows[0]["Mason"].ToString());
			this.txtPainter.Value = ((model.Rows[0]["Painter"] == null) ? "" : model.Rows[0]["Painter"].ToString());
			this.txtCarpentry.Value = ((model.Rows[0]["Carpentry"] == null) ? "" : model.Rows[0]["Carpentry"].ToString());
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (base.Request["type"] != null)
		{
			BuildDiaryModel buildDiaryModel = new BuildDiaryModel();
			buildDiaryModel.SN = this.txtSN.Value;
			buildDiaryModel.IsValid = this.ddlSfgl.SelectedValue;
			buildDiaryModel.FlowState = -1;
			buildDiaryModel.PrjID = this.hdnPrjID.Value;
			buildDiaryModel.Jbr = this.txtJbr.Value;
			buildDiaryModel.Record = this.txtRecord.Value;
			buildDiaryModel.AmWeather = this.txtAmWeather.Value;
			buildDiaryModel.PmWeather = this.txtPmWeather.Value;
			buildDiaryModel.Fsrq = ((this.txtFsrq.Text != "") ? System.Convert.ToDateTime(this.txtFsrq.Text) : System.DateTime.Now);
			buildDiaryModel.Bzrq = ((this.txtBzrq.Text != "") ? System.Convert.ToDateTime(this.txtBzrq.Text) : System.DateTime.Now);
			buildDiaryModel.Cemperature2 = this.txt2Cemperature.Value;
			buildDiaryModel.Cemperature8 = this.txt8Cemperature.Value;
			buildDiaryModel.Cemperature14 = this.txt14Cemperature.Value;
			buildDiaryModel.Cemperature20 = this.txt20Cemperature.Value;
			buildDiaryModel.Yjqk = this.txtPreview.Value;
			buildDiaryModel.Ysqk = this.txtCheck.Value;
			buildDiaryModel.Sjbg = this.txtDesignChange.Value;
			buildDiaryModel.Cljc = this.txtMaterials.Value;
			buildDiaryModel.Zljj = this.txtDataTransfer.Value;
			buildDiaryModel.Jsjd = this.txtTechnology.Value;
			buildDiaryModel.Clsj = this.txtMaterialSubmission.Value;
			buildDiaryModel.Wbhy = this.txtExternalMeet.Value;
			buildDiaryModel.Sjjc = this.txtSuperiorCheck.Value;
			buildDiaryModel.Aqcl = this.txtSafeHand.Value;
			buildDiaryModel.Shyj = this.txtShyj.Value;
			buildDiaryModel.Qtqk = this.txtOtherSituation.Value;
			buildDiaryModel.Remark = this.txtRemark.Value;
			buildDiaryModel.AddUser = base.UserCode;
			buildDiaryModel.AddTime = ((this.txtAddTime.Text != "") ? System.Convert.ToDateTime(this.txtAddTime.Text) : System.DateTime.Now);
			buildDiaryModel.WaterElec = ((this.txtWaterElec.Value.Trim() == "") ? 0 : System.Convert.ToInt32(this.txtWaterElec.Value.Trim()));
			buildDiaryModel.Mason = ((this.txtMason.Value.Trim() == "") ? 0 : System.Convert.ToInt32(this.txtMason.Value.Trim()));
			buildDiaryModel.Painter = ((this.txtPainter.Value.Trim() == "") ? 0 : System.Convert.ToInt32(this.txtPainter.Value.Trim()));
			buildDiaryModel.Carpentry = ((this.txtCarpentry.Value.Trim() == "") ? 0 : System.Convert.ToInt32(this.txtCarpentry.Value.Trim()));
			if (base.Request["type"] == "add")
			{
				buildDiaryModel.UID = this.ViewState["diaryId"].ToString();
				if (this.bdAction.Exists(this.txtSN.Value).Rows.Count <= 0)
				{
					try
					{
						this.bdAction.Add(buildDiaryModel);
						base.RegisterScript("top.ui.tabSuccess({ parentName: '_BuildDiaryList' });");
						return;
					}
					catch (System.Exception)
					{
						base.RegisterScript("top.ui.alert('添加失败');");
						return;
					}
				}
				base.RegisterScript("top.ui.alert('系统提示：\\n\\n已经存在相同编号的施工日志，请修改日志编号!');");
				return;
			}
			if (base.Request["type"] == "edit" && base.Request["uid"] != null)
			{
				buildDiaryModel.UID = base.Request["uid"].ToString();
				try
				{
					this.bdAction.Update(buildDiaryModel);
					base.RegisterScript(" top.ui.tabSuccess({ parentName: '_BuildDiaryList' });");
				}
				catch (System.Exception ex)
				{
					this.txtPreview.Value = ex.ToString();
					base.RegisterScript("top.ui.alert('系统提示：\\n\\n更新失败!');");
				}
			}
		}
	}
}


