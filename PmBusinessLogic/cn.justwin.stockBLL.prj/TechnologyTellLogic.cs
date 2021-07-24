namespace cn.justwin.stockBLL.prj
{
    using cn.justwin.prj;
    using cn.justwin.stockBLL.epm.datum;
    using System;
    using System.Data;

    public class TechnologyTellLogic
    {
        private readonly TechnologyTellService dal = new TechnologyTellService();

        public DataTable GetDataTable(string _where)
        {
            return this.dal.GetDataTable(_where);
        }

        public int GetMaxId()
        {
            DatumLogic logic = new DatumLogic();
            return logic.getMaxID("Prj_TechnologyTell", "MainID");
        }

        public bool Update(int _mark, int _filestype, int id)
        {
            return this.dal.Update(_mark, _filestype, id);
        }
    }
}

