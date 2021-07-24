namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class BillOfDocExtrMain
    {
        public bool Add(com.jwsoft.pm.entpm.model.BillOfDocExtrMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_BillOfDocExtrMain(");
            builder.Append("DepositoryID,BillOfDocExtrNumber,BillOfDocExtrDate,TransactPerson,PullDownPerson,PullDownDept,TransactState,BillOfDocExtrMoney,Remark,UserCode,RecordDate");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.DepositoryID + ",");
            builder.Append("'" + model.BillOfDocExtrNumber + "',");
            builder.Append("'" + model.BillOfDocExtrDate + "',");
            builder.Append("'" + model.TransactPerson + "',");
            builder.Append("'" + model.PullDownPerson + "',");
            builder.Append("'" + model.PullDownDept + "',");
            builder.Append("'" + model.TransactState + "',");
            builder.Append(model.BillOfDocExtrMoney + ",");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "'");
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(int BillOfDocExtrID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_BillOfDocExtrDetail ");
            builder.Append(" where BillOfDocExtrID=" + BillOfDocExtrID + " ");
            builder.Append("delete pm_Repe_BillOfDocExtrMain ");
            builder.Append(" where BillOfDocExtrID=" + BillOfDocExtrID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Exists(int BillOfDocExtrID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_BillOfDocExtrMain");
            builder.Append(" where BillOfDocExtrID=" + BillOfDocExtrID + " ");
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
            builder.Append("select BillOfDocExtrID,DepositoryID,BillOfDocExtrNumber,BillOfDocExtrDate,TransactPerson,PullDownPerson,PullDownDept,TransactState,BillOfDocExtrMoney,Remark,UserCode,RecordDate ");
            builder.Append(" FROM pm_Repe_BillOfDocExtrMain ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_BillOfDocExtrMain", "BillOfDocExtrID"));
        }

        public com.jwsoft.pm.entpm.model.BillOfDocExtrMain GetModel(int BillOfDocExtrID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" BillOfDocExtrID,DepositoryID,BillOfDocExtrNumber,BillOfDocExtrDate,TransactPerson,PullDownPerson,PullDownDept,TransactState,BillOfDocExtrMoney,Remark,UserCode,RecordDate ");
            builder.Append(" from pm_Repe_BillOfDocExtrMain ");
            builder.Append(" where BillOfDocExtrID=" + BillOfDocExtrID + " ");
            com.jwsoft.pm.entpm.model.BillOfDocExtrMain main = new com.jwsoft.pm.entpm.model.BillOfDocExtrMain();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["BillOfDocExtrID"].ToString() != "")
            {
                main.BillOfDocExtrID = int.Parse(set.Tables[0].Rows[0]["BillOfDocExtrID"].ToString());
            }
            if (set.Tables[0].Rows[0]["DepositoryID"].ToString() != "")
            {
                main.DepositoryID = int.Parse(set.Tables[0].Rows[0]["DepositoryID"].ToString());
            }
            main.BillOfDocExtrNumber = set.Tables[0].Rows[0]["BillOfDocExtrNumber"].ToString();
            if (set.Tables[0].Rows[0]["BillOfDocExtrDate"].ToString() != "")
            {
                main.BillOfDocExtrDate = DateTime.Parse(set.Tables[0].Rows[0]["BillOfDocExtrDate"].ToString());
            }
            main.TransactPerson = set.Tables[0].Rows[0]["TransactPerson"].ToString();
            main.PullDownPerson = set.Tables[0].Rows[0]["PullDownPerson"].ToString();
            main.PullDownDept = set.Tables[0].Rows[0]["PullDownDept"].ToString();
            main.TransactState = set.Tables[0].Rows[0]["TransactState"].ToString();
            if (set.Tables[0].Rows[0]["BillOfDocExtrMoney"].ToString() != "")
            {
                main.BillOfDocExtrMoney = decimal.Parse(set.Tables[0].Rows[0]["BillOfDocExtrMoney"].ToString());
            }
            main.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            main.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                main.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            return main;
        }

        public bool Update(com.jwsoft.pm.entpm.model.BillOfDocExtrMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_BillOfDocExtrMain set ");
            builder.Append("DepositoryID=" + model.DepositoryID + ",");
            builder.Append("BillOfDocExtrNumber='" + model.BillOfDocExtrNumber + "',");
            builder.Append("BillOfDocExtrDate='" + model.BillOfDocExtrDate + "',");
            builder.Append("TransactPerson='" + model.TransactPerson + "',");
            builder.Append("PullDownPerson='" + model.PullDownPerson + "',");
            builder.Append("PullDownDept='" + model.PullDownDept + "',");
            builder.Append("TransactState='" + model.TransactState + "',");
            builder.Append("BillOfDocExtrMoney=" + model.BillOfDocExtrMoney + ",");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "'");
            builder.Append(" where BillOfDocExtrID=" + model.BillOfDocExtrID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool UpdateIsConfirm(int BillOfDocExtrID)
        {
            com.jwsoft.pm.entpm.model.BillOfDocExtrMain model = this.GetModel(BillOfDocExtrID);
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append(" select * from pm_Repe_BillOfDocExtrDetail where BillOfDocExtrID=" + model.BillOfDocExtrID + " ");
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
                    if (row["IsInfluence"].ToString() == "1")
                    {
                        builder2.Append(string.Concat(new object[] { " update pm_resources set FactCost = ", Convert.ToDecimal(row["Scalar"].ToString()) * Convert.ToDecimal(row["UnitPrice"].ToString()), " where MdResourceId = ", row["MdResourceId"].ToString() }));
                    }
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

