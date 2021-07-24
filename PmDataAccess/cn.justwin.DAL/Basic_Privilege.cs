namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Basic_Privilege"), DataContract(IsReference=true)]
    public class Basic_Privilege : EntityObject
    {
        private string _PrivilegeId;
        private string _RelationsKey;
        private string _RelationsTable;
        private string _UserCode;

        public static Basic_Privilege CreateBasic_Privilege(string privilegeId, string relationsTable, string relationsKey, string userCode)
        {
            return new Basic_Privilege { PrivilegeId = privilegeId, RelationsTable = relationsTable, RelationsKey = relationsKey, UserCode = userCode };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string PrivilegeId
        {
            get
            {
                return this._PrivilegeId;
            }
            set
            {
                if (this._PrivilegeId != value)
                {
                    this.ReportPropertyChanging("PrivilegeId");
                    this._PrivilegeId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("PrivilegeId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string RelationsKey
        {
            get
            {
                return this._RelationsKey;
            }
            set
            {
                this.ReportPropertyChanging("RelationsKey");
                this._RelationsKey = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RelationsKey");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string RelationsTable
        {
            get
            {
                return this._RelationsTable;
            }
            set
            {
                this.ReportPropertyChanging("RelationsTable");
                this._RelationsTable = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RelationsTable");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this.ReportPropertyChanging("UserCode");
                this._UserCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("UserCode");
            }
        }
    }
}

