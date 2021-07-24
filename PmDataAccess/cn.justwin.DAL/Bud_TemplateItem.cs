namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Bud_TemplateItem")]
    public class Bud_TemplateItem : EntityObject
    {
        private string _FeatureDescription;
        private string _ItemCode;
        private string _ItemName;
        private string _Note;
        private string _OrderNumber;
        private string _ParentId;
        private decimal _Quantity;
        private string _TemplateItemId;
        private string _Unit;
        private decimal? _UnitPrice;

        public static Bud_TemplateItem CreateBud_TemplateItem(string templateItemId, decimal quantity)
        {
            return new Bud_TemplateItem { TemplateItemId = templateItemId, Quantity = quantity };
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Templ__Templ__247E2EC6", "Bud_Template"), XmlIgnore, SoapIgnore, DataMember]
        public cn.justwin.DAL.Bud_Template Bud_Template
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_Template>("Pm2Model.FK__Bud_Templ__Templ__247E2EC6", "Bud_Template").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_Template>("Pm2Model.FK__Bud_Templ__Templ__247E2EC6", "Bud_Template").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Bud_Template> Bud_TemplateReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_Template>("Pm2Model.FK__Bud_Templ__Templ__247E2EC6", "Bud_Template");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_Template>("Pm2Model.FK__Bud_Templ__Templ__247E2EC6", "Bud_Template", value);
                }
            }
        }

        [DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Templ__Templ__6DB809C1", "Bud_TemplateResource"), SoapIgnore, XmlIgnore]
        public EntityCollection<cn.justwin.DAL.Bud_TemplateResource> Bud_TemplateResource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_TemplateResource>("Pm2Model.FK__Bud_Templ__Templ__6DB809C1", "Bud_TemplateResource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_TemplateResource>("Pm2Model.FK__Bud_Templ__Templ__6DB809C1", "Bud_TemplateResource", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string FeatureDescription
        {
            get
            {
                return this._FeatureDescription;
            }
            set
            {
                this.ReportPropertyChanging("FeatureDescription");
                this._FeatureDescription = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("FeatureDescription");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ItemCode
        {
            get
            {
                return this._ItemCode;
            }
            set
            {
                this.ReportPropertyChanging("ItemCode");
                this._ItemCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ItemCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ItemName
        {
            get
            {
                return this._ItemName;
            }
            set
            {
                this.ReportPropertyChanging("ItemName");
                this._ItemName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ItemName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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
        public string OrderNumber
        {
            get
            {
                return this._OrderNumber;
            }
            set
            {
                this.ReportPropertyChanging("OrderNumber");
                this._OrderNumber = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("OrderNumber");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                this.ReportPropertyChanging("ParentId");
                this._ParentId = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ParentId");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public decimal Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this.ReportPropertyChanging("Quantity");
                this._Quantity = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Quantity");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string TemplateItemId
        {
            get
            {
                return this._TemplateItemId;
            }
            set
            {
                if (this._TemplateItemId != value)
                {
                    this.ReportPropertyChanging("TemplateItemId");
                    this._TemplateItemId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TemplateItemId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Unit
        {
            get
            {
                return this._Unit;
            }
            set
            {
                this.ReportPropertyChanging("Unit");
                this._Unit = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Unit");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? UnitPrice
        {
            get
            {
                return this._UnitPrice;
            }
            set
            {
                this.ReportPropertyChanging("UnitPrice");
                this._UnitPrice = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UnitPrice");
            }
        }
    }
}

