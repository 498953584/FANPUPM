namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PtYhmcBll
    {
        private PtYhmcService ptYhmcService = new PtYhmcService();

        public int Add(PtYhmc model)
        {
            return this.ptYhmcService.Add(model);
        }

        public int Delete(string v_yhdm)
        {
            return this.ptYhmcService.Delete(v_yhdm);
        }

        public IList<PtYhmc> GetAllModelByWhere(string where)
        {
            return this.ptYhmcService.GetAllModelByWhere(where);
        }

        public PtYhmc GetModelById(string v_yhdm)
        {
            return this.ptYhmcService.GetModelById(v_yhdm);
        }

        public List<string> GetNames(IList<string> codes)
        {
            List<string> list = new List<string>();
            foreach (string str in codes)
            {
                PtYhmc modelById = this.GetModelById(str);
                if (modelById != null)
                {
                    list.Add(modelById.v_xm);
                }
            }
            return list;
        }

        public string GetRTXID(string userCode)
        {
            return this.ptYhmcService.GetRTXID(userCode);
        }

        public DataTable GetSaItems(string saBooksId, int year, int month)
        {
            return this.ptYhmcService.GetSaItems(saBooksId, year, month);
        }

        public DataTable GetSaItems(string saBooksId, int year, int month, string departmentId)
        {
            return this.ptYhmcService.GetSaItems(saBooksId, year, month, departmentId);
        }

        public int GetSaMonthCount(string departmentId, string saBooksId, string userCode, string userName, int year, int month, string isGenerate)
        {
            return this.ptYhmcService.GetSaMonthCount(departmentId, saBooksId, userCode, userName, year, month, isGenerate);
        }

        public int GetSaMonthCountPayoff(string departmentId, string saBooksId, string userCode, string userName, int year, int month, string isPayoff)
        {
            return this.ptYhmcService.GetSaMonthCountPayoff(departmentId, saBooksId, userCode, userName, year, month, isPayoff);
        }

        public int GetSaMonthCountReport(string departmentId, string userCode, string userName, int year, int month, string isPayoff)
        {
            return this.ptYhmcService.GetSaMonthCountReport(departmentId, userCode, userName, year, month, isPayoff);
        }

        public DataTable GetSaMonthInfo(string departmentId, string saBooksId, string userCode, string userName, int year, int month, string isGenerate, int pageIndex, int pageSize)
        {
            return this.ptYhmcService.GetSaMonthInfo(departmentId, saBooksId, userCode, userName, year, month, isGenerate, pageIndex, pageSize);
        }

        public DataTable GetSaMonthInfoPayoff(string departmentId, string saBooksId, string userCode, string userName, int year, int month, string isPayoff, int pageIndex, int pageSize)
        {
            return this.ptYhmcService.GetSaMonthInfoPayoff(departmentId, saBooksId, userCode, userName, year, month, isPayoff, pageIndex, pageSize);
        }

        public DataTable GetSaMonthInfoReport(string departmentId, string userCode, string userName, int year, int month, string isPayoff, int pageIndex, int pageSize)
        {
            return this.ptYhmcService.GetSaMonthInfoReport(departmentId, userCode, userName, year, month, isPayoff, pageIndex, pageSize);
        }

        public DataTable GetUserInfoByDepartBooks(string departmentId, string booksId, int year, int month)
        {
            return this.ptYhmcService.GetUserInfoByDepartBooks(departmentId, booksId, year, month);
        }

        public DataTable GetUserInfoSaBooks(string departmentId, string userCode, string userName, string postName, string isAssignSaBooks, string state, string saBooksId, int pageIndex, int pageSize)
        {
            return this.ptYhmcService.GetUserInfoSaBooks(departmentId, userCode, userName, postName, isAssignSaBooks, state, saBooksId, pageIndex, pageSize);
        }

        public int GetUserSaBooksCount(string departmentId, string userCode, string userName, string postName, string isAssignSaBooks, string state, string saBooksId)
        {
            return this.ptYhmcService.GetUserSaBooksCount(departmentId, userCode, userName, postName, isAssignSaBooks, state, saBooksId);
        }

        public int Update(PtYhmc model)
        {
            return this.ptYhmcService.Update(model);
        }
    }
}

