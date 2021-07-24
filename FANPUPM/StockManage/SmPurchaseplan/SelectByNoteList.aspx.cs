using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_SmPurchaseplan_SelectByNoteList : NBasePage, IRequiresSessionState
{
	private PTPrjInfoBll ptPrjInfoBll = new PTPrjInfoBll();
	private MaterialWantPlan materialWantPlan = new MaterialWantPlan();
	private MeterialPlanStock meterialPlanStock = new MeterialPlanStock();

	protected override void OnInit(EventArgs e)
	{
		this.gvNeed.PageSize = NBasePage.pagesize3;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.SetTitle();
			this.ViewState["wscode"] = "";
			this.BindNeedGv();
			this.hdc.Value = string.Concat(1);
			this.hdPcode.Value = "0";
		}
	}
	public void SetTitle()
	{
		if (base.Request.QueryString["prjGuid"] != null)
		{
			PrjInfoModel modelByPrjGuid = this.ptPrjInfoBll.GetModelByPrjGuid(base.Request.QueryString["prjGuid"]);
			this.lblTitle.Text = modelByPrjGuid.PrjName;
		}
	}
	public void BindNeedGv()
	{
		DataTable dataTable = this.materialWantPlan.WantPlanDataBind("");
		if (base.Request.QueryString["prjGuid"] != null)
		{
			string procode = base.Request.QueryString["prjGuid"];
			DateTime startDate = Common2.GetStartDate(this.txtStartTime);
			DateTime dateTime = Common2.GetEndDate(this.txtEndTime);
			if (!string.IsNullOrEmpty(this.txtEndTime.Text))
			{
				dateTime = Common2.GetEndDate(this.txtEndTime).AddDays(1.0);
			}
			if (startDate == DateTime.MinValue || dateTime == DateTime.MinValue)
			{
				base.RegisterScript("top.ui.alert('时间格式错误！')");
				return;
			}
			dataTable = this.materialWantPlan.Select(new DateTime?(startDate), new DateTime?(dateTime), this.txtSwcode.Text.Trim(), "", procode, "1");
		}
		if (dataTable.Rows.Count == 0)
		{
			dataTable.Rows.Add(dataTable.NewRow());
			this.gvNeed.DataSource = dataTable;
			this.gvNeed.DataBind();
			this.gvNeed.HeaderRow.Cells[0].Visible = false;
			this.gvNeed.Rows[0].Visible = false;
		}
		else
		{
			this.gvNeed.DataSource = dataTable;
			this.gvNeed.DataBind();
		}
		foreach (GridViewRow gridViewRow in this.gvNeed.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			if (checkBox != null)
			{
				string[] array = this.hdwscode.Value.Split(new char[]
				{
					','
				});
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					string[] array3 = text.Split(new char[]
					{
						'|'
					});
					if (array3[0] == base.Request.QueryString["prjGuid"])
					{
						string[] array4 = array3;
						for (int j = 0; j < array4.Length; j++)
						{
							string b = array4[j];
							if (checkBox.ToolTip == b)
							{
								checkBox.Checked = true;
							}
						}
					}
				}
			}
		}
	}
	public void BindNeedNote()
	{
		string text = "";
		string[] array = this.hdwscode.Value.Split(new char[]
		{
			','
		});
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text2 = array2[i];
			string[] array3 = text2.Split(new char[]
			{
				'|'
			});
			for (int j = 0; j < array3.Length; j++)
			{
				if (j != 0 && !string.IsNullOrEmpty(array3[j]))
				{
					text = text + "'" + array3[j] + "',";
				}
			}
		}
		if (text.Length > 0)
		{
			text = text.Substring(0, text.Length - 1);
		}
		else
		{
			text = "''";
		}
		this.hdVal.Value = text;
		DataTable listArrayByWpcodenew = this.meterialPlanStock.GetListArrayByWpcodenew(text);
		if (listArrayByWpcodenew.Rows.Count == 0)
		{
			listArrayByWpcodenew.Rows.Add(listArrayByWpcodenew.NewRow());
			this.gvNeedNote.DataSource = listArrayByWpcodenew;
			this.gvNeedNote.DataBind();
			this.gvNeedNote.Rows[0].Visible = false;
			return;
		}
		this.gvNeedNote.DataSource = listArrayByWpcodenew;
		this.gvNeedNote.DataBind();
	}
	protected void chkBox_CheckedChanged(object sender, EventArgs e)
	{
		if (this.hdPcode.Value == "0")
		{
			this.cbClick();
		}
	}
	private void cbClick()
	{
		string text = "";
		List<string> list = new List<string>();
		foreach (GridViewRow gridViewRow in this.gvNeed.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			if (checkBox != null && checkBox.Checked)
			{
				text = text + checkBox.ToolTip + "|";
				list.Add(checkBox.ToolTip);
			}
			if (checkBox != null && !checkBox.Checked)
			{
				this.DelHdwscode(checkBox.ToolTip);
			}
		}
		string str = base.Request.QueryString["prjGuid"] + "|" + text + ",";
		if (!this.hdwscode.Value.Contains(base.Request.QueryString["prjGuid"]))
		{
			HiddenField expr_F2 = this.hdwscode;
			expr_F2.Value += str;
		}
		else
		{
			string text2 = base.Request.QueryString["prjGuid"];
			int startIndex = this.hdwscode.Value.IndexOf(text2, 0) + text2.Length + 1;
			this.hdwscode.Value = this.hdwscode.Value.Insert(startIndex, text);
		}
		string value;
		if (list.Count > 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string current in list)
			{
				stringBuilder.Append("','").Append(current);
			}
			stringBuilder.Append("'");
			value = stringBuilder.ToString().Substring(2);
		}
		else
		{
			value = "''";
		}
		this.hdlfWPcodes.Value = value;
		this.BindNeedNote();
	}
	public void DelHdwscode(string ids)
	{
		string[] array = this.hdwscode.Value.Split(new char[]
		{
			','
		});
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text = array2[i];
			string[] array3 = text.Split(new char[]
			{
				'|'
			});
			string[] array4 = array3;
			for (int j = 0; j < array4.Length; j++)
			{
				string text2 = array4[j];
				if (text2 != "" && text2 == ids)
				{
					this.hdwscode.Value = this.hdwscode.Value.Remove(this.hdwscode.Value.IndexOf(text2, 0), text2.Length + 1);
				}
			}
		}
	}
	protected void btnShow_Click(object sender, EventArgs e)
	{
		this.BindNeedGv();
		this.BindNeedNote();
		this.hdc.Value = "0";
	}
	public string GetTime(string time)
	{
		if (!string.IsNullOrEmpty(time))
		{
			return Convert.ToDateTime(time).ToString("yyyy-MM-dd");
		}
		return "";
	}
	protected void gvNeed_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvNeed.PageIndex = e.NewPageIndex;
		this.BindNeedGv();
	}
	protected void gvNeedNote_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvNeedNote.PageIndex = e.NewPageIndex;
		this.BindNeedNote();
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.BindNeedGv();
	}
}


