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
public partial class HR_Organization_HRLayout_PersonPay : BasePage, IRequiresSessionState
{

	public HROrgAveragePayAction hrAction
	{
		get
		{
			return new HROrgAveragePayAction();
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
	public bool AuditState
	{
		get
		{
			object obj = this.ViewState["AUDITSTATE"];
			return obj != null && (bool)this.ViewState["AUDITSTATE"];
		}
		set
		{
			this.ViewState["AUDITSTATE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["cc"] == null || base.Request["audit"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		if (base.Request["cc"].ToString() != "")
		{
			this.RecordID = new Guid(base.Request["cc"].ToString().Trim());
		}
		if (base.Request["audit"].ToString() != "")
		{
			this.AuditState = (int.Parse(base.Request["audit"].ToString()) > -1);
		}
		if (!this.Page.IsPostBack && this.AuditState)
		{
			this.BtnSave.Enabled = false;
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
			textBox2.Attributes["onblur"] = "checkDecimal(this);";
			DataTable hRAveragePay = this.hrAction.GetHRAveragePay(this.RecordID, dataRowView["ClassID"].ToString());
			if (hRAveragePay.Rows.Count > 0)
			{
				textBox2.Text = hRAveragePay.Rows[0]["AveragePay"].ToString();
				textBox.Text = hRAveragePay.Rows[0]["Remark"].ToString();
			}
			for (int i = 2; i <= 13; i++)
			{
				int num = i - 1;
				TextBox textBox3 = (TextBox)e.Row.Cells[i].FindControl("txtMoney" + num.ToString());
				textBox3.Attributes["onblur"] = "checkDecimal(this);";
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
	private ArrayList GetData()
	{
		ArrayList arrayList = new ArrayList();
		foreach (GridViewRow gridViewRow in this.GVBook.Rows)
		{
			arrayList.Add(new HROrgAveragePay
			{
				AveragePay = (((TextBox)gridViewRow.Cells[1].FindControl("txtAveragePay")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[1].FindControl("txtAveragePay")).Text),
				ClassID = (((HtmlInputHidden)gridViewRow.Cells[1].FindControl("HdnClassID")).Value == "") ? 0 : int.Parse(((HtmlInputHidden)gridViewRow.Cells[1].FindControl("HdnClassID")).Value),
				ID = 0,
				Month1 = (((TextBox)gridViewRow.Cells[2].FindControl("txtMoney1")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[2].FindControl("txtMoney1")).Text),
				Month2 = (((TextBox)gridViewRow.Cells[3].FindControl("txtMoney2")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[3].FindControl("txtMoney2")).Text),
				Month3 = (((TextBox)gridViewRow.Cells[4].FindControl("txtMoney3")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[4].FindControl("txtMoney3")).Text),
				Month4 = (((TextBox)gridViewRow.Cells[5].FindControl("txtMoney4")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[5].FindControl("txtMoney4")).Text),
				Month5 = (((TextBox)gridViewRow.Cells[6].FindControl("txtMoney5")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[6].FindControl("txtMoney5")).Text),
				Month6 = (((TextBox)gridViewRow.Cells[7].FindControl("txtMoney6")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[7].FindControl("txtMoney6")).Text),
				Month7 = (((TextBox)gridViewRow.Cells[8].FindControl("txtMoney7")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[8].FindControl("txtMoney7")).Text),
				Month8 = (((TextBox)gridViewRow.Cells[9].FindControl("txtMoney8")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[9].FindControl("txtMoney8")).Text),
				Month9 = (((TextBox)gridViewRow.Cells[10].FindControl("txtMoney9")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[10].FindControl("txtMoney9")).Text),
				Month10 = (((TextBox)gridViewRow.Cells[11].FindControl("txtMoney10")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[11].FindControl("txtMoney10")).Text),
				Month11 = (((TextBox)gridViewRow.Cells[12].FindControl("txtMoney11")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[12].FindControl("txtMoney11")).Text),
				Month12 = (((TextBox)gridViewRow.Cells[13].FindControl("txtMoney12")).Text == "") ? 0m : Convert.ToDecimal(((TextBox)gridViewRow.Cells[13].FindControl("txtMoney12")).Text),
				RecordID = this.RecordID,
				Remark = ((TextBox)gridViewRow.Cells[14].FindControl("txtRemark")).Text
			});
		}
		return arrayList;
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		ArrayList data = this.GetData();
		if (this.hrAction.Add(data) > 0)
		{
			this.Page.RegisterStartupScript("", "<script>alert('保存成功!');</script>");
			return;
		}
		this.Page.RegisterStartupScript("", "<script>alert('保存失败!');</script>");
	}
}


