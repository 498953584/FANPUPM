namespace cn.justwin.stockBLL
{
    using cn.justwin.DAL;
    using cn.justwin.Domain;
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Storage
    {
        private readonly cn.justwin.stockDAL.Storage dal = new cn.justwin.stockDAL.Storage();
        private static string priceTypeId = ConfigHelper.Get("PurchasePriceType");

        public int Add(StorageModel model)
        {
            return this.dal.Add(null, model);
        }

        public int Add(SqlTransaction trans, StorageModel model)
        {
            return this.dal.Add(trans, model);
        }

        public int DelByTrans(SqlTransaction trans, List<string> lstStorageCode)
        {
            return this.dal.DelByTrans(trans, lstStorageCode);
        }

        public int Delete(List<string> lstStorageCode)
        {
            return this.dal.Delete(lstStorageCode);
        }

        public int Delete(string scode)
        {
            return this.dal.Delete(scode);
        }

        public string GetCodeFromGuid(string guid)
        {
            return this.dal.GetCodeFromGuid(guid);
        }

        private string GetConditionByIsFirst(bool isFirst)
        {
            if (isFirst)
            {
                return " AND isfirst = 1 ";
            }
            return " AND isfirst = 0 ";
        }

        public DataTable GetEnsureStorage(DateTime? startDate, DateTime? endDate, string storageCode, string treasuryName, string person)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" and flowstate = 1 and inflag = 0 ");
            if (startDate.HasValue)
            {
                builder.AppendFormat(" AND intime >= '{0}' ", startDate.Value.ToShortDateString());
            }
            if (endDate.HasValue)
            {
                builder.AppendFormat(" AND intime < '{0}' ", endDate.Value.ToShortDateString());
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                builder.Append("and scode like '%").Append(storageCode).Append("%' ");
            }
            if (!string.IsNullOrEmpty(treasuryName))
            {
                builder.Append("and tname like '%").Append(treasuryName).Append("%' ");
            }
            if (!string.IsNullOrEmpty(person))
            {
                builder.Append("and person like '%").Append(person).Append("%' ");
            }
            return this.dal.GetTable(builder.ToString());
        }

        public DataTable GetEnsureTalbe()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("flowstate = 1 and inflag = 0 ");
            return this.dal.GetTable(builder.ToString());
        }

        public StorageModel GetModel(string scode)
        {
            return this.dal.GetModel(scode);
        }

        public StorageModel GetModelBySid(string sid)
        {
            return this.dal.GetModelBySid(sid);
        }

        public DataTable GetTable()
        {
            return this.dal.GetTable(string.Empty);
        }

        public DataTable GetTable(bool isFirst)
        {
            string conditionByIsFirst = this.GetConditionByIsFirst(isFirst);
            return this.dal.GetTable(conditionByIsFirst);
        }

        public DataTable Select(DateTime? startDate, DateTime? endDate, string storageCode, string treasuryName, string person, bool isFirst)
        {
            StringBuilder builder = new StringBuilder();
            if (startDate.HasValue)
            {
                builder.AppendFormat(" AND intime >= '{0}' ", startDate.Value.ToShortDateString());
            }
            if (endDate.HasValue)
            {
                builder.AppendFormat(" AND intime < '{0}' ", endDate.Value.ToShortDateString());
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                builder.Append("and scode like '%").Append(storageCode).Append("%' ");
            }
            if (!string.IsNullOrEmpty(treasuryName))
            {
                builder.Append("and tname like '%").Append(treasuryName).Append("%' ");
            }
            if (!string.IsNullOrEmpty(person))
            {
                builder.Append("and person like '%").Append(person).Append("%' ");
            }
            builder.Append(this.GetConditionByIsFirst(isFirst));
            return this.dal.GetTable(builder.ToString());
        }

        public int Update(StorageModel model)
        {
            return this.dal.Update(model);
        }

        public int Update(SqlTransaction trans, StorageModel model)
        {
            return this.dal.Update(trans, model);
        }

        public bool UpdateInflag(string storageCode, string type, string userName)
        {
            bool flag = true;
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    StorageModel model = this.dal.GetModel(storageCode);
                    model.inflag = true;
                    model.IsInTime = DateTime.Now;
                    this.dal.Update(trans, model);
                    TreasuryStock stock = new TreasuryStock();
                    cn.justwin.stockBLL.StorageStock stock2 = new cn.justwin.stockBLL.StorageStock();
                    foreach (StorageStockModel model2 in stock2.GetGenericList("stcode = " + storageCode))
                    {
                        TreasuryStockModel model3 = new TreasuryStockModel {
                            tsid = Guid.NewGuid().ToString(),
                            scode = model2.scode,
                            tcode = model.tcode,
                            sprice = model2.sprice,
                            snumber = model2.number,
                            isfirst = false,
                            corp = model2.corp,
                            incode = model2.stcode,
                            intime = DateTime.Today,
                            intype = 0,
                            Type = type
                        };
                        stock.Add(trans, model3);
                        if (!model.isfirst)
                        {
                            string resourceId = Resource.GetResourceId(model2.scode);
                            stock.UpdateResPrice(trans, resourceId, priceTypeId, model2.sprice, userName);
                        }
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    flag = false;
                    trans.Rollback();
                }
            }
            return flag;
        }
    }
}

