namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class IncometBalanceBll
    {
        private IncometBalance incometBalance = new IncometBalance();

        public int Add(IncometBalanceModel model)
        {
            return this.incometBalance.Add(model);
        }

        public int Delete(SqlTransaction trans, string ID)
        {
            return this.incometBalance.Delete(trans, ID);
        }

        public List<IncometBalanceModel> GetListArray(string strWhere)
        {
            return this.incometBalance.GetListArray(strWhere);
        }

        public IncometBalanceModel GetModel(string ID)
        {
            return this.incometBalance.GetModel(ID);
        }

        public int Update(IncometBalanceModel model)
        {
            return this.incometBalance.Update(model);
        }
    }
}

