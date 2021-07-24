namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class OABooksStorageAction
    {
        public int Add(OABooksStorage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Books_Storage(");
            builder.Append("ISBN,ClassID,BookTitle,PublishingHouse,Author,SumCopy,LeaveCopy");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.ISBN + "',");
            builder.Append(model.ClassID + ",");
            builder.Append("'" + model.BookTitle + "',");
            builder.Append("'" + model.PublishingHouse + "',");
            builder.Append("'" + model.Author + "',");
            builder.Append(model.SumCopy + ",");
            builder.Append(model.LeaveCopy);
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string ISBN)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_Books_Storage ");
            builder.Append(" where ISBN='" + ISBN + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(string ISBN)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 ISBN from OA_Books_Storage where ISBN='" + ISBN + "'");
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public ArrayList GetLendBooks(string isbn)
        {
            ArrayList list = new ArrayList();
            StringBuilder builder = new StringBuilder();
            builder.Append("select ISBN,ClassID,BookTitle,PublishingHouse,Author,isnull(SumCopy,0) as SumCopy,isnull(LeaveCopy,0) as LeaveCopy from OA_Books_Storage where charindex(ISBN,'" + isbn + "')>0 ");
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    OABooksLend lend = new OABooksLend {
                        BookTitle = row["BookTitle"].ToString(),
                        BorrowMan = "",
                        Copy = 1,
                        ISBN = row["ISBN"].ToString(),
                        LendDate = DateTime.Now,
                        LendState = "1",
                        PlanReturnDate = DateTime.Now,
                        RecordID = 0,
                        ReturnApplyDate = DateTime.Now,
                        ReturnDate = DateTime.Now
                    };
                    list.Add(lend);
                }
            }
            return list;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ISBN,ClassID,BookTitle,PublishingHouse,Author,SumCopy,LeaveCopy,(SELECT top 1 LibraryName FROM OA_Books_Library where LibraryCode=dbo.OA_Books_Storage.LibraryCode) as LibraryName ");
            builder.Append(" FROM OA_Books_Storage ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_Books_Storage", "ISBN");
        }

        public OABooksStorage GetModel(string ISBN)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" ISBN,ClassID,BookTitle,PublishingHouse,Author,SumCopy,LeaveCopy ");
            builder.Append(" from OA_Books_Storage ");
            builder.Append(" where ISBN='" + ISBN + "'");
            OABooksStorage storage = new OABooksStorage();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            storage.ISBN = set.Tables[0].Rows[0]["ISBN"].ToString();
            if (set.Tables[0].Rows[0]["ClassID"].ToString() != "")
            {
                storage.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            storage.BookTitle = set.Tables[0].Rows[0]["BookTitle"].ToString();
            storage.PublishingHouse = set.Tables[0].Rows[0]["PublishingHouse"].ToString();
            storage.Author = set.Tables[0].Rows[0]["Author"].ToString();
            if (set.Tables[0].Rows[0]["SumCopy"].ToString() != "")
            {
                storage.SumCopy = int.Parse(set.Tables[0].Rows[0]["SumCopy"].ToString());
            }
            if (set.Tables[0].Rows[0]["LeaveCopy"].ToString() != "")
            {
                storage.LeaveCopy = int.Parse(set.Tables[0].Rows[0]["LeaveCopy"].ToString());
            }
            return storage;
        }

        public int Update(OABooksStorage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Books_Storage set ");
            builder.Append("ClassID=" + model.ClassID + ",");
            builder.Append("BookTitle='" + model.BookTitle + "',");
            builder.Append("PublishingHouse='" + model.PublishingHouse + "',");
            builder.Append("Author='" + model.Author + "',");
            builder.Append("SumCopy=" + model.SumCopy + ",");
            builder.Append("LeaveCopy=" + model.LeaveCopy);
            builder.Append(" where ISBN='" + model.ISBN + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

