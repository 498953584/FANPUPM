namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAFileClassesAction
    {
        public int Add(OAFileClasses model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_File_Classes(");
            builder.Append("LibraryCode,ClassCode,ClassName,Remark,IsValid");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.LibraryCode + "',");
            builder.Append("'" + model.ClassCode + "',");
            builder.Append("'" + model.ClassName + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.IsValid + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string LibraryCode, string ClassCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_File_Classes ");
            builder.Append(" where ");
            builder.Append(" LibraryCode='" + LibraryCode + "' ");
            builder.Append(" and ");
            builder.Append(" ClassCode='" + ClassCode + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int ClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_File_Classes where ClassID=" + ClassID);
            if (publicDbOpClass.ExecuteScalar(builder.ToString()) == DBNull.Value)
            {
                return false;
            }
            return true;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ClassID,LibraryCode,ClassCode,ClassName,Remark,IsValid ");
            builder.Append(" FROM OA_File_Classes ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAFileClasses GetModel(int ClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" ClassID,LibraryCode,ClassCode,ClassName,Remark,IsValid ");
            builder.Append(" from OA_File_Classes ");
            builder.Append(" where ClassID=" + ClassID);
            OAFileClasses classes = new OAFileClasses();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ClassID"].ToString() != "")
            {
                classes.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            classes.LibraryCode = set.Tables[0].Rows[0]["LibraryCode"].ToString();
            classes.ClassCode = set.Tables[0].Rows[0]["ClassCode"].ToString();
            classes.ClassName = set.Tables[0].Rows[0]["ClassName"].ToString();
            classes.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            classes.IsValid = set.Tables[0].Rows[0]["IsValid"].ToString();
            return classes;
        }

        public int Update(OAFileClasses model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OA_File_Classes set ");
            builder.Append(" ClassName='" + model.ClassName + "', ");
            builder.Append(" Remark='" + model.Remark + "' ");
            builder.Append(" where ");
            builder.Append(" LibraryCode='" + model.LibraryCode + "' and ");
            builder.Append(" ClassCode='" + model.ClassCode + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

