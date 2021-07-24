using System;
using System.Runtime.CompilerServices;

public class ConverRMB
{
    private static char[] cDelim = new char[] { '.' };
    [DecimalConstant(2, 0, (uint) 0x33b2e3c, (uint) 0x9fd0803c, (uint) 0xe7ffffff)]
    private static readonly decimal MaxNumber = 9999999999999999999999999.99M;
    [DecimalConstant(2, 0x80, (uint) 0x33b2e3c, (uint) 0x9fd0803c, (uint) 0xe7ffffff)]
    private static readonly decimal MinNumber = -9999999999999999999999999.99M;
    private static string RMBUnitChar = "元拾佰仟万拾佰仟亿拾佰仟兆拾佰仟万拾佰仟亿拾佰仟兆";
    private static string RMBUppercase = "零壹贰叁肆伍陆柒捌玖";

    private static void CheckNumberLimit(decimal number)
    {
        if ((number < -9999999999999999999999999.99M) || (number > 9999999999999999999999999.99M))
        {
            throw new RMBException("超出可转换的范围");
        }
    }

    private static string CombinUnit(string rmb)
    {
        if (rmb.Contains("兆亿万"))
        {
            return rmb.Replace("兆亿万", "兆");
        }
        if (rmb.Contains("亿万"))
        {
            return rmb.Replace("亿万", "亿");
        }
        if (rmb.Contains("兆亿"))
        {
            return rmb.Replace("兆亿", "兆");
        }
        return rmb;
    }

    public static string Convert(decimal number)
    {
        bool flag = false;
        CheckNumberLimit(number);
        decimal num = Math.Round(number, 2);
        if (num == 0M)
        {
            return "零元整";
        }
        if (num < 0M)
        {
            flag = true;
            num = Math.Abs(num);
        }
        else
        {
            flag = false;
        }
        string str2 = "";
        string str3 = "";
        string[] strArray = null;
        strArray = num.ToString().Split(cDelim, 2);
        if (num >= 1M)
        {
            str3 = ConvertInt(strArray[0]);
        }
        if (strArray.Length > 1)
        {
            str2 = ConvertDecimal(strArray[1]);
        }
        else
        {
            str2 = "整";
        }
        if (!flag)
        {
            return (str3 + str2);
        }
        return ("负" + str3 + str2);
    }

    private static string ConvertDecimal(string decPart)
    {
        string str = "";
        int length = decPart.Length;
        if ((decPart == "0") || (decPart == "00"))
        {
            return "整";
        }
        if (decPart.Length > 1)
        {
            if (decPart[0] == '0')
            {
                return (DigToCC(decPart[1]) + "分");
            }
            if (decPart[1] == '0')
            {
                return (DigToCC(decPart[0]) + "角整");
            }
            return ((DigToCC(decPart[0]) + "角") + DigToCC(decPart[1]) + "分");
        }
        return (str + DigToCC(decPart[0]) + "角整");
    }

    private static string ConvertInt(string intPart)
    {
        string str = "";
        int length = intPart.Length;
        int num2 = length;
        string str2 = "";
        string unit = "";
        int num3 = 0;
        while (num3 < (length - 1))
        {
            if (intPart[num3] != '0')
            {
                str2 = DigToCC(intPart[num3]);
                unit = GetUnit(num2 - 1);
            }
            else if (((num2 - 1) % 4) == 0)
            {
                str2 = "";
                unit = GetUnit(num2 - 1);
            }
            else
            {
                unit = "";
                if (intPart[num3 + 1] != '0')
                {
                    str2 = "零";
                }
                else
                {
                    str2 = "";
                }
            }
            str = str + str2 + unit;
            num3++;
            num2--;
        }
        if (intPart[num3] != '0')
        {
            str = str + DigToCC(intPart[num3]);
        }
        return CombinUnit(str + "元");
    }

    public static string ConvertU(decimal number)
    {
        string str = string.Empty;
        if (number < 10M)
        {
            return funConvertNumber(number.ToString());
        }
        if (number >= 10M)
        {
            return ("十" + funConvertNumber(number.ToString().Substring(1, 1)));
        }
        if ((number >= 20M) && (number < 100M))
        {
            return (funConvertNumber(number.ToString().Substring(0, 1)) + "十" + funConvertNumber(number.ToString().Substring(1, 1)));
        }
        if ((number >= 100M) && (number < 1000M))
        {
            str = funConvertNumber(number.ToString().Substring(0, 1)) + "百" + funConvertNumber(number.ToString().Substring(1, 1)) + "十" + funConvertNumber(number.ToString().Substring(2, 1));
        }
        return str;
    }

    private static string DigToCC(char c)
    {
        char ch = RMBUppercase[c - '0'];
        return ch.ToString();
    }

    public static string funConvertNumber(string number)
    {
        string str = string.Empty;
        switch (number.ToString())
        {
            case "1":
                return "一";

            case "2":
                return "二";

            case "3":
                return "三";

            case "4":
                return "四";

            case "5":
                return "五";

            case "6":
                return "六";

            case "7":
                return "七";

            case "8":
                return "八";

            case "9":
                return "九";
        }
        return str;
    }

    private static string GetUnit(int n)
    {
        char ch = RMBUnitChar[n];
        return ch.ToString();
    }

    public static decimal MaxSupportNumber
    {
        get
        {
            return 9999999999999999999999999.99M;
        }
    }

    public static decimal MinSupportNumber
    {
        get
        {
            return -9999999999999999999999999.99M;
        }
    }

    public class RMBException : Exception
    {
        public RMBException(string msg) : base(msg)
        {
        }
    }
}

