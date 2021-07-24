namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class SalaryPAction
    {
        public int Add(SalaryPModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Salary_Parameter(");
            builder.Append("ParameterCode,ParameterName,ParameterValue,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.ParameterCode + "',");
            builder.Append("'" + model.ParameterName + "',");
            builder.Append(model.ParameterValue + ",");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string ParameterCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Salary_Parameter ");
            builder.Append(" where ParameterCode='" + ParameterCode + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ParameterCode,ParameterName,ParameterValue,Remark ");
            builder.Append(" FROM HR_Salary_Parameter ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public SalaryPModel GetModel(string ParameterCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" ParameterCode,ParameterName,ParameterValue,Remark ");
            builder.Append(" from HR_Salary_Parameter ");
            builder.Append(" where ParameterCode='" + ParameterCode + "' ");
            SalaryPModel model = new SalaryPModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            model.ParameterCode = set.Tables[0].Rows[0]["ParameterCode"].ToString();
            model.ParameterName = set.Tables[0].Rows[0]["ParameterName"].ToString();
            if (set.Tables[0].Rows[0]["ParameterValue"].ToString() != "")
            {
                model.ParameterValue = new int?(int.Parse(set.Tables[0].Rows[0]["ParameterValue"].ToString()));
            }
            model.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            return model;
        }

        public int Update(SalaryPModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Salary_Parameter set ");
            builder.Append("ParameterName='" + model.ParameterName + "',");
            builder.Append("ParameterValue=" + model.ParameterValue + ",");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where ParameterCode='" + model.ParameterCode + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

