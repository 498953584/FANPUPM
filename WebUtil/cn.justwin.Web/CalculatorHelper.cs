namespace cn.justwin.Web
{
    using Jint;
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 计算器
    /// </summary>
    public class CalculatorHelper
    {
        /// <summary>
        /// JintEngine
        /// </summary>
        private static JintEngine engine = new JintEngine();

        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <example>(100 + (200 - 50) * 3) = 550m</example>
        /// <returns>计算结果</returns>
        public static decimal Calc(string expression)
        {
            decimal num;
            try
            {
                num = Convert.ToDecimal(engine.Run(expression));
            }
            catch
            {
                throw new Exception("计算错误");
            }
            return num;
        }

        /// <summary>
        /// 检查有效性
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="items">指定的项</param>
        /// <example>([基本工资] + [岗位工作] - ([奖金] * [工日])) 为正确格式</example>
        /// <returns>是否有效</returns>
        public static bool IsValid(string expression, params string[] items)
        {
            try
            {
                if (!Regex.IsMatch(expression, @"^\[\w*|^\(\w*|^\d"))
                {
                    throw new Exception("没有以 [ 或数字开始.");
                }
                if (!Regex.IsMatch(expression, @"\w*\]$|\w*\)$|\d$"))
                {
                    throw new Exception("没有以[、( 或数字结束.");
                }
                if (Regex.IsMatch(expression, "[<>?'~`#%\\$\\^《》？‘～\x00b7￥]"))
                {
                    throw new Exception("不允许出现特殊字符.");
                }
                if (items.Length > 0)
                {
                    foreach (string str in items)
                    {
                        expression = expression.Replace("[" + str + "]", " 1");
                    }
                    if (expression.Contains("[") || expression.Contains("]"))
                    {
                        throw new Exception("包含不能识别的工资项.");
                    }
                }
                else
                {
                    expression = Regex.Replace(expression, @"\[\w*\]", " 1");
                }
                engine.Run(expression);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

