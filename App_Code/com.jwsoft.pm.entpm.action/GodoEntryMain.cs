namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class GodoEntryMain
    {
        public bool Add(com.jwsoft.pm.entpm.model.GodoEntryMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_GodoEntryMain(");
            builder.Append("DepositoryID,GodoEntryNumber,GodoEntryDate,TransactPerson,TransactState,GodoEntryMoney,Remark,UserCode,RecordDate");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.DepositoryID + ",");
            builder.Append("'" + model.GodoEntryNumber + "',");
            builder.Append("'" + model.GodoEntryDate + "',");
            builder.Append("'" + model.TransactPerson + "',");
            builder.Append("'" + model.TransactState + "',");
            builder.Append(model.GodoEntryMoney + ",");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "'");
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(int GodoEntryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_GodoEntryDetail ");
            builder.Append(" where GodoEntryID=" + GodoEntryID + " ");
            builder.Append("delete pm_Repe_GodoEntryMain ");
            builder.Append(" where GodoEntryID=" + GodoEntryID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Exists(int GodoEntryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_GodoEntryMain");
            builder.Append(" where GodoEntryID=" + GodoEntryID + " ");
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
            builder.Append("select GodoEntryID,DepositoryID,GodoEntryNumber,GodoEntryDate,TransactPerson,TransactState,GodoEntryMoney,Remark,UserCode,RecordDate ");
            builder.Append(" FROM pm_Repe_GodoEntryMain ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_GodoEntryMain", "GodoEntryID"));
        }

        public com.jwsoft.pm.entpm.model.GodoEntryMain GetModel(int GodoEntryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" GodoEntryID,DepositoryID,GodoEntryNumber,GodoEntryDate,TransactPerson,TransactState,GodoEntryMoney,Remark,UserCode,RecordDate ");
            builder.Append(" from pm_Repe_GodoEntryMain ");
            builder.Append(" where GodoEntryID=" + GodoEntryID + " ");
            com.jwsoft.pm.entpm.model.GodoEntryMain main = new com.jwsoft.pm.entpm.model.GodoEntryMain();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["GodoEntryID"].ToString() != "")
            {
                main.GodoEntryID = int.Parse(set.Tables[0].Rows[0]["GodoEntryID"].ToString());
            }
            if (set.Tables[0].Rows[0]["DepositoryID"].ToString() != "")
            {
                main.DepositoryID = int.Parse(set.Tables[0].Rows[0]["DepositoryID"].ToString());
            }
            main.GodoEntryNumber = set.Tables[0].Rows[0]["GodoEntryNumber"].ToString();
            if (set.Tables[0].Rows[0]["GodoEntryDate"].ToString() != "")
            {
                main.GodoEntryDate = DateTime.Parse(set.Tables[0].Rows[0]["GodoEntryDate"].ToString());
            }
            main.TransactPerson = set.Tables[0].Rows[0]["TransactPerson"].ToString();
            main.TransactState = set.Tables[0].Rows[0]["TransactState"].ToString();
            if (set.Tables[0].Rows[0]["GodoEntryMoney"].ToString() != "")
            {
                main.GodoEntryMoney = decimal.Parse(set.Tables[0].Rows[0]["GodoEntryMoney"].ToString());
            }
            main.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            main.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                main.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            return main;
        }

        public bool Update(com.jwsoft.pm.entpm.model.GodoEntryMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_GodoEntryMain set ");
            builder.Append("DepositoryID=" + model.DepositoryID + ",");
            builder.Append("GodoEntryNumber='" + model.GodoEntryNumber + "',");
            builder.Append("GodoEntryDate='" + model.GodoEntryDate + "',");
            builder.Append("TransactPerson='" + model.TransactPerson + "',");
            builder.Append("TransactState='" + model.TransactState + "',");
            builder.Append("GodoEntryMoney=" + model.GodoEntryMoney + ",");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "'");
            builder.Append(" where GodoEntryID=" + model.GodoEntryID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool UpdateIsConfirm(int GodoEntryID)
        {
            com.jwsoft.pm.entpm.model.GodoEntryMain model = this.GetModel(GodoEntryID);
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append(" select * from pm_Repe_GodoEntryDetail where GodoEntryID=" + model.GodoEntryID + " ");
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                builder2.Append(" declare @num decimal(10,2) set @num=0.00 ");
                builder2.Append(" declare @Scalar decimal(10,2) set @Scalar=0.00 ");
                foreach (DataRow row in table.Rows)
                {
                    builder2.Append(string.Concat(new object[] { " if exists(select top 1 RecordID from pm_Repe_RealTime where DepositoryID= ", model.DepositoryID, " and MaterialId = ", row["MaterialId"].ToString(), " ) " }));
                    builder2.Append(" begin ");
                    builder2.Append(string.Concat(new object[] { " select @num=isnull(Amount,0) from pm_Repe_RealTime where DepositoryID= ", model.DepositoryID, " and MaterialId = ", row["MaterialId"].ToString() }));
                    builder2.Append(" select @Scalar=isnull('" + row["Scalar"].ToString() + "',0)");
                    builder2.Append(" select @num=@num+@Scalar ");
                    builder2.Append(" update pm_Repe_RealTime set ");
                    builder2.Append(" Amount=@num");
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
                    builder2.Append(" " + row["Scalar"].ToString() + " ");
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

