using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class oa_UserDefineFlow_MyFlowInitiate : NBasePage, IRequiresSessionState
{

	public OAWFApplyItemAction hrAction
	{
		get
		{
			return new OAWFApplyItemAction();
		}
	}
	public RecieveMsgAction rma
	{
		get
		{
			return new RecieveMsgAction();
		}
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.btnViewWF.Attributes["onclick"] = "OpenViewWF();return false;";
			this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF();return false;";
			if (this.Session["yhdm"] != null)
			{
				this.BindClass(this.Session["yhdm"].ToString());
			}
			this.BindView(this.Getwhere());
		}
	}
	protected void BindClass(string usercode)
	{
		this.DDLBusinessClass.Items.Clear();
		this.DDLBusinessClass.Items.Add(new ListItem("所有类别", ""));
		this.DDLBusinessClass.Items.Add(new ListItem("自定义流程", "999"));
		string spName = "\r\n\t\t\t--查询所有的流程类别, 并按模块排序\r\n\t\t\t--DECLARE @userCode varchar(8)\r\n\t\t\t--SET @userCode = '00000000'\r\n\t\t\tSELECT bcls.*, bcode.C_MKDM, mk.I_XH\r\n\t\t\tFROM WF_Business_Class bcls\r\n\t\t\tLEFT JOIN WF_BusinessCode bcode ON bcls.BusinessCode = bcode.BusinessCode\r\n\t\t\tJOIN PT_MK mk ON bcode.C_MKDM = mk.C_MKDM\r\n\t\t\tWHERE bcls.BusinessCode IN (\r\n\t\t\t\tSELECT BusinessCode FROM [WF_Instance_Main] WHERE Organiger = @userCode\r\n\t\t\t) AND bcls.BusinessCode!='999'\r\n\t\t\tORDER BY mk.I_XH, bcls.BusinessCode\r\n\t\t";
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
	protected void BindView(string where)
	{
		int num = (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1;
		int num2 = this.AspNetPager1.CurrentPageIndex * this.AspNetPager1.PageSize;
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		stringBuilder.AppendFormat("DECLARE @sql NVARCHAR(MAX)\r\n                                SET @sql=''; \r\n                                SELECT @sql=@sql+'SELECT '''+  BusinessCode  +''' AS BusinessCode\r\n                                ,'+PrimaryField+' AS Id,'+ StateField+' AS FlowState FROM '+LinkTable +' UNION ALL ' \r\n                                FROM WF_BusinessCode AS wfCode\r\n                                JOIN sys.tables AS tables ON tables.name=wfCode.LinkTable ;\r\n                                SET @sql=LEFT(@sql,LEN(@sql)-10);\r\n                                --execute(@sql)\r\n\r\n                                DECLARE @execSql NVARCHAR(MAX),@organiger VARCHAR(8)\r\n                                SET @organiger='00000000';\r\n                                SET @execSql=N'\r\n                                WITH AA AS(\r\n                                    SELECT ROW_NUMBER() OVER (ORDER BY StartTime DESC) Num,\r\n                                    Instance.*,Template.TemplateName,Business.BusinessClassName,Code.DoWithUrl\r\n                                    --,WFMsg.v_yhdm,WFMsg.RecieveUserCode\r\n                                    ,FlowState    --流程状态\r\n                                ,(\tSELECT yh.v_xm +'','' FROM WF_Instance \r\n                                    JOIN pt_yhmc AS yh ON Operator=yh.v_yhdm\r\n                                    WHERE ID=Instance.ID\t\r\n                                    ORDER BY TheOrder ASC\r\n                                    FOR XML PATH('''')\r\n                                ) AS OperatorPerson --参与审核人\r\n                                ,(\tSELECT yh.v_yhdm +'','' FROM WF_RecieveMsg AS Rmsg \r\n                                JOIN pt_yhmc AS yh ON Rmsg.v_yhdm=yh.v_yhdm\r\n                                    WHERE InstanceCode=Instance.InstanceCode\r\n                                    ORDER BY RecieveDate ASC\r\n                                    FOR XML PATH('''') \r\n                                )AS TellUserCodes  --被告知人编码\r\n                                ,( SELECT yh.v_xm +'','' FROM WF_RecieveMsg AS Rmsg\r\n                                JOIN pt_yhmc AS yh ON Rmsg.v_yhdm=yh.v_yhdm\r\n                                  WHERE InstanceCode=Instance.InstanceCode\r\n                                  ORDER BY RecieveDate ASC\r\n                                  FOR XML PATH('''')\r\n                                ) AS TellName --被告知人姓名\r\n                                ,( SELECT yh.v_yhdm +'','' FROM (SELECT * FROM WF_RecieveMsg \r\n                                    WHERE RecordID IN (SELECT MAX(RecordID) FROM WF_RecieveMsg\r\n                                    GROUP BY InstanceCode )) AS Rmsg\r\n                                   JOIN pt_yhmc AS yh ON Rmsg.RecieveUserCode=yh.v_yhdm\r\n                                   WHERE InstanceCode=Instance.InstanceCode\r\n                                   ORDER BY RecieveDate ASC\r\n                                   FOR XML PATH(''''))AS ToldUser --告知人编码\r\n                                ,( SELECT yh.v_xm +'','' FROM (SELECT * FROM WF_RecieveMsg \r\n                                    WHERE RecordID IN (SELECT MAX(RecordID) FROM WF_RecieveMsg\r\n                                    GROUP BY InstanceCode )) AS Rmsg\r\n                                   JOIN pt_yhmc AS yh ON Rmsg.RecieveUserCode=yh.v_yhdm\r\n                                   WHERE InstanceCode=Instance.InstanceCode\r\n                                   ORDER BY RecieveDate ASC\r\n                                   FOR XML PATH('''') \r\n                                )AS ToldName --告知人姓名\r\n                                FROM (\r\n                                    SELECT * FROM WF_Instance_Main\r\n                                    WHERE ID IN (\r\n                                        SELECT  MAX(ID) FROM WF_Instance_Main\r\n                                        GROUP BY INstanceCode,BusinessCode,Organiger)\r\n                                ) Instance\r\n                                 JOIN WF_Template Template  on Instance.TemplateID=Template.TemplateID \r\n                                 JOIN WF_Business_Class Business ON Business.BusinessCode=Instance.BusinessCode and Business.BusinessClass =Instance.BusinessClass\r\n                                 JOIN WF_BusinessCode Code ON  Instance.BusinessCode=Code.BusinessCode \r\n                                 --LEFT JOIN WF_RecieveMsg AS WFMsg ON WFMsg.InstanceCode=Instance.InstanceCode \r\n                                 JOIN ('+@sql+') AS T ON T.Id =Instance.InstanceCode AND T.BusinessCode=Instance.BusinessCode\r\n                                 WHERE organiger=' + @organiger +' AND FlowState IS NOT NULL \r\n                                  )SELECT COUNT(*) FROM AA ';\r\n                                EXEC (@execSql)\r\n                            ", this.Session["yhdm"], where);
		DataTable dataTable = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
		System.Text.StringBuilder stringBuilder2 = new System.Text.StringBuilder();
		stringBuilder2.AppendFormat(string.Concat(new object[]
		{
			"DECLARE @sql NVARCHAR(MAX)\r\n                                SET @sql=''; \r\n                                SELECT @sql=@sql+'SELECT '''+  BusinessCode  +''' AS BusinessCode\r\n                                ,'+PrimaryField+' AS Id,'+ StateField+' AS FlowState FROM '+LinkTable +' UNION ALL ' \r\n                                FROM WF_BusinessCode AS wfCode\r\n                                JOIN sys.tables AS tables ON tables.name=wfCode.LinkTable ;\r\n                                SET @sql=LEFT(@sql,LEN(@sql)-10);\r\n                                --execute(@sql)\r\n\r\n                                DECLARE @execSql NVARCHAR(MAX),@organiger VARCHAR(8)\r\n                                SET @organiger='00000000';\r\n                                SET @execSql=N'\r\n                                WITH AA AS(\r\n\t                                SELECT ROW_NUMBER() OVER (ORDER BY StartTime DESC) Num,\r\n\t                                Instance.*,Template.TemplateName,Business.BusinessClassName,Code.DoWithUrl\r\n\t                                --,WFMsg.v_yhdm,WFMsg.RecieveUserCode\r\n\t                                ,FlowState    --流程状态\r\n                                ,(\tSELECT yh.v_xm +'','' FROM WF_Instance \r\n\t                                JOIN pt_yhmc AS yh ON Operator=yh.v_yhdm\r\n\t                                WHERE ID=Instance.ID\t\r\n\t                                ORDER BY TheOrder ASC\r\n\t                                FOR XML PATH('''')\r\n                                ) AS OperatorPerson --参与审核人\r\n                                ,(\tSELECT yh.v_yhdm +'','' FROM WF_RecieveMsg AS Rmsg \r\n                                JOIN pt_yhmc AS yh ON Rmsg.v_yhdm=yh.v_yhdm\r\n\t                                WHERE InstanceCode=Instance.InstanceCode\r\n\t                                ORDER BY RecieveDate ASC\r\n\t                                FOR XML PATH('''') \r\n                                )AS TellUserCodes  --被告知人编码\r\n                                ,( SELECT yh.v_xm +'','' FROM WF_RecieveMsg AS Rmsg\r\n                                JOIN pt_yhmc AS yh ON Rmsg.v_yhdm=yh.v_yhdm\r\n                                  WHERE InstanceCode=Instance.InstanceCode\r\n                                  ORDER BY RecieveDate ASC\r\n                                  FOR XML PATH('''')\r\n                                ) AS TellName --被告知人姓名\r\n                                ,( SELECT yh.v_yhdm +'','' FROM (SELECT * FROM WF_RecieveMsg \r\n\t                                WHERE RecordID IN (SELECT MAX(RecordID) FROM WF_RecieveMsg\r\n\t                                GROUP BY InstanceCode )) AS Rmsg\r\n                                   JOIN pt_yhmc AS yh ON Rmsg.RecieveUserCode=yh.v_yhdm\r\n                                   WHERE InstanceCode=Instance.InstanceCode\r\n                                   ORDER BY RecieveDate ASC\r\n                                   FOR XML PATH(''''))AS ToldUser --告知人编码\r\n                                ,( SELECT yh.v_xm +'','' FROM (SELECT * FROM WF_RecieveMsg \r\n\t                                WHERE RecordID IN (SELECT MAX(RecordID) FROM WF_RecieveMsg\r\n\t                                GROUP BY InstanceCode )) AS Rmsg\r\n                                   JOIN pt_yhmc AS yh ON Rmsg.RecieveUserCode=yh.v_yhdm\r\n                                   WHERE InstanceCode=Instance.InstanceCode\r\n                                   ORDER BY RecieveDate ASC\r\n                                   FOR XML PATH('''') \r\n                                )AS ToldName --告知人姓名\r\n                                FROM (\r\n\t                                SELECT * FROM WF_Instance_Main\r\n\t                                WHERE ID IN (\r\n\t\t                                SELECT  MAX(ID) FROM WF_Instance_Main\r\n\t\t                                GROUP BY INstanceCode,BusinessCode,Organiger)\r\n                                ) Instance\r\n                                 JOIN WF_Template Template  on Instance.TemplateID=Template.TemplateID \r\n                                 JOIN WF_Business_Class Business ON Business.BusinessCode=Instance.BusinessCode and Business.BusinessClass =Instance.BusinessClass\r\n                                 JOIN WF_BusinessCode Code ON  Instance.BusinessCode=Code.BusinessCode \r\n                                 --LEFT JOIN WF_RecieveMsg AS WFMsg ON WFMsg.InstanceCode=Instance.InstanceCode \r\n                                 JOIN ('+@sql+') AS T ON T.Id =Instance.InstanceCode AND T.BusinessCode=Instance.BusinessCode\r\n                                 WHERE organiger=' + @organiger +' AND FlowState IS NOT NULL \r\n                                  )SELECT  * FROM AA WHERE Num BETWEEN ",
			num,
			" AND ",
			num2,
			"';\r\n                                EXEC (@execSql)"
		}), new object[]
		{
			this.Session["yhdm"],
			where,
			num,
			num2
		});
		DataTable dataSource = publicDbOpClass.DataTableQuary(stringBuilder2.ToString());
		this.AspNetPager1.RecordCount = System.Convert.ToInt32(dataTable.Rows[0][0]);
		this.GVInitiate.DataSource = dataSource;
		this.GVInitiate.DataBind();
	}
	protected void GVInitiate_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = System.Convert.ToString(e.Row.RowIndex + 1);
			string text = dataRowView["BusinessCode"].ToString();
			string text2 = dataRowView["businessclass"].ToString();
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["InstanceCode"].ToString(),
				"','",
				text,
				"','",
				text2,
				"');"
			});
			HyperLink hyperLink = (HyperLink)e.Row.Cells[4].FindControl("HLConter");
			hyperLink.Text = FlowAuditAction.AuditConter((System.Guid)dataRowView["InstanceCode"], text, text2);
			if (hyperLink.Text == "")
			{
				hyperLink.Text = "点击查看";
			}
			if (hyperLink.Text.Length > 15)
			{
				hyperLink.ToolTip = hyperLink.Text;
				hyperLink.Text = hyperLink.Text.Substring(0, 15) + "...";
			}
			string text3 = dataRowView["DoWithUrl"].ToString();
			if (text3.IndexOf('?') != -1)
			{
				text3 = text3 + "&showAudit=1&showname=1&ic=" + dataRowView["InstanceCode"].ToString();
			}
			else
			{
				text3 = text3 + "?showAudit=1&showname=1&ic=" + dataRowView["InstanceCode"].ToString();
			}
			hyperLink.Attributes["onclick"] = "toolbox_oncommand('" + text3 + "','审核内容查看')";
			hyperLink.NavigateUrl = "#";
			hyperLink.Target = "_self";
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindView(this.Getwhere());
	}
	public string Getwhere()
	{
		string text = string.Empty;
		if (!string.IsNullOrWhiteSpace(this.DDLBusinessClass.SelectedValue))
		{
			text = text + " AND Instance.BusinessCode = " + this.DDLBusinessClass.SelectedValue + " ";
		}
		if (!string.IsNullOrWhiteSpace(this.txttemplate.Text.Trim()))
		{
			text = text + " AND Template.TemplateName like ''%'+'" + this.GetSafeString(this.txttemplate.Text.Trim()) + "'+'%''";
		}
		if (!string.IsNullOrWhiteSpace(this.DBDate.Text))
		{
			text = text + " AND datediff(dd,StartTime,''" + this.DBDate.Text + "'')=0 ";
		}
		if (!string.IsNullOrEmpty(this.hidsend.Value))
		{
			text = text + " AND Instance.instanceCode in (select instancecode from WF_RecieveMsg where charindex(''" + this.hidsend.Value + "'',recieveUserCode)>0)";
		}
		if (!string.IsNullOrEmpty(this.hidtolder.Value))
		{
			text = text + " AND Instance.instanceCode in (select instancecode from WF_RecieveMsg where charindex(v_yhdm,''" + this.hidtolder.Value + "'')>0)";
		}
		if (!string.IsNullOrEmpty(this.ddsing.SelectedValue))
		{
			text = text + " AND  FlowState =" + this.ddsing.SelectedItem.Value;
		}
		this.hidsend.Value = "";
		this.hidtolder.Value = "";
		return text;
	}
	protected void GVInitiate_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.BindView(this.Getwhere());
	}
	protected void DDLBusinessClass_SelectedIndexChanged(object sender, System.EventArgs e)
	{
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindView(this.Getwhere());
	}
	private string GetSafeString(string str)
	{
		str = str.Replace("'", " ");
		str = str.Replace("%", "[%]");
		return str;
	}
}


