namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class SmWastageBll
    {
        private SmWastage smWastage = new SmWastage();

        public int Add(SqlTransaction trans, SmWastageModel model)
        {
            return this.smWastage.Add(trans, model);
        }

        public int Delete(SqlTransaction trans, string wastageCode)
        {
            return this.smWastage.Delete(trans, wastageCode);
        }

        public List<SmWastageModel> GetListArray(string strWhere)
        {
            return this.smWastage.GetListArray(strWhere);
        }

        public DataTable GetListByParm(string wastageCode, string startTime, string endTime, string person, string treasurycode, string strWhere, int pageSize, int pageIndex)
        {
            return this.smWastage.GetListByParm(wastageCode, startTime, endTime, person, treasurycode, strWhere, pageSize, pageIndex);
        }

        public int GetListCountByParm(string wastageCode, string startTime, string endTime, string person, string treasurycode, string strWhere)
        {
            return this.smWastage.GetListCountByParm(wastageCode, startTime, endTime, person, treasurycode, strWhere);
        }

        public SmWastageModel GetModel(string wastageId)
        {
            return this.smWastage.GetModel(wastageId);
        }

        public SmWastageModel GetModelByCode(string wastageCode)
        {
            return this.smWastage.GetModelByCode(wastageCode);
        }

        public SmWastageModel GetModelByIc(string ic)
        {
            return this.smWastage.GetModelByIc(ic);
        }

        public int Update(SqlTransaction trans, SmWastageModel model)
        {
            return this.smWastage.Update(trans, model);
        }
    }
}

