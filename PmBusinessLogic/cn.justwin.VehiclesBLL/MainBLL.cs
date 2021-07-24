namespace cn.justwin.VehiclesBLL
{
    using cn.justwin.VehiclesDAL;
    using cn.justwin.VehiclesModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class MainBLL
    {
        private readonly MainService dal = new MainService();

        public void Add(MainModel model)
        {
            this.dal.Add(model);
        }

        public List<MainModel> DataTableToList(DataTable dt)
        {
            List<MainModel> list = new List<MainModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    MainModel item = new MainModel();
                    if ((dt.Rows[i]["guid"] != null) && (dt.Rows[i]["guid"].ToString() != ""))
                    {
                        item.guid = new Guid(dt.Rows[i]["guid"].ToString());
                    }
                    if ((dt.Rows[i]["VehicleCode"] != null) && (dt.Rows[i]["VehicleCode"].ToString() != ""))
                    {
                        item.VehicleCode = dt.Rows[i]["VehicleCode"].ToString();
                    }
                    if ((dt.Rows[i]["VehicleName"] != null) && (dt.Rows[i]["VehicleName"].ToString() != ""))
                    {
                        item.VehicleName = dt.Rows[i]["VehicleName"].ToString();
                    }
                    if ((dt.Rows[i]["VehicleIdentify"] != null) && (dt.Rows[i]["VehicleIdentify"].ToString() != ""))
                    {
                        item.VehicleIdentify = dt.Rows[i]["VehicleIdentify"].ToString();
                    }
                    if ((dt.Rows[i]["EngineCode"] != null) && (dt.Rows[i]["EngineCode"].ToString() != ""))
                    {
                        item.EngineCode = dt.Rows[i]["EngineCode"].ToString();
                    }
                    if ((dt.Rows[i]["Specification"] != null) && (dt.Rows[i]["Specification"].ToString() != ""))
                    {
                        item.Specification = dt.Rows[i]["Specification"].ToString();
                    }
                    if ((dt.Rows[i]["VehicleType"] != null) && (dt.Rows[i]["VehicleType"].ToString() != ""))
                    {
                        item.VehicleType = new Guid(dt.Rows[i]["VehicleType"].ToString());
                    }
                    if ((dt.Rows[i]["PurchaseDate"] != null) && (dt.Rows[i]["PurchaseDate"].ToString() != ""))
                    {
                        item.PurchaseDate = new DateTime?(DateTime.Parse(dt.Rows[i]["PurchaseDate"].ToString()));
                    }
                    if ((dt.Rows[i]["OnHouserDate"] != null) && (dt.Rows[i]["OnHouserDate"].ToString() != ""))
                    {
                        item.OnHouserDate = new DateTime?(DateTime.Parse(dt.Rows[i]["OnHouserDate"].ToString()));
                    }
                    if ((dt.Rows[i]["InspectionDate"] != null) && (dt.Rows[i]["InspectionDate"].ToString() != ""))
                    {
                        item.InspectionDate = new DateTime?(DateTime.Parse(dt.Rows[i]["InspectionDate"].ToString()));
                    }
                    if ((dt.Rows[i]["InsuranceDate"] != null) && (dt.Rows[i]["InsuranceDate"].ToString() != ""))
                    {
                        item.InsuranceDate = new DateTime?(DateTime.Parse(dt.Rows[i]["InsuranceDate"].ToString()));
                    }
                    if ((dt.Rows[i]["Address"] != null) && (dt.Rows[i]["Address"].ToString() != ""))
                    {
                        item.Address = dt.Rows[i]["Address"].ToString();
                    }
                    if ((dt.Rows[i]["Sparekey"] != null) && (dt.Rows[i]["Sparekey"].ToString() != ""))
                    {
                        item.Sparekey = dt.Rows[i]["Sparekey"].ToString();
                    }
                    if ((dt.Rows[i]["Ability"] != null) && (dt.Rows[i]["Ability"].ToString() != ""))
                    {
                        item.Ability = dt.Rows[i]["Ability"].ToString();
                    }
                    if ((dt.Rows[i]["Fatfare"] != null) && (dt.Rows[i]["Fatfare"].ToString() != ""))
                    {
                        item.Fatfare = new decimal?(decimal.Parse(dt.Rows[i]["Fatfare"].ToString()));
                    }
                    if ((dt.Rows[i]["Recordedprice"] != null) && (dt.Rows[i]["Recordedprice"].ToString() != ""))
                    {
                        item.Recordedprice = new decimal?(decimal.Parse(dt.Rows[i]["Recordedprice"].ToString()));
                    }
                    if ((dt.Rows[i]["ManufactureDate"] != null) && (dt.Rows[i]["ManufactureDate"].ToString() != ""))
                    {
                        item.ManufactureDate = dt.Rows[i]["ManufactureDate"].ToString();
                    }
                    if ((dt.Rows[i]["DepreciationRate"] != null) && (dt.Rows[i]["DepreciationRate"].ToString() != ""))
                    {
                        item.DepreciationRate = new decimal?(decimal.Parse(dt.Rows[i]["DepreciationRate"].ToString()));
                    }
                    if ((dt.Rows[i]["State"] != null) && (dt.Rows[i]["State"].ToString() != ""))
                    {
                        item.State = new Guid(dt.Rows[i]["State"].ToString());
                    }
                    if ((dt.Rows[i]["Remark"] != null) && (dt.Rows[i]["Remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["Remark"].ToString();
                    }
                    if ((dt.Rows[i]["Purchaser"] != null) && (dt.Rows[i]["Purchaser"].ToString() != ""))
                    {
                        item.Purchaser = dt.Rows[i]["Purchaser"].ToString();
                    }
                    if ((dt.Rows[i]["IsShare"] != null) && (dt.Rows[i]["IsShare"].ToString() != ""))
                    {
                        item.IsShare = new int?(int.Parse(dt.Rows[i]["IsShare"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(Guid guid)
        {
            return this.dal.Delete(guid);
        }

        public bool DeleteList(string guidlist)
        {
            return this.dal.DeleteList(guidlist);
        }

        public bool Exists(Guid guid)
        {
            return this.dal.Exists(guid);
        }

        public bool Exists(string where)
        {
            return this.dal.Exists(where);
        }

        public DataTable GetAllList()
        {
            return this.GetList("");
        }

        public string GetCodeByguid(Guid guid)
        {
            return this.dal.GetCodeByguid(guid);
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public MainModel GetModel(Guid guid)
        {
            return this.dal.GetModel(guid);
        }

        public MainModel GetModel(string VehicleCode)
        {
            return this.dal.GetModel(VehicleCode);
        }

        public List<MainModel> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public DataTable getSelect(string strWhere)
        {
            return this.dal.getSelect(strWhere);
        }

        public List<MainModel> SearchVehicleList(string startDate, string endDate, string _VehicleType, string _VehicleCode, string _VehicleIdentify, string _Address, string _VehicleName, string _EngineCode, string _State, string IsShare)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" PurchaseDate BETWEEN '").Append(startDate).Append("' ");
            builder.Append(" AND '").Append(endDate).Append("' ");
            if (!string.IsNullOrEmpty(_VehicleType))
            {
                builder.Append(" AND VehicleType ='").Append(_VehicleType).Append("' ");
            }
            if (!string.IsNullOrEmpty(_VehicleCode))
            {
                builder.Append(" AND VehicleCode ='").Append(_VehicleCode).Append("' ");
            }
            if (!string.IsNullOrEmpty(_VehicleIdentify))
            {
                builder.Append(" AND VehicleIdentify LIKE '%").Append(_VehicleIdentify).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_Address))
            {
                builder.Append(" AND Address LIKE '%").Append(_Address).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_VehicleName))
            {
                builder.Append(" AND VehicleName LIKE '%").Append(_VehicleName).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_EngineCode))
            {
                builder.Append(" AND EngineCode LIKE '%").Append(_EngineCode).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_State))
            {
                builder.Append(" AND State = '").Append(_State).Append("' ");
            }
            if (IsShare != "")
            {
                builder.Append(" AND IsShare =").Append(int.Parse(IsShare)).Append(" ");
            }
            DataTable list = this.GetList(builder.ToString());
            if (list.Columns.Count > 0)
            {
                return this.DataTableToList(list);
            }
            return null;
        }

        public List<MainModel> SearchVehicleList(string startDate, string endDate, string startDateBX, string endDateBX, string startDateNJ, string endDateNJ, string startDateSH, string endDateSH, string _VehicleType, string _VehicleCode, string _VehicleIdentify, string _Address, string _VehicleName, string _EngineCode, string _State, string IsShare, string _order)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.Append(" AND PurchaseDate BETWEEN '").Append(startDate).Append("' ");
                if (string.IsNullOrEmpty(endDate))
                {
                    builder.Append(" AND '").Append(new DateTime(0x270f, 12, 0x1f)).Append("' ");
                }
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                if (string.IsNullOrEmpty(startDate))
                {
                    builder.Append(" AND PurchaseDate BETWEEN '").Append(new DateTime(0x6d9, 1, 1)).Append("' ");
                }
                builder.Append(" AND '").Append(endDate).Append("' ");
            }
            if (!string.IsNullOrEmpty(startDateSH))
            {
                builder.Append(" AND OnHouserDate BETWEEN '").Append(startDateSH).Append("' ");
                if (string.IsNullOrEmpty(endDateSH))
                {
                    builder.Append(" AND '").Append(new DateTime(0x270f, 12, 0x1f)).Append("' ");
                }
            }
            if (!string.IsNullOrEmpty(endDateSH))
            {
                if (string.IsNullOrEmpty(startDateSH))
                {
                    builder.Append(" AND OnHouserDate BETWEEN '").Append(new DateTime(0x6d9, 1, 1)).Append("' ");
                }
                builder.Append(" AND '").Append(endDateSH).Append("' ");
            }
            if (!string.IsNullOrEmpty(startDateNJ))
            {
                builder.Append(" AND InspectionDate BETWEEN '").Append(startDateNJ).Append("' ");
                if (string.IsNullOrEmpty(endDateNJ))
                {
                    builder.Append(" AND '").Append(new DateTime(0x270f, 12, 0x1f)).Append("' ");
                }
            }
            if (!string.IsNullOrEmpty(endDateNJ))
            {
                if (string.IsNullOrEmpty(startDateNJ))
                {
                    builder.Append(" AND InspectionDate BETWEEN '").Append(new DateTime(0x6d9, 1, 1)).Append("' ");
                }
                builder.Append(" AND '").Append(endDateNJ).Append("' ");
            }
            if (!string.IsNullOrEmpty(startDateBX))
            {
                builder.Append(" AND InsuranceDate BETWEEN '").Append(startDateBX).Append("' ");
                if (string.IsNullOrEmpty(endDateBX))
                {
                    builder.Append(" AND '").Append(new DateTime(0x270f, 12, 0x1f)).Append("' ");
                }
            }
            if (!string.IsNullOrEmpty(endDateBX))
            {
                if (string.IsNullOrEmpty(startDateBX))
                {
                    builder.Append(" AND InsuranceDate BETWEEN '").Append(new DateTime(0x6d9, 1, 1)).Append("' ");
                }
                builder.Append(" AND '").Append(endDateBX).Append("' ");
            }
            if (!string.IsNullOrEmpty(_VehicleType))
            {
                builder.Append(" AND VehicleType ='").Append(_VehicleType).Append("' ");
            }
            if (!string.IsNullOrEmpty(_VehicleCode))
            {
                builder.Append(" AND VehicleCode LIKE '%").Append(_VehicleCode).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_VehicleIdentify))
            {
                builder.Append(" AND VehicleIdentify LIKE '%").Append(_VehicleIdentify).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_Address))
            {
                builder.Append(" AND Address LIKE '%").Append(_Address).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_VehicleName))
            {
                builder.Append(" AND VehicleName LIKE '%").Append(_VehicleName).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_EngineCode))
            {
                builder.Append(" AND EngineCode LIKE '%").Append(_EngineCode).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_State))
            {
                builder.Append(" AND State = '").Append(_State).Append("' ");
            }
            if (IsShare != "")
            {
                builder.Append(" AND IsShare = ").Append(int.Parse(IsShare)).Append(" ");
            }
            if (_order != "")
            {
                builder.Append(_order);
            }
            DataTable list = this.GetList(builder.ToString());
            if (list.Columns.Count > 0)
            {
                return this.DataTableToList(list);
            }
            return null;
        }

        public DataTable SearchVehicleTable(string startDate, string endDate, string _VehicleType, string _VehicleCode, string _VehicleIdentify, string _Address, string _VehicleName, string _EngineCode, string _State, string IsShare, string _order)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.Append(" and PurchaseDate >= '").Append(startDate).Append("' ");
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.Append(" and PurchaseDate <= '").Append(endDate).Append("' ");
            }
            if (!string.IsNullOrEmpty(_VehicleType))
            {
                builder.Append(" AND VehicleType ='").Append(_VehicleType).Append("' ");
            }
            if (!string.IsNullOrEmpty(_VehicleCode))
            {
                builder.Append(" AND VehicleCode LIKE '%").Append(_VehicleCode).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_VehicleIdentify))
            {
                builder.Append(" AND VehicleIdentify LIKE '%").Append(_VehicleIdentify).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_Address))
            {
                builder.Append(" AND Address LIKE '%").Append(_Address).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_VehicleName))
            {
                builder.Append(" AND VehicleName LIKE '%").Append(_VehicleName).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_EngineCode))
            {
                builder.Append(" AND EngineCode LIKE '%").Append(_EngineCode).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_State))
            {
                builder.Append(" AND State = '").Append(_State).Append("' ");
            }
            if (IsShare != "")
            {
                builder.Append(" AND IsShare =").Append(int.Parse(IsShare)).Append(" ");
            }
            if (_order != "")
            {
                builder.Append(_order);
            }
            DataTable list = this.GetList(builder.ToString());
            if (list.Rows.Count > 0)
            {
                return list;
            }
            return null;
        }

        public DataTable SearchVehicleTable(DateTime startDate, DateTime endDate, DateTime startDateBX, DateTime endDateBX, DateTime startDateNJ, DateTime endDateNJ, DateTime startDateSH, DateTime endDateSH, string _VehicleType, string _VehicleCode, string _VehicleIdentify, string _Address, string _VehicleName, string _EngineCode, string _State, string IsShare)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" PurchaseDate BETWEEN '").Append(startDate.ToShortDateString()).Append("' ");
            builder.Append(" AND '").Append(endDate.ToShortDateString()).Append("' ");
            builder.Append("AND (OnHouserDate BETWEEN '").Append(startDateSH.ToShortDateString()).Append("' ");
            builder.Append(" AND '").Append(endDateSH.ToShortDateString()).Append("' )");
            builder.Append(" AND ( InspectionDate BETWEEN '").Append(startDateNJ.ToShortDateString()).Append("' ");
            builder.Append(" AND '").Append(endDateNJ.ToShortDateString()).Append("' )");
            builder.Append(" AND ( InsuranceDate BETWEEN '").Append(startDateBX.ToShortDateString()).Append("' ");
            builder.Append(" AND '").Append(endDateBX.ToShortDateString()).Append("' )");
            if (!string.IsNullOrEmpty(_VehicleType))
            {
                builder.Append(" AND VehicleType ='").Append(_VehicleType).Append("' ");
            }
            if (!string.IsNullOrEmpty(_VehicleCode))
            {
                builder.Append(" AND VehicleCode ='").Append(_VehicleCode).Append("' ");
            }
            if (!string.IsNullOrEmpty(_VehicleIdentify))
            {
                builder.Append(" AND VehicleIdentify LIKE '%").Append(_VehicleIdentify).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_Address))
            {
                builder.Append(" AND Address LIKE '%").Append(_Address).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_VehicleName))
            {
                builder.Append(" AND VehicleName LIKE '%").Append(_VehicleName).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_EngineCode))
            {
                builder.Append(" AND EngineCode LIKE '%").Append(_EngineCode).Append("%' ");
            }
            if (!string.IsNullOrEmpty(_State))
            {
                builder.Append(" AND State = '").Append(_State).Append("' ");
            }
            if (IsShare != "")
            {
                builder.Append(" AND IsShare = ").Append(int.Parse(IsShare)).Append(" ");
            }
            DataTable list = this.GetList(builder.ToString());
            if (list.Rows.Count > 0)
            {
                return list;
            }
            return null;
        }

        public bool Update(MainModel model)
        {
            return this.dal.Update(model);
        }
    }
}

