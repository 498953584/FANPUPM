namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class OAOfficeCommonClas
    {
        public static decimal GetAlready(string dept)
        {
            DataTable table = publicDbOpClass.DataTableQuary(((" select isnull(sum(a.ApplyNumber*b.PlanFee),0) " + " from OA_OfficeRes_ApplicationCollectDetail a join OA_OfficeRes_Resources b " + " on a.MaterialID=b.RecordID ") + " where ACRecordID in (select a.ACRecordID from OA_OfficeRes_DepartmentApplication a " + " join OA_OfficeRes_ApplicationCollect b on a.ACRecordID = b.ACRecordID ") + " where ApplyDepartment = " + dept + " and b.AuditState = 1)");
            if (table.Rows.Count > 0)
            {
                return Convert.ToDecimal(table.Rows[0][0]);
            }
            return 0M;
        }

        public static string GetChinaNum(string strNum)
        {
            string str = "";
            if (strNum.Length > 0)
            {
                string[] strArray = new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
                for (int i = 0; i < strNum.Length; i++)
                {
                    int index = int.Parse(strNum.Substring(i, 1));
                    str = str + strArray[index];
                }
            }
            return (str + "级");
        }

        public static decimal GetDepartmentRation(string RecordCode)
        {
            object obj2 = publicDbOpClass.ExecuteScalar(("" + " select isnull(Ration,0) from OA_OfficeRes_RationSet ") + " where RationType='1' and RecordCode='" + RecordCode + "' ");
            if ((obj2 != null) && (obj2.ToString() != ""))
            {
                return (decimal) obj2;
            }
            return 0M;
        }

        public static decimal GetMonthStat_Department(string bmbm, int year, int month)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select isnull(sum(isnull(c.PlanFee,0)*isnull(b.ApplyNumber,0)),0) ");
            builder.Append(" from OA_OfficeRes_DepartmentApplication a ");
            builder.Append(" join OA_OfficeRes_DepartmentApplicationDetail b on a.DARecordID=b.DARecordID ");
            builder.Append(" join OA_OfficeRes_Resources c on b.MaterialID=c.RecordID ");
            builder.Append(" where ");
            builder.Append(" a.ApplyDepartment in (select i_bmdm from pt_d_bm where v_bmbm like '" + bmbm + "%') and ");
            builder.Append(" year(a.ApplyDate)=" + year + " and ");
            builder.Append(" month(a.ApplyDate)=" + month + " and ");
            builder.Append(" a.IsHaveDo='1' ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != null)
            {
                return (decimal) obj2;
            }
            return 0M;
        }

        public static decimal GetMonthStat_Person(string bmbm, int year, int month)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select isnull(sum(isnull(c.PlanFee,0)*isnull(b.ApplyNumber,0)),0) ");
            builder.Append(" from OA_OfficeRes_PersonalApplication a ");
            builder.Append(" join OA_OfficeRes_PersonalApplicationDetail b on a.PARecordID=b.PARecordID ");
            builder.Append(" join OA_OfficeRes_Resources c on b.MaterialID=c.RecordID ");
            builder.Append(" where ");
            builder.Append(" a.UseMan in (select v_yhdm from pt_yhmc where i_bmdm in (select i_bmdm from pt_d_bm where v_bmbm like '" + bmbm + "%')) and ");
            builder.Append(" year(a.ApplyDate)=" + year + " and ");
            builder.Append(" month(a.ApplyDate)=" + month + " and ");
            builder.Append(" a.IsHaveDo='1' ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != null)
            {
                return (decimal) obj2;
            }
            return 0M;
        }

        public static DataTable GetPersonInfo(string strUserCode)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + " select * from PT_yhmc where v_yhdm='" + strUserCode + "' ");
        }

        public static string GetPersonInfo(string strUserCode, string strKey)
        {
            object obj2 = publicDbOpClass.ExecuteScalar(("" + " select " + strKey + " from PT_yhmc ") + " where v_yhdm='" + strUserCode + "' ");
            if (obj2 != null)
            {
                return obj2.ToString();
            }
            return "";
        }

        public static decimal GetPersonRation(string RecordCode)
        {
            object obj2 = publicDbOpClass.ExecuteScalar(("" + " select isnull(Ration,0) from OA_OfficeRes_RationSet ") + " where RationType='0' and RecordCode='" + RecordCode + "' ");
            if ((obj2 != null) && (obj2.ToString() != ""))
            {
                return (decimal) obj2;
            }
            return 0M;
        }

        public static string GetPostAndRank(string PostLevel)
        {
            object obj2 = publicDbOpClass.ExecuteScalar("" + " select PostAndRank from HR_Org_PostLevel where RecordID='" + PostLevel + "' ");
            if ((obj2 != null) && (obj2.ToString() != ""))
            {
                return (string) obj2;
            }
            return "";
        }

        public static decimal GetSubCompanyRation(string RecordCode)
        {
            object obj2 = publicDbOpClass.ExecuteScalar(("" + " select isnull(Ration,0) from OA_OfficeRes_RationSet ") + " where RationType='2' and RecordCode='" + RecordCode + "' ");
            if ((obj2 != null) && (obj2.ToString() != ""))
            {
                return (decimal) obj2;
            }
            return 0M;
        }
    }
}

