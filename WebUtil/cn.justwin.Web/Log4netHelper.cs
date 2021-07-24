namespace cn.justwin.Web
{
    using log4net;
    using log4net.Config;
    using System;
    using System.IO;

    public sealed class Log4netHelper
    {
        private static readonly string configFullName = (AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log4net.config");

        /// <summary>
        /// 构造方法
        /// </summary>
        private Log4netHelper()
        {
            StartConfigure();
        }

        /// <summary>
        /// 关闭Log4Net
        /// </summary>
        public static void CloseLog4net()
        {
            LogManager.Shutdown();
        }

        /// <summary>
        /// 封装log4net的Debug方法
        /// </summary>
        /// <param name="obj"></param>
        public static void Debug(object obj, string title)
        {
            LogManager.GetLogger(title).Debug(obj);
        }

        /// <summary>
        /// 封装log4net的Error方法
        /// </summary>
        /// <param name="obj">要记入日志的信息</param>
        public static void Error(Exception ex, string title, string usreCode)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "Exception";
            }
            LogManager.GetLogger(title).Error(usreCode, ex);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="title">日志标题</param>
        /// <param name="obj">日志内容</param>
        public static void Info(object obj, string title)
        {
            LogManager.GetLogger(title).Info(obj);
        }

        /// <summary>
        /// 在Application_Start()方法中调用此方法
        /// </summary>
        public static void StartConfigure()
        {
            FileInfo configFile = new FileInfo(configFullName);
            XmlConfigurator.ConfigureAndWatch(configFile);
        }
    }
}

