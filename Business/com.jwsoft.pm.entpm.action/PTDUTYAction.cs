namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class PTDUTYAction
    {
        public int Add(PTDUTY model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_DUTY(");
            builder.Append("i_bmdm,DutyCode,c_sfyx,DutyNumber,Remark,DutyName");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.i_bmdm + ",");
            builder.Append("'" + model.DutyCode + "',");
            builder.Append("'" + model.c_sfyx + "',");
            builder.Append(model.DutyNumber + ",");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.DutyName + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int I_DUTYID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete PT_DUTY ");
            builder.Append(" where I_DUTYID=" + I_DUTYID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        private string GenerateCondition(string employeeCode, string employeeName, string department, string post, string educationalBackground, string classID, string postAndRank, string positionLevel)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(employeeCode))
            {
                builder.AppendFormat(" AND y.UserCode LIKE '%{0}%' \n", employeeCode);
            }
            if (!string.IsNullOrEmpty(employeeName))
            {
                builder.AppendFormat("AND v_xm LIKE '%{0}%' \n", employeeName);
            }
            if (!string.IsNullOrEmpty(department))
            {
                builder.AppendFormat("AND V_BMMC LIKE '%{0}%' \n", department);
            }
            if (!string.IsNullOrEmpty(post))
            {
                builder.AppendFormat("AND DutyName LIKE '%{0}%' \n", post);
            }
            if (!string.IsNullOrEmpty(educationalBackground))
            {
                builder.AppendFormat("AND y.EducationalBackground LIKE '%{0}%' \n", educationalBackground);
            }
            if (!string.IsNullOrEmpty(classID))
            {
                builder.AppendFormat("AND y.ClassID = {0} \n", classID);
            }
            if (!string.IsNullOrEmpty(postAndRank))
            {
                builder.AppendFormat("AND y.PostAndRank LIKE '%{0}%' \n", postAndRank);
            }
            if (!string.IsNullOrEmpty(positionLevel))
            {
                builder.AppendFormat("AND y.PositionLevel = {0} \n", positionLevel);
            }
            return builder.ToString();
        }

        public DataTable GetDdlClassID()
        {
            string sqlString = "SELECT * FROM PT_SingleClasses WHERE ClassTypeCode = '003'";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetDdlEducationalBackground()
        {
            string sqlString = " SELECT distinct educationalBackground FROM PT_yhmc WHERE educationalBackground IS NOT NULL AND educationalBackground <> ''";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetDdlPositionLevel()
        {
            string sqlString = " SELECT distinct PositionLevel FROM HR_Org_PostLevel ";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetDepartment()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM PT_d_bm WHERE c_sfyx='y' ORDER BY i_xh");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetDepartmentTree(string strcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * FROM PT_d_bm ");
            builder.Append(" where i_sjdm='" + strcode + "' and c_sfyx = 'y' order by i_xh, i_bmdm");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetDTtyhmc(string strSfyx, string strIbmdm, string userName, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = this.GetDTtyhmcAll(strSfyx, strIbmdm, userName).Rows.Count;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP (" + pageSize + ") * FROM \n");
            builder.Append("( \n");
            builder.Append(" SELECT Row_Number() OVER (ORDER BY v_yhdm DESC) as Num, [v_yhdm], [v_xm], [i_bmdm],(select b.v_bmmc from pt_d_bm b where b.i_bmdm = a.i_bmdm) as bmmc,[I_DUTYID], \n");
            builder.Append(" (SELECT DutyName FROM PT_DUTY AS D WHERE D.I_DUTYID = a.I_DUTYID) AS DutyName, \n");
            builder.Append(" [OtherDepts], [OtherDutyIDs], [i_xh], [c_sfyx], [MobilePhoneCode], [RelationCorp], [EnterCorpDate], [Age], [State], \n");
            builder.Append(" (case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end ) as StateName,[IsChargeMan],(case IsChargeMan when 'False' then '否' when 'True' then '是' end) IsChargeManName \n");
            builder.Append(" ,LeaveDate \n");
            builder.Append(" FROM [PT_yhmc] a WHERE a.State = '" + strSfyx + "' and [i_bmdm] IN ('" + strIbmdm + "') \n");
            if (!string.IsNullOrEmpty(userName))
            {
                builder.AppendFormat(" AND [v_xm] LIKE '%{0}%' ", userName);
            }
            builder.Append(") T \n");
            builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetDTtyhmcAll(string strSfyx, string strIbmdm, string userName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT Row_Number() OVER (ORDER BY v_yhdm DESC) as Num, [v_yhdm], [v_xm], [i_bmdm],(select b.v_bmmc from pt_d_bm b where b.i_bmdm = a.i_bmdm) as bmmc,[I_DUTYID], \n");
            builder.Append(" (select b.RoleTypeName from pt_d_role b where b.RoleTypeCode = (select c.DutyCode from pt_duty c where c.i_dutyId = a.i_dutyId)) as DutyName, \n");
            builder.Append(" [OtherDepts], [OtherDutyIDs], [i_xh], [c_sfyx], [MobilePhoneCode], [RelationCorp], [EnterCorpDate], [Age], [State], \n");
            builder.Append(" (case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end ) as StateName,[IsChargeMan],(case IsChargeMan when 'False' then '否' when 'True' then '是' end) IsChargeManName \n");
            builder.Append(" FROM [PT_yhmc] a WHERE a.State = '" + strSfyx + "' and [i_bmdm] IN ('" + strIbmdm + "') \n");
            if (!string.IsNullOrEmpty(userName))
            {
                builder.AppendFormat(" AND [v_xm] LIKE '%{0}%' ", userName);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetDutyList(string DUTYID)
        {
            return publicDbOpClass.DataTableQuary(" select * from PT_yhmc where I_DUTYID = '" + DUTYID + "'");
        }

        public int GetEmployees_Count(string employeeCode, string employeeName, string department, string post, string educationalBackground, string classID, string postAndRank, string positionLevel)
        {
            string str = this.GenerateCondition(employeeCode, employeeName, department, post, educationalBackground, classID, postAndRank, positionLevel);
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT COUNT(*) FROM ( \n");
            builder.Append("\t    SELECT Row_Number()OVER(ORDER BY PT_d_bm.i_xh, y.I_DUTYID) AS Num , \n");
            builder.Append("\t\ty.v_yhdm,ISNULL(y.UserCode,'') UserCode,V_Dlid,v_xm,y.i_bmdm, \n");
            builder.Append("     v_bmqc,y.I_DUTYID,DutyName RoleTypeName, \n");
            builder.Append("\t\tOtherDepts, OtherDutyIDs, y.i_xh, PT_d_bm.i_xh AS Bmxh, \n");
            builder.Append("\t\ty.c_sfyx, MobilePhoneCode, RelationCorp, EnterCorpDate, Age, State, \n");
            builder.Append("\t\t(case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end ) as StateName, \n");
            builder.Append("\t\tEducationalBackground,y.ClassID,y.PostAndRank,y.PositionLevel,ClassName,HR_Org_PostLevel.PostAndRank PostName, \n");
            builder.Append("\t\ty.IsChargeMan as IsChargeMan \n");
            builder.Append("\t\tFROM PT_yhmc AS y \n");
            builder.Append("\t\tLEFT JOIN PT_d_bm ON y.i_bmdm=PT_d_bm.i_bmdm  \n");
            builder.Append("\t\tLEFT JOIN dbo.PT_SingleClasses ON PT_SingleClasses.ClassID=y.ClassID \n");
            builder.Append("\t\tLEFT JOIN dbo.HR_Org_PostLevel ON y.PositionLevel=HR_Org_PostLevel.PositionLevel AND y.PostAndRank=HR_Org_PostLevel.RecordId \n");
            builder.Append("\t\tLEFT JOIN pt_login ON y.V_Yhdm=pt_login.V_Yhdm \n");
            builder.Append("\t\tLEFT JOIN PT_DUTY ON y.I_DUTYID = PT_DUTY.I_DUTYID \n");
            builder.Append("\t\tWHERE State != 2 \n");
            builder.Append(str);
            builder.Append(") Employees \n");
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar(builder.ToString()));
        }

        public DataTable GetEmployeesData(string employeeCode, string employeeName, string department, string post, string educationalBackground, string classID, string postAndRank, string positionLevel, int pageIndex, int pageSize)
        {
            string str = this.GenerateCondition(employeeCode, employeeName, department, post, educationalBackground, classID, postAndRank, positionLevel);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" SELECT TOP({0}) * FROM ( \n", pageSize);
            builder.Append("\t    SELECT Row_Number()OVER(ORDER BY PT_d_bm.i_xh, y.I_DUTYID) AS Num , \n");
            builder.Append("\t\ty.v_yhdm,ISNULL(y.UserCode,'') UserCode,V_Dlid,v_xm,y.i_bmdm, \n");
            builder.Append("     v_bmqc,y.I_DUTYID,DutyName RoleTypeName, \n");
            builder.Append("\t\tOtherDepts, OtherDutyIDs, y.i_xh, PT_d_bm.i_xh AS Bmxh, \n");
            builder.Append("\t\ty.c_sfyx, MobilePhoneCode, RelationCorp, EnterCorpDate, Age, State, \n");
            builder.Append("\t\t(case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end ) as StateName, \n");
            builder.Append("\t\tEducationalBackground,y.ClassID,y.PostAndRank,y.PositionLevel,ClassName,HR_Org_PostLevel.PostAndRank PostName, \n");
            builder.Append("\t\ty.IsChargeMan as IsChargeMan \n");
            builder.Append("\t\tFROM PT_yhmc AS y \n");
            builder.Append("\t\tLEFT JOIN PT_d_bm ON y.i_bmdm=PT_d_bm.i_bmdm  \n");
            builder.Append("\t\tLEFT JOIN dbo.PT_SingleClasses ON PT_SingleClasses.ClassID=y.ClassID \n");
            builder.Append("\t\tLEFT JOIN dbo.HR_Org_PostLevel ON y.PositionLevel=HR_Org_PostLevel.PositionLevel AND y.PostAndRank=HR_Org_PostLevel.RecordId \n");
            builder.Append("\t\tLEFT JOIN pt_login ON y.V_Yhdm=pt_login.V_Yhdm \n");
            builder.Append("\t\tLEFT JOIN PT_DUTY ON y.I_DUTYID = PT_DUTY.I_DUTYID \n");
            builder.Append("\t\tWHERE State != 2 \n");
            builder.Append(str);
            builder.Append(" ) Employees \n");
            builder.AppendFormat(" WHERE Num > {0} \n", (pageIndex - 1) * pageSize);
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetList(int bmdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select a.I_DUTYID,a.i_bmdm,a.DutyCode,a.c_sfyx,a.RoleTypeCode,a.DutyNumber, \n");
            builder.Append("     b.V_BMMC,a.DutyName,dbo.GetDutyPersonNum(a.i_bmdm,I_DUTYID) as HaveNumber, \n");
            builder.Append(" \t(a.DutyNumber-dbo.GetDutyPersonNum(a.i_bmdm,I_DUTYID)) as Vacancy,a.Remark \n");
            builder.Append(" from PT_DUTY a ");
            builder.Append(" left join PT_d_bm b on a.i_bmdm=b.i_bmdm ");
            builder.Append(" where a.i_bmdm='" + bmdm + "' and  b.c_sfyx='y' ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetList(string id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select a.I_DUTYID,a.i_bmdm,a.DutyCode,a.c_sfyx,a.RoleTypeCode,a.DutyNumber, \n");
            builder.Append("     b.V_BMMC,a.DutyName,dbo.GetDutyPersonNum(a.i_bmdm,I_DUTYID) as HaveNumber, \n");
            builder.Append(" \t(a.DutyNumber-dbo.GetDutyPersonNum(a.i_bmdm,I_DUTYID)) as Vacancy,a.Remark \n");
            builder.Append(" from PT_DUTY a ");
            builder.Append(" left join PT_d_bm b on a.i_bmdm=b.i_bmdm ");
            builder.Append(" where a.I_DUTYID='" + id + "' and  b.c_sfyx='y' ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetPostAndRank(string positionLevel)
        {
            string sqlString = " SELECT distinct RecordID,PostAndRank FROM HR_Org_PostLevel WHERE 1=1 and PostAndRank is not null and PostAndRank <> ''";
            if (!string.IsNullOrEmpty(positionLevel))
            {
                sqlString = sqlString + "AND PositionLevel= '" + positionLevel + "'";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetPTDUTYDifferent(string strDutyName, string strBMDM)
        {
            return publicDbOpClass.DataTableQuary(" SELECT * FROM PT_DUTY WHERE i_bmdm='" + strBMDM + "' AND DutyName ='" + strDutyName + "' collate chinese_PRC_CS_AI ");
        }

        public DataTable GetRoleTree(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * FROM Pt_d_role ");
            builder.Append(" where " + strWhere);
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int Update(PTDUTY model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_DUTY set ");
            builder.Append("DutyCode='" + model.DutyCode + "',");
            builder.Append("c_sfyx='" + model.c_sfyx + "',");
            builder.Append("DutyName='" + model.DutyName + "',");
            builder.Append("DutyNumber=" + model.DutyNumber + ",");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where I_DUTYID=" + model.I_DUTYID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

