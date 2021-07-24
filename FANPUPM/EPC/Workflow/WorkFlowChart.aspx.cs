using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class WorkFlowChart : BasePage, IRequiresSessionState
	{
		public int TemplateID
		{
			get
			{
				object obj = this.ViewState["TemplateID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TemplateID"] = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.TemplateID = System.Convert.ToInt32(base.Request["tid"]);
				this.addFlowChart_top1();
				FlowChartAction.display_FlowChart(this.tbFlowChart, this.TemplateID);
			}
		}
		private void addFlowChart_top1()
		{
			int rowNum = 1;
			System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
			hashtable.Add("TemplateID", this.TemplateID.ToString());
			hashtable.Add("RowNum", rowNum.ToString());
			hashtable.Add("Column1", SqlStringConstructor.GetQuotedString("1;0;开始"));
			DataRow dataRow = FlowTemplateAction.QueryFlowChart_top1(this.TemplateID, rowNum);
			if (dataRow != null)
			{
				if ((dataRow["TemplateID"].ToString() == "" || dataRow["TemplateID"] == null) && !FlowTemplateAction.AddFlowChart(hashtable))
				{
					this.JS.Text = "alert('添加数据失败！')";
					return;
				}
			}
			else
			{
				if (!FlowTemplateAction.AddFlowChart(hashtable))
				{
					this.JS.Text = "alert('添加数据失败！')";
				}
			}
		}
	}


