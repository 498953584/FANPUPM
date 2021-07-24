namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OABooksLibraryAction
    {
        public int Add(OABooksLibrary model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Books_Library(");
            builder.Append("LibraryCode,LibraryName,Content,IsValid,Manager");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.LibraryCode + "',");
            builder.Append("'" + model.LibraryName + "',");
            builder.Append("'" + model.Content + "',");
            builder.Append("'" + model.IsValid + "',");
            builder.Append("'" + model.Manager + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string LibraryCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_Books_Library ");
            builder.Append(" where LibraryCode='" + LibraryCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(string LibraryCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 LibraryCode from OA_Books_Library where LibraryCode='" + LibraryCode + "'");
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select LibraryCode,LibraryName,Content,IsValid,Manager ");
            builder.Append(" FROM OA_Books_Library ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_Books_Library", "LibraryCode");
        }

        public OABooksLibrary GetModel(string LibraryCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" LibraryCode,LibraryName,Content,IsValid,Manager ");
            builder.Append(" from OA_Books_Library ");
            builder.Append(" where LibraryCode='" + LibraryCode + "'");
            OABooksLibrary library = new OABooksLibrary();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count > 0)
            {
                library.LibraryCode = set.Tables[0].Rows[0]["LibraryCode"].ToString();
                library.LibraryName = set.Tables[0].Rows[0]["LibraryName"].ToString();
                library.Content = set.Tables[0].Rows[0]["Content"].ToString();
                library.IsValid = set.Tables[0].Rows[0]["IsValid"].ToString();
                library.Manager = set.Tables[0].Rows[0]["Manager"].ToString();
                return library;
            }
            return null;
        }

        public string GetNewLibraryCode()
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select max(LibraryCode) from OA_Books_Library ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                str = Convert.ToString((int) (int.Parse((string) obj2) + 1));
                while (str.Length < ((string) obj2).Length)
                {
                    str = "0" + str;
                }
                return str;
            }
            return "001";
        }

        public int PopedomPersonSet(string LibraryCode, string PopedomPerson)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Books_Library set ");
            builder.Append("Manager='" + PopedomPerson + "'");
            builder.Append(" where LibraryCode='" + LibraryCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Update(OABooksLibrary model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Books_Library set ");
            builder.Append("LibraryName='" + model.LibraryName + "',");
            builder.Append("Content='" + model.Content + "'");
            builder.Append(" where LibraryCode='" + model.LibraryCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

