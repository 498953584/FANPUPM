namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Text;

    public class FileCommonClass
    {
        public static string GetNewFileCode(string strLibraryCode)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select max(ClassCode) from OA_File_Classes where LibraryCode='" + strLibraryCode + "' ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                str = Convert.ToString((int) (int.Parse((string) obj2) + 1));
                while (str.Length < ((string) obj2).Length)
                {
                    str = "0" + str;
                }
                return str;
            }
            return "001";
        }

        public static string GetNewLibraryCode()
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select max(LibraryCode) from OA_File_Library ");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                str = Convert.ToString((int) (int.Parse((string) obj2) + 1));
                while (str.Length < ((string) obj2).Length)
                {
                    str = "0" + str;
                }
                return str;
            }
            return "001";
        }
    }
}

