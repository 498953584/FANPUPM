namespace cn.justwin.epm.prog
{
    using System;

    public class progLogic
    {
        private progService dal = new progService();

        public bool Exists(int ProgSortCode)
        {
            return this.dal.Exists(ProgSortCode);
        }

        public bool Exists(string ProgSortName)
        {
            return this.dal.Exists(ProgSortName);
        }
    }
}

