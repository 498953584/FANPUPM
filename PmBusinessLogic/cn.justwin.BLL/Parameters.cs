namespace cn.justwin.BLL
{
    using System;
    using System.Drawing;

    public class Parameters
    {
        public const string AdminCode = "00000000";
        public const string HighBalanceAlarmLimit = "HighBalanceAlarmLimit";
        public static readonly Color HighColor = Color.FromArgb(0xff, 0, 0);
        public const string HighLevel = "高";
        public const string HighPayAlarmLimit = "HighPayAlarmLimit";
        public const string HighState = "高";
        public const string IncomeAlarmDays = "IncomeAlarmDays";
        public const string IsBalanceAlarm = "IsBalanceAlarm";
        public const string IsHighBalanceAlarm = "IsHighBalanceAlarm";
        public const string IsHighPayAlarm = "IsHighPayAlarm";
        public const string IsIncomeAlarm = "IsIncomeAlarm";
        public const string IsLowBalanceAlarm = "IsLowBalanceAlarm";
        public const string IsLowPayAlarm = "IsLowPayAlarm";
        public const string IsMidBalanceAlarm = "IsMidBalanceAlarm";
        public const string IsMidPayAlarm = "IsMidPayAlarm";
        public const string IsPaymentAlarm = "IsPaymentAlarm";
        public const string IsPayoutAlarm = "IsPayoutAlarm";
        public const string LowBalanceAlarmLowerLimit = "LowBalanceAlarmLowerLimit";
        public const string LowBalanceAlarmUpperLimit = "LowBalanceAlarmUpperLimit";
        public static readonly Color LowColor = Color.FromArgb(0, 0, 0xff);
        public const string LowLevel = "低";
        public const string LowPayAlarmLowerLimit = "LowPayAlarmLowerLimit";
        public const string LowPayAlarmUpperLimit = "LowPayAlarmUpperLimit";
        public const string LowState = "低";
        public const string MidBalanceAlarmLowerLimit = "MidBalanceAlarmLowerLimit";
        public const string MidBalanceAlarmUpperLimit = "MidBalanceAlarmUpperLimit";
        public static readonly Color MidColor = Color.FromArgb(0x80, 0, 0x80);
        public const string MidLevel = "中";
        public const string MidPayAlarmLowerLimit = "MidPayAlarmLowerLimit";
        public const string MidPayAlarmUpperLimit = "MidPayAlarmUpperLimit";
        public const string MidState = "中";
        public const string NormalState = "普通";
        public const string PayoutAlarmDays = "PayoutAlarmDays";
        public static readonly string[] PrjAvaildState = new string[] { "5", "7", "8", "9", "10", "11", "12", "13" };
        public static readonly string[] PrjAvaildState2 = new string[] { "5", "7", "8", "9", "10", "11", "12", "13", "17" };
        public static readonly string[] PrjAvaildState3 = new string[] { "7", "8" };
        public static readonly string[] PrjAvaildState4 = new string[] { "5", "7", "8", "9", "10" };
        public static readonly string[] PrjAvaildState5 = new string[] { 
            "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", 
            "19"
         };
        public static readonly string[] PrjInvalidState = new string[] { "1", "2", "3", "4", "6", "14", "15", "16", "17", "18" };
        public const string SM_DepotType = "DepotType";
        public const string SM_HighPlanAlarmLimit = "HighPlanAlarmLimit";
        public const string SM_IsContainPending = "IsContainPending";
        public const string SM_IsHighPlanAlarm = "IsHighPlanAlarm";
        public const string SM_IsLowAlarm = "IsLowAlarm";
        public const string SM_IsLowPlanAlarm = "IsLowPlanAlarm";
        public const string SM_IsMidPlanAlarm = "IsMidPlanAlarm";
        public const string SM_LowPlanAlarmLowerLimit = "LowPlanAlarmLowerLimit";
        public const string SM_LowPlanAlarmUpperLimit = "LowPlanAlarmUpperLimit";
        public const string SM_MidPlanAlarmLowerLimit = "MidPlanAlarmLowerLimit";
        public const string SM_MidPlanAlarmUpperLimit = "MidPlanAlarmUpperLimit";
        public const string SM_ProjectAlarm = "ProjectAlarm";
        public const int StringLength = 15;
    }
}

