namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class PrjMilestoneDetailService : Repository<PrjMilestoneDetail>
    {
        public DataTable GetComList()
        {
            DateTime time = DateTime.Today.AddDays((double) (1 - DateTime.Today.Day));
            DateTime time2 = DateTime.Today.AddDays((double) ((1 - DateTime.Today.Day) + 14));
            DateTime now = DateTime.Now;
            DateTime time4 = new DateTime();
            if (now.Day < time2.Day)
            {
                time4 = Convert.ToDateTime(time);
            }
            else
            {
                time4 = Convert.ToDateTime(time2);
            }
            string cmdText = "SELECT * FROM Prj_MilestoneDetail WHERE RptDate='" + time4 + "'";
            return base.ExecuteQuery(cmdText, new SqlParameter[0]);
        }

        public DataTable GetComYearList(int year, string sWhere)
        {
            string format = "SELECT  ROW_NUMBER() OVER(ORDER BY RptDate)AS NUM,RptDate,\r\n                         SUM(ForeCast)/NULLIF(Sum(StoreAmount),0.0) AS StoreSwitchRate,\r\n                         SUM(StoreAmount) AS StoreAmount,SUM(ForeCast) AS ForeCast,\r\n                         SUM(NextForeCast)AS NextForeCast,SUM(Stone1) AS Stone1,SUM(Stone2)AS Stone2\r\n                         ,SUM(Stone3)AS Stone3,SUM(Stone4)AS Stone4,SUM(Stone5)AS Stone5,SUM(Stone6)AS Stone6\r\n                         ,SUM(Stone7) AS Stone7,SUM(Stone8) AS Stone8,SUM(Stone9)AS Stone9 FROM  Prj_MilestoneDetail\r\n                         WHERE YEAR(RptDate)={0}{1}\r\n                         GROUP BY RptDate\r\n                         ORDER BY RptDate DESC";
            format = string.Format(format, year, sWhere);
            return base.ExecuteQuery(format, new SqlParameter[0]);
        }

        public DataTable GetCount(string sWhere, string depCode)
        {
            string format = "SELECT ROW_NUMBER() OVER(ORDER BY UserName)AS NUM, UserName,SUM(ForeCast)/NULLIF(Sum(StoreAmount),0.0) AS StoreSwitchRate,\r\n                           Sum(StoreAmount) AS StoreAmount,SUM(ForeCast) AS ForeCast,\r\n                           SUM(NextForeCast)AS NextForeCast,SUM(Stone1) AS Stone1,SUM(Stone2)AS Stone2\r\n                           ,SUM(Stone3)AS Stone3,SUM(Stone4)AS Stone4,SUM(Stone5)AS Stone5,SUM(Stone6)AS Stone6\r\n                           ,SUM(Stone7) AS Stone7,SUM(Stone8) AS Stone8,SUM(Stone9)AS Stone9 FROM  Prj_MilestoneDetail\r\n                           WHERE DepCode='{0}' {1}\r\n                           GROUP BY UserName";
            format = string.Format(format, depCode, sWhere);
            return base.ExecuteQuery(format, new SqlParameter[0]);
        }

        public DataTable GetDepAllList(string depName)
        {
            DateTime time = DateTime.Today.AddDays((double) (1 - DateTime.Today.Day));
            DateTime time2 = DateTime.Today.AddDays((double) ((1 - DateTime.Today.Day) + 14));
            DateTime now = DateTime.Now;
            DateTime time4 = new DateTime();
            if (now.Day < time2.Day)
            {
                time4 = Convert.ToDateTime(time);
            }
            else
            {
                time4 = Convert.ToDateTime(time2);
            }
            string cmdText = string.Format("SELECT * FROM Prj_MilestoneDetail WHERE DepCode='{0}' AND RptDate='" + time4 + "'", depName);
            return base.ExecuteQuery(cmdText, new SqlParameter[0]);
        }

        public DataTable GetDepYearCount(string sWhere, string depCode)
        {
            string cmdText = string.Format(" \r\n                            WITH Prj_Info AS(\r\n\t                        SELECT prjMiles.*,yh.v_xm,yh.i_bmdm FROM (\r\n\t                        SELECT ROW_NUMBER() OVER(partition by UserCode \r\n\t                        order by RptDate desc) AS orde,* FROM Prj_Milestone) AS prjMiles\r\n                            LEFT JOIN PT_yhmc AS yh ON prjMiles.UserCode=yh.v_yhdm\r\n                            WHERE 1=1 {0}\r\n                            ),Prj_User_Info AS(\r\n\t                            SELECT PrjInfo.*,dm.V_BMMC,dm.v_bmqc FROM Prj_Info AS PrjInfo \r\n                            LEFT JOIN PT_d_bm AS dm\r\n\t                            ON PrjInfo.i_bmdm=dm.i_bmdm\r\n                            ),CRT_Miles AS(\r\n                            SELECT RptDate, v_BMMC,Sum(StoreAmount) AS StoreAmount,SUM(ForeCast) AS ForeCast,\r\n                            SUM(ForeCast)/NULLIF(Sum(StoreAmount),0.0) AS StoreSwitchRate,\r\n                            SUM(NextForeCast)AS NextForeCast,SUM(Stone1) AS Stone1,SUM(Stone2)AS Stone2\r\n                            ,SUM(Stone3)AS Stone3,SUM(Stone4)AS Stone4,SUM(Stone5)AS Stone5,SUM(Stone6)AS Stone6\r\n                            ,SUM(Stone7) AS Stone7,SUM(Stone8) AS Stone8,SUM(Stone9)AS Stone9,i_bmdm  \r\n                            FROM Prj_User_Info group by i_bmdm,v_BMMC,RptDate\r\n                            ),CRT_dept_Miles AS(\r\n                            SELECT ROW_NUMBER() OVER(ORDER BY i_bmdm )AS NUM, * FROM CRT_Miles WHERE i_bmdm='{1}'\r\n                            )\r\n                            SELECT * FROM CRT_dept_Miles ", sWhere, depCode);
            return base.ExecuteQuery(cmdText, new SqlParameter[0]);
        }

        public DataTable GetDepYearDetail(string sWhere, string depCode, int pageSize, int pageIndex)
        {
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageIndex * pageSize;
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@Start", num),
                new SqlParameter("@End", num2)
            };
            string cmdText = string.Format(" \r\n                            WITH Prj_Info AS(\r\n\t                        SELECT prjMiles.*,yh.v_xm,yh.i_bmdm FROM (\r\n\t                        SELECT ROW_NUMBER() OVER(partition by UserCode \r\n\t                        order by RptDate desc) AS orde,* FROM Prj_Milestone) AS prjMiles\r\n                            LEFT JOIN PT_yhmc AS yh ON prjMiles.UserCode=yh.v_yhdm\r\n                            WHERE 1=1 {0}\r\n                            ),Prj_User_Info AS(\r\n\t                            SELECT PrjInfo.*,dm.V_BMMC,dm.v_bmqc FROM Prj_Info AS PrjInfo \r\n                            LEFT JOIN PT_d_bm AS dm\r\n\t                            ON PrjInfo.i_bmdm=dm.i_bmdm\r\n                            ),CRT_Miles AS(\r\n                            SELECT RptDate, v_BMMC,Sum(StoreAmount) AS StoreAmount,SUM(ForeCast) AS ForeCast,\r\n                            SUM(ForeCast)/NULLIF(Sum(StoreAmount),0.0) AS StoreSwitchRate,\r\n                            SUM(NextForeCast)AS NextForeCast,SUM(Stone1) AS Stone1,SUM(Stone2)AS Stone2\r\n                            ,SUM(Stone3)AS Stone3,SUM(Stone4)AS Stone4,SUM(Stone5)AS Stone5,SUM(Stone6)AS Stone6\r\n                            ,SUM(Stone7) AS Stone7,SUM(Stone8) AS Stone8,SUM(Stone9)AS Stone9,i_bmdm  \r\n                            FROM Prj_User_Info group by i_bmdm,v_BMMC,RptDate\r\n                            ),CRT_dept_Miles AS(\r\n                            SELECT ROW_NUMBER() OVER(ORDER BY i_bmdm )AS NUM, * FROM CRT_Miles  WHERE i_bmdm='{1}'\r\n                            )\r\n                            SELECT * FROM CRT_dept_Miles WHERE NUM BETWEEN @Start AND @End ORDER BY RptDate DESC", sWhere, depCode);
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public DataTable GetPerAllList(string userName)
        {
            DateTime time = DateTime.Today.AddDays((double) (1 - DateTime.Today.Day));
            DateTime time2 = DateTime.Today.AddDays((double) ((1 - DateTime.Today.Day) + 14));
            DateTime now = DateTime.Now;
            DateTime time4 = new DateTime();
            if (now.Day < time2.Day)
            {
                time4 = Convert.ToDateTime(time);
            }
            else
            {
                time4 = Convert.ToDateTime(time2);
            }
            string cmdText = string.Format("SELECT * FROM Prj_MilestoneDetail WHERE UserName='{0}' AND RptDate='" + time4 + "'", userName);
            return base.ExecuteQuery(cmdText, new SqlParameter[0]);
        }

        public DataTable GetPerList()
        {
            string cmdText = "SELECT PT_yhmc.v_xm, Prj_Milestone.RptDate,Prj_Milestone.StoreAmount,Prj_Milestone.ForeCast,Prj_Milestone.StoreSwitchRate,\r\n                            Prj_Milestone.NextForeCast,Prj_Milestone.Stone1,Prj_Milestone.Stone2,\r\n                            Prj_Milestone.Stone3,Prj_Milestone.Stone4,Prj_Milestone.Stone5,\r\n                            Prj_Milestone.Stone6,\r\n                            Prj_Milestone.Stone7,\r\n                            Prj_Milestone.Stone8,\r\n                            Prj_Milestone.Stone9,\r\n                            (Stone1+Stone2+Stone3+Stone4+Stone5+Stone6+Stone7+Stone8+Stone9)AS Total\r\n                            FROM Prj_Milestone \r\n                            JOIN PT_yhmc ON Prj_Milestone.UserCode=PT_yhmc.v_yhdm";
            return base.ExecuteQuery(cmdText, new SqlParameter[0]);
        }

        public DataTable GetTable(string sWhere, string depCode, int pageSize, int pageIndex)
        {
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageIndex * pageSize;
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@Start", num),
                new SqlParameter("@End", num2)
            };
            string format = "SELECT * FROM(\r\n                           SELECT ROW_NUMBER() OVER(ORDER BY UserName)AS NUM, UserName,SUM(ForeCast)/NULLIF(Sum(StoreAmount),0.0) AS StoreSwitchRate,\r\n                           Sum(StoreAmount) AS StoreAmount,SUM(ForeCast) AS ForeCast,\r\n                           SUM(NextForeCast)AS NextForeCast,SUM(Stone1) AS Stone1,SUM(Stone2)AS Stone2\r\n                           ,SUM(Stone3)AS Stone3,SUM(Stone4)AS Stone4,SUM(Stone5)AS Stone5,SUM(Stone6)AS Stone6\r\n                           ,SUM(Stone7) AS Stone7,SUM(Stone8) AS Stone8,SUM(Stone9)AS Stone9 FROM  Prj_MilestoneDetail\r\n                           WHERE DepCode='{0}' {1}\r\n                           GROUP BY UserName\r\n                           ) T WHERE NUM BETWEEN @Start AND @End";
            format = string.Format(format, depCode, sWhere);
            return base.ExecuteQuery(format, list.ToArray());
        }
    }
}

