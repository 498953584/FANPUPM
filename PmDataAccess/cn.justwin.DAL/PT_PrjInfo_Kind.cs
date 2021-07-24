namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="PT_PrjInfo_Kind")]
    public class PT_PrjInfo_Kind : EntityObject
    {
        private string _KindId;
        private Guid _PrjGuid;
        private string _PrjKind;
        private decimal _ProfessionalCost;

        public static PT_PrjInfo_Kind CreatePT_PrjInfo_Kind(string kindId, Guid prjGuid, decimal professionalCost)
        {
            return new PT_PrjInfo_Kind { KindId = kindId, PrjGuid = prjGuid, ProfessionalCost = professionalCost };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string KindId
        {
            get
            {
                return this._KindId;
            }
            set
            {
                if (this._KindId != value)
                {
                    this.ReportPropertyChanging("KindId");
                    this._KindId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("KindId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public Guid PrjGuid
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PrjKind
        {
            get
            {
                return this._PrjKind;
            }
            set
            {
                this.ReportPropertyChanging("PrjKind");
                this._PrjKind = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjKind");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public decimal ProfessionalCost
        {
            get
            {
                return this._ProfessionalCost;
            }
            set
            {
                this.ReportPropertyChanging("ProfessionalCost");
                this._ProfessionalCost = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProfessionalCost");
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_PrjInf__PrjGu__0313E4B1", "PT_PrjInfo_ZTB_Detail"), XmlIgnore, SoapIgnore, DataMember]
        public cn.justwin.DAL.PT_PrjInfo_ZTB_Detail PT_PrjInfo_ZTB_Detail
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail>("Pm2Model.FK__PT_PrjInf__PrjGu__0313E4B1", "PT_PrjInfo_ZTB_Detail").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail>("Pm2Model.FK__PT_PrjInf__PrjGu__0313E4B1", "PT_PrjInfo_ZTB_Detail").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail> PT_PrjInfo_ZTB_DetailReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail>("Pm2Model.FK__PT_PrjInf__PrjGu__0313E4B1", "PT_PrjInfo_ZTB_Detail");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail>("Pm2Model.FK__PT_PrjInf__PrjGu__0313E4B1", "PT_PrjInfo_ZTB_Detail", value);
                }
            }
        }
    }
}

