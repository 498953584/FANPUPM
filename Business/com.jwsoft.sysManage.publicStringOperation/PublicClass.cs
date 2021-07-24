namespace com.jwsoft.sysManage.publicStringOperation
{
    using System;

    public class PublicClass
    {
        public static string CheckString(string str)
        {
            return str.Replace("'", "''");
        }

        public string CheckStringAdd(string sic, string obj)
        {
            string[] strArray = obj.Split(new char[] { ',' });
            int length = strArray.GetLength(0);
            string str = "," + sic + ",";
            string str2 = "";
            while (length > 0)
            {
                string str3 = "," + strArray[length - 1] + ",";
                if (str.IndexOf(str3) == -1)
                {
                    if (str2 == "")
                    {
                        str2 = strArray[length - 1];
                    }
                    else
                    {
                        str2 = str2 + "," + strArray[length - 1];
                    }
                }
                length--;
            }
            return str2;
        }

        public static string strEnterTostrBr(string strIn)
        {
            string[] strArray = strIn.Split(new char[] { '\r' });
            int length = strArray.Length;
            string str = "";
            for (int i = 0; i < (length - 1); i++)
            {
                str = str + strArray[i] + "<br>";
            }
            return (str + strArray[length - 1]).Replace(" ", "&nbsp;");
        }

        public static string truncString(string oldStr, int length)
        {
            int num = 0;
            int num2 = 0;
            string str = "";
            foreach (char ch in oldStr)
            {
                if (ch > '\x007f')
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num > length)
                {
                    str = oldStr.Substring(0, num2) + "...";
                    break;
                }
                num2++;
            }
            if (num < length)
            {
                str = oldStr;
            }
            return str;
        }
    }
}

