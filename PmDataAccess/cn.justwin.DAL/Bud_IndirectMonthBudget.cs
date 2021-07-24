namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_IndirectMonthBudget"), DataContract(IsReference=true)]
    public class Bud_IndirectMonthBudget : EntityObject
    {
        private decimal? _Amount;
        private string _Id;
        private int _Month;
        private int _Year;

        public static Bud_IndirectMonthBudget CreateBud_IndirectMonthBudget(string id, int year, int month)
        {
            return new Bud_IndirectMonthBudget { Id = id, Year = year, Month = month };
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

        [DataMember, XmlIgnore, SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Indir__Indir__0801EBA9", "Bud_IndirectBudget")]
        public cn.justwin.DAL.Bud_IndirectBudget Bud_IndirectBudget
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_IndirectBudget>("Pm2Model.FK__Bud_Indir__Indir__0801EBA9", "Bud_IndirectBudget").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_IndirectBudget>("Pm2Model.FK__Bud_Indir__Indir__0801EBA9", "Bud_IndirectBudget").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Bud_IndirectBudget> Bud_IndirectBudgetReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_IndirectBudget>("Pm2Model.FK__Bud_Indir__Indir__0801EBA9", "Bud_IndirectBudget");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_IndirectBudget>("Pm2Model.FK__Bud_Indir__Indir__0801EBA9", "Bud_IndirectBudget", value);
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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

