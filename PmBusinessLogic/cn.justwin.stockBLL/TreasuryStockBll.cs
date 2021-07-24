namespace cn.justwin.stockBLL
{
    using cn.justwin.BLL;
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TreasuryStockBll
    {
        private TreasuryStock treasuryStock = new TreasuryStock();

        public int Add(SqlTransaction trans, TreasuryStockModel model)
        {
            return this.treasuryStock.Add(trans, model);
        }

        public object AlarmMethod(string tcode, string scode)
        {
            return this.treasuryStock.AlarmMethod(tcode, scode);
        }

        public int Delete(string tsid)
        {
            return this.treasuryStock.Delete(tsid);
        }

        public bool Delete(string tcode, IList<string> scodeList)
        {
            return this.treasuryStock.Delete(tcode, scodeList);
        }

        public void DeleteByUnite(string tcode, IList<TreasuryStorckParms> parms)
        {
            if (parms != null)
            {
                foreach (TreasuryStorckParms parms2 in parms)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendFormat("WHERE tcode='{0}'", tcode);
                    builder.AppendFormat("AND scode='{0}' AND sprice='{1}'", parms2.resourcCode, parms2.sprice);
                    string cropId = parms2.cropId;
                    if (string.IsNullOrEmpty(cropId))
                    {
                        builder.AppendFormat(" AND (corp ='' OR corp IS NULL)", new object[0]);
                    }
                    else
                    {
                        builder.AppendFormat(" AND corp ='{0}' ", cropId);
                    }
                    new TreasuryStock().DeleteByWhere(builder.ToString());
                }
            }
        }

        public int DeleteByWhere(string strWhere)
        {
            return this.treasuryStock.DeleteByWhere(strWhere);
        }

        public int GetCount(string tcode)
        {
            return this.treasuryStock.GetCount(tcode);
        }

        public DataTable GetGoods(string tcode, int pageSize, int pageNo)
        {
            return this.treasuryStock.GetGoods(tcode, pageSize, pageNo);
        }

        public int GetGoodsCount(string tcode)
        {
            return this.treasuryStock.GetGoodsCount(tcode);
        }

        public List<TreasuryStockModel> GetListArray(string strWhere)
        {
            return this.treasuryStock.GetListArray(strWhere);
        }

        public DataTable GetListArrayByParam(string[] scodes, string[] resourceNames, string corp, string tcode)
        {
            return this.treasuryStock.GetListArrayByParam(scodes, resourceNames, corp, tcode);
        }

        public DataTable GetListArrayByTcode(string tcode)
        {
            return this.treasuryStock.GetListArrayByTcode(tcode);
        }

        public DataTable GetListByTsid(string tsid, string tcode)
        {
            string str = "''";
            if (!string.IsNullOrEmpty(tsid))
            {
                str = "";
                foreach (string str2 in JsonHelper.GetListFromJson(tsid))
                {
                    string[] strArray = str2.Split(new char[] { '|' });
                    str = str + this.treasuryStock.GetTsidBySSc(strArray[0], strArray[1], strArray[2], tcode);
                }
                if (str.Length > 0)
                {
                    str = str.Substring(0, str.Length - 1);
                }
            }
            return this.treasuryStock.GetListByTsid(str);
        }

        public TreasuryStockModel GetModel(string tsid)
        {
            return this.treasuryStock.GetModel(tsid);
        }

        public decimal GetNumber(string code)
        {
            return this.treasuryStock.GetNumber(code);
        }

        public DataTable GetReportDataSource(string condition, string userCode)
        {
            return this.treasuryStock.GetReportDataSource(condition, userCode);
        }

        public DataTable GetReportDataSource(DateTime startDate, DateTime endDate, string resourceNames, string resourceCodes, string corpName, string treasuryName, string category, string userCode, string specification, string brand, string modelNumber)
        {
            return this.treasuryStock.GetReportDataSource(startDate, endDate, resourceNames, resourceCodes, corpName, treasuryName, category, userCode, specification, brand, modelNumber);
        }

        public int GetStuffCount(string resourceCode, string resourceName, string prjId, string isWBSRelevance, string specification, string brand, string modelNumber)
        {
            return this.treasuryStock.GetStuffCount(resourceCode, resourceName, prjId, isWBSRelevance, specification, brand, modelNumber);
        }

        public DataTable GetStuffInfo(string resourceCode, string resourceName, string prjId, int pageIndex, int pageSize, string isWBSRelevance, string specification, string brand, string modelNumber)
        {
            return this.treasuryStock.GetStuffInfo(resourceCode, resourceName, prjId, pageIndex, pageSize, isWBSRelevance, specification, brand, modelNumber);
        }

        public decimal GetTotal(string tcode)
        {
            return this.treasuryStock.GetTotal(tcode);
        }

        public DataTable GetTreasuryData(string ResourceName, string ResourceCode, string Tname, string CorpName, string userCode, string specification, string brand, string modelNumber)
        {
            return new TreasuryStock().GetTreasuryData(ResourceName, ResourceCode, Tname, CorpName, userCode, specification, brand, modelNumber);
        }

        public int Update(TreasuryStockModel model)
        {
            return this.treasuryStock.Update(model);
        }
    }
}

