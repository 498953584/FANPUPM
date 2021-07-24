namespace cn.justwin.BLL
{
    using cn.justwin.Web;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class ResourceImage
    {
        public static string GetImages(string resourceId)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                string str = ConfigHelper.Get("ResourceImg") + "/" + resourceId;
                FileInfo[] files = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + str).GetFiles();
                string[] source = new string[] { ".jpg", ".png", ".gif", ".JPG" };
                if (files.Length > 0)
                {
                    builder.Append("<div class='gallery'>").AppendLine();
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (source.Contains<string>(files[i].Extension))
                        {
                            if (i == 0)
                            {
                                builder.AppendFormat("<a href='{0}'>", str + "/" + files[i].Name).AppendLine();
                                builder.Append("<img src='/images1/icon_att0b3dfa.gif' style='cursor:pointer;border-style:none' />").AppendLine();
                                builder.Append("</a>").AppendLine();
                            }
                            else
                            {
                                builder.AppendFormat("<a href='{0}'>", str + "/" + files[i].Name).AppendLine();
                                builder.Append("</a>").AppendLine();
                            }
                        }
                    }
                    builder.Append("</div>").AppendLine();
                }
            }
            catch
            {
                builder = new StringBuilder(string.Empty);
            }
            return builder.ToString();
        }
    }
}

