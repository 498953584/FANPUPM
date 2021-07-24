namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using com.jwsoft.sysManage.publicStringOperation;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.Configuration;

    public class PMAction
    {
        private static string IsNewProject = WebConfigurationManager.AppSettings["IsNewProject"];

        public static bool AddPrjInfo(string TypeCode, string UserCode, string RecordDate, PMModel PIM, string podepom)
        {
            TypeCode = PublicClass.CheckString(TypeCode);
            int num = int.Parse(publicDbOpClass.QuaryMaxid("PT_PrjInfo", "i_XH")) + 1;
            string str2 = (" begin " + " insert into PT_PrjInfo (TypeCode,i_xh,UserCode,RecordDate,IsValid ,i_childNum,PrjCode,PrjGuid,PrjName,StartDate,EndDate," + "ComputeClass,RationClass,PrjCost,ContractSum,Duration,QualityClass,Area,PrjKindClass,PrjPlace,Remark1,FileName,FileURL,") + "Owner,Counsellor,Designer,Inspector,PrjInfo,PrjState,OwnerCode,MarketInfoGuid,Rank,BudgetWay,ContractWay,PayCondition,TenderWay,PayWay," + "KeyPart,WorkUnit,LinkMan,PrjManager,BuildingType,TotalHouseNum,BuildingArea,UsegrounArea,UndergroundArea,PrjFundInfo,OtherStatement,xmgroup,Podepom";
            object obj2 = str2 + " ) values ('" + TypeCode + "'," + num.ToString() + ",'" + UserCode + "','" + RecordDate + "','1',0,";
            object obj3 = string.Concat(new object[] { obj2, " '", PIM.PrjCode, "','", PIM.PrjGuid, "','", PIM.PrjName, "','", PIM.StartDate, "', " });
            string str3 = string.Concat(new object[] { obj3, " '", PIM.EndDate, "','", PIM.ComputeClass, "','", PIM.RationClass, "', " });
            string str4 = str3 + " '" + PIM.PrjCost + "','" + PIM.ContractSum + "','" + PIM.Duration + "', ";
            string str5 = str4 + " '" + PIM.QualityClass + "','" + PIM.Area + "','" + PIM.PrjKindClass + "', ";
            string str6 = str5 + " '" + PIM.PrjPlace + "','" + PIM.Remark + "','" + PIM.FileName + "','" + PIM.FileURL + "' ,";
            object obj4 = str6 + " '" + PIM.Owner + "','" + PIM.Counsellor + "','" + PIM.Designer + "',";
            string str7 = string.Concat(new object[] { obj4, " '", PIM.Inspector, "','", PIM.PrjInfo, "','", PIM.PrjState, "','", PIM.OwnerCode, "','", Guid.Empty, "'," });
            string str8 = str7 + " '" + PIM.Rank + "','" + PIM.BudgetWay + "','" + PIM.ContractWay + "',";
            string str9 = str8 + " '" + PIM.PayCondition + "','" + PIM.TenderWay + "','" + PIM.PayWay + "',";
            string str10 = str9 + " '" + PIM.KeyPart + "','" + PIM.WorkUnit + "','" + PIM.LinkMan + "',";
            string str11 = str10 + " '" + PIM.PrjManager + "','" + PIM.BuildingType + "','" + PIM.TotalHouseNum + "',";
            string str12 = str11 + " '" + PIM.BuildingArea + "','" + PIM.UsegrounArea + "','" + PIM.UndergroundArea + "',";
            object obj5 = (str12 + " '" + PIM.PrjFundInfo + "','" + PIM.OtherStatement + "','" + PIM.Xmgroup + "','" + podepom + "'") + "\t)";
            string str13 = string.Concat(new object[] { obj5, " INSERT INTO PT_PrjInfo_ZTB_Detail(PrjGuid,IsTender,SetUpFlowState) VALUES('", PIM.PrjGuid, "', '0', '1')" });
            return publicDbOpClass.NonQuerySqlString((str13 + " update PT_PrjInfo set i_childNum = (select count(1) from PT_PrjInfo where (TypeCode like '" + TypeCode.Substring(0, TypeCode.Length - 5) + "%') and (len(TypeCode)= " + TypeCode.Length.ToString() + ")) where TypeCode = '" + TypeCode.Substring(0, TypeCode.Length - 5) + "'") + " end");
        }

        public static bool CheckCode(string Code)
        {
            bool flag = true;
            if (publicDbOpClass.DataTableQuary("select 1 from PT_PrjInfo where PrjCode = '" + Code + "'").Rows.Count > 0)
            {
                flag = false;
            }
            return flag;
        }

        public static string CheckPrjBegin(string TypeCode)
        {
            string str2 = " select PrjGuid from Pt_PrjInfo where TypeCode like '" + TypeCode + "%' and isvalid=1 ";
            string sqlString = "begin ";
            sqlString = (((sqlString + " select count(*) a from EPM_Con_ContractMain where ProjectCode in ( " + str2 + " ) ") + " union ") + " select count(*) a from EPM_Task_TaskList where ProjectCode in ( " + str2 + " ) ") + " end ";
            DataTable table = new DataTable();
            DataRow[] rowArray = publicDbOpClass.DataTableQuary(sqlString).Select("a >0  ", "  a desc");
            if (rowArray.Length > 0)
            {
                int length = rowArray.Length;
                return length.ToString();
            }
            return "0";
        }

        public static bool DelPrjInfo(string TypeCode)
        {
            string str = "begin ";
            string str2 = str + " delete from PT_PrjInfo where TypeCode='" + TypeCode + "'";
            return publicDbOpClass.NonQuerySqlString((str2 + " update PT_PrjInfo set i_childNum = (select count(1) from PT_PrjInfo where (TypeCode like '" + TypeCode.Substring(0, TypeCode.Length - 5) + "%') and (len(TypeCode)= " + TypeCode.Length.ToString() + ")) where TypeCode = '" + TypeCode.Substring(0, TypeCode.Length - 5) + "'") + " end");
        }

        public static bool DelPrjInfoLogic(string TypeCode)
        {
            string str = "begin ";
            return publicDbOpClass.NonQuerySqlString((str + " update PT_PrjInfo set IsValid=0 where TypeCode='" + TypeCode + "'") + " end");
        }

        public static decimal getChildAllpri(string typecode)
        {
            return Convert.ToDecimal(publicDbOpClass.DataTableQuary("select isnull(SUM(PrjCost),0) from PT_PrjInfo where left(typecode,5)='" + typecode.Substring(0, 5) + "' and len(typecode)=10").Rows[0][0].ToString());
        }

        public static decimal getChildAllpriNotReg(string typecode)
        {
            return Convert.ToDecimal(publicDbOpClass.DataTableQuary("select isnull(SUM(PrjCost),0) from PT_PrjInfo where typecode='" + typecode.Substring(0, 2) + "' and len(typecode)=10").Rows[0][0].ToString());
        }

        public static DataTable GetList(string PrjCode)
        {
            string str = " select Prjguid,PrjName from pt_PrjInfo ";
            return publicDbOpClass.DataTableQuary(str + " where PrjCode = '" + PrjCode + "'");
        }

        public static string GetNameByCode(string code)
        {
            return publicDbOpClass.ExecuteScalar("select prjName from pt_PrjInfo where prjguid='" + code + "'").ToString();
        }

        public static decimal getPerpri(string typecode)
        {
            return Convert.ToDecimal(publicDbOpClass.DataTableQuary("select isnull(SUM(PrjCost),0) from PT_PrjInfo where typecode='" + typecode.Substring(0, 5) + "'").Rows[0][0].ToString());
        }

        public static int GetPrjCount(string prjCode, string prjName, string buildUnit, string prjPlace, string startDate, string endDate, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT COUNT(*) FROM (\r\nSELECT Row_Number()over(ORDER BY userDefined_Date DESC,TypeCode ASC) as Num,* FROM \r\n(SELECT \r\nTResult.*\r\n,CASE TResult.i_ChildNum\r\n\tWHEN '0' THEN (\r\n\t\t\t\t\tCASE LEN(TResult.TypeCode)\r\n\t\t\t\t\t\tWHEN '5' THEN TResult.StartDate \r\n\t\t\t\t\t\tELSE (SELECT PT1.StartDate FROM  PT_PrjInfo  AS PT1 WHERE PT1.TypeCode = LEFT(TResult.TypeCode,5) AND i_ChildNum > 0 AND IsValid = '1')\r\n\t\t\t\t\tEND\r\n\t\t\t\t\t)\r\n\tELSE TResult.StartDate\r\nEND AS userDefined_Date\r\nFROM\r\n(select * from PT_PrjInfo where left(TypeCode,5) in (\r\nselect  left (TypeCode,5) from (\r\nSELECT   *  FROM  PT_PrjInfo WHERE  TypeCode in(SELECT left(TYPECODE,5) FROM  PT_PrjInfo WHERE  i_ChildNum=0 and isvalid=1  AND   ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )) \r\nunion SELECT   *  FROM  PT_PrjInfo \r\nWHERE len(typecode)=10 and  i_ChildNum=0 and isvalid=1  AND   ( Podepom LIKE '%{2}%' or  PrjManager LIKE '%{3}%' ) ) as project ", new object[] { userCode, userCode, userCode, userCode }).AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjCode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjCode);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.AppendFormat("and PrjName like '%{0}%'", prjName);
            }
            if (!string.IsNullOrEmpty(buildUnit))
            {
                builder.AppendFormat("and Owner LIKE '%{0}%'", buildUnit);
            }
            if (!string.IsNullOrEmpty(prjPlace))
            {
                builder.AppendFormat("and PrjPlace LIKE '%{0}%'", prjPlace);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND StartDate >= '{0}' ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND EndDate <= '{0}' ", endDate);
            }
            builder.Append(") ) AS TResult) AS tbPrjInfo) AS Tab ");
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar(builder.ToString()));
        }

        public static DataTable GetPrjInfo(string prjCode, string prjName, string buildUnit, string prjPlace, string startDate, string endDate, string userCode, int pageSize, int pageIndex)
        {
            if (pageSize == 0)
            {
                pageSize = GetPrjCount(prjCode, prjName, buildUnit, prjPlace, startDate, endDate, userCode);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT TOP ({0}) * FROM (\r\nSELECT Row_Number()over(ORDER BY userDefined_Date DESC,TypeCode ASC) as Num,* FROM \r\n(SELECT \r\nTResult.*\r\n,CASE TResult.i_ChildNum\r\n\tWHEN '0' THEN (\r\n\t\t\t\t\tCASE LEN(TResult.TypeCode)\r\n\t\t\t\t\t\tWHEN '5' THEN TResult.RecordDate \r\n\t\t\t\t\t\tELSE (SELECT PT1.RecordDate FROM  PT_PrjInfo  AS PT1 WHERE PT1.TypeCode = LEFT(TResult.TypeCode,5) AND i_ChildNum > 0 AND IsValid = '1')\r\n\t\t\t\t\tEND\r\n\t\t\t\t\t)\r\n\tELSE TResult.RecordDate\r\nEND AS userDefined_Date\r\nFROM\r\n(select * from PT_PrjInfo where left(TypeCode,5) in (\r\nselect  left (TypeCode,5) from (\r\nSELECT   *  FROM  PT_PrjInfo WHERE  TypeCode in(SELECT left(TYPECODE,5) FROM  PT_PrjInfo WHERE  i_ChildNum=0 and isvalid=1  AND   ( Podepom LIKE '%{1}%' or  PrjManager LIKE '%{2}%' )) \r\nunion SELECT   *  FROM  PT_PrjInfo \r\nWHERE len(typecode)=10 and  i_ChildNum=0 and isvalid=1  AND   ( Podepom LIKE '%{3}%' or  PrjManager LIKE '%{4}%' ) ) as project ", new object[] { pageSize, userCode, userCode, userCode, userCode }).AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjCode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjCode);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.AppendFormat("and PrjName like '%{0}%'", prjName);
            }
            if (!string.IsNullOrEmpty(buildUnit))
            {
                builder.AppendFormat("and Owner LIKE '%{0}%'", buildUnit);
            }
            if (!string.IsNullOrEmpty(prjPlace))
            {
                builder.AppendFormat("and PrjPlace LIKE '%{0}%'", prjPlace);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND StartDate >= '{0}' ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND EndDate <= '{0}' ", endDate);
            }
            builder.AppendFormat(") ) AS TResult) AS tbPrjInfo) AS Tab WHERE Num > {0} * ({1} - 1)", pageSize, pageIndex);
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static int GetPrjInfoCount(string TypeCode)
        {
            object obj2 = DBNull.Value;
            obj2 = publicDbOpClass.ExecuteScalar("select count(1) from PT_PrjInfo  where TypeCode ='" + TypeCode + "'");
            if (obj2 != DBNull.Value)
            {
                return int.Parse(obj2.ToString());
            }
            return 0;
        }

        public static DataTable GetPrjInfoInfo()
        {
            string sqlString = "";
            sqlString = "select * from PT_PrjInfo where IsValid=1 ";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable GetPrjInfoInfoByTypeCode(string TypeCode)
        {
            return publicDbOpClass.DataTableQuary("select * from PT_PrjInfo where TypeCode ='" + TypeCode + "' ");
        }

        public static string GetprjManager(string TypeCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select prjManager from PT_PrjInfo where TypeCode = '" + TypeCode + "'");
            string str2 = "";
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
                try
                {
                    str2 = str2.Split(new char[] { '-' })[1];
                }
                catch
                {
                }
            }
            return str2;
        }

        public static DataTable GetPrjState(string userCode, int selyear, bool isNewProject)
        {
            string sqlString = string.Empty;
            if (isNewProject)
            {
                sqlString = "SELECT distinct CASE prjstateName WHEN '其他' THEN 1000 ELSE PrjState END AS PrjState,prjstateName FROM ( ";
                string str2 = (sqlString + " SELECT distinct PrjState,") + " ISNULL((SELECT ItemName FROM dbo.Basic_CodeList WHERE TypeCode='ProjectState' AND " + " (ItemCode = PT_PrjInfo.PrjState)),'其他') AS prjstateName ";
                object obj2 = (str2 + " FROM PT_PrjInfo  WHERE IsValid=1 and ( Podepom LIKE '%" + userCode + "%' or  PrjManager LIKE '%" + userCode + "%' )") + " AND  prjState not in('1','17','18') ";
                sqlString = string.Concat(new object[] { obj2, " and ('", selyear.ToString(), "' between year(startDate) and ISNULL(year(enddate),'", selyear, "'))" }) + "  ) prjState ORDER BY PrjState ";
            }
            else
            {
                sqlString = " SELECT distinct PrjState,";
                string str3 = sqlString + " (SELECT     CodeName  FROM     dbo.XPM_Basic_CodeList WHERE  (TypeID = 7) AND (CodeID = PT_PrjInfo.PrjState)) AS prjstateName";
                object obj3 = str3 + " FROM PT_PrjInfo  WHERE IsValid=1 and    ( Podepom LIKE '%" + userCode + "%' or  PrjManager LIKE '%" + userCode + "%' )";
                sqlString = string.Concat(new object[] { obj3, " and ('", selyear.ToString(), "' between year(startDate) and ISNULL(year(enddate),'", selyear, "')) " });
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable GetPrjState(string userCode, int selyear, string prjState)
        {
            string str = string.Empty;
            string str2 = ("SELECT distinct CASE prjstateName WHEN '其他' THEN 1000 ELSE PrjState END AS PrjState,prjstateName FROM ( " + " SELECT distinct PrjState,") + " ISNULL((SELECT ItemName FROM dbo.Basic_CodeList WHERE TypeCode='ProjectState' AND (ItemCode=5 OR ItemCode>=7) AND " + " (ItemCode = PT_PrjInfo.PrjState)),'其他') AS prjstateName ";
            str = str2 + " FROM PT_PrjInfo  WHERE IsValid=1 and ( Podepom LIKE '%" + userCode + "%' or  PrjManager LIKE '%" + userCode + "%' )";
            if (!string.IsNullOrEmpty(prjState))
            {
                str = str + string.Format(" AND  prjState IN ({0}) ", prjState);
            }
            object obj2 = str;
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " and ('", selyear.ToString(), "' between year(startDate) and ISNULL(year(enddate),'", selyear, "'))) prjState ORDER BY PrjState " }));
        }

        public static DataTable GetPrjsubTreebyUserandYear(string userCode, int selyear, string PrjState)
        {
            string str = "";
            if (IsNewProject == "1")
            {
                str = " AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1)";
            }
            if (!string.IsNullOrEmpty(PrjState))
            {
                str = str + string.Format(" AND prjState IN ({0}) ", PrjState);
            }
            else
            {
                str = str + " AND prjState not in('1','2','3','4','6','14','15','16','17','18') ";
            }
            return publicDbOpClass.DataTableQuary(string.Format("\r\n\t\t\t\tSELECT *, Detail.SetUpFlowState FROM (\r\n\t\t\t\t\tSELECT  StartDate, PrjState, PrjGuid,PrjGuid AS prjcode, PrjName AS PrjName , \r\n\t\t\t\t\t\tPrjCode AS PrjCode1, '{0}' AS UserCode,i_ChildNum AS parprj,TypeCode, \r\n\t\t\t\t\t\tCHARINDEX('{0}', Podepom) AS Permission  \r\n\t\t\t\t\tFROM PT_PrjInfo \r\n\t\t\t\t\tWHERE TypeCode IN(\r\n\t\t\t\t\t\tSELECT LEFT(TYPECODE, 5) FROM  PT_PrjInfo \r\n\t\t\t\t\t\tWHERE IsValid=1 AND (Podepom LIKE '%{0}%' OR PrjManager LIKE '%{0}%' ) \r\n\t\t\t\t\t\t\tAND ({2} BETWEEN year(startDate) AND ISNULL(YEAR(enddate),9999))  \r\n\t\t\t\t\t\t\tAND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) \r\n\t\t\t\t\t\t\t{1} \r\n\t\t\t\t\t)  \r\n\t\t\t\t\tUNION\r\n\t\t\t\t\tSELECT StartDate,PrjState, PrjGuid,PrjGuid AS prjcode, PrjName AS PrjName, \r\n\t\t\t\t\t\tPrjCode AS PrjCode1, '{0}' AS UserCode,i_ChildNum AS parprj, \r\n\t\t\t\t\t\tTypeCode, CHARINDEX('{0}', Podepom) AS Permission \r\n\t\t\t\t\tFROM  PT_PrjInfo \r\n\t\t\t\t\tWHERE LEN(typecode)=10 and IsValid=1 \r\n\t\t\t\t\t\tAND (Podepom LIKE '%{0}%' OR PrjManager LIKE '%{0}%')  \r\n\t\t\t\t\t\tAND ({2} BETWEEN year(startDate) AND ISNULL(YEAR(enddate),9999))  \r\n\t\t\t\t\t\tAND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) \r\n\t\t\t\t\t\t{1}\r\n\t\t\t\t) AS PrjInfo \r\n\t\t\t\tLEFT JOIN PT_PrjInfo_ZTB_Detail AS Detail ON PrjInfo.PrjGuid=Detail.PrjGuid\r\n\t\t\t", userCode, str, selyear));
        }

        public static DataTable GetPrjtreebyUser(string UserCode, int jd, string prjcode, string prjname)
        {
            string sqlString = "";
            if (jd == 0)
            {
                sqlString = "";
                string str2 = sqlString;
                sqlString = str2 + "SELECT   *  FROM  PT_PrjInfo WHERE  TypeCode in(SELECT left(TYPECODE,5) FROM  PT_PrjInfo WHERE  i_ChildNum=0  and isvalid=1  AND   ( Podepom LIKE '%" + UserCode + "%' or  PrjManager LIKE '%" + UserCode + "%' )";
                if (!string.IsNullOrEmpty(prjcode))
                {
                    sqlString = sqlString + " and prjcode like '" + prjcode + "' ";
                }
                if (!string.IsNullOrEmpty(prjname))
                {
                    sqlString = sqlString + " and prjname like '" + prjname + "'";
                }
                string str3 = sqlString + ") " + "union SELECT   * ";
                sqlString = str3 + " FROM  PT_PrjInfo WHERE len(typecode)=10 and  i_ChildNum=0 and isvalid=1  AND   ( Podepom LIKE '%" + UserCode + "%' or  PrjManager LIKE '%" + UserCode + "%' ) order by recorddate desc";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable GetPrjTypeCodeid()
        {
            DataTable table = new DataTable();
            string str = "select * from (";
            return publicDbOpClass.DataTableQuary((str + "SELECT '--全部--' as CodeName, 0 as CodeID union " + " SELECT DISTINCT CodeName, CodeID FROM         dbo.XPM_Basic_CodeList ") + "WHERE     (TypeID = 5) AND (IsValid = 1) AND (CodeID IN    (SELECT     PrjKindClass    FROM          dbo.PT_PrjInfo)) " + ")a order by CodeID");
        }

        public static DataTable GetPrjYears(string yhdm)
        {
            return publicDbOpClass.DataTableQuary("\r\n\t\t\t\tSELECT MIN(YEAR(startDate)) as BeginYear,\r\n\t\t\t\t\tMAX(\r\n\t\t\t\t\t\tCASE \r\n\t\t\t\t\t\t\tWHEN EndDate IS NULL THEN YEAR(GETDATE())\r\n\t\t\t\t\t\t\tELSE YEAR(EndDate)\r\n\t\t\t\t\t\tEND\r\n\t\t\t\t\t) AS EndYear \r\n\t\t\t\tFROM dbo.Pt_prjInfo \r\n\t\t\t\tWHERE  (Podepom LIKE '%" + yhdm + "%')");
        }

        public static DataTable GetProject(string userCode, int selyear, bool isNewProject)
        {
            string sqlString = string.Empty;
            if (isNewProject)
            {
                sqlString = "SELECT CASE prjstateName WHEN '其他' THEN 1000 ELSE PrjState END AS PrjState,PrjGuid, prjcode,PrjName,PrjCode1,UserCode,prjstateName FROM ( \n";
                string str2 = ((sqlString + " SELECT PrjState, PrjGuid,PrjGuid as prjcode, PrjName as PrjName , PrjCode as PrjCode1, '" + userCode + "' as UserCode, \n") + " ISNULL((SELECT ItemName FROM dbo.Basic_CodeList WHERE TypeCode='ProjectState' \n") + " AND (ItemCode = PT_PrjInfo.PrjState)),'其他') AS prjstateName,StartDate \n" + " FROM  PT_PrjInfo WHERE IsValid=1 AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) \n ";
                object obj2 = (str2 + " AND  ( Podepom LIKE '%" + userCode + "%' or PrjManager LIKE '%" + userCode + "%' ) \n") + " AND  prjState not in('1','17','18') \n";
                sqlString = string.Concat(new object[] { obj2, " and ('", selyear.ToString(), "' between year(startDate) and ISNULL(year(enddate),'", selyear, "')) \n" }) + " )tabPrj  ORDER BY StartDate DESC \n";
            }
            else
            {
                string str3 = ("SELECT     PrjState, PrjGuid,PrjGuid as prjcode, PrjName as PrjName , PrjCode as PrjCode1, '" + userCode + "' as UserCode,") + " (SELECT     CodeName  FROM     dbo.XPM_Basic_CodeList WHERE  (TypeID = 7) AND (CodeID = PT_PrjInfo.PrjState)) AS prjstateName";
                object obj3 = str3 + " FROM  PT_PrjInfo WHERE IsValid=1 AND ( Podepom LIKE '%" + userCode + "%' or  PrjManager LIKE '%" + userCode + "%' )";
                sqlString = string.Concat(new object[] { obj3, " and ('", selyear.ToString(), "' between year(startDate) and ISNULL(year(enddate),'", selyear, "')) ORDER BY StartDate DESC" });
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable GetProject(string userCode, int selyear, string prjState)
        {
            string str = string.Empty;
            string str2 = ("SELECT CASE prjstateName WHEN '其他' THEN 1000 ELSE PrjState END AS PrjState,PrjGuid, prjcode,PrjName,PrjCode1,UserCode,prjstateName FROM (" + " SELECT PrjState, PrjGuid,PrjGuid as prjcode, PrjName as PrjName , PrjCode as PrjCode1, '" + userCode + "' as UserCode, ") + " ISNULL((SELECT ItemName FROM dbo.Basic_CodeList WHERE TypeCode='ProjectState' AND  (ItemCode=5 OR ItemCode>=7)  AND " + " (ItemCode = PT_PrjInfo.PrjState)),'其他') AS prjstateName,StartDate ";
            str = str2 + " FROM  PT_PrjInfo WHERE IsValid=1 AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) AND   ( Podepom LIKE '%" + userCode + "%' or PrjManager LIKE '%" + userCode + "%' )";
            if (!string.IsNullOrEmpty(prjState))
            {
                str = str + string.Format(" AND  prjState IN ({0}) ", prjState);
            }
            object obj2 = str;
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " and ('", selyear.ToString(), "' between year(startDate) and ISNULL(year(enddate),'", selyear, "')))tabPrj  ORDER BY StartDate DESC " }));
        }

        public static DataTable GetProjectInfo(int intPrjState)
        {
            DataTable table = new DataTable();
            string sqlString = " select * from Pt_PrjInfo where PrjState='" + intPrjState + "' ";
            try
            {
                table = publicDbOpClass.DataTableQuary(sqlString);
            }
            catch
            {
            }
            return table;
        }

        public static DataTable GetProjectYears()
        {
            string sqlString = "\r\n\t\t\t\tSELECT MIN(YEAR(startDate)) as BeginYear,\r\n\t\t\t\t\tMAX(\r\n\t\t\t\t\t\tCASE \r\n\t\t\t\t\t\t\tWHEN EndDate IS NULL THEN YEAR(GETDATE())\r\n\t\t\t\t\t\t\tELSE YEAR(EndDate)\r\n\t\t\t\t\t\tEND\r\n\t\t\t\t\t) AS EndYear \r\n\t\t\t\tFROM dbo.Pt_prjInfo \r\n\t\t\t";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable GetSingle(string Code)
        {
            return publicDbOpClass.DataTableQuary("select * from PT_PrjInfo where  TypeCode = '" + Code + "'");
        }

        public static DataTable GetSingleByPrjGuid(string PrjGuid)
        {
            return publicDbOpClass.DataTableQuary("select * from PT_PrjInfo where  PrjGuid = '" + PrjGuid + "'");
        }

        public static PMModel GetSingleInfo(string Code)
        {
            PMModel model = new PMModel();
            foreach (DataRow row in GetSingle(Code).Rows)
            {
                model.PrjCode = row["PrjCode"].ToString();
                if (row["PrjGuid"].ToString() != "")
                {
                    model.PrjGuid = new Guid(row["PrjGuid"].ToString());
                }
                model.PrjName = row["PrjName"].ToString();
                try
                {
                    model.StartDate = (DateTime) row["StartDate"];
                    model.EndDate = (DateTime) row["EndDate"];
                }
                catch (Exception)
                {
                }
                model.ComputeClass = row["ComputeClass"].ToString();
                model.RationClass = row["RationClass"].ToString();
                model.PrjCost = row["PrjCost"].ToString();
                model.ContractSum = row["ContractSum"].ToString();
                model.Duration = row["Duration"].ToString();
                model.QualityClass = row["QualityClass"].ToString();
                model.Area = row["Area"].ToString();
                model.PrjKindClass = row["PrjKindClass"].ToString();
                model.PrjPlace = row["PrjPlace"].ToString();
                model.Remark = row["Remark1"].ToString();
                model.FileName = row["FileName"].ToString();
                model.FileURL = row["FileURL"].ToString();
                model.Owner = row["Owner"].ToString();
                model.Counsellor = row["Counsellor"].ToString();
                model.Designer = row["Designer"].ToString();
                model.Inspector = row["Inspector"].ToString();
                model.PrjInfo = row["PrjInfo"].ToString();
                model.PrjState = row["PrjState"].ToString();
                model.OwnerCode = row["OwnerCode"].ToString();
                model.Rank = row["Rank"].ToString();
                model.BudgetWay = row["BudgetWay"].ToString();
                model.ContractWay = row["ContractWay"].ToString();
                model.PayCondition = row["PayCondition"].ToString();
                model.TenderWay = row["TenderWay"].ToString();
                model.PayWay = row["PayWay"].ToString();
                model.KeyPart = row["KeyPart"].ToString();
                model.WorkUnit = row["WorkUnit"].ToString();
                model.LinkMan = row["LinkMan"].ToString();
                model.PrjManager = row["PrjManager"].ToString();
                model.BuildingType = row["BuildingType"].ToString();
                model.TotalHouseNum = row["TotalHouseNum"].ToString();
                model.BuildingArea = row["BuildingArea"].ToString();
                model.UsegrounArea = row["UsegrounArea"].ToString();
                model.UndergroundArea = row["UndergroundArea"].ToString();
                model.PrjFundInfo = row["PrjFundInfo"].ToString();
                model.OtherStatement = row["OtherStatement"].ToString();
                model.Xmgroup = row["xmgroup"].ToString();
            }
            return model;
        }

        public static PMModel GetSingleInfoByPrjGuid(string PrjGuid)
        {
            PMModel model = new PMModel();
            foreach (DataRow row in GetSingleByPrjGuid(PrjGuid).Rows)
            {
                model.PrjCode = row["PrjCode"].ToString();
                if (row["PrjGuid"].ToString() != "")
                {
                    model.PrjGuid = new Guid(row["PrjGuid"].ToString());
                }
                model.PrjName = row["PrjName"].ToString();
                try
                {
                    model.StartDate = (DateTime) row["StartDate"];
                    model.EndDate = (DateTime) row["EndDate"];
                }
                catch (Exception)
                {
                }
                model.ComputeClass = row["ComputeClass"].ToString();
                model.RationClass = row["RationClass"].ToString();
                model.PrjCost = row["PrjCost"].ToString();
                model.ContractSum = row["ContractSum"].ToString();
                model.Duration = row["Duration"].ToString();
                model.QualityClass = row["QualityClass"].ToString();
                model.Area = row["Area"].ToString();
                model.PrjKindClass = row["PrjKindClass"].ToString();
                model.PrjPlace = row["PrjPlace"].ToString();
                model.Remark = row["Remark1"].ToString();
                model.FileName = row["FileName"].ToString();
                model.FileURL = row["FileURL"].ToString();
                model.Owner = row["Owner"].ToString();
                model.Counsellor = row["Counsellor"].ToString();
                model.Designer = row["Designer"].ToString();
                model.Inspector = row["Inspector"].ToString();
                model.PrjInfo = row["PrjInfo"].ToString();
                model.PrjState = row["PrjState"].ToString();
                model.OwnerCode = row["OwnerCode"].ToString();
                model.Rank = row["Rank"].ToString();
                model.BudgetWay = row["BudgetWay"].ToString();
                model.ContractWay = row["ContractWay"].ToString();
                model.PayCondition = row["PayCondition"].ToString();
                model.TenderWay = row["TenderWay"].ToString();
                model.PayWay = row["PayWay"].ToString();
                model.KeyPart = row["KeyPart"].ToString();
                model.WorkUnit = row["WorkUnit"].ToString();
                model.LinkMan = row["LinkMan"].ToString();
                model.PrjManager = row["PrjManager"].ToString();
                model.BuildingType = row["BuildingType"].ToString();
                model.TotalHouseNum = row["TotalHouseNum"].ToString();
                model.BuildingArea = row["BuildingArea"].ToString();
                model.UsegrounArea = row["UsegrounArea"].ToString();
                model.UndergroundArea = row["UndergroundArea"].ToString();
                model.PrjFundInfo = row["PrjFundInfo"].ToString();
                model.OtherStatement = row["OtherStatement"].ToString();
                model.Xmgroup = row["xmgroup"].ToString();
            }
            return model;
        }

        public static string gettypeName(string codeid, int typeid)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(codeid))
            {
                DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "SELECT CodeName FROM  XPM_Basic_CodeList WHERE (CodeID = ", codeid, ") AND (TypeID = ", typeid, ") AND (IsValid = 1)" }));
                if (table.Rows.Count > 0)
                {
                    str = table.Rows[0][0].ToString();
                }
            }
            return str;
        }

        public static DataTable GetWinBidProject(string userCode, int year)
        {
            return publicDbOpClass.DataTableQuary(string.Format("\r\n                    SELECT YEAR(vTender.StartDate) Start, ISNULL(YEAR(vTender.EndDate), 2079) Endd, * FROM vTender\r\n                    LEFT JOIN Pt_PrjInfo info ON vTender.PrjGuid=info.PrjGuid\r\n                    WHERE vTender.PrjState = 5\t\t\t\t\t\t\t\t--中标的\r\n                        AND EXISTS(SELECT * FROM PT_PrjInfo_ZTB_User \r\n                            WHERE UserCode = '{0}' AND PrjGuid = vTender.PrjGuid)\t--有权限的\r\n                        AND YEAR(vTender.StartDate) <= {1}\t\t\t\t--开始时间大于等于指定年份\r\n                        AND ISNULL(YEAR(vTender.EndDate), 2079) >= {1}\t--结束时间小于等于指定年份\r\n\t                    AND info.PrjGuid IS NULL \r\n                    ORDER BY InputDate", userCode, year));
        }

        public static string GETYHDMS(string SubPrjReg)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select Podepom from PT_PrjInfo where TypeCode = '" + SubPrjReg + "'");
            string str2 = "";
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0]["Podepom"].ToString().TrimEnd(new char[] { ',' });
            }
            return str2;
        }

        public static string GETYHMCS(string yhdms)
        {
            string str = "";
            foreach (DataRow row in userManageDb.GetUserInfoByYhdm(yhdms).Rows)
            {
                str = str + row["v_xm"].ToString() + ",";
            }
            if (str.Trim() != "")
            {
                str = str.Trim(new char[] { ',' });
            }
            return str;
        }

        public static string MakeClassCode(string Code)
        {
            string str = "";
            int num = Code.Length + 5;
            StringBuilder builder = new StringBuilder();
            if (Code == "")
            {
                builder.Append(" select max(TypeCode) from PT_PrjInfo where len(TypeCode)=5");
            }
            else
            {
                builder.Append(" select max(TypeCode) from PT_PrjInfo where TypeCode like '" + Code + "%' and len(TypeCode)=" + num.ToString());
            }
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                str = (string) obj2;
                str = Convert.ToString((int) (int.Parse(str.Substring(str.Length - 5, 5)) + 1)).PadLeft(5, '0');
                return (Code.Trim() + str);
            }
            return (Code.Trim() + "00001");
        }

        public static int SwitchState(Guid code, int state)
        {
            return publicDbOpClass.ExecSqlString(" update PT_PrjInfo set PrjState = " + state.ToString() + " where PrjGuid = '" + code.ToString() + "'");
        }

        public static int SwitchStateRemark(Guid code, string remark)
        {
            return publicDbOpClass.ExecSqlString(" update PT_PrjInfo set PrjStateRemark ='" + remark.ToString() + "' where PrjGuid = '" + code.ToString() + "'");
        }

        public static bool UpCheckCode(string oldCode, string newCode)
        {
            bool flag = true;
            if (publicDbOpClass.DataTableQuary("select * from PT_PrjInfo where PrjCode  not in (select PrjCode from PT_PrjInfo where PrjCode='" + oldCode + "') AND PrjCode='" + newCode + "' ").Rows.Count > 0)
            {
                flag = false;
            }
            return flag;
        }

        public static int UpdatePodepom(string Yhdms, string SubPrjReg)
        {
            return publicDbOpClass.ExecSqlString((" update PT_PrjInfo set Podepom = '" + Yhdms + "' ") + " where TypeCode = '" + SubPrjReg + "'");
        }

        public static bool UpdPrjInfo(string TypeCode, string UserCode, string RecordDate, PMModel PIM)
        {
            string str = "";
            TypeCode = PublicClass.CheckString(TypeCode);
            str = " begin ";
            string str2 = str;
            object obj2 = str2 + " update PT_PrjInfo set UserCode='" + UserCode + "',RecordDate ='" + RecordDate + "',";
            string str3 = string.Concat(new object[] { 
                obj2, "PrjName='", PIM.PrjName, "',StartDate = '", PIM.StartDate, "',EndDate = '", PIM.EndDate, "',ComputeClass = '", PIM.ComputeClass, "',RationClass ='", PIM.RationClass, "',PrjCost ='", PIM.PrjCost, "',ContractSum = '", PIM.ContractSum, "',Duration = '", 
                PIM.Duration, "',"
             });
            string str4 = str3 + "QualityClass =  '" + PIM.QualityClass + "',Area = '" + PIM.Area + "',PrjKindClass = '" + PIM.PrjKindClass + "',PrjPlace = '" + PIM.PrjPlace + "',Remark1 = '" + PIM.Remark + "',";
            object obj3 = str4 + "FileName = '" + PIM.FileName + "',FileURL = '" + PIM.FileURL + "',Owner =  '" + PIM.Owner + "',Counsellor = '" + PIM.Counsellor + "',Designer = '" + PIM.Designer + "',";
            string str5 = string.Concat(new object[] { obj3, "[Inspector] =  '", PIM.Inspector, "',[PrjInfo] = '", PIM.PrjInfo, "',[PrjState] = '", PIM.PrjState, "',[OwnerCode] = '", PIM.OwnerCode, "',[MarketInfoGuid] = '", Guid.Empty, "'," });
            string str6 = str5 + "[Rank] = '" + PIM.Rank + "',[BudgetWay] = '" + PIM.BudgetWay + "',[ContractWay] = '" + PIM.ContractWay + "',[PayCondition] = '" + PIM.PayCondition + "',[TenderWay] ='" + PIM.TenderWay + "',";
            string str7 = str6 + "[PayWay] = '" + PIM.PayWay + "',[KeyPart] = '" + PIM.KeyPart + "',[WorkUnit] = '" + PIM.WorkUnit + "',[LinkMan] = '" + PIM.LinkMan + "',[PrjManager] = '" + PIM.PrjManager + "',";
            string str8 = str7 + "[BuildingType] = '" + PIM.BuildingType + "',[TotalHouseNum] = '" + PIM.TotalHouseNum + "',[BuildingArea] =  '" + PIM.BuildingArea + "',[UsegrounArea] = '" + PIM.UsegrounArea + "',";
            return publicDbOpClass.NonQuerySqlString(((str8 + "UndergroundArea = '" + PIM.UndergroundArea + "',PrjFundInfo = '" + PIM.PrjFundInfo + "',OtherStatement = '" + PIM.OtherStatement + "',xmgroup='" + PIM.Xmgroup + "'") + "  where TypeCode='" + TypeCode + "'") + " end ");
        }
    }
}

