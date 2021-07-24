namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class PTyhmc
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
            return (this.v_yhdm == ((PTyhmc) obj).v_yhdm);
        }

        public override int GetHashCode()
        {
            return this.v_yhdm.GetHashCode();
        }

        public override string ToString()
        {
            return this.v_yhdm.ToString();
        }

        public virtual string Address { get; set; }

        public virtual int? Age { get; set; }

        public virtual DateTime? BeginWorkDate { get; set; }

        public virtual DateTime? Birthday { get; set; }

        public virtual PTdbm Bm { get; set; }

        public virtual string c_sfyx { get; set; }

        public virtual int? ClassID { get; set; }

        public virtual string CommunicateAddress { get; set; }

        public virtual string ComputeLevel { get; set; }

        public virtual DateTime? conEndDate { get; set; }

        public virtual string DriveLevel { get; set; }

        public virtual string EducationalBackground { get; set; }

        public virtual string EndowmentInsurance { get; set; }

        public virtual string EnglishLevel { get; set; }

        public virtual DateTime? EnterCorpDate { get; set; }

        public virtual decimal? ExpectationSalary { get; set; }

        public virtual DateTime? FormalDate { get; set; }

        public virtual string GraduateSchool { get; set; }

        public virtual string HousingAccumulationFund { get; set; }

        [DataMember]
        public virtual int i_bmdm { get; set; }

        public virtual int? I_DUTYID { get; set; }

        public virtual int? i_xh { get; set; }

        public virtual string IDCard { get; set; }

        public virtual string InjuryInsurance { get; set; }

        public virtual string Introducer { get; set; }

        public virtual bool IsChargeMan { get; set; }

        public virtual string JoinCorpMode { get; set; }

        public virtual DateTime? JoinPartyDate { get; set; }

        public virtual DateTime? LeaveDate { get; set; }

        public virtual DateTime? leavetime { get; set; }

        public virtual string Level { get; set; }

        public virtual string Marriage { get; set; }

        public virtual string MedicareInsurance { get; set; }

        public virtual string MobilePhoneCode { get; set; }

        public virtual string Nation { get; set; }

        public virtual string OtherDepts { get; set; }

        public virtual string OtherDutyIDs { get; set; }

        public virtual string PastPerformance { get; set; }

        public virtual string PersonSuddennessInsurance { get; set; }

        public virtual string PoliticsFace { get; set; }

        public virtual int? PositionLevel { get; set; }

        public virtual string PostAndCompetency { get; set; }

        public virtual string PostAndRank { get; set; }

        public virtual string Postcode { get; set; }

        public virtual string rdeNature { get; set; }

        public virtual string RegisteredPlace { get; set; }

        public virtual string RelationCorp { get; set; }

        public virtual string RTXID { get; set; }

        public virtual string Sex { get; set; }

        public virtual string Specialty { get; set; }

        public virtual int? State { get; set; }

        public virtual string Stature { get; set; }

        public virtual int? SuperordinateDuty { get; set; }

        public virtual string Tel { get; set; }

        public virtual string ucmConcern { get; set; }

        public virtual string ucmTel { get; set; }

        public virtual string UnemploymentInsurance { get; set; }

        public virtual string urgentCellMan { get; set; }

        public virtual string userCode { get; set; }

        public virtual string UserPhoto { get; set; }

        [DataMember]
        public virtual string v_xm { get; set; }

        [DataMember]
        public virtual string v_yhdm { get; set; }
    }
}

