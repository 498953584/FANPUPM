namespace com.jwsoft.pm.util
{
    using System;
    using System.Text.RegularExpressions;

    public class VerifyHelper
    {
        public static bool IsAnsiSqlDate(string input)
        {
            string pattern = "^((\\d{2}(([02468][048])|([13579][26]))[\\-\\/\\s]?((((0?[13578]\r\n\t\t\t\t\t\t\t\t\t)|(1[02]))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[4\r\n\t\t\t\t\t\t\t\t\t69])|(11))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\\-\\/\\\r\n\t\t\t\t\t\t\t\t\ts]?((0?[1-9])|([1-2][0-9])))))|(\\d{2}(([02468][1235679])|([1\r\n\t\t\t\t\t\t\t\t\t3579][01345789]))[\\-\\/\\s]?((((0?[13578])|(1[02]))[\\-\\/\\s]?((\r\n\t\t\t\t\t\t\t\t\t0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\\-\\/\\s]?((\r\n\t\t\t\t\t\t\t\t\t0?[1-9])|([1-2][0-9])|(30)))|(0?2[\\-\\/\\s]?((0?[1-9])|(1[0-9]\r\n\t\t\t\t\t\t\t\t\t)|(2[0-8]))))))(\\s(((0?[1-9])|(1[0-2]))\\:([0-5][0-9])((\\s)|(\r\n\t\t\t\t\t\t\t\t\t\\:([0-5][0-9])\\s))([AM|PM|am|pm]{2,2})))?$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsDate(string input)
        {
            string pattern = @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsDouble(string input)
        {
            string pattern = @"^(-?\\d+)(\\.\\d+)?$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsEmail(string input)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsInt32(string input)
        {
            string pattern = @"^-?\d+$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsTxtFileName(string input)
        {
            string pattern = @"^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*))+\.(txt|TXT)$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool ValidatorMoney(string num)
        {
            if (num.Length == 0)
            {
                return false;
            }
            if ((num[0] < '0') || (num[0] > '9'))
            {
                return false;
            }
            for (int i = 0; i < num.Length; i++)
            {
                if (((i < (num.Length - 1)) && (num[i] == num[i + 1])) && (num[i] == '.'))
                {
                    return false;
                }
                if (((num[i] < '0') || (num[i] > '9')) && (num[i] != '.'))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

