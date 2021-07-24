namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ResourceTypeManage
    {
        private readonly ResourceTypeAction dal = new ResourceTypeAction();

        public void Add(ResourceType model)
        {
            this.dal.Add(model);
        }

        public List<ResourceType> DataTableToList(DataTable dt)
        {
            List<ResourceType> list = new List<ResourceType>();
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    ResourceType item = new ResourceType {
                        ResourceTypeId = dt.Rows[i]["ResourceTypeId"].ToString(),
                        ParentId = dt.Rows[i]["ParentId"].ToString(),
                        ResourceTypeCode = dt.Rows[i]["ResourceTypeCode"].ToString(),
                        ResourceTypeName = dt.Rows[i]["ResourceTypeName"].ToString(),
                        InputUser = dt.Rows[i]["InputUser"].ToString()
                    };
                    if (dt.Rows[i]["InputDate"].ToString() != "")
                    {
                        item.InputDate = new DateTime?(DateTime.Parse(dt.Rows[i]["InputDate"].ToString()));
                    }
                    if (dt.Rows[i]["IsValid"].ToString() != "")
                    {
                        if ((dt.Rows[i]["IsValid"].ToString() == "1") || (dt.Rows[i]["IsValid"].ToString().ToLower() == "true"))
                        {
                            item.IsValid = true;
                        }
                        else
                        {
                            item.IsValid = false;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public void Delete(string ResourceTypeId)
        {
            this.dal.Delete(ResourceTypeId);
        }

        public DataTable GetAllList()
        {
            return this.GetList("");
        }

        public DataTable getChildList(string parentid)
        {
            return this.dal.getreaourceList(" res.ResourceTYpe='" + parentid + "'");
        }

        public List<ResourceType> getChildNodes(string parentid)
        {
            return this.GetModelList(" ParentId='" + parentid + "'");
        }

        public DataTable getDetailResource(string strWhere)
        {
            return this.dal.getDetailResource(strWhere);
        }

        public ResourceType getFirstNodes(string resourcetypeid)
        {
            return this.GetModel(resourcetypeid);
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataTable GetList(string strWhere, string filedOrder)
        {
            return this.dal.GetList(strWhere, filedOrder);
        }

        public ResourceType GetModel(string ResourceTypeId)
        {
            return this.dal.GetModel(ResourceTypeId);
        }

        public List<ResourceType> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public bool ISHaveChildNodes(string parentid)
        {
            DataTable list = this.GetList(" ParentId='" + parentid + "' ");
            return ((list != null) && (list.Rows.Count > 0));
        }

        public void Update(ResourceType model)
        {
            this.dal.Update(model);
        }
    }
}

