namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class OABooksInStorageAction
    {
        public int Add(OABooksInStorage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ");
            builder.Append(" if not exists(select top 1 ISBN from OA_Books_InStorage where LibraryCode='" + model.LibraryCode + "' and ISBN='" + model.ISBN + "') ");
            builder.Append(" begin ");
            builder.Append(" insert into OA_Books_InStorage(");
            builder.Append(" ISBN,LibraryCode,ClassID,BookTitle,PublishingHouse,Author,Copy,UserCode,RecordDate");
            builder.Append(") ");
            builder.Append(" values (");
            builder.Append(" '" + model.ISBN + "', ");
            builder.Append(" '" + model.LibraryCode + "', ");
            builder.Append(" " + model.ClassID + ", ");
            builder.Append(" '" + model.BookTitle + "', ");
            builder.Append(" '" + model.PublishingHouse + "', ");
            builder.Append(" '" + model.Author + "', ");
            builder.Append(" " + model.Copy + ", ");
            builder.Append(" '" + model.UserCode + "',");
            builder.Append(" '" + model.RecordDate + "' ");
            builder.Append(" ) ");
            builder.Append(" if not exists(select top 1 ISBN from OA_Books_Storage where LibraryCode='" + model.LibraryCode + "' and ISBN='" + model.ISBN + "') ");
            builder.Append(" begin ");
            builder.Append(" insert into OA_Books_Storage(");
            builder.Append(" LibraryCode,ISBN,ClassID,BookTitle,PublishingHouse,Author,SumCopy,LeaveCopy");
            builder.Append(" ) ");
            builder.Append(" values (");
            builder.Append(" '" + model.LibraryCode + "',");
            builder.Append(" '" + model.ISBN + "',");
            builder.Append(" " + model.ClassID + ", ");
            builder.Append(" '" + model.BookTitle + "', ");
            builder.Append(" '" + model.PublishingHouse + "', ");
            builder.Append(" '" + model.Author + "', ");
            builder.Append(" " + model.Copy + ", ");
            builder.Append(" " + model.Copy + " ");
            builder.Append(" ) ");
            builder.Append(" end ");
            builder.Append(" else ");
            builder.Append(" begin ");
            builder.Append("update OA_Books_Storage set ");
            builder.Append("SumCopy=SumCopy+" + model.Copy + ", ");
            builder.Append("LeaveCopy=LeaveCopy+" + model.Copy + " ");
            builder.Append(" where LibraryCode='" + model.LibraryCode + "' and ISBN='" + model.ISBN + "'");
            builder.Append(" end ");
            builder.Append(" end ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Add(ArrayList arr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ");
            for (int i = 0; i < arr.Count; i++)
            {
                builder.Append(" if not exists(select top 1 ISBN from OA_Books_InStorage where LibraryCode='" + ((OABooksInStorage) arr[i]).LibraryCode + "' and ISBN='" + ((OABooksInStorage) arr[i]).ISBN + "') ");
                builder.Append(" begin ");
                builder.Append(" insert into OA_Books_InStorage(");
                builder.Append(" ISBN,LibraryCode,ClassID,BookTitle,PublishingHouse,Author,Copy,UserCode,RecordDate ");
                builder.Append(" ) ");
                builder.Append(" values (");
                builder.Append(" '" + ((OABooksInStorage) arr[i]).ISBN + "', ");
                builder.Append(" '" + ((OABooksInStorage) arr[i]).LibraryCode + "', ");
                builder.Append(" " + ((OABooksInStorage) arr[i]).ClassID + ", ");
                builder.Append(" '" + ((OABooksInStorage) arr[i]).BookTitle + "', ");
                builder.Append(" '" + ((OABooksInStorage) arr[i]).PublishingHouse + "', ");
                builder.Append(" '" + ((OABooksInStorage) arr[i]).Author + "', ");
                builder.Append(" " + ((OABooksInStorage) arr[i]).Copy + ", ");
                builder.Append("  '" + ((OABooksInStorage) arr[i]).UserCode + "',");
                builder.Append(" '" + ((OABooksInStorage) arr[i]).RecordDate + "' ");
                builder.Append(" ) ");
                builder.Append(" if not exists(select top 1 ISBN from OA_Books_Storage where LibraryCode='" + ((OABooksInStorage) arr[i]).LibraryCode + "' and ISBN='" + ((OABooksInStorage) arr[i]).ISBN + "') ");
                builder.Append(" begin ");
                builder.Append(" insert into OA_Books_Storage( ");
                builder.Append(" LibraryCode,ISBN,ClassID,BookTitle,PublishingHouse,Author,SumCopy,LeaveCopy ");
                builder.Append(" ) ");
                builder.Append(" values ( ");
                builder.Append(" '" + ((OABooksInStorage) arr[i]).LibraryCode + "',");
                builder.Append(" '" + ((OABooksInStorage) arr[i]).ISBN + "',");
                builder.Append(" " + ((OABooksInStorage) arr[i]).ClassID + ", ");
                builder.Append(" '" + ((OABooksInStorage) arr[i]).BookTitle + "', ");
                builder.Append(" '" + ((OABooksInStorage) arr[i]).PublishingHouse + "', ");
                builder.Append(" '" + ((OABooksInStorage) arr[i]).Author + "', ");
                builder.Append(" " + ((OABooksInStorage) arr[i]).Copy + ", ");
                builder.Append(" " + ((OABooksInStorage) arr[i]).Copy + " ");
                builder.Append(" ) ");
                builder.Append(" end ");
                builder.Append(" else ");
                builder.Append(" begin ");
                builder.Append(" update OA_Books_Storage set ");
                builder.Append(" SumCopy=SumCopy+" + ((OABooksInStorage) arr[i]).Copy + ", ");
                builder.Append(" LeaveCopy=LeaveCopy+" + ((OABooksInStorage) arr[i]).Copy + " ");
                builder.Append(" where LibraryCode='" + ((OABooksInStorage) arr[i]).LibraryCode + "' and ISBN='" + ((OABooksInStorage) arr[i]).ISBN + "' ");
                builder.Append(" end ");
                builder.Append(" update OA_Books_Apply set ");
                builder.Append(" IsInStorage='1', ");
                builder.Append(" ClassID=isnull((select ClassID from OA_Books_Storage where ISBN='" + ((OABooksInStorage) arr[i]).ISBN + "'),0) ");
                builder.Append(" where ISBN='" + ((OABooksInStorage) arr[i]).ISBN + "' ");
                builder.Append(" end ");
            }
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Books_Storage set ");
            builder.Append("SumCopy=SumCopy-(select isnull(Copy,0) from OA_Books_InStorage where RecordID='" + RecordID + "'), ");
            builder.Append("LeaveCopy=LeaveCopy-(select isnull(Copy,0) from OA_Books_InStorage where RecordID='" + RecordID + "') ");
            builder.Append(" where LibraryCode=(select LibraryCode from OA_Books_InStorage where RecordID='" + RecordID + "')");
            builder.Append(" and ISBN=(select ISBN from OA_Books_InStorage where RecordID='" + RecordID + "')");
            builder.Append("delete OA_Books_InStorage ");
            builder.Append(" where RecordID=" + RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 RecordID from OA_Books_InStorage where RecordID=" + RecordID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetClassType()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM PT_MultiClasses WHERE ClassTypeCode='001' ORDER BY ClassCode asc ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetLibraryAddress(string yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM OA_Books_Library where Manager='" + yhdm + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetLibraryName(string lc)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select LibraryName FROM OA_Books_Library ");
            builder.Append(" where LibraryCode='" + lc + "' ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                return (string) obj2;
            }
            return "";
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,LibraryCode,ISBN,ClassID,BookTitle,PublishingHouse,Author,Copy,UserCode,RecordDate ");
            builder.Append(" FROM OA_Books_InStorage ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_Books_InStorage", "RecordID");
        }

        public OABooksInStorage GetModel(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" RecordID,ISBN,ClassID,BookTitle,PublishingHouse,Author,Copy,UserCode,RecordDate ");
            builder.Append(" from OA_Books_InStorage ");
            builder.Append(" where RecordID=" + RecordID);
            OABooksInStorage storage = new OABooksInStorage();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                storage.RecordID = int.Parse(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            storage.ISBN = set.Tables[0].Rows[0]["ISBN"].ToString();
            if (set.Tables[0].Rows[0]["ClassID"].ToString() != "")
            {
                storage.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            storage.BookTitle = set.Tables[0].Rows[0]["BookTitle"].ToString();
            storage.PublishingHouse = set.Tables[0].Rows[0]["PublishingHouse"].ToString();
            storage.Author = set.Tables[0].Rows[0]["Author"].ToString();
            if (set.Tables[0].Rows[0]["Copy"].ToString() != "")
            {
                storage.Copy = int.Parse(set.Tables[0].Rows[0]["Copy"].ToString());
            }
            storage.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                storage.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            return storage;
        }

        public int Update(OABooksInStorage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ");
            builder.Append(" if not exists(select top 1 ISBN from OA_Books_Storage where LibraryCode='" + model.LibraryCode + "' and ISBN='" + model.ISBN + "') ");
            builder.Append(" begin ");
            builder.Append(" insert into OA_Books_Storage(");
            builder.Append(" LibraryCode,ISBN,ClassID,BookTitle,PublishingHouse,Author,SumCopy,LeaveCopy");
            builder.Append(" ) ");
            builder.Append(" values ( ");
            builder.Append(" '" + model.LibraryCode + "', ");
            builder.Append(" '" + model.ISBN + "',");
            builder.Append(" " + model.ClassID + ", ");
            builder.Append(" '" + model.BookTitle + "', ");
            builder.Append(" '" + model.PublishingHouse + "', ");
            builder.Append(" '" + model.Author + "', ");
            builder.Append(" " + model.Copy + ", ");
            builder.Append(" " + model.Copy + "  ");
            builder.Append(" ) ");
            builder.Append(" end ");
            builder.Append(" else ");
            builder.Append(" begin ");
            builder.Append(" update OA_Books_Storage set ");
            builder.Append(" SumCopy=SumCopy-(select isnull(Copy,0) from OA_Books_InStorage where LibraryCode='" + model.LibraryCode + "' and ISBN='" + model.ISBN + "'), ");
            builder.Append(" LeaveCopy=LeaveCopy-(select isnull(Copy,0) from OA_Books_InStorage where LibraryCode='" + model.LibraryCode + "' and ISBN='" + model.ISBN + "') ");
            builder.Append(" where ISBN='" + model.ISBN + "' ");
            builder.Append(" update OA_Books_Storage set ");
            builder.Append(" SumCopy=SumCopy+" + model.Copy + ", ");
            builder.Append(" LeaveCopy=LeaveCopy+" + model.Copy + " ");
            builder.Append(" where LibraryCode='" + model.LibraryCode + "' and ISBN='" + model.ISBN + "' ");
            builder.Append(" end ");
            builder.Append(" update OA_Books_InStorage set ");
            builder.Append(" ISBN='" + model.ISBN + "', ");
            builder.Append(" ClassID=" + model.ClassID + ", ");
            builder.Append(" LibraryCode='" + model.LibraryCode + "', ");
            builder.Append(" BookTitle='" + model.BookTitle + "', ");
            builder.Append(" PublishingHouse='" + model.PublishingHouse + "', ");
            builder.Append(" Author='" + model.Author + "', ");
            builder.Append(" Copy=" + model.Copy + " ");
            builder.Append(" where RecordID='" + model.RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

