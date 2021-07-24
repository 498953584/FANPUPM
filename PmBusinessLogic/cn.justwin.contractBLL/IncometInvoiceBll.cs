namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class IncometInvoiceBll
    {
        private IncometInvoice incometInvoice = new IncometInvoice();

        public int Add(IncometInvoiceModel model)
        {
            return this.incometInvoice.Add(model);
        }

        public int Delete(SqlTransaction trans, string InvoiceID)
        {
            return this.incometInvoice.Delete(trans, InvoiceID);
        }

        public List<IncometInvoiceModel> GetListArray(string strWhere)
        {
            return this.incometInvoice.GetListArray(strWhere);
        }

        public IncometInvoiceModel GetModel(string InvoiceID)
        {
            return this.incometInvoice.GetModel(InvoiceID);
        }

        public int Update(IncometInvoiceModel model)
        {
            return this.incometInvoice.Update(model);
        }
    }
}

