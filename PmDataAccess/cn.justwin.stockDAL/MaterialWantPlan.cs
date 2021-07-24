namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MaterialWantPlan
    {
        public int Add(MaterialWantPlanModel model)
        {
            SqlParameter[] parameterArray;
            string cmdText = string.Empty;
            this.PrepareAddCommand(model, out cmdText, out parameterArray);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, parameterArray);
        }

        public int Add(SqlTransaction trans, MaterialWantPlanModel model)
        {
            SqlParameter[] parameterArray;
            string cmdText = string.Empty;
            this.PrepareAddCommand(model, out cmdText, out parameterArray);
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, parameterArray);
        }

        public int AddModel(MaterialWantPlanModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Wantplan(");
            builder.Append("swid,swcode,flowstate,person,intime,acceptstate,annx,explain,procode,EquipmentId)");
            builder.Append(" values (");
            builder.Append("@swid,@swcode,@flowstate,@person,@intime,@acceptstate,@annx,@explain,@procode,@EquipmentId)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@swid", SqlDbType.NVarChar, 50), new SqlParameter("@swcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@acceptstate", SqlDbType.Int, 4), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@procode", SqlDbType.NVarChar, 100), new SqlParameter("@ContainPending", SqlDbType.Bit), new SqlParameter("@EquipmentId", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = model.swid;
            commandParameters[1].Value = model.swcode;
            commandParameters[2].Value = model.flowstate;
            commandParameters[3].Value = model.person;
            commandParameters[4].Value = model.intime;
            commandParameters[5].Value = model.acceptstate;
            commandParameters[6].Value = model.annx;
            commandParameters[7].Value = model.explain;
            commandParameters[8].Value = model.procode;
            commandParameters[9].Value = model.ContainPending;
            if (!string.IsNullOrEmpty(model.EquipmentId))
            {
                commandParameters[10].Value = model.EquipmentId;
            }
            else
            {
                commandParameters[10].Value = DBNull.Value;
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string swcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Wantplan_stock ");
            builder.Append(" where wpcode=@swcode ");
            builder.Append("delete from Sm_Wantplan ");
            builder.Append(" where swcode=@swcode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@swcode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = swcode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int ExcuteSql(string sql)
        {
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public DataTable getAnnxList(string recordcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select AnnexCode,OriginalName,FilePath from XPM_Basic_AnnexList where Recordcode=@RecodeCode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RecodeCode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = recordcode;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public string GetCodeByGuid(string guid)
        {
            string cmdText = "select swcode from Sm_Wantplan where swid = @swid";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@swid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = guid;
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        public DataTable GetDiff(string planCode, string prjCode, string isWBSRelevance)
        {
            string str = string.Empty;
            string str2 = "0";
            string cmdText = "SELECT paravalue FROM Sm_Set WHERE paraname='IsContainPending'";
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
            }
            if (Convert.ToBoolean(Convert.ToInt32(str2.Trim())))
            {
                str = "FlowState IN('1','0')";
            }
            else
            {
                str = "FlowState = 1";
            }
            string str4 = string.Empty;
            if (isWBSRelevance == "1")
            {
                str4 = string.Format("\r\n\t\t\t\t--DECLARE @prjCode nvarchar(200);\r\n\t\t\t\t--DECLARE @planCode nvarchar(200);\r\n\t\t\t\t--SET @prjCode = '6172309c-7ef8-4fd3-895e-080e903f6da3';\r\n\t\t\t\t--SET @planCode = '20121219101520';\r\n\t\t\t\tWITH cte_e AS\t\t\t--本次计划量\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT s.scode AS rCode,s.number AS eNumber, r.ResourceId, r.ResourceName, \r\n\t\t\t\t\t\tm.ParentId, r2.ResourceCode AS ParentCode\r\n\t\t\t\t\tFROM Sm_Wantplan_Stock AS s\r\n\t\t\t\t\tJOIN Sm_Wantplan AS w ON w.swcode = s.wpcode\r\n\t\t\t\t\tINNER JOIN Res_Resource r ON r.ResourceCode = s.scode\r\n\t\t\t\t\tLEFT JOIN Res_Mapping m ON m.ResourceId = r.ResourceId\r\n\t\t\t\t\tLEFT JOIN Res_Resource r2 ON r2.ResourceId = m.ParentId\r\n\t\t\t\t\tWHERE procode = @prjCode AND wpcode = @planCode\r\n\t\t\t\t), cte_a AS\t\t\t\t--以消耗量\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT s.scode AS rCode,sum(s.number) AS aNumber FROM Sm_Wantplan_Stock AS s\r\n\t\t\t\t\tJOIN Sm_Wantplan AS w ON w.swcode = s.wpcode\r\n\t\t\t\t\tWHERE procode = @prjCode AND wpcode != @planCode AND {0} \r\n\t\t\t\t\tGROUP by s.scode\r\n\t\t\t\t), cte_f AS\t\t\t\t--预算量\r\n\t\t\t\t(\r\n                    SELECT SUM(ResQuantity)  AS fNumber,ResourceCode AS rCode  FROM \r\n                    (\r\n                        SELECT (SUM(Bud_TaskResource.ResourceQuantity)*Bud_TaskResource.ResourcePrice) ResTotal,\r\n                        SUM(Bud_TaskResource.ResourceQuantity) ResQuantity,Bud_TaskResource.ResourceId\r\n                        FROM Bud_TaskResource JOIN Bud_Task ON Bud_TaskResource.TaskId=Bud_Task.TaskId\r\n                        JOIN  vGetCurBudVersion ON Bud_Task.PrjId=vGetCurBudVersion.PrjId \r\n\t\t\t\t\t\t\tAND Bud_Task.version=vGetCurBudVersion.curversion AND TaskType=''\r\n\t\t\t\t\t\t\tAND Bud_Task.PrjId=@prjCode\r\n                        GROUP BY Bud_TaskResource.ResourcePrice,Bud_TaskResource.ResourceId\r\n                        UNION ALL\r\n                        SELECT (SUM(ResourceQuantity)*ResourcePrice) ReseTotal,SUM(ResourceQuantity) ResQuantity,\r\n                        ResourceId FROM Bud_ModifyTaskRes modifyTaskRes\r\n                        JOIN Bud_ModifyTask modifyTask ON modifyTaskRes.ModifyTaskId=modifyTask.ModifyTaskId\r\n                        JOIN Bud_MOdify modify ON modifyTask.modifyId=modify.modifyId\r\n                        WHERE modify.FlowState='1' AND modify.PrjId=@prjCode\r\n                        GROUP BY ResourcePrice,ResourceId\r\n                    ) allBudResInfo \r\n                    INNER JOIN Res_Resource AS r ON allBudResInfo.ResourceId=r.ResourceId\r\n                    GROUP BY r.ResourceCode\r\n\t\t\t\t), cte_p AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT e.ParentCode AS rCode, \r\n\t\t\t\t\t    fNumber,\r\n\t\t\t\t\t\tSUM(aNumber) AS aNumber,\r\n\t\t\t\t\t\tSUM(eNumber) AS eNumber\r\n\t\t\t\t\tFROM cte_e AS e\r\n\t\t\t\t\tLEFT JOIN cte_f AS f ON f.rCode = e.ParentCode\r\n\t\t\t\t\tLEFT JOIN cte_a AS a ON a.rCode = e.rCode\r\n\t\t\t\t\tGROUP BY e.ParentId, e.ParentCode, fNumber\r\n\t\t\t\t\tHAVING ParentId IS NOT NULL\r\n\t\t\t\t), cte_r AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT e.rCode, f.fNumber, a.aNumber,SUM(e.eNumber) AS eNumber\r\n\t\t\t\t\tFROM cte_e AS e\r\n\t\t\t\t\tLEFT JOIN cte_f AS f ON f.rCode = e.rCode\r\n\t\t\t\t\tLEFT JOIN cte_a AS a ON a.rCode = e.rCode\r\n                    GROUP BY e.rCode, f.fNumber, a.aNumber\r\n\t\t\t\t), cte_z AS \r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT * FROM cte_r\r\n\t\t\t\t\tUNION \r\n\t\t\t\t\tSELECT * FROM cte_p\r\n\t\t\t\t), cte_no AS \r\n\t\t\t\t(\r\n                    SELECT * FROM\r\n                    (\r\n\t                    SELECT m.ResourceId,'' ResourceCode,m.ParentId + m.ResourceId AS ParentId\r\n\t                    FROM Res_Mapping AS m\r\n\t                    UNION\r\n\t                    SELECT r.ResourceId,r.ResourceCode, r.ResourceId AS ParentId\r\n\t                    FROM Res_Resource r\r\n\t                    WHERE r.ResourceId IN (SELECT ParentId FROM Res_Mapping)\r\n                    ) TA WHERE ResourceCode IN (SELECT rCode FROM cte_z)\r\n\t\t\t\t)\r\n\t\t\t\tSELECT r.ResourceId, r.ResourceName, r.Specification, \r\n\t\t\t\t\tISNULL(z.fNumber, 0.000) AS fNumber,\t\t--预算量\t\t\t\r\n\t\t\t\t\tISNULL(z.aNumber, 0.000) AS aNumber,\t\t--已消耗量\r\n\t\t\t\t\tISNULL(z.eNumber, 0.000) AS eNumber,\t\t--本次计划量\r\n\t\t\t\t\tISNULL(CONVERT(DECIMAL(18,4), \r\n\t\t\t\t\t\t(ISNULL(fNumber,0.000) - ISNULL(aNumber,0.000) - ISNULL(eNumber,0.000)) / NULLIF(fNumber,0)),\r\n\t\t\t\t\t\t0) AS Rate \t\t\t\t--差额比例\r\n\t\t\t\tFROM cte_z AS z\r\n\t\t\t\tINNER JOIN Res_Resource AS r ON r.ResourceCode = z.rCode\r\n\t\t\t\tLEFT JOIN cte_no AS no ON no.ResourceId = r.ResourceId\r\n\t\t\t\tORDER BY no.ParentId\r\n            ", str);
            }
            else
            {
                str4 = string.Format("\r\n \t\t\t\t--DECLARE @prjCode nvarchar(200);\r\n\t\t\t\t--DECLARE @planCode nvarchar(200);\r\n\t\t\t\t--SET @prjCode = '6172309c-7ef8-4fd3-895e-080e903f6da3';\r\n\t\t\t\t--SET @planCode = '20121219101520';\r\n\t\t\t\tWITH cte_e AS\t\t\t--本次计划量\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT s.scode AS rCode,s.number AS eNumber, r.ResourceId, r.ResourceName, \r\n\t\t\t\t\t\tm.ParentId, r2.ResourceCode AS ParentCode\r\n\t\t\t\t\tFROM Sm_Wantplan_Stock AS s\r\n\t\t\t\t\tJOIN Sm_Wantplan AS w ON w.swcode = s.wpcode\r\n\t\t\t\t\tINNER JOIN Res_Resource r ON r.ResourceCode = s.scode\r\n\t\t\t\t\tLEFT JOIN Res_Mapping m ON m.ResourceId = r.ResourceId\r\n\t\t\t\t\tLEFT JOIN Res_Resource r2 ON r2.ResourceId = m.ParentId\r\n\t\t\t\t\tWHERE procode = @prjCode AND wpcode = @planCode\r\n\t\t\t\t), cte_a AS\t\t\t\t--以消耗量\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT s.scode AS rCode,sum(s.number) AS aNumber FROM Sm_Wantplan_Stock AS s\r\n\t\t\t\t\tJOIN Sm_Wantplan AS w ON w.swcode = s.wpcode\r\n\t\t\t\t\tWHERE procode = @prjCode AND wpcode != @planCode AND  {0} \r\n\t\t\t\t\tGROUP by s.scode\r\n\t\t\t\t), cte_f AS\t\t\t\t--预算量\r\n\t\t\t\t(\r\n                   SELECT SUM(ResourceQuantity) AS fNumber ,ResourceCode AS rCode \r\n                   FROM Bud_TaskResource AS res\r\n                   INNER JOIN Res_Resource AS r ON r.ResourceId=res.ResourceId\r\n                   WHERE PrjGuid=@prjCode\r\n                   GROUP BY ResourceCode, ResourceName\r\n\t\t\t\t), cte_p AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT e.ParentCode AS rCode, \r\n\t\t\t\t\t\tfNumber,\r\n\t\t\t\t\t\tSUM(aNumber) AS aNumber,\r\n\t\t\t\t\t\tSUM(eNumber) AS eNumber\r\n\t\t\t\t\tFROM cte_e AS e\r\n\t\t\t\t\tLEFT JOIN cte_f AS f ON f.rCode = e.ParentCode\r\n\t\t\t\t\tLEFT JOIN cte_a AS a ON a.rCode = e.rCode\r\n\t\t\t\t\tGROUP BY e.ParentId, e.ParentCode, fNumber\r\n\t\t\t\t\tHAVING ParentId IS NOT NULL\r\n\t\t\t\t), cte_r AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT e.rCode, f.fNumber, a.aNumber, e.eNumber\r\n\t\t\t\t\tFROM cte_e AS e\r\n\t\t\t\t\tLEFT JOIN cte_f AS f ON f.rCode = e.rCode\r\n\t\t\t\t\tLEFT JOIN cte_a AS a ON a.rCode = e.rCode\r\n\t\t\t\t), cte_z AS \r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT * FROM cte_r\r\n\t\t\t\t\tUNION \r\n\t\t\t\t\tSELECT * FROM cte_p\r\n\t\t\t\t), cte_no AS \r\n\t\t\t\t(\r\n                    SELECT * FROM\r\n                    (\r\n\t                    SELECT m.ResourceId,'' ResourceCode,m.ParentId + m.ResourceId AS ParentId\r\n\t                    FROM Res_Mapping AS m\r\n\t                    UNION\r\n\t                    SELECT r.ResourceId,r.ResourceCode, r.ResourceId AS ParentId\r\n\t                    FROM Res_Resource r\r\n\t                    WHERE r.ResourceId IN (SELECT ParentId FROM Res_Mapping)\r\n                    ) TA WHERE ResourceCode IN (SELECT rCode FROM cte_z)\r\n\t\t\t\t)\r\n\t\t\t\tSELECT r.ResourceId, r.ResourceName, r.Specification, \r\n\t\t\t\t\tISNULL(z.fNumber, 0.000) AS fNumber,\t\t--预算量\t\t\t\r\n\t\t\t\t\tISNULL(z.aNumber, 0.000) AS aNumber,\t\t--已消耗量\r\n\t\t\t\t\tISNULL(z.eNumber, 0.000) AS eNumber,\t\t--本次计划量\r\n\t\t\t\t\tISNULL(CONVERT(DECIMAL(18,4), \r\n\t\t\t\t\t\t(ISNULL(fNumber,0.000) - ISNULL(aNumber,0.000) - ISNULL(eNumber,0.000)) / NULLIF(fNumber,0)),\r\n\t\t\t\t\t\t0) AS Rate\t\t\t\t\t--差额比例\r\n\t\t\t\tFROM cte_z AS z\r\n\t\t\t\tINNER JOIN Res_Resource AS r ON r.ResourceCode = z.rCode\r\n\t\t\t\tLEFT JOIN cte_no AS no ON no.ResourceId = r.ResourceId\r\n\t\t\t\tORDER BY no.ParentId\r\n                ", str);
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@planCode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = prjCode;
            commandParameters[1].Value = planCode;
            return SqlHelper.ExecuteQuery(CommandType.Text, str4, commandParameters);
        }

        public string GetGuidByCode(string code)
        {
            string cmdText = "select swid from Sm_Wantplan where swcode = @swcode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@swcode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = code;
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        public List<MaterialWantPlanModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select swid,swcode,flowstate,person,intime,acceptstate,annx,explain,procode,ContainPending,EquipmentId ");
            builder.Append(" FROM Sm_Wantplan ");
            builder.Append("ORDER BY procode ASC, intime desc ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<MaterialWantPlanModel> list = new List<MaterialWantPlanModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public DataTable GetMaterialPlanInfo(string planCode, string prjCode, string isWBSRelevance)
        {
            string str = string.Empty;
            string str2 = "0";
            string cmdText = "SELECT paravalue FROM Sm_Set WHERE paraname='IsContainPending'";
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
            }
            if (Convert.ToBoolean(Convert.ToInt32(str2.Trim())))
            {
                str = "FlowState IN('1','0')";
            }
            else
            {
                str = "FlowState = 1";
            }
            string str4 = string.Empty;
            if (isWBSRelevance == "1")
            {
                str4 = string.Format("\r\n\t\t\t\t--DECLARE @prjCode nvarchar(200);\r\n\t\t\t\t--DECLARE @planCode nvarchar(200);\r\n\t\t\t\t--SET @prjCode = '6172309c-7ef8-4fd3-895e-080e903f6da3';\r\n\t\t\t\t--SET @planCode = '20121219101520';\r\n\t\t\t\tWITH cte_a AS\t\t\t\t--已消耗量\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT s.scode AS rCode,sum(s.number) AS aNumber FROM Sm_Wantplan_Stock AS s\r\n\t\t\t\t\tJOIN Sm_Wantplan AS w ON w.swcode = s.wpcode\r\n\t\t\t\t\tWHERE procode = @prjCode AND wpcode != @planCode AND {0} \r\n\t\t\t\t\tGROUP by s.scode\r\n\t\t\t\t), cte_f AS\t\t\t\t--预算量\r\n\t\t\t\t(\r\n                    SELECT SUM(ResQuantity)  AS fNumber,ResourceCode AS rCode  FROM \r\n                    (\r\n                        SELECT (SUM(Bud_TaskResource.ResourceQuantity)*Bud_TaskResource.ResourcePrice) ResTotal,\r\n                        SUM(Bud_TaskResource.ResourceQuantity) ResQuantity,Bud_TaskResource.ResourceId\r\n                        FROM Bud_TaskResource JOIN Bud_Task ON Bud_TaskResource.TaskId=Bud_Task.TaskId\r\n                        JOIN  vGetCurBudVersion ON Bud_Task.PrjId=vGetCurBudVersion.PrjId \r\n\t\t\t\t\t\t\tAND Bud_Task.version=vGetCurBudVersion.curversion AND TaskType=''\r\n\t\t\t\t\t\t\tAND Bud_Task.PrjId=@prjCode\r\n                        GROUP BY Bud_TaskResource.ResourcePrice,Bud_TaskResource.ResourceId\r\n                        UNION ALL\r\n                        SELECT (SUM(ResourceQuantity)*ResourcePrice) ReseTotal,SUM(ResourceQuantity) ResQuantity,\r\n                        ResourceId FROM Bud_ModifyTaskRes modifyTaskRes\r\n                        JOIN Bud_ModifyTask modifyTask ON modifyTaskRes.ModifyTaskId=modifyTask.ModifyTaskId\r\n                        JOIN Bud_MOdify modify ON modifyTask.modifyId=modify.modifyId\r\n                        WHERE modify.FlowState='1' AND modify.PrjId=@prjCode\r\n                        GROUP BY ResourcePrice,ResourceId\r\n                    ) allBudResInfo \r\n                    INNER JOIN Res_Resource AS r ON allBudResInfo.ResourceId=r.ResourceId\r\n                    GROUP BY r.ResourceCode\r\n\t\t\t\t)\r\n                SELECT cte_a.rCode, cte_f.fNumber, cte_a.aNumber\r\n                FROM cte_a Left JOIN cte_f ON cte_a.rCode = cte_f.rCode", str);
            }
            else
            {
                str4 = string.Format("\r\n \t\t\t\t--DECLARE @prjCode nvarchar(200);\r\n\t\t\t\t--DECLARE @planCode nvarchar(200);\r\n\t\t\t\t--SET @prjCode = '6172309c-7ef8-4fd3-895e-080e903f6da3';\r\n\t\t\t\t--SET @planCode = '20121219101520';\r\n\t\t\t\tWITH cte_a AS\t\t\t\t--已消耗量\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT s.scode AS rCode,sum(s.number) AS aNumber FROM Sm_Wantplan_Stock AS s\r\n\t\t\t\t\tJOIN Sm_Wantplan AS w ON w.swcode = s.wpcode\r\n\t\t\t\t\tWHERE procode = @prjCode AND wpcode != @planCode AND  {0} \r\n\t\t\t\t\tGROUP by s.scode\r\n\t\t\t\t), cte_f AS\t\t\t\t--预算量\r\n\t\t\t\t(\r\n                   SELECT SUM(ResourceQuantity) AS fNumber ,ResourceCode AS rCode \r\n                   FROM Bud_TaskResource AS res\r\n                   INNER JOIN Res_Resource AS r ON r.ResourceId=res.ResourceId\r\n                   WHERE PrjGuid=@prjCode\r\n                   GROUP BY ResourceCode, ResourceName\r\n\t\t\t\t)\r\n                SELECT cte_a.rCode, cte_f.fNumber, cte_a.aNumber\r\n                FROM cte_a Left JOIN cte_f ON cte_a.rCode = cte_f.rCode", str);
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@planCode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = prjCode;
            commandParameters[1].Value = planCode;
            return SqlHelper.ExecuteQuery(CommandType.Text, str4, commandParameters);
        }

        public MaterialWantPlanModel GetModel(string swcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select swid,swcode,flowstate,person,intime,acceptstate,annx,explain,procode,ContainPending,EquipmentId from Sm_Wantplan ");
            builder.Append(" where swcode=@swcode ");
            MaterialWantPlanModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@swcode", swcode) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable getTableWantPlan(string procode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from Sm_Wantplan where procode=@procode ");
            builder.Append("order by intime desc");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@procode", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = procode;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable getTableWantPlanStock(string code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from Sm_Wantplan_Stock where wpcode=@wpcode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@wpcode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = code;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public MaterialPlanStockModel getWantPlanStockRow(string wpsid)
        {
            StringBuilder builder = new StringBuilder();
            MaterialPlanStockModel model = new MaterialPlanStockModel();
            builder.Append(" select * from Sm_Wantplan_Stock where wpsid=@wpsid");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@wpsid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = wpsid;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    model.wpsid = reader.GetString(0);
                    model.scode = reader.GetString(1);
                    model.wpcode = reader.GetString(2);
                    model.number = reader.GetInt32(3);
                }
            }
            return model;
        }

        private void PrepareAddCommand(MaterialWantPlanModel model, out string cmdText, out SqlParameter[] parameters)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" insert into Sm_Wantplan(swid,swcode,flowstate,person,intime,acceptstate,annx,explain,procode,ContainPending,EquipmentId) values(@swid,@swcode,@flowstate,@person,@intime,@acceptstate,@annx,@explain,@procode,@ContainPending,@EquipmentId)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@swid", SqlDbType.NVarChar, 50), new SqlParameter("@swcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@flowstate", SqlDbType.Int, 0x10), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime, 8), new SqlParameter("@acceptstate", SqlDbType.Int, 0x10), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@procode", SqlDbType.NVarChar, 100), new SqlParameter("@ContainPending", SqlDbType.Bit), new SqlParameter("@EquipmentId", SqlDbType.NVarChar, 500) };
            parameterArray[0].Value = model.swid;
            parameterArray[1].Value = model.swcode;
            parameterArray[2].Value = model.flowstate;
            parameterArray[3].Value = model.person;
            parameterArray[4].Value = model.intime;
            parameterArray[5].Value = model.acceptstate;
            parameterArray[6].Value = model.annx;
            parameterArray[7].Value = model.explain;
            parameterArray[8].Value = model.procode;
            parameterArray[9].Value = model.ContainPending;
            if (!string.IsNullOrEmpty(model.EquipmentId))
            {
                parameterArray[10].Value = model.EquipmentId;
            }
            else
            {
                parameterArray[10].Value = DBNull.Value;
            }
            cmdText = builder.ToString();
            parameters = parameterArray;
        }

        public void PrepareUpdateCommand(MaterialWantPlanModel model, out string cmdText, out SqlParameter[] parameters)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Wantplan set ");
            builder.Append("swid=@swid,");
            builder.Append("flowstate=@flowstate,");
            builder.Append("person=@person,");
            builder.Append("intime=@intime,");
            builder.Append("acceptstate=@acceptstate,");
            builder.Append("annx=@annx,");
            builder.Append("explain=@explain,");
            builder.Append("procode=@procode,");
            builder.Append("ContainPending=@ContainPending,");
            builder.Append("EquipmentId=@EquipmentId");
            builder.Append(" where swcode=@swcode ");
            cmdText = builder.ToString();
            parameters = new SqlParameter[] { new SqlParameter("@swid", SqlDbType.NVarChar, 50), new SqlParameter("@swcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@flowstate", SqlDbType.Int, 4), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.SmallDateTime), new SqlParameter("@acceptstate", SqlDbType.Int, 4), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@procode", SqlDbType.NVarChar, 100), new SqlParameter("@ContainPending", SqlDbType.Bit), new SqlParameter("@EquipmentId", SqlDbType.NVarChar, 500) };
            parameters[0].Value = model.swid;
            parameters[1].Value = model.swcode;
            parameters[2].Value = model.flowstate;
            parameters[3].Value = model.person;
            parameters[4].Value = model.intime;
            parameters[5].Value = model.acceptstate;
            parameters[6].Value = model.annx;
            parameters[7].Value = model.explain;
            parameters[8].Value = model.procode;
            parameters[9].Value = model.ContainPending;
            if (!string.IsNullOrEmpty(model.EquipmentId))
            {
                parameters[10].Value = model.EquipmentId;
            }
            else
            {
                parameters[10].Value = DBNull.Value;
            }
        }

        public MaterialWantPlanModel ReaderBind(IDataReader dataReader)
        {
            MaterialWantPlanModel model = new MaterialWantPlanModel {
                swid = dataReader["swid"].ToString(),
                swcode = dataReader["swcode"].ToString()
            };
            object obj2 = dataReader["flowstate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.flowstate = Convert.ToInt32(obj2);
            }
            model.person = dataReader["person"].ToString();
            obj2 = dataReader["intime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.intime = Convert.ToString(obj2);
            }
            obj2 = dataReader["acceptstate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.acceptstate = Convert.ToInt32(obj2);
            }
            model.annx = dataReader["annx"].ToString();
            model.explain = dataReader["explain"].ToString();
            model.procode = dataReader["procode"].ToString();
            model.ContainPending = DBHelper.GetBool(dataReader["ContainPending"]);
            model.EquipmentId = dataReader["EquipmentId"].ToString();
            return model;
        }

        public DataTable Search(string sqlWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Sm_wantPlan where 1=1 ");
            builder.Append(sqlWhere);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public int Update(MaterialWantPlanModel model)
        {
            SqlParameter[] parameterArray;
            string cmdText = string.Empty;
            this.PrepareUpdateCommand(model, out cmdText, out parameterArray);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, parameterArray);
        }

        public int Update(SqlTransaction trans, MaterialWantPlanModel model)
        {
            SqlParameter[] parameterArray;
            string cmdText = string.Empty;
            this.PrepareUpdateCommand(model, out cmdText, out parameterArray);
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, parameterArray);
        }

        public int UpdateAcceptstate(SqlTransaction trans, string swcode)
        {
            string cmdText = "update Sm_Wantplan set acceptstate=acceptstate+1 where swcode in(" + swcode + ")";
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, new SqlParameter[0]);
        }
    }
}

