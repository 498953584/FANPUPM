namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class PrivProjectListAction
    {
        public int Add(PrivProjectList model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CPM_Priv_ProjectList(");
            builder.Append("BussinessCode,UserCode,ProjectID");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.BussinessCode + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append(model.ProjectID);
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Add(DataTable dt)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                builder.Append(" insert into CPM_Priv_Manager(");
                builder.Append("BussinessCode,UserCode");
                builder.Append(")");
                builder.Append(" values (");
                builder.Append("'" + dt.Rows[i]["BussinessCode"].ToString() + "',");
                builder.Append("'" + dt.Rows[i]["UserCode"].ToString() + "'");
                builder.Append(") ");
            }
            builder.Append(" end");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int AddList(DataTable dt)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                builder.Append(" insert into CPM_Priv_ProjectList(");
                builder.Append("BussinessCode,UserCode,ProjectID");
                builder.Append(")");
                builder.Append(" values (");
                builder.Append("'" + dt.Rows[i]["BussinessCode"].ToString() + "',");
                builder.Append("'" + dt.Rows[i]["UserCode"].ToString() + "',");
                builder.Append(dt.Rows[i]["ProjectID"].ToString() ?? "");
                builder.Append(") ");
            }
            builder.Append(" end");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string BussinessCode, string UserCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete CPM_Priv_ProjectList ");
            builder.Append(" where BussinessCode='" + BussinessCode + "' and UserCode='" + UserCode + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select BussinessCode,UserCode,ProjectID ");
            builder.Append(" FROM CPM_Priv_ProjectList ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public PrivProjectList GetModel(string BussinessCode, string UserCode, int ProjectID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" BussinessCode,UserCode,ProjectID ");
            builder.Append(" from CPM_Priv_ProjectList ");
            builder.Append(string.Concat(new object[] { " where BussinessCode='", BussinessCode, "' and UserCode='", UserCode, "' and ProjectID=", ProjectID, " " }));
            PrivProjectList list = new PrivProjectList();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            list.BussinessCode = set.Tables[0].Rows[0]["BussinessCode"].ToString();
            list.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["ProjectID"].ToString() != "")
            {
                list.ProjectID = int.Parse(set.Tables[0].Rows[0]["ProjectID"].ToString());
            }
            return list;
        }

        public int Update(PrivProjectList model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CPM_Priv_ProjectList set ");
            builder.Append("BussinessCode='" + model.BussinessCode + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("ProjectID=" + model.ProjectID);
            builder.Append(string.Concat(new object[] { " where BussinessCode='", model.BussinessCode, "' and UserCode='", model.UserCode, "' and ProjectID=", model.ProjectID, " " }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

