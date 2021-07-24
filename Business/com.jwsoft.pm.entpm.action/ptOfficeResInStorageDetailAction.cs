namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class ptOfficeResInStorageDetailAction
    {
        public int Add(ptOfficeResInStorageDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_InStorage_Detail(");
            builder.Append("InStorageID,MaterialID,InStoragePrice,Amount,HandleMan");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.InStorageID + ",");
            builder.Append(model.MaterialID + ",");
            builder.Append(model.InStoragePrice + ",");
            builder.Append(model.Amount + ",");
            builder.Append("'" + model.HandleMan + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int AddStock(ptOfficeResStock of)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { " if not exists(select top 1 RecordID from OA_OfficeRes_Stock where DepotID='", of.DepotID, "' and MaterialID='", of.MaterialID, "' ) " }));
            builder.Append(" begin ");
            builder.Append("insert into OA_OfficeRes_Stock(");
            builder.Append("DepotID,MaterialID,Number,AvgPrice");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(of.DepotID + ",");
            builder.Append(of.MaterialID + ",");
            builder.Append(of.Number + ",");
            builder.Append(of.AvgPrice);
            builder.Append(")");
            builder.Append(" end ");
            builder.Append(" else ");
            builder.Append(" begin ");
            builder.Append(" declare @snum decimal(10,2) set @snum=0.00 ");
            builder.Append(" declare @sprice decimal(10,2) set @sprice=0.00 ");
            builder.Append(string.Concat(new object[] { " select @snum=isnull(Number,0),@sprice=isnull(AvgPrice,0) from OA_OfficeRes_Stock where DepotID='", of.DepotID, "' and MaterialID='", of.MaterialID, "' " }));
            builder.Append(" update OA_OfficeRes_Stock set ");
            builder.Append(string.Concat(new object[] { " AvgPrice=(@snum*@sprice+", of.Number * of.AvgPrice, ")/(select case @snum+", of.Number, " when 0 then 1 else @snum+", of.Number, " end)," }));
            builder.Append(" Number=(@snum+" + of.Number + ")");
            builder.Append(" where ");
            builder.Append(" DepotID=" + of.DepotID);
            builder.Append(" and MaterialID=" + of.MaterialID);
            builder.Append(" end ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_InStorage_Detail ");
            builder.Append(" where RecordID=" + RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int DeleteStock(int RecordID, int DepotID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" declare @num decimal(10,2) set @num=0.00 ");
            builder.Append(" declare @price decimal(10,2) set @price=0.00 ");
            builder.Append(" declare @MaterialID int set @MaterialID=0 ");
            builder.Append(" select @num=isnull(Amount,0),@price=isnull(InStoragePrice,0),@MaterialID=MaterialID from oa_OfficeRes_InStorage_Detail where RecordID='" + RecordID + "' ");
            builder.Append(" update OA_OfficeRes_Stock set ");
            builder.Append(" AvgPrice=(Number*AvgPrice-@num*@price)/(select case Number-@num when 0 then 1 else Number-@num end),");
            builder.Append(" Number=(Number-@num)");
            builder.Append(" where ");
            builder.Append(" DepotID=" + DepotID);
            builder.Append(" and MaterialID=@MaterialID");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 RecordID from OA_OfficeRes_InStorage_Detail where RecordID=" + RecordID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select a.InStorageID,a.DepotID,a.BillCode,a.InDate,a.Transactor,a.Remark,a.UserCode,a.RecordDate,b.RecordID,b.MaterialID,b.InStoragePrice,b.Amount,b.HandleMan,c.ResName,c.UseType,c.GetType,c.Unit,c.PlanFee ");
            builder.Append(" from oa_OfficeRes_InStorage a ");
            builder.Append(" join oa_OfficeRes_InStorage_Detail b on a.InStorageID=b.InStorageID ");
            builder.Append(" join OA_OfficeRes_Resources c on b.MaterialID=c.RecordID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ptOfficeResInStorageDetail GetModel(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select RecordID,InStorageID,MaterialID,InStoragePrice,Amount,HandleMan ");
            builder.Append(" from OA_OfficeRes_InStorage_Detail ");
            builder.Append(" where RecordID=" + RecordID);
            ptOfficeResInStorageDetail detail = new ptOfficeResInStorageDetail();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                detail.RecordID = int.Parse(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["InStorageID"].ToString() != "")
            {
                detail.InStorageID = int.Parse(set.Tables[0].Rows[0]["InStorageID"].ToString());
            }
            if (set.Tables[0].Rows[0]["MaterialID"].ToString() != "")
            {
                detail.MaterialID = int.Parse(set.Tables[0].Rows[0]["MaterialID"].ToString());
            }
            if (set.Tables[0].Rows[0]["InStoragePrice"].ToString() != "")
            {
                detail.InStoragePrice = decimal.Parse(set.Tables[0].Rows[0]["InStoragePrice"].ToString());
            }
            if (set.Tables[0].Rows[0]["Amount"].ToString() != "")
            {
                detail.Amount = decimal.Parse(set.Tables[0].Rows[0]["Amount"].ToString());
            }
            detail.HandleMan = set.Tables[0].Rows[0]["HandleMan"].ToString();
            return detail;
        }

        public int Update(ptOfficeResInStorageDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_InStorage_Detail set ");
            builder.Append("Amount=" + model.Amount + ",");
            builder.Append("HandleMan='" + model.HandleMan + "'");
            builder.Append(" where RecordID=" + model.RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateStock(ptOfficeResStock of, int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { " if not exists(select top 1 RecordID from OA_OfficeRes_Stock where DepotID='", of.DepotID, "' and MaterialID='", of.MaterialID, "' ) " }));
            builder.Append(" begin ");
            builder.Append("insert into OA_OfficeRes_Stock(");
            builder.Append("DepotID,MaterialID,Number,AvgPrice");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(of.DepotID + ",");
            builder.Append(of.MaterialID + ",");
            builder.Append(of.Number + ",");
            builder.Append(of.AvgPrice);
            builder.Append(")");
            builder.Append(" end ");
            builder.Append(" else ");
            builder.Append(" begin ");
            builder.Append(" declare @num decimal(10,2) set @num=0.00 ");
            builder.Append(" declare @price decimal(10,2) set @price=0.00 ");
            builder.Append(" declare @Storagenum decimal(10,2) set @Storagenum=0.00 ");
            builder.Append(" declare @Storageprice decimal(10,2) set @Storageprice=0.00 ");
            builder.Append(string.Concat(new object[] { " select @num=isnull(Number,0),@price=isnull(AvgPrice,0) from OA_OfficeRes_Stock where DepotID='", of.DepotID, "' and MaterialID='", of.MaterialID, "' " }));
            builder.Append(string.Concat(new object[] { " select @Storagenum=isnull(Amount,0),@Storageprice=isnull(InStoragePrice,0) from oa_OfficeRes_InStorage_Detail where RecordID='", RecordID, "' and MaterialID='", of.MaterialID, "' " }));
            builder.Append(" declare @tempnum decimal(10,2) set @tempnum=0.00 ");
            builder.Append(" select @tempnum=(case @num-@Storagenum when 0 then 1 else @num-@Storagenum end) ");
            builder.Append(" select @price=(@num*@price-@Storagenum*@Storageprice)/@tempnum ");
            builder.Append(" select @num=@num-@Storagenum ");
            builder.Append(" update OA_OfficeRes_Stock set ");
            builder.Append(string.Concat(new object[] { " AvgPrice=(@num*@price+", of.Number * of.AvgPrice, ")/(select case @num+", of.Number, " when 0 then 1 else @num+", of.Number, " end)," }));
            builder.Append(" Number=(@num+" + of.Number + ")");
            builder.Append(" where ");
            builder.Append(" DepotID=" + of.DepotID);
            builder.Append(" and MaterialID=" + of.MaterialID);
            builder.Append(" end ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

