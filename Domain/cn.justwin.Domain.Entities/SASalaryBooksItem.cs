namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class SASalaryBooksItem
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
            return (this.Id == ((SASalaryBooksItem) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual string BooksId { get; set; }

        public virtual decimal? DefaultValue { get; set; }

        public virtual string Formula { get; set; }

        public virtual string Id { get; set; }

        public virtual bool IsFormula { get; set; }

        public virtual bool IsShow { get; set; }

        public virtual string ItemId { get; set; }

        public virtual int No { get; set; }
    }
}

