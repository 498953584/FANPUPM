namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class PTDbsjModel
    {
        private string _c_openflag;
        private DateTime _dtm_dbsj;
        private int _i_dbsj_id;
        private string _i_xgid;
        private string _v_content;
        private string _v_dblj;
        private string _v_lxbm;
        private string _v_tplj;
        private string _v_yhdm;

        public string C_OpenFlag
        {
            get
            {
                return this._c_openflag;
            }
            set
            {
                this._c_openflag = value;
            }
        }

        public DateTime DTM_DBSJ
        {
            get
            {
                return this._dtm_dbsj;
            }
            set
            {
                this._dtm_dbsj = value;
            }
        }

        public int I_DBSJ_ID
        {
            get
            {
                return this._i_dbsj_id;
            }
            set
            {
                this._i_dbsj_id = value;
            }
        }

        public string I_XGID
        {
            get
            {
                return this._i_xgid;
            }
            set
            {
                this._i_xgid = value;
            }
        }

        public string V_Content
        {
            get
            {
                return this._v_content;
            }
            set
            {
                this._v_content = value;
            }
        }

        public string V_DBLJ
        {
            get
            {
                return this._v_dblj;
            }
            set
            {
                this._v_dblj = value;
            }
        }

        public string V_LXBM
        {
            get
            {
                return this._v_lxbm;
            }
            set
            {
                this._v_lxbm = value;
            }
        }

        public string V_TPLJ
        {
            get
            {
                return this._v_tplj;
            }
            set
            {
                this._v_tplj = value;
            }
        }

        public string V_YHDM
        {
            get
            {
                return this._v_yhdm;
            }
            set
            {
                this._v_yhdm = value;
            }
        }
    }
}

