namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;

    public class PtdutyBll
    {
        private PtdutyService ptdutyService = new PtdutyService();

        public int AddPtduty(Ptduty model)
        {
            return this.ptdutyService.AddPtduty(model);
        }

        public int Delete(int I_DUTYID)
        {
            return this.ptdutyService.Delete(I_DUTYID);
        }

        public IList<Ptduty> GetAllPtdutyByWhere(string where)
        {
            return this.ptdutyService.GetAllPtdutyByWhere(where);
        }

        public Ptduty GetPtDutyById(int I_DUTYID)
        {
            return this.ptdutyService.GetPtDutyById(I_DUTYID);
        }

        public int Update(Ptduty model)
        {
            return this.ptdutyService.Update(model);
        }
    }
}

