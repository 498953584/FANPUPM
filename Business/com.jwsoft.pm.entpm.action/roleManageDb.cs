namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.sysManage.publicStringOperation;
    using System;
    using System.Data;

    public class roleManageDb
    {
        private string m_ErrorMessage;

        public DataTable GetRoleModules(string roleCode)
        {
            string str = "";
            str = "SELECT a.*, ISNULL(b.RoleCode, 0) AS IsHave,ISNULL(b.IsHaveOp,0) AS ISHaveOp FROM dbo.PT_MK a LEFT OUTER JOIN ";
            return publicDbOpClass.DataTableQuary(str + "dbo.PT_Role_Privilege b ON a.C_MKDM = b.ModuleCode and b.RoleCode='" + roleCode + "' order by a.i_xh,a.c_mkdm");
        }

        public static DataTable getRoleTypeFirstLevel()
        {
            string sqlString = "select * from PT_D_Role where len(RoleTypeCode)=3";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public bool isHave(string mkdm, string jsdm)
        {
            if (publicDbOpClass.DataTableQuary("select * from pt_js where c_jsdm = '" + jsdm + "'").Rows[0]["v_xtqx"].ToString().IndexOf(mkdm) == -1)
            {
                return false;
            }
            return true;
        }

        public bool roleAdd(string RoleTypeCode, string RoleName)
        {
            bool flag = true;
            string sqlString = string.Format("INSERT INTO PT_Role(RoleTypeCode,RoleName, IsValid) VALUES('001','{0}', 1)", RoleName);
            try
            {
                publicDbOpClass.NonQuerySqlString(sqlString);
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public bool roleDel(string RoleCode)
        {
            if (publicDbOpClass.ExecuteScalar("select Count(1) from Pt_login where Charindex('," + RoleCode + ",',','+RoleCodes+',') <> 0").ToString() != "0")
            {
                return false;
            }
            publicDbOpClass.NonQuerySqlString("delete from pt_role_privilege where rolecode='" + RoleCode + "'");
            return publicDbOpClass.NonQuerySqlString("delete from pt_role where RoleCode = " + RoleCode);
        }

        public bool roleMod(string RoleCode, string RoleName)
        {
            bool flag = true;
            RoleName = PublicClass.CheckString(RoleName);
            string sqlString = "update pt_Role set RoleName='" + RoleName + "' where RoleCode='" + RoleCode + "'";
            try
            {
                publicDbOpClass.NonQuerySqlString(sqlString);
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public bool rolePrivilegeSet(string mkdm, string jsdm)
        {
            return publicDbOpClass.NonQuerySqlString("update pt_js set v_xtqx = '" + mkdm + "' where c_jsdm = '" + jsdm + "'");
        }

        public DataTable roleQuary(string RoleCode)
        {
            return publicDbOpClass.DataTableQuary("select RoleName from PT_Role where RoleCode='" + RoleCode + "'");
        }

        public DataTable selAllUsers(string jsdm)
        {
            return publicDbOpClass.DataTableQuary("select pt_login.v_yhdm,v_xm,v_bmqc from pt_yhmc,pt_login,pt_d_bm where pt_login.c_jsdm = '" + jsdm + "' and pt_yhmc.v_yhdm=pt_login.v_yhdm and pt_yhmc.i_bmdm = pt_d_bm.i_bmdm");
        }

        public bool UpdateRolePrivilage(string roleCode, string scopeCode, string moduleCode)
        {
            string[] sqlString = new string[] { " delete from PT_Role_Privilege where RoleCode = '" + roleCode + "'", "", "" };
            if (moduleCode.Trim().Length != 0)
            {
                sqlString[1] = " insert into PT_Role_Privilege (RoleCode,ModuleCode,IsBasic,IsHaveOp) select '" + roleCode + "',c_mkdm,IsBasic,'0' from pt_mk where c_mkdm in (" + moduleCode + ")";
            }
            if (scopeCode.Trim().Length != 0)
            {
                sqlString[2] = " update PT_Role_Privilege set IsHaveOp = '1' where RoleCode = '" + roleCode + "' and ModuleCode in (" + scopeCode + ")";
            }
            return publicDbOpClass.ExecuteSQL(sqlString);
        }

        public bool userPrivSet(string jsdm, string strPriv)
        {
            return publicDbOpClass.NonQuerySqlString("update pt_login set v_xtqx = '" + strPriv + "' where c_jsdm = '" + jsdm + "'");
        }

        public string errorMessage
        {
            get
            {
                return this.m_ErrorMessage;
            }
            set
            {
                if (this.m_ErrorMessage != value)
                {
                    this.m_ErrorMessage = value;
                }
            }
        }
    }
}

