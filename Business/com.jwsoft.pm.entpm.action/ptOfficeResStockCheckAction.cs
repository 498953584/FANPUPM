namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class ptOfficeResStockCheckAction
    {
        public int Add(ptOfficeResStockCheck model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_StockCheck(");
            builder.Append("DepotID,RecordDate,UserCode,IsConfirm");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.DepotID + ",");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.IsConfirm + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int CheckRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_StockCheck ");
            builder.Append(" where CheckRecordID=" + CheckRecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int CheckRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 CheckRecordID from OA_OfficeRes_StockCheck where CheckRecordID=" + CheckRecordID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CheckRecordID,DepotID,RecordDate,UserCode,IsConfirm ");
            builder.Append(" FROM OA_OfficeRes_StockCheck ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ptOfficeResStockCheck GetModel(int CheckRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" CheckRecordID,DepotID,RecordDate,UserCode,IsConfirm ");
            builder.Append(" from OA_OfficeRes_StockCheck ");
            builder.Append(" where CheckRecordID=" + CheckRecordID);
            ptOfficeResStockCheck check = new ptOfficeResStockCheck();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["CheckRecordID"].ToString() != "")
            {
                check.CheckRecordID = int.Parse(set.Tables[0].Rows[0]["CheckRecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["DepotID"].ToString() != "")
            {
                check.DepotID = int.Parse(set.Tables[0].Rows[0]["DepotID"].ToString());
            }
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                check.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            check.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            check.IsConfirm = set.Tables[0].Rows[0]["IsConfirm"].ToString();
            return check;
        }

        public int Update(ptOfficeResStockCheck model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_StockCheck set ");
            builder.Append("DepotID=" + model.DepotID + ",");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("IsConfirm='" + model.IsConfirm + "'");
            builder.Append(" where CheckRecordID=" + model.CheckRecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

