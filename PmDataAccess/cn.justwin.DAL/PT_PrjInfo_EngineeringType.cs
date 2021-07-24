namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="PT_PrjInfo_EngineeringType"), DataContract(IsReference=true)]
    public class PT_PrjInfo_EngineeringType : EntityObject
    {
        private int? _EngineeringSubType;
        private string _EngineeringType;
        private string _Grade;
        private string _ID;
        private Guid? _PrjGuid;

        public static PT_PrjInfo_EngineeringType CreatePT_PrjInfo_EngineeringType(string id)
        {
            return new PT_PrjInfo_EngineeringType { ID = id };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? EngineeringSubType
        {
            get
            {
                return this._EngineeringSubType;
            }
            set
            {
                this.ReportPropertyChanging("EngineeringSubType");
                this._EngineeringSubType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("EngineeringSubType");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string EngineeringType
        {
            get
            {
                return this._EngineeringType;
            }
            set
            {
                this.ReportPropertyChanging("EngineeringType");
                this._EngineeringType = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("EngineeringType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Grade
        {
            get
            {
                return this._Grade;
            }
            set
            {
                this.ReportPropertyChanging("Grade");
                this._Grade = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Grade");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if (this._ID != value)
                {
                    this.ReportPropertyChanging("ID");
                    this._ID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public Guid? PrjGuid
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
    }
}

