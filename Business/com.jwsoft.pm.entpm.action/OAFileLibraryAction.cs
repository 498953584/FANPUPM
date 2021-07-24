namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAFileLibraryAction
    {
        public int Add(OAFileLibrary model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_File_Library(");
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
            builder.Append("delete OA_File_Library ");
            builder.Append(" where LibraryCode='" + LibraryCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(string LibraryCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_File_Library where LibraryCode='" + LibraryCode + "'");
            if (publicDbOpClass.ExecuteScalar(builder.ToString()) == DBNull.Value)
            {
                return false;
            }
            return true;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select LibraryCode,LibraryName,Content,IsValid,Manager ");
            builder.Append(" FROM OA_File_Library ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_File_Library", "LibraryCode");
        }

        public OAFileLibrary GetModel(string LibraryCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" LibraryCode,LibraryName,Content,IsValid,Manager ");
            builder.Append(" from OA_File_Library ");
            builder.Append(" where LibraryCode='" + LibraryCode + "'");
            OAFileLibrary library = new OAFileLibrary();
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

        public int PopedomPersonSet(string LibraryCode, string PopedomPerson)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_File_Library set ");
            builder.Append("Manager='" + PopedomPerson + "'");
            builder.Append(" where LibraryCode='" + LibraryCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Update(OAFileLibrary model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_File_Library set ");
            builder.Append("LibraryName='" + model.LibraryName + "',");
            builder.Append("Content='" + model.Content + "',");
            builder.Append("IsValid='" + model.IsValid + "',");
            builder.Append("Manager='" + model.Manager + "'");
            builder.Append(" where LibraryCode='" + model.LibraryCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

