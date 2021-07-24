namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="PT_Prj_Completed_Detail")]
    public class PT_Prj_Completed_Detail : EntityObject
    {
        private string _AnnexAddress;
        private string _ID;
        private DateTime _InputDate;
        private string _PrepareStatus;
        private Guid? _PrjGuid;
        private string _Rectification;
        private string _UncompletedTrans;

        public static PT_Prj_Completed_Detail CreatePT_Prj_Completed_Detail(string id, DateTime inputDate)
        {
            return new PT_Prj_Completed_Detail { ID = id, InputDate = inputDate };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string AnnexAddress
        {
            get
            {
                return this._AnnexAddress;
            }
            set
            {
                this.ReportPropertyChanging("AnnexAddress");
                this._AnnexAddress = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("AnnexAddress");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if (this._ID != value)
                {
                    this.ReportPropertyChanging("ID");
                    this._ID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public DateTime InputDate
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
        public string PrepareStatus
        {
            get
            {
                return this._PrepareStatus;
            }
            set
            {
                this.ReportPropertyChanging("PrepareStatus");
                this._PrepareStatus = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrepareStatus");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_Prj_Co__PrjCo__6A525888", "PT_Prj_Completed"), DataMember, XmlIgnore, SoapIgnore]
        public cn.justwin.DAL.PT_Prj_Completed PT_Prj_Completed
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_Prj_Completed>("Pm2Model.FK__PT_Prj_Co__PrjCo__6A525888", "PT_Prj_Completed").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_Prj_Completed>("Pm2Model.FK__PT_Prj_Co__PrjCo__6A525888", "PT_Prj_Completed").Value = value;
            }
        }

        [SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_Prj_Co__Detai__32CD1974", "PT_Prj_Completed_Annex"), XmlIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.PT_Prj_Completed_Annex> PT_Prj_Completed_Annex
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.PT_Prj_Completed_Annex>("Pm2Model.FK__PT_Prj_Co__Detai__32CD1974", "PT_Prj_Completed_Annex");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.PT_Prj_Completed_Annex>("Pm2Model.FK__PT_Prj_Co__Detai__32CD1974", "PT_Prj_Completed_Annex", value);
                }
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.PT_Prj_Completed> PT_Prj_CompletedReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_Prj_Completed>("Pm2Model.FK__PT_Prj_Co__PrjCo__6A525888", "PT_Prj_Completed");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_Prj_Completed>("Pm2Model.FK__PT_Prj_Co__PrjCo__6A525888", "PT_Prj_Completed", value);
                }
            }
        }

        [XmlIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_Prj_Co__Input__6B467CC1", "PT_yhmc"), SoapIgnore]
        public cn.justwin.DAL.PT_yhmc PT_yhmc
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_Prj_Co__Input__6B467CC1", "PT_yhmc").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_Prj_Co__Input__6B467CC1", "PT_yhmc").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.PT_yhmc> PT_yhmcReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_Prj_Co__Input__6B467CC1", "PT_yhmc");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_Prj_Co__Input__6B467CC1", "PT_yhmc", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Rectification
        {
            get
            {
                return this._Rectification;
            }
            set
            {
                this.ReportPropertyChanging("Rectification");
                this._Rectification = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Rectification");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string UncompletedTrans
        {
            get
            {
                return this._UncompletedTrans;
            }
            set
            {
                this.ReportPropertyChanging("UncompletedTrans");
                this._UncompletedTrans = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UncompletedTrans");
            }
        }
    }
}

