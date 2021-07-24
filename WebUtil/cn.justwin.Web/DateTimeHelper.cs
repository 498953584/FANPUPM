namespace cn.justwin.Web
{
    using System;

    /// <summary>
    /// DateTime帮助类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 返回可空日期类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? GetDateTime(object obj)
        {
            try
            {
                return new DateTime?(Convert.ToDateTime(obj));
            }
            catch
            {
                return null;
            }
        }
    }
}

