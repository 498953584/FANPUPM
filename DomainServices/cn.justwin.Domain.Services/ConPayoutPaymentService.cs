namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Data;

    public class ConPayoutPaymentService : Repository<ConPayoutPayment>
    {
        public DataTable GetDtblByContractCode(string PaymentCode)
        {
            string cmdText = "SELECT * FROM Con_Payout_Payment WHERE PaymentCode like '%" + PaymentCode + "%'";
            return base.ExecuteQuery(cmdText, null);
        }
    }
}

