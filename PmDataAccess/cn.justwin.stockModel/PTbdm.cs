namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class PTbdm
    {
        private string _c_sfyx;
        private string _corpcode;
        private int _i_bmdm;
        private int _i_jb;
        private int _i_sjdm;
        private int _i_xh;
        private int _i_xjbm;
        private string _v_bmbm;
        private string _v_bmjx;
        private string _v_bmmc;
        private string _v_bmqc;

        public string c_sfyx
        {
            get
            {
                return this._c_sfyx;
            }
            set
            {
                this._c_sfyx = value;
            }
        }

        public string CorpCode
        {
            get
            {
                return this._corpcode;
            }
            set
            {
                this._corpcode = value;
            }
        }

        public int i_bmdm
        {
            get
            {
                return this._i_bmdm;
            }
            set
            {
                this._i_bmdm = value;
            }
        }

        public int i_jb
        {
            get
            {
                return this._i_jb;
            }
            set
            {
                this._i_jb = value;
            }
        }

        public int i_sjdm
        {
            get
            {
                return this._i_sjdm;
            }
            set
            {
                this._i_sjdm = value;
            }
        }

        public int i_xh
        {
            get
            {
                return this._i_xh;
            }
            set
            {
                this._i_xh = value;
            }
        }

        public int i_xjbm
        {
            get
            {
                return this._i_xjbm;
            }
            set
            {
                this._i_xjbm = value;
            }
        }

        public string v_bmbm
        {
            get
            {
                return this._v_bmbm;
            }
            set
            {
                this._v_bmbm = value;
            }
        }

        public string v_bmjx
        {
            get
            {
                return this._v_bmjx;
            }
            set
            {
                this._v_bmjx = value;
            }
        }

        public string V_BMMC
        {
            get
            {
                return this._v_bmmc;
            }
            set
            {
                this._v_bmmc = value;
            }
        }

        public string v_bmqc
        {
            get
            {
                return this._v_bmqc;
            }
            set
            {
                this._v_bmqc = value;
            }
        }
    }
}

