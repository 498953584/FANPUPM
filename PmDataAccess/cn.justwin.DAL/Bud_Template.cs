namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_Template"), DataContract(IsReference=true)]
    public class Bud_Template : EntityObject
    {
        private DateTime _InputDate;
        private string _InputUser;
        private string _TemplateCode;
        private string _TemplateId;
        private string _TemplateName;

        public static Bud_Template CreateBud_Template(string templateId, string templateCode, string templateName, string inputUser, DateTime inputDate)
        {
            return new Bud_Template { TemplateId = templateId, TemplateCode = templateCode, TemplateName = templateName, InputUser = inputUser, InputDate = inputDate };
        }

        [DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Templ__Templ__247E2EC6", "Bud_TemplateItem"), XmlIgnore, SoapIgnore]
        public EntityCollection<cn.justwin.DAL.Bud_TemplateItem> Bud_TemplateItem
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_TemplateItem>("Pm2Model.FK__Bud_Templ__Templ__247E2EC6", "Bud_TemplateItem");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_TemplateItem>("Pm2Model.FK__Bud_Templ__Templ__247E2EC6", "Bud_TemplateItem", value);
                }
            }
        }

        [DataMember, SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Templ__Templ__2611717D", "Bud_TemplateType"), XmlIgnore]
        public cn.justwin.DAL.Bud_TemplateType Bud_TemplateType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_TemplateType>("Pm2Model.FK__Bud_Templ__Templ__2611717D", "Bud_TemplateType").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_TemplateType>("Pm2Model.FK__Bud_Templ__Templ__2611717D", "Bud_TemplateType").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.Bud_TemplateType> Bud_TemplateTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_TemplateType>("Pm2Model.FK__Bud_Templ__Templ__2611717D", "Bud_TemplateType");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_TemplateType>("Pm2Model.FK__Bud_Templ__Templ__2611717D", "Bud_TemplateType", value);
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
        public string TemplateCode
        {
            get
            {
                return this._TemplateCode;
            }
            set
            {
                this.ReportPropertyChanging("TemplateCode");
                this._TemplateCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TemplateCode");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string TemplateId
        {
            get
            {
                return this._TemplateId;
            }
            set
            {
                if (this._TemplateId != value)
                {
                    this.ReportPropertyChanging("TemplateId");
                    this._TemplateId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TemplateId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string TemplateName
        {
            get
            {
                return this._TemplateName;
            }
            set
            {
                this.ReportPropertyChanging("TemplateName");
                this._TemplateName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TemplateName");
            }
        }
    }
}

