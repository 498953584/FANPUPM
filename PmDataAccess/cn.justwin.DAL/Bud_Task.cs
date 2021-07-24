namespace cn.justwin.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Objects.DataClasses;
    using System.Data.SqlClient;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Bud_Task")]
    public class Bud_Task : EntityObject
    {
        private DateTime? _EndDate;
        private DateTime _InputDate;
        private string _InputUser;
        private bool? _IsValid;
        private string _Modified;
        private string _Note;
        private string _OrderNumber;
        private string _ParentId;
        private string _PrjId;
        private decimal _Quantity;
        private DateTime? _StartDate;
        private string _TaskCode;
        private string _TaskId;
        private string _TaskName;
        private decimal? _Total;
        private decimal? _Total2;
        private string _Unit;
        private decimal? _UnitPrice;
        private int? _Version;

        public static Bud_Task CreateBud_Task(string taskId, string taskCode, string taskName, decimal quantity, string inputUser, DateTime inputDate)
        {
            return new Bud_Task { TaskId = taskId, TaskCode = taskCode, TaskName = taskName, Quantity = quantity, InputUser = inputUser, InputDate = inputDate };
        }

        public DataTable GetDescription(string tablName)
        {
            string cmdText = "SELECT c.name,ep.value FROM sys.columns AS c\r\n                               JOIN sys.tables AS t ON c.object_id = t.object_id\r\n                               JOIN sys.extended_properties AS ep ON c.column_id = ep.minor_id\r\n                               WHERE t.name = '" + tablName + "' AND ep.major_id = t.object_id";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public DataTable GetResource()
        {
            string cmdText = "SELECT c.name,ep.value FROM sys.columns AS c\r\n                               JOIN sys.tables AS t ON c.object_id = t.object_id\r\n                               JOIN sys.extended_properties AS ep ON c.column_id = ep.minor_id\r\n                               WHERE t.name = 'Res_Resource' AND c.name != 'ResourceId'\r\n                               AND ep.major_id = t.object_id";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public static int GetStartIndex(DataTable excel, IDictionary<string, int> colRelation)
        {
            for (int i = 0; i < excel.Rows.Count; i++)
            {
                if ((!string.IsNullOrEmpty(excel.Rows[i][colRelation["TaskCode"]].ToString()) && !string.IsNullOrEmpty(excel.Rows[i][colRelation["TaskName"]].ToString())) && !string.IsNullOrEmpty(excel.Rows[i][colRelation["Unit"]].ToString()))
                {
                    return i;
                }
            }
            return 0;
        }

        [DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_TaskR__TaskI__269B8162", "Bud_TaskResource"), XmlIgnore, SoapIgnore]
        public EntityCollection<cn.justwin.DAL.Bud_TaskResource> Bud_TaskResource
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_TaskResource>("Pm2Model.FK__Bud_TaskR__TaskI__269B8162", "Bud_TaskResource");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_TaskResource>("Pm2Model.FK__Bud_TaskR__TaskI__269B8162", "Bud_TaskResource", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this.ReportPropertyChanging("EndDate");
                this._EndDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("EndDate");
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
        public string InputUser
        {
            get
            {
                return this._InputUser;
            }
            set
            {
                this.ReportPropertyChanging("InputUser");
                this._InputUser = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("InputUser");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public bool? IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this.ReportPropertyChanging("IsValid");
                this._IsValid = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsValid");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Modified
        {
            get
            {
                return this._Modified;
            }
            set
            {
                this.ReportPropertyChanging("Modified");
                this._Modified = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Modified");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string OrderNumber
        {
            get
            {
                return this._OrderNumber;
            }
            set
            {
                this.ReportPropertyChanging("OrderNumber");
                this._OrderNumber = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("OrderNumber");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                this.ReportPropertyChanging("ParentId");
                this._ParentId = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ParentId");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PrjId
        {
            get
            {
                return this._PrjId;
            }
            set
            {
                this.ReportPropertyChanging("PrjId");
                this._PrjId = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjId");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public decimal Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this.ReportPropertyChanging("Quantity");
                this._Quantity = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Quantity");
            }
        }

        [XmlIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Resou__TaskI__63F9955D", "Res_ResourceTemp"), SoapIgnore]
        public EntityCollection<cn.justwin.DAL.Res_ResourceTemp> Res_ResourceTemp
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Res_ResourceTemp>("Pm2Model.FK__Res_Resou__TaskI__63F9955D", "Res_ResourceTemp");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Res_ResourceTemp>("Pm2Model.FK__Res_Resou__TaskI__63F9955D", "Res_ResourceTemp", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this.ReportPropertyChanging("StartDate");
                this._StartDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("StartDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string TaskCode
        {
            get
            {
                return this._TaskCode;
            }
            set
            {
                this.ReportPropertyChanging("TaskCode");
                this._TaskCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TaskCode");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string TaskId
        {
            get
            {
                return this._TaskId;
            }
            set
            {
                if (this._TaskId != value)
                {
                    this.ReportPropertyChanging("TaskId");
                    this._TaskId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TaskId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string TaskName
        {
            get
            {
                return this._TaskName;
            }
            set
            {
                this.ReportPropertyChanging("TaskName");
                this._TaskName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TaskName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public decimal? Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                this.ReportPropertyChanging("Total");
                this._Total = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Total");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? Total2
        {
            get
            {
                return this._Total2;
            }
            set
            {
                this.ReportPropertyChanging("Total2");
                this._Total2 = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Total2");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Unit
        {
            get
            {
                return this._Unit;
            }
            set
            {
                this.ReportPropertyChanging("Unit");
                this._Unit = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Unit");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public decimal? UnitPrice
        {
            get
            {
                return this._UnitPrice;
            }
            set
            {
                this.ReportPropertyChanging("UnitPrice");
                this._UnitPrice = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UnitPrice");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                this.ReportPropertyChanging("Version");
                this._Version = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Version");
            }
        }
    }
}

