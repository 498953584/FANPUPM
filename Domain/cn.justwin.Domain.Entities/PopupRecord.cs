namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PopupRecord
    {
        public virtual int Id { get; set; }

        public virtual string Module { get; set; }

        public virtual string PopupId { get; set; }

        public virtual DateTime? PopupTime { get; set; }

        public virtual string UserCode { get; set; }
    }
}

