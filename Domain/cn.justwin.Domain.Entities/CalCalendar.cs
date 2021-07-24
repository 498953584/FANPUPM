namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class CalCalendar
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
            return (this.Id == ((CalCalendar) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        [DataMember]
        public virtual string Color { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual DateTime? EndTime { get; set; }

        [DataMember(Name="id")]
        public virtual int Id { get; set; }

        [DataMember]
        public virtual int IsAllDayEvent { get; set; }

        [DataMember]
        public virtual string Location { get; set; }

        [DataMember]
        public virtual string RecurringRule { get; set; }

        [DataMember]
        public virtual DateTime? StartTime { get; set; }

        [DataMember]
        public virtual string Subject { get; set; }
    }
}

