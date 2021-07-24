namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class InventoryMain
    {
        public bool Add(com.jwsoft.pm.entpm.model.InventoryMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_InventoryMain(");
            builder.Append("DepositoryID,InventoryDate,InventoryPerson,Remark,TransactState,UserCode,RecordDate");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.DepositoryID + ",");
            builder.Append("'" + model.InventoryDate + "',");
            builder.Append("'" + model.InventoryPerson + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.TransactState + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "'");
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(int InventoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_InventoryDetail ");
            builder.Append(" where InventoryID=" + InventoryID + " ");
            builder.Append("delete pm_Repe_InventoryMain ");
            builder.Append(" where InventoryID=" + InventoryID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Exists(int InventoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_InventoryMain");
            builder.Append(" where InventoryID=" + InventoryID + " ");
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
            builder.Append("select InventoryID,DepositoryID,InventoryDate,InventoryPerson,Remark,TransactState,AuditState,UserCode,RecordDate,RecordID ");
            builder.Append(" FROM pm_Repe_InventoryMain ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_InventoryMain", "InventoryID"));
        }

        public com.jwsoft.pm.entpm.model.InventoryMain GetModel(int InventoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" InventoryID,DepositoryID,InventoryDate,InventoryPerson,Remark,TransactState,AuditState,UserCode,RecordDate,RecordID ");
            builder.Append(" from pm_Repe_InventoryMain ");
            builder.Append(" where InventoryID=" + InventoryID + " ");
            com.jwsoft.pm.entpm.model.InventoryMain main = new com.jwsoft.pm.entpm.model.InventoryMain();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["InventoryID"].ToString() != "")
            {
                main.InventoryID = int.Parse(set.Tables[0].Rows[0]["InventoryID"].ToString());
            }
            if (set.Tables[0].Rows[0]["DepositoryID"].ToString() != "")
            {
                main.DepositoryID = int.Parse(set.Tables[0].Rows[0]["DepositoryID"].ToString());
            }
            if (set.Tables[0].Rows[0]["InventoryDate"].ToString() != "")
            {
                main.InventoryDate = DateTime.Parse(set.Tables[0].Rows[0]["InventoryDate"].ToString());
            }
            main.InventoryPerson = set.Tables[0].Rows[0]["InventoryPerson"].ToString();
            main.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            main.TransactState = set.Tables[0].Rows[0]["TransactState"].ToString();
            if (set.Tables[0].Rows[0]["AuditState"].ToString() != "")
            {
                main.AuditState = int.Parse(set.Tables[0].Rows[0]["AuditState"].ToString());
            }
            main.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                main.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                main.RecordID = new Guid(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            return main;
        }

        public bool Update(com.jwsoft.pm.entpm.model.InventoryMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_InventoryMain set ");
            builder.Append("DepositoryID=" + model.DepositoryID + ",");
            builder.Append("InventoryDate='" + model.InventoryDate + "',");
            builder.Append("InventoryPerson='" + model.InventoryPerson + "',");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("TransactState='" + model.TransactState + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "'");
            builder.Append(" where InventoryID=" + model.InventoryID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool UpdateIsConfirm(int InventoryID)
        {
            com.jwsoft.pm.entpm.model.InventoryMain model = this.GetModel(InventoryID);
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append(" select * from pm_Repe_InventoryDetail where InventoryID=" + model.InventoryID + " ");
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    builder2.Append(string.Concat(new object[] { " if exists(select top 1 RecordID from pm_Repe_RealTime where DepositoryID= ", model.DepositoryID, " and MaterialId = ", row["MaterialId"].ToString(), " ) " }));
                    builder2.Append(" begin ");
                    builder2.Append(" update pm_Repe_RealTime set ");
                    builder2.Append(" Amount=" + row["FactScalar"].ToString());
                    builder2.Append(" where ");
                    builder2.Append(" DepositoryID=" + model.DepositoryID);
                    builder2.Append(" and MaterialId = " + row["MaterialId"].ToString() + " ");
                    builder2.Append(" end ");
                    builder2.Append(" else ");
                    builder2.Append(" begin ");
                    builder2.Append(" insert into pm_Repe_RealTime(");
                    builder2.Append(" DepositoryID,MaterialId,Amount");
                    builder2.Append(" )");
                    builder2.Append(" values (");
                    builder2.Append(" " + model.DepositoryID + ",");
                    builder2.Append(" " + row["MaterialId"].ToString() + ",");
                    builder2.Append(" " + row["FactScalar"].ToString() + " ");
                    builder2.Append(" )");
                    builder2.Append(" end ");
                }
                if (publicDbOpClass.NonQuerySqlString(builder2.ToString()))
                {
                    model.TransactState = "1";
                    return this.Update(model);
                }
            }
            return false;
        }
    }
}

