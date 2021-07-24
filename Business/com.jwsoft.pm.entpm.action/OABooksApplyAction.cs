namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OABooksApplyAction
    {
        public int Add(OABooksApply model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { " if not exists(select top 1 ISBN from OA_Books_Apply where ApplyID='", model.ApplyID, "' and ISBN='", model.ISBN, "') " }));
            builder.Append(" begin ");
            builder.Append(" insert into OA_Books_Apply( ");
            builder.Append(" ApplyID,ISBN,IsExist,ClassID,BookTitle,PublishingHouse,Author,Copy,Price,IsInStorage ");
            builder.Append(" ) ");
            builder.Append(" values ( ");
            builder.Append(" '" + model.ApplyID + "', ");
            builder.Append(" '" + model.ISBN + "', ");
            builder.Append(" '" + model.IsExist + "', ");
            builder.Append(" " + model.ClassID + ", ");
            builder.Append(" '" + model.BookTitle + "', ");
            builder.Append(" '" + model.PublishingHouse + "', ");
            builder.Append(" '" + model.Author + "', ");
            builder.Append(" " + model.Copy + ", ");
            builder.Append(" " + model.Price + ", ");
            builder.Append(" '" + model.IsInStorage + "' ");
            builder.Append(" ) ");
            builder.Append(" end ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string isbn)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_Books_Apply ");
            builder.Append(" where ISBN='" + isbn + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(Guid applyID, string isbn)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "select top 1 ISBN from OA_Books_Apply where ApplyID='", applyID, "' and ISBN='", isbn, "'" }));
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetBookStorage(string strSIBN)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OA_Books_Storage ");
            if (strSIBN.Trim() != "")
            {
                builder.Append(" where ISBN='" + strSIBN + "' ");
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ApplyID,ISBN,IsExist,ClassID,BookTitle,PublishingHouse,Author,Copy,Price,IsInStorage ");
            builder.Append(" FROM OA_Books_Apply ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_Books_Apply", "ApplyID");
        }

        public OABooksApply GetModel(Guid ApplyID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" ApplyID,ISBN,IsExist,ClassID,BookTitle,PublishingHouse,Author,Copy,Price,IsInStorage ");
            builder.Append(" from OA_Books_Apply ");
            builder.Append(" where ApplyID=" + ApplyID);
            OABooksApply apply = new OABooksApply();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ApplyID"].ToString() != "")
            {
                apply.ApplyID = new Guid(set.Tables[0].Rows[0]["ApplyID"].ToString());
            }
            apply.ISBN = set.Tables[0].Rows[0]["ISBN"].ToString();
            apply.IsExist = set.Tables[0].Rows[0]["IsExist"].ToString();
            if (set.Tables[0].Rows[0]["ClassID"].ToString() != "")
            {
                apply.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            apply.BookTitle = set.Tables[0].Rows[0]["BookTitle"].ToString();
            apply.PublishingHouse = set.Tables[0].Rows[0]["PublishingHouse"].ToString();
            apply.Author = set.Tables[0].Rows[0]["Author"].ToString();
            if (set.Tables[0].Rows[0]["Copy"].ToString() != "")
            {
                apply.Copy = int.Parse(set.Tables[0].Rows[0]["Copy"].ToString());
            }
            if (set.Tables[0].Rows[0]["Price"].ToString() != "")
            {
                apply.Price = decimal.Parse(set.Tables[0].Rows[0]["Price"].ToString());
            }
            apply.IsInStorage = set.Tables[0].Rows[0]["IsInStorage"].ToString();
            return apply;
        }

        public int Update(OABooksApply model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Books_Apply set ");
            builder.Append("IsExist='" + model.IsExist + "',");
            builder.Append("ClassID='" + model.ClassID + "',");
            builder.Append("ISBN='" + model.ISBN + "',");
            builder.Append("BookTitle='" + model.BookTitle + "',");
            builder.Append("PublishingHouse='" + model.PublishingHouse + "',");
            builder.Append("Author='" + model.Author + "',");
            builder.Append("Copy=" + model.Copy + ",");
            builder.Append("Price=" + model.Price + ",");
            builder.Append("IsInStorage='" + model.IsInStorage + "'");
            builder.Append(" where ApplyID='" + model.ApplyID + "'");
            builder.Append(" and ISBN='" + model.ISBN + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

