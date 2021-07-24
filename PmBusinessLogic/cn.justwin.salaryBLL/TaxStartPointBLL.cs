namespace cn.justwin.salaryBLL
{
    using cn.justwin.salaryDAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class TaxStartPointBLL
    {
        private readonly TaxStartPoint dal = new TaxStartPoint();

        public int Add(TaxStartPointModel model)
        {
            return this.dal.Add(model);
        }

        public List<TaxStartPointModel> DataTableToList(DataTable dt)
        {
            List<TaxStartPointModel> list = new List<TaxStartPointModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    TaxStartPointModel item = new TaxStartPointModel {
                        TaxRateID = dt.Rows[i]["TaxRateID"].ToString()
                    };
                    if (dt.Rows[i]["TaxStartPoint"].ToString() != "")
                    {
                        item.TaxStartPoint = decimal.Parse(dt.Rows[i]["TaxStartPoint"].ToString());
                    }
                    if (dt.Rows[i]["LowerLimit"].ToString() != "")
                    {
                        item.LowerLimit = decimal.Parse(dt.Rows[i]["LowerLimit"].ToString());
                    }
                    if (dt.Rows[i]["UpperLimit"].ToString() != "")
                    {
                        item.UpperLimit = decimal.Parse(dt.Rows[i]["UpperLimit"].ToString());
                    }
                    if (dt.Rows[i]["TaxRate"].ToString() != "")
                    {
                        item.TaxRate = decimal.Parse(dt.Rows[i]["TaxRate"].ToString());
                    }
                    if (dt.Rows[i]["Deduct"].ToString() != "")
                    {
                        item.Deduct = new decimal?(decimal.Parse(dt.Rows[i]["Deduct"].ToString()));
                    }
                    if (dt.Rows[i]["AddDate"].ToString() != "")
                    {
                        item.AddDate = new DateTime?(DateTime.Parse(dt.Rows[i]["AddDate"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public int Delete(string TaxRateID)
        {
            return this.dal.Delete(TaxRateID);
        }

        public int Delete(List<string> lstStorageCode)
        {
            return this.dal.Delete(lstStorageCode);
        }

        public int Exists(string TaxRateID)
        {
            object obj2 = null;
            obj2 = this.dal.Exists(TaxRateID);
            if (obj2 != null)
            {
                return int.Parse(obj2.ToString());
            }
            return 0;
        }

        public DataTable GetAllList()
        {
            return this.GetList("");
        }

        public decimal GetInComeTaxStartPoint()
        {
            object inComeTaxStartPoint = null;
            inComeTaxStartPoint = this.dal.GetInComeTaxStartPoint();
            if (inComeTaxStartPoint != null)
            {
                return decimal.Parse(inComeTaxStartPoint.ToString());
            }
            return 0M;
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public TaxStartPointModel GetModel(string TaxRateID)
        {
            return this.dal.GetModel(TaxRateID);
        }

        public List<TaxStartPointModel> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public int SavePersonIncomeTaxStartPoint(decimal point)
        {
            return this.dal.SavePersonIncomeTaxStartPoint(point);
        }

        public int Update(TaxStartPointModel model)
        {
            return this.dal.Update(model);
        }
    }
}

