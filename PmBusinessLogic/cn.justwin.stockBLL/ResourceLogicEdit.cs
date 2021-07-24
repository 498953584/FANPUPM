namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections;
    using System.Data;

    public class ResourceLogicEdit
    {
        public int GetIsExistsSameResourceCode(string resourceCode)
        {
            int isExistsSameResourceCode = 0;
            if (resourceCode != "")
            {
                isExistsSameResourceCode = ResourceEdit.GetIsExistsSameResourceCode(resourceCode);
            }
            return isExistsSameResourceCode;
        }

        public int GetIsShowCount()
        {
            return ResourceEdit.GetIsShowCount();
        }

        public DataTable GetIsShowItem()
        {
            DataTable priceShowItem = ResourceEdit.GetPriceShowItem();
            if (priceShowItem.Rows.Count > 0)
            {
                return priceShowItem;
            }
            return null;
        }

        public string GetNewPriceTypeName(string rptCode)
        {
            string newPriceTypeName = "";
            if (rptCode != "")
            {
                newPriceTypeName = ResourceEdit.GetNewPriceTypeName(rptCode);
            }
            return newPriceTypeName;
        }

        public string GetNewResPrice(string resCode, string priceTypeCode)
        {
            object newResPrice = ResourceEdit.GetNewResPrice(resCode, priceTypeCode);
            if (newResPrice == null)
            {
                return "0";
            }
            return newResPrice.ToString();
        }

        public string GetResPrice(string resCode)
        {
            string resPrice = "";
            if (resCode != "")
            {
                resPrice = ResourceEdit.GetResPrice(resCode);
            }
            return resPrice;
        }

        public ResourceModel GetResrouceList(string ResCode)
        {
            DataTable resrouceList = new DataTable();
            ResourceModel model = new ResourceModel();
            if (ResCode != "")
            {
                resrouceList = ResourceEdit.GetResrouceList(ResCode);
            }
            if (resrouceList.Rows.Count > 0)
            {
                model.ResourceCode = resrouceList.Rows[0]["ResourceCode"].ToString();
                model.CategoryCode = resrouceList.Rows[0]["CategoryCode"].ToString();
                model.ResourceName = resrouceList.Rows[0]["ResourceName"].ToString();
                model.Specification = resrouceList.Rows[0]["Specification"].ToString();
                if (resrouceList.Rows[0]["ResourceStyle"].ToString() != "")
                {
                    model.ResourceStyle = resrouceList.Rows[0]["ResourceStyle"].ToString();
                }
                if (resrouceList.Rows[0]["ResourceType"].ToString() != "")
                {
                    model.ResourceType = resrouceList.Rows[0]["ResourceType"].ToString();
                }
                if ((resrouceList.Rows[0]["imgurl"] != null) && (resrouceList.Rows[0]["imgurl"].ToString() != ""))
                {
                    model.ResourceImageUrl = resrouceList.Rows[0]["imgurl"].ToString();
                }
            }
            return model;
        }

        public string GetUnitId(string resCode)
        {
            string unitId = "";
            if (resCode != "")
            {
                unitId = ResourceEdit.GetUnitId(resCode);
            }
            return unitId;
        }

        public int GetUnitID(string UnitName)
        {
            int unitID = 0;
            if (UnitName != "")
            {
                unitID = ResourceEdit.GetUnitID(UnitName);
            }
            return unitID;
        }

        public Hashtable GetUnitNameList()
        {
            Hashtable hashtable = new Hashtable();
            DataTable unitNameList = ResourceEdit.GetUnitNameList();
            if (unitNameList.Rows.Count > 0)
            {
                for (int i = 0; i < unitNameList.Rows.Count; i++)
                {
                    hashtable.Add(unitNameList.Rows[i][0].ToString(), unitNameList.Rows[i][0].ToString());
                }
            }
            return hashtable;
        }

        public string GetUnitNameList(string unitID)
        {
            string unitNameList = "";
            if (unitID != "")
            {
                unitNameList = ResourceEdit.GetUnitNameList(unitID);
            }
            return unitNameList;
        }

        public int Insert(ResourceNewPriceModel resNewPriceModel)
        {
            int num = 0;
            if ((resNewPriceModel.rCode != "") && (resNewPriceModel.rptCode != ""))
            {
                num = ResourceEdit.Insert(resNewPriceModel);
            }
            return num;
        }

        public int Insert(ResourceModel resModel, ResourceGaugeModel resGaugeModel, ResourcePriceModel resPriceModel)
        {
            int num = 0;
            if (((resModel.ResourceCode != "") && (resGaugeModel.ResourceCode != "")) && (resPriceModel.ResourceCode != ""))
            {
                num = ResourceEdit.Insert(resModel, resGaugeModel, resPriceModel);
            }
            return num;
        }

        public int Update(ResourceNewPriceModel resNewPriceModel)
        {
            int num = 0;
            if ((resNewPriceModel.rCode != "") && (resNewPriceModel.rptCode != ""))
            {
                num = ResourceEdit.Update(resNewPriceModel);
            }
            return num;
        }

        public int Update(ResourceModel resModel, ResourceGaugeModel resGaugeModel, ResourcePriceModel resPriceModel)
        {
            int num = 0;
            if (((resModel.ResourceCode != "") && (resGaugeModel.ResourceCode != "")) && (resPriceModel.ResourceCode != ""))
            {
                num = ResourceEdit.Update(resModel, resGaugeModel, resPriceModel);
            }
            return num;
        }
    }
}

