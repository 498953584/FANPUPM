namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Basic_CodeType")]
    public class Basic_CodeType : EntityObject
    {
        private string _TypeCode;
        private string _TypeName;

        public static Basic_CodeType CreateBasic_CodeType(string typeCode, string typeName)
        {
            return new Basic_CodeType { TypeCode = typeCode, TypeName = typeName };
        }

        [DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Basic_Cod__TypeC__37A6DD2A", "Basic_CodeList"), SoapIgnore, XmlIgnore]
        public EntityCollection<cn.justwin.DAL.Basic_CodeList> Basic_CodeList
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Basic_CodeList>("Pm2Model.FK__Basic_Cod__TypeC__37A6DD2A", "Basic_CodeList");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Basic_CodeList>("Pm2Model.FK__Basic_Cod__TypeC__37A6DD2A", "Basic_CodeList", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string TypeCode
        {
            get
            {
                return this._TypeCode;
            }
            set
            {
                if (this._TypeCode != value)
                {
                    this.ReportPropertyChanging("TypeCode");
                    this._TypeCode = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TypeCode");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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
    }
}

