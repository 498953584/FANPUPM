namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_OrgDiaryDetails"), DataContract(IsReference=true)]
    public class Bud_OrgDiaryDetails : EntityObject
    {
        private decimal _Amount;
        private string _CBSCode;
        private string _Name;
        private string _Note;
        private string _OrgdetailsCode;
        private string _OrgDiaryDetailsId;

        public static Bud_OrgDiaryDetails CreateBud_OrgDiaryDetails(string orgDiaryDetailsId, string name, decimal amount, string cBSCode)
        {
            return new Bud_OrgDiaryDetails { OrgDiaryDetailsId = orgDiaryDetailsId, Name = name, Amount = amount, CBSCode = cBSCode };
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

        [XmlIgnore, SoapIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_OrgDi__OrgDi__48C67C34", "Bud_OrgDiaryCost")]
        public cn.justwin.DAL.Bud_OrgDiaryCost Bud_OrgDiaryCost
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_OrgDiaryCost>("Pm2Model.FK__Bud_OrgDi__OrgDi__48C67C34", "Bud_OrgDiaryCost").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_OrgDiaryCost>("Pm2Model.FK__Bud_OrgDi__OrgDi__48C67C34", "Bud_OrgDiaryCost").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Bud_OrgDiaryCost> Bud_OrgDiaryCostReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_OrgDiaryCost>("Pm2Model.FK__Bud_OrgDi__OrgDi__48C67C34", "Bud_OrgDiaryCost");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_OrgDiaryCost>("Pm2Model.FK__Bud_OrgDi__OrgDi__48C67C34", "Bud_OrgDiaryCost", value);
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string OrgdetailsCode
        {
            get
            {
                return this._OrgdetailsCode;
            }
            set
            {
                this.ReportPropertyChanging("OrgdetailsCode");
                this._OrgdetailsCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("OrgdetailsCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string OrgDiaryDetailsId
        {
            get
            {
                return this._OrgDiaryDetailsId;
            }
            set
            {
                if (this._OrgDiaryDetailsId != value)
                {
                    this.ReportPropertyChanging("OrgDiaryDetailsId");
                    this._OrgDiaryDetailsId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("OrgDiaryDetailsId");
                }
            }
        }
    }
}

