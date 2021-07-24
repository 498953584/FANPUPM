namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class AffairModel
    {
        private string _AffairClass = "";
        private string _AffairTitle = "";
        private int _CA;
        private string _Context = "";
        private DateTime _Date = DateTime.Now;
        private int _filesType;
        private string _Flage = "10";
        private Guid _i_id = Guid.Empty;
        private int _mark;
        private Guid _PrjCode = Guid.Empty;
        private string _Remark = "";

        public string AffairClass
        {
            get
            {
                return this._AffairClass;
            }
            set
            {
                this._AffairClass = value;
            }
        }

        public string AffairTitle
        {
            get
            {
                return this._AffairTitle;
            }
            set
            {
                this._AffairTitle = value;
            }
        }

        public int CA
        {
            get
            {
                return this._CA;
            }
            set
            {
                this._CA = value;
            }
        }

        public string Context
        {
            get
            {
                return this._Context;
            }
            set
            {
                this._Context = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                this._Date = value;
            }
        }

        public int FilesType
        {
            get
            {
                return this._filesType;
            }
            set
            {
                this._filesType = value;
            }
        }

        public string Flage
        {
            get
            {
                return this._Flage;
            }
            set
            {
                this._Flage = value;
            }
        }

        public Guid i_id
        {
            get
            {
                return this._i_id;
            }
            set
            {
                this._i_id = value;
            }
        }

        public int Mark
        {
            get
            {
                return this._mark;
            }
            set
            {
                this._mark = value;
            }
        }

        public Guid PrjCode
        {
            get
            {
                return this._PrjCode;
            }
            set
            {
                this._PrjCode = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }
    }
}

