namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class BudIndirectDiaryCostService : Repository<BudIndirectDiaryCost>
    {
        public BudIndirectDiaryCost GetById(string inDiaryId)
        {
            return (from i in this
                where i.InDiaryId == inDiaryId
                select i).FirstOrDefault<BudIndirectDiaryCost>();
        }

        public DataTable GetTable(string sWhere, int pageSize, int pageIndex)
        {
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageIndex * pageSize;
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@Start", num),
                new SqlParameter("@End", num2)
            };
            string format = "WITH T AS\r\n                            (\r\n\t                            SELECT BS.InputDate2,BS.IndireCode,BS.IssuedBy,BD.CBSCode,BS.Name,BS.ProjectId,BD.Amount,BS.FlowState,BD.Note  FROM Bud_IndirectDiaryCost BS\r\n\t                            JOIN Bud_IndirectDiaryDetails BD\r\n\t                            ON BS.InDiaryId= BD.InDiaryId\r\n                                ) ,CT AS\r\n\t                            (\r\n\t                            SELECT T.*,P.PrjCode,P.PrjName,P.i_xh FROM T\r\n                                JOIN PT_PrjInfo P \r\n\t                            ON T.ProjectId=P.PrjGuid\r\n                                WHERE LEN(T.ProjectId)>20\r\n\t                            ) ,AT AS\r\n                                (\r\n\t                            SELECT CT.*,PT.i_bmdm FROM  CT\r\n\t                            JOIN PT_yhmc PT\r\n\t                            ON CT.IssuedBy=PT.v_xm\r\n                                ) ,CET AS\r\n                                (\r\n                                SELECT ROW_NUMBER() OVER(ORDER BY InputDate2 DESC)AS NUM,AT.*,BM.V_bmmc FROM  AT\r\n                                LEFT JOIN PT_d_bm BM\r\n                                ON AT.i_bmdm=BM.i_bmdm WHERE 1=1 {0}\r\n                                )\r\n                            SELECT  DISTINCT* FROM CET WHERE NUM BETWEEN @Start AND @End\r\n                            ";
            format = string.Format(format, sWhere);
            return base.ExecuteQuery(format, list.ToArray());
        }

        public DataTable GetTableCount(string sWhere)
        {
            string format = "WITH T AS\r\n                            (\r\n\t                            SELECT BS.InputDate2,BS.IndireCode,BS.IssuedBy,BD.CBSCode,BS.Name,BS.ProjectId,BD.Amount,BS.FlowState,BD.Note  FROM Bud_IndirectDiaryCost BS\r\n\t                            JOIN Bud_IndirectDiaryDetails BD\r\n\t                            ON BS.InDiaryId= BD.InDiaryId\r\n                                ) ,CT AS\r\n\t                            (\r\n\t                            SELECT T.*,P.PrjCode,P.PrjName,P.i_xh FROM T\r\n                                JOIN PT_PrjInfo P \r\n\t                            ON T.ProjectId=P.PrjGuid\r\n                                WHERE LEN(T.ProjectId)>20\r\n\t                            ) ,AT AS\r\n                                (\r\n\t                            SELECT CT.*,PT.i_bmdm FROM  CT\r\n\t                            JOIN PT_yhmc PT\r\n\t                            ON CT.IssuedBy=PT.v_xm\r\n                                ) ,CET AS\r\n                                (\r\n                                SELECT ROW_NUMBER() OVER(ORDER BY InputDate2 DESC)AS NUM,AT.*,BM.V_bmmc FROM  AT\r\n                                LEFT JOIN PT_d_bm BM\r\n                                ON AT.i_bmdm=BM.i_bmdm WHERE 1=1 {0}\r\n                                )\r\n                            SELECT  DISTINCT* FROM CET \r\n                            ";
            format = string.Format(format, sWhere);
            return base.ExecuteQuery(format, new SqlParameter[0]);
        }
    }
}

