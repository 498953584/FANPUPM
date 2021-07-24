namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="PT_Prj_Completed_Annex")]
    public class PT_Prj_Completed_Annex : EntityObject
    {
        private string _AnnexAddress;
        private string _ID;

        public static PT_Prj_Completed_Annex CreatePT_Prj_Completed_Annex(string id)
        {
            return new PT_Prj_Completed_Annex { ID = id };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string AnnexAddress
        {
            get
            {
                return this._AnnexAddress;
            }
            set
            {
                this.ReportPropertyChanging("AnnexAddress");
                this._AnnexAddress = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("AnnexAddress");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if (this._ID != value)
                {
                    this.ReportPropertyChanging("ID");
                    this._ID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ID");
                }
            }
        }

        [XmlIgnore, SoapIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_Prj_Co__Detai__32CD1974", "PT_Prj_Completed_Detail")]
        public cn.justwin.DAL.PT_Prj_Completed_Detail PT_Prj_Completed_Detail
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_Prj_Completed_Detail>("Pm2Model.FK__PT_Prj_Co__Detai__32CD1974", "PT_Prj_Completed_Detail").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_Prj_Completed_Detail>("Pm2Model.FK__PT_Prj_Co__Detai__32CD1974", "PT_Prj_Completed_Detail").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.PT_Prj_Completed_Detail> PT_Prj_Completed_DetailReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_Prj_Completed_Detail>("Pm2Model.FK__PT_Prj_Co__Detai__32CD1974", "PT_Prj_Completed_Detail");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_Prj_Completed_Detail>("Pm2Model.FK__PT_Prj_Co__Detai__32CD1974", "PT_Prj_Completed_Detail", value);
                }
            }
        }
    }
}

