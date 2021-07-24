namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PTDRoleService
    {
        public int Add(PTDRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_D_Role(");
            builder.Append("RoleTypeCode,RoleTypeName,ParentCode,DutyTask,Achievement,MoralCharacter,Expand,AgeRequet,SexRequest,StatureRequest,Avoirdupois,Audition,Eye,DucationalBackground,Experience,Talk,Communicate,DutyExplain,Competency,DutyDescribe,Remark)");
            builder.Append(" values (");
            builder.Append("@RoleTypeCode,@RoleTypeName,@ParentCode,@DutyTask,@Achievement,@MoralCharacter,@Expand,@AgeRequet,@SexRequest,@StatureRequest,@Avoirdupois,@Audition,@Eye,@DucationalBackground,@Experience,@Talk,@Communicate,@DutyExplain,@Competency,@DutyDescribe,@Remark)");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@RoleTypeCode", SqlDbType.VarChar, 9), new SqlParameter("@RoleTypeName", SqlDbType.VarChar, 20), new SqlParameter("@ParentCode", SqlDbType.VarChar, 6), new SqlParameter("@DutyTask", SqlDbType.VarChar, 200), new SqlParameter("@Achievement", SqlDbType.VarChar, 200), new SqlParameter("@MoralCharacter", SqlDbType.VarChar, 200), new SqlParameter("@Expand", SqlDbType.VarChar, 200), new SqlParameter("@AgeRequet", SqlDbType.VarChar, 200), new SqlParameter("@SexRequest", SqlDbType.VarChar, 200), new SqlParameter("@StatureRequest", SqlDbType.VarChar, 200), new SqlParameter("@Avoirdupois", SqlDbType.VarChar, 200), new SqlParameter("@Audition", SqlDbType.VarChar, 200), new SqlParameter("@Eye", SqlDbType.VarChar, 200), new SqlParameter("@DucationalBackground", SqlDbType.VarChar, 200), new SqlParameter("@Experience", SqlDbType.VarChar, 500), new SqlParameter("@Talk", SqlDbType.VarChar, 200), 
                new SqlParameter("@Communicate", SqlDbType.VarChar, 200), new SqlParameter("@DutyExplain", SqlDbType.Text), new SqlParameter("@Competency", SqlDbType.VarChar, 200), new SqlParameter("@DutyDescribe", SqlDbType.VarChar, 500), new SqlParameter("@Remark", SqlDbType.VarChar, 200)
             };
            commandParameters[0].Value = model.RoleTypeCode;
            commandParameters[1].Value = model.RoleTypeName;
            commandParameters[2].Value = model.ParentCode;
            commandParameters[3].Value = model.DutyTask;
            commandParameters[4].Value = model.Achievement;
            commandParameters[5].Value = model.MoralCharacter;
            commandParameters[6].Value = model.Expand;
            commandParameters[7].Value = model.AgeRequet;
            commandParameters[8].Value = model.SexRequest;
            commandParameters[9].Value = model.StatureRequest;
            commandParameters[10].Value = model.Avoirdupois;
            commandParameters[11].Value = model.Audition;
            commandParameters[12].Value = model.Eye;
            commandParameters[13].Value = model.DucationalBackground;
            commandParameters[14].Value = model.Experience;
            commandParameters[15].Value = model.Talk;
            commandParameters[0x10].Value = model.Communicate;
            commandParameters[0x11].Value = model.DutyExplain;
            commandParameters[0x12].Value = model.Competency;
            commandParameters[0x13].Value = model.DutyDescribe;
            commandParameters[20].Value = model.Remark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string RoleTypeCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PT_D_Role ");
            builder.Append(" where RoleTypeCode=@RoleTypeCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RoleTypeCode", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = RoleTypeCode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public IList<PTDRole> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RoleTypeCode,RoleTypeName,ParentCode,DutyTask,Achievement,MoralCharacter,Expand,AgeRequet,SexRequest,StatureRequest,Avoirdupois,Audition,Eye,DucationalBackground,Experience,Talk,Communicate,DutyExplain,Competency,DutyDescribe,Remark ");
            builder.Append(" FROM PT_D_Role ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            IList<PTDRole> list = new List<PTDRole>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public PTDRole GetModel(string RoleTypeCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RoleTypeCode,RoleTypeName,ParentCode,DutyTask,Achievement,MoralCharacter,Expand,AgeRequet,SexRequest,StatureRequest,Avoirdupois,Audition,Eye,DucationalBackground,Experience,Talk,Communicate,DutyExplain,Competency,DutyDescribe,Remark from PT_D_Role ");
            builder.Append(" where RoleTypeCode=@RoleTypeCode ");
            PTDRole role = new PTDRole();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@RoleTypeCode", RoleTypeCode) }))
            {
                if (reader.Read())
                {
                    role = this.ReaderBind(reader);
                }
            }
            return role;
        }

        public PTDRole ReaderBind(IDataReader dataReader)
        {
            return new PTDRole { 
                RoleTypeCode = dataReader["RoleTypeCode"].ToString(), RoleTypeName = dataReader["RoleTypeName"].ToString(), ParentCode = dataReader["ParentCode"].ToString(), DutyTask = dataReader["DutyTask"].ToString(), Achievement = dataReader["Achievement"].ToString(), MoralCharacter = dataReader["MoralCharacter"].ToString(), Expand = dataReader["Expand"].ToString(), AgeRequet = dataReader["AgeRequet"].ToString(), SexRequest = dataReader["SexRequest"].ToString(), StatureRequest = dataReader["StatureRequest"].ToString(), Avoirdupois = dataReader["Avoirdupois"].ToString(), Audition = dataReader["Audition"].ToString(), Eye = dataReader["Eye"].ToString(), DucationalBackground = dataReader["DucationalBackground"].ToString(), Experience = dataReader["Experience"].ToString(), Talk = dataReader["Talk"].ToString(), 
                Communicate = dataReader["Communicate"].ToString(), DutyExplain = dataReader["DutyExplain"].ToString(), Competency = dataReader["Competency"].ToString(), DutyDescribe = dataReader["DutyDescribe"].ToString(), Remark = dataReader["Remark"].ToString()
             };
        }

        public int Update(PTDRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_D_Role set ");
            builder.Append("RoleTypeName=@RoleTypeName,");
            builder.Append("ParentCode=@ParentCode,");
            builder.Append("DutyTask=@DutyTask,");
            builder.Append("Achievement=@Achievement,");
            builder.Append("MoralCharacter=@MoralCharacter,");
            builder.Append("Expand=@Expand,");
            builder.Append("AgeRequet=@AgeRequet,");
            builder.Append("SexRequest=@SexRequest,");
            builder.Append("StatureRequest=@StatureRequest,");
            builder.Append("Avoirdupois=@Avoirdupois,");
            builder.Append("Audition=@Audition,");
            builder.Append("Eye=@Eye,");
            builder.Append("DucationalBackground=@DucationalBackground,");
            builder.Append("Experience=@Experience,");
            builder.Append("Talk=@Talk,");
            builder.Append("Communicate=@Communicate,");
            builder.Append("DutyExplain=@DutyExplain,");
            builder.Append("Competency=@Competency,");
            builder.Append("DutyDescribe=@DutyDescribe,");
            builder.Append("Remark=@Remark");
            builder.Append(" where RoleTypeCode=@RoleTypeCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@RoleTypeCode", SqlDbType.VarChar, 9), new SqlParameter("@RoleTypeName", SqlDbType.VarChar, 20), new SqlParameter("@ParentCode", SqlDbType.VarChar, 6), new SqlParameter("@DutyTask", SqlDbType.VarChar, 200), new SqlParameter("@Achievement", SqlDbType.VarChar, 200), new SqlParameter("@MoralCharacter", SqlDbType.VarChar, 200), new SqlParameter("@Expand", SqlDbType.VarChar, 200), new SqlParameter("@AgeRequet", SqlDbType.VarChar, 200), new SqlParameter("@SexRequest", SqlDbType.VarChar, 200), new SqlParameter("@StatureRequest", SqlDbType.VarChar, 200), new SqlParameter("@Avoirdupois", SqlDbType.VarChar, 200), new SqlParameter("@Audition", SqlDbType.VarChar, 200), new SqlParameter("@Eye", SqlDbType.VarChar, 200), new SqlParameter("@DucationalBackground", SqlDbType.VarChar, 200), new SqlParameter("@Experience", SqlDbType.VarChar, 500), new SqlParameter("@Talk", SqlDbType.VarChar, 200), 
                new SqlParameter("@Communicate", SqlDbType.VarChar, 200), new SqlParameter("@DutyExplain", SqlDbType.Text), new SqlParameter("@Competency", SqlDbType.VarChar, 200), new SqlParameter("@DutyDescribe", SqlDbType.VarChar, 500), new SqlParameter("@Remark", SqlDbType.VarChar, 200)
             };
            commandParameters[0].Value = model.RoleTypeCode;
            commandParameters[1].Value = model.RoleTypeName;
            commandParameters[2].Value = model.ParentCode;
            commandParameters[3].Value = model.DutyTask;
            commandParameters[4].Value = model.Achievement;
            commandParameters[5].Value = model.MoralCharacter;
            commandParameters[6].Value = model.Expand;
            commandParameters[7].Value = model.AgeRequet;
            commandParameters[8].Value = model.SexRequest;
            commandParameters[9].Value = model.StatureRequest;
            commandParameters[10].Value = model.Avoirdupois;
            commandParameters[11].Value = model.Audition;
            commandParameters[12].Value = model.Eye;
            commandParameters[13].Value = model.DucationalBackground;
            commandParameters[14].Value = model.Experience;
            commandParameters[15].Value = model.Talk;
            commandParameters[0x10].Value = model.Communicate;
            commandParameters[0x11].Value = model.DutyExplain;
            commandParameters[0x12].Value = model.Competency;
            commandParameters[0x13].Value = model.DutyDescribe;
            commandParameters[20].Value = model.Remark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

