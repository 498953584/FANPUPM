namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="XPM_Basic_CodeList"), DataContract(IsReference=true)]
    public class XPM_Basic_CodeList : EntityObject
    {
        private int _ChildNumber;
        private int _CodeID;
        private string _CodeName;
        private int? _I_xh;
        private bool _IsDefault;
        private bool _IsFixed;
        private bool _IsValid;
        private bool? _IsVisible;
        private int _NoteID;
        private string _Owner;
        private int _ParentCodeID;
        private string _ParentCodeList;
        private int _TypeID;
        private DateTime? _VersionTime;

        public static XPM_Basic_CodeList CreateXPM_Basic_CodeList(int noteID, int codeID, int typeID, int parentCodeID, string parentCodeList, string codeName, int childNumber, bool isFixed, bool isDefault, bool isValid)
        {
            return new XPM_Basic_CodeList { NoteID = noteID, CodeID = codeID, TypeID = typeID, ParentCodeID = parentCodeID, ParentCodeList = parentCodeList, CodeName = codeName, ChildNumber = childNumber, IsFixed = isFixed, IsDefault = isDefault, IsValid = isValid };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int ChildNumber
        {
            get
            {
                return this._ChildNumber;
            }
            set
            {
                this.ReportPropertyChanging("ChildNumber");
                this._ChildNumber = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ChildNumber");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int CodeID
        {
            get
            {
                return this._CodeID;
            }
            set
            {
                this.ReportPropertyChanging("CodeID");
                this._CodeID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CodeID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string CodeName
        {
            get
            {
                return this._CodeName;
            }
            set
            {
                this.ReportPropertyChanging("CodeName");
                this._CodeName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CodeName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? I_xh
        {
            get
            {
                return this._I_xh;
            }
            set
            {
                this.ReportPropertyChanging("I_xh");
                this._I_xh = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("I_xh");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsDefault
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsFixed
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

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int NoteID
        {
            get
            {
                return this._NoteID;
            }
            set
            {
                if (this._NoteID != value)
                {
                    this.ReportPropertyChanging("NoteID");
                    this._NoteID = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("NoteID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int ParentCodeID
        {
            get
            {
                return this._ParentCodeID;
            }
            set
            {
                this.ReportPropertyChanging("ParentCodeID");
                this._ParentCodeID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ParentCodeID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ParentCodeList
        {
            get
            {
                return this._ParentCodeList;
            }
            set
            {
                this.ReportPropertyChanging("ParentCodeList");
                this._ParentCodeList = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ParentCodeList");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int TypeID
        {
            get
            {
                return this._TypeID;
            }
            set
            {
                this.ReportPropertyChanging("TypeID");
                this._TypeID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("TypeID");
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
    }
}

