using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class FlowStatus : NBasePage, IRequiresSessionState
	{

		public System.Guid InstanceCode
		{
			get
			{
				object obj = this.ViewState["InstanceCode"];
				if (obj != null)
				{
					return (System.Guid)obj;
				}
				return System.Guid.Empty;
			}
			set
			{
				this.ViewState["InstanceCode"] = value;
			}
		}
		protected string BusinessCode
		{
			get
			{
				return this.ViewState["BUSINESSCODE"].ToString();
			}
			set
			{
				this.ViewState["BUSINESSCODE"] = value;
			}
		}
		protected string BusinessClass
		{
			get
			{
				return this.ViewState["BUSINESSCLASS"].ToString();
			}
			set
			{
				this.ViewState["BUSINESSCLASS"] = value;
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.InstanceCode = new System.Guid(base.Request["ic"]);
				this.BusinessCode = base.Request["bc"].ToString();
				this.BusinessClass = base.Request["bcl"].ToString();
				try
				{
					FlowAuditAction.display_FlowChart(this.tbFlowChart, this.InstanceCode, this.BusinessCode, this.BusinessClass);
				}
				catch
				{
					HtmlTableRow htmlTableRow = new HtmlTableRow();
					HtmlTableCell htmlTableCell = new HtmlTableCell();
					htmlTableCell.InnerHtml = "<font color = 'red'>数据有错误!</font>";
					htmlTableRow.Cells.Add(htmlTableCell);
					this.tbFlowChart.Rows.Add(htmlTableRow);
				}
				try
				{
					string sqlString = string.Concat(new object[]
					{
						"select * from wf_instance_main where BusinessCode = '",
						this.BusinessCode,
						"' and BusinessClass ='",
						this.BusinessClass,
						"' and InstanceCode='",
						this.InstanceCode,
						"' "
					});
					DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
					if (dataTable.Rows.Count > 1)
					{
						HtmlTableRow htmlTableRow2 = new HtmlTableRow();
						HtmlTableCell htmlTableCell2 = new HtmlTableCell();
						htmlTableCell2.ColSpan = this.tbFlowChart.Rows[1].Cells.Count;
						htmlTableCell2.InnerHtml = "<font color = 'red'>该流程重报过，流程状态显示的是最新的结果</font>";
						htmlTableRow2.Cells.Add(htmlTableCell2);
						this.tbFlowChart.Rows.Add(htmlTableRow2);
					}
				}
				catch
				{
				}
			}
		}
	}


