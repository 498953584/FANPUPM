namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_ContractTaskAudit"), DataContract(IsReference=true)]
    public class Bud_ContractTaskAudit : EntityObject
    {
        private string _ContractTaskAuditId;
        private int? _FlowState;
        private DateTime? _InputDate;
        private string _PrjId;
        private string _PrjName;

        public static Bud_ContractTaskAudit CreateBud_ContractTaskAudit(string contractTaskAuditId)
        {
            return new Bud_ContractTaskAudit { ContractTaskAuditId = contractTaskAuditId };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ContractTaskAuditId
        {
            get
            {
                return this._ContractTaskAuditId;
            }
            set
            {
                if (this._ContractTaskAuditId != value)
                {
                    this.ReportPropertyChanging("ContractTaskAuditId");
                    this._ContractTaskAuditId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ContractTaskAuditId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? FlowState
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? InputDate
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjName
        {
            get
            {
                return this._PrjName;
            }
            set
            {
                this.ReportPropertyChanging("PrjName");
                this._PrjName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjName");
            }
        }
    }
}

