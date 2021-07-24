namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="PT_PrjInfo_ZTB_User")]
    public class PT_PrjInfo_ZTB_User : EntityObject
    {
        private string _Id;
        private Guid? _PrjGuid;
        private string _UserCode;

        public static PT_PrjInfo_ZTB_User CreatePT_PrjInfo_ZTB_User(string id, string userCode)
        {
            return new PT_PrjInfo_ZTB_User { Id = id, UserCode = userCode };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if (this._Id != value)
                {
                    this.ReportPropertyChanging("Id");
                    this._Id = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("Id");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public Guid? PrjGuid
        {
            get
            {
                return this._PrjGuid;
            }
            set
            {
                this.ReportPropertyChanging("PrjGuid");
                this._PrjGuid = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PrjGuid");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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

