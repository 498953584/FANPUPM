namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class ptOfficeResInStorageAction
    {
        public int Add(ptOfficeResInStorage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_InStorage(");
            builder.Append("DepotID,BillCode,InDate,Transactor,Remark,UserCode,RecordDate");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.DepotID + ",");
            builder.Append("'" + model.BillCode + "',");
            builder.Append("'" + model.InDate + "',");
            builder.Append("'" + model.Transactor + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int InStorageID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete OA_OfficeRes_InStorage_Detail ");
            builder.Append(" where InStorageID=" + InStorageID);
            builder.Append(" delete OA_OfficeRes_InStorage ");
            builder.Append(" where InStorageID=" + InStorageID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int InStorageID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 InStorageID from OA_OfficeRes_InStorage where InStorageID=" + InStorageID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select InStorageID,DepotID,BillCode,InDate,Transactor,Remark,UserCode,RecordDate,IsConfirm ");
            builder.Append(" FROM OA_OfficeRes_InStorage ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ptOfficeResInStorage GetModel(int InStorageID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select InStorageID,DepotID,BillCode,InDate,Transactor,Remark,UserCode,RecordDate ");
            builder.Append(" from OA_OfficeRes_InStorage ");
            builder.Append(" where InStorageID=" + InStorageID);
            ptOfficeResInStorage storage = new ptOfficeResInStorage();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["InStorageID"].ToString() != "")
            {
                storage.InStorageID = int.Parse(set.Tables[0].Rows[0]["InStorageID"].ToString());
            }
            if (set.Tables[0].Rows[0]["DepotID"].ToString() != "")
            {
                storage.DepotID = int.Parse(set.Tables[0].Rows[0]["DepotID"].ToString());
            }
            storage.BillCode = set.Tables[0].Rows[0]["BillCode"].ToString();
            if (set.Tables[0].Rows[0]["InDate"].ToString() != "")
            {
                storage.InDate = DateTime.Parse(set.Tables[0].Rows[0]["InDate"].ToString());
            }
            storage.Transactor = set.Tables[0].Rows[0]["Transactor"].ToString();
            storage.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            storage.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                storage.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            return storage;
        }

        public int Update(ptOfficeResInStorage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_InStorage set ");
            builder.Append("DepotID=" + model.DepotID + ",");
            builder.Append("BillCode='" + model.BillCode + "',");
            builder.Append("InDate='" + model.InDate + "',");
            builder.Append("Transactor='" + model.Transactor + "',");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "'");
            builder.Append(" where InStorageID=" + model.InStorageID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateConfirm(int InStorageID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OA_OfficeRes_InStorage set ");
            builder.Append(" IsConfirm='1' ");
            builder.Append(" where InStorageID=" + InStorageID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateStock(int InStorageID, int DepotID)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append(" select * from OA_OfficeRes_InStorage_Detail where InStorageID=" + InStorageID + " ");
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                builder2.Append(" declare @num decimal(10,2) set @num=0.00 ");
                builder2.Append(" declare @price decimal(10,2) set @price=0.00 ");
                builder2.Append(" declare @Storagenum decimal(10,2) set @Storagenum=0.00 ");
                builder2.Append(" declare @Storageprice decimal(10,2) set @Storageprice=0.00 ");
                builder2.Append(" declare @tempnum decimal(10,2) set @tempnum=0.00 ");
                foreach (DataRow row in table.Rows)
                {
                    builder2.Append(string.Concat(new object[] { " if exists(select top 1 RecordID from OA_OfficeRes_Stock where DepotID='", DepotID, "' and MaterialID='", row["MaterialID"].ToString(), "' ) " }));
                    builder2.Append(" begin ");
                    builder2.Append(string.Concat(new object[] { " select @num=isnull(Number,0),@price=isnull(AvgPrice,0) from OA_OfficeRes_Stock where DepotID='", DepotID, "' and MaterialID='", row["MaterialID"].ToString(), "' " }));
                    builder2.Append(" select @Storagenum=isnull('" + row["Amount"].ToString() + "',0),@Storageprice=isnull('" + row["InStoragePrice"].ToString() + "',0) ");
                    builder2.Append(" select @tempnum=(case @num-@Storagenum when 0 then 1 else @num-@Storagenum end) ");
                    builder2.Append(" select @price=(@num*@price-@Storagenum*@Storageprice)/@tempnum ");
                    builder2.Append(" select @num=@num-@Storagenum ");
                    builder2.Append(" update OA_OfficeRes_Stock set ");
                    builder2.Append(" AvgPrice=@price,");
                    builder2.Append(" Number=@num");
                    builder2.Append(" where ");
                    builder2.Append(" DepotID=" + DepotID);
                    builder2.Append(" and MaterialID='" + row["MaterialID"].ToString() + "'");
                    builder2.Append(" end ");
                }
            }
            return publicDbOpClass.ExecSqlString(builder2.ToString());
        }
    }
}

