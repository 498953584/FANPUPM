using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
	public partial class dbrSet : NBasePage, IRequiresSessionState
	{
		public new string UserCode
		{
			get
			{
				object obj = this.ViewState["UserCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["UserCode"] = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.AspNetPager1.PageSize = NBasePage.pagesize4;
				this.UserCode = this.Session["yhdm"].ToString();
				this.BindClass(this.UserCode);
				this.dgFlow_Bind();
			}
		}
		private void dgFlow_Bind()
		{
			this.AspNetPager1.RecordCount = FlowTemplateAction.GetTempNodeCout(this.UserCode, this.GetSafeString(this.txtTemplateName.Text.Trim()), this.DDLBusinessClass.SelectedValue, this.GetSafeString(this.txtNodeName.Text.Trim()));
			this.dgFlow.DataSource = FlowTemplateAction.GetTempNodesSouce(this.UserCode, this.GetSafeString(this.txtTemplateName.Text.Trim()), this.DDLBusinessClass.SelectedValue, this.GetSafeString(this.txtNodeName.Text.Trim()), this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
			this.dgFlow.DataBind();
		}
		protected void BindClass(string usercode)
		{
			this.DDLBusinessClass.Items.Clear();
			this.DDLBusinessClass.Items.Add(new ListItem("所有类别", ""));
			this.DDLBusinessClass.Items.Add(new ListItem("自定义流程", "999"));
			string spName = "\r\n                --DECLARE @userCode varchar(8)\r\n\t\t\t\t--SET @userCode = '00000000'\r\n\t\t\t\tSELECT bcls.*, bcode.C_MKDM, mk.I_XH\r\n\t\t\t\tFROM WF_Business_Class bcls\r\n\t\t\t\tLEFT JOIN WF_BusinessCode bcode ON bcls.BusinessCode = bcode.BusinessCode\r\n\t\t\t\tJOIN PT_MK mk ON bcode.C_MKDM = mk.C_MKDM\r\n\t\t\t\tWHERE bcls.BusinessCode IN (\r\n                     SELECT DISTINCT BusinessCode \r\n                     FROM [WF_Template] tmp \r\n                     INNER  JOIN [WF_TemplateNode] tmpN ON tmp.TemplateID=tmpN.TemplateID and tmpN.Operater=@userCode\r\n\t\t\t\t) AND bcls.BusinessCode!='999'\r\n\t\t\t\tORDER BY mk.I_XH, bcls.BusinessCode              \r\n              ";
			SqlParameter[] commandParameters = new SqlParameter[]
			{
				new SqlParameter("@userCode", usercode)
			};
			DataTable dataTable = publicDbOpClass.ExecuteDataTable(CommandType.Text, spName, commandParameters);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				this.DDLBusinessClass.Items.Add(new ListItem(dataTable.Rows[i]["businessclassname"].ToString(), dataTable.Rows[i]["businesscode"].ToString()));
			}
		}
		protected void dgFlow_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["id"] = System.Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);doClickRow('",
					this.UserCode.ToString(),
					"','",
					dataRowView["TemplateID"].ToString(),
					"','",
					dataRowView["nodeid"].ToString(),
					"');"
				});
				if (dataRowView["TemplateName"].ToString().Length > 20)
				{
					e.Item.Cells[1].ToolTip = dataRowView["TemplateName"].ToString();
					e.Item.Cells[1].Text = dataRowView["TemplateName"].ToString().Substring(0, 20) + "...";
				}
				if (dataRowView["Remark"].ToString().Length > 20)
				{
					e.Item.Cells[5].ToolTip = dataRowView["Remark"].ToString();
					e.Item.Cells[5].Text = dataRowView["Remark"].ToString().Substring(0, 20) + "...";
				}
				return;
			}
			default:
				return;
			}
		}
		protected void dgFlow_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgFlow.CurrentPageIndex = e.NewPageIndex;
			this.dgFlow_Bind();
		}
		protected void brnQuery_Click(object sender, System.EventArgs e)
		{
			this.AspNetPager1.CurrentPageIndex = 1;
			this.dgFlow_Bind();
		}
		protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
		{
			this.dgFlow_Bind();
		}
		protected new string GetUserName(string usercode)
		{
			string text = string.Empty;
			string arg = usercode.Contains(",") ? usercode.Substring(0, usercode.LastIndexOf(',')) : usercode;
			string cmdText = string.Format("SELECT v_xm FROM pt_yhmc WHERE v_yhdm IN({0}) ", arg);
			DataTable dataTable = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
			foreach (DataRow dataRow in dataTable.Rows)
			{
				text = text + dataRow["v_xm"].ToString() + ",";
			}
			return text;
		}
		private string GetSafeString(string str)
		{
			str = str.Replace("'", "''");
			str = str.Replace("%", "[%]");
			return str;
		}
	}


