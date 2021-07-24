namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class SpoilageDetail
    {
        public bool Add(com.jwsoft.pm.entpm.model.SpoilageDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_SpoilageDetail(");
            builder.Append("SpoilageID,MaterialId,Scalar,UnitPrice");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.SpoilageID + ",");
            builder.Append(model.MaterialId + ",");
            builder.Append(model.Scalar + ",");
            builder.Append(model.UnitPrice);
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(int SpoilageDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_SpoilageDetail ");
            builder.Append(" where SpoilageDetailID=" + SpoilageDetailID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Exists(int SpoilageDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_SpoilageDetail");
            builder.Append(" where SpoilageDetailID=" + SpoilageDetailID + " ");
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
            builder.Append("select SpoilageDetailID,SpoilageID,MaterialId,Scalar,UnitPrice ");
            builder.Append(" FROM pm_Repe_SpoilageDetail ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_SpoilageDetail", "SpoilageDetailID"));
        }

        public com.jwsoft.pm.entpm.model.SpoilageDetail GetModel(int SpoilageDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" SpoilageDetailID,SpoilageID,MaterialId,Scalar,UnitPrice ");
            builder.Append(" from pm_Repe_SpoilageDetail ");
            builder.Append(" where SpoilageDetailID=" + SpoilageDetailID + " ");
            com.jwsoft.pm.entpm.model.SpoilageDetail detail = new com.jwsoft.pm.entpm.model.SpoilageDetail();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["SpoilageDetailID"].ToString() != "")
            {
                detail.SpoilageDetailID = int.Parse(set.Tables[0].Rows[0]["SpoilageDetailID"].ToString());
            }
            if (set.Tables[0].Rows[0]["SpoilageID"].ToString() != "")
            {
                detail.SpoilageID = int.Parse(set.Tables[0].Rows[0]["SpoilageID"].ToString());
            }
            if (set.Tables[0].Rows[0]["MaterialId"].ToString() != "")
            {
                detail.MaterialId = int.Parse(set.Tables[0].Rows[0]["MaterialId"].ToString());
            }
            if (set.Tables[0].Rows[0]["Scalar"].ToString() != "")
            {
                detail.Scalar = decimal.Parse(set.Tables[0].Rows[0]["Scalar"].ToString());
            }
            if (set.Tables[0].Rows[0]["UnitPrice"].ToString() != "")
            {
                detail.UnitPrice = decimal.Parse(set.Tables[0].Rows[0]["UnitPrice"].ToString());
            }
            return detail;
        }

        public bool Update(com.jwsoft.pm.entpm.model.SpoilageDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_SpoilageDetail set ");
            builder.Append("SpoilageID=" + model.SpoilageID + ",");
            builder.Append("MaterialId=" + model.MaterialId + ",");
            builder.Append("Scalar=" + model.Scalar + ",");
            builder.Append("UnitPrice=" + model.UnitPrice);
            builder.Append(" where SpoilageDetailID=" + model.SpoilageDetailID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

