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

    public class MeterialPlanStock
    {
        private static string BudgetPriceType = ConfigHelper.Get("BudgetPriceType");

        public int Add(MaterialPlanStockModel model)
        {
            SqlParameter[] parameterArray;
            string cmdText = string.Empty;
            this.PrepareAddCommand(model, out cmdText, out parameterArray);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, parameterArray);
        }

        public int Add(SqlTransaction truans, MaterialPlanStockModel model)
        {
            SqlParameter[] parameterArray;
            string cmdText = string.Empty;
            this.PrepareAddCommand(model, out cmdText, out parameterArray);
            return SqlHelper.ExecuteNonQuery(truans, CommandType.Text, cmdText, parameterArray);
        }

        public int Delete(string wpsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Wantplan_Stock ");
            builder.Append(" where wpsid=@wpsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@wpsid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = wpsid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByWantplanCode(string wantplanCode)
        {
            return this.DeleteByWantplanCode(null, wantplanCode);
        }

        public int DeleteByWantplanCode(SqlTransaction trans, string wantplanCode)
        {
            string cmdText = "delete from Sm_Wantplan_Stock where wpcode = @wpcode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@wpcode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = wantplanCode;
            if (trans != null)
            {
                return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        public int delMore(string sql)
        {
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public DataTable GetBudTaskDiff(string wpcode, string prjId)
        {
            string cmdText = "\r\n                    --DECLARE @wpcode NVARCHAR(64),@prjId NVARCHAR(64);\r\n                    --SET @wpcode = '20140106104843';\r\n                    --SET @prjId = '1f37a43d-b2d4-4f4f-9477-fc5fc9633edd';\r\n                   WITH cteBudTask AS \r\n                    (\r\n                        SELECT Bud_Task.TaskId,TaskCode,TaskName,ResourceId,ResourceQuantity\r\n                        FROM Bud_Task\r\n                        JOIN Bud_TaskResource ON Bud_Task.TaskId = Bud_TaskResource.TaskId\r\n                        WHERE PrjId = @prjId AND ResourceId IS NOT NULL AND Bud_Task.TaskType = ''\r\n                    ),cteBudModify AS \r\n                    (\r\n                        SELECT modifyTask.ModifyTaskId AS TaskId,ModifyTaskCode AS TaskCode,ModifyTaskContent AS TaskName,\r\n                        ResourceId,ResourceQuantity\r\n                        FROM Bud_ModifyTask modifyTask\r\n                        JOIN Bud_ModifyTaskRes ON modifyTask.ModifyTaskId = Bud_ModifyTaskRes.ModifyTaskId\r\n                        JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n                        WHERE ModifyType=0 AND FlowState=1 AND PrjId = @prjId AND ResourceId IS NOT NULL\r\n                    ),cteWantStock AS\r\n                    (\t--本次计划量\r\n                        SELECT TaskId,scode,number AS WantNumber\r\n                        FROM Sm_Wantplan_Stock  \r\n                        JOIN Sm_Wantplan ON Sm_Wantplan_Stock.wpcode = Sm_Wantplan.swcode\r\n                        WHERE procode = @prjId AND wpcode = @wpcode \r\n                    ),cteHasUsedStock AS\r\n                    (\t--已消耗量\r\n\t\t\t\t\t\tSELECT WS.TaskId, WS.scode,SUM(number) AS UsedNum\r\n\t\t\t\t\t\tFROM Sm_Wantplan_Stock AS WS\r\n\t\t\t\t\t\tJOIN Sm_Wantplan ON WS.wpcode = Sm_Wantplan.swcode\r\n\t\t\t\t\t\tJOIN cteWantStock ON cteWantStock.scode = WS.scode AND cteWantStock.TaskId = WS.TaskId\r\n\t\t\t\t\t\tWHERE procode = @prjId AND WS.wpcode != @wpcode \r\n\t\t\t\t\t\t\tAND flowState IN('1','0')\r\n\t\t\t\t\t\tGROUP BY WS.TaskId,WS.scode\r\n                    ),cteTask AS\r\n                    (\r\n                        SELECT TaskId,TaskCode,TaskName,ResourceId,ResourceQuantity FROM cteBudTask\r\n\t                    WHERE ResourceId IN\r\n\t                    (\r\n\t\t                    SELECT ResourceId FROM cteWantStock\r\n\t\t                    JOIN Res_Resource ON cteWantStock.scode = Res_Resource.ResourceCode\r\n\t                    )\r\n                        UNION\r\n                        SELECT TaskId,TaskCode,TaskName,ResourceId,ResourceQuantity FROM cteBudModify\r\n\t                    WHERE ResourceId IN\r\n\t                    (\r\n\t\t                    SELECT ResourceId FROM cteWantStock\r\n\t\t                    JOIN Res_Resource ON cteWantStock.scode = Res_Resource.ResourceCode\r\n\t                    )\r\n                    )\r\n                    SELECT cteTask.TaskCode, cteTask.TaskName,cteTask.ResourceQuantity,\r\n\t                R.ResourceName,R.Specification,\r\n\t                ISNULL(cteWantStock.WantNumber,0.00) AS WantNumber,\t\t\t\t\t--本次计划量\r\n\t                ISNULL(cteHasUsedStock.UsedNum,0.00) AS UsedNumber,\t\t\t\t\t--已消耗量\r\n\t                (cteTask.ResourceQuantity - ISNULL(UsedNum,0.00)) AS RestNumber\t\t--剩余量\r\n                    FROM cteTask\r\n                    LEFT JOIN cteWantStock ON cteWantStock.TaskId = cteTask.TaskId\t\t\r\n                    LEFT JOIN cteHasUsedStock ON cteWantStock.TaskId = cteTask.TaskId\r\n\t                    AND cteHasUsedStock.scode = cteWantStock.scode\t\r\n                    LEFT JOIN Res_Resource R ON cteWantStock.scode = R.ResourceCode\r\n                    WHERE R.ResourceId = cteTask.ResourceId\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@wpcode", wpcode), new SqlParameter("@prjId", prjId) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public List<MaterialPlanStockModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select wpsid,scode,wpcode,number,arrivalDate,remark ");
            builder.Append(" FROM Sm_Wantplan_Stock ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<MaterialPlanStockModel> list = new List<MaterialPlanStockModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public DataTable GetListArrayByWantpcode(string wpcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select p2.scode,p1.ResourceName,");
            builder.Append("p1.Specification,p4.Name,sum(p2.number) as number ");
            builder.Append(",p1.Brand,ModelNumber,TechnicalParameter,p2.Remark ");
            builder.Append(" from dbo.Res_Resource as p1 ");
            builder.Append(" join dbo.Sm_Wantplan_Stock as p2 on p1.resourceCode=p2.scode");
            builder.Append(" left join Res_Unit as p4 on p1.unit=p4.UnitID");
            builder.Append(" where p2.wpcode in(" + wpcode + ")");
            builder.Append(" group by p2.scode,p1.ResourceName,p1.Specification,p4.Name");
            builder.Append(",p1.Brand,ModelNumber,TechnicalParameter,p2.Remark ");
            builder.Append("  ORDER BY p2.scode DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public DataTable GetListArrayByWpcode(string wpcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select p2.scode,p2.wpcode,p1.ResourceName,");
            builder.Append("p1.Specification,p4.Name,sum(p2.number) as number ,p2.arrivalDate,p2.Remark ");
            builder.Append(",p1.Brand,ModelNumber,TechnicalParameter ");
            builder.Append(" from dbo.Res_Resource as p1 ");
            builder.Append(" join dbo.Sm_Wantplan_Stock as p2 on p1.resourceCode=p2.scode");
            builder.Append(" left join Res_Unit as p4 on p1.unit=p4.UnitID");
            builder.Append(" where p2.wpcode in(" + wpcode + ")");
            builder.Append(" group by p2.scode,p2.wpcode,p1.ResourceName,p1.Specification,p4.Name ,p2.arrivalDate,p2.Remark");
            builder.Append(",p1.Brand,ModelNumber,TechnicalParameter ");
            builder.Append("  ORDER BY p2.scode DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public DataTable GetListArrayByWpcodenew(string wpcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select p2.scode,p1.ResourceName,");
            builder.Append("p1.Specification,p4.Name,sum(p2.number) as number ");
            builder.Append(",p1.Brand,ModelNumber,TechnicalParameter ");
            builder.Append(" from dbo.Res_Resource as p1 ");
            builder.Append(" join dbo.Sm_Wantplan_Stock as p2 on p1.resourceCode=p2.scode");
            builder.Append(" left join Res_Unit as p4 on p1.unit=p4.UnitID");
            builder.Append(" where p2.wpcode in(" + wpcode + ")");
            builder.Append(" group by p2.scode,p1.ResourceName,p1.Specification,p4.Name ");
            builder.Append(",p1.Brand,ModelNumber,TechnicalParameter ");
            builder.Append("  ORDER BY p2.scode DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public DataTable GetListByScode(string scode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select p1.resourceCode as scode,p1.ResourceName,convert(decimal(18,3),0) as number,");
            builder.Append(" p1.Specification,p4.Name");
            builder.Append(" ,p1.Brand,ModelNumber,TechnicalParameter");
            builder.Append(" from dbo.Res_Resource as p1 ");
            builder.Append(" left join Res_Unit as p4 on p1.Unit=p4.UnitID");
            builder.Append(" where p1.resourceCode in(" + scode + ") ORDER BY ResourceCode DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public string GetMinArrivalDate(string resourceCode, string Wpcode)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT TOP 1 ArrivalDate FROM Sm_Wantplan_Stock WHERE Wpcode IN ({0}) AND Scode='{1}' AND ArrivalDate is not null ", Wpcode, resourceCode);
            builder.Append("order by ArrivalDate");
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                if (reader.Read())
                {
                    str = reader["ArrivalDate"].ToString();
                }
            }
            return str;
        }

        public MaterialPlanStockModel GetModel(string wpsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select wpsid,scode,wpcode,number,arrivalDate,remark from Sm_Wantplan_Stock ");
            builder.Append(" where wpsid=@wpsid ");
            MaterialPlanStockModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@wpsid", wpsid) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public string GetRemark(string resourceCode, string Wpcode)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT TOP 1 Remark FROM Sm_Wantplan_Stock WHERE Wpcode IN ({0}) AND Scode='{1}' AND Remark is not null ", Wpcode, resourceCode);
            builder.Append("order by Remark");
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                if (reader.Read())
                {
                    str = reader["Remark"].ToString();
                }
            }
            return str;
        }

        public DataTable GetResByBud(string resCode, string resName, string specification, string brand, string prjId, int version, int pageSize, int pageIndex, string isWBSRelevance, string taskCode)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetResCountByBud(resCode, resName, specification, brand, prjId, version, isWBSRelevance);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(resCode))
            {
                builder.AppendFormat(" AND vResouce.ResourceCode LIKE '%{0}%' ", resCode);
            }
            if (!string.IsNullOrEmpty(resName))
            {
                builder.AppendFormat(" AND vResouce.ResourceName LIKE '%{0}%' ", resName);
            }
            if (!string.IsNullOrEmpty(specification))
            {
                builder.AppendFormat(" AND vResouce.Specification LIKE '%{0}%' ", specification);
            }
            if (!string.IsNullOrEmpty(brand))
            {
                builder.AppendFormat(" AND vResouce.Brand LIKE '%{0}%' ", brand);
            }
            if (!string.IsNullOrEmpty(taskCode))
            {
                builder.AppendFormat(" AND TaskCode LIKE '%{0}%' ", taskCode);
            }
            string cmdText = string.Empty;
            if (isWBSRelevance == "0")
            {
                cmdText = "\r\n                    SELECT TOP (@pageSize) * FROM (\r\n                    SELECT Row_Number()over(order by ResourceCode desc) as Num,'' TaskCode,'' TaskName,'' TaskId,'' OrderNumber,BudTaskRes.* FROM (\r\n                    SELECT Budres.ResourceId,vResouce.ResourceCode,vResouce.ResourceName, \r\n                    Name,vResouce.Specification,ResourcePrice,SUM(ResourceQuantity) ResourceQuantity,vResouce.Brand,\r\n                    vResouce.TechnicalParameter,res.Note FROM  Bud_TaskResource Budres \r\n                    LEFT JOIN vResouce ON Budres.ResourceId=vResouce.ResourceId\r\n                    INNER JOIN res_Resource res ON Budres.ResourceId=res.ResourceId\r\n                    WHERE PrjGuid=@prjId AND VerSions='1' ";
                cmdText = cmdText + builder.ToString() + " GROUP BY Budres.ResourceId,vResouce.ResourceCode,\r\n                        vResouce.ResourceName,Name,vResouce.Specification,\r\n                        ResourcePrice,vResouce.Brand,vResouce.TechnicalParameter,\r\n                        res.Note) BudTaskRes) tabBud WHERE Num > @pageSize*(@pageIndex-1)";
            }
            else
            {
                cmdText = "\r\n                    --DECLARE @prjId NVARCHAR(500),@pageSize INT, @pageIndex INT\r\n                    --SET @prjId='4c91f99d-2929-49dc-874b-953dc32a8776';\r\n                    --SET @pageSize=1000;\r\n                    --SET @pageIndex=1;\r\n                    WITH budRes AS\r\n                    (\r\n\t                    SELECT TaskCode,TaskName,BudTask.TaskId,OrderNumber,Budres.ResourceId\r\n\t                    ,budres.ResourcePrice*budres.ResourceQuantity ResTotal\r\n\t                    ,budres.ResourceQuantity ResQuantity\r\n\t                    FROM Bud_Task budTask \r\n\t                    INNER JOIN Bud_TaskResource budres ON budTask.TaskId=budres.TaskId\r\n\t                    WHERE budTask.PrjId=@prjId AND VerSion='1' AND TaskType=''\r\n                    ), modifyResBud AS\r\n                    (\r\n\t                    --根据原预算的资源进行变更的数据\r\n\t                    SELECT TaskCode,TaskName,BudTask.TaskId,OrderNumber,modifyInfo.ResourceId,\r\n\t                     modifyInfo.ModifyResTotal ResTotal,modifyQuantity ResQuantity\r\n\t                    FROM Bud_Task budTask \r\n\t                    INNER JOIN \r\n\t                    (\r\n\t\t                    SELECT ResourceId,SUM(ResourceQuantity*ResourcePrice) ModifyResTotal,\r\n\t\t                    SUM(ResourceQuantity) modifyQuantity,TaskId FROM Bud_Modify modify\r\n\t\t                    JOIN Bud_ModifyTask modifyTask ON modify.modifyId=modifyTask.modifyId\r\n\t\t                    JOIN Bud_ModifyTaskRes modifyTaskRes ON modifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId\r\n\t\t                    WHERE modify.PrjId=@prjId AND FlowState=1 AND modifyType=1\r\n\t\t                    GROUP BY ResourceId,TaskId\r\n\t                    ) modifyInfo ON budTask.TaskId=modifyInfo.TaskId  \r\n                    ),budAllRes AS\r\n                    (\r\n\t                    --原预算节点的所有资源\r\n\t                    SELECT TaskCode,TaskName,TaskId,OrderNumber,ResourceId\r\n\t\t                    ,SUM(ResTotal) ResTotal,SUM(ResQuantity) ResQuantity FROM \r\n\t                    (\r\n\t\t                    select * from budRes\r\n\t\t                    UNION ALL\r\n\t\t                    select * from modifyResBud\r\n\t                    ) tab\r\n\t                    GROUP BY TaskCode,TaskName,TaskId,OrderNumber,ResourceId\r\n                    ),outModifyTask AS\r\n                    (\r\n\t                    --清单外的节点\r\n\t                    SELECT ModifyTaskCode TaskCode,ModifyTaskContent TaskName,\r\n\t                    modifyTask.modifyTaskId,OrderNumber FROM Bud_Modify modify\r\n\t                    JOIN Bud_ModifyTask modifyTask ON modify.modifyId=modifyTask.modifyId\r\n\t                    WHERE modify.PrjId=@prjId AND FlowState=1 AND modifyType=0\r\n                    ),outModifyRes AS\r\n                    (\r\n\t                    --直接在清单外做的资源\r\n\t                    SELECT  TaskCode,TaskName,outModifyTask.ModifyTaskId,OrderNumber,ResourceId,\r\n\t                    SUM(ResourceQuantity*ResourcePrice) ModifyResTotal,SUM(ResourceQuantity) modifyQuantity\r\n                        FROM outModifyTask \r\n\t                    JOIN Bud_ModifyTaskRes modifyTaskRes ON outModifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId\r\n\t                    GROUP BY TaskCode,TaskName,OrderNumber,ResourceId,outModifyTask.ModifyTaskId\r\n                    ), inModifyRes AS\r\n                    (\r\n\t                    --针对清单外做的新增资源\r\n\t                    SELECT TaskCode,TaskName,outModifyTask.ModifyTaskId,OrderNumber,ResourceId, \r\n\t                    ModifyResTotal,modifyQuantity FROM outModifyTask INNER JOIN \r\n\t                    (\r\n\t\t                    SELECT ResourceId,SUM(ResourceQuantity*ResourcePrice) ModifyResTotal,\r\n\t\t                    SUM(ResourceQuantity) modifyQuantity,TaskId FROM Bud_Modify modify\r\n\t\t                    JOIN Bud_ModifyTask modifyTask ON modify.modifyId=modifyTask.modifyId\r\n\t\t                    JOIN Bud_ModifyTaskRes modifyTaskRes ON modifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId\r\n\t\t                    WHERE modify.PrjId=@prjId AND FlowState=1 AND modifyType=1\r\n\t\t                    GROUP BY ResourceId,TaskId\r\n\t                    ) inModifyTaskRes ON outModifyTask.ModifyTaskId=inModifyTaskRes.TaskId\r\n                    ), outAllRes AS\r\n                    (\r\n\t                    --针对清单外做的所有资源\r\n\t                    SELECT TaskCode,TaskName,ModifyTaskId TaskId,OrderNumber,ResourceId\r\n\t\t                    ,SUM(ModifyResTotal) ResTotal,SUM(modifyQuantity) ResQuantity FROM \r\n\t                    (\r\n\t\t                    select * from outModifyRes\r\n\t\t                    UNION ALL\r\n\t\t                    select * from inModifyRes\r\n\t                    ) modifyRes\r\n\t                    GROUP BY TaskCode,TaskName,ModifyTaskId,OrderNumber,ResourceId\r\n                    )\r\n                    SELECT TOP (@pageSize) * FROM (\r\n                        SELECT Row_Number()over(order by OrderNumber,vResouce.ResourceCode desc) as Num,TaskCode,TaskName,TaskId,\r\n                        OrderNumber,aLLTaskRes.ResourceId,\r\n                        CONVERT(DECIMAL(18,3),ISNULL(ResTotal/NULLIF(ResQuantity,0),0)) ResourcePrice, ResQuantity ResourceQuantity,vResouce.ResourceCode,vResouce.ResourceName, \r\n                        Name,vResouce.Specification,vResouce.Brand,vResouce.TechnicalParameter, res.ModelNumber,res.Note \t FROM \r\n                        (\r\n\t                        SELECT * FROM budAllRes\r\n\t                        UNION \r\n\t                        SELECT * FROM outAllRes\r\n                        ) allTaskRes\r\n                        LEFT JOIN vResouce ON allTaskRes.ResourceId=vResouce.ResourceId\r\n                        INNER JOIN res_Resource res ON allTaskRes.ResourceId=res.ResourceId\r\n                        WHERE 1=1 ";
                cmdText = cmdText + builder.ToString() + ") tab WHERE Num > @pageSize*(@pageIndex-1)";
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@pageSize", pageSize), new SqlParameter("@pageIndex", pageIndex) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public int GetResCountByBud(string resCode, string resName, string specification, string brand, string prjId, int version, string isWBSRelevance)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" AND vResouce.ResourceCode LIKE '%{0}%' ", resCode);
            builder.AppendFormat(" AND vResouce.ResourceName LIKE '%{0}%' ", resName);
            if (!string.IsNullOrEmpty(specification))
            {
                builder.AppendFormat(" AND vResouce.Specification LIKE '%{0}%' ", specification);
            }
            if (!string.IsNullOrEmpty(brand))
            {
                builder.AppendFormat(" AND vResouce.Brand LIKE '%{0}%' ", brand);
            }
            string cmdText = string.Empty;
            if (isWBSRelevance == "0")
            {
                cmdText = "\r\n                        SELECT COUNT(*) FROM (\r\n                        SELECT Budres.ResourceId,vResouce.ResourceCode,vResouce.ResourceName, \r\n                        Name,vResouce.Specification,ResourcePrice,SUM(ResourceQuantity) ResourceQuantity FROM Bud_TaskResource Budres \r\n                        LEFT JOIN vResouce ON Budres.ResourceId=vResouce.ResourceId\r\n                        INNER JOIN res_Resource res ON Budres.ResourceId=res.ResourceId\r\n                        WHERE PrjGuid=@prjId AND VerSions=@version \r\n                ";
                cmdText = cmdText + builder.ToString() + "\r\n                    GROUP BY Budres.ResourceId,vResouce.ResourceCode,vResouce.ResourceName,Name,vResouce.Specification, \r\n                    ResourcePrice,vResouce.Brand,vResouce.TechnicalParameter,res.Note\r\n                    ) BudTaskRes\r\n                ";
            }
            if (isWBSRelevance == "1")
            {
                cmdText = "\r\n                    --DECLARE @prjId NVARCHAR(500),@version INT \r\n                    --SET @prjId='4c91f99d-2929-49dc-874b-953dc32a8776';\r\n                    --SET @version=1;\r\n                    WITH budRes AS\r\n                    (\r\n\t                    SELECT TaskCode,TaskName,BudTask.TaskId,OrderNumber,Budres.ResourceId\r\n\t                    ,budres.ResourcePrice*budres.ResourceQuantity ResTotal\r\n\t                    ,budres.ResourceQuantity ResQuantity\r\n\t                    FROM Bud_Task budTask \r\n\t                    INNER JOIN Bud_TaskResource budres ON budTask.TaskId=budres.TaskId\r\n\t                    WHERE budTask.PrjId=@prjId AND VerSion=@version AND TaskType=''\r\n                    ), modifyResBud AS\r\n                    (\r\n\t                    --根据原预算的资源进行变更的数据\r\n\t                    SELECT TaskCode,TaskName,BudTask.TaskId,OrderNumber,modifyInfo.ResourceId,\r\n\t                     modifyInfo.ModifyResTotal ResTotal,modifyQuantity ResQuantity\r\n\t                    FROM Bud_Task budTask \r\n\t                    INNER JOIN \r\n\t                    (\r\n\t\t                    SELECT ResourceId,SUM(ResourceQuantity*ResourcePrice) ModifyResTotal,\r\n\t\t                    SUM(ResourceQuantity) modifyQuantity,TaskId FROM Bud_Modify modify\r\n\t\t                    JOIN Bud_ModifyTask modifyTask ON modify.modifyId=modifyTask.modifyId\r\n\t\t                    JOIN Bud_ModifyTaskRes modifyTaskRes ON modifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId\r\n\t\t                    WHERE modify.PrjId=@prjId AND FlowState=1 AND modifyType=1\r\n\t\t                    GROUP BY ResourceId,TaskId\r\n\t                    ) modifyInfo ON budTask.TaskId=modifyInfo.TaskId  \r\n                    ),budAllRes AS\r\n                    (\r\n\t                    --原预算节点的所有资源\r\n\t                    SELECT TaskCode,TaskName,TaskId,OrderNumber,ResourceId\r\n\t\t                    ,SUM(ResTotal) ResTotal,SUM(ResQuantity) ResQuantity FROM \r\n\t                    (\r\n\t\t                    select * from budRes\r\n\t\t                    UNION ALL\r\n\t\t                    select * from modifyResBud\r\n\t                    ) tab\r\n\t                    GROUP BY TaskCode,TaskName,TaskId,OrderNumber,ResourceId\r\n                    ),outModifyTask AS\r\n                    (\r\n\t                    --清单外的节点\r\n\t                    SELECT ModifyTaskCode TaskCode,ModifyTaskContent TaskName,\r\n\t                    modifyTask.modifyTaskId,OrderNumber FROM Bud_Modify modify\r\n\t                    JOIN Bud_ModifyTask modifyTask ON modify.modifyId=modifyTask.modifyId\r\n\t                    WHERE modify.PrjId=@prjId AND FlowState=1 AND modifyType=0\r\n                    ),outModifyRes AS\r\n                    (\r\n\t                    --直接在清单外做的资源\r\n\t                    SELECT  TaskCode,TaskName,outModifyTask.ModifyTaskId,OrderNumber,ResourceId,\r\n\t                    SUM(ResourceQuantity*ResourcePrice) ModifyResTotal,SUM(ResourceQuantity) modifyQuantity\r\n                        FROM outModifyTask \r\n\t                    JOIN Bud_ModifyTaskRes modifyTaskRes ON outModifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId\r\n\t                    GROUP BY TaskCode,TaskName,OrderNumber,ResourceId,outModifyTask.ModifyTaskId\r\n                    ), inModifyRes AS\r\n                    (\r\n\t                    --针对清单外做的新增资源\r\n\t                    SELECT TaskCode,TaskName,outModifyTask.ModifyTaskId,OrderNumber,ResourceId, \r\n\t                    ModifyResTotal,modifyQuantity FROM outModifyTask INNER JOIN \r\n\t                    (\r\n\t\t                    SELECT ResourceId,SUM(ResourceQuantity*ResourcePrice) ModifyResTotal,\r\n\t\t                    SUM(ResourceQuantity) modifyQuantity,TaskId FROM Bud_Modify modify\r\n\t\t                    JOIN Bud_ModifyTask modifyTask ON modify.modifyId=modifyTask.modifyId\r\n\t\t                    JOIN Bud_ModifyTaskRes modifyTaskRes ON modifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId\r\n\t\t                    WHERE modify.PrjId=@prjId AND FlowState=1 AND modifyType=1\r\n\t\t                    GROUP BY ResourceId,TaskId\r\n\t                    ) inModifyTaskRes ON outModifyTask.ModifyTaskId=inModifyTaskRes.TaskId\r\n                    ), outAllRes AS\r\n                    (\r\n\t                    --针对清单外做的所有资源\r\n\t                    SELECT TaskCode,TaskName,ModifyTaskId TaskId,OrderNumber,ResourceId\r\n\t\t                    ,SUM(ModifyResTotal) ResTotal,SUM(modifyQuantity) ResQuantity FROM \r\n\t                    (\r\n\t\t                    select * from outModifyRes\r\n\t\t                    UNION ALL\r\n\t\t                    select * from inModifyRes\r\n\t                    ) modifyRes\r\n\t                    GROUP BY TaskCode,TaskName,ModifyTaskId,OrderNumber,ResourceId\r\n                    )\r\n                    SELECT COUNT(*)\t FROM \r\n                    (\r\n\t                    SELECT * FROM budAllRes\r\n\t                    UNION \r\n\t                    SELECT * FROM outAllRes\r\n                    ) aLLTaskRes\r\n                    LEFT JOIN vResouce ON aLLTaskRes.ResourceId=vResouce.ResourceId\r\n                    WHERE 1=1";
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", SqlDbType.NVarChar, 500), new SqlParameter("@version", SqlDbType.Int) };
            commandParameters[0].Value = prjId;
            commandParameters[1].Value = version;
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        public DataTable GetTableByParam(string startTime, string endTime, string wpcode, string[] ResourceNames, string[] ResourceCodes, string prjName, string category, string specification, string brand, string modelNumber)
        {
            int num = 0;
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select p5.swid, p1.wpcode,ISNULL(p6.prjCode,PT_PrjInfo_ZTB.prjCode) AS prjCode,ISNULL(p6.prjName,PT_PrjInfo_ZTB.prjName) AS prjName,ISNULL(p6.prjGuid,PT_PrjInfo_ZTB.prjGuid) AS prjGuid,p2.resourceCode,");
            builder.Append("P2.Specification,P2.TechnicalParameter,P2.ModelNumber,P2.Brand,");
            builder.Append("p2.ResourceName,p1.number,convert(nvarchar(10),p5.intime,121) as intime,Res_ResourceType.ResourceTypeName from dbo.Sm_Wantplan_Stock as p1  ");
            builder.Append("inner join Res_Resource as p2 on p1.scode=p2.resourceCode ");
            builder.Append("inner join Res_Unit as p3 on p3.UnitID=p2.Unit ");
            builder.Append("inner join dbo.Sm_Wantplan as p5 on p1.wpcode=p5.swcode ");
            builder.Append("left join dbo.PT_PrjInfo as p6 on p5.procode=p6.prjGuid ");
            builder.Append("left join PT_PrjInfo_ZTB on PT_PrjInfo_ZTB.prjGuid=p5.procode ");
            builder.Append("inner join Res_ResourceType on Res_ResourceType.ResourceTypeId = ResourceType");
            builder.Append(" where p5.flowstate = 1");
            if (!string.IsNullOrEmpty(wpcode))
            {
                builder.Append(" and p1.wpcode like @wpcode");
                list.Add(new SqlParameter("@wpcode", '%' + wpcode.Trim() + '%'));
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.Append(" and ISNULL(p6.prjName,PT_PrjInfo_ZTB.prjName) like @prjName");
                list.Add(new SqlParameter("@prjName", '%' + prjName.Trim() + '%'));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and p5.intime>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and p5.intime<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            if (!string.IsNullOrEmpty(category))
            {
                builder.Append(" and Res_ResourceType.ResourceTypeName like @category");
                list.Add(new SqlParameter("@category", '%' + category + '%'));
            }
            if (!string.IsNullOrEmpty(specification))
            {
                builder.Append(" and P2.Specification like @specification");
                list.Add(new SqlParameter("@specification", '%' + specification + '%'));
            }
            if (!string.IsNullOrEmpty(brand))
            {
                builder.Append(" and P2.Brand like @brand");
                list.Add(new SqlParameter("@brand", '%' + brand + '%'));
            }
            if (!string.IsNullOrEmpty(modelNumber))
            {
                builder.Append(" and P2.ModelNumber like @modelNumber");
                list.Add(new SqlParameter("@modelNumber", '%' + modelNumber + '%'));
            }
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(ResourceNames[i]))
                {
                    num = 1;
                    if (i == 0)
                    {
                        builder.Append(" and (p2.ResourceName like @ResourceName" + i);
                    }
                    else
                    {
                        builder.Append(" or p2.ResourceName like @ResourceName" + i);
                    }
                    list.Add(new SqlParameter("@ResourceName" + i, "%" + ResourceNames[i].Trim() + "%"));
                }
            }
            if (num == 1)
            {
                builder.Append(")");
                num = 0;
            }
            for (int j = 0; j < ResourceCodes.Length; j++)
            {
                if (!string.IsNullOrEmpty(ResourceCodes[j]))
                {
                    num = 1;
                    if (j == 0)
                    {
                        builder.Append(" and (p2.resourceCode like @ResourceCode" + j);
                    }
                    else
                    {
                        builder.Append(" or p2.resourceCode like @ResourceCode" + j);
                    }
                    list.Add(new SqlParameter("@ResourceCode" + j, "%" + ResourceCodes[j].Trim() + "%"));
                }
            }
            if (num == 1)
            {
                builder.Append(")");
                num = 0;
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        private void PrepareAddCommand(MaterialPlanStockModel model, out string cmdText, out SqlParameter[] parameters)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Wantplan_Stock(");
            builder.Append("wpsid,scode,wpcode,number,arrivalDate,DesignCode,Remark,TaskId)");
            builder.Append(" values (");
            builder.Append("@wpsid,@scode,@wpcode,@number,@arrivalDate,@designCode,@remark,@taskId)");
            parameters = new SqlParameter[] { new SqlParameter("@wpsid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@wpcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@arrivalDate", SqlDbType.DateTime), new SqlParameter("@designCode", SqlDbType.NVarChar, 100), new SqlParameter("@remark", SqlDbType.NVarChar), new SqlParameter("@taskId", SqlDbType.NVarChar, 0x40) };
            parameters[0].Value = model.wpsid;
            parameters[1].Value = model.scode;
            parameters[2].Value = model.wpcode;
            parameters[3].Value = model.number;
            if (model.ArrivalDate == null)
            {
                parameters[4].Value = DBNull.Value;
            }
            else
            {
                parameters[4].Value = model.ArrivalDate;
            }
            parameters[5].Value = model.DesignCode;
            parameters[6].Value = model.Remark;
            if (string.IsNullOrEmpty(model.TaskId))
            {
                parameters[7].Value = string.Empty;
            }
            else
            {
                parameters[7].Value = model.TaskId;
            }
            cmdText = builder.ToString();
        }

        public MaterialPlanStockModel ReaderBind(IDataReader dataReader)
        {
            MaterialPlanStockModel model = new MaterialPlanStockModel {
                wpsid = dataReader["wpsid"].ToString(),
                scode = dataReader["scode"].ToString(),
                wpcode = dataReader["wpcode"].ToString()
            };
            object obj2 = dataReader["number"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.number = Convert.ToInt32(obj2);
            }
            obj2 = dataReader["arrivalDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ArrivalDate = obj2.ToString();
            }
            model.Remark = dataReader["remark"].ToString();
            return model;
        }

        public DataTable showMaterialListForAdd(string codeList, string prjId, string taskIdList, int count)
        {
            string cmdText = "\r\n                    --DECLARE @prjId NVARCHAR(500),@count int;\r\n                    --SET @prjId='c085a55e-f6e2-4297-be37-caf873e7e3db';\r\n                    --SET @count = 2;\r\n                    WITH budRes AS\r\n                    (\r\n                        SELECT TaskCode,TaskName,BudTask.TaskId,OrderNumber,budres.ResourceId\r\n                        ,budres.ResourcePrice*budres.ResourceQuantity ResTotal\r\n                        ,budres.ResourceQuantity ResQuantity\r\n                        FROM Bud_Task budTask \r\n                        INNER JOIN Bud_TaskResource budres ON budTask.TaskId=budres.TaskId\r\n                        WHERE budTask.PrjId=@prjId AND VerSion=1 AND TaskType=''\r\n                    ), modifyResBud AS\r\n                    (\r\n                        --根据原预算的资源进行变更的数据\r\n                        SELECT TaskCode,TaskName,BudTask.TaskId,OrderNumber,modifyInfo.ResourceId,\r\n                         modifyInfo.ModifyResTotal ResTotal,modifyQuantity ResQuantity\r\n                        FROM Bud_Task budTask \r\n                        INNER JOIN \r\n                        (\r\n                            SELECT ResourceId,SUM(ResourceQuantity*ResourcePrice) ModifyResTotal,\r\n                            SUM(ResourceQuantity) modifyQuantity,TaskId FROM Bud_Modify modify\r\n                            JOIN Bud_ModifyTask modifyTask ON modify.modifyId=modifyTask.modifyId\r\n                            JOIN Bud_ModifyTaskRes modifyTaskRes ON modifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId\r\n                            WHERE modify.PrjId=@prjId AND FlowState=1 AND modifyType=1\r\n                            GROUP BY ResourceId,TaskId\r\n                        ) modifyInfo ON budTask.TaskId=modifyInfo.TaskId  \r\n                    ),budAllRes AS\r\n                    (\r\n                        --原预算节点的所有资源\r\n                        SELECT TaskCode,TaskName,TaskId,OrderNumber,ResourceId\r\n                            ,SUM(ResTotal) ResTotal,SUM(ResQuantity) ResQuantity FROM \r\n                        (\r\n                            select * from budRes\r\n                            UNION ALL\r\n                            select * from modifyResBud\r\n                        ) tab\r\n                        GROUP BY TaskCode,TaskName,TaskId,OrderNumber,ResourceId\r\n                    ),outModifyTask AS\r\n                    (\r\n                        --清单外的节点\r\n                        SELECT ModifyTaskCode TaskCode,ModifyTaskContent TaskName,\r\n                        modifyTask.modifyTaskId,OrderNumber FROM Bud_Modify modify\r\n                        JOIN Bud_ModifyTask modifyTask ON modify.modifyId=modifyTask.modifyId\r\n                        WHERE modify.PrjId=@prjId AND FlowState=1 AND modifyType=0\r\n                    ),outModifyRes AS\r\n                    (\r\n                        --直接在清单外做的资源\r\n                        SELECT  TaskCode,TaskName,outModifyTask.ModifyTaskId,OrderNumber,ResourceId,\r\n                        SUM(ResourceQuantity*ResourcePrice) ModifyResTotal,SUM(ResourceQuantity) modifyQuantity\r\n                        FROM outModifyTask \r\n                        JOIN Bud_ModifyTaskRes modifyTaskRes ON outModifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId\r\n                        GROUP BY TaskCode,TaskName,OrderNumber,ResourceId,outModifyTask.ModifyTaskId\r\n                    ), inModifyRes AS\r\n                    (\r\n                        --针对清单外做的新增资源\r\n                        SELECT TaskCode,TaskName,outModifyTask.ModifyTaskId,OrderNumber,ResourceId, \r\n                        ModifyResTotal,modifyQuantity FROM outModifyTask INNER JOIN \r\n                        (\r\n                            SELECT ResourceId,SUM(ResourceQuantity*ResourcePrice) ModifyResTotal,\r\n                            SUM(ResourceQuantity) modifyQuantity,TaskId FROM Bud_Modify modify\r\n                            JOIN Bud_ModifyTask modifyTask ON modify.modifyId=modifyTask.modifyId\r\n                            JOIN Bud_ModifyTaskRes modifyTaskRes ON modifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId\r\n                            WHERE modify.PrjId=@prjId AND FlowState=1 AND modifyType=1\r\n                            GROUP BY ResourceId,TaskId\r\n                        ) inModifyTaskRes ON outModifyTask.ModifyTaskId=inModifyTaskRes.TaskId\r\n                    ), outAllRes AS\r\n                    (\r\n                        --针对清单外做的所有资源\r\n                        SELECT TaskCode,TaskName,ModifyTaskId TaskId,OrderNumber,ResourceId\r\n                            ,SUM(ModifyResTotal) ResTotal,SUM(modifyQuantity) ResQuantity FROM \r\n                        (\r\n                            select * from outModifyRes\r\n                            UNION ALL\r\n                            select * from inModifyRes\r\n                        ) modifyRes\r\n                        GROUP BY TaskCode,TaskName,ModifyTaskId,OrderNumber,ResourceId\r\n                    ),cteTaskRes AS\r\n                    (\r\n\t                    SELECT Row_Number()over(order by OrderNumber,vResouce.ResourceCode desc) as Num,TaskCode,TaskName,TaskId,\r\n                        OrderNumber,aLLTaskRes.ResourceId,\r\n                        CONVERT(DECIMAL(18,3),ISNULL(ResTotal/NULLIF(ResQuantity,0),0)) ResourcePrice,\r\n\t                    ResQuantity ResourceQuantity,vResouce.ResourceCode,vResouce.ResourceName, \r\n                        Name,vResouce.Specification,vResouce.Brand,vResouce.TechnicalParameter,res.Note\r\n\t                    FROM \r\n                        (\r\n                            SELECT * FROM budAllRes\r\n                            UNION \r\n                            SELECT * FROM outAllRes\r\n                        ) allTaskRes\r\n                        LEFT JOIN vResouce ON allTaskRes.ResourceId=vResouce.ResourceId\r\n                        INNER JOIN res_Resource res ON allTaskRes.ResourceId=res.ResourceId\r\n                        WHERE 1=1  AND vResouce.ResourceCode LIKE '%%'  AND vResouce.ResourceName LIKE '%%'\r\n                    ),cteWantStock AS\r\n                    (\r\n\t                    SELECT row_number() OVER(PARTITION BY Res_Resource.ResourceCode ORDER BY cteTaskRes.TaskId) Num,\r\n\t                    Res_Resource.ResourceId,Res_Resource.ResourceCode,Res_Resource.ResourceName,Res_Resource.Specification,Res_Unit.Name AS UnitName,PriceValue AS Price,\r\n\t                    Res_PriceType.PriceTypeId,'' DesignCode,wpsid,scode,ISNULL(wpcode,1) wpcode,number,arrivalDate,'' Remark,\r\n\t                    cteTaskRes.TaskId,Res_Resource.Brand,ModelNumber,Res_Resource.TechnicalParameter \r\n\t                    FROM Res_Resource \r\n\t                    LEFT JOIN cteTaskRes ON Res_Resource.ResourceId = cteTaskRes.ResourceId\r\n\t                    LEFT JOIN Sm_WantPlan_Stock ON Res_Resource.ResourceCode=scode \r\n\t                    LEFT JOIN Res_Unit ON UnitId = Unit \r\n\t                    LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId";
            cmdText = ((cmdText + " AND PriceTypeId='" + BudgetPriceType + "'") + " LEFT JOIN Res_PriceType ON Res_PriceType.PriceTypeId = Res_Price.PriceTypeId") + " WHERE Res_Resource.ResourceId IN(" + codeList + ")";
            if (!string.IsNullOrEmpty(taskIdList))
            {
                cmdText = cmdText + " AND cteTaskRes.TaskId IN(" + taskIdList + ")";
            }
            cmdText = cmdText + ")SELECT * FROM cteWantStock WHERE Num = 1 --AND Num<=@count";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", SqlDbType.NVarChar), new SqlParameter("@count", SqlDbType.Int) };
            commandParameters[0].Value = prjId;
            commandParameters[1].Value = count;
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable showMaterialListForUpdate(string code)
        {
            string cmdText = "\r\n                     WITH cteBudTask AS \r\n                    (\r\n\t                    SELECT TaskId,TaskName FROM Bud_Task\r\n                    ),cteBudModify AS \r\n                    (\r\n\t                    SELECT ModifyTaskId AS TaskId,ModifyTaskContent AS TaskName\r\n\t                    FROM Bud_ModifyTask modifyTask\r\n\t                    JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n\t                    WHERE ModifyType=0 AND FlowState=1 \r\n                    ),cteTask AS\r\n                    (\r\n\t                    SELECT TaskId,TaskName FROM cteBudTask\r\n\t                    UNION\r\n\t                    SELECT TaskId,TaskName FROM cteBudModify\r\n                    )\r\n                    SELECT ResourceCode,ResourceName,Specification,\r\n                    Sm_WantPlan_Stock.*, Name AS UnitName, PriceValue AS Price,DesignCode\r\n                    ,Res_Resource.Brand,ModelNumber,TechnicalParameter,TaskName\r\n                    FROM Sm_WantPlan_Stock\r\n                    INNER JOIN Res_Resource ON ResourceCode=Sm_WantPlan_Stock.scode  \r\n                    LEFT JOIN Res_Unit ON UnitId = Unit \r\n                    LEFT JOIN cteTask ON Sm_WantPlan_Stock.TaskId = cteTask.TaskId\r\n                    LEFT JOIN (\r\n\t                    SELECT *\r\n\t                    FROM Res_Price\r\n\t                    WHERE PriceTypeId = @priceTypeId\r\n                    ) AS price ON price.ResourceId = Res_Resource.ResourceId\r\n                    WHERE wpcode=@code ORDER BY ResourceCode DESC ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@code", SqlDbType.NVarChar, 0x40), new SqlParameter("@priceTypeId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = code;
            commandParameters[1].Value = BudgetPriceType;
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public int Update(MaterialPlanStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Wantplan_Stock set ");
            builder.Append("scode=@scode,");
            builder.Append("wpcode=@wpcode,");
            builder.Append("number=@number, ");
            builder.Append("arrivalDate=@arrivalDate, ");
            builder.Append("DesignCode=@designCode, ");
            builder.Append("Remark=@remark ");
            builder.Append(" where wpsid=@wpsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@wpsid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@wpcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@arrivalDate", SqlDbType.DateTime), new SqlParameter("@designCode", SqlDbType.NVarChar, 100), new SqlParameter("@remark", SqlDbType.NVarChar) };
            commandParameters[0].Value = model.wpsid;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.wpcode;
            commandParameters[3].Value = model.number;
            commandParameters[4].Value = model.ArrivalDate;
            commandParameters[5].Value = model.DesignCode;
            commandParameters[6].Value = model.Remark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int updateMorePlanStock(string codeList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_WantPlan_Stock where wpsid in (");
            builder.Append(codeList);
            builder.Append(" )");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }
    }
}

