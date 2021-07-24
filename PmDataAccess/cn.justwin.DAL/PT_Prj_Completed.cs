namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="PT_Prj_Completed")]
    public class PT_Prj_Completed : EntityObject
    {
        private string _ID;
        private DateTime _InputDate;
        private string _Name;

        public static PT_Prj_Completed CreatePT_Prj_Completed(string id, string name, DateTime inputDate)
        {
            return new PT_Prj_Completed { ID = id, Name = name, InputDate = inputDate };
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
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.ReportPropertyChanging("Name");
                this._Name = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Name");
            }
        }

        [SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_Prj_Co__PrjCo__6A525888", "PT_Prj_Completed_Detail"), DataMember, XmlIgnore]
        public EntityCollection<cn.justwin.DAL.PT_Prj_Completed_Detail> PT_Prj_Completed_Detail
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.PT_Prj_Completed_Detail>("Pm2Model.FK__PT_Prj_Co__PrjCo__6A525888", "PT_Prj_Completed_Detail");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.PT_Prj_Completed_Detail>("Pm2Model.FK__PT_Prj_Co__PrjCo__6A525888", "PT_Prj_Completed_Detail", value);
                }
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_Prj_Co__Input__5EE0A5DC", "PT_yhmc"), SoapIgnore, DataMember]
        public cn.justwin.DAL.PT_yhmc PT_yhmc
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_Prj_Co__Input__5EE0A5DC", "PT_yhmc").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_Prj_Co__Input__5EE0A5DC", "PT_yhmc").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.PT_yhmc> PT_yhmcReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_Prj_Co__Input__5EE0A5DC", "PT_yhmc");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK__PT_Prj_Co__Input__5EE0A5DC", "PT_yhmc", value);
                }
            }
        }
    }
}

