namespace cn.justwin.stockBLL.Fund.prjReturn
{
    using cn.justwin.Fund.prjReturn;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class prjReturenBLL
    {
        private prjReturnDAL ptDal = new prjReturnDAL();

        public int Add(SqlTransaction trans, PrjReturnModel model)
        {
            return this.ptDal.Add(trans, model);
        }

        public int Delete(SqlTransaction trans, Guid id)
        {
            return this.ptDal.Delete(trans, id);
        }

        public DataTable GetInfoByWhe(string strWhere)
        {
            return this.ptDal.GetInfoByWhe(strWhere);
        }

        public DataTable GetInfoByWhe(string strWhere, bool isvall)
        {
            return this.ptDal.GetInfoByWhe(strWhere, true);
        }

        public List<PrjReturnModel> GetList(string strWhere)
        {
            return this.ptDal.GetList(strWhere);
        }

        public DataTable GetLoadInfo(string userCode, string sql, string AccounID)
        {
            return this.ptDal.GetLoadInfo(userCode, sql, AccounID);
        }

        public DataTable GetLoanReturnInfo(string selWhere)
        {
            return this.ptDal.GetLoanReturnInfo(selWhere);
        }

        public PrjReturnModel GetModelById(Guid FRid)
        {
            return this.ptDal.GetModel(FRid);
        }

        public DataTable GetSumByLoanid(Guid id)
        {
            return this.ptDal.GetSumByLoanid(id);
        }

        public int Update(SqlTransaction trans, PrjReturnModel returnModel)
        {
            return this.ptDal.Update(trans, returnModel);
        }
    }
}

