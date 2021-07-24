using System;
using System.Text;

public class ConvertChineseNum
{
    private readonly char[] chnGenDigit = new char[] { '十', '百', '千', '万', '亿' };
    private readonly char[] chnGenText = new char[] { '零', '一', '二', '三', '四', '五', '六', '七', '八', '九' };

    private bool CheckDigit(ref string strDigit)
    {
        bool flag = false;
        decimal num = 0M;
        try
        {
            num = decimal.Parse(strDigit);
            flag = true;
        }
        catch (FormatException)
        {
            flag = false;
        }
        if ((num <= -10000000000000000M) || (num >= 10000000000000000M))
        {
            return false;
        }
        return true;
    }

    public string Convert(string strDigit)
    {
        if (this.CheckDigit(ref strDigit))
        {
            StringBuilder strResult = new StringBuilder();
            this.ExtractSign(ref strResult, ref strDigit);
            this.ConvertNumber(ref strResult, ref strDigit);
            return strResult.ToString();
        }
        return "数据无效！";
    }

    protected string ConvertFractional(string strFractional)
    {
        char[] chArray = strFractional.ToCharArray();
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < chArray.Length; i++)
        {
            builder.Append(this.chnGenText[chArray[i] - '0']);
        }
        return builder.ToString();
    }

    protected string ConvertIntegral(string strIntegral)
    {
        string str;
        char[] chArray = long.Parse(strIntegral).ToString().ToCharArray();
        StringBuilder builder = new StringBuilder();
        int num = chArray.Length - 1;
        char[] chnGenText = this.chnGenText;
        char[] chnGenDigit = this.chnGenDigit;
        int index = 0;
        while (index < (chArray.Length - 1))
        {
            builder.Append(chnGenText[chArray[index] - '0']);
            if ((num % 4) == 0)
            {
                if ((4 == num) || (12 == num))
                {
                    builder.Append(chnGenDigit[3]);
                }
                else if (8 == num)
                {
                    builder.Append(chnGenDigit[4]);
                }
            }
            else
            {
                builder.Append(chnGenDigit[(num % 4) - 1]);
            }
            num--;
            index++;
        }
        if (('0' != chArray[chArray.Length - 1]) || (1 == chArray.Length))
        {
            builder.Append(chnGenText[chArray[index] - '0']);
        }
        index = 0;
        while (index < builder.Length)
        {
            int num3 = index;
            bool flag = false;
            while ((num3 < (builder.Length - 1)) && ("零" == builder.ToString().Substring(num3, 1)))
            {
                str = builder.ToString().Substring(num3 + 1, 1);
                if ((chnGenDigit[3].ToString() == str) || (chnGenDigit[4].ToString() == str))
                {
                    flag = true;
                    break;
                }
                num3 += 2;
            }
            if (num3 != index)
            {
                builder = builder.Remove(index, num3 - index);
                if ((index <= (builder.Length - 1)) && !flag)
                {
                    builder = builder.Insert(index, 0x96f6);
                    index++;
                }
            }
            if (flag)
            {
                builder = builder.Remove(index, 1);
                index++;
            }
            else
            {
                index += 2;
            }
        }
        str = chnGenDigit[4].ToString() + chnGenDigit[3].ToString();
        int startIndex = builder.ToString().IndexOf(str);
        if (-1 != startIndex)
        {
            if ((((builder.Length - 2) != startIndex) && ((startIndex + 2) < builder.Length)) && ("零" != builder.ToString().Substring(startIndex + 2, 1)))
            {
                builder = builder.Replace(str, chnGenDigit[4].ToString(), startIndex, 2).Insert(startIndex + 1, "零");
            }
            else
            {
                builder = builder.Replace(str, chnGenDigit[4].ToString(), startIndex, 2);
            }
        }
        if ((builder.Length > 1) && ("一十" == builder.ToString().Substring(0, 2)))
        {
            builder = builder.Remove(0, 1);
        }
        return builder.ToString();
    }

    protected void ConvertNumber(ref StringBuilder strResult, ref string strDigit)
    {
        int num;
        if (-1 == (num = strDigit.IndexOf('.')))
        {
            strResult.Append(this.ConvertIntegral(strDigit));
        }
        else
        {
            if (num == 0)
            {
                strResult.Append(0x96f6);
            }
            else
            {
                strResult.Append(this.ConvertIntegral(strDigit.Substring(0, num)));
            }
            if ((strDigit.Length - 1) != num)
            {
                strResult.Append('点');
                this.ConvertFractional(strDigit.Substring(num + 1));
            }
        }
    }

    protected void ExtractSign(ref StringBuilder strResult, ref string strDigit)
    {
        if ("+" == strDigit.Substring(0, 1))
        {
            strDigit = strDigit.Substring(1);
        }
        else if ("-" == strDigit.Substring(0, 1))
        {
            strResult.Append(0x8d1f);
            strDigit = strDigit.Substring(1);
        }
        else if ("+" == strDigit.Substring(strDigit.Length - 1, 1))
        {
            strDigit = strDigit.Substring(0, strDigit.Length - 1);
        }
        else if ("-" == strDigit.Substring(strDigit.Length - 1, 1))
        {
            strResult.Append(0x8d1f);
            strDigit = strDigit.Substring(0, strDigit.Length - 1);
        }
    }
}

