using System;
using System.Text.RegularExpressions;

public static class StRex
{
    public static bool ValidateString(string _value, int _kind)
    {
        _value = _value.Trim();
        string pattern = null;
        switch (_kind)
        {
            case 1:
                pattern = "^[A-Za-z]+$";
                break;

            case 2:
                pattern = "^[A-Za-z0-9]+$";
                break;

            case 3:
                pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                break;

            case 4:
                pattern = @"^[a-zA-z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$";
                break;

            case 5:
                pattern = "^[^一-龥]";
                break;

            case 6:
                pattern = "^[0-9]*$";
                break;
        }
        return Regex.Match(_value, pattern).Success;
    }
}

