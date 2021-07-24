namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OABooksApplyMainAction
    {
        public int Add(OABooksApplyMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Books_ApplyMain(");
            builder.Append("RecordID,AuditState,CorpCode,UserCode,RecordDate,SumMoney,Remark,IsInStorage");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RecordID + "',");
            builder.Append(model.AuditState + ",");
            builder.Append("'" + model.CorpCode + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append(model.SumMoney + ",");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.IsInStorage + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete OA_Books_Apply ");
            builder.Append(" where ApplyID='" + RecordID + "' ");
            builder.Append(" delete OA_Books_ApplyMain ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 RecordID from OA_Books_ApplyMain where RecordID=" + RecordID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select RecordID,AuditState,CorpCode,UserCode,RecordDate,(select isnull(sum(Copy*Price),0) from OA_Books_Apply where ApplyID=RecordID) as SumMoney,Remark,IsInStorage ");
            builder.Append(" FROM OA_Books_ApplyMain");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append("  order by RecordDate desc");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_Books_ApplyMain", "RecordID");
        }

        public OABooksApplyMain GetModel(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" RecordID,AuditState,CorpCode,UserCode,RecordDate,SumMoney,Remark,IsInStorage ");
            builder.Append(" from OA_Books_ApplyMain ");
            builder.Append(" where RecordID=" + RecordID);
            OABooksApplyMain main = new OABooksApplyMain();
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
            main.CorpCode = set.Tables[0].Rows[0]["CorpCode"].ToString();
            main.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                main.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["SumMoney"].ToString() != "")
            {
                main.SumMoney = decimal.Parse(set.Tables[0].Rows[0]["SumMoney"].ToString());
            }
            main.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            main.IsInStorage = set.Tables[0].Rows[0]["IsInStorage"].ToString();
            return main;
        }

        public int Update(OABooksApplyMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Books_ApplyMain set ");
            builder.Append(" RecordDate='" + model.RecordDate + "',");
            builder.Append(" Remark='" + model.Remark + "' ");
            builder.Append(" where RecordID='" + model.RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

