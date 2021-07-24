namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="PT_Warning"), DataContract(IsReference=true)]
    public class PT_Warning : EntityObject
    {
        private DateTime? _InputDate;
        private bool _IsOpened;
        private bool? _IsValid;
        private string _RelationsColumn;
        private string _RelationsKey;
        private string _RelationsTable;
        private string _URI;
        private string _UserCode;
        private string _WarningContent;
        private int _WarningId;
        private string _WarningTitle;

        public static PT_Warning CreatePT_Warning(int warningId, bool isOpened)
        {
            return new PT_Warning { WarningId = warningId, IsOpened = isOpened };
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsOpened
        {
            get
            {
                return this._IsOpened;
            }
            set
            {
                this.ReportPropertyChanging("IsOpened");
                this._IsOpened = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsOpened");
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string RelationsColumn
        {
            get
            {
                return this._RelationsColumn;
            }
            set
            {
                this.ReportPropertyChanging("RelationsColumn");
                this._RelationsColumn = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("RelationsColumn");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string RelationsKey
        {
            get
            {
                return this._RelationsKey;
            }
            set
            {
                this.ReportPropertyChanging("RelationsKey");
                this._RelationsKey = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("RelationsKey");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string RelationsTable
        {
            get
            {
                return this._RelationsTable;
            }
            set
            {
                this.ReportPropertyChanging("RelationsTable");
                this._RelationsTable = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("RelationsTable");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string URI
        {
            get
            {
                return this._URI;
            }
            set
            {
                this.ReportPropertyChanging("URI");
                this._URI = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("URI");
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string WarningContent
        {
            get
            {
                return this._WarningContent;
            }
            set
            {
                this.ReportPropertyChanging("WarningContent");
                this._WarningContent = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("WarningContent");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int WarningId
        {
            get
            {
                return this._WarningId;
            }
            set
            {
                if (this._WarningId != value)
                {
                    this.ReportPropertyChanging("WarningId");
                    this._WarningId = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("WarningId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string WarningTitle
        {
            get
            {
                return this._WarningTitle;
            }
            set
            {
                this.ReportPropertyChanging("WarningTitle");
                this._WarningTitle = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("WarningTitle");
            }
        }
    }
}

