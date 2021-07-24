namespace cn.justwin.Domain
{
    using System;
    using System.Collections.Generic;

    public class BudTaskTypeGBCode : IBudTaskTypeSeries
    {
        public List<int> GetLayerByCode(List<string> codeList)
        {
            List<int> list = new List<int>();
            int item = 0;
            foreach (string str in codeList)
            {
                if (str.Contains("、"))
                {
                    item = 1;
                    list.Add(item);
                }
                if (str.Length >= 12)
                {
                    item = 3;
                    list.Add(item);
                }
                if (str.Length == 1)
                {
                    item = 2;
                    list.Add(item);
                }
            }
            return list;
        }
    }
}

