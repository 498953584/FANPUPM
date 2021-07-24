namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class IncometInvoice
    {
        public int Add(IncometInvoiceModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Incomet_Invoice(");
            builder.Append("InvoiceID,ContractID,InvoiceNo,InvoiceCode,InvoiceType,TaxNo,BankCode,Project,Contact,InvoiceDate,Amount,Transactor,Annex,FlowState,Payer,Payee,InputPerson,InputDate,Notes,OrganizationCode)");
            builder.Append(" values (");
            builder.Append("@InvoiceID,@ContractID,@InvoiceNo,@InvoiceCode,@InvoiceType,@TaxNo,@BankCode,@Project,@Contact,@InvoiceDate,@Amount,@Transactor,@Annex,@FlowState,@Payer,@Payee,@InputPerson,@InputDate,@Notes,@OrganizationCode)");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@InvoiceID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@InvoiceNo", SqlDbType.NVarChar, 0x40), new SqlParameter("@InvoiceCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@InvoiceType", SqlDbType.NVarChar, 0x40), new SqlParameter("@TaxNo", SqlDbType.NVarChar, 0x40), new SqlParameter("@BankCode", SqlDbType.NVarChar, 500), new SqlParameter("@Project", SqlDbType.NVarChar, 0x40), new SqlParameter("@Contact", SqlDbType.NVarChar, 500), new SqlParameter("@InvoiceDate", SqlDbType.DateTime), new SqlParameter("@Amount", SqlDbType.Decimal, 9), new SqlParameter("@Transactor", SqlDbType.NVarChar, 0x40), new SqlParameter("@Annex", SqlDbType.NVarChar, 0x40), new SqlParameter("@FlowState", SqlDbType.Int, 4), new SqlParameter("@Payer", SqlDbType.NVarChar, 0x40), new SqlParameter("@Payee", SqlDbType.NVarChar, 0x40), 
                new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@Notes", SqlDbType.NVarChar), new SqlParameter("@OrganizationCode", SqlDbType.NVarChar, 500)
             };
            commandParameters[0].Value = model.InvoiceID;
            commandParameters[1].Value = model.ContractID;
            commandParameters[2].Value = model.InvoiceNo;
            commandParameters[3].Value = model.InvoiceCode;
            commandParameters[4].Value = model.InvoiceType;
            commandParameters[5].Value = model.TaxNo;
            commandParameters[6].Value = model.BankCode;
            commandParameters[7].Value = model.Project;
            commandParameters[8].Value = model.Contact;
            commandParameters[9].Value = model.InvoiceDate;
            commandParameters[10].Value = model.Amount;
            commandParameters[11].Value = model.Transactor;
            commandParameters[12].Value = model.Annex;
            commandParameters[13].Value = model.FlowState;
            commandParameters[14].Value = model.Payer;
            commandParameters[15].Value = model.Payee;
            commandParameters[0x10].Value = model.InputPerson;
            commandParameters[0x11].Value = model.InputDate;
            commandParameters[0x12].Value = model.Notes;
            commandParameters[0x13].Value = model.OrganizationCode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string InvoiceID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Incomet_Invoice ");
            builder.Append(" where InvoiceID=@InvoiceID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@InvoiceID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = InvoiceID;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<IncometInvoiceModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select InvoiceID,ContractID,InvoiceNo,InvoiceCode,InvoiceType,TaxNo,BankCode,Project,Contact,InvoiceDate,Amount,Transactor,Annex,FlowState,Payer,Payee,InputPerson,InputDate,Notes,OrganizationCode ");
            builder.Append(" FROM Con_Incomet_Invoice ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<IncometInvoiceModel> list = new List<IncometInvoiceModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public IncometInvoiceModel GetModel(string InvoiceID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select InvoiceID,ContractID,InvoiceNo,InvoiceCode,InvoiceType,TaxNo,BankCode,Project,Contact,InvoiceDate,Amount,Transactor,Annex,FlowState,Payer,Payee,InputPerson,InputDate,Notes,OrganizationCode from Con_Incomet_Invoice ");
            builder.Append(" where InvoiceID=@InvoiceID ");
            IncometInvoiceModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@InvoiceID", InvoiceID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public IncometInvoiceModel ReaderBind(IDataReader dataReader)
        {
            IncometInvoiceModel model = new IncometInvoiceModel {
                InvoiceID = dataReader["InvoiceID"].ToString(),
                ContractID = dataReader["ContractID"].ToString(),
                InvoiceNo = dataReader["InvoiceNo"].ToString(),
                InvoiceCode = dataReader["InvoiceCode"].ToString(),
                InvoiceType = dataReader["InvoiceType"].ToString(),
                TaxNo = dataReader["TaxNo"].ToString(),
                BankCode = dataReader["BankCode"].ToString(),
                Project = dataReader["Project"].ToString(),
                Contact = dataReader["Contact"].ToString()
            };
            object obj2 = dataReader["InvoiceDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.InvoiceDate = new DateTime?((DateTime) obj2);
            }
            obj2 = dataReader["Amount"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Amount = new decimal?((decimal) obj2);
            }
            model.Transactor = dataReader["Transactor"].ToString();
            model.Annex = dataReader["Annex"].ToString();
            obj2 = dataReader["FlowState"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.FlowState = new int?((int) obj2);
            }
            model.Payer = dataReader["Payer"].ToString();
            model.Payee = dataReader["Payee"].ToString();
            model.InputPerson = dataReader["InputPerson"].ToString();
            obj2 = dataReader["InputDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.InputDate = new DateTime?((DateTime) obj2);
            }
            model.Notes = dataReader["Notes"].ToString();
            model.OrganizationCode = dataReader["OrganizationCode"].ToString();
            return model;
        }

        public int Update(IncometInvoiceModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_Incomet_Invoice set ");
            builder.Append("ContractID=@ContractID,");
            builder.Append("InvoiceNo=@InvoiceNo,");
            builder.Append("InvoiceCode=@InvoiceCode,");
            builder.Append("InvoiceType=@InvoiceType,");
            builder.Append("TaxNo=@TaxNo,");
            builder.Append("BankCode=@BankCode,");
            builder.Append("Project=@Project,");
            builder.Append("Contact=@Contact,");
            builder.Append("InvoiceDate=@InvoiceDate,");
            builder.Append("Amount=@Amount,");
            builder.Append("Transactor=@Transactor,");
            builder.Append("Annex=@Annex,");
            builder.Append("FlowState=@FlowState,");
            builder.Append("Payer=@Payer,");
            builder.Append("Payee=@Payee,");
            builder.Append("InputPerson=@InputPerson,");
            builder.Append("InputDate=@InputDate,");
            builder.Append("Notes=@Notes,");
            builder.Append("OrganizationCode=@OrganizationCode");
            builder.Append(" where InvoiceID=@InvoiceID ");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@InvoiceID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@InvoiceNo", SqlDbType.NVarChar, 0x40), new SqlParameter("@InvoiceCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@InvoiceType", SqlDbType.NVarChar, 0x40), new SqlParameter("@TaxNo", SqlDbType.NVarChar, 0x40), new SqlParameter("@BankCode", SqlDbType.NVarChar, 500), new SqlParameter("@Project", SqlDbType.NVarChar, 0x40), new SqlParameter("@Contact", SqlDbType.NVarChar, 500), new SqlParameter("@InvoiceDate", SqlDbType.DateTime), new SqlParameter("@Amount", SqlDbType.Decimal, 9), new SqlParameter("@Transactor", SqlDbType.NVarChar, 0x40), new SqlParameter("@Annex", SqlDbType.NVarChar, 0x40), new SqlParameter("@FlowState", SqlDbType.Int, 4), new SqlParameter("@Payer", SqlDbType.NVarChar, 0x40), new SqlParameter("@Payee", SqlDbType.NVarChar, 0x40), 
                new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@Notes", SqlDbType.NVarChar), new SqlParameter("@OrganizationCode", SqlDbType.NVarChar, 500)
             };
            commandParameters[0].Value = model.InvoiceID;
            commandParameters[1].Value = model.ContractID;
            commandParameters[2].Value = model.InvoiceNo;
            commandParameters[3].Value = model.InvoiceCode;
            commandParameters[4].Value = model.InvoiceType;
            commandParameters[5].Value = model.TaxNo;
            commandParameters[6].Value = model.BankCode;
            commandParameters[7].Value = model.Project;
            commandParameters[8].Value = model.Contact;
            commandParameters[9].Value = model.InvoiceDate;
            commandParameters[10].Value = model.Amount;
            commandParameters[11].Value = model.Transactor;
            commandParameters[12].Value = model.Annex;
            commandParameters[13].Value = model.FlowState;
            commandParameters[14].Value = model.Payer;
            commandParameters[15].Value = model.Payee;
            commandParameters[0x10].Value = model.InputPerson;
            commandParameters[0x11].Value = model.InputDate;
            commandParameters[0x12].Value = model.Notes;
            commandParameters[0x13].Value = model.OrganizationCode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

