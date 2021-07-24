namespace cn.justwin.stockDAL.PTBULLETINMAIN
{
    using System;

    [Serializable]
    public class PTBulletinMainModel
    {
        private int _auditstate;
        private string _corpcode;
        private string _deptrange;
        private DateTime _dtm_expriesdate;
        private DateTime _dtm_releasetime;
        private Guid _i_bulletinid;
        private int _i_releasebound;
        private string _url;
        private string _v_content;
        private string _v_releaseuser;
        private string _v_relusercode;
        private string _v_title;

        public int AuditState
        {
            get
            {
                return this._auditstate;
            }
            set
            {
                this._auditstate = value;
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

        public string DeptRange
        {
            get
            {
                return this._deptrange;
            }
            set
            {
                this._deptrange = value;
            }
        }

        public DateTime DTM_EXPRIESDATE
        {
            get
            {
                return this._dtm_expriesdate;
            }
            set
            {
                this._dtm_expriesdate = value;
            }
        }

        public DateTime DTM_RELEASETIME
        {
            get
            {
                return this._dtm_releasetime;
            }
            set
            {
                this._dtm_releasetime = value;
            }
        }

        public Guid I_BULLETINID
        {
            get
            {
                return this._i_bulletinid;
            }
            set
            {
                this._i_bulletinid = value;
            }
        }

        public int I_RELEASEBOUND
        {
            get
            {
                return this._i_releasebound;
            }
            set
            {
                this._i_releasebound = value;
            }
        }

        public string URL
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }

        public string V_CONTENT
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

        public string V_RELEASEUSER
        {
            get
            {
                return this._v_releaseuser;
            }
            set
            {
                this._v_releaseuser = value;
            }
        }

        public string V_RELUSERCODE
        {
            get
            {
                return this._v_relusercode;
            }
            set
            {
                this._v_relusercode = value;
            }
        }

        public string V_TITLE
        {
            get
            {
                return this._v_title;
            }
            set
            {
                this._v_title = value;
            }
        }
    }
}

