namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_IndirectBudget"), DataContract(IsReference=true)]
    public class Bud_IndirectBudget : EntityObject
    {
        private decimal _AccountAmount;
        private decimal _BudgetAmount;
        private string _CBSCode;
        private string _Id;
        private DateTime _InputDate;
        private string _InputUser;
        private string _Note;
        private string _ProjectId;
        private string _State;

        public static Bud_IndirectBudget CreateBud_IndirectBudget(string id, string projectId, string cBSCode, decimal budgetAmount, decimal accountAmount, string state, string inputUser, DateTime inputDate)
        {
            return new Bud_IndirectBudget { Id = id, ProjectId = projectId, CBSCode = cBSCode, BudgetAmount = budgetAmount, AccountAmount = accountAmount, State = state, InputUser = inputUser, InputDate = inputDate };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public decimal AccountAmount
        {
            get
            {
                return this._AccountAmount;
            }
            set
            {
                this.ReportPropertyChanging("AccountAmount");
                this._AccountAmount = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AccountAmount");
            }
        }

        [XmlIgnore, SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Indir__Indir__0801EBA9", "Bud_IndirectMonthBudget"), DataMember]
        public EntityCollection<cn.justwin.DAL.Bud_IndirectMonthBudget> Bud_IndirectMonthBudget
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_IndirectMonthBudget>("Pm2Model.FK__Bud_Indir__Indir__0801EBA9", "Bud_IndirectMonthBudget");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_IndirectMonthBudget>("Pm2Model.FK__Bud_Indir__Indir__0801EBA9", "Bud_IndirectMonthBudget", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public decimal BudgetAmount
        {
            get
            {
                return this._BudgetAmount;
            }
            set
            {
                this.ReportPropertyChanging("BudgetAmount");
                this._BudgetAmount = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("BudgetAmount");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string CBSCode
        {
            get
            {
                return this._CBSCode;
            }
            set
            {
                this.ReportPropertyChanging("CBSCode");
                this._CBSCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CBSCode");
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ProjectId
        {
            get
            {
                return this._ProjectId;
            }
            set
            {
                this.ReportPropertyChanging("ProjectId");
                this._ProjectId = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ProjectId");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string State
        {
            get
            {
                return this._State;
            }
            set
            {
                this.ReportPropertyChanging("State");
                this._State = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("State");
            }
        }
    }
}

