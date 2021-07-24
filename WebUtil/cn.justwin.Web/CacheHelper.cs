namespace cn.justwin.Web
{
    using Memcached.ClientLibrary;
    using System;

    /// <summary>
    /// CacheHelper
    /// </summary>
    public static class CacheHelper
    {
        private static MemcachedClient mc;

        /// <summary>
        /// 构造函数
        /// </summary>
        static CacheHelper()
        {
            InitSockIOPool();
            mc = new MemcachedClient();
            mc.EnableCompression = true;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Delete(string key)
        {
            mc.Delete(key);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return mc.KeyExists(key);
        }

        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return mc.Get(key);
        }

        /// <summary>
        /// 初始化SockIOPool
        /// </summary>
        private static void InitSockIOPool()
        {
            string[] servers = new string[] { "127.0.0.1:11211" };
            SockIOPool instance = SockIOPool.GetInstance();
            instance.SetServers(servers);
            instance.InitConnections = 3;
            instance.MinConnections = 3;
            instance.MaxConnections = 50;
            instance.SocketConnectTimeout = 0x3e8;
            instance.SocketTimeout = 0xbb8;
            instance.MaintenanceSleep = 30L;
            instance.Failover = true;
            instance.Nagle = false;
            instance.Initialize();
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void Set(string key, object val)
        {
            mc.Set(key, val);
        }
    }
}

