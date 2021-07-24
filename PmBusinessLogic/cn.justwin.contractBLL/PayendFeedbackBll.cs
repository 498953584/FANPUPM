namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class PayendFeedbackBll
    {
        private PayendFeedback payendFeedback = new PayendFeedback();

        public int Add(PayendFeedbackModel model)
        {
            return this.payendFeedback.Add(model);
        }

        public int Delete(SqlTransaction trans, string ID)
        {
            return this.payendFeedback.Delete(trans, ID);
        }

        public List<PayendFeedbackModel> GetListArray(string strWhere)
        {
            return this.payendFeedback.GetListArray(strWhere);
        }

        public PayendFeedbackModel GetModel(string ID)
        {
            return this.payendFeedback.GetModel(ID);
        }

        public int Update(PayendFeedbackModel model)
        {
            return this.payendFeedback.Update(model);
        }
    }
}

