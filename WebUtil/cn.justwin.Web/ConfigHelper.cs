namespace cn.justwin.Web
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// ConfigParam 的摘要说明
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// 审核过期时间
        /// </summary>
        private static string auditExpire;
        private static Dictionary<string, string> dic = new Dictionary<string, string>();
        /// <summary>
        /// 项目类型
        /// </summary>
        private static string projectType;
        /// <summary>
        /// 是否支持RTX
        /// </summary>
        private static string rtxEnabled;
        /// <summary>
        /// RTX代办工作提醒周期（小时）
        /// </summary>
        private static string rtxPeriodHour;
        /// <summary>
        /// RTX弹出消息停留时间（秒）
        /// </summary>
        private static string rtxPopupSecond;
        /// <summary>
        /// RTX服务器IP
        /// </summary>
        private static string rtxServerIP;
        /// <summary>
        /// RTX服务器端口号
        /// </summary>
        private static string rtxServerPort;
        /// <summary>
        /// SMTP服务器
        /// </summary>
        private static string smtpHost;
        /// <summary>
        /// 邮箱帐号
        /// </summary>
        private static string smtpId;
        /// <summary>
        /// 邮箱密码
        /// </summary>
        private static string smtpPwd;

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>配置信息</returns>
        public static string Get(string key)
        {
            try
            {
                if (!dic.ContainsKey(key))
                {
                    string str = ConfigurationManager.AppSettings[key];
                    dic.Add(key, str);
                }
                return dic[key];
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 审核过期时间
        /// </summary>
        public static string AuditExpire
        {
            get
            {
                if (string.IsNullOrEmpty(auditExpire))
                {
                    auditExpire = ConfigurationManager.AppSettings["AuditExpire"];
                }
                return auditExpire;
            }
        }

        /// <summary>
        /// 项目类型
        /// </summary>
        public static string ProjectType
        {
            get
            {
                if (string.IsNullOrEmpty(projectType))
                {
                    projectType = ConfigurationManager.AppSettings["ProjectType"];
                }
                return projectType;
            }
        }

        /// <summary>
        /// 是否支持RTX
        /// </summary>
        public static string RTXEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(rtxEnabled))
                {
                    rtxEnabled = ConfigurationManager.AppSettings["RTXEnabled"];
                }
                return rtxEnabled;
            }
        }

        /// <summary>
        /// RTX代办工作提醒周期（小时）
        /// </summary>
        public static string RTXPeriodHour
        {
            get
            {
                if (string.IsNullOrEmpty(rtxPeriodHour))
                {
                    rtxPeriodHour = ConfigurationManager.AppSettings["RTXPeriodHour"];
                }
                return rtxPeriodHour;
            }
        }

        /// <summary>
        /// RTX弹出消息停留时间（秒）
        /// </summary>
        public static string RTXPopupSecond
        {
            get
            {
                if (string.IsNullOrEmpty(rtxPopupSecond))
                {
                    rtxPopupSecond = ConfigurationManager.AppSettings["RTXPopupSecond"];
                }
                return rtxPopupSecond;
            }
        }

        /// <summary>
        /// RTX服务器IP
        /// </summary>
        public static string RTXServerIP
        {
            get
            {
                if (string.IsNullOrEmpty(rtxServerIP))
                {
                    rtxServerIP = ConfigurationManager.AppSettings["RTXServerIP"];
                }
                return rtxServerIP;
            }
        }

        /// <summary>
        /// RTX服务器端口号
        /// </summary>
        public static string RTXServerPort
        {
            get
            {
                if (string.IsNullOrEmpty(rtxServerPort))
                {
                    rtxServerPort = ConfigurationManager.AppSettings["RTXServerPort"];
                }
                return rtxServerPort;
            }
        }

        /// <summary>
        /// SMTP服务器
        /// </summary>
        public static string SmtpHost
        {
            get
            {
                if (string.IsNullOrEmpty(smtpHost))
                {
                    smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                }
                return smtpHost;
            }
        }

        /// <summary>
        /// 邮箱帐号
        /// </summary>
        public static string SmtpId
        {
            get
            {
                if (string.IsNullOrEmpty(smtpId))
                {
                    smtpId = ConfigurationManager.AppSettings["SmtpId"];
                }
                return smtpId;
            }
        }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public static string SmtpPwd
        {
            get
            {
                if (string.IsNullOrEmpty(smtpPwd))
                {
                    smtpPwd = ConfigurationManager.AppSettings["SmtpPwd"];
                }
                return smtpPwd;
            }
        }
    }
}

