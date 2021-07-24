namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class SalaryGADAction
    {
        public int Add(SalaryGADModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Salary_GroupAdjustDetail(");
            builder.Append("RecordID,EmployeeCode,DeptName,DutyName,OldSalary,NewSalary,AdjustReason");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RecordID + "',");
            builder.Append("'" + model.EmployeeCode + "',");
            builder.Append("'" + model.DeptName + "',");
            builder.Append("'" + model.DutyName + "',");
            builder.Append(model.OldSalary + ",");
            builder.Append(model.NewSalary + ",");
            builder.Append("'" + model.AdjustReason + "'");
            builder.Append(")");
            builder.Append(";select @@IDENTITY");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int DetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Salary_GroupAdjustDetail ");
            builder.Append(" where DetailID=" + DetailID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select DetailID,RecordID,EmployeeCode,DeptName,DutyName,OldSalary,NewSalary,AdjustReason ");
            builder.Append(" FROM HR_Salary_GroupAdjustDetail ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public SalaryGADModel GetModel(int DetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" DetailID,RecordID,EmployeeCode,DeptName,DutyName,OldSalary,NewSalary,AdjustReason ");
            builder.Append(" from HR_Salary_GroupAdjustDetail ");
            builder.Append(" where DetailID=" + DetailID + " ");
            SalaryGADModel model = new SalaryGADModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["DetailID"].ToString() != "")
            {
                model.DetailID = int.Parse(set.Tables[0].Rows[0]["DetailID"].ToString());
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                model.RecordID = new Guid(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            model.EmployeeCode = set.Tables[0].Rows[0]["EmployeeCode"].ToString();
            model.DeptName = set.Tables[0].Rows[0]["DeptName"].ToString();
            model.DutyName = set.Tables[0].Rows[0]["DutyName"].ToString();
            if (set.Tables[0].Rows[0]["OldSalary"].ToString() != "")
            {
                model.OldSalary = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["OldSalary"].ToString()));
            }
            if (set.Tables[0].Rows[0]["NewSalary"].ToString() != "")
            {
                model.NewSalary = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["NewSalary"].ToString()));
            }
            model.AdjustReason = set.Tables[0].Rows[0]["AdjustReason"].ToString();
            return model;
        }

        public int Update(SalaryGADModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Salary_GroupAdjustDetail set ");
            builder.Append("RecordID='" + model.RecordID + "',");
            builder.Append("EmployeeCode='" + model.EmployeeCode + "',");
            builder.Append("DeptName='" + model.DeptName + "',");
            builder.Append("DutyName='" + model.DutyName + "',");
            builder.Append("OldSalary=" + model.OldSalary + ",");
            builder.Append("NewSalary=" + model.NewSalary + ",");
            builder.Append("AdjustReason='" + model.AdjustReason + "'");
            builder.Append(" where DetailID=" + model.DetailID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

