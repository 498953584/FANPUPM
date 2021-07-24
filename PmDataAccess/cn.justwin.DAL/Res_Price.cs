namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Res_Price")]
    public class Res_Price : EntityObject
    {
        private DateTime? _InputDate;
        private string _InputUser;
        private string _PriceTypeId;
        private decimal _PriceValue;
        private string _ResourceId;

        public static Res_Price CreateRes_Price(string priceTypeId, decimal priceValue, string resourceId)
        {
            return new Res_Price { PriceTypeId = priceTypeId, PriceValue = priceValue, ResourceId = resourceId };
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

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string PriceTypeId
        {
            get
            {
                return this._PriceTypeId;
            }
            set
            {
                if (this._PriceTypeId != value)
                {
                    this.ReportPropertyChanging("PriceTypeId");
                    this._PriceTypeId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("PriceTypeId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public decimal PriceValue
        {
            get
            {
                return this._PriceValue;
            }
            set
            {
                this.ReportPropertyChanging("PriceValue");
                this._PriceValue = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PriceValue");
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Price__Price__176E4C6B", "Res_PriceType"), SoapIgnore, XmlIgnore, DataMember]
        public cn.justwin.DAL.Res_PriceType Res_PriceType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_PriceType>("Pm2Model.FK__Res_Price__Price__176E4C6B", "Res_PriceType").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_PriceType>("Pm2Model.FK__Res_Price__Price__176E4C6B", "Res_PriceType").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Res_PriceType> Res_PriceTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_PriceType>("Pm2Model.FK__Res_Price__Price__176E4C6B", "Res_PriceType");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_PriceType>("Pm2Model.FK__Res_Price__Price__176E4C6B", "Res_PriceType", value);
                }
            }
        }

        [DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Price__Resou__167A2832", "Res_Resource"), XmlIgnore, SoapIgnore]
        public cn.justwin.DAL.Res_Resource Res_Resource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Price__Resou__167A2832", "Res_Resource").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Price__Resou__167A2832", "Res_Resource").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Res_Resource> Res_ResourceReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Price__Resou__167A2832", "Res_Resource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Price__Resou__167A2832", "Res_Resource", value);
                }
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
    }
}

