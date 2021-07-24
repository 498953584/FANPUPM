namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Res_Attribute"), DataContract(IsReference=true)]
    public class Res_Attribute : EntityObject
    {
        private string _AttributeId;
        private string _AttributeName;
        private DateTime? _InputDate;
        private string _InputUser;

        public static Res_Attribute CreateRes_Attribute(string attributeId)
        {
            return new Res_Attribute { AttributeId = attributeId };
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string AttributeId
        {
            get
            {
                return this._AttributeId;
            }
            set
            {
                if (this._AttributeId != value)
                {
                    this.ReportPropertyChanging("AttributeId");
                    this._AttributeId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("AttributeId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string AttributeName
        {
            get
            {
                return this._AttributeName;
            }
            set
            {
                this.ReportPropertyChanging("AttributeName");
                this._AttributeName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("AttributeName");
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resou__Attri__7D446614", "Res_Resource"), DataMember, XmlIgnore, SoapIgnore]
        public EntityCollection<cn.justwin.DAL.Res_Resource> Res_Resource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Resou__Attri__7D446614", "Res_Resource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Resou__Attri__7D446614", "Res_Resource", value);
                }
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Attri__Resou__1A4AB916", "Res_ResourceType"), SoapIgnore, DataMember]
        public cn.justwin.DAL.Res_ResourceType Res_ResourceType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_ResourceType>("Pm2Model.FK__Res_Attri__Resou__1A4AB916", "Res_ResourceType").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_ResourceType>("Pm2Model.FK__Res_Attri__Resou__1A4AB916", "Res_ResourceType").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Res_ResourceType> Res_ResourceTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_ResourceType>("Pm2Model.FK__Res_Attri__Resou__1A4AB916", "Res_ResourceType");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_ResourceType>("Pm2Model.FK__Res_Attri__Resou__1A4AB916", "Res_ResourceType", value);
                }
            }
        }
    }
}

