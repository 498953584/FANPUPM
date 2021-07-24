namespace cn.justwin.contractBLL
{
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;

    public class ContractParameter
    {
        private static bool loaded = false;
        private static Dictionary<string, string> parameters = new Dictionary<string, string>();

        public ContractParameter()
        {
            if (!loaded)
            {
                LoadParameters();
            }
        }

        private bool GetBoolean(string name)
        {
            return (this.GetValue(name) == "1");
        }

        private int GetInt32(string name)
        {
            string str = this.GetValue(name);
            if (!string.IsNullOrEmpty(str))
            {
                return Convert.ToInt32(str);
            }
            return 0;
        }

        private string GetValue(string name)
        {
            if (parameters.ContainsKey(name))
            {
                return parameters[name];
            }
            return string.Empty;
        }

        private static void LoadParameters()
        {
            Config config = new Config();
            foreach (ConfigModel model in config.GetAllList())
            {
                if (parameters.ContainsKey(model.ParaName))
                {
                    parameters[model.ParaName] = model.ParaValue;
                }
                else
                {
                    parameters.Add(model.ParaName, model.ParaValue);
                }
            }
            loaded = true;
        }

        public static void Update(Dictionary<string, string> parameters)
        {
            new Config().Update(parameters);
            loaded = false;
        }

        public static int HighBalanceAlarmLimit
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("HighBalanceAlarmLimit");
            }
        }

        public static int HighPayAlarmLimit
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("HighPayAlarmLimit");
            }
        }

        public static int IncomeAlarmDays
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("IncomeAlarmDays");
            }
        }

        public static bool IsBalanceAlarm
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetBoolean("IsBalanceAlarm");
            }
        }

        public static bool IsHighBalanceAlarm
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetBoolean("IsHighBalanceAlarm");
            }
        }

        public static bool IsHighPayAlarm
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetBoolean("IsHighPayAlarm");
            }
        }

        public static bool IsIncomeAlarm
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetBoolean("IsIncomeAlarm");
            }
        }

        public static bool IsLowBalanceAlarm
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetBoolean("IsLowBalanceAlarm");
            }
        }

        public static bool IsLowPayAlarm
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetBoolean("IsLowPayAlarm");
            }
        }

        public static bool IsMidBalanceAlarm
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetBoolean("IsMidBalanceAlarm");
            }
        }

        public static bool IsMidPayAlarm
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetBoolean("IsMidPayAlarm");
            }
        }

        public static bool IsPaymentAlarm
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetBoolean("IsPaymentAlarm");
            }
        }

        public static bool IsPayoutAlarm
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetBoolean("IsPayoutAlarm");
            }
        }

        public static int LowBalanceAlarmLowerLimit
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("LowBalanceAlarmLowerLimit");
            }
        }

        public static int LowBalanceAlarmUpperLimit
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("LowBalanceAlarmUpperLimit");
            }
        }

        public static int LowPayAlarmLowerLimit
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("LowPayAlarmLowerLimit");
            }
        }

        public static int LowPayAlarmUpperLimit
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("LowPayAlarmUpperLimit");
            }
        }

        public static int MidBalanceAlarmLowerLimit
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("MidBalanceAlarmLowerLimit");
            }
        }

        public static int MidBalanceAlarmUpperLimit
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("MidBalanceAlarmUpperLimit");
            }
        }

        public static int MidPayAlarmLowerLimit
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("MidPayAlarmLowerLimit");
            }
        }

        public static int MidPayAlarmUpperLimit
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("MidPayAlarmUpperLimit");
            }
        }

        public static int PayoutAlarmDays
        {
            get
            {
                ContractParameter parameter = new ContractParameter();
                return parameter.GetInt32("PayoutAlarmDays");
            }
        }
    }
}

