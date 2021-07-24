namespace cn.justwin.salaryBLL
{
    using cn.justwin.salaryDAL;
    using cn.justwin.salaryModel;
    using cn.justwin.stockBLL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class MonthSalaryBll
    {
        private MonthSalary monthSalary = new MonthSalary();

        public int Add(SqlTransaction trans, MonthSalaryModel model)
        {
            return this.monthSalary.Add(trans, model);
        }

        public int Delete(string Id)
        {
            return this.monthSalary.Delete(Id);
        }

        public int DeleteByBmdm(SqlTransaction trans, string bmdm, string month)
        {
            IList<PtYhmc> allModelByWhere = new PtYhmcBll().GetAllModelByWhere(" where i_bmdm in (select i_bmdm from f_cid('" + bmdm + "'))");
            string userCode = "";
            foreach (PtYhmc yhmc in allModelByWhere)
            {
                userCode = userCode + "'" + yhmc.v_yhdm + "',";
            }
            if (userCode.Length > 1)
            {
                userCode = userCode.Substring(0, userCode.Length - 1);
            }
            else
            {
                userCode = "''";
            }
            return this.monthSalary.DeleteByUserCode(trans, userCode, month);
        }

        public List<MonthSalaryModel> GetListArray(string strWhere)
        {
            return this.monthSalary.GetListArray(strWhere);
        }

        public MonthSalaryModel GetModel(string Id)
        {
            return this.monthSalary.GetModel(Id);
        }

        public DataTable GetSalaryTable(string bmdm, string month)
        {
            return this.monthSalary.GetSalaryTable(bmdm, month);
        }

        public DataTable GetTableByBmdm(string bmdm, string month)
        {
            return this.monthSalary.GetTableByBmdm(bmdm, month);
        }

        public int Update(MonthSalaryModel model)
        {
            return this.monthSalary.Update(model);
        }
    }
}

