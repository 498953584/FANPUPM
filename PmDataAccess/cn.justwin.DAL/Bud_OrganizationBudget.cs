namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Bud_OrganizationBudget")]
    public class Bud_OrganizationBudget : EntityObject
    {
        private decimal? _AccountingAmount;
        private decimal? _BudgetAmount;
        private string _CBSCode;
        private string _Id;
        private DateTime _InputDate;
        private string _InputUser;
        private string _Note;
        private string _OrganizationBudgetId;
        private string _State;

        public static Bud_OrganizationBudget CreateBud_OrganizationBudget(string id, string organizationBudgetId, string cBSCode, string state, string inputUser, DateTime inputDate)
        {
            return new Bud_OrganizationBudget { Id = id, OrganizationBudgetId = organizationBudgetId, CBSCode = cBSCode, State = state, InputUser = inputUser, InputDate = inputDate };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public decimal? AccountingAmount
        {
            get
            {
                return this._AccountingAmount;
            }
            set
            {
                this.ReportPropertyChanging("AccountingAmount");
                this._AccountingAmount = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AccountingAmount");
            }
        }

        [SoapIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Organ__Organ__69D26A44", "Bud_OrganizationMonthBudget"), XmlIgnore]
        public EntityCollection<cn.justwin.DAL.Bud_OrganizationMonthBudget> Bud_OrganizationMonthBudget
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_OrganizationMonthBudget>("Pm2Model.FK__Bud_Organ__Organ__69D26A44", "Bud_OrganizationMonthBudget");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_OrganizationMonthBudget>("Pm2Model.FK__Bud_Organ__Organ__69D26A44", "Bud_OrganizationMonthBudget", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public decimal? BudgetAmount
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string OrganizationBudgetId
        {
            get
            {
                return this._OrganizationBudgetId;
            }
            set
            {
                this.ReportPropertyChanging("OrganizationBudgetId");
                this._OrganizationBudgetId = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("OrganizationBudgetId");
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

