namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_ContractTask"), DataContract(IsReference=true)]
    public class Bud_ContractTask : EntityObject
    {
        private int? _ConstructionPeriod;
        private DateTime? _EndDate;
        private DateTime _InputDate;
        private string _InputUser;
        private bool? _IsValid;
        private string _Note;
        private string _OrderNumber;
        private string _ParentId;
        private string _PrjId;
        private decimal _Quantity;
        private DateTime? _StartDate;
        private string _TaskCode;
        private string _TaskId;
        private string _TaskName;
        private string _TaskType;
        private decimal? _Total;
        private string _Unit;
        private decimal? _UnitPrice;

        public static Bud_ContractTask CreateBud_ContractTask(string taskId, string taskCode, string taskName, decimal quantity, string inputUser, DateTime inputDate, string taskType)
        {
            return new Bud_ContractTask { TaskId = taskId, TaskCode = taskCode, TaskName = taskName, Quantity = quantity, InputUser = inputUser, InputDate = inputDate, TaskType = taskType };
        }

        [DataMember, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Contr__TaskI__6F6B4809", "Bud_ContractResource"), SoapIgnore]
        public EntityCollection<cn.justwin.DAL.Bud_ContractResource> Bud_ContractResource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_ContractResource>("Pm2Model.FK__Bud_Contr__TaskI__6F6B4809", "Bud_ContractResource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_ContractResource>("Pm2Model.FK__Bud_Contr__TaskI__6F6B4809", "Bud_ContractResource", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? ConstructionPeriod
        {
            get
            {
                return this._ConstructionPeriod;
            }
            set
            {
                this.ReportPropertyChanging("ConstructionPeriod");
                this._ConstructionPeriod = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ConstructionPeriod");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this.ReportPropertyChanging("EndDate");
                this._EndDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("EndDate");
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
        public bool? IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this.ReportPropertyChanging("IsValid");
                this._IsValid = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsValid");
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this.ReportPropertyChanging("StartDate");
                this._StartDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("StartDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string TaskCode
        {
            get
            {
                return this._TaskCode;
            }
            set
            {
                this.ReportPropertyChanging("TaskCode");
                this._TaskCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TaskCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string TaskId
        {
            get
            {
                return this._TaskId;
            }
            set
            {
                if (this._TaskId != value)
                {
                    this.ReportPropertyChanging("TaskId");
                    this._TaskId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TaskId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string TaskName
        {
            get
            {
                return this._TaskName;
            }
            set
            {
                this.ReportPropertyChanging("TaskName");
                this._TaskName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TaskName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string TaskType
        {
            get
            {
                return this._TaskType;
            }
            set
            {
                this.ReportPropertyChanging("TaskType");
                this._TaskType = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TaskType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                this.ReportPropertyChanging("Total");
                this._Total = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Total");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

