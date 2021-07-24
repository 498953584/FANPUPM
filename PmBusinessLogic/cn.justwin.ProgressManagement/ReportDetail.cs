namespace cn.justwin.ProgressManagement
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class ReportDetail
    {
        protected ReportDetail()
        {
        }

        protected internal static void Add(plus_report report, List<ReportDetail> details, pm2Entities context)
        {
            if (details != null)
            {
                foreach (ReportDetail detail in details)
                {
                    plus_reportDetail detail2 = new plus_reportDetail {
                        Id = Guid.NewGuid().ToString(),
                        plus_report = report,
                        Start = detail.Start,
                        Finish = detail.Finish,
                        Completed = new byte?(detail.Completed),
                        Note = detail.Note,
                        TaskUID = detail.TaskUID
                    };
                    context.AddToplus_reportDetail(detail2);
                }
            }
        }

        public static ReportDetail Create(string id, string reportId, string taskUID, DateTime? start, DateTime? finish, byte completed, string note)
        {
            return new ReportDetail { Id = id, ReportId = reportId, TaskUID = taskUID, Completed = completed, Start = start, Finish = finish, Note = note };
        }

        protected internal static void Del(string reportId, pm2Entities context)
        {
            List<plus_reportDetail> list = (from m in context.plus_reportDetail
                where m.ReportId == reportId
                select m).ToList<plus_reportDetail>();
            if (list != null)
            {
                foreach (plus_reportDetail detail in list)
                {
                    context.DeleteObject(detail);
                }
            }
        }

        public static DataTable GetChildTask(string proVerId, string taskUID)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n            WITH cteplus \r\n                 AS (SELECT \r\n                            PROJECTUID_,\r\n                            ID_, \r\n                            UID_, \r\n                            PARENTTASKUID_, \r\n                            NAME_, \r\n                            START_, \r\n                            FINISH_, \r\n                            DURATION_, \r\n                            ACTUALSTART_, \r\n                            ACTUALFINISH_, \r\n                            PERCENTCOMPLETE_, \r\n                            1 AS Layer \r\n                     FROM   plus_task \r\n                     WHERE  PARENTTASKUID_ = '-1' \r\n                            AND PROJECTUID_ = '{0}' \r\n                     UNION ALL \r\n                     SELECT Task.PROJECTUID_,\r\n                            Task.ID_, \r\n                            Task.UID_, \r\n                            Task.PARENTTASKUID_, \r\n                            Task.NAME_, \r\n                            Task.START_, \r\n                            Task.FINISH_, \r\n                            Task.DURATION_, \r\n                            Task.ACTUALSTART_, \r\n                            Task.ACTUALFINISH_, \r\n                            Task.PERCENTCOMPLETE_, \r\n                            Layer + 1 \r\n                     FROM   plus_task AS Task \r\n                            INNER JOIN cteplus p \r\n                              ON Task.PARENTTASKUID_ = p.UID_ \r\n                     WHERE  Task.PROJECTUID_ = '{0}'), \r\n                 cteplusdesc \r\n                 AS (SELECT Row_number() OVER(ORDER BY ID_ ASC)    AS No, \r\n                            *, \r\n                            (SELECT Count(UID_) \r\n                             FROM   plus_task \r\n                             WHERE  PARENTTASKUID_ = ctePlus.UID_ AND PROJECTUID_=ctePlus.PROJECTUID_) AS SubCount \r\n                     FROM   cteplus) \r\n            SELECT * \r\n            FROM   cteplusdesc \r\n            WHERE  PARENTTASKUID_='{1}' ", proVerId, taskUID);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static DataTable GetDetails(string reportId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n            SELECT detail.Id, \r\n                   detail.TaskUID, \r\n                   detail.Start, \r\n                   detail.Finish, \r\n                   CAST( detail.Completed AS INT) AS Completed, \r\n                   detail.Note, \r\n                   ptask.start_, \r\n                   ptask.finish_, \r\n                   ptask.duration_, \r\n                   ptask.name_                                AS TaskName \r\n            FROM   plus_reportDetail AS detail \r\n                   INNER JOIN plus_task AS ptask \r\n                     ON detail.TaskUID = ptask.UID_ \r\n            WHERE  ReportId = '{0}' \r\n                   AND PROJECTUID_ = (SELECT TOP(1) ProVersionId \r\n                                      FROM   plus_report \r\n                                      WHERE  Id = '{0}') \r\n            ORDER BY ptask .ID_ ASC", reportId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static DataTable GetTasks(string proVerId, List<string> taskIds)
        {
            DataTable table = new DataTable();
            if (taskIds != null)
            {
                string inParameterSql = DBHelper.GetInParameterSql(taskIds.ToArray());
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("\r\n                SELECT  CAST(NEWID() AS VARCHAR(100))  AS Id, \r\n                       UID_                   AS TaskUID, \r\n                       ptask.ACTUALSTART_     AS Start, \r\n                       ptask.ACTUALFINISH_    AS Finish, \r\n                       ptask.PERCENTCOMPLETE_ AS Completed, \r\n                       ''                     AS Note, \r\n                       ptask.START_, \r\n                       ptask.FINISH_, \r\n                       ptask.DURATION_, \r\n                       ptask.NAME_            AS TaskName \r\n                FROM   plus_task AS ptask \r\n                WHERE  UID_ IN( {0} ) AND PROJECTUID_='{1}'  \r\n                ORDER BY ID_ ASC  ", inParameterSql, proVerId);
                table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
            }
            return table;
        }

        public static DataTable GetTreeTask(string proVerId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n            WITH cteplus \r\n                    AS (SELECT ID_, \r\n                            UID_, \r\n                            PARENTTASKUID_, \r\n                            NAME_, \r\n                            START_, \r\n                            FINISH_, \r\n                            DURATION_, \r\n                            ACTUALSTART_, \r\n                            ACTUALFINISH_, \r\n                            PERCENTCOMPLETE_, \r\n                            1 AS Layer \r\n                        FROM   plus_task \r\n                        WHERE  PARENTTASKUID_ = '-1' \r\n                            AND PROJECTUID_ = '{0}' \r\n                        UNION ALL \r\n                        SELECT Task.ID_, \r\n                            Task.UID_, \r\n                            Task.PARENTTASKUID_, \r\n                            Task.NAME_, \r\n                            Task.START_, \r\n                            Task.FINISH_, \r\n                            Task.DURATION_, \r\n                            Task.ACTUALSTART_, \r\n                            Task.ACTUALFINISH_, \r\n                            Task.PERCENTCOMPLETE_, \r\n                            Layer + 1 \r\n                        FROM   plus_task AS Task \r\n                            INNER JOIN cteplus p \r\n                                ON Task.PARENTTASKUID_ = p.UID_ \r\n                        WHERE  Task.PROJECTUID_ = '{0}'), \r\n                    cteplusdesc \r\n                    AS (SELECT Row_number() OVER(ORDER BY ID_ ASC)    AS No, \r\n                            *, \r\n                            (SELECT Count(UID_) \r\n                                FROM   ctePlus AS T\r\n                                WHERE  T.PARENTTASKUID_ = ctePlus.UID_) AS SubCount \r\n                        FROM   cteplus) \r\n            SELECT * \r\n            FROM   cteplusdesc \r\n            WHERE  Layer <= 2 ", proVerId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        private byte Completed { get; set; }

        private DateTime? Finish { get; set; }

        private string Id { get; set; }

        private string Note { get; set; }

        private string ReportId { get; set; }

        private DateTime? Start { get; set; }

        private string TaskUID { get; set; }
    }
}

