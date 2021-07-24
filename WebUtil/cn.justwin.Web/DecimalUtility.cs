namespace cn.justwin.Web
{
    using System;

    /// <summary>
    /// Decimal辅助类
    /// </summary>
    public static class DecimalUtility
    {
        /// <summary>
        /// 小数位数
        /// </summary>
        public static readonly int decimalNumber = 3;

        /// <summary>
        /// 转换类型，转换失败返回null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal? ConvertDecimal(object obj)
        {
            decimal? nullable = null;
            try
            {
                nullable = new decimal?(decimal.Parse(obj.ToString()));
            }
            catch
            {
            }
            return nullable;
        }

        /// <summary>
        /// 格式化小数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatToString(object obj)
        {
            if (obj == DBNull.Value)
            {
                obj = null;
            }
            decimal num = Convert.ToDecimal(obj);
            string format = string.Format("F{0}", decimalNumber);
            return num.ToString(format);
        }
    }
}

