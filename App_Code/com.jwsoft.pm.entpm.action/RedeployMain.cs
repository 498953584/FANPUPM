namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class RedeployMain
    {
        public bool Add(com.jwsoft.pm.entpm.model.RedeployMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_RedeployMain(");
            builder.Append("DepositoryID,RedeployNumber,RedeployDate,RedeployMoney,TransactPerson,TransactState,Remark,UserCode,RecordDate");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.DepositoryID + ",");
            builder.Append("'" + model.RedeployNumber + "',");
            builder.Append("'" + model.RedeployDate + "',");
            builder.Append(model.RedeployMoney + ",");
            builder.Append("'" + model.TransactPerson + "',");
            builder.Append("'" + model.TransactState + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "'");
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(int Redeployid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_RedeployDetail ");
            builder.Append(" where Redeployid=" + Redeployid + " ");
            builder.Append("delete pm_Repe_RedeployMain ");
            builder.Append(" where Redeployid=" + Redeployid + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Exists(int Redeployid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_RedeployMain");
            builder.Append(" where Redeployid=" + Redeployid + " ");
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
            builder.Append("select Redeployid,DepositoryID,RedeployNumber,RedeployDate,RedeployMoney,TransactPerson,TransactState,Remark,UserCode,RecordDate ");
            builder.Append(" FROM pm_Repe_RedeployMain ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_RedeployMain", "Redeployid"));
        }

        public com.jwsoft.pm.entpm.model.RedeployMain GetModel(int Redeployid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" Redeployid,DepositoryID,RedeployNumber,RedeployDate,RedeployMoney,TransactPerson,TransactState,Remark,UserCode,RecordDate ");
            builder.Append(" from pm_Repe_RedeployMain ");
            builder.Append(" where Redeployid=" + Redeployid + " ");
            com.jwsoft.pm.entpm.model.RedeployMain main = new com.jwsoft.pm.entpm.model.RedeployMain();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["Redeployid"].ToString() != "")
            {
                main.Redeployid = int.Parse(set.Tables[0].Rows[0]["Redeployid"].ToString());
            }
            if (set.Tables[0].Rows[0]["DepositoryID"].ToString() != "")
            {
                main.DepositoryID = int.Parse(set.Tables[0].Rows[0]["DepositoryID"].ToString());
            }
            main.RedeployNumber = set.Tables[0].Rows[0]["RedeployNumber"].ToString();
            if (set.Tables[0].Rows[0]["RedeployDate"].ToString() != "")
            {
                main.RedeployDate = DateTime.Parse(set.Tables[0].Rows[0]["RedeployDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["RedeployMoney"].ToString() != "")
            {
                main.RedeployMoney = decimal.Parse(set.Tables[0].Rows[0]["RedeployMoney"].ToString());
            }
            main.TransactPerson = set.Tables[0].Rows[0]["TransactPerson"].ToString();
            main.TransactState = set.Tables[0].Rows[0]["TransactState"].ToString();
            main.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            main.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                main.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            return main;
        }

        public bool Update(com.jwsoft.pm.entpm.model.RedeployMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_RedeployMain set ");
            builder.Append("DepositoryID=" + model.DepositoryID + ",");
            builder.Append("RedeployNumber='" + model.RedeployNumber + "',");
            builder.Append("RedeployDate='" + model.RedeployDate + "',");
            builder.Append("RedeployMoney=" + model.RedeployMoney + ",");
            builder.Append("TransactPerson='" + model.TransactPerson + "',");
            builder.Append("TransactState='" + model.TransactState + "',");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "'");
            builder.Append(" where Redeployid=" + model.Redeployid + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool UpdateIsConfirm(int Redeployid)
        {
            com.jwsoft.pm.entpm.model.RedeployMain model = this.GetModel(Redeployid);
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append(" select * from pm_Repe_RedeployDetail where Redeployid=" + model.Redeployid + " ");
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                builder2.Append(" declare @num decimal(10,2) set @num=0.00 ");
                builder2.Append(" declare @Scalar decimal(10,2) set @Scalar=0.00 ");
                foreach (DataRow row in table.Rows)
                {
                    builder2.Append(" if exists(select top 1 RecordID from pm_Repe_RealTime where DepositoryID= " + row["FoldDepositoryID"].ToString() + " and MaterialId = " + row["MaterialId"].ToString() + " ) ");
                    builder2.Append(" begin ");
                    builder2.Append(" select @num=isnull(Amount,0) from pm_Repe_RealTime where DepositoryID= " + row["FoldDepositoryID"].ToString() + " and MaterialId = " + row["MaterialId"].ToString());
                    builder2.Append(" select @Scalar=isnull('" + row["Scalar"].ToString() + "',0)");
                    builder2.Append(" select @num=@num+@Scalar ");
                    builder2.Append(" update pm_Repe_RealTime set ");
                    builder2.Append(" Amount=@num");
                    builder2.Append(" where ");
                    builder2.Append(" DepositoryID=" + row["FoldDepositoryID"].ToString());
                    builder2.Append(" and MaterialId = " + row["MaterialId"].ToString() + " ");
                    builder2.Append(" end ");
                    builder2.Append(" else ");
                    builder2.Append(" begin ");
                    builder2.Append(" insert into pm_Repe_RealTime(");
                    builder2.Append(" DepositoryID,MaterialId,Amount");
                    builder2.Append(" )");
                    builder2.Append(" values (");
                    builder2.Append(" " + row["FoldDepositoryID"].ToString() + ",");
                    builder2.Append(" " + row["MaterialId"].ToString() + ",");
                    builder2.Append(" " + row["Scalar"].ToString() + " ");
                    builder2.Append(" )");
                    builder2.Append(" end ");
                    builder2.Append(string.Concat(new object[] { " if exists(select top 1 RecordID from pm_Repe_RealTime where DepositoryID= ", model.DepositoryID, " and MaterialId = ", row["MaterialId"].ToString(), " ) " }));
                    builder2.Append(" begin ");
                    builder2.Append(string.Concat(new object[] { " select @num=isnull(Amount,0) from pm_Repe_RealTime where DepositoryID= ", model.DepositoryID, " and MaterialId = ", row["MaterialId"].ToString() }));
                    builder2.Append(" select @Scalar=isnull('" + row["Scalar"].ToString() + "',0)");
                    builder2.Append(" select @num=@num-@Scalar ");
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
                    builder2.Append(" -" + row["Scalar"].ToString() + " ");
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

