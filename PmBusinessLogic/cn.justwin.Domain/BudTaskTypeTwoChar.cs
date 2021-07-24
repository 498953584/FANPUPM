namespace cn.justwin.Domain
{
    using System;
    using System.Collections.Generic;

    public class BudTaskTypeTwoChar : IBudTaskTypeSeries
    {
        public List<int> GetLayerByCode(List<string> codeList)
        {
            List<int> layerList = new List<int>();
            foreach (string str in codeList)
            {
                int item = str.Length / 2;
                layerList.Add(item);
            }
            return BudTaskTypeServices.GetStandardLayer(layerList);
        }
    }
}

