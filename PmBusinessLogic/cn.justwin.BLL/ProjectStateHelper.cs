namespace cn.justwin.BLL
{
    using System;

    public static class ProjectStateHelper
    {
        public static string GetName(object state)
        {
            string str = string.Empty;
            switch (state.ToString())
            {
                case "1":
                    return "信息预立项";

                case "2":
                    return "信息立项";

                case "3":
                    return "报名通过";

                case "4":
                    return "投标";

                case "5":
                    return "中标";

                case "6":
                    return "落标";

                case "7":
                    return "在建";

                case "8":
                    return "停工";

                case "9":
                    return "验收";

                case "10":
                    return "竣工";

                case "11":
                    return "保内";

                case "12":
                    return "保外";

                case "13":
                    return "解除";

                case "14":
                    return "资格预审";

                case "15":
                    return "预审通过";

                case "16":
                    return "预审失败";

                case "17":
                    return "待开工";

                case "18":
                    return "放弃";

                case "19":
                    return "报名不通过";
            }
            return str;
        }
    }
}

