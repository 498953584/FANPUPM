namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Basic_CodeList")]
    public class Basic_CodeList : EntityObject
    {
        private int _ItemCode;
        private string _ItemName;
        private string _TypeCode;

        public static Basic_CodeList CreateBasic_CodeList(string typeCode, int itemCode, string itemName)
        {
            return new Basic_CodeList { TypeCode = typeCode, ItemCode = itemCode, ItemName = itemName };
        }

        [DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Basic_Cod__TypeC__37A6DD2A", "Basic_CodeType"), XmlIgnore, SoapIgnore]
        public cn.justwin.DAL.Basic_CodeType Basic_CodeType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Basic_CodeType>("Pm2Model.FK__Basic_Cod__TypeC__37A6DD2A", "Basic_CodeType").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Basic_CodeType>("Pm2Model.FK__Basic_Cod__TypeC__37A6DD2A", "Basic_CodeType").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Basic_CodeType> Basic_CodeTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Basic_CodeType>("Pm2Model.FK__Basic_Cod__TypeC__37A6DD2A", "Basic_CodeType");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Basic_CodeType>("Pm2Model.FK__Basic_Cod__TypeC__37A6DD2A", "Basic_CodeType", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int ItemCode
        {
            get
            {
                return this._ItemCode;
            }
            set
            {
                if (this._ItemCode != value)
                {
                    this.ReportPropertyChanging("ItemCode");
                    this._ItemCode = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("ItemCode");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ItemName
        {
            get
            {
                return this._ItemName;
            }
            set
            {
                this.ReportPropertyChanging("ItemName");
                this._ItemName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ItemName");
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
    }
}

