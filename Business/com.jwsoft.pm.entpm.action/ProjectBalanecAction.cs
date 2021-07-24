namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class ProjectBalanecAction
    {
        public int Add(ProjectBalanec model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into EPM_ProjectBalanec(");
            builder.Append("ProjectCode,BalanecPrice,AuditPrice,BalanecDate");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.ProjectCode + "',");
            builder.Append(model.BalanecPrice + ",");
            builder.Append(model.AuditPrice + ",");
            builder.Append("'" + model.BalanecDate + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete EPM_ProjectBalanec ");
            builder.Append(" where ID=" + ID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from EPM_ProjectBalanec ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by ID ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ProjectBalanec GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from EPM_ProjectBalanec ");
            builder.Append(" where ID=" + ID);
            ProjectBalanec balanec = new ProjectBalanec();
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            balanec.id = ID;
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["id"].ToString() != "")
            {
                balanec.id = int.Parse(table.Rows[0]["id"].ToString());
            }
            balanec.ProjectCode = new Guid(table.Rows[0]["ProjectCode"].ToString());
            if (table.Rows[0]["BalanecPrice"].ToString() != "")
            {
                balanec.BalanecPrice = decimal.Parse(table.Rows[0]["BalanecPrice"].ToString());
            }
            if (table.Rows[0]["AuditPrice"].ToString() != "")
            {
                balanec.AuditPrice = decimal.Parse(table.Rows[0]["AuditPrice"].ToString());
            }
            if (table.Rows[0]["BalanecDate"].ToString() != "")
            {
                balanec.BalanecDate = DateTime.Parse(table.Rows[0]["BalanecDate"].ToString());
            }
            return balanec;
        }

        public int Update(ProjectBalanec model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update EPM_ProjectBalanec set ");
            builder.Append("BalanecPrice=" + model.BalanecPrice + ",");
            builder.Append("AuditPrice=" + model.AuditPrice + ",");
            builder.Append("BalanecDate='" + model.BalanecDate + "'");
            builder.Append("where ProjectCode='" + model.ProjectCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

