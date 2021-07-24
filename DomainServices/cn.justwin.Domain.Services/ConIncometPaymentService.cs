namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Data;

    public class ConIncometPaymentService : Repository<ConIncometPayment>
    {
        public DataTable GetDtblByContractCode(string CllectionCode)
        {
            string cmdText = "SELECT * FROM Con_Incomet_Payment WHERE CllectionCode like '%" + CllectionCode + "%'";
            return base.ExecuteQuery(cmdText, null);
        }
    }
}

