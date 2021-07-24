namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTDBSJToday
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (base.GetType() != obj.GetType())
            {
                return false;
            }
            return (this.I_DBSJ_ID == ((PTDBSJToday) obj).I_DBSJ_ID);
        }

        public override int GetHashCode()
        {
            return this.I_DBSJ_ID.GetHashCode();
        }

        public override string ToString()
        {
            return this.I_DBSJ_ID.ToString();
        }

        public virtual string C_OpenFlag { get; set; }

        public virtual DateTime? DTM_DBSJ { get; set; }

        public virtual int I_DBSJ_ID { get; set; }

        public virtual string I_XGID { get; set; }

        public virtual bool IsOpened { get; set; }

        public virtual string V_Content { get; set; }

        public virtual string V_DBLJ { get; set; }

        public virtual string V_LXBM { get; set; }

        public virtual string V_TPLJ { get; set; }

        public virtual string V_YHDM { get; set; }
    }
}

