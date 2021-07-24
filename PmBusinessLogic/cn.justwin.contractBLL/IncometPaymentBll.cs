namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class IncometPaymentBll
    {
        private IncometPayment incometPayment = new IncometPayment();

        public int Add(IncometPaymentModel model)
        {
            return this.incometPayment.Add(model);
        }

        public int Delete(SqlTransaction trans, string ID)
        {
            return this.incometPayment.Delete(trans, ID);
        }

        public string GetCurrentMonthUID(string contractId, bool IsExamineApprove)
        {
            return this.incometPayment.GetCurrentMonthUID(contractId, IsExamineApprove);
        }

        public DataTable GetFundPlan(string contractId, bool IsExamineApprove)
        {
            return this.incometPayment.GetFundPlan(contractId, IsExamineApprove);
        }

        public List<string> GetFundPlanByMonthPlanUID(string monthPlanUID, bool IsExamineApprove)
        {
            return this.incometPayment.GetFundPlanByMonthPlanUID(monthPlanUID, IsExamineApprove);
        }

        public List<IncometPaymentModel> GetListArray(string strWhere)
        {
            return this.incometPayment.GetListArray(strWhere);
        }

        public List<IncometPaymentModel> GetListByWhere(string strWhere)
        {
            return this.incometPayment.GetListByWhere(strWhere);
        }

        public List<IncometPaymentModel> GetListByWhere(string strWhere, string prj, ref List<string> dt)
        {
            return this.incometPayment.GetListByWhere(strWhere, prj, ref dt);
        }

        public IncometPaymentModel GetModel(string ID)
        {
            return this.incometPayment.GetModel(ID);
        }

        public int Update(IncometPaymentModel model)
        {
            return this.incometPayment.Update(model);
        }

        public int UpdateState(SqlTransaction trans, string id)
        {
            return this.incometPayment.UpdateState(trans, id);
        }
    }
}

