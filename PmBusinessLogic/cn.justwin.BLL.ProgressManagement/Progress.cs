namespace cn.justwin.BLL.ProgressManagement
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Progress
    {
        private const string adminCode = "00000000";
        private const int initializeFlowState = -1;
        private const bool initializeLatest = false;

        private Progress()
        {
        }

        public void Add(Progress entity)
        {
            if (entity != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    PT_yhmc _yhmc = (from m in entities.PT_yhmc
                        where m.v_yhdm == entity.InputUser
                        select m).FirstOrDefault<PT_yhmc>();
                    plus_progress _progress = new plus_progress {
                        ProgressId = entity.ProgressId,
                        PrjId = new Guid?(entity.PrjId),
                        ProgressName = entity.ProgressName,
                        IsMain = entity.IsMain,
                        FlowState = new int?(entity.FlowState),
                        Note = entity.Note,
                        InputDate = new DateTime?(entity.InputDate),
                        PT_yhmc = _yhmc
                    };
                    entities.AddToplus_progress(_progress);
                    if (entity.IsMain)
                    {
                        List<plus_progress> list = (from m in entities.plus_progress
                            where (m.PrjId == entity.PrjId) && m.IsMain
                            select m).ToList<plus_progress>();
                        if (list.Count > 0)
                        {
                            foreach (plus_progress _progress2 in list)
                            {
                                _progress2.IsMain = false;
                            }
                        }
                    }
                    string str = Guid.NewGuid().ToString();
                    plus_progress_version _version = new plus_progress_version {
                        ProgressVersionId = str,
                        ParentVersionId = null,
                        VersionCode = entity.VersionCode,
                        VersionName = entity.ProgressName,
                        FlowState = null,
                        InputDate = new DateTime?(entity.InputDate),
                        IsLatest = false,
                        Note = entity.Note,
                        plus_progress = _progress
                    };
                    entities.AddToplus_progress_version(_version);
                    List<string> userCodes = new List<string> {
                        entity.InputUser
                    };
                    if (entity.InputUser != "00000000")
                    {
                        userCodes.Add("00000000");
                    }
                    Privilege.AddPrivilege(userCodes, _progress, entities);
                    entities.SaveChanges();
                }
            }
        }

        public static Progress Creat(string progressId, Guid prjId, string progressName, string versionCode, bool isMain, string inputUser, DateTime inputDate, string note)
        {
            if (string.IsNullOrEmpty(progressId))
            {
                throw new ArgumentNullException("进度计划标示 progressId", "不能为null或空字符串");
            }
            return new Progress { ProgressId = progressId, PrjId = prjId, FlowState = -1, ProgressName = progressName, VersionCode = versionCode, IsMain = isMain, InputDate = inputDate, InputUser = inputUser, Note = note };
        }

        public static void Del(string progressId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                plus_progress entity = (from m in entities.plus_progress
                    where m.ProgressId == progressId
                    select m).FirstOrDefault<plus_progress>();
                if (entity != null)
                {
                    entities.DeleteObject(entity);
                    entities.SaveChanges();
                }
            }
        }

        public static void Del(List<string> verIds)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (verIds != null)
                {
                    int num = 0;
                    using (List<string>.Enumerator enumerator = verIds.GetEnumerator())
                    {
                        string verId;
                        while (enumerator.MoveNext())
                        {
                            verId = enumerator.Current;
                            string progressId = (from m in entities.plus_progress_version
                                where m.ProgressVersionId == verId
                                select m.plus_progress.ProgressId).FirstOrDefault<string>();
                            plus_progress entity = (from m in entities.plus_progress
                                where m.ProgressId == progressId
                                select m).FirstOrDefault<plus_progress>();
                            if (entity != null)
                            {
                                entities.DeleteObject(entity);
                                num++;
                            }
                        }
                    }
                    if (num > 0)
                    {
                        entities.SaveChanges();
                    }
                }
            }
        }

        public static DataTable GetAnalysisDetailSource(string prjName, string level)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n            WITH  ctePlus  AS(\r\n                SELECT ID_, UID_,NAME_,PERCENTCOMPLETE_,PARENTTASKUID_,0 AS loclevel FROM plus_task\r\n                WHERE PARENTTASKUID_='-1' AND PROJECTUID_  IN (SELECT ProgressVersionId FROM plus_progress_version\r\n                    WHERE IsLatest=1 AND ProgressId IN (SELECT ProgressId FROM plus_progress WHERE IsMain=1 \r\n                        AND FlowState=1  AND PrjId IN (SELECT PrjGuid FROM PT_PrjInfo WHERE PrjName='{0}')\r\n                    )\r\n                )\r\n                UNION ALL\r\n                SELECT Task.ID_, Task.UID_,Task.NAME_,Task.PERCENTCOMPLETE_,Task.PARENTTASKUID_,loclevel+1 FROM plus_task Task \r\n                INNER JOIN ctePlus p ON Task.PARENTTASKUID_=p.UID_\r\n                    WHERE Task.PROJECTUID_ IN (SELECT ProgressVersionId FROM plus_progress_version\r\n                        WHERE IsLatest=1 AND ProgressId IN (SELECT ProgressId FROM plus_progress WHERE IsMain=1 AND FlowState=1  \r\n                            AND PrjId IN (SELECT PrjGuid FROM PT_PrjInfo WHERE PrjName='{1}')\r\n                        ) \r\n                )\r\n             )\r\n             SELECT * FROM ctePlus  ", prjName, prjName);
            if (!string.IsNullOrEmpty(level))
            {
                builder.AppendFormat(" WHERE loclevel <{0}", level);
            }
            builder.Append("  ORDER BY ID_ ASC");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static DataTable GetAnalysisSource()
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                WITH cteProgress AS(\r\n\t                SELECT ProgressVersionId ,PrjId FROM plus_progress_version V\r\n\t                RIGHT JOIN (\r\n\t                SELECT PrjId,ProgressId FROM plus_progress\r\n\t                WHERE IsMain=1 AND FlowState=1 ) AS Progress ON V.ProgressId=Progress.ProgressId\r\n\t                WHERE IsLatest=1\r\n\t                )\r\n                SELECT PrjId,P.PrjName,\r\n                CONVERT (DECIMAL(10,0),(SUM((PERCENTCOMPLETE_ /100.00 * DURATION_))/SUM(DURATION_) *100)) AS PERCENTCOMPLETE_ , --进度完成百分比\r\n                ISNULL(CONVERT(DECIMAL(18,3),P.PrjCost),0.000) AS PrjCost,P.Duration,Y.v_xm AS PrjManagerName FROM plus_task T\r\n                RIGHT JOIN cteProgress ON T.PROJECTUID_=cteProgress.ProgressVersionId\r\n                LEFT JOIN PT_PrjInfo P ON P.PrjGuid=cteProgress.PrjId\r\n                LEFT JOIN PT_yhmc Y ON Y.V_yhdm=P.PrjManager\r\n                WHERE PARENTTASKUID_='-1' AND  P.PrjState=7\r\n                GROUP BY PARENTTASKUID_,PrjId,P.PrjName,P.PrjCost,P.Duration,Y.v_xm");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static Progress GetById(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                plus_progress _progress = (from m in entities.plus_progress
                    where m.ProgressId == id
                    select m).FirstOrDefault<plus_progress>();
                return new Progress { ProgressId = _progress.ProgressId, ProgressName = _progress.ProgressName, PrjId = _progress.PrjId.Value, FlowState = _progress.FlowState.Value, InputDate = _progress.InputDate.Value, InputUser = _progress.PT_yhmc.v_yhdm, Note = _progress.Note };
            }
        }

        public static DataTable GetHistoryVersions(string progressId, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                WITH cteVersions AS(\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY V.InputDate DESC) AS No,\r\n                    V.ProgressId,V.ProgressVersionId,V.VersionName,V.VersionCOde,\r\n                    CASE V.IsLatest \r\n\t                    WHEN 0 THEN '否'\r\n\t                    ELSE '是'\r\n                    END AS Latest, --是否是可执行版本\r\n\t                    CASE P.IsMain \r\n                        WHEN 0 THEN '否'\r\n                        ELSE '是'\r\n                    END AS Main,    -- 是否是主计划\r\n                   V2.ProgressVersionId AS ParentVerId, V2.VersionName AS PVersionName,V2.VersionCOde AS PVersionCode,\r\n\t                v_xm AS UserName,V.InputDate FROM plus_progress_version  AS V\r\n                    LEFT JOIN plus_progress_version AS V2 ON V.ParentVersionId=V2.ProgressVersionId\r\n\t                LEFT JOIN pt_yhmc AS yh ON V.InputUser =yh.v_yhdm \r\n                    INNER JOIN plus_progress AS P ON V.ProgressId=P.ProgressId\r\n\t                WHERE V.ProgressId=@progressId\r\n\t                AND (V.FlowState IS NULL OR V.FlowState =1)\r\n                )\r\n                SELECT * FROM cteVersions WHERE No BETWEEN @start AND @end ");
            int num = (pageSize * (pageNo - 1)) + 1;
            int num2 = pageSize * pageNo;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@progressId", progressId), new SqlParameter("@start", num), new SqlParameter("@end", num2) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int GetHistoryVersionsCount(string progressId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                \r\n\t                SELECT COUNT(*) FROM plus_progress_version \r\n\t                WHERE ProgressId=@progressId\r\n\t                AND (FlowState IS NULL OR FlowState = 1)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@progressId", progressId) };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters).ToString());
        }

        public static DataTable GetInitalizePlans(string prjId, string userCode, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                WITH Plans AS(\r\n                    SELECT ROW_NUMBER() OVER( ORDER BY V.VersionCode ASC) AS No,\r\n\t                V.ProgressId,V.ProgressVersionId,V.VersionName,V.VersionCode,\r\n\t                P.FlowState,v_xm AS UserName,P.InputDate,P.Note,V.IsLatest,\r\n\t                CASE V.IsLatest \r\n\t\t                WHEN 0 THEN '否'\r\n\t\t                ELSE '是'\r\n\t                END AS Latest, --是否是可执行版本\r\n                    CASE P.IsMain \r\n                        WHEN 0 THEN '否'\r\n                        ELSE '是'\r\n                    END AS Main    -- 是否是主计划\r\n                    FROM plus_progress AS P\r\n                    LEFT JOIN plus_progress_version AS V ON P.ProgressId=V.ProgressId\r\n                    LEFT JOIN PT_yhmc AS yhmc  ON P.InputUser=yhmc.v_yhdm\r\n\t                INNER    JOIN plus_progress_privilege AS PR ON  P.ProgressId = PR.ProgressId\r\n                    WHERE  V.FlowState IS NULL  --初始化的进度计划\r\n\t                 AND PrjId=@prjId AND PR.UserCode =@userCode\r\n                )\r\n                SELECT * FROM Plans WHERE No BETWEEN @start AND @end Order By Plans.InputDate Desc");
            int num = ((pageNo - 1) * pageSize) + 1;
            int num2 = pageNo * pageSize;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@userCode", userCode), new SqlParameter("@start", num), new SqlParameter("@end", num2) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int GetInitalizePlansCount(string prjId, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n               SELECT COUNT(*) FROM plus_progress AS P\r\n                    LEFT JOIN plus_progress_version AS V ON P.ProgressId=V.ProgressId\r\n                    LEFT JOIN PT_yhmc AS yhmc  ON P.InputUser=yhmc.v_yhdm\r\n\t                INNER JOIN plus_progress_privilege AS PR ON  P.ProgressId = PR.ProgressId\r\n                    WHERE  V.FlowState IS NULL  --初始化的进度计划\r\n\t                 AND PrjId=@prjId AND PR.UserCode =@userCode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@userCode", userCode) };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters).ToString());
        }

        public static DataTable GetLagAnalysis(string prjName, string userCode, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("uspLagAnalysis");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode), new SqlParameter("@prjName", prjName), new SqlParameter("@pageIndex", pageNo), new SqlParameter("@pageSize", pageSize) };
            return SqlHelper.ExecuteQuery(CommandType.StoredProcedure, builder.ToString(), commandParameters);
        }

        public static int GetLagAnalysisCount(string prjName, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("uspLagAnalysisCount");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode), new SqlParameter("@prjName", prjName) };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, builder.ToString(), commandParameters).ToString());
        }

        public static DataTable GetLatestPlans(string prjId, string userCode, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                WITH Plans AS(\r\n                    SELECT ROW_NUMBER() OVER( ORDER BY P.InputDate DESC) AS No,V.ProgressId,V.ProgressVersionId,V.VersionName,P.FlowState,\r\n                    v_xm AS UserName,P.InputDate,P.Note,V.VersionCode,\r\n                    CASE P.IsMain \r\n                        WHEN 0 THEN '否'\r\n                        ELSE '是'\r\n                    END AS Main    -- 是否是主计划\r\n                    FROM plus_progress AS P\r\n                    LEFT JOIN plus_progress_version AS V ON P.ProgressId=V.ProgressId\r\n                    LEFT JOIN PT_yhmc AS yhmc  ON P.InputUser=yhmc.v_yhdm\r\n                    INNER JOIN plus_progress_privilege AS PR ON  P.ProgressId = PR.ProgressId\r\n                    WHERE  V.IsLatest='1' AND PrjId=@prjId AND PR.UserCode=@userCode\r\n                )\r\n                SELECT * FROM Plans WHERE No BETWEEN @start AND @end");
            int num = ((pageNo - 1) * pageSize) + 1;
            int num2 = pageNo * pageSize;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@userCode", userCode), new SqlParameter("@start", num), new SqlParameter("@end", num2) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int GetLatestPlansCount(string prjId, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                SELECT COUNT(*)  FROM plus_progress AS P\r\n                LEFT JOIN plus_progress_version AS V ON P.ProgressId=V.ProgressId\r\n                INNER JOIN plus_progress_privilege AS PR ON  P.ProgressId = PR.ProgressId\r\n                WHERE  V.IsLatest='1' AND PrjId=@prjId AND PR.UserCode =@userCode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@userCode", userCode) };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters).ToString());
        }

        public static string GetMainPlanName(string prjId)
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                SELECT V.VersionName FROM plus_progress AS P\r\n                INNER JOIN plus_progress_version AS V ON P.ProgressId=V.ProgressId \r\n                WHERE P.PrjId=@prjId \r\n                AND P.IsMain=1");
            SqlParameter parameter = new SqlParameter("@prjId", prjId);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
            if (obj2 != null)
            {
                str = obj2.ToString();
            }
            return str;
        }

        public static int GetMaxLevel(string prjName)
        {
            int num = 0;
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                WITH cteChildren AS(\r\n\t                SELECT UID_,PARENTTASKUID_,1 AS Level FROM plus_task \r\n\t                WHERE PROJECTUID_=(SELECT ProgressVersionId FROM plus_progress_version\r\n                        WHERE IsLatest=1 AND ProgressId IN (SELECT ProgressId FROM plus_progress WHERE IsMain=1 \r\n                            AND FlowState=1  AND PrjId IN (SELECT PrjGuid FROM PT_PrjInfo WHERE PrjName=@prjName)\r\n                        )\r\n                    )\r\n\t                AND PARENTTASKUID_ ='-1'\r\n\t                UNION ALL\r\n\t                SELECT \tT.UID_,T.PARENTTASKUID_,cteChildren.Level+1 FROM plus_task AS T\r\n\t                INNER JOIN cteChildren ON cteChildren.UID_=T.PARENTTASKUID_\r\n                )\r\n                SELECT MAX(Level) FROM cteChildren ");
            SqlParameter parameter = new SqlParameter("@prjName", prjName);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
            if (obj2 != null)
            {
                num = Convert.ToInt32(obj2.ToString());
            }
            return num;
        }

        public static DataTable GetModifyPlans(string prjId, string userCode, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                WITH Plans AS(\r\n\t                SELECT ROW_NUMBER() OVER( ORDER BY V.VersionCode ASC) AS No,V.ProgressId, ISNULL( V.FlowState, 1) AS MFlowState,\r\n\t\t                V.InputDate,P.FlowState,P.Note,v_xm AS UserName,V.VersionName,V.VersionCode,V.ProgressVersionId,\r\n                        CASE V.IsLatest \r\n\t\t                    WHEN 0 THEN '否'\r\n\t\t                    ELSE '是'\r\n\t                    END AS Latest, --是否是可执行版本\r\n                        CASE P.IsMain \r\n                            WHEN 0 THEN '否'\r\n                            ELSE '是'\r\n                        END AS Main    -- 是否是主计划\r\n\t                FROM plus_progress AS P\r\n\t                LEFT JOIN plus_progress_version AS V ON P.ProgressId=V.ProgressId\r\n\t                LEFT JOIN PT_yhmc AS yhmc ON V.InputUser=yhmc.v_yhdm\r\n                    INNER JOIN plus_progress_privilege AS PR ON  P.ProgressId = PR.ProgressId\r\n\t                WHERE P.FlowState=1 AND V.ProgressVersionId IN(SELECT TOP(1) V.ProgressVersionId \r\n\t\t                FROM plus_progress_version AS V WHERE V.ProgressId = P.ProgressId ORDER BY V.InputDate DESC)\r\n\t\t                AND PrjId=@prjId AND PR.UserCode=@userCode\r\n                )\r\n                SELECT * FROM Plans WHERE No BETWEEN @start AND @end");
            int num = ((pageNo - 1) * pageSize) + 1;
            int num2 = pageNo * pageSize;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@userCode", userCode), new SqlParameter("@start", num), new SqlParameter("@end", num2) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int GetModifyPlansCount(string prjId, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                 SELECT COUNT(*) FROM plus_progress AS P\r\n\t                LEFT JOIN plus_progress_version AS V ON P.ProgressId=V.ProgressId\r\n\t               -- LEFT JOIN PT_yhmc AS yhmc ON V.InputUser=yhmc.v_yhdm\r\n                    INNER JOIN plus_progress_privilege AS PR ON  P.ProgressId = PR.ProgressId\r\n\t                WHERE P.FlowState=1 AND V.ProgressVersionId IN(SELECT TOP(1) V.ProgressVersionId \r\n\t\t                FROM plus_progress_version AS V WHERE V.ProgressId = P.ProgressId ORDER BY V.InputDate DESC)\r\n\t\t                AND PrjId=@prjId AND PR.UserCode=@userCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@userCode", userCode) };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters).ToString());
        }

        public static DataTable GetModifyProgress(string progressId, string versionCode)
        {
            return new DataTable();
        }

        public static DataTable GetModifyWF(string userCode, string prjId, string name, string version, bool? sing, string organigerName, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("\r\n                WITH WF AS (\r\n                     SELECT ROW_NUMBER() OVER(ORDER BY P.InputDate DESC) AS No,  \r\n\t                    a.BusinessCode, DATEDIFF(hh, b.OutOfTime, GETDATE()) AS cs,\r\n\t                    a.BusinessClass,CONVERT (varchar(10),a.StartTime, 20) as rq, \r\n\t                    (SELECT BusinessClassName FROM WF_Business_Class AS d WHERE (BusinessCode = a.BusinessCode) AND (BusinessClass = a.BusinessClass)) AS BusinessClassName,\r\n\t                    b.NoteID, b.IsAllPass, a.TemplateID,b.NodeID, b.NodeName,\r\n\t                     a.StartTime,--流程发起时间\r\n\t                     a.InstanceCode, \r\n\t                    (SELECT TemplateName FROM WF_Template AS c WHERE (TemplateID = a.TemplateID)) AS TemplateName,\r\n\t                     v_xm AS OrganigerName,--发起人姓名\r\n\t                    dbo.GetBusinessName(a.BusinessCode) AS BusinessName ,b.ArriveTime ,b.During,b.Sing,\r\n                        V.VersionName,V.VersionCode, --计划版本\r\n                        V.FlowState,  --流程状态,  --流程状态\r\n\t\t\t\t\t     CASE P.IsMain \r\n                            WHEN 0 THEN '否'\r\n                            ELSE '是'\r\n                        END AS Main    -- 是否是主计划\r\n                    FROM (SELECT * FROM (\r\n\t\t\t\t\t\t\tSELECT ROW_NUMBER() OVER(PARTITION BY InstanceCode ORDER BY StartTime DESC) AS [No]\r\n\t\t\t\t\t\t\t , * FROM WF_Instance_Main)AS T WHERE [No] <2\r\n\t\t\t\t\t\t) AS a  \r\n                    INNER JOIN WF_Instance AS b ON a.ID = b.ID \r\n                    INNER JOIN plus_progress_version AS V ON V.ProgressVersionId=a.InstanceCode\r\n                    INNER JOIN plus_progress AS P ON P.ProgressId=V.ProgressId --计划审核\r\n                    LEFT JOIN pt_yhmc AS YH ON  a.Organiger=YH.v_yhdm\r\n                    WHERE  b.Operator = @userCode AND BusinessCode='108' AND P.PrjId=@prjId ");
            if (sing.HasValue)
            {
                builder.Append(" AND b.Sing=@sing");
                list.Add(new SqlParameter("@sing", sing));
            }
            else
            {
                builder.Append(" AND b.Sing IN('0','1')");
            }
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND VersionName LIKE '%'+@name+'%'");
                list.Add(new SqlParameter("@name", name));
            }
            if (!string.IsNullOrEmpty(version))
            {
                builder.Append(" AND VersionCode LIKE '%'+@version+'%'");
                list.Add(new SqlParameter("@version", version));
            }
            if (!string.IsNullOrEmpty(organigerName))
            {
                builder.Append(" AND YH.v_xm LIKE '%'+@orgName+'%' ");
                list.Add(new SqlParameter("@orgName", organigerName));
            }
            builder.Append("\r\n                ) \r\n                SELECT  * FROM WF WHERE No BETWEEN @start AND @end");
            int num = (pageSize * (pageNo - 1)) + 1;
            int num2 = pageSize * pageNo;
            list.Add(new SqlParameter("@userCode", userCode));
            list.Add(new SqlParameter("@prjId", prjId));
            list.Add(new SqlParameter("@start", num));
            list.Add(new SqlParameter("@end", num2));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public static int GetModifyWFCount(string userCode, string prjId, string name, string version, bool? sing, string organigerName)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("\r\n                SELECT COUNT(*)  FROM WF_Instance_Main AS a \r\n                INNER JOIN WF_Instance AS b ON a.ID = b.ID \r\n                INNER JOIN plus_progress_version AS V ON V.ProgressVersionId=a.InstanceCode\r\n                INNER JOIN plus_progress AS P ON P.ProgressId=V.ProgressId --计划审核\r\n                LEFT JOIN pt_yhmc AS YH ON  a.Organiger=YH.v_yhdm\r\n                WHERE  b.Operator = @userCode AND BusinessCode='108' AND P.PrjId=@prjId ");
            if (sing.HasValue)
            {
                builder.Append(" AND b.Sing=@sing");
                list.Add(new SqlParameter("@sing", sing));
            }
            else
            {
                builder.Append(" AND b.Sing IN('0','1')");
            }
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND VersionName LIKE '%'+@name+'%'");
                list.Add(new SqlParameter("@name", name));
            }
            if (!string.IsNullOrEmpty(version))
            {
                builder.Append(" AND VersionCode LIKE '%'+@version+'%'");
                list.Add(new SqlParameter("@version", version));
            }
            if (!string.IsNullOrEmpty(organigerName))
            {
                builder.Append(" AND YH.v_xm LIKE '%'+@orgName+'%' ");
                list.Add(new SqlParameter("@orgName", organigerName));
            }
            list.Add(new SqlParameter("@userCode", userCode));
            list.Add(new SqlParameter("@prjId", prjId));
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()).ToString());
        }

        public static DataTable GetPartById(string verId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                SELECT V.VersionName,V.VersionCode,V.Note,P.IsMain FROM plus_progress_version AS V\r\n                INNER JOIN plus_progress AS P ON V.ProgressId=P.ProgressId\r\n                 WHERE ProgressVersionId=@progressVerId");
            SqlParameter parameter = new SqlParameter("@progressVerId", verId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static DataTable GetPlan(string progressId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Progress.ProgressId,ProgressName,Progress.InputDate,Progress.Note,Version.VersionCode,\r\n                yhmc.v_xm AS UserName,PrjInfo.PrjName,\r\n                 CASE Progress.IsMain \r\n                        WHEN 0 THEN '否'\r\n                        ELSE '是'\r\n                    END AS Main    -- 是否是主计划\r\n                FROM plus_progress  AS Progress\r\n                LEFT JOIN plus_progress_version AS Version ON Progress.ProgressId = Version.ProgressId\r\n                LEFT JOIN pt_yhmc AS yhmc ON Progress.InputUser=yhmc.v_yhdm\r\n                LEFT JOIN PT_PrjInfo AS PrjInfo ON PrjId=PrjGuid\r\n                WHERE Version.FlowState IS NULL AND Progress.ProgressId=@progressId");
            SqlParameter parameter = new SqlParameter("@progressId", progressId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static DataTable GetPrivilegePlans(string prjId, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                WITH Plans AS(\r\n                    SELECT ROW_NUMBER() OVER( ORDER BY P.InputDate DESC) AS No,V.ProgressId,V.ProgressVersionId,V.VersionName,P.FlowState,\r\n                    v_xm AS UserName,P.InputDate,P.Note,V.VersionCode,\r\n                    STUFF((SELECT '、'+[v_xm] FROM plus_progress_privilege AS P1\r\n                        LEFT JOIN PT_yhmc ON v_yhdm=P1.UserCode WHERE P1.ProgressId=P.ProgressId\r\n                        FOR XML PATH('')),1,1,'') AS Limits,\r\n                     CASE V.IsLatest \r\n\t\t                WHEN 0 THEN '否'\r\n\t\t                ELSE '是'\r\n\t                END AS Latest, --是否是可执行版本\r\n                    CASE P.IsMain \r\n                        WHEN 0 THEN '否'\r\n                        ELSE '是'\r\n                    END AS Main    -- 是否是主计划\r\n                    FROM plus_progress AS P\r\n                    LEFT JOIN plus_progress_version AS V ON P.ProgressId=V.ProgressId\r\n                    LEFT JOIN PT_yhmc AS yhmc  ON P.InputUser=yhmc.v_yhdm\r\n                    WHERE  V.IsLatest='1' AND PrjId=@prjId \r\n                )\r\n                SELECT * FROM Plans WHERE No BETWEEN @start AND @end");
            int num = ((pageNo - 1) * pageSize) + 1;
            int num2 = pageNo * pageSize;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@start", num), new SqlParameter("@end", num2) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int GetPrivilegePlansCount(string prjId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                SELECT COUNT(*)  FROM plus_progress AS P\r\n                LEFT JOIN plus_progress_version AS V ON P.ProgressId=V.ProgressId\r\n                WHERE  V.IsLatest='1' AND PrjId=@prjId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId) };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters).ToString());
        }

        public static string GetProgressName(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.plus_progress_version
                    where m.ProgressVersionId == id
                    select m.VersionName).FirstOrDefault<string>();
            }
        }

        public static DataTable GetVersionPlans(string prjId, string userCode, string name, string version, string modifyUserName, bool isLatest, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("\r\n                WITH VersionInfo AS (\r\n\t                SELECT ROW_NUMBER() OVER (ORDER BY V.VersionCode ASC) AS No, V.ProgressVersionId,V.VersionName,V.VersionCode,V.Note,\r\n\t\t                V.InputDate,v_xm AS UserName,v_yhdm AS UserCode,V.FlowState AS MFlowState,P.FlowState,\r\n\t\t                V2.ProgressVersionId AS ParentVerId,\r\n\t\t                V2.VersionName AS PVersionName,\r\n\t\t                V2.VersionCode AS PVersionCode,\r\n                        CASE V.IsLatest \r\n\t\t                    WHEN 0 THEN '否'\r\n\t\t                    ELSE '是'\r\n\t                    END AS Latest, --是否是可执行版本\r\n                        CASE P.IsMain \r\n                            WHEN 0 THEN '否'\r\n                            ELSE '是'\r\n                        END AS Main    -- 是否是主计划\r\n\t                FROM plus_progress_version AS V\r\n\t                LEFT JOIN plus_progress AS P ON V.ProgressId=P.ProgressId\r\n\t                LEFT JOIN plus_progress_version AS V2 ON V.ParentVersionId=V2.ProgressVersionId\r\n\t                LEFT JOIN pt_yhmc ON V.InputUser =pt_yhmc.v_yhdm\r\n                    INNER JOIN plus_progress_privilege AS PR ON  P.ProgressId = PR.ProgressId\r\n\t                WHERE P.PrjId=@prjId AND PR.UserCode=@userCode AND (P.FlowState = 1 AND V.FlowState IS NULL OR V.FlowState = 1)");
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND V.VersionName LIKE '%'+@name+'%'");
                list.Add(new SqlParameter("@name", name));
            }
            if (!string.IsNullOrEmpty(version))
            {
                builder.Append(" AND V.VersionCode LIKE '%'+@version+'%'");
                list.Add(new SqlParameter("@version", version));
            }
            if (!string.IsNullOrEmpty(modifyUserName))
            {
                builder.Append(" AND v_xm LIKE '%'+@modifyUserName+'%'");
                list.Add(new SqlParameter("@modifyUserName", modifyUserName));
            }
            if (isLatest)
            {
                builder.Append(" AND V.IsLatest ='1'");
            }
            builder.Append("\r\n                )\r\n                SELECT * FROM VersionInfo WHERE No BETWEEN @start AND @end ORDER BY VersionCode ASC");
            int num = ((pageNo - 1) * pageSize) + 1;
            int num2 = pageNo * pageSize;
            list.Add(new SqlParameter("@prjId", prjId));
            list.Add(new SqlParameter("@userCode", userCode));
            list.Add(new SqlParameter("@start", num));
            list.Add(new SqlParameter("@end", num2));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public static int GetVersionPlansCount(string prjId, string userCode, string name, string version, string modifyUserName, bool isLatest)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("\r\n                    SELECT COUNT(*) FROM plus_progress_version AS V\r\n\t                LEFT JOIN plus_progress AS P ON V.ProgressId=P.ProgressId\r\n                    LEFT JOIN pt_yhmc AS yhmc ON V.InputUser=yhmc.v_yhdm\r\n                    INNER JOIN plus_progress_privilege AS PR ON  P.ProgressId = PR.ProgressId\r\n\t                WHERE P.PrjId=@prjId AND PR.UserCode=@userCode AND (P.FlowState = 1 AND V.FlowState IS NULL OR V.FlowState =1)");
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND V.VersionName LIKE '%'+@name+'%'");
                list.Add(new SqlParameter("@name", name));
            }
            if (!string.IsNullOrEmpty(version))
            {
                builder.Append(" AND V.VersionCode LIKE '%'+@version+'%'");
                list.Add(new SqlParameter("@version", version));
            }
            if (!string.IsNullOrEmpty(modifyUserName))
            {
                builder.Append(" AND v_xm LIKE '%'+@modifyUserName+'%'");
                list.Add(new SqlParameter("@modifyUserName", modifyUserName));
            }
            if (isLatest)
            {
                builder.Append(" AND V.IsLatest ='1'");
            }
            list.Add(new SqlParameter("@prjId", prjId));
            list.Add(new SqlParameter("@userCode", userCode));
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()).ToString());
        }

        public static DataTable GetWF(string userCode, string prjId, string name, string version, bool? sing, string organigerName, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("\r\n                WITH WF AS (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY P.InputDate DESC) AS No,  \r\n\t                    a.BusinessCode, DATEDIFF(hh, b.OutOfTime, GETDATE()) AS cs,\r\n\t                    a.BusinessClass,CONVERT (varchar(10),a.StartTime, 20) as rq, \r\n\t                    (SELECT BusinessClassName FROM WF_Business_Class AS d WHERE (BusinessCode = a.BusinessCode) AND (BusinessClass = a.BusinessClass)) AS BusinessClassName,\r\n\t                    b.NoteID, b.IsAllPass, a.TemplateID,b.NodeID, b.NodeName,\r\n\t                     a.StartTime,--流程发起时间\r\n\t                     a.InstanceCode, \r\n\t                    (SELECT TemplateName FROM WF_Template AS c WHERE (TemplateID = a.TemplateID)) AS TemplateName,\r\n\t                     v_xm AS OrganigerName,--发起人姓名\r\n\t                    dbo.GetBusinessName(a.BusinessCode) AS BusinessName ,b.ArriveTime ,b.During,b.Sing,\r\n                        V.VersionName,V.VersionCode, --计划版本\r\n                        P.FlowState,--流程状态\r\n\t\t\t\t\t     CASE P.IsMain \r\n                            WHEN 0 THEN '否'\r\n                            ELSE '是'\r\n                        END AS Main    -- 是否是主计划\r\n                    FROM (SELECT * FROM (\r\n\t\t\t\t\t\t\tSELECT ROW_NUMBER() OVER(PARTITION BY InstanceCode ORDER BY StartTime DESC) AS [No]\r\n\t\t\t\t\t\t\t , * FROM WF_Instance_Main)AS T WHERE [No] <2\r\n\t\t\t\t\t\t) AS a \r\n                    INNER JOIN WF_Instance AS b ON a.ID = b.ID \r\n                    INNER JOIN plus_progress AS P ON P.ProgressId=a.InstanceCode\r\n                    INNER JOIN plus_progress_version AS V ON P.InputDate=V.InputDate --计划审核\r\n                    LEFT JOIN pt_yhmc AS YH ON  a.Organiger=YH.v_yhdm\r\n                    WHERE  b.Operator = @userCode AND BusinessCode='107' AND P.PrjId=@prjId ");
            if (sing.HasValue)
            {
                builder.Append(" AND b.Sing=@sing");
                list.Add(new SqlParameter("@sing", sing));
            }
            else
            {
                builder.Append(" AND b.Sing IN ('0','1')");
            }
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND VersionName LIKE '%'+@name+'%'");
                list.Add(new SqlParameter("@name", name));
            }
            if (!string.IsNullOrEmpty(version))
            {
                builder.Append(" AND VersionCode LIKE '%'+@version+'%'");
                list.Add(new SqlParameter("@version", version));
            }
            if (!string.IsNullOrEmpty(organigerName))
            {
                builder.Append(" AND YH.v_xm LIKE '%'+@orgName+'%' ");
                list.Add(new SqlParameter("@orgName", organigerName));
            }
            builder.Append("\r\n                ) \r\n                SELECT  * FROM WF WHERE No BETWEEN @start AND @end");
            int num = (pageSize * (pageNo - 1)) + 1;
            int num2 = pageSize * pageNo;
            list.Add(new SqlParameter("@userCode", userCode));
            list.Add(new SqlParameter("@prjId", prjId));
            list.Add(new SqlParameter("@start", num));
            list.Add(new SqlParameter("@end", num2));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public static int GetWFCount(string userCode, string prjId, string name, string version, bool? sing, string organigerName)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("\r\n                SELECT COUNT(*) FROM WF_Instance_Main AS a \r\n                INNER JOIN WF_Instance AS b ON a.ID = b.ID \r\n                INNER JOIN plus_progress AS P ON P.ProgressId=a.InstanceCode\r\n                INNER JOIN plus_progress_version AS V ON P.InputDate=V.InputDate --计划审核\r\n                LEFT JOIN pt_yhmc AS YH ON  a.Organiger=YH.v_yhdm\r\n                WHERE  b.Operator = @userCode AND BusinessCode='107' AND P.PrjId=@prjId ");
            if (sing.HasValue)
            {
                builder.Append(" AND b.Sing=@sing");
                list.Add(new SqlParameter("@sing", sing));
            }
            else
            {
                builder.Append(" AND b.Sing IN ('0','1')");
            }
            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(" AND VersionName LIKE '%'+@name+'%'");
                list.Add(new SqlParameter("@name", name));
            }
            if (!string.IsNullOrEmpty(version))
            {
                builder.Append(" AND VersionCode LIKE '%'+@version+'%'");
                list.Add(new SqlParameter("@version", version));
            }
            if (!string.IsNullOrEmpty(organigerName))
            {
                builder.Append(" AND YH.v_xm LIKE '%'+@orgName+'%' ");
                list.Add(new SqlParameter("@orgName", organigerName));
            }
            list.Add(new SqlParameter("@userCode", userCode));
            list.Add(new SqlParameter("@prjId", prjId));
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()).ToString());
        }

        public void Update(Progress entity)
        {
            if (entity != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    plus_progress _progress = (from m in entities.plus_progress
                        where m.ProgressId == entity.ProgressId
                        select m).FirstOrDefault<plus_progress>();
                    if (_progress == null)
                    {
                        _progress.ProgressName = entity.ProgressName;
                        _progress.PT_yhmc = (from m in entities.PT_yhmc
                            where m.v_yhdm == entity.InputUser
                            select m).FirstOrDefault<PT_yhmc>();
                        _progress.Note = entity.Note;
                        entities.SaveChanges();
                    }
                }
            }
        }

        public static void Update(string verId, string name, string versionCode, bool isMain, string note)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                UPDATE plus_progress_Version SET VersionName=@name,Note=@note,VersionCode=@version WHERE ProgressVersionId=@id\r\n                --将之前的主计划覆盖\r\n                IF(@isMain='1')\r\n                BEGIN \r\n                    UPDATE plus_progress SET IsMain=0\r\n                    WHERE IsMain='1' AND PrjId=(SELECT TOP(1) PrjId FROM plus_progress  \r\n\t                    WHERE ProgressId =(SELECT TOP(1) ProgressId FROM plus_progress_version \r\n\t\t                    WHERE ProgressVersionId=@id))\r\n                END\r\n                --修改当前计划为主计划\r\n                UPDATE plus_progress SET IsMain=@isMain WHERE ProgressId=\r\n                    (SELECT ProgressId FROM plus_progress_version WHERE ProgressVersionId=@id)\r\n                ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@name", name), new SqlParameter("@version", versionCode), new SqlParameter("@note", note), new SqlParameter("@id", verId), new SqlParameter("@isMain", isMain) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static void UpdateLatest(string progressId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                plus_progress_version _version = (from m in entities.plus_progress
                    join n in entities.plus_progress_version on m.ProgressId equals n.plus_progress.ProgressId
                    where (m.ProgressId == progressId) && (n.FlowState == null)
                    select n).FirstOrDefault<plus_progress_version>();
                plus_progress progress = (from m in entities.plus_progress
                    where m.ProgressId == progressId
                    select m).FirstOrDefault<plus_progress>();
                if ((progress != null) && progress.IsMain)
                {
                    plus_progress _progress = (from m in entities.plus_progress
                        where ((m.IsMain && (m.PrjId == progress.PrjId)) && (m.FlowState == 1)) && (m.ProgressId != progressId)
                        select m).FirstOrDefault<plus_progress>();
                    if (_progress != null)
                    {
                        _progress.IsMain = false;
                    }
                }
                if (_version != null)
                {
                    _version.IsLatest = true;
                    cn.justwin.BLL.ProgressManagement.Version.AddWarn(_version.ProgressVersionId, entities);
                    entities.SaveChanges();
                }
            }
        }

        private int FlowState { get; set; }

        private DateTime InputDate { get; set; }

        private string InputUser { get; set; }

        public bool IsMain { get; set; }

        private string Note { get; set; }

        private Guid PrjId { get; set; }

        private string ProgressId { get; set; }

        private string ProgressName { get; set; }

        public string VersionCode { get; set; }
    }
}

