namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class ptOfficeResStockCheckDetailAction
    {
        public int Add(ArrayList arr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ");
            for (int i = 0; i < arr.Count; i++)
            {
                builder.Append(string.Concat(new object[] { " if not exists(select top 1 CheckRecordID from OA_OfficeRes_StockCheck_Detail where CheckRecordID='", ((ptOfficeResStockCheckDetail) arr[i]).CheckRecordID, "' and MaterialID='", ((ptOfficeResStockCheckDetail) arr[i]).MaterialID, "') " }));
                builder.Append(" begin ");
                builder.Append("insert into OA_OfficeRes_StockCheck_Detail(");
                builder.Append("CheckRecordID,MaterialID,AccountNumber,CheckNumber,Remark");
                builder.Append(")");
                builder.Append(" values (");
                builder.Append(((ptOfficeResStockCheckDetail) arr[i]).CheckRecordID + ",");
                builder.Append(((ptOfficeResStockCheckDetail) arr[i]).MaterialID + ",");
                builder.Append(((ptOfficeResStockCheckDetail) arr[i]).AccountNumber + ",");
                builder.Append(((ptOfficeResStockCheckDetail) arr[i]).CheckNumber + ",");
                builder.Append("'" + ((ptOfficeResStockCheckDetail) arr[i]).Remark + "'");
                builder.Append(")");
                builder.Append(" end ");
                builder.Append(" else ");
                builder.Append(" begin ");
                builder.Append("update OA_OfficeRes_StockCheck_Detail set ");
                builder.Append("AccountNumber=" + ((ptOfficeResStockCheckDetail) arr[i]).AccountNumber + ",");
                builder.Append("CheckNumber=" + ((ptOfficeResStockCheckDetail) arr[i]).CheckNumber + ",");
                builder.Append("Remark='" + ((ptOfficeResStockCheckDetail) arr[i]).Remark + "'");
                builder.Append(string.Concat(new object[] { " where CheckRecordID='", ((ptOfficeResStockCheckDetail) arr[i]).CheckRecordID, "' and MaterialID='", ((ptOfficeResStockCheckDetail) arr[i]).MaterialID, "'" }));
                builder.Append(" end ");
            }
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int CheckDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_StockCheck_Detail ");
            builder.Append(" where CheckDetailID=" + CheckDetailID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int CheckDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 CheckDetailID from OA_OfficeRes_StockCheck_Detail where CheckDetailID=" + CheckDetailID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select a.RecordID,a.DepotID,a.MaterialID,a.Number,a.AvgPrice,b.ResName,b.UseType,b.GetType,b.Unit,b.PlanFee ");
            builder.Append(" FROM OA_OfficeRes_Stock a ");
            builder.Append(" join OA_OfficeRes_Resources b on a.MaterialID=b.RecordID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ptOfficeResStockCheckDetail GetModel(int CheckDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select CheckDetailID,CheckRecordID,MaterialID,AccountNumber,CheckNumber,Remark ");
            builder.Append(" from OA_OfficeRes_StockCheck_Detail ");
            builder.Append(" where CheckDetailID=" + CheckDetailID);
            ptOfficeResStockCheckDetail detail = new ptOfficeResStockCheckDetail();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["CheckDetailID"].ToString() != "")
            {
                detail.CheckDetailID = int.Parse(set.Tables[0].Rows[0]["CheckDetailID"].ToString());
            }
            if (set.Tables[0].Rows[0]["CheckRecordID"].ToString() != "")
            {
                detail.CheckRecordID = int.Parse(set.Tables[0].Rows[0]["CheckRecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["MaterialID"].ToString() != "")
            {
                detail.MaterialID = int.Parse(set.Tables[0].Rows[0]["MaterialID"].ToString());
            }
            if (set.Tables[0].Rows[0]["AccountNumber"].ToString() != "")
            {
                detail.AccountNumber = decimal.Parse(set.Tables[0].Rows[0]["AccountNumber"].ToString());
            }
            if (set.Tables[0].Rows[0]["CheckNumber"].ToString() != "")
            {
                detail.CheckNumber = decimal.Parse(set.Tables[0].Rows[0]["CheckNumber"].ToString());
            }
            detail.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            return detail;
        }

        public DataTable GetStockCheckDetailTable(string CheckRecordID, string MaterialID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * FROM V_OA_OfficeRes_StockCheck ");
            builder.Append(" where CheckRecordID='" + CheckRecordID + "' and MaterialID='" + MaterialID + "' ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int Update(ptOfficeResStockCheckDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_StockCheck_Detail set ");
            builder.Append("CheckRecordID=" + model.CheckRecordID + ",");
            builder.Append("MaterialID=" + model.MaterialID + ",");
            builder.Append("AccountNumber=" + model.AccountNumber + ",");
            builder.Append("CheckNumber=" + model.CheckNumber + ",");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where CheckDetailID=" + model.CheckDetailID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateStockCheck(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_StockCheck set ");
            builder.Append("IsConfirm='1'");
            builder.Append(" where CheckRecordID=" + RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

