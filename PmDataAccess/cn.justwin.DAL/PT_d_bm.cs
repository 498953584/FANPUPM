namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="PT_d_bm")]
    public class PT_d_bm : EntityObject
    {
        private string _adss;
        private string _c_sfyx;
        private string _CorpCode;
        private string _fx;
        private int _i_bmdm;
        private int? _i_jb;
        private int? _i_sjdm;
        private int? _i_xh;
        private int? _i_xjbm;
        private string _v_bmbm;
        private string _v_bmjx;
        private string _V_BMMC;
        private string _v_bmqc;
        private string _yb;

        public static PT_d_bm CreatePT_d_bm(int i_bmdm)
        {
            return new PT_d_bm { i_bmdm = i_bmdm };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string adss
        {
            get
            {
                return this._adss;
            }
            set
            {
                this.ReportPropertyChanging("adss");
                this._adss = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("adss");
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_OrgDi__OrgId__31B8F9AA", "Bud_OrgDiaryCost"), SoapIgnore, DataMember, XmlIgnore]
        public EntityCollection<cn.justwin.DAL.Bud_OrgDiaryCost> Bud_OrgDiaryCost
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_OrgDiaryCost>("Pm2Model.FK__Bud_OrgDi__OrgId__31B8F9AA", "Bud_OrgDiaryCost");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_OrgDiaryCost>("Pm2Model.FK__Bud_OrgDi__OrgId__31B8F9AA", "Bud_OrgDiaryCost", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string c_sfyx
        {
            get
            {
                return this._c_sfyx;
            }
            set
            {
                this.ReportPropertyChanging("c_sfyx");
                this._c_sfyx = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("c_sfyx");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string CorpCode
        {
            get
            {
                return this._CorpCode;
            }
            set
            {
                this.ReportPropertyChanging("CorpCode");
                this._CorpCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("CorpCode");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string fx
        {
            get
            {
                return this._fx;
            }
            set
            {
                this.ReportPropertyChanging("fx");
                this._fx = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("fx");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int i_bmdm
        {
            get
            {
                return this._i_bmdm;
            }
            set
            {
                if (this._i_bmdm != value)
                {
                    this.ReportPropertyChanging("i_bmdm");
                    this._i_bmdm = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("i_bmdm");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? i_jb
        {
            get
            {
                return this._i_jb;
            }
            set
            {
                this.ReportPropertyChanging("i_jb");
                this._i_jb = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("i_jb");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? i_sjdm
        {
            get
            {
                return this._i_sjdm;
            }
            set
            {
                this.ReportPropertyChanging("i_sjdm");
                this._i_sjdm = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("i_sjdm");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? i_xh
        {
            get
            {
                return this._i_xh;
            }
            set
            {
                this.ReportPropertyChanging("i_xh");
                this._i_xh = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("i_xh");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? i_xjbm
        {
            get
            {
                return this._i_xjbm;
            }
            set
            {
                this.ReportPropertyChanging("i_xjbm");
                this._i_xjbm = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("i_xjbm");
            }
        }

        [DataMember, SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "用户_部门", "PT_yhmc")]
        public EntityCollection<cn.justwin.DAL.PT_yhmc> PT_yhmc
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.PT_yhmc>("Pm2Model.用户_部门", "PT_yhmc");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.PT_yhmc>("Pm2Model.用户_部门", "PT_yhmc", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string v_bmbm
        {
            get
            {
                return this._v_bmbm;
            }
            set
            {
                this.ReportPropertyChanging("v_bmbm");
                this._v_bmbm = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("v_bmbm");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string v_bmjx
        {
            get
            {
                return this._v_bmjx;
            }
            set
            {
                this.ReportPropertyChanging("v_bmjx");
                this._v_bmjx = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("v_bmjx");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string V_BMMC
        {
            get
            {
                return this._V_BMMC;
            }
            set
            {
                this.ReportPropertyChanging("V_BMMC");
                this._V_BMMC = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("V_BMMC");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string v_bmqc
        {
            get
            {
                return this._v_bmqc;
            }
            set
            {
                this.ReportPropertyChanging("v_bmqc");
                this._v_bmqc = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("v_bmqc");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string yb
        {
            get
            {
                return this._yb;
            }
            set
            {
                this.ReportPropertyChanging("yb");
                this._yb = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("yb");
            }
        }
    }
}

