namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="plus_progress_version")]
    public class plus_progress_version : EntityObject
    {
        private int? _FlowState;
        private DateTime? _InputDate;
        private bool? _IsLatest;
        private string _Note;
        private string _ParentVersionId;
        private string _ProgressVersionId;
        private string _VersionCode;
        private string _VersionName;

        public static plus_progress_version Createplus_progress_version(string progressVersionId)
        {
            return new plus_progress_version { ProgressVersionId = progressVersionId };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? FlowState
        {
            get
            {
                return this._FlowState;
            }
            set
            {
                this.ReportPropertyChanging("FlowState");
                this._FlowState = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FlowState");
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public bool? IsLatest
        {
            get
            {
                return this._IsLatest;
            }
            set
            {
                this.ReportPropertyChanging("IsLatest");
                this._IsLatest = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsLatest");
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ParentVersionId
        {
            get
            {
                return this._ParentVersionId;
            }
            set
            {
                this.ReportPropertyChanging("ParentVersionId");
                this._ParentVersionId = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ParentVersionId");
            }
        }

        [DataMember, SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK_progress_version_ProgressId", "plus_progress")]
        public cn.justwin.DAL.plus_progress plus_progress
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_progress>("Pm2Model.FK_progress_version_ProgressId", "plus_progress").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_progress>("Pm2Model.FK_progress_version_ProgressId", "plus_progress").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.plus_progress> plus_progressReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_progress>("Pm2Model.FK_progress_version_ProgressId", "plus_progress");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.plus_progress>("Pm2Model.FK_progress_version_ProgressId", "plus_progress", value);
                }
            }
        }

        [DataMember, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK_plus_progress_version", "plus_report"), SoapIgnore]
        public EntityCollection<cn.justwin.DAL.plus_report> plus_report
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.plus_report>("Pm2Model.FK_plus_progress_version", "plus_report");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.plus_report>("Pm2Model.FK_plus_progress_version", "plus_report", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ProgressVersionId
        {
            get
            {
                return this._ProgressVersionId;
            }
            set
            {
                if (this._ProgressVersionId != value)
                {
                    this.ReportPropertyChanging("ProgressVersionId");
                    this._ProgressVersionId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ProgressVersionId");
                }
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK_progress_version_InputUser", "PT_yhmc"), XmlIgnore, SoapIgnore, DataMember]
        public cn.justwin.DAL.PT_yhmc PT_yhmc
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_progress_version_InputUser", "PT_yhmc").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_progress_version_InputUser", "PT_yhmc").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.PT_yhmc> PT_yhmcReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_progress_version_InputUser", "PT_yhmc");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_progress_version_InputUser", "PT_yhmc", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string VersionCode
        {
            get
            {
                return this._VersionCode;
            }
            set
            {
                this.ReportPropertyChanging("VersionCode");
                this._VersionCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("VersionCode");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string VersionName
        {
            get
            {
                return this._VersionName;
            }
            set
            {
                this.ReportPropertyChanging("VersionName");
                this._VersionName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("VersionName");
            }
        }
    }
}

