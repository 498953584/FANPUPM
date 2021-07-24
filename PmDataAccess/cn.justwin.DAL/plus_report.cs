namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="plus_report"), DataContract(IsReference=true)]
    public class plus_report : EntityObject
    {
        private string _FlowState;
        private string _Id;
        private DateTime? _InputDate;
        private string _InputUser;
        private string _Note;
        private string _ProVersionId;

        public static plus_report Createplus_report(string id)
        {
            return new plus_report { Id = id };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string FlowState
        {
            get
            {
                return this._FlowState;
            }
            set
            {
                this.ReportPropertyChanging("FlowState");
                this._FlowState = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("FlowState");
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
        public string InputUser
        {
            get
            {
                return this._InputUser;
            }
            set
            {
                this.ReportPropertyChanging("InputUser");
                this._InputUser = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("InputUser");
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

        [SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK_plus_progress_version", "plus_progress_version"), DataMember]
        public cn.justwin.DAL.plus_progress_version plus_progress_version
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_progress_version>("Pm2Model.FK_plus_progress_version", "plus_progress_version").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_progress_version>("Pm2Model.FK_plus_progress_version", "plus_progress_version").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.plus_progress_version> plus_progress_versionReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_progress_version>("Pm2Model.FK_plus_progress_version", "plus_progress_version");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.plus_progress_version>("Pm2Model.FK_plus_progress_version", "plus_progress_version", value);
                }
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK_plus_report", "plus_reportDetail"), DataMember, XmlIgnore, SoapIgnore]
        public EntityCollection<cn.justwin.DAL.plus_reportDetail> plus_reportDetail
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.plus_reportDetail>("Pm2Model.FK_plus_report", "plus_reportDetail");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.plus_reportDetail>("Pm2Model.FK_plus_report", "plus_reportDetail", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ProVersionId
        {
            get
            {
                return this._ProVersionId;
            }
            set
            {
                this.ReportPropertyChanging("ProVersionId");
                this._ProVersionId = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProVersionId");
            }
        }
    }
}

