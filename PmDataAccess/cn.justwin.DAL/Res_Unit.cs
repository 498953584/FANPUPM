namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Res_Unit")]
    public class Res_Unit : EntityObject
    {
        private string _Code;
        private DateTime? _InputDate;
        private string _InputUser;
        private string _Name;
        private string _UnitId;

        public static Res_Unit CreateRes_Unit(string code, string name, string unitId)
        {
            return new Res_Unit { Code = code, Name = name, UnitId = unitId };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                this.ReportPropertyChanging("Code");
                this._Code = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Code");
            }
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

        [DataMember, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resour__Unit__11B57315", "Res_Resource"), SoapIgnore]
        public EntityCollection<cn.justwin.DAL.Res_Resource> Res_Resource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Resour__Unit__11B57315", "Res_Resource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Resour__Unit__11B57315", "Res_Resource", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string UnitId
        {
            get
            {
                return this._UnitId;
            }
            set
            {
                if (this._UnitId != value)
                {
                    this.ReportPropertyChanging("UnitId");
                    this._UnitId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("UnitId");
                }
            }
        }
    }
}

