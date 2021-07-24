namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class RealTime
    {
        public bool Add(com.jwsoft.pm.entpm.model.RealTime model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_RealTime(");
            builder.Append("DepositoryID,MaterialId,Amount");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.DepositoryID + ",");
            builder.Append(model.MaterialId + ",");
            builder.Append(model.Amount);
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_RealTime ");
            builder.Append(" where RecordID=" + RecordID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Exists(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_RealTime");
            builder.Append(" where RecordID=" + RecordID + " ");
            if (publicDbOpClass.ExecuteSQL(builder.ToString()) <= 0)
            {
                return false;
            }
            return true;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,DepositoryID,MaterialId,Amount ");
            builder.Append(" FROM pm_Repe_RealTime ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_RealTime", "RecordID"));
        }

        public com.jwsoft.pm.entpm.model.RealTime GetModel(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" RecordID,DepositoryID,MaterialId,Amount ");
            builder.Append(" from pm_Repe_RealTime ");
            builder.Append(" where RecordID=" + RecordID + " ");
            com.jwsoft.pm.entpm.model.RealTime time = new com.jwsoft.pm.entpm.model.RealTime();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                time.RecordID = int.Parse(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["DepositoryID"].ToString() != "")
            {
                time.DepositoryID = int.Parse(set.Tables[0].Rows[0]["DepositoryID"].ToString());
            }
            if (set.Tables[0].Rows[0]["MaterialId"].ToString() != "")
            {
                time.MaterialId = int.Parse(set.Tables[0].Rows[0]["MaterialId"].ToString());
            }
            if (set.Tables[0].Rows[0]["Amount"].ToString() != "")
            {
                time.Amount = decimal.Parse(set.Tables[0].Rows[0]["Amount"].ToString());
            }
            return time;
        }

        public bool Update(com.jwsoft.pm.entpm.model.RealTime model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_RealTime set ");
            builder.Append("DepositoryID=" + model.DepositoryID + ",");
            builder.Append("MaterialId=" + model.MaterialId + ",");
            builder.Append("Amount=" + model.Amount);
            builder.Append(" where RecordID=" + model.RecordID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

