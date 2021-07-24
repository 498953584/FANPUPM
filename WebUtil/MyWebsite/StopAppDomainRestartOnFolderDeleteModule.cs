namespace MyWebsite
{
    using System;
    using System.Reflection;
    using System.Web;

    /// <summary>
    /// 防止删除目录，session丢失
    /// </summary>
    public class StopAppDomainRestartOnFolderDeleteModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            object obj2 = typeof(HttpRuntime).GetProperty("FileChangesMonitor", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).GetValue(null, null);
            object obj3 = obj2.GetType().GetField("_dirMonSubdirs", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase).GetValue(obj2);
            obj3.GetType().GetMethod("StopMonitoring", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj3, new object[0]);
        }
    }
}

