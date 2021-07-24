using cn.justwin.DAL;
using PluSoft.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class DBProject
{
    public static void ConvertVersion(string prjId, int version)
    {
        int num2 = GetMaxVersion(prjId) + 1;
        StringBuilder builder = new StringBuilder();
        builder.Append("\r\n            INSERT INTO plus_BackTask (UID_,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ ,WEIGHT_ ,CONSTRAINTTYPE_ ,\r\n                CONSTRAINTDATE_ ,MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ ,PREDECESSORLINK_ ,FIXEDDATE_ ,\r\n                PARENTTASKUID_ ,PROJECTUID_ ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ ,WBS_ ,CRITICAL2_ ,Version)\r\n            SELECT UID_ ,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ ,WEIGHT_ ,CONSTRAINTTYPE_ ,CONSTRAINTDATE_ ,\r\n                MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ ,PREDECESSORLINK_ ,FIXEDDATE_ ,PARENTTASKUID_ ,\r\n                PROJECTUID_ ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ ,WBS_ ,CRITICAL2_ ,@nextVersion FROM plus_task \r\n            WHERE PROJECTUID_=@prjId");
        builder.AppendLine();
        builder.Append("DELETE FROM plus_task WHERE PROJECTUID_ =@prjId");
        builder.AppendLine();
        builder.Append("\r\n            INSERT INTO plus_task (UID_,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ ,WEIGHT_ ,CONSTRAINTTYPE_ ,\r\n                CONSTRAINTDATE_ ,MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ ,PREDECESSORLINK_ ,FIXEDDATE_ ,\r\n                PARENTTASKUID_ ,PROJECTUID_ ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ ,WBS_ ,CRITICAL2_ )\r\n            SELECT UID_ ,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ , WEIGHT_ ,CONSTRAINTTYPE_ ,CONSTRAINTDATE_ ,\r\n                MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ ,PREDECESSORLINK_ ,FIXEDDATE_ ,PARENTTASKUID_ ,\r\n                PROJECTUID_ ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ ,WBS_ ,CRITICAL2_  FROM plus_BackTask \r\n            WHERE PROJECTUID_=@prjId AND Version=@version");
        builder.AppendLine();
        builder.Append("\r\n                INSERT INTO plus_BackProject(ProjectGuid,Calendars,Version,Start,Finish)\r\n                SELECT ProjectGuid,Calendars,@nextVersion+1,Start,Finish FROM plus_BackProject WHERE ProjectGuid=@prjId AND Version=@version");
        SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), new List<SqlParameter> { new SqlParameter("@nextVersion", num2), new SqlParameter("@prjId", prjId), new SqlParameter("@version", version) }.ToArray());
    }

    public static void ConvertXML(string prjId, Hashtable dataProject)
    {
        int num2 = GetMaxVersion(prjId) + 1;
        StringBuilder builder = new StringBuilder();
        builder.Append("\r\n            INSERT INTO plus_BackTask (UID_,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ ,WEIGHT_ ,CONSTRAINTTYPE_ ,\r\n                CONSTRAINTDATE_ ,MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ ,PREDECESSORLINK_ ,FIXEDDATE_ ,\r\n                PARENTTASKUID_ ,PROJECTUID_ ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ ,WBS_ ,CRITICAL2_ ,Version)\r\n            SELECT UID_ ,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ , WEIGHT_ ,CONSTRAINTTYPE_ ,CONSTRAINTDATE_ ,\r\n                MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ , PREDECESSORLINK_ ,FIXEDDATE_ ,PARENTTASKUID_ ,\r\n                PROJECTUID_ ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ ,WBS_ ,CRITICAL2_ ,@nextVersion FROM plus_task \r\n            WHERE PROJECTUID_=@prjId");
        builder.AppendLine();
        builder.Append("DELETE FROM plus_task WHERE PROJECTUID_ =@prjId");
        builder.AppendLine();
        string str = JSON.Encode(dataProject["Calendars"]);
        object obj2 = dataProject["StartDate"];
        object obj3 = dataProject["FinishDate"];
        builder.Append("\r\n            --更新项目时间\r\n          --  UPDATE PT_PrjInfo SET StartDate=@start, EndDate=@finish WHERE PrjGuid=@prjId\r\n            DECLARE @currentBackProject INT  --项目的备份信息\r\n            SELECT @currentBackProject=COUNT(*) FROM  plus_BackProject WHERE ProjectGuid=@prjId AND Version=@nextVersion\r\n            IF EXISTS(SELECT PROJECTUID_ FROM plus_BackTask WHERE PROJECTUID_=@prjId AND Version=@nextVersion) --有数据备份\r\n                BEGIN\r\n                    IF(@currentBackProject=0) \r\n                        INSERT INTO plus_BackProject(ProjectGuid,Calendars,Version,Start,Finish) \r\n                            SELECT UID_,CALENDARS_,@nextVersion,STARTDATE_,FINISHDATE_ FROM plus_project WHERE UID_=@prjId\r\n                     --添加xml中的Calendars信息\r\n                    INSERT INTO plus_BackProject(ProjectGuid,Calendars,Version,Start,Finish)  VALUES( @prjId,@XmlCalendars,@nextVersion+1,@start,@finish)\r\n                END \r\n            ELSE --无数据备份\r\n                BEGIN\r\n                    IF(@currentBackProject>0) \r\n                        BEGIN\r\n                            DELETE FROM plus_BackProject WHERE ProjectGuid=@prjId AND Version=@nextVersion\r\n                            INSERT INTO plus_BackProject(ProjectGuid,Calendars,Version,Start,Finish)  VALUES( @prjId,@XmlCalendars,@nextVersion,@start,@finish)\r\n                        END\r\n                    ELSE\r\n                        INSERT INTO plus_BackProject(ProjectGuid,Calendars,Version,Start,Finish)  VALUES( @prjId,@XmlCalendars,@nextVersion,@start,@finish)\r\n                END");
        builder.AppendLine();
        string calendars = GetCalendars(prjId, num2.ToString());
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@nextVersion", num2), new SqlParameter("@prjId", prjId), new SqlParameter("@calendars", calendars), new SqlParameter("@start", obj2), new SqlParameter("@finish", obj3), new SqlParameter("@XmlCalendars", str) };
        SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        ArrayList tasks = TreeUtil.ToList((ArrayList) dataProject["Tasks"], "-1", "children", "UID", "ParentTaskUID");
        InsertTasks(prjId, tasks);
    }

    public static string CreateTaskUID()
    {
        return Guid.NewGuid().ToString();
    }

    public static void DeleteProject(string projectUID)
    {
        DBUtil.Execute("delete from plus_project where UID_ ='" + projectUID + "'");
        DeleteTasks(projectUID);
    }

    public static void DeleteTasks(string projectUID)
    {
        DBUtil.Execute("delete from plus_task where PROJECTUID_ ='" + projectUID + "'");
    }

    public static string GetCalendars(string progressVerId)
    {
        string str = string.Empty;
        string cmdText = "SELECT Calendars FROM  plus_BackProject WHERE ProjectGuid=@prjId";
        SqlParameter parameter = new SqlParameter("@prjId", progressVerId);
        object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter });
        if (obj2 != null)
        {
            str = obj2.ToString();
        }
        return str;
    }

    public static string GetCalendars(string prjId, string version)
    {
        string str = string.Empty;
        string cmdText = "SELECT Calendars FROM  plus_BackProject WHERE ProjectGuid=@prjId AND Version=@version";
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@version", version) };
        object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters);
        if (obj2 != null)
        {
            str = obj2.ToString();
        }
        return str;
    }

    public static int GetMaxVersion(string prjUID)
    {
        string cmdText = "SELECT ISNULL(MAX(Version),0) FROM plus_BackTask WHERE PROJECTUID_=@prjUID";
        SqlParameter parameter = new SqlParameter("@prjUID", prjUID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter }).ToString());
    }

    public static ArrayList GetStartFinish(string progressVerId)
    {
        return DBUtil.Select("SELECT Project.* FROM plus_project AS Project\r\n                    LEFT JOIN plus_progress AS Progress ON Project.UID_=Progress.PrjId\r\n                    LEFT JOIN plus_progress_version AS Version ON Version.ProgressId=Progress.ProgressId\r\n                    WHERE Version.ProgressVersionId ='" + progressVerId + "'");
    }

    public static DataTable GetStartFinishDate(string progressVerId)
    {
        DataTable table = new DataTable();
        string cmdText = "SELECT Start,Finish FROM  plus_BackProject WHERE ProjectGuid=@prjId";
        SqlParameter parameter = new SqlParameter("@prjId", progressVerId);
        return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
    }

    public static DataTable GetStartFinishDate(string prjId, string version)
    {
        DataTable table = new DataTable();
        string cmdText = "SELECT Start,Finish FROM  plus_BackProject WHERE ProjectGuid=@prjId AND Version=@version";
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@version", version) };
        return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
    }

    public static string ImportVersion(string progressVerId, string hisotryProgresVerId)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("\r\n                DELETE FROM plus_BackProject WHERE ProjectGuid=@progressVerId  --删除日历信息\r\n\t            INSERT INTO plus_BackProject(ProjectGuid,Calendars,Start,Finish) \r\n\t            SELECT @progressVerId,Calendars,Start,Finish FROM plus_BackProject WHERE ProjectGuid=@historyId");
        builder.AppendLine();
        builder.Append("\r\n            DELETE FROM plus_task WHERE PROJECTUID_=@progressVerId");
        builder.AppendLine();
        builder.Append("\r\n            INSERT INTO plus_task (UID_,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ ,WEIGHT_ ,CONSTRAINTTYPE_ ,\r\n                CONSTRAINTDATE_ ,MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ ,PREDECESSORLINK_ ,FIXEDDATE_ ,\r\n                PARENTTASKUID_ ,PROJECTUID_ ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ ,WBS_ ,CRITICAL2_ )\r\n            SELECT UID_ ,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ , WEIGHT_ ,CONSTRAINTTYPE_ ,CONSTRAINTDATE_ ,\r\n                MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ ,PREDECESSORLINK_ ,FIXEDDATE_ ,PARENTTASKUID_ ,\r\n                @progressVerId ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ ,WBS_ ,CRITICAL2_  FROM plus_task \r\n            WHERE PROJECTUID_=@historyId");
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@progressVerId", progressVerId), new SqlParameter("@historyId", hisotryProgresVerId) };
        SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        return progressVerId;
    }

    public static string ImportXML(string progressVerId, Hashtable dataProject)
    {
        StringBuilder builder = new StringBuilder();
        string str = JSON.Encode(dataProject["Calendars"]);
        object obj2 = dataProject["StartDate"];
        object obj3 = dataProject["FinishDate"];
        builder.Append("\r\n            IF EXISTS(SELECT * FROM plus_BackProject WHERE ProjectGuid=@progressVerId)\r\n\t            UPDATE plus_BackProject SET Calendars=@calendars,Start=@start,Finish=@finish WHERE ProjectGuid=@progressVerId \r\n            ELSE\r\n\t            INSERT INTO plus_BackProject(ProjectGuid,Calendars,Start,Finish) \r\n\t            VALUES(@progressVerId,@calendars,@start,@finish)");
        builder.AppendLine();
        builder.Append("\r\n            DELETE FROM plus_task WHERE PROJECTUID_=@progressVerId");
        builder.AppendLine();
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@progressVerId", progressVerId), new SqlParameter("@calendars", str), new SqlParameter("@start", obj2), new SqlParameter("@finish", obj3) };
        SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        ArrayList tasks = TreeUtil.ToList((ArrayList) dataProject["Tasks"], "-1", "children", "UID", "ParentTaskUID");
        InsertTasks(progressVerId, tasks);
        return progressVerId;
    }

    private static void InsertRes(string prjId, string resId, string resName, int units)
    {
        DBUtil.Execute(string.Concat(new object[] { "INSERT INTO plus_resource VALUES('", resId, "','", resName, "',1,", units, ",'", prjId, "')" }));
    }

    public static void InsertTask(string projectUID, Hashtable task)
    {
        ArrayList tasks = new ArrayList();
        tasks.Add(task);
        InsertTasks(projectUID, tasks);
    }

    public static void InsertTasks(string projectUID, ArrayList tasks)
    {
        string sql = "insert into plus_task(UID_, ID_, NAME_, START_, FINISH_, DURATION_, WORK_, PERCENTCOMPLETE_, WEIGHT_, CONSTRAINTTYPE_, CONSTRAINTDATE_, MILESTONE_, SUMMARY_,CRITICAL_, PRIORITY_, NOTES_, DEPARTMENT_, PRINCIPAL_, PREDECESSORLINK_, FIXEDDATE_, PARENTTASKUID_, PROJECTUID_, ACTUALSTART_, ACTUALFINISH_, ACTUALDURATION_, ASSIGNMENTS_, WBS_, CRITICAL2_)values(@UID, @ID, @Name, @Start, @Finish, @Duration, @Work, @PercentComplete, @Weight, @ConstraintType, @ConstraintDate, @Milestone, @Summary, @Critical, @Priority, @Notes, @Department, @Principal, @PredecessorLink, @FixedDate, @ParentTaskUID, @ProjectUID, @ActualStart, @ActualFinish, @ActualDuration, @Assignments, @WBS, @Critical2)";
        foreach (Hashtable hashtable in tasks)
        {
            hashtable["ProjectUID"] = projectUID;
            hashtable["PredecessorLink"] = JSON.Encode(hashtable["PredecessorLink"]);
            hashtable["Assignments"] = JSON.Encode(hashtable["Assignments"]);
        }
        foreach (Hashtable hashtable2 in tasks)
        {
            DBUtil.Execute(sql, hashtable2);
        }
    }

    private static bool isExist(string resId, string prjId)
    {
        bool flag = false;
        if (DBUtil.Select("SELECT * FROM  plus_resource WHERE UID_='" + resId + "' AND PROJECTUID_='" + prjId + "'").Count > 0)
        {
            flag = true;
        }
        return flag;
    }

    public static Hashtable LoadProject(string progressVerId)
    {
        ArrayList startFinish = GetStartFinish(progressVerId);
        if (startFinish.Count == 0)
        {
            throw new Exception("没有找到项目, UID:" + progressVerId);
        }
        Hashtable hashtable = (Hashtable) startFinish[0];
        Hashtable hashtable2 = new Hashtable();
        hashtable2["UID"] = progressVerId;
        hashtable2["Name"] = hashtable["NAME_"];
        hashtable2["LastSaved"] = hashtable["LASTSAVED_"];
        hashtable2["CalendarUID"] = hashtable["CALENDARUID_"];
        hashtable2["StartDate"] = hashtable["STARTDATE_"];
        hashtable2["FinishDate"] = hashtable["FINISHDATE_"];
        DataTable startFinishDate = GetStartFinishDate(progressVerId);
        if ((startFinishDate != null) && (startFinishDate.Rows.Count > 0))
        {
            hashtable2["StartDate"] = startFinishDate.Rows[0]["Start"];
            hashtable2["FinishDate"] = startFinishDate.Rows[0]["Finish"];
        }
        if ((hashtable2["FinishDate"] != null) && (Convert.ToDateTime(hashtable2["FinishDate"].ToString()) <= Convert.ToDateTime(hashtable2["StartDate"].ToString())))
        {
            hashtable2["FinishDate"] = Convert.ToDateTime(hashtable2["StartDate"]).AddMinutes(1.0);
        }
        hashtable2["Calendars"] = (ArrayList) JSON.Decode(hashtable["CALENDARS_"].ToString());
        string calendars = GetCalendars(progressVerId);
        if (!string.IsNullOrEmpty(calendars))
        {
            hashtable2["Calendars"] = (ArrayList) JSON.Decode(calendars);
        }
        ArrayList list2 = TreeUtil.ToTree(SelectTasks(progressVerId), "children", "UID", "ParentTaskUID");
        hashtable2["Tasks"] = list2;
        hashtable2["Resources"] = new ArrayList();
        return hashtable2;
    }

    public static Hashtable LoadProject(string projectUID, string version)
    {
        ArrayList list = DBUtil.Select("select * from plus_project where UID_ ='" + projectUID + "'");
        if (list.Count == 0)
        {
            throw new Exception("没有找到项目, UID:" + projectUID);
        }
        Hashtable hashtable = (Hashtable) list[0];
        Hashtable hashtable2 = new Hashtable();
        hashtable2["UID"] = projectUID;
        hashtable2["Name"] = hashtable["NAME_"];
        hashtable2["LastSaved"] = hashtable["LASTSAVED_"];
        hashtable2["CalendarUID"] = hashtable["CALENDARUID_"];
        hashtable2["StartDate"] = hashtable["STARTDATE_"];
        hashtable2["FinishDate"] = hashtable["FINISHDATE_"];
        DataTable startFinishDate = GetStartFinishDate(projectUID, version);
        if ((startFinishDate != null) && (startFinishDate.Rows.Count > 0))
        {
            hashtable2["StartDate"] = startFinishDate.Rows[0]["Start"];
            hashtable2["FinishDate"] = startFinishDate.Rows[0]["Finish"];
        }
        hashtable2["Calendars"] = (ArrayList) JSON.Decode(hashtable["CALENDARS_"].ToString());
        string calendars = GetCalendars(projectUID, version);
        if (!string.IsNullOrEmpty(calendars))
        {
            hashtable2["Calendars"] = (ArrayList) JSON.Decode(calendars);
        }
        ArrayList list2 = TreeUtil.ToTree(SelectTasks(projectUID, version), "children", "UID", "ParentTaskUID");
        hashtable2["Tasks"] = list2;
        hashtable2["Resources"] = new ArrayList();
        return hashtable2;
    }

    public static ArrayList LoadProjects()
    {
        string sql = "select * from plus_project";
        return DBUtil.Select(sql);
    }

    public static string SaveAll(Hashtable dataProject, string projectUID)
    {
        JSON.Encode(dataProject["Calendars"]);
        SaveAsTask(projectUID);
        DeleteTasks(projectUID);
        ArrayList tasks = TreeUtil.ToList((ArrayList) dataProject["Tasks"], "-1", "children", "UID", "ParentTaskUID");
        InsertTasks(projectUID, tasks);
        return projectUID;
    }

    public static void SaveAsTask(string prjId)
    {
        StringBuilder builder = new StringBuilder();
        int num2 = GetMaxVersion(prjId) + 1;
        builder.Append("\r\n            INSERT INTO plus_BackTask (UID_,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ ,WEIGHT_ ,CONSTRAINTTYPE_ ,\r\n                CONSTRAINTDATE_ ,MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ ,PREDECESSORLINK_ ,FIXEDDATE_ ,\r\n                PARENTTASKUID_ ,PROJECTUID_ ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ ,WBS_ ,CRITICAL2_ ,Version)\r\n            SELECT UID_ ,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ , WEIGHT_ ,CONSTRAINTTYPE_ ,CONSTRAINTDATE_ ,\r\n                MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ , PREDECESSORLINK_ ,FIXEDDATE_ ,PARENTTASKUID_ ,\r\n                PROJECTUID_ ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ , WBS_ ,CRITICAL2_ ,@nextVersion FROM plus_task \r\n            WHERE PROJECTUID_=@prjId");
        builder.AppendLine();
        builder.Append("\r\n\t            INSERT INTO plus_BackProject(ProjectGuid,Calendars,Version,Start,Finish) \r\n\t            SELECT ProjectGuid,Calendars,Version+1,Start,Finish \r\n\t\t            FROM plus_BackProject WHERE projectGuid=@prjId AND Version=@nextVersion");
        GetCalendars(prjId, num2.ToString());
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@nextVersion", num2), new SqlParameter("@prjId", prjId) };
        SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
    }

    public static string SaveAsTask(string progressVerId, string versionCode, bool isLatest)
    {
        string str = Guid.NewGuid().ToString();
        StringBuilder builder = new StringBuilder();
        builder.Append("\r\n            --版本信息另存为\r\n            INSERT INTO plus_progress_version(ProgressVersionId,ProgressId,ParentVersionId,\r\n\t            VersionName,VersionCode,FlowState,IsLatest,InputUser,InputDate,Note)\r\n\t            SELECT @newProgressVerId,ProgressId,ParentVersionId,VersionName,@versionCode,\r\n\t            FlowState,@IsLatest,InputUser,InputDate,Note FROM plus_progress_version\r\n\t            WHERE ProgressVersionId=@progressVerId\r\n            --WBS信息另存为\r\n            INSERT INTO plus_task (UID_,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ ,WEIGHT_ ,CONSTRAINTTYPE_ ,\r\n                CONSTRAINTDATE_ ,MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ ,PREDECESSORLINK_ ,FIXEDDATE_ ,\r\n                PARENTTASKUID_ ,PROJECTUID_ ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ ,WBS_ ,CRITICAL2_ )\r\n            SELECT UID_ ,ID_ ,NAME_ ,START_ ,FINISH_ ,DURATION_ ,WORK_ ,PERCENTCOMPLETE_ , WEIGHT_ ,CONSTRAINTTYPE_ ,CONSTRAINTDATE_ ,\r\n                MILESTONE_ ,SUMMARY_ ,CRITICAL_ ,PRIORITY_ ,NOTES_ ,DEPARTMENT_ ,PRINCIPAL_ , PREDECESSORLINK_ ,FIXEDDATE_ ,PARENTTASKUID_ ,\r\n                @newProgressVerId ,ACTUALSTART_ ,ACTUALFINISH_ ,ACTUALDURATION_ ,ASSIGNMENTS_ , WBS_ ,CRITICAL2_  FROM plus_task \r\n            WHERE PROJECTUID_=@progressVerId");
        builder.AppendLine();
        if (isLatest)
        {
            builder.Append("UPDATE plus_progress_version SET IsLatest='0' \r\n                WHERE ProgressId=(SELECT ProgressId FROM plus_progress_version WHERE ProgressVersionId=@progressVerId)\r\n                AND IsLatest ='1'");
            builder.AppendLine();
        }
        builder.Append("INSERT INTO plus_BackProject(ProjectGuid,Calendars,Start,Finish) \r\n                    SELECT @newProgressVerId,Calendars,Start,Finish \r\n                    FROM plus_BackProject WHERE projectGuid=@progressVerId");
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@IsLatest", isLatest), new SqlParameter("@versionCode", versionCode), new SqlParameter("@progressVerId", progressVerId), new SqlParameter("@newProgressVerId", str) };
        SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        return str;
    }

    public static void SaveCalendars(string progressVerId, string calendars)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("\r\n            IF EXISTS( SELECT ProjectGuid FROM  plus_BackProject WHERE ProjectGuid=@prjId )\r\n\t            UPDATE plus_BackProject SET Calendars=@calendars WHERE  ProjectGuid=@prjId \r\n            ELSE \r\n\t            INSERT INTO plus_BackProject(ProjectGuid,Calendars,Start,Finish) \r\n                SELECT @prjId,@calendars,StartDate,EndDate FROM PT_PrjInfo AS Project\r\n                    LEFT JOIN plus_progress AS Progress ON Project.PrjGuid=Progress.PrjId\r\n                    LEFT JOIN plus_progress_version AS Version ON Version.ProgressId=Progress.ProgressId\r\n                    WHERE Version.ProgressVersionId=@prjId");
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", progressVerId), new SqlParameter("@calendars", calendars) };
        SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
    }

    public static void SaveCalendars(string prjId, string calendars, string version)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("\r\n            IF EXISTS( SELECT ProjectGuid FROM  plus_BackProject WHERE ProjectGuid=@prjId AND Version=@version)\r\n\t            UPDATE plus_BackProject SET Calendars=@calendars WHERE  ProjectGuid=@prjId  AND Version=@version\r\n            ELSE \r\n\t            INSERT INTO plus_BackProject(ProjectGuid,Calendars,Version,Start,Finish) \r\n                SELECT @prjId,@calendars,@version,StartDate,EndDate FROM PT_PrjInfo WHERE PrjGuid=@prjId");
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", prjId), new SqlParameter("@calendars", calendars), new SqlParameter("@version", version) };
        SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
    }

    public static string SavePart(Hashtable dataProject)
    {
        string prjId = dataProject["UID"].ToString();
        string calendars = JSON.Encode(dataProject["Calendars"]);
        SaveCalendars(prjId, calendars, (GetMaxVersion(prjId) + 1).ToString());
        ArrayList list = TreeUtil.ToList((ArrayList) dataProject["Tasks"], "-1", "children", "UID", "ParentTaskUID");
        ArrayList c = (ArrayList) dataProject["RemovedTasks"];
        if (c != null)
        {
            list.AddRange(c);
        }
        foreach (Hashtable hashtable in list)
        {
            string str3 = Convert.ToString(hashtable["_state"]);
            if (str3 != "")
            {
                hashtable["ProjectUID"] = prjId;
                string str4 = str3;
                if (str4 != null)
                {
                    if (!(str4 == "added"))
                    {
                        if (str4 == "modified")
                        {
                            goto Label_010C;
                        }
                        if (str4 == "removed")
                        {
                            goto Label_0122;
                        }
                    }
                    else
                    {
                        InsertTask(prjId, hashtable);
                    }
                }
            }
            continue;
        Label_010C:
            DBUtil.Execute("delete from plus_task where PROJECTUID_ = @ProjectUID and UID_ = @UID ", hashtable);
            InsertTask(prjId, hashtable);
            continue;
        Label_0122:
            DBUtil.Execute("delete from plus_task where PROJECTUID_ = @ProjectUID and UID_ = @UID ", hashtable);
        }
        return prjId;
    }

    public static void SavePart2(Hashtable dataProject)
    {
        string progressVerId = dataProject["UID"].ToString();
        string calendars = JSON.Encode(dataProject["Calendars"]);
        SaveCalendars(progressVerId, calendars);
        ArrayList list = TreeUtil.ToList((ArrayList) dataProject["Tasks"], "-1", "children", "UID", "ParentTaskUID");
        ArrayList c = (ArrayList) dataProject["RemovedTasks"];
        if (c != null)
        {
            list.AddRange(c);
        }
        foreach (Hashtable hashtable in list)
        {
            string str3 = Convert.ToString(hashtable["_state"]);
            if (str3 != "")
            {
                hashtable["ProjectUID"] = progressVerId;
                string str4 = str3;
                if (str4 != null)
                {
                    if (!(str4 == "added"))
                    {
                        if (str4 == "modified")
                        {
                            goto Label_00F4;
                        }
                        if (str4 == "removed")
                        {
                            goto Label_010A;
                        }
                    }
                    else
                    {
                        InsertTask(progressVerId, hashtable);
                    }
                }
            }
            continue;
        Label_00F4:
            DBUtil.Execute("delete from plus_task where PROJECTUID_ = @ProjectUID and UID_ = @UID ", hashtable);
            InsertTask(progressVerId, hashtable);
            continue;
        Label_010A:
            DBUtil.Execute("delete from plus_task where PROJECTUID_ = @ProjectUID and UID_ = @UID ", hashtable);
        }
    }

    public static string SaveProject(Hashtable dataProject)
    {
        return SavePart(dataProject);
    }

    private static string SaveProjectOnly(Hashtable dataProject)
    {
        if (dataProject["UID"] == null)
        {
            dataProject["UID"] = Guid.NewGuid().ToString();
        }
        return dataProject["UID"].ToString();
    }

    public static void SaveRes(Hashtable taskData, string prjId)
    {
        ArrayList list = (ArrayList) taskData["Assignments"];
        for (int i = 0; i < list.Count; i++)
        {
            Hashtable hashtable = (Hashtable) list[i];
            string resId = hashtable["ResourceUID"].ToString();
            string resName = hashtable["ResourceName"].ToString();
            int units = Convert.ToInt32(hashtable["Units"].ToString());
            if (!isExist(resId, prjId))
            {
                InsertRes(prjId, resId, resName, units);
            }
        }
    }

    public static ArrayList SelectTasks(string progressVerId)
    {
        ArrayList list = DBUtil.Select("SELECT * FROM plus_task WHERE PROJECTUID_ ='" + progressVerId + "' ORDER BY ID_");
        ArrayList list2 = new ArrayList();
        foreach (Hashtable hashtable in list)
        {
            Hashtable hashtable2 = new Hashtable();
            hashtable2["UID"] = hashtable["UID_"];
            hashtable2["WBS"] = hashtable["WBS_"];
            hashtable2["ID"] = hashtable["ID_"];
            hashtable2["Name"] = hashtable["NAME_"];
            hashtable2["Start"] = hashtable["START_"];
            hashtable2["Finish"] = hashtable["FINISH_"];
            hashtable2["Duration"] = hashtable["DURATION_"];
            hashtable2["Work"] = hashtable["WORK_"];
            hashtable2["PercentComplete"] = hashtable["PERCENTCOMPLETE_"];
            hashtable2["Weight"] = hashtable["WEIGHT_"];
            hashtable2["ConstraintType"] = hashtable["CONSTRAINTTYPE_"];
            hashtable2["ConstraintDate"] = hashtable["CONSTRAINTDATE_"];
            hashtable2["Milestone"] = hashtable["MILESTONE_"];
            hashtable2["Summary"] = hashtable["SUMMARY_"];
            hashtable2["Critical2"] = hashtable["CRITICAL2_"];
            hashtable2["Critical"] = hashtable["CRITICAL2_"];
            hashtable2["Priority"] = "500";
            hashtable2["Notes"] = hashtable["NOTES_"];
            if (!StringUtil.IsNullOrEmpty(hashtable["PREDECESSORLINK_"]))
            {
                hashtable2["PredecessorLink"] = (ArrayList) JSON.Decode(hashtable["PREDECESSORLINK_"].ToString());
            }
            if (!StringUtil.IsNullOrEmpty(hashtable["ASSIGNMENTS_"]))
            {
                ArrayList list3 = (ArrayList) JSON.Decode(hashtable["ASSIGNMENTS_"].ToString());
                hashtable2["Assignments"] = list3;
            }
            hashtable2["FixedDate"] = hashtable["FIXEDDATE_"];
            hashtable2["ParentTaskUID"] = hashtable["PARENTTASKUID_"];
            hashtable2["ProjectUID"] = hashtable["PROJECTUID_"];
            hashtable2["ActualStart"] = hashtable["ACTUALSTART_"];
            hashtable2["ActualFinish"] = hashtable["ACTUALFINISH_"];
            hashtable2["ActualDuration"] = hashtable["ACTUALDURATION_"];
            list2.Add(hashtable2);
        }
        return list2;
    }

    public static ArrayList SelectTasks(string projectUID, string version)
    {
        string sql = string.Empty;
        int num = GetMaxVersion(projectUID) + 1;
        if (version.Equals(num.ToString()))
        {
            sql = "SELECT * FROM plus_task WHERE PROJECTUID_ ='" + projectUID + "' ORDER BY ID_";
        }
        else
        {
            sql = "SELECT * FROM plus_BackTask WHERE PROJECTUID_='" + projectUID + "' AND Version=" + version + "  ORDER BY ID_";
        }
        ArrayList list = DBUtil.Select(sql);
        ArrayList list2 = new ArrayList();
        foreach (Hashtable hashtable in list)
        {
            Hashtable hashtable2 = new Hashtable();
            hashtable2["UID"] = hashtable["UID_"];
            hashtable2["WBS"] = hashtable["WBS_"];
            hashtable2["ID"] = hashtable["ID_"];
            hashtable2["Name"] = hashtable["NAME_"];
            hashtable2["Start"] = hashtable["START_"];
            hashtable2["Finish"] = hashtable["FINISH_"];
            hashtable2["Duration"] = hashtable["DURATION_"];
            hashtable2["Work"] = hashtable["WORK_"];
            hashtable2["PercentComplete"] = hashtable["PERCENTCOMPLETE_"];
            hashtable2["Weight"] = hashtable["WEIGHT_"];
            hashtable2["ConstraintType"] = hashtable["CONSTRAINTTYPE_"];
            hashtable2["ConstraintDate"] = hashtable["CONSTRAINTDATE_"];
            hashtable2["Milestone"] = hashtable["MILESTONE_"];
            hashtable2["Summary"] = hashtable["SUMMARY_"];
            hashtable2["Critical2"] = hashtable["CRITICAL2_"];
            hashtable2["Critical"] = hashtable["CRITICAL2_"];
            hashtable2["Priority"] = "500";
            hashtable2["Notes"] = hashtable["NOTES_"];
            if (!StringUtil.IsNullOrEmpty(hashtable["PREDECESSORLINK_"]))
            {
                hashtable2["PredecessorLink"] = (ArrayList) JSON.Decode(hashtable["PREDECESSORLINK_"].ToString());
            }
            if (!StringUtil.IsNullOrEmpty(hashtable["ASSIGNMENTS_"]))
            {
                ArrayList list3 = (ArrayList) JSON.Decode(hashtable["ASSIGNMENTS_"].ToString());
                hashtable2["Assignments"] = list3;
            }
            hashtable2["FixedDate"] = hashtable["FIXEDDATE_"];
            hashtable2["ParentTaskUID"] = hashtable["PARENTTASKUID_"];
            hashtable2["ProjectUID"] = hashtable["PROJECTUID_"];
            hashtable2["ActualStart"] = hashtable["ACTUALSTART_"];
            hashtable2["ActualFinish"] = hashtable["ACTUALFINISH_"];
            hashtable2["ActualDuration"] = hashtable["ACTUALDURATION_"];
            list2.Add(hashtable2);
        }
        return list2;
    }
}

