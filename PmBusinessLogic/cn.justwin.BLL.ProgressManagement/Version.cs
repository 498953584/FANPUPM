namespace cn.justwin.BLL.ProgressManagement
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Version
    {
        private const int initializeFlowState = -1;

        private Version()
        {
        }

        public void Add(cn.justwin.BLL.ProgressManagement.Version entity)
        {
            if (entity != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    plus_progress_version _version = new plus_progress_version {
                        ProgressVersionId = entity.ProgressVersionId,
                        ParentVersionId = entity.ParentVersionId,
                        VersionCode = entity.VersionCode,
                        VersionName = entity.VersionName,
                        FlowState = new int?(entity.FlowState),
                        InputDate = new DateTime?(entity.InputDate),
                        IsLatest = new bool?(entity.IsLatest),
                        Note = entity.Note,
                        PT_yhmc = (from m in entities.PT_yhmc
                            where m.v_yhdm == entity.InputUser
                            select m).FirstOrDefault<PT_yhmc>(),
                        plus_progress = (from m in entities.plus_progress
                            where m.ProgressId == entity.ProgressId
                            select m).FirstOrDefault<plus_progress>()
                    };
                    entities.AddToplus_progress_version(_version);
                    entities.SaveChanges();
                }
            }
        }

        public string AddModifyApply(cn.justwin.BLL.ProgressManagement.Version entity)
        {
            string str = Guid.NewGuid().ToString();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                INSERT INTO plus_progress_version(ProgressVersionId,ProgressId,ParentVersionId,VersionName,\r\n\t                VersionCode,FlowState,IsLatest,InputUser,InputDate,Note)\r\n                VALUES (@newVerId,@progressId,@parVerId,@name,@version,'-1','0',@user,getdate(),@note)");
            builder.AppendLine();
            builder.Append("\r\n                INSERT INTO Plus_BackProject(ProjectGuid,Start,Finish,Calendars)\r\n                SELECT @newVerId,Start,Finish,Calendars FROM Plus_BackProject WHERE ProjectGuid=@parVerId");
            builder.AppendLine();
            builder.Append("\r\n                INSERT INTO plus_task (UID_,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ ,WEIGHT_ ,CONSTRAINTTYPE_ ,\r\n                    CONSTRAINTDATE_ ,MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ ,PREDECESSORLINK_ ,FIXEDDATE_ ,\r\n                    PARENTTASKUID_ ,PROJECTUID_ ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ ,WBS_ ,CRITICAL2_ )\r\n                SELECT UID_ ,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ , WEIGHT_ ,CONSTRAINTTYPE_ ,CONSTRAINTDATE_ ,\r\n                    MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ , PREDECESSORLINK_ ,FIXEDDATE_ ,PARENTTASKUID_ ,\r\n                    @newVerId ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ , WBS_ ,CRITICAL2_  FROM plus_task \r\n                WHERE PROJECTUID_=@parVerId");
            string inputUser = entity.InputUser;
            SqlParameter parameter = new SqlParameter {
                ParameterName = "@user"
            };
            if (string.IsNullOrEmpty(inputUser))
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = inputUser;
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@newVerId", str), new SqlParameter("@progressId", entity.ProgressId), new SqlParameter("@parVerId", entity.ProgressVersionId), new SqlParameter("@name", entity.VersionName), new SqlParameter("@version", entity.VersionCode), parameter, new SqlParameter("@note", entity.Note) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            return str;
        }

        protected internal static void AddWarn(string progressVerId, pm2Entities context)
        {
            List<string> list = (from m in context.plus_progress_version
                join n in context.plus_progress on m.plus_progress.ProgressId equals n.ProgressId 
                join l in context.plus_progress_privilege on n.ProgressId equals l.plus_progress.ProgressId 
                where m.ProgressVersionId == progressVerId
                select l.PT_yhmc.v_yhdm).ToList<string>();
            if (list != null)
            {
                plus_progress_version _version = (from m in context.plus_progress_version
                    where m.ProgressVersionId == progressVerId
                    select m).FirstOrDefault<plus_progress_version>();
                string str = (from m in context.PT_PrjInfo
                    join n in context.plus_progress on m.PrjGuid equals n.PrjId 
                    join l in context.plus_progress_version on n.ProgressId equals l.plus_progress.ProgressId 
                    where l.ProgressVersionId == progressVerId
                    select m.PrjName).FirstOrDefault<string>();
                string str2 = string.Empty;
                string str3 = string.Empty;
                if (_version.FlowState.HasValue)
                {
                    str2 = "调整";
                }
                else
                {
                    str3 = "&modify=false";
                }
                string str4 = "项目[" + str + "]的进度计划[" + str2 + _version.VersionName + "  " + _version.VersionCode + "]已发布";
                string str5 = "项目[" + str + "]的进度计划[" + str2 + _version.VersionName + "  " + _version.VersionCode + "]已通过" + str2 + "审核，此版本为进度计划的执行版本";
                foreach (string str6 in list)
                {
                    PT_Warning warning = new PT_Warning {
                        WarningTitle = str4,
                        WarningContent = str5,
                        UserCode = str6,
                        URI = "/ProgressManagement/Modify/ApplyView.aspx?ic=" + progressVerId + str3,
                        IsValid = true,
                        InputDate = new DateTime?(DateTime.Now),
                        RelationsTable = "plus_progress_version",
                        RelationsColumn = "ProgressVersionId",
                        RelationsKey = progressVerId
                    };
                    context.AddToPT_Warning(warning);
                }
            }
        }

        public static bool CheckVersionCode(string progressId, string versionCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                bool flag = false;
                if (!string.IsNullOrEmpty((from m in entities.plus_progress_version
                    where (m.plus_progress.ProgressId == progressId) && (m.VersionCode == versionCode)
                    select m.ProgressVersionId).FirstOrDefault<string>()))
                {
                    flag = true;
                }
                return flag;
            }
        }

        public static cn.justwin.BLL.ProgressManagement.Version Create(string progressVersionId, string progressId, string parentVersionId, string VersionName, string VersionCode, string inputUser, DateTime inputDate, string note)
        {
            return new cn.justwin.BLL.ProgressManagement.Version { ProgressVersionId = progressVersionId, ParentVersionId = parentVersionId, ProgressId = progressId, VersionCode = VersionCode, VersionName = VersionName, FlowState = -1, InputUser = inputUser, IsLatest = false, Note = note };
        }

        public static void DelApply(List<string> verIds)
        {
            if (verIds != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    foreach (string str in verIds)
                    {
                        DelApply(str, entities);
                    }
                    entities.SaveChanges();
                }
            }
        }

        private static void DelApply(string verId, pm2Entities context)
        {
            plus_progress_version entity = (from m in context.plus_progress_version
                where m.ProgressVersionId == verId
                select m).FirstOrDefault<plus_progress_version>();
            if (entity != null)
            {
                context.DeleteObject(entity);
            }
        }

        public static DataTable GetAllVersion(string progressVerId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ProgressVersionId,VersionCode FROM plus_progress_version\r\n                WHERE ProgressId=(SELECT ProgressId FROM plus_progress_version WHERE ProgressVersionId=@id)\r\n                AND (FlowState IS NULL OR FlowState=1)\r\n                AND ProgressVersionId <> @id ORDER BY InputDate DESC");
            SqlParameter parameter = new SqlParameter("@id", progressVerId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static cn.justwin.BLL.ProgressManagement.Version GetById(string verId)
        {
            cn.justwin.BLL.ProgressManagement.Version result;
            using (pm2Entities context = new pm2Entities())
            {
                cn.justwin.BLL.ProgressManagement.Version pversion = (
                    from m in context.plus_progress_version
                    where m.ProgressVersionId == verId
                    select new cn.justwin.BLL.ProgressManagement.Version
                    {
                        ProgressVersionId = m.ProgressVersionId,
                        ProgressId = m.plus_progress.ProgressId,
                        VersionName = m.VersionName,
                        VersionCode = m.VersionCode,
                        InputUser = m.PT_yhmc.v_yhdm,
                        Note = m.Note,
                        FlowState = m.FlowState ?? -1,
                        InputDate = (System.DateTime)m.InputDate,
                        IsLatest = (bool)m.IsLatest,
                        ParentVersionId = m.ParentVersionId
                    }).FirstOrDefault<cn.justwin.BLL.ProgressManagement.Version>();
                result = pversion;
            }
            return result;
        }

        public static DataTable GetModifyInfo(string verId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                SELECT V.ProgressVersionId,V.VersionName,V.VersionCode,V.Note,\r\n                    V.InputDate,v_xm AS UserName,v_yhdm AS UserCode,\r\n\t                CASE \r\n\t\t                WHEN V2.VersionName IS NOT NULL THEN V2.VersionName \r\n\t\t                ELSE V.VersionName \r\n\t                END AS PVersionName,\r\n\t                CASE \r\n\t\t                WHEN V2.VersionCode IS NOT NULL THEN V2.VersionCode \r\n\t\t                ELSE V.VersionCode \r\n\t                END AS PVersionCode,\r\n                    (SELECT PrjName FROM PT_PrjInfo WHERE PrjGuid =(\r\n\t                    SELECT TOP(1) PrjId FROM plus_progress WHERE ProgressId \r\n\t\t                    IN(SELECT ProgressId FROM plus_progress_version WHERE ProgressVersionId=V.ProgressVersionId)\r\n                    )) AS PrjName\r\n                FROM plus_progress_version AS V\r\n                LEFT JOIN plus_progress_version AS V2 ON V.ParentVersionId=V2.ProgressVersionId\r\n                LEFT JOIN pt_yhmc ON V.InputUser =pt_yhmc.v_yhdm\r\n                WHERE V.ProgressVersionId=@verId");
            SqlParameter parameter = new SqlParameter("@verId", verId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static string GetProgressVersionId(string progressId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.plus_progress_version
                    where (m.plus_progress.ProgressId == progressId) && (m.FlowState == null)
                    select m.ProgressVersionId).FirstOrDefault<string>();
            }
        }

        public static void Update(string verId, string name, string versionCode, string userCode, string note)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE plus_progress_Version SET VersionName=@name,InputUser=@user,Note=@note,VersionCode=@version WHERE ProgressVersionId=@id");
            SqlParameter parameter = new SqlParameter {
                ParameterName = "@user"
            };
            if (!string.IsNullOrEmpty(userCode))
            {
                parameter.Value = userCode;
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@name", name), new SqlParameter("@version", versionCode), parameter, new SqlParameter("@note", note), new SqlParameter("@id", verId) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static void UpdateLatest(string progressVersionId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (!string.IsNullOrEmpty(progressVersionId))
                {
                    plus_progress progress = (from m in entities.plus_progress_version
                        where m.ProgressVersionId == progressVersionId
                        select m.plus_progress).FirstOrDefault<plus_progress>();
                    List<plus_progress_version> list = (from m in entities.plus_progress_version
                        where ((m.plus_progress.ProgressId == progress.ProgressId) && (m.IsLatest == true)) || (m.ProgressVersionId == progressVersionId)
                        select m).ToList<plus_progress_version>();
                    plus_progress_version _version = (from m in entities.plus_progress_version
                        where m.ProgressVersionId == progressVersionId
                        select m).FirstOrDefault<plus_progress_version>();
                    if ((list != null) && (_version != null))
                    {
                        foreach (plus_progress_version _version2 in list)
                        {
                            _version2.IsLatest = false;
                        }
                        _version.IsLatest = true;
                    }
                    AddWarn(progressVersionId, entities);
                    entities.SaveChanges();
                }
            }
        }

        private int FlowState { get; set; }

        public DateTime InputDate { get; set; }

        public string InputUser { get; set; }

        private bool IsLatest { get; set; }

        public string Note { get; set; }

        public string ParentVersionId { get; set; }

        private string ProgressId { get; set; }

        private string ProgressVersionId { get; set; }

        public string VersionCode { get; set; }

        public string VersionName { get; set; }
    }
}

