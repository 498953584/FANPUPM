namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_IndirectDiaryCost"), DataContract(IsReference=true)]
    public class Bud_IndirectDiaryCost : EntityObject
    {
        private string _Department;
        private int _FlowState;
        private string _InDiaryId;
        private string _IndireCode;
        private DateTime _InputDate;
        private string _InputUser;
        private string _IssuedBy;
        private string _Name;
        private string _ProjectId;

        public static Bud_IndirectDiaryCost CreateBud_IndirectDiaryCost(string projectId, string name, string issuedBy, string inputUser, DateTime inputDate, int flowState, string inDiaryId)
        {
            return new Bud_IndirectDiaryCost { ProjectId = projectId, Name = name, IssuedBy = issuedBy, InputUser = inputUser, InputDate = inputDate, FlowState = flowState, InDiaryId = inDiaryId };
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_Indir__InDia__0D85BAD5", "Bud_IndirectDiaryDetails"), SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.Bud_IndirectDiaryDetails> Bud_IndirectDiaryDetails
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_IndirectDiaryDetails>("Pm2Model.FK__Bud_Indir__InDia__0D85BAD5", "Bud_IndirectDiaryDetails");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_IndirectDiaryDetails>("Pm2Model.FK__Bud_Indir__InDia__0D85BAD5", "Bud_IndirectDiaryDetails", value);
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

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string InDiaryId
        {
            get
            {
                return this._InDiaryId;
            }
            set
            {
                if (this._InDiaryId != value)
                {
                    this.ReportPropertyChanging("InDiaryId");
                    this._InDiaryId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("InDiaryId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string IndireCode
        {
            get
            {
                return this._IndireCode;
            }
            set
            {
                this.ReportPropertyChanging("IndireCode");
                this._IndireCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("IndireCode");
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
    }
}

