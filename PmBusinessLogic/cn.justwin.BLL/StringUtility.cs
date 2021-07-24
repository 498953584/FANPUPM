namespace cn.justwin.BLL
{
    using cn.justwin.DAL;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;

    public class StringUtility
    {
        private static int CompareByLength(string x, string y)
        {
            if (x.Length > y.Length)
            {
                return -1;
            }
            if (x.Length == y.Length)
            {
                return 0;
            }
            return 1;
        }

        public static string DealUrl(string url)
        {
            if (url.IndexOf('?') != -1)
            {
                url = url + "&";
                return url;
            }
            url = url + "?";
            return url;
        }

        public static string FilesBind(string recordCode, string fileKey)
        {
            string path = ConfigHelper.Get(fileKey);
            if (path.Substring(path.Length - 1, 1) != "/")
            {
                path = path + "/";
            }
            try
            {
                string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath(path) + recordCode);
                StringBuilder builder = new StringBuilder();
                foreach (string str2 in files)
                {
                    string str3 = string.Empty;
                    str3 = str2.Substring(str2.LastIndexOf(@"\") + 1);
                    string str = (path + "/" + recordCode) + "/" + str3;
                    str3 = "<a  class=\"link\"  style=\"white-space:nowrap;\"  target=_blank  href=\"../../Common/DownLoad.aspx?path=" + HttpUtility.UrlEncode(str) + "\"  >" + str3 + "</a><br/>";
                    builder.Append(str3);
                }
                return builder.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string GetArrayToInStr(string[] arr)
        {
            return DBHelper.GetInParameterSql(arr);
        }

        public static string GetStr(string str)
        {
            return GetStr(str, 15);
        }

        public static string GetStr(object obj, int length)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return GetStr(obj.ToString(), 0, length);
        }

        public static string GetStr(string str, int length)
        {
            return GetStr(str, 0, length);
        }

        public static string GetStr(string str, int stratIndex, int length)
        {
            if (str.Length <= (stratIndex + length))
            {
                return str;
            }
            return (str.Substring(stratIndex, length) + "...");
        }

        public static string[] GetTemName(string Formula)
        {
            List<string> list = new List<string>();
            string item = "";
            int num = 0;
            for (int i = 0; i < Formula.Length; i++)
            {
                if ((i != 0) && (i != (Formula.Length - 1)))
                {
                    if (Formula[i - 1] == '[')
                    {
                        num = 1;
                    }
                    if (num == 1)
                    {
                        item = item + Formula[i];
                    }
                    if (Formula[i + 1] == ']')
                    {
                        list.Add(item);
                        num = 0;
                        item = "";
                    }
                }
            }
            return list.ToArray();
        }

        public static string GetUserNames(List<string> listUserNames)
        {
            if (listUserNames.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in listUserNames)
            {
                builder.Append(str).Append(",");
            }
            string str2 = builder.ToString();
            return str2.Substring(0, str2.Length - 1);
        }

        public static List<string> ParseString(string str, string leftChar, string rightChar)
        {
            List<string> lst = new List<string>();
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    ParseStringRecursive(str, leftChar, rightChar, lst);
                }
            }
            catch
            {
            }
            lst.Sort(new Comparison<string>(StringUtility.CompareByLength));
            return lst;
        }

        private static void ParseStringRecursive(string str, string leftChar, string rightChar, List<string> lst)
        {
            int startIndex = str.LastIndexOf(leftChar);
            int num2 = str.LastIndexOf(rightChar);
            string item = str.Substring(startIndex + 1, (num2 - startIndex) - 1);
            lst.Add(item);
            str = str.Remove(startIndex);
            if (str.Contains(leftChar))
            {
                ParseStringRecursive(str, leftChar, rightChar, lst);
            }
        }

        public static string ReplaceTxt(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(" ", "&nbsp;&nbsp;");
                str = str.Replace("\r\n", "<br/>");
            }
            return str;
        }

        public static string StripTagsCharArray(string source)
        {
            char[] chArray = new char[source.Length];
            int index = 0;
            bool flag = false;
            for (int i = 0; i < source.Length; i++)
            {
                char ch = source[i];
                switch (ch)
                {
                    case '<':
                        flag = true;
                        break;

                    case '>':
                        flag = false;
                        break;

                    default:
                        if (!flag)
                        {
                            chArray[index] = ch;
                            index++;
                        }
                        break;
                }
            }
            return new string(chArray, 0, index);
        }
    }
}

