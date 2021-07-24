namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;

    public class TreasuryPermitBll
    {
        private TreasuryPermitService treasuryPermitService = new TreasuryPermitService();

        public int AddTreasuryPermit(TreasuryPermit model)
        {
            return this.treasuryPermitService.AddTreasuryPermit(model);
        }

        public int DelTreasuryPermitById(int id)
        {
            return this.treasuryPermitService.DelTreasuryPermitById(id);
        }

        public int DelTreasuryPermitByTcode(string tcode)
        {
            return this.treasuryPermitService.DelTreasuryPermitByTcode(tcode);
        }

        public IList<TreasuryPermit> GetAllTreasuryPermitByWhere(string where)
        {
            return this.treasuryPermitService.GetAllTreasuryPermitByWhere(where);
        }

        public TreasuryPermit GetTreasuryPermitById(int id)
        {
            return this.treasuryPermitService.GetTreasuryPermitById(id);
        }

        public bool IsPermitAccept(string allocCode, string userCode)
        {
            return this.treasuryPermitService.IsPermitAccept(allocCode, userCode);
        }

        public bool IsPermitBool(string tcode, string yhdm)
        {
            return this.treasuryPermitService.IsPermitBool(tcode, yhdm);
        }

        public int UpdateTreasuryPermit(TreasuryPermit model)
        {
            return this.treasuryPermitService.UpdateTreasuryPermit(model);
        }
    }
}

