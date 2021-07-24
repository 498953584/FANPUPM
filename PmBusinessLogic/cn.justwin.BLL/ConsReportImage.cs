namespace cn.justwin.BLL
{
    using cn.justwin.Domain;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class ConsReportImage
    {
        public static string GetImages(string consReportId)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append("<div class='gallery'>").AppendLine();
                IList<ConstructTask> yByReportId = ConstructTask.GetyByReportId(consReportId);
                for (int i = 0; i < yByReportId.Count; i++)
                {
                    string str = ConfigHelper.Get("BudgetPriceType") + "/" + yByReportId[i].Id;
                    DirectoryInfo info = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + str);
                    if (info.Exists)
                    {
                        FileInfo[] files = info.GetFiles();
                        string[] source = new string[] { ".jpg", ".png", ".gif" };
                        if (files.Length > 0)
                        {
                            for (int j = 0; j < files.Length; j++)
                            {
                                if (source.Contains<string>(files[j].Extension))
                                {
                                    if (j == 0)
                                    {
                                        if (i == 0)
                                        {
                                            builder.AppendFormat("<a href='{0}'>", str + "/" + files[j].Name).AppendLine();
                                            builder.Append("<img src='/images1/icon_att0b3dfa.gif' style='cursor:pointer;border-style:none' />").AppendLine();
                                            builder.Append("</a>").AppendLine();
                                        }
                                        else
                                        {
                                            builder.AppendFormat("<a href='{0}'>", str + "/" + files[j].Name).AppendLine();
                                            builder.Append("</a>").AppendLine();
                                        }
                                    }
                                    else
                                    {
                                        builder.AppendFormat("<a href='{0}'>", str + "/" + files[j].Name).AppendLine();
                                        builder.Append("</a>").AppendLine();
                                    }
                                }
                            }
                        }
                    }
                }
                builder.Append("</div>").AppendLine();
            }
            catch
            {
                builder = new StringBuilder(string.Empty);
            }
            return builder.ToString();
        }
    }
}

