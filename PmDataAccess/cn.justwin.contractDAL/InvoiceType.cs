namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class InvoiceType
    {
        public List<InvoiceTypeInfo> GetInvoiceType()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT NoteID,CodeName ");
            builder.Append("FROM XPM_Basic_CodeList ");
            builder.Append("JOIN XPM_Basic_CodeType ON XPM_Basic_CodeType.TypeID = XPM_Basic_CodeList.TypeID ");
            builder.Append("WHERE XPM_Basic_CodeType.SignCode = 'InvoiceType' ");
            builder.Append("\tAND XPM_Basic_CodeList.IsValid = 1");
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        private List<InvoiceTypeInfo> GetList(IDataReader dr)
        {
            List<InvoiceTypeInfo> list = new List<InvoiceTypeInfo>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        private InvoiceTypeInfo GetModel(IDataReader dr)
        {
            return new InvoiceTypeInfo { NoteID = DBHelper.GetString(dr["NoteID"]), CodeName = DBHelper.GetString(dr["CodeName"]) };
        }
    }
}

