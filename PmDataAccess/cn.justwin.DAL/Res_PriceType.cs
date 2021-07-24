namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Res_PriceType"), DataContract(IsReference=true)]
    public class Res_PriceType : EntityObject
    {
        private DateTime? _InputDate;
        private string _InputUser;
        private string _Note;
        private string _PriceTypeCode;
        private string _PriceTypeId;
        private string _PriceTypeName;
        private string _UserCodes;

        public static Res_PriceType CreateRes_PriceType(string priceTypeCode, string priceTypeId, string priceTypeName)
        {
            return new Res_PriceType { PriceTypeCode = priceTypeCode, PriceTypeId = priceTypeId, PriceTypeName = priceTypeName };
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string PriceTypeCode
        {
            get
            {
                return this._PriceTypeCode;
            }
            set
            {
                this.ReportPropertyChanging("PriceTypeCode");
                this._PriceTypeCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("PriceTypeCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string PriceTypeName
        {
            get
            {
                return this._PriceTypeName;
            }
            set
            {
                this.ReportPropertyChanging("PriceTypeName");
                this._PriceTypeName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("PriceTypeName");
            }
        }

        [SoapIgnore, XmlIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Price__Price__176E4C6B", "Res_Price")]
        public EntityCollection<cn.justwin.DAL.Res_Price> Res_Price
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Res_Price>("Pm2Model.FK__Res_Price__Price__176E4C6B", "Res_Price");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Res_Price>("Pm2Model.FK__Res_Price__Price__176E4C6B", "Res_Price", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string UserCodes
        {
            get
            {
                return this._UserCodes;
            }
            set
            {
                this.ReportPropertyChanging("UserCodes");
                this._UserCodes = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UserCodes");
            }
        }
    }
}

