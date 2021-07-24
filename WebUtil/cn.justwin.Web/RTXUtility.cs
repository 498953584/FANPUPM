namespace cn.justwin.Web
{
    using System;
    using System.Runtime.CompilerServices;

    public class RTXUtility
    {
        public void SendNotify(string receiver, string title, int millisecond, string content)
        {
        }

        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerIP { get; set; }

        /// <summary>
        /// 服务器端口号
        /// </summary>
        public short ServerPort { get; set; }
    }
}

