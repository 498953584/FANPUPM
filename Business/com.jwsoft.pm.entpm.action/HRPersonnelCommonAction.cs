namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class HRPersonnelCommonAction
    {
        public static int AddDuty(string UserCode, string deptId, string dutyId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from pt_yhmc where v_yhdm='" + UserCode + "' ");
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                deptId = deptId + "," + table.Rows[0]["OtherDepts"].ToString().Trim().Trim(new char[] { ',' });
                dutyId = dutyId + "," + table.Rows[0]["OtherDutyIDs"].ToString().Trim().Trim(new char[] { ',' });
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(" update pt_yhmc set ");
            builder2.Append(" OtherDepts='" + deptId.Trim().Trim(new char[] { ',' }) + "', ");
            builder2.Append(" OtherDutyIDs='" + dutyId.Trim().Trim(new char[] { ',' }) + "' ");
            builder2.Append(" where v_yhdm='" + UserCode + "'");
            return publicDbOpClass.ExecSqlString(builder2.ToString());
        }

        public static int DeleteDutyAndDept(string UserCode, string deptId, string dutyId)
        {
            string str = "";
            string str2 = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from pt_yhmc where v_yhdm='" + UserCode + "' ");
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                string[] strArray = table.Rows[0]["OtherDepts"].ToString().Trim().Trim(new char[] { ',' }).Split(new char[] { ',' });
                string[] strArray2 = table.Rows[0]["OtherDutyIDs"].ToString().Trim().Trim(new char[] { ',' }).Split(new char[] { ',' });
                foreach (string str3 in strArray)
                {
                    if (str3 != deptId)
                    {
                        str = str + "," + str3;
                    }
                }
                foreach (string str4 in strArray2)
                {
                    if (str4 != dutyId)
                    {
                        str2 = str2 + "," + str4;
                    }
                }
            }
            str = str.Trim().Trim(new char[] { ',' });
            str2 = str2.Trim().Trim(new char[] { ',' });
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(" update pt_yhmc set ");
            builder2.Append(" OtherDepts='" + str.Trim().Trim(new char[] { ',' }) + "', ");
            builder2.Append(" OtherDutyIDs='" + str2.Trim().Trim(new char[] { ',' }) + "' ");
            builder2.Append(" where v_yhdm='" + UserCode + "'");
            return publicDbOpClass.ExecSqlString(builder2.ToString());
        }

        public static DataTable GetDutyInfo(string yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" declare @OtherDutyIDs varchar(500) set @OtherDutyIDs='' ");
            builder.Append(" select @OtherDutyIDs=OtherDutyIDs from PT_YHMC where v_yhdm='" + yhdm + "' ");
            builder.Append(" select a.*,0 as MainRole,(select V_BMMC from pt_d_bm where i_bmdm=a.i_bmdm) as V_BMMC,(select RoleTypeName from PT_d_Role where RoleTypeCode=a.DutyCode) as RoleTypeName from PT_Duty a where charindex(','+Convert(varchar(20),a.I_DUTYID)+',',','+@OtherDutyIDs+',')>0 ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable GetDutyView(string EmployeeCode, string type)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            if (type == "o")
            {
                StringBuilder builder2 = new StringBuilder();
                builder2.Append(" select RecordID from HR_Personnel_Changes where EmployeeCode='" + EmployeeCode + "' order by ApplyDate desc ");
                DataTable table2 = publicDbOpClass.DataTableQuary(builder2.ToString());
                if (table2.Rows.Count >= 2)
                {
                    builder.Append(" select IsMain,(select V_BMMC from pt_d_bm where i_bmdm=(select i_bmdm from pt_Duty where I_DUTYID=a.DutyID)) as V_BMMC, ");
                    builder.Append(" (select RoleTypeName from PT_d_Role where RoleTypeCode=(select DutyCode from PT_Duty where I_DUTYID=a.DutyID)) as RoleTypeName ");
                    builder.Append(" from HR_Personnel_ChangesDuty a ");
                    builder.Append(" where a.RecordID='" + table2.Rows[1]["RecordID"].ToString() + "' ");
                }
            }
            if (type == "n")
            {
                builder.Append(" select IsMain,(select V_BMMC from pt_d_bm where i_bmdm=(select i_bmdm from pt_Duty where I_DUTYID=a.DutyID)) as V_BMMC, ");
                builder.Append(" (select RoleTypeName from PT_d_Role where RoleTypeCode=(select DutyCode from PT_Duty where I_DUTYID=a.DutyID)) as RoleTypeName ");
                builder.Append(" from HR_Personnel_ChangesDuty a ");
                builder.Append(" where a.RecordID=(select top 1 RecordID from HR_Personnel_Changes where EmployeeCode='" + EmployeeCode + "' order by ApplyDate desc) ");
            }
            try
            {
                table = publicDbOpClass.DataTableQuary(builder.ToString());
            }
            catch
            {
            }
            return table;
        }

        public static DataTable GetGroupNewDuty(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select a.*,(select V_BMMC from pt_d_bm where i_bmdm=(select i_bmdm from pt_Duty where I_DUTYID=a.DutyID)) as V_BMMC, ");
            builder.Append(" (select RoleTypeName from PT_d_Role where RoleTypeCode=(select DutyCode from PT_Duty where I_DUTYID=a.DutyID)) as RoleTypeName ");
            builder.Append(" from HR_Personnel_GroupChangesDuty a where a.RecordID='" + RecordID + "' ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable GetGroupOldDuty(string yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" declare @OtherDutyIDs varchar(500) set @OtherDutyIDs='' ");
            builder.Append(" select @OtherDutyIDs=OtherDutyIDs from PT_YHMC where v_yhdm='" + yhdm + "' ");
            builder.Append(" select a.*,1 as MainRole,(select V_BMMC from pt_d_bm where i_bmdm=a.i_bmdm) as V_BMMC,(select RoleTypeName from PT_d_Role where RoleTypeCode=a.DutyCode) as RoleTypeName from PT_Duty a where a.I_DUTYID=(select I_DUTYID from PT_YHMC where v_yhdm='" + yhdm + "') ");
            builder.Append(" union ");
            builder.Append(" select a.*,0 as MainRole,(select V_BMMC from pt_d_bm where i_bmdm=a.i_bmdm) as V_BMMC,(select RoleTypeName from PT_d_Role where RoleTypeCode=a.DutyCode) as RoleTypeName from PT_Duty a where charindex(','+Convert(varchar(20),a.I_DUTYID)+',',','+@OtherDutyIDs+',')>0 ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable GetNewDuty(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select a.*,(select V_BMMC from pt_d_bm where i_bmdm=(select i_bmdm from pt_Duty where I_DUTYID=a.DutyID)) as V_BMMC, ");
            builder.Append(" (select RoleTypeName from PT_d_Role where RoleTypeCode=(select DutyCode from PT_Duty where I_DUTYID=a.DutyID)) as RoleTypeName ");
            builder.Append(" from HR_Personnel_ChangesDuty a where a.RecordID='" + RecordID + "' ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable GetOldDuty(string yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" declare @OtherDutyIDs varchar(500) set @OtherDutyIDs='' ");
            builder.Append(" select @OtherDutyIDs=OtherDutyIDs from PT_YHMC where v_yhdm='" + yhdm + "' ");
            builder.Append(" select a.*,1 as MainRole,(select V_BMMC from pt_d_bm where i_bmdm=a.i_bmdm) as V_BMMC,(select RoleTypeName from PT_d_Role where RoleTypeCode=a.DutyCode) as RoleTypeName from PT_Duty a where a.I_DUTYID=(select I_DUTYID from PT_YHMC where v_yhdm='" + yhdm + "') ");
            builder.Append(" union ");
            builder.Append(" select a.*,0 as MainRole,(select V_BMMC from pt_d_bm where i_bmdm=a.i_bmdm) as V_BMMC,(select RoleTypeName from PT_d_Role where RoleTypeCode=a.DutyCode) as RoleTypeName from PT_Duty a where charindex(','+Convert(varchar(20),a.I_DUTYID)+',',','+@OtherDutyIDs+',')>0 ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable GetUserInfoTab(string yhdm)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *,datediff(yyyy,BirthDay,getdate()) as Age from PT_yhmc where v_yhdm='" + yhdm + "' ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }
    }
}

