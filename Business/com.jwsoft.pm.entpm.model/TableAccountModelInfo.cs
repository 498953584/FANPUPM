namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class TableAccountModelInfo
    {
        private string _filepath;
        private string _flag;
        private int _id;
        private string _modelname;

        public string FilePath
        {
            get
            {
                return this._filepath;
            }
            set
            {
                this._filepath = value;
            }
        }

        public string Flag
        {
            get
            {
                return this._flag;
            }
            set
            {
                this._flag = value;
            }
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string ModelName
        {
            get
            {
                return this._modelname;
            }
            set
            {
                this._modelname = value;
            }
        }
    }
}

