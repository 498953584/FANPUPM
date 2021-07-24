using ASP;
using cn.justwin.BLL;
using cn.justwin.opm;
using cn.justwin.opm.OPS;
using cn.justwin.stockBLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Business_Data_Schedule : NBasePage, IRequiresSessionState
{
	private PrjStageSetAction action = new PrjStageSetAction();
	private MaintainDataAction mda = new MaintainDataAction();
	private PrjStageInfoAction pia = new PrjStageInfoAction();
	private DataTable dt;
	private string[] codeID = new string[100];

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.GetStage();
		}
	}
	protected void gvStageSchedule_RowCreated(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			TableCellCollection cells = e.Row.Cells;
			cells.Clear();
			cells.Add(new TableHeaderCell());
			cells[0].Attributes.Add("rowspan", "2");
			cells[0].Style.Add("width", "40px");
			cells[0].Text = "项目名称";
			cells.Add(new TableHeaderCell());
			cells[1].Attributes.Add("rowspan", "2");
			cells[1].Style.Add("width", "40px");
			cells[1].Text = "uid";
			cells.Add(new TableHeaderCell());
			cells[2].Attributes.Add("rowspan", "2");
			cells[2].Style.Add("width", "40px");
			cells[2].Text = "prjID";
			cells.Add(new TableHeaderCell());
			cells[3].Attributes.Add("rowspan", "2");
			cells[3].Style.Add("width", "40px");
			cells[3].Text = "PrjCode";
			this.dt = publicDbOpClass.DataTableQuary("\r\n                select codeID,codeName from XPM_Basic_CodeList where TypeID IN (\r\n                SELECT typeId FROM XPM_Basic_CodeType\r\n                WHERE SignCode='Img'\r\n                ) and [IsValid]='true' and ChildNumber=0  order by I_xh\r\n                ");
			int count = this.dt.Rows.Count;
			for (int i = 0; i < count; i++)
			{
				cells.Add(new TableHeaderCell());
				cells[i + 4].Attributes.Add("colspan", "3");
				cells[i + 4].Text = this.dt.Rows[i]["codename"].ToString();
			}
			TableCell expr_200 = cells[cells.Count - 1];
			expr_200.Text += "</th></tr><tr class=\"header\">";
			for (int j = 0; j < count; j++)
			{
				cells.Add(new TableHeaderCell());
				if (j == 0)
				{
					cells[cells.Count - 1].Text = "计划日期";
				}
				else
				{
					cells[cells.Count - 1].Text = "计划日期";
				}
				cells.Add(new TableHeaderCell());
				cells[cells.Count - 1].Text = "实际日期";
				cells.Add(new TableHeaderCell());
				cells[cells.Count - 1].Text = "状态";
			}
			TableCell expr_2BE = cells[cells.Count - 1];
			expr_2BE.Text += "</tr>";
		}
	}
	protected void gvStageSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			e.Row.Cells[1].Visible = false;
			e.Row.Cells[2].Visible = false;
			e.Row.Cells[3].Visible = false;
			string sqlString = "select TypeID from XPM_Basic_CodeType where SignCode='img'";
			object obj = publicDbOpClass.ExecuteScalar(sqlString);
			int typeid;
			if (obj != null)
			{
				typeid = System.Convert.ToInt32(obj);
			}
			else
			{
				typeid = 189;
			}
			for (int i = 0; i < (e.Row.Cells.Count - 3) / 4; i++)
			{
				if (i != (e.Row.Cells.Count - 3) / 4 - 1)
				{
					this.codeID[i] = this.mda.GetCodeID(typeid, e.Row.Cells[i + 4].Text);
				}
				else
				{
					this.codeID[i] = this.mda.GetCodeID(typeid, e.Row.Cells[i + 4].Text.Replace("</th></tr><tr class=\"header\">", ""));
				}
			}
		}
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Cells[1].Visible = false;
			e.Row.Cells[2].Visible = false;
			e.Row.Cells[3].Visible = false;
			e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
			e.Row.Cells[0].ForeColor = Color.Black;
			int count = e.Row.Cells.Count;
			for (int j = 4; j < count; j++)
			{
				if (e.Row.Cells[j].Text != "--" && j % 3 == 0 && e.Row.Cells[j].Text != "未完成")
				{
					e.Row.Cells[j].Text = string.Concat(new string[]
					{
						"<span  class=\"link\" onclick=\"toolbox_oncommand('/OPM/Business_Data/Business_Data_Main.aspx?w=0&codetype=Img&opType=img&isshowall=true&isShow=1&PrjCode=",
						e.Row.Cells[3].Text,
						"&pc=",
						e.Row.Cells[2].Text,
						"&codeid=",
						this.codeID[(j - 2) / 3 - 1],
						"', '图纸信息查看')\">",
						e.Row.Cells[j].Text
					});
				}
			}
			if (e.Row.Cells[0].Text != "")
			{
				e.Row.Cells[0].Text = string.Concat(new string[]
				{
					"<span class=\"link\" onclick=\"toolbox_oncommand('/PrjManager/PrjInfoView.aspx?&ic=",
					e.Row.Cells[2].Text,
					"', '项目信息查看')\">",
					e.Row.Cells[0].Text,
					" "
				});
			}
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.GetStage();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.GetStage();
	}
	public void GetStage()
	{
		string prjcode = this.txtCode.Text.Trim();
		string prjname = this.txtPrjName.Text.Trim();
		string strSignCode = string.Empty;
		if (!string.IsNullOrEmpty(base.Request.Params["businessType"].ToString()))
		{
			strSignCode = base.Request.Params["businessType"].ToString();
		}
		DataTable dataTable = CodingAction.CodeInfoListType(strSignCode);
		this.dt = publicDbOpClass.DataTableQuary("select codeID,codeName from XPM_Basic_CodeList where TypeID=" + System.Convert.ToInt32(dataTable.Rows[0]["TypeID"].ToString()) + " and [IsValid]='true' and ChildNumber=0  order by I_xh");
		DataTable dataTable2 = new DataTable();
		dataTable2.Columns.Add("项目名称");
		dataTable2.Columns.Add("uid");
		dataTable2.Columns.Add("prjID");
		dataTable2.Columns.Add("PrjCode");
		for (int i = 0; i < this.dt.Rows.Count; i++)
		{
			dataTable2.Columns.Add("计划日期" + i.ToString());
			dataTable2.Columns.Add("实际日期" + i.ToString());
			dataTable2.Columns.Add("状态" + i.ToString());
		}
		PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
		int projectCount = pTPrjInfoBll.GetProjectCount(base.UserCode, prjcode, prjname);
		this.AspNetPager1.RecordCount = projectCount;
		DataTable project = pTPrjInfoBll.GetProject(base.UserCode, prjcode, prjname, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		foreach (DataRow dataRow in project.Rows)
		{
			string text = dataRow["prjGuid"].ToString();
			string text2 = dataRow["prjName"].ToString();
			string value = dataRow["PrjCode"].ToString();
			DataRow dataRow2 = dataTable2.NewRow();
			if (text2.Length > 20)
			{
				dataRow2["项目名称"] = text2.Substring(1, 20) + "...";
			}
			else
			{
				dataRow2["项目名称"] = text2;
			}
			dataRow2["prjID"] = text;
			dataRow2["PrjCode"] = value;
			for (int j = 0; j < this.dt.Rows.Count; j++)
			{
				string value2 = "--";
				string value3 = "--";
				string text3 = "--";
				string text4 = this.dt.Rows[j]["codeID"].ToString();
				this.dt.Rows[j]["codeName"].ToString();
				string text5 = "\r\n                 select top (1) UID,enddate FROM [OPM_Business_Data]  \r\n                 left join WF_Instance_Main on InstanceCode=uid inner join WF_Instance   on WF_Instance.ID=WF_Instance_Main.ID \r\n                 where [IsValid]=1 and flowstate=1 and btype='img' ";
				string text6 = text5;
				text5 = string.Concat(new string[]
				{
					text6,
					"and [prjid]='",
					text,
					"' and [codeID]='",
					text4,
					"'  order by AuditDate desc"
				});
				DataTable dataTable3 = publicDbOpClass.DataTableQuary(text5.ToString());
				if (dataTable3.Rows.Count > 0)
				{
					string text7 = dataTable3.Rows[0][0].ToString();
					if (dataTable3.Rows[0]["enddate"].ToString() != "")
					{
						value2 = System.Convert.ToDateTime(dataTable3.Rows[0]["enddate"].ToString()).ToString("yyyy-MM-dd");
					}
					if ((int)publicDbOpClass.ExecuteScalar("select count(*) FROM [OPM_Business_Dataitem] where flowstate='1' and codeid='" + text7 + "'") > 0)
					{
						string text8 = "select top(1) AuditDate FROM [OPM_Business_Dataitem] \r\n                        left join WF_Instance_Main on InstanceCode=uid inner join WF_Instance   on WF_Instance.ID=WF_Instance_Main.ID where   flowstate='1' ";
						text8 = text8 + " and codeid='" + text7 + "' order by AuditDate desc ";
						if (System.Convert.ToDateTime(publicDbOpClass.ExecuteScalar(text8)).ToString("yyyy-MM-dd") != "0001-01-01")
						{
							value3 = System.Convert.ToDateTime(publicDbOpClass.ExecuteScalar(text8)).ToString("yyyy-MM-dd");
						}
						text3 = "1";
					}
					else
					{
						DataTable dataTable4 = publicDbOpClass.DataTableQuary("select top(1)flowstate FROM [OPM_Business_Dataitem] where codeid='" + text7 + "' order by addtime");
						if (dataTable4.Rows.Count > 0)
						{
							text3 = dataTable4.Rows[0][0].ToString();
						}
						else
						{
							text3 = "-1";
						}
					}
					dataRow2["uid"] = text7;
				}
				dataRow2[3 * j + 4] = value2;
				dataRow2[3 * j + 5] = value3;
				dataRow2[3 * j + 6] = ((text3 == "--") ? "--" : Business_Data_Schedule.GetState(text3));
			}
			dataTable2.Rows.Add(dataRow2);
		}
		this.gvStageSchedule.DataSource = dataTable2;
		this.gvStageSchedule.DataBind();
	}
	public static string GetState(string state)
	{
		string result;
		if (state != null)
		{
			if (state == "0")
			{
				result = "审核中";
				return result;
			}
			if (state == "1")
			{
				result = "已审核";
				return result;
			}
			if (state == "-2")
			{
				result = "驳回";
				return result;
			}
			if (state == "-3")
			{
				result = "重报";
				return result;
			}
		}
		result = "未完成";
		return result;
	}
}


