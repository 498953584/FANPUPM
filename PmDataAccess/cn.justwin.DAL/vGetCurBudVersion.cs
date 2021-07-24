namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="vGetCurBudVersion")]
    public class vGetCurBudVersion : EntityObject
    {
        private int? _CurVersion;
        private string _PrjId;
        private string _TaskChangeId;

        public static vGetCurBudVersion CreatevGetCurBudVersion(string taskChangeId)
        {
            return new vGetCurBudVersion { TaskChangeId = taskChangeId };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? CurVersion
        {
            get
            {
                return this._CurVersion;
            }
            set
            {
                this.ReportPropertyChanging("CurVersion");
                this._CurVersion = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CurVersion");
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
        public string TaskChangeId
        {
            get
            {
                return this._TaskChangeId;
            }
            set
            {
                if (this._TaskChangeId != value)
                {
                    this.ReportPropertyChanging("TaskChangeId");
                    this._TaskChangeId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TaskChangeId");
                }
            }
        }
    }
}

