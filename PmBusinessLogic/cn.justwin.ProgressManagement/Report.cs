namespace cn.justwin.ProgressManagement
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Report
    {
        private Report()
        {
        }

        public void Add(Report entity, List<ReportDetail> details)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (entity != null)
                {
                    plus_report _report = new plus_report {
                        Id = entity.Id,
                        InputDate = entity.InputDate,
                        InputUser = entity.InputUser,
                        FlowState = entity.FlowState,
                        Note = entity.Note,
                        ProVersionId = entity.ProVersionId
                    };
                    entities.AddToplus_report(_report);
                    ReportDetail.Add(_report, details, entities);
                    entities.SaveChanges();
                }
            }
        }

        public static Report Create(string id, DateTime? inputDate, string inputUser, string note, string ProVersionId)
        {
            return new Report { Id = id, InputDate = inputDate, InputUser = inputUser, FlowState = "-1", Note = note, ProVersionId = ProVersionId };
        }

        public static void Del(List<string> reportIds)
        {
            if (reportIds != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    foreach (string str in reportIds)
                    {
                        Del(str, entities);
                        entities.SaveChanges();
                    }
                }
            }
        }

        private static void Del(string reportId, pm2Entities context)
        {
            plus_report entity = (from m in context.plus_report
                where m.Id == reportId
                select m).FirstOrDefault<plus_report>();
            if (entity != null)
            {
                context.DeleteObject(entity);
            }
        }

        public static Report GetById(string reportId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Report report = null;
                if (!string.IsNullOrWhiteSpace(reportId))
                {
                    report = (from m in entities.plus_report
                        where m.Id == reportId
                        select new Report { Id = m.Id, InputDate = m.InputDate, InputUser = m.InputUser, Note = m.Note }).FirstOrDefault<Report>();
                }
                return report;
            }
        }

        public static DataTable GetReports(string proVerId, int pageSize, int pageNo)
        {
            StringBuilder builder = new StringBuilder();
            int num = (pageSize * (pageNo - 1)) + 1;
            int num2 = pageSize * pageNo;
            builder.AppendFormat("\r\n            WITH ctereports \r\n                 AS (SELECT Row_number() OVER(ORDER BY report.InputDate \r\n                            DESC, report.FlowState ASC) \r\n                            AS NO, \r\n                            report.Id, \r\n                            report.FlowState, \r\n                            report.InputDate, \r\n                            report.Note, \r\n                            yh.v_xm \r\n                               AS UserName \r\n                     FROM   plus_report AS report \r\n                            LEFT JOIN pt_yhmc AS yh \r\n                              ON report.InputUser = yh.v_yhdm \r\n                     WHERE  ProVersionId = '{0}') \r\n            SELECT * \r\n            FROM   ctereports \r\n            WHERE  No BETWEEN {1} AND {2} ", proVerId, num, num2);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static int GetReportsCount(string proVerId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n            SELECT Count(report.Id) \r\n            FROM   plus_report AS report \r\n                   LEFT JOIN pt_yhmc AS yh \r\n                     ON report.InputUser = yh.v_yhdm \r\n            WHERE  ProVersionId = '{0}'", proVerId);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString()).ToString());
        }

        public void Update(Report entity, List<ReportDetail> details)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (entity != null)
                {
                    plus_report report = (from m in entities.plus_report
                        where m.Id == entity.Id
                        select m).FirstOrDefault<plus_report>();
                    if (report != null)
                    {
                        report.InputUser = entity.InputUser;
                        report.InputDate = entity.InputDate;
                        report.Note = entity.Note;
                        ReportDetail.Del(report.Id, entities);
                        ReportDetail.Add(report, details, entities);
                        entities.SaveChanges();
                    }
                }
            }
        }

        public static void UpdateProgress(string reportId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n            WITH cteprogress\r\n                 AS (SELECT detail.TaskUID,\r\n                            detail.Start,\r\n                            detail.Finish,\r\n                            detail.Completed\r\n                     FROM   plus_reportDetail AS detail\r\n                     WHERE  ReportId = '{0}')\r\n            UPDATE plus_task\r\n            SET    ACTUALSTART_ = Start,\r\n                   ACTUALFINISH_ = Finish,\r\n                   PERCENTCOMPLETE_ = Completed\r\n            FROM   plus_task AS T1\r\n                   INNER JOIN cteprogress\r\n                     ON T1.UID_ = TaskUID\r\n            WHERE  PROJECTUID_ = (SELECT TOP(1) ProVersionId\r\n                                  FROM   plus_report\r\n                                  WHERE  Id = '{0}') ", reportId);
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString());
        }

        private string FlowState { get; set; }

        public string Id { get; set; }

        public DateTime? InputDate { get; set; }

        public string InputUser { get; set; }

        public string Note { get; set; }

        private string ProVersionId { get; set; }
    }
}

