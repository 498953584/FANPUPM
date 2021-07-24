namespace cn.justwin.Domain
{
    using System;
    using System.Collections.Generic;

    public class BudTaskTypeDotSep : IBudTaskTypeSeries
    {
        public List<int> GetLayerByCode(List<string> codeList)
        {
            List<int> layerList = new List<int>();
            foreach (string str in codeList)
            {
                int item = 1;
                if (str.Contains("."))
                {
                    item = BudTaskTypeServices.GetSeparatorCount(str, '.');
                }
                layerList.Add(item);
            }
            return BudTaskTypeServices.GetStandardLayer(layerList);
        }
    }
}

