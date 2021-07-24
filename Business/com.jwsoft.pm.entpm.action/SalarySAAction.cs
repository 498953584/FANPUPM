namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class SalarySAAction
    {
        public int Add(SalarySAModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Salary_SingleAdjust(");
            builder.Append("AuditState,EmployeeCode,DeptName,DutyName,OldSalary,NewSalary,IsConfirm,UserCode,RecordDate,AdjustReason,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.AuditState + ",");
            builder.Append("'" + model.EmployeeCode + "',");
            builder.Append("'" + model.DeptName + "',");
            builder.Append("'" + model.DutyName + "',");
            builder.Append(model.OldSalary + ",");
            builder.Append(model.NewSalary + ",");
            builder.Append("'" + model.IsConfirm + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.AdjustReason + "',");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Salary_SingleAdjust ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,AuditState,EmployeeCode,DeptName,DutyName,OldSalary,NewSalary,IsConfirm,UserCode,RecordDate,AdjustReason,Remark ");
            builder.Append(" FROM HR_Salary_SingleAdjust ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public SalarySAModel GetModel(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" RecordID,AuditState,EmployeeCode,DeptName,DutyName,OldSalary,NewSalary,IsConfirm,UserCode,RecordDate,AdjustReason,Remark ");
            builder.Append(" from HR_Salary_SingleAdjust ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            SalarySAModel model = new SalarySAModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                model.RecordID = new Guid(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["AuditState"].ToString() != "")
            {
                model.AuditState = new int?(int.Parse(set.Tables[0].Rows[0]["AuditState"].ToString()));
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
            model.IsConfirm = set.Tables[0].Rows[0]["IsConfirm"].ToString();
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                model.RecordDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString()));
            }
            model.AdjustReason = set.Tables[0].Rows[0]["AdjustReason"].ToString();
            model.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            return model;
        }

        public decimal OldSalary(string yhdm)
        {
            string sqlString = "";
            sqlString = " select TempletID ,ItemID from HR_Salary_TempletItem where ItemID = (select ItemID from HR_Salary_Item where ItemType = 0)";
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                DataTable table2 = publicDbOpClass.DataTableQuary(" select F" + row["ItemID"].ToString() + " from HR_Salary_EST_" + row["TempletID"].ToString() + " where UserCode = '" + yhdm + "'");
                if (table2.Rows.Count > 0)
                {
                    return Convert.ToDecimal(table2.Rows[0][0]);
                }
            }
            return 0M;
        }

        public DataTable PersonnelInfo(string yhdm)
        {
            string str = "SELECT [v_yhdm], [v_xm], [i_bmdm],(select b.v_bmmc from pt_d_bm b where b.i_bmdm = a.i_bmdm) as bmmc, ";
            return publicDbOpClass.DataTableQuary((str + " [I_DUTYID],(select b.RoleTypeName from pt_d_role b where b.RoleTypeCode = (select c.DutyCode from pt_duty c where c.i_dutyId = a.i_dutyId)) as DutyName ") + " FROM [PT_yhmc] a WHERE a.c_sfyx = 'y' and a.v_yhdm = '" + yhdm + "' order by v_xm");
        }

        public int Update(SalarySAModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Salary_SingleAdjust set ");
            builder.Append("AuditState=" + model.AuditState + ",");
            builder.Append("EmployeeCode='" + model.EmployeeCode + "',");
            builder.Append("DeptName='" + model.DeptName + "',");
            builder.Append("DutyName='" + model.DutyName + "',");
            builder.Append("OldSalary=" + model.OldSalary + ",");
            builder.Append("NewSalary=" + model.NewSalary + ",");
            builder.Append("IsConfirm='" + model.IsConfirm + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("AdjustReason='" + model.AdjustReason + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where RecordID='" + model.RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

