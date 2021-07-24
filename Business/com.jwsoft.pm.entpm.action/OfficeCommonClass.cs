namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Text;

    public class OfficeCommonClass
    {
        public static string GetNewLowerLevelCode(string strTab, string strKey, string strCode, string strWhere, int intReturnLen)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select max(" + strKey + ") from " + strTab + " ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                str = Convert.ToString((int) (int.Parse((string) obj2) + 1));
                while (str.Length < ((string) obj2).Length)
                {
                    str = "0" + str;
                }
            }
            else
            {
                int num = 0;
                int num2 = intReturnLen - strCode.Length;
                while (num < num2)
                {
                    if (num == (num2 - 1))
                    {
                        str = str + "1";
                    }
                    else
                    {
                        str = "0" + str;
                    }
                    num++;
                }
                str = strCode + str;
            }
            return str.Trim();
        }

        public static string GetNewSameLevelCode(string strTab, string strKey, string strWhere, int intReturnLen)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(" select max(" + strKey + ") from " + strTab + " ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                str = Convert.ToString((int) (int.Parse((string) obj2) + 1));
                while (str.Length < ((string) obj2).Length)
                {
                    str = "0" + str;
                }
            }
            else
            {
                for (int i = 0; i < intReturnLen; i++)
                {
                    if (i == (intReturnLen - 1))
                    {
                        str = str + "1";
                    }
                    else
                    {
                        str = "0" + str;
                    }
                }
            }
            return str.Trim();
        }
    }
}

