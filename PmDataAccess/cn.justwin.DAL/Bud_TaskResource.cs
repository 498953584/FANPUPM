namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Bud_TaskResource")]
    public class Bud_TaskResource : EntityObject
    {
        private DateTime _InputDate;
        private string _InputUser;
        private decimal? _LossCoefficient;
        private string _PrjGuid;
        private decimal? _ResourcePrice;
        private decimal? _ResourceQuantity;
        private string _TaskResourceId;
        private int? _Versions;

        public static Bud_TaskResource CreateBud_TaskResource(string taskResourceId, string inputUser, DateTime inputDate)
        {
            return new Bud_TaskResource { TaskResourceId = taskResourceId, InputUser = inputUser, InputDate = inputDate };
        }

        [SoapIgnore, DataMember, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_TaskR__TaskI__269B8162", "Bud_Task")]
        public cn.justwin.DAL.Bud_Task Bud_Task
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_Task>("Pm2Model.FK__Bud_TaskR__TaskI__269B8162", "Bud_Task").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_Task>("Pm2Model.FK__Bud_TaskR__TaskI__269B8162", "Bud_Task").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.Bud_Task> Bud_TaskReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_Task>("Pm2Model.FK__Bud_TaskR__TaskI__269B8162", "Bud_Task");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_Task>("Pm2Model.FK__Bud_TaskR__TaskI__269B8162", "Bud_Task", value);
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjGuid
        {
            get
            {
                return this._PrjGuid;
            }
            set
            {
                this.ReportPropertyChanging("PrjGuid");
                this._PrjGuid = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjGuid");
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_TaskR__Resou__278FA59B", "Res_Resource"), DataMember, XmlIgnore, SoapIgnore]
        public cn.justwin.DAL.Res_Resource Res_Resource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_TaskR__Resou__278FA59B", "Res_Resource").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_TaskR__Resou__278FA59B", "Res_Resource").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.Res_Resource> Res_ResourceReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_TaskR__Resou__278FA59B", "Res_Resource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_TaskR__Resou__278FA59B", "Res_Resource", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string TaskResourceId
        {
            get
            {
                return this._TaskResourceId;
            }
            set
            {
                if (this._TaskResourceId != value)
                {
                    this.ReportPropertyChanging("TaskResourceId");
                    this._TaskResourceId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TaskResourceId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? Versions
        {
            get
            {
                return this._Versions;
            }
            set
            {
                this.ReportPropertyChanging("Versions");
                this._Versions = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Versions");
            }
        }
    }
}

