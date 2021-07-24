namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class XPMBasicContactCorp
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
            return (this.CorpID == ((XPMBasicContactCorp) obj).CorpID);
        }

        public override int GetHashCode()
        {
            return this.CorpID.GetHashCode();
        }

        public override string ToString()
        {
            return this.CorpID.ToString();
        }

        public virtual string AccountBank { get; set; }

        public virtual string Address { get; set; }

        public virtual string Aptitude { get; set; }

        public virtual int? AuditState { get; set; }

        public virtual string BankAccounts { get; set; }

        public virtual string Brand { get; set; }

        public virtual string Capital { get; set; }

        public virtual string Client { get; set; }

        public virtual string Contry { get; set; }

        public virtual string CorpBrief { get; set; }

        public virtual int CorpID { get; set; }

        public virtual int? CorpKind { get; set; }

        public virtual string CorpName { get; set; }

        public virtual string Corporation { get; set; }

        public virtual int CorpTypeID { get; set; }

        public virtual string Email { get; set; }

        public virtual string Fax { get; set; }

        public virtual Guid? FlowGuid { get; set; }

        public virtual string HandPhone { get; set; }

        public virtual bool? IFProvider { get; set; }

        public virtual bool? IsDefault { get; set; }

        public virtual bool? IsFixed { get; set; }

        public virtual string IsHot { get; set; }

        public virtual bool? IsValid { get; set; }

        public virtual bool? IsVisible { get; set; }

        public virtual string LinkMan { get; set; }

        public virtual string Owner { get; set; }

        public virtual string PeopleNumber { get; set; }

        public virtual string PostCode { get; set; }

        public virtual string ShopCard { get; set; }

        public virtual string Speciality { get; set; }

        public virtual string TaxCard { get; set; }

        public virtual string Telephone { get; set; }

        public virtual string UnderlayAbility { get; set; }

        public virtual string UserCode { get; set; }

        public virtual DateTime? VersionTime { get; set; }

        public virtual string WebSite { get; set; }

        public virtual int? WorkType { get; set; }

        public virtual string Zone { get; set; }
    }
}

