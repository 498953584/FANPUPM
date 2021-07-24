using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Organization_HRLayoutLock : BasePage, IRequiresSessionState
{
	public HROrgManpowerPlanAction hrAction
	{
		get
		{
			return new HROrgManpowerPlanAction();
		}
	}
	public HROrgAveragePayAction pa
	{
		get
		{
			return new HROrgAveragePayAction();
		}
	}
	public AnnexAction _AnnexAction
	{
		get
		{
			return new AnnexAction();
		}
	}
	public Guid RecordID
	{
		get
		{
			object obj = this.ViewState["RECORDID"];
			if (obj != null)
			{
				return (Guid)this.ViewState["RECORDID"];
			}
			return Guid.NewGuid();
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack && base.Request["ic"] != null)
		{
			base.Response.Cache.SetNoStore();
			this.RecordID = new Guid(base.Request["ic"].ToString());
			this.EditDisplay();
			this.FileUp_Bind();
		}
	}
	private void EditDisplay()
	{
		HROrgManpowerPlan model = this.hrAction.GetModel(this.RecordID);
		if (model != null)
		{
			this.LbLayoutName.Text = model.PlanName;
			this.LbLayoutDate.Text = model.RecordDate.ToShortDateString();
			this.LblAddPerson.Text = BooksCommonClass.GetUserName(model.UserCode);
		}
	}
	private void FileUp_Bind()
	{
		string text = "";
		ArrayList annexList = this._AnnexAction.GetAnnexList(this.RecordID.ToString(), 0, 28);
		if (annexList.Count > 0)
		{
			for (int i = 0; i < annexList.Count; i++)
			{
				text = text + ((AnnexInfo)annexList[i]).OriginalName + ",";
			}
			this.lblAnnex.Text = text.Trim(new char[]
			{
				','
			});
		}
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		switch (e.Row.RowType)
		{
		case DataControlRowType.Header:
			e.Row.Cells.Clear();
			e.Row.Cells.Add(this.Bind_Head());
			return;
		case DataControlRowType.Footer:
			break;
		case DataControlRowType.DataRow:
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			TextBox textBox = (TextBox)e.Row.Cells[1].FindControl("txtRemark");
			TextBox textBox2 = (TextBox)e.Row.Cells[1].FindControl("txtAveragePay");
			DataTable hRAveragePay = this.pa.GetHRAveragePay(this.RecordID, dataRowView["ClassID"].ToString());
			if (hRAveragePay.Rows.Count > 0)
			{
				textBox2.Text = hRAveragePay.Rows[0]["AveragePay"].ToString();
				textBox.Text = hRAveragePay.Rows[0]["Remark"].ToString();
			}
			for (int i = 2; i <= 13; i++)
			{
				int num = i - 1;
				TextBox textBox3 = (TextBox)e.Row.Cells[i].FindControl("txtMoney" + num.ToString());
				if (hRAveragePay.Rows.Count > 0)
				{
					textBox3.Text = hRAveragePay.Rows[0]["Month" + num.ToString()].ToString();
				}
			}
			break;
		}
		default:
			return;
		}
	}
	private TableCell Bind_Head()
	{
		return new TableCell
		{
			RowSpan = 2,
			BackColor = Color.FromName("#ece9d8"),
			HorizontalAlign = HorizontalAlign.Center,
			Text = " <font style=\"FONT-WEIGHT: bold\">人员类别</font></td>  <td rowspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">平均工资</font></td>  <td colspan=14 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">平均人数分布</font></td>  </tr>  <tr>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">1月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">2月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">3月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">4月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">5月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">6月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">7月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">8月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">9月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">10月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">11月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">12月</font></td>  <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">备注</font> "
		};
	}
}


