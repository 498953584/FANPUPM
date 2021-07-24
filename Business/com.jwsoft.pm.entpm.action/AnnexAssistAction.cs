namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class AnnexAssistAction
    {
        public static AnnexModuleSettingsInfo GetAnnexModelInfo(int moduleID)
        {
            AnnexModuleSettingsInfo info = new AnnexModuleSettingsInfo();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_AnnexSettings where ModuleID = " + moduleID.ToString()))
            {
                if (table.Rows.Count == 1)
                {
                    return GetSetttingsInfoFromDataRow(table.Rows[0]);
                }
            }
            return info;
        }

        public static AnnexModuleSettingsInfo GetAnnexModelInfo(string strRemark)
        {
            AnnexModuleSettingsInfo info = new AnnexModuleSettingsInfo();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_AnnexSettings where ModuleName = '" + strRemark + "'"))
            {
                if (table.Rows.Count == 1)
                {
                    return GetSetttingsInfoFromDataRow(table.Rows[0]);
                }
            }
            return info;
        }

        public static AnnexModuleSettingsInfo GetBidInfo()
        {
            string strRemark = "投标信息";
            return GetAnnexModelInfo(strRemark);
        }

        public static AnnexModuleSettingsInfo GetConPrjScheme()
        {
            string strRemark = "合同外包时招标信息";
            return GetAnnexModelInfo(strRemark);
        }

        public static AnnexModuleSettingsInfo GetHrPhoto()
        {
            string strRemark = "人事管理";
            return GetAnnexModelInfo(strRemark);
        }

        public static AnnexModuleSettingsInfo GetInviteBidInfo()
        {
            string strRemark = "招标信息";
            return GetAnnexModelInfo(strRemark);
        }

        public static AnnexModuleSettingsInfo GetPrjConference()
        {
            string strRemark = "项目会议";
            return GetAnnexModelInfo(strRemark);
        }

        public static AnnexModuleSettingsInfo GetProjectExponent()
        {
            string strRemark = "项目范围说明书";
            return GetAnnexModelInfo(strRemark);
        }

        public static AnnexModuleSettingsInfo GetQSTable()
        {
            string strRemark = "质安报表附件";
            return GetAnnexModelInfo(strRemark);
        }

        public static AnnexModuleSettingsInfo GetQuality()
        {
            string strRemark = "质量规范";
            return GetAnnexModelInfo(strRemark);
        }

        public static AnnexModuleSettingsInfo GetSafty()
        {
            string strRemark = "安全规范";
            return GetAnnexModelInfo(strRemark);
        }

        private static AnnexModuleSettingsInfo GetSetttingsInfoFromDataRow(DataRow dr)
        {
            return new AnnexModuleSettingsInfo { ModuleID = (int) dr["ModuleID"], ModuleCode = dr["ModuleCode"].ToString(), ModuleName = dr["ModuleName"].ToString(), FileSize = (int) dr["FileSize"], ExtName = dr["ExtName"].ToString(), FilePath = dr["FilePath"].ToString(), FileNumber = (int) dr["FileNumber"], Remark = dr["Remark"].ToString() };
        }

        public static AnnexModuleSettingsInfo GetSubPrj()
        {
            string strRemark = "单位工程相关附件";
            return GetAnnexModelInfo(strRemark);
        }
    }
}

