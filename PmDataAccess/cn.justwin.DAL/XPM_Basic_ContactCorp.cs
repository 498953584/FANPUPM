namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="XPM_Basic_ContactCorp"), DataContract(IsReference=true)]
    public class XPM_Basic_ContactCorp : EntityObject
    {
        private string _AccountBank;
        private string _Address;
        private string _Aptitude;
        private int? _AuditState;
        private string _BankAccounts;
        private string _Brand;
        private string _Capital;
        private string _Client;
        private string _Contry;
        private string _CorpBrief;
        private int _CorpID;
        private int? _CorpKind;
        private string _CorpName;
        private string _Corporation;
        private int _CorpTypeID;
        private string _Email;
        private string _Fax;
        private Guid? _FlowGuid;
        private string _HandPhone;
        private bool? _IFProvider;
        private bool? _IsDefault;
        private bool? _IsFixed;
        private string _IsHot;
        private bool? _IsValid;
        private bool? _IsVisible;
        private string _LinkMan;
        private string _Owner;
        private string _PeopleNumber;
        private string _PostCode;
        private string _ShopCard;
        private string _Speciality;
        private string _TaxCard;
        private string _Telephone;
        private string _UnderlayAbility;
        private string _UserCode;
        private DateTime? _VersionTime;
        private string _WebSite;
        private int? _WorkType;
        private string _Zone;

        public static XPM_Basic_ContactCorp CreateXPM_Basic_ContactCorp(int corpID, int corpTypeID, string corpName)
        {
            return new XPM_Basic_ContactCorp { CorpID = corpID, CorpTypeID = corpTypeID, CorpName = corpName };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string AccountBank
        {
            get
            {
                return this._AccountBank;
            }
            set
            {
                this.ReportPropertyChanging("AccountBank");
                this._AccountBank = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("AccountBank");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Aptitude
        {
            get
            {
                return this._Aptitude;
            }
            set
            {
                this.ReportPropertyChanging("Aptitude");
                this._Aptitude = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Aptitude");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? AuditState
        {
            get
            {
                return this._AuditState;
            }
            set
            {
                this.ReportPropertyChanging("AuditState");
                this._AuditState = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AuditState");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string BankAccounts
        {
            get
            {
                return this._BankAccounts;
            }
            set
            {
                this.ReportPropertyChanging("BankAccounts");
                this._BankAccounts = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("BankAccounts");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Capital
        {
            get
            {
                return this._Capital;
            }
            set
            {
                this.ReportPropertyChanging("Capital");
                this._Capital = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Capital");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Client
        {
            get
            {
                return this._Client;
            }
            set
            {
                this.ReportPropertyChanging("Client");
                this._Client = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Client");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Contry
        {
            get
            {
                return this._Contry;
            }
            set
            {
                this.ReportPropertyChanging("Contry");
                this._Contry = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Contry");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string CorpBrief
        {
            get
            {
                return this._CorpBrief;
            }
            set
            {
                this.ReportPropertyChanging("CorpBrief");
                this._CorpBrief = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("CorpBrief");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int CorpID
        {
            get
            {
                return this._CorpID;
            }
            set
            {
                if (this._CorpID != value)
                {
                    this.ReportPropertyChanging("CorpID");
                    this._CorpID = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("CorpID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? CorpKind
        {
            get
            {
                return this._CorpKind;
            }
            set
            {
                this.ReportPropertyChanging("CorpKind");
                this._CorpKind = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CorpKind");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string CorpName
        {
            get
            {
                return this._CorpName;
            }
            set
            {
                this.ReportPropertyChanging("CorpName");
                this._CorpName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CorpName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Corporation
        {
            get
            {
                return this._Corporation;
            }
            set
            {
                this.ReportPropertyChanging("Corporation");
                this._Corporation = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Corporation");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int CorpTypeID
        {
            get
            {
                return this._CorpTypeID;
            }
            set
            {
                this.ReportPropertyChanging("CorpTypeID");
                this._CorpTypeID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CorpTypeID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public Guid? FlowGuid
        {
            get
            {
                return this._FlowGuid;
            }
            set
            {
                this.ReportPropertyChanging("FlowGuid");
                this._FlowGuid = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FlowGuid");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string HandPhone
        {
            get
            {
                return this._HandPhone;
            }
            set
            {
                this.ReportPropertyChanging("HandPhone");
                this._HandPhone = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("HandPhone");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public bool? IFProvider
        {
            get
            {
                return this._IFProvider;
            }
            set
            {
                this.ReportPropertyChanging("IFProvider");
                this._IFProvider = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IFProvider");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public bool? IsDefault
        {
            get
            {
                return this._IsDefault;
            }
            set
            {
                this.ReportPropertyChanging("IsDefault");
                this._IsDefault = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsDefault");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public bool? IsFixed
        {
            get
            {
                return this._IsFixed;
            }
            set
            {
                this.ReportPropertyChanging("IsFixed");
                this._IsFixed = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsFixed");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string IsHot
        {
            get
            {
                return this._IsHot;
            }
            set
            {
                this.ReportPropertyChanging("IsHot");
                this._IsHot = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("IsHot");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public bool? IsVisible
        {
            get
            {
                return this._IsVisible;
            }
            set
            {
                this.ReportPropertyChanging("IsVisible");
                this._IsVisible = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsVisible");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string LinkMan
        {
            get
            {
                return this._LinkMan;
            }
            set
            {
                this.ReportPropertyChanging("LinkMan");
                this._LinkMan = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("LinkMan");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Owner
        {
            get
            {
                return this._Owner;
            }
            set
            {
                this.ReportPropertyChanging("Owner");
                this._Owner = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Owner");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PeopleNumber
        {
            get
            {
                return this._PeopleNumber;
            }
            set
            {
                this.ReportPropertyChanging("PeopleNumber");
                this._PeopleNumber = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PeopleNumber");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PostCode
        {
            get
            {
                return this._PostCode;
            }
            set
            {
                this.ReportPropertyChanging("PostCode");
                this._PostCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PostCode");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ShopCard
        {
            get
            {
                return this._ShopCard;
            }
            set
            {
                this.ReportPropertyChanging("ShopCard");
                this._ShopCard = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ShopCard");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Speciality
        {
            get
            {
                return this._Speciality;
            }
            set
            {
                this.ReportPropertyChanging("Speciality");
                this._Speciality = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Speciality");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string TaxCard
        {
            get
            {
                return this._TaxCard;
            }
            set
            {
                this.ReportPropertyChanging("TaxCard");
                this._TaxCard = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TaxCard");
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
        public string UnderlayAbility
        {
            get
            {
                return this._UnderlayAbility;
            }
            set
            {
                this.ReportPropertyChanging("UnderlayAbility");
                this._UnderlayAbility = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UnderlayAbility");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this.ReportPropertyChanging("UserCode");
                this._UserCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UserCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? VersionTime
        {
            get
            {
                return this._VersionTime;
            }
            set
            {
                this.ReportPropertyChanging("VersionTime");
                this._VersionTime = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("VersionTime");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string WebSite
        {
            get
            {
                return this._WebSite;
            }
            set
            {
                this.ReportPropertyChanging("WebSite");
                this._WebSite = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("WebSite");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? WorkType
        {
            get
            {
                return this._WorkType;
            }
            set
            {
                this.ReportPropertyChanging("WorkType");
                this._WorkType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("WorkType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Zone
        {
            get
            {
                return this._Zone;
            }
            set
            {
                this.ReportPropertyChanging("Zone");
                this._Zone = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Zone");
            }
        }
    }
}

