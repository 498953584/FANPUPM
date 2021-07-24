namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class PTMultiClassesAction
    {
        public int Add(PTMultiClasses model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_MultiClasses(");
            builder.Append("ClassTypeCode,CorpCode,ClassCode,ParentClassCode,ClassName,Remark,IsValid");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.ClassTypeCode + "',");
            builder.Append("'" + model.CorpCode + "',");
            builder.Append("'" + this.GetNewClassCode(model) + "',");
            builder.Append("'" + model.ClassCode + "',");
            builder.Append("'" + model.ClassName + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.IsValid + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string ClassTypeCode, string ClassCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete PT_MultiClasses ");
            builder.Append(" where ClassTypeCode='" + ClassTypeCode + "' ");
            builder.Append(" and ClassCode='" + ClassCode + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int DeleteAllzd(string ClassCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete OA_System_Info ");
            builder.Append(" WHERE ClassID='" + ClassCode + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(string ClassCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from PT_MultiClasses where ClassCode='" + ClassCode + "'");
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public static string GetClassCodeByUserCode(string UserCode)
        {
            string str = "";
            if (UserCode != "")
            {
                DataTable table = publicDbOpClass.DataTableQuary("select classcode from  pt_multiclasses where purview like'%" + UserCode + "%'");
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    str = str + table.Rows[i]["classcode"].ToString() + ",";
                }
            }
            if (str.Length < 1)
            {
                return "-1";
            }
            return str.Substring(0, str.Length - 1);
        }

        public static string GetCompanyCodeOfUser(string usercode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select CorpCode from PT_d_CorpCode where CorpCode= ");
            builder.Append(" (select top 1 CorpCode from pt_d_bm where i_bmdm= ");
            builder.Append(" (select top 1 i_bmdm from pt_yhmc where v_yhdm='" + usercode + "')) ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                return (string) obj2;
            }
            return "";
        }

        public static string GetCompanyNameOfUser(string usercode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select CorpName from PT_d_CorpCode where CorpCode= ");
            builder.Append(" (select top 1 CorpCode from pt_d_bm where i_bmdm= ");
            builder.Append(" (select top 1 i_bmdm from pt_yhmc where v_yhdm='" + usercode + "')) ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                return (string) obj2;
            }
            return "";
        }

        public static string GetDepartmentCodeOfUser(string usercode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select v_bmbm from pt_d_bm where i_bmdm= ");
            builder.Append(" (select top 1 i_bmdm from pt_yhmc where v_yhdm='" + usercode + "') ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                return (string) obj2;
            }
            return "";
        }

        public static string GetDepartmentNameOfUser(string usercode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select V_BMMC from pt_d_bm where i_bmdm= ");
            builder.Append(" (select top 1 i_bmdm from pt_yhmc where v_yhdm='" + usercode + "') ");
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
            builder.Append(" select ClassID,ClassTypeCode,CorpCode,ClassCode,ParentClassCode,(replicate('&nbsp;',(len(ClassCode)/3-1)*4)+ClassName) as ClassName,Remark,IsValid ");
            builder.Append(" FROM PT_MultiClasses ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere + " order by ClassCode asc");
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public PTMultiClasses GetModel(string ClassCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" ClassID,ClassTypeCode,CorpCode,ClassCode,ParentClassCode,ClassName,Remark,IsValid ");
            builder.Append(" from PT_MultiClasses ");
            builder.Append(" where ClassCode='" + ClassCode + "'");
            PTMultiClasses classes = new PTMultiClasses();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ClassID"].ToString() != "")
            {
                classes.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            classes.ClassTypeCode = set.Tables[0].Rows[0]["ClassTypeCode"].ToString();
            classes.CorpCode = set.Tables[0].Rows[0]["CorpCode"].ToString();
            classes.ClassCode = set.Tables[0].Rows[0]["ClassCode"].ToString();
            classes.ParentClassCode = set.Tables[0].Rows[0]["ParentClassCode"].ToString();
            classes.ClassName = set.Tables[0].Rows[0]["ClassName"].ToString();
            classes.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            classes.IsValid = set.Tables[0].Rows[0]["IsValid"].ToString();
            return classes;
        }

        public static string GetNameOfUser(string usercode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select v_xm from pt_yhmc ");
            builder.Append(" where v_yhdm = '" + usercode + "' ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                return (string) obj2;
            }
            return "";
        }

        private string GetNewClassCode(PTMultiClasses model)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select max(ClassCode) from PT_MultiClasses ");
            builder.Append(" where ");
            builder.Append(" ClassTypeCode='" + model.ClassTypeCode + "' ");
            builder.Append(" and ");
            builder.Append(" ParentClassCode='" + model.ClassCode + "' ");
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
            return (model.ClassCode + "001");
        }

        public string multiName(string strWhere)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from PT_MultiClasses ");
            if (strWhere.Trim() != "")
            {
                builder.Append("where" + strWhere);
            }
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["ClassName"].ToString();
            }
            return str;
        }

        public int Update(PTMultiClasses model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_MultiClasses set ");
            builder.Append("ClassName='" + model.ClassName + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append("where ClassCode='" + model.ClassCode + "'");
            builder.Append("and ClassTypeCode='" + model.ClassTypeCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

