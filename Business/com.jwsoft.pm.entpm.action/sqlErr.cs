namespace com.jwsoft.pm.entpm.action
{
    using System;

    public class sqlErr
    {
        public static string sqlerr(int errnum)
        {
            if (errnum == 0xa43)
            {
                return "编号重复;";
            }
            return "";
        }
    }
}

