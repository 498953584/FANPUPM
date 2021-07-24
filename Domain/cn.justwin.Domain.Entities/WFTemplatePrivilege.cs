namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class WFTemplatePrivilege
    {
        public virtual int PrivilegeId { get; set; }

        public virtual int TemplateId { get; set; }

        public virtual string UserCode { get; set; }
    }
}

