namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_TaskChange"), DataContract(IsReference=true)]
    public class Bud_TaskChange : EntityObject
    {
        private int? _FlowState;
        private DateTime? _InputDate;
        private string _InputUser;
        private string _Note;
        private string _PrjId;
        private string _TaskChangeId;
        private int? _Version;
        private string _VersionCode;

        public static Bud_TaskChange CreateBud_TaskChange(string taskChangeId)
        {
            return new Bud_TaskChange { TaskChangeId = taskChangeId };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjId
        {
            get
            {
                return this._PrjId;
            }
            set
            {
                this.ReportPropertyChanging("PrjId");
                this._PrjId = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjId");
            }
        }

        [SoapIgnore, XmlIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_TaskC__Input__292EA0A1", "PT_yhmc")]
        public cn.justwin.DAL.PT_yhmc PT_yhmc
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__Bud_TaskC__Input__292EA0A1", "PT_yhmc").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__Bud_TaskC__Input__292EA0A1", "PT_yhmc").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.PT_yhmc> PT_yhmcReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__Bud_TaskC__Input__292EA0A1", "PT_yhmc");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__Bud_TaskC__Input__292EA0A1", "PT_yhmc", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string TaskChangeId
        {
            get
            {
                return this._TaskChangeId;
            }
            set
            {
                if (this._TaskChangeId != value)
                {
                    this.ReportPropertyChanging("TaskChangeId");
                    this._TaskChangeId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TaskChangeId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                this.ReportPropertyChanging("Version");
                this._Version = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Version");
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
    }
}

