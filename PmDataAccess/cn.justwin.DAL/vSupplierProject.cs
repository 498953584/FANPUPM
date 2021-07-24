namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="vSupplierProject"), DataContract(IsReference=true)]
    public class vSupplierProject : EntityObject
    {
        private string _ProjectId;
        private string _SupplierId;

        public static vSupplierProject CreatevSupplierProject(string supplierId, string projectId)
        {
            return new vSupplierProject { SupplierId = supplierId, ProjectId = projectId };
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
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

