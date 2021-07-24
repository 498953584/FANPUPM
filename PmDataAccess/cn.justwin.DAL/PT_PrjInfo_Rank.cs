namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="PT_PrjInfo_Rank"), DataContract(IsReference=true)]
    public class PT_PrjInfo_Rank : EntityObject
    {
        private Guid _PrjGuid;
        private string _PrjRank;
        private string _RankId;
        private string _RankLevel;

        public static PT_PrjInfo_Rank CreatePT_PrjInfo_Rank(string rankId, Guid prjGuid)
        {
            return new PT_PrjInfo_Rank { RankId = rankId, PrjGuid = prjGuid };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public Guid PrjGuid
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PrjRank
        {
            get
            {
                return this._PrjRank;
            }
            set
            {
                this.ReportPropertyChanging("PrjRank");
                this._PrjRank = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjRank");
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_PrjInf__PrjGu__06E47595", "PT_PrjInfo_ZTB_Detail"), DataMember, SoapIgnore]
        public cn.justwin.DAL.PT_PrjInfo_ZTB_Detail PT_PrjInfo_ZTB_Detail
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail>("Pm2Model.FK__PT_PrjInf__PrjGu__06E47595", "PT_PrjInfo_ZTB_Detail").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail>("Pm2Model.FK__PT_PrjInf__PrjGu__06E47595", "PT_PrjInfo_ZTB_Detail").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail> PT_PrjInfo_ZTB_DetailReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail>("Pm2Model.FK__PT_PrjInf__PrjGu__06E47595", "PT_PrjInfo_ZTB_Detail");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail>("Pm2Model.FK__PT_PrjInf__PrjGu__06E47595", "PT_PrjInfo_ZTB_Detail", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string RankId
        {
            get
            {
                return this._RankId;
            }
            set
            {
                if (this._RankId != value)
                {
                    this.ReportPropertyChanging("RankId");
                    this._RankId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("RankId");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string RankLevel
        {
            get
            {
                return this._RankLevel;
            }
            set
            {
                this.ReportPropertyChanging("RankLevel");
                this._RankLevel = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("RankLevel");
            }
        }
    }
}

