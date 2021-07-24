namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Bud_ContractResource")]
    public class Bud_ContractResource : EntityObject
    {
        private DateTime _InputDate;
        private string _InputUser;
        private string _PrjGuid;
        private decimal? _ResourcePrice;
        private decimal? _ResourceQuantity;
        private string _TaskResourceId;

        public static Bud_ContractResource CreateBud_ContractResource(string taskResourceId, string inputUser, DateTime inputDate)
        {
            return new Bud_ContractResource { TaskResourceId = taskResourceId, InputUser = inputUser, InputDate = inputDate };
        }

        [DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Contr__TaskI__6F6B4809", "Bud_ContractTask"), XmlIgnore, SoapIgnore]
        public cn.justwin.DAL.Bud_ContractTask Bud_ContractTask
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_ContractTask>("Pm2Model.FK__Bud_Contr__TaskI__6F6B4809", "Bud_ContractTask").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_ContractTask>("Pm2Model.FK__Bud_Contr__TaskI__6F6B4809", "Bud_ContractTask").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Bud_ContractTask> Bud_ContractTaskReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_ContractTask>("Pm2Model.FK__Bud_Contr__TaskI__6F6B4809", "Bud_ContractTask");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_ContractTask>("Pm2Model.FK__Bud_Contr__TaskI__6F6B4809", "Bud_ContractTask", value);
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

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Contr__Resou__705F6C42", "Res_Resource"), XmlIgnore, SoapIgnore, DataMember]
        public cn.justwin.DAL.Res_Resource Res_Resource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_Contr__Resou__705F6C42", "Res_Resource").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_Contr__Resou__705F6C42", "Res_Resource").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.Res_Resource> Res_ResourceReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_Contr__Resou__705F6C42", "Res_Resource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_Resource>("Pm2Model.FK__Bud_Contr__Resou__705F6C42", "Res_Resource", value);
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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
    }
}

