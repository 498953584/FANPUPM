namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="V_PCSupplier"), DataContract(IsReference=true)]
    public class V_PCSupplier : EntityObject
    {
        private string _ProjectId;
        private string _SupplierId;

        public static V_PCSupplier CreateV_PCSupplier(string supplierId, string projectId)
        {
            return new V_PCSupplier { SupplierId = supplierId, ProjectId = projectId };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ProjectId
        {
            get
            {
                return this._ProjectId;
            }
            set
            {
                if (this._ProjectId != value)
                {
                    this.ReportPropertyChanging("ProjectId");
                    this._ProjectId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ProjectId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string SupplierId
        {
            get
            {
                return this._SupplierId;
            }
            set
            {
                if (this._SupplierId != value)
                {
                    this.ReportPropertyChanging("SupplierId");
                    this._SupplierId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("SupplierId");
                }
            }
        }
    }
}

