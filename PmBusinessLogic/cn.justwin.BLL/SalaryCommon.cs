namespace cn.justwin.BLL
{
    using cn.justwin.salaryBLL;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class SalaryCommon
    {
        public static decimal CustomizePagTax(decimal shouldMoney)
        {
            TaxStartPointBLL tbll = new TaxStartPointBLL();
            decimal inComeTaxStartPoint = tbll.GetInComeTaxStartPoint();
            decimal num2 = 0M;
            if (shouldMoney <= inComeTaxStartPoint)
            {
                return num2;
            }
            shouldMoney -= inComeTaxStartPoint;
            DataTable table = new DataTable();
            table = tbll.GetList(1, " LowerLimit<" + shouldMoney.ToString(), " LowerLimit desc ");
            return ((shouldMoney * decimal.Parse(table.Rows[0]["TaxRate"].ToString())) - decimal.Parse(table.Rows[0]["Deduct"].ToString()));
        }

        public static double[] GetAdminSalary(double preTax, double ybasicWage)
        {
            double num = preTax * 0.6;
            double num2 = preTax * 0.13;
            double num3 = (preTax * 0.4) - num2;
            if ((num2 % 100.0) != 0.0)
            {
                num3 += num2 % 100.0;
                num2 = Math.Floor((double) (num2 / 100.0)) * 100.0;
            }
            if (num3 < ybasicWage)
            {
                if (((preTax * 0.4) - ybasicWage) >= 200.0)
                {
                    num3 = ybasicWage + (((preTax * 0.4) - ybasicWage) % 100.0);
                    num2 = Math.Floor((double) (((preTax * 0.4) - ybasicWage) / 100.0)) * 100.0;
                }
                if (((preTax * 0.4) - ybasicWage) < 200.0)
                {
                    num3 = preTax * 0.4;
                    num2 = 0.0;
                }
            }
            if ((preTax * 0.4) < ybasicWage)
            {
                num3 = ybasicWage;
                num2 = Math.Floor((double) ((preTax * 0.13) / 100.0)) * 100.0;
                num = (preTax - 1100.0) - num2;
            }
            if (preTax < ybasicWage)
            {
                num3 = preTax;
                num = 0.0;
                num2 = 0.0;
            }
            return new double[] { num3, num, num2 };
        }

        public static Dictionary<string, string> GetDictionaryFromTable(DataTable table, string key, string value)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    dictionary.Add(row[key].ToString(), row[value].ToString());
                }
                catch
                {
                }
            }
            return dictionary;
        }

        public static double GetDouble(double str)
        {
            string str2 = str.ToString();
            str2.IndexOf('.');
            if (str2.Length > (str2.IndexOf('.') + 3))
            {
                return Convert.ToDouble(str2.Substring(0, str2.IndexOf('.') + 3));
            }
            return str;
        }

        public static double GetDouble(double str, int num)
        {
            string str2 = str.ToString();
            int length = str2.IndexOf('.') + num;
            if ((str2.Length > (str2.IndexOf('.') + num)) && (length != -1))
            {
                return Convert.ToDouble(str2.Substring(0, length));
            }
            return str;
        }

        public static double[] GetWorkerSalry(int type, string currentMonth, double ybasicWage, double dayLaborersPrice, double LowestAayPrice, double hoursDay, double days, double actualDays, double livingSubsidy)
        {
            double num = LowestAayPrice * actualDays;
            if (num > ybasicWage)
            {
                num = ybasicWage;
            }
            double num2 = (dayLaborersPrice * actualDays) - num;
            if (type == 1)
            {
                num2 += days * (livingSubsidy / 30.0);
            }
            double[] numArray2 = new double[3];
            numArray2[0] = num;
            numArray2[1] = num2;
            return numArray2;
        }

        public static decimal PayTax(decimal shouldMoney)
        {
            decimal num = 0M;
            if (shouldMoney <= 500M)
            {
                num = shouldMoney * 0.05M;
            }
            if ((shouldMoney > 500M) && (shouldMoney <= 2000M))
            {
                num = (shouldMoney * 0.1M) - 25M;
            }
            if ((shouldMoney > 2000M) && (shouldMoney <= 5000M))
            {
                num = (shouldMoney * 0.15M) - 125M;
            }
            if ((shouldMoney > 5000M) && (shouldMoney <= 20000M))
            {
                num = (shouldMoney * 0.2M) - 375M;
            }
            if ((shouldMoney > 20000M) && (shouldMoney <= 40000M))
            {
                num = (shouldMoney * 0.25M) - 1375M;
            }
            if ((shouldMoney > 40000M) && (shouldMoney <= 60000M))
            {
                num = (shouldMoney * 0.3M) - 3375M;
            }
            if ((shouldMoney > 60000M) && (shouldMoney <= 80000M))
            {
                num = (shouldMoney * 0.35M) - 6375M;
            }
            if ((shouldMoney > 80000M) && (shouldMoney <= 100000M))
            {
                num = (shouldMoney * 0.4M) - 10375M;
            }
            if (shouldMoney > 100000M)
            {
                num = (shouldMoney * 0.45M) - 15375M;
            }
            return num;
        }

        public static string ReplaceFormula(string formula)
        {
            new SalaryTemplate();
            SetSalaryBll bll = new SetSalaryBll();
            new SamTemplateItemBll();
            List<string> list = StringUtility.ParseString(formula, "[", "]");
            List<string> list2 = StringUtility.ParseString(formula, "{", "}");
            SalaryParameter parameter = new SalaryParameter();
            foreach (string str in list2)
            {
                formula = formula.Replace(str, parameter.Paras[str]);
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary = GetDictionaryFromTable(bll.GetFieldAndRemark(), "name", "value");
            foreach (string str2 in list)
            {
                formula = formula.Replace(str2, dictionary[str2]);
            }
            return formula;
        }

        public static string ReplaceFormula(string formula, string templateType)
        {
            new SalaryTemplate();
            SetSalaryBll bll = new SetSalaryBll();
            SamTemplateItemBll bll2 = new SamTemplateItemBll();
            List<string> list = StringUtility.ParseString(formula, "[", "]");
            List<string> list2 = StringUtility.ParseString(formula, "{", "}");
            SalaryParameter parameter = new SalaryParameter();
            foreach (string str in list2)
            {
                formula = formula.Replace(str, parameter.Paras[str]);
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (string.Compare(templateType, "B", true) == 0)
            {
                dictionary = GetDictionaryFromTable(bll.GetFieldAndRemark(), "name", "value");
            }
            else if (string.Compare(templateType, "C", true) == 0)
            {
                dictionary = GetDictionaryFromTable(bll2.GetAllList(), "ItemCode", "ItemName");
            }
            foreach (string str2 in list)
            {
                formula = formula.Replace(str2, dictionary[str2]);
            }
            return formula;
        }
    }
}

