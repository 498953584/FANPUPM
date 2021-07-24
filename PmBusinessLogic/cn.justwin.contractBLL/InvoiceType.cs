namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;

    public class InvoiceType
    {
        private cn.justwin.contractDAL.InvoiceType invoiceType = new cn.justwin.contractDAL.InvoiceType();

        public List<InvoiceTypeInfo> GetInvoiceType()
        {
            return this.invoiceType.GetInvoiceType();
        }
    }
}

