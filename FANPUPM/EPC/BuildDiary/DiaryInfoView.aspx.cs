using ASP;
using cn.justwin.BLL;
using cn.justwin.opm.BuildDiary;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OPM_EPCM_BuildDiary_DiaryInfoView : NBasePage, IRequiresSessionState
{
	private BuildDiaryAction briefAction = new BuildDiaryAction();
	private string uid = string.Empty;
	public string imgStr = string.Empty;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["ic"] != null)
			{
				this.uid = base.Request["ic"].ToString();
			}
			this.InitPage(this.uid);
			this.InitPic();
			this.FileUpload1.RecordCode = this.uid;
		}
	}
	public void InitPage(string uid)
	{
		DataTable model = this.briefAction.GetModel(uid);
		this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
		if (model.Rows.Count > 0)
		{
			this.lblSN.Text = model.Rows[0]["SN"].ToString();
			this.lblPrjName.Text = model.Rows[0]["prjname"].ToString();
			this.lblBllProducer.Text = model.Rows[0]["Record"].ToString();
			this.lblSfgl.Text = model.Rows[0]["Sfgl"].ToString();
			this.lblJbr.Text = model.Rows[0]["Jbr"].ToString();
			this.lblRecord.Text = model.Rows[0]["Record"].ToString();
			this.lblAmWeather.Text = model.Rows[0]["AmWeather"].ToString();
			this.lblPmWeather.Text = model.Rows[0]["PmWeather"].ToString();
			if (model.Rows[0]["Fsrq"] != null)
			{
				this.lblFsrq.Text = System.Convert.ToDateTime(model.Rows[0]["Fsrq"]).ToString("yyyy-M-d");
			}
			if (model.Rows[0]["Bzrq"] != null)
			{
				this.lblBzrq.Text = System.Convert.ToDateTime(model.Rows[0]["Bzrq"]).ToString("yyyy-M-d");
			}
			this.lbl2Cemperature.Text = model.Rows[0]["Cemperature2"].ToString();
			this.lbl8Cemperature.Text = model.Rows[0]["Cemperature8"].ToString();
			this.lbl14Cemperature.Text = model.Rows[0]["Cemperature14"].ToString();
			this.lbl20Cemperature.Text = model.Rows[0]["Cemperature20"].ToString();
			this.lblPreview.Text = model.Rows[0]["Yjqk"].ToString();
			this.lblCheck.Text = model.Rows[0]["Ysqk"].ToString();
			this.lblDesignChange.Text = model.Rows[0]["Sjbg"].ToString();
			this.lblMaterials.Text = model.Rows[0]["Cljc"].ToString();
			this.lblTechnology.Text = model.Rows[0]["Jsjd"].ToString();
			this.lblMaterialSubmission.Text = model.Rows[0]["Clsj"].ToString();
			this.lblDataTransfer.Text = model.Rows[0]["Zljj"].ToString();
			this.lblExternalMeet.Text = model.Rows[0]["Wbhy"].ToString();
			this.lblSuperiorCheck.Text = model.Rows[0]["Sjjc"].ToString();
			this.lblSafeHand.Text = model.Rows[0]["Aqcl"].ToString();
			this.lblShyj.Text = model.Rows[0]["Shyj"].ToString();
			this.lblOtherSituation.Text = model.Rows[0]["Qtqk"].ToString();
			this.lblRemark.Text = model.Rows[0]["Remark"].ToString();
			this.lblAddUser.Text = PageHelper.QueryUser(this, model.Rows[0]["AddUser"].ToString());
			this.lblAddTime.Text = model.Rows[0]["AddTime"].ToString();
			if (model.Rows[0]["WaterElec"].ToString() != "0" || model.Rows[0]["Mason"].ToString() != "0" || model.Rows[0]["Painter"].ToString() != "0" || model.Rows[0]["Carpentry"].ToString() != "0")
			{
				this.workNum.Visible = true;
				this.workNum1.Visible = true;
				this.lblWaterElec.Text = ((model.Rows[0]["WaterElec"] == null) ? "" : model.Rows[0]["WaterElec"].ToString());
				this.lblMason.Text = ((model.Rows[0]["Mason"] == null) ? "" : model.Rows[0]["Mason"].ToString());
				this.lblPainter.Text = ((model.Rows[0]["Painter"] == null) ? "" : model.Rows[0]["Painter"].ToString());
				this.lblCarpentry.Text = ((model.Rows[0]["Carpentry"] == null) ? "" : model.Rows[0]["Carpentry"].ToString());
			}
			else
			{
				this.workNum.Visible = false;
				this.workNum1.Visible = false;
			}
		}
		this.BindGV(uid);
	}
	public void BindGV(string uid)
	{
		DataTable list = new BuildDiary_mxAction().GetList(uid);
		this.gvDiary_MX.DataSource = list;
		this.gvDiary_MX.DataBind();
	}
	public void InitPic()
	{
		string path = base.Server.MapPath(base.Request.ApplicationPath) + base.Request.ApplicationPath + this.FileUpload1.Folder + this.uid;
		if (System.IO.Directory.Exists(path))
		{
			string[] files = System.IO.Directory.GetFiles(path);
			string item = string.Empty;
			string text = string.Empty;
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string path2 = array[i];
				text = System.IO.Path.GetFileName(path2);
				item = string.Concat(new string[]
				{
					"../..",
					this.FileUpload1.Folder,
					this.uid,
					"/",
					text
				});
				list.Add(item);
			}
			string value = JsonHelper.JsonSerializer<System.Collections.Generic.List<string>>(list);
			this.hfldImgPath.Value = value;
		}
	}
}


