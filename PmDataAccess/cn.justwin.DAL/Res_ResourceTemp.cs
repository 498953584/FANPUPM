namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Res_ResourceTemp")]
    public class Res_ResourceTemp : EntityObject
    {
        private decimal? _Amount;
        private string _Id;
        private string _PrjId;
        private decimal? _Quantity;
        private string _ResourceCode;
        private string _ResourceName;
        private decimal? _UnitPrice;

        public static Res_ResourceTemp CreateRes_ResourceTemp(string id)
        {
            return new Res_ResourceTemp { Id = id };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public decimal? Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                this.ReportPropertyChanging("Amount");
                this._Amount = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Amount");
            }
        }

        [XmlIgnore, DataMember, SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resou__TaskI__63F9955D", "Bud_Task")]
        public cn.justwin.DAL.Bud_Task Bud_Task
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_Task>("Pm2Model.FK__Res_Resou__TaskI__63F9955D", "Bud_Task").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_Task>("Pm2Model.FK__Res_Resou__TaskI__63F9955D", "Bud_Task").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Bud_Task> Bud_TaskReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_Task>("Pm2Model.FK__Res_Resou__TaskI__63F9955D", "Bud_Task");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_Task>("Pm2Model.FK__Res_Resou__TaskI__63F9955D", "Bud_Task", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjId
        {
            get
            {
                return this._PrjId;
            }
            set
            {
                this.ReportPropertyChanging("PrjId");
                this._PrjId = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjId");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? Quantity
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

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resou__Resou__63057124", "Res_Resource"), SoapIgnore, DataMember, XmlIgnore]
        public cn.justwin.DAL.Res_Resource Res_Resource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Resou__Resou__63057124", "Res_Resource").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Resou__Resou__63057124", "Res_Resource").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Res_Resource> Res_ResourceReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Resou__Resou__63057124", "Res_Resource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Res_Resou__Resou__63057124", "Res_Resource", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ResourceCode
        {
            get
            {
                return this._ResourceCode;
            }
            set
            {
                this.ReportPropertyChanging("ResourceCode");
                this._ResourceCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ResourceCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ResourceName
        {
            get
            {
                return this._ResourceName;
            }
            set
            {
                this.ReportPropertyChanging("ResourceName");
                this._ResourceName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ResourceName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

