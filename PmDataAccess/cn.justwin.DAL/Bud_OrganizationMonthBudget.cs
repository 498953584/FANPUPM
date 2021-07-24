namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Bud_OrganizationMonthBudget")]
    public class Bud_OrganizationMonthBudget : EntityObject
    {
        private decimal? _Amount;
        private string _Id;
        private int _Month;
        private int _Year;

        public static Bud_OrganizationMonthBudget CreateBud_OrganizationMonthBudget(string id, int year, int month)
        {
            return new Bud_OrganizationMonthBudget { Id = id, Year = year, Month = month };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Organ__Organ__69D26A44", "Bud_OrganizationBudget"), DataMember]
        public cn.justwin.DAL.Bud_OrganizationBudget Bud_OrganizationBudget
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_OrganizationBudget>("Pm2Model.FK__Bud_Organ__Organ__69D26A44", "Bud_OrganizationBudget").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_OrganizationBudget>("Pm2Model.FK__Bud_Organ__Organ__69D26A44", "Bud_OrganizationBudget").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.Bud_OrganizationBudget> Bud_OrganizationBudgetReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_OrganizationBudget>("Pm2Model.FK__Bud_Organ__Organ__69D26A44", "Bud_OrganizationBudget");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_OrganizationBudget>("Pm2Model.FK__Bud_Organ__Organ__69D26A44", "Bud_OrganizationBudget", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
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
        public int Month
        {
            get
            {
                return this._Month;
            }
            set
            {
                this.ReportPropertyChanging("Month");
                this._Month = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Month");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int Year
        {
            get
            {
                return this._Year;
            }
            set
            {
                this.ReportPropertyChanging("Year");
                this._Year = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Year");
            }
        }
    }
}

