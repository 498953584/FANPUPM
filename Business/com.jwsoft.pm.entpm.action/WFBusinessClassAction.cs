namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class WFBusinessClassAction
    {
        public int Add(WFBusinessClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into WF_Business_Class(");
            builder.Append("BusinessCode,BusinessClass,BusinessClassName");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.BusinessCode + "',");
            builder.Append("'" + model.BusinessClass + "',");
            builder.Append("'" + model.BusinessClassName + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string BusinessCode, string BusinessClass)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete WF_Business_Class ");
            builder.Append(" where BusinessCode='" + BusinessCode + "' ");
            builder.Append(" and BusinessClass='" + BusinessClass + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select BusinessCode,BusinessClass,BusinessClassName ");
            builder.Append(" FROM WF_Business_Class ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetNewBusinessClass(string strCode)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select max(BusinessClass) from WF_Business_Class where BusinessCode='" + strCode + "'");
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

        public DataTable GetTemplateList(string BusinessClass, string corpcode, string usercode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM (");
            builder.Append(" select * from wf_template where BusinessCode = '999' and BusinessClass = '" + BusinessClass + "' and IsGeneral = '1' and IsComplete='1' ");
            builder.Append(" union select * from wf_template where BusinessCode = '999' and BusinessClass = '" + BusinessClass + "' and IsGeneral = '0' and CorpCode = '" + corpcode + "' and IsComplete = '1'");
            builder.AppendFormat(") AS A WHERE TemplateID IN (SELECT TemplateID FROM WF_Template_Privilege WHERE UserCode='{0}') ", usercode);
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int Update(WFBusinessClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update WF_Business_Class set ");
            builder.Append("BusinessClassName='" + model.BusinessClassName + "'");
            builder.Append(" where BusinessCode='" + model.BusinessCode + "'");
            builder.Append(" and BusinessClass='" + model.BusinessClass + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

