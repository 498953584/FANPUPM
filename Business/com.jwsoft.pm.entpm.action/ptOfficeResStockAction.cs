namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class ptOfficeResStockAction
    {
        public int Add(ptOfficeResStock model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_Stock(");
            builder.Append("DepotID,MaterialID,Number,AvgPrice");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.DepotID + ",");
            builder.Append(model.MaterialID + ",");
            builder.Append(model.Number + ",");
            builder.Append(model.AvgPrice);
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_Stock ");
            builder.Append(" where RecordID=" + RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int DepotID, int MaterialID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "select top 1 MaterialID from OA_OfficeRes_Stock where DepotID=", DepotID, " and MaterialID=", MaterialID }));
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select a.RecordID,a.DepotID,a.MaterialID,a.Number,a.AvgPrice,b.TypeCode,b.ResName,b.UseType,b.GetType,b.Unit,b.PlanFee FROM OA_OfficeRes_Stock a join OA_OfficeRes_Resources b on a.MaterialID=b.RecordID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" ORDER BY a.MaterialID asc ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ptOfficeResStock GetModel(int MaterialID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" RecordID,DepotID,MaterialID,Number,AvgPrice ");
            builder.Append(" from OA_OfficeRes_Stock ");
            builder.Append(" where MaterialID=" + MaterialID);
            ptOfficeResStock stock = new ptOfficeResStock();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                stock.RecordID = int.Parse(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["DepotID"].ToString() != "")
            {
                stock.DepotID = int.Parse(set.Tables[0].Rows[0]["DepotID"].ToString());
            }
            if (set.Tables[0].Rows[0]["MaterialID"].ToString() != "")
            {
                stock.MaterialID = int.Parse(set.Tables[0].Rows[0]["MaterialID"].ToString());
            }
            if (set.Tables[0].Rows[0]["Number"].ToString() != "")
            {
                stock.Number = decimal.Parse(set.Tables[0].Rows[0]["Number"].ToString());
            }
            if (set.Tables[0].Rows[0]["AvgPrice"].ToString() != "")
            {
                stock.AvgPrice = decimal.Parse(set.Tables[0].Rows[0]["AvgPrice"].ToString());
            }
            return stock;
        }

        public int Update(ptOfficeResStock model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_Stock set ");
            builder.Append("Number=" + model.Number + ",");
            builder.Append("AvgPrice=" + model.AvgPrice);
            builder.Append(" where DepotID=" + model.DepotID);
            builder.Append(" and MaterialID=" + model.MaterialID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

