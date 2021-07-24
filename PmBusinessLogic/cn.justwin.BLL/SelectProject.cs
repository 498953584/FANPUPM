namespace cn.justwin.BLL
{
    using System;
    using System.Runtime.CompilerServices;

    public class SelectProject
    {
        public string Code { get; set; }

        public string Id { get; set; }

        public bool IsParent { get; set; }

        public string Name { get; set; }

        public string Order { get; set; }

        public string OwnerCode { get; set; }

        public string Place { get; set; }

        public int State { get; set; }

        public string TypeCode { get; set; }
    }
}

