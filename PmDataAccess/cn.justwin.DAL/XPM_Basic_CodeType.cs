namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="XPM_Basic_CodeType")]
    public class XPM_Basic_CodeType : EntityObject
    {
        private Guid? _ContractCropType;
        private bool _IsValid;
        private bool _IsVisible;
        private string _Owner;
        private string _Remark;
        private string _SignCode;
        private int _TypeID;
        private string _TypeName;
        private DateTime _VersionTime;

        public static XPM_Basic_CodeType CreateXPM_Basic_CodeType(int typeID, string typeName, bool isVisible, bool isValid, string remark, string signCode, string owner, DateTime versionTime)
        {
            return new XPM_Basic_CodeType { TypeID = typeID, TypeName = typeName, IsVisible = isVisible, IsValid = isValid, Remark = remark, SignCode = signCode, Owner = owner, VersionTime = versionTime };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public Guid? ContractCropType
        {
            get
            {
                return this._ContractCropType;
            }
            set
            {
                this.ReportPropertyChanging("ContractCropType");
                this._ContractCropType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ContractCropType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsValid
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsVisible
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string Owner
        {
            get
            {
                return this._Owner;
            }
            set
            {
                this.ReportPropertyChanging("Owner");
                this._Owner = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Owner");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this.ReportPropertyChanging("Remark");
                this._Remark = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Remark");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string SignCode
        {
            get
            {
                return this._SignCode;
            }
            set
            {
                this.ReportPropertyChanging("SignCode");
                this._SignCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("SignCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int TypeID
        {
            get
            {
                return this._TypeID;
            }
            set
            {
                if (this._TypeID != value)
                {
                    this.ReportPropertyChanging("TypeID");
                    this._TypeID = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("TypeID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string TypeName
        {
            get
            {
                return this._TypeName;
            }
            set
            {
                this.ReportPropertyChanging("TypeName");
                this._TypeName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TypeName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public DateTime VersionTime
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
    }
}

