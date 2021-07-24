namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class RefundingBll
    {
        private Refunding refunding = new Refunding();

        public int Add(SqlTransaction trans, RefundingModel model)
        {
            return this.refunding.Add(trans, model);
        }

        public int Delete(SqlTransaction trans, string rcode)
        {
            return this.refunding.Delete(trans, rcode);
        }

        public List<RefundingModel> GetListArray(string strWhere)
        {
            return this.refunding.GetListArray(strWhere);
        }

        public DataTable GetListByTimeAndNum(string startTime, string endTime, string rcode, string person, string procode, string tcode, string strWhere)
        {
            return this.refunding.GetListByTimeAndNum(startTime, endTime, rcode, person, procode, tcode, strWhere);
        }

        public RefundingModel GetModel(string rcode)
        {
            return this.refunding.GetModel(rcode);
        }

        public RefundingModel GetModelByIc(string ic)
        {
            return this.refunding.GetModelByIc(ic);
        }

        public int Update(SqlTransaction trans, RefundingModel model)
        {
            return this.refunding.Update(trans, model);
        }
    }
}

