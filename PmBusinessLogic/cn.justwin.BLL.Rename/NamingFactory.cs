namespace cn.justwin.BLL.Rename
{
    using System;

    public class NamingFactory
    {
        public INaming CreateNaming()
        {
            return new DateTimeNaming();
        }
    }
}

