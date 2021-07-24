namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_OrgDiaryCost"), DataContract(IsReference=true)]
    public class Bud_OrgDiaryCost : EntityObject
    {
        private string _Department;
        private int _FlowState;
        private DateTime _InputDate;
        private string _InputUser;
        private string _IssuedBy;
        private string _Name;
        private string _OrgdiaryCode;
        private string _OrgDiaryId;
        private int? _OrgId;

        public static Bud_OrgDiaryCost CreateBud_OrgDiaryCost(string orgDiaryId, string name, string issuedBy, int flowState, string inputUser, DateTime inputDate)
        {
            return new Bud_OrgDiaryCost { OrgDiaryId = orgDiaryId, Name = name, IssuedBy = issuedBy, FlowState = flowState, InputUser = inputUser, InputDate = inputDate };
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_OrgDi__OrgDi__48C67C34", "Bud_OrgDiaryDetails"), SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.Bud_OrgDiaryDetails> Bud_OrgDiaryDetails
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_OrgDiaryDetails>("Pm2Model.FK__Bud_OrgDi__OrgDi__48C67C34", "Bud_OrgDiaryDetails");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_OrgDiaryDetails>("Pm2Model.FK__Bud_OrgDi__OrgDi__48C67C34", "Bud_OrgDiaryDetails", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Department
        {
            get
            {
                return this._Department;
            }
            set
            {
                this.ReportPropertyChanging("Department");
                this._Department = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Department");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int FlowState
        {
            get
            {
                return this._FlowState;
            }
            set
            {
                this.ReportPropertyChanging("FlowState");
                this._FlowState = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FlowState");
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string IssuedBy
        {
            get
            {
                return this._IssuedBy;
            }
            set
            {
                this.ReportPropertyChanging("IssuedBy");
                this._IssuedBy = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("IssuedBy");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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
        public string OrgdiaryCode
        {
            get
            {
                return this._OrgdiaryCode;
            }
            set
            {
                this.ReportPropertyChanging("OrgdiaryCode");
                this._OrgdiaryCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("OrgdiaryCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string OrgDiaryId
        {
            get
            {
                return this._OrgDiaryId;
            }
            set
            {
                if (this._OrgDiaryId != value)
                {
                    this.ReportPropertyChanging("OrgDiaryId");
                    this._OrgDiaryId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("OrgDiaryId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? OrgId
        {
            get
            {
                return this._OrgId;
            }
            set
            {
                this.ReportPropertyChanging("OrgId");
                this._OrgId = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("OrgId");
            }
        }

        [SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_OrgDi__OrgId__31B8F9AA", "PT_d_bm"), DataMember]
        public cn.justwin.DAL.PT_d_bm PT_d_bm
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_d_bm>("Pm2Model.FK__Bud_OrgDi__OrgId__31B8F9AA", "PT_d_bm").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_d_bm>("Pm2Model.FK__Bud_OrgDi__OrgId__31B8F9AA", "PT_d_bm").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.PT_d_bm> PT_d_bmReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_d_bm>("Pm2Model.FK__Bud_OrgDi__OrgId__31B8F9AA", "PT_d_bm");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_d_bm>("Pm2Model.FK__Bud_OrgDi__OrgId__31B8F9AA", "PT_d_bm", value);
                }
            }
        }
    }
}

