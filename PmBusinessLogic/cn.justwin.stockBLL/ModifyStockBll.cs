namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class ModifyStockBll
    {
        private readonly ModifyStock dal = new ModifyStock();

        public void Add(ModifyStockModel model)
        {
            this.dal.Add(null, model);
        }

        public void Add(SqlTransaction trans, ModifyStockModel model)
        {
            this.dal.Add(trans, model);
        }

        public void Delete(SqlTransaction trans, List<string> ids)
        {
            this.dal.Delete(trans, ids);
        }

        public int DeleteByModifyId(SqlTransaction trans, string modifyId)
        {
            return this.dal.DeleteByModifyId(trans, modifyId);
        }

        public DataTable GetInfoByModifyId(string modifyId)
        {
            return this.dal.GetInfoByModifyId(modifyId);
        }

        public DataTable GetModelById(string id)
        {
            return this.dal.GetModelById(id);
        }

        public void Update(SqlTransaction trans, ModifyStockModel model)
        {
            this.dal.Update(trans, model);
        }
    }
}

