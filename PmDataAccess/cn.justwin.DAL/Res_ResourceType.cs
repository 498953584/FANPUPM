namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Res_ResourceType"), DataContract(IsReference=true)]
    public class Res_ResourceType : EntityObject
    {
        private string _CBSCode;
        private DateTime? _InputDate;
        private string _InputUser;
        private bool? _IsValid;
        private string _ResourceTypeCode;
        private string _ResourceTypeId;
        private string _ResourceTypeName;

        public static Res_ResourceType CreateRes_ResourceType(string resourceTypeCode, string resourceTypeId, string resourceTypeName)
        {
            return new Res_ResourceType { ResourceTypeCode = resourceTypeCode, ResourceTypeId = resourceTypeId, ResourceTypeName = resourceTypeName };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string CBSCode
        {
            get
            {
                return this._CBSCode;
            }
            set
            {
                this.ReportPropertyChanging("CBSCode");
                this._CBSCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("CBSCode");
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
        public bool? IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this.ReportPropertyChanging("IsValid");
                this._IsValid = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsValid");
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Attri__Resou__1A4AB916", "Res_Attribute"), SoapIgnore, XmlIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.Res_Attribute> Res_Attribute
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Res_Attribute>("Pm2Model.FK__Res_Attri__Resou__1A4AB916", "Res_Attribute");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Res_Attribute>("Pm2Model.FK__Res_Attri__Resou__1A4AB916", "Res_Attribute", value);
                }
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resou__Resou__10C14EDC", "Res_Resource"), SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.Res_Resource> Res_Resource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Resou__Resou__10C14EDC", "Res_Resource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Resou__Resou__10C14EDC", "Res_Resource", value);
                }
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resou__Paren__0DE4E231", "Res_ResourceType1"), SoapIgnore, DataMember, XmlIgnore]
        public EntityCollection<Res_ResourceType> Res_ResourceType1
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Res_ResourceType>("Pm2Model.FK__Res_Resou__Paren__0DE4E231", "Res_ResourceType1");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Res_ResourceType>("Pm2Model.FK__Res_Resou__Paren__0DE4E231", "Res_ResourceType1", value);
                }
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resou__Paren__0DE4E231", "Res_ResourceType"), DataMember, XmlIgnore, SoapIgnore]
        public Res_ResourceType Res_ResourceType2
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Res_ResourceType>("Pm2Model.FK__Res_Resou__Paren__0DE4E231", "Res_ResourceType").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Res_ResourceType>("Pm2Model.FK__Res_Resou__Paren__0DE4E231", "Res_ResourceType").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<Res_ResourceType> Res_ResourceType2Reference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Res_ResourceType>("Pm2Model.FK__Res_Resou__Paren__0DE4E231", "Res_ResourceType");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Res_ResourceType>("Pm2Model.FK__Res_Resou__Paren__0DE4E231", "Res_ResourceType", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ResourceTypeCode
        {
            get
            {
                return this._ResourceTypeCode;
            }
            set
            {
                this.ReportPropertyChanging("ResourceTypeCode");
                this._ResourceTypeCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourceTypeCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ResourceTypeId
        {
            get
            {
                return this._ResourceTypeId;
            }
            set
            {
                if (this._ResourceTypeId != value)
                {
                    this.ReportPropertyChanging("ResourceTypeId");
                    this._ResourceTypeId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ResourceTypeId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ResourceTypeName
        {
            get
            {
                return this._ResourceTypeName;
            }
            set
            {
                this.ReportPropertyChanging("ResourceTypeName");
                this._ResourceTypeName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourceTypeName");
            }
        }
    }
}

