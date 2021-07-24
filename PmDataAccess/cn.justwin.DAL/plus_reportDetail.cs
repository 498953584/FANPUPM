namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="plus_reportDetail")]
    public class plus_reportDetail : EntityObject
    {
        private byte? _Completed;
        private DateTime? _Finish;
        private string _Id;
        private string _Note;
        private string _ReportId;
        private DateTime? _Start;
        private string _TaskUID;

        public static plus_reportDetail Createplus_reportDetail(string id)
        {
            return new plus_reportDetail { Id = id };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public byte? Completed
        {
            get
            {
                return this._Completed;
            }
            set
            {
                this.ReportPropertyChanging("Completed");
                this._Completed = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Completed");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? Finish
        {
            get
            {
                return this._Finish;
            }
            set
            {
                this.ReportPropertyChanging("Finish");
                this._Finish = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Finish");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if (this._Id != value)
                {
                    this.ReportPropertyChanging("Id");
                    this._Id = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("Id");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [EdmRelationshipNavigationProperty("Pm2Model", "FK_plus_report", "plus_report"), SoapIgnore, DataMember, XmlIgnore]
        public cn.justwin.DAL.plus_report plus_report
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_report>("Pm2Model.FK_plus_report", "plus_report").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_report>("Pm2Model.FK_plus_report", "plus_report").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.plus_report> plus_reportReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_report>("Pm2Model.FK_plus_report", "plus_report");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.plus_report>("Pm2Model.FK_plus_report", "plus_report", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ReportId
        {
            get
            {
                return this._ReportId;
            }
            set
            {
                this.ReportPropertyChanging("ReportId");
                this._ReportId = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ReportId");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? Start
        {
            get
            {
                return this._Start;
            }
            set
            {
                this.ReportPropertyChanging("Start");
                this._Start = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Start");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string TaskUID
        {
            get
            {
                return this._TaskUID;
            }
            set
            {
                this.ReportPropertyChanging("TaskUID");
                this._TaskUID = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TaskUID");
            }
        }
    }
}

