namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PTbdmService
    {
        public int AddPTbdm(PTbdm model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_d_bm(");
            builder.Append("i_bmdm,i_sjdm,v_bmbm,CorpCode,V_BMMC,v_bmqc,i_xh,v_bmjx,i_xjbm,i_jb,c_sfyx)");
            builder.Append(" values (");
            builder.Append("@i_bmdm,@i_sjdm,@v_bmbm,@CorpCode,@V_BMMC,@v_bmqc,@i_xh,@v_bmjx,@i_xjbm,@i_jb,@c_sfyx)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@i_bmdm", SqlDbType.Int, 4), new SqlParameter("@i_sjdm", SqlDbType.Int, 4), new SqlParameter("@v_bmbm", SqlDbType.VarChar, 20), new SqlParameter("@CorpCode", SqlDbType.VarChar, 2), new SqlParameter("@V_BMMC", SqlDbType.VarChar, 100), new SqlParameter("@v_bmqc", SqlDbType.VarChar, 100), new SqlParameter("@i_xh", SqlDbType.Int, 4), new SqlParameter("@v_bmjx", SqlDbType.VarChar, 50), new SqlParameter("@i_xjbm", SqlDbType.Int, 4), new SqlParameter("@i_jb", SqlDbType.Int, 4), new SqlParameter("@c_sfyx", SqlDbType.Char, 1) };
            commandParameters[0].Value = model.i_bmdm;
            commandParameters[1].Value = model.i_sjdm;
            commandParameters[2].Value = model.v_bmbm;
            commandParameters[3].Value = model.CorpCode;
            commandParameters[4].Value = model.V_BMMC;
            commandParameters[5].Value = model.v_bmqc;
            commandParameters[6].Value = model.i_xh;
            commandParameters[7].Value = model.v_bmjx;
            commandParameters[8].Value = model.i_xjbm;
            commandParameters[9].Value = model.i_jb;
            commandParameters[10].Value = model.c_sfyx;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(int i_bmdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PT_d_bm ");
            builder.Append(" where i_bmdm=@i_bmdm ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@i_bmdm", SqlDbType.Int, 4) };
            commandParameters[0].Value = i_bmdm;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        private PTbdm GetModelByReader(SqlDataReader reader)
        {
            return new PTbdm { c_sfyx = Convert.ToString(reader["c_sfyx"]), CorpCode = Convert.ToString(reader["corpCode"]), i_bmdm = Convert.ToInt32(reader["i_bmdm"]), i_jb = Convert.ToInt32(reader["i_jb"]), i_sjdm = Convert.ToInt32(reader["i_sjdm"]), i_xh = Convert.ToInt32(reader["i_xh"]), i_xjbm = Convert.ToInt32(reader["i_xjbm"]), v_bmbm = Convert.ToString(reader["v_bmbm"]), v_bmjx = Convert.ToString(reader["v_bmjx"]), V_BMMC = Convert.ToString(reader["v_bmmc"]), v_bmqc = Convert.ToString(reader["v_bmqc"]) };
        }

        public PTbdm GetPTbdmById(int i_bmdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 i_bmdm,i_sjdm,v_bmbm,CorpCode,V_BMMC,v_bmqc,i_xh,v_bmjx,i_xjbm,i_jb,c_sfyx from PT_d_bm ");
            builder.Append(" where i_bmdm=@i_bmdm ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@i_bmdm", SqlDbType.Int, 4) };
            commandParameters[0].Value = i_bmdm;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    return this.GetModelByReader(reader);
                }
            }
            return null;
        }

        public IList<PTbdm> GetPTbdmByWhere(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   i_bmdm,i_sjdm,v_bmbm,CorpCode,V_BMMC,v_bmqc,i_xh,v_bmjx,i_xjbm,i_jb,c_sfyx from PT_d_bm ");
            builder.Append(where);
            if (!string.IsNullOrEmpty(where))
            {
                builder.Append(" and c_sfyx='y'");
            }
            else
            {
                builder.Append(" where c_sfyx='y'");
            }
            IList<PTbdm> list = new List<PTbdm>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.GetModelByReader(reader));
                }
            }
            return list;
        }

        public int UpdatePTdbm(PTbdm model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_d_bm set ");
            builder.Append("i_sjdm=@i_sjdm,");
            builder.Append("v_bmbm=@v_bmbm,");
            builder.Append("CorpCode=@CorpCode,");
            builder.Append("V_BMMC=@V_BMMC,");
            builder.Append("v_bmqc=@v_bmqc,");
            builder.Append("i_xh=@i_xh,");
            builder.Append("v_bmjx=@v_bmjx,");
            builder.Append("i_xjbm=@i_xjbm,");
            builder.Append("i_jb=@i_jb,");
            builder.Append("c_sfyx=@c_sfyx");
            builder.Append(" where i_bmdm=@i_bmdm ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@i_bmdm", SqlDbType.Int, 4), new SqlParameter("@i_sjdm", SqlDbType.Int, 4), new SqlParameter("@v_bmbm", SqlDbType.VarChar, 20), new SqlParameter("@CorpCode", SqlDbType.VarChar, 2), new SqlParameter("@V_BMMC", SqlDbType.VarChar, 100), new SqlParameter("@v_bmqc", SqlDbType.VarChar, 100), new SqlParameter("@i_xh", SqlDbType.Int, 4), new SqlParameter("@v_bmjx", SqlDbType.VarChar, 50), new SqlParameter("@i_xjbm", SqlDbType.Int, 4), new SqlParameter("@i_jb", SqlDbType.Int, 4), new SqlParameter("@c_sfyx", SqlDbType.Char, 1) };
            commandParameters[0].Value = model.i_bmdm;
            commandParameters[1].Value = model.i_sjdm;
            commandParameters[2].Value = model.v_bmbm;
            commandParameters[3].Value = model.CorpCode;
            commandParameters[4].Value = model.V_BMMC;
            commandParameters[5].Value = model.v_bmqc;
            commandParameters[6].Value = model.i_xh;
            commandParameters[7].Value = model.v_bmjx;
            commandParameters[8].Value = model.i_xjbm;
            commandParameters[9].Value = model.i_jb;
            commandParameters[10].Value = model.c_sfyx;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

