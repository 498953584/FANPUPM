namespace cn.justwin.VehiclesBLL
{
    using cn.justwin.VehiclesDAL;
    using cn.justwin.VehiclesModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ReimbursementBLL
    {
        private ReimbursementService dal = new ReimbursementService();

        public bool Add(Reimbursement model)
        {
            return this.dal.Add(model);
        }

        public List<Reimbursement> DataTableToList(DataTable dt)
        {
            List<Reimbursement> list = new List<Reimbursement>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Reimbursement item = new Reimbursement();
                    if ((dt.Rows[i]["Guid"] != null) && (dt.Rows[i]["Guid"].ToString() != ""))
                    {
                        item.Guid = new Guid(dt.Rows[i]["Guid"].ToString());
                    }
                    if ((dt.Rows[i]["UserName"] != null) && (dt.Rows[i]["UserName"].ToString() != ""))
                    {
                        item.UserName = dt.Rows[i]["UserName"].ToString();
                    }
                    if ((dt.Rows[i]["VehicleGuid"] != null) && (dt.Rows[i]["VehicleGuid"].ToString() != ""))
                    {
                        item.VehicleGuid = new Guid(dt.Rows[i]["VehicleGuid"].ToString());
                    }
                    if ((dt.Rows[i]["Date"] != null) && (dt.Rows[i]["Date"].ToString() != ""))
                    {
                        item.Date = new DateTime?(DateTime.Parse(dt.Rows[i]["Date"].ToString()));
                    }
                    if ((dt.Rows[i]["Destination"] != null) && (dt.Rows[i]["Destination"].ToString() != ""))
                    {
                        item.Destination = dt.Rows[i]["Destination"].ToString();
                    }
                    if ((dt.Rows[i]["Tolls"] != null) && (dt.Rows[i]["Tolls"].ToString() != ""))
                    {
                        item.Tolls = new decimal?(decimal.Parse(dt.Rows[i]["Tolls"].ToString()));
                    }
                    if ((dt.Rows[i]["Repairs"] != null) && (dt.Rows[i]["Repairs"].ToString() != ""))
                    {
                        item.Repairs = new decimal?(decimal.Parse(dt.Rows[i]["Repairs"].ToString()));
                    }
                    if ((dt.Rows[i]["FuelCosts"] != null) && (dt.Rows[i]["FuelCosts"].ToString() != ""))
                    {
                        item.FuelCosts = new decimal?(decimal.Parse(dt.Rows[i]["FuelCosts"].ToString()));
                    }
                    if ((dt.Rows[i]["MaintenanceCosts"] != null) && (dt.Rows[i]["MaintenanceCosts"].ToString() != ""))
                    {
                        item.MaintenanceCosts = new decimal?(decimal.Parse(dt.Rows[i]["MaintenanceCosts"].ToString()));
                    }
                    if ((dt.Rows[i]["Remark"] != null) && (dt.Rows[i]["Remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["Remark"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(Guid Guid)
        {
            return this.dal.Delete(Guid);
        }

        public bool DeleteList(string Guidlist)
        {
            return this.dal.DeleteList(Guidlist);
        }

        public bool Exists(Guid Guid)
        {
            return this.dal.Exists(Guid);
        }

        public DataTable getAllList(string strWhere)
        {
            return this.dal.getAllList(strWhere);
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public Reimbursement GetModel(Guid Guid)
        {
            return this.dal.GetModel(Guid);
        }

        public List<Reimbursement> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public bool Update(Reimbursement model)
        {
            return this.dal.Update(model);
        }
    }
}

