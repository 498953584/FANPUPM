namespace cn.justwin.VehiclesBLL
{
    using cn.justwin.VehiclesDAL;
    using cn.justwin.VehiclesModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class InsuranceAndReviewBLL
    {
        private readonly InsuranceAndReviewService dal = new InsuranceAndReviewService();

        public void Add(InsuranceAndReviewModel model)
        {
            this.dal.Add(model);
        }

        public List<InsuranceAndReviewModel> DataTableToList(DataTable dt)
        {
            List<InsuranceAndReviewModel> list = new List<InsuranceAndReviewModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    InsuranceAndReviewModel item = new InsuranceAndReviewModel();
                    if ((dt.Rows[i]["Guid"] != null) && (dt.Rows[i]["Guid"].ToString() != ""))
                    {
                        item.Guid = new Guid(dt.Rows[i]["Guid"].ToString());
                    }
                    if ((dt.Rows[i]["Date"] != null) && (dt.Rows[i]["Date"].ToString() != ""))
                    {
                        item.Date = dt.Rows[i]["Date"].ToString();
                    }
                    if ((dt.Rows[i]["Type"] != null) && (dt.Rows[i]["Type"].ToString() != ""))
                    {
                        item.Type = new int?(int.Parse(dt.Rows[i]["Type"].ToString()));
                    }
                    if ((dt.Rows[i]["VehicleCode"] != null) && (dt.Rows[i]["VehicleCode"].ToString() != ""))
                    {
                        item.VehicleCode = new Guid(dt.Rows[i]["VehicleCode"].ToString());
                    }
                    if ((dt.Rows[i]["code"] != null) && (dt.Rows[i]["code"].ToString() != ""))
                    {
                        item.code = dt.Rows[i]["code"].ToString();
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

        public DataTable GetAllList()
        {
            return this.GetList("");
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public InsuranceAndReviewModel GetModel(Guid Guid)
        {
            return this.dal.GetModel(Guid);
        }

        public List<InsuranceAndReviewModel> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public List<InsuranceAndReviewModel> GetModelList(string startDate, string endDate, string code, string guid, string type, string _order)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1").Append(" ");
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.Append(" AND Date BETWEEN '").Append(startDate).Append("' ");
                if (string.IsNullOrEmpty(endDate))
                {
                    builder.Append(" AND '").Append(new DateTime(0x270f, 12, 0x1f)).Append("' ");
                }
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                if (string.IsNullOrEmpty(startDate))
                {
                    builder.Append(" AND Date BETWEEN '").Append(new DateTime(0x6d9, 1, 1)).Append("' ");
                }
                builder.Append(" AND '").Append(endDate).Append("' ");
            }
            if (!string.IsNullOrEmpty(code))
            {
                builder.Append(" AND code like '%").Append(code).Append("%' ");
            }
            if (!string.IsNullOrEmpty(guid.ToString()))
            {
                builder.Append(" AND OA_Vehicle_Main.VehicleCode like '%").Append(guid).Append("%' ");
            }
            if (type != "")
            {
                builder.Append(" AND Type =").Append(int.Parse(type)).Append(" ");
            }
            if (_order != "")
            {
                builder.Append(_order);
            }
            DataTable list = this.dal.GetList(builder.ToString());
            return this.DataTableToList(list);
        }

        public bool Update(InsuranceAndReviewModel model)
        {
            return this.dal.Update(model);
        }
    }
}

