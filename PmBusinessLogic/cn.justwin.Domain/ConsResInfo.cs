namespace cn.justwin.Domain
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ConsResInfo
    {
        public string cbsCode { get; set; }

        public string consTaskResId { get; set; }

        public decimal num { get; set; }

        public string resCode { get; set; }
    }
}

