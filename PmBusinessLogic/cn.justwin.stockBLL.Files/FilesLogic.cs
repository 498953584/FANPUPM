namespace cn.justwin.stockBLL.Files
{
    using cn.justwin.Files;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;

    public class FilesLogic
    {
        private readonly filesService dal = new filesService();
        private static Regex RegNumber = new Regex("^[0-9]+$");

        public void Add(filesModel model, SqlTransaction trans)
        {
            this.dal.Add(model, trans);
        }

        public List<filesModel> DataTableToList(DataTable dt)
        {
            List<filesModel> list = new List<filesModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    filesModel item = new filesModel();
                    if ((dt.Rows[i]["ID"] != null) && (dt.Rows[i]["ID"].ToString() != ""))
                    {
                        item.ID = new Guid(dt.Rows[i]["ID"].ToString());
                    }
                    if ((dt.Rows[i]["file_sid"] != null) && (dt.Rows[i]["file_sid"].ToString() != ""))
                    {
                        item.file_sid = dt.Rows[i]["file_sid"].ToString();
                    }
                    if ((dt.Rows[i]["file_name"] != null) && (dt.Rows[i]["file_name"].ToString() != ""))
                    {
                        item.file_name = dt.Rows[i]["file_name"].ToString();
                    }
                    if ((dt.Rows[i]["file_info"] != null) && (dt.Rows[i]["file_info"].ToString() != ""))
                    {
                        item.file_info = dt.Rows[i]["file_info"].ToString();
                    }
                    if ((dt.Rows[i]["file_mark"] != null) && (dt.Rows[i]["file_mark"].ToString() != ""))
                    {
                        item.file_mark = new int?(int.Parse(dt.Rows[i]["file_mark"].ToString()));
                    }
                    if ((dt.Rows[i]["Original_table"] != null) && (dt.Rows[i]["Original_table"].ToString() != ""))
                    {
                        item.Original_table = dt.Rows[i]["Original_table"].ToString();
                    }
                    if ((dt.Rows[i]["Content"] != null) && (dt.Rows[i]["Content"].ToString() != ""))
                    {
                        item.Content = dt.Rows[i]["Content"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(Guid ID, SqlTransaction trans)
        {
            return this.dal.Delete(ID, trans);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public bool Exists(Guid ID, SqlTransaction trans)
        {
            return this.dal.Exists(ID, trans);
        }

        public bool Exists(string SID, SqlTransaction trans)
        {
            return this.dal.Exists(SID, trans);
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

        public DataTable GetList(string TABLENAME, string id, string rowName)
        {
            return this.dal.GetList(TABLENAME, id, rowName);
        }

        public filesModel GetModel(Guid ID)
        {
            return this.dal.GetModel(ID);
        }

        public filesModel getModelByRow(DataRow dr)
        {
            filesModel model = new filesModel();
            if ((dr["ID"] != null) && (dr["ID"].ToString() != ""))
            {
                model.ID = new Guid(dr["ID"].ToString());
            }
            model.file_sid = dr["file_sid"].ToString();
            model.file_name = dr["file_name"].ToString();
            model.file_info = dr["file_info"].ToString();
            model.Content = dr["Content"].ToString();
            if (((dr["file_mark"] != null) && (dr["file_mark"].ToString() != "")) && IsNumber(dr["file_mark"].ToString()))
            {
                model.file_mark = new int?(int.Parse(dr["file_mark"].ToString()));
            }
            if ((dr["Original_table"] != null) && (dr["Original_table"].ToString() != ""))
            {
                model.Original_table = dr["Original_table"].ToString();
            }
            if ((dr["sid_ColumnName"] != null) && (dr["sid_ColumnName"].ToString() != ""))
            {
                model.Sid_ColumnName = dr["sid_ColumnName"].ToString();
            }
            if ((dr["prjcode"] != null) && (dr["prjcode"].ToString() != ""))
            {
                model.Prjcode = dr["prjcode"].ToString();
            }
            if (((dr["sid_ColumnType"] != null) && (dr["sid_ColumnType"].ToString() != "")) && IsNumber(dr["sid_ColumnType"].ToString()))
            {
                model.Sid_ColumnType = int.Parse(dr["sid_ColumnType"].ToString());
            }
            return model;
        }

        public List<filesModel> GetModelList(string strWhere)
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

        public bool Update(filesModel model, SqlTransaction trans)
        {
            return this.dal.Update(model, trans);
        }

        public bool Update(string ids, int marks, string table_name, SqlTransaction trans)
        {
            return this.dal.Update(ids, marks, table_name, trans);
        }

        public bool Update(string idvals, string tableName, string rowName, int rowType, SqlTransaction trans)
        {
            return this.dal.Update(idvals, tableName, rowName, rowType, trans);
        }
    }
}

