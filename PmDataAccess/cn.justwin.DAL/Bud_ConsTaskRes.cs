namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_ConsTaskRes"), DataContract(IsReference=true)]
    public class Bud_ConsTaskRes : EntityObject
    {
        private decimal? _AccountingQuantity;
        private string _CBSCode;
        private string _ConsTaskId;
        private string _ConsTaskResId;
        private decimal _Quantity;
        private string _ResourceId;
        private decimal _UnitPrice;

        public static Bud_ConsTaskRes CreateBud_ConsTaskRes(string consTaskResId, string consTaskId, string resourceId, decimal quantity, decimal unitPrice)
        {
            return new Bud_ConsTaskRes { ConsTaskResId = consTaskResId, ConsTaskId = consTaskId, ResourceId = resourceId, Quantity = quantity, UnitPrice = unitPrice };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public decimal? AccountingQuantity
        {
            get
            {
                return this._AccountingQuantity;
            }
            set
            {
                this.ReportPropertyChanging("AccountingQuantity");
                this._AccountingQuantity = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AccountingQuantity");
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_ConsT__ConsT__65ACD3A5", "Bud_ConsTask"), SoapIgnore, DataMember]
        public cn.justwin.DAL.Bud_ConsTask Bud_ConsTask
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_ConsTask>("Pm2Model.FK__Bud_ConsT__ConsT__65ACD3A5", "Bud_ConsTask").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_ConsTask>("Pm2Model.FK__Bud_ConsT__ConsT__65ACD3A5", "Bud_ConsTask").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Bud_ConsTask> Bud_ConsTaskReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_ConsTask>("Pm2Model.FK__Bud_ConsT__ConsT__65ACD3A5", "Bud_ConsTask");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_ConsTask>("Pm2Model.FK__Bud_ConsT__ConsT__65ACD3A5", "Bud_ConsTask", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string CBSCode
        {
            get
            {
                return this._CBSCode;
            }
            set
            {
                this.ReportPropertyChanging("CBSCode");
                this._CBSCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("CBSCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ConsTaskId
        {
            get
            {
                return this._ConsTaskId;
            }
            set
            {
                this.ReportPropertyChanging("ConsTaskId");
                this._ConsTaskId = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ConsTaskId");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string ConsTaskResId
        {
            get
            {
                return this._ConsTaskResId;
            }
            set
            {
                if (this._ConsTaskResId != value)
                {
                    this.ReportPropertyChanging("ConsTaskResId");
                    this._ConsTaskResId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ConsTaskResId");
                }
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

        [XmlIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_ConsT__Resou__66A0F7DE", "Res_Resource"), SoapIgnore]
        public cn.justwin.DAL.Res_Resource Res_Resource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_ConsT__Resou__66A0F7DE", "Res_Resource").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_ConsT__Resou__66A0F7DE", "Res_Resource").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.Res_Resource> Res_ResourceReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_ConsT__Resou__66A0F7DE", "Res_Resource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_ConsT__Resou__66A0F7DE", "Res_Resource", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ResourceId
        {
            get
            {
                return this._ResourceId;
            }
            set
            {
                this.ReportPropertyChanging("ResourceId");
                this._ResourceId = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ResourceId");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public decimal UnitPrice
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

