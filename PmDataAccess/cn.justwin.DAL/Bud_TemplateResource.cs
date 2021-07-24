namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_TemplateResource"), DataContract(IsReference=true)]
    public class Bud_TemplateResource : EntityObject
    {
        private DateTime _InputDate;
        private string _InputUser;
        private decimal? _LossCoefficient;
        private decimal? _ResourcePrice;
        private decimal? _ResourceQuantity;
        private string _TemplateResourceId;

        public static Bud_TemplateResource CreateBud_TemplateResource(string templateResourceId, string inputUser, DateTime inputDate)
        {
            return new Bud_TemplateResource { TemplateResourceId = templateResourceId, InputUser = inputUser, InputDate = inputDate };
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Templ__Templ__6DB809C1", "Bud_TemplateItem"), DataMember, XmlIgnore, SoapIgnore]
        public cn.justwin.DAL.Bud_TemplateItem Bud_TemplateItem
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_TemplateItem>("Pm2Model.FK__Bud_Templ__Templ__6DB809C1", "Bud_TemplateItem").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_TemplateItem>("Pm2Model.FK__Bud_Templ__Templ__6DB809C1", "Bud_TemplateItem").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.Bud_TemplateItem> Bud_TemplateItemReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_TemplateItem>("Pm2Model.FK__Bud_Templ__Templ__6DB809C1", "Bud_TemplateItem");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_TemplateItem>("Pm2Model.FK__Bud_Templ__Templ__6DB809C1", "Bud_TemplateItem", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? LossCoefficient
        {
            get
            {
                return this._LossCoefficient;
            }
            set
            {
                this.ReportPropertyChanging("LossCoefficient");
                this._LossCoefficient = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("LossCoefficient");
            }
        }

        [SoapIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Templ__Resou__338B682C", "Res_Resource"), XmlIgnore]
        public cn.justwin.DAL.Res_Resource Res_Resource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_Templ__Resou__338B682C", "Res_Resource").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_Templ__Resou__338B682C", "Res_Resource").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Res_Resource> Res_ResourceReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_Templ__Resou__338B682C", "Res_Resource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_Templ__Resou__338B682C", "Res_Resource", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? ResourcePrice
        {
            get
            {
                return this._ResourcePrice;
            }
            set
            {
                this.ReportPropertyChanging("ResourcePrice");
                this._ResourcePrice = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ResourcePrice");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public decimal? ResourceQuantity
        {
            get
            {
                return this._ResourceQuantity;
            }
            set
            {
                this.ReportPropertyChanging("ResourceQuantity");
                this._ResourceQuantity = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ResourceQuantity");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string TemplateResourceId
        {
            get
            {
                return this._TemplateResourceId;
            }
            set
            {
                if (this._TemplateResourceId != value)
                {
                    this.ReportPropertyChanging("TemplateResourceId");
                    this._TemplateResourceId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TemplateResourceId");
                }
            }
        }
    }
}

