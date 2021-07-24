namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="PT_PrjInfo_ZTB_Stock")]
    public class PT_PrjInfo_ZTB_Stock : EntityObject
    {
        private string _businessman;
        private string _grade;
        private Guid _PrjGuid;
        private string _telephone;

        public static PT_PrjInfo_ZTB_Stock CreatePT_PrjInfo_ZTB_Stock(Guid prjGuid)
        {
            return new PT_PrjInfo_ZTB_Stock { PrjGuid = prjGuid };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string businessman
        {
            get
            {
                return this._businessman;
            }
            set
            {
                this.ReportPropertyChanging("businessman");
                this._businessman = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("businessman");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string grade
        {
            get
            {
                return this._grade;
            }
            set
            {
                this.ReportPropertyChanging("grade");
                this._grade = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("grade");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public Guid PrjGuid
        {
            get
            {
                return this._PrjGuid;
            }
            set
            {
                if (this._PrjGuid != value)
                {
                    this.ReportPropertyChanging("PrjGuid");
                    this._PrjGuid = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("PrjGuid");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string telephone
        {
            get
            {
                return this._telephone;
            }
            set
            {
                this.ReportPropertyChanging("telephone");
                this._telephone = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("telephone");
            }
        }
    }
}

