namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;

    public class PTbdmBll
    {
        private PTbdmService ptbdmService = new PTbdmService();

        public int AddPTbdm(PTbdm model)
        {
            return this.ptbdmService.AddPTbdm(model);
        }

        public int Delete(int i_bmdm)
        {
            return this.ptbdmService.Delete(i_bmdm);
        }

        public PTbdm GetPTbdmById(int i_bmdm)
        {
            return this.ptbdmService.GetPTbdmById(i_bmdm);
        }

        public IList<PTbdm> GetPTbdmByWhere(string where)
        {
            return this.ptbdmService.GetPTbdmByWhere(where);
        }

        public int UpdatePTdbm(PTbdm model)
        {
            return this.ptbdmService.UpdatePTdbm(model);
        }
    }
}

