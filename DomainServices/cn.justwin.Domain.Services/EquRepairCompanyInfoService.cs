namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class EquRepairCompanyInfoService : Repository<EquRepairCompanyInfo>
    {
        public DataTable DeleteAllChildData(string code)
        {
            string format = "WITH children AS(SELECT * FROM Equ_RepairCompanyInfo WHERE ItemCode = '{0}'\r\n                            UNION all SELECT Equ_RepairCompanyInfo.*\r\n                                      FROM children, Equ_RepairCompanyInfo\r\n                                      WHERE children.ItemCode = Equ_RepairCompanyInfo.ParentCode)\r\n                           DELETE FROM Equ_RepairCompanyInfo WHERE ItemCode IN (SELECT ItemCode FROM children)";
            return base.ExecuteQuery(string.Format(format, code), new SqlParameter[0]);
        }

        public DataTable GetAllChildData(string code)
        {
            string format = "WITH children AS(SELECT * FROM Equ_RepairCompanyInfo WHERE ItemCode = '{0}'\r\n                            UNION all SELECT Equ_RepairCompanyInfo.*\r\n                                      FROM children, Equ_RepairCompanyInfo\r\n                                      WHERE children.ItemCode = Equ_RepairCompanyInfo.ParentCode)\r\n                           SELECT * FROM children WHERE ItemCode <> '{0}'";
            return base.ExecuteQuery(string.Format(format, code), new SqlParameter[0]);
        }

        public IList<object> GetAllCompanyCode()
        {
            string sql = "SELECT  ItemCode  FROM  Equ_RepairCompanyInfo";
            return base.ExcuteSql(sql);
        }

        public DataTable GetAllParentData(string code)
        {
            string format = "WITH fathers AS(SELECT * FROM Equ_RepairCompanyInfo WHERE ItemCode = '{0}'\r\n                            UNION all SELECT Equ_RepairCompanyInfo.*\r\n                                      FROM fathers, Equ_RepairCompanyInfo\r\n                                      WHERE fathers.ParentCode = Equ_RepairCompanyInfo.ItemCode)\r\n                           SELECT * FROM fathers WHERE ItemCode <> '{0}'";
            return base.ExecuteQuery(string.Format(format, code), new SqlParameter[0]);
        }

        public EquRepairCompanyInfo GetById(string code)
        {
            return (from er in this
                where er.ItemCode.Equals(code)
                select er).FirstOrDefault<EquRepairCompanyInfo>();
        }
    }
}

