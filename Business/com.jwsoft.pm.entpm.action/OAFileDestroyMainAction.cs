namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAFileDestroyMainAction
    {
        public int Add(OAFileDestroyMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_File_DestroyMain(");
            builder.Append("RecordID,LibraryCode,AuditState,UserCode,RecordDate,Remark,IsConfirm");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RecordID + "',");
            builder.Append("'" + model.LibraryCode + "',");
            builder.Append(model.AuditState + ",");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.IsConfirm + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete OA_File_Destroy ");
            builder.Append(" where DestroyRecordID='" + RecordID + "' ");
            builder.Append(" delete OA_File_DestroyMain ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_File_DestroyMain where RecordID=" + RecordID);
            if (publicDbOpClass.ExecuteScalar(builder.ToString()) == DBNull.Value)
            {
                return false;
            }
            return true;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,LibraryCode,AuditState,UserCode,RecordDate,Remark,IsConfirm ");
            builder.Append(" FROM OA_File_DestroyMain ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_File_DestroyMain", "RecordID");
        }

        public OAFileDestroyMain GetModel(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" RecordID,LibraryCode,AuditState,UserCode,RecordDate,Remark,IsConfirm ");
            builder.Append(" from OA_File_DestroyMain ");
            builder.Append(" where RecordID=" + RecordID);
            OAFileDestroyMain main = new OAFileDestroyMain();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                main.RecordID = new Guid(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            main.LibraryCode = set.Tables[0].Rows[0]["LibraryCode"].ToString();
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

        public int Update(OAFileDestroyMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_File_DestroyMain set ");
            builder.Append("LibraryCode='" + model.LibraryCode + "',");
            builder.Append("AuditState=" + model.AuditState + ",");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("IsConfirm='" + model.IsConfirm + "'");
            builder.Append(" where RecordID='" + model.RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateIsConfirm(Guid RecordCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_File_DestroyMain set ");
            builder.Append("IsConfirm='1' ");
            builder.Append(" where RecordID='" + RecordCode + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

