namespace cn.justwin.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BudTaskTypeServices
    {
        private IBudTaskTypeSeries budTaskTypeSeries;

        public BudTaskTypeServices(IList<string> codeList)
        {
            if (codeList.Any<string>(code => string.IsNullOrEmpty(code.Trim())))
            {
                this.budTaskTypeSeries = null;
            }
            else if (codeList.All<string>(code => (code.Length % 2) == 0))
            {
                this.budTaskTypeSeries = new BudTaskTypeTwoChar();
            }
            else if (codeList.All<string>(code => (code.Length % 3) == 0))
            {
                this.budTaskTypeSeries = new BudTaskTypeThreeChar();
            }
            else if (codeList.Any<string>(code => code.Contains("-")) && !codeList.Any<string>(code => code.Contains(".")))
            {
                this.budTaskTypeSeries = new BudTaskTypeHorizSep();
            }
            else if (codeList.Any<string>(code => code.Contains(".")) && !codeList.Any<string>(code => code.Contains("-")))
            {
                this.budTaskTypeSeries = new BudTaskTypeDotSep();
            }
            else if (codeList.Any<string>(code => code.Length >= 12))
            {
                this.budTaskTypeSeries = new BudTaskTypeGBCode();
            }
            else
            {
                this.budTaskTypeSeries = null;
            }
        }

        public List<int> GetLayerByCode(List<string> codeList)
        {
            return this.budTaskTypeSeries.GetLayerByCode(codeList);
        }

        public static int GetSeparatorCount(string code, char ch)
        {
            int num = 1;
            string[] source = code.Split(new char[] { ch });
            num = source.Count<string>();
            if (!source.Any<string>(c => (c.Length == 0)))
            {
                return num;
            }
            return (num - source.Count<string>(c => (c.Length == 0)));
        }

        public static List<int> GetStandardLayer(List<int> layerList)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < layerList.Count; i++)
            {
                int item = 1;
                if (i > 0)
                {
                    System.Func<int, bool> predicate = null;
                    int currentLayer = layerList[i];
                    int num3 = layerList[i - 1];
                    int num4 = list[i - 1];
                    if (currentLayer == num3)
                    {
                        item = num4;
                    }
                    else if (currentLayer > num3)
                    {
                        item = num4 + 1;
                    }
                    else if (currentLayer == 1)
                    {
                        item = 1;
                    }
                    else
                    {
                        if (predicate == null)
                        {
                            predicate = c => c == currentLayer;
                        }
                        if (layerList.GetRange(0, i - 1).Any<int>(predicate))
                        {
                            int num5 = layerList.GetRange(0, i - 1).LastIndexOf(currentLayer);
                            item = list[num5];
                        }
                        else
                        {
                            item = num4;
                        }
                    }
                }
                list.Add(item);
            }
            return list;
        }
    }
}

