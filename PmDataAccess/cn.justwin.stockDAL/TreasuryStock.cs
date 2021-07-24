namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class TreasuryStock
    {
        private static string StuffCBSCode = ConfigHelper.Get("StuffCBSCode");

        public int Add(TreasuryStockModel model)
        {
            SqlParameter[] parameterArray;
            string cmdText = string.Empty;
            this.PrepareAddCommand(model, out cmdText, out parameterArray);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, parameterArray);
        }

        public int Add(SqlTransaction trans, TreasuryStockModel model)
        {
            SqlParameter[] parameterArray;
            string cmdText = string.Empty;
            this.PrepareAddCommand(model, out cmdText, out parameterArray);
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, parameterArray);
        }

        public object AlarmMethod(string tcode, string scode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sum(snumber) as number ");
            builder.Append(" from dbo.Sm_Treasury_Stock ");
            builder.Append("where tcode=@tcode and scode=@scode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tcode", tcode), new SqlParameter("@scode", scode) };
            return SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string tsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Treasury_Stock ");
            builder.Append(" where tsid=@tsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tsid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = tsid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public bool Delete(string tcode, IList<string> scodeList)
        {
            bool flag = true;
            try
            {
                foreach (string str in scodeList)
                {
                    string cmdText = string.Format("DELETE FROM Sm_Treasury_Stock WHERE tcode = '{0}' AND scode = '{1}'", tcode, str);
                    SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[0]);
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public int DeleteByWhere(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Treasury_Stock ");
            builder.Append(strWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public int GetCount(string tcode)
        {
            string cmdText = string.Format("SELECT COUNT(*) FROM Sm_Treasury_Stock WHERE tcode='{0}'", tcode);
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]));
        }

        public DataTable GetGoods(string tcode, int pageSize, int pageNo)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                WITH Goods AS(\r\n\t                SELECT ROW_NUMBER() OVER (ORDER BY ResourceCode,sprice ) AS No\r\n\t\t                ,ResourceCode,ResourceName,sprice,Specification\r\n\t\t                ,Res_Unit.Name AS UnitName\r\n\t\t                ,XPM_Basic_ContactCorp.CorpId,XPM_Basic_ContactCorp.CorpName\r\n                        ,Res_Resource.Brand,ModelNumber,TechnicalParameter\r\n\t\t                ,SUM(snumber) AS SNumber\r\n\t\t                ,SUM(CAST(sprice*snumber AS DECIMAL(18,3))) AS Total\r\n\t                FROM Sm_Treasury_Stock AS STS\r\n\t                JOIN Res_Resource ON STS.scode=Res_Resource.ResourceCode\r\n\t                LEFT JOIN Res_Unit ON Res_Resource.Unit=Res_Unit.UnitID\r\n\t                LEFT JOIN XPM_Basic_ContactCorp ON STS.corp=XPM_Basic_ContactCorp.CorpID\r\n\t                WHERE STS.tcode=@tcode\r\n\t                GROUP BY ResourceCode,ResourceName,sprice,Specification,Name,CorpId,CorpName\r\n                    ,Res_Resource.Brand,ModelNumber,TechnicalParameter\r\n                )\r\n                SELECT * FROM Goods WHERE No BETWEEN @start AND @end");
            int num = ((pageNo - 1) * pageSize) + 1;
            int num2 = pageNo * pageSize;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tcode", tcode), new SqlParameter("@start", num), new SqlParameter("@end", num2) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int GetGoodsCount(string tcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                SELECT COUNT(*)\r\n                FROM(\r\n                SELECT ResourceCode,ResourceName,sprice,Specification,Name,CorpName,corp \r\n                FROM Sm_Treasury_Stock AS STS\r\n                JOIN Res_Resource ON STS.scode=Res_Resource.ResourceCode\r\n                LEFT JOIN Res_Unit ON Res_Resource.Unit=Res_Unit.UnitID\r\n                LEFT JOIN XPM_Basic_ContactCorp ON STS.corp=XPM_Basic_ContactCorp.CorpID\r\n                WHERE STS.tcode=@tcode\r\n                GROUP BY ResourceCode,ResourceName,sprice,Specification,Name,CorpName,corp\r\n\t            ) Res");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tcode", tcode) };
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
            return ((obj2 == null) ? 0 : Convert.ToInt32(obj2.ToString()));
        }

        public List<TreasuryStockModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select tsid,scode,tcode,sprice,snumber,isfirst,corp,incode,intime,intype ");
            builder.Append(" FROM Sm_Treasury_Stock ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<TreasuryStockModel> list = new List<TreasuryStockModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public DataTable GetListArrayByParam(string[] scodes, string[] resourceNames, string corp, string tcode)
        {
            int num = 0;
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            builder.Append(" select p6.scode,p6.sprice,p6.snumber,p6.corp,p4.ResourceName,p4.Brand,ModelNumber,TechnicalParameter,");
            builder.Append(" p4.Specification,p2.Name,p5.CorpName from ");
            builder.Append(" (select p2.scode,p2.sprice,sum(p2.snumber) as snumber,p2.Corp");
            builder.Append(" from  dbo.Sm_Treasury_Stock as p2  ");
            builder.Append(" where p2.tcode in(" + tcode + ") group by scode,sprice,Corp) as p6");
            if (!string.IsNullOrEmpty(corp))
            {
                builder.Append(" join dbo.XPM_Basic_ContactCorp as p5 on p5.corpid=p6.Corp ");
                builder.Append(" and p5.CorpName LIKE @corp");
                list.Add(new SqlParameter("@corp", '%' + corp + '%'));
            }
            else
            {
                builder.Append(" left join dbo.XPM_Basic_ContactCorp as p5 on p5.corpid=p6.Corp ");
            }
            builder.Append(" join dbo.Res_Resource as p4 on p4.resourceCode=p6.scode");
            builder.Append(" left join Res_Unit as p2 on p4.Unit=p2.UnitID  where 1=1 ");
            for (int i = 0; i < scodes.Length; i++)
            {
                if (!string.IsNullOrEmpty(scodes[i]))
                {
                    num = 1;
                    if (i == 0)
                    {
                        builder.Append(" and ( p6.scode like @scode" + i);
                    }
                    else
                    {
                        builder.Append(" or  p6.scode like @scode" + i);
                    }
                    list.Add(new SqlParameter("@scode" + i, "%" + scodes[i] + "%"));
                }
            }
            if (num == 1)
            {
                builder.Append(")");
                num = 0;
            }
            for (int j = 0; j < resourceNames.Length; j++)
            {
                if (!string.IsNullOrEmpty(resourceNames[j]))
                {
                    num = 1;
                    if (j == 0)
                    {
                        builder.Append(" and (p4.resourceName like @name" + j);
                    }
                    else
                    {
                        builder.Append(" or p4.resourceName like @name" + j);
                    }
                    list.Add(new SqlParameter("@name" + j, "%" + resourceNames[j] + "%"));
                }
            }
            if (num == 1)
            {
                builder.Append(")");
                num = 0;
            }
            builder.Append(" ORDER BY scode DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public DataTable GetListArrayByTcode(string tcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select p6.scode,p6.sprice,p6.snumber,p6.corp,p4.ResourceName,");
            builder.Append(" p4.Specification,p2.Name,p5.CorpName,p4.Brand,ModelNumber,TechnicalParameter from ");
            builder.Append(" (select p2.scode,p2.sprice,sum(p2.snumber) as snumber,p2.Corp");
            builder.Append(" from  dbo.Sm_Treasury_Stock as p2  ");
            builder.Append(" where p2.tcode in(" + tcode + ") group by scode,sprice,Corp) as p6");
            builder.Append(" left join dbo.XPM_Basic_ContactCorp as p5 on p5.corpid=p6.Corp ");
            builder.Append(" join dbo.Res_Resource as p4 on p4.resourceCode=p6.scode");
            builder.Append(" left join Res_Unit as p2 on p4.Unit=p2.UnitID ");
            builder.Append(" ORDER BY scode DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public DataTable GetListByTsid(string tsid)
        {
            if (string.IsNullOrEmpty(tsid))
            {
                tsid = "''";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("select distinct 0.000 as number,p2.scode,p2.sprice,(0*p2.sprice) Total, \n");
            builder.Append("\tISNULL(p2.Corp,'') as CorpId ,p5.corpName as Corp,p0.snumber,p1.ResourceName,p1.Specification,p4.Name \n");
            builder.Append(",p1.Brand,ModelNumber,TechnicalParameter   \n");
            builder.Append("from dbo.Res_Resource as p1  \n");
            builder.Append("left join dbo.Sm_Treasury_Stock as p2 on p1.resourceCode=p2.scode  \n");
            builder.Append("left join Res_Unit as p4 on p1.Unit=p4.UnitID   \n");
            builder.Append("join (select scode,sum(snumber) as snumber,sprice,ISNULL(corp,'') corp   \n");
            builder.Append("\t  from ( select p2.*,p1.ResourceName,p1.Specification,p4.Name  \n");
            builder.Append("\t\t\t from dbo.Res_Resource as p1  \n");
            builder.Append("\t\t\t left join dbo.Sm_Treasury_Stock as p2 on p1.resourceCode=p2.scode  \n");
            builder.Append("\t\t\t left join Res_Unit as p4 on p1.Unit=p4.UnitID  \n");
            builder.Append("\t\t\t where p2.tsid in(" + tsid + ") ) a \n");
            builder.Append("\t  group by scode,sprice,Corp) as p0  \n");
            builder.Append("\ton p2.scode=p0.scode and p2.sprice=p0.sprice and ISNULL(p2.corp,'') = p0.corp  \n");
            builder.Append("left join dbo.XPM_Basic_ContactCorp as p5 on ISNULL(p2.corp,'') = p5.CorpId  \n");
            builder.Append("where p2.tsid in(" + tsid + ") \n");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public DataTable GetListByTsid1(string tsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select p2.*,p1.ResourceName,");
            builder.Append("p1.Specification,p4.Name");
            builder.Append(" from dbo.Res_Resource as p1 ");
            builder.Append(" join dbo.Sm_Treasury_Stock as p2 on p1.resourceCode=p2.scode");
            builder.Append(" join Res_Unit as p4 on p1.Unit=p4.UnitID");
            builder.Append(" where p2.tsid in(" + tsid + ")");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public TreasuryStockModel GetModel(string tsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select tsid,scode,tcode,sprice,snumber,isfirst,corp,incode,intime,intype from Sm_Treasury_Stock ");
            builder.Append(" where tsid=@tsid ");
            TreasuryStockModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@tsid", tsid) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public decimal GetNumber(string code)
        {
            string cmdText = string.Format("SELECT SUM(snumber) AS Number FROM Sm_Treasury_Stock WHERE scode = '{0}'", code);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]);
            if (string.IsNullOrEmpty(obj2.ToString()))
            {
                obj2 = 0;
            }
            return Convert.ToDecimal(obj2);
        }

        public decimal GetNumber(string code, string tcode)
        {
            string cmdText = string.Format("SELECT SUM(snumber) AS Number FROM Sm_Treasury_Stock WHERE scode = '{0}' AND tcode='{1}'", code, tcode);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]);
            if (string.IsNullOrEmpty(obj2.ToString()))
            {
                obj2 = 0;
            }
            return Convert.ToDecimal(obj2);
        }

        public DataTable GetReportDataSource(string condition, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select row_number() over(order by tsid) as RowNumber, tsid, scode,ResourceName, ").AppendLine();
            builder.Append("Sm_Treasury_Stock.tcode,tname,Specification,Name,sprice, snumber,").AppendLine();
            builder.Append("Convert(decimal(18,3),sprice*snumber) as total, isfirst, corp,CorpName, incode, convert(nvarchar(10),intime,121) as intime, intype ").AppendLine();
            builder.Append(",Res_Resource.TechnicalParameter,Res_Resource.ModelNumber,Res_Resource.Brand ").AppendLine();
            builder.Append("from Sm_Treasury_Stock ").AppendLine();
            builder.Append("join Sm_Treasury on Sm_Treasury.tcode = Sm_Treasury_Stock.tcode ").AppendLine();
            builder.Append("join Res_Resource on Res_Resource.ResourceCode = Sm_Treasury_Stock.scode ").AppendLine();
            builder.Append("join Res_Unit on Res_Resource.Unit = Res_Unit.UnitID ").AppendLine();
            builder.Append("left join XPM_Basic_ContactCorp on Sm_Treasury_Stock.corp = XPM_Basic_ContactCorp.CorpID ").AppendLine();
            builder.Append("join Res_ResourceType on Res_ResourceType.ResourceTypeId = Res_Resource.ResourceType ").AppendLine();
            builder.Append("where snumber != 0 ").AppendLine();
            builder.AppendFormat(" \r\n\t\t\t\t\t\t\tAND Sm_Treasury.TCode IN \r\n\t\t\t\t\t\t\t(\r\n\t\t\t\t\t\t\t\t--仓库权限\r\n\t\t\t\t\t\t\t\tSELECT tcode FROM Sm_Treasury_Permit \r\n\t\t\t\t\t\t\t\tWHERE ptype='Person'  AND pcode='{0}'\r\n\t\t\t\t\t\t\t\tUNION\r\n\t\t\t\t\t\t\t\tSELECT tcode FROM Sm_Treasury_Permit permit\r\n\t\t\t\t\t\t\t\tINNER JOIN Pt_YHMC yhmc ON permit.pcode=i_bmdm\r\n\t\t\t\t\t\t\t\tWHERE ptype='Department' and v_yhdm='{0}'\r\n\t\t\t\t\t\t\t\tUNION\r\n\t\t\t\t\t\t\t\tSELECT tcode FROM Sm_Treasury_Permit permit\r\n\t\t\t\t\t\t\t\tINNER JOIN Pt_YHMC yhmc ON permit.pcode=i_DutyId\r\n\t\t\t\t\t\t\t\tWHERE ptype='Post' and v_yhdm='{0}'\r\n\t\t\t\t\t\t\t) ", userCode).AppendLine();
            builder.Append(condition).AppendLine();
            builder.Append(" order by intime desc").AppendLine();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetReportDataSource(DateTime startDate, DateTime endDate, string resourceNames, string resourceCodes, string corpName, string treasuryName, string category, string userCode, string specification, string brand, string modelNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("and intime between '{0}' and '{1}' ", startDate.ToShortDateString(), endDate.ToShortDateString());
            builder.Append(DBHelper.GetQuerySql("ResourceName", resourceNames)).Append(" ");
            builder.Append(DBHelper.GetQuerySql("scode", resourceCodes)).Append(" ");
            if (!string.IsNullOrEmpty(corpName))
            {
                builder.AppendFormat("and CorpName like '%{0}%'", corpName);
            }
            if (!string.IsNullOrEmpty(treasuryName))
            {
                builder.AppendFormat("and tname like '%{0}%'", treasuryName);
            }
            if (!string.IsNullOrEmpty(category))
            {
                builder.AppendFormat("and CategoryName like '%{0}%' ", category);
            }
            if (!string.IsNullOrEmpty(specification))
            {
                builder.AppendFormat("and Specification like '%{0}%' ", specification);
            }
            if (!string.IsNullOrEmpty(brand))
            {
                builder.AppendFormat("and Res_Resource.Brand like '%{0}%' ", brand);
            }
            if (!string.IsNullOrEmpty(modelNumber))
            {
                builder.AppendFormat("and Res_Resource.ModelNumber like '%{0}%' ", modelNumber);
            }
            return this.GetReportDataSource(builder.ToString(), userCode);
        }

        public int GetStuffCount(string resourceCode, string resourceName, string prjId, string isWBSRelevance, string specification, string brand, string modelNumber)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(resourceCode))
            {
                builder.AppendFormat(" AND ResourceCode LIKE '%'+@resourceCode+'%' ", new object[0]);
                list.Add(new SqlParameter("@resourceCode", resourceCode));
            }
            if (!string.IsNullOrEmpty(resourceName))
            {
                builder.AppendFormat(" AND ResourceName LIKE '%'+@resourceName+'%' ", new object[0]);
                list.Add(new SqlParameter("@resourceName", resourceName));
            }
            if (!string.IsNullOrEmpty(specification))
            {
                builder.AppendFormat(" AND Specification LIKE '%'+@specification+'%' ", new object[0]);
                list.Add(new SqlParameter("@specification", specification));
            }
            if (!string.IsNullOrEmpty(brand))
            {
                builder.AppendFormat(" AND Brand LIKE '%'+@brand+'%' ", new object[0]);
                list.Add(new SqlParameter("@brand", brand));
            }
            if (!string.IsNullOrEmpty(modelNumber))
            {
                builder.AppendFormat(" AND ModelNumber LIKE '%'+@modelNumber+'%' ", new object[0]);
                list.Add(new SqlParameter("@modelNumber", modelNumber));
            }
            string cmdText = "    \r\n                    --DECLARE @prjId NVARCHAR(50),@StuffCBSCode NVARCHAR(50),@pageSize INT,@pageIndex INT,@version int;\r\n                    --SET @prjId='7ff9b64f-4068-4a99-973b-d18cc8bb749b';\r\n                    --SET @StuffCBSCode='001001002';\r\n                    --SET @pageSize=1000;\r\n                    --SET @pageIndex=1;\r\n                    --set @version=2;\r\n                    WITH BudTask AS\t\t\t\t\t\t--目标成本的用量,价格和小计\r\n                    ( \r\n            ";
            if (isWBSRelevance == "1")
            {
                cmdText = cmdText + "    \r\n\t                SELECT ResourceId,Convert(decimal(18,3),SUM(Target)) Total,Convert(decimal(18,3),SUM(ResourceQuantity)) ResourceQuantity,\r\n\t                Convert(decimal(18,3),ISNULL(SUM(Target)/NULLIF(SUM(ResourceQuantity),0),0)) ResourcePrice  FROM \r\n\t                (\r\n\t\t                SELECT ResourceId,ISNULL(ResourceQuantity,0)*ISNULL(ResourcePrice,0) AS Target,\r\n\t\t                ISNULL(ResourceQuantity,0) ResourceQuantity\tFROM Bud_TaskResource TaskRes \r\n\t\t                INNER JOIN Bud_Task AS Task ON Task.TaskId =TaskRes.TaskId \r\n\t\t                INNER JOIN vGetCurBudVersion ON Task.PrjId=vGetCurBudVersion.PrjId AND Version=CurVersion \r\n\t\t                WHERE TaskType='' AND Task.PrjId=@prjId AND \r\n\t\t                dbo.GetResourceType(ResourceId) IN (SELECT ResourceTypeId FROM Res_ResourceType WHERE CBSCode=@StuffCBSCode)\r\n\t\t                UNION\r\n\t\t                SELECT ResourceId,ISNULL(ResourceQuantity,0)*ISNULL(ResourcePrice,0) ModifyAmount, \r\n\t\t                ISNULL(ResourceQuantity,0) ResourceQuantity FROM Bud_ModifyTaskRes modifyTaskRes \r\n\t\t                INNER JOIN Bud_ModifyTask modifyTask ON modifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId \r\n\t\t                INNER JOIN Bud_Modify modify ON  modify.ModifyId=modifyTask.ModifyId \r\n\t\t                WHERE FlowState=1 AND PrjId=@prjId AND \r\n\t\t                dbo.GetResourceType(ResourceId) IN (SELECT ResourceTypeId FROM Res_ResourceType WHERE CBSCode=@StuffCBSCode)\r\n\t                ) BudTaskRes \r\n\t                GROUP BY ResourceId ";
            }
            else
            {
                cmdText = cmdText + "    \r\n\t                SELECT ResourceId,SUM(ResourceQuantity) ResourceQuantity,SUM(Total) Total,\r\n\t                ISNULL(SUM(Total)/NULLIF(SUM(ResourceQuantity),0),0) ResourcePrice FROM \r\n\t                (\r\n\t\t                SELECT budTaskRes.ResourceId,ISNULL(ResourceQuantity,0) ResourceQuantity,\r\n\t\t                ISNULL(ResourceQuantity,0)*ISNULL(ResourcePrice,0) Total \r\n\t\t                from Bud_TaskResource budTaskRes  \r\n\t\t                INNER JOIN vGetCurBudVersion ON PrjGuid=vGetCurBudVersion.PrjId AND Versions=CurVersion \r\n\t\t                where prjGuid=@prjId  AND \r\n\t\t                dbo.GetResourceType(ResourceId) IN (SELECT ResourceTypeId FROM Res_ResourceType WHERE CBSCode=@StuffCBSCode)\r\n\t\t                UNION ALL\r\n\t\t                SELECT ResourceId,ISNULL(ResourceQuantity,0)*ISNULL(ResourcePrice,0) Total, \r\n\t\t                ISNULL(ResourceQuantity,0) ResourceQuantity FROM Bud_ModifyTaskRes modifyTaskRes \r\n\t\t                INNER JOIN Bud_Modify modify ON modify.ModifyId=modifyTaskRes.ModifyId \r\n\t\t                WHERE FlowState=1 AND PrjId=@prjId AND \r\n\t\t                dbo.GetResourceType(ResourceId) IN (SELECT ResourceTypeId FROM Res_ResourceType WHERE CBSCode=@StuffCBSCode)\r\n\t                ) BudTaskRes GROUP BY ResourceId ";
            }
            cmdText = (cmdText + "\r\n                    ),PurchaseInfo AS\t\t\t\t\t\t--物资采购\r\n                    (\r\n\t                    SELECT ResourceId,SCode,Sprice,Number,PurchaseCost,CBSCode FROM (\r\n\t\t                    SELECT SCode,res.ResourceId,Sprice,Number,PurchaseCost,dbo.ufn_GetRootResTypeId(res.ResourceId) ResourceTypeId FROM (\r\n\t\t\t                    SELECT SCode,Sprice,SUM(Number) Number,Convert(decimal(18,3),SUM(Sprice*Number)) PurchaseCost\r\n\t\t\t                    FROM Sm_Purchase smPurchase\r\n\t\t\t                    INNER JOIN Sm_Purchase_Stock smPurchaseStock on smPurchase.pCode=smPurchaseStock.psCode\r\n\t\t\t                    WHERE Project=@prjId AND FlowState=1\r\n\t\t\t                    GROUP BY SCode,Sprice\r\n\t\t                    ) Purchase\r\n\t\t                    INNER JOIN Res_Resource res ON Purchase.SCode=res.ResourceCode \r\n\t                    ) PurchaseStockType\r\n\t                    INNER JOIN Res_ResourceType resType ON PurchaseStockType.ResourceTypeId=resType.ResourceTypeId\r\n\t                    WHERE CBSCode=@StuffCBSCode\r\n                    ),StorageInfo AS\t\t\t\t\t\t--物资入库\r\n                    (\r\n                    SELECT ResourceId,SCode,Sprice,Number,StorageCost,CBSCode FROM (\r\n\t                    SELECT SCode,res.ResourceId,Sprice,Number,StorageCost,dbo.ufn_GetRootResTypeId(res.ResourceId) ResourceTypeId FROM (\r\n\t\t                    SELECT smStorageStock.SCode,Sprice,SUM(Number) Number,Convert(decimal(18,3),SUM(Sprice*Number)) StorageCost\r\n\t\t                    FROM Sm_Storage smStorage\r\n\t\t                    INNER JOIN Sm_Storage_Stock smStorageStock on smStorage.scode=smStorageStock.stcode\r\n\t\t                    WHERE project=@prjId AND FlowState=1 AND InFlag=1 AND isFirst=0\r\n\t\t                    GROUP BY smStorageStock.SCode,Sprice\r\n\t                    ) Storage\r\n\t                    INNER JOIN Res_Resource res ON Storage.SCode=res.ResourceCode \r\n                    ) StorageStockType\r\n                    INNER JOIN Res_ResourceType resType ON StorageStockType.ResourceTypeId=resType.ResourceTypeId\r\n                    WHERE CBSCode=@StuffCBSCode\r\n                    ),OutInfo AS\t\t\t\t\t\t\t--出库信息\r\n                    (\r\n\t                    SELECT ResourceId,SCode,Sprice,Number,OutCost,CBSCode FROM (\r\n\t\t                    SELECT SCode,res.ResourceId,Sprice,Number,OutCost,dbo.ufn_GetRootResTypeId(res.ResourceId) ResourceTypeId FROM (\r\n\t\t\t\t                    SELECT SCode,Sprice,SUM(Number) Number,Convert(decimal(18,3),SUM(Sprice*Number)) OutCost\r\n\t\t\t\t                    FROM Sm_OutReserve smOutReserve\r\n\t\t\t\t                    INNER JOIN Sm_out_Stock smOutStock on smOutReserve.OrCode=smOutStock.OrCode\r\n\t\t\t\t                    WHERE ProCode=@prjId AND FlowState=1 AND IsOut=1\r\n\t\t\t\t                    GROUP BY SCode,Sprice\r\n\t\t                    ) OutReserve\r\n\t\t                    INNER JOIN Res_Resource res ON OutReserve.SCode=res.ResourceCode \r\n\t                    ) OutReserveStockType\r\n\t                    INNER JOIN Res_ResourceType resType ON OutReserveStockType.ResourceTypeId=resType.ResourceTypeId\r\n\t                    WHERE CBSCode=@StuffCBSCode\r\n                    ),RefundingInfo AS\t\t\t\t\t--退库信息\r\n                    (\r\n\t                    SELECT SCode,Sprice,SUM(Number) Number,Convert(decimal(18,3),\r\n\t                    SUM(Sprice*Number)) RefundingCost FROM Sm_Refunding Refunding\r\n\t                    INNER JOIN Sm_back_Stock backStock ON Refunding.RCode=backStock.RCode\r\n\t                    WHERE ProCode=@prjId AND flowState=1 AND IsIn=1 \r\n\t                    GROUP BY SCode,Sprice    \r\n                    ),RealityInfo AS\t\t\t\t\t--实际用量信息\r\n                    (\r\n\t                    SELECT ResourceId,SCode,Sprice,Number,RealityCost,RealityDetail.ResourceTypeId,CBSCode FROM \r\n\t                    (\r\n\t\t                    SELECT SCode,Sprice,Number,RealityCost,res.ResourceId,dbo.ufn_GetRootResTypeId(res.ResourceId) ResourceTypeId \r\n\t\t                    FROM (\r\n\t\t\t                    SELECT OutInfo.SCode,OutInfo.Sprice,(OutInfo.Number-ISNULL(RefundingInfo.Number,0)) Number,\r\n\t\t\t                    (OutCost-ISNULL(RefundingCost,0)) RealityCost\r\n\t\t\t                    FROM OutInfo LEFT JOIN RefundingInfo ON OutInfo.SCode=RefundingInfo.SCode  AND OutInfo.Sprice=RefundingInfo.Sprice \r\n\t\t\t                    ) RealityCost INNER JOIN Res_Resource res ON RealityCost.SCode=res.ResourceCode \r\n\t                    ) RealityDetail INNER JOIN Res_ResourceType resType ON RealityDetail.ResourceTypeId=resType.ResourceTypeId\r\n\t                    WHERE CBSCode=@StuffCBSCode \r\n                    ),AllRes AS\r\n                    (\r\n\t                    SELECT ResourceId,ResourcePrice Sprice FROM BudTask \r\n\t                    UNION\r\n\t                    SELECT ResourceId,Sprice FROM PurchaseInfo\r\n\t                    UNION\r\n\t                    SELECT ResourceId,Sprice FROM StorageInfo\r\n\t                    UNION \r\n\t                    SELECT ResourceId,Sprice FROM RealityInfo\r\n                    )\r\n                    SELECT COUNT(*) FROM \r\n                    (\r\n\t                    SELECT AllRes.ResourceId,ResourceCode,ResourceName,ISNULL(ResourceQuantity,0) BudQuantity,\r\n\t                    ISNULL(ResourcePrice,0) BudPrice,ISNULL(total,0) BudTotal,ISNULL(RealityInfo.Sprice,0)RealityPrice,\r\n\t                    ISNULL(RealityInfo.Number,0) RealityNumber,ISNULL(RealityCost,0) RealityTotal,\r\n\t                    ISNULL(PurchaseInfo.Number,0) PurchaseNumber,ISNULL(PurchaseInfo.Sprice,0) PurchaseSprice,\r\n\t                    ISNULL(PurchaseInfo.PurchaseCost,0) PurchaseCost,ISNULL(StorageInfo.Number,0) StorageNumber,\r\n\t                    ISNULL(StorageInfo.StorageCost,0) StorageCost,ISNULL(OutInfo.Number,0) OutNumber,ISNULL(OutInfo.OutCost,0) OutCost,\r\n                        res.Brand, res.Specification, res.ModelNumber  \r\n\t                    FROM AllRes \r\n\t                    LEFT JOIN BudTask ON AllRes.ResourceId=BudTask.ResourceId AND AllRes.Sprice=BudTask.ResourcePrice\r\n\t                    LEFT JOIN PurchaseInfo ON AllRes.ResourceId=PurchaseInfo.ResourceId AND AllRes.Sprice=PurchaseInfo.Sprice\r\n\t                    LEFT JOIN StorageInfo ON AllRes.ResourceId=StorageInfo.ResourceId AND AllRes.Sprice=StorageInfo.Sprice\r\n\t                    LEFT JOIN OutInfo ON AllRes.ResourceId=OutInfo.ResourceId AND AllRes.Sprice=OutInfo.Sprice\r\n\t                    LEFT JOIN RealityInfo ON AllRes.ResourceId=RealityInfo.ResourceId AND AllRes.Sprice=RealityInfo.Sprice\r\n\t                    INNER JOIN Res_Resource res ON AllRes.ResourceId=res.ResourceId\r\n                        WHERE 1=1 ") + builder.ToString() + " ) ResInfo ";
            list.Add(new SqlParameter("@prjId", prjId));
            list.Add(new SqlParameter("@StuffCBSCode", StuffCBSCode));
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, list.ToArray()));
        }

        public DataTable GetStuffInfo(string resourceCode, string resourceName, string prjId, int pageIndex, int pageSize, string isWBSRelevance, string specification, string brand, string modelNumber)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = this.GetStuffCount(resourceCode, resourceName, prjId, isWBSRelevance, specification, brand, modelNumber);
            }
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(resourceCode))
            {
                builder.AppendFormat(" AND ResourceCode LIKE '%'+@resourceCode+'%' ", new object[0]);
                list.Add(new SqlParameter("@resourceCode", resourceCode));
            }
            if (!string.IsNullOrEmpty(resourceName))
            {
                builder.AppendFormat(" AND ResourceName LIKE '%'+@resourceName+'%' ", new object[0]);
                list.Add(new SqlParameter("@resourceName", resourceName));
            }
            if (!string.IsNullOrEmpty(specification))
            {
                builder.AppendFormat(" AND Specification LIKE '%'+@specification+'%' ", new object[0]);
                list.Add(new SqlParameter("@specification", specification));
            }
            if (!string.IsNullOrEmpty(brand))
            {
                builder.AppendFormat(" AND Brand LIKE '%'+@brand+'%' ", new object[0]);
                list.Add(new SqlParameter("@brand", brand));
            }
            if (!string.IsNullOrEmpty(modelNumber))
            {
                builder.AppendFormat(" AND ModelNumber LIKE '%'+@modelNumber+'%' ", new object[0]);
                list.Add(new SqlParameter("@modelNumber", modelNumber));
            }
            string cmdText = "    \r\n                    --DECLARE @prjId NVARCHAR(50),@StuffCBSCode NVARCHAR(50),@pageSize INT,@pageIndex INT,@version int;\r\n                    --SET @prjId='7ff9b64f-4068-4a99-973b-d18cc8bb749b';\r\n                    --SET @StuffCBSCode='001001002';\r\n                    --SET @pageSize=1000;\r\n                    --SET @pageIndex=1;\r\n                    --set @version=2;\r\n                    WITH BudTask AS\t\t\t\t\t\t--目标成本的用量,价格和小计\r\n                    (\r\n            ";
            if (isWBSRelevance == "1")
            {
                cmdText = cmdText + "    \r\n\t                SELECT ResourceId,Convert(decimal(18,3),SUM(Target)) Total,Convert(decimal(18,3),SUM(ResourceQuantity)) ResourceQuantity,\r\n\t                Convert(decimal(18,3),ISNULL(SUM(Target)/NULLIF(SUM(ResourceQuantity),0),0)) ResourcePrice  FROM \r\n\t                (\r\n\t\t                SELECT ResourceId,ISNULL(ResourceQuantity,0)*ISNULL(ResourcePrice,0) AS Target,\r\n\t\t                ISNULL(ResourceQuantity,0) ResourceQuantity\tFROM Bud_TaskResource TaskRes \r\n\t\t                INNER JOIN Bud_Task AS Task ON Task.TaskId =TaskRes.TaskId \r\n\t\t                INNER JOIN vGetCurBudVersion ON Task.PrjId=vGetCurBudVersion.PrjId AND Version=CurVersion \r\n\t\t                WHERE TaskType='' AND Task.PrjId=@prjId AND \r\n\t\t                dbo.GetResourceType(ResourceId) IN (SELECT ResourceTypeId FROM Res_ResourceType WHERE CBSCode=@StuffCBSCode)\r\n\t\t                UNION\r\n\t\t                SELECT ResourceId,ISNULL(ResourceQuantity,0)*ISNULL(ResourcePrice,0) ModifyAmount, \r\n\t\t                ISNULL(ResourceQuantity,0) ResourceQuantity FROM Bud_ModifyTaskRes modifyTaskRes \r\n\t\t                INNER JOIN Bud_ModifyTask modifyTask ON modifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId \r\n\t\t                INNER JOIN Bud_Modify modify ON  modify.ModifyId=modifyTask.ModifyId \r\n\t\t                WHERE FlowState=1 AND PrjId=@prjId AND \r\n\t\t                dbo.GetResourceType(ResourceId) IN (SELECT ResourceTypeId FROM Res_ResourceType WHERE CBSCode=@StuffCBSCode)\r\n\t                ) BudTaskRes \r\n\t                GROUP BY ResourceId ";
            }
            else
            {
                cmdText = cmdText + "    \r\n\t                SELECT ResourceId,SUM(ResourceQuantity) ResourceQuantity,SUM(Total) Total,\r\n\t                ISNULL(SUM(Total)/NULLIF(SUM(ResourceQuantity),0),0) ResourcePrice FROM \r\n\t                (\r\n\t\t                SELECT budTaskRes.ResourceId,ISNULL(ResourceQuantity,0) ResourceQuantity,\r\n\t\t                ISNULL(ResourceQuantity,0)*ISNULL(ResourcePrice,0) Total \r\n\t\t                from Bud_TaskResource budTaskRes  \r\n\t\t                INNER JOIN vGetCurBudVersion ON PrjGuid=vGetCurBudVersion.PrjId AND Versions=CurVersion \r\n\t\t                where prjGuid=@prjId  AND \r\n\t\t                dbo.GetResourceType(ResourceId) IN (SELECT ResourceTypeId FROM Res_ResourceType WHERE CBSCode=@StuffCBSCode)\r\n\t\t                UNION ALL\r\n\t\t                SELECT ResourceId,ISNULL(ResourceQuantity,0)*ISNULL(ResourcePrice,0) Total, \r\n\t\t                ISNULL(ResourceQuantity,0) ResourceQuantity FROM Bud_ModifyTaskRes modifyTaskRes \r\n\t\t                INNER JOIN Bud_Modify modify ON modify.ModifyId=modifyTaskRes.ModifyId \r\n\t\t                WHERE FlowState=1 AND PrjId=@prjId AND \r\n\t\t                dbo.GetResourceType(ResourceId) IN (SELECT ResourceTypeId FROM Res_ResourceType WHERE CBSCode=@StuffCBSCode)\r\n\t                ) BudTaskRes GROUP BY ResourceId ";
            }
            cmdText = (cmdText + "\r\n                    ),PurchaseInfo AS\t\t\t\t\t\t--物资采购\r\n                    (\r\n\t                    SELECT ResourceId,SCode,Sprice,Number,PurchaseCost,CBSCode FROM (\r\n\t\t                    SELECT SCode,res.ResourceId,Sprice,Number,PurchaseCost,dbo.ufn_GetRootResTypeId(res.ResourceId) ResourceTypeId FROM (\r\n\t\t\t                    SELECT SCode,Sprice,SUM(Number) Number,Convert(decimal(18,3),SUM(Sprice*Number)) PurchaseCost\r\n\t\t\t                    FROM Sm_Purchase smPurchase\r\n\t\t\t                    INNER JOIN Sm_Purchase_Stock smPurchaseStock on smPurchase.pCode=smPurchaseStock.psCode\r\n\t\t\t                    WHERE Project=@prjId AND FlowState=1\r\n\t\t\t                    GROUP BY SCode,Sprice\r\n\t\t                    ) Purchase\r\n\t\t                    INNER JOIN Res_Resource res ON Purchase.SCode=res.ResourceCode \r\n\t                    ) PurchaseStockType\r\n\t                    INNER JOIN Res_ResourceType resType ON PurchaseStockType.ResourceTypeId=resType.ResourceTypeId\r\n\t                    WHERE CBSCode=@StuffCBSCode\r\n                    ),StorageInfo AS\t\t\t\t\t\t--物资入库\r\n                    (\r\n                    SELECT ResourceId,SCode,Sprice,Number,StorageCost,CBSCode FROM (\r\n\t                    SELECT SCode,res.ResourceId,Sprice,Number,StorageCost,dbo.ufn_GetRootResTypeId(res.ResourceId) ResourceTypeId FROM (\r\n\t\t                    SELECT smStorageStock.SCode,Sprice,SUM(Number) Number,Convert(decimal(18,3),SUM(Sprice*Number)) StorageCost\r\n\t\t                    FROM Sm_Storage smStorage\r\n\t\t                    INNER JOIN Sm_Storage_Stock smStorageStock on smStorage.scode=smStorageStock.stcode\r\n\t\t                    WHERE project=@prjId AND FlowState=1 AND InFlag=1 AND isFirst=0\r\n\t\t                    GROUP BY smStorageStock.SCode,Sprice\r\n\t                    ) Storage\r\n\t                    INNER JOIN Res_Resource res ON Storage.SCode=res.ResourceCode \r\n                    ) StorageStockType\r\n                    INNER JOIN Res_ResourceType resType ON StorageStockType.ResourceTypeId=resType.ResourceTypeId\r\n                    WHERE CBSCode=@StuffCBSCode\r\n                    ),OutInfo AS\t\t\t\t\t\t\t--出库信息\r\n                    (\r\n\t                    SELECT ResourceId,SCode,Sprice,Number,OutCost,CBSCode FROM (\r\n\t\t                    SELECT SCode,res.ResourceId,Sprice,Number,OutCost,dbo.ufn_GetRootResTypeId(res.ResourceId) ResourceTypeId FROM (\r\n\t\t\t\t                    SELECT SCode,Sprice,SUM(Number) Number,Convert(decimal(18,3),SUM(Sprice*Number)) OutCost\r\n\t\t\t\t                    FROM Sm_OutReserve smOutReserve\r\n\t\t\t\t                    INNER JOIN Sm_out_Stock smOutStock on smOutReserve.OrCode=smOutStock.OrCode\r\n\t\t\t\t                    WHERE ProCode=@prjId AND FlowState=1 AND IsOut=1\r\n\t\t\t\t                    GROUP BY SCode,Sprice\r\n\t\t                    ) OutReserve\r\n\t\t                    INNER JOIN Res_Resource res ON OutReserve.SCode=res.ResourceCode \r\n\t                    ) OutReserveStockType\r\n\t                    INNER JOIN Res_ResourceType resType ON OutReserveStockType.ResourceTypeId=resType.ResourceTypeId\r\n\t                    WHERE CBSCode=@StuffCBSCode\r\n                    ),RefundingInfo AS\t\t\t\t\t--退库信息\r\n                    (\r\n\t                    SELECT SCode,Sprice,SUM(Number) Number,Convert(decimal(18,3),\r\n\t                    SUM(Sprice*Number)) RefundingCost FROM Sm_Refunding Refunding\r\n\t                    INNER JOIN Sm_back_Stock backStock ON Refunding.RCode=backStock.RCode\r\n\t                    WHERE ProCode=@prjId AND flowState=1 AND IsIn=1 \r\n\t                    GROUP BY SCode,Sprice    \r\n                    ),RealityInfo AS\t\t\t\t\t--实际用量信息\r\n                    (\r\n\t                    SELECT ResourceId,SCode,Sprice,Number,RealityCost,RealityDetail.ResourceTypeId,CBSCode FROM \r\n\t                    (\r\n\t\t                    SELECT SCode,Sprice,Number,RealityCost,res.ResourceId,dbo.ufn_GetRootResTypeId(res.ResourceId) ResourceTypeId \r\n\t\t                    FROM (\r\n\t\t\t                    SELECT OutInfo.SCode,OutInfo.Sprice,(OutInfo.Number-ISNULL(RefundingInfo.Number,0)) Number,\r\n\t\t\t                    (OutCost-ISNULL(RefundingCost,0)) RealityCost\r\n\t\t\t                    FROM OutInfo LEFT JOIN RefundingInfo ON OutInfo.SCode=RefundingInfo.SCode  AND OutInfo.Sprice=RefundingInfo.Sprice \r\n\t\t\t                    ) RealityCost INNER JOIN Res_Resource res ON RealityCost.SCode=res.ResourceCode \r\n\t                    ) RealityDetail INNER JOIN Res_ResourceType resType ON RealityDetail.ResourceTypeId=resType.ResourceTypeId\r\n\t                    WHERE CBSCode=@StuffCBSCode \r\n                    ),AllRes AS\r\n                    (\r\n\t                    SELECT ResourceId,ResourcePrice Sprice FROM BudTask \r\n\t                    UNION\r\n\t                    SELECT ResourceId,Sprice FROM PurchaseInfo\r\n\t                    UNION\r\n\t                    SELECT ResourceId,Sprice FROM StorageInfo\r\n\t                    UNION \r\n\t                    SELECT ResourceId,Sprice FROM RealityInfo\r\n                    )\r\n                    SELECT TOP(@pageSize)CONVERT(NVARCHAR(30),Num) Num,ResourceId,ResourceCode,ResourceName,Brand,Specification,\r\n                    ModelNumber,BudQuantity,BudPrice,BudTotal,PurchaseNumber,PurchaseSprice,PurchaseCost,\r\n                    StorageNumber,StorageCost,RealityNumber,RealityPrice,RealityTotal,(BudQuantity-PurchaseNumber) ProfitLossNumber,\r\n                    (BudTotal-PurchaseCost) ProfitLossCost,(PurchaseNumber-OutNumber) BalanceNumber,(PurchaseCost-OutCost) BalanceCost FROM (\r\n                     SELECT Row_Number()over(order by ResourceCode) as Num,ResInfo.* FROM\r\n                    (\r\n\t                    SELECT AllRes.ResourceId,ResourceCode,ResourceName,ISNULL(ResourceQuantity,0) BudQuantity,\r\n\t                    ISNULL(ResourcePrice,0) BudPrice,ISNULL(total,0) BudTotal,ISNULL(RealityInfo.Sprice,0)RealityPrice,\r\n\t                    ISNULL(RealityInfo.Number,0) RealityNumber,ISNULL(RealityCost,0) RealityTotal,\r\n\t                    ISNULL(PurchaseInfo.Number,0) PurchaseNumber,ISNULL(PurchaseInfo.Sprice,0) PurchaseSprice,\r\n\t                    ISNULL(PurchaseInfo.PurchaseCost,0) PurchaseCost,ISNULL(StorageInfo.Number,0) StorageNumber,\r\n\t                    ISNULL(StorageInfo.StorageCost,0) StorageCost,ISNULL(OutInfo.Number,0) OutNumber,ISNULL(OutInfo.OutCost,0) OutCost,\r\n                        res.Brand, res.Specification, res.ModelNumber\r\n\t                    FROM AllRes \r\n\t                    LEFT JOIN BudTask ON AllRes.ResourceId=BudTask.ResourceId AND AllRes.Sprice=BudTask.ResourcePrice\r\n\t                    LEFT JOIN PurchaseInfo ON AllRes.ResourceId=PurchaseInfo.ResourceId AND AllRes.Sprice=PurchaseInfo.Sprice\r\n\t                    LEFT JOIN StorageInfo ON AllRes.ResourceId=StorageInfo.ResourceId AND AllRes.Sprice=StorageInfo.Sprice\r\n\t                    LEFT JOIN OutInfo ON AllRes.ResourceId=OutInfo.ResourceId AND AllRes.Sprice=OutInfo.Sprice\r\n\t                    LEFT JOIN RealityInfo ON AllRes.ResourceId=RealityInfo.ResourceId AND AllRes.Sprice=RealityInfo.Sprice\r\n\t                    INNER JOIN Res_Resource res ON AllRes.ResourceId=res.ResourceId\r\n                        WHERE 1=1  ") + builder.ToString() + " ) ResInfo) DATA WHERE Num>@pageSize*(@pageIndex-1) ";
            list.Add(new SqlParameter("@prjId", prjId));
            list.Add(new SqlParameter("@StuffCBSCode", StuffCBSCode));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, list.ToArray());
        }

        public decimal GetTotal(string tcode)
        {
            string cmdText = string.Format("\r\n                    SELECT SUM(sprice*snumber) FROM Sm_Treasury_Stock \r\n                    JOIN Res_Resource ON Sm_Treasury_Stock.scode=Res_Resource.ResourceCode\r\n                    WHERE Sm_Treasury_Stock.tcode='{0}' \r\n                    ", tcode);
            return Convert.ToDecimal(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]));
        }

        public DataTable GetTreasuryData(string ResourceName, string ResourceCode, string Tname, string CorpName, string userCode, string specification, string brand, string modelNumber)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n\t\t\t\tSELECT CK.*,Sm_Treasury.tname,Res_Resource.ResourceName,Res_Resource.Specification,Res_Unit.Name,\r\n\t\t\t\tXPM_Basic_ContactCorp.CorpName,Res_Resource.TechnicalParameter,Res_Resource.ModelNumber,Res_Resource.Brand FROM (\r\n\t\t\t\tSELECT scode,sprice,corp,tcode,SUM(snumber) AS snumber,SUM(CONVERT(DECIMAL(18,3),sprice*snumber)) as total  FROM Sm_Treasury_Stock \r\n\t\t\t\tWHERE snumber <>0 GROUP BY scode,sprice,corp,tcode) AS CK\r\n\t\t\t\tINNER JOIN Sm_Treasury ON Sm_Treasury.tcode = CK.tcode \r\n\t\t\t\tINNER JOIN Res_Resource ON Res_Resource.ResourceCode = CK.scode \r\n\t\t\t\tINNER JOIN Res_Unit ON Res_Resource.Unit = Res_Unit.UnitID \r\n\t\t\t\tLEFT JOIN XPM_Basic_ContactCorp ON CK.corp = XPM_Basic_ContactCorp.CorpID \r\n\t\t\t\tWHERE 1=1 AND Sm_Treasury.TCode IN \r\n\t\t\t\t(\r\n\t\t\t\t\t--仓库权限\r\n\t\t\t\t\tSELECT tcode FROM Sm_Treasury_Permit \r\n\t\t\t\t\tWHERE ptype='Person'  AND pcode='{0}'\r\n\t\t\t\t\tUNION\r\n\t\t\t\t\tSELECT tcode FROM Sm_Treasury_Permit permit\r\n\t\t\t\t\tINNER JOIN Pt_YHMC yhmc ON permit.pcode=i_bmdm\r\n\t\t\t\t\tWHERE ptype='Department' and v_yhdm='{0}'\r\n\t\t\t\t\tUNION\r\n\t\t\t\t\tSELECT tcode FROM Sm_Treasury_Permit permit\r\n\t\t\t\t\tINNER JOIN Pt_YHMC yhmc ON permit.pcode=i_DutyId\r\n\t\t\t\t\tWHERE ptype='Post' and v_yhdm='{0}'\r\n\t\t\t\t) ", userCode).AppendLine();
            if (!string.IsNullOrEmpty(ResourceName))
            {
                builder.AppendFormat("  AND ResourceName like  '%{0}%'  ", ResourceName);
            }
            if (!string.IsNullOrEmpty(ResourceCode))
            {
                builder.AppendFormat("  AND scode like '%{0}%'  ", ResourceCode);
            }
            if (!string.IsNullOrEmpty(Tname))
            {
                builder.AppendFormat("  AND tname='{0}'  ", Tname);
            }
            if (!string.IsNullOrEmpty(CorpName))
            {
                builder.AppendFormat("  AND CorpName LIKE '%{0}%'  ", CorpName);
            }
            if (!string.IsNullOrEmpty(specification))
            {
                builder.AppendFormat("  AND Res_Resource.Specification LIKE '%{0}%' ", specification);
            }
            if (!string.IsNullOrEmpty(brand))
            {
                builder.AppendFormat("  AND Res_Resource.Brand LIKE '%{0}%' ", brand);
            }
            if (!string.IsNullOrEmpty(modelNumber))
            {
                builder.AppendFormat("  AND Res_Resource.ModelNumber LIKE '%{0}%' ", modelNumber);
            }
            builder.AppendLine();
            builder.Append("ORDER BY scode ASC");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public string GetTsidBySSc(string scode, string sprice, string corp, string tcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select tsid from dbo.Sm_Treasury_Stock where ");
            builder.Append("scode='" + scode + "' and sprice=" + sprice + " and ISNULL(corp,'')='" + corp + "' and tcode='" + tcode + "'");
            string str = "";
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    str = str + "'" + reader[0].ToString() + "',";
                }
            }
            return str;
        }

        private void PrepareAddCommand(TreasuryStockModel model, out string cmdText, out SqlParameter[] parameters)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Treasury_Stock(");
            builder.Append("tsid,scode,tcode,sprice,snumber,isfirst,corp,incode,intime,intype,type)");
            builder.Append(" values (");
            builder.Append("@tsid,@scode,@tcode,@sprice,@snumber,@isfirst,@corp,@incode,@intime,@intype,@type)");
            cmdText = builder.ToString();
            parameters = new SqlParameter[] { new SqlParameter("@tsid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@sprice", SqlDbType.Decimal, 9), new SqlParameter("@snumber", SqlDbType.Decimal, 9), new SqlParameter("@isfirst", SqlDbType.Bit, 1), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@incode", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@intype", SqlDbType.Int, 4), new SqlParameter("type", SqlDbType.NVarChar, 0x40) };
            parameters[0].Value = model.tsid;
            parameters[1].Value = model.scode;
            parameters[2].Value = model.tcode;
            parameters[3].Value = model.sprice;
            parameters[4].Value = model.snumber;
            parameters[5].Value = model.isfirst;
            parameters[6].Value = model.corp;
            parameters[7].Value = model.incode;
            parameters[8].Value = model.intime;
            parameters[9].Value = model.intype;
            parameters[10].Value = model.Type;
        }

        public TreasuryStockModel ReaderBind(IDataReader dataReader)
        {
            TreasuryStockModel model = new TreasuryStockModel {
                tsid = dataReader["tsid"].ToString(),
                scode = dataReader["scode"].ToString(),
                tcode = dataReader["tcode"].ToString()
            };
            object obj2 = dataReader["sprice"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.sprice = (decimal) obj2;
            }
            obj2 = dataReader["snumber"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.snumber = (decimal) obj2;
            }
            obj2 = dataReader["isfirst"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.isfirst = (bool) obj2;
            }
            model.corp = dataReader["corp"].ToString();
            model.incode = dataReader["incode"].ToString();
            obj2 = dataReader["intime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.intime = (DateTime) obj2;
            }
            obj2 = dataReader["intype"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.intype = (int) obj2;
            }
            return model;
        }

        public int Update(TreasuryStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Treasury_Stock set ");
            builder.Append("scode=@scode,");
            builder.Append("tcode=@tcode,");
            builder.Append("sprice=@sprice,");
            builder.Append("snumber=@snumber,");
            builder.Append("isfirst=@isfirst,");
            builder.Append("corp=@corp,");
            builder.Append("incode=@incode,");
            builder.Append("intime=@intime,");
            builder.Append("intype=@intype");
            builder.Append(" where tsid=@tsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tsid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200), new SqlParameter("@sprice", SqlDbType.Decimal, 9), new SqlParameter("@snumber", SqlDbType.Decimal, 9), new SqlParameter("@isfirst", SqlDbType.Bit, 1), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@incode", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@intype", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.tsid;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.tcode;
            commandParameters[3].Value = model.sprice;
            commandParameters[4].Value = model.snumber;
            commandParameters[5].Value = model.isfirst;
            commandParameters[6].Value = model.corp;
            commandParameters[7].Value = model.incode;
            commandParameters[8].Value = model.intime;
            commandParameters[9].Value = model.intype;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public void UpdateResPrice(SqlTransaction trans, string resId, string prieceTypeId, decimal price, string userName)
        {
            string str = string.Format("SELECT COUNT(*) FROM  Res_Price WHERE PriceTypeId='{0}' AND ResourceId='{1}'", prieceTypeId, resId);
            int num = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, str.ToString(), null).ToString());
            string cmdText = string.Empty;
            if (num > 0)
            {
                cmdText = string.Format("UPDATE Res_Price SET PriceValue={0} WHERE PriceTypeId='{1}' AND ResourceId='{2}'", price, prieceTypeId, resId);
            }
            else
            {
                cmdText = string.Format("INSERT INTO Res_Price VALUES('{0}','{1}',{2},'{3}','{4}')", new object[] { resId, prieceTypeId, price, userName, DateTime.Now });
            }
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, new SqlParameter[0]);
        }
    }
}

