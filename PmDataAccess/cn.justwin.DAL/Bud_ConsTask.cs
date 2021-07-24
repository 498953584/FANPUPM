namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Bud_ConsTask"), DataContract(IsReference=true)]
    public class Bud_ConsTask : EntityObject
    {
        private decimal? _AccountingQuantity;
        private decimal _CompleteQuantity;
        private string _ConsReportId;
        private string _ConsTaskId;
        private string _Note;
        private string _TaskId;
        private string _WorkContent;

        public static Bud_ConsTask CreateBud_ConsTask(string consTaskId, string taskId, decimal completeQuantity)
        {
            return new Bud_ConsTask { ConsTaskId = consTaskId, TaskId = taskId, CompleteQuantity = completeQuantity };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? AccountingQuantity
        {
            get
            {
                return this._AccountingQuantity;
            }
            set
            {
                this.ReportPropertyChanging("AccountingQuantity");
                this._AccountingQuantity = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("AccountingQuantity");
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_ConsT__ConsR__61DC42C1", "Bud_ConsReport"), SoapIgnore, DataMember]
        public cn.justwin.DAL.Bud_ConsReport Bud_ConsReport
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_ConsReport>("Pm2Model.FK__Bud_ConsT__ConsR__61DC42C1", "Bud_ConsReport").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_ConsReport>("Pm2Model.FK__Bud_ConsT__ConsR__61DC42C1", "Bud_ConsReport").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Bud_ConsReport> Bud_ConsReportReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Bud_ConsReport>("Pm2Model.FK__Bud_ConsT__ConsR__61DC42C1", "Bud_ConsReport");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Bud_ConsReport>("Pm2Model.FK__Bud_ConsT__ConsR__61DC42C1", "Bud_ConsReport", value);
                }
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_ConsT__ConsT__65ACD3A5", "Bud_ConsTaskRes"), XmlIgnore, DataMember, SoapIgnore]
        public EntityCollection<cn.justwin.DAL.Bud_ConsTaskRes> Bud_ConsTaskRes
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_ConsTaskRes>("Pm2Model.FK__Bud_ConsT__ConsT__65ACD3A5", "Bud_ConsTaskRes");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_ConsTaskRes>("Pm2Model.FK__Bud_ConsT__ConsT__65ACD3A5", "Bud_ConsTaskRes", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public decimal CompleteQuantity
        {
            get
            {
                return this._CompleteQuantity;
            }
            set
            {
                this.ReportPropertyChanging("CompleteQuantity");
                this._CompleteQuantity = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CompleteQuantity");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ConsReportId
        {
            get
            {
                return this._ConsReportId;
            }
            set
            {
                this.ReportPropertyChanging("ConsReportId");
                this._ConsReportId = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ConsReportId");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string ConsTaskId
        {
            get
            {
                return this._ConsTaskId;
            }
            set
            {
                if (this._ConsTaskId != value)
                {
                    this.ReportPropertyChanging("ConsTaskId");
                    this._ConsTaskId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ConsTaskId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Note
        {
            get
            {
                return this._Note;
            }
            set
            {
                this.ReportPropertyChanging("Note");
                this._Note = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Note");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string TaskId
        {
            get
            {
                return this._TaskId;
            }
            set
            {
                this.ReportPropertyChanging("TaskId");
                this._TaskId = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TaskId");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string WorkContent
        {
            get
            {
                return this._WorkContent;
            }
            set
            {
                this.ReportPropertyChanging("WorkContent");
                this._WorkContent = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("WorkContent");
            }
        }
    }
}

