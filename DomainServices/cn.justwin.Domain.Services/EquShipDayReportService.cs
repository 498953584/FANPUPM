namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class EquShipDayReportService : Repository<EquShipDayReport>
    {
        public EquShipDayReport GetById(string id)
        {
            return (from r in this
                where r.DayId.Equals(id)
                select r).FirstOrDefault<EquShipDayReport>();
        }

        public DataTable GetEquDayReportList(string equId, int year, int month)
        {
            string cmdText = string.Format("WITH EquProperty AS(\r\n                                           SELECT ItemCode,ItemName FROM Basic_CodeList WHERE TypeCode = 'EquProperty')\r\n                                         SELECT row_number() over(order by report.ConstructionDate ASC) AS Num,\r\n                                                report.DayId, report.ConstructionDate, equ.EquName, prj.PrjName, pro.ItemName, report.ReportDate, report.Note\r\n                                         FROM Equ_ShipDayReport AS report LEFT JOIN Equ_Equipment AS equ ON report.EquId = equ.Id\r\n                                              LEFT JOIN PT_PrjInfo AS prj ON report.PrjId = prj.PrjGuid\r\n                                              LEFT JOIN EquProperty AS pro ON equ.EquProperty = pro.ItemCode\r\n                                         WHERE report.EquId = '{0}' AND year(report.ConstructionDate) = {1} AND month(report.ConstructionDate) = {2}", equId, year, month);
            return base.ExecuteQuery(cmdText, new SqlParameter[0]);
        }
    }
}

