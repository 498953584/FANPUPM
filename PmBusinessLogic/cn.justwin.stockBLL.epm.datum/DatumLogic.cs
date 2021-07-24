namespace cn.justwin.stockBLL.epm.datum
{
    using cn.justwin.epm.datum;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;

    public class DatumLogic
    {
        private readonly DatumService dal = new DatumService();
        private static Regex RegNumber = new Regex("^[0-9]+$");

        public void Add(DatumModel model)
        {
            this.dal.Add(model);
        }

        public List<DatumModel> DataTableToList(DataTable dt)
        {
            List<DatumModel> list = new List<DatumModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    DatumModel item = new DatumModel();
                    if ((dt.Rows[i]["i_id"] != null) && (dt.Rows[i]["i_id"].ToString() != ""))
                    {
                        item.i_id = new Guid(dt.Rows[i]["i_id"].ToString());
                    }
                    if ((dt.Rows[i]["PrjCode"] != null) && (dt.Rows[i]["PrjCode"].ToString() != ""))
                    {
                        item.PrjCode = new Guid(dt.Rows[i]["PrjCode"].ToString());
                    }
                    if ((dt.Rows[i]["AffairClass"] != null) && (dt.Rows[i]["AffairClass"].ToString() != ""))
                    {
                        item.AffairClass = dt.Rows[i]["AffairClass"].ToString();
                    }
                    if ((dt.Rows[i]["AffairTitle"] != null) && (dt.Rows[i]["AffairTitle"].ToString() != ""))
                    {
                        item.AffairTitle = dt.Rows[i]["AffairTitle"].ToString();
                    }
                    if ((dt.Rows[i]["AddDate"] != null) && (dt.Rows[i]["AddDate"].ToString() != ""))
                    {
                        item.AddDate = DateTime.Parse(dt.Rows[i]["AddDate"].ToString());
                    }
                    if ((dt.Rows[i]["Remark"] != null) && (dt.Rows[i]["Remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["Remark"].ToString();
                    }
                    if ((dt.Rows[i]["Context"] != null) && (dt.Rows[i]["Context"].ToString() != ""))
                    {
                        item.Context = dt.Rows[i]["Context"].ToString();
                    }
                    if ((dt.Rows[i]["DatumClass"] != null) && (dt.Rows[i]["DatumClass"].ToString() != ""))
                    {
                        item.DatumClass = int.Parse(dt.Rows[i]["DatumClass"].ToString());
                    }
                    if ((dt.Rows[i]["CA"] != null) && (dt.Rows[i]["CA"].ToString() != ""))
                    {
                        item.CA = new int?(int.Parse(dt.Rows[i]["CA"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(Guid i_id)
        {
            return this.dal.Delete(i_id);
        }

        public bool DeleteList(string i_idlist)
        {
            return this.dal.DeleteList(i_idlist);
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

        public int getMaxID(string table)
        {
            return this.dal.getMaxID(table);
        }

        public int getMaxID(string table, string idClomname)
        {
            return this.dal.getMaxID(table, idClomname);
        }

        public DatumModel GetModel(Guid i_id)
        {
            return this.dal.GetModel(i_id);
        }

        public List<DatumModel> GetModelList(string strWhere)
        {
            DataSet set = new DataSet();
            DataTable list = this.dal.GetList(strWhere);
            set.Tables.Add(list);
            return this.DataTableToList(set.Tables[0]);
        }

        public static bool IsNumber(string inputData)
        {
            return RegNumber.Match(inputData).Success;
        }

        public bool UpdateByID(string tablename, string liename, string id, SqlTransaction trans)
        {
            string str = string.Empty;
            string[] strArray = id.Split(new char[] { ',' });
            if (strArray.Length > 0)
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        str = str + "'" + strArray[i].ToString() + "',";
                    }
                }
            }
            str = str.Remove(str.Length - 1);
            return this.dal.UpdateByID(tablename, liename, str, trans);
        }

        public bool UpdateByID(string tablename, int markVal, string idname, string idval, SqlTransaction trans)
        {
            string str = string.Empty;
            string[] strArray = idval.Split(new char[] { ',' });
            if (strArray.Length > 0)
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        str = str + "'" + strArray[i].ToString() + "',";
                    }
                }
            }
            str = str.Remove(str.Length - 1);
            return this.dal.UpdateByID(tablename, markVal, idname, str, trans);
        }

        public bool UpdateByID(string tablename, string setName_val, string idname, string idval, SqlTransaction trans)
        {
            string str = string.Empty;
            if (!IsNumber(idval))
            {
                string[] strArray = idval.Split(new char[] { ',' });
                if (strArray.Length > 0)
                {
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (strArray[i].ToString() != "")
                        {
                            str = str + "'" + strArray[i].ToString() + "',";
                        }
                    }
                }
                str = str.Remove(str.Length - 1);
            }
            else
            {
                str = idval;
            }
            return this.dal.UpdateByID(tablename, setName_val, idname, str, trans);
        }
    }
}

