namespace cn.justwin.stockBLL.PTPrjInfo
{
    using cn.justwin.stockDAL;
    using System;
    using System.Data;

    public class PTPrjInfoZTBBll
    {
        private PrjInfo prjInfo = new PrjInfo();

        public void Add(string _PrjGuid, string _grade, string _businessman, string _telephone)
        {
            this.prjInfo.Add(_PrjGuid, _grade, _businessman, _telephone);
        }

        public DataTable getDataTable(string PrjGuid)
        {
            return this.prjInfo.getDataTable(PrjGuid, "PT_PrjInfo_ZTB_Stock");
        }

        public int update(string PrjGuid, string grade, string businessman, string telephone)
        {
            return this.prjInfo.update(PrjGuid, grade, businessman, telephone, "PT_PrjInfo_ZTB_Stock");
        }
    }
}

