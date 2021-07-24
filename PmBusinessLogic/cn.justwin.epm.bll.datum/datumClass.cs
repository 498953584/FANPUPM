namespace cn.justwin.epm.bll.datum
{
    using cn.justwin.epm.datum;
    using System;
    using System.Data;
    using System.Text;

    public class datumClass
    {
        private readonly datumClassService dal = new datumClassService();

        public bool Exists(int id)
        {
            bool flag = false;
            DataTable list = new DataTable();
            string strWhere = " (1=1)  and ParentId = '" + id + "'  and isValid = '1'";
            list = this.dal.GetList(strWhere);
            if ((list != null) && (list.Rows.Count > 0))
            {
                flag = true;
            }
            return flag;
        }

        public string GetClassList(string pid)
        {
            StringBuilder builder = new StringBuilder();
            DataTable list = new DataTable();
            list = this.dal.GetList(pid);
            if (list.Rows.Count > 0)
            {
                foreach (DataRow row in list.Rows)
                {
                    if ((row["TypeId"] != null) && (row["TypeId"].ToString() != ""))
                    {
                        builder.Append(",'").Append(row["TypeId"]).Append("'");
                    }
                }
            }
            return builder.ToString();
        }

        public DataTable GetListByTable(string strWhere, string table)
        {
            return this.dal.GetListByTable(strWhere, table);
        }

        public datumClassModel GetModel(int TypeId)
        {
            return this.dal.GetModel(TypeId);
        }

        public string GetType(string strwhere)
        {
            StringBuilder builder = new StringBuilder();
            DataTable list = new DataTable();
            list = this.dal.GetList(strwhere);
            if (list.Rows.Count > 0)
            {
                foreach (DataRow row in list.Rows)
                {
                    if ((row["TypeId"] != null) && (row["TypeId"].ToString() != ""))
                    {
                        builder.Append(",'").Append(row["ParentId"]).Append("'");
                    }
                }
            }
            return builder.ToString();
        }

        public int GetTypePID(string strwhere)
        {
            int num = 0;
            DataTable list = new DataTable();
            list = this.dal.GetList(strwhere);
            if (((list != null) && (list.Rows[0]["ParentId"] != null)) && (list.Rows[0]["ParentId"].ToString() != ""))
            {
                num = int.Parse(list.Rows[0]["ParentId"].ToString());
            }
            return num;
        }
    }
}

