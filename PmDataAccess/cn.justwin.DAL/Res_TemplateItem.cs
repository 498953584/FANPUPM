namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="Res_TemplateItem"), DataContract(IsReference=true)]
    public class Res_TemplateItem : EntityObject
    {
        private string _DbColumn;
        private string _ExcelColumn;
        private string _ExcelRealCoumn;
        private string _ItemId;

        public static Res_TemplateItem CreateRes_TemplateItem(string itemId, string excelColumn, string dbColumn, string excelRealCoumn)
        {
            return new Res_TemplateItem { ItemId = itemId, ExcelColumn = excelColumn, DbColumn = dbColumn, ExcelRealCoumn = excelRealCoumn };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string DbColumn
        {
            get
            {
                return this._DbColumn;
            }
            set
            {
                this.ReportPropertyChanging("DbColumn");
                this._DbColumn = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("DbColumn");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ExcelColumn
        {
            get
            {
                return this._ExcelColumn;
            }
            set
            {
                this.ReportPropertyChanging("ExcelColumn");
                this._ExcelColumn = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ExcelColumn");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ExcelRealCoumn
        {
            get
            {
                return this._ExcelRealCoumn;
            }
            set
            {
                this.ReportPropertyChanging("ExcelRealCoumn");
                this._ExcelRealCoumn = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ExcelRealCoumn");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ItemId
        {
            get
            {
                return this._ItemId;
            }
            set
            {
                if (this._ItemId != value)
                {
                    this.ReportPropertyChanging("ItemId");
                    this._ItemId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ItemId");
                }
            }
        }

        [SoapIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Templ__Templ__61674175", "Res_Template"), XmlIgnore]
        public cn.justwin.DAL.Res_Template Res_Template
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Template>("Pm2Model.FK__Res_Templ__Templ__61674175", "Res_Template").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Template>("Pm2Model.FK__Res_Templ__Templ__61674175", "Res_Template").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.Res_Template> Res_TemplateReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.Res_Template>("Pm2Model.FK__Res_Templ__Templ__61674175", "Res_Template");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.Res_Template>("Pm2Model.FK__Res_Templ__Templ__61674175", "Res_Template", value);
                }
            }
        }
    }
}

