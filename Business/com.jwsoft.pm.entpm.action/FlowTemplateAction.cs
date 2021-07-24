namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class FlowTemplateAction
    {
        public static bool AddAgent(Hashtable agentInfo)
        {
            return publicDbOpClass.Insert("[wf_agent]", agentInfo);
        }

        public static bool AddFlowChart(Hashtable flowChartInfo)
        {
            return publicDbOpClass.Insert("[wf_FlowChart]", flowChartInfo);
        }

        public static bool AddInstance(Hashtable instanceInfo)
        {
            return publicDbOpClass.Insert("[WF_Instance]", instanceInfo);
        }

        public static object AddInstanceMain(Hashtable instanceMainInfo)
        {
            string select = "";
            select = " SELECT SCOPE_IDENTITY() AS [IDENTITY] ";
            publicDbOpClass class2 = new publicDbOpClass();
            return class2.Insert("[WF_Instance_Main]", instanceMainInfo, select);
        }

        public static object AddNode(Hashtable nodeInfo)
        {
            string select = "";
            select = " SELECT SCOPE_IDENTITY() AS [IDENTITY] ";
            publicDbOpClass class2 = new publicDbOpClass();
            return class2.Insert("[wf_templatenode]", nodeInfo, select);
        }

        public static bool AddNode(int templateId, string nodeName, string operater, string remark, int operaterType)
        {
            string str2 = ((("" + " declare @theOrder int ") + " if not exists(select 1 from wf_templatenode where templateid=" + templateId.ToString() + ") ") + "\tset @theOrder = 1 " + " else ") + "    select @theOrder = max(TheOrder)+1 from wf_templatenode where templateid=" + templateId.ToString();
            return publicDbOpClass.NonQuerySqlString(str2 + "insert into wf_templatenode values(" + templateId.ToString() + ",'" + nodeName + "','" + remark + "','" + operater + "'," + operaterType.ToString() + ",@theOrder)");
        }

        public static bool AddNodeCondition(Hashtable nodeConditionInfo)
        {
            return publicDbOpClass.Insert("[wf_NodeCondition]", nodeConditionInfo);
        }

        public static bool AddTemplate(Hashtable templateInfo, string currentUser)
        {
            string select = " SELECT SCOPE_IDENTITY() AS [IDENTITY] ";
            object obj2 = new publicDbOpClass().Insert("[wf_template]", templateInfo, select);
            string str3 = " INSERT INTO WF_Template_Privilege(TemplateId,UserCode) VALUES(" + obj2.ToString() + ",'00000000') ;";
            return (publicDbOpClass.ExecuteSQL(str3 + "INSERT INTO WF_Template_Privilege(TemplateId,UserCode) VALUES(" + obj2.ToString() + ",'" + currentUser + "') ;") > 0);
        }

        public static bool AddTemplate(string templateName, string remark)
        {
            return publicDbOpClass.NonQuerySqlString("insert into wf_template values('" + templateName + "','" + remark + "',1)");
        }

        public static void AddTempLimitUser(string templateid, List<string> usercodes)
        {
            publicDbOpClass.ExecuteSQL(" DELETE FROM WF_Template_Privilege WHERE TemplateId=" + templateid);
            string sqlString = string.Empty;
            for (int i = 0; i < usercodes.Count; i++)
            {
                string str2 = sqlString;
                sqlString = str2 + " INSERT INTO WF_Template_Privilege(TemplateId,UserCode) VALUES(" + templateid + ",'" + usercodes[i] + "') ";
            }
            publicDbOpClass.ExecuteSQL(sqlString);
        }

        public static bool DelAgent(int nodeId)
        {
            return publicDbOpClass.NonQuerySqlString("delete wf_agent where nodeId=" + nodeId.ToString());
        }

        public static bool DelNode(int nodeId, int templateId)
        {
            string str = "";
            return publicDbOpClass.NonQuerySqlString(((((str + " begin " + " declare @theOrder int ") + " select @theOrder = theOrder from wf_templatenode where nodeid=" + nodeId.ToString()) + " delete from wf_templatenode where nodeid=" + nodeId.ToString()) + " update wf_templatenode set theOrder=theOrder-1 where templateid=" + templateId.ToString() + " and theOrder>@theOrder") + " end ");
        }

        public static bool DelTemplate(int templateId)
        {
            string str = "";
            object obj2 = str + "begin " + " delete from WF_NodeCondition ";
            object obj3 = string.Concat(new object[] { obj2, " where NodeID in (select NodeID from WF_TemplateNode where TemplateID = ", templateId, ")" });
            object obj4 = string.Concat(new object[] { obj3, " delete from WF_TemplateNode where TemplateID =  ", templateId, " " });
            object obj5 = string.Concat(new object[] { obj4, " delete from WF_FlowChart where TemplateID =  ", templateId, " " });
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj5, " delete from wf_template where TemplateID =  ", templateId, " " }) + " end");
        }

        public static bool DelTemplateNode(int nodeId, int templateId)
        {
            string str2 = " delete from WF_NodeCondition where nodeid = " + nodeId.ToString();
            return publicDbOpClass.NonQuerySqlString(str2 + " delete from WF_TemplateNode where nodeid= " + nodeId.ToString() + " and templateid=" + templateId.ToString());
        }

        public static DataTable GetAgentTempNodeSource(string usercode, string templateName, string businessCode, string nodeName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT  TempNode.NodeID AS nodeid,TempNode.NodeName AS nodename,TempNode.TemplateID AS templateid,\r\nTemp.TemplateName AS templateName,TempNode.Operater AS usercode,Temp.Remark AS remark,\r\n(SELECT v_xm FROM PT_yhmc WHERE v_yhdm='{0}') AS auditor,Class.BusinessClassName AS BusinessClassName,Class.BusinessCode \r\n FROM WF_TemplateNode TempNode\r\nLEFT JOIN WF_Template Temp  ON TempNode.TemplateID=Temp.TemplateID\r\nLEFT JOIN WF_Business_Class Class ON Temp.BusinessCode=Class.BusinessCode AND Temp.BusinessClass=Class.BusinessClass\r\nWHERE TempNode.Operater='{1}' AND AuditorType='1'", usercode, usercode);
            if (!string.IsNullOrEmpty(templateName))
            {
                builder.AppendFormat(" AND templateName like '%{0}%' ", templateName);
            }
            if (!string.IsNullOrEmpty(businessCode))
            {
                builder.AppendFormat("AND Class.BusinessCode ='{0}' ", businessCode);
            }
            if (!string.IsNullOrEmpty(nodeName))
            {
                builder.AppendFormat(" AND nodename like '%{0}%' ", nodeName);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable GetAuditRemark(int nodeId)
        {
            return publicDbOpClass.DataTableQuary(" select * from wf_instance where NoteID = " + nodeId);
        }

        public static DataTable GetMKMC()
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + " select C_MKDM,(select V_MKMC from pt_mk where C_MKDM =wf_BusinessCode.C_MKDM)as V_MKMC from wf_BusinessCode where not C_MKDM is null group by C_MKDM ");
        }

        public static int GetQueryAgentCount(int templateId, string userCode)
        {
            string str = "";
            str = " select nodeid,mainUser,(select v_xm from pt_yhmc where v_yhdm = mainUser) as auditor,secondUser,bt_Use,(select v_xm from pt_yhmc where v_yhdm = secondUser) as agent from wf_agent ";
            string str2 = str;
            return publicDbOpClass.DataTableQuary(str2 + " where templateId = " + templateId.ToString() + " and MainUser =" + userCode.ToString() + " order by nodeId asc").Rows.Count;
        }

        public static DataTable GetTempLimitUser(string templateid)
        {
            return publicDbOpClass.DataTableQuary(" SELECT DISTINCT UserCode FROM WF_Template_Privilege WHERE TemplateId=" + templateid);
        }

        public static int GetTempNodeCout(string usercode, string templateName, string businessCode, string nodeName)
        {
            StringBuilder builder = new StringBuilder();
            int num = 0;
            string str = GetTempNodeWhere(templateName, businessCode, nodeName);
            builder.AppendFormat("\r\n\t\t\t\tWITH WF_TempNodes AS( \r\n                    --获取单人的流程模板和节点名称\r\n\t                SELECT *,Operater AS Codes\r\n\t                FROM WF_TemplateNode WHERE AuditorType = 1\r\n                ),WF_UserCodes AS(\r\n\t                SELECT WF_TempNodes.*,WF_Template.TemplateName,WF_Template.BusinessCode,\r\n\t\t\t\t\t\tWF_Template.BusinessClass,WF_Template.Remark,WF_Class.BusinessClassName \r\n\t\t\t\t\tFROM WF_TempNodes \r\n\t\t\t\t\tJOIN WF_Template ON  WF_TempNodes.TemplateID=WF_Template.TemplateID\r\n\t\t\t\t\tJOIN WF_Business_Class  AS WF_Class ON WF_Class.BusinessCode=WF_Template.BusinessCode\r\n\t\t\t\t\t\tAND WF_Class.BusinessClass=WF_Template.BusinessClass\r\n                ),TempNodeTable AS( \r\n\t\t\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY TemplateID) AS Num, * \r\n                    FROM WF_UserCodes WHERE Codes LIKE '%{0}%' {1}\r\n                )\r\n\t\t\t\tSELECT COUNT(*) FROM TempNodeTable ", usercode, str);
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                num = Convert.ToInt32(table.Rows[0][0]);
            }
            return num;
        }

        public static DataTable GetTempNodesSouce(string usercode, string templateName, string businessCode, string nodeName, int pagesize, int pageindex)
        {
            StringBuilder builder = new StringBuilder();
            int num = (pagesize * (pageindex - 1)) + 1;
            int num2 = pagesize * pageindex;
            string str = GetTempNodeWhere(templateName, businessCode, nodeName);
            builder.AppendFormat("\r\n\t\t\t\tWITH WF_TempNodes AS( \r\n                    --获取单人流程模板和节点名称\r\n\t                SELECT *,Operater AS Codes\r\n\t                FROM WF_TemplateNode WHERE AuditorType = 1\r\n                ),WF_UserCodes AS(\r\n\t                SELECT WF_TempNodes.*,WF_Template.TemplateName,WF_Template.BusinessCode,\r\n\t\t\t\t\t\tWF_Template.BusinessClass,WF_Template.Remark,WF_Class.BusinessClassName \r\n\t\t\t\t\tFROM WF_TempNodes \r\n\t\t\t\t\tJOIN WF_Template ON  WF_TempNodes.TemplateID = WF_Template.TemplateID\r\n\t\t\t\t\tJOIN WF_Business_Class  AS WF_Class ON WF_Class.BusinessCode = WF_Template.BusinessCode\r\n\t\t\t\t\t\tAND WF_Class.BusinessClass  =WF_Template.BusinessClass\r\n                ),TempNodeTable AS( \r\n\t\t\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY TemplateID) AS Num, * \r\n                    FROM WF_UserCodes WHERE Codes LIKE '%{0}%' {1}\r\n                ) \r\n\t\t\t\tSELECT * FROM TempNodeTable \r\n                WHERE Num BETWEEN {2} AND {3}", new object[] { usercode, str, num, num2 });
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static string GetTempNodeWhere(string templateName, string businessCode, string nodeName)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(templateName))
            {
                builder.AppendFormat(" AND TemplateName like '%{0}%' ", templateName);
            }
            if (!string.IsNullOrEmpty(businessCode))
            {
                builder.AppendFormat("AND BusinessCode ='{0}' ", businessCode);
            }
            if (!string.IsNullOrEmpty(nodeName))
            {
                builder.AppendFormat(" AND NodeName like '%{0}%' ", nodeName);
            }
            return builder.ToString();
        }

        public static bool IsDelTemplate(int templateId)
        {
            if (publicDbOpClass.DataTableQuary("select * from WF_Instance_Main where TemplateID =" + templateId).Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public static bool IsProjectRelated(int templateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT C.ProjectField FROM WF_BusinessCode C \n");
            builder.Append("INNER JOIN WF_Template T ON T.BusinessCode = C.BusinessCode \n");
            builder.AppendFormat("WHERE TemplateID = '{0}'", templateId);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != DBNull.Value);
        }

        public static DataTable QueryAgent(int templateId, string userCode, int pageSize, int pageNo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP(" + pageSize + ") * FROM ( ");
            builder.Append("SELECT ROW_NUMBER() OVER(ORDER BY nodeId ASC) AS Num, nodeid,mainUser,");
            builder.Append("(SELECT v_xm FROM pt_yhmc WHERE v_yhdm = mainUser) AS auditor,secondUser,bt_Use,");
            builder.Append("(SELECT v_xm FROM pt_yhmc WHERE v_yhdm = secondUser) AS agent FROM wf_agent ");
            builder.Append(string.Concat(new object[] { "WHERE templateId = '", templateId, "' and MainUser = '", userCode, "' " }));
            builder.Append(string.Concat(new object[] { " )tab WHERE Num > (", pageNo, " - 1 ) * ", pageSize }));
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static object QueryAuditor(string userCode)
        {
            return publicDbOpClass.ExecuteScalar("select v_xm from pt_yhmc where v_yhdm =  '" + userCode.ToString() + "' ");
        }

        public static DataTable QueryAuditTemplate(string userCode)
        {
            return publicDbOpClass.DataTableQuary(" select *,(select v_xm from pt_yhmc where v_yhdm = '" + userCode + "') as auditor from v_template_auditor where charindex((select ','+v_yhdm+',' from pt_yhmc where v_yhdm = '" + userCode + "'), ','+UserCode+',') != 0 ");
        }

        public static DataTable QueryBusiness(string businessClass, int templateId)
        {
            string str = "select distinct a.* from wf_businesscode a  inner join ";
            string str2 = str + " wf_business_class b  on a.businesscode = b.businesscode inner join " + " wf_Template c on a.businesscode = c.businesscode and b.businessclass = c.businessclass ";
            return publicDbOpClass.DataTableQuary(str2 + " where b.businessclass = '" + businessClass.ToString() + "' and c.templateID = " + templateId.ToString());
        }

        public static DataTable QueryBusinessClass(string businessCode)
        {
            return publicDbOpClass.DataTableQuary("select * from WF_Business_Class where BusinessCode = " + businessCode.ToString());
        }

        public static DataTable QueryBusinessCode()
        {
            string sqlString = "";
            sqlString = "select BusinessCode,BusinessName from wf_BusinessCode ";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable QueryBusinessCode(string MKDM)
        {
            return publicDbOpClass.DataTableQuary("select BusinessCode,BusinessName,C_MKDM from wf_BusinessCode where C_MKDM='" + MKDM + "'");
        }

        public static DataTable QueryChildCorp(string userCode, string types)
        {
            string sqlString = "";
            if (types == "drop")
            {
                sqlString = " select a.v_bmbm,a.v_bmmc from pt_d_bm a where  a.c_sfyx ='y' ";
                sqlString = sqlString + " and a.i_bmdm in (select i_bmdm from PT_d_bm where charindex( ','+Cast(I_bmdm as varchar(30))+',',(select ','+Cast(I_bmdm as varchar(30)) + ',' + OtherDepts+',' from pt_yhmc where v_yhdm = '" + userCode.ToString() + "'),1) != 0)";
            }
            else
            {
                sqlString = " select a.v_bmbm,a.v_bmmc,a.corpCode,(select corpName from PT_d_CorpCode where corpCode = a.corpCode) as corpName from pt_d_bm a where  a.c_sfyx ='y' ";
                sqlString = sqlString + " and a.i_bmdm in (select i_bmdm from PT_d_bm where charindex( ','+Cast(I_bmdm as varchar(30))+',',(select ','+Cast(I_bmdm as varchar(30)) + ',' + OtherDepts+',' from pt_yhmc where v_yhdm = '" + userCode.ToString() + "'),1) != 0)";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable QueryConditionField(int nodeID)
        {
            string str = "";
            str = "select d.*,a.LinkTable,c.NodeID from wf_BusinessCode a,wf_template b,wf_TemplateNode c,v_FieldType d ";
            return publicDbOpClass.DataTableQuary(str + " where a.BusinessCode = b.BusinessCode and b.TemplateID = c.TemplateID and a.linkTable = d.tableName and c.NodeID=" + nodeID.ToString());
        }

        public static DataTable QueryFlowChart(int templateId)
        {
            return publicDbOpClass.DataTableQuary("select * from WF_FlowChart where templateid=" + templateId.ToString() + " order by RowNum asc");
        }

        public static DataRow QueryFlowChart_top1(int templateID, int rowNum)
        {
            return publicDbOpClass.QueryDataRow("select * from WF_FlowChart where RowNum = " + rowNum.ToString() + " and TemplateID =" + templateID.ToString());
        }

        public static DataSet QueryNodeCondition(int nodeID)
        {
            string str = "";
            str = " select NodeID,orderNumber,(case when andor='1' then '与' when andor='0' then '或' else '无' end) as andor,ConditionField,FieldType,";
            return publicDbOpClass.DataSetQuary((str + " (case when ConditionType='1' then '小于等于' when ConditionType='2' then '小于' when ConditionType='3' then '等于'when ConditionType='4' then '大于' when ConditionType='5' then '大于等于' end) as ConditionType, conditionValue ") + " from wf_NodeCondition where NodeID=" + nodeID.ToString() + " order by OrderNumber asc ");
        }

        public static DataTable QueryNodeConditionInfo(int nodeID)
        {
            return publicDbOpClass.DataTableQuary("select * from wf_NodeCondition where NodeID=" + nodeID.ToString() + " order by OrderNumber asc");
        }

        public static DataTable QueryNodeInfo(int templateID, int nodeID, string frontNode)
        {
            return publicDbOpClass.DataTableQuary("select * from wf_templatenode where templateid = " + templateID.ToString() + " and nodeid <> " + nodeID.ToString() + " and FrontNode = '" + frontNode.ToString() + "'");
        }

        public static DataTable QueryNodeList(int templateId)
        {
            return publicDbOpClass.DataTableQuary("select * from wf_templatenode where TemplateID=" + templateId.ToString() + " order by NodeID asc");
        }

        public static DataTable QueryNodeList(int templateId, int nodeId)
        {
            return publicDbOpClass.DataTableQuary("select * from wf_templatenode where TemplateID=" + templateId.ToString() + " and NodeID= " + nodeId.ToString());
        }

        public static DataTable QueryNodeList1(int templateId)
        {
            return publicDbOpClass.DataTableQuary("select a.*,isnull(b.RoleName,'') as RoleName  from wf_templatenode a left join wf_role b on a.Operater=b.roleid where templateid=" + templateId.ToString() + " order by theorder asc");
        }

        public static object QueryNodeName(int nodeId)
        {
            return publicDbOpClass.ExecuteScalar("Select NodeName from wf_templatenode where nodeID = " + nodeId.ToString());
        }

        public static DataTable QueryOffsetNodeFirst(string frontNode, int templateId)
        {
            return publicDbOpClass.DataTableQuary("Select * from wf_templateNode where frontNode = '" + frontNode.ToString() + "' and templateid =" + templateId.ToString());
        }

        public static object QueryOneAgent(int nodeId)
        {
            return publicDbOpClass.ExecuteScalar("select secondUser from wf_agent where nodeId = " + nodeId.ToString());
        }

        public static DataTable QueryOneNode(int nodeId)
        {
            string spName = "\r\n\t\t\t\tSELECT a.*,isnull(b.RoleName,'') as RoleName, d.V_BMMC\r\n\t\t\t\tFROM wf_templatenode a \r\n\t\t\t\tLEFT JOIN wf_role b ON a.Operater = CONVERT(varchar(20),b.roleid) \r\n\t\t\t\tLEFT JOIN PT_d_bm d ON CAST(d.i_bmdm AS nvarchar(200)) IN( a.DepCode)\r\n\t\t\t\tWHERE a.nodeId = @NodeId\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@NodeId", nodeId) };
            return publicDbOpClass.ExecuteDataTable(CommandType.Text, spName, commandParameters);
        }

        public static DataTable QueryOneTemplate(int templateId)
        {
            return publicDbOpClass.DataTableQuary("select * from wf_template where templateId=" + templateId.ToString());
        }

        public static DataRow QueryOrderNumber(int nodeID)
        {
            return publicDbOpClass.QueryDataRow("select max(OrderNumber) as OrderNumber from wf_NodeCondition where NodeID = " + nodeID.ToString());
        }

        public static DataRow QueryOrderNumberList(int nodeID, int orderNumber)
        {
            return publicDbOpClass.QueryDataRow("select * from wf_NodeCondition where NodeID = " + nodeID.ToString() + " and OrderNumber = " + orderNumber.ToString());
        }

        public static DataTable QueryRoleUsers(int roleID)
        {
            return publicDbOpClass.DataTableQuary("select * from wf_roleusers where roleID = " + roleID.ToString() + " order by userCode asc");
        }

        public static DataTable QueryTempByClass(string businessclass, string businesscode, string usercode)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT  * FROM (\r\nselect * from wf_template where IsGeneral='1' and IsAbnormality=0 and BusinessClass ='{0}' and BusinessCode = '{1}' and IsComplete='1' \r\nunion  \r\nselect * from wf_template where IsGeneral='0'and IsAbnormality=0 and  BusinessClass ='{2}' and BusinessCode ='{3}' and IsComplete='1' \r\nand CorpCode = (select a.corpCode from pt_d_bm a where  a.c_sfyx ='y' and a.i_bmdm = (select i_bmdm from pt_yhmc where v_yhdm = '{4}'))\r\n) AS A \r\nWHERE TemplateID IN (SELECT TemplateID FROM WF_Template_Privilege WHERE UserCode='{5}')\r\nORDER BY TemplateID DESC ", new object[] { businessclass, businesscode, businessclass, businesscode, usercode, usercode });
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable QueryTemplateByClass(string businessclass, string businessCode, string userCode)
        {
            string str2 = (" select * from wf_template where IsGeneral='1' and IsAbnormality=0 and BusinessClass = " + businessclass.ToString() + " and BusinessCode = " + businessCode.ToString() + "and IsComplete='1'") + " union ";
            return publicDbOpClass.DataTableQuary((str2 + " select * from wf_template where IsGeneral='0'and IsAbnormality=0 and  BusinessClass = " + businessclass.ToString() + " and BusinessCode = " + businessCode.ToString()) + " and IsComplete='1' and CorpCode = (select a.corpCode from pt_d_bm a where  a.c_sfyx ='y' and a.i_bmdm = (select i_bmdm from pt_yhmc where v_yhdm = '" + userCode.ToString() + "'))");
        }

        public static DataTable QueryTemplateList()
        {
            string sqlString = "";
            sqlString = "select * from wf_template";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable QueryTemplateList(string businessCode, string businessClass, string[] childCorp, string isGeneral)
        {
            string sqlString = "";
            if (isGeneral == "1")
            {
                if ((businessClass == "") || (businessClass == null))
                {
                    sqlString = "select * from wf_template where IsGeneral = '1' and BusinessClass like '%' and BusinessCode = " + businessCode.ToString();
                }
                else
                {
                    sqlString = "select * from wf_template where IsGeneral = '1' and BusinessClass = " + businessClass.ToString() + " and BusinessCode = " + businessCode.ToString() + " ORDER BY TemplateID DESC";
                }
            }
            else if (isGeneral == "2")
            {
                if ((businessClass == "") || (businessClass == null))
                {
                    sqlString = "select * from wf_template where IsGeneral = '0' and BusinessClass like '%' and BusinessCode = " + businessCode.ToString();
                }
                else
                {
                    sqlString = "select * from wf_template where IsGeneral = '0' and BusinessClass = " + businessClass.ToString() + " and BusinessCode = " + businessCode.ToString() + " ORDER BY TemplateID DESC";
                }
            }
            else
            {
                string str2 = "(";
                if (childCorp.Length > 0)
                {
                    for (int i = 0; i < childCorp.Length; i++)
                    {
                        if (i != 0)
                        {
                            str2 = str2 + ",";
                        }
                        str2 = str2 + SqlStringConstructor.GetQuotedString(childCorp[i]);
                    }
                    str2 = str2 + ")";
                }
                if ((businessClass == "") || (businessClass == null))
                {
                    string str3 = "select a.*,b.v_bmmc,'0'as s_bs from wf_template a left join pt_d_bm b on a.CorpCode = b.v_bmbm where a.IsGeneral='0' and a.BusinessCode = '" + businessCode.ToString() + "' and a.BusinessClass like '%' and a.CorpCode in " + str2.ToString();
                    sqlString = str3 + " union select a.*,case when b.v_bmmc is null then a.CorpCode else b.v_bmmc end as v_bmmc,'1' as s_bs from wf_template a left join pt_d_bm b on a.CorpCode = b.v_bmbm where a.IsGeneral='0' and a.BusinessCode = '" + businessCode.ToString() + "' and a.BusinessClass like '%' and a.CorpCode not in " + str2.ToString();
                }
                else
                {
                    string str4 = "select a.*,b.v_bmmc,'0' as s_bs from wf_template a left join pt_d_bm b on a.CorpCode = b.v_bmbm where a.IsGeneral='0' and a.BusinessCode = '" + businessCode.ToString() + "' and a.BusinessClass = '" + businessClass.ToString() + "'and a.CorpCode in " + str2.ToString();
                    sqlString = str4 + " union select a.*,case when b.v_bmmc is null then a.CorpCode else b.v_bmmc end as v_bmmc,'1' as s_bs from wf_template a left join pt_d_bm b on a.CorpCode = b.v_bmbm where a.IsGeneral='0' and a.BusinessCode = '" + businessCode.ToString() + "' and a.BusinessClass = '" + businessClass.ToString() + "'and a.CorpCode not in " + str2.ToString();
                }
                if (childCorp.Length > 0)
                {
                    for (int j = 0; j < childCorp.Length; j++)
                    {
                        if (j == 0)
                        {
                            sqlString = sqlString + " and (";
                        }
                        else
                        {
                            sqlString = sqlString + " or ";
                        }
                        sqlString = sqlString + " a.CorpCode like '" + childCorp[j] + "%'";
                    }
                    sqlString = sqlString + ")";
                }
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static object QueryTemplateName(int templateId)
        {
            return publicDbOpClass.ExecuteScalar("select templateName from wf_template where templateId=" + templateId.ToString());
        }

        public static object QueryTemplateState(int templateId)
        {
            return publicDbOpClass.ExecuteScalar("select IsComplete from wf_template where templateId=" + templateId.ToString());
        }

        public static DataTable QueryTopFlowChart(int templateID, int rowNum)
        {
            return publicDbOpClass.DataTableQuary("select * from WF_FlowChart where RowNum = " + rowNum.ToString() + " and TemplateID =" + templateID.ToString());
        }

        public static bool UpdAgent(Hashtable agentInfo, string where)
        {
            return publicDbOpClass.Update("[wf_agent]", agentInfo, where);
        }

        public static bool UpdFlowChart(Hashtable flowChartInfo, string where)
        {
            return publicDbOpClass.Update("[wf_FlowChart]", flowChartInfo, where);
        }

        public static bool UpdNode(Hashtable nodeInfo, string where)
        {
            return publicDbOpClass.Update("[wf_templatenode]", nodeInfo, where);
        }

        public static bool UpdNode(int nodeId, string nodeName, string operater, string remark, int operaterType)
        {
            string str2 = "";
            return publicDbOpClass.NonQuerySqlString(str2 + " update wf_templatenode set nodename='" + nodeName + "',remark='" + remark + "',operater='" + operater + "',operatertype=" + operaterType.ToString() + " where nodeid=" + nodeId.ToString());
        }

        public static bool UpdNodeCondition(Hashtable nodeConditionInfo, string where)
        {
            return publicDbOpClass.Update("[wf_NodeCondition]", nodeConditionInfo, where);
        }

        public static bool UpdOrder(int nodeId, int theOrder)
        {
            string str2 = "";
            return publicDbOpClass.NonQuerySqlString(str2 + "update wf_templatenode set theOrder=" + theOrder.ToString() + " where nodeId=" + nodeId.ToString());
        }

        public static bool UpdTemplate(Hashtable templateInfo, string where)
        {
            return publicDbOpClass.Update("[wf_template]", templateInfo, where);
        }

        public static bool UpdTemplate(int templateId, string templateName, string remark)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update wf_template set templatename='", templateName, "',remark='", remark, "' where templateId=", templateId }));
        }
    }
}

