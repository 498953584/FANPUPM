namespace cn.justwin.Web
{
    using System;

    public static class FileHelper
    {
        /// <summary>
        /// 根据文件的完整路径返回文件所在地文件夹
        /// </summary>
        /// <param name="path">文件的完整路径</param>
        /// <returns>文件所在地文件夹</returns>
        public static string GetFileDir(string path)
        {
            int length = path.LastIndexOf('\\') + 1;
            return path.Substring(0, length);
        }

        /// <summary>
        /// 根据文件的完整路径名称返回文件的文件名
        /// </summary>
        /// <param name="path">文件的完整路径名称</param>
        /// <returns>文件名</returns>
        public static string GetFileName(string path)
        {
            int startIndex = path.LastIndexOf('\\') + 1;
            return path.Substring(startIndex);
        }
    }
}

