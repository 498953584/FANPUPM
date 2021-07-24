namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class IEPInfoBLL
    {
        private IEPInfo IEPInfoDal = new IEPInfo();

        public int Add(IEPInfoModel model)
        {
            SqlTransaction trans = null;
            return this.IEPInfoDal.Add(trans, model);
        }

        public int Del(SqlTransaction trans, string id)
        {
            return this.IEPInfoDal.Delete(trans, id);
        }

        public int DelByIEPid(SqlTransaction trans, string IEPid)
        {
            return this.IEPInfoDal.DelByIEPid(trans, IEPid);
        }

        public DataTable GetList(string IEPId)
        {
            string strWhere = " IEPid='" + IEPId + "'";
            return this.IEPInfoDal.GetList(strWhere);
        }

        public IEPInfoModel GetModel(string Id)
        {
            return this.IEPInfoDal.GetModel(Id);
        }

        public int Update(IEPInfoModel model)
        {
            SqlTransaction trans = null;
            return this.IEPInfoDal.Update(trans, model);
        }
    }
}

