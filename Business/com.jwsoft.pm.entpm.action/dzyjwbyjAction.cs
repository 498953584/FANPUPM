namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class dzyjwbyjAction
    {
        public int Add(dzyjwbyjModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pt_dzyj_wbyj(");
            builder.Append("v_yhdm,v_wbyjmc,v_password,v_xm");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.v_yhdm + "',");
            builder.Append("'" + model.v_wbyjmc + "',");
            builder.Append("'" + model.v_password + "',");
            builder.Append("'" + model.v_xm + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string v_yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pt_dzyj_wbyj ");
            builder.Append(" where v_yhdm='" + v_yhdm + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select v_yhdm,v_wbyjmc,v_password,v_xm ");
            builder.Append(" FROM pt_dzyj_wbyj ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public dzyjwbyjModel GetModel(string v_yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" v_yhdm,v_wbyjmc,v_password,v_xm ");
            builder.Append(" from pt_dzyj_wbyj ");
            builder.Append(" where v_yhdm='" + v_yhdm + "' ");
            dzyjwbyjModel model = new dzyjwbyjModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count > 0)
            {
                model.v_yhdm = set.Tables[0].Rows[0]["v_yhdm"].ToString();
                model.v_wbyjmc = set.Tables[0].Rows[0]["v_wbyjmc"].ToString();
                model.v_password = set.Tables[0].Rows[0]["v_password"].ToString();
                model.v_xm = set.Tables[0].Rows[0]["v_xm"].ToString();
                return model;
            }
            return null;
        }

        public int Update(dzyjwbyjModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pt_dzyj_wbyj set ");
            builder.Append("v_wbyjmc='" + model.v_wbyjmc + "',");
            builder.Append("v_password='" + model.v_password + "',");
            builder.Append("v_xm='" + model.v_xm + "'");
            builder.Append(" where v_yhdm='" + model.v_yhdm + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

