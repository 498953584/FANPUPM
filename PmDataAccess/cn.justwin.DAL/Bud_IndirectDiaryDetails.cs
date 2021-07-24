namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_IndirectDiaryDetails"), DataContract(IsReference=true)]
    public class Bud_IndirectDiaryDetails : EntityObject
    {
        private decimal _Amount;
        private string _CBSCode;
        private string _IndetailsCode;
        private string _InDiaryDetailsId;
        private string _Name;
        private string _Note;

        public static Bud_IndirectDiaryDetails CreateBud_IndirectDiaryDetails(string name, decimal amount, string cBSCode, string inDiaryDetailsId)
        {
            return new Bud_IndirectDiaryDetails { Name = name, Amount = amount, CBSCode = cBSCode, InDiaryDetailsId = inDiaryDetailsId };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public decimal Amount
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

        [DataMember, SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Indir__InDia__0D85BAD5", "Bud_IndirectDiaryCost"), XmlIgnore]
        public cn.justwin.DAL.Bud_IndirectDiaryCost Bud_IndirectDiaryCost
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_IndirectDiaryCost>("Pm2Model.FK__Bud_Indir__InDia__0D85BAD5", "Bud_IndirectDiaryCost").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_IndirectDiaryCost>("Pm2Model.FK__Bud_Indir__InDia__0D85BAD5", "Bud_IndirectDiaryCost").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.Bud_IndirectDiaryCost> Bud_IndirectDiaryCostReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_IndirectDiaryCost>("Pm2Model.FK__Bud_Indir__InDia__0D85BAD5", "Bud_IndirectDiaryCost");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_IndirectDiaryCost>("Pm2Model.FK__Bud_Indir__InDia__0D85BAD5", "Bud_IndirectDiaryCost", value);
                }
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string IndetailsCode
        {
            get
            {
                return this._IndetailsCode;
            }
            set
            {
                this.ReportPropertyChanging("IndetailsCode");
                this._IndetailsCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("IndetailsCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string InDiaryDetailsId
        {
            get
            {
                return this._InDiaryDetailsId;
            }
            set
            {
                if (this._InDiaryDetailsId != value)
                {
                    this.ReportPropertyChanging("InDiaryDetailsId");
                    this._InDiaryDetailsId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("InDiaryDetailsId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.ReportPropertyChanging("Name");
                this._Name = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Name");
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
    }
}

