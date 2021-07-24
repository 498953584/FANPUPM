namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Bud_PrjTaskInfo")]
    public class Bud_PrjTaskInfo : EntityObject
    {
        private int _ConFlowState;
        private bool? _IsLocked;
        private string _PrjId;
        private string _PrjTaskInfoId;

        public static Bud_PrjTaskInfo CreateBud_PrjTaskInfo(string prjTaskInfoId, int conFlowState)
        {
            return new Bud_PrjTaskInfo { PrjTaskInfoId = prjTaskInfoId, ConFlowState = conFlowState };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int ConFlowState
        {
            get
            {
                return this._ConFlowState;
            }
            set
            {
                this.ReportPropertyChanging("ConFlowState");
                this._ConFlowState = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ConFlowState");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public bool? IsLocked
        {
            get
            {
                return this._IsLocked;
            }
            set
            {
                this.ReportPropertyChanging("IsLocked");
                this._IsLocked = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsLocked");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string PrjTaskInfoId
        {
            get
            {
                return this._PrjTaskInfoId;
            }
            set
            {
                if (this._PrjTaskInfoId != value)
                {
                    this.ReportPropertyChanging("PrjTaskInfoId");
                    this._PrjTaskInfoId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("PrjTaskInfoId");
                }
            }
        }
    }
}

