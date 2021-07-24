namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class News
    {
        private string _imageURL;
        private string m_c_sfyx;
        private string m_c_ttbj;
        private string m_c_xwlxdm;
        private DateTime m_dtm_fbsj = new DateTime(0x76c, 1, 1);
        private int m_i_xw_id;
        private string m_txt_xwnr;
        private string m_v_xwbt;

        public string c_sfyx
        {
            get
            {
                return this.m_c_sfyx;
            }
            set
            {
                this.m_c_sfyx = value;
            }
        }

        public string c_ttbj
        {
            get
            {
                return this.m_c_ttbj;
            }
            set
            {
                this.m_c_ttbj = value;
            }
        }

        public string c_xwlxdm
        {
            get
            {
                return this.m_c_xwlxdm;
            }
            set
            {
                this.m_c_xwlxdm = value;
            }
        }

        public DateTime dtm_fbsj
        {
            get
            {
                return this.m_dtm_fbsj;
            }
            set
            {
                this.m_dtm_fbsj = value;
            }
        }

        public int i_xw_id
        {
            get
            {
                return this.m_i_xw_id;
            }
            set
            {
                this.m_i_xw_id = value;
            }
        }

        public string imageURL
        {
            get
            {
                return this._imageURL;
            }
            set
            {
                this._imageURL = value;
            }
        }

        public string txt_xwnr
        {
            get
            {
                return this.m_txt_xwnr;
            }
            set
            {
                this.m_txt_xwnr = value;
            }
        }

        public string v_xwbt
        {
            get
            {
                return this.m_v_xwbt;
            }
            set
            {
                this.m_v_xwbt = value;
            }
        }
    }
}

