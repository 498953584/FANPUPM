namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Basic_ContactInfo"), DataContract(IsReference=true)]
    public class Basic_ContactInfo : EntityObject
    {
        private string _Address;
        private string _ContactInfoId;
        private string _ContactPerson;
        private string _Email;
        private string _Fax;
        private string _Note;
        private int _OrderNumber;
        private string _Phone;
        private string _RelationsKey;
        private string _Telephone;
        private string _Type;
        private string _ZipCode;

        public static Basic_ContactInfo CreateBasic_ContactInfo(string contactInfoId, string relationsKey, int orderNumber)
        {
            return new Basic_ContactInfo { ContactInfoId = contactInfoId, RelationsKey = relationsKey, OrderNumber = orderNumber };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                this.ReportPropertyChanging("Address");
                this._Address = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Address");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ContactInfoId
        {
            get
            {
                return this._ContactInfoId;
            }
            set
            {
                if (this._ContactInfoId != value)
                {
                    this.ReportPropertyChanging("ContactInfoId");
                    this._ContactInfoId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ContactInfoId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ContactPerson
        {
            get
            {
                return this._ContactPerson;
            }
            set
            {
                this.ReportPropertyChanging("ContactPerson");
                this._ContactPerson = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ContactPerson");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this.ReportPropertyChanging("Email");
                this._Email = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Email");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Fax
        {
            get
            {
                return this._Fax;
            }
            set
            {
                this.ReportPropertyChanging("Fax");
                this._Fax = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Fax");
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int OrderNumber
        {
            get
            {
                return this._OrderNumber;
            }
            set
            {
                this.ReportPropertyChanging("OrderNumber");
                this._OrderNumber = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("OrderNumber");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                this.ReportPropertyChanging("Phone");
                this._Phone = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Phone");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string RelationsKey
        {
            get
            {
                return this._RelationsKey;
            }
            set
            {
                this.ReportPropertyChanging("RelationsKey");
                this._RelationsKey = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RelationsKey");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Telephone
        {
            get
            {
                return this._Telephone;
            }
            set
            {
                this.ReportPropertyChanging("Telephone");
                this._Telephone = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Telephone");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this.ReportPropertyChanging("Type");
                this._Type = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Type");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ZipCode
        {
            get
            {
                return this._ZipCode;
            }
            set
            {
                this.ReportPropertyChanging("ZipCode");
                this._ZipCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ZipCode");
            }
        }
    }
}

