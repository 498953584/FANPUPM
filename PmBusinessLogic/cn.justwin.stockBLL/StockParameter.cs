namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using System;
    using System.Collections.Specialized;

    public static class StockParameter
    {
        public static bool DepotBindType;
        public static string DepotType;
        public static double HighPlanAlarmLimit;
        public static bool IsContainPending;
        public static bool IsHighPlanAlarm;
        public static bool IsLowAlarm;
        public static bool IsLowPlanAlarm;
        public static bool IsMidPlanAlarm;
        public static double LowPlanAlarmLowerLimit;
        public static double LowPlanAlarmUpperLimit;
        public static double MidPlanAlarmLowerLimit;
        public static double MidPlanAlarmUpperLimit;
        public static string ProjectAlarm;
        public static bool ProjectTransparentSet;

        static StockParameter()
        {
            Init();
        }

        public static string getSm_setValue(string paraname)
        {
            return basicSetDal.getSm_setValue(paraname);
        }

        private static void Init()
        {
            NameValueCollection basicParameters = basicSetBLL.GetBasicParameters();
            DepotType = basicParameters["DepotType"];
            DepotBindType = basicParameters["DepotBindType"] == "Bind";
            ProjectAlarm = basicParameters["ProjectAlarm"];
            ProjectTransparentSet = basicParameters["ProjectTransparentSet"] == "Transparent";
            IsLowAlarm = basicParameters["IsLowAlarm"] == "1";
            IsHighPlanAlarm = basicParameters["IsHighPlanAlarm"] == "1";
            HighPlanAlarmLimit = double.Parse(basicParameters["HighPlanAlarmLimit"]);
            IsMidPlanAlarm = basicParameters["IsMidPlanAlarm"] == "1";
            MidPlanAlarmUpperLimit = double.Parse(basicParameters["MidPlanAlarmUpperLimit"]);
            MidPlanAlarmLowerLimit = double.Parse(basicParameters["MidPlanAlarmLowerLimit"]);
            IsLowPlanAlarm = basicParameters["IsLowPlanAlarm"] == "1";
            LowPlanAlarmUpperLimit = double.Parse(basicParameters["LowPlanAlarmUpperLimit"]);
            LowPlanAlarmLowerLimit = double.Parse(basicParameters["LowPlanAlarmLowerLimit"]);
            IsContainPending = basicParameters["IsContainPending"] == "1";
        }

        public static void Update()
        {
            Init();
        }

        public static int UpdateDatabase()
        {
            int num = 1;
            try
            {
                UpdateVal("DepotType", DepotType);
                UpdateVal("DepotBindType", DepotBindType ? "Bind" : "UnBind");
                UpdateVal("ProjectAlarm", ProjectAlarm);
                UpdateVal("ProjectTransparentSet", ProjectTransparentSet ? "Transparent" : "UnTransparent");
                UpdateVal("IsLowAlarm", IsLowAlarm ? "1" : "0");
                UpdateVal("IsHighPlanAlarm", IsHighPlanAlarm ? "1" : "0");
                UpdateVal("HighPlanAlarmLimit", HighPlanAlarmLimit.ToString());
                UpdateVal("IsMidPlanAlarm", IsMidPlanAlarm ? "1" : "0");
                UpdateVal("MidPlanAlarmUpperLimit", MidPlanAlarmUpperLimit.ToString());
                UpdateVal("MidPlanAlarmLowerLimit", MidPlanAlarmLowerLimit.ToString());
                UpdateVal("IsLowPlanAlarm", IsLowPlanAlarm ? "1" : "0");
                UpdateVal("MidPlanAlarmUpperLimit", MidPlanAlarmUpperLimit.ToString());
                UpdateVal("MidPlanAlarmLowerLimit", MidPlanAlarmLowerLimit.ToString());
                UpdateVal("IsContainPending", IsContainPending ? "1" : "0");
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        public static int UpdateVal(string key, string val)
        {
            return basicSetDal.UpdateSetByKeyVal(key, val);
        }
    }
}

