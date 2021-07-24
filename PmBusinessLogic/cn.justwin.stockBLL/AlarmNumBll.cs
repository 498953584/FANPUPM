namespace cn.justwin.stockBll
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class AlarmNumBll
    {
        private readonly AlarmNum dal = new AlarmNum();

        public int Add(AlarmNumModel model)
        {
            return this.dal.Add(model);
        }

        public object AlarmMethod(string tcode, string scode)
        {
            return this.dal.AlarmMethod(tcode, scode);
        }

        public int Delete(string scode, string tcode)
        {
            return this.dal.Delete(scode, tcode);
        }

        public List<AlarmNumModel> GetListArray(string strWhere)
        {
            return this.dal.GetListArray(strWhere);
        }

        public AlarmNumModel GetModel(string scode, string tcode)
        {
            return this.dal.GetModel(scode, tcode);
        }

        public DataTable GetTableList(string strWhere)
        {
            return this.dal.GetTableList(strWhere);
        }

        public int Update(AlarmNumModel model)
        {
            return this.dal.Update(model);
        }

        public int UpdateNum(string scode, string tcode, decimal AlarmNum)
        {
            return this.dal.UpdateNum(scode, tcode, AlarmNum);
        }
    }
}

