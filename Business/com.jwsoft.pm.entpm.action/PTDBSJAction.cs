namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class PTDBSJAction
    {
        public int Add(PTDBSJ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_DBSJ(");
            builder.Append("I_XGID,V_LXBM,V_YHDM,DTM_DBSJ,V_Content,V_TPLJ,V_DBLJ,C_OpenFlag");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.I_XGID + "',");
            builder.Append("'" + model.V_LXBM + "',");
            builder.Append("'" + model.V_YHDM + "',");
            builder.Append("'" + model.DTM_DBSJ + "',");
            builder.Append("'" + model.V_Content + "',");
            builder.Append("'" + model.V_TPLJ + "',");
            builder.Append("'" + model.V_DBLJ + "',");
            builder.Append("'" + model.C_OpenFlag + "'");
            builder.Append(")");
            builder.Append(";select @@IDENTITY");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int I_DBSJ_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete PT_DBSJ ");
            builder.Append(" where I_DBSJ_ID=" + I_DBSJ_ID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string strWhere, int t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete PT_DBSJ ");
            builder.Append(" where " + strWhere);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select I_DBSJ_ID,I_XGID,V_LXBM,V_YHDM,DTM_DBSJ,V_Content,V_TPLJ,V_DBLJ,C_OpenFlag ");
            builder.Append(" FROM PT_DBSJ ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public PTDBSJ GetModel(int I_DBSJ_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" I_DBSJ_ID,I_XGID,V_LXBM,V_YHDM,DTM_DBSJ,V_Content,V_TPLJ,V_DBLJ,C_OpenFlag ");
            builder.Append(" from PT_DBSJ ");
            builder.Append(" where I_DBSJ_ID=" + I_DBSJ_ID);
            PTDBSJ ptdbsj = new PTDBSJ();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["I_DBSJ_ID"].ToString() != "")
            {
                ptdbsj.I_DBSJ_ID = int.Parse(set.Tables[0].Rows[0]["I_DBSJ_ID"].ToString());
            }
            ptdbsj.I_XGID = set.Tables[0].Rows[0]["I_XGID"].ToString();
            ptdbsj.V_LXBM = set.Tables[0].Rows[0]["V_LXBM"].ToString();
            ptdbsj.V_YHDM = set.Tables[0].Rows[0]["V_YHDM"].ToString();
            ptdbsj.DTM_DBSJ = DateTime.Parse(set.Tables[0].Rows[0]["DTM_DBSJ"].ToString());
            ptdbsj.V_Content = set.Tables[0].Rows[0]["V_Content"].ToString();
            ptdbsj.V_TPLJ = set.Tables[0].Rows[0]["V_TPLJ"].ToString();
            ptdbsj.V_DBLJ = set.Tables[0].Rows[0]["V_DBLJ"].ToString();
            ptdbsj.C_OpenFlag = set.Tables[0].Rows[0]["C_OpenFlag"].ToString();
            return ptdbsj;
        }

        public int Update(PTDBSJ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_DBSJ set ");
            builder.Append("I_XGID='" + model.I_XGID + "',");
            builder.Append("DTM_DBSJ='" + model.DTM_DBSJ + "',");
            builder.Append("V_Content='" + model.V_Content + "',");
            builder.Append("V_TPLJ='" + model.V_TPLJ + "',");
            builder.Append("V_DBLJ='" + model.V_DBLJ + "',");
            builder.Append("C_OpenFlag='" + model.C_OpenFlag + "'");
            builder.Append(" where I_DBSJ_ID=" + model.I_DBSJ_ID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

