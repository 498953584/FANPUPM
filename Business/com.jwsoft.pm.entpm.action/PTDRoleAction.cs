namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class PTDRoleAction
    {
        public int Add(PTDRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_D_Role(");
            builder.Append("RoleTypeCode,RoleTypeName,ParentCode,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RoleTypeCode + "',");
            builder.Append("'" + model.RoleTypeName + "',");
            builder.Append("'" + model.ParentCode + "',");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int AddThird(PTDRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_D_Role(");
            builder.Append("RoleTypeCode,RoleTypeName,ParentCode,DutyTask,Achievement,MoralCharacter,Expand,AgeRequet,SexRequest,StatureRequest,Avoirdupois,Audition,Eye,DucationalBackground,Experience,Talk,Communicate,DutyExplain,Competency,DutyDescribe,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RoleTypeCode + "',");
            builder.Append("'" + model.RoleTypeName + "',");
            builder.Append("'" + model.ParentCode + "',");
            builder.Append("'" + model.DutyTask + "',");
            builder.Append("'" + model.Achievement + "',");
            builder.Append("'" + model.MoralCharacter + "',");
            builder.Append("'" + model.Expand + "',");
            builder.Append("'" + model.AgeRequet + "',");
            builder.Append("'" + model.SexRequest + "',");
            builder.Append("'" + model.StatureRequest + "',");
            builder.Append("'" + model.Avoirdupois + "',");
            builder.Append("'" + model.Audition + "',");
            builder.Append("'" + model.Eye + "',");
            builder.Append("'" + model.DucationalBackground + "',");
            builder.Append("'" + model.Experience + "',");
            builder.Append("'" + model.Talk + "',");
            builder.Append("'" + model.Communicate + "',");
            builder.Append("'" + model.DutyExplain + "',");
            builder.Append("'" + model.Competency + "',");
            builder.Append("'" + model.DutyDescribe + "',");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string RoleTypeCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete PT_D_Role ");
            builder.Append(" where RoleTypeCode='" + RoleTypeCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RoleTypeCode,RoleTypeName,ParentCode,dbo.GetJobChildNum(RoleTypeCode) as ChildNum,DutyTask,Achievement,MoralCharacter,Expand,AgeRequet,SexRequest,StatureRequest,Avoirdupois,Audition,Eye,DucationalBackground,Experience,Talk,Communicate,DutyExplain,Competency,DutyDescribe,Remark ");
            builder.Append(" FROM PT_D_Role ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetNewRoleTypeCode(string RoleTypeCode)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select max(RoleTypeCode) from PT_D_Role where ParentCode='" + RoleTypeCode + "'");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                str = Convert.ToString((int) (int.Parse((string) obj2) + 1));
                while (str.Length < ((string) obj2).Length)
                {
                    str = "0" + str;
                }
                return str;
            }
            return (RoleTypeCode + "001");
        }

        public DataTable getRoleTypeList(string rtc)
        {
            return publicDbOpClass.DataTableQuary("select * from PT_DUTY where DutyCode = '" + rtc + "'");
        }

        public int Update(PTDRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_D_Role set ");
            builder.Append("RoleTypeName='" + model.RoleTypeName + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where RoleTypeCode='" + model.RoleTypeCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateThird(PTDRole model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_D_Role set ");
            builder.Append("RoleTypeName='" + model.RoleTypeName + "',");
            builder.Append("DutyTask='" + model.DutyTask + "',");
            builder.Append("Achievement='" + model.Achievement + "',");
            builder.Append("MoralCharacter='" + model.MoralCharacter + "',");
            builder.Append("Expand='" + model.Expand + "',");
            builder.Append("AgeRequet='" + model.AgeRequet + "',");
            builder.Append("SexRequest='" + model.SexRequest + "',");
            builder.Append("StatureRequest='" + model.StatureRequest + "',");
            builder.Append("Avoirdupois='" + model.Avoirdupois + "',");
            builder.Append("Audition='" + model.Audition + "',");
            builder.Append("Eye='" + model.Eye + "',");
            builder.Append("DucationalBackground='" + model.DucationalBackground + "',");
            builder.Append("Experience='" + model.Experience + "',");
            builder.Append("Talk='" + model.Talk + "',");
            builder.Append("Communicate='" + model.Communicate + "',");
            builder.Append("DutyExplain='" + model.DutyExplain + "',");
            builder.Append("Competency='" + model.Competency + "',");
            builder.Append("DutyDescribe='" + model.DutyDescribe + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where RoleTypeCode='" + model.RoleTypeCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

