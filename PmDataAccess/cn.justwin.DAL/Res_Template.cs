namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="Res_Template")]
    public class Res_Template : EntityObject
    {
        private DateTime? _InputDate;
        private string _InputUser;
        private bool? _IsValid;
        private int? _StartRowIndex;
        private string _TemplateId;
        private string _TemplateName;

        public static Res_Template CreateRes_Template(string templateId, string templateName)
        {
            return new Res_Template { TemplateId = templateId, TemplateName = templateName };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? InputDate
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string InputUser
        {
            get
            {
                return this._InputUser;
            }
            set
            {
                this.ReportPropertyChanging("InputUser");
                this._InputUser = StructuralObject.SetValidValue(value, true);
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

        [SoapIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Res_Templ__Templ__61674175", "Res_TemplateItem"), DataMember, XmlIgnore]
        public EntityCollection<cn.justwin.DAL.Res_TemplateItem> Res_TemplateItem
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Res_TemplateItem>("Pm2Model.FK__Res_Templ__Templ__61674175", "Res_TemplateItem");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Res_TemplateItem>("Pm2Model.FK__Res_Templ__Templ__61674175", "Res_TemplateItem", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? StartRowIndex
        {
            get
            {
                return this._StartRowIndex;
            }
            set
            {
                this.ReportPropertyChanging("StartRowIndex");
                this._StartRowIndex = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("StartRowIndex");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public string TemplateId
        {
            get
            {
                return this._TemplateId;
            }
            set
            {
                if (this._TemplateId != value)
                {
                    this.ReportPropertyChanging("TemplateId");
                    this._TemplateId = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TemplateId");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string TemplateName
        {
            get
            {
                return this._TemplateName;
            }
            set
            {
                this.ReportPropertyChanging("TemplateName");
                this._TemplateName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TemplateName");
            }
        }
    }
}

