namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class eFileAuthorizationAction
    {
        public int Add(eFileAuthorization model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_eFile_Authorization(");
            builder.Append("LeaderCode,FileRecordID,ReaderCodes");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.LeaderCode + "',");
            builder.Append(model.FileRecordID + ",");
            builder.Append("'" + model.ReaderCodes + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string LeaderCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_eFile_Authorization ");
            builder.Append(" where LeaderCode='" + LeaderCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select LeaderCode,FileRecordID,ReaderCodes ");
            builder.Append(" FROM OA_eFile_Authorization ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public eFileAuthorization GetModel(string LeaderCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" LeaderCode,FileRecordID,ReaderCodes ");
            builder.Append(" from OA_eFile_Authorization ");
            builder.Append(" where LeaderCode='" + LeaderCode + "'");
            eFileAuthorization authorization = new eFileAuthorization();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            authorization.LeaderCode = set.Tables[0].Rows[0]["LeaderCode"].ToString();
            if (set.Tables[0].Rows[0]["FileRecordID"].ToString() != "")
            {
                authorization.FileRecordID = int.Parse(set.Tables[0].Rows[0]["FileRecordID"].ToString());
            }
            authorization.ReaderCodes = set.Tables[0].Rows[0]["ReaderCodes"].ToString();
            return authorization;
        }

        public int Update(eFileAuthorization model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_eFile_Authorization set ");
            builder.Append("ReaderCodes='" + model.ReaderCodes + "'");
            builder.Append(" where LeaderCode='" + model.LeaderCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

