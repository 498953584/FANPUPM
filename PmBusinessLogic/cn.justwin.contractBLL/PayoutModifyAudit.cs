namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.stockBLL;
    using cn.justwin.stockModel;
    using PmBusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class PayoutModifyAudit : ISelfEvent
    {
        private BudModifyTaskService budModifyTaskSer = new BudModifyTaskService();
        private BudTaskService budTaskSer = new BudTaskService();
        private BudModifyService modifySer = new BudModifyService();
        private ConPayoutModifyService payOutSer = new ConPayoutModifyService();
        private PurchaseStock purchaseSotck = new PurchaseStock();

        public void CommitEvent(object key)
        {
            string primarykey = key.ToString();
            ConPayoutModify payOutModify = (from r in this.payOutSer
                where r.ModifyID == primarykey
                select r).FirstOrDefault<ConPayoutModify>();
            BudModify budModify = (from r in this.modifySer
                where r.ModifyId == payOutModify.BudModifyId
                select r).FirstOrDefault<BudModify>();
            budModify.Flowstate = 1;
            this.modifySer.Update(budModify);
            using (List<BudModifyTask>.Enumerator enumerator = (from p in this.budModifyTaskSer
                where p.ModifyId == budModify.ModifyId
                select p).ToList<BudModifyTask>().GetEnumerator())
            {
                BudModifyTask task;
                while (enumerator.MoveNext())
                {
                    task = enumerator.Current;
                    BudTask item = (from p in this.budTaskSer
                        where p.TaskId == task.TaskId
                        select p).FirstOrDefault<BudTask>();
                    if ((item != null) && (budModify.Flowstate == 1))
                    {
                        if (task.ModifyType == 0)
                        {
                            item.IsValid = true;
                            this.budTaskSer.Update(item);
                            this.budModifyTaskSer.UpdateTotal2(task.ModifyTaskId);
                        }
                        if (item.ModifyId != task.ModifyId)
                        {
                            item.ModifyId = task.ModifyId;
                            item.Total2 += task.Total2;
                            decimal? quantity = item.Quantity;
                            decimal num = task.Quantity;
                            item.Quantity = quantity.HasValue ? new decimal?(quantity.GetValueOrDefault() + num) : null;
                            if (item.Quantity != 0M)
                            {
                                item.UnitPrice = item.Total2 / item.Quantity;
                            }
                            this.budTaskSer.Update(item);
                            this.budModifyTaskSer.UpdateTotal2(task.ModifyTaskId);
                        }
                    }
                }
            }
            string cmdText = string.Format(" SELECT * FROM Con_Modify_Stock  where ModifyId='{0}' ", primarykey);
            if (SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null).Rows.Count != 0)
            {
                string str2 = string.Format("SELECT ModifyStock.*FROM Con_Modify_Stock ModifyStock\r\n                                            INNER JOIN Sm_Purchase_Stock purchaseStock ON ModifyStock.purchaseId=purchaseStock.psid\r\n                                            WHERE ModifyId='{0}'", primarykey);
                DataTable table2 = SqlHelper.ExecuteQuery(CommandType.Text, str2, null);
                string str3 = string.Format("SELECT purchaseStock.* FROM Sm_Purchase_Stock purchaseStock \r\n                                                  LEFT JOIN (SELECT *FROM Con_Modify_Stock  where ModifyId='{0}') ModifyStock\r\n                                                  ON purchaseStock.psid=ModifyStock.purchaseId\r\n                                                  WHERE purchaseStock.pscode in (SELECT DISTINCT Pscode FROM Con_Modify_Stock WHERE ModifyId='{0}')\r\n                                                  AND ModifyStockId IS NULL", primarykey);
                DataTable table3 = SqlHelper.ExecuteQuery(CommandType.Text, str3, null);
                string str4 = string.Format("SELECT ModifyStock.* FROM Con_Modify_Stock ModifyStock\r\n                                               LEFT JOIN Sm_Purchase_Stock purchaseStock ON ModifyStock.purchaseId=purchaseStock.psid\r\n                                               WHERE ModifyId='{0}'AND psid IS NULL", primarykey);
                DataTable table4 = SqlHelper.ExecuteQuery(CommandType.Text, str4, null);
                using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                {
                    connection.Open();
                    SqlTransaction trans = connection.BeginTransaction();
                    foreach (DataRow row in table2.Rows)
                    {
                        PurchaseStockModel model = new PurchaseStockModel {
                            psid = row["PurchaseId"].ToString(),
                            sprice = DBHelper.GetDecimal(row["Sprice"]),
                            number = DBHelper.GetDecimal(row["Quantity"]),
                            corp = DBHelper.GetString(row["Corp"])
                        };
                        if (row["ArrivalDate"] != null)
                        {
                            model.ArrivalDate = row["ArrivalDate"].ToString();
                        }
                        else
                        {
                            model.ArrivalDate = null;
                        }
                        model.pscode = row["Pscode"].ToString();
                        model.scode = row["Scode"].ToString();
                        this.purchaseSotck.Update(trans, model);
                    }
                    foreach (DataRow row2 in table3.Rows)
                    {
                        this.purchaseSotck.Delete(trans, row2["psid"].ToString());
                    }
                    foreach (DataRow row3 in table4.Rows)
                    {
                        PurchaseStockModel model2 = new PurchaseStockModel {
                            psid = row3["PurchaseId"].ToString(),
                            sprice = DBHelper.GetDecimal(row3["Sprice"]),
                            number = DBHelper.GetDecimal(row3["Quantity"]),
                            corp = DBHelper.GetString(row3["Corp"])
                        };
                        if (row3["ArrivalDate"] != null)
                        {
                            model2.ArrivalDate = row3["ArrivalDate"].ToString();
                        }
                        else
                        {
                            model2.ArrivalDate = null;
                        }
                        model2.pscode = row3["Pscode"].ToString();
                        model2.scode = row3["Scode"].ToString();
                        this.purchaseSotck.Add(trans, model2);
                    }
                    trans.Commit();
                    return;
                }
            }
            string str5 = string.Format("\r\n\t\t\t\t\t\t\t\t\tSELECT PCode FROM Sm_Purchase purchase\r\n\t\t\t\t\t\t\t\t\tINNER JOIN Con_Payout_Modify payoutModify ON purchase.Contract=payoutModify.ContractId\r\n\t\t\t\t\t\t\t\t\tWHERE ModifyId='{0}' AND purchase.FlowState=1", primarykey);
            DataTable table5 = SqlHelper.ExecuteQuery(CommandType.Text, str5, null);
            if (table5.Rows.Count > 0)
            {
                using (SqlConnection connection2 = new SqlConnection(SqlHelper.ConnectionString))
                {
                    connection2.Open();
                    SqlTransaction transaction2 = connection2.BeginTransaction();
                    foreach (DataRow row4 in table5.Rows)
                    {
                        this.purchaseSotck.DeleteByPscode(transaction2, row4["PCode"].ToString());
                    }
                    transaction2.Commit();
                }
            }
        }

        public void RefuseEvent(object primarykey)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
            cn.justwin.contractDAL.PayoutModify modify = new cn.justwin.contractDAL.PayoutModify();
            PayoutModifyModel model = new PayoutModifyModel();
            model = modify.GetModel(key.ToString());
            if (model != null)
            {
                cn.justwin.contractDAL.PayoutContract contract = new cn.justwin.contractDAL.PayoutContract();
                PayoutContractModel model2 = contract.GetModel(model.ContractID);
                if ((model2 != null) && model2.ModifiedMoney.HasValue)
                {
                    model2.ModifiedMoney -= model.ModifyMoney;
                }
                contract.Update(model2, null);
            }
        }
    }
}

