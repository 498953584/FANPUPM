namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="PT_PrjMember")]
    public class PT_PrjMember : EntityObject
    {
        private string _EducationalBackground;
        private DateTime? _InputDate;
        private string _Note;
        private string _PastPerformance;
        private string _Post;
        private string _PostAndCompetency;
        private Guid? _PrjGuid;
        private string _PrjMemberId;
        private string _Technical;
        private string _TrainingInformation;

        public static PT_PrjMember CreatePT_PrjMember(string prjMemberId)
        {
            return new PT_PrjMember { PrjMemberId = prjMemberId };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string EducationalBackground
        {
            get
            {
                return this._EducationalBackground;
            }
            set
            {
                this.ReportPropertyChanging("EducationalBackground");
                this._EducationalBackground = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("EducationalBackground");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? InputDate
        {
            get
            {
                return this._InputDate;
            }
            set
            {
                this.ReportPropertyChanging("InputDate");
                this._InputDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("InputDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Note
        {
            get
            {
                return this._Note;
            }
            set
            {
                this.ReportPropertyChanging("Note");
                this._Note = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Note");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PastPerformance
        {
            get
            {
                return this._PastPerformance;
            }
            set
            {
                this.ReportPropertyChanging("PastPerformance");
                this._PastPerformance = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PastPerformance");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Post
        {
            get
            {
                return this._Post;
            }
            set
            {
                this.ReportPropertyChanging("Post");
                this._Post = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Post");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PostAndCompetency
        {
            get
            {
                return this._PostAndCompetency;
            }
            set
            {
                this.ReportPropertyChanging("PostAndCompetency");
                this._PostAndCompetency = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PostAndCompetency");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public Guid? PrjGuid
        {
            get
            {
                return this._PrjGuid;
            }
            set
            {
                this.ReportPropertyChanging("PrjGuid");
                this._PrjGuid = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PrjGuid");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string PrjMemberId
        {
            get
            {
                return this._PrjMemberId;
            }
            set
            {
                if (this._PrjMemberId != value)
                {
                    this.ReportPropertyChanging("PrjMemberId");
                    this._PrjMemberId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("PrjMemberId");
                }
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_PrjMem__Membe__40071901", "PT_yhmc"), SoapIgnore, DataMember, XmlIgnore]
        public cn.justwin.DAL.PT_yhmc PT_yhmc
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_PrjMem__Membe__40071901", "PT_yhmc").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_PrjMem__Membe__40071901", "PT_yhmc").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.PT_yhmc> PT_yhmcReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_PrjMem__Membe__40071901", "PT_yhmc");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_PrjMem__Membe__40071901", "PT_yhmc", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Technical
        {
            get
            {
                return this._Technical;
            }
            set
            {
                this.ReportPropertyChanging("Technical");
                this._Technical = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Technical");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string TrainingInformation
        {
            get
            {
                return this._TrainingInformation;
            }
            set
            {
                this.ReportPropertyChanging("TrainingInformation");
                this._TrainingInformation = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TrainingInformation");
            }
        }
    }
}

