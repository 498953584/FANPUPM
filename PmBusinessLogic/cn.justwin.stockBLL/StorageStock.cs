namespace cn.justwin.stockBLL
{
    using cn.justwin.DAL;
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class StorageStock
    {
        private readonly cn.justwin.stockDAL.StorageStock dal = new cn.justwin.stockDAL.StorageStock();

        public int Add(StorageStockModel model)
        {
            return this.dal.Add(null, model);
        }

        public int Add(List<StorageStockModel> lstModel)
        {
            int num = 0;
            foreach (StorageStockModel model in lstModel)
            {
                this.dal.Add(null, model);
                num++;
            }
            return num;
        }

        public int Add(SqlTransaction trans, StorageStockModel model)
        {
            return this.dal.Add(null, model);
        }

        public int Add(SqlTransaction trans, List<StorageStockModel> lstModel)
        {
            int num = 0;
            foreach (StorageStockModel model in lstModel)
            {
                this.dal.Add(trans, model);
                num++;
            }
            return num;
        }

        public int Delete(string ssid)
        {
            return this.dal.Delete(ssid);
        }

        public int DeleteByStorageCode(string storageCode)
        {
            return this.dal.DeleteByStorageCode(null, storageCode);
        }

        public int DeleteByStorageCode(SqlTransaction trans, string storageCode)
        {
            return this.dal.DeleteByStorageCode(trans, storageCode);
        }

        public List<StorageStockModel> GetGenericList(string condition)
        {
            return this.dal.GetGenericList(condition);
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataTable GetListByStcode(string stcode)
        {
            return this.dal.GetListByStcode(stcode);
        }

        public StorageStockModel GetModel(string ssid)
        {
            return this.dal.GetModel(ssid);
        }

        public DataTable GetReportDataSource(string condition)
        {
            return this.dal.GetReportDataSource(condition);
        }

        public DataTable GetReportDataSource(DateTime? startDate, DateTime? endDate, string resourceNames, string resourceCodes, string storageCode, string corpName, string projectName, string treasuryName, string isFirst, string category, string specification, string brand, string modelNumber)
        {
            StringBuilder builder = new StringBuilder();
            if (startDate.HasValue)
            {
                builder.AppendFormat(" AND intime >= '{0}' ", startDate.Value);
            }
            if (endDate.HasValue)
            {
                builder.AppendFormat(" AND intime < '{0}' ", endDate.Value);
            }
            builder.Append(DBHelper.GetQuerySql("ResourceName", resourceNames)).Append(" ");
            builder.Append(DBHelper.GetQuerySql("Sm_Storage_Stock.scode", resourceCodes)).Append(" ");
            if (!string.IsNullOrEmpty(storageCode))
            {
                builder.AppendFormat("and Sm_Storage.scode like '%{0}%' ", storageCode);
            }
            if (!string.IsNullOrEmpty(corpName))
            {
                builder.AppendFormat("and CorpName like '%{0}%' ", corpName);
            }
            if (!string.IsNullOrEmpty(projectName))
            {
                builder.AppendFormat("and ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) like '%{0}%' ", projectName);
            }
            if (!string.IsNullOrEmpty(treasuryName))
            {
                builder.AppendFormat("and tname like '%{0}%' ", treasuryName);
            }
            if (isFirst != "2")
            {
                builder.AppendFormat("and isfirst = '{0}' ", isFirst);
            }
            if (!string.IsNullOrEmpty(category))
            {
                builder.AppendFormat("and ResourceTypeName like '%{0}%' ", category);
            }
            if (!string.IsNullOrEmpty(specification))
            {
                builder.AppendFormat("and Specification like '%{0}%' ", specification);
            }
            if (!string.IsNullOrEmpty(brand))
            {
                builder.AppendFormat("and Res_Resource.Brand like '%{0}%' ", brand);
            }
            if (!string.IsNullOrEmpty(modelNumber))
            {
                builder.AppendFormat("and Res_Resource.ModelNumber like '%{0}%' ", modelNumber);
            }
            return this.GetReportDataSource(builder.ToString());
        }

        public DataTable GetStorageStockDataSource(string storageCode)
        {
            return this.dal.GetStorageStockDataSource(storageCode);
        }

        public DataTable GetTableByStcode(string stcode)
        {
            return this.dal.GetTableByStcode(stcode);
        }

        public int Update(StorageStockModel model)
        {
            return this.dal.Update(null, model);
        }

        public int Update(SqlTransaction trans, StorageStockModel model)
        {
            return this.dal.Update(trans, model);
        }
    }
}

