namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OABooksDiscardMainAction
    {
        public int Add(OABooksDiscardMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" insert into OA_Books_DiscardMain( ");
            builder.Append(" RecordID,AuditState,LibraryCode,UserCode,RecordDate,Remark,IsConfirm ");
            builder.Append(" ) ");
            builder.Append(" values ( ");
            builder.Append(" '" + model.RecordID + "', ");
            builder.Append(" '" + model.AuditState + "', ");
            builder.Append(" '" + model.LibraryCode + "', ");
            builder.Append(" '" + model.UserCode + "', ");
            builder.Append(" '" + model.RecordDate + "', ");
            builder.Append(" '" + model.Remark + "', ");
            builder.Append(" '" + model.IsConfirm + "' ");
            builder.Append(" ) ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_Books_Discard ");
            builder.Append(" where DiscardRecordID='" + RecordID + "' ");
            builder.Append("delete OA_Books_DiscardMain ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 RecordID from OA_Books_DiscardMain where RecordID=" + RecordID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,AuditState,LibraryCode,UserCode,RecordDate,Remark,IsConfirm ");
            builder.Append(" FROM OA_Books_DiscardMain ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_Books_DiscardMain", "RecordID");
        }

        public OABooksDiscardMain GetModel(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" RecordID,AuditState,UserCode,RecordDate,Remark,IsConfirm ");
            builder.Append(" from OA_Books_DiscardMain ");
            builder.Append(" where RecordID=" + RecordID);
            OABooksDiscardMain main = new OABooksDiscardMain();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                main.RecordID = new Guid(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["AuditState"].ToString() != "")
            {
                main.AuditState = int.Parse(set.Tables[0].Rows[0]["AuditState"].ToString());
            }
            main.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                main.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            main.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            main.IsConfirm = set.Tables[0].Rows[0]["IsConfirm"].ToString();
            return main;
        }

        public int Update(OABooksDiscardMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Books_DiscardMain set ");
            builder.Append("LibraryCode='" + model.LibraryCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where RecordID='" + model.RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateIsConfirm(Guid RecordCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Books_DiscardMain set ");
            builder.Append("IsConfirm='1' ");
            builder.Append(" where RecordID='" + RecordCode + "' ");
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(" select * from OA_Books_Discard where DiscardRecordID='" + RecordCode + "' ");
            DataTable table = publicDbOpClass.DataTableQuary(builder2.ToString());
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    builder.Append("update OA_Books_Storage set ");
                    builder.Append("LeaveCopy=LeaveCopy-isnull(" + row["DiscardCopy"].ToString() + ",0) ");
                    builder.Append(" where ISBN='" + row["DiscardBookISBN"].ToString() + "' ");
                }
            }
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

