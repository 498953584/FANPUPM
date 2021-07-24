namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class MeterialPlanStock
    {
        private cn.justwin.stockDAL.MeterialPlanStock meterialPlanStock = new cn.justwin.stockDAL.MeterialPlanStock();

        public int Add(MaterialPlanStockModel model)
        {
            return this.meterialPlanStock.Add(model);
        }

        public int Add(SqlTransaction trans, List<MaterialPlanStockModel> lstModel)
        {
            int num = 0;
            foreach (MaterialPlanStockModel model in lstModel)
            {
                this.meterialPlanStock.Add(trans, model);
                num++;
            }
            return num;
        }

        public int Delete(string wpsid)
        {
            return this.meterialPlanStock.Delete(wpsid);
        }

        public int DeleteByWantplanCode(string wantplanCode)
        {
            return this.meterialPlanStock.DeleteByWantplanCode(wantplanCode);
        }

        public int DeleteByWantplanCode(SqlTransaction trans, string wantplanCode)
        {
            return this.meterialPlanStock.DeleteByWantplanCode(trans, wantplanCode);
        }

        public int delMore(string sql)
        {
            return this.meterialPlanStock.delMore(sql);
        }

        public DataTable GetBudTaskDiff(string wpcode, string prjId)
        {
            return this.meterialPlanStock.GetBudTaskDiff(wpcode, prjId);
        }

        public List<MaterialPlanStockModel> GetListArray(string strWhere)
        {
            return this.meterialPlanStock.GetListArray(strWhere);
        }

        public DataTable GetListArrayByWantpcode(string wpcode)
        {
            return this.meterialPlanStock.GetListArrayByWantpcode(wpcode);
        }

        public DataTable GetListArrayByWpcode(string wpcode)
        {
            return this.meterialPlanStock.GetListArrayByWpcode(wpcode);
        }

        public DataTable GetListArrayByWpcodenew(string wpcode)
        {
            return this.meterialPlanStock.GetListArrayByWpcodenew(wpcode);
        }

        public DataTable GetListByScode(string scode)
        {
            return this.meterialPlanStock.GetListByScode(scode);
        }

        public string GetMinArrivalDate(string resourceCode, string Wpcode)
        {
            return this.meterialPlanStock.GetMinArrivalDate(resourceCode, Wpcode);
        }

        public MaterialPlanStockModel GetModel(string wpsid)
        {
            return this.meterialPlanStock.GetModel(wpsid);
        }

        public string GetRemark(string resourceCode, string Wpcode)
        {
            return this.meterialPlanStock.GetRemark(resourceCode, Wpcode);
        }

        public DataTable GetResByBud(string resCode, string resName, string specification, string brand, string prjId, int version, int pageSize, int pageIndex, string isWBSRelevance, string taskCode)
        {
            return this.meterialPlanStock.GetResByBud(resCode, resName, specification, brand, prjId, version, pageSize, pageIndex, isWBSRelevance, taskCode);
        }

        public int GetResCountByBud(string resCode, string resName, string specification, string brand, string prjId, int version, string isWBSRelevance)
        {
            return this.meterialPlanStock.GetResCountByBud(resCode, resName, specification, brand, prjId, version, isWBSRelevance);
        }

        public DataTable GetTableByParam(string startTime, string endTime, string wpcode, string[] ResourceNames, string[] ResourceCodes, string prjName, string category, string specification, string brand, string modelNumber)
        {
            return this.meterialPlanStock.GetTableByParam(startTime, endTime, wpcode, ResourceNames, ResourceCodes, prjName, category, specification, brand, modelNumber);
        }

        public DataTable showMaterialListForAdd(string codeList, string prjId, string taskIdList, int count)
        {
            return this.meterialPlanStock.showMaterialListForAdd(codeList, prjId, taskIdList, count);
        }

        public DataTable showMaterialListForUpdate(string code)
        {
            return this.meterialPlanStock.showMaterialListForUpdate(code);
        }

        public int Update(MaterialPlanStockModel model)
        {
            return this.meterialPlanStock.Update(model);
        }

        public int updateMorePlanStock(string codeList)
        {
            return this.meterialPlanStock.updateMorePlanStock(codeList);
        }
    }
}

