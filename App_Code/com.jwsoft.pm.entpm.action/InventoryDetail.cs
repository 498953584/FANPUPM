namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class InventoryDetail
    {
        public bool Add(com.jwsoft.pm.entpm.model.InventoryDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_InventoryDetail(");
            builder.Append("InventoryID,MaterialId,UnitPrice,BookScalar,FactScalar");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.InventoryID + ",");
            builder.Append(model.MaterialId + ",");
            builder.Append(model.UnitPrice + ",");
            builder.Append(model.BookScalar + ",");
            builder.Append(model.FactScalar);
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(int InventoryDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_InventoryDetail ");
            builder.Append(" where InventoryDetailID=" + InventoryDetailID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Exists(int InventoryDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_InventoryDetail");
            builder.Append(" where InventoryDetailID=" + InventoryDetailID + " ");
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
            builder.Append("select InventoryDetailID,InventoryID,MaterialId,UnitPrice,BookScalar,FactScalar ");
            builder.Append(" FROM pm_Repe_InventoryDetail ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_InventoryDetail", "InventoryDetailID"));
        }

        public com.jwsoft.pm.entpm.model.InventoryDetail GetModel(int InventoryDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" InventoryDetailID,InventoryID,MaterialId,UnitPrice,BookScalar,FactScalar ");
            builder.Append(" from pm_Repe_InventoryDetail ");
            builder.Append(" where InventoryDetailID=" + InventoryDetailID + " ");
            com.jwsoft.pm.entpm.model.InventoryDetail detail = new com.jwsoft.pm.entpm.model.InventoryDetail();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["InventoryDetailID"].ToString() != "")
            {
                detail.InventoryDetailID = int.Parse(set.Tables[0].Rows[0]["InventoryDetailID"].ToString());
            }
            if (set.Tables[0].Rows[0]["InventoryID"].ToString() != "")
            {
                detail.InventoryID = int.Parse(set.Tables[0].Rows[0]["InventoryID"].ToString());
            }
            if (set.Tables[0].Rows[0]["MaterialId"].ToString() != "")
            {
                detail.MaterialId = int.Parse(set.Tables[0].Rows[0]["MaterialId"].ToString());
            }
            if (set.Tables[0].Rows[0]["UnitPrice"].ToString() != "")
            {
                detail.UnitPrice = decimal.Parse(set.Tables[0].Rows[0]["UnitPrice"].ToString());
            }
            if (set.Tables[0].Rows[0]["BookScalar"].ToString() != "")
            {
                detail.BookScalar = decimal.Parse(set.Tables[0].Rows[0]["BookScalar"].ToString());
            }
            if (set.Tables[0].Rows[0]["FactScalar"].ToString() != "")
            {
                detail.FactScalar = decimal.Parse(set.Tables[0].Rows[0]["FactScalar"].ToString());
            }
            return detail;
        }

        public bool InitDetailByDepoID(int inventoryID)
        {
            com.jwsoft.pm.entpm.action.InventoryMain main = new com.jwsoft.pm.entpm.action.InventoryMain();
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            com.jwsoft.pm.entpm.model.InventoryMain model = new com.jwsoft.pm.entpm.model.InventoryMain();
            model = main.GetModel(inventoryID);
            builder.Append(" select * from pm_Repe_RealTime where DepositoryID=" + model.DepositoryID + " ");
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count <= 0)
            {
                return false;
            }
            foreach (DataRow row in table.Rows)
            {
                builder2.Append(string.Concat(new object[] { " if not exists(select top 1 InventoryDetailID from pm_Repe_InventoryDetail where InventoryID= ", model.InventoryID, " and MaterialId = ", row["MaterialId"].ToString(), " ) " }));
                builder2.Append(" begin ");
                builder2.Append(" insert into pm_Repe_InventoryDetail(");
                builder2.Append(" InventoryID,MaterialId,BookScalar,FactScalar");
                builder2.Append(" )");
                builder2.Append(" values (");
                builder2.Append(" " + model.InventoryID + ",");
                builder2.Append(" " + row["MaterialId"].ToString() + ",");
                builder2.Append(" " + row["Amount"].ToString() + ",");
                builder2.Append(" " + row["Amount"].ToString() + " ");
                builder2.Append(" )");
                builder2.Append(" end ");
                builder2.Append(" else ");
                builder2.Append(" begin ");
                builder2.Append(" update pm_Repe_InventoryDetail set ");
                builder2.Append(" BookScalar=" + row["Amount"].ToString() + ",");
                builder2.Append(" FactScalar=" + row["Amount"].ToString());
                builder2.Append(" where ");
                builder2.Append(" InventoryID=" + model.InventoryID);
                builder2.Append(" and MaterialId = " + row["MaterialId"].ToString() + " ");
                builder2.Append(" end ");
            }
            return publicDbOpClass.NonQuerySqlString(builder2.ToString());
        }

        public bool Update(com.jwsoft.pm.entpm.model.InventoryDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_InventoryDetail set ");
            builder.Append("InventoryID=" + model.InventoryID + ",");
            builder.Append("MaterialId=" + model.MaterialId + ",");
            builder.Append("UnitPrice=" + model.UnitPrice + ",");
            builder.Append("BookScalar=" + model.BookScalar + ",");
            builder.Append("FactScalar=" + model.FactScalar);
            builder.Append(" where InventoryDetailID=" + model.InventoryDetailID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

