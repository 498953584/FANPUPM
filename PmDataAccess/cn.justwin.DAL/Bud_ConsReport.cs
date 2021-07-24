namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Bud_ConsReport")]
    public class Bud_ConsReport : EntityObject
    {
        private string _CancelAuditReason;
        private string _CancelReportReason;
        private string _ConsReportId;
        private int _FlowState;
        private DateTime _InputDate;
        private string _InputUser;
        private bool? _IsValid;
        private string _PrjId;
        private string _State;
        private string _WorkCard;

        public static Bud_ConsReport CreateBud_ConsReport(string consReportId, string prjId, string inputUser, DateTime inputDate, string state, int flowState)
        {
            return new Bud_ConsReport { ConsReportId = consReportId, PrjId = prjId, InputUser = inputUser, InputDate = inputDate, State = state, FlowState = flowState };
        }

        [SoapIgnore, XmlIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_ConsT__ConsR__61DC42C1", "Bud_ConsTask")]
        public EntityCollection<cn.justwin.DAL.Bud_ConsTask> Bud_ConsTask
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_ConsTask>("Pm2Model.FK__Bud_ConsT__ConsR__61DC42C1", "Bud_ConsTask");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_ConsTask>("Pm2Model.FK__Bud_ConsT__ConsR__61DC42C1", "Bud_ConsTask", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string CancelAuditReason
        {
            get
            {
                return this._CancelAuditReason;
            }
            set
            {
                this.ReportPropertyChanging("CancelAuditReason");
                this._CancelAuditReason = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("CancelAuditReason");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string CancelReportReason
        {
            get
            {
                return this._CancelReportReason;
            }
            set
            {
                this.ReportPropertyChanging("CancelReportReason");
                this._CancelReportReason = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("CancelReportReason");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string ConsReportId
        {
            get
            {
                return this._ConsReportId;
            }
            set
            {
                if (this._ConsReportId != value)
                {
                    this.ReportPropertyChanging("ConsReportId");
                    this._ConsReportId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ConsReportId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string PrjId
        {
            get
            {
                return this._PrjId;
            }
            set
            {
                this.ReportPropertyChanging("PrjId");
                this._PrjId = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("PrjId");
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string WorkCard
        {
            get
            {
                return this._WorkCard;
            }
            set
            {
                this.ReportPropertyChanging("WorkCard");
                this._WorkCard = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("WorkCard");
            }
        }
    }
}

