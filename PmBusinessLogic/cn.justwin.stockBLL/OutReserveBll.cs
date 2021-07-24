namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class OutReserveBll
    {
        private OutReserve outReserve = new OutReserve();

        public int Add(SqlTransaction trans, OutReserveModel model)
        {
            return this.outReserve.Add(trans, model);
        }

        public int Delete(SqlTransaction trans, string orcode)
        {
            return this.outReserve.Delete(trans, orcode);
        }

        public List<OutReserveModel> GetListArray(string strWhere)
        {
            return this.outReserve.GetListArray(strWhere);
        }

        public DataTable GetListByParm(string orcode, string startTime, string endTime, string person, string procode, string tcode, string strWhere)
        {
            return this.outReserve.GetListByParm(orcode, startTime, endTime, person, procode, tcode, strWhere);
        }

        public OutReserveModel GetModel(string orcode)
        {
            return this.outReserve.GetModel(orcode);
        }

        public OutReserveModel GetModelByIc(string ic)
        {
            return this.outReserve.GetModelByIc(ic);
        }

        public int Update(SqlTransaction trans, OutReserveModel model)
        {
            return this.outReserve.Update(trans, model);
        }
    }
}

