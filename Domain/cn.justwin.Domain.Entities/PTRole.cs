namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTRole
    {
        public virtual string IsValid { get; set; }

        public virtual int RoleCode { get; set; }

        public virtual string RoleName { get; set; }

        public virtual string RoleTypeCode { get; set; }
    }
}

