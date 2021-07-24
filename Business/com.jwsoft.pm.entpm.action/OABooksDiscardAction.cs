namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OABooksDiscardAction
    {
        public int Add(OABooksDiscard model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Books_Discard(");
            builder.Append("DiscardRecordID,DiscardBookISBN,DiscardCopy");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.DiscardRecordID + "',");
            builder.Append("'" + model.DiscardBookISBN + "',");
            builder.Append(model.DiscardCopy);
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid DiscardRecordID, string isbn)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_Books_Discard ");
            builder.Append(" where DiscardRecordID='" + DiscardRecordID + "' ");
            builder.Append(" and DiscardBookISBN='" + isbn + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(Guid DiscardRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 DiscardRecordID from OA_Books_Discard where DiscardRecordID=" + DiscardRecordID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select a.DiscardRecordID,a.DiscardBookISBN,a.DiscardCopy,b.BookTitle,b.Author,b.PublishingHouse from OA_Books_Discard a left join OA_Books_Storage b on a.DiscardBookISBN=b.ISBN ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_Books_Discard", "DiscardRecordID");
        }

        public OABooksDiscard GetModel(Guid DiscardRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" DiscardRecordID,DiscardBookISBN,DiscardCopy ");
            builder.Append(" from OA_Books_Discard ");
            builder.Append(" where DiscardRecordID=" + DiscardRecordID);
            OABooksDiscard discard = new OABooksDiscard();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["DiscardRecordID"].ToString() != "")
            {
                discard.DiscardRecordID = new Guid(set.Tables[0].Rows[0]["DiscardRecordID"].ToString());
            }
            discard.DiscardBookISBN = set.Tables[0].Rows[0]["DiscardBookISBN"].ToString();
            if (set.Tables[0].Rows[0]["DiscardCopy"].ToString() != "")
            {
                discard.DiscardCopy = int.Parse(set.Tables[0].Rows[0]["DiscardCopy"].ToString());
            }
            return discard;
        }

        public int Update(OABooksDiscard model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Books_Discard set ");
            builder.Append("DiscardCopy=" + model.DiscardCopy);
            builder.Append(" where DiscardRecordID='" + model.DiscardRecordID + "' ");
            builder.Append(" and DiscardBookISBN='" + model.DiscardBookISBN + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

