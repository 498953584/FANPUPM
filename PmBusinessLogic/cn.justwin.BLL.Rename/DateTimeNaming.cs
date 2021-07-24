namespace cn.justwin.BLL.Rename
{
    using System;
    using System.IO;

    public class DateTimeNaming : INaming
    {
        public string Rename(string fullName)
        {
            int num = fullName.LastIndexOf(@"\");
            int num2 = fullName.LastIndexOf(".");
            string str = fullName.Substring(0, num + 1);
            fullName.Substring(num + 1, (num2 - num) - 1);
            string extension = Path.GetExtension(fullName);
            string str3 = DateTime.Now.ToString("yyyyMMddHHmmssms");
            return string.Concat(new object[] { str, str3, new Random().Next(0x3e8, 0x2710), extension });
        }
    }
}

