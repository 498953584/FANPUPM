using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBll;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_basicset_SetAlarmNum : NBasePage, IRequiresSessionState
{
	private AlarmNumBll alarmNumBll = new AlarmNumBll();
	private Treasury tresury = new Treasury();

	protected override void OnInit(System.EventArgs e)
	{
		this.gvNeed.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindNeedGv();
			this.SetTitle();
			this.delInfoCode();
		}
	}
	public void delInfoCode()
	{
		Common2.DelByStrWhere("Sm_AlarmNum", " where infoCode='0'");
	}
	public void SetTitle()
	{
		if (base.Request.QueryString["tcode"] != null)
		{
			TreasuryModel model = this.tresury.GetModel(base.Request.QueryString["tcode"]);
			if (model != null)
			{
				this.lblTitle.Text = model.tname;
			}
		}
	}
	public void BindNeedGv()
	{
		DataTable tableList = this.alarmNumBll.GetTableList(" where tcode='" + base.Request.QueryString["tcode"] + "'");
		if (tableList.Rows.Count == 0)
		{
			tableList.Rows.Add(tableList.NewRow());
			this.gvNeed.DataSource = tableList;
			this.gvNeed.DataBind();
			this.gvNeed.HeaderRow.Cells[0].Visible = false;
			this.gvNeed.Rows[0].Visible = false;
			return;
		}
		this.gvNeed.DataSource = tableList;
		this.gvNeed.DataBind();
	}
	protected void btnShowList_Click(object sender, System.EventArgs e)
	{
		this.Add();
		this.BindNeedGv();
	}
	protected void Add()
	{
		string[] array = JsonHelper.GetListFromJson(this.hfldResourceCode.Value).ToArray();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text = array2[i];
			AlarmNumModel alarmNumModel = new AlarmNumModel();
			alarmNumModel.AlarmNum = 0m;
			alarmNumModel.said = System.Guid.NewGuid().ToString();
			alarmNumModel.scode = text.Replace("'", "");
			alarmNumModel.settime = System.DateTime.Now;
			alarmNumModel.InfoCode = "0";
			alarmNumModel.tcode = base.Request.QueryString["tcode"];
			if (this.alarmNumBll.GetModel(alarmNumModel.scode, alarmNumModel.tcode) == null && !string.IsNullOrEmpty(text))
			{
				this.alarmNumBll.Add(alarmNumModel);
			}
		}
	}
	protected void Update()
	{
		foreach (GridViewRow gridViewRow in this.gvNeed.Rows)
		{
			TextBox textBox = gridViewRow.FindControl("txtAlarmNum") as TextBox;
			if (textBox != null && !string.IsNullOrEmpty(textBox.Text))
			{
				this.alarmNumBll.UpdateNum(textBox.ToolTip, base.Request.QueryString["tcode"], System.Convert.ToDecimal(textBox.Text));
			}
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		foreach (GridViewRow gridViewRow in this.gvNeed.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			if (checkBox != null && checkBox.Checked)
			{
				this.alarmNumBll.Delete(checkBox.ToolTip, base.Request.QueryString["tcode"]);
			}
		}
		this.BindNeedGv();
	}
	protected void gvNeed_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.Update();
		this.gvNeed.PageIndex = e.NewPageIndex;
		this.BindNeedGv();
	}
	protected void btnCanel_Click(object sender, System.EventArgs e)
	{
		this.Update();
		base.RegisterScript("alert('设置成功!');");
		this.BindNeedGv();
	}
}


