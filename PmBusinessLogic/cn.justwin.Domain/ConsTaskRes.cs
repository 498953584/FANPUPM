namespace cn.justwin.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ConsTaskRes
    {
        public string Code { get; set; }

        public IList<ConsResInfo> resources { get; set; }
    }
}

