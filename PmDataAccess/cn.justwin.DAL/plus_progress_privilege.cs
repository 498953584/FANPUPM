namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="plus_progress_privilege"), DataContract(IsReference=true)]
    public class plus_progress_privilege : EntityObject
    {
        private string _PrivilegeId;

        public static plus_progress_privilege Createplus_progress_privilege(string privilegeId)
        {
            return new plus_progress_privilege { PrivilegeId = privilegeId };
        }

        [SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK_privilege_ProgressId", "plus_progress"), DataMember]
        public cn.justwin.DAL.plus_progress plus_progress
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_progress>("Pm2Model.FK_privilege_ProgressId", "plus_progress").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_progress>("Pm2Model.FK_privilege_ProgressId", "plus_progress").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.plus_progress> plus_progressReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.plus_progress>("Pm2Model.FK_privilege_ProgressId", "plus_progress");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.plus_progress>("Pm2Model.FK_privilege_ProgressId", "plus_progress", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
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

        [EdmRelationshipNavigationProperty("Pm2Model", "FK_privilege_UserCode", "PT_yhmc"), SoapIgnore, XmlIgnore, DataMember]
        public cn.justwin.DAL.PT_yhmc PT_yhmc
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_privilege_UserCode", "PT_yhmc").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_privilege_UserCode", "PT_yhmc").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<cn.justwin.DAL.PT_yhmc> PT_yhmcReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_privilege_UserCode", "PT_yhmc");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_yhmc>("Pm2Model.FK_privilege_UserCode", "PT_yhmc", value);
                }
            }
        }
    }
}

