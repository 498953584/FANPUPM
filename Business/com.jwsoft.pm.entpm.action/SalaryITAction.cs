namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class SalaryITAction
    {
        public int Add(SalaryIncomeTaxModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Salary_IncomeTax(");
            builder.Append("LowerLimit,UpperLimit,TaxRate,Deduct");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.LowerLimit + ",");
            builder.Append(model.UpperLimit + ",");
            builder.Append(model.TaxRate + ",");
            builder.Append(model.Deduct);
            builder.Append(")");
            builder.Append(";select @@IDENTITY");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int TaxRateID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Salary_IncomeTax ");
            builder.Append(" where TaxRateID=" + TaxRateID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TaxRateID,LowerLimit,UpperLimit,TaxRate,Deduct ");
            builder.Append(" FROM HR_Salary_IncomeTax ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public SalaryIncomeTaxModel GetModel(int TaxRateID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" TaxRateID,LowerLimit,UpperLimit,TaxRate,Deduct ");
            builder.Append(" from HR_Salary_IncomeTax ");
            builder.Append(" where TaxRateID=" + TaxRateID + " ");
            SalaryIncomeTaxModel model = new SalaryIncomeTaxModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["TaxRateID"].ToString() != "")
            {
                model.TaxRateID = int.Parse(set.Tables[0].Rows[0]["TaxRateID"].ToString());
            }
            if (set.Tables[0].Rows[0]["LowerLimit"].ToString() != "")
            {
                model.LowerLimit = new int?(int.Parse(set.Tables[0].Rows[0]["LowerLimit"].ToString()));
            }
            if (set.Tables[0].Rows[0]["UpperLimit"].ToString() != "")
            {
                model.UpperLimit = new int?(int.Parse(set.Tables[0].Rows[0]["UpperLimit"].ToString()));
            }
            if (set.Tables[0].Rows[0]["TaxRate"].ToString() != "")
            {
                model.TaxRate = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["TaxRate"].ToString()));
            }
            if (set.Tables[0].Rows[0]["Deduct"].ToString() != "")
            {
                model.Deduct = new int?(int.Parse(set.Tables[0].Rows[0]["Deduct"].ToString()));
            }
            return model;
        }

        public int Update(SalaryIncomeTaxModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Salary_IncomeTax set ");
            builder.Append("LowerLimit=" + model.LowerLimit + ",");
            builder.Append("UpperLimit=" + model.UpperLimit + ",");
            builder.Append("TaxRate=" + model.TaxRate + ",");
            builder.Append("Deduct=" + model.Deduct);
            builder.Append(" where TaxRateID=" + model.TaxRateID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

