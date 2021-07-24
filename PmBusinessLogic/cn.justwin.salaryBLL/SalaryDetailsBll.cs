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

    public class SalaryDetailsBll
    {
        private PtYhmcBll ptYhmcBll = new PtYhmcBll();
        private SalaryDetails salaryDetails = new SalaryDetails();

        public int Add(SqlTransaction trans, SalaryDetailsModel model)
        {
            return this.salaryDetails.Add(trans, model);
        }

        public int Delete(string ID)
        {
            return this.salaryDetails.Delete(ID);
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
            return this.salaryDetails.DeleteByUserCode(trans, userCode, month);
        }

        public List<SalaryDetailsModel> GetListArray(string strWhere)
        {
            return this.salaryDetails.GetListArray(strWhere);
        }

        public SalaryDetailsModel GetModel(string ID)
        {
            return this.salaryDetails.GetModel(ID);
        }

        public string GetSalaryCode()
        {
            return this.salaryDetails.CreateSalaryCode();
        }

        public DataTable GetTbByIds(string id)
        {
            return this.salaryDetails.GetTbByIds(id);
        }

        public DataTable GetTbByQuery(string userCode, string currentMonth, string bmdm, string userName)
        {
            return this.salaryDetails.GetTbByQuery(userCode, currentMonth, bmdm, userName);
        }

        public int Update(SalaryDetailsModel model)
        {
            return this.salaryDetails.Update(model);
        }
    }
}

