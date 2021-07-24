namespace cn.justwin.Web
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// JwDebug
    /// </summary>
    public static class JwDebug
    {
        /// <summary>
        /// 条件为假时抛出异常
        /// </summary>
        /// <param name="condition">条件</param>
        [Conditional("DEBUG")]
        public static void Assert(bool condition)
        {
            if (!condition)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 条件为假时抛出异常
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="ex">异常</param>
        [Conditional("DEBUG")]
        public static void Assert(bool condition, Exception ex)
        {
            if (!condition)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 条件为假时抛出异常
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="msg">异常的信息</param>
        [Conditional("DEBUG")]
        public static void Assert(bool condition, string msg)
        {
            if (!condition)
            {
                throw new Exception(msg);
            }
        }
    }
}

