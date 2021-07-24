namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class RedeployDetail
    {
        public bool Add(com.jwsoft.pm.entpm.model.RedeployDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_RedeployDetail(");
            builder.Append("Redeployid,FoldDepositoryID,MaterialId,Scalar,UnitPrice");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.Redeployid + ",");
            builder.Append(model.FoldDepositoryID + ",");
            builder.Append(model.MaterialId + ",");
            builder.Append(model.Scalar + ",");
            builder.Append(model.UnitPrice);
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(int RedeployDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_RedeployDetail ");
            builder.Append(" where RedeployDetailID=" + RedeployDetailID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Exists(int RedeployDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_RedeployDetail");
            builder.Append(" where RedeployDetailID=" + RedeployDetailID + " ");
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
            builder.Append("select RedeployDetailID,Redeployid,FoldDepositoryID,MaterialId,Scalar,UnitPrice ");
            builder.Append(" FROM pm_Repe_RedeployDetail ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_RedeployDetail", "RedeployDetailID"));
        }

        public com.jwsoft.pm.entpm.model.RedeployDetail GetModel(int RedeployDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" RedeployDetailID,Redeployid,FoldDepositoryID,MaterialId,Scalar,UnitPrice ");
            builder.Append(" from pm_Repe_RedeployDetail ");
            builder.Append(" where RedeployDetailID=" + RedeployDetailID + " ");
            com.jwsoft.pm.entpm.model.RedeployDetail detail = new com.jwsoft.pm.entpm.model.RedeployDetail();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RedeployDetailID"].ToString() != "")
            {
                detail.RedeployDetailID = int.Parse(set.Tables[0].Rows[0]["RedeployDetailID"].ToString());
            }
            if (set.Tables[0].Rows[0]["Redeployid"].ToString() != "")
            {
                detail.Redeployid = int.Parse(set.Tables[0].Rows[0]["Redeployid"].ToString());
            }
            if (set.Tables[0].Rows[0]["FoldDepositoryID"].ToString() != "")
            {
                detail.FoldDepositoryID = int.Parse(set.Tables[0].Rows[0]["FoldDepositoryID"].ToString());
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

        public bool Update(com.jwsoft.pm.entpm.model.RedeployDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_RedeployDetail set ");
            builder.Append("Redeployid=" + model.Redeployid + ",");
            builder.Append("FoldDepositoryID=" + model.FoldDepositoryID + ",");
            builder.Append("MaterialId=" + model.MaterialId + ",");
            builder.Append("Scalar=" + model.Scalar + ",");
            builder.Append("UnitPrice=" + model.UnitPrice);
            builder.Append(" where RedeployDetailID=" + model.RedeployDetailID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

