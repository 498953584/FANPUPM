namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class FlowRoleAction
    {
        public static bool AddRole(string roleName, int roleType, string remark)
        {
            return publicDbOpClass.NonQuerySqlString(" insert into WF_Role values('" + roleName + "'," + roleType.ToString() + ",'" + remark + "',1)");
        }

        public static bool AddUser(int roleId, string userCode, string CorpCode)
        {
            return publicDbOpClass.NonQuerySqlString(" insert into wf_roleusers values(" + roleId.ToString() + ",'" + userCode.Trim() + "','" + CorpCode + "')");
        }

        public static bool DelRole(int roleId)
        {
            return publicDbOpClass.NonQuerySqlString("update WF_Role set IsValid=0 where roleId=" + roleId.ToString());
        }

        public static bool DelUser(int UserId)
        {
            return publicDbOpClass.NonQuerySqlString("delete from wf_roleusers where Role_User_ID=" + UserId);
        }

        public static bool DelUser(int roleId, string userCode)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "delete from wf_roleusers where (roleid=", roleId, ") and (usercode='", userCode, "')" }));
        }

        public static DataTable GetCorpList(int roleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT R.*,Y.v_xm FROM WF_RoleUsers R\r\nLEFT JOIN PT_yhmc Y ON R.UserCode=Y.v_yhdm\r\nWHERE RoleID={0}", roleId);
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static string GetCorpNames(string corpcode)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(corpcode))
            {
                return str;
            }
            DataTable table = publicDbOpClass.DataTableQuary("SELECT  V_BMMC FROM PT_d_bm WHERE  i_bmdm IN (" + corpcode + ")");
            if (table.Rows.Count <= 0)
            {
                return str;
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                str = str + table.Rows[i][0].ToString() + ",";
            }
            return str.Substring(0, str.Length - 1);
        }

        public static DataTable GetCorpRole(string roleUserId)
        {
            return publicDbOpClass.DataTableQuary("SELECT R.* ,Y.v_xm FROM WF_RoleUsers R LEFT JOIN PT_yhmc  Y ON Y.v_yhdm=R.UserCode  WHERE Role_User_ID=" + roleUserId + " ");
        }

        public static DataTable GetCorpTable(string corpcode)
        {
            return publicDbOpClass.DataTableQuary("SELECT i_bmdm, V_BMMC FROM PT_d_bm WHERE  i_bmdm IN (" + corpcode + ")");
        }

        public static DataTable GetCorpTableByBmbm(string bmbm)
        {
            return publicDbOpClass.DataTableQuary("SELECT i_bmdm, V_BMMC, v_bmbm FROM PT_d_bm WHERE  v_bmbm IN (" + bmbm + ")");
        }

        public static DataTable GetCorpUserList(string corpCode, string userName, int pageSize, int pageNo)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" SELECT * FROM (\r\nSELECT Row_Number() OVER(ORDER BY v_xm) AS Num, [v_yhdm], [v_xm], [i_bmdm],(select b.v_bmmc from pt_d_bm b where b.i_bmdm = a.i_bmdm) as bmmc, \r\n[I_DUTYID],(select b.RoleTypeName from pt_d_role b where b.RoleTypeCode = (select c.DutyCode from pt_duty c where c.i_dutyId = a.i_dutyId)) as DutyName\r\n FROM [PT_yhmc] a WHERE a.State = 1 and ([i_bmdm] ={0}) \r\n", corpCode);
            builder.AppendLine();
            if (!string.IsNullOrEmpty(userName))
            {
                builder.AppendFormat(" AND v_xm LIKE '%{0}%' ", userName);
            }
            builder.AppendFormat(" ) AS A  WHERE Num BETWEEN ({0}-1)*{1}+1 AND {2}*{3}", new object[] { pageNo, pageSize, pageNo, pageSize });
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable GetDutyInfo(string yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" declare @OtherDutyIDs varchar(500) set @OtherDutyIDs='' ");
            builder.Append(" select @OtherDutyIDs=(OtherDutyIDs+','+cast(I_DUTYID as varchar(50))) from PT_YHMC where v_yhdm ='" + yhdm + "' ");
            builder.Append(" select (select V_BMMC from pt_d_bm where i_bmdm=a.i_bmdm) as V_BMMC,(select CorpCode from pt_d_bm where i_bmdm=a.i_bmdm) as CorpCode ,(select RoleTypeName from PT_d_Role where RoleTypeCode=a.DutyCode) as RoleTypeName from PT_Duty a where charindex(','+Convert(varchar(20),a.I_DUTYID)+',',','+@OtherDutyIDs+',')>0 ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable GetUserInfo(string usercodes)
        {
            DataTable table = new DataTable();
            if (!string.IsNullOrEmpty(usercodes))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("SELECT A.v_yhdm,C.v_xm,C.V_BMMC,A.RoleTypeName FROM (\r\nSELECT v_yhdm, RoleTypeName FROM PT_D_Role r\r\nLEFT JOIN PT_DUTY d ON r.RoleTypeCode=d.DutyCode \r\nLEFT JOIN PT_yhmc  y ON y.i_bmdm=d.i_bmdm AND y.I_DUTYID=d.I_DUTYID \r\nWHERE v_yhdm IN ({0}))AS A\r\nLEFT JOIN (\r\nSELECT * FROM (\r\nSELECT v_yhdm, v_xm,V_BMMC FROM PT_yhmc Y \r\nLEFT JOIN PT_d_bm P ON Y.i_bmdm=P.i_bmdm \r\nWHERE v_yhdm IN ({1})) AS B) AS C\r\nON A.v_yhdm =C.v_yhdm ", usercodes, usercodes);
                table = publicDbOpClass.DataTableQuary(builder.ToString());
            }
            return table;
        }

        public static bool IsHaveCorpUser(string usercode, string corpcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT COUNT(*) FROM WF_RoleUsers WHERE UserCode='{0}' AND CorpCode='{1}'", usercode, corpcode);
            return (((int) publicDbOpClass.ExecuteScalar(builder.ToString())) > 0);
        }

        public static bool IsHaveUser(int roleId)
        {
            return (((int) publicDbOpClass.ExecuteScalar("select count(1) from wf_roleusers where roleid=" + roleId.ToString())) > 0);
        }

        public static bool IsSameUser(int roleId, string usercode)
        {
            return (((int) publicDbOpClass.ExecuteScalar("select count(1) from wf_roleusers where roleid=" + roleId.ToString() + " and UserCode='" + usercode + "'")) > 0);
        }

        public static DataTable QueryAllRole()
        {
            string sqlString = "";
            sqlString = "select * from wf_role where isvalid=1 order by RoleType";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable QueryAllRole(int roleType)
        {
            return publicDbOpClass.DataTableQuary("select * from wf_role where RoleType=" + roleType.ToString() + " and isvalid=1");
        }

        public static DataTable QueryOneRole(int roleId)
        {
            return publicDbOpClass.DataTableQuary(" select * from wf_role where roleid=" + roleId.ToString());
        }

        public static DataTable QueryRoleUser(int roleId)
        {
            string str = "";
            str = " select * ,(select CorpName from pt_d_corpcode where CorpCode = a.CorpCode) CorpName ,";
            return publicDbOpClass.DataTableQuary((str + " (select v_xm from pt_yhmc where v_yhdm = a.UserCode) as UserName \tfrom wf_roleusers a") + " where a.roleID =  " + roleId.ToString());
        }

        public static bool UpdateCorpUser(string roleUserId, string userCode, string corpCode)
        {
            return publicDbOpClass.NonQuerySqlString("UPDATE WF_RoleUsers SET CorpCode='" + corpCode + "',UserCode='" + userCode + "'  WHERE Role_User_ID=" + roleUserId + " ");
        }

        public static bool UpdRole(int roleId, string roleName, int roleType, string remark)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update WF_Role set RoleName='", roleName, "',RoleType=", roleType, ",remark='", remark, "' where RoleID=", roleId.ToString() }));
        }
    }
}

