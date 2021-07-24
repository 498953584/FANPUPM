namespace cn.justwin.Domain
{
    using System.Collections.Generic;

    public interface IBudTaskTypeSeries
    {
        List<int> GetLayerByCode(List<string> codeList);
    }
}

