namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Bud_CostAccounting")]
    public class Bud_CostAccounting : EntityObject
    {
        private string _CBSCode;
        private string _Id;
        private string _Name;
        private string _Note;
        private string _ResourceType;
        private string _Type;

        public static Bud_CostAccounting CreateBud_CostAccounting(string id, string cBSCode, string name, string type)
        {
            return new Bud_CostAccounting { Id = id, CBSCode = cBSCode, Name = name, Type = type };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string CBSCode
        {
            get
            {
                return this._CBSCode;
            }
            set
            {
                this.ReportPropertyChanging("CBSCode");
                this._CBSCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CBSCode");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if (this._Id != value)
                {
                    this.ReportPropertyChanging("Id");
                    this._Id = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("Id");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.ReportPropertyChanging("Name");
                this._Name = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Name");
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ResourceType
        {
            get
            {
                return this._ResourceType;
            }
            set
            {
                this.ReportPropertyChanging("ResourceType");
                this._ResourceType = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ResourceType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this.ReportPropertyChanging("Type");
                this._Type = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Type");
            }
        }
    }
}

