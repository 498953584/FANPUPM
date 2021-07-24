namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_TemplateType"), DataContract(IsReference=true)]
    public class Bud_TemplateType : EntityObject
    {
        private DateTime _InputDate;
        private string _InputUser;
        private string _TypeCode;
        private string _TypeId;
        private string _TypeName;

        public static Bud_TemplateType CreateBud_TemplateType(string typeId, string typeCode, string typeName, string inputUser, DateTime inputDate)
        {
            return new Bud_TemplateType { TypeId = typeId, TypeCode = typeCode, TypeName = typeName, InputUser = inputUser, InputDate = inputDate };
        }

        [XmlIgnore, SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Templ__Templ__2611717D", "Bud_Template"), DataMember]
        public EntityCollection<cn.justwin.DAL.Bud_Template> Bud_Template
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_Template>("Pm2Model.FK__Bud_Templ__Templ__2611717D", "Bud_Template");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_Template>("Pm2Model.FK__Bud_Templ__Templ__2611717D", "Bud_Template", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public DateTime InputDate
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string InputUser
        {
            get
            {
                return this._InputUser;
            }
            set
            {
                this.ReportPropertyChanging("InputUser");
                this._InputUser = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("InputUser");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string TypeCode
        {
            get
            {
                return this._TypeCode;
            }
            set
            {
                this.ReportPropertyChanging("TypeCode");
                this._TypeCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TypeCode");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string TypeId
        {
            get
            {
                return this._TypeId;
            }
            set
            {
                if (this._TypeId != value)
                {
                    this.ReportPropertyChanging("TypeId");
                    this._TypeId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TypeId");
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

