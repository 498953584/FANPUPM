namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="plus_progress")]
    public class plus_progress : EntityObject
    {
        private int? _FlowState;
        private DateTime? _InputDate;
        private bool _IsMain;
        private string _Note;
        private Guid? _PrjId;
        private string _ProgressId;
        private string _ProgressName;

        public static plus_progress Createplus_progress(string progressId, bool isMain)
        {
            return new plus_progress { ProgressId = progressId, IsMain = isMain };
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsMain
        {
            get
            {
                return this._IsMain;
            }
            set
            {
                this.ReportPropertyChanging("IsMain");
                this._IsMain = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsMain");
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

        [DataMember, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK_privilege_ProgressId", "plus_progress_privilege"), SoapIgnore]
        public EntityCollection<cn.justwin.DAL.plus_progress_privilege> plus_progress_privilege
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.plus_progress_privilege>("Pm2Model.FK_privilege_ProgressId", "plus_progress_privilege");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.plus_progress_privilege>("Pm2Model.FK_privilege_ProgressId", "plus_progress_privilege", value);
                }
            }
        }

        [DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK_progress_version_ProgressId", "plus_progress_version"), SoapIgnore, XmlIgnore]
        public EntityCollection<cn.justwin.DAL.plus_progress_version> plus_progress_version
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.plus_progress_version>("Pm2Model.FK_progress_version_ProgressId", "plus_progress_version");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.plus_progress_version>("Pm2Model.FK_progress_version_ProgressId", "plus_progress_version", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public Guid? PrjId
        {
            get
            {
                return this._PrjId;
            }
            set
            {
                this.ReportPropertyChanging("PrjId");
                this._PrjId = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PrjId");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string ProgressId
        {
            get
            {
                return this._ProgressId;
            }
            set
            {
                if (this._ProgressId != value)
                {
                    this.ReportPropertyChanging("ProgressId");
                    this._ProgressId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ProgressId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ProgressName
        {
            get
            {
                return this._ProgressName;
            }
            set
            {
                this.ReportPropertyChanging("ProgressName");
                this._ProgressName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProgressName");
            }
        }

        [DataMember, SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK_plus_progress_InputUser", "PT_yhmc"), XmlIgnore]
        public cn.justwin.DAL.PT_yhmc PT_yhmc
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_plus_progress_InputUser", "PT_yhmc").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_plus_progress_InputUser", "PT_yhmc").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.PT_yhmc> PT_yhmcReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_plus_progress_InputUser", "PT_yhmc");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_plus_progress_InputUser", "PT_yhmc", value);
                }
            }
        }
    }
}

