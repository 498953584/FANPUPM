namespace cn.justwin.salaryBLL
{
    using cn.justwin.salarykDAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SamTemplateItemBll
    {
        public int Add(SamTemplateItemModel model)
        {
            return SamTemplateItem.Add(model);
        }

        public List<SamTemplateItemModel> DataTableToList(DataTable dt)
        {
            List<SamTemplateItemModel> list = new List<SamTemplateItemModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    SamTemplateItemModel item = new SamTemplateItemModel {
                        ItemID = dt.Rows[i]["ItemID"].ToString(),
                        ItemName = dt.Rows[i]["ItemName"].ToString(),
                        Remark = dt.Rows[i]["Remark"].ToString(),
                        ItemCode = dt.Rows[i]["ItemCode"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public int Delete(string ItemID)
        {
            return SamTemplateItem.Delete(ItemID);
        }

        public int Delete(List<string> lstStorageCode)
        {
            return SamTemplateItem.Delete(lstStorageCode);
        }

        public int Exists(string ItemID)
        {
            int num = 0;
            object obj2 = SamTemplateItem.Exists(ItemID);
            if (obj2 != null)
            {
                num = int.Parse(obj2.ToString());
            }
            return num;
        }

        public DataTable GetAllList()
        {
            return this.GetList("");
        }

        public DataTable GetList(string strWhere)
        {
            return SamTemplateItem.GetList(strWhere);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return SamTemplateItem.GetList(Top, strWhere, filedOrder);
        }

        public SamTemplateItemModel GetModel(string ItemID)
        {
            return SamTemplateItem.GetModel(ItemID);
        }

        public List<SamTemplateItemModel> GetModelList(string strWhere)
        {
            DataTable list = SamTemplateItem.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public int Update(SamTemplateItemModel model)
        {
            return SamTemplateItem.Update(model);
        }
    }
}

