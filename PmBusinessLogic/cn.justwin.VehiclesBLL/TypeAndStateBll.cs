namespace cn.justwin.VehiclesBLL
{
    using cn.justwin.VehiclesDAL;
    using cn.justwin.VehiclesModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class TypeAndStateBll
    {
        private TypeAndStateService dal = new TypeAndStateService();

        public void Add(TypeAndState model)
        {
            this.dal.Add(model);
        }

        public List<TypeAndState> DataTableToList(DataTable dt)
        {
            List<TypeAndState> list = new List<TypeAndState>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    TypeAndState item = new TypeAndState();
                    if ((dt.Rows[i]["guid"] != null) && (dt.Rows[i]["guid"].ToString() != ""))
                    {
                        item.guid = new Guid(dt.Rows[i]["guid"].ToString());
                    }
                    if ((dt.Rows[i]["code"] != null) && (dt.Rows[i]["code"].ToString() != ""))
                    {
                        item.code = dt.Rows[i]["code"].ToString();
                    }
                    if ((dt.Rows[i]["Name"] != null) && (dt.Rows[i]["Name"].ToString() != ""))
                    {
                        item.Name = dt.Rows[i]["Name"].ToString();
                    }
                    if ((dt.Rows[i]["ParentGuid"] != null) && (dt.Rows[i]["ParentGuid"].ToString() != ""))
                    {
                        item.ParentGuid = new Guid(dt.Rows[i]["ParentGuid"].ToString());
                    }
                    if ((dt.Rows[i]["Type"] != null) && (dt.Rows[i]["Type"].ToString() != ""))
                    {
                        item.Type = new int?(int.Parse(dt.Rows[i]["Type"].ToString()));
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

        public void Delete(List<string> Guidlist)
        {
            this.dal.Delete(Guidlist);
        }

        public bool Exists(Guid guid)
        {
            return this.dal.Exists(guid);
        }

        public bool Exists(string where)
        {
            return this.dal.Exists(where);
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public TypeAndState GetModel(Guid guid)
        {
            return this.dal.GetModel(guid);
        }

        public List<TypeAndState> GetModelList(string strWhere)
        {
            return this.DataTableToList(this.GetList(strWhere));
        }

        public bool Update(TypeAndState model)
        {
            return this.dal.Update(model);
        }
    }
}

