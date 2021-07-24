namespace cn.justwin.opm.Public
{
    using System;

    public class GridViewStringSub
    {
        public static string LinkSub(string substring, int length, string onclick, string href)
        {
            return Sub(substring, length, "a", " onclick=\"" + onclick + "\"  href=\"" + href + "\" ", null, null);
        }

        public static string Strsub(string substring, int length, string classstr, string onclick)
        {
            return Sub(substring, (length == 0) ? 10 : length, "span", " onclick=\"" + onclick + "\" ", null, classstr);
        }

        public static string StrSub(string substint, int length)
        {
            return Sub(substint, length, "span", null, null, null);
        }

        public static string Sub(string substring, int length, string tagname, string any, string style, string classstr)
        {
            if (string.IsNullOrEmpty(substring))
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(tagname))
            {
                tagname = "span";
            }
            return ((((("<" + tagname + " style='border-style:none;" + style + "' ") + " title='" + substring + "' ") + (string.IsNullOrEmpty(any) ? string.Empty : any) + (string.IsNullOrEmpty(classstr) ? string.Empty : (" class='" + classstr + "' "))) + ">" + ((substring.Length > length) ? (substring.Substring(0, length) + "...") : substring)) + "</" + tagname + ">");
        }
    }
}

