namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public class ProgressProceedAction
    {
        public static bool AddNewProce(ProgressProceedInfo objInfo, out int MainID)
        {
            objInfo.MainID = GetNewMainID();
            string str2 = "insert into Prj_TechnologyAdvancementIncome values(" + objInfo.MainID.ToString() + ",'" + objInfo.PrjCode + "'";
            string str3 = str2 + ",'" + objInfo.FruitName + "','" + objInfo.AdministerFruitUnit + "'," + objInfo.EtcaeterasPeopleAmount.ToString();
            string str4 = str3 + ",'" + objInfo.AppPrejectName + "'," + objInfo.PPMAdvancementIncomeCount.ToString();
            string str5 = str4 + ",'" + objInfo.StartDate.ToString() + "','" + objInfo.EndDate.ToString();
            string sqlString = (str5 + "','" + objInfo.Account + "','" + objInfo.FruitContent + "','" + objInfo.Engineer + "','" + objInfo.PrejectMinister) + "','" + objInfo.DealinMinister + "','','',0,'','',0,'','','','',0)";
            MainID = objInfo.MainID;
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public static bool CanncleAuditState(int MainId)
        {
            return publicDbOpClass.NonQuerySqlString(string.Format("update Prj_TechnologyAdvancementIncome set EntAuditResult=-1,EntAuditPeople='',ComAuditValue=0 where MainId={0}", MainId));
        }

        public static bool DelProce(int MainID)
        {
            return publicDbOpClass.NonQuerySqlString(string.Format("delete Prj_TechnologyAdvancementIncome where mainid={0}", MainID));
        }

        public static bool EditProce(ProgressProceedInfo objInfo)
        {
            string str2 = "Update Prj_TechnologyAdvancementIncome set FruitName='";
            string str3 = str2 + objInfo.FruitName + "',AdministerFruitUnit='" + objInfo.AdministerFruitUnit + "',EtcaeterasPeopleAmount=";
            string str4 = str3 + objInfo.EtcaeterasPeopleAmount.ToString() + ",AppPrejectName='" + objInfo.AppPrejectName + "',StartDate='";
            string str5 = (str4 + objInfo.StartDate.ToString() + "',EndDate='" + objInfo.EndDate.ToString() + "',PPMAdvancementIncomeCount=") + objInfo.PPMAdvancementIncomeCount.ToString();
            string str6 = str5 + ",Account='" + objInfo.Account + "',FruitContent='" + objInfo.FruitContent + "',Engineer='" + objInfo.Engineer;
            return publicDbOpClass.NonQuerySqlString((str6 + "',PrejectMinister='" + objInfo.PrejectMinister + "',DealinMinister='" + objInfo.DealinMinister + "'") + " where MainID = " + objInfo.MainID.ToString());
        }

        public static bool EntpmAuditProce(ProgressProceedInfo objInfo)
        {
            string str2 = "Update Prj_TechnologyAdvancementIncome set EntAuditPeople='" + objInfo.EntAuditPeople + "',EntAuditDate='";
            string str3 = str2 + objInfo.EntAuditDate.ToString() + "',EntAuditResult=" + (objInfo.EntAuditResult ? "1" : "0") + ",EntAuditIdea='";
            return publicDbOpClass.NonQuerySqlString(str3 + objInfo.EntAuditIdea + "',ComAuditValue=" + objInfo.AuditValue.ToString() + " where MainId = " + objInfo.MainID.ToString());
        }

        private ProgressProceedInfo FormatToModel(DataRow dr)
        {
            ProgressProceedInfo info = new ProgressProceedInfo {
                Account = dr["Account"].ToString(),
                AdministerFruitUnit = dr["AdministerFruitUnit"].ToString(),
                AppPrejectName = dr["AppPrejectName"].ToString(),
                DealinMinister = dr["DealinMinister"].ToString(),
                EndDate = DateTime.Parse(dr["EndDate"].ToString()),
                PPMAdvancementIncomeCount = decimal.Parse(dr["PPMAdvancementIncomeCount"].ToString()),
                Engineer = dr["Engineer"].ToString()
            };
            if (DateTime.Parse(dr["EntAuditDate"].ToString()).ToShortDateString() != "1900-1-1")
            {
                info.EntAuditDate = DateTime.Parse(dr["EntAuditDate"].ToString());
            }
            info.EntAuditIdea = dr["EntAuditIdea"].ToString();
            info.EntAuditPeople = dr["EntAuditPeople"].ToString();
            info.EntAuditResult = dr["EntAuditResult"].ToString() == "True";
            info.EtcaeterasPeopleAmount = int.Parse(dr["EtcaeterasPeopleAmount"].ToString());
            info.FruitContent = dr["FruitContent"].ToString();
            info.FruitName = dr["FruitName"].ToString();
            info.MainID = int.Parse(dr["MainID"].ToString());
            info.PPMAuditResult = int.Parse(dr["PPMAuditResult"].ToString());
            info.PPMCommitteeIdea = dr["PPMCommitteeIdea"].ToString();
            info.PPMDeclareUnitIdea = dr["PPMDeclareUnitIdea"].ToString();
            info.PPMGroupIdea = dr["PPMGroupIdea"].ToString();
            info.PPMRemark = dr["PPMRemark"].ToString();
            info.PPMSerialNumber = dr["PPMSerialNumber"].ToString();
            info.PrejectMinister = dr["PrejectMinister"].ToString();
            info.PrjCode = dr["PrjCode"].ToString();
            info.StartDate = DateTime.Parse(dr["StartDate"].ToString());
            info.AuditValue = decimal.Parse(dr["ComAuditValue"].ToString());
            return info;
        }

        private static int GetNewMainID()
        {
            string sqlString = "select isnull(Max(MainId),0)+1 from Prj_TechnologyAdvancementIncome";
            return int.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString());
        }

        public ProgressProceedInfo GetProceInfo(int MainID)
        {
            string sqlString = string.Format("select * from Prj_TechnologyAdvancementIncome where MainID={0}", MainID);
            ProgressProceedInfo info = new ProgressProceedInfo();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                info = this.FormatToModel(row);
            }
            return info;
        }

        public ProgressProceedCollection GetProceInfos(string prjCode)
        {
            string sqlString = string.Format("select * from Prj_TechnologyAdvancementIncome where prjCode='{0}'", prjCode);
            ProgressProceedCollection proceeds = new ProgressProceedCollection();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                proceeds.Add(this.FormatToModel(row));
            }
            return proceeds;
        }

        public static bool PpmAuditProce(ProgressProceedInfo objInfo)
        {
            string str2 = "Update Prj_TechnologyAdvancementIncome set PPMSerialNumber='" + objInfo.PPMSerialNumber + "',PPMAuditResult=";
            string str3 = str2 + objInfo.PPMAuditResult.ToString() + ",PPMDeclareUnitIdea='" + objInfo.PPMDeclareUnitIdea + "',PPMGroupIdea='";
            return publicDbOpClass.NonQuerySqlString((str3 + objInfo.PPMGroupIdea + "',PPMCommitteeIdea='" + objInfo.PPMCommitteeIdea + "',PPMRemark='") + objInfo.PPMRemark + "' where MainID = " + objInfo.MainID.ToString());
        }
    }
}

