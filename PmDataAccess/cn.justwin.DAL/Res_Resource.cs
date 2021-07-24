namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Res_Resource"), DataContract(IsReference=true)]
    public class Res_Resource : EntityObject
    {
        private string _Brand;
        private DateTime? _InputDate;
        private string _InputUser;
        private string _ModelNumber;
        private string _Note;
        private string _ResourceCode;
        private string _ResourceId;
        private string _ResourceName;
        private string _Series;
        private string _Specification;
        private int? _SupplierId;
        private decimal? _TaxRate;
        private string _TechnicalParameter;

        public static Res_Resource CreateRes_Resource(string resourceCode, string resourceId, string resourceName)
        {
            return new Res_Resource { ResourceCode = resourceCode, ResourceId = resourceId, ResourceName = resourceName };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Brand
        {
            get
            {
                return this._Brand;
            }
            set
            {
                this.ReportPropertyChanging("Brand");
                this._Brand = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Brand");
            }
        }

        [XmlIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_ConsT__Resou__66A0F7DE", "Bud_ConsTaskRes"), SoapIgnore]
        public EntityCollection<cn.justwin.DAL.Bud_ConsTaskRes> Bud_ConsTaskRes
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_ConsTaskRes>("Pm2Model.FK__Bud_ConsT__Resou__66A0F7DE", "Bud_ConsTaskRes");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_ConsTaskRes>("Pm2Model.FK__Bud_ConsT__Resou__66A0F7DE", "Bud_ConsTaskRes", value);
                }
            }
        }

        [DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Contr__Resou__705F6C42", "Bud_ContractResource"), XmlIgnore, SoapIgnore]
        public EntityCollection<cn.justwin.DAL.Bud_ContractResource> Bud_ContractResource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_ContractResource>("Pm2Model.FK__Bud_Contr__Resou__705F6C42", "Bud_ContractResource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_ContractResource>("Pm2Model.FK__Bud_Contr__Resou__705F6C42", "Bud_ContractResource", value);
                }
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_TaskR__Resou__278FA59B", "Bud_TaskResource"), SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.Bud_TaskResource> Bud_TaskResource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_TaskResource>("Pm2Model.FK__Bud_TaskR__Resou__278FA59B", "Bud_TaskResource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_TaskResource>("Pm2Model.FK__Bud_TaskR__Resou__278FA59B", "Bud_TaskResource", value);
                }
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Templ__Resou__338B682C", "Bud_TemplateResource"), XmlIgnore, SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.Bud_TemplateResource> Bud_TemplateResource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_TemplateResource>("Pm2Model.FK__Bud_Templ__Resou__338B682C", "Bud_TemplateResource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_TemplateResource>("Pm2Model.FK__Bud_Templ__Resou__338B682C", "Bud_TemplateResource", value);
                }
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
        public string ModelNumber
        {
            get
            {
                return this._ModelNumber;
            }
            set
            {
                this.ReportPropertyChanging("ModelNumber");
                this._ModelNumber = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ModelNumber");
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

        [XmlIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resou__Attri__7D446614", "Res_Attribute"), SoapIgnore]
        public cn.justwin.DAL.Res_Attribute Res_Attribute
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Attribute>("Pm2Model.FK__Res_Resou__Attri__7D446614", "Res_Attribute").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Attribute>("Pm2Model.FK__Res_Resou__Attri__7D446614", "Res_Attribute").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Res_Attribute> Res_AttributeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Attribute>("Pm2Model.FK__Res_Resou__Attri__7D446614", "Res_Attribute");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_Attribute>("Pm2Model.FK__Res_Resou__Attri__7D446614", "Res_Attribute", value);
                }
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Price__Resou__167A2832", "Res_Price"), XmlIgnore, SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.Res_Price> Res_Price
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Res_Price>("Pm2Model.FK__Res_Price__Resou__167A2832", "Res_Price");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Res_Price>("Pm2Model.FK__Res_Price__Resou__167A2832", "Res_Price", value);
                }
            }
        }

        [DataMember, XmlIgnore, SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resou__Resou__63057124", "Res_ResourceTemp")]
        public EntityCollection<cn.justwin.DAL.Res_ResourceTemp> Res_ResourceTemp
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Res_ResourceTemp>("Pm2Model.FK__Res_Resou__Resou__63057124", "Res_ResourceTemp");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Res_ResourceTemp>("Pm2Model.FK__Res_Resou__Resou__63057124", "Res_ResourceTemp", value);
                }
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resou__Resou__10C14EDC", "Res_ResourceType"), SoapIgnore, DataMember]
        public cn.justwin.DAL.Res_ResourceType Res_ResourceType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_ResourceType>("Pm2Model.FK__Res_Resou__Resou__10C14EDC", "Res_ResourceType").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_ResourceType>("Pm2Model.FK__Res_Resou__Resou__10C14EDC", "Res_ResourceType").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Res_ResourceType> Res_ResourceTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_ResourceType>("Pm2Model.FK__Res_Resou__Resou__10C14EDC", "Res_ResourceType");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_ResourceType>("Pm2Model.FK__Res_Resou__Resou__10C14EDC", "Res_ResourceType", value);
                }
            }
        }

        [DataMember, XmlIgnore, SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resour__Unit__11B57315", "Res_Unit")]
        public cn.justwin.DAL.Res_Unit Res_Unit
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Unit>("Pm2Model.FK__Res_Resour__Unit__11B57315", "Res_Unit").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Unit>("Pm2Model.FK__Res_Resour__Unit__11B57315", "Res_Unit").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.Res_Unit> Res_UnitReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Unit>("Pm2Model.FK__Res_Resour__Unit__11B57315", "Res_Unit");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_Unit>("Pm2Model.FK__Res_Resour__Unit__11B57315", "Res_Unit", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ResourceCode
        {
            get
            {
                return this._ResourceCode;
            }
            set
            {
                this.ReportPropertyChanging("ResourceCode");
                this._ResourceCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourceCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ResourceId
        {
            get
            {
                return this._ResourceId;
            }
            set
            {
                if (this._ResourceId != value)
                {
                    this.ReportPropertyChanging("ResourceId");
                    this._ResourceId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ResourceId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ResourceName
        {
            get
            {
                return this._ResourceName;
            }
            set
            {
                this.ReportPropertyChanging("ResourceName");
                this._ResourceName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourceName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Series
        {
            get
            {
                return this._Series;
            }
            set
            {
                this.ReportPropertyChanging("Series");
                this._Series = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Series");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Specification
        {
            get
            {
                return this._Specification;
            }
            set
            {
                this.ReportPropertyChanging("Specification");
                this._Specification = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Specification");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? SupplierId
        {
            get
            {
                return this._SupplierId;
            }
            set
            {
                this.ReportPropertyChanging("SupplierId");
                this._SupplierId = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SupplierId");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public decimal? TaxRate
        {
            get
            {
                return this._TaxRate;
            }
            set
            {
                this.ReportPropertyChanging("TaxRate");
                this._TaxRate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("TaxRate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string TechnicalParameter
        {
            get
            {
                return this._TechnicalParameter;
            }
            set
            {
                this.ReportPropertyChanging("TechnicalParameter");
                this._TechnicalParameter = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TechnicalParameter");
            }
        }
    }
}

