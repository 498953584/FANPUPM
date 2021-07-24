namespace cn.justwin.salaryBLL
{
    using cn.justwin.BLL;
    using cn.justwin.salarykDAL;
    using cn.justwin.salaryModel;
    using cn.justwin.stockBLL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class SetSalaryBll
    {
        private SetSalary setSalary = new SetSalary();

        public int Add(SqlTransaction trans, SetSalaryModel model)
        {
            return this.setSalary.Add(trans, model);
        }

        public int Delete(string ID)
        {
            return this.setSalary.Delete(ID);
        }

        public int DeleteByBmdm(SqlTransaction trans, string bmdm)
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
            return this.setSalary.DeleteByUserCode(trans, userCode);
        }

        public DataTable GetFieldAndRemark()
        {
            DataView defaultView = Common2.GetFieldAndRemark("Sam_SetSalary").DefaultView;
            defaultView.RowFilter = "name IN ('DayLaborersPrice','BasicWage','PhoneSubsidies','LivingSubsidy')";
            return defaultView.ToTable();
        }

        public List<SetSalaryModel> GetListArray(string strWhere)
        {
            return this.setSalary.GetListArray(strWhere);
        }

        public SetSalaryModel GetModel(string ID)
        {
            return this.setSalary.GetModel(ID);
        }

        public SetSalaryModel GetModelByUserCode(string userCode)
        {
            return this.setSalary.GetModelByUserCode(userCode);
        }

        public DataTable GetTableByBmdm(string bmdm)
        {
            return this.setSalary.GetTableByBmdm(bmdm);
        }

        public int Update(SetSalaryModel model)
        {
            return this.setSalary.Update(model);
        }
    }
}

